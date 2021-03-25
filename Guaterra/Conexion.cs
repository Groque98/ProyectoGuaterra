using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using System.Windows.Forms;

namespace Guaterra
{
    class Conexion
    {
        public OdbcConnection nuevaConexion()
        {
            OdbcConnection conectar = new OdbcConnection("Dsn=dsnGuaterra");
            conectar.Open();
            return conectar;
        }
    }
}
