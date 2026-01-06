using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatApp.Entities.Models
{
    public class Modulo
    {
        public int IdModulo { get; set; }
        public string NombreModulo { get; set; }
        public string Componente { get; set; }
        public string IconoBase { get; set; }
        public int Opciones { get; set; }
        public int IdPadre { get; set; }
    }
}
