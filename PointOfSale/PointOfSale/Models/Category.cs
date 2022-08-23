using Microsoft.AspNetCore.Mvc;
using PointOfSale.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSale.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Category Name is Required")]
        [Display(Name ="Category Name")]
        //[Remote(action: "ISCategoryExist" , controller:"Categories" , ErrorMessage ="This Category Already Exist")]
        [UniqueCategory]
        public string CategoryName { get; set; }
        [Required]
        public int Sorting { get; set; }

        public ICollection<BarItem> BarItems { get; set; }

    }
}
