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
    public class MachinesController : Controller
    {
        #region Properties
        private IMachineRepository machineRepository;
        private ISupplierRepository supplierRepository;
        #endregion

        #region Constructors
        public MachinesController()
        {
            this.machineRepository = new MachineRepository(new GymContext());
            this.supplierRepository = new SupplierRepository(new GymContext());
        }

        public MachinesController(IMachineRepository machineRepository, ISupplierRepository supplierRepository)
        {
            this.machineRepository = machineRepository;
            this.supplierRepository = supplierRepository;
        }
        #endregion

        // GET: Machines
        public ActionResult Index()
        {
            var machines = machineRepository.GetMachines();
            return View(machines.ToList());
        }

        // GET: Machines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine machine = machineRepository.GetMachineByID((int)id);
            if (machine == null)
            {
                return HttpNotFound();
            }
            return View(machine);
        }

        // GET: Machines/Create
        public ActionResult Create()
        {
            ViewBag.SupplierID = new SelectList(supplierRepository.GetSuppliers(), "SupplierID", "BusinessName");
            return View();
        }

        // POST: Machines/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MachineID,Type,Price,Status,PurchaseDate,SupplierID")] Machine machine)
        {
            if (ModelState.IsValid)
            {
                machineRepository.InsertMachine(machine);
                machineRepository.Save();
                return RedirectToAction("Index");
            }

            ViewBag.SupplierID = new SelectList(supplierRepository.GetSuppliers(), "SupplierID", "BusinessName", machine.SupplierID);
            return View(machine);
        }

        // GET: Machines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine machine = machineRepository.GetMachineByID((int)id);
            if (machine == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupplierID = new SelectList(supplierRepository.GetSuppliers(), "SupplierID", "BusinessName", machine.SupplierID);
            return View(machine);
        }

        // POST: Machines/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MachineID,Type,Price,Status,PurchaseDate,SupplierID")] Machine machine)
        {
            if (ModelState.IsValid)
            {
                machineRepository.UpdateMachine(machine);
                machineRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.SupplierID = new SelectList(supplierRepository.GetSuppliers(), "SupplierID", "BusinessName", machine.SupplierID);
            return View(machine);
        }

        // GET: Machines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine machine = machineRepository.GetMachineByID((int)id);
            if (machine == null)
            {
                return HttpNotFound();
            }
            return View(machine);
        }

        // POST: Machines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Machine machine = machineRepository.GetMachineByID((int)id);
            machineRepository.DeleteMachine((int)id);
            machineRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                machineRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
