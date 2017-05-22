using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Master.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Master
{
    public class PhantomTesterContext : IdentityDbContext<ApplicationUser>
    {
        public static string ConnectionString { get; set; }

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
