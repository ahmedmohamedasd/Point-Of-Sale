using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using PointOfSale.Data;
using PointOfSale.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSale.Controllers
{
    [Authorize]
    public class CartStatisticsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IHostingEnvironment hostingEnviroment;
        public CartStatisticsController(ApplicationDbContext context , IHostingEnvironment hostingEnviroment)
        {
            this.context = context;
            this.hostingEnviroment = hostingEnviroment;
        }
        [Authorize(Policy = "CartShow")]
        public IActionResult Index()
        {
            var cartItems = context.CartItems.Where(c =>c.DateOfReceipt.Date == DateTime.Now.Date).OrderByDescending(c=>c.Id).ToList();
            return View(cartItems);
        }
       
        [AllowAnonymous]
        public IActionResult SearchOrder(DateTime StartDate)
        {
            var cartItems = context.CartItems.Where(c => c.DateOfReceipt.Date == StartDate.Date).OrderByDescending(c => c.Id).ToList();
            return View("Index",cartItems);
        }
       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CartDelete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cartItem = await context.CartItems.FindAsync(id);
            if (cartItem == null)
                return NotFound();

            var barContent = context.Contents.Where(c => c.BarId == cartItem.ProductId).ToList();
           
            if(barContent != null)
            {
                for (int j = 0; j < barContent.Count; j++)
                {
                    var contentStock = context.Stocks.FirstOrDefault(c => c.ProductId == barContent[j].ContentId);
                    if (contentStock != null)
                    {
                        contentStock.Quantity = contentStock.Quantity + cartItem.Quantity * barContent[j].Amount;
                        context.Stocks.Update(contentStock);
                        context.SaveChanges();
                    }
                }
            }
            var operations = context.OperationStocks.Where(c => c.CartOrderId == id).ToList();
            if (operations.Any())
            {
                for(int i=0; i < operations.Count(); i++)
                {
                    context.OperationStocks.Remove(operations[i]);
                    context.SaveChanges();
                }
            }
            context.CartItems.Remove(cartItem);
            await context.SaveChangesAsync();
            return RedirectToAction("Index"); //Json(new { html = Helper.RenderRazorViewToString(this, "Index", context.Transactions.ToList()) });

        }
       
        [AllowAnonymous]
        public IActionResult DailyReceipt(DateTime StartDate , DateTime EndDate)
        {
            if(StartDate == null && EndDate == null)
            {
                StartDate = DateTime.Now;
                EndDate = DateTime.Now;
            }
            IEnumerable<DailyReport> dailyReports = context.CartItems.Where(c=>c.IsFree == false && 
                                                                            c.DateOfReceipt.Date >= StartDate.Date &&
                                                                            c.DateOfReceipt.Date <= EndDate.Date)
                                                        .GroupBy(p => new { p.ProductName })
                                                        .Select(c => new DailyReport
                                                        {
                                                            ProductName = c.Key.ProductName,
                                                            Sorting = context.BarItems.Where(x=>x.Name == c.Key.ProductName).First().Category.Sorting,
                                                            Price = context.BarItems.Where(x => x.Name == c.Key.ProductName).First().Price,
                                                            IsFree = false,
                                                            Sub_Total = c.Sum(p=>p.Quantity * p.Price),
                                                            Quantity = c.Sum(p=>p.Quantity)
                                                        }).OrderBy(c=>c.Sorting);
            IEnumerable<DailyReport> freedailyReports = context.CartItems.Where(c => c.IsFree == true &&
                                                                            c.DateOfReceipt.Date >= StartDate.Date &&
                                                                            c.DateOfReceipt.Date <= EndDate.Date)
                                                       .GroupBy(p => new { p.ProductName })
                                                       .Select(c => new DailyReport
                                                       {
                                                           ProductName = c.Key.ProductName,
                                                           Sorting = context.BarItems.Where(x => x.Name == c.Key.ProductName).First().Category.Sorting,
                                                           Price = context.BarItems.Where(x => x.Name == c.Key.ProductName).First().Price,
                                                           IsFree = true,
                                                           Sub_Total = 0,
                                                           Quantity = c.Sum(p => p.Quantity)
                                                       }).OrderBy(c => c.Sorting); 
            DailyReportViewModel vm = new DailyReportViewModel()
                {
                    DailyReports = dailyReports,
                    FreeDailyReports = freedailyReports,
                    TotalAmount = dailyReports.Sum(c => c.Quantity * c.Price),
                    TotalQuantity = dailyReports.Sum(c => c.Quantity),
                    TotalFreeQuantity = freedailyReports.Sum(c=>c.Quantity),
                };
            ViewBag.GrandTotal = vm.TotalQuantity + vm.TotalFreeQuantity;
                if (StartDate == EndDate)
                {
                    ViewBag.ReciptDate = StartDate.ToString("MM/dd/yyyy");
                }
                else
                {
                    ViewBag.ReciptDate = StartDate.ToString("MM/dd/yyyy") + "--" + EndDate.ToString("MM/dd/yyyy");
                }
            TempData["ReportsVm"] = JsonConvert.SerializeObject(vm);
            // in another hand  MyObject myObject2 = JsonConvert.DeserializeObject<MyObject>(TempData["Key"].ToString());
            return View(vm);
            }
       
        [AllowAnonymous]
        public IActionResult PrintDailyReceipt()
        {
            if(TempData["ReportsVm"] == null)
            {
                return RedirectToAction("Index");
            }
            CultureInfo ci = new CultureInfo("en-us");
            DailyReportViewModel model = JsonConvert.DeserializeObject<DailyReportViewModel>(TempData["ReportsVm"].ToString());
            if (model.Equals(null))
            {
                return RedirectToAction("Index");
            }
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var stream = new MemoryStream();
            //Bs ana Fannan 
            // Get Photo From Project
            string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "Images"); // Get Root To Image Folder
            string uniqueFile = "ilcampoLogo.jpg";// Get a Unique Path For Image
            string filePath = Path.Combine(uploadsFolder, uniqueFile);
            using (var xlPackage = new ExcelPackage(stream))
            {
                // Basic Column in Excel Sheet 
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
                //Style of Total quantity Price Quantity  
                using (var r = workSheet.Cells["A11:E11"])
                {
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(Color.Gray);
                    r.Style.Font.Size = 16;
                    r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                }
                int row = 12;
                if (model.DailyReports.Any())
                {
                    foreach (var item in model.DailyReports)
                    {
                        workSheet.Cells[string.Format("A{0}", row)].Value = item.ProductName.ToUpper();
                        workSheet.Cells[string.Format("C{0}", row)].Value = item.Quantity;
                        workSheet.Cells[string.Format("C{0}", row)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        workSheet.Cells[string.Format("D{0}", row)].Value = item.Price + "";
                        workSheet.Cells[string.Format("E{0}", row)].Value = item.Sub_Total.ToString("F02",ci);
                        row++;
                    }
                }
                if (model.FreeDailyReports.Any())
                {
                    
                    workSheet.Cells[string.Format("A{0}", row)].Value = "Free Items";
                    using(var t = workSheet.Cells[string.Format("A{0}:E{1}", row, row)])
                    {
                        t.Merge = true;
                        t.Style.Font.Name = "Engravers MT";
                        t.Style.Font.Size = 11;
                        t.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        t.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(211, 211, 211));
                        t.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;


                    }

                    workSheet.Cells[string.Format("A{0}", row)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;


                    row++;
                    foreach (var item in model.FreeDailyReports)
                    {
                        workSheet.Cells[string.Format("A{0}", row)].Value = item.ProductName.ToUpper();
                        workSheet.Cells[string.Format("C{0}", row)].Value = item.Quantity;
                        workSheet.Cells[string.Format("C{0}", row)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        workSheet.Cells[string.Format("D{0}", row)].Value = item.Price + "";
                        workSheet.Cells[string.Format("E{0}", row)].Value = item.Sub_Total+"";
                        row++;
                    }
                }
                // Last items in 
                workSheet.Cells[string.Format("A{0}", row + 1)].Value = "Sub Total";
                workSheet.Cells[string.Format("A{0}", row + 2)].Value = "SERVICE CHARGE";
                workSheet.Cells[string.Format("A{0}", row + 3)].Value = "V.A.Tax";
                workSheet.Cells[string.Format("A{0}", row + 4)].Value = "BALANCE DUE";
                using (var t = workSheet.Cells[string.Format("A{0}:A{1}", row + 1, row + 4)])
                {
                    t.Style.Font.Bold = true;
                    t.Style.Font.Size = 12;
                }
                decimal sub_total = model.DailyReports.Sum(c => c.Quantity * c.Price);
                decimal tax = sub_total * (decimal).14;
                workSheet.Cells[string.Format("E{0}", row + 1)].Value = model.TotalAmount +"";
                workSheet.Cells[string.Format("E{0}", row + 2)].Value = "0.00";
                workSheet.Cells[string.Format("E{0}", row + 3)].Value = tax.ToString("F02",ci);
                workSheet.Cells[string.Format("E{0}", row + 4)].Value = model.TotalAmount + "";

                using (var t = workSheet.Cells[string.Format("E{0}:E{1}", row + 1, row + 4)])
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
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "listOfItem.xlsx");
           


        }



    }
}
