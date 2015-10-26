using System.Web.Mvc;
using MvcTestSystem.Authentication;
using MvcTestSystem.Models;
using TestDatabase.Entities;

namespace MvcTestSystem.Controllers
{
    [Auth]
    public class AccountController : Controller
    {
        private readonly IAuthProvider _authProvider;
        
        public AccountController(IAuthProvider authProvider)
        {
            _authProvider = authProvider;
         }

        public ActionResult LogIn()
        {
            Session["user"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LoginModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User user = _authProvider.Authenticate(loginModel.Name, loginModel.Password);
                if (user != null)
                {
                    Session["user"] = user;

                    if (user.Role == Role.User.ToString())
                    {
                        return Redirect(returnUrl ?? Url.Action("Index", "Test"));
                    }
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                }
                TempData["CustomError"] = @"Error: User with such login and password does not exist";
            }
            return View();
        }

        public ActionResult Register(string returnUrl)
        {
            Session["returnUrl"] = returnUrl;
            return View(new RegisterModel());
        }


        [HttpPost]
        public ActionResult Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                User user = _authProvider.Authenticate(registerModel.Name, registerModel.Password);
                if (user != null)
                {
                    TempData["CustomError"] = string.Format("Error: User with name '{0}' already exists", user.Name);
                    return View();
                }
                
                user = _authProvider.Register(registerModel.Name, registerModel.Password, registerModel.Role);
                
                string returnUrl = Session["returnUrl"] as string;

                if (returnUrl != null && returnUrl.Contains("Admin")) return Redirect(Url.Action("Index", "Admin"));

                Session["user"] = user;
                if (user.Role == Role.User.ToString())
                {
                    return Redirect(Url.Action("Index", "Test"));
                }
                return Redirect(Url.Action("Index", "Admin"));
            }
            return View(registerModel);
        }

        public ActionResult LogOut()
        {
            Session["user"] = null;
            return View("LogIn");
        }

    }
}
