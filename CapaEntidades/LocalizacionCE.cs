using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class LocalizacionCE
    {
        public int idLocalizacion { get; set; }
        public int idIntervencion { get; set; }
        public DateTime fecha { get; set; }
        public string tecnico { get; set; }
        public string telefonoTecnico { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public double accuracy { get; set; }
    }
}
