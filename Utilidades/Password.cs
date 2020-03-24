using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Utilidades
{
    public class Password
    {
        public string obtenerHash(string pPassword)
        {
            Byte[] bData = System.Text.UTF8Encoding.ASCII.GetBytes(pPassword);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            Byte[] bHashbyte = md5.ComputeHash(bData, 0, bData.Length);
            return BitConverter.ToString(bHashbyte);
        }


    }
}
