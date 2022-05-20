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
        public UsageDbContext()
               : base()
        {
            /*Database.EnsureDeleted();
            Database.EnsureCreated();*/
        }
        public UsageDbContext(DbContextOptions<UsageDbContext> options)
               : base(options)
        {
            /*Database.EnsureDeleted();
            Database.EnsureCreated();*/
        }

        public DbSet<Customer> Users { get; set; }
        public DbSet<HotelRoomReservation> HotelRoomReservations { get; set; }
        public DbSet<TransportTicket> TransportTickets { get; set; }
        public DbSet<Tour> OrderedTours { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var builder = new ConfigurationBuilder();

            builder.SetBasePath(Directory.GetCurrentDirectory());

            builder.AddJsonFile("appsettings.json");

            var config = builder.Build();

            string connectionString = config.GetConnectionString("UsageConnection");

            optionsBuilder
            .UseSqlServer(connectionString)
            .UseLazyLoadingProxies();
        }
    }
}

