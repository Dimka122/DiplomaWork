namespace Sushi.Models
{
    public class Category
    {
        
            public int CategoryId { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }

            // Навигационные свойства для определения отношений с продуктами
            public virtual ICollection<Product> Products { get; set; }

    }

}
