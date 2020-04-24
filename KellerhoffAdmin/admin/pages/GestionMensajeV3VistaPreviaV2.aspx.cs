using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SitioBase.clases;
using System.Web.Services;

public partial class admin_pages_GestionMensajeV3VistaPreviaV2 : cBaseAdmin
{
    public const string consPalabraClave = "gestionmensaje";
    protected void Page_Load(object sender, EventArgs e)
    {
        Seguridad(consPalabraClave);
        if (!IsPostBack)
        {

        }
    }
    public void AgregarHtmlOculto()
    {
        string resultado = string.Empty;
        if (HttpContext.Current.Session["GestionMensajeV3Editar_vistaPrevia_Asunto"] != null && HttpContext.Current.Session["GestionMensajeV3Editar_vistaPrevia_Mensaje"] != null)
        {
            resultado += "<input type=\"hidden\" id=\"hidden_asunto\" value=\"" + Server.HtmlEncode(HttpContext.Current.Session["GestionMensajeV3Editar_vistaPrevia_Asunto"].ToString()) + "\" />";
            resultado += "<input type=\"hidden\" id=\"hidden_mensaje\" value=\"" + Server.HtmlEncode(HttpContext.Current.Session["GestionMensajeV3Editar_vistaPrevia_Mensaje"].ToString()) + "\" />";
        }
        Response.Write(resultado);
    }
}