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
    public class StockOrdersController : Controller
    {
        private Sprint_3_V1Context db = new Sprint_3_V1Context();

        // GET: StockOrders
        [Authorize(Roles = "Admin , Manager , Customer , Clerk, Foreman")]
        public ActionResult AddStockOrders([Bind(Include = "SOID,OrderID,StockID,Quantity,SubPrice")] StockOrder stockOrder)
        {
            try
            {
                db.StockOrders.Add(stockOrder);
                db.SaveChanges();

                ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "status", stockOrder.OrderID);
                ViewBag.StockID = new SelectList(db.Stocks, "StockID", "StockID", stockOrder.StockID);

                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "status", stockOrder.OrderID);
                ViewBag.StockID = new SelectList(db.Stocks, "StockID", "StockID", stockOrder.StockID);
                return View(stockOrder);
            }

            //ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "status", stockOrder.OrderID);
            // ViewBag.StockID = new SelectList(db.Stocks, "StockID", "StockID", stockOrder.StockID);

        }


        // GET: StockOrders
        [Authorize(Roles = "Admin , Manager , Clerk, Foreman")]
        public ActionResult Index()
        {
            var stockOrders = db.StockOrders.Include(s => s.Order).Include(s => s.Stock);
            return View(stockOrders.ToList());
        }

        // GET: StockOrders/Details/5
        [Authorize(Roles = "Admin , Manager , Customer , Clerk, Foreman")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockOrder stockOrder = db.StockOrders.Find(id);
            if (stockOrder == null)
            {
                return HttpNotFound();
            }
            return View(stockOrder);
        }

        // GET: StockOrders/Create
        [Authorize(Roles = "Admin , Manager , Customer , Clerk, Foreman")]
        public ActionResult Create()
        {
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "status");
            ViewBag.StockID = new SelectList(db.Stocks, "StockID", "StockID");
            return View();
        }

        // POST: StockOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin , Manager , Customer , Clerk, Foreman")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SOID,OrderID,StockID,Quantity,SubPrice")] StockOrder stockOrder)
        {
            if (ModelState.IsValid)
            {
                db.StockOrders.Add(stockOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "status", stockOrder.OrderID);
            ViewBag.StockID = new SelectList(db.Stocks, "StockID", "StockID", stockOrder.StockID);
            return View(stockOrder);
        }

        // GET: StockOrders/Edit/5
        [Authorize(Roles = "Admin , Manager , Clerk, Foreman")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockOrder stockOrder = db.StockOrders.Find(id);
            if (stockOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "status", stockOrder.OrderID);
            ViewBag.StockID = new SelectList(db.Stocks, "StockID", "StockID", stockOrder.StockID);
            return View(stockOrder);
        }

        // POST: StockOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin , Manager , Clerk, Foreman")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SOID,OrderID,StockID,Quantity,SubPrice")] StockOrder stockOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stockOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "status", stockOrder.OrderID);
            ViewBag.StockID = new SelectList(db.Stocks, "StockID", "StockID", stockOrder.StockID);
            return View(stockOrder);
        }

        // GET: StockOrders/Delete/5
        [Authorize(Roles = "Admin , Manager , Clerk")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockOrder stockOrder = db.StockOrders.Find(id);
            if (stockOrder == null)
            {
                return HttpNotFound();
            }
            return View(stockOrder);
        }

        // POST: StockOrders/Delete/5
        [Authorize(Roles = "Admin , Manager , Clerk")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StockOrder stockOrder = db.StockOrders.Find(id);
            db.StockOrders.Remove(stockOrder);
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
