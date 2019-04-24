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
    [Authorize(Roles = "Admin")]
    public class EmployeesController : Controller
    {
        private HARTravelDBEntities db = new HARTravelDBEntities();

        // GET: Admin/Employees
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.Role);
            return View(employees.ToList());
        }

        // GET: Admin/Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Admin/Employees/Create
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName");
            return View();
        }

        // POST: Admin/Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LoginName,Password,EmpName,Phone,Email,Gender,Address,Birthday,IdentityCard,RoleId,IsActive")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.Password = MySecurity.EncryptedPassword(employee.Password);
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName", employee.RoleId);
            return View(employee);
        }

        // GET: Admin/Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName", employee.RoleId);
            return View(employee);
        }

        // POST: Admin/Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LoginName,Password,EmpName,Phone,Email,Gender,Address,Birthday,IdentityCard,RoleId,IsActive")] Employee employee)
        {
            if (ModelState.IsValid)
            {

                db.Entry(employee).State = EntityState.Modified;
                employee.Password = MySecurity.EncryptedPassword(employee.Password);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName", employee.RoleId);
            return View(employee);
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
