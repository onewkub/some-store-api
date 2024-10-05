using Microsoft.EntityFrameworkCore;
using SomeStoreAPI.Models;

namespace SomeStoreAPI.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Image> Images { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=products.db");  // Specify the SQLite database file
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Tags)
                .WithMany(t => t.Products)
                .UsingEntity(j => j.ToTable("ProductTags"));

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Categories)
                .WithMany(c => c.Products)
                .UsingEntity(j => j.ToTable("ProductCategories"));

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Images)
                .WithOne(i => i.Product)
                .HasForeignKey(i => i.ProductId);

            // Configure Price as an owned entity
            modelBuilder.Entity<Product>()
                .OwnsOne(p => p.Price);

            modelBuilder.Entity<Product>()
                .OwnsOne(p => p.OriginalPrice);

            modelBuilder.Entity<Product>()
                .OwnsOne(p => p.FullPriceBeforeOverallDiscount);

            modelBuilder.Entity<Product>()
                .OwnsOne(p => p.PossibleDiscountPrice);

            modelBuilder.Entity<Product>()
                .OwnsOne(p => p.Brand);

            base.OnModelCreating(modelBuilder);
        }



    }
}
