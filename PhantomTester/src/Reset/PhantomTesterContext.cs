using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Reset.Models;

namespace Reset
{
    public class PhantomTesterContext : IdentityDbContext<ApplicationUser>
    {
        public static string ConnectionString { get { return "Server=tcp:ptdatabase.database.windows.net,1433;Initial Catalog=phantomtesterdb;Persist Security Info=False;User ID=ptadmin;Password=Tomas123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"; } }

        public PhantomTesterContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        public DbSet<Token> Tokens { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        
    }
}
