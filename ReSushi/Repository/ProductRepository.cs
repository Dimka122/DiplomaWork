using Microsoft.EntityFrameworkCore;
using ReSushi.Models;

namespace ReSushi.Repository
{
    public class ProductRepository:IProductRepository
    {
        private readonly EFDataContext _efDataContext;
        public ProductRepository(EFDataContext context)
        {
            _efDataContext = context??
                throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _efDataContext.Products.ToListAsync();
        }
        public async Task<Product> GetProductByID(int ID)
        {
            return await _efDataContext.Products.FindAsync(ID);
        }
        public async Task<Product> InsertProduct(Product objProduct)
        {
            _efDataContext.Products.Add(objProduct);
            await _efDataContext.SaveChangesAsync();
            return objProduct;
        }
        public async Task<Product> UpdateProduct(Product objProduct)
        {
            _efDataContext.Entry(objProduct).State = EntityState.Modified;
            await _efDataContext.SaveChangesAsync();
            return objProduct;
        }
        public bool DeleteProduct(int ID)
        {
            bool result = false;
            var product = _efDataContext.Products.Find(ID);
            if (product != null)
            {
                _efDataContext.Entry(product).State = EntityState.Deleted;
                _efDataContext.SaveChanges();
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
    }
}
