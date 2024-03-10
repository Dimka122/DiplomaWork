using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiStore.Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();
        public void AddItem(Sushi sushi,int quantity)
        {
            CartLine line=lineCollection
                .Where(g=>g.Sushi.SushiId==sushi.SushiId)
                .FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Sushi = sushi,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public void RemoveLine(Sushi sushi)
        {
            lineCollection.RemoveAll(l=>l.Sushi.SushiId == sushi.SushiId);
        }
        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Sushi.Price * e.Quantity);
        }
        public void Clear()
        { lineCollection.Clear(); }
        public IEnumerable<CartLine> Lines { get { return lineCollection; } }


        public class CartLine
        {
           public Sushi Sushi { get; set; }
           public int Quantity { get; set; }
        }
    }
}
