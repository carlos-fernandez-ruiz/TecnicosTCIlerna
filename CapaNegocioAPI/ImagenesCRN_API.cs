using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatosAPI;

namespace CapaNegocioAPI
{
    public class ImagenesCRN_API
    {
        public long insertarImagen(string rutaImagen)
        {
            long idImagen = 0;
            try
            {
                idImagen = new ImagenesCAD().insertarImagen(rutaImagen);
            }
            catch
            {
                throw;
            }
            return idImagen;
        }
    }
}
