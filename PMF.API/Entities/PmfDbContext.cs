using Microsoft.EntityFrameworkCore;

namespace PMF.API.Entities
{
    public class PmfDbContext(DbContextOptions<PmfDbContext> options) : DbContext(options)
    {
        public DbSet<Order> Order { get; set; }
        public DbSet<Part> Part { get; set; }
        public DbSet<Storage> Storage { get; set; }
        public DbSet<PartStorage> PartStorage { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(eb =>
            {
                eb.Property(o => o.Name).IsRequired();
                eb.Property(o => o.Number).IsRequired();
                eb.HasMany(o => o.Parts).WithOne(p => p.Order).HasForeignKey(p => p.OrderId);
            });


            modelBuilder.Entity<Part>(eb =>
            {
                eb.Property(p => p.Code).IsRequired();
                eb.Property(p => p.Quantity).IsRequired();
                eb.Property(p => p.SurfaceId).IsRequired();
                eb.Property(p => p.OrderId).IsRequired();
                eb.HasMany(p => p.Storages).WithMany(s => s.Parts)
                .UsingEntity<PartStorage>(
                    p => p.HasOne(ps => ps.Storage)
                    .WithMany()
                    .HasForeignKey(ps => ps.StorageId),

                    p => p.HasOne(ps => ps.Part)
                    .WithMany()
                    .HasForeignKey(ps => ps.PartId),

                    ps =>
                    {
                        ps.HasKey(x => new { x.StorageId, x.PartId });
                        ps.Property(x => x.Type).IsRequired();
                    });
            });

            modelBuilder.Entity<Storage>()
                .Property(u => u.Name)
                .IsRequired();
        }
    }
}
