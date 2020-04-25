using CapaEntidades;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocioAPP
{
    public class IntervencionCRN_APP
    {
        public async Task<string> enviarImagenIntervencionFirma(Stream imagenStream, string nombreImagen, string rutaImagen, ImagenCE oImagen)
        {
            try
            {
                string metodo = "intervencion/insertarImagen";

                Dictionary<string, string> parametros = new Dictionary<string, string>();
                parametros.Add("idIntervencion", oImagen.idIntervencion.ToString());
                parametros.Add("idTipoImagen", oImagen.idTipoImagen.ToString());
                parametros.Add("tecnico", oImagen.tecnico);
                parametros.Add("telefonoTecnico", oImagen.telefonoTecnico);

                parametros.Add("tipoIntervencionImagen", "IntervencionFinalizacion");

                if (oImagen.comentario != "")
                {
                    parametros.Add("comentario", oImagen.comentario);
                }
                parametros.Add("idUsuario", oImagen.idUsuario.ToString());

                HttpResponseMessage response = await SingleHttpCliente.postImage(imagenStream, nombreImagen, rutaImagen, metodo, parametros);
                return await response.Content.ReadAsStringAsync();
            }
            catch
            {
                throw;
            }

        }
        public async Task<List<ImagenCE>> getImagenesFirmaByIntervencion(int idIntervencion)
        {
            List<ImagenCE> lstImagenesCE = new List<ImagenCE>();
            try
            {
                string metodo = "intervencion/getImagenesFirmaByIntervencion?idIntervencion=" + idIntervencion;
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
        public async Task<List<IntervencionNoTerminadaMotivosCE>> getMotivosIntervencionNoTerminada()
        {
            List<IntervencionNoTerminadaMotivosCE> lstMotivos = new List<IntervencionNoTerminadaMotivosCE>();
            try
            {
                string metodo = "intervencion/getMotivosIntervencionNoTerminada";
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

                    lstMotivos = oJsonArray.ToObject<List<IntervencionNoTerminadaMotivosCE>>();
                }
            }
            catch
            {
                throw;
            }
            return lstMotivos;
        }
        public async void insertarLocalizacion(LocalizacionCE oLocalizacion)
        {
            try
            {
                await SingleHttpCliente.postMethod(JsonConvert.SerializeObject(oLocalizacion), "intervencion/insertarLocalizacion");
            }
            catch
            {
                throw;
            }
        }
        public async Task<IntervencionCE> actualizarIntervencionEstado(int idIntervencion, int idEstado)
        {
            try
            {
                IntervencionCE oIntervencionCE = null;
                string jsonResult = (await SingleHttpCliente.getMethod("intervencion/actualizarEstado?idIntervencion=" + idIntervencion.ToString() + "&idEstado=" + idEstado.ToString())).Replace("[", "").Replace("]", "");
                JObject jsonObject = JObject.Parse(jsonResult);
                if (jsonObject.Count >= 1)
                {
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                                        
                    oIntervencionCE = JsonConvert.DeserializeObject<IntervencionCE>(jsonResult, settings);
                }
                return oIntervencionCE;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IntervencionNoTerminadaCE> insertarIntervencionNoTerminada(IntervencionNoTerminadaCE oNoTerminada)
        {
            try
            {
                string jsonResult = await SingleHttpCliente.postMethod(JsonConvert.SerializeObject(oNoTerminada), "intervencion/insertarIntervencionNoTerminada");
                JObject jsonObject = JObject.Parse(jsonResult);
                if (jsonObject.Count >= 1)
                {
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };

                    oNoTerminada = JsonConvert.DeserializeObject<IntervencionNoTerminadaCE>(jsonResult, settings);
                }

                return oNoTerminada;
            }
            catch
            {
                throw;
            }
        }
        public async Task<string> insertarImagenNoTerminada(Stream imagenStream, string nombreImagen, string rutaImagen, ImagenCE oImagen, int idIntervencionNoTerminada)
        {
            string resultado = "";
            try
            {
                string metodo = "intervencion/insertarImagen";

                Dictionary<string, string> parametros = new Dictionary<string, string>();
                parametros.Add("idIntervencion", oImagen.idIntervencion.ToString());
                parametros.Add("idIntervencionNoTerminada", idIntervencionNoTerminada.ToString());
                parametros.Add("idTipoImagen", "0");
                parametros.Add("tecnico", oImagen.tecnico);
                parametros.Add("telefonoTecnico", oImagen.telefonoTecnico);

                parametros.Add("tipoIntervencionImagen", "NoTerminada");

                if (oImagen.comentario != "")
                {
                    parametros.Add("comentario", oImagen.comentario);
                }
                parametros.Add("idUsuario", oImagen.idUsuario.ToString());

                HttpResponseMessage response = await SingleHttpCliente.postImage(imagenStream, nombreImagen, rutaImagen, metodo, parametros);
                return resultado;
            }
            catch
            {
                throw;
            }

        }

    }
}
