using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;

namespace Diploma___Work.Models
{
    public class ApplicationDbContext : DbContext
    {
    public DbSet<Sushi> Sushi { get; set; }
    // public DbSet<ApplicationUser> Users { get; set; }
    // public DbSet<Order> Orders { get; set; }
    }
}