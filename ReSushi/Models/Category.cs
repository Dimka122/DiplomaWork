namespace ReSushi.Models
{
    public class Category
    {

        public int idCategory { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }

}
