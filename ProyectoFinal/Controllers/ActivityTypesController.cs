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

namespace ProyectoFinal.Controllers
{
    public class ActivityTypesController : Controller
    {
        #region Properties
        private IActivityTypeRepository activityTypeRepository;
        #endregion

        #region Constructors
        public ActivityTypesController()
        {
            this.activityTypeRepository = new ActivityTypeRepository(new GymContext());
        }

        public ActivityTypesController(IActivityTypeRepository activityTypeRepository)
        {
            this.activityTypeRepository = activityTypeRepository;
        }
        #endregion

        // GET: ActivityTypes
        public ActionResult Index()
        {
            return View(activityTypeRepository.GetActivityTypes());
        }

        // GET: ActivityTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityType activityType = activityTypeRepository.GetActivityTypeByID((int)id);
            if (activityType == null)
            {
                return HttpNotFound();
            }
            return View(activityType);
        }

        // GET: ActivityTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActivityTypes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ActivityTypeID,Description,Status")] ActivityType activityType)
        {
            if (ModelState.IsValid)
            {
                activityTypeRepository.InsertActivityType(activityType);
                activityTypeRepository.Save();
                return RedirectToAction("Index");
            }

            return View(activityType);
        }

        // GET: ActivityTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityType activityType = activityTypeRepository.GetActivityTypeByID((int)id);
            if (activityType == null)
            {
                return HttpNotFound();
            }
            return View(activityType);
        }

        // POST: ActivityTypes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ActivityTypeID,Description,Status")] ActivityType activityType)
        {
            if (ModelState.IsValid)
            {
                activityTypeRepository.UpdateActivityType(activityType);
                activityTypeRepository.Save();
                return RedirectToAction("Index");
            }
            return View(activityType);
        }

        // GET: ActivityTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityType activityType = activityTypeRepository.GetActivityTypeByID((int)id);
            if (activityType == null)
            {
                return HttpNotFound();
            }
            return View(activityType);
        }

        // POST: ActivityTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ActivityType activityType = activityTypeRepository.GetActivityTypeByID((int)id);
            activityTypeRepository.DeleteActivityType(id);
            activityTypeRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                activityTypeRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
