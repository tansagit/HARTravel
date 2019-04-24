using HARTravel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HARTravel.Controllers
{
    public class ContactController : Controller
    {
        HARTravelDBEntities db = new HARTravelDBEntities();
        // GET: Contact
        public ActionResult Index()
        {
            return View(db.ContactUs.ToList());
        }
    }
}