using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Master.Controllers
{
    public class MainController : Controller
    {
        /// <summary>
        /// POST method for receiving and proccessing the command
        /// </summary>
        /// <param name="command"></param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Route("main")]
        public IActionResult Post([FromBody] JArray command)
        {
            return Ok(command);
        }
    }
}
