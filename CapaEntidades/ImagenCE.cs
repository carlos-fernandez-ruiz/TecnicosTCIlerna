using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class ImagenCE
    {
        public int idImagen { get; set; }
        public int idReplanteoImagen { get; set; }
        public int idInstalacionImagen { get; set; }
        public int idIntervencion { get; set; }
        public int idTipoImagen { get; set; }
        public string rutaImagen { get; set; }
        public string descripcion { get; set; }
        public string tecnico { get; set; }
        public string telefonoTecnico { get; set; }
        public int idUsuario { get; set; }
        public string comentario { get; set; }
        public ImagenCE()
        {
            this.comentario = "";
        }
    }
}
