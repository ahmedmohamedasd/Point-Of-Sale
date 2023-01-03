using PointOfSale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSale.ViewModel
{
    public class BarContentViewModel
    {
        public Content contentId { get; set; }
        public List<Content> ContentList { get; set; }
        public decimal Quantity { get; set; }
    }
}
