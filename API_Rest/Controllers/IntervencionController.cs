using CapaEntidades;
using CapaNegocioAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace API_Rest.Controllers
{
    public class IntervencionController : ApiController
    {
        
        // GET: api/Intervencion
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost]
        [Route("intervencion/insertarImagen")]
        public async Task<HttpResponseMessage> insertarImagen()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {

                var httpRequest = HttpContext.Current.Request;

                long idImagen = 0;

                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {
                        int MaxContentLength = 1024 * 1024 * 3; //Size = 3 MB  

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {

                            var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else if (postedFile.ContentLength > MaxContentLength)
                        {

                            var message = string.Format("Please Upload a file upto 1 mb.");

                            dict.Add("error", message);
                            return  Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else
                        {
                            string tipoIntervencionImagen = httpRequest.Form["tipoIntervencionImagen"].ToString();
                            string directorioImagenes = System.Configuration.ConfigurationManager.AppSettings["directorioImagenes"];
                            string directorio = System.Configuration.ConfigurationManager.AppSettings[tipoIntervencionImagen];
                           
                            //var filePath = HttpContext.Current.Server.MapPath("~/" + directorio + "/" + postedFile.FileName);
                            var filePath = directorioImagenes + directorio + "/" + file;

                            //if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/" + directorio)))
                            //{
                            //   Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/" + directorio));
                            //}

                            if (!Directory.Exists(directorioImagenes + directorio))
                            {
                                Directory.CreateDirectory(directorioImagenes + directorio);
                            }

                            //Si existe el fichero le cambiamos el nombre
                            if (System.IO.File.Exists(filePath))
                            {
                                filePath = directorioImagenes + directorio + "/" + "1_" + file;
                            } 

                            postedFile.SaveAs(filePath);
                            idImagen = new ImagenesCRN_API().insertarImagen(filePath);

                            if (idImagen > 0)
                            {
                                ImagenCE oImagen = new ImagenCE();
                                int idIntervencion = int.TryParse(httpRequest.Form["idIntervencion"].ToString(), out idIntervencion) ? idIntervencion : 0;                               
                                int idUsuario = int.TryParse(httpRequest.Form["idUsuario"].ToString(), out idUsuario) ? idUsuario : 0;
                                int idTipoImagen = int.TryParse(httpRequest.Form["idTipoImagen"].ToString(), out idTipoImagen) ? idTipoImagen : 0;
                                //int idIntervencion = int.TryParse(httpRequest.Headers["idIntervencion"].ToString(), out idIntervencion) ? idIntervencion : 0;
                                //int idUsuario = int.TryParse(httpRequest.Headers["idUsuario"].ToString(), out idUsuario) ? idUsuario : 0;
                                //int idTipoImagen = int.TryParse(httpRequest.Headers["idTipoImagen"].ToString(), out idTipoImagen) ? idTipoImagen : 0;
                                oImagen.idIntervencion = idIntervencion;
                                oImagen.idTipoImagen = idTipoImagen;
                                oImagen.idImagen = (int)idImagen;
                                oImagen.idUsuario = idUsuario;
                                oImagen.tecnico = httpRequest.Form["tecnico"].ToString();
                                oImagen.telefonoTecnico = httpRequest.Form["telefonoTecnico"].ToString();
                                //oImagen.tecnico = httpRequest.Headers["tecnico"].ToString();
                                //oImagen.telefonoTecnico = httpRequest.Headers["telefonoTecnico"].ToString();
                                                                
                                if (httpRequest.Form.AllKeys.Contains("comentario"))
                                {
                                    oImagen.comentario = httpRequest.Form["comentario"].ToString();
                                }                                



                                switch (tipoIntervencionImagen)
                                {
                                    case "Replanteo":
                                        new ReplanteoCRN_API().insertarReplanteoImagen(oImagen);
                                        break;
                                    case "IntervencionFinalizacion":
                                        new IntervencionCRN_API().insertarIntervencionFirmaImagen(oImagen);
                                        break;
                                    case "NoTerminada":
                                        int idIntervencionNoTerminada = int.TryParse(httpRequest.Form["idIntervencionNoTerminada"].ToString(), out idIntervencionNoTerminada) ? idIntervencionNoTerminada : 0;
                                        new IntervencionCRN_API().insertarImagenIntervencionNoTerminada(oImagen, idIntervencion);
                                        break;
                                }
                            }
                        }
                    }

                    var message1 = string.Format("Imagen subida correctamente. IdImagen: " + idImagen.ToString());
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, message1);
                    //response.Headers.Add("idImagen", idImagen.ToString());

                    return response;

                }
                var res = string.Format("Please Upload a image.");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            catch (Exception ex)
            {
                var res = string.Format(ex.Message);
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
        }

        [HttpGet]
        [Route("intervencion/getImagenesFirmaByIntervencion")]
        public List<ImagenCE> getImagenesFirmaByIntervencion(int idIntervencion)
        {
            return (new IntervencionCRN_API()).getImagenesFirmaByIntervencion(idIntervencion);
        }

        [HttpGet]
        [Route("intervencion/getMotivosIntervencionNoTerminada")]
        public List<IntervencionNoTerminadaMotivosCE> getMotivosIntervencionNoTerminada()
        {
          return new IntervencionCRN_API().getMotivosIntervencionNoTerminada();
        }

        [HttpPost]
        [Route("intervencion/insertarIntervencionNoTerminada")]
        public IntervencionNoTerminadaCE insertarIntervencionNoTerminada([FromBody]IntervencionNoTerminadaCE oNoTerminada)
        {
            return new IntervencionCRN_API().insertarIntervencionNoTerminada(oNoTerminada);
        }

        [HttpPost]
        [Route("intervencion/insertarLocalizacion")]
        public DataTable insertarLocalizacion([FromBody]LocalizacionCE oLocalizacion)
        {
            return new IntervencionCRN_API().insertarLocalizacion(oLocalizacion);
        }

        [HttpGet]
        [Route("intervencion/actualizarEstado")]
        public DataTable actualizarIntervencionEstado(int idIntervencion, int idEstado)
        {
            return new IntervencionCRN_API().actualizarIntervencionEstado(idIntervencion, idEstado);
        }
    }
}
