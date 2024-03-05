using SushiStore.Domain.Abstract;
using SushiStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiStore.Domain.Concrete
{
    public class EFSushiRepository : ISushiRepository
    {
        EFDbContext context=new EFDbContext();
        public IEnumerable<Sushi> Sushis
        {
            get {  return context.Sushis; }
        }
    }
}
