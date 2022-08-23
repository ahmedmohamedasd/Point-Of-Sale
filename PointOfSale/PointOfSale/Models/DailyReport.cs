using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSale.Models
{
    public class DailyReport
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Sub_Total { get; set; }
        public bool IsFree { get; set; }
        public int Sorting { get; set; }

    }
}
