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
    public class TaskMastersController : Controller
    {
        private Sprint_3_V1Context db = new Sprint_3_V1Context();

        // GET: TaskMasters
        public ActionResult Index()
        {
            return View(db.TaskMasters.ToList());
        }

        // GET: TaskMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskMaster taskMaster = db.TaskMasters.Find(id);
            if (taskMaster == null)
            {
                return HttpNotFound();
            }
            return View(taskMaster);
        }

        // GET: TaskMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaskMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MTaskID,TaskName,description,Duration")] TaskMaster taskMaster)
        {
            if (ModelState.IsValid)
            {
                db.TaskMasters.Add(taskMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(taskMaster);
        }

        // GET: TaskMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskMaster taskMaster = db.TaskMasters.Find(id);
            if (taskMaster == null)
            {
                return HttpNotFound();
            }
            return View(taskMaster);
        }

        // POST: TaskMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MTaskID,TaskName,description,Duration")] TaskMaster taskMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taskMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taskMaster);
        }

        // GET: TaskMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskMaster taskMaster = db.TaskMasters.Find(id);
            if (taskMaster == null)
            {
                return HttpNotFound();
            }
            return View(taskMaster);
        }

        // POST: TaskMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaskMaster taskMaster = db.TaskMasters.Find(id);
            db.TaskMasters.Remove(taskMaster);
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
