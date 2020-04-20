using CapaDatosAPI;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocioAPI
{
    public class IntervencionCRN_API
    {
        public void insertarIntervencionFirmaImagen(ImagenCE oImagen)
        {
            try
            {
                new IntervencionCAD().insertarIntervencionFirmaImagen(oImagen);
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
                new IntervencionCAD().insertarImagenIntervencionNoTerminada(oImagen, idIntervencionNoterminada);
            }
            catch
            {
                throw;
            }
        }
        public void actualizarIntervencionEstado(int idIntervencion, int idEstado)
        {
            try
            {
                new IntervencionCAD().actualizarIntervencionEstado(idIntervencion, idEstado);
            }
            catch
            {
                throw;
            }
        }


        public void insertarLocalizacion(LocalizacionCE oLocalizacion)
        {
            try
            {
                new IntervencionCAD().insertarLocalizacion(oLocalizacion);
            }
            catch
            {
                throw;
            }
        }
        public List<ImagenCE> getImagenesFirmaByIntervencion(int idIntervencion)
        {
            try
            {
                List<ImagenCE> lstImagenes = new List<ImagenCE>();
                DataTable dtImagenesIntervencion = new IntervencionCAD().getImagenesFirmaByIntervencion(idIntervencion);
                if (dtImagenesIntervencion.Rows.Count > 0)
                {
                    foreach (DataRow fila in dtImagenesIntervencion.Rows)
                    {
                        ImagenCE oImagenCE = new ImagenCE();
                        oImagenCE.idReplanteoImagen = int.Parse(fila["idFinalizacionImagen"].ToString());
                        oImagenCE.idTipoImagen = int.Parse(fila["idTipoImagen"].ToString());
                        oImagenCE.descripcion = fila["descripcion"].ToString();
                        oImagenCE.idImagen = int.Parse(fila["idImagen"].ToString());
                        oImagenCE.rutaImagen = fila["rutaImagen"].ToString();
                        oImagenCE.comentario = fila["comentario"].ToString();

                        lstImagenes.Add(oImagenCE);
                    }
                }
                return lstImagenes;
            }
            catch
            {
                throw;
            }
        }

        public List<IntervencionNoTerminadaMotivosCE> getMotivosIntervencionNoTerminada()
        {
            try
            {
                List<IntervencionNoTerminadaMotivosCE> lstMotivos = new List<IntervencionNoTerminadaMotivosCE>();
                DataTable dtMotivos = new IntervencionCAD().getMotivosIntervencionNoTerminada();
                if (dtMotivos.Rows.Count > 0)
                {
                    foreach (DataRow fila in dtMotivos.Rows)
                    {
                        IntervencionNoTerminadaMotivosCE oMotivo = new IntervencionNoTerminadaMotivosCE();
                        oMotivo.idMotivo = int.Parse(fila["idMotivo"].ToString());
                        oMotivo.orden = int.Parse(fila["orden"].ToString());
                        oMotivo.descripcion = fila["descripcion"].ToString();
                        lstMotivos.Add(oMotivo);
                    }
                }
                return lstMotivos;
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
                return new IntervencionCAD().insertarIntervencionNoTerminada(oNoTerminada);
            } 
            catch
            {
                throw;
            }
        }


    }
}
