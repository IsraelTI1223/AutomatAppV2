using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatApp.Entities.Models.IngresioMercancia
{
    public class IngresoModel
    {
        public int WH { get; set; }
        public DateTime Dia_Operacion { get; set; }
        public string ean { get; set; }
        public int Cantidad { get; set; }
        public int Id_Tipo_mov { get; set; }
        public int Id_Proveedor { get; set; }
        public string no_factura { get; set; }
        public string no_pedido { get; set; }
        public decimal Costo { get; set; }
        public bool Procesado { get; set; }
        public DateTime Fecha_Proceso { get; set; }
    }
}
