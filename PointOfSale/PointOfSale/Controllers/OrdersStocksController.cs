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
    public class OrdersStocksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersStocksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OrdersStocks
        [Authorize(Policy = "StockShow")]
        public async Task<IActionResult> Index()
        {
            var stock = await _context.Stocks.Include(c => c.BarItem).OrderBy(c => c.Quantity).ToListAsync();
            return View(stock);
        }
        [AllowAnonymous]
        public async Task<IActionResult> StockContent()
        {           
            var applicationDbContext = _context.OrdersStocks.Include(o => o.BarItem).OrderByDescending(c => c.DateOfOrder);
            return View(await applicationDbContext.ToListAsync());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "StockDelete")]
        public async Task<IActionResult> DeleteStock(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if(stock == null)
            {
                return NotFound();
            }
             _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        // GET: OrdersStocks/Create
        [Authorize(Policy = "StockAdd")]
        public IActionResult Create()
        {
            ViewData["BarItemId"] = new SelectList(_context.BarItems.Include(c=>c.Category).OrderBy(c=>c.Category.Sorting), "Id", "Name");
            OrdersStock vm = new OrdersStock()
            {
                DateOfOrder = DateTime.Now
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "StockAdd")]
        public async Task<IActionResult> Create([Bind("Id,BarItemId,Quantity,DateOfOrder")] OrdersStock ordersStock)
        {
            if (ModelState.IsValid)
            {
                _context.OrdersStocks.Add(ordersStock);
                await _context.SaveChangesAsync();
                var stock = _context.Stocks.FirstOrDefault(c => c.ProductId == ordersStock.BarItemId);
                if (stock == null)
                {
                    Stock model = new Stock()
                    {
                        ProductId = ordersStock.BarItemId,
                        Quantity = ordersStock.Quantity
                    };
                     _context.Stocks.Add(model);
                    _context.SaveChanges();
                }
                else
                {
                    stock.Quantity = stock.Quantity + ordersStock.Quantity;
                    _context.Stocks.Update(stock);
                    _context.SaveChanges();
                }
                return RedirectToAction("StockContent");
            }
            ViewData["BarItemId"] = new SelectList(_context.BarItems, "Id", "Name", ordersStock.BarItemId);
            return View(ordersStock);
        }

        // GET: OrdersStocks/Edit/5
        [Authorize(Policy = "StockEdit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordersStock = await _context.OrdersStocks.FindAsync(id);
            if (ordersStock == null)
            {
                return NotFound();
            }
            ViewData["BarItemId"] = new SelectList(_context.BarItems, "Id", "Name", ordersStock.BarItemId);
            return View(ordersStock);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "StockEdit")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BarItemId,Quantity,DateOfOrder")] OrdersStock ordersStock)
        {
            if (id != ordersStock.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var model = _context.OrdersStocks.AsNoTracking().FirstOrDefault(c => c.Id == id);
                    if(model.Quantity != ordersStock.Quantity || model.BarItemId != ordersStock.BarItemId)
                    {
                        var stock = _context.Stocks.FirstOrDefault(c => c.ProductId == ordersStock.BarItemId);
                        if (stock == null)
                        {
                            Stock stock1 = new Stock()
                            {
                                ProductId = ordersStock.BarItemId,
                                Quantity = ordersStock.Quantity
                            };
                            var oldStock = _context.Stocks.AsNoTracking().FirstOrDefault(c => c.ProductId == model.BarItemId);
                            oldStock.Quantity = oldStock.Quantity - model.Quantity;
                            _context.Stocks.Update(oldStock);
                            _context.SaveChanges();

                            _context.Update(ordersStock);
                            await _context.SaveChangesAsync();

                            _context.Stocks.Add(stock1);
                            _context.SaveChanges();
                        }
                        else
                        {
                            if (model.BarItemId == ordersStock.BarItemId)
                            {
                                if (model.Quantity != ordersStock.Quantity)
                                {
                                    if (ordersStock.Quantity > model.Quantity)
                                    {
                                        stock.Quantity = stock.Quantity + (ordersStock.Quantity - model.Quantity);
                                    }
                                    else
                                    {
                                        stock.Quantity = stock.Quantity - (model.Quantity - ordersStock.Quantity);
                                    }
                                    _context.Stocks.Update(stock);
                                }
                            }
                            else
                            {
                                // Delete Old Stock
                                var oldStock = _context.Stocks.AsNoTracking().FirstOrDefault(c => c.ProductId == model.BarItemId);
                                oldStock.Quantity = oldStock.Quantity - model.Quantity;
                                _context.Stocks.Update(oldStock);
                                _context.SaveChanges();
                                var newStock = _context.Stocks.FirstOrDefault(c => c.ProductId == ordersStock.BarItemId);
                                newStock.Quantity = newStock.Quantity + ordersStock.Quantity;

                                _context.Stocks.Update(newStock);
                                _context.SaveChanges();

                            }
                            _context.Update(ordersStock);
                            await _context.SaveChangesAsync();

                        }
                    }
                    else
                    {
                        _context.Update(ordersStock);
                        await _context.SaveChangesAsync();
                    }
                    
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersStockExists(ordersStock.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("StockContent");
            }
            ViewData["BarItemId"] = new SelectList(_context.BarItems, "Id", "Name", ordersStock.BarItemId);
            return View(ordersStock);
        }


        // POST: OrdersStocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "StockDelete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ordersStock = await _context.OrdersStocks.FindAsync(id);
            var stock = _context.Stocks.FirstOrDefault(c => c.ProductId == ordersStock.BarItemId);
            if (stock != null)
            {
                stock.Quantity = stock.Quantity - ordersStock.Quantity;

                _context.Stocks.Update(stock);
                _context.SaveChanges();
            }
            

            _context.OrdersStocks.Remove(ordersStock);
            await _context.SaveChangesAsync();
            return RedirectToAction("StockContent");
        }
        [AllowAnonymous]
        private bool OrdersStockExists(int id)
        {
            return _context.OrdersStocks.Any(e => e.Id == id);
        }
        [HttpGet]
        public async Task<IActionResult> ExpiredIndex()
        {
            var applicationDbContext = _context.ExpiredStocks.Include(o => o.BarItem).OrderByDescending(c => c.DateOfOrder);
            return View(await applicationDbContext.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> AddOrEditExpired(int id = 0)
        {
            if (id == 0)
            {
                ViewData["BarItemId"] = new SelectList(_context.BarItems.Include(c => c.Category).OrderBy(c => c.Category.Sorting), "Id", "Name");
                ExpiredStock vm = new ExpiredStock();
                vm.DateOfOrder = DateTime.Now;
                ViewBag.Header = "Add Expired Item";
                return View(vm);
            }
            else
            {
                var expired = await _context.ExpiredStocks.FindAsync(id);
                if (expired == null)
                {
                    return NotFound();
                }
                ViewBag.Header = "Edit Expired Item";

                ViewData["BarItemId"] = new SelectList(_context.BarItems.Include(c => c.Category).OrderBy(c => c.Category.Sorting), "Id", "Name");
                return View(expired);
            }

        }
        [HttpPost]
        public async Task<IActionResult> AddOrEditExpired(int id, ExpiredStock expiredStock)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                     _context.ExpiredStocks.Add(expiredStock);
                    await _context.SaveChangesAsync();
                    var stock =  _context.Stocks.FirstOrDefault(c => c.ProductId == expiredStock.BarItemId);
                    if(stock != null)
                    {
                        stock.Quantity = stock.Quantity - expiredStock.Quantity;
                        _context.Stocks.Update(stock);
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    var IndbExpiredItem = _context.ExpiredStocks.AsNoTracking().FirstOrDefault(c => c.Id == id);
                    if (IndbExpiredItem.BarItemId == expiredStock.BarItemId)
                    {
                        var stock = _context.Stocks.FirstOrDefault(c => c.ProductId == expiredStock.BarItemId);
                        if (stock != null && IndbExpiredItem.Quantity != expiredStock.Quantity)
                        {
                            if(expiredStock.Quantity > IndbExpiredItem.Quantity)
                            {
                                stock.Quantity = stock.Quantity - (expiredStock.Quantity-IndbExpiredItem.Quantity);

                            }
                            else
                            {
                                stock.Quantity = stock.Quantity + (IndbExpiredItem.Quantity - expiredStock.Quantity);

                            }
                            _context.Stocks.Update(stock);
                            await _context.SaveChangesAsync();
                        }

                    }
                    else
                    {
                        var stockInDb = _context.Stocks.FirstOrDefault(c => c.ProductId == IndbExpiredItem.BarItemId);
                        if(stockInDb != null)
                        {
                            stockInDb.Quantity = stockInDb.Quantity + IndbExpiredItem.Quantity;
                            _context.Stocks.Update(stockInDb);
                            await _context.SaveChangesAsync();
                        }
                        var realStock = _context.Stocks.FirstOrDefault(c => c.ProductId == expiredStock.BarItemId);
                        if (realStock != null)
                        {
                            realStock.Quantity = realStock.Quantity - expiredStock.Quantity;
                            _context.Stocks.Update(realStock);
                            await _context.SaveChangesAsync();
                        }
                    }
                    
                    
                   
                    _context.ExpiredStocks.Update(expiredStock);
                    await _context.SaveChangesAsync();

                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllExpired", _context.ExpiredStocks.Include(c=>c.BarItem).ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEditExpired", expiredStock) });
        }
        [HttpPost, ActionName("DeleteExpired")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteExpiredConfirmed(int id)
        {
           
            var expired = await _context.ExpiredStocks.FindAsync(id);
            var stock =  _context.Stocks.FirstOrDefault(c => c.ProductId == expired.BarItemId);
            if(stock != null)
            {
                stock.Quantity = stock.Quantity + expired.Quantity;
                _context.Stocks.Update(stock);
                await _context.SaveChangesAsync();
            }
            _context.ExpiredStocks.Remove(expired);
            await _context.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAllExpired", _context.ExpiredStocks.Include(c=>c.BarItem).ToList()) });

        }
    }
}
