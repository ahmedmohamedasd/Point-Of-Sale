using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PointOfSale.Data;
using PointOfSale.Models;

namespace PointOfSale.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Categories
        [Authorize(Policy = "CategriesShow")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.OrderBy(c =>c.Sorting).ToListAsync());
        }
        [AcceptVerbs("Get","Post")]
        [AllowAnonymous]
        public async Task<IActionResult> ISCategoryExist(string CategoryName)
        {
            var category = await _context.Categories.FindAsync(CategoryName);
            if(category == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"this {CategoryName} Category Already Exist ! ");
            }
        }

        // GET: Categories/Create
        [Authorize(Policy = "CategriesAdd")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CategriesAdd")]
        public async Task<IActionResult> Create([Bind("Id,CategoryName,Sorting")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        [Authorize(Policy = "CategriesEdit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CategriesEdit")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryName,Sorting")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CategriesDelete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            var item = _context.BarItems.FirstOrDefault(c => c.CategoryId == category.Id);
            if(item != null)
            {
                ViewBag.ErrorTitle = $"Category<<<   {category.CategoryName}   >>>> Cannot be deleted";
                ViewBag.ErrorMessage = "This Category has Item so it cannot be deleted";
                ViewBag.returnUrl = Request.Headers["Referer"].ToString();
                ModelState.AddModelError(string.Empty,"This Category has Item in it ");
                return View("Error");
            }
           
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [AllowAnonymous]
        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
