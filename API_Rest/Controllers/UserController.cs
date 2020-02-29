using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CapaNegocioAPI;
using CapaEntidades;
using CapaNegocioAPI;

namespace API_Rest.Controllers
{
    public class UserController : ApiController
    {
        // GET api/values
        [HttpGet]
        [Route("user/usercheck")]
        public bool UserCheck(string user, string password)
        {
            bool result = false;
            bool encrypted = true;

            UsuariosCE oUsuarioCE = (new UsuariosCRN()).AutentificarUsuario(user, password, encrypted);

            if (oUsuarioCE != null)
            {
                //recuperamos el usuario
                result = true;
            }

            return result;

        }


        // GET api/values/5
        public string Get(int id)
        {
            return "value";
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
