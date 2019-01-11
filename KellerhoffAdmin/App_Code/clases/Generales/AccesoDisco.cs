using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;

namespace SitioBase.clases
{
    public class AccesoDisco
    {
        public static void LogError(string pantalla, string mensaje)
        {
            grabarLog("fecha: " + DateTime.Now.ToString() + " - pantalla: " + pantalla + " - mensaje :" + mensaje, Constantes.cArchivo_LogErrorTxt);
        }
        public static bool EliminarArchivo(string pRutaYNombreArchivo) 
        {
            try
            {
                File.Delete(pRutaYNombreArchivo);
                return true;
            }
            catch (Exception ex)
            {
                grabarLog("fecha: " + DateTime.Now.ToString() + " - " + "Error al eliminar archivo: " + " - " + pRutaYNombreArchivo, Constantes.cArchivo_LogErrorTxt);
                return false;
            }
        
        }
        public static bool CopiarArchivo(string pRutaYNombreArchivoOrigen, string pRutaYNombreArchivoDestino)
        {
            try
            {
                File.Copy(pRutaYNombreArchivoOrigen, pRutaYNombreArchivoDestino);
                return true;
            }
            catch (Exception ex)
            {
                grabarLog("fecha: " + DateTime.Now.ToString() + " - " + "Error al copiar archivo desde: " + " - " + pRutaYNombreArchivoOrigen + " - " + pRutaYNombreArchivoDestino, Constantes.cArchivo_LogErrorTxt);
                return false;
            }

        }
        public static void grabarLog(string pMensaje, string pNombreArchivo) 
        {
            try
            {
                string path =HttpContext.Current.Server.MapPath( @"../" + Constantes.cArchivo_log + @"/");
                if (Directory.Exists(path) == false)
                {
                    Directory.CreateDirectory(path);
                }
                string FilePath = path + pNombreArchivo;
                if (!File.Exists(FilePath))
                {
                    using (StreamWriter sw = File.CreateText(FilePath))
                    {
                        sw.WriteLine(pMensaje);
                        sw.Close();
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(FilePath))
                    {
                        sw.WriteLine(pMensaje);
                        sw.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}