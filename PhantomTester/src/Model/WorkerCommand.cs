using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Model
{
    [DataContract]
    public abstract class WorkerCommand
    {
        [DataMember]
        protected readonly string Command;
        [DataMember]
        protected readonly string[] Parameters;
        protected WorkerCommand(string[] parameters)
        {
            Parameters = parameters;
            Command = GetType().Name;
        }
        //public abstract void Execute(PhantomJSDriver driver);
    }
}
