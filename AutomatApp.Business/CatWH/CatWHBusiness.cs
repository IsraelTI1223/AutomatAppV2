using AutomatApp.Data.CatProducto;
using AutomatApp.Data.CatWH;
using AutomatApp.Entities.Models.CatProducto;
using AutomatApp.Entities.Models.CatWH;
using AutomatApp.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatApp.Business.CatWH
{
    public class CatWHBusiness
    {
        private readonly CatWHData _CatWHData = new CatWHData();
        public List<WHModel> GetAll()
        {
            var response = _CatWHData.GetAll();

            return response;
        }

        public Response CrearWHB(WHModel model)
        {
            var response = new Response();
            try
            {
                response = _CatWHData.CrearWHD(model);
            }
            catch (Exception ex)
            {
                response.Message = "Error al guardar. " + ex.Message;
            }
            return response;
        }
    }
}
