using Microsoft.EntityFrameworkCore;
using Store3x.Services.ProductAPI.Models;

namespace Store3x.Services.ProductAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Seller> Sellers { get; set;}
        public DbSet<Category> Categories { get; set;}
        public DbSet<Wishlist> Wishlists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>()
                .HasKey(c => new { c.buyer_id, c.product_id });
            
            modelBuilder.Entity<Wishlist>()
                .HasKey(c => new { c.buyer_id, c.product_id });
        }

    }

}
