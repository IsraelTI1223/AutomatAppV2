using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatApp.Entities.Response
{
    public class ResponseList<T>: Response
    {
        public List<T> Result { get; set; }
    }
}
