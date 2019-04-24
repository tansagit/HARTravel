using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HARTravel.Models.ViewModels
{
    public class TourHot
    {
        HARTravelDBEntities db = null;
        public TourHot()
        {
            db = new HARTravelDBEntities();
        }
        public List<Tour> ListTourHot()
        {
            return db.Tours.Where(x => x.Hot==true).ToList();
        }
    }
}