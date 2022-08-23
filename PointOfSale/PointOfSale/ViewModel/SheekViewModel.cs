using PointOfSale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSale.ViewModel
{
    public class SheekViewModel
    {
        public List<AssignToSheek> AssignToSheeks { get; set; }
        public decimal GrandTotal { get; set; }
    }
}
