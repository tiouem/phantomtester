using System;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Model;
using Newtonsoft.Json;
using Worker.Converters;

namespace Worker
{
    /// <summary>
    ///     Handles retrieving messages from the request queue and sending the responses to the master.
    /// </summary>
    internal class MessageHandler
    {
        private readonly string _masterUrl = "http://ptmaster.azurewebsites.net/response";
        private readonly string _ptQueue = "ptqueue";
        private QueueClient _ptQueueClient;

        public MessageHandler()
        {
            StartHandler();
        }

        private void StartHandler()
        {
            var manager = NamespaceManager.Create();
            if (!manager.QueueExists(_ptQueue))
                return;
            _ptQueueClient = QueueClient.Create(_ptQueue);
            StartListening();
        }

        /// <summary>
        ///     Starts listening to the request queue.
        /// </summary>
        public void StartListening()
        {
            //TODO:Maybe provide a way to turn it off somehow
            while (true)
                try
                {
                    var message = _ptQueueClient.Receive();
                    //Console.WriteLine("Received message " + message.MessageId);                   
                    if (message != null)
                        Task.Run(() => ProcessMessage(message));
                }
                catch (Exception)
                {
                    //Error handling
                }
        }

        /// <summary>
        ///     Attempts to Deserialize the incoming message to a Request object, then excutes it.
        /// </summary>
        /// <param name="message"></param>
        public void ProcessMessage(BrokeredMessage message)
        {
            WorkerRequest workerRequest = null;
            try
            {
                var request = message.GetBody<Request>();
                Console.WriteLine("Decoded the message " + request.Guid);
                workerRequest = JsonConvert.DeserializeObject<WorkerRequest>(JsonConvert.SerializeObject(request),
                    new CommandConverter());
                message.Complete();
            }
            catch (Exception e)
            {
                //The message could not be parsed. Calls "message.DeadLetter();" so the message isn't read again and again.
                //TODO:Needs a way to let master know that a request was in wrong format so that master can let the user know.
                message.DeadLetter();
                Console.WriteLine("////////////////////EXCEPTION trown: " + e.Message);
                //throw;
            }
            if (workerRequest != null)
            {
                try

                {
                    //Worker implements IDisposable, so the using statement makes sure everything is disposed of.
                    WorkerResponse response;
                    using (var worker = new PhantomWorker())
                    {
                        var tClient = new TelemetryClient();
                        tClient.TrackEvent("Working on " + workerRequest.Guid);
                        response = worker.ExecuteRequest(workerRequest);
                        Console.WriteLine("Response retrieved");
                    }
                    SendResponse(response);
                }
                catch (Exception e)
                {
                    //Something went wrong with the worker.
                    //TODO:Let user know something went wrong and he should try again
                    message.DeadLetter();
                    Console.WriteLine("////////////////////EXCEPTION trown: " + e.Message);
                    //throw;
                }
            }
        }

        /// <summary>
        ///     Sends the response message to the master using HTTP Post.
        /// </summary>
        /// <param name="body">The body of the message. Should be a serialized "Response" model.</param>
        public void SendResponse(WorkerResponse response)
        {
            Task.Run(async () =>
            {
                using (var client = GetClient())
                {
                    var tClient = new TelemetryClient();
                    var postAsJsonAsync = await client.PostAsJsonAsync(_masterUrl, response);
                    var statusCode = postAsJsonAsync.StatusCode;
                    if (statusCode != HttpStatusCode.Accepted)
                    {
                        tClient.TrackEvent("There was an error while sending response " + response.Guid);
                    }
                    tClient.TrackEvent(response.Guid + " Response sent");
                    Console.WriteLine("----- Message sent -----");
                }
            });
        }

        /// <summary>
        ///     Returns an HttpClient thats ready to be used.
        /// </summary>
        /// <returns></returns>
        private HttpClient GetClient()
        {
            var client = new HttpClient {BaseAddress = new Uri(_masterUrl)};
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(0, 0, 30);
            return client;
        }
    }
}