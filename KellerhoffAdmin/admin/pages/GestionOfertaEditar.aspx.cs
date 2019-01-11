using SitioBase.clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_pages_GestionOfertaEditar : cBaseAdmin
{
    public const string consPalabraClave = "gestionhome";
    protected void Page_Load(object sender, EventArgs e)
    {
        Seguridad(consPalabraClave);

        if (Request.QueryString.AllKeys.Contains("id"))
        {
            HttpContext.Current.Session["GestionOfertaEditar_idOferta"] = Request.QueryString.Get("id");
            cOferta objOferta = WebService.RecuperarTodasOfertas().FirstOrDefault(x => x.ofe_idOferta == Convert.ToInt32(HttpContext.Current.Session["GestionOfertaEditar_idOferta"]));
            HttpContext.Current.Session["GestionOfertaEditar_objOferta"] = objOferta;
        }
    }
    [WebMethod(EnableSession = true)]
    public static string RecuperarProductos(string pTxtBuscador)
    {
        HttpContext.Current.Session["GestionProductoImagen_Text"] = pTxtBuscador;
        List<cProductos> resultado = WebService.ObtenerProductosImagenesBusqueda(pTxtBuscador);
        return resultado == null ? string.Empty : SitioBase.clases.Serializador.SerializarAJson(resultado);
    }
    [WebMethod(EnableSession = true)]
    public static string AgregarProducto(string pCodigoProducto)
    {
        if (HttpContext.Current.Session["GestionOfertaEditar_idOferta"] != null)
        {
            cOfertaDetalle temp = null;
            if (HttpContext.Current.Session["GestionOfertaEditar_OfertaDetalles"] != null)
            {
                List<cOfertaDetalle> l = (List<cOfertaDetalle>)HttpContext.Current.Session["GestionOfertaEditar_OfertaDetalles"];
                temp = l.FirstOrDefault(x => x.ofd_productoCodigo == pCodigoProducto);
            }
            if (temp == null)
                WebService.InsertarActualizarOfertaDetalle(0, Convert.ToInt32(HttpContext.Current.Session["GestionOfertaEditar_idOferta"]), pCodigoProducto, null);
        }
        return RecuperarTodasOfertaDetalles();
    }
    [WebMethod(EnableSession = true)]
    public static string RecuperarTodasOfertaDetalles()
    {
        List<cOfertaDetalle> resultado = new List<cOfertaDetalle>();
        if (HttpContext.Current.Session["GestionOfertaEditar_idOferta"] != null)
        {
            resultado = WebService.RecuperarTodasOfertaDetalles().Where(x => x.ofd_idOferta == Convert.ToInt32(HttpContext.Current.Session["GestionOfertaEditar_idOferta"])).ToList();
            HttpContext.Current.Session["GestionOfertaEditar_OfertaDetalles"] = resultado;
        }
        return resultado == null ? string.Empty : SitioBase.clases.Serializador.SerializarAJson(resultado);
    }
    [WebMethod(EnableSession = true)]
    public static string ElimimarOfertaDetallePorId(int pCodigo)
    {
        bool? resultado = WebService.ElimimarOfertaDetallePorId(pCodigo);
        return RecuperarTodasOfertaDetalles();
    }
    [WebMethod(EnableSession = true)]
    public static string InsertarActualizarOferta(string pOfe_titulo, string pOfe_descr, string pOfe_descuento, string pOfe_etiqueta, string pOfe_etiquetaColor, int tipo, string ofe_nombreTransfer, string ofe_fechaFinOferta_string)
    {
        if (HttpContext.Current.Session["GestionOfertaEditar_idOferta"] != null)
        {
            DateTime? ofe_fechaFinOferta = null;
            if (!string.IsNullOrEmpty(ofe_fechaFinOferta_string))
            {
                ofe_fechaFinOferta = Convert.ToDateTime(ofe_fechaFinOferta_string);
            }
            bool? resultado = WebService.InsertarActualizarOferta(Convert.ToInt32(HttpContext.Current.Session["GestionOfertaEditar_idOferta"]), pOfe_titulo, pOfe_descr, pOfe_descuento, pOfe_etiqueta, pOfe_etiquetaColor, tipo, ofe_nombreTransfer, ofe_fechaFinOferta);
        }
        return RecuperarTodasOfertaDetalles();
    }
    //
    public void AgregarHtmlOculto()
    {
        string resultado = string.Empty;
        if (HttpContext.Current.Session["GestionOfertaEditar_idOferta"] != null)
            resultado += "<input type=\"hidden\" id=\"hiddenOfe_idOferta\" value=\"" + Server.HtmlEncode(HttpContext.Current.Session["GestionOfertaEditar_idOferta"].ToString()) + "\" />";

        if (HttpContext.Current.Session["GestionOfertaEditar_objOferta"] != null)
        {
            cOferta objOferta = (cOferta)HttpContext.Current.Session["GestionOfertaEditar_objOferta"];
            resultado += "<input type=\"hidden\" id=\"hiddenOfe_titulo\" value=\"" + Server.HtmlEncode(objOferta.ofe_titulo) + "\" />";
            resultado += "<input type=\"hidden\" id=\"hiddenOfe_descr\" value=\"" + Server.HtmlEncode(objOferta.ofe_descr) + "\" />";
            resultado += "<input type=\"hidden\" id=\"hiddenOfe_descuento\" value=\"" + Server.HtmlEncode(objOferta.ofe_descuento) + "\" />";
            resultado += "<input type=\"hidden\" id=\"hiddenOfe_etiqueta\" value=\"" + Server.HtmlEncode(objOferta.ofe_etiqueta) + "\" />";
            resultado += "<input type=\"hidden\" id=\"hiddenOfe_etiquetaColor\" value=\"" + Server.HtmlEncode(objOferta.ofe_etiquetaColor) + "\" />";
            resultado += "<input type=\"hidden\" id=\"hiddenOfe_tipo\" value=\"" + Server.HtmlEncode(objOferta.ofe_tipo.ToString()) + "\" />";
            resultado += "<input type=\"hidden\" id=\"hiddenOfe_nombreTransfer\" value=\"" + Server.HtmlEncode(objOferta.ofe_nombreTransfer == null ? "" : objOferta.ofe_nombreTransfer) + "\" />";
            resultado += "<input type=\"hidden\" id=\"hiddenOfe_fechaFin\" value=\"" + Server.HtmlEncode(string.IsNullOrEmpty( objOferta.ofe_fechaFinOfertaToString)?"": objOferta.ofe_fechaFinOfertaToString) + "\" />";

        }
        Response.Write(resultado);
    }
    public static string getHtmlOptionTransfer()
    {
        string result = string.Empty;
        List<cTransfer> l = WebService.RecuperarTodosTransfer();
        if (l != null)
        {
            result += "<option value='-1'>((Sin Seleccionar))</option>";
            for (int i = 0; i < l.Count; i++)
            {
                result += "<option value='" + l[i].tfr_nombre.ToString() + "'>" + l[i].tfr_nombre + "</option>";
            }
        }
        return result;

    }
}