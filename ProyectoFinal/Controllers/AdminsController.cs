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
    public class AdminsController : Controller
    {
        #region Properties
        private IAdminRepository adminRepository;
        #endregion

        #region Constructors
        public AdminsController()
        {
            this.adminRepository = new AdminRepository(new GymContext());
        }

        public AdminsController(IAdminRepository adminRepository)
        {
            this.adminRepository = adminRepository;
        }
        #endregion

        // GET: Admins
        public ActionResult Index()
        {
            return View(adminRepository.GetAdmins());
        }

        // GET: Admins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = adminRepository.GetAdminByID((int)id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // GET: Admins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admins/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdminID,FirstName,LastName,DocType,DocNumber,BirthDate,DateFrom,DateTo,IdentityCard,Email,Password,PasswordSalt")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                adminRepository.InsertAdmin(admin);
                adminRepository.Save();
                return RedirectToAction("Index");
            }

            return View(admin);
        }

        // GET: Admins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = adminRepository.GetAdminByID((int)id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdminID,FirstName,LastName,DocType,DocNumber,BirthDate,DateFrom,DateTo,IdentityCard,Email,Password,PasswordSalt")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                adminRepository.UpdateAdmin(admin);
                adminRepository.Save();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        // GET: Admins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = adminRepository.GetAdminByID((int)id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin admin = adminRepository.GetAdminByID((int)id);
            adminRepository.DeleteAdmin((int)id);
            adminRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                adminRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
