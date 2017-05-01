using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Response
    {
        public DateTime Timestamp { get; set; }
        public List<string> Assertions { get; set; }
        public string HtmlBody { get; set; }
    }
}
