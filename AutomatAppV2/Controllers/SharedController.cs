using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutomatApp.Business.Login;
using AutomatApp.Entities.Models;

namespace AutomatAppV2.Controllers
{
    public class SharedController : Controller
    {
        // GET: Shared
        public ActionResult TopMenu()
        {
            var message = Session["message"]?.ToString();

            var usuario = new LoginBusines().Login(message.ToString());
            UsuarioModel user = usuario.Result;       
            
            return PartialView("_topMenu", user);
        }
    }
}