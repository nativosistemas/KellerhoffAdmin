using SitioBase;
using SitioBase.clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class admin_pages_GestionCurriculumVitae_v2 : cBaseAdmin
{
    public const string consPalabraClave = "gestioncurriculumvitae";
    protected void Page_Load(object sender, EventArgs e)
    {
        Seguridad(consPalabraClave);
        if (!IsPostBack)
        {

        }
    }
    //[WebMethod(EnableSession = true)]
    //public static string RecuperarTodasCurriculumVitae()
    //{
    //    List<cCurriculumVitae> resultado = SitioBase.clases.AccesoGrilla.GetCurriculumVitae("","");
    //    return resultado == null ? string.Empty : SitioBase.clases.Serializador.SerializarAJson(resultado);
    //}
    [WebMethod(EnableSession = true)]
    public static string RecuperarCurriculumVitae(string pValor)
    {
        List<cCurriculumVitae> resultado = SitioBase.clases.AccesoGrilla.GetCurriculumVitae("", pValor);
        return resultado == null ? string.Empty : SitioBase.clases.Serializador.SerializarAJson(resultado);
    }
    [WebMethod(EnableSession = true)]
    public static string RecuperarCurriculumVitae(string pValor,string pPuesto,string pSucursal)
    {
        List<cCurriculumVitae> resultado = SitioBase.clases.AccesoGrilla.GetCurriculumVitae("", string.IsNullOrEmpty( pValor)?"":pValor.Trim());
        if (!string.IsNullOrEmpty(pPuesto))
            resultado = resultado.Where(x => x.tcv_puesto == pPuesto).ToList();
        if (!string.IsNullOrEmpty(pSucursal))
            resultado = resultado.Where(x => x.tcv_sucursal== pSucursal).ToList();

        System.Web.HttpContext.Current.Session["paginador_lista"] = new Class_Admin.cPaginador( resultado);
        System.Web.HttpContext.Current.Session["paginador_pPage"] = 1;
        return RecuperarPaginador(1);
       // return resultado == null ? string.Empty : SitioBase.clases.Serializador.SerializarAJson(resultado);
    }
    [WebMethod(EnableSession = true)]
    public static string RecuperarPaginador(int pPage)
    {
        if (System.Web.HttpContext.Current.Session["paginador_lista"] != null &&
            System.Web.HttpContext.Current.Session["paginador_pPage"] != null)
        {
            System.Web.HttpContext.Current.Session["paginador_pPage"] = pPage;
            Class_Admin.cPaginador resultado = new Class_Admin.cPaginador((Class_Admin.cPaginador)System.Web.HttpContext.Current.Session["paginador_lista"]);
            return RecuperarCV_generarPaginador(resultado, pPage);
        }
        return string.Empty;
    }
    public static string RecuperarCV_generarPaginador(Class_Admin.cPaginador resultado, int pPage)
    {
        int pageSize = 100;// Constantes.cCantidadFilaPorPagina;
        resultado.CantidadRegistroTotal = resultado.listaCurriculumVitae.Count;
        if (resultado.listaCurriculumVitae.Count > 100) //if (resultado.listaProductos.Count > Constantes.cCantidadFilaPorPagina)//
            resultado.listaCurriculumVitae = resultado.listaCurriculumVitae.Skip((pPage - 1) * pageSize).Take(pageSize).ToList();
        System.Web.HttpContext.Current.Session["paginador_productosOrdenar"] = resultado;
        return Serializador.SerializarAJson(resultado);
    }
    [WebMethod(EnableSession = true)]
    public static bool EliminarCurriculumVitae(int pId)
    {
        WebService.EliminarCurriculumVitae(pId);
        return true;
    }
    public void AgregarHtmlOculto()
    {
        string resultado = string.Empty;
     //   resultado += "<input type=\"hidden\" id=\"hiddenListaSlider\" value=\"" + Server.HtmlEncode(RecuperarTodasHomeSlide()) + "\" />";
        Response.Write(resultado);
    }
}