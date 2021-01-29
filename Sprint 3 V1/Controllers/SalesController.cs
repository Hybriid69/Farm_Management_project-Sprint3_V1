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
    public class SalesController : Controller
    {
        private Sprint_3_V1Context db = new Sprint_3_V1Context();

            [Authorize(Roles = "Admin , Manager , Customer , Clerk, Foreman")]
            public void AddSale([Bind(Include = "SaleID,Date,Total,CustomerID,OrderID")] Sale sale)
            {
                if (ModelState.IsValid)
                {
                    db.Sales.Add(sale);
                    db.SaveChanges();
                }
            }

            [Authorize(Roles = "Admin , Manager , Clerk, Foreman")]
            // GET: Sales
            public ActionResult Index()
            {
                var sales = db.Sales.Include(s => s.Customer).Include(s => s.Order);
                return View(sales.ToList());
            }

            [Authorize(Roles = "Admin , Manager , Clerk, Foreman")]
            // GET: Sales/Details/5
            public ActionResult Details(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Sale sale = db.Sales.Find(id);
                if (sale == null)
                {
                    return HttpNotFound();
                }
                return View(sale);
            }

            // GET: Sales/Create
            [Authorize(Roles = "Admin , Manager , Clerk")]
            public ActionResult Create()
            {
                ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name");
                ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "status");
                return View();
            }

            // POST: Sales/Create
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
            [Authorize(Roles = "Admin , Manager , Clerk")]
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create([Bind(Include = "SaleID,Date,Total,CustomerID,OrderID")] Sale sale)
            {
                if (ModelState.IsValid)
                {
                    db.Sales.Add(sale);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name", sale.CustomerID);
                ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "status", sale.OrderID);
                return View(sale);
            }

            // GET: Sales/Edit/5
            [Authorize(Roles = "Admin , Manager , Clerk, Foreman")]
            public ActionResult Edit(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Sale sale = db.Sales.Find(id);
                if (sale == null)
                {
                    return HttpNotFound();
                }
                ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name", sale.CustomerID);
                ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "status", sale.OrderID);
                return View(sale);
            }

            // POST: Sales/Edit/5
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
            [Authorize(Roles = "Admin , Manager , Clerk, Foreman")]
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit([Bind(Include = "SaleID,Date,Total,CustomerID,OrderID")] Sale sale)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(sale).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name", sale.CustomerID);
                ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "status", sale.OrderID);
                return View(sale);
            }

            // GET: Sales/Delete/5
            [Authorize(Roles = "Admin , Manager , Clerk")]
            public ActionResult Delete(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Sale sale = db.Sales.Find(id);
                if (sale == null)
                {
                    return HttpNotFound();
                }
                return View(sale);
            }

            // POST: Sales/Delete/5
            [Authorize(Roles = "Admin , Manager , Clerk")]
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteConfirmed(int id)
            {
                Sale sale = db.Sales.Find(id);
                db.Sales.Remove(sale);
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
