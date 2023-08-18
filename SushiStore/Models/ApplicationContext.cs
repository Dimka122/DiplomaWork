using Microsoft.EntityFrameworkCore;

namespace SushiStore.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> context) : base(context)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasIndex(e => e.Name);
            modelBuilder.Entity<Product>().HasIndex(e => e.Detail);
            modelBuilder.Entity<Product>().HasIndex(e => e.RetailPrice);
            modelBuilder.Entity<Category>().HasIndex(e => e.Name);
            modelBuilder.Entity<Category>().HasIndex(e => e.Description);
        }
    }
}
