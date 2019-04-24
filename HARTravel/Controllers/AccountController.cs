using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HARTravel.Models;
using HARTravel.Models.ViewModels;

namespace HARTravel.Controllers
{
    public class AccountController : Controller
    {
        HARTravelDBEntities db = new HARTravelDBEntities();
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string pass, string returnUrl)
        {
            var cus = db.Customers.Where(x => x.Email.Equals(email)).SingleOrDefault();
            if (cus != null)
            {
                if (!cus.IsActive)
                {
                    ViewBag.Message = "This account has been disabled";
                }
                else if (cus.Password.Equals(MySecurity.EncryptedPassword(pass)))
                {
                    FormsAuthentication.SetAuthCookie("c" + cus.Id, false);

                    Session["CusId"] = cus.Id.ToString();
                    Session["CusName"] = cus.CustomerName.ToString();

                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToAction("Index", "Tour");
                    }
                    else
                    {

                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    ViewBag.Message = "Password wrong!";
                }

            }
            else
            {
                ViewBag.Message = "Email wrong!";
            }
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterVM data)
        {
            try
            {
                var acc = db.Customers.Where(x => x.Email.Equals(data.Email)).SingleOrDefault();
                if (acc == null)
                {
                    if (data.ConfirmPassword.Equals(data.Password))
                    {
                        var cus = new Customer()
                        {
                            Email = data.Email,
                            Password = MySecurity.EncryptedPassword(data.Password),
                            CustomerName = data.CustomerName,
                            Phone = data.Phone,
                            IsActive = true
                        };
                        db.Customers.Add(cus);
                        db.SaveChanges();
                        Session["Email"] = cus.Email;
                        return View("RegisterSuccess");
                    }
                    else
                    {
                        ViewBag.Message = "Mat khau xac nhan ko khop";
                    }
                }
                else
                {
                    ViewBag.Message = "Tai khoan da ton tai!";
                }



            }
            catch (Exception ex)
            {

                ViewBag.Message = ex.Message;
            }
            return View(data);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}