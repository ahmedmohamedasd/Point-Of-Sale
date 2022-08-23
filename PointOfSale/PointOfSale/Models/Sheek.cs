using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSale.Models
{
    public class Sheek
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Guest Name is Required")]
        [Display(Name = "Guest Name")]
        //[Remote(action: "ISCategoryExist" , controller:"Categories" , ErrorMessage ="This Category Already Exist")]
        public string GuestName { get; set; }
        [Display(Name ="Have Item")]
        public bool HaveData { get; set; } = false;

        public ICollection<AssignToSheek> AssignToSeeks{get;set;}
    }
}
