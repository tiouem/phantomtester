using System.Runtime.Serialization;
using Model;
using OpenQA.Selenium.PhantomJS;

namespace Worker.Commands
{
    [DataContract]
    public abstract class WorkerCommand:Command
    {
        protected WorkerCommand(string[] parameters)
        {
            Parameters = parameters;
            Cmd = GetType().Name;
        }
        public abstract bool Execute(PhantomJSDriver driver);
    }
}
