using Microsoft.EntityFrameworkCore;

namespace SushiStore.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> context) : base(context)
        {

        }
        public DbSet<Product> Products { get; set; }
    }
}
