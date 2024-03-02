namespace Sushi.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        // Навигационные свойства для определения отношений с категориями
        public virtual ICollection<Category> Categories { get; set; }

        // Навигационные свойства для определения отношений с элементами корзины
        public virtual ICollection<CartItem> CartItems { get; set; }
    }

}
