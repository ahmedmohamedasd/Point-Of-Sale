using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointOfSale.Models.ViewModel
{
    public class StockDetailsViewModel
    {
        public List<OrdersStock> OrdersStocks { get; set; }
        public List<ConsolidateOperation> OperationStocks { get; set; }
       
        [Column(TypeName ="decimal(18,3)")]
        public decimal TotalOrders { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal TotalOperation { get; set; }
    }
}
