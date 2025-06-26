using Microsoft.EntityFrameworkCore;
using RisedorApi.Domain.Entities;

namespace RisedorApi.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Promotion> Promotions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User configuration
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();

        // Order configuration
        modelBuilder
            .Entity<Order>()
            .HasOne(o => o.SalesRep)
            .WithMany()
            .HasForeignKey(o => o.SalesRepUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder
            .Entity<Order>()
            .HasOne(o => o.Supermarket)
            .WithMany()
            .HasForeignKey(o => o.SupermarketUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // OrderItem configuration
        modelBuilder
            .Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.Items)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<OrderItem>()
            .HasOne(oi => oi.Vendor)
            .WithMany()
            .HasForeignKey(oi => oi.VendorId)
            .OnDelete(DeleteBehavior.Restrict);

        // Product configuration
        modelBuilder
            .Entity<Product>()
            .HasOne(p => p.Vendor)
            .WithMany(u => u.Products)
            .HasForeignKey(p => p.VendorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Product>().HasIndex(p => p.UpcCode).IsUnique();

        modelBuilder.Entity<Product>().HasIndex(p => p.ItemCode).IsUnique();

        // Promotion configuration
        modelBuilder
            .Entity<Promotion>()
            .HasOne(p => p.Vendor)
            .WithMany(u => u.Promotions)
            .HasForeignKey(p => p.VendorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
