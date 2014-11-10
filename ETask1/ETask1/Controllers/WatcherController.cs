using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ETask1.Models;

namespace ETask1.Controllers
{
    public class WatcherController : Controller
    {
        private ETaskContext db = new ETaskContext();

        //
        // GET: /Watcher/

        public ActionResult Index()
        {
            var watchers = db.Watchers.Include(w => w.Task).Include(w => w.User);
            return View(watchers.ToList());
        }

        //
        // GET: /Watcher/Details/5

        public ActionResult Details(string id = null)
        {
            Watcher watcher = db.Watchers.Find(id);
            if (watcher == null)
            {
                return HttpNotFound();
            }
            return View(watcher);
        }

        //
        // GET: /Watcher/Create

        public ActionResult Create()
        {
            ViewBag.TaskID = new SelectList(db.Tasks, "TaskID", "ProjectID");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName");
            return View();
        }

        //
        // POST: /Watcher/Create

        [HttpPost]
        public ActionResult Create(Watcher watcher)
        {
            if (ModelState.IsValid)
            {
                db.Watchers.Add(watcher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TaskID = new SelectList(db.Tasks, "TaskID", "ProjectID", watcher.TaskID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", watcher.UserID);
            return View(watcher);
        }

        //
        // GET: /Watcher/Edit/5

        public ActionResult Edit(string id = null)
        {
            Watcher watcher = db.Watchers.Find(id);
            if (watcher == null)
            {
                return HttpNotFound();
            }
            ViewBag.TaskID = new SelectList(db.Tasks, "TaskID", "ProjectID", watcher.TaskID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", watcher.UserID);
            return View(watcher);
        }

        //
        // POST: /Watcher/Edit/5

        [HttpPost]
        public ActionResult Edit(Watcher watcher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(watcher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TaskID = new SelectList(db.Tasks, "TaskID", "ProjectID", watcher.TaskID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", watcher.UserID);
            return View(watcher);
        }

        //
        // GET: /Watcher/Delete/5

        public ActionResult Delete(string id = null)
        {
            Watcher watcher = db.Watchers.Find(id);
            if (watcher == null)
            {
                return HttpNotFound();
            }
            return View(watcher);
        }

        //
        // POST: /Watcher/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            Watcher watcher = db.Watchers.Find(id);
            db.Watchers.Remove(watcher);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}