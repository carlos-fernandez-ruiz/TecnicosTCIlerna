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

        public List<ImagenTiposCE> getImagenesTipos()
        {
            try
            {
                List<ImagenTiposCE> lstImagenesTipos = new List<ImagenTiposCE>();
                DataTable dtImagenesTipos = new ReplanteoCAD().getImagenesTipos();
                if (dtImagenesTipos.Rows.Count > 0)
                {
                    foreach (DataRow fila in dtImagenesTipos.Rows)
                    {
                        ImagenTiposCE oImagenTipoCE = new ImagenTiposCE();
                        oImagenTipoCE.idTipoImagen = int.Parse(fila["idTipoImagen"].ToString());
                        oImagenTipoCE.descripcionTipoImagen = fila["descripcion"].ToString();
                        oImagenTipoCE.orden = int.Parse(fila["orden"].ToString());
                        lstImagenesTipos.Add(oImagenTipoCE);
                    }
                }
                return lstImagenesTipos;
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

        public IntervencionFinalizacionCE getReplanteoFinalizacionByIntervencion(int idIntervencion)
        {
            IntervencionFinalizacionCE oFinalizacion = new IntervencionFinalizacionCE();
            try
            {
                DataTable dt = new ReplanteoCAD().getReplanteoFinalizacionByIntervencion(idIntervencion);
                if (dt.Rows.Count > 0)
                {
                    DataRow fila = dt.Rows[0];
                    oFinalizacion.idIntervencion = int.Parse(fila["idIntervencion"].ToString());
                    oFinalizacion.medidasValidado = bool.Parse(fila["medidasValidado"].ToString());
                    oFinalizacion.fotografiasValidado = bool.Parse(fila["fotografiasValidado"].ToString());
                    oFinalizacion.firmaValidado = bool.Parse(fila["firmaValidado"].ToString());
                    oFinalizacion.codigoFinalizacion = fila["codigoFinalizacion"].ToString();
                    oFinalizacion.idEstado = int.Parse(fila["idEstado"].ToString());
                    oFinalizacion.tecnico = fila["tecnico"].ToString();
                    oFinalizacion.telefonoTecnico = fila["telefonoTecnico"].ToString();
                    oFinalizacion.conclusionIntervencion = fila["conclusionIntervencion"].ToString();


                }
                return oFinalizacion;
            } 
            catch
            {
                throw;
            }
        }

        public IntervencionFinalizacionCE replanteoFinalizacionActualizarEstado(IntervencionFinalizacionCE oFinalizacion)
        {
            IntervencionFinalizacionCE oFinalizacionCE = new IntervencionFinalizacionCE();
            try
            {
                DataTable dt = new ReplanteoCAD().replanteoFinalizacionActualizarEstado(oFinalizacion);
                if (dt.Rows.Count > 0)
                {
                    DataRow fila = dt.Rows[0];
                    oFinalizacionCE.idIntervencion = int.Parse(fila["idIntervencion"].ToString());
                    oFinalizacionCE.medidasValidado = bool.Parse(fila["medidasValidado"].ToString());
                    oFinalizacionCE.fotografiasValidado = bool.Parse(fila["fotografiasValidado"].ToString());
                    oFinalizacionCE.firmaValidado = bool.Parse(fila["firmaValidado"].ToString());
                    oFinalizacionCE.codigoFinalizacion = fila["codigoFinalizacion"].ToString();
                    oFinalizacion.tecnico = fila["tecnico"].ToString();
                    oFinalizacion.telefonoTecnico = fila["telefonoTecnico"].ToString();
                    oFinalizacionCE.idEstado = int.Parse(fila["idEstado"].ToString());
                    oFinalizacionCE.conclusionIntervencion =fila["conclusionIntervencion"].ToString();
                }
                return oFinalizacionCE;
            }
            catch
            {
                throw;
            }
            
        }
        public List<ImagenCE> getImagenesByIntervencion(int idIntervencion)
        {
            try
            {
                List<ImagenCE> lstImagenes = new List<ImagenCE>();
                DataTable dtImagenesIntervencion = new ReplanteoCAD().getImagenesByIntervencion(idIntervencion);
                if (dtImagenesIntervencion.Rows.Count > 0)
                {
                    foreach (DataRow fila in dtImagenesIntervencion.Rows)
                    {
                        ImagenCE oImagenCE = new ImagenCE();
                        oImagenCE.idReplanteoImagen = int.Parse(fila["idReplanteoImagen"].ToString());
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


        public void insertarReplanteoImagen(ImagenCE oImagen)
        {
            try
            {
                new ReplanteoCAD().insertarReplanteoImagen(oImagen);
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
