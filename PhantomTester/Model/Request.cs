using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Request
    {
        public string Name { get; set; }
        public string RootUrl { get; set; }
        public bool ReturnHtml { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid Guid { get; set; }
        public List<Command> Commands { get; set; }
    }
}
