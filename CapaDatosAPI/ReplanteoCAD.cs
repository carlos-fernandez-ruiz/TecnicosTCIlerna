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
            catch 
            {
                throw;
            }
        }

        public DataTable getImagenesTipos()
        {
            try
            {
                DataTable dt = null;
                DbCommand cmd = base.CrearComandoSP();
                cmd.CommandText = "Replanteo_ImagenesTiposRecuperar";

                dt = base.EjecutarReader(cmd);
                return dt;
            }
            catch 
            {
                throw;
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
            catch 
            {
                throw;
            }
        }

        public DataTable getReplanteoFinalizacionByIntervencion(int idIntervencion)
        {
            try
            {
                DataTable dt = null;
                DbCommand cmd = base.CrearComandoSP();
                cmd.CommandText = "Replanteo_FinalizacionRecuperarByIntervencion";
                cmd.Parameters.Add(new SqlParameter("@idIntervencion", idIntervencion));
                dt = base.EjecutarReader(cmd);
                return dt;
            }
            catch 
            {
                throw;
            }
        }

        public DataTable getImagenesByIntervencion(int idIntervencion)
        {
            try
            {
                DataTable dt = null;
                DbCommand cmd = base.CrearComandoSP();
                cmd.CommandText = "Replanteo_ImagenesRecuperarByIntervencion";
                cmd.Parameters.Add(new SqlParameter("@idIntervencion", idIntervencion));
                dt = base.EjecutarReader(cmd);
                return dt;
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
                DbCommand cmd = base.CrearComandoSP();
                cmd.CommandText = "Replanteo_MedidasInsertar";
                cmd.Parameters.Add(new SqlParameter("@idIntervencion", oMedida.idIntervencion));
                cmd.Parameters.Add(new SqlParameter("@idTipoMedida", oMedida.idTipoMedida));
                cmd.Parameters.Add(new SqlParameter("@valor", oMedida.valor));
                cmd.Parameters.Add(new SqlParameter("@comentario", oMedida.comentario == null ? "" : oMedida.comentario));
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

        public void insertarReplanteoImagen(ImagenCE oImagen)
        {
            try
            {
                DbCommand cmd = base.CrearComandoSP();
                cmd.CommandText = "Replanteo_ImagenesInsertar";
                cmd.Parameters.Add(new SqlParameter("@idIntervencion", oImagen.idIntervencion));
                cmd.Parameters.Add(new SqlParameter("@idTipoImagen", oImagen.idTipoImagen));
                cmd.Parameters.Add(new SqlParameter("@idImagen", oImagen.idImagen));
                cmd.Parameters.Add(new SqlParameter("@comentario", oImagen.comentario == null ? "" : oImagen.comentario));
                cmd.Parameters.Add(new SqlParameter("@idUsuario", oImagen.idUsuario));
                cmd.Parameters.Add(new SqlParameter("@tecnico", oImagen.tecnico));
                cmd.Parameters.Add(new SqlParameter("@telefonoTecnico", oImagen.telefonoTecnico));
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

        public DataTable replanteoFinalizacionActualizarEstado (IntervencionFinalizacionCE oFinalizacion)
        {
            try
            {
                DataTable dt = null;
                DbCommand cmd = base.CrearComandoSP();
                cmd.CommandText = "Replanteo_FinalizacionActualizarEstado";
                cmd.Parameters.Add(new SqlParameter("@idIntervencion", oFinalizacion.idIntervencion));
                cmd.Parameters.Add(new SqlParameter("@idEstado", oFinalizacion.idEstado)); 
                cmd.Parameters.Add(new SqlParameter("@tecnico", oFinalizacion.idIntervencion));
                cmd.Parameters.Add(new SqlParameter("@telefonoTecnico", oFinalizacion.telefonoTecnico));
                dt = base.EjecutarReader(cmd);
                return dt;
            }
            catch
            {
                throw;
            }
        }
    }
}
