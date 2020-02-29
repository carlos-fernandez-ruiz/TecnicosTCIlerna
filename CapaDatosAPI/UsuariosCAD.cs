using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;

namespace CapaDatosAPI
{
    public class UsuariosCAD : baseCAD
    {
        public DataTable getUsuarios()
        {
            try
            {
                DataTable dt = null;
                DbCommand cmd = base.CrearComandoSP();
                cmd.CommandText = "UsuariosRecuperar";               

                dt = base.EjecutarReader(cmd);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
