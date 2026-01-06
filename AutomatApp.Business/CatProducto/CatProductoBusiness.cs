using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatApp.Data.CatProducto;
using AutomatApp.Data.Login;
using AutomatApp.Entities.Models;
using AutomatApp.Entities.Models.CatProducto;
using AutomatApp.Entities.Response;
using AutomatApp.Utilities.Core.Interceptors;
using AutomatApp.Utilities.Core.Responses;

namespace AutomatApp.Business.CatProducto
{
    public class CatProductoBusiness
    {
        private readonly CatProductoData _CatProductoData = new CatProductoData();

        public List<ProductoModel> GetAll()
        {
            var response = _CatProductoData.GetAll();

            return response;
        }

        public List<DepartamentoModel> GetAllDept()
        {
            var response = _CatProductoData.GetAllDept();

            return response;
        }

        public List<CategoriaModel> GetAllCat()
        {
            var response = _CatProductoData.GetAllCat();

            return response;
        }

        public List<MarcaModel> GetAllMarca()
        {
            var response = _CatProductoData.GetAllMarca();

            return response;
        }

        public List<ProvModel> GetAllProveedor()
        {
            var response = _CatProductoData.GetAllProveedor();

            return response;
        }

        public Response CrearDepartamentoB(DepartamentoModel model)
        {
            var response = new Response();
            try
            {
                response = _CatProductoData.CrearDepartamentoD(model);
            }
            catch (Exception ex)
            {
                response.Message = "Error al guardar. " + ex.Message;
            }
            return response;
        }

        public Response CrearCategoriaB(CategoriaModel model)
        {
            var response = new Response();
            try
            {
                response = _CatProductoData.CrearCategoriaD(model);
            }
            catch (Exception ex)
            {
                response.Message = "Error al guardar. " + ex.Message;
            }
            return response;
        }

        public Response CrearProductoB(ProductoModel model)
        {
            var response = new Response();
            try
            {
                response = _CatProductoData.CrearProductoD(model);
            }
            catch (Exception ex)
            {
                response.Message = "Error al guardar. " + ex.Message;
            }
            return response;
        }

        public Response CrearMarcaB(MarcaModel model)
        {
            var response = new Response();
            try
            {
                response = _CatProductoData.CrearMarcaD(model);
            }
            catch (Exception ex)
            {
                response.Message = "Error al guardar. " + ex.Message;
            }
            return response;
        }
    }
}
