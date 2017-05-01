using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model
{
    public class Token
    {
        public int Id { get; set; }
        public Guid GuidToken { get; set; }
        public int SubscriptionId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int SubscritionId { get; set; }
        public virtual Subscription Subscription { get; set; }
    }
}
