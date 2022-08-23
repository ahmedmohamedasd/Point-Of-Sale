using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSale.Models
{
    public class AssignToSheek
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get { return Quantity * Price; } }
        public string Image { get; set; }
        [Display(Name = "Sheek Name")]
        public int sheekId { get; set; }

        [ForeignKey("sheekId")]
        public Sheek sheek { get; set; }

        public AssignToSheek()
        {

        }
        public AssignToSheek(CartItem item)
        {
            ProductId = item.ProductId;
            ProductName = item.ProductName;
            Price = item.Price;
            Image = item.Image;
            Quantity = item.Quantity;

        }
    }
}
