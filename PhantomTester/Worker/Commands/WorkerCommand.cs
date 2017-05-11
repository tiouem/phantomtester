using System.Runtime.Serialization;
using Model;
using OpenQA.Selenium.PhantomJS;

namespace Worker.Commands
{
    /// <summary>
    /// An abstract class that inherits from the Command model and adds some behaviour.
    /// </summary>
    [DataContract]
    public abstract class WorkerCommand:Command
    {
        protected WorkerCommand(string[] parameters)
        {
            Parameters = parameters;
            Cmd = GetType().Name;
        }

        /// <summary>
        /// Executes the command using the provided PhantomJSDriver.
        /// </summary>
        /// <param name="driver">The driver that will execute the command.</param>
        /// <returns>True if the command was successfully executed.</returns>
        public abstract bool Execute(PhantomJSDriver driver);
    }
}
