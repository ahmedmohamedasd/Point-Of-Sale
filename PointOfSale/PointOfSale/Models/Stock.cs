using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSale.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public BarItem BarItem { get; set; }
        [ForeignKey("BarItem")]
        public int ProductId { get; set; }
        [Column(TypeName ="decimal(18,3)")]
        public decimal Quantity { get; set; }
    }
}
