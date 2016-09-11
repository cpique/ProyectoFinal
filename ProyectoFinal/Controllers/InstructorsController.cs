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
    public class InstructorsController : Controller
    {
        #region Properties
        private IInstructorRepository instructorRepository;
        private IActivityRepository activityRepository;
        #endregion

        #region Constructors
        public InstructorsController()
        {
            this.instructorRepository = new InstructorRepository(new GymContext());
            this.activityRepository = new ActivityRepository(new GymContext());
        }

        public InstructorsController(IInstructorRepository instructorRepository, IActivityRepository activityRepository)
        {
            this.instructorRepository = instructorRepository;
            this.activityRepository = activityRepository;
        }
        #endregion

        // GET: Instructors
        public ActionResult Index()
        {
            var instructors = instructorRepository.GetInstructors();
            return View(instructors.ToList());
        }

        // GET: Instructors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = instructorRepository.GetInstructorByID((int)id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        // GET: Instructors/Create
        public ActionResult Create()
        {
            ViewBag.ActivityID = new SelectList(activityRepository.GetActivities(), "ActivityID", "Name");
            return View();
        }

        // POST: Instructors/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InstructorID,FirstName,LastName,DocType,DocNumber,BirthDate,DateFrom,DateTo,IdentityCard,Email,Password,PasswordSalt,ActivityID")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                instructorRepository.InsertInstructor(instructor);
                instructorRepository.Save();
                return RedirectToAction("Index");
            }

            ViewBag.ActivityID = new SelectList(activityRepository.GetActivities(), "ActivityID", "Name", instructor.ActivityID);
            return View(instructor);
        }

        // GET: Instructors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = instructorRepository.GetInstructorByID((int)id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActivityID = new SelectList(activityRepository.GetActivities(), "ActivityID", "Name", instructor.ActivityID);
            return View(instructor);
        }

        // POST: Instructors/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InstructorID,FirstName,LastName,DocType,DocNumber,BirthDate,DateFrom,DateTo,IdentityCard,Email,Password,PasswordSalt,ActivityID")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                instructorRepository.UpdateInstructor(instructor);
                instructorRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.ActivityID = new SelectList(activityRepository.GetActivities(), "ActivityID", "Name", instructor.ActivityID);
            return View(instructor);
        }

        // GET: Instructors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = instructorRepository.GetInstructorByID((int)id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        // POST: Instructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Instructor instructor = instructorRepository.GetInstructorByID((int)id);
            instructorRepository.DeleteInstructor((int)id);
            instructorRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                instructorRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
