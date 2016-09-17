using ProyectoFinal.Filters;
using ProyectoFinal.Models;
using ProyectoFinal.Models.Repositories;
using ProyectoFinal.Models.ViewModels;
using ProyectoFinal.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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

        [AuthorizationPrivilege(Role = "Client")]
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

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Client client = Authenticate(model.Email, model.Password);
                if (client != null)
                {
                    Session["User"] = client;
                    Session["UserName"] = client.Email;
                    Session["Role"] = client.Role; 
                    return Redirect(Url.Action("Index", "Admins"));
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username or password");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            return View("Index");
        }

        [HttpGet]
        public ActionResult Access()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Access(string docNumber)
        {
            int documentNumber = Convert.ToInt32(docNumber);
            Client client = clientRepository.GetClients().Where(c => c.DocNumber == documentNumber).FirstOrDefault();

            if (client != null && clientRepository.HasActivePayment(client))
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

        private Client Authenticate(string username, string password)
        {
            Client client = clientRepository.GetClients().Where(c => c.Email == username).FirstOrDefault();
            if (PasswordUtilities.Compare(password, client.Password, client.PasswordSalt))
                return client;
            else
                return null;
        }
    }


}