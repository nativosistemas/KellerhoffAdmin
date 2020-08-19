using SitioBase.clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_pages_GestionTiposEnviosSucursal_Reparto : cBaseAdmin
{
    public const string consPalabraClave = "gestiontiposenviossucursal";

    protected void Page_Load(object sender, EventArgs e)
    {
        Seguridad(consPalabraClave);
        if (Request.QueryString.AllKeys.Contains("id"))
        {
           int id = Convert.ToInt32(Request.QueryString.Get("id"));
            HttpContext.Current.Session["GestionTiposEnviosSucursal_Reparto_id"] = id;
            cSucursalDependienteTipoEnviosCliente obj = WebService.RecuperarTodosSucursalDependienteTipoEnvioCliente().Where(x => x.tsd_id == id).First();
            List<cSucursalDependienteTipoEnviosCliente_TiposEnvios> listaTipoEnvio = WebService.RecuperarTodosSucursalDependienteTipoEnvioCliente_TiposEnvios().Where(x => x.tdt_idSucursalDependienteTipoEnvioCliente == id).ToList();
            HttpContext.Current.Session["GestionTiposEnviosSucursal_Reparto_listaTipoEnvio"] = listaTipoEnvio;
            HttpContext.Current.Session["GestionTiposEnviosSucursal_Reparto_obj"] = obj;
        }
        if (!IsPostBack)
        {

        }
    }
    [WebMethod(EnableSession = true)]
    public static int InsertarExcepciones(int pTdr_idSucursalDependienteTipoEnvioCliente, int pTdr_idTipoEnvio,string pTdr_codReparto)
    {
        int resultado = WebService.InsertarEliminarSucursalDependienteTipoEnvioCliente_TipoEnvios_Excepciones(null,pTdr_idSucursalDependienteTipoEnvioCliente, pTdr_idTipoEnvio, pTdr_codReparto);
        return resultado;
    }
    [WebMethod(EnableSession = true)]
    public static int EliminarExcepciones(int pTdr_idSucursalDependienteTipoEnvioCliente, int pTdr_idTipoEnvio, string pTdr_codReparto)
    {
        int resultado = WebService.InsertarEliminarSucursalDependienteTipoEnvioCliente_TipoEnvios_Excepciones(-1, pTdr_idSucursalDependienteTipoEnvioCliente, pTdr_idTipoEnvio, pTdr_codReparto);
        return resultado;
    }
    [WebMethod(EnableSession = true)]
    public static string RecuperarExcepciones(int pIdSucursalDependienteTipoEnvioCliente, string pTdr_codReparto)
    {
        List<SitioBase.clases.cCombo> resultado = WebService.RecuperarSucursalDependienteTipoEnvioCliente_TipoEnvios_Excepciones_paraAdmin(pIdSucursalDependienteTipoEnvioCliente, pTdr_codReparto);
        return SitioBase.clases.Serializador.SerializarAJson(resultado);
    }
    [WebMethod(EnableSession = true)]
    public static string RecuperarTodasExcepciones(int pIdSucursalDependienteTipoEnvioCliente)
    {
        List<cSucursalDependienteTipoEnviosCliente_TiposEnvios> resultado = WebService.RecuperarSucursalDependienteTipoEnvioCliente_TipoEnvios_TodasLasExcepciones( pIdSucursalDependienteTipoEnvioCliente);
        return SitioBase.clases.Serializador.SerializarAJson(resultado);
    }
   
    public void AgregarHtmlOculto()
    {
        string resultado = string.Empty;
        List<cTiposEnvios> l_Envios = WebService.RecuperarTodosTiposEnvios();
        List<string> l_Reparto = WebService.RecuperarTodosCodigoReparto();
        cSucursalDependienteTipoEnviosCliente obj = (cSucursalDependienteTipoEnviosCliente)HttpContext.Current.Session["GestionTiposEnviosSucursal_Reparto_obj"];
        List<cSucursalDependienteTipoEnviosCliente_TiposEnvios> listaTipoEnvio = (List<cSucursalDependienteTipoEnviosCliente_TiposEnvios>)HttpContext.Current.Session["GestionTiposEnviosSucursal_Reparto_listaTipoEnvio"];
        resultado += "<input type=\"hidden\" id=\"hiddenListaTipoEnvio\" value=\"" + Server.HtmlEncode(SitioBase.clases.Serializador.SerializarAJson(listaTipoEnvio)) + "\" />";
        resultado += "<input type=\"hidden\" id=\"hiddenSucursalDependienteTipoEnvios\" value=\"" + Server.HtmlEncode(SitioBase.clases.Serializador.SerializarAJson(obj)) + "\" />";
        resultado += "<input type=\"hidden\" id=\"hiddenListaTodosTiposEnvios\" value=\"" + Server.HtmlEncode(SitioBase.clases.Serializador.SerializarAJson(l_Envios)) + "\" />";
        resultado += "<input type=\"hidden\" id=\"hiddenListaTodosCodigoReparto\" value=\"" + Server.HtmlEncode(SitioBase.clases.Serializador.SerializarAJson(l_Reparto)) + "\" />";

        Response.Write(resultado);
    }
}