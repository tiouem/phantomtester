using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.ServiceBus.Messaging;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Master.Controllers
{
    public class MainController : Controller
    {
        private static String _queueName = "Name";
        private static String _connectionString = "connectionString";
        private readonly IMemoryCache _memoryCach;

        private Response _response = null;

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
        public IActionResult PostRequest([FromBody]Request request)
        {
            if (request == null)
            {
                return BadRequest("Json is in wrong format");
            }

            request.Timestamp = DateTime.Now;
            request.Guid = Guid.NewGuid();

            //QueueClient client = QueueClient.CreateFromConnectionString(_connectionString, _queueName);
            //BrokeredMessage message = new BrokeredMessage(request);
            //client.Send(message);

            _memoryCach.Set(request.Guid, request, TimeSpan.FromMinutes(2));

            while (_response == null && _memoryCach.Get(request.Guid) != null)
            {
                //do nothing
            }

            return Ok(_response);
        }

        [HttpPost]
        [Route("response")]
        public IActionResult PostResponse([FromBody]Response response)
        {
            if (response != null)
            {
                Request request = new Request();
                if (_memoryCach.TryGetValue(response.Guid, out request))
                {
                    _memoryCach.Remove(response.Guid);
                    _response = response;
                    return Ok();
                }
            }
            return BadRequest();
        }
    }
}
