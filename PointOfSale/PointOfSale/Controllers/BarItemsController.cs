using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PointOfSale.Data;
using PointOfSale.Models;
using PointOfSale.Models.ViewModel;
using PointOfSale.ViewModel;

namespace PointOfSale.Controllers
{
    [Authorize]
    public class BarItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment hostingEnviroment;

        public BarItemsController(ApplicationDbContext context , IHostingEnvironment hostingEnviroment)
        {
            _context = context;
            this.hostingEnviroment = hostingEnviroment;
        }
    
        // GET: BarItems
        [Authorize(Policy = "BarItemsShow")]
        public async Task<IActionResult> Index(int p=1)
        {
            
            return View(await PaginatedList<BarItem>.CreateAsync(_context.BarItems.Include(b => b.Category).OrderBy(c=>c.Category.Sorting), p, 8));
        }
        [HttpGet]
        [Route("/BarItems/ProductsByCategory/{id?}/{p?}")]
        public async Task<IActionResult> ProductsByCategory(int id, int p = 1)
        {
            Category category = _context.Categories.Find(id);

            if (category == null)
            {
                return RedirectToAction("Index");
            }
            var items = _context.BarItems.OrderByDescending(c => c.Id)
                                        .Where(c => c.CategoryId == category.Id);
            ViewBag.CategoryName = category.CategoryName;
            ViewBag.CategoryId = category.Id;
            return View(await PaginatedList<BarItem>.CreateAsync(items, p, 8));

        }

        [Authorize(Policy = "BarItemsAdd")]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName");
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "BarItemsAdd")]
        public async Task<IActionResult> Create(BarItem model)
        {
            if (ModelState.IsValid)
            {
                var prductname = _context.BarItems.FirstOrDefault(c => c.Name.ToLower() == model.Name.ToLower());
                if(prductname == null)
                {
                    string uniqueFile = "no_image.jpg";
                    if (model.PhotoUpload != null)
                    {
                        string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "images"); // Get Root To Image Folder
                        uniqueFile = Guid.NewGuid().ToString() + "_" + model.PhotoUpload.FileName;// Get a Unique Path For Image
                        string filePath = Path.Combine(uploadsFolder, uniqueFile);
                        model.PhotoUpload.CopyTo(new FileStream(filePath, FileMode.Create));

                    }
                    model.ImagePath = uniqueFile;


                    _context.Add(model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "This Prduct is Exist ");
                }
                    
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", model.CategoryId);
            return View(model);
        }

        
        [Authorize(Policy = "BarItemsEdit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var barItem = await _context.BarItems.FindAsync(id);
            if (barItem == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", barItem.CategoryId);
            return View(barItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "BarItemsEdit")]
        public async Task<IActionResult> Edit(int id, BarItem barItem , string image)
        {
            if (id != barItem.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try

                {
                    barItem.ImagePath = image;
                    if (barItem.PhotoUpload != null)
                    {
                        string uploadDir = Path.Combine(hostingEnviroment.WebRootPath, "images");
                        if (!String.Equals(barItem.ImagePath, "no_image.jpg"))
                        {if (image == null)
                            {
                                image = "No_image.png";
                            }
                                string oldPath = Path.Combine(uploadDir,image);
                            if (System.IO.File.Exists(oldPath))
                            {
                                System.IO.File.Delete(oldPath);
                            }
                        }
                        string imageName = Guid.NewGuid().ToString() + "_" + barItem.PhotoUpload.FileName;
                        string filePath = Path.Combine(uploadDir, imageName);
                        FileStream fs = new FileStream(filePath, FileMode.Create);
                        await barItem.PhotoUpload.CopyToAsync(fs);
                        fs.Close();
                        barItem.ImagePath = imageName;
                    }
                    
                    _context.Update(barItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BarItemExists(barItem.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", barItem.CategoryId);
            barItem.ImagePath = image;
            return View(barItem);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "BarItemsDelete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var barItem = await _context.BarItems.FindAsync(id);
            if (barItem == null)
                return NotFound();

            if (!String.Equals(barItem.ImagePath, "no_image.jpg"))
            {
                string uploadsDir = Path.Combine(hostingEnviroment.WebRootPath, "images");
                string oldPath = Path.Combine(uploadsDir, barItem.ImagePath);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }
            }
            var contentItems = _context.Contents.Where(c => c.BarId == barItem.Id).ToList();
            if(contentItems != null)
            {
                for(int i = 0; i < contentItems.Count; i++)
                {
                    _context.Contents.Remove(contentItems[i]);
                    await _context.SaveChangesAsync();
                }
            }
            var contentSecond = _context.Contents.Where(c => c.ContentId == barItem.Id).ToList();
            if(contentSecond != null)
            {
                for (int i = 0; i < contentSecond.Count; i++)
                {
                    _context.Contents.Remove(contentSecond[i]);
                    await _context.SaveChangesAsync();
                }
            }
            _context.BarItems.Remove(barItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        private bool BarItemExists(int id)
        {
            return _context.BarItems.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int id)
        {
            var bartems = _context.BarItems.FirstOrDefault(c => c.Id == id);
            if (bartems == null)
                return NotFound();
            var itemList = _context.BarItems.Include(c => c.Category).Where(x => x.Id != id).ToList();
            var contentModel = _context.Contents.Where(c => c.BarId == id).ToList();
            if (contentModel.Count > 0)
            {
                for (int i = 0; i < contentModel.Count; i++)
                {
                    var item = _context.BarItems.FirstOrDefault(c => c.Id == contentModel[i].ContentId);
                    var index = itemList.IndexOf(item);
                    itemList.RemoveAt(index);
                }
            }
            ViewData["ContentId"] = new SelectList(itemList.OrderBy(c => c.Category.Sorting), "Id", "Name");

            var contentList = await _context.Contents.Include(c => c.BarItem2).Where(c => c.BarId == id).ToListAsync();
            ViewBag.ItemName = bartems.Name;
            ViewBag.ItemId = id;
            Content model = new Content
            {
                BarId = id
            };
            BarContentViewModel vm = new BarContentViewModel
            {
                contentId = model,
                ContentList = contentList
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrEdit(BarContentViewModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Contents.Add(model.contentId);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");

        }
        //Content Function 
        [HttpGet]
        [Authorize(Policy = "BarItemsShow")]
        public async Task<IActionResult> Content(int id)
        {

            var bartems = _context.BarItems.FirstOrDefault(c => c.Id == id);
            if (bartems == null)
                return NotFound();
            var itemList = _context.BarItems.Include(c => c.Category).Where(x => x.Id != id).ToList();
            var contentModel = _context.Contents.Where(c => c.BarId == id).ToList();
            if (contentModel.Count > 0)
            {
                for (int i = 0; i < contentModel.Count; i++)
                {
                    var item = _context.BarItems.FirstOrDefault(c => c.Id == contentModel[i].ContentId);
                    var index = itemList.IndexOf(item);
                    itemList.RemoveAt(index);
                }
            }
            ViewData["ContentId"] = new SelectList(itemList.OrderBy(c => c.Category.Sorting), "Id", "Name");

            var contentList = await _context.Contents.Include(c=>c.BarItem2).Where(c=>c.BarId == id).ToListAsync();
            ViewBag.ItemName = bartems.Name;
            ViewBag.ItemId = id;
            Content model = new Content
            {
                BarId = id
            };
            BarContentViewModel vm = new BarContentViewModel
            {
                contentId = model,
                ContentList = contentList
            };
            return View(vm);

        }
        [HttpPost]
        [Authorize(Policy = "BarItemsAdd")]
        public async Task<IActionResult> AddToContent( BarContentViewModel model)
        {
            _context.Contents.Add(model.contentId);
            await _context.SaveChangesAsync();
            return RedirectToAction( "Content", new { id = model.contentId.BarId });
        }
        [HttpPost]
        [Authorize(Policy = "BarItemsDelete")]
        public async Task<IActionResult> DeleteContent(int id , int contentId)
        {

            var content = _context.Contents.FirstOrDefault(c => c.BarId == id && c.ContentId == contentId);
            if (content == null)
                return NotFound();
            _context.Contents.Remove(content);
            await _context.SaveChangesAsync();
            return RedirectToAction("Content", new { id = id });
        }

    }
}
