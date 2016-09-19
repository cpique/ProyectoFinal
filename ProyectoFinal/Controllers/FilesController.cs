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
using System.Collections.Specialized;
using System.IO;

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
        public ActionResult Index(int? id, int? page)
        {
            int pageSize = ConfigurationManager.AppSettings["PageSize"] != null ? Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]) : 10;

            if (id!=null)
            {
                return View(fileRepository.GetFiles().Where(f => f.RoutineID == id).AsPagination(page??1, pageSize));
            }
            else
            {
                return View(fileRepository.GetFiles().AsPagination(page ?? 1, pageSize));
            }

        }

        // GET: Files/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProyectoFinal.Models.File file = fileRepository.GetFileByID((int)id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        // GET: Files/Create
        public ActionResult Create(int? id)
        {
            SelectList selectList;
            string viewName = string.Empty;
            if (id != null)
            {
                var routine = routineRepository.GetRoutineByID((int)id);
                var model = new ProyectoFinal.Models.File { Routine = routine, RoutineID = (int)id };
                selectList = new SelectList(routineRepository.GetRoutines(), "RoutineID", "Description", id.ToString());
                ViewBag.RoutineID = selectList;
                return View("CreateFromRoutine", model);
            }
            else
            {
                selectList = new SelectList(routineRepository.GetRoutines(), "RoutineID", "Description");
                ViewBag.RoutineID = selectList;
                return View();
            }

        }

        // POST: Files/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProyectoFinal.Models.File file)
        {
            if (ModelState.IsValid)
            {
                fileRepository.InsertFile(file);
                fileRepository.Save();
                return RedirectToAction("Index", new { id = file.RoutineID });
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
            ProyectoFinal.Models.File file = fileRepository.GetFileByID((int)id);
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
        public ActionResult Edit(ProyectoFinal.Models.File file)
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
            ProyectoFinal.Models.File file = fileRepository.GetFileByID((int)id);
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
            ProyectoFinal.Models.File file = fileRepository.GetFileByID((int)id);
            fileRepository.DeleteFile((int)id);
            fileRepository.Save();
            return RedirectToAction("Index");
        }

        public ActionResult IndexPDF(int id)
        {
            return View(fileRepository.GetFiles().Where(f => f.RoutineID == id));
        }

        public FileResult GeneratePDF(int id)
        {
            string apiKey = ConfigurationManager.AppSettings["API_KEY"];
            string value = string.Format("http://amosgym.azurewebsites.net/Files/IndexPDF/{0}", id.ToString());

            using (var client = new WebClient())
            {
                NameValueCollection options = new NameValueCollection();
                options.Add("apikey", apiKey);
                options.Add("value", value);

                // Call the API convert to a PDF
                byte[] result = client.UploadValues("http://api.html2pdfrocket.com/pdf", options);

                // Download to the client
                return File(result, System.Net.Mime.MediaTypeNames.Application.Pdf, "MiRutina.pdf");

            }
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
