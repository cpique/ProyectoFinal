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
    public class RoutinesController : Controller
    {
        #region Properties
        private IRoutineRepository routineRepository;
        private IClientRepository clientRepository;
        #endregion

        #region Constructors
        public RoutinesController()
        {
            this.routineRepository = new RoutineRepository(new GymContext());
            this.clientRepository = new ClientRepository(new GymContext());
        }

        public RoutinesController(IRoutineRepository routineRepository, IClientRepository clientRepository)
        {
            this.routineRepository = routineRepository;
            this.clientRepository = clientRepository;
        }
        #endregion

        // GET: Routines
        public ActionResult Index()
        {
            var routines = routineRepository.GetRoutines();
            return View(routines);
        }

        // GET: Routines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Routine routine = routineRepository.GetRoutineByID((int)id);
            if (routine == null)
            {
                return HttpNotFound();
            }
            return View(routine);
        }

        // GET: Routines/Create
        public ActionResult Create()
        {
            ViewBag.ClientID = new SelectList(clientRepository.GetClients(), "ClientID", "FirstName");
            return View();
        }

        // POST: Routines/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Routine routine)
        {
            if (ModelState.IsValid)
            {
                routineRepository.InsertRoutine(routine);
                routineRepository.Save();
                return RedirectToAction("Index");
            }

            ViewBag.ClientID = new SelectList(clientRepository.GetClients(), "ClientID", "FirstName", routine.ClientID);
            return View(routine);
        }

        // GET: Routines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Routine routine = routineRepository.GetRoutineByID((int)id);
            if (routine == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientID = new SelectList(clientRepository.GetClients(), "ClientID", "FirstName", routine.ClientID);
            return View(routine);
        }

        // POST: Routines/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Routine routine)
        {
            if (ModelState.IsValid)
            {
                routineRepository.UpdateRoutine(routine);
                routineRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.ClientID = new SelectList(clientRepository.GetClients(), "ClientID", "FirstName", routine.ClientID);
            return View(routine);
        }

        // GET: Routines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Routine routine = routineRepository.GetRoutineByID((int)id);
            if (routine == null)
            {
                return HttpNotFound();
            }
            return View(routine);
        }

        // POST: Routines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Routine routine = routineRepository.GetRoutineByID((int)id);
            routineRepository.DeleteRoutine((int)id);
            routineRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                routineRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
