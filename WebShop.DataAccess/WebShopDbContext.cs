using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace WebShop.Repository;

public class WebShopDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
        
    public WebShopDbContext(DbContextOptions<WebShopDbContext> options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Example: Configure relationships or constraints

        modelBuilder.Entity<Product>();

        //modelBuilder.Entity<OrderItem>()
        //    .HasOne(oi => oi.Order)
        //    .WithMany(o => o.OrderItems)
        //    .HasForeignKey(oi => oi.OrderId);

        //modelBuilder.Entity<OrderItem>()
        //    .HasOne(oi => oi.Product)
        //    .WithMany()
        //    .HasForeignKey(oi => oi.ProductId);

        base.OnModelCreating(modelBuilder);
    }
}