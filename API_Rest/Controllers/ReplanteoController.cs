using System;
using System.Collections.Generic;
using System.Data;
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
        [Route("replanteo/replanteoFinalizacionActualizarEstado")]
        public IntervencionFinalizacionCE replanteoFinalizacionActualizarEstado([FromBody]IntervencionFinalizacionCE oFinalizacion)
        {
            return (new ReplanteoCRN_API()).replanteoFinalizacionActualizarEstado(oFinalizacion);
        }

        [HttpPost]
        [Route("replanteo/insertarReplanteoMedida")]
        public void insertarReplanteoMedida([FromBody]MedidaCE oMedida)
        {
            new ReplanteoCRN_API().insertarReplanteoMedida(oMedida);
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
