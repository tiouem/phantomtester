using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User
    {
        public int Id { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public int TokenId { get; set; }
        public virtual Token Token { get; set; }
    }
}
