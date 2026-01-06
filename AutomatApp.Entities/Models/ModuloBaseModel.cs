using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatApp.Entities.Models
{
    public abstract class ModuloBaseModel
    {
        public int Orden { get; set; }
        public string IconoBase { get; set; }
        public string Componente { get; set; }
    }
}
