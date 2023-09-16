using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace ReSushi.Models
{
    public class EFDataContext : IdentityDbContext<IdentityUser>
    {
        public EFDataContext(DbContextOptions<EFDataContext> options)
              : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            ////config primary key(product,category)
            //modelBuilder.Entity<Product>().HasKey(s => s.idProduct);
            //modelBuilder.Entity<Category>().HasKey(s => s.idCategory);

            ////set config replationship Product vs Category
            //modelBuilder.Entity<Category>()
            //    .HasMany<Product>(s => s.Products)
            //    .WithOne(a => a.Category)
            //    .HasForeignKey(a => a.idCategory)
            //    .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
