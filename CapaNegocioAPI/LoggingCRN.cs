using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;
using CapaDatosAPI;

namespace CapaNegocioAPI
{
    public class LoggingCRN
    {
        public void InsertErrorLog(ApiErrorCE apiError)
        {
            try
            {
                new LoggingCAD().InsertErrorLog(apiError);
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
                new LoggingCAD().InsertLog(apiLog);
            }
            catch
            {
                throw;
            }
        }
        public void IncomingMessageAsync(ApiLogCE apiLog)
        {
            try
            {
                apiLog.RequestType = "Request";
                var sqlErrorLogging = new LoggingCAD();
                sqlErrorLogging.InsertLog(apiLog);
            }
            catch
            {
                throw;
            }
        }

        public void OutgoingMessageAsync(ApiLogCE apiLog)
        {
            try
            {
                apiLog.RequestType = "Response";
                var sqlErrorLogging = new LoggingCAD();
                sqlErrorLogging.InsertLog(apiLog);
            }
            catch
            {
                throw;
            }
        }

    }
}
