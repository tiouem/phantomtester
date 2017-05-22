using System;

namespace Client.Models
{
    public class Token
    {
        public int Id { get; set; }
        public Guid GuidToken { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int SubscriptionId { get; set; }
        public virtual Subscription Subscription { get; set; }
        public int Usages { get; set; }
    }
}
