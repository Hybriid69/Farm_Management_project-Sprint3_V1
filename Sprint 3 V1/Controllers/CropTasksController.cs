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
    public class CropTasksController : Controller
    {
        private Sprint_3_V1Context db = new Sprint_3_V1Context();

        // GET: CropTasks
        public ActionResult Index()
        {
            var cropTasks = db.CropTasks.Include(c => c.CropInfo).Include(c => c.TaskMaster);
            return View(cropTasks.ToList());
        }

        // GET: CropTasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CropTask cropTask = db.CropTasks.Find(id);
            if (cropTask == null)
            {
                return HttpNotFound();
            }
            return View(cropTask);
        }

        // GET: CropTasks/Create
        public ActionResult Create()
        {
            ViewBag.CropID = new SelectList(db.CropInfoes, "CropID", "Name");
            ViewBag.MTaskID = new SelectList(db.TaskMasters, "MTaskID", "TaskName");
            return View();
        }

        // POST: CropTasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CTID,MTaskID,CropID,Duration")] CropTask cropTask)
        {
            if (ModelState.IsValid)
            {
                db.CropTasks.Add(cropTask);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CropID = new SelectList(db.CropInfoes, "CropID", "Name", cropTask.CropID);
            ViewBag.MTaskID = new SelectList(db.TaskMasters, "MTaskID", "TaskName", cropTask.MTaskID);
            return View(cropTask);
        }

        // GET: CropTasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CropTask cropTask = db.CropTasks.Find(id);
            if (cropTask == null)
            {
                return HttpNotFound();
            }
            ViewBag.CropID = new SelectList(db.CropInfoes, "CropID", "Name", cropTask.CropID);
            ViewBag.MTaskID = new SelectList(db.TaskMasters, "MTaskID", "TaskName", cropTask.MTaskID);
            return View(cropTask);
        }

        // POST: CropTasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CTID,MTaskID,CropID,Duration")] CropTask cropTask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cropTask).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CropID = new SelectList(db.CropInfoes, "CropID", "Name", cropTask.CropID);
            ViewBag.MTaskID = new SelectList(db.TaskMasters, "MTaskID", "TaskName", cropTask.MTaskID);
            return View(cropTask);
        }

        // GET: CropTasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CropTask cropTask = db.CropTasks.Find(id);
            if (cropTask == null)
            {
                return HttpNotFound();
            }
            return View(cropTask);
        }

        // POST: CropTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CropTask cropTask = db.CropTasks.Find(id);
            db.CropTasks.Remove(cropTask);
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
