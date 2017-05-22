using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using Microsoft.ServiceBus.Messaging;
using Model;

namespace Master.Controllers
{
    public class MainController : Controller
    {
        private static readonly String _pttoken = "pttoken";
        private EventWaitHandle _waitHandle;

        private static String _queueName = "ptqueue";

        private static String _connectionString =
            "Endpoint=sb://ptservicebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=QIphzMy8SYdjw7N8N26isODvtGgyeU7azbSai3ZvRiE=";

        private readonly IMemoryCache _memoryCach;
        private Response _response = null;

        private PhantomTesterContext _db = new PhantomTesterContext();

        public MainController(IMemoryCache memoryCache)
        {
            _memoryCach = memoryCache;
        }

        /// <summary>
        /// POST method for receiving and proccessing the command
        /// </summary>
        /// <param name="request"></param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Route("main")]
        public IActionResult PostRequest([FromBody] Request request)
        {
            if (!Request.Headers.ContainsKey(_pttoken))
            {
                return BadRequest("pttoken not specified");
            }

            if (request == null)
            {
                return BadRequest("Body format of the request is wrong");
            }

            TelemetryClient telemetryClient = new TelemetryClient();

            try
            {
                Guid tokenGuid = Guid.Parse(Request.Headers[_pttoken]);
                var queryToken =
                    from t in _db.Tokens
                    where t.GuidToken == tokenGuid
                    select t;

                if (!queryToken.Any())
                {
                    return BadRequest("Incorrect pttoken");
                }

                Token token = queryToken.FirstOrDefault();

                var queryMaxUsages =
                    from s in _db.Subscriptions
                    where s.Id == token.SubscriptionId
                    select s.Limit;

                if (!queryMaxUsages.Any())
                {
                    return StatusCode(500, "Subscription not found");
                }

                int limit = queryMaxUsages.FirstOrDefault();

                if (token.Usages >= limit)
                {
                    return BadRequest("Your subscription limit was exceeded");
                }

                _db.Tokens.Find(token.Id).Usages++;
                _db.SaveChanges();

            }
            catch (Exception e)
            {
                telemetryClient.TrackException(e);
                return StatusCode(500, e.Message);
            }

            request.Timestamp = DateTime.Now;
            request.Guid = Guid.NewGuid();

            QueueClient client = QueueClient.CreateFromConnectionString(_connectionString, _queueName);
            BrokeredMessage message = new BrokeredMessage(request);
            client.Send(message);

            _memoryCach.Set<Model.Response>(request.Guid, null, TimeSpan.FromMinutes(2));
            _waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset, request.Guid.ToString());
            telemetryClient.TrackEvent(String.Format(@"{0} Message {1} sent to queue", request.Guid, request.Name));

            //Wait for the response using the same timeout as for the cache
            _waitHandle.WaitOne(TimeSpan.FromMinutes(2));

            Response response;
            if (_memoryCach.TryGetValue(request.Guid, out response))
            {
                _memoryCach.Remove(request.Guid);
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("response")]
        public IActionResult PostResponse([FromBody]Response response)
        {
            if (response != null)
            {
                TelemetryClient telemetryClient = new TelemetryClient();

                _memoryCach.Set(response.Guid, response, TimeSpan.FromMinutes(2));
                _waitHandle = new EventWaitHandle(true, EventResetMode.AutoReset, response.Guid.ToString());
                _waitHandle.Set();
                telemetryClient.TrackEvent(String.Format(@"{0} Message received from worker", response.Guid));

                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("iamalive")]
        public IActionResult Get()
        {
            return Ok();
        }

    }
}
