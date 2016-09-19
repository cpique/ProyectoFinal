using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoFinal.Models;
using ProyectoFinal.Models.Repositories;
using MvcContrib.Pagination;
using System.Configuration;

namespace ProyectoFinal.Controllers
{
    public class ActivitySchedulesController : Controller
    {
        #region Properties
        private IActivityScheduleRepository activityScheduleRepository;
        private IActivityRepository activityRepository;
        #endregion

        #region Constructors
        public ActivitySchedulesController()
        {
            this.activityScheduleRepository = new ActivityScheduleRepository(new GymContext());
            this.activityRepository = new ActivityRepository(new GymContext());
        }

        public ActivitySchedulesController(IActivityScheduleRepository activityScheduleRepository, ActivityRepository activityRepository)
        {
            this.activityScheduleRepository = activityScheduleRepository;
            this.activityRepository = activityRepository;
        }
        #endregion

        // GET: ActivitySchedules
        public ActionResult Index(int? page)
        {
            int pageSize = ConfigurationManager.AppSettings["PageSize"] != null ? Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]) : 10;
            var activitySchedules = activityScheduleRepository.GetActivitySchedules()
                                                              .AsPagination(page ?? 1, pageSize);
            return View(activitySchedules);
        }

        // GET: ActivitySchedules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivitySchedule activitySchedule = activityScheduleRepository.GetActivityScheduleByID((int)id);
            if (activitySchedule == null)
            {
                return HttpNotFound();
            }
            return View(activitySchedule);
        }

        // GET: ActivitySchedules/Create
        public ActionResult Create()
        {
            ViewBag.ActivityID = new SelectList(activityRepository.GetActivities(), "ActivityID", "Name");
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
                activityScheduleRepository.InsertActivitySchedule(activitySchedule);
                activityScheduleRepository.Save();
                return RedirectToAction("Index");
            }

            ViewBag.ActivityID = new SelectList(activityRepository.GetActivities(), "ActivityID", "Name", activitySchedule.ActivityID);
            return View(activitySchedule);
        }

        // GET: ActivitySchedules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivitySchedule activitySchedule = activityScheduleRepository.GetActivityScheduleByID((int)id);
            if (activitySchedule == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActivityID = new SelectList(activityRepository.GetActivities(), "ActivityID", "Name", activitySchedule.ActivityID);
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
                activityScheduleRepository.UpdateActivitySchedule(activitySchedule);
                activityScheduleRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.ActivityID = new SelectList(activityRepository.GetActivities(), "ActivityID", "Name", activitySchedule.ActivityID);
            return View(activitySchedule);
        }

        // GET: ActivitySchedules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivitySchedule activitySchedule = activityScheduleRepository.GetActivityScheduleByID((int)id);
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
            ActivitySchedule activitySchedule = activityScheduleRepository.GetActivityScheduleByID((int)id);
            activityScheduleRepository.DeleteActivitySchedule((int)id);
            activityScheduleRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                activityScheduleRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
