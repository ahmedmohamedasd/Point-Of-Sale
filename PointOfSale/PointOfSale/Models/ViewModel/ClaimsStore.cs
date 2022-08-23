using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PointOfSale.Models.ViewModel
{
    public class ClaimsStore
    {
        public static List<Claim> AllClaims = new List<Claim>()
        {
            new Claim("BarItems","BarItems"),
            new Claim("Categries","Categries"),
            new Claim("Cart","Cart"),
            new Claim("Stock","Stock"),
            new Claim("Users","Users"),
            new Claim("Role","Role")

        };
     } 
}
