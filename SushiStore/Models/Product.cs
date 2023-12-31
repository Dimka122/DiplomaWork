﻿using SushiStore.Models.Pages;

namespace SushiStore.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sostav { get; set; }
        public string Detail { get; set; }
        public decimal RetailPrice { get; set; }

        public  int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
