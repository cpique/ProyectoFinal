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
    public class PaymentsController : Controller
    {
        #region Properties
        private IPaymentRepository paymentRepository;
        private IPaymentTypeRepository paymentTypeRepository;
        private IClientRepository clientRepository;
        #endregion

        #region Constructors
        public PaymentsController()
        {
            this.paymentRepository = new PaymentRepository(new GymContext());
            this.paymentTypeRepository = new PaymentTypeRepository(new GymContext());
            this.clientRepository = new ClientRepository(new GymContext());
        }

        public PaymentsController(IPaymentRepository paymentRepository, IPaymentTypeRepository paymentTypeRepository, IClientRepository clientRepository)
        {
            this.paymentRepository = paymentRepository;
            this.paymentTypeRepository = paymentTypeRepository;
            this.clientRepository = clientRepository;
        }
        #endregion


        // GET: Payments
        public ActionResult Index()
        {
            var payments = paymentRepository.GetPayments();
            return View(payments.ToList());
        }

        // GET: Payments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = paymentRepository.GetPaymentByID((int)id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // GET: Payments/Create
        public ActionResult Create()
        {
            ViewBag.ClientID = new SelectList(clientRepository.GetClients(), "ClientID", "FirstName");
            ViewBag.PaymentTypeID = new SelectList(paymentTypeRepository.GetPaymentTypes(), "PaymentTypeID", "Description");
            return View();
        }

        // POST: Payments/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaymentID,Status,ClientID,PaymentTypeID")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                paymentRepository.InsertPayment(payment);
                paymentRepository.Save();
                return RedirectToAction("Index");
            }

            ViewBag.ClientID = new SelectList(clientRepository.GetClients(), "ClientID", "FirstName", payment.ClientID);
            ViewBag.PaymentTypeID = new SelectList(paymentTypeRepository.GetPaymentTypes(), "PaymentTypeID", "Description", payment.PaymentTypeID);
            return View(payment);
        }

        // GET: Payments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = paymentRepository.GetPaymentByID((int)id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientID = new SelectList(clientRepository.GetClients(), "ClientID", "FirstName", payment.ClientID);
            ViewBag.PaymentTypeID = new SelectList(paymentTypeRepository.GetPaymentTypes(), "PaymentTypeID", "Description", payment.PaymentTypeID);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaymentID,Status,ClientID,PaymentTypeID")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                paymentRepository.UpdatePayment(payment);
                paymentRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.ClientID = new SelectList(clientRepository.GetClients(), "ClientID", "FirstName", payment.ClientID);
            ViewBag.PaymentTypeID = new SelectList(paymentTypeRepository.GetPaymentTypes(), "PaymentTypeID", "Description", payment.PaymentTypeID);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = paymentRepository.GetPaymentByID((int)id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment payment = paymentRepository.GetPaymentByID((int)id);
            paymentRepository.DeletePayment((int)id);
            paymentRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                paymentRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
