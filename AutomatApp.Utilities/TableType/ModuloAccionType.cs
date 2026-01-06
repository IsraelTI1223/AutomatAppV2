using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatApp.Utilities.TableType
{
    public class ModuloAccionType
    {
        public static DataTable GetDefinition()
        {
            var dt = new DataTable();

            dt.Columns.Add("IdModulo", typeof(int));
            dt.Columns.Add("IdAccion", typeof(int));

            return dt;
        }
    }
}
