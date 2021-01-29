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
    public class CropInfoesController : Controller
    {
        private Sprint_3_V1Context db = new Sprint_3_V1Context();

        // GET: CropInfoes
        public ActionResult Index()
        {
            return View(db.CropInfoes.ToList());
        }

        // GET: CropInfoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CropInfo cropInfo = db.CropInfoes.Find(id);
            if (cropInfo == null)
            {
                return HttpNotFound();
            }
            return View(cropInfo);
        }

        // GET: CropInfoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CropInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CropID,Name,Description,Season,Spacing,AverageYield,IrrigationInterval,GrowthTime,LifeExpect,Disabled")] CropInfo cropInfo)
        {
            if (ModelState.IsValid)
            {
                db.CropInfoes.Add(cropInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cropInfo);
        }

        // GET: CropInfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CropInfo cropInfo = db.CropInfoes.Find(id);
            if (cropInfo == null)
            {
                return HttpNotFound();
            }
            return View(cropInfo);
        }

        // POST: CropInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CropID,Name,Description,Season,Spacing,AverageYield,IrrigationInterval,GrowthTime,LifeExpect,Disabled")] CropInfo cropInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cropInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cropInfo);
        }

        // GET: CropInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CropInfo cropInfo = db.CropInfoes.Find(id);
            if (cropInfo == null)
            {
                return HttpNotFound();
            }
            return View(cropInfo);
        }

        // POST: CropInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CropInfo cropInfo = db.CropInfoes.Find(id);
            db.CropInfoes.Remove(cropInfo);
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
