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
    [Authorize(Roles = "Admin,Manager,Employee")]
    public class ReviewsController : Controller
    {
        private HARTravelDBEntities db = new HARTravelDBEntities();

        // GET: Admin/Reviews
        public ActionResult Index()
        {
            var reviews = db.Reviews.Include(r => r.Customer).Include(r => r.Tour);
            return View(reviews.ToList());
        }

        // GET: Admin/Reviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: Admin/Reviews/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Email");
            ViewBag.TourId = new SelectList(db.Tours, "Id", "TourName");
            return View();
        }

        // POST: Admin/Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Content,DateCreate,StarRate,CustomerId,TourId")] Review review)
        {
            if (ModelState.IsValid)
            {
                review.DateCreate = DateTime.Now;
                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Email", review.CustomerId);
            ViewBag.TourId = new SelectList(db.Tours, "Id", "TourName", review.TourId);
            return View(review);
        }

        // GET: Admin/Reviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Email", review.CustomerId);
            ViewBag.TourId = new SelectList(db.Tours, "Id", "TourName", review.TourId);
            return View(review);
        }

        // POST: Admin/Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Content,DateCreate,StarRate,CustomerId,TourId")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Email", review.CustomerId);
            ViewBag.TourId = new SelectList(db.Tours, "Id", "TourName", review.TourId);
            return View(review);
        }

        // GET: Admin/Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Admin/Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
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
