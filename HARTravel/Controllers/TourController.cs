using HARTravel.Models;
using System;
using HARTravel.Models.ViewModels;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Configuration;
using HARTravel.Models.Common;
using System.Web.Helpers;

namespace HARTravel.Controllers
{
    public class TourController : Controller
    {
        private HARTravelDBEntities db = new HARTravelDBEntities();
        int pageSize = 10;
        // GET: Tour
        public ActionResult Index(int? page, string sort, string sub, string des)
        {
            int pageNumber = page ?? 1;
            var tours = db.Tours.Include(t=>t.SubCategory).AsQueryable();
            if (string.IsNullOrEmpty(sub) && string.IsNullOrEmpty(des))
            {
                tours= db.Tours.Where(x => x.IsActive == true).Include(t=>t.SubCategory).AsQueryable();
            }
            else
            {

                if (!string.IsNullOrEmpty(sub) && string.IsNullOrEmpty(des))
                {
                    tours = db.Tours.Where(x => x.IsActive == true && x.SubCategoryId.ToString() == sub).ToList().AsQueryable();
                }
                else
                {
                    if (string.IsNullOrEmpty(sub) && !string.IsNullOrEmpty(des))
                    {
                        tours= db.Tours.Where(x => x.IsActive == true && x.DestinationId.ToString() == des).AsQueryable();
                    }
                }
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
            
            ViewBag.SubCategoryId = new SelectList(db.SubCategories.Where(x=>x.CategoryId==2), "Id", "SubCategoryName");
            return View(tours.ToPagedList(pageNumber, pageSize));
            
            
        }
        [HttpPost]
        public ActionResult Index(int SubCategoryId,string location,string day,int? page,string sort)
        {
            int pageNumber = page ?? 1;
            var tours = db.Tours.Include(t => t.SubCategory).Where(x => x.SubCategoryId == SubCategoryId).AsQueryable();
            if (!string.IsNullOrEmpty(location))
            {
                tours = tours.Include(t => t.SubCategory).Where(x => x.SubCategoryId == SubCategoryId 
                                                                    
                                                                    && x.Destination.DestinationName.ToLower().Contains(location.ToLower())).AsQueryable();
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


            ViewBag.SubCategoryId = new SelectList(db.SubCategories.Where(x=>x.CategoryId==2), "Id", "SubCategoryName");
            return View(tours.ToPagedList(pageNumber, pageSize));
        }

        // GET: Tour/Details/5
        public ActionResult TourDetail(int? id)
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


        // GET: Tour/Create
        public ActionResult Purchase(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Tour tour = db.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }


        [HttpPost]
        public ActionResult Purchase(int? id, string CusName, string CusPhone, string CusEmail, int number, DateTime dayStart, string voucher, string note)
        {
            decimal voucher_price = 0;
            voucher = null;
            foreach (var v in db.Vouchers)
            {
                if (v.SerialNumber.Equals(voucher))
                {
                    voucher_price = v.Price;
                }
                else
                {
                    ViewBag.Message = "Voucher ko ton tai";
                }
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour cur_tour = db.Tours.Find(id);
            if (cur_tour == null)
            {
                return HttpNotFound();
            }
            else
            {
                try
                {
                    var cus = db.Customers.Where(x => x.Email.Equals(CusEmail)).SingleOrDefault();
                    if (cus != null)
                    {
                        var booktour = new BookTour()
                       

                        {
                            Numbers = number,
                            Price = (cur_tour.Price - voucher_price) * number,
                            Note = note,
                            Voucher = voucher,
                            StatusId = 1,
                            TourId = cur_tour.Id,
                            EmpId = 1,
                            CusId = cus.Id,
                            DayStart=dayStart.Date
                        };
                        db.BookTours.Add(booktour);
                        db.SaveChanges();
                        string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/Client/template/newtour.html"));

                        content = content.Replace("{{CustomerName}}", CusName);
                        content = content.Replace("{{Phone}}", CusPhone);
                        content = content.Replace("{{Email}}", CusEmail);
                        content = content.Replace("{{Number}}", number.ToString());
                        content = content.Replace("{{day}}", dayStart.ToString());
                        content = content.Replace("{{Total}}", booktour.Price.ToString("N0"));
                        content = content.Replace("{{Schedule}}", booktour.Tour.TourSchedule);
                        var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                        new MailHelper().SendMail(CusEmail, "Đơn tour từ HAR Travel", content);
                        new MailHelper().SendMail(toEmail, "Đơn hàng mới từ OnlineShop", content);
                        return View("Finish");
                    }
                    else
                    {
                        cus = new Customer()
                        {
                            Email = CusEmail,
                            Password = "123",
                            CustomerName = CusName,
                            Phone = CusPhone,
                            IsActive = false
                        };
                        db.Customers.Add(cus);
                        db.SaveChanges();

                        var booktour = new BookTour()
                        {
                            Numbers = number,
                            Price = (cur_tour.Price - voucher_price) * number,
                            Note = note,
                            Voucher = voucher,
                            StatusId = 1,
                            TourId = cur_tour.Id,
                            EmpId = 1,
                            CusId = cus.Id,
                            DayStart=dayStart.Date
                            
                        };
                        db.BookTours.Add(booktour);
                        db.SaveChanges();

                        string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/Client/template/newtour.html"));

                        content = content.Replace("{{CustomerName}}", CusName);
                        content = content.Replace("{{Phone}}", CusPhone);
                        content = content.Replace("{{Email}}", CusEmail);
                        content = content.Replace("{{Number}}",number.ToString());
                        content = content.Replace("{{day}}",dayStart.ToString());
                        content = content.Replace("{{Total}}", booktour.Price.ToString("N0"));
                        content = content.Replace("{{Schedule}}", booktour.Tour.TourSchedule);
                        var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                        new MailHelper().SendMail(CusEmail, "Đơn tour từ HAR Travel", content);
                        new MailHelper().SendMail(toEmail, "Đơn hàng mới từ OnlineShop", content);
                        return View("Finish");
                    }


                }
                catch (Exception ex)
                {

                    ViewBag.Message = ex.Message;
                }
            }
            return View();
        }
        public ActionResult Confirm()
        {
            return View();
        }

        public ActionResult Finish()
        {
            return View();
        }
        public JsonResult ListName(string q)
        {
            var data = new ListTour().ListName(q);
            return Json(new {
                data=data,
                status=true
            }, JsonRequestBehavior.AllowGet);
        }

    }
}
