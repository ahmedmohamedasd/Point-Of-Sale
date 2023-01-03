using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using PointOfSale.Data;
using PointOfSale.Infrastructure;
using PointOfSale.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSale.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IHostingEnvironment hostingEnviroment;
        public CartController(ApplicationDbContext _context , IHostingEnvironment hostingEnviroment)
        {
            this.context = _context; 
            this.hostingEnviroment = hostingEnviroment;
        }
        [Authorize(Policy = "CartShow")]
        public IActionResult Index()
        {

            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            CartViewModel vm;
            bool flage = false;
            DateTime dateTime = DateTime.UtcNow;
            bool free = false;
            for (int i = 0; i < cart.Count; i++)
            {
                if(cart[i].Id != 0)
                {
                    dateTime =(DateTime)cart[i].DateOfReceipt;
                    free = cart[i].IsFree;
                    flage = true;
                    break;
                }
            }
            if(flage == false)
            {
                vm = new CartViewModel()
                {
                    CartItems = cart,
                    GrandTotal = cart.Sum(c => c.Quantity * c.Price),
                    Date = DateTime.UtcNow,
                    IsFree = false
                };
            }
            else
            {
                vm = new CartViewModel()
                {
                    CartItems = cart,
                    GrandTotal = cart.Sum(c => c.Quantity * c.Price),
                    Date = dateTime,
                    IsFree = free
                };
            }
           
            return View(vm);
        }
        [Authorize(Policy = "CartAdd")]
        public async Task<IActionResult> Add(int id)
        {
            BarItem item = await context.BarItems.FindAsync(id);
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            CartItem cartItem = cart.Where(c => c.ProductId == id).FirstOrDefault();
            if(cartItem == null)
            {
                cart.Add(new CartItem(item));
            }
            else
            {
                cartItem.Quantity += 1;
            }
            HttpContext.Session.SetJson("Cart", cart);
            if (HttpContext.Request.Headers["X-Requested-With"] != "XMLHttpRequest")
                return RedirectToAction("Index");
            return ViewComponent("SmallCart");
        }
        [Authorize(Policy = "CartEdit")]
        public async Task<IActionResult> Update(int id)
        {
            CartItem item = await context.CartItems.FindAsync(id);
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            cart.Add(item);
            HttpContext.Session.SetJson("Cart", cart);
            if (HttpContext.Request.Headers["X-Requested-With"] != "XMLHttpRequest")
                return RedirectToAction("Index");
            return ViewComponent("SmallCart");
        }
        [Authorize(Policy = "CartAdd")]
        public IActionResult Decrease(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            CartItem cartItem = cart.Where(c => c.ProductId == id).FirstOrDefault();
            if(cartItem.Quantity > 1)
            {
                --cartItem.Quantity;
            }
            else
            {
                cart.RemoveAll(c=>c.ProductId == id);
            }
            if(cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }
            return RedirectToAction("Index");

        }
        [Authorize(Policy = "CartAdd")]
        public IActionResult Remove(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            // Malo4 lazma El Satr Elly ta7t da
            CartItem cartItem = cart.Where(c => c.ProductId == id).FirstOrDefault();
            cart.RemoveAll(c => c.ProductId == id);
            if(cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }
        [AllowAnonymous]
        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");
            return Redirect(Request.Headers["Referer"].ToString());
        }
        [Authorize(Policy = "CartAdd")]
        public IActionResult Save(DateTime Rdate, bool IsFree)
        {
            //DateTime tdate = ViewBag.TestDate;
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            for(int i = 0; i < cart.Count; i++)
            {
                if(cart[i].Id == 0)
                {
                    cart[i].DateOfReceipt = Rdate;
                    cart[i].IsFree = IsFree;
                    context.CartItems.Add(cart[i]);
                    context.SaveChanges();
                    var stock = context.Stocks.FirstOrDefault(c => c.ProductId == cart[i].ProductId);
                    var barContent = context.Contents.Where(c => c.BarId == cart[i].ProductId).ToList();
                    if (stock != null)
                    {
                        stock.Quantity = stock.Quantity - cart[i].Quantity;
                        context.Stocks.Update(stock);
                        context.SaveChanges();
                        
                    }if(barContent.Count > 0)
                    {
                        for (int j = 0; j < barContent.Count; j++)
                        {
                            var contentStock = context.Stocks.FirstOrDefault(c => c.ProductId == barContent[j].ContentId);
                            if (contentStock != null)
                            {
                                contentStock.Quantity = contentStock.Quantity - cart[i].Quantity;
                                context.Stocks.Update(contentStock);
                                context.SaveChanges();
                            }
                        }
                    }
                   
                }
                else
                {
                    var stock = context.Stocks.FirstOrDefault(c => c.ProductId == cart[i].ProductId);
                    if(stock != null)
                    {
                        var oldCart = context.CartItems.AsNoTracking().FirstOrDefault(c => c.Id == cart[i].Id);
                        if (oldCart.Quantity != cart[i].Quantity)
                        {
                            if (oldCart.Quantity > cart[i].Quantity)
                            {
                                stock.Quantity = stock.Quantity + (oldCart.Quantity - cart[i].Quantity);
                            }
                            else
                            {                       
                                stock.Quantity = stock.Quantity - (cart[i].Quantity -oldCart.Quantity );
                            }
                            context.Stocks.Update(stock);
                            context.SaveChanges();
                           

                        }
                      

                    }
                    var barContent = context.Contents.Where(c => c.BarId == cart[i].ProductId).ToList();

                    if (barContent.Count > 0)
                    {
                        var oldCart = context.CartItems.AsNoTracking().FirstOrDefault(c => c.Id == cart[i].Id);
                        if (oldCart.Quantity != cart[i].Quantity)
                        {
                            
                            for (int j = 0; j < barContent.Count; j++)
                            {
                                var contentStock = context.Stocks.FirstOrDefault(c => c.ProductId == barContent[j].ContentId);
                                if (contentStock != null)
                                {
                                    if (contentStock.Quantity > cart[i].Quantity)
                                    {
                                        contentStock.Quantity = contentStock.Quantity + (oldCart.Quantity - cart[i].Quantity);
                                    }
                                    else
                                    {
                                        contentStock.Quantity = contentStock.Quantity - (cart[i].Quantity - oldCart.Quantity);
                                    }
                                    context.Stocks.Update(contentStock);
                                    context.SaveChanges();
                                }
                            }
                        }
                    }

                    cart[i].DateOfReceipt = Rdate;
                    cart[i].IsFree = IsFree;
                    context.CartItems.Update(cart[i]);
                    context.SaveChanges();

                }

               
            }
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("Index", "Products"); // Redirect(Request.Headers["Referer"].ToString());

            
           
        }
        [AllowAnonymous]
        public IActionResult PrintReceipt()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var stream = new MemoryStream();
            //Bs ana Fannan 
            // Get Photo From Project
            string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "Images"); // Get Root To Image Folder
            string uniqueFile = "ilcampoLogo.jpg";// Get a Unique Path For Image
            string filePath = Path.Combine(uploadsFolder, uniqueFile);
            using (var xlPackage = new ExcelPackage(stream))
            {
                var workSheet = xlPackage.Workbook.Worksheets.Add("Items");
                ExcelPicture pic = workSheet.Drawings.AddPicture("", new FileInfo(filePath));
                int rowIndex = 0;
                int collIndex = 0;
                pic.SetPosition(rowIndex, 0, collIndex, 0);
                pic.SetSize(160, 160);
                workSheet.Cells["D1"].Value = DateTime.Now.ToString();
                workSheet.Cells["D2"].Value = "ILCAMPO";
                workSheet.Cells["D3"].Value = "MASR ASWAN ROAD";
                workSheet.Cells["D4"].Value = "IN FRONT OF ";
                workSheet.Cells["D5"].Value = "Abo Enooomrous Police Office ";
                workSheet.Cells["D6"].Value = "Manil Shiha Giza Egypt";
                workSheet.Cells["D7"].Value = "Tel : 02-37493173";
                workSheet.Cells["D8"].Value = "Site : www.ilcampoeg.com";
                workSheet.Cells["D9"].Value = "EMAIL: reservation@ilcampoeg.com";
                workSheet.Cells["A11"].Value = "ITEM";
                workSheet.Cells["C11"].Value = "QUANTITY";
                workSheet.Cells["D11"].Value = "PRICE";
                workSheet.Cells["E11"].Value = "TOTAL";
                using (var r = workSheet.Cells["A11:E11"])
                {
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(Color.Gray);
                    r.Style.Font.Size = 16;
                    r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                }
                    List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
                int row = 12;
                for (int i = 0; i < cart.Count; i++)
                {
                    workSheet.Cells[string.Format("A{0}", row)].Value = cart[i].ProductName.ToUpper();
                    workSheet.Cells[string.Format("C{0}", row)].Value = cart[i].Quantity;
                    workSheet.Cells[string.Format("C{0}", row)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    workSheet.Cells[string.Format("D{0}", row)].Value = cart[i].Price+ "";
                    workSheet.Cells[string.Format("E{0}", row)].Value =  cart[i].Total +"";

                    row++;
                }
                workSheet.Cells[string.Format("A{0}", row+1)].Value = "Sub Total";
                workSheet.Cells[string.Format("A{0}", row+2)].Value = "SERVICE CHARGE";
                workSheet.Cells[string.Format("A{0}", row+3)].Value = "V.A.Tax";
                workSheet.Cells[string.Format("A{0}", row+4)].Value = "BALANCE DUE";
               // Style For Sub_Total , service charge , tax , balance 
                using (var t = workSheet.Cells[string.Format("A{0}:A{1}", row + 1, row + 4)])
                {
                    t.Style.Font.Bold = true;
                    t.Style.Font.Size = 12;
                }
                    
                decimal sub_total = cart.Sum(c => c.Quantity * c.Price);
                decimal tax = sub_total *(decimal).14;
                workSheet.Cells[string.Format("E{0}", row+1)].Value = cart.Sum(c => c.Quantity * c.Price) +"";
                workSheet.Cells[string.Format("E{0}", row + 2)].Value = "0.00";
                workSheet.Cells[string.Format("E{0}", row + 3)].Value = tax ;
                workSheet.Cells[string.Format("E{0}", row + 4)].Value = cart.Sum(c => c.Quantity * c.Price);
                // Style For output of  Sub_Total , service charge , tax , balance 
                using (var t = workSheet.Cells[string.Format("E{0}:E{1}", row + 1 , row + 4)])
                {
                    t.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                    t.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                    t.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                    t.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                    t.Style.Font.Bold = true;
                    t.Style.Font.Size = 12;
                    t.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                }
                workSheet.Cells[string.Format("A{0}", row + 6)].Value = "CUSTOMER SIGNATURE";
                workSheet.Cells[string.Format("A{0}", row + 6)].Style.Font.Name = "Candara Light";

                workSheet.Cells[string.Format("B{0}", row + 8)].Value = "THANKS";
                workSheet.Cells[string.Format("B{0}", row + 8)].Style.Font.Name = "Candara Light";
                xlPackage.Save();
            }
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BarItem.xlsx");
        }
    }
}
