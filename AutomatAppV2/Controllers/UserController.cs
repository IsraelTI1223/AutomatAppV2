using AutomatAppV2.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutomatApp.Business.Users;
using AutomatApp.Entities.Models;
using AutomatApp.Business.Login;

namespace AutomatAppV2.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        // GET: User
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
            var pp = usuario.Permisos.ToList().Where(x => x.Modulo == "SEGURIDAD");
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


            UserConfiguration getUsers = new UserConfiguration();
            var list = getUsers.GetUsuarios().OrderBy(x => x.Nombre).ToList();


            return View(list);
        }

        public ActionResult Crud(int id = 0)
        {

            var message = Session["message"]?.ToString();
            var User = new LoginBusines().Login(message);
            UsuarioModel usuario = User.Result;

            //var usuario = this.GetUsuario();
            UserConfiguration UsersC = new UserConfiguration();
            var perfiles = UsersC.GetPerfilAll().OrderBy(x => x.Nombre).ToList();

            if (id == 0)
            {
                ViewBag.Perfil = new SelectList(perfiles, "IdPerfil", "Nombre");
                return View();
            }
            else
            {
                var user = UsersC.GetUsuarios().Where(x => x.IdUsusario == id);
                var datos = user.First();
                ViewBag.Perfil = new SelectList(perfiles, "IdPerfil", "Nombre", datos.idPerfil);
                return View(datos);
            }
        }

        [HttpPost]
        public ActionResult Crud(CatUsersModel model, string Perfil)
        {
            //var message = Session["message"]?.ToString();
            //var User = new LoginBusines().Login(message);
            //UsuarioModel usuario = User.Result;

            if (Perfil == "")
            {
                ModelState.AddModelError("Perfiles", "Seleccione un perfil");
                UserConfiguration UsersC = new UserConfiguration();
                var perfiles = UsersC.GetPerfilAll().OrderBy(x => x.Nombre).ToList();
                ViewBag.Perfil = new SelectList(perfiles, "IdPerfil", "Nombre", 1);
                return View();
            }

            model.UsuarioALta = model.IdUsusario;
            model.FechaAlta = DateTime.Now;
            model.idPerfil = int.Parse(Perfil);
            UserConfiguration Users = new UserConfiguration();
            if (model.IdUsusario > 0)
            {
                Users.UpdateUser(model);
            }
            else
            {
                var idUs = Users.InsertUser(model);
                if (idUs > 0)
                {
                    UserConfiguration UsersC = new UserConfiguration();
                    var perfiles = UsersC.GetPerfilAll().OrderBy(x => x.Nombre).ToList();
                    ViewBag.Perfil = new SelectList(perfiles, "IdPerfil", "Nombre", 1);
                    ViewBag.UsuarioId = "Ya existe un usuario con ese Correo";
                    return View();
                }
            }

            return RedirectToAction("Index", "User");
        }
    }
}