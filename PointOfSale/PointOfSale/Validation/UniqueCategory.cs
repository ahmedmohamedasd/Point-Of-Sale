using PointOfSale.Data;
using PointOfSale.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSale.Validation
{
    public class UniqueCategory :ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var context = (ApplicationDbContext)validationContext
                         .GetService(typeof(ApplicationDbContext));
            var category = (Category)(validationContext.ObjectInstance);
            if (category.Id == 0)
            {
                var checkCategory = context.Categories.FirstOrDefault(c => c.CategoryName.ToUpper() == category.CategoryName.ToUpper());
                return (checkCategory == null) ? ValidationResult.Success : new ValidationResult("this Category Already Exist");

            }
            else
            {
                var checkCategory = context.Categories.FirstOrDefault(c => c.CategoryName.ToUpper() == category.CategoryName.ToUpper() && c.Id !=category.Id);
                return (checkCategory == null) ? ValidationResult.Success : new ValidationResult("this Category Already Exist");
            }
            

        }
    }
}
