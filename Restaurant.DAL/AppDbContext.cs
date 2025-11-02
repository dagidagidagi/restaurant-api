using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Restaurant.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
namespace Restaurant.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeliveryPosition>().HasKey(u => new { u.DeliveryId, u.PositionId });
        }
        public DbSet<DeliveryPosition> DeliveryPositions { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ReservationsQuery> ReservationsQueries { get; set; }
    }
}
