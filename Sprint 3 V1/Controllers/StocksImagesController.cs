using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sprint_3_V1.Models;

namespace Sprint_3_V1.Controllers
{
    
    public class StocksImagesController : Controller
    {
        private Sprint_3_V1Context db = new Sprint_3_V1Context();

        // GET: StocksImages
        [Authorize(Roles = "Admin , Manager , Customer , Clerk")]
        public ActionResult Index()
        {
            var stocksImages = db.StocksImages.Include(s => s.Stock);
            return View(stocksImages.ToList());
        }

        // GET: StocksImages/Details/5
        [Authorize(Roles = "Admin , Manager , Customer , Clerk")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StocksImage stocksImage = db.StocksImages.Find(id);
            if (stocksImage == null)
            {
                return HttpNotFound();
            }
            return View(stocksImage);
        }

        // GET: StocksImages/Create
        [Authorize(Roles = "Admin , Manager , Clerk")]
        public ActionResult Create()
        {
            ViewBag.crop = new SelectList(db.CropInfoes, "Name", "Name");
            return View();
        }

        // POST: StocksImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin , Manager , Clerk")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StockImageID,StockImage,StockID")] StocksImage stocksImage, HttpPostedFileBase image1, string crop)
        {
            int ID;
            if (image1 != null)
            {
                stocksImage.StockImage = new byte[image1.ContentLength];
                image1.InputStream.Read(stocksImage.StockImage, 0, image1.ContentLength);

                var id1 = db.CropInfoes.Where(x => x.Name == crop).Select(y => y.CropID).First();
                int cid=Convert.ToInt16(id1);
                var findStock = db.Stocks.Where(x => x.CropID == cid).Select(y => y.StockID).Count();

                if (findStock>0)
                {
                    var id2 = db.Stocks.Where(x => x.CropID == cid).Select(y => y.StockID).First();
                    ID = Convert.ToInt16(id2);
                }
                else
                {
                    ViewBag.noStock = "Stock has not yet been created ie. Harvest pending";
                    ViewBag.crop = new SelectList(db.CropInfoes, "Name", "Name");
                    return View();
                }
               
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Please add image");
                return View(stocksImage);


            }
            if (ModelState.IsValid)
            {
                var imageExists = db.StocksImages.Where(x => x.ImageName == crop).Select(y => y.ImageName).Count();
                if (imageExists>0)
                {
                    var stockid = db.StocksImages.Where(x => x.ImageName == crop).Select(y => y.StockImageID).First();
                    stocksImage.StockImageID = Convert.ToInt16(stockid);
                    stocksImage.StockID = ID;
                    stocksImage.ImageName = crop;
                    db.Entry(stocksImage).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    stocksImage.StockID = ID;
                    stocksImage.ImageName = crop;
                    db.StocksImages.Add(stocksImage);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }   
            }
            ViewBag.crop = new SelectList(db.CropInfoes, "Name", "Name");
            //ViewBag.StockID = new SelectList(db.Stocks, "StockID", "StockID", stocksImage.StockID);
            return View(stocksImage);
        }

        // GET: StocksImages/Edit/5
        [Authorize(Roles = "Admin , Manager , Clerk")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StocksImage stocksImage = db.StocksImages.Find(id);
            if (stocksImage == null)
            {
                return HttpNotFound();
            }
            ViewBag.StockID = new SelectList(db.Stocks, "StockID", "StockID", stocksImage.StockID);
            return View(stocksImage);
        }

        // POST: StocksImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin , Manager , Clerk")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StockImageID,StockImage,StockID")] StocksImage stocksImage,HttpPostedFileBase image1)
        {
            var img = db.StocksImages.Where(x => x.StockImage == stocksImage.StockImage).Select(x => x.StockImage).Single();
            if (image1 != null)
            {
                stocksImage.StockImage = new byte[image1.ContentLength];
                image1.InputStream.Read(stocksImage.StockImage, 0, image1.ContentLength);
                db.Entry(stocksImage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else if (img != null)
            {
                stocksImage.StockImage = img;
                //image1.InputStream.Read(beverage.Beverage_Image, 0, image1.ContentLength);
                db.Entry(stocksImage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.imageError = "Please add an Image";
            }
        ViewBag.StockID = new SelectList(db.Stocks, "StockID", "StockID", stocksImage.StockID);
            return View(stocksImage);
        }

        // GET: StocksImages/Delete/5
        [Authorize(Roles = "Admin , Manager , Clerk")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StocksImage stocksImage = db.StocksImages.Find(id);
            if (stocksImage == null)
            {
                return HttpNotFound();
            }
            return View(stocksImage);
        }

        // POST: StocksImages/Delete/5
        [Authorize(Roles = "Admin , Manager , Clerk")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StocksImage stocksImage = db.StocksImages.Find(id);
            db.StocksImages.Remove(stocksImage);
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
