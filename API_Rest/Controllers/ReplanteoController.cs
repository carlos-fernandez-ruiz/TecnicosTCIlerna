using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using CapaEntidades;
using CapaNegocioAPI;

namespace API_Rest.Controllers
{
    public class ReplanteoController : ApiController
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        [HttpGet]
        [Route("replanteo/getMedidasTipos")]
        public List<MedidaTiposCE> getMedidasTipos()
        {                       
            return (new ReplanteoCRN_API()).getMedidasTipos();
        }

        [HttpGet]
        [Route("replanteo/getImagenesTipos")]
        public List<ImagenTiposCE> getImagenesTipos()
        {
            return (new ReplanteoCRN_API()).getImagenesTipos();
        }

        [HttpGet]
        [Route("replanteo/getMedidasByIntervencion")]
        public List<MedidaCE> getMedidasByIntervencion(int idIntervencion)
        {
            return (new ReplanteoCRN_API()).getMedidasByIntervencion(idIntervencion);
        }

        [HttpGet]
        [Route("replanteo/getImagenesByIntervencion")]
        public List<ImagenCE> getImagenesByIntervencion(int idIntervencion)
        {
            return (new ReplanteoCRN_API()).getImagenesByIntervencion(idIntervencion);
        }

        [HttpGet]
        [Route("replanteo/getReplanteoFinalizacionByIntervencion")]
        public IntervencionFinalizacionCE getReplanteoFinalizacionByIntervencion(int idIntervencion)
        {
            return (new ReplanteoCRN_API()).getReplanteoFinalizacionByIntervencion(idIntervencion);
        }


        [HttpPost]
        [Route("replanteo/insertarReplanteoMedida")]
        public void insertarReplanteoMedida([FromBody]MedidaCE oMedida)
        {
            new ReplanteoCRN_API().insertarReplanteoMedida(oMedida);
        }

        //[HttpPost]
        //[Route("replanteo/insertarReplanteoImagen")]
        //public async Task<HttpResponseMessage> insertarReplanteoImagen()
        //{
        //    Dictionary<string, object> dict = new Dictionary<string, object>();
        //    try
        //    {

        //        var httpRequest = HttpContext.Current.Request;

        //        long idImagen = 0;

        //        foreach (string file in httpRequest.Files)
        //        {

        //            var postedFile = httpRequest.Files[file];                    
        //            if (postedFile != null && postedFile.ContentLength > 0)
        //            {

        //                int MaxContentLength = 2048 * 2048 * 1; //Size = 2 MB  

        //                IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
        //                var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
        //                var extension = ext.ToLower();
        //                if (!AllowedFileExtensions.Contains(extension))
        //                {

        //                    var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

        //                    dict.Add("error", message);
        //                    return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
        //                }
        //                else if (postedFile.ContentLength > MaxContentLength)
        //                {

        //                    var message = string.Format("Please Upload a file upto 1 mb.");

        //                    dict.Add("error", message);
        //                    return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
        //                }
        //                else
        //                {
        //                    var filePath = HttpContext.Current.Server.MapPath("~/ReplanteoImagenes/" + postedFile.FileName);

        //                    postedFile.SaveAs(filePath);
        //                    idImagen = new ImagenesCRN_API().insertarImagen(filePath);
        //                    if (idImagen > 0)
        //                    {
        //                        ImagenCE oImagen = new ImagenCE();
        //                        int idIntervencion = int.TryParse(httpRequest.Headers["idIntervencion"].ToString(), out idIntervencion) ? idIntervencion : 0;
        //                        int idUsuario = int.TryParse(httpRequest.Headers["idUsuario"].ToString(), out idUsuario) ? idUsuario : 0;
        //                        int idTipoImagen = int.TryParse(httpRequest.Headers["idTipoImagen"].ToString(), out idTipoImagen) ? idTipoImagen : 0;
        //                        oImagen.idIntervencion = idIntervencion;
        //                        oImagen.idTipoImagen = idTipoImagen;
        //                        oImagen.idImagen = (int)idImagen;
        //                        oImagen.idUsuario = idUsuario;
        //                        oImagen.tecnico = httpRequest.Headers["tecnico"].ToString();
        //                        oImagen.telefonoTecnico = httpRequest.Headers["telefonoTecnico"].ToString();
        //                        if (httpRequest.Headers.AllKeys.Contains("comentario"))
        //                        {
        //                            oImagen.comentario = httpRequest.Headers["comentario"].ToString();
        //                        }                                

        //                        new ReplanteoCRN_API().insertarReplanteoImagen(oImagen);

        //                    }
        //                }
        //            }

        //            var message1 = string.Format("Imagen subida correctamente. IdImagen: " + idImagen.ToString());
        //            HttpResponseMessage response =  Request.CreateResponse(HttpStatusCode.Created, message1);
        //            //response.Headers.Add("idImagen", idImagen.ToString());

        //            return response;

        //        }
        //        var res = string.Format("Please Upload a image.");
        //        dict.Add("error", res);
        //        return Request.CreateResponse(HttpStatusCode.NotFound, dict);
        //    }
        //    catch (Exception ex)
        //    {
        //        var res = string.Format(ex.Message);
        //        dict.Add("error", res);
        //        return Request.CreateResponse(HttpStatusCode.NotFound, dict);
        //    }
        //}

        [HttpDelete]
        [Route("replanteo/eliminarReplanteoMedida")]
        // DELETE: api/Replanteo/5
        public string eliminarReplanteoMedida(int idMedida)
        {
            new ReplanteoCRN_API().eliminarReplanteoMedida(idMedida);
            return "Medida eliminada";
        }
    }
}
