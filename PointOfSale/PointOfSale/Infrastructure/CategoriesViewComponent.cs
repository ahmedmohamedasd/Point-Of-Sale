using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PointOfSale.Data;
using PointOfSale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSale.Infrastructure
{
    public class CategoriesViewComponent :ViewComponent
    {
        private readonly ApplicationDbContext context;
        public CategoriesViewComponent( ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await GetCategoriesAsync();
            return View(categories);
        }
        private Task<List<Category>> GetCategoriesAsync()
        {
            return context.Categories.Where(c=>c.CategoryName != "NOTFORPAYING").OrderBy(x => x.Sorting).ToListAsync();
        }
    }
}
