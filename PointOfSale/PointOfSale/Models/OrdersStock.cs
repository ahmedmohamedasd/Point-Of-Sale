using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSale.Models
{
    public class OrdersStock
    {
        public int Id { get; set; }
        public BarItem BarItem { get; set; }
        [ForeignKey("BarItem")]
        [Display(Name ="Bar Item")]
        public int BarItemId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Display(Name = "Date Of Order")]
        public DateTime DateOfOrder { get; set; }
       
    }
}
