using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatApp.Entities.Models
{
    public class PermisoUsuarioVistas
    {
        public string Usuario { get; set; }
        public string Marca { get; set; }
        public int Farmacia_Id { get; set; }
        public string Farmacia { get; set; }
        public string Controlador { get; set; }
        public string Vista { get; set; }
    }
}
