using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatosAPI
{
    public class IntervencionCAD : baseCAD
    {
        public void insertarIntervencionFirmaImagen(ImagenCE oImagen)
        {
            try
            {
                DbCommand cmd = base.CrearComandoSP();
                cmd.CommandText = "IntervencionFirma_ImagenesInsertar";
                cmd.Parameters.Add(new SqlParameter("@idIntervencion", oImagen.idIntervencion));
                cmd.Parameters.Add(new SqlParameter("@idTipoImagen", oImagen.idTipoImagen));
                cmd.Parameters.Add(new SqlParameter("@idImagen", oImagen.idImagen));
                cmd.Parameters.Add(new SqlParameter("@comentario", oImagen.comentario));
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
        public void insertarImagenIntervencionNoTerminada(ImagenCE oImagen, int idIntervencionNoterminada)
        {
            try
            {
                DbCommand cmd = base.CrearComandoSP();
                cmd.CommandText = "IntervencionNoTerminada_ImagenesInsertar";
                cmd.Parameters.Add(new SqlParameter("@idIntervencion", oImagen.idIntervencion));
                cmd.Parameters.Add(new SqlParameter("@idIntervencionNoTerminada", idIntervencionNoterminada));
                cmd.Parameters.Add(new SqlParameter("@idImagen", oImagen.idImagen));
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

        public DataTable getImagenesFirmaByIntervencion(int idIntervencion)
        {
            try
            {
                DataTable dt = null;
                DbCommand cmd = base.CrearComandoSP();
                cmd.CommandText = "IntervencionFirma_ImagenesRecuperarByIntervencion";
                cmd.Parameters.Add(new SqlParameter("@idIntervencion", idIntervencion));
                dt = base.EjecutarReader(cmd);
                return dt;
            }
            catch 
            {
                throw;
            }
        }

        public DataTable getMotivosIntervencionNoTerminada()
        {
            try
            {
                DataTable dt = null;
                DbCommand cmd = base.CrearComandoSP();
                cmd.CommandText = "IntervencionNoTerminada_MotivosRecuperar";
                dt = base.EjecutarReader(cmd);
                return dt;
            }
            catch 
            {
                throw;
            }
        }

        public DataTable insertarLocalizacion(LocalizacionCE oLocalizacion)
        {
            try
            {
                DbCommand cmd = base.CrearComandoSP();
                cmd.CommandText = "Intervencion_LocalizacionesInsertar";
                cmd.Parameters.Add(new SqlParameter("@idIntervencion", oLocalizacion.idIntervencion));
                cmd.Parameters.Add(new SqlParameter("@fecha", oLocalizacion.fecha));
                cmd.Parameters.Add(new SqlParameter("@tecnico", oLocalizacion.tecnico));
                cmd.Parameters.Add(new SqlParameter("@telefonoTecnico", oLocalizacion.telefonoTecnico));
                cmd.Parameters.Add(new SqlParameter("@latitude", oLocalizacion.latitude));
                cmd.Parameters.Add(new SqlParameter("@longitude", oLocalizacion.longitude));
                cmd.Parameters.Add(new SqlParameter("@accuracy", oLocalizacion.accuracy));
                
                cmd.CommandTimeout = 120;
                return base.EjecutarReader(cmd);

            }
            catch
            {
                throw;
            }
        }

        public DataTable actualizarIntervencionEstado(int idIntervencion, int idEstado)
        {
            try
            {
                DataTable result = new DataTable();
                DbCommand cmd = base.CrearComandoSP();
                cmd.CommandText = "Intervencion_ActualizarEstado";
                cmd.Parameters.Add(new SqlParameter("@idIntervencion", idIntervencion));
                cmd.Parameters.Add(new SqlParameter("@idEstado", idEstado));
                cmd.CommandTimeout = 120;
                result = base.EjecutarReader(cmd);
                return result;

            }
            catch
            {
                throw;
            }
        }

        public IntervencionNoTerminadaCE insertarIntervencionNoTerminada(IntervencionNoTerminadaCE oNoTerminada)
        {
            try
            {
                DbCommand cmd = base.CrearComandoSP();
                cmd.CommandText = "IntervencionNoTerminada_Insertar";
                cmd.Parameters.Add(new SqlParameter("@idIntervencion", oNoTerminada.idIntervencion));
                cmd.Parameters.Add(new SqlParameter("@idMotivo", oNoTerminada.idMotivo));
                cmd.Parameters.Add(new SqlParameter("@observaciones", oNoTerminada.observaciones == null ? "" : oNoTerminada.observaciones));
                cmd.Parameters.Add(new SqlParameter("@idUsuario", oNoTerminada.idUsuario));
                cmd.Parameters.Add(new SqlParameter("@tecnico", oNoTerminada.tecnico));
                cmd.Parameters.Add(new SqlParameter("@telefonoTecnico", oNoTerminada.telefonoTecnico));
                cmd.CommandTimeout = 120;
                oNoTerminada.idIntervencionNoTerminada = (int)base.EjecutarScalar(cmd);

                return oNoTerminada;

            }
            catch
            {
                throw;
            }
        }

    }
}
