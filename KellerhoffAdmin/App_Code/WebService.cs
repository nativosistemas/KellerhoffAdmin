using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using SitioBase.capaDatos;
using System.Data;
using SitioBase;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WebService : System.Web.Services.WebService
{

    //public WebService () {
    //    //Uncomment the following line if using designed components 
    //    //InitializeComponent(); 
    //}

    //[WebMethod]
    //public static string HolaMundo()
    //{
    //    return "Hola Mundo!";
    //}
    public static Autenticacion CredencialAutenticacion;

    public static Boolean VerificarPermisos(Autenticacion pValor)
    {
        if (pValor == null)
        {
            return false;
        }
        else
        {
            //Verifica los permiso Ej. Consulta a BD 
            if (pValor.UsuarioNombre == System.Configuration.ConfigurationManager.AppSettings["ws_usu"] && pValor.UsuarioClave == System.Configuration.ConfigurationManager.AppSettings["ws_psw"])
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    //// Archivo
    public static int InsertarActualizarArchivo(int arc_codRecurso, int arc_codRelacion, string arc_galeria, string arc_tipo, string arc_mime, string arc_nombre, string arc_titulo, string arc_descripcion, string arc_hash, int arc_codUsuarioUltMov)
    {
        //if (VerificarPermisos(CredencialAutenticacion))
        //{
        string accion = arc_codRecurso == 0 ? Constantes.cSQL_INSERT : Constantes.cSQL_UPDATE;
        int codigoAccion = arc_codRecurso == 0 ? Constantes.cACCION_ALTA : Constantes.cACCION_MODIFICACION;
        int? codigoEstado = arc_codRecurso == 0 ? Constantes.cESTADO_ACTIVO : (int?)null;
        DataSet dsResultado = capaRecurso.GestiónArchivo(arc_codRecurso, arc_codRelacion, arc_galeria, arc_tipo, arc_mime, arc_nombre, arc_titulo, arc_descripcion, arc_hash, arc_codUsuarioUltMov, codigoEstado, codigoAccion, null, accion);
        int resultado = -1;
        if (arc_codRecurso == 0)
        {
            if (dsResultado != null)
            {
                if (dsResultado.Tables["Archivo"].Rows[0]["arc_codRecurso"] != DBNull.Value)
                {
                    resultado = Convert.ToInt32(dsResultado.Tables["Archivo"].Rows[0]["arc_codRecurso"]);
                }
            }
        }
        else
        {
            resultado = arc_codRecurso;
        }
        return resultado;
        //}
        //else
        //{
        //    return -100;
        //}
    }


    private static cArchivo ConvertToArchivo(DataRow pItem)
    {
        cArchivo obj = new cArchivo();

        if (pItem["arc_codRecurso"] != DBNull.Value)
        {
            obj.arc_codRecurso = Convert.ToInt32(pItem["arc_codRecurso"]);
        }
        if (pItem["arc_codRelacion"] != DBNull.Value)
        {
            obj.arc_codRelacion = Convert.ToInt32(pItem["arc_codRelacion"]);
        }
        if (pItem["arc_galeria"] != DBNull.Value)
        {
            obj.arc_galeria = pItem["arc_galeria"].ToString();
        }
        if (pItem["arc_orden"] != DBNull.Value)
        {
            obj.arc_orden = Convert.ToInt32(pItem["arc_orden"]);
        }
        if (pItem["arc_tipo"] != DBNull.Value)
        {
            obj.arc_tipo = pItem["arc_tipo"].ToString();
        }
        if (pItem["arc_mime"] != DBNull.Value)
        {
            obj.arc_mime = pItem["arc_mime"].ToString();
        }
        if (pItem["arc_nombre"] != DBNull.Value)
        {
            obj.arc_nombre = pItem["arc_nombre"].ToString();
        }
        if (pItem["arc_titulo"] != DBNull.Value)
        {
            obj.arc_titulo = pItem["arc_titulo"].ToString();
        }
        if (pItem["arc_descripcion"] != DBNull.Value)
        {
            obj.arc_descripcion = pItem["arc_descripcion"].ToString();
        }
        if (pItem["arc_tipo"] != DBNull.Value)
        {
            obj.arc_tipo = pItem["arc_tipo"].ToString();
        }
        if (pItem["arc_codUsuarioUltMov"] != DBNull.Value)
        {
            obj.arc_codUsuarioUltMov = Convert.ToInt32(pItem["arc_codUsuarioUltMov"]);
        }
        if (pItem["NombreYapellido"] != DBNull.Value)
        {
            obj.NombreYapellido = pItem["NombreYapellido"].ToString();
        }
        if (pItem["arc_hash"] != DBNull.Value)
        {
            obj.arc_hash = pItem["arc_hash"].ToString();
        }
        if (pItem["arc_fecha"] != DBNull.Value)
        {
            obj.arc_fecha = Convert.ToDateTime(pItem["arc_fecha"]);
            obj.arc_fechaToString = obj.arc_fecha.ToString();
        }
        if (pItem["arc_fechaUltMov"] != DBNull.Value)
        {
            obj.arc_fechaUltMov = Convert.ToDateTime(pItem["arc_fechaUltMov"]);
        }
        if (pItem["arc_estado"] != DBNull.Value)
        {
            obj.arc_estado = Convert.ToInt32(pItem["arc_estado"]);
            obj.arc_estadoToString = SitioBase.capaDatos.capaSeguridad.obtenerStringEstado(obj.arc_estado);
        }
        if (pItem["arc_accion"] != DBNull.Value)
        {
            obj.arc_accion = Convert.ToInt32(pItem["arc_accion"]);
        }
        return obj;
    }
    public static List<cArchivo> RecuperarTodosArchivos(int pArc_codRelacion, string pArc_galeria, string pFiltro)
    {
        //if (VerificarPermisos(CredencialAutenticacion))
        //{
        List<cArchivo> lista = new List<cArchivo>();
        DataSet dsResultado = capaRecurso.GestiónArchivo(null, pArc_codRelacion, pArc_galeria, null, null, null, null, null, null, null, null, null, pFiltro, Constantes.cSQL_SELECT);
        if (dsResultado != null)
        {
            foreach (DataRow item in dsResultado.Tables["Archivo"].Rows)
            {
                lista.Add(ConvertToArchivo(item));
            }
        }
        return lista;
        //}
        //else
        //{
        //    return null;
        //}
    }

    public static void CambiarPosicionArchivoPorId(int pArc_codRecurso, int pArc_codRelacion, string pArc_galeria, bool isSubir, int pIdUsuarioSession)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            string accion = isSubir ? Constantes.cSQL_SUBIR : Constantes.cSQL_BAJAR;
            DataSet dsResultado = capaRecurso.GestiónArchivo(pArc_codRecurso, pArc_codRelacion, pArc_galeria, null, null, null, null, null, null, pIdUsuarioSession, null, Constantes.cACCION_CAMBIOORDEN, null, accion);
        }
    }
    public static void CambiarEstadoArchivoPorId(int pArc_codRecurso, int pIdUsuarioSession)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            cArchivo obj = RecuperarArchivoPorId(pArc_codRecurso);
            if (obj != null)
            {
                int estado = obj.arc_estado == 2 ? 3 : 2;
                DataSet dsResultado = capaRecurso.GestiónArchivo(pArc_codRecurso, null, null, null, null, null, null, null, null, pIdUsuarioSession, estado, Constantes.cACCION_CAMBIOESTADO, null, Constantes.cSQL_ESTADO);
            }
        }
    }
    public static void EliminarArchivoPorId(int pArc_codRecurso)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataSet dsResultado = capaRecurso.GestiónArchivo(pArc_codRecurso, null, null, null, null, null, null, null, null, null, null, null, null, Constantes.cSQL_DELETE);
        }
    }
    public static cArchivo RecuperarArchivoPorId(int pArc_codRecurso)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            cArchivo obj = null;
            DataSet dsResultado = capaRecurso.GestiónArchivo(pArc_codRecurso, null, null, null, null, null, null, null, null, null, null, null, null, Constantes.cSQL_SELECT);
            if (dsResultado != null)
            {
                foreach (DataRow item in dsResultado.Tables["Archivo"].Rows)
                {
                    obj = ConvertToArchivo(item);
                    break;
                }
            }
            return obj;
        }
        else
        {
            return null;
        }
    }

    //// Noticia
    public static int InsertarActualizarNoticia(int not_codNoticia, DateTime not_fechaDesde, DateTime? not_fechaHasta, string not_titulo, string not_bajada, string not_descripcion, string not_destacado, int not_codTipoNoticia, int not_codUsuarioUltMov)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            string accion = not_codNoticia == 0 ? Constantes.cSQL_INSERT : Constantes.cSQL_UPDATE;
            int codigoAccion = not_codNoticia == 0 ? Constantes.cACCION_ALTA : Constantes.cACCION_MODIFICACION;
            int? codigoEstado = not_codNoticia == 0 ? Constantes.cESTADO_ACTIVO : (int?)null;
            bool? isPublicar = not_codNoticia == 0 ? false : (bool?)null;
            DataSet dsResultado = capaNoticia.GestiónNoticia(not_codNoticia, not_fechaDesde, not_fechaHasta, not_titulo, not_bajada, not_descripcion, not_destacado, not_codTipoNoticia, isPublicar, codigoEstado, codigoAccion, not_codUsuarioUltMov, null, accion);
            int resultado = -1;
            if (not_codNoticia == 0)
            {
                if (dsResultado != null)
                {
                    if (dsResultado.Tables["Noticia"].Rows[0]["not_codNoticia"] != DBNull.Value)
                    {
                        resultado = Convert.ToInt32(dsResultado.Tables["Noticia"].Rows[0]["not_codNoticia"]);
                    }
                }
            }
            else
            {
                resultado = not_codNoticia;
            }
            return resultado;
        }
        else
        {
            return -100;
        }
    }
    private static cNoticia ConvertToNoticia(DataRow pItem)
    {
        cNoticia obj = new cNoticia();
        if (pItem["not_codNoticia"] != DBNull.Value)
        {
            obj.not_codNoticia = Convert.ToInt32(pItem["not_codNoticia"]);
        }
        if (pItem["not_fechaDesde"] != DBNull.Value)
        {
            obj.not_fechaDesde = Convert.ToDateTime(pItem["not_fechaDesde"]);
            obj.not_fechaDesdeReducido = Convert.ToDateTime(pItem["not_fechaDesde"]).ToShortDateString();
        }
        if (pItem["not_fechaHasta"] != DBNull.Value)
        {
            obj.not_fechaHasta = Convert.ToDateTime(pItem["not_fechaHasta"]);
            obj.not_fechaHastaReducido = Convert.ToDateTime(pItem["not_fechaHasta"]).ToShortDateString();
        }
        if (pItem["not_titulo"] != DBNull.Value)
        {
            obj.not_titulo = pItem["not_titulo"].ToString();
        }
        if (pItem["not_bajada"] != DBNull.Value)
        {
            obj.not_bajada = pItem["not_bajada"].ToString();
        }
        if (pItem["not_descripcion"] != DBNull.Value)
        {
            obj.not_descripcion = pItem["not_descripcion"].ToString();
        }
        if (pItem["not_destacado"] != DBNull.Value)
        {
            obj.not_destacado = pItem["not_destacado"].ToString();
        }
        if (pItem["not_codTipoNoticia"] != DBNull.Value)
        {
            obj.not_codTipoNoticia = Convert.ToInt32(pItem["not_codTipoNoticia"]);
        }
        if (pItem["not_isPublicar"] != DBNull.Value)
        {
            obj.not_isPublicar = Convert.ToBoolean(pItem["not_isPublicar"]);
            obj.not_isPublicarToString = (bool)obj.not_isPublicar ? "Publicar" : "No publicar";
        }
        if (pItem["not_estado"] != DBNull.Value)
        {
            obj.not_estado = Convert.ToInt32(pItem["not_estado"]);
            obj.not_estadoToString = SitioBase.capaDatos.capaSeguridad.obtenerStringEstado(obj.not_estado);
        }
        if (pItem["not_codUsuarioUltMov"] != DBNull.Value)
        {
            obj.not_codUsuarioUltMov = Convert.ToInt32(pItem["not_codUsuarioUltMov"]);
        }
        if (pItem["not_accion"] != DBNull.Value)
        {
            obj.not_accion = Convert.ToInt32(pItem["not_accion"]);
        }
        if (pItem["not_fechaUltMov"] != DBNull.Value)
        {
            obj.not_fechaUltMov = Convert.ToDateTime(pItem["not_fechaUltMov"]);
        }
        return obj;
    }
    public static List<cNoticia> RecuperarTodasNoticias(string pFiltro, int ptipo)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            List<cNoticia> lista = new List<cNoticia>();
            DataSet dsResultado = capaNoticia.GestiónNoticia(null, null, null, null, null, null, null, ptipo, null, null, null, null, pFiltro, Constantes.cSQL_SELECT);
            if (dsResultado != null)
            {
                foreach (DataRow item in dsResultado.Tables["Noticia"].Rows)
                {
                    lista.Add(ConvertToNoticia(item));
                }
            }
            return lista;
        }
        else
        {
            return null;
        }
    }
    public static cNoticia RecuperarNoticiaPorId(int pIdNoticia)
    {
        //if (VerificarPermisos(CredencialAutenticacion))
        //{
        cNoticia obj = null;
        DataSet dsResultado = capaNoticia.GestiónNoticia(pIdNoticia, null, null, null, null, null, null, null, null, null, null, null, null, Constantes.cSQL_SELECT);
        if (dsResultado != null)
        {
            foreach (DataRow item in dsResultado.Tables["Noticia"].Rows)
            {
                obj = ConvertToNoticia(item);
                break;
            }
        }
        return obj;
        //}
        //else
        //{s
        //    return null;
        //}
    }
    public static void EliminarNoticiaPorId(int pIdNoticia)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            capaNoticia.GestiónNoticia(pIdNoticia, null, null, null, null, null, null, null, null, null, null, null, null, Constantes.cSQL_DELETE);
        }
    }
    public static void CambiarEstadoNoticiaPorId(int pIdNoticia, int pIdEstado, int pIdUsuarioEnSession)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            capaNoticia.GestiónNoticia(pIdNoticia, null, null, null, null, null, null, null, null, pIdEstado, Constantes.cACCION_CAMBIOESTADO, pIdUsuarioEnSession, null, Constantes.cSQL_ESTADO);
        }
    }
    public static void PublicarNoticiaPorId(int pIdNoticia, bool pIsPublicar, int pIdUsuarioEnSession)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            capaNoticia.GestiónNoticia(pIdNoticia, null, null, null, null, null, null, null, pIsPublicar, null, Constantes.cACCION_ISPUBLICAR, pIdUsuarioEnSession, null, Constantes.cSQL_PUBLICAR);
        }
    }
    public static List<cNoticia> RecuperarNoticias(int pIdNoticia, int tipo, string pFiltro)
    {
        List<cNoticia> lista = new List<cNoticia>();
        DataSet dsResultado = capaNoticia.MostrarNoticias(pIdNoticia, tipo, pFiltro);
        if (dsResultado != null)
        {
            foreach (DataRow item in dsResultado.Tables["Noticia"].Rows)
            {
                lista.Add(ConvertToNoticia(item));
            }
        }
        return lista;
    }
    //// Home
    public static int InsertarActualizarHome(int hom_codNoticia1, int hom_codNoticia2, string accion)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataSet dsResultado = capaNoticia.MostrarNoticiaHome(hom_codNoticia1, hom_codNoticia2, 3, "", accion);
            int resultado = -1;
            return resultado;
        }
        else
        {
            return -100;
        }
    }
    public static int InsertarActualizarHome(int hom_codNoticia1, int hom_codNoticia2, int hom_codNoticia3, string accion)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataSet dsResultado = capaNoticia.MostrarNoticiaHome(hom_codNoticia1, hom_codNoticia2, hom_codNoticia3, "", accion);
            int resultado = -1;
            return resultado;
        }
        else
        {
            return -100;
        }
    }
    public static List<cNoticia> RecuperarNoticiaHome(string pFiltro)
    {
        List<cNoticia> lista = new List<cNoticia>();
        DataSet dsResultado = capaNoticia.MostrarNoticiaHome(null, null, null, pFiltro, Constantes.cSQL_SELECT);
        if (dsResultado != null)
        {
            foreach (DataRow item in dsResultado.Tables["Home"].Rows)
            {
                lista.Add(ConvertToHome(item));
            }
        }
        return lista;
    }
    private static cNoticia ConvertToHome(DataRow pItem)
    {
        cNoticia obj = new cNoticia();
        if (pItem["hom_codNoticia1"] != DBNull.Value)
        {
            obj.hom_codNoticia1 = Convert.ToInt32(pItem["hom_codNoticia1"]);
        }
        if (pItem["hom_codNoticia2"] != DBNull.Value)
        {
            obj.hom_codNoticia2 = Convert.ToInt32(pItem["hom_codNoticia2"]);
        }
        if (pItem.Table.Columns.Contains("hom_codNoticia3"))
        {
            if (pItem["hom_codNoticia3"] != DBNull.Value)
            {
                obj.hom_codNoticia3 = Convert.ToInt32(pItem["hom_codNoticia3"]);
            }
        }
        if (pItem["not_codNoticia"] != DBNull.Value)
        {
            obj.not_codNoticia = Convert.ToInt32(pItem["not_codNoticia"]);
        }
        if (pItem["not_titulo"] != DBNull.Value)
        {
            obj.not_titulo = pItem["not_titulo"].ToString();
        }
        if (pItem["not_bajada"] != DBNull.Value)
        {
            obj.not_bajada = pItem["not_bajada"].ToString();
        }
        if (pItem.Table.Columns.Contains("not_isPublicar"))
        {
            if (pItem["not_isPublicar"] != DBNull.Value)
            {
                obj.not_isPublicar = Convert.ToBoolean(pItem["not_isPublicar"]);
                obj.not_isPublicarToString = (bool)obj.not_isPublicar ? "Publicar" : "No publicar"; ;
            }
        }
        return obj;
    }
    //// LinksInteres
    public static int InsertarActualizarLinks(int lnk_codLinks, string lnk_titulo, string lnk_bajada, string lnk_descripcion, string lnk_web, string lnk_origen, int lnk_codTipo, int lnk_codUsuarioUltMov)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            string accion = lnk_codLinks == 0 ? Constantes.cSQL_INSERT : Constantes.cSQL_UPDATE;
            int codigoAccion = lnk_codLinks == 0 ? Constantes.cACCION_ALTA : Constantes.cACCION_MODIFICACION;
            int? codigoEstado = lnk_codLinks == 0 ? Constantes.cESTADO_ACTIVO : (int?)null;
            bool? isPublicar = lnk_codLinks == 0 ? false : (bool?)null;
            DataSet dsResultado = capaNoticia.GestiónLinks(lnk_codLinks, lnk_titulo, lnk_bajada, lnk_descripcion, lnk_web, lnk_origen, lnk_codTipo, isPublicar, codigoEstado, codigoAccion, lnk_codUsuarioUltMov, null, accion);
            int resultado = -1;
            if (lnk_codLinks == 0)
            {
                if (dsResultado != null)
                {
                    if (dsResultado.Tables["Links"].Rows[0]["lnk_codLinks"] != DBNull.Value)
                    {
                        resultado = Convert.ToInt32(dsResultado.Tables["Links"].Rows[0]["lnk_codLinks"]);
                    }
                }
            }
            else
            {
                resultado = lnk_codLinks;
            }
            return resultado;
        }
        else
        {
            return -100;
        }
    }
    private static cNoticia ConvertToLinks(DataRow pItem)
    {
        cNoticia obj = new cNoticia();
        if (pItem["lnk_codLinks"] != DBNull.Value)
        {
            obj.lnk_codLinks = Convert.ToInt32(pItem["lnk_codLinks"]);
        }

        if (pItem["lnk_titulo"] != DBNull.Value)
        {
            obj.lnk_titulo = pItem["lnk_titulo"].ToString();
        }
        if (pItem["lnk_bajada"] != DBNull.Value)
        {
            obj.lnk_bajada = pItem["lnk_bajada"].ToString();
        }
        if (pItem["lnk_descripcion"] != DBNull.Value)
        {
            obj.lnk_descripcion = pItem["lnk_descripcion"].ToString();
        }
        if (pItem["lnk_web"] != DBNull.Value)
        {
            obj.lnk_web = pItem["lnk_web"].ToString();
        }
        if (pItem["lnk_origen"] != DBNull.Value)
        {
            obj.lnk_origen = pItem["lnk_origen"].ToString();
        }
        if (pItem["lnk_codTipo"] != DBNull.Value)
        {
            obj.lnk_codTipo = Convert.ToInt32(pItem["lnk_codTipo"]);
        }
        if (pItem["lnk_isPublicar"] != DBNull.Value)
        {
            obj.lnk_isPublicar = Convert.ToBoolean(pItem["lnk_isPublicar"]);
            obj.lnk_isPublicarToString = (bool)obj.lnk_isPublicar ? "Publicar" : "No publicar";
        }
        if (pItem["lnk_estado"] != DBNull.Value)
        {
            obj.lnk_estado = Convert.ToInt32(pItem["lnk_estado"]);
            obj.lnk_estadoToString = SitioBase.capaDatos.capaSeguridad.obtenerStringEstado(obj.lnk_estado);
        }
        if (pItem["lnk_codUsuarioUltMov"] != DBNull.Value)
        {
            obj.lnk_codUsuarioUltMov = Convert.ToInt32(pItem["lnk_codUsuarioUltMov"]);
        }
        if (pItem["lnk_accion"] != DBNull.Value)
        {
            obj.lnk_accion = Convert.ToInt32(pItem["lnk_accion"]);
        }
        if (pItem["lnk_fechaUltMov"] != DBNull.Value)
        {
            obj.lnk_fechaUltMov = Convert.ToDateTime(pItem["lnk_fechaUltMov"]);
        }
        return obj;
    }
    public static List<cNoticia> RecuperarTodosLinks(string pFiltro, int ptipo)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            List<cNoticia> lista = new List<cNoticia>();
            DataSet dsResultado = capaNoticia.GestiónLinks(null, null, null, null, null, null, ptipo, null, null, null, null, pFiltro, Constantes.cSQL_SELECT);
            if (dsResultado != null)
            {
                foreach (DataRow item in dsResultado.Tables["Links"].Rows)
                {
                    lista.Add(ConvertToLinks(item));
                }
            }
            return lista;
        }
        else
        {
            return null;
        }
    }
    public static cNoticia RecuperarLinksPorId(int pIdLinks)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            cNoticia obj = null;
            DataSet dsResultado = capaNoticia.GestiónLinks(pIdLinks, null, null, null, null, null, null, null, null, null, null, null, Constantes.cSQL_SELECT);
            if (dsResultado != null)
            {
                foreach (DataRow item in dsResultado.Tables["Links"].Rows)
                {
                    obj = ConvertToLinks(item);
                    break;
                }
            }
            return obj;
        }
        else
        {
            return null;
        }
    }
    public static void CambiarEstadoLinksPorId(int pIdLinks, int pIdEstado, int pIdUsuarioEnSession)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            capaNoticia.GestiónLinks(pIdLinks, null, null, null, null, null, null, null, pIdEstado, Constantes.cACCION_CAMBIOESTADO, pIdUsuarioEnSession, null, Constantes.cSQL_ESTADO);
        }
    }
    public static void PublicarLinksPorId(int pIdLinks, bool pIsPublicar, int pIdUsuarioEnSession)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            capaNoticia.GestiónLinks(pIdLinks, null, null, null, null, null, null, pIsPublicar, null, Constantes.cACCION_ISPUBLICAR, pIdUsuarioEnSession, null, Constantes.cSQL_PUBLICAR);
        }
    }
    public static void EliminarLinksPorId(int pIdLinks, int pIdUsuarioEnSession)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            capaNoticia.GestiónLinks(pIdLinks, null, null, null, null, null, null, null, null, Constantes.cACCION_ELIMINAR, pIdUsuarioEnSession, null, Constantes.cSQL_DELETE);
        }
    }
    public static List<cNoticia> RecuperarLinks(int pIdLinks, int tipo, string pFiltro)
    {
        //if (VerificarPermisos(CredencialAutenticacion))
        //{
        List<cNoticia> lista = new List<cNoticia>();
        DataSet dsResultado = capaNoticia.MostrarLinks(pIdLinks, tipo, pFiltro);
        if (dsResultado != null)
        {
            foreach (DataRow item in dsResultado.Tables["Links"].Rows)
            {
                lista.Add(ConvertToLinks(item));
            }
        }
        return lista;
        //}
        //else
        //{
        //    return null;
        //}
    }


    //// Contacto
    public static int InsertarActualizarContacto(int con_codContacto, DateTime? con_fecha, string con_nombre, string con_mail, string con_asunto, string con_empresa, string con_comentario, string con_leido, int con_codUsuarioUltMov)
    {
        //if (VerificarPermisos(CredencialAutenticacion))
        // {
        string accion = con_codContacto == 0 ? Constantes.cSQL_INSERT : Constantes.cSQL_UPDATE;
        int codigoAccion = con_codContacto == 0 ? Constantes.cACCION_ALTA : Constantes.cACCION_MODIFICACION;
        DataSet dsResultado = capaContactos.GestiónContacto(con_codContacto, con_fecha, con_nombre, con_empresa, con_mail, con_asunto, con_comentario, con_leido, null, accion);
        int resultado = -1;
        if (con_codContacto == 0)
        {
            if (dsResultado != null)
            {
                if (dsResultado.Tables["Contacto"].Rows[0]["con_codContacto"] != DBNull.Value)
                {
                    resultado = Convert.ToInt32(dsResultado.Tables["Contacto"].Rows[0]["con_codContacto"]);
                }
            }
        }
        else
        {
            resultado = con_codContacto;
        }
        return resultado;
        //}
        //else
        //{
        //    return -100;
        //}
    }
    private static cContacto ConvertToContacto(DataRow pItem)
    {
        cContacto obj = new cContacto();
        if (pItem["con_codContacto"] != DBNull.Value)
        {
            obj.con_codContacto = Convert.ToInt32(pItem["con_codContacto"]);
        }

        if (pItem["con_fecha"] != DBNull.Value)
        {
            obj.con_fecha = pItem["con_fecha"].ToString();
        }
        if (pItem["con_nombre"] != DBNull.Value)
        {
            obj.con_nombre = pItem["con_nombre"].ToString();
        }
        if (pItem["con_empresa"] != DBNull.Value)
        {
            obj.con_empresa = pItem["con_empresa"].ToString();
        }
        if (pItem["con_asunto"] != DBNull.Value)
        {
            obj.con_asunto = pItem["con_asunto"].ToString();
        }
        if (pItem["con_mail"] != DBNull.Value)
        {
            obj.con_mail = pItem["con_mail"].ToString();
        }
        if (pItem["con_comentario"] != DBNull.Value)
        {
            obj.con_comentario = pItem["con_comentario"].ToString();
        }
        if (pItem["con_leido"] != DBNull.Value)
        {
            obj.con_leido = pItem["con_leido"].ToString();
        }

        return obj;
    }
    public static List<cContacto> RecuperarTodosContactos(string pFiltro)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            List<cContacto> lista = new List<cContacto>();
            DataSet dsResultado = capaContactos.GestiónContacto(null, null, null, null, null, null, null, null, pFiltro, Constantes.cSQL_SELECT);
            if (dsResultado != null)
            {
                foreach (DataRow item in dsResultado.Tables["Contacto"].Rows)
                {
                    lista.Add(ConvertToContacto(item));
                }
            }
            return lista;
        }
        else
        {
            return null;
        }
    }
    public static cContacto RecuperarContactoPorId(int pIdContacto)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            cContacto obj = null;
            DataSet dsResultado = capaContactos.GestiónContacto(pIdContacto, null, null, null, null, null, null, null, null, Constantes.cSQL_SELECT);
            if (dsResultado != null)
            {
                foreach (DataRow item in dsResultado.Tables["Contacto"].Rows)
                {
                    obj = ConvertToContacto(item);
                    break;
                }
            }
            return obj;
        }
        else
        {
            return null;
        }
    }

    public static List<cClientes> RecuperarTodasProvincias(string pFiltro)
    {
        //if (VerificarPermisos(CredencialAutenticacion))
        //{
        List<cClientes> lista = new List<cClientes>();
        DataSet dsResultado = capaClientes.MostrarProvincia(pFiltro);
        if (dsResultado != null)
        {
            foreach (DataRow item in dsResultado.Tables["Clientes"].Rows)
            {
                lista.Add(ConvertToProvincia(item));
            }
        }
        return lista;
        //}
        //else
        //{
        //    return null;
        //}
    }

    private static cClientes ConvertToProvincia(DataRow pItem)
    {
        cClientes obj = new cClientes();
        if (pItem["cli_codprov"] != DBNull.Value)
        {
            obj.cli_codprov = pItem["cli_codprov"].ToString();
        }
        return obj;
    }

    public static cClientes RecuperarClienteAdministradorPorIdUsuarios(int pIdUsuario)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            cClientes obj = null;
            DataTable dtResultado = capaClientes.RecuperarClienteAdministradorPorIdUsuarios(pIdUsuario);
            if (dtResultado != null)
            {
                foreach (DataRow item in dtResultado.Rows)
                {
                    obj = ConvertToCliente(item);
                }
            }
            return obj;
        }
        else
        {
            return null;
        }
    }
    public static List<cClientes> RecuperarClientes(int pIdCliente, string tipo, string pFiltro)
    {
        //if (VerificarPermisos(CredencialAutenticacion))
        //{
        List<cClientes> lista = new List<cClientes>();
        DataSet dsResultado = capaClientes.MostrarClientes(pIdCliente, tipo, pFiltro);
        if (dsResultado != null)
        {
            foreach (DataRow item in dsResultado.Tables["Clientes"].Rows)
            {
                lista.Add(ConvertToCliente(item));
            }
        }
        return lista;
        //}
        //else
        //{
        //    return null;
        //}
    }

    public static List<cUsuario> RecuperarUsuariosDeCliente(int usu_codRol, int usu_codCliente, string filtro)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            List<cUsuario> lista = new List<cUsuario>();
            DataSet dsResultado = capaClientes.RecuperarUsuariosDeCliente(usu_codRol, usu_codCliente, filtro);
            if (dsResultado != null)
            {
                foreach (DataRow item in dsResultado.Tables["UsuariosCliente"].Rows)
                {
                    lista.Add(SitioBase.clases.Seguridad.ConvertToUsuario(item));
                }
            }
            return lista;
        }
        else
        {
            return null;
        }
    }


    public static void SincronizarClientes()
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            capaSincronizador.SincronizarClientes();
        }
    }
    public static void SincronizarClientes_Todos()
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            capaSincronizador.SincronizarClientes_Todos();
        }
    }
    public static void SincronizarProductos()
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            capaSincronizador.SincronizarProductos();
        }
    }
    public static void SincronizarProductos_Todos()
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            capaSincronizador.SincronizarProductos_Todos();
        }
    }
    public static void SincronizarOfertas()
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            capaSincronizador.SincronizarOfertas();
        }
    }
    public static void SincronizarOfertas_Todos()
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            capaSincronizador.SincronizarOfertas_Todos();
        }
    }
    public static void SincronizarPrecios()
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            capaSincronizador.SincronizarPrecios();
        }
    }
    public static void SincronizarStocks()
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            capaSincronizador.SincronizarStocks();
        }
    }
    public static void SincronizarVales_Todos()
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            capaSincronizador.SincronizarVales_Todos();
        }
    }
    public static void SincronizarTransfers()
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            capaSincronizador.SincronizarTransfers();
        }
    }
    public static void SincronizarTransfers_Todos()
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            capaSincronizador.SincronizarTransfers_Todos();
        }
    }
    public static void SincronizarTransfers_Clientes_Todos()
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            capaSincronizador.SincronizarTransfers_Clientes_Todos();
        }
    }
    private static cClientes ConvertToCliente(DataRow pItem)
    {
        cClientes obj = new cClientes();
        if (pItem["cli_codigo"] != DBNull.Value)
        {
            obj.cli_codigo = Convert.ToInt32(pItem["cli_codigo"]);
        }
        if (pItem["cli_nombre"] != DBNull.Value)
        {
            obj.cli_nombre = pItem["cli_nombre"].ToString();
        }
        if (pItem["cli_dirección"] != DBNull.Value)
        {
            obj.cli_dirección = pItem["cli_dirección"].ToString();
        }
        if (pItem["cli_Telefono"] != DBNull.Value)
        {
            obj.cli_telefono = pItem["cli_Telefono"].ToString();
        }
        if (pItem["cli_localidad"] != DBNull.Value)
        {
            obj.cli_localidad = pItem["cli_localidad"].ToString();
        }
        if (pItem["cli_codprov"] != DBNull.Value)
        {
            obj.cli_codprov = pItem["cli_codprov"].ToString();
        }
        if (pItem["cli_email"] != DBNull.Value)
        {
            obj.cli_email = pItem["cli_email"].ToString();
        }
        if (pItem["cli_paginaweb"] != DBNull.Value)
        {
            obj.cli_paginaweb = pItem["cli_paginaweb"].ToString();
        }
        if (pItem["cli_codsuc"] != DBNull.Value)
        {
            obj.cli_codsuc = pItem["cli_codsuc"].ToString();
        }
        if (pItem["cli_pordesespmed"] != DBNull.Value)
        {
            obj.cli_pordesespmed = Convert.ToDecimal(pItem["cli_pordesespmed"]);
        }
        if (pItem["cli_pordesbetmed"] != DBNull.Value)
        {
            obj.cli_pordesbetmed = Convert.ToDecimal(pItem["cli_pordesbetmed"]);
        }
        if (pItem["cli_pordesnetos"] != DBNull.Value)
        {
            obj.cli_pordesnetos = Convert.ToDecimal(pItem["cli_pordesnetos"]);
        }
        if (pItem["cli_pordesfinperfcyo"] != DBNull.Value)
        {
            obj.cli_pordesfinperfcyo = Convert.ToDecimal(pItem["cli_pordesfinperfcyo"]);
        }
        if (pItem["cli_pordescomperfcyo"] != DBNull.Value)
        {
            obj.cli_pordescomperfcyo = Convert.ToDecimal(pItem["cli_pordescomperfcyo"]);
        }
        if (pItem["cli_deswebespmed"] != DBNull.Value)
        {
            obj.cli_deswebespmed = Convert.ToBoolean(pItem["cli_deswebespmed"]);
        }
        if (pItem["cli_deswebnetmed"] != DBNull.Value)
        {
            obj.cli_deswebnetmed = Convert.ToBoolean(pItem["cli_deswebnetmed"]);
        }
        if (pItem["cli_deswebnetacc"] != DBNull.Value)
        {
            obj.cli_deswebnetacc = Convert.ToBoolean(pItem["cli_deswebnetacc"]);
        }
        if (pItem["cli_deswebnetperpropio"] != DBNull.Value)
        {
            obj.cli_deswebnetperpropio = Convert.ToBoolean(pItem["cli_deswebnetperpropio"]);
        }
        if (pItem["cli_deswebnetpercyo"] != DBNull.Value)
        {
            obj.cli_deswebnetpercyo = Convert.ToBoolean(pItem["cli_deswebnetpercyo"]);
        }
        if (pItem["cli_destransfer"] != DBNull.Value)
        {
            obj.cli_destransfer = Convert.ToDecimal(pItem["cli_destransfer"]);
        }
        if (pItem["cli_login"] != DBNull.Value)
        {
            obj.cli_login = pItem["cli_login"].ToString();
        }
        if (pItem["cli_codtpoenv"] != DBNull.Value)
        {
            obj.cli_codtpoenv = pItem["cli_codtpoenv"].ToString();
        }
        if (pItem["cli_codrep"] != DBNull.Value)
        {
            obj.cli_codrep = pItem["cli_codrep"].ToString();
        }
        if (pItem.Table.Columns.Contains("cli_isGLN"))
        {
            if (pItem["cli_isGLN"] != DBNull.Value)
            {
                obj.cli_isGLN = Convert.ToBoolean(pItem["cli_isGLN"]);
            }
        }
        if (pItem.Table.Columns.Contains("cli_tomaOfertas"))
        {
            if (pItem["cli_tomaOfertas"] != DBNull.Value)
            {
                obj.cli_tomaOfertas = Convert.ToBoolean(pItem["cli_tomaOfertas"]);
            }
        }
        if (pItem.Table.Columns.Contains("cli_tomaPerfumeria"))
        {
            if (pItem["cli_tomaPerfumeria"] != DBNull.Value)
            {
                obj.cli_tomaPerfumeria = Convert.ToBoolean(pItem["cli_tomaPerfumeria"]);
            }
        }
        if (pItem.Table.Columns.Contains("cli_tomaTransfers"))
        {
            if (pItem["cli_tomaTransfers"] != DBNull.Value)
            {
                obj.cli_tomaTransfers = Convert.ToBoolean(pItem["cli_tomaTransfers"]);
            }
        }
        if (pItem.Table.Columns.Contains("cli_password"))
        {
            if (pItem["cli_password"] != DBNull.Value)
            {
                obj.cli_password = Convert.ToString(pItem["cli_password"]);
            }
        }
        if (pItem.Table.Columns.Contains("cli_estado"))
        {
            if (pItem["cli_estado"] != DBNull.Value)
            {
                obj.cli_estado = Convert.ToString(pItem["cli_estado"]);
            }
        }
        if (pItem.Table.Columns.Contains("cli_tipo"))
        {
            if (pItem["cli_tipo"] != DBNull.Value)
            {
                obj.cli_tipo = Convert.ToString(pItem["cli_tipo"]);
            }
        }
        if (pItem.Table.Columns.Contains("cli_mostrardireccion"))
        {
            if (pItem["cli_mostrardireccion"] != DBNull.Value)
            {
                obj.cli_mostrardireccion = Convert.ToBoolean(pItem["cli_mostrardireccion"]);
            }
        }
        if (pItem.Table.Columns.Contains("cli_mostrarweb"))
        {
            if (pItem["cli_mostrarweb"] != DBNull.Value)
            {
                obj.cli_mostrarweb = Convert.ToBoolean(pItem["cli_mostrarweb"]);
            }
        }
        if (pItem.Table.Columns.Contains("cli_mostrartelefono"))
        {
            if (pItem["cli_mostrartelefono"] != DBNull.Value)
            {
                obj.cli_mostrartelefono = Convert.ToBoolean(pItem["cli_mostrartelefono"]);
            }
        }
        if (pItem.Table.Columns.Contains("cli_mostraremail"))
        {
            if (pItem["cli_mostraremail"] != DBNull.Value)
            {
                obj.cli_mostraremail = Convert.ToBoolean(pItem["cli_mostraremail"]);
            }
        }
        if (pItem.Table.Columns.Contains("cli_IdSucursalAlternativa") && pItem["cli_IdSucursalAlternativa"] != DBNull.Value)
            obj.cli_IdSucursalAlternativa = Convert.ToString(pItem["cli_IdSucursalAlternativa"]);
        return obj;
    }
    private static cProductos ConvertToProductos(DataRow pItem)
    {
        cProductos obj = new cProductos();
        obj.pro_codigo = pItem["pro_codigo"].ToString();
        if (pItem["pro_nombre"] != DBNull.Value)
        {
            obj.pro_nombre = pItem["pro_nombre"].ToString();
        }
        if (pItem["pro_precio"] != DBNull.Value)
        {
            obj.pro_precio = Convert.ToDecimal(pItem["pro_precio"]);
        }
        if (pItem["pro_preciofarmacia"] != DBNull.Value)
        {
            obj.pro_preciofarmacia = Convert.ToDecimal(pItem["pro_preciofarmacia"]);
        }
        if (pItem["pro_ofeunidades"] != DBNull.Value)
        {
            obj.pro_ofeunidades = Convert.ToInt32(pItem["pro_ofeunidades"]);
        }
        if (pItem["pro_ofeporcentaje"] != DBNull.Value)
        {
            obj.pro_ofeporcentaje = Convert.ToDecimal(pItem["pro_ofeporcentaje"]);
        }
        if (pItem["pro_neto"] != DBNull.Value)
        {
            obj.pro_neto = Convert.ToBoolean(pItem["pro_neto"]);
        }
        if (pItem["pro_codtpopro"] != DBNull.Value)
        {
            obj.pro_codtpopro = pItem["pro_codtpopro"].ToString().ToUpper();
        }
        if (pItem["pro_descuentoweb"] != DBNull.Value)
        {
            obj.pro_descuentoweb = Convert.ToDecimal(pItem["pro_descuentoweb"]);
        }
        if (pItem["pro_laboratorio"] != DBNull.Value)
        {
            obj.pro_laboratorio = pItem["pro_laboratorio"].ToString();
        }
        if (pItem["pro_monodroga"] != DBNull.Value)
        {
            obj.pro_monodroga = pItem["pro_monodroga"].ToString();
        }
        if (pItem["pro_codigobarra"] != DBNull.Value)
        {
            obj.pro_codigobarra = pItem["pro_codigobarra"].ToString();
        }
        if (pItem["pro_codigoalfabeta"] != DBNull.Value)
        {
            obj.pro_codigoalfabeta = pItem["pro_codigoalfabeta"].ToString();
        }
        if (pItem["pro_isTrazable"] != DBNull.Value)
        {
            obj.pro_isTrazable = Convert.ToBoolean(pItem["pro_isTrazable"]);
        }
        if (pItem["pro_isCadenaFrio"] != DBNull.Value)
        {
            obj.pro_isCadenaFrio = Convert.ToBoolean(pItem["pro_isCadenaFrio"]);
        }
        if (pItem.Table.Columns.Contains("pro_acuerdo"))
        {
            if (pItem["pro_acuerdo"] != DBNull.Value)
            {
                obj.pro_acuerdo = Convert.ToInt32(pItem["pro_acuerdo"]);
            }
        }

        return obj;
    }

    public static cProductos RecuperarProductoPorNombre(string pNombreProducto)
    {
        cProductos resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataTable tablaProductos = capaProductos.RecuperarProductoPorNombre(pNombreProducto);
            if (tablaProductos != null)
            {
                if (tablaProductos.Rows.Count > 0)
                {
                    resultado = ConvertToProductos(tablaProductos.Rows[0]);
                }
            }
        }
        return resultado;
    }
    public static List<cProductos> RecuperarTodosProductos()
    {
        List<cProductos> resultado = null;

        if (VerificarPermisos(CredencialAutenticacion))
        {
            resultado = new List<cProductos>();
            DataTable tablaProductos = capaProductos.RecuperarTodosProductos();
            if (tablaProductos != null)
            {
                for (int i = 0; i < tablaProductos.Rows.Count; i++)
                {
                    resultado.Add(ConvertToProductos(tablaProductos.Rows[i]));
                }
            }
        }
        return resultado;
    }
    public static cProductos RecuperadorProductoPorCodigo(string pCodigoProducto)
    {
        cProductos resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataTable tablaProductos = capaProductos.RecuperadorProductoPorCodigo(pCodigoProducto);
            if (tablaProductos != null)
            {
                if (tablaProductos.Rows.Count > 0)
                {
                    resultado = ConvertToProductos(tablaProductos.Rows[0]);
                }
            }
        }
        return resultado;
    }
    //private static cProductos ConvertToProductosAUX(DataRow pItem)
    //{
    //    cProductosBuscador obj = new cProductosBuscador();
    //    obj.pro_codigo = pItem["pro_codigo"].ToString();
    //    if (pItem["pro_nombre"] != DBNull.Value)
    //    {
    //        obj.pro_nombre = pItem["pro_nombre"].ToString();
    //    }
    //    if (pItem["pro_precio"] != DBNull.Value)
    //    {
    //        obj.pro_precio = Convert.ToDecimal(pItem["pro_precio"]);
    //    }
    //    if (pItem["pro_preciofarmacia"] != DBNull.Value)
    //    {
    //        obj.pro_preciofarmacia = Convert.ToDecimal(pItem["pro_preciofarmacia"]);
    //    }
    //    if (pItem["pro_ofeunidades"] != DBNull.Value)
    //    {
    //        obj.pro_ofeunidades = Convert.ToInt32(pItem["pro_ofeunidades"]);
    //    }
    //    if (pItem["pro_ofeporcentaje"] != DBNull.Value)
    //    {
    //        obj.pro_ofeporcentaje = Convert.ToDecimal(pItem["pro_ofeporcentaje"]);
    //    }
    //    if (pItem["pro_neto"] != DBNull.Value)
    //    {
    //        obj.pro_neto = Convert.ToBoolean(pItem["pro_neto"]);
    //    }
    //    if (pItem["pro_codtpopro"] != DBNull.Value)
    //    {
    //        obj.pro_codtpopro = pItem["pro_codtpopro"].ToString().ToUpper();
    //    }
    //    if (pItem["pro_descuentoweb"] != DBNull.Value)
    //    {
    //        obj.pro_descuentoweb = Convert.ToDecimal(pItem["pro_descuentoweb"]);
    //    }
    //    if (pItem["pro_laboratorio"] != DBNull.Value)
    //    {
    //        obj.pro_laboratorio = pItem["pro_laboratorio"].ToString();
    //    }
    //    if (pItem["pro_monodroga"] != DBNull.Value)
    //    {
    //        obj.pro_monodroga = pItem["pro_monodroga"].ToString();
    //    }
    //    if (pItem["pro_codigobarra"] != DBNull.Value)
    //    {
    //        obj.pro_codigobarra = pItem["pro_codigobarra"].ToString();
    //    }
    //    if (pItem["pro_codigoalfabeta"] != DBNull.Value)
    //    {
    //        obj.pro_codigoalfabeta = pItem["pro_codigoalfabeta"].ToString();
    //    }
    //    return obj;
    //}
    public static List<cClientes> RecuperarTodosClientes()
    {
        List<cClientes> resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataTable tablaClientes = capaClientes.RecuperarTodosClientes();
            if (tablaClientes != null)
            {
                resultado = new List<cClientes>();
                foreach (DataRow item in tablaClientes.Rows)
                {
                    resultado.Add(ConvertToCliente(item));
                }
            }
        }
        return resultado;
    }
    public static List<string> RecuperarTodosCodigoReparto()
    {
        List<string> resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataTable tabla = capaClientes.RecuperarTodosCodigoReparto();
            if (tabla != null)
            {
                resultado = new List<string>();
                for (int i = 0; i < tabla.Rows.Count; i++)
                {
                    if (tabla.Rows[i]["cli_codrep"] != DBNull.Value)
                    {
                        resultado.Add(tabla.Rows[i]["cli_codrep"].ToString());
                    }
                }
            }
        }
        return resultado;
    }
    public static cClientes RecuperarClientePorId(int pIdCliente)
    {
        cClientes resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataTable tablaClientes = capaClientes.RecuperarClientePorId(pIdCliente);
            if (tablaClientes != null)
            {
                foreach (DataRow item in tablaClientes.Rows)
                {
                    resultado = ConvertToCliente(item);
                }
            }
        }
        return resultado;
    }
    public static List<cProductosBuscador> RecuperarTodosProductosDesdeBuscadorV3(int? pIdOferta, string pTxtBuscador, List<string> pListaColumna, string pSucursal, int? pIdCliente, bool pIsOferta, bool pIsTransfer, string pCli_codprov)
    {
        List<cProductosBuscador> resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataSet dsResultado = null;
            if (pIdOferta == null)
                dsResultado = capaProductos.RecuperarTodosProductosBuscadorV3(pTxtBuscador, pListaColumna, pSucursal, pIdCliente, pCli_codprov);
            else
                dsResultado = capaProductos.RecuperarTodosProductosBuscadorOferta(pIdOferta.Value, pSucursal, pIdCliente, pCli_codprov); ;
            if (dsResultado != null)
            {
                DataTable tablaProductos = dsResultado.Tables[2];
                DataTable tablaSucursalStocks = dsResultado.Tables[1];// (pIsOferta || pIsTransfer) ? dsResultado.Tables[1] : dsResultado.Tables[1];              
                tablaProductos.Merge(dsResultado.Tables[0]);
                List<cTransferDetalle> listaTransferDetalle = null;
                if (pIsTransfer)
                {
                    if (dsResultado.Tables.Count > 3)
                    {
                        listaTransferDetalle = new List<cTransferDetalle>();
                        DataTable tablaTransferDetalle = dsResultado.Tables[3];
                        foreach (DataRow itemTransferDetalle in tablaTransferDetalle.Rows)
                        {
                            cTransferDetalle objTransferDetalle = ConvertToTransferDetalle(itemTransferDetalle);
                            objTransferDetalle.CargarTransfer(ConvertToTransfer(itemTransferDetalle));
                            listaTransferDetalle.Add(objTransferDetalle);
                        }
                    }
                }
                resultado = new List<cProductosBuscador>();
                foreach (DataRow item in tablaProductos.Rows)
                {
                    if (item["pro_codigo"] != DBNull.Value)
                    {
                        List<cSucursalStocks> tempListaSucursalStocks = new List<cSucursalStocks>();
                        tempListaSucursalStocks = (from r in tablaSucursalStocks.Select("stk_codpro = '" + item["pro_codigo"].ToString() + "'").AsEnumerable()
                                                   select new cSucursalStocks { stk_codpro = r["stk_codpro"].ToString(), stk_codsuc = r["stk_codsuc"].ToString(), stk_stock = r["stk_stock"].ToString() }).ToList();
                        if (tempListaSucursalStocks.Count > 0)
                        {
                            cProductosBuscador obj = new cProductosBuscador();
                            obj.pro_codigo = item["pro_codigo"].ToString();
                            if (item["pro_nombre"] != DBNull.Value)
                            {
                                obj.pro_nombre = item["pro_nombre"].ToString();
                            }
                            if (item["pro_precio"] != DBNull.Value)
                            {
                                obj.pro_precio = Convert.ToDecimal(item["pro_precio"]);
                            }
                            if (item["pro_preciofarmacia"] != DBNull.Value)
                            {
                                obj.pro_preciofarmacia = Convert.ToDecimal(item["pro_preciofarmacia"]);
                            }
                            if (item["pro_ofeunidades"] != DBNull.Value)
                            {
                                obj.pro_ofeunidades = Convert.ToInt32(item["pro_ofeunidades"]);
                            }
                            if (item["pro_ofeporcentaje"] != DBNull.Value)
                            {
                                obj.pro_ofeporcentaje = Convert.ToDecimal(item["pro_ofeporcentaje"]);
                            }
                            if (item["pro_neto"] != DBNull.Value)
                            {
                                obj.pro_neto = Convert.ToBoolean(item["pro_neto"]);
                            }
                            if (item["pro_codtpopro"] != DBNull.Value)
                            {
                                obj.pro_codtpopro = item["pro_codtpopro"].ToString().ToUpper();
                            }
                            if (item["pro_descuentoweb"] != DBNull.Value)
                            {
                                obj.pro_descuentoweb = Convert.ToDecimal(item["pro_descuentoweb"]);
                            }
                            if (item["pro_laboratorio"] != DBNull.Value)
                            {
                                obj.pro_laboratorio = item["pro_laboratorio"].ToString();
                            }
                            if (item["pro_monodroga"] != DBNull.Value)
                            {
                                obj.pro_monodroga = item["pro_monodroga"].ToString();
                            }
                            if (item["pro_codigobarra"] != DBNull.Value)
                            {
                                obj.pro_codigobarra = item["pro_codigobarra"].ToString();
                            }
                            if (item["pro_codigoalfabeta"] != DBNull.Value)
                            {
                                obj.pro_codigoalfabeta = item["pro_codigoalfabeta"].ToString();
                            }
                            if (item["pro_troquel"] != DBNull.Value)
                            {
                                obj.pro_troquel = item["pro_troquel"].ToString();
                            }
                            if (item["pro_Ranking"] != DBNull.Value)
                            {
                                obj.pro_Ranking = Convert.ToInt32(item["pro_Ranking"]);
                            }
                            if (item["pro_codtpovta"] != DBNull.Value)
                            {
                                obj.pro_codtpovta = Convert.ToString(item["pro_codtpovta"]);
                            }
                            if (item["isTransfer"] == DBNull.Value)
                            {
                                obj.isTieneTransfer = false;
                            }
                            else
                            {
                                obj.isTieneTransfer = true;
                            }
                            if (item["pro_isTrazable"] != DBNull.Value)
                            {
                                obj.pro_isTrazable = Convert.ToBoolean(item["pro_isTrazable"]);
                            }
                            if (item["pro_isCadenaFrio"] != DBNull.Value)
                            {
                                obj.pro_isCadenaFrio = Convert.ToBoolean(item["pro_isCadenaFrio"]);
                            }
                            if (item["pro_canmaxima"] != DBNull.Value)
                            {
                                obj.pro_canmaxima = Convert.ToInt32(item["pro_canmaxima"]);
                            }
                            // Default = false
                            if (item["pro_entransfer"] != DBNull.Value)
                            {
                                obj.pro_entransfer = Convert.ToBoolean(item["pro_entransfer"]);
                            }
                            if (item["pro_vtasolotransfer"] != DBNull.Value)
                            {
                                obj.pro_vtasolotransfer = Convert.ToBoolean(item["pro_vtasolotransfer"]);
                            }
                            if (item["RequiereVale"] != DBNull.Value)
                            {
                                obj.isValePsicotropicos = Convert.ToBoolean(item["RequiereVale"]);
                            }
                            if (item.Table.Columns.Contains("pro_acuerdo"))
                            {
                                if (item["pro_acuerdo"] != DBNull.Value)
                                {
                                    obj.pro_acuerdo = Convert.ToInt32(item["pro_acuerdo"]);
                                }
                            }
                            obj.isProductoFacturacionDirecta = false;
                            if (item.Table.Columns.Contains("pro_NoTransfersEnClientesPerf") && item["pro_NoTransfersEnClientesPerf"] != DBNull.Value)
                            {
                                obj.pro_NoTransfersEnClientesPerf = Convert.ToBoolean(item["pro_NoTransfersEnClientesPerf"]);
                                if (HttpContext.Current.Session["clientesDefault_Cliente"] != null && ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_tipo == "P" && obj.pro_NoTransfersEnClientesPerf)
                                    obj.isMostrarTransfersEnClientesPerf = false;
                            }
                            if (listaTransferDetalle != null && obj.isMostrarTransfersEnClientesPerf)
                            {
                                List<cTransferDetalle> listaAUXtransferDetalle = listaTransferDetalle.Where(x => x.tde_codpro == obj.pro_nombre).ToList();
                                if (listaAUXtransferDetalle.Count > 0)
                                {
                                    obj.isProductoFacturacionDirecta = true;
                                    obj.CargarTransferYTransferDetalle(listaAUXtransferDetalle[0]);
                                    if (HttpContext.Current.Session["clientesDefault_Cliente"] != null)
                                    {
                                        obj.PrecioFinalTransfer = FuncionesPersonalizadas.ObtenerPrecioFinalTransferBase((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"], obj.tfr_deshab, obj.tfr_pordesadi, obj.pro_neto, obj.pro_codtpopro, obj.pro_descuentoweb, obj.tde_predescuento == null ? 0 : (decimal)obj.tde_predescuento);
                                    }
                                }
                            }
                            obj.listaSucursalStocks = tempListaSucursalStocks;

                            resultado.Add(obj);
                        }
                    }
                }
            }
        }
        return resultado;
    }
    public static List<cProductosBuscador> RecuperarTodosProductosDesdeBuscador_OfertaTransfer(string pSucursal, int? pIdCliente, bool pIsOferta, bool pIsTransfer, string pCli_codprov)
    {
        List<cProductosBuscador> resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataSet dsResultado = null;
            if (pIsOferta)
            {
                dsResultado = capaProductos.RecuperarTodosProductosBuscadorEnOferta(pSucursal, pIdCliente, pCli_codprov);
            }
            else if (pIsTransfer)
            {
                dsResultado = capaProductos.RecuperarTodosProductosBuscadorEnTransfer(pSucursal, pIdCliente, pCli_codprov);
            }
            if (dsResultado != null)
            {
                DataTable tablaProductos = dsResultado.Tables[0];
                DataTable tablaSucursalStocks = dsResultado.Tables[1];
                List<cTransferDetalle> listaTransferDetalle = null;
                //int index = pIsTransfer ? 3 : 2;
                if (dsResultado.Tables.Count > 2)
                {
                    listaTransferDetalle = new List<cTransferDetalle>();
                    DataTable tablaTransferDetalle = dsResultado.Tables[2];
                    foreach (DataRow itemTransferDetalle in tablaTransferDetalle.Rows)
                    {
                        cTransferDetalle objTransferDetalle = ConvertToTransferDetalle(itemTransferDetalle);
                        objTransferDetalle.CargarTransfer(ConvertToTransfer(itemTransferDetalle));
                        listaTransferDetalle.Add(objTransferDetalle);
                    }
                }
                resultado = new List<cProductosBuscador>();
                foreach (DataRow item in tablaProductos.Rows)
                {
                    if (item["pro_codigo"] != DBNull.Value)
                    {
                        List<cSucursalStocks> tempListaSucursalStocks = new List<cSucursalStocks>();
                        tempListaSucursalStocks = (from r in tablaSucursalStocks.Select("stk_codpro = '" + item["pro_codigo"].ToString() + "'").AsEnumerable()
                                                   select new cSucursalStocks { stk_codpro = r["stk_codpro"].ToString(), stk_codsuc = r["stk_codsuc"].ToString(), stk_stock = r["stk_stock"].ToString() }).ToList();
                        if (tempListaSucursalStocks.Count > 0)
                        {
                            cProductosBuscador obj = new cProductosBuscador();
                            obj.pro_codigo = item["pro_codigo"].ToString();
                            if (item["pro_nombre"] != DBNull.Value)
                            {
                                obj.pro_nombre = item["pro_nombre"].ToString();
                            }
                            if (item["pro_precio"] != DBNull.Value)
                            {
                                obj.pro_precio = Convert.ToDecimal(item["pro_precio"]);
                            }
                            if (item["pro_preciofarmacia"] != DBNull.Value)
                            {
                                obj.pro_preciofarmacia = Convert.ToDecimal(item["pro_preciofarmacia"]);
                            }
                            if (item["pro_ofeunidades"] != DBNull.Value)
                            {
                                obj.pro_ofeunidades = Convert.ToInt32(item["pro_ofeunidades"]);
                            }
                            if (item["pro_ofeporcentaje"] != DBNull.Value)
                            {
                                obj.pro_ofeporcentaje = Convert.ToDecimal(item["pro_ofeporcentaje"]);
                            }
                            if (item["pro_neto"] != DBNull.Value)
                            {
                                obj.pro_neto = Convert.ToBoolean(item["pro_neto"]);
                            }
                            if (item["pro_codtpopro"] != DBNull.Value)
                            {
                                obj.pro_codtpopro = item["pro_codtpopro"].ToString().ToUpper();
                            }
                            if (item["pro_descuentoweb"] != DBNull.Value)
                            {
                                obj.pro_descuentoweb = Convert.ToDecimal(item["pro_descuentoweb"]);
                            }
                            if (item["pro_laboratorio"] != DBNull.Value)
                            {
                                obj.pro_laboratorio = item["pro_laboratorio"].ToString();
                            }
                            if (item["pro_monodroga"] != DBNull.Value)
                            {
                                obj.pro_monodroga = item["pro_monodroga"].ToString();
                            }
                            if (item["pro_codigobarra"] != DBNull.Value)
                            {
                                obj.pro_codigobarra = item["pro_codigobarra"].ToString();
                            }
                            if (item["pro_codigoalfabeta"] != DBNull.Value)
                            {
                                obj.pro_codigoalfabeta = item["pro_codigoalfabeta"].ToString();
                            }
                            if (item["pro_troquel"] != DBNull.Value)
                            {
                                obj.pro_troquel = item["pro_troquel"].ToString();
                            }
                            if (item["pro_Ranking"] != DBNull.Value)
                            {
                                obj.pro_Ranking = Convert.ToInt32(item["pro_Ranking"]);
                            }
                            if (item["pro_codtpovta"] != DBNull.Value)
                            {
                                obj.pro_codtpovta = Convert.ToString(item["pro_codtpovta"]);
                            }
                            if (item["isTransfer"] == DBNull.Value)
                            {
                                obj.isTieneTransfer = false;
                            }
                            else
                            {
                                obj.isTieneTransfer = true;
                            }
                            if (item["pro_isTrazable"] != DBNull.Value)
                            {
                                obj.pro_isTrazable = Convert.ToBoolean(item["pro_isTrazable"]);
                            }
                            if (item["pro_isCadenaFrio"] != DBNull.Value)
                            {
                                obj.pro_isCadenaFrio = Convert.ToBoolean(item["pro_isCadenaFrio"]);
                            }
                            if (item["pro_canmaxima"] != DBNull.Value)
                            {
                                obj.pro_canmaxima = Convert.ToInt32(item["pro_canmaxima"]);
                            }
                            // Default = false
                            if (item["pro_entransfer"] != DBNull.Value)
                            {
                                obj.pro_entransfer = Convert.ToBoolean(item["pro_entransfer"]);
                            }
                            if (item["pro_vtasolotransfer"] != DBNull.Value)
                            {
                                obj.pro_vtasolotransfer = Convert.ToBoolean(item["pro_vtasolotransfer"]);
                            }
                            if (item["RequiereVale"] != DBNull.Value)
                            {
                                obj.isValePsicotropicos = Convert.ToBoolean(item["RequiereVale"]);
                            }
                            if (item.Table.Columns.Contains("pro_acuerdo"))
                            {
                                if (item["pro_acuerdo"] != DBNull.Value)
                                {
                                    obj.pro_acuerdo = Convert.ToInt32(item["pro_acuerdo"]);
                                }
                            }
                            obj.isProductoFacturacionDirecta = false;
                            if (item.Table.Columns.Contains("pro_NoTransfersEnClientesPerf") && item["pro_NoTransfersEnClientesPerf"] != DBNull.Value)
                            {
                                obj.pro_NoTransfersEnClientesPerf = Convert.ToBoolean(item["pro_NoTransfersEnClientesPerf"]);
                                if (HttpContext.Current.Session["clientesDefault_Cliente"] != null && ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_tipo == "P" && obj.pro_NoTransfersEnClientesPerf)
                                    obj.isMostrarTransfersEnClientesPerf = false;
                            }
                            if (listaTransferDetalle != null)
                            {
                                List<cTransferDetalle> listaAUXtransferDetalle = listaTransferDetalle.Where(x => x.tde_codpro == obj.pro_nombre).ToList();
                                if (listaAUXtransferDetalle.Count > 0)
                                {
                                    obj.isProductoFacturacionDirecta = true;
                                    obj.CargarTransferYTransferDetalle(listaAUXtransferDetalle[0]);
                                    if (HttpContext.Current.Session["clientesDefault_Cliente"] != null)
                                    {
                                        obj.PrecioFinalTransfer = FuncionesPersonalizadas.ObtenerPrecioFinalTransferBase((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"], obj.tfr_deshab, obj.tfr_pordesadi, obj.pro_neto, obj.pro_codtpopro, obj.pro_descuentoweb, obj.tde_predescuento == null ? 0 : (decimal)obj.tde_predescuento);
                                    }
                                }
                            }
                            obj.listaSucursalStocks = tempListaSucursalStocks;
                            resultado.Add(obj);
                        }
                    }
                }
            }
            if (resultado != null && pIsTransfer)
                resultado = resultado.Where(x => x.isMostrarTransfersEnClientesPerf).OrderBy(x => x.pro_nombre).ToList();
        }
        return resultado;
    }
    public static List<cProductosBuscador> RecuperarTodosProductosDesdeBuscador(string pTxtBuscador, List<string> pListaColumna, string pSucursal, int? pIdCliente, bool pIsOferta, bool pIsTransfer, string pCli_codprov)
    {
        List<cProductosBuscador> resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataSet dsResultado = null;
            if (pIsOferta)
            {
                dsResultado = capaProductos.RecuperarTodosProductosBuscadorEnOferta(pSucursal, pIdCliente, pCli_codprov);
            }
            else if (pIsTransfer)
            {
                dsResultado = capaProductos.RecuperarTodosProductosBuscadorEnTransfer(pSucursal, pIdCliente, pCli_codprov);
            }
            else
            {
                if (pListaColumna == null)
                {
                    dsResultado = capaProductos.RecuperarTodosProductosBuscador(pTxtBuscador, pSucursal, pIdCliente, pCli_codprov);
                }
                else
                {
                    dsResultado = capaProductos.RecuperarTodosProductosBuscadorConVariasColumnas(pTxtBuscador, pListaColumna, pSucursal, pIdCliente, pCli_codprov);
                }
            }
            if (dsResultado != null)
            {
                DataTable tablaProductos = dsResultado.Tables[0];
                DataTable tablaSucursalStocks = dsResultado.Tables[1];
                resultado = new List<cProductosBuscador>();
                foreach (DataRow item in tablaProductos.Rows)
                {
                    if (item["pro_codigo"] != DBNull.Value)
                    {
                        List<cSucursalStocks> tempListaSucursalStocks = new List<cSucursalStocks>();
                        tempListaSucursalStocks = (from r in tablaSucursalStocks.Select("stk_codpro = '" + item["pro_codigo"].ToString() + "'").AsEnumerable()
                                                   select new cSucursalStocks { stk_codpro = r["stk_codpro"].ToString(), stk_codsuc = r["stk_codsuc"].ToString(), stk_stock = r["stk_stock"].ToString() }).ToList();
                        if (tempListaSucursalStocks.Count > 0)
                        {
                            cProductosBuscador obj = new cProductosBuscador();
                            obj.pro_codigo = item["pro_codigo"].ToString();
                            if (item["pro_nombre"] != DBNull.Value)
                            {
                                obj.pro_nombre = item["pro_nombre"].ToString();
                            }
                            if (item["pro_precio"] != DBNull.Value)
                            {
                                obj.pro_precio = Convert.ToDecimal(item["pro_precio"]);
                            }
                            if (item["pro_preciofarmacia"] != DBNull.Value)
                            {
                                obj.pro_preciofarmacia = Convert.ToDecimal(item["pro_preciofarmacia"]);
                            }
                            if (item["pro_ofeunidades"] != DBNull.Value)
                            {
                                obj.pro_ofeunidades = Convert.ToInt32(item["pro_ofeunidades"]);
                            }
                            if (item["pro_ofeporcentaje"] != DBNull.Value)
                            {
                                obj.pro_ofeporcentaje = Convert.ToDecimal(item["pro_ofeporcentaje"]);
                            }
                            if (item["pro_neto"] != DBNull.Value)
                            {
                                obj.pro_neto = Convert.ToBoolean(item["pro_neto"]);
                            }
                            if (item["pro_codtpopro"] != DBNull.Value)
                            {
                                obj.pro_codtpopro = item["pro_codtpopro"].ToString().ToUpper();
                            }
                            if (item["pro_descuentoweb"] != DBNull.Value)
                            {
                                obj.pro_descuentoweb = Convert.ToDecimal(item["pro_descuentoweb"]);
                            }
                            if (item["pro_laboratorio"] != DBNull.Value)
                            {
                                obj.pro_laboratorio = item["pro_laboratorio"].ToString();
                            }
                            if (item["pro_monodroga"] != DBNull.Value)
                            {
                                obj.pro_monodroga = item["pro_monodroga"].ToString();
                            }
                            if (item["pro_codigobarra"] != DBNull.Value)
                            {
                                obj.pro_codigobarra = item["pro_codigobarra"].ToString();
                            }
                            if (item["pro_codigoalfabeta"] != DBNull.Value)
                            {
                                obj.pro_codigoalfabeta = item["pro_codigoalfabeta"].ToString();
                            }
                            if (item["pro_Ranking"] != DBNull.Value)
                            {
                                obj.pro_Ranking = Convert.ToInt32(item["pro_Ranking"]);
                            }
                            if (item["pro_codtpovta"] != DBNull.Value)
                            {
                                obj.pro_codtpovta = Convert.ToString(item["pro_codtpovta"]);
                            }
                            if (item["isTransfer"] == DBNull.Value)
                            {
                                obj.isTieneTransfer = false;
                            }
                            else
                            {
                                obj.isTieneTransfer = true;
                            }
                            if (item["pro_isTrazable"] != DBNull.Value)
                            {
                                obj.pro_isTrazable = Convert.ToBoolean(item["pro_isTrazable"]);
                            }
                            if (item["pro_isCadenaFrio"] != DBNull.Value)
                            {
                                obj.pro_isCadenaFrio = Convert.ToBoolean(item["pro_isCadenaFrio"]);
                            }
                            if (item["pro_canmaxima"] != DBNull.Value)
                            {
                                obj.pro_canmaxima = Convert.ToInt32(item["pro_canmaxima"]);
                            }
                            // Default = false
                            if (item["pro_entransfer"] != DBNull.Value)
                            {
                                obj.pro_entransfer = Convert.ToBoolean(item["pro_entransfer"]);
                            }
                            if (item["pro_vtasolotransfer"] != DBNull.Value)
                            {
                                obj.pro_vtasolotransfer = Convert.ToBoolean(item["pro_vtasolotransfer"]);
                            }
                            if (item["RequiereVale"] != DBNull.Value)
                            {
                                obj.isValePsicotropicos = Convert.ToBoolean(item["RequiereVale"]);
                            }
                            if (item.Table.Columns.Contains("pro_acuerdo"))
                            {
                                if (item["pro_acuerdo"] != DBNull.Value)
                                {
                                    obj.pro_acuerdo = Convert.ToInt32(item["pro_acuerdo"]);
                                }
                            }
                            obj.listaSucursalStocks = tempListaSucursalStocks;
                            resultado.Add(obj);
                        }
                    }
                }
            }
        }
        return resultado;
    }
    public static List<cProductosBuscador> RecuperarTodosProductosDesdeBuscadorOptimizado(string pTxtBuscador, string pSucursal, int? pIdCliente)
    {
        List<cProductosBuscador> resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            // Recuperar sucursales dependientes
            List<cSucursal> listaSucursalDependiente = RecuperarTodasSucursalesDependientes().Where(x => x.sde_sucursal == pSucursal).ToList();
            DataSet dsResultado = capaProductos.RecuperarTodosProductosBuscadorOptimizado(pTxtBuscador);
            if (dsResultado != null)
            {
                DataTable tablaProductos = dsResultado.Tables[0];
                resultado = new List<cProductosBuscador>();

                foreach (DataRow item in tablaProductos.Rows)
                {
                    if (item["pro_codigo"] != DBNull.Value)
                    {
                        List<cSucursalStocks> tempListaSucursalStocks = new List<cSucursalStocks>();
                        if (item["pop_ListaStocks"] != DBNull.Value)
                        {
                            string strSucursalStock = item["pop_ListaStocks"].ToString();
                            int cantSucursalStock = item["pop_ListaStocks"].ToString().Length;
                            for (int i = 0; i < cantSucursalStock; i = i + 3)
                            {
                                int cantIS = listaSucursalDependiente.Where(x => x.sde_sucursalDependiente == strSucursalStock.Substring(i, 2)).Count();
                                if (cantIS > 0)
                                {
                                    tempListaSucursalStocks.Add(new cSucursalStocks { stk_codpro = item["pro_codigo"].ToString(), stk_codsuc = strSucursalStock.Substring(i, 2), stk_stock = strSucursalStock.Substring(i + 2, 1) });
                                }
                            }
                        }
                        if (tempListaSucursalStocks.Count > 0)
                        {
                            cProductosBuscador obj = new cProductosBuscador(ConvertToProductos(item));
                            if (item["pro_Ranking"] != DBNull.Value)
                            {
                                obj.pro_Ranking = Convert.ToInt32(item["pro_Ranking"]);
                            }
                            if (item["pop_isTransfer"] != DBNull.Value)
                            {
                                obj.isTieneTransfer = Convert.ToBoolean(item["pop_isTransfer"]);
                            }
                            obj.listaSucursalStocks = tempListaSucursalStocks;
                            resultado.Add(obj);
                        }
                    }
                }
            }
        }
        return resultado;
    }
    private static cHistorialProcesos ConvertToHistorialProcesos(DataRow pItem)
    {
        cHistorialProcesos obj = new cHistorialProcesos();
        if (pItem["his_id"] != DBNull.Value)
        {
            obj.his_id = Convert.ToInt32(pItem["his_id"]);
        }
        if (pItem["his_Descripcion"] != DBNull.Value)
        {
            obj.his_Descripcion = pItem["his_Descripcion"].ToString();
        }
        if (pItem["his_NombreProcedimiento"] != DBNull.Value)
        {
            obj.his_NombreProcedimiento = pItem["his_NombreProcedimiento"].ToString();
        }
        if (pItem["his_Fecha"] != DBNull.Value)
        {
            obj.his_Fecha = Convert.ToDateTime(pItem["his_Fecha"]);
            obj.his_FechaToString = ((DateTime)obj.his_Fecha).ToString();
        }
        return obj;
    }
    public static List<cHistorialProcesos> RecuperarTodasLasSincronizaciones()
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            List<cHistorialProcesos> lista = new List<cHistorialProcesos>();
            DataTable tabla = capaLogRegistro.RecuperarTodoHistorialProcesos();
            if (tabla != null)
            {
                foreach (DataRow item in tabla.Rows)
                {
                    lista.Add(ConvertToHistorialProcesos(item));
                }
            }
            return lista;
        }
        return null;
    }
    private static cPalabraBusqueda ConvertToPalabrasBusquedas(DataRow pItem)
    {
        cPalabraBusqueda obj = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            obj = new cPalabraBusqueda();
            obj.hbp_id = Convert.ToInt32(pItem["hbp_id"]);
            if (pItem["hbp_Palabra"] != DBNull.Value)
            {
                obj.hbp_Palabra = pItem["hbp_Palabra"].ToString();
            }
            //if (pItem["hbp_PalabraElegida"] != DBNull.Value)
            //{
            //    obj.hbp_PalabraElegida = pItem["hbp_PalabraElegida"].ToString();
            //}
        }
        return obj;
    }
    public static List<cPalabraBusqueda> RecuperarTodasPalabrasYaBuscada(int? pIdUsuario, string pNombreTabla)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataTable tabla = capaLogRegistro.RecuperarTodasPalabrasBusquedas(pIdUsuario, pNombreTabla);
            if (tabla != null)
            {
                List<cPalabraBusqueda> lista = new List<cPalabraBusqueda>();
                foreach (DataRow item in tabla.Rows)
                {
                    lista.Add(ConvertToPalabrasBusquedas(item));
                }
                return lista;
            }
        }
        return null;
    }
    public static List<string> RecuperarLasPalabrasMasBusquedasPorUsuario(int pIdUsuario)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataTable tabla = capaLogRegistro.RecuperarLasPalabrasMasBusquedasPorUsuario(pIdUsuario);
            if (tabla != null)
            {
                List<string> lista = new List<string>();
                foreach (DataRow item in tabla.Rows)
                {
                    if (item["cbp_Palabra"] != DBNull.Value)
                    {
                        lista.Add(item["cbp_Palabra"].ToString());
                    }
                }
                return lista;
            }
        }
        return null;
    }
    public static int InsertarPalabraBuscada(string pPalabra, int pIdUsuario, string pNombreTabla)
    {
        int resultado = -1;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            resultado = capaLogRegistro.IngresarPalabraBusqueda(pIdUsuario, pNombreTabla, pPalabra);
        }
        return resultado;
    }
    public static int CambiarContraseñaUsuarioPersonal(int pIdUsuario, string pContraseñaVieja, string pContraseñaNueva)
    {
        int resultado = -1;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            resultado = capaSeguridad.CambiarContraseñaPersonal(pIdUsuario, pContraseñaVieja, pContraseñaNueva);
        }
        return resultado;
    }
    //ESCONTRASEÑACORRECTA
    public static void AgregarHistorialProductoCarrito(int pIdCliente, string pIdProducto, int? pIdUsuario)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            capaLogRegistro.AgregarProductosBuscadosDelCarrito(pIdCliente, pIdProducto, pIdUsuario);
        }
    }

    public static void AgregarHistorialProductoCarritoTransfer(int pIdCliente, List<cProductosAndCantidad> pListaProductosMasCantidad, int? pIdUsuario)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataTable pTablaDetalle = FuncionesPersonalizadas.ConvertProductosAndCantidadToDataTable(pListaProductosMasCantidad);
            capaLogRegistro.AgregarProductosBuscadosDelCarritoTransfer(pIdCliente, pTablaDetalle, pIdUsuario);
        }
    }
    //public static void AgregarProductoAlCarritoDesdeArchivoPedidos(string pSucursal, DataTable pTabla, string pTipoDeArchivo, int pIdCliente, int? pIdUsuario)
    //{
    //    if (VerificarPermisos(CredencialAutenticacion))
    //    {
    //        capaLogRegistro.AgregarProductoAlCarritoDesdeArchivoPedidos(pSucursal, pTabla, pTipoDeArchivo, pIdCliente, pIdUsuario);
    //    }
    //}
    //public static void AgregarProductoAlCarritoDesdeArchivoPedidos(string pSucursal, DataTable pTabla, string pTipoDeArchivo, int pIdCliente, string pCli_codprov, int? pIdUsuario)
    //{
    //    if (VerificarPermisos(CredencialAutenticacion))
    //    {
    //        capaLogRegistro.AgregarProductoAlCarritoDesdeArchivoPedidosV2(pSucursal, pTabla, pTipoDeArchivo, pIdCliente,pCli_codprov, pIdUsuario);
    //    }
    //}
    //public static void AgregarProductoAlCarritoDesdeArchivoPedidosV3(string pSucursal, DataTable pTabla, string pTipoDeArchivo, int pIdCliente, string pCli_codprov, bool pCli_isGLN, int? pIdUsuario)
    //{
    //    if (VerificarPermisos(CredencialAutenticacion))
    //    {
    //        capaLogRegistro.AgregarProductoAlCarritoDesdeArchivoPedidosV3(pSucursal, pTabla, pTipoDeArchivo, pIdCliente, pCli_codprov, pCli_isGLN, pIdUsuario);
    //    }
    //}
    //public static List<cProductosBuscador> AgregarProductoAlCarritoDesdeArchivoPedidosV4(string pSucursalElejida, string pSucursalCliente, DataTable pTabla, string pTipoDeArchivo, int pIdCliente, string pCli_codprov, bool pCli_isGLN, int? pIdUsuario)
    //{
    //    List<cProductosBuscador> resultado = null;
    //    if (VerificarPermisos(CredencialAutenticacion))
    //    {
    //        DataSet DataSetResultado = capaLogRegistro.AgregarProductoAlCarritoDesdeArchivoPedidosV4(pSucursalElejida, pSucursalCliente, pTabla, pTipoDeArchivo, pIdCliente, pCli_codprov, pCli_isGLN, pIdUsuario);
    //        resultado = ConvertToProductoBuscador(DataSetResultado);
    //    }
    //    return resultado;
    //}
    public static List<cProductosBuscador> AgregarProductoAlCarritoDesdeArchivoPedidosV5(string pSucursalElejida, string pSucursalCliente, DataTable pTabla, string pTipoDeArchivo, int pIdCliente, string pCli_codprov, bool pCli_isGLN, int? pIdUsuario)
    {
        List<cProductosBuscador> resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataSet DataSetResultado = capaLogRegistro.AgregarProductoAlCarritoDesdeArchivoPedidosV5(pSucursalElejida, pSucursalCliente, pTabla, pTipoDeArchivo, pIdCliente, pCli_codprov, pCli_isGLN, pIdUsuario);
            resultado = ConvertToProductoBuscadorV5(DataSetResultado, pSucursalElejida);

            if (resultado != null)
            {
                // TIPO CLIENTE
                if (((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_tipo == SitioBase.Constantes.cTipoCliente_Perfumeria) // Solamente perfumeria
                {
                    //resultado = resultado.Where(x => x.pro_codtpopro == SitioBase.Constantes.cTIPOPRODUCTO_Perfumeria || x.pro_codtpopro == SitioBase.Constantes.cTIPOPRODUCTO_PerfumeriaCuentaYOrden).ToList();
                    foreach (var item in resultado.Where(x => x.pro_codtpopro != SitioBase.Constantes.cTIPOPRODUCTO_Perfumeria && x.pro_codtpopro != SitioBase.Constantes.cTIPOPRODUCTO_PerfumeriaCuentaYOrden))
                    {
                        item.isPermitirPedirProducto = false;
                    }
                }
                else if (((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_tipo == SitioBase.Constantes.cTipoCliente_Todos) // Todos los productos
                {
                    //// Si el cliente no toma perfumeria
                    //if (!((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_tomaPerfumeria)
                    //{
                    //    resultado = resultado.Where(x => x.pro_codtpopro != SitioBase.Constantes.cTIPOPRODUCTO_Perfumeria && x.pro_codtpopro != SitioBase.Constantes.cTIPOPRODUCTO_PerfumeriaCuentaYOrden).ToList();
                    //}
                    //// fin Si el cliente no toma perfumeria
                    if (!((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_tomaPerfumeria)
                    {
                        foreach (var item in resultado.Where(x => x.pro_codtpopro == SitioBase.Constantes.cTIPOPRODUCTO_Perfumeria || x.pro_codtpopro == SitioBase.Constantes.cTIPOPRODUCTO_PerfumeriaCuentaYOrden))
                        {
                            item.isPermitirPedirProducto = false;
                        }
                    }
                }
                // FIN TIPO CLIENTE
                resultado = resultado.OrderBy(x => x.nroordenamiento).ToList();
            }
        }
        return resultado;
    }
    public static List<cProductosBuscador> ConvertToProductoBuscador(DataSet DataSetResultado)
    {
        List<cProductosBuscador> resultado = null;
        if (DataSetResultado != null)
        {
            if (DataSetResultado.Tables.Count > 1)
            {
                DataTable tablaProductos = DataSetResultado.Tables[0];
                DataTable tablaSucursalStocks = DataSetResultado.Tables[1];
                resultado = new List<cProductosBuscador>();
                foreach (DataRow item in tablaProductos.Rows)
                {
                    if (item["pro_codigo"] != DBNull.Value)
                    {
                        List<cSucursalStocks> tempListaSucursalStocks = new List<cSucursalStocks>();
                        tempListaSucursalStocks = (from r in tablaSucursalStocks.Select("stk_codpro = '" + item["pro_codigo"].ToString() + "'").AsEnumerable()
                                                   select new cSucursalStocks { stk_codpro = r["stk_codpro"].ToString(), stk_codsuc = r["stk_codsuc"].ToString(), stk_stock = r["stk_stock"].ToString() }).ToList();
                        if (tempListaSucursalStocks.Count > 0)
                        {
                            cProductosBuscador obj = new cProductosBuscador();
                            obj.pro_codigo = item["pro_codigo"].ToString();
                            if (item["pro_nombre"] != DBNull.Value)
                            {
                                obj.pro_nombre = item["pro_nombre"].ToString();
                            }
                            if (item["pro_precio"] != DBNull.Value)
                            {
                                obj.pro_precio = Convert.ToDecimal(item["pro_precio"]);
                            }
                            if (item["pro_preciofarmacia"] != DBNull.Value)
                            {
                                obj.pro_preciofarmacia = Convert.ToDecimal(item["pro_preciofarmacia"]);
                            }
                            if (item["pro_ofeunidades"] != DBNull.Value)
                            {
                                obj.pro_ofeunidades = Convert.ToInt32(item["pro_ofeunidades"]);
                            }
                            if (item["pro_ofeporcentaje"] != DBNull.Value)
                            {
                                obj.pro_ofeporcentaje = Convert.ToDecimal(item["pro_ofeporcentaje"]);
                            }
                            if (item["pro_neto"] != DBNull.Value)
                            {
                                obj.pro_neto = Convert.ToBoolean(item["pro_neto"]);
                            }
                            if (item["pro_codtpopro"] != DBNull.Value)
                            {
                                obj.pro_codtpopro = item["pro_codtpopro"].ToString().ToUpper();
                            }
                            if (item["pro_descuentoweb"] != DBNull.Value)
                            {
                                obj.pro_descuentoweb = Convert.ToDecimal(item["pro_descuentoweb"]);
                            }
                            if (item["pro_laboratorio"] != DBNull.Value)
                            {
                                obj.pro_laboratorio = item["pro_laboratorio"].ToString();
                            }
                            if (item["pro_monodroga"] != DBNull.Value)
                            {
                                obj.pro_monodroga = item["pro_monodroga"].ToString();
                            }
                            if (item["pro_codigobarra"] != DBNull.Value)
                            {
                                obj.pro_codigobarra = item["pro_codigobarra"].ToString();
                            }
                            if (item["pro_codigoalfabeta"] != DBNull.Value)
                            {
                                obj.pro_codigoalfabeta = item["pro_codigoalfabeta"].ToString();
                            }
                            if (item["pro_troquel"] != DBNull.Value)
                            {
                                obj.pro_troquel = item["pro_troquel"].ToString();
                            }
                            if (item.Table.Columns.Contains("pro_Ranking"))
                            {
                                if (item["pro_Ranking"] != DBNull.Value)
                                {
                                    obj.pro_Ranking = Convert.ToInt32(item["pro_Ranking"]);
                                }
                            }
                            if (item["pro_codtpovta"] != DBNull.Value)
                            {
                                obj.pro_codtpovta = Convert.ToString(item["pro_codtpovta"]);
                            }
                            if (item["isTransfer"] == DBNull.Value)
                            {
                                obj.isTieneTransfer = false;
                            }
                            else
                            {
                                obj.isTieneTransfer = true;
                            }
                            if (item["pro_isTrazable"] != DBNull.Value)
                            {
                                obj.pro_isTrazable = Convert.ToBoolean(item["pro_isTrazable"]);
                            }
                            if (item["pro_isCadenaFrio"] != DBNull.Value)
                            {
                                obj.pro_isCadenaFrio = Convert.ToBoolean(item["pro_isCadenaFrio"]);
                            }
                            if (item["pro_canmaxima"] != DBNull.Value)
                            {
                                obj.pro_canmaxima = Convert.ToInt32(item["pro_canmaxima"]);
                            }
                            if (item["pro_entransfer"] != DBNull.Value)
                            {
                                obj.pro_entransfer = Convert.ToBoolean(item["pro_entransfer"]);
                            }
                            if (item["pro_vtasolotransfer"] != DBNull.Value)
                            {
                                obj.pro_vtasolotransfer = Convert.ToBoolean(item["pro_vtasolotransfer"]);
                            }
                            if (item["RequiereVale"] != DBNull.Value)
                            {
                                obj.isValePsicotropicos = Convert.ToBoolean(item["RequiereVale"]);
                            }
                            if (item.Table.Columns.Contains("cantidad"))
                            {
                                if (item["cantidad"] != DBNull.Value)
                                {
                                    obj.cantidad = Convert.ToInt32(item["cantidad"]);
                                }
                            }
                            if (item.Table.Columns.Contains("nroordenamiento"))
                            {
                                if (item["nroordenamiento"] != DBNull.Value)
                                {
                                    obj.nroordenamiento = Convert.ToInt32(item["nroordenamiento"]);
                                }
                            }
                            if (item.Table.Columns.Contains("pro_acuerdo"))
                            {
                                if (item["pro_acuerdo"] != DBNull.Value)
                                {
                                    obj.pro_acuerdo = Convert.ToInt32(item["pro_acuerdo"]);
                                }
                            }
                            obj.listaSucursalStocks = tempListaSucursalStocks;
                            resultado.Add(obj);
                        }

                    }

                }//foreach (DataRow item in tablaProductos.Rows)
            }
        }
        return resultado;
    }
    public static List<cProductosBuscador> ConvertToProductoBuscadorV5(DataSet DataSetResultado, string pSucursalElejida)
    {
        List<cProductosBuscador> resultado = null;
        if (DataSetResultado != null)
        {
            if (DataSetResultado.Tables.Count > 1)
            {
                DataTable tablaProductos = DataSetResultado.Tables[0];
                DataTable tablaSucursalStocks = DataSetResultado.Tables[1];
                DataTable tablaProductosNoEncontrado = DataSetResultado.Tables[2];
                DataTable tablaTransferDetalle = DataSetResultado.Tables[3];
                List<cTransferDetalle> listaTransferDetalle = new List<cTransferDetalle>();
                foreach (DataRow itemTransferDetalle in tablaTransferDetalle.Rows)
                {
                    cTransferDetalle objTransferDetalle = ConvertToTransferDetalle(itemTransferDetalle);
                    objTransferDetalle.CargarTransfer(ConvertToTransfer(itemTransferDetalle));
                    listaTransferDetalle.Add(objTransferDetalle);
                }
                resultado = new List<cProductosBuscador>();
                foreach (DataRow item in tablaProductos.Rows)
                {
                    if (item["pro_codigo"] != DBNull.Value)
                    {
                        List<cSucursalStocks> tempListaSucursalStocks = new List<cSucursalStocks>();
                        tempListaSucursalStocks = (from r in tablaSucursalStocks.Select("stk_codpro = '" + item["pro_codigo"].ToString() + "'").AsEnumerable()
                                                   select new cSucursalStocks { stk_codpro = r["stk_codpro"].ToString(), stk_codsuc = r["stk_codsuc"].ToString(), stk_stock = r["stk_stock"].ToString() }).ToList();
                        if (tempListaSucursalStocks.Count > 0)
                        {
                            cProductosBuscador obj = new cProductosBuscador();
                            obj.pro_codigo = item["pro_codigo"].ToString();
                            if (item["pro_nombre"] != DBNull.Value)
                            {
                                obj.pro_nombre = item["pro_nombre"].ToString();
                            }
                            if (item["pro_precio"] != DBNull.Value)
                            {
                                obj.pro_precio = Convert.ToDecimal(item["pro_precio"]);
                            }
                            if (item["pro_preciofarmacia"] != DBNull.Value)
                            {
                                obj.pro_preciofarmacia = Convert.ToDecimal(item["pro_preciofarmacia"]);
                            }
                            if (item["pro_ofeunidades"] != DBNull.Value)
                            {
                                obj.pro_ofeunidades = Convert.ToInt32(item["pro_ofeunidades"]);
                            }
                            if (item["pro_ofeporcentaje"] != DBNull.Value)
                            {
                                obj.pro_ofeporcentaje = Convert.ToDecimal(item["pro_ofeporcentaje"]);
                            }
                            if (item["pro_neto"] != DBNull.Value)
                            {
                                obj.pro_neto = Convert.ToBoolean(item["pro_neto"]);
                            }
                            if (item["pro_codtpopro"] != DBNull.Value)
                            {
                                obj.pro_codtpopro = item["pro_codtpopro"].ToString().ToUpper();
                            }
                            if (item["pro_descuentoweb"] != DBNull.Value)
                            {
                                obj.pro_descuentoweb = Convert.ToDecimal(item["pro_descuentoweb"]);
                            }
                            if (item["pro_laboratorio"] != DBNull.Value)
                            {
                                obj.pro_laboratorio = item["pro_laboratorio"].ToString();
                            }
                            if (item["pro_monodroga"] != DBNull.Value)
                            {
                                obj.pro_monodroga = item["pro_monodroga"].ToString();
                            }
                            if (item["pro_codigobarra"] != DBNull.Value)
                            {
                                obj.pro_codigobarra = item["pro_codigobarra"].ToString();
                            }
                            if (item["pro_codigoalfabeta"] != DBNull.Value)
                            {
                                obj.pro_codigoalfabeta = item["pro_codigoalfabeta"].ToString();
                            }
                            if (item["pro_troquel"] != DBNull.Value)
                            {
                                obj.pro_troquel = item["pro_troquel"].ToString();
                            }
                            if (item.Table.Columns.Contains("pro_Ranking"))
                            {
                                if (item["pro_Ranking"] != DBNull.Value)
                                {
                                    obj.pro_Ranking = Convert.ToInt32(item["pro_Ranking"]);
                                }
                            }
                            if (item["pro_codtpovta"] != DBNull.Value)
                            {
                                obj.pro_codtpovta = Convert.ToString(item["pro_codtpovta"]);
                            }
                            if (item["isTransfer"] == DBNull.Value)
                            {
                                obj.isTieneTransfer = false;
                            }
                            else
                            {
                                obj.isTieneTransfer = true;
                            }
                            if (item["pro_isTrazable"] != DBNull.Value)
                            {
                                obj.pro_isTrazable = Convert.ToBoolean(item["pro_isTrazable"]);
                            }
                            if (item["pro_isCadenaFrio"] != DBNull.Value)
                            {
                                obj.pro_isCadenaFrio = Convert.ToBoolean(item["pro_isCadenaFrio"]);
                            }
                            if (item["pro_canmaxima"] != DBNull.Value)
                            {
                                obj.pro_canmaxima = Convert.ToInt32(item["pro_canmaxima"]);
                            }
                            if (item["pro_entransfer"] != DBNull.Value)
                            {
                                obj.pro_entransfer = Convert.ToBoolean(item["pro_entransfer"]);
                            }
                            if (item["pro_vtasolotransfer"] != DBNull.Value)
                            {
                                obj.pro_vtasolotransfer = Convert.ToBoolean(item["pro_vtasolotransfer"]);
                            }
                            if (item["RequiereVale"] != DBNull.Value)
                            {
                                obj.isValePsicotropicos = Convert.ToBoolean(item["RequiereVale"]);
                            }
                            if (item.Table.Columns.Contains("cantidad"))
                            {
                                if (item["cantidad"] != DBNull.Value)
                                {
                                    obj.cantidad = Convert.ToInt32(item["cantidad"]);
                                }
                            }
                            if (item.Table.Columns.Contains("nroordenamiento"))
                            {
                                if (item["nroordenamiento"] != DBNull.Value)
                                {
                                    obj.nroordenamiento = Convert.ToInt32(item["nroordenamiento"]);
                                }
                            }
                            if (item.Table.Columns.Contains("pro_acuerdo"))
                            {
                                if (item["pro_acuerdo"] != DBNull.Value)
                                {
                                    obj.pro_acuerdo = Convert.ToInt32(item["pro_acuerdo"]);
                                }
                            }
                            //
                            obj.isProductoFacturacionDirecta = false;
                            if (item.Table.Columns.Contains("pro_NoTransfersEnClientesPerf") && item["pro_NoTransfersEnClientesPerf"] != DBNull.Value)
                            {
                                obj.pro_NoTransfersEnClientesPerf = Convert.ToBoolean(item["pro_NoTransfersEnClientesPerf"]);
                                if (HttpContext.Current.Session["clientesDefault_Cliente"] != null && ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_tipo == "P" && obj.pro_NoTransfersEnClientesPerf)
                                    obj.isMostrarTransfersEnClientesPerf = false;
                            }
                            if (listaTransferDetalle != null)
                            {
                                List<cTransferDetalle> listaAUXtransferDetalle = listaTransferDetalle.Where(x => x.tde_codpro == obj.pro_nombre).ToList();
                                if (listaAUXtransferDetalle.Count > 0)
                                {
                                    obj.isProductoFacturacionDirecta = true;
                                    obj.CargarTransferYTransferDetalle(listaAUXtransferDetalle[0]);
                                    if (HttpContext.Current.Session["clientesDefault_Cliente"] != null)
                                    {
                                        obj.PrecioFinalTransfer = FuncionesPersonalizadas.ObtenerPrecioFinalTransferBase((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"], obj.tfr_deshab, obj.tfr_pordesadi, obj.pro_neto, obj.pro_codtpopro, obj.pro_descuentoweb, obj.tde_predescuento == null ? 0 : (decimal)obj.tde_predescuento);
                                    }
                                }
                            }
                            //
                            obj.listaSucursalStocks = tempListaSucursalStocks;
                            for (int iSucursalStocks = 0; iSucursalStocks < obj.listaSucursalStocks.Count; iSucursalStocks++)
                            {
                                if (pSucursalElejida == obj.listaSucursalStocks[iSucursalStocks].stk_codsuc)
                                {
                                    obj.listaSucursalStocks[iSucursalStocks].cantidadSucursal = obj.cantidad;
                                    break;
                                }
                            }

                            resultado.Add(obj);
                        }
                    }

                }//foreach (DataRow item in tablaProductos.Rows)

                //////////////
                foreach (DataRow item in tablaProductosNoEncontrado.Rows)
                {
                    if (item["nombreNoEncontrado"] != DBNull.Value)
                    {

                        //List<cSucursalStocks> tempListaSucursalStocks = new List<cSucursalStocks>();
                        //tempListaSucursalStocks = (from r in tablaSucursalStocks.Select("stk_codpro = '" + item["pro_codigo"].ToString() + "'").AsEnumerable()
                        //                           select new cSucursalStocks { stk_codpro = r["stk_codpro"].ToString(), stk_codsuc = r["stk_codsuc"].ToString(), stk_stock = r["stk_stock"].ToString() }).ToList();
                        //if (tempListaSucursalStocks.Count > 0)
                        //{
                        cProductosBuscador obj = new cProductosBuscador();
                        obj.isProductoNoEncontrado = true;
                        obj.pro_codigo = "-1";
                        if (item["nombreNoEncontrado"] != DBNull.Value)
                        {
                            obj.pro_nombre = "<b> Código producto: </b>" + item["nombreNoEncontrado"].ToString();
                        }
                        string nombreVer = string.Empty;
                        if (item.Table.Columns.Contains("codigobarra"))
                        {
                            if (item["codigobarra"] != DBNull.Value)
                            {
                                if (item["codigobarra"].ToString() != string.Empty)
                                {
                                    nombreVer += "<b> Código Barra: </b>" + Convert.ToString(item["codigobarra"]);
                                }
                            }
                        }
                        if (item.Table.Columns.Contains("troquel"))
                        {
                            if (item["troquel"] != DBNull.Value)
                            {
                                if (item["troquel"].ToString() != string.Empty)
                                {
                                    nombreVer += "<b> Troquel: </b>" + Convert.ToString(item["troquel"]);
                                }
                            }
                        }
                        if (item.Table.Columns.Contains("codigoalfabeta"))
                        {
                            if (item["codigoalfabeta"] != DBNull.Value)
                            {
                                if (item["codigoalfabeta"].ToString() != string.Empty)
                                {
                                    nombreVer += "<b> Código Alfabeta: </b>" + Convert.ToString(item["codigoalfabeta"]);
                                }
                            }
                        }
                        if (nombreVer != string.Empty)
                        {
                            obj.pro_nombre = nombreVer;
                        }
                        if (item.Table.Columns.Contains("cantidad"))
                        {
                            if (item["cantidad"] != DBNull.Value)
                            {
                                obj.cantidad = Convert.ToInt32(item["cantidad"]);
                            }
                        }
                        if (item.Table.Columns.Contains("nroordenamiento"))
                        {
                            if (item["nroordenamiento"] != DBNull.Value)
                            {
                                obj.nroordenamiento = Convert.ToInt32(item["nroordenamiento"]);
                            }
                        }
                        //obj.listaSucursalStocks = tempListaSucursalStocks;
                        resultado.Add(obj);
                    }

                    //}

                }
                //////////////
            }
        }
        return resultado;
    }
    public static bool AgregarProductoAlCarrito(string pIdSucursal, string pIdProducto, int pCantidadProducto, int pIdCliente, int? pIdUsuario)
    {
        bool result = false;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            result = capaLogRegistro.AgregarProductoAlCarrito(pIdSucursal, pIdProducto, pCantidadProducto, pIdCliente, pIdUsuario);
        }
        return result;
    }
    public static bool AgregarProductoAlCarrito_TempSubirArchivo(string pIdSucursal, string pIdProducto, int pCantidadProducto, int pIdCliente, int? pIdUsuario)
    {
        return capaLogRegistro.AgregarProductoAlCarrito_TempSubirArchivo(pIdSucursal, pIdProducto, pCantidadProducto, pIdCliente, pIdUsuario);
    }
    public static void AgregarProductoAlCarritoDesdeRecuperador(string pIdSucursal, string pNombreProducto, int pCantidadProducto, int pIdCliente, int? pIdUsuario)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            capaLogRegistro.AgregarProductoAlCarritoDesdeRecuperador(pIdSucursal, pNombreProducto, pCantidadProducto, pIdCliente, pIdUsuario);
        }
    }
    public static bool CargarCarritoDiferido(string pIdSucursal, string pNombreProducto, int pCantidadProducto, int pIdCliente, int? pIdUsuario)
    {
        bool result = false;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            result = CapaCarritoDiferido.CargarCarritoDiferido(pIdSucursal, pNombreProducto, pCantidadProducto, pIdCliente, pIdUsuario);
        }
        return result;
    }
    public static List<cCarrito> RecuperarCarritosDiferidosPorCliente(int pIdCliente)
    {
        cClientes objClientes = WebService.RecuperarClientePorId(pIdCliente);
        DataSet dsProductoCarrito = CapaCarritoDiferido.RecuperarCarritosDiferidosPorCliente(pIdCliente);// capaLogRegistro.RecuperarCarritosPorSucursalYProductos(pIdCliente);
        List<cCarrito> listaSucursal = (from item in dsProductoCarrito.Tables[1].AsEnumerable()
                                        select new cCarrito { lrc_id = item.Field<int>("rcd_id"), codSucursal = item.Field<string>("rcd_codSucursal") }).ToList();
        foreach (cCarrito item in listaSucursal)
        {
            item.proximoHorarioEntrega = FuncionesPersonalizadas.ObtenerHorarioCierre(objClientes.cli_codsuc, item.codSucursal, objClientes.cli_codrep);
            List<cProductosEnCarrito> listaProductoCarrtios = (from itemProductoCarrtios in dsProductoCarrito.Tables[0].AsEnumerable()
                                                               where itemProductoCarrtios.Field<int>("rdd_codCarrito") == item.lrc_id
                                                               select new cProductosEnCarrito
                                                               {
                                                                   codProducto = itemProductoCarrtios.Field<string>("rdd_codProducto"),
                                                                   cantidad = itemProductoCarrtios.Field<int>("rdd_cantidad"),
                                                                   pro_nombre = itemProductoCarrtios.Field<string>("pro_nombre"),
                                                                   pro_precio = itemProductoCarrtios.Field<decimal>("pro_precio"),
                                                                   pro_preciofarmacia = itemProductoCarrtios.Field<decimal>("pro_preciofarmacia"),
                                                                   pro_neto = itemProductoCarrtios.Field<bool>("pro_neto"),
                                                                   pro_codtpopro = itemProductoCarrtios.Field<string>("pro_codtpopro"),
                                                                   pro_descuentoweb = itemProductoCarrtios.IsNull("pro_descuentoweb") ? 0 : itemProductoCarrtios.Field<decimal>("pro_descuentoweb"),
                                                                   pro_ofeunidades = itemProductoCarrtios.IsNull("pro_ofeunidades") ? 0 : itemProductoCarrtios.Field<int>("pro_ofeunidades"),
                                                                   pro_ofeporcentaje = itemProductoCarrtios.IsNull("pro_ofeporcentaje") ? 0 : itemProductoCarrtios.Field<decimal>("pro_ofeporcentaje"),
                                                                   stk_stock = itemProductoCarrtios.Field<string>("stk_stock"),
                                                                   pro_canmaxima = itemProductoCarrtios.IsNull("pro_canmaxima") ? (int?)null : itemProductoCarrtios.Field<int>("pro_canmaxima")
                                                               }).ToList();
            for (int iPrecioFinal = 0; iPrecioFinal < listaProductoCarrtios.Count; iPrecioFinal++)
            {
                listaProductoCarrtios[iPrecioFinal].PrecioFinal = FuncionesPersonalizadas.ObtenerPrecioFinal(objClientes, listaProductoCarrtios[iPrecioFinal]);
            }
            item.listaProductos = listaProductoCarrtios;
        }


        return listaSucursal;
    }
    public static bool BorrarCarrito(int lrc_id)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            return capaLogRegistro.BorrarCarrito(lrc_id);
        }
        else
        {
            return false;
        }
    }
    public static bool BorrarCarritoTransfer(int pIdCliente, string pSucursal)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            return capaTransfer.BorrarCarritoTransfer(pIdCliente, pSucursal);
        }
        else
        {
            return false;
        }
    }
    public static void BorrarCarritosDiferidos(int pIdCarrito)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            CapaCarritoDiferido.BorrarCarritosDiferidos(pIdCarrito);
        }
    }
    public static List<cCarrito> RecuperarCarritosPorSucursalYProductos(int pIdCliente)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            cClientes objClientes = WebService.RecuperarClientePorId(pIdCliente);
            DataSet dsProductoCarrito = capaLogRegistro.RecuperarCarritosPorSucursalYProductos(pIdCliente);

            List<cCarrito> listaSucursal = (from item in dsProductoCarrito.Tables[1].AsEnumerable()
                                            select new cCarrito { lrc_id = item.Field<int>("lrc_id"), codSucursal = item.Field<string>("lrc_codSucursal") }).ToList();

            foreach (cCarrito item in listaSucursal)
            {
                item.proximoHorarioEntrega = FuncionesPersonalizadas.ObtenerHorarioCierre(objClientes.cli_codsuc, item.codSucursal, objClientes.cli_codrep);
                //ObtenerHorarioCierre(((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codsuc, pIdSucursal, ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codrep);
                List<cProductosEnCarrito> listaProductoCarrtios = (from itemProductoCarrtios in dsProductoCarrito.Tables[0].AsEnumerable()
                                                                   where itemProductoCarrtios.Field<int>("lcp_codCarrito") == item.lrc_id
                                                                   select new cProductosEnCarrito
                                                                   {
                                                                       codProducto = itemProductoCarrtios.Field<string>("lcp_codProducto"),
                                                                       cantidad = itemProductoCarrtios.Field<int>("lcp_cantidad"),
                                                                       pro_nombre = itemProductoCarrtios.Field<string>("pro_nombre"),
                                                                       pro_precio = itemProductoCarrtios.Field<decimal>("pro_precio"),
                                                                       pro_preciofarmacia = itemProductoCarrtios.Field<decimal>("pro_preciofarmacia"),
                                                                       pro_neto = itemProductoCarrtios.Field<bool>("pro_neto"),
                                                                       pro_codtpopro = itemProductoCarrtios.Field<string>("pro_codtpopro"),
                                                                       pro_descuentoweb = itemProductoCarrtios.IsNull("pro_descuentoweb") ? 0 : itemProductoCarrtios.Field<decimal>("pro_descuentoweb"),
                                                                       pro_ofeunidades = itemProductoCarrtios.IsNull("pro_ofeunidades") ? 0 : itemProductoCarrtios.Field<int>("pro_ofeunidades"),
                                                                       pro_ofeporcentaje = itemProductoCarrtios.IsNull("pro_ofeporcentaje") ? 0 : itemProductoCarrtios.Field<decimal>("pro_ofeporcentaje"),
                                                                       stk_stock = itemProductoCarrtios.Field<string>("stk_stock"),
                                                                       pro_canmaxima = itemProductoCarrtios.IsNull("pro_canmaxima") ? (int?)null : itemProductoCarrtios.Field<int>("pro_canmaxima")
                                                                   }).ToList();
                /// Nuevo
                List<cTransferDetalle> listaTransferDetalle = null;
                if (dsProductoCarrito.Tables.Count > 2)
                {
                    listaTransferDetalle = new List<cTransferDetalle>();
                    DataTable tablaTransferDetalle = dsProductoCarrito.Tables[2];
                    foreach (DataRow itemTransferDetalle in tablaTransferDetalle.Rows)
                    {
                        // listaTransferDetalle.Add(ConvertToTransferDetalle(itemTransferDetalle));
                        cTransferDetalle objTransferDetalle = ConvertToTransferDetalle(itemTransferDetalle);
                        objTransferDetalle.CargarTransfer(ConvertToTransfer(itemTransferDetalle));
                        listaTransferDetalle.Add(objTransferDetalle);

                    }
                }
                /// FIN Nuevo
                for (int iPrecioFinal = 0; iPrecioFinal < listaProductoCarrtios.Count; iPrecioFinal++)
                {
                    listaProductoCarrtios[iPrecioFinal].PrecioFinal = FuncionesPersonalizadas.ObtenerPrecioFinal(objClientes, listaProductoCarrtios[iPrecioFinal]);
                    /// Nuevo
                    listaProductoCarrtios[iPrecioFinal].isProductoFacturacionDirecta = false;
                    if (listaTransferDetalle != null)
                    {
                        List<cTransferDetalle> listaAUXtransferDetalle = listaTransferDetalle.Where(x => x.tde_codpro == listaProductoCarrtios[iPrecioFinal].pro_nombre).ToList();
                        if (listaAUXtransferDetalle.Count > 0)
                        {
                            listaProductoCarrtios[iPrecioFinal].isProductoFacturacionDirecta = true;
                            listaProductoCarrtios[iPrecioFinal].CargarTransferYTransferDetalle(listaAUXtransferDetalle[0]);
                            if (HttpContext.Current.Session["clientesDefault_Cliente"] != null)
                            {
                                listaProductoCarrtios[iPrecioFinal].PrecioFinalTransfer = FuncionesPersonalizadas.ObtenerPrecioFinalTransferBase((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"], listaProductoCarrtios[iPrecioFinal].tfr_deshab, listaProductoCarrtios[iPrecioFinal].tfr_pordesadi, listaProductoCarrtios[iPrecioFinal].pro_neto, listaProductoCarrtios[iPrecioFinal].pro_codtpopro, listaProductoCarrtios[iPrecioFinal].pro_descuentoweb, listaProductoCarrtios[iPrecioFinal].tde_predescuento == null ? 0 : (decimal)listaProductoCarrtios[iPrecioFinal].tde_predescuento);
                            }
                        }
                    }
                    /// FIN Nuevo
                }
                item.listaProductos = listaProductoCarrtios;
            }
            return listaSucursal;
        }
        // sin no valida la credencial
        return null;
    }
    public static List<cCarritoTransfer> RecuperarCarritosTransferPorIdCliente(cClientes pCliente)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataSet dsProductoCarrito = capaTransfer.RecuperarCarritoTransferPorIdCliente(pCliente.cli_codigo);
            if (dsProductoCarrito.Tables.Count > 1)
            {
                List<cCarritoTransfer> listaSucursal = (from item in dsProductoCarrito.Tables[1].AsEnumerable()
                                                        select new cCarritoTransfer { ctr_id = item.Field<int>("ctr_id"), ctr_codSucursal = item.Field<string>("ctr_codSucursal"), tfr_codigo = item.Field<int>("tfr_codigo"), tfr_nombre = item.Field<string>("tfr_nombre"), tfr_deshab = item.Field<Boolean>("tfr_deshab"), tfr_pordesadi = item.IsNull("tfr_pordesadi") ? null : (decimal?)item.Field<decimal>("tfr_pordesadi"), tfr_tipo = item.Field<string>("tfr_tipo") }).OrderBy(x => x.ctr_codSucursal).ToList();


                foreach (cCarritoTransfer item in listaSucursal)
                {
                    List<cProductosEnCarrito> listaProductoCarrtios = (from itemProductoCarrtios in dsProductoCarrito.Tables[0].AsEnumerable()
                                                                       where itemProductoCarrtios.Field<int>("ctd_idCarritoTransfers") == item.ctr_id
                                                                       select new cProductosEnCarrito
                                                                       {
                                                                           codProducto = itemProductoCarrtios.Field<string>("ctd_codProducto"),
                                                                           cantidad = itemProductoCarrtios.Field<int>("ctd_Cantidad"),
                                                                           pro_nombre = itemProductoCarrtios.Field<string>("pro_nombre"),
                                                                           tde_codpro = itemProductoCarrtios.Field<string>("pro_nombre"),
                                                                           pro_precio = itemProductoCarrtios.Field<decimal>("pro_precio"),
                                                                           pro_preciofarmacia = itemProductoCarrtios.Field<decimal>("pro_preciofarmacia"),
                                                                           pro_neto = itemProductoCarrtios.Field<bool>("pro_neto"),
                                                                           pro_codtpopro = itemProductoCarrtios.Field<string>("pro_codtpopro"),
                                                                           pro_descuentoweb = itemProductoCarrtios.IsNull("pro_descuentoweb") ? 0 : itemProductoCarrtios.Field<decimal>("pro_descuentoweb"),
                                                                           pro_ofeunidades = itemProductoCarrtios.IsNull("pro_ofeunidades") ? 0 : itemProductoCarrtios.Field<int>("pro_ofeunidades"),
                                                                           pro_ofeporcentaje = itemProductoCarrtios.IsNull("pro_ofeporcentaje") ? 0 : itemProductoCarrtios.Field<decimal>("pro_ofeporcentaje"),
                                                                           tde_prepublico = itemProductoCarrtios.IsNull("tde_prepublico") ? 0 : itemProductoCarrtios.Field<decimal>("tde_prepublico"),
                                                                           tde_predescuento = itemProductoCarrtios.IsNull("tde_predescuento") ? 0 : itemProductoCarrtios.Field<decimal>("tde_predescuento"),
                                                                           PrecioFinalTransfer = FuncionesPersonalizadas.ObtenerPrecioFinalTransferBase(pCliente, item.tfr_deshab, item.tfr_pordesadi, itemProductoCarrtios.Field<bool>("pro_neto"), itemProductoCarrtios.Field<string>("pro_codtpopro"), itemProductoCarrtios.IsNull("pro_descuentoweb") ? 0 : itemProductoCarrtios.Field<decimal>("pro_descuentoweb"), itemProductoCarrtios.Field<decimal>("tde_predescuento"))
                                                                       }).ToList();
                    item.listaProductos = listaProductoCarrtios;
                }
                return listaSucursal;
            }
            else
            {
                return new List<cCarritoTransfer>();
            }
        }
        // sin no valida la credencial
        return null;
    }
    public static List<cSucursalCarritoTransfer> RecuperarCarritosTransferPorIdClienteOrdenadosPorSucursal(cClientes pCliente)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {

            DataSet dsProductoCarrito = capaTransfer.RecuperarCarritoTransferPorIdCliente(pCliente.cli_codigo);
            if (dsProductoCarrito.Tables.Count > 1)
            {
                List<cSucursalCarritoTransfer> resultado = new List<cSucursalCarritoTransfer>();
                List<cCarritoTransfer> listaSucursal = (from item in dsProductoCarrito.Tables[1].AsEnumerable()
                                                        select new cCarritoTransfer { ctr_id = item.Field<int>("ctr_id"), ctr_codSucursal = item.Field<string>("ctr_codSucursal"), tfr_codigo = item.Field<int>("tfr_codigo"), tfr_nombre = item.Field<string>("tfr_nombre"), tfr_deshab = item.Field<Boolean>("tfr_deshab"), tfr_pordesadi = item.IsNull("tfr_pordesadi") ? null : (decimal?)item.Field<decimal>("tfr_pordesadi"), tfr_tipo = item.Field<string>("tfr_tipo") }).OrderBy(x => x.ctr_codSucursal).ToList();

                /// Nuevo
                List<cTransferDetalle> listaTransferDetalle = null;
                if (dsProductoCarrito.Tables.Count > 2)
                {
                    listaTransferDetalle = new List<cTransferDetalle>();
                    DataTable tablaTransferDetalle = dsProductoCarrito.Tables[2];
                    foreach (DataRow itemTransferDetalle in tablaTransferDetalle.Rows)
                    {
                        cTransferDetalle objTransferDetalle = ConvertToTransferDetalle(itemTransferDetalle);
                        objTransferDetalle.CargarTransfer(ConvertToTransfer(itemTransferDetalle));
                        listaTransferDetalle.Add(objTransferDetalle);

                    }
                }
                /// FIN Nuevo


                foreach (cCarritoTransfer item in listaSucursal)
                {
                    List<cProductosEnCarrito> listaProductoCarrtios = (from itemProductoCarrtios in dsProductoCarrito.Tables[0].AsEnumerable()
                                                                       where itemProductoCarrtios.Field<int>("ctd_idCarritoTransfers") == item.ctr_id
                                                                       select new cProductosEnCarrito
                                                                       {
                                                                           codProducto = itemProductoCarrtios.Field<string>("ctd_codProducto"),
                                                                           cantidad = itemProductoCarrtios.Field<int>("ctd_Cantidad"),
                                                                           pro_nombre = itemProductoCarrtios.Field<string>("pro_nombre"),
                                                                           tde_codpro = itemProductoCarrtios.Field<string>("pro_nombre"),
                                                                           pro_precio = itemProductoCarrtios.Field<decimal>("pro_precio"),
                                                                           pro_preciofarmacia = itemProductoCarrtios.Field<decimal>("pro_preciofarmacia"),
                                                                           pro_neto = itemProductoCarrtios.Field<bool>("pro_neto"),
                                                                           pro_codtpopro = itemProductoCarrtios.Field<string>("pro_codtpopro"),
                                                                           pro_descuentoweb = itemProductoCarrtios.IsNull("pro_descuentoweb") ? 0 : itemProductoCarrtios.Field<decimal>("pro_descuentoweb"),
                                                                           pro_ofeunidades = itemProductoCarrtios.IsNull("pro_ofeunidades") ? 0 : itemProductoCarrtios.Field<int>("pro_ofeunidades"),
                                                                           pro_ofeporcentaje = itemProductoCarrtios.IsNull("pro_ofeporcentaje") ? 0 : itemProductoCarrtios.Field<decimal>("pro_ofeporcentaje"),
                                                                           tde_prepublico = itemProductoCarrtios.IsNull("tde_prepublico") ? 0 : itemProductoCarrtios.Field<decimal>("tde_prepublico"),
                                                                           tde_predescuento = itemProductoCarrtios.IsNull("tde_predescuento") ? 0 : itemProductoCarrtios.Field<decimal>("tde_predescuento"),
                                                                           PrecioFinalTransfer = FuncionesPersonalizadas.ObtenerPrecioFinalTransferBase(pCliente, item.tfr_deshab, item.tfr_pordesadi, itemProductoCarrtios.Field<bool>("pro_neto"), itemProductoCarrtios.Field<string>("pro_codtpopro"), itemProductoCarrtios.IsNull("pro_descuentoweb") ? 0 : itemProductoCarrtios.Field<decimal>("pro_descuentoweb"), itemProductoCarrtios.Field<decimal>("tde_predescuento"))
                                                                       }).ToList();

                    for (int iProductoCarrtios = 0; iProductoCarrtios < listaProductoCarrtios.Count; iProductoCarrtios++)
                    {
                        listaProductoCarrtios[iProductoCarrtios].isProductoFacturacionDirecta = false;
                        if (listaTransferDetalle != null)
                        {
                            List<cTransferDetalle> listaAUXtransferDetalle = listaTransferDetalle.Where(x => x.tde_codpro == listaProductoCarrtios[iProductoCarrtios].pro_nombre && x.tfr_codigo == item.tfr_codigo).ToList();
                            if (listaAUXtransferDetalle.Count > 0)
                            {
                                listaProductoCarrtios[iProductoCarrtios].isProductoFacturacionDirecta = true;
                                listaProductoCarrtios[iProductoCarrtios].CargarTransferYTransferDetalle(listaAUXtransferDetalle[0]);
                            }
                        }

                    }
                    //List<string> ListaSucursal = FuncionesPersonalizadas.RecuperarSucursalesParaBuscadorDeCliente();
                    //FuncionesPersonalizadas.ActualizarStockListaProductos(ListaSucursal, listaProductoCarrtios);
                    item.listaProductos = listaProductoCarrtios;


                    bool isAgregoResultado = false;
                    for (int i = 0; i < resultado.Count; i++)
                    {
                        if (resultado[i].Sucursal == item.ctr_codSucursal)
                        {
                            resultado[i].listaTransfer.Add(item);
                            isAgregoResultado = true;
                            break;
                        }
                    }
                    if (!isAgregoResultado)
                    {
                        cSucursalCarritoTransfer obj = new cSucursalCarritoTransfer();
                        obj.listaTransfer = new List<cCarritoTransfer>();
                        obj.Sucursal = item.ctr_codSucursal;
                        obj.proximoHorarioEntrega = FuncionesPersonalizadas.ObtenerHorarioCierre(pCliente.cli_codsuc, item.ctr_codSucursal, pCliente.cli_codrep);
                        obj.listaTransfer.Add(item);
                        resultado.Add(obj);
                    }
                }
                return resultado;
            }
            else
            {
                return new List<cSucursalCarritoTransfer>();
            }
        }
        // sin no valida la credencial
        return null;
    }
    public static int InsertarActualizarSucursal(int sde_codigo, string sde_sucursal, string sde_sucursalDependiente)
    {
        int resultado = -1;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            string accion = sde_codigo == 0 ? Constantes.cSQL_INSERT : Constantes.cSQL_UPDATE;
            DataSet dsResultado = capaClientes.GestiónSucursal(sde_codigo, sde_sucursal, sde_sucursalDependiente, accion);
            if (sde_codigo == 0)
            {
                if (dsResultado != null)
                {
                    if (dsResultado.Tables["Sucursal"].Rows[0]["sde_codigo"] != DBNull.Value)
                    {
                        resultado = Convert.ToInt32(dsResultado.Tables["Sucursal"].Rows[0]["sde_codigo"]);
                    }
                }
            }
            else
            {
                resultado = sde_codigo;
            }
        }
        return resultado;
    }
    public static List<cSucursal> RecuperarTodasSucursalesDependientes()
    {
        List<cSucursal> resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataSet dsResultado = capaClientes.GestiónSucursal(null, null, null, Constantes.cSQL_SELECT);

            if (dsResultado != null)
            {
                resultado = new List<cSucursal>();
                for (int i = 0; i < dsResultado.Tables["Sucursal"].Rows.Count; i++)
                {
                    cSucursal obj = new cSucursal();
                    if (dsResultado.Tables["Sucursal"].Rows[i]["sde_codigo"] != DBNull.Value)
                    {
                        obj.sde_codigo = Convert.ToInt32(dsResultado.Tables["Sucursal"].Rows[i]["sde_codigo"]);
                    }
                    if (dsResultado.Tables["Sucursal"].Rows[i]["sde_sucursal"] != DBNull.Value)
                    {
                        obj.sde_sucursal = dsResultado.Tables["Sucursal"].Rows[i]["sde_sucursal"].ToString();
                    }
                    if (dsResultado.Tables["Sucursal"].Rows[i]["sde_sucursalDependiente"] != DBNull.Value)
                    {
                        obj.sde_sucursalDependiente = dsResultado.Tables["Sucursal"].Rows[i]["sde_sucursalDependiente"].ToString();
                    }
                    if (dsResultado.Tables["Sucursal"].Rows[i]["sde_sucursalDependiente"] != DBNull.Value && dsResultado.Tables["Sucursal"].Rows[i]["sde_sucursal"] != DBNull.Value)
                    {
                        obj.sucursal_sucursalDependiente = obj.sde_sucursal + " - " + obj.sde_sucursalDependiente;
                    }
                    resultado.Add(obj);
                }

            }
        }
        return resultado;
    }
    public static bool AgregarMontoMinimo(string suc_codigo, decimal suc_montoMinimo)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            return capaClientes.AgregarMontoMinimo(suc_codigo, suc_montoMinimo);
        }
        else
        {
            return false;
        }
    }
    public static List<cSucursal> RecuperarTodasSucursalesSinCategoriaRestriccion()
    {
        List<cSucursal> resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            List<cCadeteriaRestricciones> listaCadeteriaRestricciones = RecuperarTodosCadeteriaRestricciones();
            DataTable tabla = capaClientes.RecuperarTodasSucursales();

            if (tabla != null)
            {
                resultado = new List<cSucursal>();
                for (int i = 0; i < tabla.Rows.Count; i++)
                {
                    bool isAgregar = true;
                    if (tabla.Rows[i]["suc_codigo"] != DBNull.Value)
                    {
                        string codSucursal = tabla.Rows[i]["suc_codigo"].ToString();
                        if (listaCadeteriaRestricciones.Where(x => x.tcr_codigoSucursal == codSucursal).Count() > 0)
                        {
                            isAgregar = false;
                        }
                    }
                    if (isAgregar)
                    {
                        cSucursal obj = new cSucursal();
                        if (tabla.Rows[i]["suc_codigo"] != DBNull.Value)
                        {
                            obj.suc_codigo = tabla.Rows[i]["suc_codigo"].ToString();
                            obj.sde_sucursal = tabla.Rows[i]["suc_codigo"].ToString();
                        }
                        if (tabla.Rows[i]["suc_nombre"] != DBNull.Value)
                        {
                            obj.suc_nombre = tabla.Rows[i]["suc_nombre"].ToString();
                        }
                        if (tabla.Rows[i]["suc_montoMinimo"] != DBNull.Value)
                        {
                            obj.suc_montoMinimo = Convert.ToDecimal(tabla.Rows[i]["suc_montoMinimo"]);
                        }
                        resultado.Add(obj);
                    }
                }

            }
        }
        return resultado;
    }
    public static List<cSucursal> RecuperarTodasSucursales()
    {
        List<cSucursal> resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataTable tabla = capaClientes.RecuperarTodasSucursales();

            if (tabla != null)
            {
                resultado = new List<cSucursal>();
                for (int i = 0; i < tabla.Rows.Count; i++)
                {
                    cSucursal obj = new cSucursal();
                    if (tabla.Rows[i]["suc_codigo"] != DBNull.Value)
                    {
                        obj.suc_codigo = tabla.Rows[i]["suc_codigo"].ToString();
                        obj.sde_sucursal = tabla.Rows[i]["suc_codigo"].ToString();
                    }
                    if (tabla.Rows[i]["suc_nombre"] != DBNull.Value)
                    {
                        obj.suc_nombre = tabla.Rows[i]["suc_nombre"].ToString();
                    }
                    if (tabla.Rows[i]["suc_montoMinimo"] != DBNull.Value)
                    {
                        obj.suc_montoMinimo = Convert.ToDecimal(tabla.Rows[i]["suc_montoMinimo"]);
                    }
                    resultado.Add(obj);
                }

            }
        }
        return resultado;
    }

    public static void EliminarSucursal(int sde_codigo)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataSet dsResultado = capaClientes.GestiónSucursal(sde_codigo, null, null, Constantes.cSQL_DELETE);
        }
    }
    private static cTransfer ConvertToTransfer(DataRow pItem)
    {
        cTransfer obj = new cTransfer();
        if (pItem["tfr_codigo"] != DBNull.Value)
        {
            obj.tfr_codigo = Convert.ToInt32(pItem["tfr_codigo"]);
        }
        if (pItem["tfr_accion"] != DBNull.Value)
        {
            obj.tfr_accion = pItem["tfr_accion"].ToString();
        }
        if (pItem["tfr_descripcion"] != DBNull.Value)
        {
            obj.tfr_descripcion = pItem["tfr_descripcion"].ToString();
        }
        if (pItem["tfr_deshab"] != DBNull.Value)
        {
            obj.tfr_deshab = Convert.ToBoolean(pItem["tfr_deshab"]);
        }
        if (pItem["tfr_nombre"] != DBNull.Value)
        {
            obj.tfr_nombre = pItem["tfr_nombre"].ToString();
        }
        if (pItem["tfr_tipo"] != DBNull.Value)
        {
            obj.tfr_tipo = pItem["tfr_tipo"].ToString();
        }
        if (pItem["tfr_mospap"] != DBNull.Value)
        {
            obj.tfr_mospap = Convert.ToBoolean(pItem["tfr_mospap"]);
        }
        if (pItem["tfr_fijunidades"] != DBNull.Value)
        {
            obj.tfr_fijunidades = Convert.ToInt32(pItem["tfr_fijunidades"]);
        }
        if (pItem["tfr_maxunidades"] != DBNull.Value)
        {
            obj.tfr_maxunidades = Convert.ToInt32(pItem["tfr_maxunidades"]);
        }
        if (pItem["tfr_mulunidades"] != DBNull.Value)
        {
            obj.tfr_mulunidades = Convert.ToInt32(pItem["tfr_mulunidades"]);
        }
        if (pItem["tfr_minrenglones"] != DBNull.Value)
        {
            obj.tfr_minrenglones = Convert.ToInt32(pItem["tfr_minrenglones"]);
        }
        if (pItem["tfr_minunidades"] != DBNull.Value)
        {
            obj.tfr_minunidades = Convert.ToInt32(pItem["tfr_minunidades"]);
        }
        if (pItem["tfr_pordesadi"] != DBNull.Value)
        {
            obj.tfr_pordesadi = Convert.ToDecimal(pItem["tfr_pordesadi"]);
        }
        else
        {
            obj.tfr_pordesadi = 0;
        }
        if (pItem.Table.Columns.Contains("tfr_facturaciondirecta"))
        {
            if (pItem["tfr_facturaciondirecta"] != DBNull.Value)
            {
                obj.tfr_facturaciondirecta = Convert.ToBoolean(pItem["tfr_facturaciondirecta"]);
            }
        }
        if (pItem.Table.Columns.Contains("tfr_provincia"))
        {
            if (pItem["tfr_provincia"] != DBNull.Value)
            {
                obj.tfr_provincia = Convert.ToString(pItem["tfr_provincia"]);
            }
        }
        return obj;
    }
    private static cTransferDetalle ConvertToTransferDetalle(DataRow pItem)
    {
        cTransferDetalle obj = new cTransferDetalle();
        if (pItem["tde_codpro"] != DBNull.Value)
        {
            obj.tde_codpro = pItem["tde_codpro"].ToString();
        }
        if (pItem["tde_codtfr"] != DBNull.Value)
        {
            obj.tde_codtfr = Convert.ToInt32(pItem["tde_codtfr"]);
        }
        if (pItem["tde_descripcion"] != DBNull.Value)
        {
            obj.tde_descripcion = pItem["tde_descripcion"].ToString();
        }
        if (pItem["tde_fijuni"] != DBNull.Value)
        {
            obj.tde_fijuni = Convert.ToInt32(pItem["tde_fijuni"]);
        }
        if (pItem["tde_maxuni"] != DBNull.Value)
        {
            obj.tde_maxuni = Convert.ToInt32(pItem["tde_maxuni"]);
        }
        if (pItem["tde_minuni"] != DBNull.Value)
        {
            obj.tde_minuni = Convert.ToInt32(pItem["tde_minuni"]);
        }
        if (pItem["tde_muluni"] != DBNull.Value)
        {
            obj.tde_muluni = Convert.ToInt32(pItem["tde_muluni"]);
        }
        if (pItem["tde_predescuento"] != DBNull.Value)
        {
            obj.tde_predescuento = Convert.ToDecimal(pItem["tde_predescuento"]);
        }
        if (pItem["tde_prepublico"] != DBNull.Value)
        {
            obj.tde_prepublico = Convert.ToDecimal(pItem["tde_prepublico"]);
        }
        if (pItem["tde_proobligatorio"] != DBNull.Value)
        {
            obj.tde_proobligatorio = Convert.ToBoolean(pItem["tde_proobligatorio"]);
        }
        if (pItem.Table.Columns.Contains("pro_neto"))
        {
            if (pItem["pro_neto"] != DBNull.Value)
            {
                obj.pro_neto = Convert.ToBoolean(pItem["pro_neto"]);
            }
        }
        if (pItem.Table.Columns.Contains("pro_codtpopro"))
        {
            if (pItem["pro_codtpopro"] != DBNull.Value)
            {
                obj.pro_codtpopro = Convert.ToString(pItem["pro_codtpopro"]);
            }
        }
        if (pItem.Table.Columns.Contains("pro_codigo"))
        {
            if (pItem["pro_codigo"] != DBNull.Value)
            {
                obj.pro_codigo = Convert.ToString(pItem["pro_codigo"]);
            }
        }
        if (pItem.Table.Columns.Contains("pro_descuentoweb"))
        {
            if (pItem["pro_descuentoweb"] != DBNull.Value)
            {
                obj.pro_descuentoweb = Convert.ToDecimal(pItem["pro_descuentoweb"]);
            }
        }
        if (pItem.Table.Columns.Contains("pro_isTrazable"))
        {
            if (pItem["pro_isTrazable"] != DBNull.Value)
            {
                obj.pro_isTrazable = Convert.ToBoolean(pItem["pro_isTrazable"]);
            }
        }
        if (pItem.Table.Columns.Contains("tde_unidadesbonificadas"))
        {
            if (pItem["tde_unidadesbonificadas"] != DBNull.Value)
            {
                obj.tde_unidadesbonificadas = Convert.ToInt32(pItem["tde_unidadesbonificadas"]);
            }
        }
        if (pItem.Table.Columns.Contains("tde_unidadesbonificadasdescripcion"))
        {
            if (pItem["tde_unidadesbonificadasdescripcion"] != DBNull.Value)
            {
                obj.tde_unidadesbonificadasdescripcion = Convert.ToString(pItem["tde_unidadesbonificadasdescripcion"]);
            }
        }

        return obj;
    }
    public static List<cTransfer> RecuperarTodosTransferMasDetallePorIdProducto(string pNombreProducto, cClientes pClientes)
    {
        List<cTransfer> resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataSet dsResultado = capaTransfer.RecuperarTodosTransferMasDetallePorIdProducto(pClientes.cli_codsuc, pNombreProducto);

            if (dsResultado != null)
            {
                resultado = new List<cTransfer>();
                DataTable tbTransfer = dsResultado.Tables[0];
                DataTable tbTransferDetalle = dsResultado.Tables[1];
                DataTable tbSucursalStocks = dsResultado.Tables[2];
                for (int i = 0; i < tbTransfer.Rows.Count; i++)
                {
                    cTransfer obj = ConvertToTransfer(tbTransfer.Rows[i]);
                    if (obj.tfr_provincia == null || obj.tfr_provincia == pClientes.cli_codprov)
                    {
                        obj.listaDetalle = new List<cTransferDetalle>();
                        DataRow[] listaFila = tbTransferDetalle.Select("tde_codtfr =" + obj.tfr_codigo);
                        foreach (DataRow item in listaFila)
                        {
                            List<cSucursalStocks> tempListaSucursalStocks = new List<cSucursalStocks>();
                            tempListaSucursalStocks = (from r in tbSucursalStocks.Select("stk_codpro = '" + item["pro_codigo"].ToString() + "'").AsEnumerable()
                                                       select new cSucursalStocks { stk_codpro = r["stk_codpro"].ToString(), stk_codsuc = r["stk_codsuc"].ToString(), stk_stock = r["stk_stock"].ToString() }).ToList();
                            if (tempListaSucursalStocks.Count > 0)
                            {
                                obj.listaDetalle.Add(ConvertToTransferDetalle(item));//
                                obj.listaDetalle[obj.listaDetalle.Count - 1].PrecioFinalTransfer = FuncionesPersonalizadas.ObtenerPrecioFinalTransfer(pClientes, obj, obj.listaDetalle[obj.listaDetalle.Count - 1]);
                                obj.listaDetalle[obj.listaDetalle.Count - 1].listaSucursalStocks = FuncionesPersonalizadas.ActualizarStockListaProductos_Transfer(item["pro_codigo"].ToString(), tempListaSucursalStocks);
                                //obj.listaDetalle[obj.listaDetalle.Count - 1].listaSucursalStocks = tempListaSucursalStocks;
                            }
                        }
                        resultado.Add(obj);
                    }
                }

            }
        }
        return resultado;
    }
    public static List<cTransfer> RecuperarTodosTransfer()
    {
        List<cTransfer> resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataSet dsResultado = capaTransfer.RecuperarTodosTransfer();

            if (dsResultado != null)
            {
                resultado = new List<cTransfer>();
                DataTable tbTransfer = dsResultado.Tables[0];

                for (int i = 0; i < tbTransfer.Rows.Count; i++)
                {
                    cTransfer obj = ConvertToTransfer(tbTransfer.Rows[i]);
                    resultado.Add(obj);
                }

            }
        }
        return resultado;
    }
    public static cTransfer RecuperarTransferMasDetallePorIdTransfer(int pIdTransfer, cClientes pClientes)
    {
        cTransfer resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataSet dsResultado = capaTransfer.RecuperarTransferMasDetallePorIdTransfer(pClientes.cli_codsuc, pIdTransfer);

            if (dsResultado != null)
            {
                resultado = new cTransfer();
                DataTable tbTransfer = dsResultado.Tables[0];
                DataTable tbTransferDetalle = dsResultado.Tables[1];
                DataTable tbSucursalStocks = dsResultado.Tables[2];

                for (int i = 0; i < tbTransfer.Rows.Count; i++)
                {
                    resultado = ConvertToTransfer(tbTransfer.Rows[i]);
                    //cTransfer obj = ConvertToTransfer(tbTransfer.Rows[i]);
                    resultado.listaDetalle = new List<cTransferDetalle>();
                    DataRow[] listaFila = tbTransferDetalle.Select("tde_codtfr =" + resultado.tfr_codigo);
                    foreach (DataRow item in listaFila)
                    {
                        List<cSucursalStocks> tempListaSucursalStocks = new List<cSucursalStocks>();
                        tempListaSucursalStocks = (from r in tbSucursalStocks.Select("stk_codpro = '" + item["pro_codigo"].ToString() + "'").AsEnumerable()
                                                   select new cSucursalStocks { stk_codpro = r["stk_codpro"].ToString(), stk_codsuc = r["stk_codsuc"].ToString(), stk_stock = r["stk_stock"].ToString() }).ToList();
                        if (tempListaSucursalStocks.Count > 0)
                        {
                            resultado.listaDetalle.Add(ConvertToTransferDetalle(item));
                            resultado.listaDetalle[resultado.listaDetalle.Count - 1].PrecioFinalTransfer = FuncionesPersonalizadas.ObtenerPrecioFinalTransfer(pClientes, resultado, resultado.listaDetalle[resultado.listaDetalle.Count - 1]);
                            resultado.listaDetalle[resultado.listaDetalle.Count - 1].listaSucursalStocks = FuncionesPersonalizadas.ActualizarStockListaProductos_Transfer(item["pro_codigo"].ToString(), tempListaSucursalStocks);
                            //resultado.listaDetalle[resultado.listaDetalle.Count - 1].listaSucursalStocks = tempListaSucursalStocks;
                        }
                    }
                    //resultado = obj;
                }

            }
        }
        return resultado;
    }
    public static bool AgregarProductosTransfersAlCarrito(List<cProductosAndCantidad> pListaProductosMasCantidad, int pIdCliente, int pIdUsuario, int pIdTransfers, string pCodSucursal)
    {
        bool resultado = false;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataTable pTablaDetalle = FuncionesPersonalizadas.ConvertProductosAndCantidadToDataTable(pListaProductosMasCantidad);
            resultado = capaTransfer.AgregarProductosTransfersAlCarrito(pTablaDetalle, pIdCliente, pIdUsuario, pIdTransfers, pCodSucursal);
        }
        return resultado;
    }
    public static bool AgregarProductosTransfersAlCarrito_TempSubirArchivo(List<cProductosAndCantidad> pListaProductosMasCantidad, int pIdCliente, int pIdUsuario, int pIdTransfers, string pCodSucursal)
    {
        DataTable pTablaDetalle = FuncionesPersonalizadas.ConvertProductosAndCantidadToDataTable(pListaProductosMasCantidad);
        return capaTransfer.AgregarProductosTransfersAlCarrito_TempSubirArchivo(pTablaDetalle, pIdCliente, pIdUsuario, pIdTransfers, pCodSucursal);
    }
    private static cMensaje ConvertToMensaje(DataRow pItem)
    {
        cMensaje obj = new cMensaje();
        if (pItem["tme_codigo"] != DBNull.Value)
        {
            obj.tme_codigo = Convert.ToInt32(pItem["tme_codigo"]);
        }
        if (pItem["tme_fecha"] != DBNull.Value)
        {
            obj.tme_fecha = Convert.ToDateTime(pItem["tme_fecha"]);
            obj.tme_fechaToString = Convert.ToDateTime(pItem["tme_fecha"]).ToShortDateString();
        }
        if (pItem["tme_asunto"] != DBNull.Value)
        {
            obj.tme_asunto = pItem["tme_asunto"].ToString();
        }
        if (pItem["tme_mensaje"] != DBNull.Value)
        {
            obj.tme_mensaje = pItem["tme_mensaje"].ToString();
        }
        if (pItem["tme_codUsuario"] != DBNull.Value)
        {
            obj.tme_codUsuario = Convert.ToInt32(pItem["tme_codUsuario"]);
        }
        if (pItem["tme_estado"] != DBNull.Value)
        {
            obj.tme_estado = Convert.ToInt32(pItem["tme_estado"]);
        }
        if (pItem["tme_codClienteDestinatario"] != DBNull.Value)
        {
            obj.tme_codClienteDestinatario = Convert.ToInt32(pItem["tme_codClienteDestinatario"]);
        }
        if (pItem.Table.Columns.Contains("est_nombre"))
        {
            if (pItem["est_nombre"] != DBNull.Value)
            {
                obj.est_nombre = Convert.ToString(pItem["est_nombre"]);
            }
        }

        if (pItem.Table.Columns.Contains("tme_importante"))
        {
            if (pItem["tme_importante"] != DBNull.Value)
            {
                obj.tme_importante = Convert.ToBoolean(pItem["tme_importante"]);
            }
        }
        obj.tme_fechaDesde = DateTime.MinValue;
        if (pItem.Table.Columns.Contains("tme_fechaDesde"))
        {
            if (pItem["tme_fechaDesde"] != DBNull.Value)
            {
                obj.tme_fechaDesde = Convert.ToDateTime(pItem["tme_fechaDesde"]);
            }
        }
        obj.tme_fechaHasta = DateTime.MinValue;
        if (pItem.Table.Columns.Contains("tme_fechaHasta"))
        {
            if (pItem["tme_fechaHasta"] != DBNull.Value)
            {
                obj.tme_fechaHasta = Convert.ToDateTime(pItem["tme_fechaHasta"]);
            }
        }
        if (obj.tme_importante)
        {
            obj.tme_fechaDesdeToString = ((DateTime)obj.tme_fechaDesde).ToShortDateString();
            obj.tme_fechaHastaToString = ((DateTime)obj.tme_fechaHasta).ToShortDateString();
            obj.tme_importanteToString = "Si";
        }
        else
        {
            obj.tme_importanteToString = "No";
        }
        obj.tme_todos = null;
        if (pItem.Table.Columns.Contains("tme_todos"))
        {
            if (pItem["tme_todos"] != DBNull.Value)
            {
                obj.tme_todos = Convert.ToInt32(pItem["tme_todos"]);
            }
        }
        if (pItem.Table.Columns.Contains("tme_todosSucursales"))
        {
            if (pItem["tme_todosSucursales"] != DBNull.Value)
            {
                obj.tme_todosSucursales = Convert.ToInt32(pItem["tme_todosSucursales"]);
            }
        }
        return obj;
    }
    public static List<cMensaje> RecuperarTodosMensaje()
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            List<cMensaje> lista = new List<cMensaje>();
            DataTable tabla = capaMensaje.RecuperartTodosMensajes();
            if (tabla != null)
            {
                foreach (DataRow item in tabla.Rows)
                {
                    cMensaje obj = ConvertToMensaje(item);
                    if (item["cli_nombre"] != DBNull.Value)
                    {
                        obj.cli_nombre = Convert.ToString(item["cli_nombre"]);
                    }
                    lista.Add(obj);
                }
            }
            return lista;
        }
        else
        {
            return null;
        }
    }
    public static cMensaje RecuperarMensajePorId(int pIdMensaje)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataTable tabla = capaMensaje.RecuperarMensajePorId(pIdMensaje);
            if (tabla != null)
            {
                foreach (DataRow item in tabla.Rows)
                {
                    return ConvertToMensaje(item);
                }
            }
            return null;
        }
        else
        {
            return null;
        }
    }
    public static List<string> ObtenerTodasSucursalesPorMensajeSucursalId(int pTodosSucursales)
    {

        if (VerificarPermisos(CredencialAutenticacion))
        {
            List<string> lista = new List<string>();
            DataTable tabla = capaMensaje.ObtenerTodasSucursalesPorMensajeSucursalId(pTodosSucursales);
            if (tabla != null)
            {
                foreach (DataRow item in tabla.Rows)
                {
                    if (item.Table.Columns.Contains("cli_codsuc"))
                    {
                        if (item["cli_codsuc"] != DBNull.Value)
                        {
                            lista.Add(Convert.ToString(item["cli_codsuc"]));
                        }
                    }
                }
            }
            return lista;
        }
        else
        {
            return null;
        }
    }
    public static List<cMensaje> RecuperarTodosMensajesPorIdCliente(int pIdCliente)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            List<cMensaje> resultado = new List<cMensaje>();
            DataTable tabla = capaMensaje.RecuperartTodosMensajesPorIdCliente(pIdCliente);
            if (tabla != null)
            {
                foreach (DataRow item in tabla.Rows)
                {
                    resultado.Add(ConvertToMensaje(item));
                }
            }
            return resultado;
        }
        else
        {
            return null;
        }
    }
    public static int InsertarActualizarMensaje(int pIdMensaje, string pAsunto, string pMensaje, int pIdCliente, int pIdUsuario, int pIdEstado, DateTime? pFechaDesde, DateTime? pFechaHasta, bool pImportante)
    {
        return capaMensaje.InsertarActualizarMensaje(pIdMensaje, pAsunto, pMensaje, pIdCliente, pIdUsuario, pIdEstado, pFechaDesde, pFechaHasta, pImportante);
    }
    public static bool InsertarMensajeParaTodosClientes(int pIdMensaje, string pAsunto, string pMensaje, int pIdUsuario, int pIdEstado, DateTime? pFechaDesde, DateTime? pFechaHasta, bool pImportante, int tme_todos)
    {
        return capaMensaje.InsertarMensajeParaTodosClientes(pIdMensaje, pAsunto, pMensaje, pIdUsuario, pIdEstado, pFechaDesde, pFechaHasta, pImportante, tme_todos);
    }
    public static bool InsertarActualizarMensajeParaTodasSucursales(int pIdMensaje, string pAsunto, string pMensaje, int pIdUsuario, int pIdEstado, DateTime? pFechaDesde, DateTime? pFechaHasta, bool pImportante, int tme_todosSucursales, List<string> pListaSucursal)
    {
        return capaMensaje.InsertarActualizarMensajeParaTodasSucursales(pIdMensaje, pAsunto, pMensaje, pIdUsuario, pIdEstado, pFechaDesde, pFechaHasta, pImportante, tme_todosSucursales, pListaSucursal);
    }
    public static void CambiarEstadoMensajePorId(int pIdMensaje, int pIdEstado)
    {
        capaMensaje.CambiarEstadoMensajePorId(pIdMensaje, pIdEstado);
    }
    public static void ElimimarMensajePorId(int pIdMensaje)
    {
        capaMensaje.ElimimarMensajePorId(pIdMensaje);
    }
    public static void ElimimarTodosMensajePorId(int pIdTodosMensaje)
    {
        capaMensaje.ElimimarTodosMensajePorId(pIdTodosMensaje);
    }
    public static void ElimimarTodosMensajeSucursalesPorId(int pIdTodosSucursales)
    {
        capaMensaje.ElimimarTodosMensajeSucursalesPorId(pIdTodosSucursales);
    }
    //////////
    //// FaltantesProblemasCrediticios
    public static int InsertarFaltantesProblemasCrediticios(int? fpc_codCarrito, string fpc_codSucursal, int fpc_codCliente, string fpc_nombreProducto, int fpc_cantidad, int fpc_tipo)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataSet dsResultado = capaLogRegistro.GestiónFaltantesProblemasCrediticios(null, fpc_codCarrito, fpc_codSucursal, fpc_codCliente, fpc_nombreProducto, fpc_cantidad, fpc_tipo, null, Constantes.cSQL_INSERT);
            int resultado = -1;
            if (dsResultado != null)
            {
                if (dsResultado.Tables["FaltantesProblemasCrediticios"].Rows[0]["fpc_id"] != DBNull.Value)
                {
                    resultado = Convert.ToInt32(dsResultado.Tables["FaltantesProblemasCrediticios"].Rows[0]["fpc_id"]);
                }
            }
            return resultado;
        }
        else
        {
            return -100;
        }
    }
    public static bool BorrarPorProductosFaltasProblemasCrediticios(string fpc_codSucursal, int fpc_codCliente, int fpc_tipo, string fpc_nombreProducto)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            return capaLogRegistro.BorrarPorProductosFaltasProblemasCrediticios(fpc_codSucursal, fpc_codCliente, fpc_tipo, fpc_nombreProducto);
        }
        else
        {
            return false;
        }
    }
    public static bool BorrarPorProductosFaltasProblemasCrediticiosV2(string fpc_codSucursal, int fpc_codCliente, int fpc_tipo, string fpc_nombreProducto, int pCantidadDia, int pCantidadProductoGrabarNuevo)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            return capaLogRegistro.BorrarPorProductosFaltasProblemasCrediticiosV2(fpc_codSucursal, fpc_codCliente, fpc_tipo, fpc_nombreProducto, pCantidadDia, pCantidadProductoGrabarNuevo);
        }
        else
        {
            return false;
        }
    }
    public static decimal? RecuperarLimiteSaldo()
    {
        decimal? resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            resultado = capaClientes.RecuperarLimiteSaldo();
        }
        return resultado;
    }
    //public static int RecuperarFaltantesProblemasCrediticios(int fpc_codCarrito, string fpc_codSucursal, int fpc_codCliente, string fpc_nombreProducto, int fpc_cantidad, int fpc_tipo)
    //{
    //    if (VerificarPermisos(CredencialAutenticacion))
    //    {
    //        DataSet dsResultado = capaLogRegistro.GestiónFaltantesProblemasCrediticios(null, fpc_codCarrito, fpc_codSucursal, fpc_codCliente, fpc_nombreProducto, fpc_cantidad, fpc_tipo, null, Constantes.cSQL_SELECT);
    //        int resultado = -1;
    //        if (dsResultado != null)
    //        {
    //            if (dsResultado.Tables["FaltantesProblemasCrediticios"].Rows[0]["fpc_id"] != DBNull.Value)
    //            {
    //                resultado = Convert.ToInt32(dsResultado.Tables["FaltantesProblemasCrediticios"].Rows[0]["fpc_id"]);
    //            }
    //        }
    //        return resultado;
    //    }
    //    else
    //    {
    //        return -100;
    //    }
    //}
    public static List<cFaltantesConProblemasCrediticiosPadre> RecuperarFaltasProblemasCrediticios(int fpc_codCliente, int fpc_tipo, int pCantidadDia)
    {
        List<cFaltantesConProblemasCrediticiosPadre> resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataTable tabla = capaLogRegistro.RecuperarFaltasProblemasCrediticios(fpc_codCliente, fpc_tipo, pCantidadDia);
            resultado = ConvertDataTableAClase(tabla);
            return resultado;
        }
        else
        {
            return resultado;
        }
        //if (VerificarPermisos(CredencialAutenticacion))
        //{
        //    List<cFaltantesConProblemasCrediticiosPadre> resultado = new List<cFaltantesConProblemasCrediticiosPadre>();
        //    DataTable tabla = capaLogRegistro.RecuperarFaltasProblemasCrediticios(fpc_codCliente, fpc_tipo, pCantidadDia);
        //    if (tabla != null)
        //    {
        //        var resultadoTemporal = (from item in tabla.AsEnumerable()
        //                                 select new { fpc_tipo = item.Field<int>("fpc_tipo"), fpc_codSucursal = item.Field<string>("fpc_codSucursal"), suc_nombre = item.IsNull("suc_nombre") ? item.Field<string>("fpc_codSucursal") : item.Field<string>("suc_nombre") }).Distinct().ToList();

        //        var resultadoTemporalDistinct = (from t in resultadoTemporal select new { fpc_tipo = t.fpc_tipo, fpc_codSucursal = t.fpc_codSucursal, suc_nombre = t.suc_nombre }).Distinct().ToList();
        //        for (int i = 0; i < resultadoTemporalDistinct.Count; i++)
        //        {
        //            cFaltantesConProblemasCrediticiosPadre obj = new cFaltantesConProblemasCrediticiosPadre();
        //            obj.fpc_codSucursal = resultadoTemporalDistinct[i].fpc_codSucursal;
        //            obj.suc_nombre = resultadoTemporalDistinct[i].suc_nombre;
        //            obj.fpc_tipo = resultadoTemporalDistinct[i].fpc_tipo;
        //            obj.listaProductos = new List<cFaltantesConProblemasCrediticios>();
        //            obj.listaProductos.AddRange((from itemTabla in tabla.AsEnumerable()
        //                                         where itemTabla.Field<string>("fpc_codSucursal") == obj.fpc_codSucursal
        //                                         select new cFaltantesConProblemasCrediticios { fpc_cantidad = itemTabla.Field<int>("fpc_cantidad"), fpc_nombreProducto = itemTabla.Field<string>("fpc_nombreProducto"), stk_stock = itemTabla.Field<string>("stk_stock"), pro_ofeunidades = itemTabla.Field<int>("pro_ofeunidades"), pro_ofeporcentaje = itemTabla.Field<decimal>("pro_ofeporcentaje") }));
        //            resultado.Add(obj);
        //        }
        //    }
        //    return resultado;
        //}
        //else
        //{
        //    return null;
        //}
    }
    public static List<cFaltantesConProblemasCrediticiosPadre> RecuperarFaltasProblemasCrediticios_TodosEstados(int fpc_codCliente, int fpc_tipo, int pCantidadDia)
    {
        List<cFaltantesConProblemasCrediticiosPadre> resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataTable tabla = capaLogRegistro.RecuperarFaltasProblemasCrediticios_TodosEstados(fpc_codCliente, fpc_tipo, pCantidadDia);
            resultado = ConvertDataTableAClase(tabla);
            return resultado;
        }
        else
        {
            return resultado;
        }
    }
    public static List<cFaltantesConProblemasCrediticiosPadre> ConvertDataTableAClase(DataTable tabla)
    {
        List<cFaltantesConProblemasCrediticiosPadre> resultado = null;
        if (tabla != null)
        {
            resultado = new List<cFaltantesConProblemasCrediticiosPadre>();
            var resultadoTemporal = (from item in tabla.AsEnumerable()
                                     select new { fpc_tipo = item.Field<int>("fpc_tipo"), fpc_codSucursal = item.Field<string>("fpc_codSucursal"), suc_nombre = item.IsNull("suc_nombre") ? item.Field<string>("fpc_codSucursal") : item.Field<string>("suc_nombre") }).Distinct().ToList();

            var resultadoTemporalDistinct = (from t in resultadoTemporal select new { fpc_tipo = t.fpc_tipo, fpc_codSucursal = t.fpc_codSucursal, suc_nombre = t.suc_nombre }).Distinct().ToList();
            for (int i = 0; i < resultadoTemporalDistinct.Count; i++)
            {
                cFaltantesConProblemasCrediticiosPadre obj = new cFaltantesConProblemasCrediticiosPadre();
                obj.fpc_codSucursal = resultadoTemporalDistinct[i].fpc_codSucursal;
                obj.suc_nombre = resultadoTemporalDistinct[i].suc_nombre;
                obj.fpc_tipo = resultadoTemporalDistinct[i].fpc_tipo;
                obj.listaProductos = new List<cFaltantesConProblemasCrediticios>();
                obj.listaProductos.AddRange((from itemTabla in tabla.AsEnumerable()
                                             where itemTabla.Field<string>("fpc_codSucursal") == obj.fpc_codSucursal
                                             select new cFaltantesConProblemasCrediticios { fpc_cantidad = itemTabla.Field<int>("fpc_cantidad"), fpc_nombreProducto = itemTabla.Field<string>("fpc_nombreProducto"), stk_stock = itemTabla.Field<string>("stk_stock"), pro_ofeunidades = itemTabla.Field<int>("pro_ofeunidades"), pro_ofeporcentaje = itemTabla.Field<decimal>("pro_ofeporcentaje") }));
                resultado.Add(obj);
            }

        }
        return resultado;
    }

    public static int? InsertarActualizarHorariosSucursalDependiente(int sdh_SucursalDependienteHorario, string sdh_sucursal, string sdh_sucursalDependiente, string sdh_codReparto, string sdh_diaSemana, string sdh_horario)
    {
        int? resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            string accion = sdh_SucursalDependienteHorario == 0 ? Constantes.cSQL_INSERT : Constantes.cSQL_UPDATE;
            DataSet dsResultado = capaClientes.GestiónSucursalDependienteHorarios(sdh_SucursalDependienteHorario, sdh_sucursal, sdh_sucursalDependiente, sdh_codReparto, sdh_diaSemana, sdh_horario, accion);

            if (sdh_SucursalDependienteHorario == 0)
            {
                if (dsResultado != null)
                {
                    if (dsResultado.Tables["SucursalHorario"].Rows[0]["sdh_SucursalDependienteHorario"] != DBNull.Value)
                    {
                        resultado = Convert.ToInt32(dsResultado.Tables["SucursalHorario"].Rows[0]["sdh_SucursalDependienteHorario"]);
                    }
                }
            }
            else
            {
                resultado = sdh_SucursalDependienteHorario;
            }
        }
        return resultado;
    }
    public static List<cHorariosSucursal> RecuperarTodosHorariosSucursalDependiente()
    {
        List<cHorariosSucursal> resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataSet dsResultado = capaClientes.GestiónSucursalDependienteHorarios(null, null, null, null, null, null, Constantes.cSQL_SELECT);

            if (dsResultado != null)
            {
                resultado = new List<cHorariosSucursal>();

                foreach (DataRow item in dsResultado.Tables["SucursalHorario"].Rows)
                {
                    cHorariosSucursal obj = new cHorariosSucursal();
                    if (item["sdh_SucursalDependienteHorario"] != DBNull.Value)
                    {
                        obj.sdh_SucursalDependienteHorario = Convert.ToInt32(item["sdh_SucursalDependienteHorario"]);
                    }
                    if (item["sdh_sucursal"] != DBNull.Value)
                    {
                        obj.sdh_sucursal = item["sdh_sucursal"].ToString();
                    }
                    if (item["sdh_sucursalDependiente"] != DBNull.Value)
                    {
                        obj.sdh_sucursalDependiente = item["sdh_sucursalDependiente"].ToString();
                    }
                    if (item["sdh_codReparto"] != DBNull.Value)
                    {
                        obj.sdh_codReparto = item["sdh_codReparto"].ToString();
                    }
                    if (item["sdh_diaSemana"] != DBNull.Value)
                    {
                        obj.sdh_diaSemana = item["sdh_diaSemana"].ToString();
                    }
                    if (item["sdh_horario"] != DBNull.Value)
                    {
                        obj.sdh_horario = item["sdh_horario"].ToString();
                        string[] arrayHorarios = obj.sdh_horario.Split('-');
                        obj.listaHorarios = arrayHorarios.ToList();
                    }
                    resultado.Add(obj);
                }

            }
        }
        return resultado;
    }

    public static cHistorialArchivoSubir ConvertToHistorialArchivoSubir(DataRow pFila)
    {
        cHistorialArchivoSubir obj = null;

        if (pFila != null)
        {
            obj = new cHistorialArchivoSubir();
            if (pFila["has_id"] != DBNull.Value)
            {
                obj.has_id = Convert.ToInt32(pFila["has_id"]);
            }
            if (pFila["has_NombreArchivo"] != DBNull.Value)
            {
                obj.has_NombreArchivo = pFila["has_NombreArchivo"].ToString();
            }
            if (pFila["has_NombreArchivoOriginal"] != DBNull.Value)
            {
                obj.has_NombreArchivoOriginal = pFila["has_NombreArchivoOriginal"].ToString();
            }
            if (pFila["has_sucursal"] != DBNull.Value)
            {
                obj.has_sucursal = pFila["has_sucursal"].ToString();
            }
            if (pFila.Table.Columns.Contains("suc_nombre"))
            {
                if (pFila["suc_nombre"] != DBNull.Value)
                {
                    obj.suc_nombre = pFila["suc_nombre"].ToString();
                }
            }
            if (pFila["has_codCliente"] != DBNull.Value)
            {
                obj.has_codCliente = Convert.ToInt32(pFila["has_codCliente"]);
            }
            if (pFila["has_fecha"] != DBNull.Value)
            {
                obj.has_fecha = Convert.ToDateTime(pFila["has_fecha"]);
            }
            if (pFila["has_fecha"] != DBNull.Value)
            {
                obj.has_fechaToString = Convert.ToDateTime(pFila["has_fecha"]).ToString();
            }
        }

        return obj;
    }
    public static bool IsBanderaCodigo(string pCodigoBandera)
    {
        bool resultado = true;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataTable tabla = capaSeguridad.RecuperarTablaBandera(pCodigoBandera);
            if (tabla != null)
            {
                if (tabla.Rows.Count > 0)
                {
                    if (tabla.Rows[0]["ban_estado"] != DBNull.Value)
                    {
                        resultado = Convert.ToBoolean(tabla.Rows[0]["ban_estado"]);
                    }
                }
            }

        }
        return resultado;
    }
    public static List<cHistorialArchivoSubir> RecuperarHistorialSubirArchivo(int pIdCliente)
    {
        List<cHistorialArchivoSubir> resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            resultado = new List<cHistorialArchivoSubir>();
            DataTable tabla = capaLogRegistro.RecuperarHistorialSubirArchivo(pIdCliente);
            if (tabla != null)
            {
                foreach (DataRow item in tabla.Rows)
                {
                    resultado.Add(ConvertToHistorialArchivoSubir(item));
                }

            }
        }
        return resultado;
    }
    public static List<cHistorialArchivoSubir> RecuperarHistorialSubirArchivoPorNombreArchivoOriginal(string pNombreArchivoOriginal)
    {
        List<cHistorialArchivoSubir> resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataTable tabla = capaLogRegistro.RecuperarHistorialSubirArchivoPorNombreArchivoOriginal(pNombreArchivoOriginal);
            if (tabla != null)
            {
                resultado = new List<cHistorialArchivoSubir>();
                foreach (DataRow item in tabla.Rows)
                {
                    resultado.Add(ConvertToHistorialArchivoSubir(item));
                }

            }
        }
        return resultado;
    }
    public static cHistorialArchivoSubir RecuperarHistorialSubirArchivoPorId(int pId)
    {
        cHistorialArchivoSubir resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataTable tabla = capaLogRegistro.RecuperarHistorialSubirArchivoPorId(pId);
            if (tabla != null)
            {
                if (tabla.Rows.Count > 0)
                {
                    resultado = ConvertToHistorialArchivoSubir(tabla.Rows[0]);

                }
            }
        }
        return resultado;
    }
    public static bool AgregarHistorialSubirArchivo(int pIdCliente, string pSucursal, string pNombreArchivo, string pNombreArchivoOriginal, DateTime pFecha)
    {
        bool resultado = false;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            resultado = capaLogRegistro.AgregarHistorialSubirArchivo(pIdCliente, pNombreArchivo, pNombreArchivoOriginal, pSucursal, pFecha);
        }
        return resultado;
    }

    public static List<cUsuarioSinPermisosIntranet> RecuperarTodosSinPermisosIntranetPorIdUsuario(int pIdUsuario)
    {
        List<cUsuarioSinPermisosIntranet> resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataTable tabla = capaSeguridad.RecuperarSinPermisoUsuarioIntranetPorIdUsuario(pIdUsuario);

            if (tabla != null)
            {
                resultado = new List<cUsuarioSinPermisosIntranet>();
                for (int i = 0; i < tabla.Rows.Count; i++)
                {
                    cUsuarioSinPermisosIntranet obj = new cUsuarioSinPermisosIntranet();
                    if (tabla.Rows[i]["usp_id"] != DBNull.Value)
                    {
                        obj.usp_id = Convert.ToInt32(tabla.Rows[i]["usp_id"]);
                    }
                    if (tabla.Rows[i]["usp_codUsuario"] != DBNull.Value)
                    {
                        obj.usp_codUsuario = Convert.ToInt32(tabla.Rows[i]["usp_codUsuario"]);
                    }
                    if (tabla.Rows[i]["usp_nombreSeccion"] != DBNull.Value)
                    {
                        obj.usp_nombreSeccion = Convert.ToString(tabla.Rows[i]["usp_nombreSeccion"]);
                    }
                    resultado.Add(obj);
                }
            }
        }
        return resultado;
    }
    public static bool InsertarSinPermisoUsuarioIntranetPorIdUsuario(int pIdUsuario, List<string> pListaNombreSeccion)
    {
        bool resultado = false;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataTable pTablaDetalle = FuncionesPersonalizadas.ConvertNombresSeccionToDataTable(pListaNombreSeccion);
            capaSeguridad.InsertarSinPermisoUsuarioIntranetPorIdUsuario(pIdUsuario, pTablaDetalle);
            resultado = true;
        }
        return resultado;
    }
    private static cFrasesFront ConvertToFrasesFront(DataRow pItem)
    {
        cFrasesFront obj = new cFrasesFront();

        if (pItem["tff_id"] != DBNull.Value)
        {
            obj.tff_id = Convert.ToInt32(pItem["tff_id"]);
        }
        if (pItem["tff_nombre"] != DBNull.Value)
        {
            obj.tff_nombre = Convert.ToString(pItem["tff_nombre"]);
        }
        if (pItem["tff_publicar"] != DBNull.Value)
        {
            obj.tff_publicar = Convert.ToBoolean(pItem["tff_publicar"]);
        }

        return obj;
    }
    public static List<cFrasesFront> RecuperarTodasFrasesFront()
    {
        List<cFrasesFront> resultado = null;
        //if (VerificarPermisos(CredencialAutenticacion))
        //{
        DataTable tabla = capaLogRegistro.RecuperarTodasFrasesFront();
        if (tabla != null)
        {
            resultado = new List<cFrasesFront>();
            foreach (DataRow item in tabla.Rows)
            {
                cFrasesFront obj = ConvertToFrasesFront(item);
                resultado.Add(obj);
            }
        }
        //}
        return resultado;
    }
    public static int InsertarActualizarFrasesFront(int pTff_id, string pTff_nombre)
    {
        int resultado = -1;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            resultado = capaLogRegistro.InsertarActualizarFrasesFront(pTff_id, pTff_nombre);
        }
        return resultado;
    }
    public static void CambiarEstadoPublicarFrasesFront(int pTff_id, bool pTff_publicar)
    {

        if (VerificarPermisos(CredencialAutenticacion))
        {
            capaLogRegistro.CambiarEstadoPublicarFrasesFront(pTff_id, pTff_publicar);
        }

    }
    public static void EliminarFrasesFront(int pTff_id)
    {

        if (VerificarPermisos(CredencialAutenticacion))
        {
            capaLogRegistro.EliminarFrasesFront(pTff_id);
        }
    }
    private static cCatalogo ConvertToCatalogo(DataRow pItem)
    {
        cCatalogo obj = new cCatalogo();

        if (pItem["tbc_codigo"] != DBNull.Value)
        {
            obj.tbc_codigo = Convert.ToInt32(pItem["tbc_codigo"]);
        }
        if (pItem["tbc_titulo"] != DBNull.Value)
        {
            obj.tbc_titulo = Convert.ToString(pItem["tbc_titulo"]);
        }
        if (pItem["tbc_descripcion"] != DBNull.Value)
        {
            obj.tbc_descripcion = Convert.ToString(pItem["tbc_descripcion"]);
        }
        if (pItem["tbc_orden"] != DBNull.Value)
        {
            obj.tbc_orden = Convert.ToInt32(pItem["tbc_orden"]);
        }
        if (pItem["tbc_fecha"] != DBNull.Value)
        {
            obj.tbc_fecha = Convert.ToDateTime(pItem["tbc_fecha"]);
            obj.tbc_fechaToString = ((DateTime)obj.tbc_fecha).ToString();
        }
        if (pItem["tbc_estado"] != DBNull.Value)
        {
            obj.tbc_estado = Convert.ToInt32(pItem["tbc_estado"]);
        }
        if (pItem["est_nombre"] != DBNull.Value)
        {
            obj.tbc_estadoToString = Convert.ToString(pItem["est_nombre"]);
        }
        //obj.tbc_publicarHomeToString = "No publicar";
        if (pItem.Table.Columns.Contains("tbc_publicarHome"))
        {
            if (pItem["tbc_publicarHome"] != DBNull.Value)
            {
                obj.tbc_publicarHome = Convert.ToBoolean(pItem["tbc_publicarHome"]);
                if (obj.tbc_publicarHome.Value)
                    obj.tbc_publicarHomeToString = "Publicar Home";
            }
        }
        return obj;
    }
    public static int? InsertarActualizarCatalogo(int tbc_codigo, string tbc_titulo, string tbc_descripcion, int? tbc_orden, DateTime? tbc_fecha, int tbc_estado)
    {
        int? resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            resultado = capaCatalogo.InsertarActualizarCatalogo(tbc_codigo, tbc_titulo, tbc_descripcion, tbc_orden, tbc_fecha, tbc_estado);
        }
        return resultado;
    }
    public static void ElininarCatalogo(int tbc_codigo)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            capaCatalogo.EliminarCatalogo(tbc_codigo);
        }
    }
    public static void PublicarHomeCatalogo(int tbc_codigo, bool tbc_publicarHome)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            capaCatalogo.PublicarHomeCatalogo(tbc_codigo, tbc_publicarHome);
        }
    }
    public static List<cCatalogo> RecuperarTodosCatalogos()
    {
        List<cCatalogo> resultado = null;
        //if (VerificarPermisos(CredencialAutenticacion))
        {
            DataTable tabla = capaCatalogo.RecuperarTodosCatalogos();
            if (tabla != null)
            {
                resultado = new List<cCatalogo>();
                foreach (DataRow fila in tabla.Rows)
                {
                    resultado.Add(ConvertToCatalogo(fila));
                }
            }
        }
        return resultado;
    }
    //
    private static cCurriculumVitae ConvertToCurriculumVitae(DataRow pItem)
    {
        cCurriculumVitae obj = new cCurriculumVitae();

        if (pItem["tcv_codCV"] != DBNull.Value)
        {
            obj.tcv_codCV = Convert.ToInt32(pItem["tcv_codCV"]);
        }
        if (pItem["tcv_nombre"] != DBNull.Value)
        {
            obj.tcv_nombre = Convert.ToString(pItem["tcv_nombre"]);
        }
        if (pItem["tcv_comentario"] != DBNull.Value)
        {
            obj.tcv_comentario = Convert.ToString(pItem["tcv_comentario"]);
        }
        if (pItem["tcv_dni"] != DBNull.Value)
        {
            obj.tcv_dni = Convert.ToString(pItem["tcv_dni"]);
        }
        if (pItem["tcv_mail"] != DBNull.Value)
        {
            obj.tcv_mail = Convert.ToString(pItem["tcv_mail"]);
        }
        if (pItem["tcv_fecha"] != DBNull.Value)
        {
            obj.tcv_fecha = Convert.ToDateTime(pItem["tcv_fecha"]);
            obj.tcv_fechaToString = ((DateTime)obj.tcv_fecha).ToString();
        }
        if (pItem["tcv_estado"] != DBNull.Value)
        {
            obj.tcv_estado = Convert.ToInt32(pItem["tcv_estado"]);
        }
        if (pItem["est_nombre"] != DBNull.Value)
        {
            obj.tcv_estadoToString = Convert.ToString(pItem["est_nombre"]);
        }
        if (pItem["tcv_puesto"] != DBNull.Value)
        {
            obj.tcv_puesto = Convert.ToString(pItem["tcv_puesto"]);
        }
        else
        {
            obj.tcv_puesto = "";
        }
        if (pItem["tcv_sucursal"] != DBNull.Value)
        {
            obj.tcv_sucursal = Convert.ToString(pItem["tcv_sucursal"]);
        }
        else
        {
            obj.tcv_sucursal = "";
        }
        if (pItem["tcv_fechaPresentacion"] != DBNull.Value)
        {
            obj.tcv_fechaPresentacion = Convert.ToDateTime(pItem["tcv_fechaPresentacion"]);
            obj.tcv_fechaPresentacionToString = ((DateTime)obj.tcv_fechaPresentacion).ToString();
        }
        List<SitioBase.capaDatos.cArchivo> listaArchivo = WebService.RecuperarTodosArchivos(obj.tcv_codCV, Constantes.cTABLA_CV, string.Empty);
        if (listaArchivo != null)
        {
            if (listaArchivo.Count > 0)
            {
                obj.arc_nombre = listaArchivo[0].arc_nombre;
                // lbl_archivo.Text = "<div><a href=\"../../servicios/descargarArchivo.aspx?t=" + Constantes.cTABLA_CV + "&n=" + listaArchivo[0].arc_nombre + "\" >" + listaArchivo[0].arc_nombre + "</a>&nbsp; </div>";
            }
        }

        return obj;
    }
    public static int InsertarCurriculumVitae(string tcv_nombre, string tcv_comentario, string tcv_mail, string tcv_dni)
    {
        int resultado = 0;
        string accion = Constantes.cSQL_INSERT;
        int codigoEstado = Constantes.cESTADO_SINLEER;
        DataSet dsResultado = capaCV.GestiónCurriculumVitae(null, DateTime.Now, tcv_nombre, tcv_comentario, tcv_mail, tcv_dni, codigoEstado, null, accion, null, null, null);
        if (dsResultado != null)
        {
            if (dsResultado.Tables.Contains("CurriculumVitae"))
            {
                if (dsResultado.Tables["CurriculumVitae"].Rows.Count > 0)
                {
                    if (dsResultado.Tables["CurriculumVitae"].Rows[0]["tcv_codCV"] != DBNull.Value)
                    {
                        resultado = Convert.ToInt32(dsResultado.Tables["CurriculumVitae"].Rows[0]["tcv_codCV"]);
                        SitioBase.clases.cMail.enviarMail(System.Configuration.ConfigurationManager.AppSettings["mail_cv"], "Nuevo Curriculum Vitae", GenerarHtmlCuerpoMail(tcv_nombre, tcv_comentario, tcv_mail));
                    }
                }
            }
        }
        return resultado;
    }
    private static string GenerarHtmlCuerpoMail(string tcv_nombre, string tcv_comentario, string tcv_mail)
    {
        string resultado = string.Empty;
        resultado += "<table>";
        resultado += "<tr>";
        resultado += "<td>";
        resultado += "Nombre:";
        resultado += "</td>";
        resultado += "<td>";
        resultado += tcv_nombre;
        resultado += "</td>";
        resultado += "</tr>";
        resultado += "<tr>";
        resultado += "<td>";
        resultado += "Mail:";
        resultado += "</td>";
        resultado += "<td>";
        resultado += tcv_mail;
        resultado += "</td>";
        resultado += "</tr>";
        resultado += "<tr>";
        resultado += "<td>";
        resultado += "Comentario:";
        resultado += "</td>";
        resultado += "<td>";
        resultado += tcv_comentario;
        resultado += "</td>";
        resultado += "</tr>";
        resultado += "</table>";
        return resultado;
    }
    public static cCurriculumVitae RecuperarCurriculumVitae(int pId)
    {
        cCurriculumVitae resultado = null;
        string accion = Constantes.cSQL_SELECT;
        DataSet dsResultado = capaCV.GestiónCurriculumVitae(pId, null, null, null, null, null, null, null, accion, null, null, null);
        if (dsResultado != null)
        {
            if (dsResultado.Tables.Contains("CurriculumVitae"))
            {
                foreach (DataRow item in dsResultado.Tables["CurriculumVitae"].Rows)
                {
                    resultado = ConvertToCurriculumVitae(item);
                    break;
                }
            }
        }
        return resultado;
    }
    public static List<cCurriculumVitae> RecuperarTodosCurriculumVitae(string pFiltro)
    {
        List<cCurriculumVitae> resultado = null;
        string accion = Constantes.cSQL_SELECT;
        DataSet dsResultado = capaCV.GestiónCurriculumVitae(null, null, null, null, null, null, null, pFiltro, accion, null, null, null);
        if (dsResultado != null)
        {
            resultado = new List<cCurriculumVitae>();
            if (dsResultado.Tables.Contains("CurriculumVitae"))
            {
                foreach (DataRow item in dsResultado.Tables["CurriculumVitae"].Rows)
                {
                    resultado.Add(ConvertToCurriculumVitae(item));
                }
            }
        }
        return resultado;
    }
    public static void EliminarCurriculumVitae(int tcv_codCV)
    {
        string accion = Constantes.cSQL_DELETE;
        DataSet dsResultado = capaCV.GestiónCurriculumVitae(tcv_codCV, null, null, null, null, null, null, null, accion, null, null, null);
    }
    public static void CambiarEstadoCurriculumVitae(int tcv_codCV, int tcv_estado)
    {
        string accion = Constantes.cSQL_ESTADO;
        DataSet dsResultado = capaCV.GestiónCurriculumVitae(tcv_codCV, null, null, null, null, null, tcv_estado, null, accion, null, null, null);
    }

    //////////
    public static int InsertarActualizarTiposEnvios(int pEnv_id, string pEnv_codigo, string pEnv_nombre)
    {
        int resultado = -1;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            resultado = capaTiposEnvios.InsertarActualizarTiposEnvios(pEnv_id, pEnv_codigo, pEnv_nombre);
        }
        return resultado;
    }
    public static bool EliminarTiposEnvios(int pEnv_id)
    {
        bool resultado = false;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            capaTiposEnvios.EliminarTiposEnvios(pEnv_id);
            resultado = true;
        }
        return resultado;
    }
    public static List<cTiposEnvios> RecuperarTodosTiposEnvios()
    {
        List<cTiposEnvios> resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataTable tabla = capaTiposEnvios.RecuperarTodosTiposEnvios();
            if (tabla != null)
            {
                resultado = new List<cTiposEnvios>();
                foreach (DataRow item in tabla.Rows)
                {
                    cTiposEnvios obj = ConvertToTiposEnvios(item);
                    if (obj != null)
                    {
                        resultado.Add(obj);
                    }
                }
            }
        }
        return resultado;
    }
    public static List<cTiposEnvios> RecuperarTodosTiposEnviosParaCombo()
    {
        List<cTiposEnvios> resultado = new List<cTiposEnvios>();
        resultado.Add(new cTiposEnvios(-1, "<< Todos tipos envíos >>"));
        resultado.AddRange(WebService.RecuperarTodosTiposEnvios());
        return resultado;
    }
    //
    public static int InsertarActualizarSucursalDependienteTipoEnvioCliente(int pTsd_id, int pTsd_idSucursalDependiente, int? pTsd_idTipoEnvioCliente, List<int> listaIdTiposEnvios)
    {
        int resultado = -1;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            resultado = capaTiposEnvios.InsertarActualizarSucursalDependienteTipoEnvioCliente(pTsd_id, pTsd_idSucursalDependiente, pTsd_idTipoEnvioCliente);
            List<cSucursalDependienteTipoEnviosCliente_TiposEnvios> listaSucursalDependienteTipoEnvioCliente_TipoEnvioAntesGrabar = RecuperarTodosSucursalDependienteTipoEnvioCliente_TiposEnvios().Where(x => x.tdt_idSucursalDependienteTipoEnvioCliente == resultado).ToList();
            foreach (cSucursalDependienteTipoEnviosCliente_TiposEnvios itemAnterior in listaSucursalDependienteTipoEnvioCliente_TipoEnvioAntesGrabar)
            {
                bool isEliminar = true;
                foreach (int itemTiposEnvios in listaIdTiposEnvios)
                {
                    if (itemAnterior.tdt_idTipoEnvio == itemTiposEnvios)
                    {
                        isEliminar = false;
                    }
                }
                if (isEliminar)
                {
                    capaTiposEnvios.EliminarSucursalDependienteTipoEnvioCliente_TiposEnvios(itemAnterior.tdt_id);
                }
            }
            foreach (int itemTiposEnvios in listaIdTiposEnvios)
            {
                capaTiposEnvios.InsertarActualizarSucursalDependienteTipoEnvioCliente_TiposEnvios(0, resultado, itemTiposEnvios);
            }
        }
        return resultado;
    }
    public static bool EliminarSucursalDependienteTipoEnvioCliente(int pTsd_id)
    {
        bool resultado = false;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            capaTiposEnvios.EliminarSucursalDependienteTipoEnvioCliente(pTsd_id);
            resultado = true;
        }
        return resultado;
    }
    public static List<cSucursalDependienteTipoEnviosCliente> RecuperarTodosSucursalDependienteTipoEnvioCliente()
    {
        List<cSucursalDependienteTipoEnviosCliente> resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataTable tabla = capaTiposEnvios.RecuperarTodosSucursalDependienteTipoEnvioCliente();
            if (tabla != null)
            {
                resultado = new List<cSucursalDependienteTipoEnviosCliente>();
                foreach (DataRow item in tabla.Rows)
                {
                    cSucursalDependienteTipoEnviosCliente obj = ConvertToTiposEnviosSucursalDependiente(item);
                    if (obj != null)
                    {
                        resultado.Add(obj);
                    }
                }
            }
        }
        return resultado;
    }
    public static List<cSucursalDependienteTipoEnviosCliente_TiposEnvios> RecuperarTodosSucursalDependienteTipoEnvioCliente_TiposEnvios()
    {
        List<cSucursalDependienteTipoEnviosCliente_TiposEnvios> resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataTable tabla = capaTiposEnvios.RecuperarTodosSucursalDependienteTipoEnvioCliente_TiposEnvios();
            if (tabla != null)
            {
                resultado = new List<cSucursalDependienteTipoEnviosCliente_TiposEnvios>();
                foreach (DataRow item in tabla.Rows)
                {
                    cSucursalDependienteTipoEnviosCliente_TiposEnvios obj = ConvertToTiposEnviosSucursalDependiente_TiposEnvios(item);
                    if (obj != null)
                    {
                        resultado.Add(obj);
                    }
                }
            }
        }
        return resultado;
    }

    private static cTiposEnvios ConvertToTiposEnvios(DataRow pItem)
    {
        cTiposEnvios obj = new cTiposEnvios();

        if (pItem["env_id"] != DBNull.Value)
        {
            obj.env_id = Convert.ToInt32(pItem["env_id"]);
        }
        if (pItem["env_codigo"] != DBNull.Value)
        {
            obj.env_codigo = Convert.ToString(pItem["env_codigo"]);
        }
        if (pItem["env_nombre"] != DBNull.Value)
        {
            obj.env_nombre = Convert.ToString(pItem["env_nombre"]);
        }
        return obj;
    }
    private static cSucursalDependienteTipoEnviosCliente ConvertToTiposEnviosSucursalDependiente(DataRow pItem)
    {
        cSucursalDependienteTipoEnviosCliente obj = new cSucursalDependienteTipoEnviosCliente();

        if (pItem["sde_codigo"] != DBNull.Value)
        {
            obj.sde_codigo = Convert.ToInt32(pItem["sde_codigo"]);
        }
        if (pItem["sde_sucursal"] != DBNull.Value)
        {
            obj.sde_sucursal = Convert.ToString(pItem["sde_sucursal"]);
        }
        if (pItem["sde_sucursalDependiente"] != DBNull.Value)
        {
            obj.sde_sucursalDependiente = Convert.ToString(pItem["sde_sucursalDependiente"]);
        }
        if (pItem["tsd_id"] != DBNull.Value)
        {
            obj.tsd_id = Convert.ToInt32(pItem["tsd_id"]);
        }
        if (pItem["tsd_idSucursalDependiente"] != DBNull.Value)
        {
            obj.tsd_idSucursalDependiente = Convert.ToInt32(pItem["tsd_idSucursalDependiente"]);
        }
        if (pItem["tsd_idTipoEnvioCliente"] != DBNull.Value)
        {
            obj.tsd_idTipoEnvioCliente = Convert.ToInt32(pItem["tsd_idTipoEnvioCliente"]);
        }
        else
        {

            obj.env_nombre = "Todos tipos envíos";
        }
        if (pItem["env_id"] != DBNull.Value)
        {
            obj.env_id = Convert.ToInt32(pItem["env_id"]);
        }
        if (pItem["env_codigo"] != DBNull.Value)
        {
            obj.env_codigo = Convert.ToString(pItem["env_codigo"]);
        }
        if (pItem["env_nombre"] != DBNull.Value)
        {
            obj.env_nombre = Convert.ToString(pItem["env_nombre"]);
        }
        return obj;
    }
    private static cSucursalDependienteTipoEnviosCliente_TiposEnvios ConvertToTiposEnviosSucursalDependiente_TiposEnvios(DataRow pItem)
    {
        cSucursalDependienteTipoEnviosCliente_TiposEnvios obj = new cSucursalDependienteTipoEnviosCliente_TiposEnvios();

        if (pItem["tdt_id"] != DBNull.Value)
        {
            obj.tdt_id = Convert.ToInt32(pItem["tdt_id"]);
        }
        if (pItem["tdt_idSucursalDependienteTipoEnvioCliente"] != DBNull.Value)
        {
            obj.tdt_idSucursalDependienteTipoEnvioCliente = Convert.ToInt32(pItem["tdt_idSucursalDependienteTipoEnvioCliente"]);
        }
        if (pItem["tdt_idTipoEnvio"] != DBNull.Value)
        {
            obj.tdt_idTipoEnvio = Convert.ToInt32(pItem["tdt_idTipoEnvio"]);
        }
        if (pItem["env_id"] != DBNull.Value)
        {
            obj.env_id = Convert.ToInt32(pItem["env_id"]);
        }
        if (pItem["env_codigo"] != DBNull.Value)
        {
            obj.env_codigo = Convert.ToString(pItem["env_codigo"]);
        }
        if (pItem["env_nombre"] != DBNull.Value)
        {
            obj.env_nombre = Convert.ToString(pItem["env_nombre"]);
        }
        return obj;
    }
    public static int InsertarActualizarProductoParametrizadoCantidad(int pCpc_cantidadParametrizada)
    {
        int resultado = -1;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            resultado = capaProductos.InsertarActualizarProductoParametrizadoCantidad(pCpc_cantidadParametrizada);
        }
        return resultado;
    }
    public static int RecuperarProductoParametrizadoCantidad()
    {
        int resultado = -1;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            resultado = 0;
            DataTable dtResultado = capaProductos.RecuperarProductoParametrizadoCantidad();
            if (dtResultado != null)
            {
                foreach (DataRow item in dtResultado.Rows)
                {
                    if (item["cpc_cantidadParametrizada"] != DBNull.Value)
                    {
                        resultado = Convert.ToInt32(item["cpc_cantidadParametrizada"]);
                        break;
                    }
                }
            }
        }
        return resultado;
    }
    public static int InsertarActualizarCadeteriaRestricciones(int pTcr_id, string pTcr_codigoSucursal, int pTcr_UnidadesMinimas, int pTcr_UnidadesMaximas, double pTcr_MontoMinimo, double pTcr_MontoIgnorar)
    {
        int resultado = -1;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            resultado = capaTiposEnvios.InsertarActualizarCadeteriaRestricciones(pTcr_id, pTcr_codigoSucursal, pTcr_UnidadesMinimas, pTcr_UnidadesMaximas, pTcr_MontoMinimo, pTcr_MontoIgnorar);
        }
        return resultado;
    }
    public static bool EliminarCadeteriaRestricciones(int pTcr_id)
    {
        bool resultado = false;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            capaTiposEnvios.EliminarCadeteriaRestricciones(pTcr_id);
            resultado = true;
        }
        return resultado;
    }
    public static List<cCadeteriaRestricciones> RecuperarTodosCadeteriaRestricciones()
    {
        List<cCadeteriaRestricciones> resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataTable tabla = capaTiposEnvios.RecuperarTodosCadeteriaRestricciones();
            if (tabla != null)
            {
                resultado = new List<cCadeteriaRestricciones>();
                foreach (DataRow item in tabla.Rows)
                {
                    cCadeteriaRestricciones obj = ConvertToCadeteriaRestricciones(item);
                    if (obj != null)
                    {
                        resultado.Add(obj);
                    }
                }
            }
        }
        return resultado;
    }
    private static cCadeteriaRestricciones ConvertToCadeteriaRestricciones(DataRow pItem)
    {
        cCadeteriaRestricciones obj = new cCadeteriaRestricciones();

        if (pItem["tcr_id"] != DBNull.Value)
        {
            obj.tcr_id = Convert.ToInt32(pItem["tcr_id"]);
        }
        if (pItem["tcr_codigoSucursal"] != DBNull.Value)
        {
            obj.tcr_codigoSucursal = Convert.ToString(pItem["tcr_codigoSucursal"]);
        }
        if (pItem["tcr_MontoIgnorar"] != DBNull.Value)
        {
            obj.tcr_MontoIgnorar = Convert.ToDouble(pItem["tcr_MontoIgnorar"]);
        }
        if (pItem["tcr_MontoMinimo"] != DBNull.Value)
        {
            obj.tcr_MontoMinimo = Convert.ToDouble(pItem["tcr_MontoMinimo"]);
        }
        if (pItem["tcr_UnidadesMaximas"] != DBNull.Value)
        {
            obj.tcr_UnidadesMaximas = Convert.ToInt32(pItem["tcr_UnidadesMaximas"]);
        }
        if (pItem["tcr_UnidadesMinimas"] != DBNull.Value)
        {
            obj.tcr_UnidadesMinimas = Convert.ToInt32(pItem["tcr_UnidadesMinimas"]);
        }
        if (pItem["suc_nombre"] != DBNull.Value)
        {
            obj.suc_nombre = Convert.ToString(pItem["suc_nombre"]);
        }
        return obj;
    }
    public static List<cProductosBuscador> RecuperarTodosProductosDesdeTabla(List<cProductosAndCantidad> pListaProducto, string pSucursalPerteneciente, string pCli_codprov, int pCli_codigo)
    {
        List<cProductosBuscador> resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataSet dsResultado = null;
            DataTable pTablaDetalle = FuncionesPersonalizadas.ObtenerDataTableProductosCarritoArchivosPedidos();
            //DataRow fila = pTablaDetalle.NewRow();
            if (pListaProducto.Count > 0)
            {
                foreach (cProductosAndCantidad itemProductosAndCantidad in pListaProducto)
                {
                    DataRow fila = pTablaDetalle.NewRow();
                    fila["codProducto"] = itemProductosAndCantidad.codProductoNombre;
                    fila["cantidad"] = itemProductosAndCantidad.cantidad;
                    pTablaDetalle.Rows.Add(fila);
                }
                //fila["codProducto"] = pListaProducto[0].codProductoNombre;
                //fila["cantidad"] = pListaProducto[0].cantidad;
                ////fila["codigobarra"] = pCodigoBarra;
                ////fila["codigoalfabeta"] = pCodigoAlfaBeta;
                ////fila["troquel"] = pTroquel;
                ////fila["cantidad"] = pCantidad;
                ////fila["nroordenamiento"] = pTabla.Rows.Count + 1;
                //pTablaDetalle.Rows.Add(fila);
            }
            dsResultado = capaProductos.RecuperarProductosDesdeTabla(pTablaDetalle, pSucursalPerteneciente, pCli_codprov, pCli_codigo);
            if (dsResultado != null)
            {
                DataTable tablaProductos = dsResultado.Tables[0];
                DataTable tablaSucursalStocks = dsResultado.Tables[1];
                List<cTransferDetalle> listaTransferDetalle = null;
                listaTransferDetalle = new List<cTransferDetalle>();
                DataTable tablaTransferDetalle = dsResultado.Tables[2];
                foreach (DataRow itemTransferDetalle in tablaTransferDetalle.Rows)
                {
                    cTransferDetalle objTransferDetalle = ConvertToTransferDetalle(itemTransferDetalle);
                    objTransferDetalle.CargarTransfer(ConvertToTransfer(itemTransferDetalle));
                    listaTransferDetalle.Add(objTransferDetalle);
                }
                resultado = new List<cProductosBuscador>();
                foreach (DataRow item in tablaProductos.Rows)
                {
                    if (item["pro_codigo"] != DBNull.Value)
                    {
                        List<cSucursalStocks> tempListaSucursalStocks = new List<cSucursalStocks>();
                        tempListaSucursalStocks = (from r in tablaSucursalStocks.Select("stk_codpro = '" + item["pro_codigo"].ToString() + "'").AsEnumerable()
                                                   select new cSucursalStocks { stk_codpro = r["stk_codpro"].ToString(), stk_codsuc = r["stk_codsuc"].ToString(), stk_stock = r["stk_stock"].ToString() }).ToList();
                        if (tempListaSucursalStocks.Count > 0)
                        {
                            cProductosBuscador obj = new cProductosBuscador();
                            obj.pro_codigo = item["pro_codigo"].ToString();
                            if (item["pro_nombre"] != DBNull.Value)
                            {
                                obj.pro_nombre = item["pro_nombre"].ToString();
                            }
                            if (item["pro_precio"] != DBNull.Value)
                            {
                                obj.pro_precio = Convert.ToDecimal(item["pro_precio"]);
                            }
                            if (item["pro_preciofarmacia"] != DBNull.Value)
                            {
                                obj.pro_preciofarmacia = Convert.ToDecimal(item["pro_preciofarmacia"]);
                            }
                            if (item["pro_ofeunidades"] != DBNull.Value)
                            {
                                obj.pro_ofeunidades = Convert.ToInt32(item["pro_ofeunidades"]);
                            }
                            if (item["pro_ofeporcentaje"] != DBNull.Value)
                            {
                                obj.pro_ofeporcentaje = Convert.ToDecimal(item["pro_ofeporcentaje"]);
                            }
                            if (item["pro_neto"] != DBNull.Value)
                            {
                                obj.pro_neto = Convert.ToBoolean(item["pro_neto"]);
                            }
                            if (item["pro_codtpopro"] != DBNull.Value)
                            {
                                obj.pro_codtpopro = item["pro_codtpopro"].ToString().ToUpper();
                            }
                            if (item["pro_descuentoweb"] != DBNull.Value)
                            {
                                obj.pro_descuentoweb = Convert.ToDecimal(item["pro_descuentoweb"]);
                            }
                            if (item["pro_laboratorio"] != DBNull.Value)
                            {
                                obj.pro_laboratorio = item["pro_laboratorio"].ToString();
                            }
                            if (item["pro_monodroga"] != DBNull.Value)
                            {
                                obj.pro_monodroga = item["pro_monodroga"].ToString();
                            }
                            if (item["pro_codigobarra"] != DBNull.Value)
                            {
                                obj.pro_codigobarra = item["pro_codigobarra"].ToString();
                            }
                            if (item["pro_codigoalfabeta"] != DBNull.Value)
                            {
                                obj.pro_codigoalfabeta = item["pro_codigoalfabeta"].ToString();
                            }
                            if (item["pro_troquel"] != DBNull.Value)
                            {
                                obj.pro_troquel = item["pro_troquel"].ToString();
                            }
                            if (item["pro_Ranking"] != DBNull.Value)
                            {
                                obj.pro_Ranking = Convert.ToInt32(item["pro_Ranking"]);
                            }
                            if (item["pro_codtpovta"] != DBNull.Value)
                            {
                                obj.pro_codtpovta = Convert.ToString(item["pro_codtpovta"]);
                            }
                            if (item["isTransfer"] == DBNull.Value)
                            {
                                obj.isTieneTransfer = false;
                            }
                            else
                            {
                                obj.isTieneTransfer = true;
                            }
                            if (item["pro_isTrazable"] != DBNull.Value)
                            {
                                obj.pro_isTrazable = Convert.ToBoolean(item["pro_isTrazable"]);
                            }
                            if (item["pro_isCadenaFrio"] != DBNull.Value)
                            {
                                obj.pro_isCadenaFrio = Convert.ToBoolean(item["pro_isCadenaFrio"]);
                            }
                            if (item["pro_canmaxima"] != DBNull.Value)
                            {
                                obj.pro_canmaxima = Convert.ToInt32(item["pro_canmaxima"]);
                            }
                            // Default = false
                            if (item["pro_entransfer"] != DBNull.Value)
                            {
                                obj.pro_entransfer = Convert.ToBoolean(item["pro_entransfer"]);
                            }
                            if (item["pro_vtasolotransfer"] != DBNull.Value)
                            {
                                obj.pro_vtasolotransfer = Convert.ToBoolean(item["pro_vtasolotransfer"]);
                            }
                            if (item["RequiereVale"] != DBNull.Value)
                            {
                                obj.isValePsicotropicos = Convert.ToBoolean(item["RequiereVale"]);
                            }
                            if (item.Table.Columns.Contains("pro_acuerdo"))
                            {
                                if (item["pro_acuerdo"] != DBNull.Value)
                                {
                                    obj.pro_acuerdo = Convert.ToInt32(item["pro_acuerdo"]);
                                }
                            }
                            obj.isProductoFacturacionDirecta = false;
                            if (listaTransferDetalle != null)
                            {
                                List<cTransferDetalle> listaAUXtransferDetalle = listaTransferDetalle.Where(x => x.tde_codpro == obj.pro_nombre).ToList();
                                if (listaAUXtransferDetalle.Count > 0)
                                {
                                    obj.isProductoFacturacionDirecta = true;
                                    obj.CargarTransferYTransferDetalle(listaAUXtransferDetalle[0]);
                                    if (HttpContext.Current.Session["clientesDefault_Cliente"] != null)
                                    {
                                        obj.PrecioFinalTransfer = FuncionesPersonalizadas.ObtenerPrecioFinalTransferBase((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"], obj.tfr_deshab, obj.tfr_pordesadi, obj.pro_neto, obj.pro_codtpopro, obj.pro_descuentoweb, obj.tde_predescuento == null ? 0 : (decimal)obj.tde_predescuento);
                                    }
                                }
                            }
                            obj.listaSucursalStocks = tempListaSucursalStocks;
                            obj.PrecioFinal = FuncionesPersonalizadas.ObtenerPrecioFinal(((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]), obj);
                            obj.PrecioConDescuentoOferta = FuncionesPersonalizadas.ObtenerPrecioUnitarioConDescuentoOferta(obj.PrecioFinal, obj);
                            resultado.Add(obj);
                        }
                    }
                }
            }
        }
        return resultado;
    }
    public static List<int> RecuperarCantidadProductoEnCarritoYCarritoTransferFacturacionDirecta(int codCliente, string codSucursal, string codProducto, string nombreProducto)
    {
        List<int> resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataSet dtResultado = capaLogRegistro.RecuperarCantidadProductoEnCarritoYCarritoTransferFacturacionDirecta(codCliente, codSucursal, codProducto, nombreProducto);
            if (dtResultado != null)
            {
                resultado = new List<int>();
                resultado.Add(0);
                resultado.Add(0);
                if (dtResultado.Tables.Count > 1)
                {
                    if (dtResultado.Tables[0].Rows.Count > 0)
                    {
                        if (dtResultado.Tables[0].Rows[0]["lcp_cantidad"] != DBNull.Value)
                        {
                            resultado[0] = Convert.ToInt32(dtResultado.Tables[0].Rows[0]["lcp_cantidad"]);
                        }
                    }
                    if (dtResultado.Tables[1].Rows.Count > 0)
                    {
                        if (dtResultado.Tables[1].Rows[0]["ctd_Cantidad"] != DBNull.Value)
                        {
                            resultado[1] = Convert.ToInt32(dtResultado.Tables[1].Rows[0]["ctd_Cantidad"]);
                        }
                    }
                }
            }
        }
        return resultado;
    }

    private static cProductos ConvertToProductosImagen(DataRow pItem)
    {
        cProductos obj = new cProductos();
        if (pItem["pro_codigo"] != DBNull.Value)
        {
            obj.pro_codigo = Convert.ToString(pItem["pro_codigo"]);
        }
        if (pItem.Table.Columns.Contains("pro_nombre"))
        {
            if (pItem["pro_nombre"] != DBNull.Value)
            {
                obj.pro_nombre = Convert.ToString(pItem["pro_nombre"]);
            }
        }
        if (pItem["pri_nombreArchivo"] != DBNull.Value)
        {
            obj.pri_nombreArchivo = Convert.ToString(pItem["pri_nombreArchivo"]);
        }
        return obj;
    }
    public static List<cProductos> ObtenerProductosImagenesBusqueda(string pTxtBuscador)
    {
        List<cProductos> resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataTable tabla = capaProductos.ObtenerProductosImagenesBusqueda(pTxtBuscador);
            if (tabla != null)
            {
                resultado = new List<cProductos>();
                foreach (DataRow item in tabla.Rows)
                {
                    cProductos obj = ConvertToProductosImagen(item);
                    if (obj != null)
                        resultado.Add(obj);
                }
            }
        }
        return resultado;
    }
    public static List<cProductos> ObtenerProductosImagenes()
    {
        List<cProductos> resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            DataTable tabla = capaProductos.ObtenerProductosImagenes();
            if (tabla != null)
            {
                resultado = new List<cProductos>();
                foreach (DataRow item in tabla.Rows)
                {
                    cProductos obj = ConvertToProductosImagen(item);
                    if (obj != null)
                        resultado.Add(obj);
                }
            }
        }
        return resultado;
    }
    public static bool? ActualizarInsertarProductosImagen(string pCodigoProducto, string pNombreArchivo)
    {
        bool? resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
            resultado = capaProductos.ActualizarInsertarProductosImagen(pCodigoProducto, pNombreArchivo);
        return resultado;
    }
    public static bool? ElimimarProductoImagenPorId(string pCodigoProducto)
    {
        bool? resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
            resultado = capaProductos.ElimimarProductoImagenPorId(pCodigoProducto);
        return resultado;
    }

    private static cOferta ConvertToOferta(DataRow pItem)
    {
        cOferta obj = new cOferta();

        if (pItem["ofe_idOferta"] != DBNull.Value)
        {
            obj.ofe_idOferta = Convert.ToInt32(pItem["ofe_idOferta"]);
        }
        if (pItem["ofe_titulo"] != DBNull.Value)
        {
            obj.ofe_titulo = Convert.ToString(pItem["ofe_titulo"]);
        }
        if (pItem["ofe_descuento"] != DBNull.Value)
        {
            obj.ofe_descuento = Convert.ToString(pItem["ofe_descuento"]);
        }
        if (pItem["ofe_tipo"] != DBNull.Value)
        {
            obj.ofe_tipo = Convert.ToInt32(pItem["ofe_tipo"]);
        }
        if (pItem["ofe_descr"] != DBNull.Value)
        {
            obj.ofe_descr = Convert.ToString(pItem["ofe_descr"]);
        }
        if (pItem["ofe_publicar"] != DBNull.Value)
        {
            obj.ofe_publicar = Convert.ToBoolean(pItem["ofe_publicar"]);
        }
        if (pItem["ofe_activo"] != DBNull.Value)
        {
            obj.ofe_activo = Convert.ToBoolean(pItem["ofe_activo"]);
        }
        if (pItem["ofe_fecha"] != DBNull.Value)
        {
            obj.ofe_fecha = Convert.ToDateTime(pItem["ofe_fecha"]);
            obj.ofe_fechaToString = obj.ofe_fecha.ToString();
        }
        if (pItem["ofe_etiqueta"] != DBNull.Value)
        {
            obj.ofe_etiqueta = Convert.ToString(pItem["ofe_etiqueta"]);
        }
        if (pItem["ofe_etiquetaColor"] != DBNull.Value)
        {
            obj.ofe_etiquetaColor = Convert.ToString(pItem["ofe_etiquetaColor"]);
        }
        if (pItem.Table.Columns.Contains("countOfertaDetalles") && pItem["countOfertaDetalles"] != DBNull.Value)
            obj.countOfertaDetalles = Convert.ToInt32(pItem["countOfertaDetalles"]);
        if (pItem.Table.Columns.Contains("Rating") && pItem["Rating"] != DBNull.Value)
            obj.Rating = Convert.ToInt32(pItem["Rating"]);
        if (pItem.Table.Columns.Contains("ofe_nombreTransfer") && pItem["ofe_nombreTransfer"] != DBNull.Value)
            obj.ofe_nombreTransfer = Convert.ToString(pItem["ofe_nombreTransfer"]);
        if (pItem.Table.Columns.Contains("tfr_codigo") && pItem["tfr_codigo"] != DBNull.Value)
            obj.tfr_codigo = Convert.ToInt32(pItem["tfr_codigo"]);
        if (pItem.Table.Columns.Contains("nameImagen") && pItem["nameImagen"] != DBNull.Value)
            obj.nameImagen = Convert.ToString(pItem["nameImagen"]);
        if (pItem.Table.Columns.Contains("namePdf") && pItem["namePdf"] != DBNull.Value)
            obj.namePdf = Convert.ToString(pItem["namePdf"]);
        if (pItem.Table.Columns.Contains("ofe_fechaFinOferta") && pItem["ofe_fechaFinOferta"] != DBNull.Value)
        {
            obj.ofe_fechaFinOferta = Convert.ToDateTime(pItem["ofe_fechaFinOferta"]);
            obj.ofe_fechaFinOfertaToString = obj.ofe_fechaFinOferta.Value.ToString("dd'/'MM'/'yyyy");
        }
        return obj;
    }
    private static cOfertaDetalle ConvertToOfertaDetalle(DataRow pItem)
    {
        cOfertaDetalle obj = new cOfertaDetalle();

        if (pItem["ofd_idOfertaDetalle"] != DBNull.Value)
        {
            obj.ofd_idOfertaDetalle = Convert.ToInt32(pItem["ofd_idOfertaDetalle"]);
        }
        if (pItem["ofd_idOferta"] != DBNull.Value)
        {
            obj.ofd_idOferta = Convert.ToInt32(pItem["ofd_idOferta"]);
        }
        if (pItem["ofd_productoCodigo"] != DBNull.Value)
        {
            obj.ofd_productoCodigo = Convert.ToString(pItem["ofd_productoCodigo"]);
        }
        if (pItem["ofd_productoNombre"] != DBNull.Value)
        {
            obj.ofd_productoNombre = Convert.ToString(pItem["ofd_productoNombre"]);
        }
        if (pItem["codigo"] != DBNull.Value)
        {
            obj.codigo = Convert.ToString(pItem["codigo"]);
        }
        if (pItem["nombre"] != DBNull.Value)
        {
            obj.nombre = Convert.ToString(pItem["nombre"]);
        }
        return obj;
    }
    public static List<cOferta> RecuperarTodasOfertas()
    {
        List<cOferta> resultado = null;
        DataTable tabla = capaHome.RecuperarTodasOfertas();
        if (tabla != null)
        {
            resultado = new List<cOferta>();
            foreach (DataRow fila in tabla.Rows)
            {
                resultado.Add(ConvertToOferta(fila));
            }
        }
        return resultado;
    }
    public static List<cOferta> RecuperarTodasOfertaPublicar()
    {
        List<cOferta> resultado = null;
        DataTable tabla = capaHome.RecuperarTodasOfertaPublicar();
        if (tabla != null)
        {
            resultado = new List<cOferta>();
            foreach (DataRow fila in tabla.Rows)
            {
                resultado.Add(ConvertToOferta(fila));
            }
        }
        return resultado;
    }
    public static cOferta RecuperarOfertaPorId(int pIdOferta)
    {
        cOferta resultado = null;
        DataTable tabla = capaHome.RecuperarOfertaPorId(pIdOferta);
        if (tabla != null && tabla.Rows.Count > 0)
            resultado = ConvertToOferta(tabla.Rows[0]);
        return resultado;
    }
    //public static cOferta RecuperarOfertaPorId(int pId)
    //{
    //    cOferta resultado = RecuperarTodasOfertas().FirstOrDefault(x => x.ofe_idOferta == pId);
    //    return resultado;
    //}
    public static List<cOfertaDetalle> RecuperarTodasOfertaDetalles()
    {
        List<cOfertaDetalle> resultado = null;
        DataTable tabla = capaHome.RecuperarTodasOfertaDetalles();
        if (tabla != null)
        {
            resultado = new List<cOfertaDetalle>();
            foreach (DataRow fila in tabla.Rows)
            {
                resultado.Add(ConvertToOfertaDetalle(fila));
            }
        }
        return resultado;
    }
    public static bool? ElimimarOfertaPorId(int pIdOferta)
    {
        bool? resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
            resultado = capaHome.ElimimarOfertaPorId(pIdOferta);
        return resultado;
    }
    public static bool? CambiarEstadoPublicarOferta(int pIdOferta)
    {
        bool? resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
            resultado = capaHome.CambiarEstadoPublicarOferta(pIdOferta);
        return resultado;
    }
    public static bool? InsertarActualizarOferta(int pOfe_idOferta, string pOfe_titulo, string pOfe_descr, string pOfe_descuento, string pOfe_etiqueta, string pOfe_etiquetaColor, int pOfe_tipo, string ofe_nombreTransfer, DateTime? ofe_fechaFinOferta)
    {
        bool? resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
            resultado = capaHome.InsertarActualizarOferta(pOfe_idOferta, pOfe_titulo, pOfe_descr, pOfe_descuento, pOfe_etiqueta, pOfe_etiquetaColor, pOfe_tipo, ofe_nombreTransfer, ofe_fechaFinOferta);
        return resultado;
    }
    public static bool? InsertarActualizarOfertaDetalle(int pIdOfertaDetalle, int pOfd_idOferta, string pProductoCodigo, string pProductoNombre)
    {
        bool? resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
            resultado = capaHome.InsertarActualizarOfertaDetalle(pIdOfertaDetalle, pOfd_idOferta, pProductoCodigo, pProductoNombre);
        return resultado;
    }
    public static bool? ElimimarOfertaDetallePorId(int pIdOfertaDetalle)
    {
        bool? resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
            resultado = capaHome.ElimimarOfertaDetallePorId(pIdOfertaDetalle);
        return resultado;
    }

    public static List<tbl_HomeSlide> RecuperarTodasHomeSlide()
    {
        List<tbl_HomeSlide> resultado = capaEF.RecuperarTodasHomeSlide();
        return resultado;
    }
    public static bool? InsertarActualizarHomeSlide(int hsl_idHomeSlide, string hsl_titulo, string hsl_descr, string hsl_descrHtml, string hsl_descrHtmlReducido, int hsl_tipo, int? hsl_idOferta, int? hsl_idRecursoDoc, int? hsl_idRecursoImgPC, int? hsl_idRecursoImgMobil)
    {
        bool? resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
            resultado = capaEF.InsertarActualizarHomeSlide(hsl_idHomeSlide, hsl_titulo, hsl_descr, hsl_descrHtml, hsl_descrHtmlReducido, hsl_tipo, hsl_idOferta, hsl_idRecursoDoc, hsl_idRecursoImgPC, hsl_idRecursoImgMobil);
        return resultado;
    }
    public static bool? ActualizarImagenHomeSlide(int hsl_idHomeSlide, int idRecurso, int pTipo)
    {
        bool? resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
            resultado = capaEF.ActualizarImagenHomeSlide(hsl_idHomeSlide, idRecurso, pTipo);
        return resultado;
    }
    private static cOfertaHome ConvertTocOfertaHome(DataRow pItem)
    {
        cOfertaHome obj = new cOfertaHome();

        if (pItem["ofh_idOfertaHome"] != DBNull.Value)
        {
            obj.ofh_idOfertaHome = Convert.ToInt32(pItem["ofh_idOfertaHome"]);
        }
        if (pItem["ofh_orden"] != DBNull.Value)
        {
            obj.ofh_orden = Convert.ToInt32(pItem["ofh_orden"]);
        }
        if (pItem["ofh_idOferta"] != DBNull.Value)
        {
            obj.ofh_idOferta = Convert.ToInt32(pItem["ofh_idOferta"]);
        }

        return obj;
    }
    private static cOfertaHome ConvertAddOferta(DataRow pItem, cOfertaHome obj)
    {
        //cOferta oAux= ConvertToOferta(pItem);

        if (pItem["ofe_idOferta"] != DBNull.Value)
        {
            obj.ofe_idOferta = Convert.ToInt32(pItem["ofe_idOferta"]);
        }
        if (pItem["ofe_titulo"] != DBNull.Value)
        {
            obj.ofe_titulo = Convert.ToString(pItem["ofe_titulo"]);
        }
        if (pItem["ofe_descuento"] != DBNull.Value)
        {
            obj.ofe_descuento = Convert.ToString(pItem["ofe_descuento"]);
        }
        if (pItem["ofe_tipo"] != DBNull.Value)
        {
            obj.ofe_tipo = Convert.ToInt32(pItem["ofe_tipo"]);
        }
        if (pItem["ofe_descr"] != DBNull.Value)
        {
            obj.ofe_descr = Convert.ToString(pItem["ofe_descr"]);
        }
        if (pItem["ofe_publicar"] != DBNull.Value)
        {
            obj.ofe_publicar = Convert.ToBoolean(pItem["ofe_publicar"]);
        }
        if (pItem["ofe_activo"] != DBNull.Value)
        {
            obj.ofe_activo = Convert.ToBoolean(pItem["ofe_activo"]);
        }
        if (pItem["ofe_fecha"] != DBNull.Value)
        {
            obj.ofe_fecha = Convert.ToDateTime(pItem["ofe_fecha"]);
            obj.ofe_fechaToString = obj.ofe_fecha.ToString();
        }
        if (pItem["ofe_etiqueta"] != DBNull.Value)
        {
            obj.ofe_etiqueta = Convert.ToString(pItem["ofe_etiqueta"]);
        }
        if (pItem["ofe_etiquetaColor"] != DBNull.Value)
        {
            obj.ofe_etiquetaColor = Convert.ToString(pItem["ofe_etiquetaColor"]);
        }
        if (pItem.Table.Columns.Contains("countOfertaDetalles") && pItem["countOfertaDetalles"] != DBNull.Value)
            obj.countOfertaDetalles = Convert.ToInt32(pItem["countOfertaDetalles"]);
        if (pItem.Table.Columns.Contains("Rating") && pItem["Rating"] != DBNull.Value)
            obj.Rating = Convert.ToInt32(pItem["Rating"]);
        if (pItem.Table.Columns.Contains("ofe_nombreTransfer") && pItem["ofe_nombreTransfer"] != DBNull.Value)
            obj.ofe_nombreTransfer = Convert.ToString(pItem["ofe_nombreTransfer"]);
        if (pItem.Table.Columns.Contains("tfr_codigo") && pItem["tfr_codigo"] != DBNull.Value)
            obj.tfr_codigo = Convert.ToInt32(pItem["tfr_codigo"]);
        if (pItem.Table.Columns.Contains("nameImagen") && pItem["nameImagen"] != DBNull.Value)
            obj.nameImagen = Convert.ToString(pItem["nameImagen"]);
        if (pItem.Table.Columns.Contains("namePdf") && pItem["namePdf"] != DBNull.Value)
            obj.namePdf = Convert.ToString(pItem["namePdf"]);
        if (pItem.Table.Columns.Contains("ofe_fechaFinOferta") && pItem["ofe_fechaFinOferta"] != DBNull.Value)
        {
            obj.ofe_fechaFinOferta = Convert.ToDateTime(pItem["ofe_fechaFinOferta"]);
            obj.ofe_fechaFinOfertaToString = obj.ofe_fechaFinOferta.Value.ToString("dd'/'MM'/'yyyy");
        }


        return obj;
    }
    public static List<cOfertaHome> RecuperarTodasOfertaHome()
    {
        List<cOfertaHome> resultado = null;
        DataTable tabla = capaHome.RecuperarTodasOfertaHome();
        if (tabla != null)
        {
            resultado = new List<cOfertaHome>();
            foreach (DataRow fila in tabla.Rows)
            {
                resultado.Add(ConvertTocOfertaHome(fila));
            }
        }
        return resultado;
    }
    public static bool? InsertarActualizarOfertaHome(int ofh_idOfertaHome, int ofh_orden, int ofh_idOferta)
    {
        bool? resultado = null;
        resultado = capaHome.InsertarActualizarOfertaHome(ofh_idOfertaHome, ofh_orden, ofh_idOferta);
        return resultado;
    }
    public static List<cOfertaHome> RecuperarTodasOfertaParaHome()
    {
        List<cOfertaHome> resultado = null;
        DataTable tabla = capaHome.RecuperarTodasOfertaParaHome();
        if (tabla != null)
        {
            resultado = new List<cOfertaHome>();
            foreach (DataRow fila in tabla.Rows)
            {
                cOfertaHome o = ConvertTocOfertaHome(fila);
                resultado.Add(ConvertAddOferta(fila, o));
            }
        }
        return resultado;
    }
    public static bool? EliminarOfertaHome(int pIdOfertaHome)
    {
        bool? resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
            resultado = capaHome.EliminarOfertaHome(pIdOfertaHome);
        return resultado;
    }
    private static cHomeSlide ConvertToHomeSlide(DataRow pItem)
    {
        cHomeSlide obj = new cHomeSlide();

        if (pItem["hsl_idHomeSlide"] != DBNull.Value)
        {
            obj.hsl_idHomeSlide = Convert.ToInt32(pItem["hsl_idHomeSlide"]);
        }
        if (pItem["hsl_titulo"] != DBNull.Value)
        {
            obj.hsl_titulo = Convert.ToString(pItem["hsl_titulo"]);
        }
        if (pItem["hsl_descr"] != DBNull.Value)
        {
            obj.hsl_descr = Convert.ToString(pItem["hsl_descr"]);
        }
        if (pItem["hsl_descrReducido"] != DBNull.Value)
        {
            obj.hsl_descrReducido = Convert.ToString(pItem["hsl_descrReducido"]);
        }
        if (pItem["hsl_descrHtml"] != DBNull.Value)
        {
            obj.hsl_descrHtml = Convert.ToString(pItem["hsl_descrHtml"]);
        }
        if (pItem["hsl_descrHtmlReducido"] != DBNull.Value)
        {
            obj.hsl_descrHtmlReducido = Convert.ToString(pItem["hsl_descrHtmlReducido"]);
        }
        if (pItem["hsl_tipo"] != DBNull.Value)
        {
            obj.hsl_tipo = Convert.ToInt32(pItem["hsl_tipo"]);
        }
        obj.tipoRecurso = "slider";
        if (pItem["hsl_idRecursoDoc"] != DBNull.Value)
        {
            obj.hsl_idRecursoDoc = Convert.ToInt32(pItem["hsl_idRecursoDoc"]);
        }
        if (pItem["hsl_NombreRecursoDoc"] != DBNull.Value)
        {
            obj.hsl_NombreRecursoDoc = Convert.ToString(pItem["hsl_NombreRecursoDoc"]);
        }
        if (pItem["hsl_idRecursoImgPC"] != DBNull.Value)
        {
            obj.hsl_idRecursoImgPC = Convert.ToInt32(pItem["hsl_idRecursoImgPC"]);
        }
        if (pItem["arc_nombrePC"] != DBNull.Value)
        {
            obj.arc_nombrePC = Convert.ToString(pItem["arc_nombrePC"]);
        }
        if (pItem["hsl_idRecursoImgMobil"] != DBNull.Value)
        {
            obj.hsl_idRecursoImgMobil = Convert.ToInt32(pItem["hsl_idRecursoImgMobil"]);
        }
        if (pItem["arc_nombreMobil"] != DBNull.Value)
        {
            obj.arc_nombreMobil = Convert.ToString(pItem["arc_nombreMobil"]);
        }
        if (pItem["hsl_idOferta"] != DBNull.Value)
        {
            obj.hsl_idOferta = Convert.ToInt32(pItem["hsl_idOferta"]);
        }
        if (pItem["hsl_etiqueta"] != DBNull.Value)
        {
            obj.hsl_etiqueta = Convert.ToString(pItem["hsl_etiqueta"]);
        }
        if (pItem["hsl_publicar"] != DBNull.Value)
        {
            obj.hsl_publicar = Convert.ToBoolean(pItem["hsl_publicar"]);
        }
        if (pItem["hsl_activo"] != DBNull.Value)
        {
            obj.hsl_activo = Convert.ToBoolean(pItem["hsl_activo"]);
        }
        if (pItem["hsl_fecha"] != DBNull.Value)
        {
            obj.hsl_fecha = Convert.ToDateTime(pItem["hsl_fecha"]);
            obj.hsl_fechaToString = obj.hsl_fecha.ToString();
        }
        if (pItem.Table.Columns.Contains("hsl_orden") && pItem["hsl_orden"] != DBNull.Value)
        {
            obj.hsl_orden = Convert.ToInt32(pItem["hsl_orden"]);
        }
        return obj;
    }
    public static List<cHomeSlide> RecuperarTodasHomeSlidePublicar()
    {
        List<cHomeSlide> resultado = null;
        DataTable tabla = capaHome.RecuperarTodasHomeSlidePublicar();
        if (tabla != null)
        {
            resultado = new List<cHomeSlide>();
            foreach (DataRow fila in tabla.Rows)
            {
                resultado.Add(ConvertToHomeSlide(fila));
            }
        }
        return resultado;
    }
    public static cHomeSlide RecuperarHomeSlidePorId(int pIdHomeSlide)
    {
        cHomeSlide resultado = null;
        DataTable tabla = capaHome.RecuperarHomeSlidePorId(pIdHomeSlide);
        if (tabla != null && tabla.Rows.Count > 0)
            resultado = ConvertToHomeSlide(tabla.Rows[0]);
        return resultado;
    }
    public static bool? CambiarOrdenHomeSlide(int hsl_idHomeSlide, bool isSubir)
    {
        bool? resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
            resultado = capaEF.CambiarOrdenHomeSlide(hsl_idHomeSlide, isSubir);
        return resultado;
    }
    public static bool? EliminarHomeSlide(int hsl_idHomeSlide)
    {
        bool? resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
            resultado = capaEF.EliminarHomeSlide(hsl_idHomeSlide);
        return resultado;
    }
    public static bool? CambiarPublicarHomeSlide(int hsl_idHomeSlide)
    {
        bool? resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
            resultado = capaEF.CambiarPublicarHomeSlide(hsl_idHomeSlide);
        return resultado;
    }
    public static bool? InsertarOfertaRating(int ofr_idOferta, int ofr_idCliente, bool ofr_isDesdeHome)
    {
        return capaEF.InsertarOfertaRating(ofr_idOferta, ofr_idCliente, ofr_isDesdeHome);
    }
    public static bool? ContadorHomeSlideRating(int hsr_idHomeSlide, int? hsr_idCliente)
    {
        capaEF.SubirCountadorHomeSlideRating(hsr_idHomeSlide);
        //capaEF.InsertarHomeSlideRating(hsr_idHomeSlide,  hsr_idCliente);
        return true;
    }
    public static bool? CambiarPublicarReCall(int rec_id)
    {
        if (VerificarPermisos(CredencialAutenticacion))
            return capaEF.CambiarPublicarReCall(rec_id);
        return null;
    }
    public static bool? EliminarReCall(int rec_id)
    {
        if (VerificarPermisos(CredencialAutenticacion))
            return capaEF.EliminarReCall(rec_id);
        return null;
    }
    public static bool? InsertarActualizarReCall(int rec_id, string rec_titulo, string rec_descripcion, string rec_descripcionReducido, string rec_descripcionHTML, DateTime? rec_FechaNoticia, DateTime? rec_FechaFinNoticia)
    {
        if (VerificarPermisos(CredencialAutenticacion))
            return capaEF.InsertarActualizarReCall(rec_id, rec_titulo, rec_descripcion, rec_descripcionReducido, rec_descripcionHTML, rec_FechaNoticia, rec_FechaFinNoticia);
        return null;
    }
    public static List<tbl_Recall> RecuperarTodaReCall()
    {
        return capaEF.RecuperarTodaReCall();
    }
    public class Autenticacion : SoapHeader
    {
        private string sUserPass;
        private string sUserName;

        /// <summary> 
        /// Lee o escribe la clave del usuario 
        /// </summary> 
        public string UsuarioClave
        {
            get
            {
                return sUserPass;
            }
            set
            {
                sUserPass = value;
            }

        }
        /// <summary> 
        /// Lee o escribe el nombre del usuario 
        /// </summary> 
        public string UsuarioNombre
        {
            get
            {
                return sUserName;
            }
            set
            {
                sUserName = value;
            }
        }
    }
}
