using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class MedidaCE
    {
        public int idReplanteoMedida { get; set; }
        public int idInstalacionMedida { get; set; }
        public int idTipoMedida { get; set; }
        public int idIntervencion { get; set; }    
        public string descripcion { get; set; }
        public float valor { get; set; }
        public string comentario { get; set; }
        public int idUsuario { get; set; }
        public string tecnico { get; set; }
        public string telefonoTecnico { get; set; }

    }
}
