using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSale.Models.ViewModel
{
    public class AllRolesViewModel
    {
        public AllRolesViewModel()
        {
            allRoles = new List<Role>();
        }
        public AddUserViewModel addUserViewModel { get; set; }
        public IEnumerable<Role> allRoles { get; set; }
    }
}
