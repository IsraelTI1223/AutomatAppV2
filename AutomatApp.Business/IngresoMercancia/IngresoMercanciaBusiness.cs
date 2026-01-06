using AutomatApp.Data.CatProducto;
using AutomatApp.Data.CatWH;
using AutomatApp.Data.IngresoMercancia;
using AutomatApp.Data.TableTypes;
using AutomatApp.Entities.Models.CatProducto;
using AutomatApp.Entities.Models.CatWH;
using AutomatApp.Entities.Models.IngresioMercancia;
using AutomatApp.Entities.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatApp.Business.IngresoMercancia
{
    public class IngresoMercanciaBusiness
    {
        private readonly IngresoMercanciaData _IngresoData = new IngresoMercanciaData();

        public List<MovimientoInventarioModel> GetAllTipoMovB()
        {
            var response = _IngresoData.GetAllTipoMovD();

            return response;
        }
        public static ResponseList<object> CrearIngresoB(List<IngresoModel> model)
        {
            var response = new ResponseList<object>();

            var dt = IngresoMercanciaType.Definicion();            
            foreach (var item in model)
            {
                DataRow renglon = dt.NewRow();
                renglon[0] = item.WH;
                renglon[1] = item.Dia_Operacion;
                renglon[2] = item.ean;
                renglon[3] = item.Cantidad;
                renglon[4] = item.Id_Tipo_mov;
                renglon[5] = item.Id_Proveedor;
                renglon[6] = item.no_factura;
                renglon[7] = item.no_pedido;
                renglon[8] = item.Costo;
                renglon[9] = item.Procesado;
                //renglon[10] = usuario;
                dt.Rows.Add(renglon);
            }

            response = IngresoMercanciaData.CrearIngresoD(dt);
            response.Message = response.Success ? "La operación se realizo con exito." : "No se pudo completar la operación";

            return response;
        }
    }
}
