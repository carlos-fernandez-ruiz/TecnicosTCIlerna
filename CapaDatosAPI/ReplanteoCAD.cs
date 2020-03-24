using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;


namespace CapaDatosAPI
{
    public class ReplanteoCAD : baseCAD
    {
        public DataTable getMedidasTipos()
        {
            try
            {
                DataTable dt = null;
                DbCommand cmd = base.CrearComandoSP();
                cmd.CommandText = "Replanteo_MedidasTiposRecuperar";

                dt = base.EjecutarReader(cmd);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getMedidasByIntervencion(int idIntervencion)
        {
            try
            {
                DataTable dt = null;
                DbCommand cmd = base.CrearComandoSP();
                cmd.CommandText = "Replanteo_MedidasRecuperarByIntervencion";
                cmd.Parameters.Add(new SqlParameter("@idIntervencion", idIntervencion));
                dt = base.EjecutarReader(cmd);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void insertarReplanteoMedida(MedidaCE oMedida)
        {
            try
            {
                DbCommand cmd = base.CrearComandoSP();
                cmd.CommandText = "Replanteo_MedidasInsertar";
                cmd.Parameters.Add(new SqlParameter("@idIntervencion", oMedida.idIntervencion));
                cmd.Parameters.Add(new SqlParameter("@idTipoMedida", oMedida.idTipoMedida));
                cmd.Parameters.Add(new SqlParameter("@valor", oMedida.valor));
                cmd.Parameters.Add(new SqlParameter("@comentario", oMedida.comentario));
                cmd.Parameters.Add(new SqlParameter("@idUsuario", oMedida.idUsuario));
                cmd.Parameters.Add(new SqlParameter("@tecnico", oMedida.tecnico));
                cmd.Parameters.Add(new SqlParameter("@telefonoTecnico", oMedida.telefonoTecnico));                
                cmd.CommandTimeout = 120;
                base.EjecutarComando(cmd);

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
                DbCommand cmd = base.CrearComandoSP();
                cmd.CommandText = "Replanteo_MedidasEliminar";
                cmd.Parameters.Add(new SqlParameter("@idReplanteoMedida", idMedida));
               
                cmd.CommandTimeout = 120;
                base.EjecutarComando(cmd);
            }
            catch
            {
                throw;
            }
        }
    }
}
