namespace ReSushi.Models
{
    public class Product
    {
        public int idProduct { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Detail { get; set; }
        public string? ImageUrl { get; set; }


        public int idCategory { get; set; }
        public Category Category { get; set; }
    }

}
