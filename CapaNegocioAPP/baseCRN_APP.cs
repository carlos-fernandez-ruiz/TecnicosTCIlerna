using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocioAPP
{
    public class baseCRN_APP
    {
        public string usernameAPI;
        public string passwordAPI;
        protected string obtenerHash(string sPassword)
        {
            Byte[] bData = System.Text.UTF8Encoding.ASCII.GetBytes(sPassword);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            Byte[] bHashbyte = md5.ComputeHash(bData, 0, bData.Length);
            return BitConverter.ToString(bHashbyte);
        }

        public string callMethod(string method)
        {
            try
            {
                string result = "";
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(method);

                webrequest.Method = "GET";
                string userPass = String.Format("{0}:{1}", usernameAPI, passwordAPI);
                webrequest.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(userPass));

                using (var response = webrequest.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        result = reader.ReadToEnd();
                    }
                }

                return result;
            }
            catch
            {
                throw;
            }

        }
    }
}
