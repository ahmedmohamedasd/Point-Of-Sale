using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSale.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage ="You Should Enter Full Name")]
        [MinLength(4 ,ErrorMessage ="Min length of Charachter shoulld be atleast 4 ")]
        public string FullName { get; set; }
        public bool IsActive { get; set; }
    }
}
