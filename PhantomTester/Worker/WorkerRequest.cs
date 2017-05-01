using System.Collections.Generic;
using Model;
using Worker.Commands;

namespace Worker
{
    public class WorkerRequest:Request
    {
        public new List<WorkerCommand> Commands { get; }
        public WorkerRequest(string requestName, string rootUrl, bool returnHtml)
        {
            Name = requestName;
            RootUrl = rootUrl;
            ReturnHtml = returnHtml;
            Commands = new List<WorkerCommand>();
        }

        public void AddCommand(WorkerCommand command)
        {
            Commands.Add(command);
        }
    }
}
