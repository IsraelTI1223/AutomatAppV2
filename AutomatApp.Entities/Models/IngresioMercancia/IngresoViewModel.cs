using AutomatApp.Entities.Models.CatProducto;
using AutomatApp.Entities.Models.CatWH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatApp.Entities.Models.IngresioMercancia
{
    public class IngresoViewModel
    {
        public List<WHModel> WHModels { get; set; }
        public List<ProductoModel> productoModels { get; set; }
        public List<ProvModel> provModels { get; set; }
        public List<MovimientoInventarioModel> movimientoInventarioModels { get; set; }
        public List<IngresoModel> ingresoModels { get; set; }
    }
}
