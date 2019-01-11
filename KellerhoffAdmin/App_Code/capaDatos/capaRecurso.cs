using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace SitioBase.capaDatos
{
    public class cArchivo
    {
        public int arc_codRecurso { get; set; }
        public int arc_codRelacion { get; set; }
        public string arc_galeria { get; set; }
        public int arc_orden { get; set; }
        public string arc_tipo { get; set; }
        public string arc_mime { get; set; }
        public string arc_nombre { get; set; }
        public string arc_titulo { get; set; }
        public string arc_descripcion { get; set; }
        public string arc_hash { get; set; }
        public DateTime arc_fecha { get; set; }
        public string arc_fechaToString { get; set; }
        public int arc_accion { get; set; }
        public int arc_estado { get; set; }
        public string arc_estadoToString { get; set; }
        public DateTime arc_fechaUltMov { get; set; }
        public int arc_codUsuarioUltMov { get; set; }
        public string NombreYapellido { get; set; }
    }
    public class cNombreArchivos
    {
        public string NombreOriginal { get; set; }
        public string NombreGrabado { get; set; }
    }
    public class capaRecurso
    {
        public static DataSet GestiónArchivo(int? arc_codRecurso, int? arc_codRelacion, string arc_galeria, string arc_tipo, string arc_mime, string arc_nombre, string arc_titulo, string arc_descripcion, string arc_hash, int? arc_codUsuarioUltMov, int? arc_estado, int? arc_accion, string filtro, string accion)
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("Recursos.spGestionArchivo", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;

            SqlParameter paArc_codRecurso = cmdComandoInicio.Parameters.Add("@arc_codRecurso", SqlDbType.Int);
            SqlParameter paArc_codRelacion = cmdComandoInicio.Parameters.Add("@arc_codRelacion", SqlDbType.Int);
            SqlParameter paArc_galeria = cmdComandoInicio.Parameters.Add("@arc_galeria", SqlDbType.NVarChar, 50);
            SqlParameter paArc_tipo = cmdComandoInicio.Parameters.Add("@arc_tipo", SqlDbType.NVarChar, 50);
            SqlParameter paArc_mime = cmdComandoInicio.Parameters.Add("@arc_mime", SqlDbType.NVarChar, 100);
            SqlParameter paArc_nombre = cmdComandoInicio.Parameters.Add("@arc_nombre", SqlDbType.NVarChar, 150);
            SqlParameter paArc_titulo = cmdComandoInicio.Parameters.Add("@arc_titulo", SqlDbType.NVarChar, 200);
            SqlParameter paArc_descripcion = cmdComandoInicio.Parameters.Add("@arc_descripcion", SqlDbType.NVarChar, -1);
            SqlParameter paArc_hash = cmdComandoInicio.Parameters.Add("@arc_hash", SqlDbType.NVarChar, 50);
            SqlParameter paArc_codUsuarioUltMov = cmdComandoInicio.Parameters.Add("@arc_codUsuarioUltMov", SqlDbType.Int);
            SqlParameter paArc_estado = cmdComandoInicio.Parameters.Add("@arc_estado", SqlDbType.Int);
            SqlParameter paArc_accion = cmdComandoInicio.Parameters.Add("@arc_accion", SqlDbType.Int);
            SqlParameter paFiltro = cmdComandoInicio.Parameters.Add("@filtro", SqlDbType.NVarChar, 50);
            SqlParameter paAccion = cmdComandoInicio.Parameters.Add("@accion", SqlDbType.NVarChar, 50);

            if (arc_codRecurso == null)
            {
                paArc_codRecurso.Value = DBNull.Value;
            }
            else
            {
                paArc_codRecurso.Value = arc_codRecurso;
            }
            if (arc_codRelacion == null)
            {
                paArc_codRelacion.Value = DBNull.Value;
            }
            else
            {
                paArc_codRelacion.Value = arc_codRelacion;
            }
            if (arc_galeria == null)
            {
                paArc_galeria.Value = DBNull.Value;
            }
            else
            {
                paArc_galeria.Value = arc_galeria;
            }
            if (arc_tipo == null)
            {
                paArc_tipo.Value = DBNull.Value;
            }
            else
            {
                paArc_tipo.Value = arc_tipo;
            }
            if (arc_mime == null)
            {
                paArc_mime.Value = DBNull.Value;
            }
            else
            {
                paArc_mime.Value = arc_mime;
            }
            if (arc_nombre == null)
            {
                paArc_nombre.Value = DBNull.Value;
            }
            else
            {
                paArc_nombre.Value = arc_nombre;
            }
            if (arc_titulo == null)
            {
                paArc_titulo.Value = DBNull.Value;
            }
            else
            {
                paArc_titulo.Value = arc_titulo;
            }

            if (arc_descripcion == null)
            {
                paArc_descripcion.Value = DBNull.Value;
            }
            else
            {
                paArc_descripcion.Value = arc_descripcion;
            }
            if (arc_hash == null)
            {
                paArc_hash.Value = DBNull.Value;
            }
            else
            {
                paArc_hash.Value = arc_hash;
            }
            if (arc_codUsuarioUltMov == null)
            {
                paArc_codUsuarioUltMov.Value = DBNull.Value;
            }
            else
            {
                paArc_codUsuarioUltMov.Value = arc_codUsuarioUltMov;
            }
            if (arc_estado == null)
            {
                paArc_estado.Value = DBNull.Value;
            }
            else
            {
                paArc_estado.Value = arc_estado;
            }
            if (arc_accion == null)
            {
                paArc_accion.Value = DBNull.Value;
            }
            else
            {
                paArc_accion.Value = arc_accion;
            }
            if (filtro == null)
            {
                paFiltro.Value = DBNull.Value;
            }
            else
            {
                paFiltro.Value = filtro;
            }
            paAccion.Value = accion;

            try
            {
                Conn.Open();
                DataSet dsResultado = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
                da.Fill(dsResultado, "Archivo");
                return dsResultado;

            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }
        public static string obtenerExtencion(string pNombreArchivo)
        {
            string resultado = string.Empty;
            if (!string.IsNullOrEmpty(pNombreArchivo))
            {
                string[] listaNombre = pNombreArchivo.Split('.');
                resultado = listaNombre[listaNombre.Length - 1].Trim().ToLower();
            }
            return resultado;
        }
        public static string obtenerMIME(string pExtencion)
        {
            string resultado = string.Empty;

            switch (pExtencion.Trim().ToLower())
            {
                case "jpe":
                    return Constantes.cMIME_jpe;
                //break;
                case "jpeg":
                    return Constantes.cMIME_jpeg;
                //break;
                case "jpg":
                    return Constantes.cMIME_jpg;
                //break;                
                case "doc":
                    return Constantes.cMIME_doc;
                //break;
                case "docx":
                    return Constantes.cMIME_docx;
                //break;
                case "xls":
                    return Constantes.cMIME_xls;
                //break;
                case "xlsx":
                    return Constantes.cMIME_xlsx;
                case "gif":
                    return Constantes.cMIME_gif;
                //break;
                case "png":
                    return Constantes.cMIME_png;
                //break;
                case "ai":
                    return Constantes.cMIME_ai;
                //break;
                case "pdf":
                    return Constantes.cMIME_pdf;
                //break;
                case "psd":
                    return Constantes.cMIME_psd;
                //break;
                case "vsd":
                    return Constantes.cMIME_vsd;
                //break;
                default:
                    break;
            }
            return resultado;
        }
        public static string nombreArchivoSinRepetir(string pPath, string pNombreArchivo)
        {
            string resultado = string.Empty;
            string[] listaNombre = pNombreArchivo.Split('.');
            string NombreArchivo = string.Empty;
            string ExtencionArchivo = string.Empty;
            for (int i = 0; i < listaNombre.Length - 1; i++)
            {
                NombreArchivo += listaNombre[i];
            }
            NombreArchivo = remplazarCaracteresEspeciales(NombreArchivo);
            ExtencionArchivo = listaNombre[listaNombre.Length - 1];
            int contNombre = -1;
            string NombreTemporal = NombreArchivo + "." + ExtencionArchivo;
            while (System.IO.File.Exists(pPath + "\\" + NombreTemporal))
            {
                contNombre++;
                NombreTemporal = NombreArchivo + "_" + contNombre.ToString() + "." + ExtencionArchivo;
            }
            if (contNombre == -1)
            {
                resultado = NombreArchivo + "." + ExtencionArchivo;
            }
            else
            {
                resultado = NombreTemporal;
            }
            return resultado;
        }
        public static string remplazarCaracteresEspeciales(string pStr)
        {
            //pStr = Encoding.UTF8.GetString(pStr.GetEnumerator());
            const string pStrOriginal = "áéíóúàèìòùâêîôûäëïöüãõñÁÉÍÓÚÀÈÌÒÙÂÊÎÔÛÄËÏÖÜÑçÇ";
            const string pStrARemplazar = "aeiouaeiouaeiouaeiouaonaeiouaeiouaeiouaeiouncc";
            if (pStr != null)
            {
                string resultado = string.Empty;
                for (int i = 0; i < pStrOriginal.Length; i++)
                {
                    pStr.Replace(pStrOriginal[i], pStrARemplazar[i]);
                }

                for (int i = 0; i < pStr.Length; i++)
                {
                    char[] CharEspeciales = new char[] { '\r', '\n', '\t' }; ;
                    if (pStr[i] == CharEspeciales[0] || pStr[i] == CharEspeciales[1] || pStr[i] == CharEspeciales[2])
                    {
                    }
                    else
                    {
                        resultado += pStr[i];
                    }

                }
                return resultado;
            }
            else
            {
                return null;
            }
        }
    }
    
}