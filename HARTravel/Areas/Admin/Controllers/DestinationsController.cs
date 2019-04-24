using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HARTravel.Models;

namespace HARTravel.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class DestinationsController : Controller
    {
        private HARTravelDBEntities db = new HARTravelDBEntities();

        // GET: Admin/Destinations
        public ActionResult Index()
        {
            return View(db.Destinations.ToList());
        }

        // GET: Admin/Destinations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Destination destination = db.Destinations.Find(id);
            if (destination == null)
            {
                return HttpNotFound();
            }
            return View(destination);
        }

        // GET: Admin/Destinations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Destinations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DestinationName,Image,Hot,Description")] Destination destination)
        {
            if (ModelState.IsValid)
            {
                db.Destinations.Add(destination);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(destination);
        }

        // GET: Admin/Destinations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Destination destination = db.Destinations.Find(id);
            if (destination == null)
            {
                return HttpNotFound();
            }
            return View(destination);
        }

        // POST: Admin/Destinations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DestinationName,Image,Hot,Description")] Destination destination)
        {
            if (ModelState.IsValid)
            {
                db.Entry(destination).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(destination);
        }

        // GET: Admin/Destinations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Destination destination = db.Destinations.Find(id);
            if (destination == null)
            {
                return HttpNotFound();
            }
            return View(destination);
        }

        // POST: Admin/Destinations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Destination destination = db.Destinations.Find(id);
            db.Destinations.Remove(destination);
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
