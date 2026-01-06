using AutomatApp.Data.Modulos;
using AutomatApp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatApp.Entities.Response;

namespace AutomatApp.Business.Modulos
{
    public class ModuloBusiness
    {
        private readonly ModuloData moduloData;
        public ModuloBusiness()
        {
            moduloData = new ModuloData();
        }

        public ResponseList<ModuloAccion> GetListByPadre(int IdPadre, int TipoOperacion, int IdPerfil)
        {
            var response = new ResponseList<ModuloAccion>();
            try
            {
                response = moduloData.GetListByPadre(IdPadre, TipoOperacion, IdPerfil);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información." + ex.Message;
            }
            return response;
        }
    }
}
