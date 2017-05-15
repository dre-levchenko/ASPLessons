using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notebook.Models;
using Microsoft.EntityFrameworkCore;

namespace Notebook.Controllers
{
    public class NotesController : Controller
    {
        NotebookContext db;

        public NotesController(NotebookContext context)
        {
            db = context;
        }

        // GET: Notes
        public ActionResult Index(string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            var notes = from n in db.Notes select n;
            if (!String.IsNullOrEmpty(searchString))
            {
                var inputDate = Convert.ToDateTime(searchString);
                notes = notes.Where(n => n.Date.Year == inputDate.Year &&
                    n.Date.Month == inputDate.Month && 
                    n.Date.Day == inputDate.Day);
            }

            int pageSize = 5;

            return View(new NotesViewModel()
            {
                Notes = notes.ToList(),
                PageIndex = page ?? 1,
                TotalPages = pageSize
            });
            //return View(new PaginatedList<Note>(notes.ToList(), notes.Count(), page ?? 1, pageSize));
            //return View(await PaginatedList<Note>.CreateAsync(notes.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: Notes/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Notes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Notes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Notes/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Notes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Notes/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Notes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}