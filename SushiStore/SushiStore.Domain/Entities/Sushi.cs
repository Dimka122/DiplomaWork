using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SushiStore.Domain.Entities
{
    public class Sushi
    {
        [HiddenInput(DisplayValue = false)]
        public int SushiId { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Категория")]
        public string Category { get; set; }

        [Display(Name = "Цена (грн.)")]
        public decimal Price { get; set; }
    }
}
