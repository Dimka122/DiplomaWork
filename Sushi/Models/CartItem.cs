namespace Sushi.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }

        public int Quantity { get; set; }

        // Навигационные свойства для связи с продуктом и корзиной
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int CartId { get; set; }
        public virtual Cart Cart { get; set; }
    }

}
