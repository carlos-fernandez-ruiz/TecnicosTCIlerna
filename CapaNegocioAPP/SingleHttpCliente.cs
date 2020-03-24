using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocioAPP
{
    public static class SingleHttpCliente
    {
        private const string URL = "https://10.0.2.2:44303/";
        private static readonly HttpClient cliente;

        static SingleHttpCliente()
        {
            ServicePointManager.ServerCertificateValidationCallback += (o, cert, chain, errors) => true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            cliente = new HttpClient(clientHandler);
        }

        public async static Task<string> postMethod(string jsonString, string metodo)
        {
            try
            {
                StringContent prueba = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = await cliente.PostAsync(URL + metodo, new StringContent(jsonString, Encoding.UTF8, "application/json"));
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async static Task<string> deleteMethod(string metodo)
        {
            try
            {
                string result = await (await cliente.DeleteAsync(URL + metodo)).Content.ReadAsStringAsync();
                return result;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                throw ex;
            }
        }

        public static async Task<string> getMethod(string metodo)
        {
            try
            {

                string result = await cliente.GetStringAsync(URL + metodo);
                return result;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }
    }
}
