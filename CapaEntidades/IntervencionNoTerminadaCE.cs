using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class IntervencionNoTerminadaCE
    {
        public int idIntervencionNoTerminada { get; set; }
        public int idIntervencion { get; set; }
        public int idMotivo { get; set; }
        public int idUsuario { get; set; }
        public string observaciones { get; set; }
        public string tecnico { get; set; }
        public string telefonoTecnico { get; set; }

        public IntervencionNoTerminadaCE()
        {
            this.observaciones = "";
        }

    }
}
