using ProyectoFinal.Filters;
using ProyectoFinal.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinal.Controllers
{
    [AuthorizationPrivilege(Role = "Admin")]
    [HandleError()]
    public class EmailsController : Controller
    {
        private String _templatePath;
        private String _finalTemplate;

        public String TemplatePath
        {
            get { return _templatePath; }
            set { _templatePath = value; }
        }

        public String FinalTempalte
        {
            get { return _finalTemplate; }
            set { _finalTemplate = value; }
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Preview(EmailViewModel model)
        {
            string result = "OK";
            string data = "some template";
            

            if (ModelState.IsValid)
            {
                this.FinalTempalte = this.BuildTemplate(model);
                data = this.FinalTempalte;
            }
            else
            {
                result = "NOOK";
                data = string.Empty;
            }

            return Json(new { Result = result, Data = data }, JsonRequestBehavior.AllowGet);
        }

        private string BuildTemplate(EmailViewModel model)
        {
            const string TITLE = "REPLACE_TEXT_TITLE";
            const string SUBTITLE = "REPLACE_TEXT_SUBTITLE";
            const string INNERTITLE = "REPLACE_TEXT_INNER";
            const string DESC = "REPLACE_TEXT_DESCRIPTION";
            this.TemplatePath = Server.MapPath(@"~/Templates/EmailTemplate.html");

            var template = System.IO.File.ReadAllText(TemplatePath);
            template = template.Replace(TITLE, model.Title)
                               .Replace(SUBTITLE, model.SubTitle)
                               .Replace(INNERTITLE, model.InnerTitle)
                               .Replace(DESC, model.Description);

            return template;
        }

    }
}