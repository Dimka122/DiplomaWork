using ReSushi.Models;

namespace ReSushi.Repository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategory();
        Task<Category> GetCategoryByID(int ID);
        Task<Category> InsertCategory(Category objCategory);
        Task<Category> UpdateCategory(Category objCategory);
        bool DeleteCategory(int ID);
    }
}
