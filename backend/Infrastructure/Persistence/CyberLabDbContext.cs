using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class CyberLabDbContext : DbContext
{
    public CyberLabDbContext(
        DbContextOptions<CyberLabDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);

            entity.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasIndex(u => u.Username)
                .IsUnique();

            entity.Property(u => u.PasswordHash)
                .IsRequired();
        });
        
        
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(p => p.Price)
                .HasPrecision(10, 2);
        });

    }
}