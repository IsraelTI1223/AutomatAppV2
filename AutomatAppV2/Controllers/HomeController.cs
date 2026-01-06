using AutomatApp.Business.Login;
using AutomatApp.Entities.Models;
using AutomatAppV2.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutomatAppV2.Controllers
{
    public class HomeController : BaseController
    {
        #region [Vistas]
        [HttpGet]
        public ActionResult Index()
        {
            var message = Session["message"]?.ToString();

            if (message == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                TempData["message"] = message.ToString();
                TempData.Keep("message");
                return View("~/Views/Home/index.cshtml");
            }
        }

        [HttpGet]
        public ActionResult ErrorPermiso()
        {
            ViewBag.ErrorMessage = this.GetErrorPermisos();
            return View();
        }
        #endregion

    }
}