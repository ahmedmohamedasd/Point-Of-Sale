using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PointOfSale.Models.ViewModel
{
    public class ConsolidateOperation
    {
        [DataType(DataType.Date)]
        public DateTime OrderedDate { get; set; }

        public string ProductName { get; set; }

        public decimal Amount { get; set; }
        public decimal Quantity { get; set; }
        public decimal SubTotal { get { return Amount * Quantity; } }
        

    }
}
