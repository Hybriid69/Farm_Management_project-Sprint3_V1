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
    public class GroupTasksController : Controller
    {
        private Sprint_3_V1Context db = new Sprint_3_V1Context();

        // GET: GroupTasks
        public ActionResult Index()
        {
            var groupTasks = db.GroupTasks.Include(g => g.Group).Include(g => g.PlantedTask);
            return View(groupTasks.ToList());
        }

        // GET: GroupTasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupTask groupTask = db.GroupTasks.Find(id);
            if (groupTask == null)
            {
                return HttpNotFound();
            }
            return View(groupTask);
        }

        // GET: GroupTasks/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeID = new SelectList(db.Groups, "GroupID", "GName");
            ViewBag.PTaskID = new SelectList(db.PlantedTasks, "PTaskID", "Description");
            return View();
        }

        // POST: GroupTasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GTID,PTaskID,EmployeeID")] GroupTask groupTask)
        {
            if (ModelState.IsValid)
            {
                db.GroupTasks.Add(groupTask);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeID = new SelectList(db.Groups, "GroupID", "GName", groupTask.EmployeeID);
            ViewBag.PTaskID = new SelectList(db.PlantedTasks, "PTaskID", "Description", groupTask.PTaskID);
            return View(groupTask);
        }

        // GET: GroupTasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupTask groupTask = db.GroupTasks.Find(id);
            if (groupTask == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.Groups, "GroupID", "GName", groupTask.EmployeeID);
            ViewBag.PTaskID = new SelectList(db.PlantedTasks, "PTaskID", "Description", groupTask.PTaskID);
            return View(groupTask);
        }

        // POST: GroupTasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GTID,PTaskID,EmployeeID")] GroupTask groupTask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(groupTask).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(db.Groups, "GroupID", "GName", groupTask.EmployeeID);
            ViewBag.PTaskID = new SelectList(db.PlantedTasks, "PTaskID", "Description", groupTask.PTaskID);
            return View(groupTask);
        }

        // GET: GroupTasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupTask groupTask = db.GroupTasks.Find(id);
            if (groupTask == null)
            {
                return HttpNotFound();
            }
            return View(groupTask);
        }

        // POST: GroupTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GroupTask groupTask = db.GroupTasks.Find(id);
            db.GroupTasks.Remove(groupTask);
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
