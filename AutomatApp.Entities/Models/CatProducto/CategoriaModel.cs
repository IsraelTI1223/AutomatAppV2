using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatApp.Entities.Models.CatProducto
{
    public class CategoriaModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }        
        public bool Activo { get; set; }
        public int DepartamentoId { get; set; }        
        public string Departamento { get; set; }
    }
}
