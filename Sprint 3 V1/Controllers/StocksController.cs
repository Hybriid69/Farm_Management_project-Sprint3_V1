using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sprint_3_V1.Models;
using Sprint_3_V1.ViewModels;

namespace Sprint_3_V1.Controllers
{
    public class StocksController : Controller
    {
        private Sprint_3_V1Context db = new Sprint_3_V1Context();

        // GET: Stocks
        [AllowAnonymous]
        public ActionResult Quantity(StockQuantityViewModel quantity)
        {
            return PartialView();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Quantity(StockQuantityViewModel stockQuantity, int? quantity)
        {

            return PartialView();
        }

        [AllowAnonymous]
        public ActionResult Search(StockViewModel stockViewModel)
        {
            //ModelState.Clear();
            return PartialView(/*stockViewModel*/);
        }


        [AllowAnonymous]
        public ActionResult StockSort()
        {
            return PartialView();
        }

        static string sortMain = "";

        [AllowAnonymous]
        [HttpPost]
        public ActionResult StockSort(StockSort stockSort)
        {
            if (stockSort.SortBy != null)
            {
                sortMain = stockSort.SortBy.ToString();
                return RedirectToAction("Store");
            }
            else
            {
                return PartialView();
            }
            //StocksController sc = new StocksController();
            //sc.Store(sort);  
        }


        [AllowAnonymous]
        public ActionResult Store(StockSort stockSort)
        {
            if (TempData.ContainsKey("SuccessMessage"))
            {
                ViewBag.successMessage = TempData["SuccessMessage"].ToString();
            }
            if (TempData.ContainsKey("lessQuantityMessage"))
            {
                ViewBag.LessQuantityMessage = TempData["lessQuantityMessage"].ToString();
            }
            string sort = sortMain;
            if (sort == null || sort == "")
            {
                sort = "Name Ascending";
            }
            Stock stock = new Stock();
            //stock.sortValue(sort);
            sortMain = "";
            return View(stock.sortValue(sort));

            //var stocks = db.Stocks.Include(s => s.CropInfo).Include(y=>y.StocksImage);
            //return View(stocks.ToList());
            //List<StockViewModel> searchStockList = new List<StockViewModel>();

            //var mergedList = (from c in db.CropInfoes
            //                  join x in db.Stocks
            //                  on c.CropID equals x.CropID
            //                  join y in db.StocksImages
            //                  on x.StockID equals y.StockID
            //                  orderby c.Name ascending
            //                  select new
            //                  {
            //                      x.StockID,
            //                      c.CropID,
            //                      c.Name,
            //                      x.CurQuantity,
            //                      x.Expiery,
            //                      x.Price,
            //                      y.StockImage
            //                  }).ToList();
            //if (mergedList.Count() > 0)
            //{
            //    foreach (var item in mergedList)
            //    {
            //        StockViewModel stk = new StockViewModel();
            //        stk.StockID = item.StockID;
            //        stk.CropID = item.CropID;
            //        stk.CropName = item.Name;
            //        stk.CurQuantity = item.CurQuantity;
            //        stk.Expiery = item.Expiery;
            //        stk.StockImage = item.StockImage;
            //        searchStockList.Add(stk);
            //    }
            //}
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Store(string search, int? cropid, string name)
        {
            //TODO :   clear search bar
            List<StockViewModel> searchStockList = new List<StockViewModel>();

            if (search != null)
            {
                var mergedList = (from c in db.CropInfoes
                                  join x in db.Stocks
                                  on c.CropID equals x.CropID
                                  join y in db.StocksImages
                                  on x.StockID equals y.StockID
                                  where c.Name.Contains(search)
                                  orderby c.Name ascending
                                  select new
                                  {
                                      x.StockID,
                                      c.CropID,
                                      c.Name,
                                      x.CurQuantity,
                                      x.Expiery,
                                      x.Price,
                                      y.StockImage
                                  }).ToList();
                if (mergedList.Count() > 0)
                {
                    foreach (var item in mergedList)
                    {
                        StockViewModel stk = new StockViewModel();
                        stk.StockID = item.StockID;
                        stk.CropID = item.CropID;
                        stk.CropName = item.Name;
                        stk.CurQuantity = item.CurQuantity;
                        stk.Expiery = item.Expiery;
                        stk.StockImage = item.StockImage;
                        stk.Price = item.Price;
                        searchStockList.Add(stk);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Crop not found");
                }
                search = string.Empty;
                ModelState.Clear();
                return View(searchStockList);

            }
            else
            {
                var mergedList = (from c in db.CropInfoes
                                  join x in db.Stocks
                                  on c.CropID equals x.CropID
                                  join y in db.StocksImages
                                  on x.StockID equals y.StockID
                                  orderby c.Name ascending
                                  select new
                                  {
                                      x.StockID,
                                      c.CropID,
                                      c.Name,
                                      x.CurQuantity,
                                      x.Expiery,
                                      x.Price,
                                      y.StockImage
                                  }).ToList();
                if (mergedList.Count() > 0)
                {
                    foreach (var item in mergedList)
                    {
                        StockViewModel stk = new StockViewModel();
                        stk.StockID = item.StockID;
                        stk.CropID = item.CropID;
                        stk.CropName = item.Name;
                        stk.CurQuantity = item.CurQuantity;
                        stk.Expiery = item.Expiery;
                        stk.StockImage = item.StockImage;
                        stk.Price = item.Price;
                        searchStockList.Add(stk);
                    }
                }
                return View(searchStockList);
            }
        }

        [AllowAnonymous]
        // GET: Stocks
        public ActionResult Index()
        {
            var stocks = db.Stocks.Include(s => s.CropInfo);

            return View(stocks.ToList());
        }

        [Authorize(Roles = "Admin , Manager , Clerk")]
        public ActionResult AddImage(int? id)
        {
            return RedirectToAction("Create", "StocksImages");
        }

        // GET: Stocks/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockViewModel stk = new StockViewModel();

            var mergedList = (from c in db.CropInfoes
                              join x in db.Stocks
                              on c.CropID equals x.CropID
                              join y in db.StocksImages
                              on x.StockID equals y.StockID
                              orderby c.Name ascending
                              where x.StockID == id
                              select new
                              {
                                  x.StockID,
                                  c.CropID,
                                  c.Name,
                                  x.CurQuantity,
                                  x.Expiery,
                                  x.Price,
                                  y.StockImage
                              }).ToList();
            if (mergedList.Count() > 0)
            {
                foreach (var item in mergedList)
                {
                    //StockViewModel stk = new StockViewModel();
                    stk.StockID = item.StockID;
                    stk.CropID = item.CropID;
                    stk.CropName = item.Name;
                    stk.CurQuantity = item.CurQuantity;
                    stk.Expiery = item.Expiery;
                    stk.StockImage = item.StockImage;
                    stk.Price = item.Price;
                    // searchStockList.Add(stk);
                }
            }
            if (stk == null)
            {
                return HttpNotFound();
            }
            return View(stk);
        }

        // GET: Stocks/Create
        [Authorize(Roles = "Admin , Manager , Clerk")]
        public ActionResult Create()
        {
            ViewBag.CropID = new SelectList(db.CropInfoes, "CropID", "Name");
            return View();
        }

        // POST: Stocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin , Manager , Clerk")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StockID,CurQuantity,HarQuantity,Harvested,Expiery,ExpFlag,Price,Disabled,CropID")] Stock stock)
        {

            if (ModelState.IsValid)
            {
                db.Stocks.Add(stock);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CropID = new SelectList(db.CropInfoes, "CropID", "Name", stock.CropID);
            return View(stock);
        }

        // GET: Stocks/Edit/5
        [Authorize(Roles = "Admin , Manager , Clerk")]
        public ActionResult Edit(int? id)
        {
            int stockId = 0;
            var sImage = db.StocksImages.Select(x => x.ImageName).ToList();
            if (TempData.ContainsKey("stockID"))
            {
                stockId = Convert.ToInt16(TempData["stockID"]);
            }
            if (id == null)
            {
                if (stockId == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {
                    id = stockId;
                }
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            ViewBag.CropID = new SelectList(db.CropInfoes, "CropID", "Name", stock.CropID);
            ViewBag.StockImage = new SelectList(db.StocksImages, "StockImageID", "ImageName", sImage);
            ModelState.AddModelError(string.Empty, "Please enter the price");
            ViewBag.priceMessage = "Please enter price";

            return View(stock);
        }

        // POST: Stocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin , Manager , Clerk")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StockID,CurQuantity,HarQuantity,Harvested,Expiery,ExpFlag,Price,Disabled,CropID,StocksImage")] Stock stock)
        {
            var sImage = db.StocksImages.Select(x => x.ImageName).ToList();
            if (ModelState.IsValid)
            {
                db.Entry(stock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CropID = new SelectList(db.CropInfoes, "CropID", "Name", stock.CropID);
            ViewBag.StockImage = new SelectList(db.StocksImages, "StockImageID", "ImageName", sImage);
            return View(stock);
        }

        // GET: Stocks/Delete/5
        [Authorize(Roles = "Admin , Manager , Clerk")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // POST: Stocks/Delete/5
        [Authorize(Roles = "Admin , Manager , Clerk")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stock stock = db.Stocks.Find(id);
            db.Stocks.Remove(stock);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
