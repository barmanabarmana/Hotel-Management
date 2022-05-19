using Entities;
using Entities.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public class ApplicationDbContext : IdentityDbContext<Customer, Role, int>
    {
        private string _connectionString;
        public ApplicationDbContext()
               : base()
        {
        }
        public ApplicationDbContext(string connectionString)
            : base()
        {
            _connectionString = connectionString;
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Hotel> Hotels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string constring = null;
            if (_connectionString == null)
            {
                var builder = new ConfigurationBuilder();

                builder.SetBasePath(Directory.GetCurrentDirectory());

                builder.AddJsonFile("appsettings.json");

                var config = builder.Build();

                constring = config.GetConnectionString("DefaultConnection");
            }
            else
            {
                constring = _connectionString;
            }

            optionsBuilder
                .UseSqlServer(constring)
                .UseLazyLoadingProxies();
        }
    }
}