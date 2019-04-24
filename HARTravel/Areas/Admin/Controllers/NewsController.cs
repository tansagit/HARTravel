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
    public class NewsController : Controller
    {
        private HARTravelDBEntities db = new HARTravelDBEntities();

        // GET: Admin/News
        public ActionResult Index()
        {
            var news = db.News.Include(n => n.Employee).Include(n => n.SubCategory);
            return View(news.ToList());
        }

        // GET: Admin/News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: Admin/News/Create
        [ValidateInput(false)]
        public ActionResult Create()

        {
            ViewBag.EmpId = new SelectList(db.Employees, "Id", "LoginName");
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "Id", "SubCategoryName");
            return View();
        }

        // POST: Admin/News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,Title,SubTitle,Content,Image,DateCreate,Tag,EmpId,SubCategoryId")] News news)
        {
            if (ModelState.IsValid)
            {
                db.News.Add(news);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmpId = new SelectList(db.Employees, "Id", "LoginName", news.EmpId);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "Id", "SubCategoryName", news.SubCategoryId);
            return View(news);
        }

        // GET: Admin/News/Edit/5
        [ValidateInput(false)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmpId = new SelectList(db.Employees, "Id", "LoginName", news.EmpId);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "Id", "SubCategoryName", news.SubCategoryId);
            return View(news);
        }

        // POST: Admin/News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,Title,SubTitle,Content,Image,DateCreate,Tag,EmpId,SubCategoryId")] News news)
        {
            if (ModelState.IsValid)
            {
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmpId = new SelectList(db.Employees, "Id", "LoginName", news.EmpId);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "Id", "SubCategoryName", news.SubCategoryId);
            return View(news);
        }

        // GET: Admin/News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: Admin/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            db.News.Remove(news);
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
