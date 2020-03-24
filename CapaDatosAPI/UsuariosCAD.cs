using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace CapaDatosAPI
{
    public class UsuariosCAD : baseCAD
    {     
        public DataTable getUsuarioIntervencionLogin(string pUsuario, string pCodigoIntervencion)
        {
            try
            {
                DataTable dt;
                DbCommand cmd = base.CrearComandoSP();
                cmd.CommandText = "UsuariosIntervencionLogin";

                cmd.Parameters.Add(new SqlParameter("@usuario", pUsuario));
                cmd.Parameters.Add(new SqlParameter("@codigoIntervencion", pCodigoIntervencion));

                return base.EjecutarReader(cmd);
            }
            catch
            {
                throw;
            }
        }
    }
}
