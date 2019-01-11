using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace SitioBase.capaDatos
{
    public class accesoBD
    {
        public static string ObtenerConexión()
        {
            string strConexión;
            strConexión = ConfigurationManager.ConnectionStrings["db_conexion"].ConnectionString;
            return strConexión;
        }
    }
}