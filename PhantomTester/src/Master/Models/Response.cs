using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Master.Models
{
    public class Response
    {
        public DateTime Timestamp { get; set; }
        public List<string> Assertions { get; set; }
        public string HtmlBody { get; set; }
        public Guid Guid { get; set; }
    }
}
