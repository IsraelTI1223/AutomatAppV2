using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatApp.Entities.Models.CatProducto
{
    public class ProductoViewModel
    {
        public List<ProductoModel> ProductoModel { get; set; }
        public List<DepartamentoModel> DepartamentoModel { get; set; }
        public List<CategoriaModel> CategoriaModel { get; set; }
        public List<MarcaModel> MarcaModel { get; set; }
        public List<ProvModel> ProvModel { get; set; }      
        
        public List<CargaPreciosViewModel> CargaPreciosModel { get; set; }
        public List<PreciosManualModel> PreciosManualModel { get; set; }
    }
}
