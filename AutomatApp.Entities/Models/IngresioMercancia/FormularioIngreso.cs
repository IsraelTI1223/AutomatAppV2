using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatApp.Entities.Models.IngresioMercancia
{
    public class FormularioIngreso
    {
        public int WH { get; set; }
        public int Id_Proveedor { get; set; }
        public int TipoMovimiento { get; set; }
        public string Dia_Operacion { get; set; }
        public string Factura { get; set; }
    }
}
