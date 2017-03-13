﻿using Gallery.Models.ViewModels;
using Gallery.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gallery.Models;
using System.Data;
using System.Data.Entity;
using System.Net;


namespace Gallery.Controllers
{
    public class AccountsController : Controller
    {
        private GalleryContext db = new GalleryContext();

        // GET: Accounts
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Acounts/Details/5
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

        // GET: Accounts/Logout
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
                    
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Such username exists!");
                    return View(register);
                }
            }

            return View(register);
        }

        // GET: Accounts/Edit/5
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

        // POST: Accounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Password,IsActive")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Accounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User student = db.Users.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}