using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatApp.Entities.Models.IngresioMercancia
{
    public class ProductoIngreso
    {
        public string ean { get; set; }
        public int Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Costo { get; set; }
    }
}
