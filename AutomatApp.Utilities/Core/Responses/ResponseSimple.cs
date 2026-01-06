using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatApp.Utilities.Core.Responses
{
    public class ResponseSimple<T> : ResponseBase
    {
        /// <summary>
        /// Datos resultado
        /// </summary>
        public T Result { get; set; }
    }
}
