using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades;
using CapaEntidades;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;

namespace CapaNegocioAPP
{
    public class UsuariosCRN_APP
    {
        
        public async Task<(UsuariosCE, IntervencionCE)> getUsuarioIntervencionLogin(string username, string intervencion)
        {
            UsuariosCE oUsuarioCE = null;
            IntervencionCE oIntervencionCE = null;
            try
            {
                string metodo = "user/getUsuarioIntervencionLoginTabla?user=" + username + "&codigointervencion=" + intervencion;

                string jsonResult = (await SingleHttpCliente.getMethod(metodo)).Replace("[", "").Replace("]", "");
                //string jsonResult = base.callMethod(metodo).Replace("[", "").Replace("]", "");
                JObject jsonObject = JObject.Parse(jsonResult);
                if (jsonObject.Count >= 1)
                {
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };

                    oUsuarioCE = JsonConvert.DeserializeObject<UsuariosCE>(jsonResult, settings);
                    oIntervencionCE = JsonConvert.DeserializeObject<IntervencionCE>(jsonResult, settings);
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return (oUsuarioCE, oIntervencionCE);
                throw;
            }
            return (oUsuarioCE, oIntervencionCE);
        }
    }
}
