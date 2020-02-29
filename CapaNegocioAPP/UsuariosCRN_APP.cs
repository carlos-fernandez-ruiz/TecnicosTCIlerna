using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocioAPP
{
    public class UsuariosCRN_APP : baseCRN_APP
    {
        public bool CheckUser(string username, string password)
        {
            string encriptedPassword = this.obtenerHash(password);
            bool result = false;
            return result;
        }

      
    }
}
