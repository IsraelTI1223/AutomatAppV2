using AutomatApp.Business.Login;
using AutomatApp.Entities.Models.IngresioMercancia;
using AutomatApp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutomatApp.Entities.Models.CatWH;
using AutomatApp.Business.CatWH;
using AutomatApp.Business.CatProducto;
using AutomatApp.Business.IngresoMercancia;
using System.Data;
using AutomatApp.Entities.Response;


namespace AutomatAppV2.Controllers
{
    public class IngresoMercanciaController : Controller
    {
        // GET: IngresoMercancia
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
            var pp = usuario.Permisos.ToList().Where(x => x.Modulo == "INVENTARIOS");
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

            CatWHBusiness WHBusiness = new CatWHBusiness();

            var Almacenes = WHBusiness.GetAll().OrderBy(x => x.WH).ToList();
            ViewBag.Almacenes = Almacenes;

            CatProductoBusiness ProdBusiness = new CatProductoBusiness();

            var producto = ProdBusiness.GetAll().OrderBy(x => x.codigo_unico).ToList();
            ViewBag.Productos = producto;

            var Proveedor = ProdBusiness.GetAllProveedor().OrderBy(x => x.Id).ToList();
            ViewBag.Proveedor = Proveedor;

            IngresoMercanciaBusiness IngresoBusiness = new IngresoMercanciaBusiness();
            var tipoMov = IngresoBusiness.GetAllTipoMovB().OrderBy(x => x.id_Tipo_mov).ToList();
            ViewBag.tipoMov = tipoMov;
            
            var viewModel = new IngresoViewModel
            {
                productoModels = producto,
                WHModels = Almacenes,
                provModels = Proveedor,
                movimientoInventarioModels = tipoMov                
            };

            return View(viewModel);
        }

        [HttpPost]
        public JsonResult CrearIngreso(IngresoViewModelContoller data)
        {

            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Formulario inválido" });

            // Aquí se guardan los datos en la base de datos
            var response = new Response();            
            
            List<IngresoModel> list = new List<IngresoModel>();
            IngresoModel d = new IngresoModel();

            foreach (var detalle in data.Productos)
            {
                d.WH = data.Formulario.WH;
                d.Dia_Operacion = DateTime.Parse(data.Formulario.Dia_Operacion);
                d.Id_Tipo_mov = data.Formulario.TipoMovimiento;
                d.Id_Proveedor = data.Formulario.Id_Proveedor;
                d.no_factura = data.Formulario.Factura;
                d.ean = detalle.ean;
                d.Costo = detalle.Costo;
                d.Cantidad = detalle.Cantidad;
                list.Add(d);
            }

            response = IngresoMercanciaBusiness.CrearIngresoB(list);

            var json = Json(response, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
            //return Json(new { success = true, message = "Ingreso guardado correctamente" });
        }
    }
}