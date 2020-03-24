using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CapaDatosAPI;
using CapaEntidades;
using Utilidades;


namespace CapaNegocioAPI
{
    public class UsuariosCRN_API
    {      

        public DataTable getUsuarioIntervencionTabla(string pUsuario, string pCodigoIntervencion)
        {
            try
            {
                DataTable dtUsuarioIntervencion = new UsuariosCAD().getUsuarioIntervencionLogin(pUsuario, pCodigoIntervencion);
                return dtUsuarioIntervencion;

            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }
}
