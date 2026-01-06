using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatApp.Entities.Models.CatProducto
{
    public class CargaPreciosViewModel
    {
        [Required(ErrorMessage = "Seleccione un archivo Excel o CSV.")]
        [Display(Name = "Archivo de precios")]
        public IFormFile Archivo { get; set; }
             
    }  
  
}
