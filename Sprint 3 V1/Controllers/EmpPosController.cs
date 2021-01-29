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
    [Authorize(Roles = "Admin , Manager , Human resources , Clerk")]
    public class EmpPosController : Controller
    {
        private Sprint_3_V1Context db = new Sprint_3_V1Context();

        // GET: EmpPos
        public ActionResult Index()
        {
            List<EmpPos> empPosList = new List<EmpPos>();
            var empPos = (from x in db.EmpPos
                          select x).Include(e => e.Position).ToList();
            foreach (var item in empPos)
            {
                EmpPos ep = new EmpPos();
                ep.EmpPosID = item.EmpPosID;
                ep.EmployeeID = item.EmployeeID;
                ep.PositionID = item.PositionID;
                ep.Started = item.Started;
                if (ep.Ended == null)
                {
                    ViewBag.EndDate = "Still Employed";
                    //ep.Ended = "Still Employed";
                }

            }
            //db.EmpPos.Include(e => e.Employee).Include(e => e.Position);

            return View(empPos.ToList());
        }

        // GET: EmpPos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpPos empPos = db.EmpPos.Find(id);
            if (empPos == null)
            {
                return HttpNotFound();
            }
            return View(empPos);
        }

        // GET: EmpPos/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "Name");
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Name");
            return View();
        }

        // POST: EmpPos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmpPosID,EmployeeID,PositionID,Started,Ended")] EmpPos empPos)
        {
            if (ModelState.IsValid)
            {
                db.EmpPos.Add(empPos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "Name", empPos.EmployeeID);
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Name", empPos.PositionID);
            return View(empPos);
        }

        // GET: EmpPos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpPos empPos = db.EmpPos.Find(id);
            if (empPos == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "Name", empPos.EmployeeID);
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Name", empPos.PositionID);
            return View(empPos);
        }

        // POST: EmpPos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmpPosID,EmployeeID,PositionID,Started,Ended")] EmpPos empPos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empPos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "Name", empPos.EmployeeID);
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Name", empPos.PositionID);
            return View(empPos);
        }

        // GET: EmpPos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpPos empPos = db.EmpPos.Find(id);
            if (empPos == null)
            {
                return HttpNotFound();
            }
            return View(empPos);
        }

        // POST: EmpPos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmpPos empPos = db.EmpPos.Find(id);
            db.EmpPos.Remove(empPos);
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
