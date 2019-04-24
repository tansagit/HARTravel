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
    public class BookToursController : Controller
    {
        private HARTravelDBEntities db = new HARTravelDBEntities();


        // GET: Admin/BookTours
        public ActionResult Index()
        {
            var bookTours = db.BookTours.Include(b => b.Customer).Include(b => b.Employee).Include(b => b.Status).Include(b => b.Tour);
            return View(bookTours.ToList());
        }

        // GET: Admin/BookTours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookTour bookTour = db.BookTours.Find(id);
            if (bookTour == null)
            {
                return HttpNotFound();
            }
            return View(bookTour);
        }

        // GET: Admin/BookTours/Create
        public ActionResult Create()
        {

            ViewBag.CusId = new SelectList(db.Customers, "Id", "Email");
            ViewBag.EmpId = new SelectList(db.Employees, "Id", "LoginName");
            ViewBag.StatusId = new SelectList(db.Status, "Id", "StatusName");
            ViewBag.TourId = new SelectList(db.Tours, "Id", "TourName");
            return View();
        }

        // POST: Admin/BookTours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Numbers,Price,Note,StatusId,TourId,EmpId,CusId,Voucher")] BookTour bookTour)
        {
            if (ModelState.IsValid)
            {
                db.BookTours.Add(bookTour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CusId = new SelectList(db.Customers, "Id", "Email", bookTour.CusId);
            ViewBag.EmpId = new SelectList(db.Employees, "Id", "LoginName", bookTour.EmpId);
            ViewBag.StatusId = new SelectList(db.Status, "Id", "StatusName", bookTour.StatusId);
            ViewBag.TourId = new SelectList(db.Tours, "Id", "TourName", bookTour.TourId);
            return View(bookTour);
        }

        // GET: Admin/BookTours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookTour bookTour = db.BookTours.Find(id);
            var cur_emp = Session["EmpId"].ToString();
            if (bookTour == null || (bookTour.EmpId).ToString() != cur_emp)
            {
                return HttpNotFound();
            }
            ViewBag.CusId = new SelectList(db.Customers, "Id", "Email", bookTour.CusId);
            ViewBag.EmpId = new SelectList(db.Employees, "Id", "LoginName", bookTour.EmpId);
            ViewBag.StatusId = new SelectList(db.Status, "Id", "StatusName", bookTour.StatusId);
            ViewBag.TourId = new SelectList(db.Tours, "Id", "TourName", bookTour.TourId);
            return View(bookTour);
        }

        // POST: Admin/BookTours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Numbers,Price,Note,StatusId,TourId,EmpId,CusId,Voucher")] BookTour bookTour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookTour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CusId = new SelectList(db.Customers, "Id", "Email", bookTour.CusId);
            ViewBag.EmpId = new SelectList(db.Employees, "Id", "LoginName", bookTour.EmpId);
            ViewBag.StatusId = new SelectList(db.Status, "Id", "StatusName", bookTour.StatusId);
            ViewBag.TourId = new SelectList(db.Tours, "Id", "TourName", bookTour.TourId);
            return View(bookTour);
        }

        // GET: Admin/BookTours/Confirm/5
        public ActionResult Confirm(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookTour bookTour = db.BookTours.Find(id);

            if (bookTour == null)
            {
                return HttpNotFound();
            }
            ViewBag.CusId = new SelectList(db.Customers, "Id", "Email", bookTour.CusId);
            ViewBag.EmpId = new SelectList(db.Employees, "Id", "LoginName", bookTour.EmpId);
            ViewBag.StatusId = new SelectList(db.Status, "Id", "StatusName", bookTour.StatusId);
            ViewBag.TourId = new SelectList(db.Tours, "Id", "TourName", bookTour.TourId);
            return View(bookTour);
        }

        // POST: Admin/BookTours/Confirm/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Confirm([Bind(Include = "Id,Numbers,Price,Note,StatusId,TourId,EmpId,CusId,Voucher")] BookTour bookTour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookTour).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CusId = new SelectList(db.Customers, "Id", "Email", bookTour.CusId);
            ViewBag.EmpId = new SelectList(db.Employees, "Id", "LoginName", bookTour.EmpId);
            ViewBag.StatusId = new SelectList(db.Status, "Id", "StatusName", bookTour.StatusId);
            ViewBag.TourId = new SelectList(db.Tours, "Id", "TourName", bookTour.TourId);
            return View(bookTour);
        }

        // GET: Admin/BookTours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookTour bookTour = db.BookTours.Find(id);
            var cur_emp = Session["EmpId"].ToString();
            if (bookTour == null || (bookTour.EmpId).ToString() != cur_emp)
            {
                return HttpNotFound();
            }
            return View(bookTour);
        }

        // POST: Admin/BookTours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookTour bookTour = db.BookTours.Find(id);
            db.BookTours.Remove(bookTour);
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
