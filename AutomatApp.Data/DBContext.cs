using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatApp.Data
{
    public abstract class DBContext
    {
        protected Database Context { get { return DatabaseFactory.CreateDatabase("DBPORTAL"); } }
    }
}
