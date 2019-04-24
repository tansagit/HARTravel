using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HARTravel.Models;
using PagedList;

namespace HARTravel.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class ToursController : Controller
    {
        private HARTravelDBEntities db = new HARTravelDBEntities();
        int pageSize = 10;
        // GET: Admin/Tours
        public ActionResult Index(int? page, string sort, string id, string name)
        {
            int pageNumber = page ?? 1;

            var tours = db.Tours.Include(t => t.SubCategory).AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                tours = tours.Where(x => x.TourName.ToLower().Contains(name.ToLower())
                                || x.Id.ToString().ToLower().Contains(name.ToLower())
                                || x.Destination.DestinationName.ToLower().Contains(name.ToLower()));
            }
            
            if (string.IsNullOrEmpty(sort))
            {
                sort = "id_asc";
            }
            ViewBag.SortName = "name_asc";
            switch (sort)
            {
                case "id_asc":
                    tours = tours.OrderBy(x => x.Id);
                    ViewBag.SortId = "id_desc";
                    break;
                case "id_desc":
                    tours = tours.OrderByDescending(x => x.Id);
                    ViewBag.SortId = "id_asc";
                    break;

                case "name_asc":
                    tours = tours.OrderBy(x => x.TourName);
                    ViewBag.SortName = "name_desc";
                    break;
                case "name_desc":
                    tours = tours.OrderByDescending(x => x.TourName);
                    ViewBag.SortName = "name_asc";
                    break;
            }
            ViewBag.CurrentSort = sort;
            ViewBag.CurrenSearch = name;
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "Id", "SubCategoryName");
            ViewBag.StartingPlaceId = new SelectList(db.Destinations, "Id", "DestinationName");
            ViewBag.DestinationId = new SelectList(db.Destinations, "Id", "DestinationName");
            return View(tours.ToPagedList(pageNumber,pageSize));
        }
        [HttpPost]
        public ActionResult Index(int? page, string sort, int? SubCategoryId)
        {
            int pageNumber = page ?? 1;

            var tour = db.Tours.Include(t => t.SubCategory).Where(x => x.SubCategoryId == SubCategoryId).AsQueryable();
           
            
            if (string.IsNullOrEmpty(sort))
            {
                sort = "id_asc";
            }
            ViewBag.SortName = "name_asc";
            switch (sort)
            {
                case "id_asc":
                    tour = tour.OrderBy(x => x.Id);
                    ViewBag.SortId = "id_desc";
                    break;
                case "id_desc":
                    tour = tour.OrderByDescending(x => x.Id);
                    ViewBag.SortId = "id_asc";
                    break;

                case "name_asc":
                    tour = tour.OrderBy(x => x.TourName);
                    ViewBag.SortName = "name_desc";
                    break;
                case "name_desc":
                    tour = tour.OrderByDescending(x => x.TourName);
                    ViewBag.SortName = "name_asc";
                    break;
            }
            ViewBag.CurrentSort = sort;
           
            ViewBag.SubCategoryId = new SelectList(db.SubCategories.Where(x=>x.CategoryId==2), "Id", "SubCategoryName");
            ViewBag.StartingPlaceId = new SelectList(db.Destinations, "Id", "DestinationName");
            ViewBag.DestinationId = new SelectList(db.Destinations, "Id", "DestinationName");
            return View(tour.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Tours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // GET: Admin/Tours/Create
        public ActionResult Create()
        {
            ViewBag.StartingPlaceId = new SelectList(db.Destinations, "Id", "DestinationName");
            ViewBag.DestinationId = new SelectList(db.Destinations, "Id", "DestinationName");
            ViewBag.SubCategoryId = new SelectList(db.SubCategories.Where(x=>x.CategoryId==2), "Id", "SubCategoryName");
            return View();
        }

        // POST: Admin/Tours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,TourName,Description,Price,DaysOfTour,TourSchedule,Image,IsActive,StartingPlaceId,DestinationId,SubCategoryId,Hot,CreateDate")] Tour tour)
        {
            if (ModelState.IsValid)
            {
                tour.CreateDate = DateTime.Now;
                db.Tours.Add(tour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StartingPlaceId = new SelectList(db.Destinations, "Id", "DestinationName", tour.StartingPlaceId);
            ViewBag.DestinationId = new SelectList(db.Destinations, "Id", "DestinationName", tour.DestinationId);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories.Where(x=>x.CategoryId==2), "Id", "SubCategoryName", tour.SubCategoryId);
            return View(tour);
        }

        // GET: Admin/Tours/Edit/5
        [ValidateInput(false)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            ViewBag.StartingPlaceId = new SelectList(db.Destinations, "Id", "DestinationName", tour.StartingPlaceId);
            ViewBag.DestinationId = new SelectList(db.Destinations, "Id", "DestinationName", tour.DestinationId);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "Id", "SubCategoryName", tour.SubCategoryId);
            return View(tour);
        }

        // POST: Admin/Tours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,TourName,Description,Price,DaysOfTour,TourSchedule,Image,IsActive,StartingPlaceId,DestinationId,SubCategoryId,Hot,CreateDate")] Tour tour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StartingPlaceId = new SelectList(db.Destinations, "Id", "DestinationName", tour.StartingPlaceId);
            ViewBag.DestinationId = new SelectList(db.Destinations, "Id", "DestinationName", tour.DestinationId);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "Id", "SubCategoryName", tour.SubCategoryId);
            return View(tour);
        }

        // GET: Admin/Tours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // POST: Admin/Tours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tour tour = db.Tours.Find(id);
            db.Tours.Remove(tour);
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

        public string ChangeImage(string id, string image)
        {
            if (id == null)
            {
                return "Ma khong ton tai";
            }
            Tour b = db.Tours.Find(id);
            if (b == null)
            {
                return "Ma khong ton tai";
            }
            b.Image = image;
            db.SaveChanges();
            return "";
        }
    }
}
