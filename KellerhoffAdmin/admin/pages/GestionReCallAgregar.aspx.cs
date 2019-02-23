using SitioBase.clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_pages_GestionReCallAgregar : cBaseAdmin
{
    public const string consPalabraClave = "gestionrecall";
    protected void Page_Load(object sender, EventArgs e)
    {
        Seguridad(consPalabraClave);
        if (Request.QueryString.AllKeys.Contains("id"))
        {
            HttpContext.Current.Session["GestionReCallAgregar_id"] = Request.QueryString.Get("id");
            tbl_Recall obj = WebService.RecuperarTodaReCall().FirstOrDefault(x => x.rec_id == Convert.ToInt32(HttpContext.Current.Session["GestionReCallAgregar_id"]));
            HttpContext.Current.Session["GestionReCallAgregar_obj"] = obj;
        }
        if (!IsPostBack)
        {

        }
    }
    [WebMethod(EnableSession = true)]
    public static string InsertarActualizarReCall(string titulo, string descr, string descrReducido, string FechaNoticia_string)
    {
        if (HttpContext.Current.Session["GestionReCallAgregar_id"] != null)
        {
            DateTime? FechaNoticia = null;
            if (!string.IsNullOrEmpty(FechaNoticia_string))
            {
                FechaNoticia = Convert.ToDateTime(FechaNoticia_string);
            }
            bool? resultado = WebService.InsertarActualizarReCall(Convert.ToInt32(HttpContext.Current.Session["GestionReCallAgregar_id"]), titulo, descr, descrReducido,"", FechaNoticia, null);
        }
        return "";
    }
    public void AgregarHtmlOculto()
    {
        string resultado = string.Empty;
        if (HttpContext.Current.Session["GestionReCallAgregar_id"] != null)
            resultado += "<input type=\"hidden\" id=\"hidden_rec_id\" value=\"" + Server.HtmlEncode(HttpContext.Current.Session["GestionReCallAgregar_id"].ToString()) + "\" />";
        if (HttpContext.Current.Session["GestionReCallAgregar_obj"] != null)
        {
            tbl_Recall o = (tbl_Recall)HttpContext.Current.Session["GestionReCallAgregar_obj"];

            resultado += "<input type=\"hidden\" id=\"hidden_titulo\" value=\"" + Server.HtmlEncode(o.rec_titulo) + "\" />";
            resultado += "<input type=\"hidden\" id=\"hidden_descr\" value=\"" + Server.HtmlEncode(o.rec_descripcion) + "\" />";
            resultado += "<input type=\"hidden\" id=\"hidden_descrReducido\" value=\"" + Server.HtmlEncode(o.rec_descripcionReducido) + "\" />";
            resultado += "<input type=\"hidden\" id=\"hidden_FechaNoticia\" value=\"" + Server.HtmlEncode(o.rec_FechaNoticia == null ? "" : o.rec_FechaNoticia.Value.ToString("dd'/'MM'/'yyyy")) + "\" />";

        }
        Response.Write(resultado);
    }
}