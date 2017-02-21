using MusicPortal.Models.ViewModels;
using MusicPortal.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicPortal.Models;
using System.Data;
using System.Data.Entity;
using System.Net;


namespace MusicPortal.Controllers
{
    public class AccountsController : Controller
    {
        private MusicPortalContext db = new MusicPortalContext();

        // GET: Accounts
        public ActionResult Index()
        {
            return View();
        }

        // GET: Accounts/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Accounts/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel logon)
        {
            if (ModelState.IsValid)
            {
                var dbUser = db.Users.FirstOrDefault(u => u.Name == logon.Name);
                
                if (dbUser == null ||
                    dbUser.Password != MD5Hasher.ComputeHash(logon.Password))
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(logon);
                }
                else if (dbUser != null &&
                    dbUser.Password == MD5Hasher.ComputeHash(logon.Password) &&
                    string.Equals(dbUser.Name, logon.Name))
                {
                    if (logon.Name == "admin")
                    {
                        Session["IsAdmin"] = true;
                    }
                    else
                    {
                        Session["IsAdmin"] = null;
                    }
                    Session["Name"] = logon.Name;
                    Session["Id"] = dbUser.Id;
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(logon);
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Accounts");
        }

        // GET: Accounts/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Accounts/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel register)
        {
            if (ModelState.IsValid)
            {
                var dbUser = db.Users.FirstOrDefault(u => u.Name == register.Name);

                if (dbUser == null || !string.Equals(dbUser.Name, register.Name))
                {
                    register.Password = MD5Hasher.ComputeHash(register.Password);

                    db.Users.Add(new User
                    {
                        Name = register.Name,
                        Password = register.Password
                    });
                    db.SaveChanges();

                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "Such username exists!");
                    return View(register);
                }
            }

            return View(register);
        }
    }
}