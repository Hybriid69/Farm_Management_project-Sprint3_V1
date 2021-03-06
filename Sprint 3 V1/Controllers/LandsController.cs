﻿using System;
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
    public class LandsController : Controller
    {
        private Sprint_3_V1Context db = new Sprint_3_V1Context();

        // GET: Lands
        public ActionResult Index()
        {
            var lands = db.Lands.Include(l => l.Farm);
            return View(lands.ToList());
        }

        // GET: Lands/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Land land = db.Lands.Find(id);
            if (land == null)
            {
                return HttpNotFound();
            }
            return View(land);
        }

        // GET: Lands/Create
        public ActionResult Create()
        {
            ViewBag.FarmID = new SelectList(db.Farms, "FarmID", "FarmName");
            return View();
        }

        // POST: Lands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LandID,FarmID,NumFields,Size,Disabled")] Land land)
        {
            if (ModelState.IsValid)
            {
                db.Lands.Add(land);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FarmID = new SelectList(db.Farms, "FarmID", "FarmName", land.FarmID);
            return View(land);
        }

        // GET: Lands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Land land = db.Lands.Find(id);
            if (land == null)
            {
                return HttpNotFound();
            }
            ViewBag.FarmID = new SelectList(db.Farms, "FarmID", "FarmName", land.FarmID);
            return View(land);
        }

        // POST: Lands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LandID,FarmID,NumFields,Size,Disabled")] Land land)
        {
            if (ModelState.IsValid)
            {
                db.Entry(land).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FarmID = new SelectList(db.Farms, "FarmID", "FarmName", land.FarmID);
            return View(land);
        }

        // GET: Lands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Land land = db.Lands.Find(id);
            if (land == null)
            {
                return HttpNotFound();
            }
            return View(land);
        }

        // POST: Lands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Land land = db.Lands.Find(id);
            db.Lands.Remove(land);
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
