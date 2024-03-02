namespace Sushi.Models
{
    public class Cart
    {
        public int CartId { get; set; }

        // Другие свойства, если необходимо

        // Навигационные свойства для определения отношений
        public virtual ICollection<CartItem> CartItems { get; set; }
    }

}
