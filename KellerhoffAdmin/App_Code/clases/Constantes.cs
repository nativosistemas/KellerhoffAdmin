using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitioBase
{
    public static class Constantes
    {
        public static string cSQL_INSERT
        {
            get { return "INSERT"; }
        }
        public static string cSQL_UPDATE
        {
            get { return "UPDATE"; }
        }
        public static string cSQL_SELECT
        {
            get { return "SELECT"; }
        }
        public static string cSQL_COMBO
        {
            get { return "COMBO"; }
        }
        public static string cSQL_ESTADO
        {
            get { return "ESTADO"; }
        }
        public static string cSQL_DELETE
        {
            get { return "DELETE"; }
        }
        public static string cSQL_CAMBIOCONTRASEÑA
        {
            get { return "CAMBIOCONTRASEÑA"; }
        }
        //public static string cSQL_ESCONTRASEÑACORRECTA
        //{
        //    get { return "ESCONTRASEÑACORRECTA"; }
        //}
        public static string cSQL_PUBLICAR
        {
            get { return "PUBLICAR"; }
        }
        public static string cSQL_SUBIR
        {
            get { return "SUBIR"; }
        }
        public static string cSQL_BAJAR
        {
            get { return "BAJAR"; }
        }

        public static string cDESC
        {
            get { return "DESC"; }
        }
        public static string cASC
        {
            get { return "ASC"; }
        }

        public static int cESTADO_SINESTADO
        {
            get { return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["estado_SinEstado"]); }
        }
        public static int cESTADO_ACTIVO
        {
            get { return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["estado_Activo"]); }
        }
        public static int cESTADO_INACTIVO
        {
            get { return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["estado_Inactivo"]); }
        }
        public static int cESTADO_SINLEER
        {
            get { return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["estado_SinLeer"]); }
        }
        public static int cESTADO_LEIDO
        {
            get { return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["estado_Leido"]); }
        }
        public static string cESTADO_STRING_SINESTADO
        {
            get { return "Sin Estado"; }
        }
        public static string cESTADO_STRING_ACTIVO
        {
            get { return "Activo"; }
        }
        public static string cESTADO_STRING_INACTIVO
        {
            get { return "Inactivo"; }
        }
        public static int cACCION_ALTA
        {
            get { return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["accion_Alta"]); }
        }
        public static int cACCION_MODIFICACION
        {
            get { return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["accion_Modificar"]); }
        }
        public static int cACCION_CAMBIOESTADO
        {
            get { return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["accion_CambioEstado"]); }
        }
        public static int cACCION_CAMBIOCONTRASEÑA
        {
            get { return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["accion_CambioContraseña"]); }
        }
        public static int cACCION_CAMBIOORDEN
        {
            get { return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["accion_CambioOrden"]); }
        }
        public static int cACCION_ISPUBLICAR
        {
            get { return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["accion_CambioPublicar"]); }
        }
        public static int cACCION_ELIMINAR
        {
            get { return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["accion_CambioEliminar"]); }
        }
        public static int cROL_ADMINISTRADORCLIENTE
        {
            get { return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["rol_AdministradorCliente"]); }
        }
        public static int cROL_OPERADORCLIENTE
        {
            get { return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["rol_OperadorCliente"]); }
        }
        public static string cTABLA_CATALOGO
        {
            get { return "catalogo"; }
        }
        public static string cTABLA_CV
        {
            get { return "curriculumvitae"; }
        }
        public static string cTABLA_PRODUCTO
        {
            get { return "producto"; }
        }
        public static string cTABLA_NOTICIA
        {
            get { return "noticia"; }
        }
        public static string cMIME_xls
        {
            get { return "application/excel"; }
        }
        public static string cMIME_xlsx
        {
            get { return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; }
        }
        public static string cMIME_doc
        {
            get { return "application/msword"; }
        }
        public static string cMIME_docx
        {
            get { return "application/vnd.openxmlformats-officedocument.wordprocessingml.document"; }
        }
        public static string cMIME_jpeg
        {
            get { return "image/pjpeg"; }
        }
        public static string cMIME_jpe
        {
            get { return cMIME_jpeg; }
        }
        public static string cMIME_jpg
        {
            get { return cMIME_jpeg; }
        }
        
        public static string cMIME_png
        {
            get { return "image/png"; }
        }
        public static string cMIME_pdf
        {
            get { return "application/pdf"; }
        }
        public static string cMIME_ai
        {
            get { return "application/postscript"; }
        }
        public static string cMIME_gif
        {
            get { return "image/gif"; }
        }
        public static string cMIME_psd
        {
            get { return "application/octet-stream"; }
        }
        public static string cMIME_vsd
        {
            get { return "application/x-visio"; }
        }
        public static string cMIME_CSV
        {
            get { return "text/csv"; }
        }
        public static string cArchivo_Raiz
        {
            get { return "archivos"; }
        }
        public static string cArchivo_Temp
        {
            get { return "temp"; }
        }
        public static string cArchivo_TempUpload
        {
            get { return @"temp/upload"; }
        }
        public static string cArchivo_log
        {
            get { return @"log"; }
        }
        public static string cArchivo_LogErrorTxt
        {
            get { return @"LogError.txt"; }
        }
        public static string cArchivo_ImpresionesComprobante
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["ImpresionesComprobante"]; }
        }
        public static string cImagenesRedimensionadas
        {
            get { return "ImagenesRedimensionadas"; }
        }
        //public static string cEstadoCarrito_EnProceso
        //{
        //    get { return "P"; }
        //}
        //public static string cEstadoCarrito_Enviado
        //{
        //    get { return "E"; }
        //}
        public static string cTIPOPRODUCTO_Medicamento
        {
            get { return "M"; }
        }
        public static string cTIPOPRODUCTO_Perfumeria
        {
            get { return "P"; }
        }
        public static string cTIPOPRODUCTO_Accesorio
        {
            get { return "A"; }
        }
        public static string cTIPOPRODUCTO_PerfumeriaCuentaYOrden
        {
            get { return "F"; }
        }
        public static int cPEDIDO_FALTANTES
        {
            get { return 1; }
        }
        public static int cPEDIDO_PROBLEMACREDITICIO
        {
            get { return 2; }
        }
        public static string cRaizLog
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["raiz_Log"].ToString(); }
        }
        public static string cRaizArchivos
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["raiz_archivos"].ToString(); }
        }
        public static string cDIASEMANA_Lunes
        {
            get { return "LU"; }
        }
        public static string cDIASEMANA_Martes
        {
            get { return "MA"; }
        }
        public static string cDIASEMANA_Miercoles
        {
            get { return "MI"; }
        }
        public static string cDIASEMANA_Jueves
        {
            get { return "JU"; }
        }
        public static string cDIASEMANA_Viernes
        {
            get { return "VI"; }
        }
        public static string cDIASEMANA_Sabado
        {
            get { return "SA"; }
        }
        public static string cDIASEMANA_Domingo
        {
            get { return "DO"; }
        }
        public static string cESTADO_HAB
        {
            get { return "HAB"; }
        }
        public static string cESTADO_INH
        {
            get { return "INH"; }
        }
        public static string cRaiz
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["raiz"].ToString(); }
        }
        public static string cBAN_SERVIDORDLL
        {
            get { return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["BAN_SERVIDORDLL"]); }
        }
        public static string cSECCION_PEDIDOS
        {
            get { return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["seccion_PEDIDOS"]); }
        }
        public static string cSECCION_DESCARGAS
        {
            get { return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["seccion_DESCARGAS"]); }
        }
        public static string cSECCION_CUENTASCORRIENTES
        {
            get { return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["seccion_CUENTASCORRIENTES"]); }
        }
        public static string cSeparadorCSV
        {
            get { return ";"; }
        }
        public static int cAcuerdo_SinAcuerdo
        {
            get { return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["acuerdo_SinAcuerdo"]); }
        }
        public static int cAcuerdo_GENOMA
        {
            get { return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["acuerdo_GENOMA"]); }
        }
        public static int cAcuerdo_ADEM
        {
            get { return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["acuerdo_ADEM"]); }
        }
        public static string cTipoCliente_Perfumeria
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["tipoCliente_Perfumeria"].ToString(); }
        }
        public static string cTipoCliente_Todos
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["tipoCliente_Todos"].ToString(); }
        }
        public static int cWidth_Oferta
        { 
            get { return 300; }
        }
        public static int cHeight_Oferta
        {
            get { return 300; }
        }
    }
}