using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CapaNegocioAPI;
using CapaEntidades;
using System.Web;
using System.ServiceModel.Channels;

namespace API_Rest.Controllers
{
    public class UserController : ApiController
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        // GET api/values/5
        [HttpGet]
        [Route("user/getUsuarioIntervencionLoginTabla")]
        public DataTable GetUsuarioIntervencionTabla(string user, string codigoIntervencion)
        {
            return (new UsuariosCRN_API()).getUsuarioIntervencionTabla(user, codigoIntervencion);
        }

        private string GetClientIp(HttpRequestMessage request = null)
        {
            request = request ?? Request;

            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
            else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            {
                RemoteEndpointMessageProperty prop = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name];
                return prop.Address;
            }
            else if (HttpContext.Current != null)
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
            else
            {
                return null;
            }
        }
    }
}
