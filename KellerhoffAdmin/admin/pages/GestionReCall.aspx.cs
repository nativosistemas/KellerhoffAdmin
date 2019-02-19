using SitioBase.clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_pages_GestionReCall : cBaseAdmin
{
    public const string consPalabraClave = "gestionrecall";
    protected void Page_Load(object sender, EventArgs e)
    {
        Seguridad(consPalabraClave);
        if (!IsPostBack)
        {

        }
    }
    [WebMethod(EnableSession = true)]
    public static string RecuperarTodosReCall()
    {
        List<tbl_Recall> resultado = WebService.RecuperarTodaReCall();
        return resultado == null ? string.Empty : SitioBase.clases.Serializador.SerializarAJson(resultado);
    }
    [WebMethod(EnableSession = true)]
    public static string EliminarReCall(int pValor)
    {
        WebService.EliminarReCall(pValor);
        return string.Empty;
    }
    [WebMethod(EnableSession = true)]
    public static bool CambiarEstadoReCallPorId(int pValue)
    {
        WebService.CambiarPublicarReCall(pValue);
        return true;
    }
    //[WebMethod(EnableSession = true)]
    //public static string RecuperarTodosArchivos()
    //{
    //    List<SitioBase.capaDatos.cArchivo> resultado = WebService.RecuperarTodosArchivos(1, "recallpdf", string.Empty);
    //    return resultado == null ? string.Empty : SitioBase.clases.Serializador.SerializarAJson(resultado);
    //}
}