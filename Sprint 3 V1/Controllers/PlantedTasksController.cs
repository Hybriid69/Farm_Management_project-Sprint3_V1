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
    public class PlantedTasksController : Controller
    {
        private Sprint_3_V1Context db = new Sprint_3_V1Context();

        // GET: PlantedTasks
        public ActionResult Index()
        {
            var plantedTasks = db.PlantedTasks.Include(p => p.Planted);
            return View(plantedTasks.ToList());
        }

        // GET: PlantedTasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlantedTask plantedTask = db.PlantedTasks.Find(id);
            if (plantedTask == null)
            {
                return HttpNotFound();
            }
            return View(plantedTask);
        }

        // GET: PlantedTasks/Create
        public ActionResult Create()
        {
            ViewBag.PlantedID = new SelectList(db.Planteds, "PlantedID", "Status");
            return View();
        }

        // POST: PlantedTasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PTaskID,Description,Status,Assigned,StartOn,ExpectedCompletion,CompletedOn,flag,PlantedID")] PlantedTask plantedTask)
        {
            if (ModelState.IsValid)
            {
                db.PlantedTasks.Add(plantedTask);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PlantedID = new SelectList(db.Planteds, "PlantedID", "Status", plantedTask.PlantedID);
            return View(plantedTask);
        }

        // GET: PlantedTasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlantedTask plantedTask = db.PlantedTasks.Find(id);
            if (plantedTask == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlantedID = new SelectList(db.Planteds, "PlantedID", "Status", plantedTask.PlantedID);
            return View(plantedTask);
        }

        // POST: PlantedTasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PTaskID,Description,Status,Assigned,StartOn,ExpectedCompletion,CompletedOn,flag,PlantedID")] PlantedTask plantedTask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(plantedTask).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlantedID = new SelectList(db.Planteds, "PlantedID", "Status", plantedTask.PlantedID);
            return View(plantedTask);
        }

        // GET: PlantedTasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlantedTask plantedTask = db.PlantedTasks.Find(id);
            if (plantedTask == null)
            {
                return HttpNotFound();
            }
            return View(plantedTask);
        }

        // POST: PlantedTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlantedTask plantedTask = db.PlantedTasks.Find(id);
            db.PlantedTasks.Remove(plantedTask);
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
