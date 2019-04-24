using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HARTravel.Models.ViewModels
{
    public class NewsView
    {
        HARTravelDBEntities db = null;
        public NewsView()
        {
            db = new HARTravelDBEntities();
        }
        public List<News> ListNews(string sub)
        {
            if (string.IsNullOrEmpty(sub))
                return db.News.ToList();
            else
                return db.News.Where(x =>  x.SubCategoryId.ToString() == sub).ToList();
        }
        public News getNews(string Id)
        {
            return db.News.Where(x => x.Id.ToString() == Id).FirstOrDefault();
        }
    }
}