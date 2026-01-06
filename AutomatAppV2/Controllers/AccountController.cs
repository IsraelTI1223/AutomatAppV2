using System.Linq;
using System.Web.Mvc;
using System.Security.Claims;
using System.Web;
using System.Web.Security;
using AutomatApp.Business.Login;
using AutomatApp.Entities.Models;
using System.Web.Services.Description;


namespace AutomatAppV2.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login(string message)
        {
            return View(model: message);
        }


        [HttpPost]
        public ActionResult SignIn(string correo,string pass)
        {

                var mail = correo.Trim() + "|" + pass.Trim();

                var res = new LoginBusines().Login(mail);

                UsuarioModel user = res.Result;    

                    
                if (res.Result != null)
                {
                Session["message"] = mail;
                return RedirectToAction("Index", "Home");                
                }
                else
                {
                    return RedirectToAction("Login", "Account", new { message = res.Messages.FirstOrDefault() });
                }
        }

        [HttpGet]
        public ActionResult SignOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}