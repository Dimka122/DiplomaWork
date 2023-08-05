using SushiStore.Interfaces;
using SushiStore.Models;

namespace SushiStore.Repository
{
    public class ProductRepository : IProduct
    {
        private List<Product> products;
        public ProductRepository()
        {
            products = new List<Product>();
        }

        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }
    }
}
