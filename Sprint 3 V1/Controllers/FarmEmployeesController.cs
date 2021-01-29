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
    public class FarmEmployeesController : Controller
    {
        private Sprint_3_V1Context db = new Sprint_3_V1Context();

        // GET: FarmEmployees
        public ActionResult Index()
        {
            var farmEmployees = db.FarmEmployees.Include(f => f.Employee).Include(f => f.Farm);
            return View(farmEmployees.ToList());
        }

        // GET: FarmEmployees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FarmEmployee farmEmployee = db.FarmEmployees.Find(id);
            if (farmEmployee == null)
            {
                return HttpNotFound();
            }
            return View(farmEmployee);
        }

        // GET: FarmEmployees/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "Name");
            ViewBag.FarmID = new SelectList(db.Farms, "FarmID", "FarmName");
            return View();
        }

        // POST: FarmEmployees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FEID,FarmID,EmployeeID,DateStarted,DateEnded")] FarmEmployee farmEmployee)
        {
            if (ModelState.IsValid)
            {
                db.FarmEmployees.Add(farmEmployee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "Name", farmEmployee.EmployeeID);
            ViewBag.FarmID = new SelectList(db.Farms, "FarmID", "FarmName", farmEmployee.FarmID);
            return View(farmEmployee);
        }

        // GET: FarmEmployees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FarmEmployee farmEmployee = db.FarmEmployees.Find(id);
            if (farmEmployee == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "Name", farmEmployee.EmployeeID);
            ViewBag.FarmID = new SelectList(db.Farms, "FarmID", "FarmName", farmEmployee.FarmID);
            return View(farmEmployee);
        }

        // POST: FarmEmployees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FEID,FarmID,EmployeeID,DateStarted,DateEnded")] FarmEmployee farmEmployee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(farmEmployee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "Name", farmEmployee.EmployeeID);
            ViewBag.FarmID = new SelectList(db.Farms, "FarmID", "FarmName", farmEmployee.FarmID);
            return View(farmEmployee);
        }

        // GET: FarmEmployees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FarmEmployee farmEmployee = db.FarmEmployees.Find(id);
            if (farmEmployee == null)
            {
                return HttpNotFound();
            }
            return View(farmEmployee);
        }

        // POST: FarmEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FarmEmployee farmEmployee = db.FarmEmployees.Find(id);
            db.FarmEmployees.Remove(farmEmployee);
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
