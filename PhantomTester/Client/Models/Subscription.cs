using System;
using System.Collections.Generic;

namespace Client.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int Limit { get; set; }
        public virtual List<Token> Tokens { get; set; }
    }
}
