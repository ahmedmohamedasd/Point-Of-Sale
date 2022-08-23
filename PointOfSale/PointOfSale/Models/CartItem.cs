using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSale.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get { return Quantity * Price; } }
        public string? Image { get; set; }

        public Category Category { get; set; }
        [ForeignKey("Category")]
        [Display(Name = "Category Name")]
        public int CategoryId { get; set; }

        public DateTime DateOfReceipt { get; set; }
        public bool IsFree { get; set; } = false;

        public CartItem()
        {

        }
        public CartItem(BarItem item)
        {
            ProductId = item.Id;
            ProductName = item.Name;
            Price = item.Price;
            Image = item.ImagePath;
            Quantity = 1;
            CategoryId = item.CategoryId;
        }
    }
}
