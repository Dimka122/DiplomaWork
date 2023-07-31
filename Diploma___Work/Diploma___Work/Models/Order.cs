namespace Diploma___Work.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Ссылка на пользователя, сделавшего заказ
        public List<Sushi> SushiItems { get; set; } // Список суши в заказе
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string DeliveryAddress { get; set; }
        public bool IsDelivered { get; set; }
    }
}
