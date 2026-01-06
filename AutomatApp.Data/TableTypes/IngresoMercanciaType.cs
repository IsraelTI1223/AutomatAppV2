using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatApp.Data.TableTypes
{
    public class IngresoMercanciaType
    {
        public static DataTable Definicion()
        {
            var dt = new DataTable();

            dt.Columns.Add("WH", typeof(int));
            dt.Columns.Add("Dia_Operacion", typeof(DateTime));
            dt.Columns.Add("ean", typeof(string));
            dt.Columns.Add("Cantidad", typeof(int));
            dt.Columns.Add("Id_Tipo_mov", typeof(int));
            dt.Columns.Add("Id_Proveedor", typeof(int));
            dt.Columns.Add("no_factura", typeof(string));
            dt.Columns.Add("no_pedido", typeof(string));
            dt.Columns.Add("Costo", typeof(decimal));
            dt.Columns.Add("Procesado", typeof(bool));
            dt.Columns.Add("Fecha_Proceso", typeof(DateTime));

            return dt;
        }
    }
}
