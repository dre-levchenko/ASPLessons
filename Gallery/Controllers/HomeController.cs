using Gallery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gallery.Controllers
{
    public class HomeController : Controller
    {
        private GalleryContext db = new GalleryContext();

        public ActionResult Index()
        {
            var dbUser = new User();
            if (Session["Name"] != null)
            {
                var username = Session["Name"].ToString();
                dbUser = db.Users.FirstOrDefault(u => u.Name == username);
            }
            return View(dbUser);
        }
    }
}