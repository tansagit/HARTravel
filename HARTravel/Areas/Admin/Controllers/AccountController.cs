using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HARTravel.Models;


namespace HARTravel.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        HARTravelDBEntities db = new HARTravelDBEntities();
        // GET: Admin/Account

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string loginName, string pass, string ReturnUrl)
        {
            var emp = db.Employees.Where(x => x.LoginName.Equals(loginName)).SingleOrDefault();
            if (emp != null)
            {
                if (!emp.IsActive)
                {
                    ViewBag.Message = "This account has been disabled";
                }
                else if (emp.Password.Equals(MySecurity.EncryptedPassword(pass)))
                {

                    FormsAuthentication.SetAuthCookie("e" + emp.Id, false);     //ki tu e de phan biet emp vs cus

                    Session["EmpId"] = emp.Id.ToString();
                    Session["EmpName"] = emp.EmpName.ToString();
                    Session["EmpRole"] = emp.Role.RoleName.ToString();

                    if (string.IsNullOrEmpty(ReturnUrl))
                    {
                        return RedirectToAction("Index", "BookTours");
                    }
                    else
                    {
                        return Redirect(ReturnUrl);
                    }
                }
                else
                {
                    ViewBag.Message = "Password wrong!";
                }
            }
            else
            {
                ViewBag.Message = "Login name wrong!";
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}