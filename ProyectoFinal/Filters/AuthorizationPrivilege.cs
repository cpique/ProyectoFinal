using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ProyectoFinal.Utils;

namespace ProyectoFinal.Filters
{
    public class AuthorizationPrivilege : ActionFilterAttribute
    {
        public string Role { get; set; }
        public const string USER = "UserName";
        public const string ROLE = "Role";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string userName = GetValue(context, USER);
            string userRole = GetValue(context, ROLE);
            RouteValueDictionary routeValueDictionary = new RouteValueDictionary { { "controller", "Home" }, { "action", "Login" } };

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(userRole) || string.IsNullOrEmpty(this.Role) || userRole != this.Role)
            {
                context.Result = new RedirectToRouteResult(routeValueDictionary);
            }
            
            base.OnActionExecuting(context);
        }

        private string GetValue(ActionExecutingContext context, string key)
        {
            return (context.HttpContext.Session[key] != null) ? context.HttpContext.Session[key].ToString() : string.Empty;
        }

    }
}