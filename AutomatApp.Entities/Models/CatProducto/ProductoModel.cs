using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AutomatApp.Entities.Models.CatProducto
{
    public class ProductoModel
    {
        public string codigo_unico { get; set; }
        public string ean { get; set; }
        public string Descripcion { get; set; }
        public int Departamento { get; set; }
        public int Categoria { get; set; }                
        public int Subcategoria { get; set; }
        public bool activo { get; set; }
        public DateTime fecha_alta { get; set; }
        public bool facturable { get; set; }
        public bool inventariable { get; set; }        
        public bool disponible_compra { get; set; }
        public string codigosat { get; set; }
        public string claveunidad { get; set; }
        public bool Permite_Devolucion { get; set; }
        public int Id_Fabricante { get; set; }
        public int Id_Proveedor { get; set; }
        public int Id_Marca { get; set; }
        public bool iva { get; set; }
        public bool ieps { get; set; }
    }
}
