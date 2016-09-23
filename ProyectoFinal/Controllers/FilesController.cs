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
using System.Collections.Specialized;
using System.IO;
using ProyectoFinal.Filters;
using PagedList;

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
        public ActionResult Index(int? id, string sortOrder, string currentFilter, string searchString, int? page)
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

            IEnumerable<Models.File> files;
            if(id!=null)
                files = fileRepository.GetFiles().Where(f => f.RoutineID == id);
            else
                files = fileRepository.GetFiles();

            #region search

            if (!String.IsNullOrEmpty(searchString))
            {
                files = files.Where(c => c.ExerciseName.ToLower().Contains(searchString));
            }
            #endregion

            #region OrderBy
            ViewBag.MuscleSortParm = String.IsNullOrEmpty(sortOrder) ? "muscle_desc" : "";
            ViewBag.RoutineNameSortParm = sortOrder == "routine_asc" ? "routine_desc" : "routine_asc";
            ViewBag.ExerciseSortParm = sortOrder == "exer_asc" ? "exer_desc" : "exer_asc";
            ViewBag.PesoSortParm = sortOrder == "peso_asc" ? "peso_desc" : "peso_asc";
            ViewBag.Repetitions = sortOrder == "rep_asc" ? "rep_desc" : "rep_asc";
            ViewBag.DayParm = sortOrder == "day_asc" ? "day_desc" : "day_asc";

            switch (sortOrder)
            {
                case "muscle_desc":
                    files = files.OrderByDescending(c => c.MuscleName);
                    break;
                case "routine_desc":
                    files = files.OrderByDescending(c => c.Routine.NameFile);
                    break;
                case "routine_asc":
                    files = files.OrderBy(c => c.Routine.NameFile);
                    break;
                case "exer_desc":
                    files = files.OrderByDescending(c => c.ExerciseName);
                    break;
                case "exer_asc":
                    files = files.OrderBy(c => c.ExerciseName);
                    break;
                case "peso_desc":
                    files = files.OrderByDescending(c => c.Peso);
                    break;
                case "peso_asc":
                    files = files.OrderBy(c => c.Peso);
                    break;
                case "rep_desc":
                    files = files.OrderByDescending(c => c.Repetitions);
                    break;
                case "rep_asc":
                    files = files.OrderBy(c => c.Repetitions);
                    break;
                case "day_desc":
                    files = files.OrderByDescending(c => c.Day);
                    break;
                case "day_asc":
                    files = files.OrderBy(c => c.Day);
                    break;
                default:
                    files = files.OrderBy(c => c.MuscleName);
                    break;
            }
            #endregion

            int pageNumber = (page ?? 1);
            int pageSize = ConfigurationManager.AppSettings["PageSize"] != null ? Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]) : 8;

            return View(files.ToPagedList(pageNumber, pageSize));
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
