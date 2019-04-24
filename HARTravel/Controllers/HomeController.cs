using HARTravel.Models;
using HARTravel.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace HARTravel.Controllers
{
    
    public class HomeController : Controller
    {
        HARTravelDBEntities db= new HARTravelDBEntities();


        public ActionResult Index()
        {
           
            ViewBag.Title = "Home Page";
            ViewBag.Slides = new Slide().ListSlide();
            ViewBag.TourHots = new TourHot().ListTourHot();
            ViewBag.NewsAndEvents = new NewsAndEvent().ListNews(4);
            
            return View();
        }
        
        [ChildActionOnly]
        public ActionResult _Header()
        {
            var model = new Menu().ListMenu();
           
            return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult _SubMenu()
        {
            var model1 = new Menu().ListSubMenu();
            return PartialView(model1);
        }

        
    }
}
