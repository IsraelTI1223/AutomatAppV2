using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatApp.Entities.Models.CatWH
{
    public class WHModel
    {
        public int WH {get; set;}
        public string Descripcion { get; set; }
        public string Des_Corta { get; set; }
        public string Direccion_calleynum { get; set; } 
        public string Direccion_Colonia { get; set; }
        public string Direccion_CP { get; set; }
        public string Direccion_Entidad { get; set; }
        public string Direccion_Estado { get; set; }
        public string RFC { get; set; }
        public string Razon_Social { get; set; }
        public string Telefono_1 { get; set; }
        public bool instalada { get; set; }
        public bool inventario { get; set; }
        public DateTime fecha_instalacion { get; set; }
    }
}
