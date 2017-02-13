using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Guestbook.Models;
using Guestbook.Models.ViewModels;

namespace Guestbook.Controllers
{
    public class UsersController : Controller
    {
        private GuestbookContext db = new GuestbookContext();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Users/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel logon)
        {
            if (ModelState.IsValid)
            {
                var dbUser = db.Users.FirstOrDefault(u => u.Name == logon.Name);

                if (dbUser != null &&
                    dbUser.Password == Models.Security.MD5Hasher.ComputeHash(logon.Password) &&
                    string.Equals(dbUser.Name, logon.Name))
                {
                    Session["Name"] = logon.Name;
                    return RedirectToAction("Index");
                }
            }

            return View(logon);
        }

        // GET: Users/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Users/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel register)
        {
            if (ModelState.IsValid)
            {
                var dbUser = db.Users.FirstOrDefault(u => u.Name == register.Name);
                
                if (dbUser == null || !string.Equals(dbUser.Name, register.Name))
                {
                    register.Password = Models.Security.MD5Hasher.ComputeHash(register.Password);

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

                }
            }

            return View(register);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            Session.Abandon();

            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
