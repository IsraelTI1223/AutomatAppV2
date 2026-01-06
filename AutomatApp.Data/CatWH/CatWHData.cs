using AutomatApp.Data.Extensions;
using AutomatApp.Entities.Models.CatProducto;
using AutomatApp.Entities.Models.CatWH;
using AutomatApp.Entities.Response;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatApp.Data.CatWH
{
    public class CatWHData
    {
        public List<WHModel> GetAll()
        {

            var response = new List<WHModel>();
            var db = DatabaseFactory.CreateDatabase("DBPORTAL");

            var command = db.GetStoredProcCommand("dbo.sp_GET_Warehouse");
            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    response.Add(new WHModel
                    {


                        WH = dr.Get<int>("WH"),
                        Descripcion = dr.Get<string>("Descripcion"),
                        RFC = dr.Get<string>("RFC"),
                        Razon_Social = dr.Get<string>("Razon_Social"),
                        Telefono_1 = dr.Get<string>("Telefono_1"),
                        instalada = dr.Get<bool>("instalada"),
                    });
                }
            }
            command.Connection.Close();

            return response;
        }

        public Response CrearWHD(WHModel model)
        {
            var response = new Response();
            try
            {
                var db = DatabaseFactory.CreateDatabase("DBPORTAL");

                var command = db.GetStoredProcCommand("dbo.sp_Load_Warehouse");


                db.AddInParameter(command, "@WH", DbType.Int32, model.WH);
                db.AddInParameter(command, "@Descripcion", DbType.String, model.Descripcion);
                db.AddInParameter(command, "@Direccion_calleynum", DbType.String, model.Direccion_calleynum);
                db.AddInParameter(command, "@Direccion_Colonia", DbType.String, model.Direccion_Colonia);
                db.AddInParameter(command, "@Direccion_CP", DbType.String, model.Direccion_CP);
                db.AddInParameter(command, "@Direccion_Entidad", DbType.String, model.Direccion_Entidad);
                db.AddInParameter(command, "@Direccion_Estado", DbType.String, model.Direccion_Estado);
                db.AddInParameter(command, "@RFC", DbType.String, model.RFC);
                db.AddInParameter(command, "@Razon_Social", DbType.String, model.Razon_Social);
                db.AddInParameter(command, "@Telefono_1", DbType.String, model.Telefono_1);
                db.AddInParameter(command, "@instalada", DbType.Boolean, model.instalada);
                db.AddInParameter(command, "@inventario", DbType.Boolean, model.inventario);
                db.AddInParameter(command, "@fecha_instalacion", DbType.DateTime, model.fecha_instalacion);

                command.CommandTimeout = 0;

                var exito = db.ExecuteNonQuery(command);

                response.Message = "Cambios Guardados";
                response.Success = 1 != default;

            }
            catch (Exception ex)
            {


            }

            return response;
        }
    }
}
