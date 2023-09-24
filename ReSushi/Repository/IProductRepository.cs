using ReSushi.Models;

namespace ReSushi.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProductByID(int ID);
        Task<Product> InsertProduct(Product objProduct);
        Task<Product> UpdateProduct(Product objProduct);
        bool DeleteProduct(int ID);
    }
}
