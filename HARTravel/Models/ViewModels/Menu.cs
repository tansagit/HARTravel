using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HARTravel.Models.ViewModels
{
    public class Menu
    {
        HARTravelDBEntities db = null;
        public Menu()
        {
            db = new HARTravelDBEntities();
        }
        public List<Category> ListMenu()
        {
            return db.Categories.Where(x => x.Id !=0).ToList();
        }
        public List<SubCategory> ListSubMenu()
        {
            return db.SubCategories.Where(x => x.CategoryId == x.Category.Id).ToList();
        }
    }
}