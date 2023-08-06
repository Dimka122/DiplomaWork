using SushiStore.Models;

namespace SushiStore.Interfaces
{
    public interface IProduct
    {
        IEnumerable<Product> GetAllProducts();
        void AddProduct(Product product);
        Product GetProduct(int id);
        void UpdateProduct(Product product);
    }
}
