using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CapaNegocioAPI;
using CapaEntidades;

namespace API_Rest.Controllers
{
    public class UserController : ApiController
    {        

        // GET api/values/5
        [HttpGet]
        [Route("user/getUsuarioIntervencionLoginTabla")]
        public DataTable GetUsuarioIntervencionTabla(string user, string codigoIntervencion)
        {
             
            return (new UsuariosCRN_API()).getUsuarioIntervencionTabla(user, codigoIntervencion);
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
