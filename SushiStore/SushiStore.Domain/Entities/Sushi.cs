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
        [Required(ErrorMessage = "Пожалуйста, введите название продукта")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Пожалуйста, введите описание продукта")]
        public string Description { get; set; }

        [Display(Name = "Категория")]
        [Required(ErrorMessage = "Пожалуйста, укажите категорию продукта")]
        public string Category { get; set; }

        [Display(Name = "Цена (грн.)")]
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Пожалуйста, введите положительное значение для цены")]
        public decimal Price { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}
