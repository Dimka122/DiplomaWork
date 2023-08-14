using SushiStore.Models;
using SushiStore.Models.Pages;

namespace SushiStore.Interfaces
{
    public interface IProduct
    {
        PagedList<Product> GetProducts(QueryOptions options);
        IEnumerable<Product> GetAllProducts();

        void AddProduct(Product product);
        Product GetProduct(int id);
        void UpdateProduct(Product product);
        void UpdateAll(Product[] products);
        void DeleteProduct(Product product);
    }
}
