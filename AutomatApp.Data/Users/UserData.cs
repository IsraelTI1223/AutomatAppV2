using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatApp.Entities.Models;
using AutomatApp.Data.Extensions;

namespace AutomatApp.Data.Users
{
    public class UserData
    {
        public List<CatUsersModel> GetAll()
        {
            var response = new List<CatUsersModel>();
            var db = DatabaseFactory.CreateDatabase("DBPORTAL");

            var command = db.GetStoredProcCommand("SP_CAT_USUARIO_GETALL");
            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    response.Add(new CatUsersModel
                    {
                        Correo = dr.Get<string>("Correo"),
                        idPerfil = dr.Get<int>("IdPerfil"),
                        IdUsusario = dr.Get<int>("IdUsuario"),
                        Nombre = dr.Get<string>("Nombre"),
                        Perfil = dr.Get<string>("Perfil"),
                        ApPaterno = dr.Get<string>("APaterno"),
                        ApMaterno = dr.Get<string>("AMaterno"),
                        Activo = dr.Get<bool>("Activo"),
                        FechaAlta = dr.Get<DateTime>("FechaRegistro")
                    });

                }
            }
            command.Connection.Close();

            return response;
        }

        public int InsertUser(CatUsersModel model)
        {
            var id = 0;
            try
            {
                var db = DatabaseFactory.CreateDatabase("DBPORTAL");
                var command = db.GetStoredProcCommand("SP_CAT_USUARIO_INSERT");
                command.Parameters.Add(new SqlParameter("@Correo", model.Correo));
                command.Parameters.Add(new SqlParameter("@idPerfil", model.idPerfil));
                command.Parameters.Add(new SqlParameter("@nombre", model.Nombre));
                command.Parameters.Add(new SqlParameter("@aPaterno", model.ApPaterno));
                command.Parameters.Add(new SqlParameter("@aMaterno", model.ApMaterno));
                command.Parameters.Add(new SqlParameter("@activo", model.Activo));
                command.Parameters.Add(new SqlParameter("@idUsuarioRegistro", model.UsuarioALta));
                command.Parameters.Add(new SqlParameter("@fechaRegistro", model.FechaAlta));
                var dr = db.ExecuteReader(command);
                while (dr.Read())
                {
                    id = dr.Get<int>("IdUsuario");
                }
                command.Connection.Close();

                return id;
            }
            catch
            {
                return id;
            }

        }

        public List<Perfil> GetAllPerfiles()
        {
            var list = new List<Perfil>();
            var db = DatabaseFactory.CreateDatabase("DBPORTAL");

            var command = db.GetStoredProcCommand("SP_GRFP_CAT_PERFIL_GETALL");
            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    list.Add(new Perfil { IdPerfil = dr.Get<int>("IdPerfil"), Nombre = dr.Get<string>("Nombre") });
                }
            }
            command.Connection.Close();

            return list;
        }

        public bool UpdateUser(CatUsersModel model)
        {

            try
            {
                var db = DatabaseFactory.CreateDatabase("DBPORTAL");
                var command = db.GetStoredProcCommand("SP_CAT_USUARIO_UPDATE");
                command.Parameters.Add(new SqlParameter("@id", model.IdUsusario));
                command.Parameters.Add(new SqlParameter("@Correo", model.Correo));
                command.Parameters.Add(new SqlParameter("@idPerfil", model.idPerfil));
                command.Parameters.Add(new SqlParameter("@nombre", model.Nombre));
                command.Parameters.Add(new SqlParameter("@aPaterno", model.ApPaterno));
                command.Parameters.Add(new SqlParameter("@aMaterno", model.ApMaterno));
                command.Parameters.Add(new SqlParameter("@activo", model.Activo));
                command.Parameters.Add(new SqlParameter("@idUsuarioActualiza", model.UsuarioALta));
                var dr = db.ExecuteReader(command);
                command.Connection.Close();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
