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
    public class FilesController : Controller
    {
        #region Properties
        private IFileRepository fileRepository;
        private IRoutineRepository routineRepository;
        #endregion

        #region Constructors
        public FilesController()
        {
            this.fileRepository = new FileRepository(new GymContext());
            this.routineRepository = new RoutineRepository(new GymContext());
        }

        public FilesController(IFileRepository fileRepository, IRoutineRepository routineRepository)
        {
            this.fileRepository = fileRepository;
            this.routineRepository = routineRepository;
        }
        #endregion

        // GET: Files
        public ActionResult Index()
        {
            var files = fileRepository.GetFiles();
            return View(files.ToList());
        }

        // GET: Files/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            File file = fileRepository.GetFileByID((int)id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        // GET: Files/Create
        public ActionResult Create()
        {
            ViewBag.RoutineID = new SelectList(routineRepository.GetRoutines(), "RoutineID", "Description");
            return View();
        }

        // POST: Files/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FileID,NameFile,DisplayName,Extension,ContentType,FileData,FileSize,CreationDate,RoutineID")] File file)
        {
            if (ModelState.IsValid)
            {
                fileRepository.InsertFile(file);
                fileRepository.Save();
                return RedirectToAction("Index");
            }

            ViewBag.RoutineID = new SelectList(routineRepository.GetRoutines(), "RoutineID", "Description", file.RoutineID);
            return View(file);
        }

        // GET: Files/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            File file = fileRepository.GetFileByID((int)id);
            if (file == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoutineID = new SelectList(routineRepository.GetRoutines(), "RoutineID", "Description", file.RoutineID);
            return View(file);
        }

        // POST: Files/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FileID,NameFile,DisplayName,Extension,ContentType,FileData,FileSize,CreationDate,RoutineID")] File file)
        {
            if (ModelState.IsValid)
            {
                fileRepository.UpdateFile(file);
                fileRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.RoutineID = new SelectList(routineRepository.GetRoutines(), "RoutineID", "Description", file.RoutineID);
            return View(file);
        }

        // GET: Files/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            File file = fileRepository.GetFileByID((int)id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        // POST: Files/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            File file = fileRepository.GetFileByID((int)id);
            fileRepository.DeleteFile((int)id);
            fileRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                fileRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
