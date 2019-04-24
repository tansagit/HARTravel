using HARTravel.Models;
using HARTravel.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace HARTravel.Controllers
{
    public class NewsController : Controller
    {
        HARTravelDBEntities db = new HARTravelDBEntities();
        // GET: News
        int pageSize = 10;
        // GET: Tour
        public ActionResult Index(int? page, string sort, string sub, string des)
        {
            int pageNumber = page ?? 1;
            var news = db.News.AsQueryable();
            if (string.IsNullOrEmpty(sort))
            {
                sort = "id_asc";
            }
            ViewBag.SortName = "name_asc";
            switch (sort)
            {
                case "id_asc":
                    news = news.OrderBy(x => x.Id);
                    ViewBag.SortId = "id_desc";
                    break;
                case "id_desc":
                    news = news.OrderByDescending(x => x.Id);
                    ViewBag.SortId = "id_asc";
                    break;

                case "name_asc":
                    news = news.OrderBy(x => x.Title);
                    ViewBag.SortName = "name_desc";
                    break;
                case "name_desc":
                    news = news.OrderByDescending(x => x.Title);
                    ViewBag.SortName = "name_asc";
                    break;
            }

            ViewBag.CurrentSort = sort;
            ViewBag.AllNews = new NewsView().ListNews(sub);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories.Where(x => x.CategoryId == 3), "Id", "SubCategoryName");
            return View(news.ToPagedList(pageNumber, pageSize));


        }
        public ActionResult NewDetail(string Id)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                News news = new NewsView().getNews(Id);
                if (news != null)
                    return View(news);
                else
                    return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }
    }
}