using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HARTravel.Models.ViewModels
{
    public class Slide
    {
        HARTravelDBEntities db = null;
        public Slide()
        {
            db = new HARTravelDBEntities();
        }
        public List<Destination> ListSlide()
        {
            return db.Destinations.Where(x => x.Hot==true).ToList();
        }
    }
}