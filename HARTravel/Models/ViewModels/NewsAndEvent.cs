using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HARTravel.Models.ViewModels
{
    public class NewsAndEvent
    {
        HARTravelDBEntities db = null;
        public NewsAndEvent()
        {
            db = new HARTravelDBEntities();
        }
        public List<News> ListNews(int top)
        {
            return db.News.OrderByDescending(x => x.DateCreate).Take(top).ToList();
        }
    }
}