using Entities;
using Entities.Transports;
using Entities.Transports;
using Entities.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public class ManagementDbContext : IdentityDbContext<Customer, Role, int>
    {
        private string _connectionString;
        public ManagementDbContext()
               : base()
        {
            Database.EnsureCreated();
        }
        public ManagementDbContext(string connectionString)
            : base()
        {
            _connectionString = connectionString;
        }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<Tour> TourTemplates { get; set; }
        public DbSet<Transport> Transports { get; set; }
        public DbSet<TransportPlace> TransportPlaces { get; set; }

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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>()
            .HasMany(r => r.Rooms)
            .WithOne(h => h.Hotel)
            .HasForeignKey(h => h.HotelId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Transport>()
           .HasMany(t => t.TransportPlaces)
           .WithOne(t => t.Transport)
           .HasForeignKey(t => t.TransportId)
           .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}