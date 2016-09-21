using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoFinal.Models;
using ProyectoFinal.Utils;
using ProyectoFinal.Models.Repositories;
using System.Configuration;
using MvcContrib.Pagination;
using ProyectoFinal.Filters;

namespace ProyectoFinal.Controllers
{
    [AuthorizationPrivilege(Role = "Admin")]
    public class ClientsController : Controller
    {
        private IClientRepository clientRepository;

        public ClientsController()
        {
            this.clientRepository = new ClientRepository(new GymContext());
        }

        public ClientsController(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        // GET: Clients
        public ActionResult Index(int? page)
        {
            int pageSize = ConfigurationManager.AppSettings["PageSize"] != null ? Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]) : 10;
            var clients = clientRepository.GetClients()
                                          .AsPagination(page ?? 1, pageSize);
            return View(clients);
        }

        // GET: Clients/Details/5
        public ActionResult Details(int id)
        {
            Client client = clientRepository.GetClientByID(id);
            if (client == null)
            {
                return HttpNotFound();
            }

            return View(client);

        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ClientID,FirstName,LastName,DocType,DocNumber,BirthDate,DateFrom,DateTo,IdentityCard,Email,Password,PasswordSalt")] Client client)
        public ActionResult Create(Client client)
        {
            if (ModelState.IsValid)
            {
                clientRepository.HashPassword(client);
                clientRepository.InsertClient(client);
                clientRepository.Save();
                return RedirectToAction("Create", "MedicalRecords", new { ClientID = client.ClientID  });
            }

            return View(client);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = clientRepository.GetClientByID((int)id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientID,FirstName,LastName,DocType,DocNumber,BirthDate,DateFrom,IdentityCard,Email,Password")] Client client)
        {
            if (ModelState.IsValid)
            {
                clientRepository.HashPassword(client);
                clientRepository.UpdateClient(client);
                clientRepository.Save();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = clientRepository.GetClientByID((int)id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = clientRepository.GetClientByID(id);
            clientRepository.DeleteClient(id);
            clientRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                clientRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
