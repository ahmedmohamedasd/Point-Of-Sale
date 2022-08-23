using Microsoft.AspNetCore.Http;
using PointOfSale.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSale.Models
{
    public class BarItem
    {
        public int Id { get; set; }
        [Required]
        [UniqueProduct]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        
        public Category Category { get; set; }
        [ForeignKey("Category")]
        [Display(Name ="Category Name")]
        public int CategoryId { get; set; }

        [NotMapped]
        [AllowedExtensions(new string[] { ".jpg", ".png" } , ErrorMessage ="Img Extension should be .jpg or png")]
        public IFormFile PhotoUpload { get; set; }
    }
}
