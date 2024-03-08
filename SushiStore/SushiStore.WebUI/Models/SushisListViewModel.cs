using SushiStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SushiStore.WebUI.Models
{
    public class SushisListViewModel
    {
        public IEnumerable<Sushi> Sushis { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}