using AstraTech.Routeplanner.Models;
using Microsoft.EntityFrameworkCore;

namespace AstraTech.Routeplanner.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Station> Stations => Set<Station>();
        public DbSet<Connection> Connections => Set<Connection>();
        public DbSet<TransportRoute> TransportRoutes => Set<TransportRoute>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TransportRoute>()
                .HasOne(r => r.StartStation)
                .WithMany()
                .HasForeignKey(r => r.StartStationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TransportRoute>()
                .HasOne(r => r.DestinationStation)
                .WithMany()
                .HasForeignKey(r => r.DestinationStationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Connection>()
                .HasOne(c => c.FromStation)
                .WithMany()
                .HasForeignKey(c => c.FromStationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Connection>()
                .HasOne(c => c.ToStation)
                .WithMany()
                .HasForeignKey(c => c.ToStationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}