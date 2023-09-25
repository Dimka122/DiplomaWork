

using System.ComponentModel.DataAnnotations;

namespace ReSushi.Models
{
    public class Product
    {
        [Key]
        public int idProduct { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Detail { get; set; }
        public DateTime DOJ { get; set; }


        
    }

}
