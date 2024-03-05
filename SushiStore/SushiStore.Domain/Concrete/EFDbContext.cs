using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SushiStore.Domain.Entities;
using System.Data.Entity;

namespace SushiStore.Domain.Concrete
{
    public class EFDbContext:DbContext
    {
        public DbSet<Sushi> Sushis {  get; set; }
    }
}
