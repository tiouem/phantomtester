using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Client.Models;

namespace Client.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Subscription>()
                .HasMany(s => s.Tokens)
                .WithOne(t => t.Subscription)
                .HasForeignKey(t => t.SubscriptionId);

            builder.Entity<ApplicationUser>()
                .HasOne(u => u.Token)
                .WithOne(t => t.User)
                .HasForeignKey<Token>(t => t.UserId);


            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
