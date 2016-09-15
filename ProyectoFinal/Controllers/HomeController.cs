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
        private IAssistanceRepository assistanceRepository;

        public HomeController()
        {
            this.clientRepository = new ClientRepository(new GymContext());
            this.assistanceRepository = new AssistanceRepository(new GymContext());
        }

        public HomeController(IClientRepository clientRepository, IAssistanceRepository assistanceRepository)
        {
            this.clientRepository = clientRepository;
            this.assistanceRepository = assistanceRepository;
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
                //Guardar nueva asistencia
                Assistance assistance = new Assistance { assistanceDate = DateTime.Now, ClientID = client.ClientID };
                assistanceRepository.InsertAssistance(assistance);
                assistanceRepository.Save();

                return View("About");
            }
            else
            {
                return View("Contact");
            }
        }
    }
}