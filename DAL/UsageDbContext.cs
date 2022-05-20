using Entities;
using Entities.Hotels;
using Entities.Transports;
using Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public class UsageDbContext : DbContext
    {
        private string _connectionString;
        public UsageDbContext()
               : base()
        {
            Database.EnsureCreated();
        }
        public UsageDbContext(string connectionString)
            : base()
        {
            _connectionString = connectionString;
        }

        public DbSet<Customer> Users { get; set; }
        public DbSet<HotelRoomReservation> HotelRoomReservations { get; set; }
        public DbSet<TransportTicket> TransportTickets { get; set; }
        public DbSet<Tour> OrderedTours { get; set; }

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

