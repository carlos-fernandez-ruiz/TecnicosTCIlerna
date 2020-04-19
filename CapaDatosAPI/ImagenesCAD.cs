using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatosAPI
{
    public class ImagenesCAD : baseCAD
    {
        public long insertarImagen(string rutaImagen)
        {
            long idImagen = 0;
            try
            {
                DbCommand cmd = base.CrearComandoSP();
                cmd.CommandText = "ImagenesInsertar";
                cmd.Parameters.Add(new SqlParameter("@rutaImagen", rutaImagen));
               
                cmd.CommandTimeout = 120;
                idImagen = base.EjecutarScalar(cmd);
                return idImagen;

            }
            catch
            {
                throw;
            }
        }
    }
}
