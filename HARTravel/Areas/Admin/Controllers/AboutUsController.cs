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
    public class AboutUsController : Controller
    {
        private HARTravelDBEntities db = new HARTravelDBEntities();

        // GET: Admin/AboutUs
        public ActionResult Index()
        {
            return View(db.AboutUs.ToList());
        }

        // GET: Admin/AboutUs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AboutU aboutU = db.AboutUs.Find(id);
            if (aboutU == null)
            {
                return HttpNotFound();
            }
            return View(aboutU);
        }

        // GET: Admin/AboutUs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/AboutUs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AboutTheCompany,AboutOurTour,Id")] AboutU aboutU)
        {
            if (ModelState.IsValid)
            {
                db.AboutUs.Add(aboutU);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aboutU);
        }

        // GET: Admin/AboutUs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AboutU aboutU = db.AboutUs.Find(id);
            if (aboutU == null)
            {
                return HttpNotFound();
            }
            return View(aboutU);
        }

        // POST: Admin/AboutUs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AboutTheCompany,AboutOurTour,Id")] AboutU aboutU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aboutU).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aboutU);
        }

        // GET: Admin/AboutUs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AboutU aboutU = db.AboutUs.Find(id);
            if (aboutU == null)
            {
                return HttpNotFound();
            }
            return View(aboutU);
        }

        // POST: Admin/AboutUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AboutU aboutU = db.AboutUs.Find(id);
            db.AboutUs.Remove(aboutU);
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
