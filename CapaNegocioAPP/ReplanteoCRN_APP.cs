using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CapaNegocioAPP
{
    public class ReplanteoCRN_APP
    {
        public async Task<List<MedidaTiposCE>> getMedidasTipos()
        {
            List<MedidaTiposCE> lstMedidasTiposCE = new List<MedidaTiposCE>();
            try
            {                
                string metodo = "replanteo/getMedidasTipos";
                //string jsonResult = base.callMethod(metodo);
                string jsonResult = await SingleHttpCliente.getMethod(metodo);
                JArray oJsonArray = JArray.Parse(jsonResult);
                if (oJsonArray.Count >= 1)
                {
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };

                    lstMedidasTiposCE = oJsonArray.ToObject<List<MedidaTiposCE>>();
                }
            }
            catch
            {               
                throw;
            }
            return lstMedidasTiposCE;
        }

        public async Task<List<ImagenTiposCE>> getImagenesTipos()
        {
            List<ImagenTiposCE> lstImagenesTiposCE = new List<ImagenTiposCE>();
            try
            {
                string metodo = "replanteo/getImagenesTipos";
                //string jsonResult = base.callMethod(metodo);
                string jsonResult = await SingleHttpCliente.getMethod(metodo);
                JArray oJsonArray = JArray.Parse(jsonResult);
                if (oJsonArray.Count >= 1)
                {
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };

                    lstImagenesTiposCE = oJsonArray.ToObject<List<ImagenTiposCE>>();
                }
            }
            catch
            {
                throw;
            }
            return lstImagenesTiposCE;
        }

        public async Task<List<MedidaCE>> getMedidasByIntervencion(int idIntervencion)
        {
            List<MedidaCE> lstMedidasCE = new List<MedidaCE>();
            try
            {
                string metodo = "replanteo/getMedidasByIntervencion?idIntervencion=" + idIntervencion;
                //string jsonResult = base.callMethod(metodo);
                string jsonResult = await SingleHttpCliente.getMethod(metodo);
                JArray oJsonArray = JArray.Parse(jsonResult);
                if (oJsonArray.Count >= 1)
                {
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };

                    lstMedidasCE = oJsonArray.ToObject<List<MedidaCE>>();
                }
            }
            catch
            {
                throw;
            }
            return lstMedidasCE;
        }

        public async Task<IntervencionFinalizacionCE> getReplanteoFinalizacionByIntervencion(int idIntervencion)
        {
            IntervencionFinalizacionCE oFinalizacion = new IntervencionFinalizacionCE();
            try
            {
                string metodo = "replanteo/getReplanteoFinalizacionByIntervencion?idIntervencion=" + idIntervencion;
                //string jsonResult = base.callMethod(metodo);
                string jsonResult = (await SingleHttpCliente.getMethod(metodo)).Replace("[", "").Replace("]", "");
                JObject jsonObject = JObject.Parse(jsonResult);
                if (jsonObject.Count >= 1)
                {
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };

                    oFinalizacion = JsonConvert.DeserializeObject<IntervencionFinalizacionCE>(jsonResult, settings);
                }
            }
            catch
            {
                throw;
            }
            return oFinalizacion;
        }

        public async Task<List<ImagenCE>> getImagenesByIntervencion(int idIntervencion)
        {
            List<ImagenCE> lstImagenesCE = new List<ImagenCE>();
            try
            {
                string metodo = "replanteo/getImagenesByIntervencion?idIntervencion=" + idIntervencion;
                //string jsonResult = base.callMethod(metodo);
                string jsonResult = await SingleHttpCliente.getMethod(metodo);
                JArray oJsonArray = JArray.Parse(jsonResult);
                if (oJsonArray.Count >= 1)
                {
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };

                    lstImagenesCE = oJsonArray.ToObject<List<ImagenCE>>();
                }
            }
            catch
            {
                throw;
            }
            return lstImagenesCE;
        }

        public async Task<string> enviarImagenReplanteo(Stream imagenStream, string nombreImagen, string rutaImagen, ImagenCE oImagen)
        {
            string resultado = "";
            try
            {
                string metodo = "intervencion/insertarImagen";
                
                Dictionary<string, string> parametros = new Dictionary<string, string>();
                parametros.Add("idIntervencion", oImagen.idIntervencion.ToString());
                parametros.Add("idTipoImagen", oImagen.idTipoImagen.ToString());
                parametros.Add("tecnico", oImagen.tecnico);
                parametros.Add("telefonoTecnico", oImagen.telefonoTecnico);

                parametros.Add("tipoIntervencionImagen", "Replanteo");                
                
                if (oImagen.comentario != "")
                {
                    parametros.Add("comentario", oImagen.comentario);
                }
                parametros.Add("idUsuario", oImagen.idUsuario.ToString());

                HttpResponseMessage response = await SingleHttpCliente.postImage(imagenStream, nombreImagen, rutaImagen, metodo, parametros);     
                if (response.StatusCode != System.Net.HttpStatusCode.Created)
                {
                    return response.ReasonPhrase;
                } 
                else
                {
                    resultado = "OK";
                }
                return resultado;
            } 
            catch 
            {
                throw;
            }
            
        }

        public async void insertarReplanteoMedida(MedidaCE oMedida)
        {
            try
            {                
                await SingleHttpCliente.postMethod(JsonConvert.SerializeObject(oMedida), "replanteo/insertarReplanteoMedida");
            } 
            catch 
            {
                throw;
            }
        }

        public async void eliminarReplanteoMedida(int idMedida)
        {
            try
            {                
                await SingleHttpCliente.deleteMethod("replanteo/eliminarReplanteoMedida?idMedida=" + idMedida);
            }
            catch 
            {
                throw;
            }
        }
    }
}
