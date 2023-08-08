using SushiStore.Models;

namespace SushiStore.Interfaces
{
    public interface ICategory
    {
        IEnumerable<Category> GetAllCategories();
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
    }
}
