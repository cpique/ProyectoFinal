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
using System.Configuration;
using ProyectoFinal.Filters;
using PagedList;

namespace ProyectoFinal.Controllers
{
    [AuthorizationPrivilege(Role = "Admin")]
    public class ActivitiesController : Controller
    {
        #region Properties
        private IActivityRepository activityRepository;
        #endregion

        #region Constructors
        public ActivitiesController()
        {
            this.activityRepository = new ActivityRepository(new GymContext());
        }

        public ActivitiesController(IActivityRepository activityRepository)
        {
            this.activityRepository = activityRepository;
        }
        #endregion

        // GET: Activities
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var activities = activityRepository.GetActivities();

            #region search

            if (!String.IsNullOrEmpty(searchString))
            {
                activities = activities.Where(c => c.Name.ToLower().Contains(searchString));
            }
            #endregion

            #region OrderBy
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DescSortParm = sortOrder == "description_asc" ? "description_desc" : "description_asc";

            switch (sortOrder)
            {
                case "name_desc":
                    activities = activities.OrderByDescending(a => a.Name);
                    break;
                case "description_desc":
                    activities = activities.OrderByDescending(a => a.Description);
                    break;
                case "description_asc":
                    activities = activities.OrderBy(a => a.Description);
                    break;
                default:
                    activities = activities.OrderBy(a => a.Name);
                    break;
            }
            #endregion

            int pageNumber = (page ?? 1);
            int pageSize = ConfigurationManager.AppSettings["PageSize"] != null ? Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]) : 8;
            return View(activities.ToPagedList(pageNumber, pageSize));
        }

        // GET: Activities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = activityRepository.GetActivityByID((int)id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // GET: Activities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Activities/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ActivityID,Name,Description")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                activityRepository.InsertActivity(activity);
                activityRepository.Save();
                return RedirectToAction("Index");
            }

            return View(activity);
        }

        // GET: Activities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = activityRepository.GetActivityByID((int)id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Activities/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ActivityID,Name,Description")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                activityRepository.UpdateActivity(activity);
                activityRepository.Save();
                return RedirectToAction("Index");
            }
            return View(activity);
        }

        // GET: Activities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = activityRepository.GetActivityByID((int)id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Activity activity = activityRepository.GetActivityByID((int)id);
            activityRepository.DeleteActivity((int)id);
            activityRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                activityRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
