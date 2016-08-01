using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        // GET: Authentication

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult DoLogin(UserDetails u)
        {
            if (ModelState.IsValid)
            {
                EmployeeBusinessLayer bal = new EmployeeBusinessLayer();
                UserStatus status = bal.GetUserValidity(u);
                bool IsAdmin = false;
                if (status == UserStatus.AuthenticatedAdmin)
                {
                    IsAdmin = true;
                }

                else if (status == UserStatus.AuthenticatedUser)
                {
                    IsAdmin = false;
                }
                else
                {
                    ModelState.AddModelError("CredentialError", "Invalid username or password.");
                    return View("Login");
                }
                FormsAuthentication.SetAuthCookie(u.UserName, false);
                Session["IsAdmin"] = IsAdmin;
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                return View("Login");
            }
        }


        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }

}
