using System;
using System.Collections.Generic;
using System.Linq;
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
            List<MedidaTiposCE> lstMedidasCE = new List<MedidaTiposCE>();
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

                    lstMedidasCE = oJsonArray.ToObject<List<MedidaTiposCE>>();
                }
            }
            catch
            {               
                throw;
            }
            return lstMedidasCE;
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

        public async void insertarReplanteoMedida(MedidaCE oMedida)
        {
            try
            {                
                await SingleHttpCliente.postMethod(JsonConvert.SerializeObject(oMedida), "replanteo/insertarReplanteoMedida");
            } 
            catch (Exception ex)
            {
                string prueba = ex.Message;
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
