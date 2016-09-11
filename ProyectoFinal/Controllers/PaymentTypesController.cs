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
    public class PaymentTypesController : Controller
    {
        #region Properties
        private IPaymentTypeRepository paymentTypeRepository;
        private IActivityRepository activityRepository;
        #endregion

        #region Constructors
        public PaymentTypesController()
        {
            this.paymentTypeRepository = new PaymentTypeRepository(new GymContext());
            this.activityRepository = new ActivityRepository(new GymContext());
        }

        public PaymentTypesController(IPaymentTypeRepository paymentTypeRepository, IActivityRepository activityRepository)
        {
            this.paymentTypeRepository = paymentTypeRepository;
            this.activityRepository = activityRepository;
        }
        #endregion

        // GET: PaymentTypes
        public ActionResult Index()
        {
            var paymentTypes = paymentTypeRepository.GetPaymentTypes();
            return View(paymentTypes.ToList());
        }

        // GET: PaymentTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentType paymentType = paymentTypeRepository.GetPaymentTypeByID((int)id);
            if (paymentType == null)
            {
                return HttpNotFound();
            }
            return View(paymentType);
        }

        // GET: PaymentTypes/Create
        public ActionResult Create()
        {
            ViewBag.ActivityID = new SelectList(activityRepository.GetActivities(), "ActivityID", "Name");
            return View();
        }

        // POST: PaymentTypes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaymentTypeID,Description,DurationInMonths,Status,ActivityID")] PaymentType paymentType)
        {
            if (ModelState.IsValid)
            {
                paymentTypeRepository.InsertPaymentType(paymentType);
                paymentTypeRepository.Save();
                return RedirectToAction("Index");
            }

            ViewBag.ActivityID = new SelectList(activityRepository.GetActivities(), "ActivityID", "Name", paymentType.ActivityID);
            return View(paymentType);
        }

        // GET: PaymentTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentType paymentType = paymentTypeRepository.GetPaymentTypeByID((int)id);
            if (paymentType == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActivityID = new SelectList(activityRepository.GetActivities(), "ActivityID", "Name", paymentType.ActivityID);
            return View(paymentType);
        }

        // POST: PaymentTypes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaymentTypeID,Description,DurationInMonths,Status,ActivityID")] PaymentType paymentType)
        {
            if (ModelState.IsValid)
            {
                paymentTypeRepository.UpdatePaymentType(paymentType);
                paymentTypeRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.ActivityID = new SelectList(activityRepository.GetActivities(), "ActivityID", "Name", paymentType.ActivityID);
            return View(paymentType);
        }

        // GET: PaymentTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentType paymentType = paymentTypeRepository.GetPaymentTypeByID((int)id);
            if (paymentType == null)
            {
                return HttpNotFound();
            }
            return View(paymentType);
        }

        // POST: PaymentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PaymentType paymentType = paymentTypeRepository.GetPaymentTypeByID((int)id);
            paymentTypeRepository.DeletePaymentType((int)id);
            paymentTypeRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                paymentTypeRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
