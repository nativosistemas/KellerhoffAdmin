using SitioBase.clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using System.IO;

public partial class admin_pages_GestionProductoImagen : cBaseAdmin
{
    public const string consPalabraClave = "gestionproductoimagen";
    protected void Page_Load(object sender, EventArgs e)
    {
        Seguridad(consPalabraClave);
        if (!IsPostBack)
        {
            HttpContext.Current.Session["GestionProductoImagen_Text"] = null;
        }
    }
    [WebMethod(EnableSession = true)]
    public static bool EliminarImagenProducto(string pCodigo)
    {
        bool result = false;

        cProductos obj = WebService.ObtenerProductosImagenes().Where(x => x.pro_codigo == pCodigo).FirstOrDefault();
        if (obj != null)
        {
           // string RutaCompleta = AppDomain.CurrentDomain.BaseDirectory + @"archivos\" + "productos" + @"\";
            string RutaCompleta = SitioBase.Constantes.cRaizArchivos + @"\archivos\" + "productos" + @"\";
            try
            {
                File.Delete(RutaCompleta + obj.pri_nombreArchivo);
                result = true;
            }
            catch (Exception ex)
            {
                var lll = ex;
                //    throw;
            }
        }
        bool? resultFunc = WebService.ElimimarProductoImagenPorId(pCodigo);
        result = resultFunc == null ? false : resultFunc.Value;
        return result;
    }
    [WebMethod(EnableSession = true)]
    public static string RecuperarProductos(string pTxtBuscador)
    {
        HttpContext.Current.Session["GestionProductoImagen_Text"] = pTxtBuscador;
        List <cProductos> resultado = WebService.ObtenerProductosImagenesBusqueda(pTxtBuscador);
        return resultado == null ? string.Empty : SitioBase.clases.Serializador.SerializarAJson(resultado);
    }
    public void AgregarHtmlOculto()
    {
        if (Request.QueryString.AllKeys.Contains("text"))
        {
            string resultado = string.Empty;
            resultado += "<input type=\"hidden\" id=\"hiddenText\" value=\"" + Request.QueryString.Get("text") + "\" />";

            Response.Write(resultado);
        }
    }
}