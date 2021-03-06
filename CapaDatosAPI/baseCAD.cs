﻿using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;

namespace CapaDatosAPI
{
    public class baseCAD
    {        
        protected DbCommand CrearComandoSP(int timeout = 1)
        {
            //' Creamos DbFactory con el Proveedor y Conexion 
            DbProviderFactory Factory = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["ConexionBaseDatos"].ProviderName);
            DbConnection Conn = Factory.CreateConnection();
            Conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConexionBaseDatos"].ConnectionString;

            //' Creamos el Comando para ejecutar la Stored Procedure
            DbCommand oDbComando = Conn.CreateCommand();
            oDbComando.CommandType = CommandType.StoredProcedure;

            if (timeout > 0)
            {
                oDbComando.CommandTimeout = timeout;
            }
            else
            {
                oDbComando.CommandTimeout = 180;
            }

            return oDbComando;
        }
        protected DataTable EjecutarReader(DbCommand oDbComando)
        {
            DataTable tabla = null;

            try
            {
                oDbComando.Connection.Open();
                DbDataReader reader = oDbComando.ExecuteReader();
                tabla = new DataTable();
                tabla.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en BaseDAO-EjecutarReader: " + ex.Message);
            }
            finally
            {
                oDbComando.Connection.Close();
            }

            //' Devolver tabla con los datos de la consulta
            return tabla;

        }

        
        protected long EjecutarScalar(DbCommand oDbComando)
        {

            long returnValue = 0;
            try
            {
                oDbComando.Connection.Open();
                oDbComando.CommandTimeout = 180;
                returnValue = Convert.ToInt64(oDbComando.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw new Exception("Error en BaseDAO-EjecutarScalar: " + ex.Message);
            }
            finally
            {
                oDbComando.Connection.Close();
            }

            //' Devolver id del comando ejecutado
            return returnValue;
        }

     
        protected long EjecutarComando(DbCommand oDbComando)
        {
            try
            {
                oDbComando.Connection.Open();
                return oDbComando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en BaseDAO-EjecutarComando: " + ex.Message);
            }
            finally
            {
                oDbComando.Connection.Close();
            }
        }



    }
}
