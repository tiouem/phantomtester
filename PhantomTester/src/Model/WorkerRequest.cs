using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Model
{

    public class WorkerRequest
    {
        public string Name { get; set; }
        public string RootUrl { get; set; }
        public ResponseType ResponseType { get; }
        public List<WorkerCommand> Commands { get; }

        public WorkerRequest(string requestName, string rootUrl, ResponseType responseType)
        {
            Name = requestName;
            RootUrl = rootUrl;
            ResponseType = responseType;
            Commands = new List<WorkerCommand>();
        }

        public void AddCommand(WorkerCommand command)
        {
            Commands.Add(command);
        }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ResponseType
    {
        Assertion,
        Html
    }
}
