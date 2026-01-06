using AutomatApp.Business.CatProducto;
using AutomatApp.Business.CatWH;
using AutomatApp.Business.Login;
using AutomatApp.Entities.Models;
using AutomatApp.Entities.Models.CatProducto;
using AutomatApp.Entities.Models.CatWH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace AutomatAppV2.Controllers
{
    public class CatWHController : Controller
    {
        CatWHBusiness WHBusiness = new CatWHBusiness();
        // GET: CatWH
        public ActionResult Index()
        {
            var message = Session["message"]?.ToString();
            var User = new LoginBusines().Login(message);
            UsuarioModel usuario = User.Result;
            //var usuario = this.GetUsuario();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var pp = usuario.Permisos.ToList().Where(x => x.Modulo == "MANTENIMIENTO");
            var per = pp.FirstOrDefault().SubModulos.ToList();
            var action = per.FirstOrDefault().Acciones.ToList();

            foreach (var item in action)
            {
                if (item.Value.Equals("Actualizar") && Session["UsuarioActualiza"] == null)
                {
                    Session["UsuarioActualiza"] = item.Value;
                }

                if (item.Value.Equals("Registrar") && Session["UsuarioRegistra"] == null)
                {
                    Session["UsuarioRegistra"] = item.Value;
                }

                if (item.Value.Equals("Consultar") && ViewBag.PermisoConsulta == null)
                {
                    ViewBag.PermisoConsulta = "Consultar";
                }

                if (Session["UsuarioRegistra"] != null && Session["UsuarioActualiza"] != null && ViewBag.PermisoConsulta != null)
                {
                    break;
                }

            }
            if (Session["UsuarioRegistra"] == null)
            {
                Session["UsuarioRegistra"] = "Sin Permisos";
            }
            if (Session["UsuarioActualiza"] == null)
            {
                Session["UsuarioActualiza"] = "Sin Permisos";
            }
            if (ViewBag.PermisoConsulta == null)
            {
                ViewBag.PermisoConsulta = "Sin Permisos";
            }

            var Almacenes = WHBusiness.GetAll().OrderBy(x => x.WH).ToList();
            ViewBag.Almacenes = Almacenes;

            var viewModel = new WHProductModel
            {
                WHModel = Almacenes.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CrearWH(WHModel model)
        {
            if (model.WH != 0)
            {
                WHBusiness.CrearWHB(model);
            }

            return RedirectToAction("Index");
        }
      
    }
}