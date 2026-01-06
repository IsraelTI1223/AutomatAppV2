using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatApp.Entities.Models
{
    public class ModuloModel : ModuloBaseModel
    {
        public int IdModulo { get; set; }
        public int IdModuloPadre { get; set; }
        public string Modulo { get; set; }
        public List<SubModuloModel> SubModulos { get; set; }
        public List<ModuloModel> SubModuloHijos { get; set; }
        public Dictionary<int, string> Acciones { get; set; }
    }
}
