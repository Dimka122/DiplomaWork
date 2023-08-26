using ReSushi.Models;
using ReSushi.Models.Pages;

namespace ReSushi.interfaces
{
    public interface ICategory
    {
        PagedList<Category> GetCategories(QueryOptions options);
        IEnumerable<Category> GetAllCategories();
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
        Category GetCategory(int id);
    }

}
