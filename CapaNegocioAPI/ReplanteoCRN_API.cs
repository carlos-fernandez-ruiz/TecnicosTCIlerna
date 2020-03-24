using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;
using CapaDatosAPI;

namespace CapaNegocioAPI
{
    public class ReplanteoCRN_API
    {
        public List<MedidaTiposCE> getMedidasTipos()
        {
            try
            {
                List<MedidaTiposCE> lstMedidasTipos = new List<MedidaTiposCE>();
                DataTable dtMedidasTipos = new ReplanteoCAD().getMedidasTipos();
                if (dtMedidasTipos.Rows.Count > 0)
                {
                    foreach(DataRow fila in dtMedidasTipos.Rows)
                    {
                        MedidaTiposCE oMedidaTipoCE = new MedidaTiposCE();                        
                        oMedidaTipoCE.idTipoMedida = int.Parse(fila["idTipoMedida"].ToString());
                        oMedidaTipoCE.descripcionTipoMedida = fila["descripcion"].ToString();
                        oMedidaTipoCE.orden = int.Parse(fila["orden"].ToString());
                        lstMedidasTipos.Add(oMedidaTipoCE);
                    }
                }
                return lstMedidasTipos;
            }
            catch
            {
                throw;
            }
        }

        public List<MedidaCE> getMedidasByIntervencion(int idIntervencion)
        {
            try
            {
                List<MedidaCE> lstMedidas = new List<MedidaCE>();
                DataTable dtMedidasIntervencion = new ReplanteoCAD().getMedidasByIntervencion(idIntervencion);
                if (dtMedidasIntervencion.Rows.Count > 0)
                {
                    foreach (DataRow fila in dtMedidasIntervencion.Rows)
                    {
                        MedidaCE oMedidaCE = new MedidaCE();
                        oMedidaCE.idReplanteoMedida = int.Parse(fila["idReplanteoMedida"].ToString());
                        oMedidaCE.idTipoMedida = int.Parse(fila["idTipoMedida"].ToString());
                        oMedidaCE.descripcion = fila["descripcion"].ToString();                        
                        oMedidaCE.valor = float.Parse(fila["valor"].ToString());
                        oMedidaCE.comentario = fila["comentario"].ToString();

                        lstMedidas.Add(oMedidaCE);
                    }
                }
                return lstMedidas;
            }
            catch
            {
                throw;
            }
        }

        public void insertarReplanteoMedida(MedidaCE oMedida)
        {
            try
            {
                new ReplanteoCAD().insertarReplanteoMedida(oMedida);
            }
            catch
            {
                throw;
            }
        }

        public void eliminarReplanteoMedida(int idMedida)
        {
            try
            {
                new ReplanteoCAD().eliminarReplanteoMedida(idMedida);
            }
            catch
            {
                throw;
            }
        }
    }
}
