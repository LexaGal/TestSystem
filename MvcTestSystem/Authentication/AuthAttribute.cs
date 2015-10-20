using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using TestDatabase.Entities;
using AuthorizeAttribute = System.Web.Mvc.AuthorizeAttribute;


namespace MvcTestSystem.Authentication
{
    public class AuthAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            string controller = httpContext.Request.RequestContext.RouteData.Values["controller"].ToString();
            User user = HttpContext.Current.Session["user"] as User;

            if (controller == "Account")
            {
                return true;
            }
            if (user != null)
            {
                if (controller == "Test")
                {
                    if (user.Role == "User")
                    {
                        return true;
                    }
                    HttpContext.Current.Response.RedirectPermanent("/Account/LogIn");
                    return false;
                }
                if (controller == "Admin")
                {
                    if (user.Role == "Admin")
                    {
                        return true;
                    }
                    HttpContext.Current.Response.RedirectPermanent("/Account/LogIn");
                    return false;
                }
            }
            HttpContext.Current.Response.RedirectPermanent("/Account/LogIn");
            return false;
        }
    }
}