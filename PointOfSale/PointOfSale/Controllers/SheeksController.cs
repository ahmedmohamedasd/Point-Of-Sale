using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PointOfSale.Data;
using PointOfSale.Infrastructure;
using PointOfSale.Models;
using PointOfSale.ViewModel;

namespace PointOfSale.Controllers
{
    public class SheeksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SheeksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sheeks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sheeks.ToListAsync());
        }
        
        //Assign to Guest
        [HttpGet]
        public IActionResult AssignToGuest()
        {
            ViewData["sheekId"] = new SelectList(_context.Sheeks, "Id", "GuestName");
            return View();
        }
       //Assign to Guest
        [HttpPost]
        public async Task<IActionResult> AssignToGuest(AssignToSheek assignToSeek)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            List<AssignToSheek> listSheek = new List<AssignToSheek>();
           for(int i = 0; i < cart.Count; i++)
            {
                listSheek.Add(new AssignToSheek(cart[i]));
            }
            var sheek = _context.AssignToSheeks.FirstOrDefault(c => c.sheekId == assignToSeek.sheekId);
            if(sheek == null)
            {
                for(int i = 0; i < listSheek.Count; i++)
                {
                    listSheek[i].sheekId = assignToSeek.sheekId;
                    await _context.AssignToSheeks.AddAsync(listSheek[i]);
                    await _context.SaveChangesAsync();
                }
                var sheekhaveData = _context.Sheeks.FirstOrDefault(c => c.Id == assignToSeek.sheekId);
                sheekhaveData.HaveData = true;
                _context.Sheeks.Update(sheekhaveData);
                _context.SaveChanges();
            }
            else
            {
                for(int i = 0; i < listSheek.Count; i++)
                {
                    var sheekid = _context.AssignToSheeks.FirstOrDefault(c => c.ProductId == listSheek[i].ProductId);
                    if(sheekid == null)
                    {
                        listSheek[i].sheekId = assignToSeek.sheekId;
                        await _context.AssignToSheeks.AddAsync(listSheek[i]);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        sheekid.Quantity = sheekid.Quantity + listSheek[i].Quantity;
                        _context.AssignToSheeks.Update(sheekid);
                        _context.SaveChanges();
                    }
                }
            }
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ViewSheek(int id)
        {
            var sheekname = _context.Sheeks.FirstOrDefault(c => c.Id == id).GuestName;
            ViewBag.SheekId = id;
            var sheek = await _context.AssignToSheeks.Where(c => c.sheekId == id).Include(c => c.sheek).ToListAsync();
            ViewBag.SheekName = sheekname;
            SheekViewModel model = new SheekViewModel
            {
                AssignToSheeks = sheek,
                GrandTotal = sheek.Sum(c => c.Price * c.Quantity)
            };
            return View(model);
        }
        public async Task<IActionResult> DeleteItem(int id)
        {
            var assigntoSheek = _context.AssignToSheeks.FirstOrDefault(c => c.Id == id);
            
            var sheekid = assigntoSheek.sheekId;
            _context.AssignToSheeks.Remove(assigntoSheek);
            await _context.SaveChangesAsync();
            var sheekInDb = _context.AssignToSheeks.FirstOrDefault(c => c.sheekId == sheekid);
            if(sheekInDb == null)
            {
                var model = _context.Sheeks.FirstOrDefault(c => c.Id == sheekid);
                model.HaveData = false;
                _context.Sheeks.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("ViewSheek",new { id=sheekid});
        }

        // Clear Sheek Data
        public IActionResult ClearSheekData(int id)
        {
            var sheek = _context.Sheeks.FirstOrDefault(c => c.Id == id);
            if(sheek == null)
            {
                return NotFound();
            }
            var assigntoSheek = _context.AssignToSheeks.Where(c=>c.sheekId == id).ToList();
            for(int i=0;i<assigntoSheek.Count; i++)
            {
                _context.AssignToSheeks.Remove(assigntoSheek[i]);
                _context.SaveChanges();
            }
            sheek.HaveData = false;
            _context.Sheeks.Update(sheek);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult CheckOut(int id)
        {
            var sheekData = _context.AssignToSheeks.Where(c => c.sheekId == id).ToList();
            if(sheekData.Count == 0)
            {
                return NotFound();
            }
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            for(int i = 0; i < sheekData.Count; i++)
            {
                CartItem model = new CartItem
                {
                    ProductId = sheekData[i].ProductId,
                    Price = sheekData[i].Price,
                    ProductName = sheekData[i].ProductName,
                    Quantity = sheekData[i].Quantity,
                };
                cart.Add(model);
            }
            HttpContext.Session.SetJson("Cart", cart);
            if (HttpContext.Request.Headers["X-Requested-With"] != "XMLHttpRequest")
                return RedirectToAction("Index","Cart");
            return ViewComponent("SmallCart");
        }
        // GET: Sheeks/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GuestName")] Sheek sheek)
        {
            if (ModelState.IsValid)
            {
                sheek.HaveData = false;
                _context.Add(sheek);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sheek);
        }

        // GET: Sheeks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sheek = await _context.Sheeks.FindAsync(id);
            if (sheek == null)
            {
                return NotFound();
            }
            return View(sheek);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Sheek sheek)
        {
            if (id != sheek.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sheek);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SheekExists(sheek.Id))
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
            return View(sheek);
        }

        // POST: Sheeks/Delete/5
        
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var sheek = await _context.Sheeks.FindAsync(id);
            _context.Sheeks.Remove(sheek);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SheekExists(int id)
        {
            return _context.Sheeks.Any(e => e.Id == id);
        }
    }
}
