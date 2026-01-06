using AutomatApp.Data.Extensions;
using AutomatApp.Entities.Models.CatProducto;
using AutomatApp.Entities.Models.CatWH;
using AutomatApp.Entities.Models.IngresioMercancia;
using AutomatApp.Entities.Response;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatApp.Data.IngresoMercancia
{
    public  class IngresoMercanciaData
    {
        public List<MovimientoInventarioModel> GetAllTipoMovD()
        {

            var response = new List<MovimientoInventarioModel>();
            var db = DatabaseFactory.CreateDatabase("DBPORTAL");

            var command = db.GetStoredProcCommand("sp_Get_TipoMovimiento");
            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    response.Add(new MovimientoInventarioModel
                    {


                        id_Tipo_mov = dr.Get<int>("id_Tipo_mov"),
                        Descripcion = dr.Get<string>("Descripcion")
                    });
                }
            }
            command.Connection.Close();

            return response;
        }
        public static ResponseList<object> CrearIngresoD(DataTable dt)
        {
            var response = new ResponseList<object>();          
            
            return response;
        }
    }
}
