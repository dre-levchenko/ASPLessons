using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MusicPortal.Models;
using MusicPortal.Models.ViewModels;
using System.IO;
using MusicPortal.Models.Security;

namespace MusicPortal.Controllers
{
    public class SongsController : Controller
    {
        private MusicPortalContext db = new MusicPortalContext();

        // GET: Songs
        public ActionResult Index()
        {
            return View(db.Songs.ToList());
        }

        // GET: Songs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = db.Songs.Include(g => g.Genres).FirstOrDefault(g => g.Id == id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        // GET: Songs/Create
        public ActionResult Create()
        {
            ViewBag.Genres = db.Genres.ToList();
            return View();
        }

        // POST: Songs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Author,Album,Year")] Song song, string[] selectedGenres, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                song.Genres = new List<Genre>();
                if (selectedGenres != null)
                {
                    foreach (var item in selectedGenres)
                    {
                        int genreId = Convert.ToInt32(item);
                        var genre = db.Genres.FirstOrDefault(g => g.Id == genreId);
                        if (genre != null)
                        {
                            song.Genres.Add(genre);
                        }
                    }
                }

                if (fileUpload == null)
                {
                    ModelState.AddModelError("", "Файл не указан!");
                }
                string filename = MD5Hasher.ComputeHash(Path.GetFileName(fileUpload.FileName) + DateTime.Now) + Path.GetExtension(fileUpload.FileName);
                string tempfolder = Server.MapPath("~/Songs");
                if (filename != null)
                {
                    fileUpload.SaveAs(Path.Combine(tempfolder, filename));
                    song.FilePath = filename;
                }

                var userName = Session["Name"].ToString();
                var publisher = db.Users.FirstOrDefault(u => u.Name == userName);
                song.Publisher = publisher;
                db.Songs.Add(song);
                db.SaveChanges();
                
                return View("Index", db.Songs.ToList());
            }

            return View(song);
        }

        // GET: Songs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = db.Songs.Include(g => g.Genres).FirstOrDefault(g => g.Id == id);
            if (song == null)
            {
                return HttpNotFound();
            }
            ViewBag.Genres = db.Genres.ToList();
            return View(song);
        }

        // POST: Songs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Author,Album,Year,Publisher,Genre")] Song song, string[] selectedGenres)
        {
            if (ModelState.IsValid)
            {
                Song mus = db.Songs.Include(g => g.Genres).FirstOrDefault(s => s.Id == song.Id);
                mus.Album = song.Album;
                mus.Author = song.Author;
                mus.Title = song.Title;
                mus.Year = song.Year;
                mus.Genres.RemoveRange(0, mus.Genres.Count);
                ViewBag.Genres = db.Genres.ToList();

                if (selectedGenres != null)
                {
                    foreach (var item in selectedGenres)
                    {
                        int genreId = Convert.ToInt32(item);
                        var genre = db.Genres.FirstOrDefault(g => g.Id == genreId);
                        if (genre != null)
                        {
                            mus.Genres.Add(genre);
                        }
                    }
                }

                db.Entry(mus).State = EntityState.Modified;
                db.SaveChanges();
                return View("Index", db.Songs.ToList());
            }
            ViewBag.Genres = db.Genres.ToList();
            return View(song);
        }

        // GET: Songs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = db.Songs.Include(g => g.Genres).FirstOrDefault(g => g.Id == id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Song song = db.Songs.Find(id);
            db.Songs.Remove(song);
            db.SaveChanges();
            var file = new FileInfo(Server.MapPath("~/Songs/" + song.FilePath));
            file.Delete();
            return View("Index", db.Songs.ToList());
        }

        public FileResult Download(int? id)
        {
            Song song = db.Songs.Find(id);
            // Путь к файлу
            string file_path = Server.MapPath("~/Songs/" + song.FilePath);
            // Тип файла - content-type
            string file_type = "song/" + Path.GetExtension(file_path).Replace(".", "");
            // Имя файла - необязательно
            string file_name = song.Author + " - " + song.Title + Path.GetExtension(file_path);
            return File(file_path, file_type, file_name);
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
