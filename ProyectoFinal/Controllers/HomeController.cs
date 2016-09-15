using ProyectoFinal.Models;
using ProyectoFinal.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinal.Controllers
{
    public class HomeController : Controller
    {
        private IClientRepository clientRepository;

        public HomeController()
        {
            this.clientRepository = new ClientRepository(new GymContext());
        }

        public HomeController(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult RegisterAccess(string docNumber)
        {
            int documentNumber = Convert.ToInt32(docNumber);
            Client client = clientRepository.GetClients().Where(c => c.DocNumber == documentNumber).FirstOrDefault();

            if(client != null && clientRepository.HasActivePayment(client))
            {
                return View("About");
            }
            else
            {
                return View("Contact");
            }
        }
    }
}