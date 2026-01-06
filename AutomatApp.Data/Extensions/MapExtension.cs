using AutomatApp.Entities.Models;
using AutomatApp.Entities.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatApp.Data.Extensions
{
    public static class MapExtension
    {
        public static Response ToResponse(this IDataReader reader)
        {
            return new Response
            {
                Message = reader.Get<string>("Message"),
                Success = reader.Get<bool>("Success")
            };
        }      

        public static Modulo ToModulo(this IDataReader reader)
        {
            return new Modulo
            {
                IdModulo = reader.Get<int>("IdModulo"),
                NombreModulo = reader.Get<string>("Modulo"),
                Componente = reader.Get<string>("Componente"),
                IconoBase = reader.Get<string>("IconoBase"),
                Opciones = reader.Get<int>("Opciones"),
                IdPadre = reader.Get<int>("IdPadre"),
            };
        }

        public static Perfil ToPerfil(this IDataReader reader)
        {
            return new Perfil
            {
                IdPerfil = reader.Get<int>("IdPerfil"),
                Nombre = reader.Get<string>("Nombre"),
                Activo = reader.Get<bool>("Activo")
            };
        }


        public static CtrlPerfil ToCtrlPerfil(this IDataReader reader)
        {
            return new CtrlPerfil
            {
                IdCtrlPerfil = reader.Get<int>("IdCtrlPerfil"),
                IdPerfil = reader.Get<int>("IdPerfil"),
                IdModulo = reader.Get<int>("IdModulo"),
                Nombre = reader.Get<string>("Nombre"),
                Registra = reader.Get<bool>("Registra"),
                Actualiza = reader.Get<bool>("Actualiza"),
                Elimina = reader.Get<bool>("Elimina"),
                Consulta = reader.Get<bool>("Consulta"),
                Activo = reader.Get<bool>("Activo")
            };
        }

        public static ModuloAccion ToModuloAccion(this IDataReader reader)
        {
            return new ModuloAccion
            {
                IdModulo = reader.Get<int>("IdModulo"),
                NombreModulo = reader.Get<string>("Modulo"),
                //Componente = reader.Get<string>("Componente"),
                //IconoBase = reader.Get<string>("IconoBase"),
                Actualizar = reader.Get<int>("Actualizar") == 1 ? true : false,
                Consultar = reader.Get<int>("Consultar") == 1 ? true : false,
                Cargar = reader.Get<int>("Cargar") == 1 ? true : false,
                Eliminar = reader.Get<int>("Eliminar") == 1 ? true : false,
                Insertar = reader.Get<int>("Insertar") == 1 ? true : false,
                Descargar = reader.Get<int>("Descargar") == 1 ? true : false,
                Opciones = reader.Get<int>("Opciones")
            };
        }

        public static Accion ToAccion(this IDataReader reader)
        {
            return new Accion
            {
                IdAccion = reader.Get<int>("IdAccion"),
                Nombre = reader.Get<string>("Nombre")
            };
        }

        public static PerfilModuloAccion ToPerfilModuloAccion(this IDataReader reader)
        {
            return new PerfilModuloAccion
            {
                IdModulo = reader.Get<int>("IdModulo"),
                IdPadre = reader.Get<int>("IdPadre"),
                Modulo = reader.Get<string>("Modulo"),
                IdAccion = reader.Get<int>("IdAccion"),
                Nombre = reader.Get<string>("Nombre"),
                Opciones = reader.Get<int>("Opciones"),
                EstatusAccion = reader.Get<int>("EstatusAccion")
            };
        }

        public static PermisoUsuarioVistas ToPermisoUsuarioVistas(this IDataReader reader)
        {
            return new PermisoUsuarioVistas
            {
                Usuario = reader.Get<string>("usuario"),
                Marca = reader.Get<string>("marca"),
                Farmacia_Id = reader.Get<int>("farmacia_id"),
                Farmacia = reader.Get<string>("farmacia"),
                Controlador = reader.Get<string>("Controlador"),
                Vista = reader.Get<string>("Vistas")
            };
        }
    }
}
