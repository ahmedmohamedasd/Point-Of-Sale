using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointOfSale.Models
{
    public class OperationStock
    {
        [Key]
        public int Id { get; set; }
        public int ContentID { get; set; }


        public int BarItemId { get; set; }

        [Column(TypeName =("decimal(18,3)"))]
        public decimal Amount { get; set; }

        [Column(TypeName = ("decimal(18,3)"))]
        public decimal Quantity { get; set; }

        [Column(TypeName =("decimal(18,3)"))]
        public decimal SubTotal { get { return Amount * Quantity; } }

        [ForeignKey("CartOrderId")]
        public CartItem CartItems { get; set; }
        public int CartOrderId { get; set; }
        
        [DisplayName("Date Of Order")]
        public DateTime DateOfOrder { get; set; }


    }
}
