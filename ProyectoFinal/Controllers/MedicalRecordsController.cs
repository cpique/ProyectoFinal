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
    public class MedicalRecordsController : Controller
    {
        #region Properties
        private IMedicalRecordRepository medicalRepository;
        private IClientRepository clientRepository;
        #endregion

        #region Constructors
        public MedicalRecordsController()
        {
            this.medicalRepository = new MedicalRecordRepository(new GymContext());
            this.clientRepository = new ClientRepository(new GymContext());
        }

        public MedicalRecordsController(IMedicalRecordRepository medicalRepository, IClientRepository clientRepository)
        {
            this.medicalRepository = medicalRepository;
            this.clientRepository = clientRepository;
        }
        #endregion

        // GET: MedicalRecords
        public ActionResult Index()
        {
            var medicalRecords = medicalRepository.GetMedicalRecords();
            return View(medicalRecords.ToList());
        }

        // GET: MedicalRecords/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalRecord medicalRecord = medicalRepository.GetMedicalRecordByID((int)id);
            if (medicalRecord == null)
            {
                return HttpNotFound();
            }
            return View(medicalRecord);
        }

        // GET: MedicalRecords/Create
        public ActionResult Create(int? id)
        {
            SelectList selectList;
            if (id != null)
                selectList = new SelectList(clientRepository.GetClients(), "ClientID", "FirstName", id.ToString());
            else
                selectList = new SelectList(clientRepository.GetClients(), "ClientID", "FirstName");

            ViewBag.ClientID = selectList;
            return View();
        }

        // POST: MedicalRecords/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MedicalRecordID,Weight,Heigth,Age,ClientID")] MedicalRecord medicalRecord)
        {
            if (ModelState.IsValid)
            {
                medicalRepository.InsertMedicalRecord(medicalRecord);
                medicalRepository.Save();
                return RedirectToAction("Index");
            }

            ViewBag.ClientID = new SelectList(clientRepository.GetClients(), "ClientID", "FirstName", medicalRecord.ClientID);
            return View(medicalRecord);
        }

        // GET: MedicalRecords/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalRecord medicalRecord = medicalRepository.GetMedicalRecordByID((int)id);
            if (medicalRecord == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientID = new SelectList(clientRepository.GetClients(), "ClientID", "FirstName", medicalRecord.ClientID);
            return View(medicalRecord);
        }

        // POST: MedicalRecords/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MedicalRecordID,Weight,Heigth,Age,ClientID")] MedicalRecord medicalRecord)
        {
            if (ModelState.IsValid)
            {
                medicalRepository.UpdateMedicalRecord(medicalRecord);
                medicalRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.ClientID = new SelectList(clientRepository.GetClients(), "ClientID", "FirstName", medicalRecord.ClientID);
            return View(medicalRecord);
        }

        // GET: MedicalRecords/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalRecord medicalRecord = medicalRepository.GetMedicalRecordByID((int)id);
            if (medicalRecord == null)
            {
                return HttpNotFound();
            }
            return View(medicalRecord);
        }

        // POST: MedicalRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MedicalRecord medicalRecord = medicalRepository.GetMedicalRecordByID((int)id);
            medicalRepository.DeleteMedicalRecord((int)id);
            medicalRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                medicalRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
