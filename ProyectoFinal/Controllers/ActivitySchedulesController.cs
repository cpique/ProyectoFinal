using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoFinal.Models;

namespace ProyectoFinal.Controllers
{
    public class ActivitySchedulesController : Controller
    {
        private GymContext db = new GymContext();

        // GET: ActivitySchedules
        public ActionResult Index()
        {
            var activitySchedules = db.ActivitySchedules.Include(a => a.Activity);
            return View(activitySchedules.ToList());
        }

        // GET: ActivitySchedules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivitySchedule activitySchedule = db.ActivitySchedules.Find(id);
            if (activitySchedule == null)
            {
                return HttpNotFound();
            }
            return View(activitySchedule);
        }

        // GET: ActivitySchedules/Create
        public ActionResult Create()
        {
            ViewBag.ActivityID = new SelectList(db.Activities, "ActivityID", "Name");
            return View();
        }

        // POST: ActivitySchedules/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ActivityScheduleID,Day,HourFrom,HourTo,ActivityID")] ActivitySchedule activitySchedule)
        {
            if (ModelState.IsValid)
            {
                db.ActivitySchedules.Add(activitySchedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ActivityID = new SelectList(db.Activities, "ActivityID", "Name", activitySchedule.ActivityID);
            return View(activitySchedule);
        }

        // GET: ActivitySchedules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivitySchedule activitySchedule = db.ActivitySchedules.Find(id);
            if (activitySchedule == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActivityID = new SelectList(db.Activities, "ActivityID", "Name", activitySchedule.ActivityID);
            return View(activitySchedule);
        }

        // POST: ActivitySchedules/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ActivityScheduleID,Day,HourFrom,HourTo,ActivityID")] ActivitySchedule activitySchedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activitySchedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActivityID = new SelectList(db.Activities, "ActivityID", "Name", activitySchedule.ActivityID);
            return View(activitySchedule);
        }

        // GET: ActivitySchedules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivitySchedule activitySchedule = db.ActivitySchedules.Find(id);
            if (activitySchedule == null)
            {
                return HttpNotFound();
            }
            return View(activitySchedule);
        }

        // POST: ActivitySchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ActivitySchedule activitySchedule = db.ActivitySchedules.Find(id);
            db.ActivitySchedules.Remove(activitySchedule);
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
