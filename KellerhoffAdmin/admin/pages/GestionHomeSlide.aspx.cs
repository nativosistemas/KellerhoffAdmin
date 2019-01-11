using SitioBase.clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_pages_GestionHomeSlide : cBaseAdmin
{
    public const string consPalabraClave = "gestionhome";
    protected void Page_Load(object sender, EventArgs e)
    {
        Seguridad(consPalabraClave);
        if (!IsPostBack)
        {

        }
    }
    [WebMethod(EnableSession = true)]
    public static string RecuperarTodasHomeSlide()
    {
        List<tbl_HomeSlide> resultado = WebService.RecuperarTodasHomeSlide();
        return resultado == null ? string.Empty : SitioBase.clases.Serializador.SerializarAJson(resultado);
    }
    [WebMethod(EnableSession = true)]
    public static bool CambiarOrdenHomeSlide(int hsl_idHomeSlide, bool isSubir)
    {
        bool? resultado = WebService.CambiarOrdenHomeSlide( hsl_idHomeSlide,  isSubir);
        return resultado == null ? false : resultado.Value;
    }
    [WebMethod(EnableSession = true)]
    public static bool EliminarHomeSlide(int hsl_idHomeSlide)
    {
        bool? resultado = WebService.EliminarHomeSlide(hsl_idHomeSlide);
        return resultado == null ? false : resultado.Value;
    }
    [WebMethod(EnableSession = true)]
    public static bool CambiarPublicarHomeSlide(int hsl_idHomeSlide)
    {
        bool? resultado = WebService.CambiarPublicarHomeSlide(hsl_idHomeSlide);
        return resultado == null ? false : resultado.Value;
    }
    public void AgregarHtmlOculto()
    {
        string resultado = string.Empty;
        resultado += "<input type=\"hidden\" id=\"hiddenListaSlider\" value=\"" + Server.HtmlEncode(RecuperarTodasHomeSlide()) + "\" />";
        Response.Write(resultado);
    }
}