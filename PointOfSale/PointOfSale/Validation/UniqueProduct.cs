using PointOfSale.Data;
using PointOfSale.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSale.Validation
{
    public class UniqueProduct : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var context = (ApplicationDbContext)validationContext
                         .GetService(typeof(ApplicationDbContext));
            var baritem = (BarItem)(validationContext.ObjectInstance);
            if (baritem.Id == 0)
            {
                var checkItem = context.BarItems.FirstOrDefault(c => c.Name.ToUpper() == baritem.Name.ToUpper());
                return (checkItem == null) ? ValidationResult.Success : new ValidationResult("this Product Already Exist");

            }
            else
            {
                var checkItem = context.BarItems.FirstOrDefault(c => c.Name.ToUpper() == baritem.Name.ToUpper()  && c.Id != baritem.Id);
                return (checkItem == null) ? ValidationResult.Success : new ValidationResult("this Product Already Exist");

            }
          

        }
    }
}
