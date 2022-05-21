using Entities;
using Entities.Hotels;
using Entities.Transports;
using Entities.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public class ManagementDbContext : IdentityDbContext<Customer, Role, int>
    {
        public ManagementDbContext()
               : base()
        {
           /* Database.EnsureDeleted();
            Database.EnsureCreated();*/
        }
        public ManagementDbContext(DbContextOptions<ManagementDbContext> options)
               : base(options)
        {
            /*Database.EnsureDeleted();
            Database.EnsureCreated();*/
        }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<Tour> TourTemplates { get; set; }
        public DbSet<Transport> Transports { get; set; }
        public DbSet<TransportPlace> TransportPlaces { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder();

            builder.SetBasePath(Directory.GetCurrentDirectory());

            builder.AddJsonFile("appsettings.json");

            var config = builder.Build();

            string connectionString = config.GetConnectionString("ManagementConnection");

            optionsBuilder
                .UseSqlServer(connectionString)
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

            modelBuilder.Entity<Tour>()
                .HasOne(t => t.TransportIn)
                .WithMany(t => t.Tours)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }
    }
}