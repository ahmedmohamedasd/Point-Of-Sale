using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PointOfSale.Data;
using PointOfSale.Models;
using PointOfSale.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSale.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProductsController(ApplicationDbContext _context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = _context;
            this.webHostEnvironment = webHostEnvironment;
        }
        [Authorize(Policy = "CartShow")]
        public async Task<IActionResult> Index(int p=1)
        {
            return View(await PaginatedList<BarItem>.CreateAsync(context.BarItems.OrderBy(c=>c.Category.Sorting) , p , 12));
        }
        [Authorize(Policy = "CartShow")]
        [Route("/Products/ProductsByCategory/{id?}/{p?}")]
        public async Task<IActionResult> ProductsByCategory(int id , int p = 1)
        {
            Category category = context.Categories.Find(id);

            if(category == null)
            {
                return RedirectToAction("Index");
            }
            var items = context.BarItems.OrderByDescending(c => c.Id)
                                        .Where(c => c.CategoryId == category.Id);
            ViewBag.CategoryName = category.CategoryName;
            ViewBag.CategoryId = category.Id;
            return View(await PaginatedList<BarItem>.CreateAsync(items, p, 12));

        }
    }
}
