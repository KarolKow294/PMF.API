using Microsoft.EntityFrameworkCore;

namespace PMF.API.Entities
{
    public class PmfDbContext(DbContextOptions<PmfDbContext> options) : DbContext
    {
        public DbSet<Order> Order { get; set; }
        public DbSet<Part> Part { get; set; }
        public DbSet<Storage> Storage { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(u => u.Name)
                .IsRequired();

            modelBuilder.Entity<Order>()
                .Property(u => u.Number)
                .IsRequired();

            modelBuilder.Entity<Part>()
                .Property(u => u.Code)
                .IsRequired();

            modelBuilder.Entity<Part>()
                .Property(u => u.Quantity)
                .IsRequired();

            modelBuilder.Entity<Part>()
                .Property(u => u.SurfaceId)
                .IsRequired();

            modelBuilder.Entity<Part>()
                .Property(u => u.ActualStorageId)
                .IsRequired();

            modelBuilder.Entity<Part>()
                .Property(u => u.DestinationStorageId)
                .IsRequired();

            modelBuilder.Entity<Part>()
                .Property(u => u.OrderId)
                .IsRequired();

            modelBuilder.Entity<Storage>()
                .Property(u => u.Name)
                .IsRequired();
        }
    }
}
