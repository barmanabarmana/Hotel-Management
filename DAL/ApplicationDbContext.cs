using Entities;
using Entities.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public class ApplicationDbContext : IdentityDbContext<Customer, Role, int>
    {
        public ApplicationDbContext()
               : base()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public string? ConnectionString { get; private set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string constring = null;
            if (ConnectionString == null)
            {
                var builder = new ConfigurationBuilder();

                builder.SetBasePath(Directory.GetCurrentDirectory());

                builder.AddJsonFile("appsettings.json");

                var config = builder.Build();

                constring = config.GetConnectionString("DefaultConnection");
            }
            else
            {
                constring = ConnectionString;
            }

            optionsBuilder
                .UseSqlServer(constring)
                .UseLazyLoadingProxies();
        }
    }
}