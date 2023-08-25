using Microsoft.AspNetCore.Mvc;
using ReSushi.Models;
using ReSushi.Models.Pages;

namespace ReSushi.interfaces
{
    public interface IProduct
{
    PagedList<Product> GetProducts(QueryOptions options, int category = 0);
    IEnumerable<Product> GetAllProducts();
    Product GetProduct(int id);
    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void UpdateAll(Product[] products);
    void DeleteProduct(Product product);
}
}
