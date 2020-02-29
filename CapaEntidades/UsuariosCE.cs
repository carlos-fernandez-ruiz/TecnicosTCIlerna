using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class UsuariosCE
    {
        public int idUsuario { get; set; }
        public string usuario { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public bool activo { get; set; }
    }
}
