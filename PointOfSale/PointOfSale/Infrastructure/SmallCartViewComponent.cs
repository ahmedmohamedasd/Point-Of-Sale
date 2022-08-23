using Microsoft.AspNetCore.Mvc;
using PointOfSale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSale.Infrastructure
{
    public class SmallCartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            List<CartItem> cartItems = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            SmallCartViewModel smallCartVm;
            if(cartItems == null || cartItems.Count == 0)
            {
                smallCartVm = null;
            }
            else
            {
                smallCartVm = new SmallCartViewModel
                {
                    NumberOfItems = cartItems.Sum(c => c.Quantity),
                    TotalAmount = cartItems.Sum(c => c.Quantity * c.Price)

                };
            }
            return View(smallCartVm);
        }
    }
}
