using SitioBase.clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_pages_GestionHomeSlideEditar : cBaseAdmin
{
    public const string consPalabraClave = "gestionhome";
    protected void Page_Load(object sender, EventArgs e)
    {
        Seguridad(consPalabraClave);
        if (Request.QueryString.AllKeys.Contains("id"))
        {
            HttpContext.Current.Session["GestionHomeSlideEditar_idHomeSlide"] = Request.QueryString.Get("id");
            tbl_HomeSlide objOferta = WebService.RecuperarTodasHomeSlide().FirstOrDefault(x => x.hsl_idHomeSlide == Convert.ToInt32(HttpContext.Current.Session["GestionHomeSlideEditar_idHomeSlide"]));
            HttpContext.Current.Session["GestionHomeSlideEditar_objHomeSlide"] = objOferta;
        }

        if (!IsPostBack)
        {

        }
    }
    public static string getHtmlOptionOferta()
    {
        string result = string.Empty;
        List<cOferta> l = WebService.RecuperarTodasOfertas();
        if (l != null)
        {
           // result += "<option value='-1'>((Sin Seleccionar))</option>";
            for (int i = 0; i < l.Count; i++)
            {
                result += "<option value='" + l[i].ofe_idOferta.ToString() + "'>" + l[i].ofe_titulo + "</option>";
            }
        }
        return result;

    }
    public static string getHtmlOptionCatalogo()
    {
        string result = string.Empty;
        List<cCatalogo> l = WebService.RecuperarTodosCatalogos();
        if (l != null)
        {
            // result += "<option value='-1'>((Sin Seleccionar))</option>";
            for (int i = 0; i < l.Count; i++)
            {
                result += "<option value='" + l[i].tbc_codigo.ToString() + "'>" + l[i].tbc_titulo + "</option>";
            }
        }
        return result;

    }
    [WebMethod(EnableSession = true)]
    public static string InsertarActualizarHomeSlide(string hsl_titulo, string hsl_descr, string hsl_descrHtml,string hsl_descrHtmlReducido, int hsl_tipo, int? hsl_idOferta, int? hsl_idRecursoDoc, int? hsl_idRecursoImgPC, int? hsl_idRecursoImgMobil)
    {
        if (HttpContext.Current.Session["GestionHomeSlideEditar_idHomeSlide"] != null)
        {
            bool? resultado = WebService.InsertarActualizarHomeSlide(Convert.ToInt32(HttpContext.Current.Session["GestionHomeSlideEditar_idHomeSlide"]), hsl_titulo, hsl_descr, hsl_descrHtml, hsl_descrHtmlReducido, hsl_tipo,  hsl_idOferta, hsl_idRecursoDoc,  hsl_idRecursoImgPC, hsl_idRecursoImgMobil);
        }
        return "";
    }
    public void AgregarHtmlOculto()
    {
        string resultado = string.Empty;
        if (HttpContext.Current.Session["GestionHomeSlideEditar_idHomeSlide"] != null)
            resultado += "<input type=\"hidden\" id=\"hiddenOfe_idHomeSlide\" value=\"" + Server.HtmlEncode(HttpContext.Current.Session["GestionHomeSlideEditar_idHomeSlide"].ToString()) + "\" />";
        if (HttpContext.Current.Session["GestionHomeSlideEditar_objHomeSlide"] != null)
        {
            tbl_HomeSlide o = (tbl_HomeSlide)HttpContext.Current.Session["GestionHomeSlideEditar_objHomeSlide"];

            resultado += "<input type=\"hidden\" id=\"hidden_titulo\" value=\"" + Server.HtmlEncode(o.hsl_titulo) + "\" />";
            resultado += "<input type=\"hidden\" id=\"hidden_descr\" value=\"" + Server.HtmlEncode(o.hsl_descr) + "\" />";
            resultado += "<input type=\"hidden\" id=\"hidden_descrHtml\" value=\"" + Server.HtmlEncode(o.hsl_descrHtml) + "\" />";
            resultado += "<input type=\"hidden\" id=\"hidden_tipo\" value=\"" + Server.HtmlEncode(o.hsl_tipo.ToString()) + "\" />";
            resultado += "<input type=\"hidden\" id=\"hidden_idRecurso\" value=\"" + Server.HtmlEncode(o.hsl_idRecursoDoc == null? "" : o.hsl_idRecursoDoc.Value.ToString()) + "\" />";
            resultado += "<input type=\"hidden\" id=\"hidden_idOferta\" value=\"" + Server.HtmlEncode(o.hsl_idOferta == null ? "" : o.hsl_idOferta.Value.ToString()) + "\" />";
            resultado += "<input type=\"hidden\" id=\"hidden_descrHtmlReducido\" value=\"" + Server.HtmlEncode(o.hsl_descrHtmlReducido) + "\" />";

        }
        Response.Write(resultado);
    }
}