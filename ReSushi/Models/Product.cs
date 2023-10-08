

using System.ComponentModel.DataAnnotations;

namespace ReSushi.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string ? Name { get; set; }
        public int  CategoryId { get; set; }
        public string ? Detail { get; set; }
        public string ? ImageUrl { get; set; }
        public DateTime DOJ { get; set; }

        //public int CategoryId { get; set; }
        public Category Category { get; set; }


    }

}
