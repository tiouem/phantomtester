using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Master.Models;

namespace Master.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int Limit { get; set; }
        public virtual List<Token> Tokens { get; set; }
    }
}
