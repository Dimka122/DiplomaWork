using Microsoft.EntityFrameworkCore;
using ReSushi.Models;

namespace ReSushi.Repository
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly EFDataContext _efDataContext;
        public CategoryRepository(EFDataContext context) 
        {
            _efDataContext=context??
                throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Category>> GetCategoryes()
        {
            return await _efDataContext.Categories.ToListAsync();
        }
        public async Task<Category> GetCategoryByID(int ID)
        {
            return await _efDataContext.Categories.FindAsync(ID);
        }
        public async Task<Category> InsertCategory(Category objCategory)
        {
            _efDataContext.Categories.Add(objCategory);
            await _efDataContext.SaveChangesAsync();
            return objCategory;
        }
        public async Task<Category> UpdateCategory(Category objCategory)
        {
            _efDataContext.Entry(objCategory).State = EntityState.Modified;
            await _efDataContext.SaveChangesAsync();
            return objCategory;
        }
        public bool DeleteCategory(int ID)
        {
            bool result = false;
            var category = _efDataContext.Categories.Find(ID);
            if (category != null)
            {
                _efDataContext.Entry(category).State = EntityState.Deleted;
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
