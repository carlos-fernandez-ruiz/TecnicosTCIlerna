using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CapaEntidades;
using CapaNegocioAPI;

namespace API_Rest.Controllers
{
    public class ReplanteoController : ApiController
    {       
       
        [HttpGet]
        [Route("replanteo/getMedidasTipos")]
        public List<MedidaTiposCE> getMedidasTipos()
        {
            return (new ReplanteoCRN_API()).getMedidasTipos();
        }

        [HttpGet]
        [Route("replanteo/getMedidasByIntervencion")]
        public List<MedidaCE> getMedidasTipos(int idIntervencion)
        {
            return (new ReplanteoCRN_API()).getMedidasByIntervencion(idIntervencion);
        }


        [HttpPost]
        [Route("replanteo/insertarReplanteoMedida")]
        public string insertarReplanteoMedida([FromBody]MedidaCE oMedida)
        {
            return new ReplanteoCRN_API().insertarReplanteoMedida(oMedida);
        }

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
