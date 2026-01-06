using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatApp.Entities.Models.CatProducto;
using AutomatApp.Data.Extensions;
using AutomatApp.Entities.Response;


namespace AutomatApp.Data.CatProducto
{
    public class CatProductoData
    {
        public List<ProductoModel> GetAll()
        {

            var response = new List<ProductoModel>();
            var db = DatabaseFactory.CreateDatabase("DBPORTAL");

            var command = db.GetStoredProcCommand("sp_GET_producto_maestro");
            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    response.Add(new ProductoModel
                    {                        
                        codigo_unico=dr.Get<string>("codigo_unico"),
                        ean = dr.Get<string>("ean"),
                        Descripcion = dr.Get<string>("Descripcion"),
                        activo = dr.Get<bool>("Activo"),
                        fecha_alta = dr.Get<DateTime>("fecha_alta"),
                    });
                }
            }
            command.Connection.Close();

            return response;
        }

        public List<DepartamentoModel> GetAllDept()
        {
            var response = new List<DepartamentoModel>();
            var db = DatabaseFactory.CreateDatabase("DBPORTAL");

            var command = db.GetStoredProcCommand("dbo.sp_GET_Departamento");
            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    response.Add(new DepartamentoModel
                    {
                        Id = dr.Get<int>("IdDepartamento"),
                        Nombre = dr.Get<string>("Nombre")                       
                    });
                }
            }
            command.Connection.Close();

            return response;
        }

        public List<CategoriaModel> GetAllCat()
        {
            var response = new List<CategoriaModel>();
            var db = DatabaseFactory.CreateDatabase("DBPORTAL");

            var command = db.GetStoredProcCommand("dbo.sp_GET_Categoria");
            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    response.Add(new CategoriaModel
                    {
                        Id = dr.Get<int>("IdCategoria"),
                        Nombre = dr.Get<string>("Nombre"), 
                        Departamento = dr.Get<string>("Departamento"),
                        DepartamentoId = dr.Get<int>("DepartamentoId"),
                        Activo = dr.Get<bool>("activo"),
                    });
                }
            }
            command.Connection.Close();

            return response;
        }

        public List<ProvModel> GetAllProveedor()
        {
            var response = new List<ProvModel>();
            var db = DatabaseFactory.CreateDatabase("DBPORTAL");

            var command = db.GetStoredProcCommand("dbo.sp_GET_Proveedor");
            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    response.Add(new ProvModel
                    {
                        Id = dr.Get<int>("Id_Proveedor"),
                        Nombre = dr.Get<string>("Nombre")
                    });
                }
            }
            command.Connection.Close();

            return response;
        }

        public List<MarcaModel> GetAllMarca()
        {
            var response = new List<MarcaModel>();
            var db = DatabaseFactory.CreateDatabase("DBPORTAL");

            var command = db.GetStoredProcCommand("dbo.sp_GET_Marca");
            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    response.Add(new MarcaModel
                    {
                        Id = dr.Get<int>("IdMarca"),
                        Nombre = dr.Get<string>("Descripcion")                       
                    });
                }
            }
            command.Connection.Close();

            return response;
        }

        public Response CrearDepartamentoD(DepartamentoModel model)
        {
            var response = new Response();
            try
            {
                var db = DatabaseFactory.CreateDatabase("DBPORTAL");

                var command = db.GetStoredProcCommand("dbo.sp_Load_Departamento");

                db.AddInParameter(command, "@Nombre", DbType.String, model.Nombre);
                db.AddInParameter(command, "@Descripcion", DbType.String, model.Descripcion);
                db.AddInParameter(command, "@activo", DbType.Boolean, model.Activo);

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

        public Response CrearCategoriaD(CategoriaModel model)
        {
            var response = new Response();
            try
            {
                var db = DatabaseFactory.CreateDatabase("DBPORTAL");

                var command = db.GetStoredProcCommand("dbo.sp_Load_Categoria");

                db.AddInParameter(command, "@Nombre", DbType.String, model.Nombre);
                db.AddInParameter(command, "@Descripcion", DbType.String, model.Descripcion);
                db.AddInParameter(command, "@deptId", DbType.Int32, model.DepartamentoId);
                db.AddInParameter(command, "@activo", DbType.Boolean, model.Activo);

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

        public Response CrearProductoD(ProductoModel model)
        {
            var response = new Response();
            try
            {
                var db = DatabaseFactory.CreateDatabase("DBPORTAL");

                var command = db.GetStoredProcCommand("dbo.sp_Load_Productos");

                db.AddInParameter(command, "@ean", DbType.String, model.ean);
                db.AddInParameter(command, "@Descripcion", DbType.String, model.Descripcion);
                db.AddInParameter(command, "@claveunidad", DbType.String, model.claveunidad);
                db.AddInParameter(command, "@codigosat", DbType.String, model.codigosat);
                db.AddInParameter(command, "@inventariable", DbType.Boolean, model.inventariable);
                db.AddInParameter(command, "@facturable", DbType.Boolean, model.facturable);
                db.AddInParameter(command, "@disponible_compra", DbType.Boolean, model.disponible_compra);
                db.AddInParameter(command, "@Permite_Devolucion", DbType.Boolean, model.Permite_Devolucion);
                db.AddInParameter(command, "@activo", DbType.Boolean, model.activo);
                db.AddInParameter(command, "@Departamento", DbType.Int32, model.Departamento);
                db.AddInParameter(command, "@Categoria", DbType.Int32, model.Categoria);
                db.AddInParameter(command, "@iva", DbType.Boolean, model.iva);
                db.AddInParameter(command, "@ieps", DbType.Boolean, model.ieps);
                db.AddInParameter(command, "@Marca", DbType.Int32, model.Id_Marca);
                db.AddInParameter(command, "@Proveedor", DbType.Int32, model.Id_Proveedor);

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

        public Response CrearMarcaD(MarcaModel model)
        {
            var response = new Response();
            try
            {
                var db = DatabaseFactory.CreateDatabase("DBPORTAL");

                var command = db.GetStoredProcCommand("dbo.sp_Load_Marca");

                db.AddInParameter(command, "@Marca", DbType.String, model.Nombre);                
                db.AddInParameter(command, "@activo", DbType.Boolean, model.activo);

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
