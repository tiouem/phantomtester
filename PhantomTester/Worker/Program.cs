using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Worker.Converters;

namespace Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test string to see if request object is correctly serialized from json (this could be the incoming message received from a queue)
            var jsonRequestString =
                @"{""Name"":""Test google search""," +
                   @"""RootUrl"":""https://www.google.dk/""," +
                   @"""ReturnHtml"":true," +
                   @"""Commands"":[" +
                   @"{""Command"":""GiveInput"",""Parameters"":[""name"",""q"",""Hello World!""]}," +
                   @"{""Command"":""ClickElement"",""Parameters"":[""name"",""btnG""]}," +
                   @"{""Command"":""ElementExists"",""Parameters"":[""""]}]}";

            var request = JsonConvert.DeserializeObject<WorkerRequest>(jsonRequestString, new CommandConverter());
        }
    }
}
