using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class IntervencionCE
    {
        public int idIntervencion { get; set; }
        public string codigoIntervencion { get; set; }
        public int idTipoIntervencion { get; set; }
        public int idEstado { get; set; }
        public int idUsuario { get; set; }
        public string comentarioIntervencion { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaUltimaModificacion { get; set; }
        public string nombreTienda { get; set; }
        public int idTienda { get; set; }
        public string direccion { get; set; }
        public string poblacion { get; set; }
        public int idCliente { get; set; }
        public string cliente { get; set; }



    }
}
