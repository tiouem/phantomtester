using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DataContract]
    public class Command
    {
        [DataMember]
        public string Cmd { get; set; }
        [DataMember]
        public string[] Parameters { get; set; }
    }
}
