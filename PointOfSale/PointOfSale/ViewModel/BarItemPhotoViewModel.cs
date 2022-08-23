using Microsoft.AspNetCore.Http;
using PointOfSale.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSale.ViewModel
{
    public class BarItemPhotoViewModel
    {
       
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        public IFormFile Photo { get; set; }

        public Category Category { get; set; }
        [ForeignKey("Category")]
        [Display(Name = "Category Name")]
        public int CategoryId { get; set; }
    }
}
