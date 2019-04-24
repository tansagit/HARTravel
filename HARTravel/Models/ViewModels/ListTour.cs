using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace HARTravel.Models.ViewModels

{
    public class ListTour
    {
        HARTravelDBEntities db = null;
        public ListTour()
        {
            db = new HARTravelDBEntities();
        }
        public List<Tour> AllTour(string sub, string des)
        {
            
            if (string.IsNullOrEmpty(sub) && string.IsNullOrEmpty(des))
            {
                return db.Tours.Where(x => x.IsActive == true).ToList();
            }
            else
            {

                if (!string.IsNullOrEmpty(sub) && string.IsNullOrEmpty(des))
                {
                    return db.Tours.Where(x => x.IsActive == true && x.SubCategoryId.ToString() == sub).ToList();
                }
                else
                {
                    if(string.IsNullOrEmpty(sub) && !string.IsNullOrEmpty(des))
                    {
                        return db.Tours.Where(x => x.IsActive == true && x.DestinationId.ToString() == des).ToList();
                    }
                }
            }
            return db.Tours.Where(x => x.IsActive == true && x.SubCategoryId.ToString() == sub && x.DestinationId.ToString() == des).ToList();
            
            
            
                
           
                
        }
        public List<string> ListName(string keyword)
        {
            return db.Destinations.Where(x => x.DestinationName.Contains(keyword)).Select(x => x.DestinationName).ToList();
        }
    }
}