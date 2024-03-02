using Microsoft.EntityFrameworkCore;

namespace Sushi.Models
{
    public class VDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Cart> Carts { get; set; }

        public VDbContext(DbContextOptions<VDbContext> options) : base(options) { }

        // Дополнительная конфигурация моделей, если необходимо
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Пример конфигурации внешнего ключа для связи продуктов и категорий
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Categories)
                .WithMany(c => c.Products)
                .UsingEntity(j => j.ToTable("ProductCategory"));

            // Другие настройки моделей, если необходимо
            base.OnModelCreating(modelBuilder);
        }
    }
}
