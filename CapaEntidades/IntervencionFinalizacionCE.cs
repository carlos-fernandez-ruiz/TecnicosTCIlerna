using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class IntervencionFinalizacionCE
    {
        public int idIntervencion { get; set; }
        public bool medidasValidado { get; set; }
        public bool fotografiasValidado { get; set; }
        public bool firmaValidado { get; set; }
        public string codigoFinalizacion { get; set; }
        public int idEstado { get; set; }
        public string observaciones { get; set; }
        public string tecnico { get; set; }
        public string  telefonoTecnico { get; set; }
        public int idUsuarioValidacion { get; set; }
        public DateTime fechaUltimaModificacion { get; set; }
    }
}
