using SushiStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiStore.Domain.Abstract
{
    public interface ISushiRepository
    {
        IEnumerable<Sushi>Sushis {  get; }
    }
}
