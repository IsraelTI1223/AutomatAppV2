using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatApp.Entities.Models
{
    public class SubModuloModel : ModuloBaseModel
    {
        public int IdSubModulo { get; set; }
        public string SubModulo { get; set; }
        public Dictionary<int, string> Acciones { get; set; }
    }
}
