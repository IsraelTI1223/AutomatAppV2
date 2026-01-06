using AutomatApp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutomatAppV2.Extensions;
using AutomatApp.Business.Login;
using System.Web.Services.Description;

namespace AutomatAppV2.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            var message = Session["message"]?.ToString();

            var usuario = new LoginBusines().Login(message.ToString());
            UsuarioModel user = usuario.Result;

            //var usuario = this.GetUsuario() ?? new UsuarioModel { Permisos = new List<ModuloModel>() };
            return PartialView(user);
        }
    }
}