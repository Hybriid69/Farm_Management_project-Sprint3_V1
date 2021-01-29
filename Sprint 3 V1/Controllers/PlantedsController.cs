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
    public class PlantedsController : Controller
    {
        private Sprint_3_V1Context db = new Sprint_3_V1Context();

        // GET: Planteds
        public ActionResult Index()
        {
            var planteds = db.Planteds.Include(p => p.Cropsinfo).Include(p => p.Field);
            return View(planteds.ToList());
        }

        // GET: Planteds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Planted planted = db.Planteds.Find(id);
            if (planted == null)
            {
                return HttpNotFound();
            }
            return View(planted);
        }

        // GET: Planteds/Create
        public ActionResult Create()
        {
            ViewBag.CropID = new SelectList(db.CropInfoes, "CropID", "Name");
            ViewBag.FieldID = new SelectList(db.Fields, "FieldID", "FieldName");
            return View();
        }

        // POST: Planteds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlantedID,Status,FieldID,FieldName,CropID,CropName,QuantityPlanted,PlantedDate,ExpectedHarvestDate,ExpectedYield,LastWatered,NextWater")] Planted planted)
        {
            if (ModelState.IsValid)
            {
                db.Planteds.Add(planted);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CropID = new SelectList(db.CropInfoes, "CropID", "Name", planted.CropID);
            ViewBag.FieldID = new SelectList(db.Fields, "FieldID", "FieldName", planted.FieldID);
            return View(planted);
        }

        // GET: Planteds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Planted planted = db.Planteds.Find(id);
            if (planted == null)
            {
                return HttpNotFound();
            }
            ViewBag.CropID = new SelectList(db.CropInfoes, "CropID", "Name", planted.CropID);
            ViewBag.FieldID = new SelectList(db.Fields, "FieldID", "FieldName", planted.FieldID);
            return View(planted);
        }

        // POST: Planteds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlantedID,Status,FieldID,FieldName,CropID,CropName,QuantityPlanted,PlantedDate,ExpectedHarvestDate,ExpectedYield,LastWatered,NextWater")] Planted planted)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planted).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CropID = new SelectList(db.CropInfoes, "CropID", "Name", planted.CropID);
            ViewBag.FieldID = new SelectList(db.Fields, "FieldID", "FieldName", planted.FieldID);
            return View(planted);
        }

        // GET: Planteds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Planted planted = db.Planteds.Find(id);
            if (planted == null)
            {
                return HttpNotFound();
            }
            return View(planted);
        }

        // POST: Planteds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Planted planted = db.Planteds.Find(id);
            db.Planteds.Remove(planted);
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
