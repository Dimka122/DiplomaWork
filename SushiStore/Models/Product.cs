namespace SushiStore.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Detail { get; set; }
        public decimal RetailPrice { get; set; }
    }
}
