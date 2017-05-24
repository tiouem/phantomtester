using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Client.Models
{
    public class UserDetailVM
    {
        public string Usename { get; set; }

        public string TokenGuid { get; set; }
        public int TokenUsage { get; set; }

        public string SubscriptionName { get; set; }
        public int SubscriptionLimit { get; set; }

        public Table Bible{ get; set; }
    }

}
