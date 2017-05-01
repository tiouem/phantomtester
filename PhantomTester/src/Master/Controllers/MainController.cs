using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Master.Controllers
{
    public class MainController : Controller
    {
        /// <summary>
        /// POST method for receiving and proccessing the command
        /// </summary>
        /// <param name="request"></param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Route("main")]
        public IActionResult Post([FromBody]Request request)
        {
            return Ok(request);
        }
    }
}
