using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;

namespace CapaDatosAPI
{
    public class LoggingCAD : baseCAD
    {
        public void InsertErrorLog(ApiErrorCE apiError)
        {
            try
            {
                DbCommand cmd = base.CrearComandoSP();
                cmd.CommandText = "ApiError_Insertar";

                cmd.Parameters.Add(new SqlParameter("@RequestUri", apiError.RequestUri));
                cmd.Parameters.Add(new SqlParameter("@Message", apiError.Message));
                cmd.Parameters.Add(new SqlParameter("@RequestMethod", apiError.RequestMethod));
                cmd.CommandTimeout = 120;
                base.EjecutarComando(cmd);

            }
            catch
            {
                throw;
            }
        }

        public void InsertLog(ApiLogCE apiLog)
        {
            try
            {
                DbCommand cmd = base.CrearComandoSP();
                cmd.CommandText = "ApiLog_Insertar";

                cmd.Parameters.Add(new SqlParameter("@Host", apiLog.Host));
                cmd.Parameters.Add(new SqlParameter("@Headers", apiLog.Headers));
                cmd.Parameters.Add(new SqlParameter("@StatusCode", apiLog.StatusCode));
                cmd.Parameters.Add(new SqlParameter("@RequestBody", apiLog.RequestBody));
                cmd.Parameters.Add(new SqlParameter("@RequestedMethod", apiLog.RequestedMethod));
                cmd.Parameters.Add(new SqlParameter("@UserHostAddress", apiLog.UserHostAddress));
                cmd.Parameters.Add(new SqlParameter("@Useragent", apiLog.Useragent));
                cmd.Parameters.Add(new SqlParameter("@AbsoluteUri", apiLog.AbsoluteUri));
                cmd.Parameters.Add(new SqlParameter("@RequestType", apiLog.RequestType));
                cmd.CommandTimeout = 120;
                base.EjecutarComando(cmd);

            }
            catch
            {
                throw;
            }
        }

    }
}
