using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatApp.Entities.Models.IngresioMercancia
{
    public class IngresoViewModelContoller
    {
        public FormularioIngreso Formulario { get; set; }
        public List<ProductoIngreso> Productos { get; set; }
    }
}
