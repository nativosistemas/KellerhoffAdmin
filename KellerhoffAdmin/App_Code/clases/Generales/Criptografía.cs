using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitioBase.clases
{
    /// <summary>
    /// Summary description for Criptografía
    /// </summary>
    public class Criptografía
    {
        public static string md5(string pValor)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.ASCII.GetBytes(pValor);
            data = x.ComputeHash(data);
            string ret = string.Empty;
            for (int i = 0; i < data.Length; i++)
            {
                ret += data[i].ToString("x2").ToLower();
            }
            return ret;
        }
    }
}