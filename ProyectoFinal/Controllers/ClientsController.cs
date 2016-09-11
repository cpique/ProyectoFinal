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

namespace ProyectoFinal.Controllers
{
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
        public ActionResult Index()
        {
            return View(clientRepository.GetClients());
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

        public ActionResult GetSamples()
        {
            List<Client> samples = new List<Client>();
            var passwordSalt1 = PasswordUtilities.CreateSalt(16);
            var password1 = PasswordUtilities.GenerateSHA256Hash("12345", passwordSalt1);
            var passwordSalt2 = PasswordUtilities.CreateSalt(16);
            var password2 = PasswordUtilities.GenerateSHA256Hash("335588", passwordSalt2);

            var clients = new List<Client>
            {
                new Client { FirstName = "John", LastName = "Doe", DocType = "DNI", DocNumber = 34578800, BirthDate = new DateTime(1990, 12, 31),
                DateFrom = new DateTime(2016, 09, 01), Email = "john.doe@hotmail.com",
                Password = password1, PasswordSalt = passwordSalt1 },

                new Client { FirstName = "Cristian", LastName = "Piqué", DocType = "DNI", DocNumber = 34578644, BirthDate = new DateTime(1989, 12, 31),
                DateFrom = new DateTime(2016, 09, 01), Email = "cristian.pique@hotmail.com",
                Password = password2, PasswordSalt = passwordSalt2 }
            };
            return View(clients);
        }

        public ActionResult GeneratePDF()
        {
            return new Rotativa.ActionAsPdf("GetSamples");
        }
    }
}
