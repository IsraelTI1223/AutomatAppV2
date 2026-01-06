using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using AutomatApp.Business.CatProducto;
using AutomatApp.Business.Login;
using AutomatApp.Business.Users;
using AutomatApp.Entities.Models;
using AutomatApp.Entities.Models.CatProducto;
using OfficeOpenXml;

namespace AutomatAppV2.Controllers
{
    public class CatProductoController : Controller
    {

        CatProductoBusiness ProdBusiness = new CatProductoBusiness();
        // GET: CatProducto
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


            var producto = ProdBusiness.GetAll().OrderBy(x => x.codigo_unico).ToList();
            ViewBag.Productos = producto;

            var Departamento = ProdBusiness.GetAllDept().OrderBy(x => x.Id).ToList();
            ViewBag.Departamento = Departamento;

            var Categoria = ProdBusiness.GetAllCat().OrderBy(x => x.Id).ToList();
            ViewBag.Categoria = Categoria;

            var Proveedor = ProdBusiness.GetAllProveedor().OrderBy(x => x.Id).ToList();
            ViewBag.Proveedor = Proveedor;

            var Marca = ProdBusiness.GetAllMarca().OrderBy(x => x.Id).ToList();
            ViewBag.Marca = Marca;
            

            var viewModel = new ProductoViewModel
            {
                ProductoModel = producto.ToList(),
                DepartamentoModel = Departamento.ToList(),
                CategoriaModel = Categoria.ToList(),
                MarcaModel = Marca.ToList(),
                ProvModel = Proveedor.ToList()
            };

            return View(viewModel);

        }      

        // Ajax: obtener categorías por departamento
        public JsonResult GetCategoriasPorDepartamento(int departamentoId)
        {
            var Categoria = ProdBusiness.GetAllCat()
                .Where(c => c.DepartamentoId == departamentoId)
                .Select(c => new { c.Id, c.Nombre })
                .ToList();

            return Json(Categoria, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CrearDepartamento(string nombre, string descripcion, bool activo = false)
        {
            if (!string.IsNullOrWhiteSpace(nombre))
            {
                var dep = new DepartamentoModel
                {
                    Nombre = nombre,
                    Descripcion = descripcion,
                    Activo = activo
                };
                ProdBusiness.CrearDepartamentoB(dep);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CrearCategoria(string nombre, string descripcion, int departamentoId, bool activo = false)
        {
            if (!string.IsNullOrWhiteSpace(nombre))
            {
                var cat = new CategoriaModel
                {
                    Nombre = nombre,
                    Descripcion = descripcion,
                    DepartamentoId = departamentoId,
                    Activo = activo
                };
                ProdBusiness.CrearCategoriaB(cat);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CrearProducto(ProductoModel model)
        {
           if(model.ean!=null)
            {
                ProdBusiness.CrearProductoB(model);
            }
                       
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CrearMarca(string Nombre, bool activo = false)
        {
            if (!string.IsNullOrWhiteSpace(Nombre))
            {
                var dep = new MarcaModel
                {
                    Nombre = Nombre,                    
                    activo = activo
                };
                ProdBusiness.CrearMarcaB(dep);
            }

            return RedirectToAction("Index");
        }

        public ActionResult DescargarExcel()
        {
            // Establecer licencia
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            // Obtener datos de la base de datos
            var datos = ProdBusiness.GetAll().OrderBy(x => x.codigo_unico).ToList(); // Ajusta según tu tabla

            // Crear archivo Excel
            using (var package = new ExcelPackage())
            {
                var ws = package.Workbook.Worksheets.Add("Productos");

                // Encabezados
                ws.Cells[1, 1].Value = "codigo_unico";
                ws.Cells[1, 2].Value = "ean";
                ws.Cells[1, 3].Value = "descripcion";
                ws.Cells[1, 4].Value = "activo";
                ws.Cells[1, 5].Value = "iva";
                ws.Cells[1, 6].Value = "ieps";
                ws.Cells[1, 7].Value = "disponible_compra";                

                int row = 2;
                foreach (var item in datos)
                {
                    ws.Cells[row, 1].Value = item.codigo_unico;
                    ws.Cells[row, 2].Value = item.ean;
                    ws.Cells[row, 3].Value = item.Descripcion;
                    ws.Cells[row, 4].Value = item.activo;
                    ws.Cells[row, 5].Value = item.iva;
                    ws.Cells[row, 6].Value = item.ieps;
                    ws.Cells[row, 7].Value = item.disponible_compra;
                    row++;
                }

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                string fileName = "Productos.xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return File(stream, contentType, fileName);
            }
        }

        [HttpPost]
        public ActionResult CargarManual(ProductoViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    model.Productos = _productoService.ObtenerProductos()
            //        .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Nombre });

            //    return View("Cargar", model);
            //}

            //_precioService.GuardarPrecio(model.ProductoId, model.Precio);

            //TempData["msg"] = "Precio actualizado correctamente.";
            return RedirectToAction("Cargar");
        }
    }
}
