using SushiStore.Models;
using SushiStore.Models.Pages;

namespace SushiStore.Interfaces
{
    public interface ICategory
    {
        PagedList<Category> GetCategories(QueryOptions options);
        IEnumerable<Category> GetAllCategories();
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
    }
}
