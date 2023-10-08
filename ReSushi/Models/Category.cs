using System.ComponentModel.DataAnnotations;

namespace ReSushi.Models
{
    public class Category
    {
        [Key]
        public int idCategory { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<Product> Products { get; set; }



    }

}
