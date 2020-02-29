using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CapaDatosAPI;


namespace CapaNegocioAPI
{
    public class UsuariosCRN
    {
        public DataTable getUsuarios()
        {
            return (new UsuariosCAD().getUsuarios());
        }
    }
}
