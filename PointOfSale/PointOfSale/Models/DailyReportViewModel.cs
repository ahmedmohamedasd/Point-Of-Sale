using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSale.Models
{
    public class DailyReportViewModel
    {
        public IEnumerable<DailyReport> DailyReports { get; set; }
        public IEnumerable<DailyReport> FreeDailyReports { get; set; }

        public int TotalQuantity { get; set; }
        public int TotalFreeQuantity { get; set; }
        public decimal TotalAmount { get; set; }
        
    }
}
