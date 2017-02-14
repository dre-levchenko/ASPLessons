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
    public class MessagesController : Controller
    {
        private GuestbookContext db = new GuestbookContext();

        // GET: Messages
        public ActionResult Index()
        {
            var messages = db.Messages.Include(m => m.User);
            return View(new MessagesModel()
            {
                NewMessage = new Message(),
                Messages = messages.ToList()
            });
        }

        // POST: Messages
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(MessagesModel messagesModel)
        {
            if (messagesModel != null)
            {
                Create(messagesModel.NewMessage);
            }

            var messages = db.Messages.Include(m => m.User);
            messagesModel.Messages = messages.ToList();
            return View(messagesModel);
        }

        // GET: Messages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // GET: Messages/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: Messages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MessageBody,MessageDate,UserId")] Message message)
        {
            if (ModelState.IsValid && Session["Id"] != null)
            {
                var sessionUser = new User()
                {
                    Id = Convert.ToInt32(Session["Id"]),
                    Name = Session["Name"].ToString()
                };
                var dbUser = db.Users.FirstOrDefault(u => u.Id == sessionUser.Id);

                message.MessageDate = DateTime.Now;
                message.User = dbUser;

                db.Messages.Add(message);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", message.User);
            return View(message);
        }

        // GET: Messages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", message.User);
            return View(message);
        }

        // POST: Messages/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MessageBody,MessageDate,UserId")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", message.User);
            return View(message);
        }

        // GET: Messages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Message message = db.Messages.Find(id);
            db.Messages.Remove(message);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
