using SitioBase.clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_pages_GestionOferta : cBaseAdmin
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
    public static string RecuperarTodasOfertas()
    {
        List<cOferta> resultado = WebService.RecuperarTodasOfertas();
        return resultado == null ? string.Empty : SitioBase.clases.Serializador.SerializarAJson(resultado);
    }
    [WebMethod(EnableSession = true)]
    public static bool ElimimarOfertaPorId(int pValue)
    {
        bool? resultado = WebService.ElimimarOfertaPorId(pValue);
        return true;
    }
    [WebMethod(EnableSession = true)]
    public static bool CambiarEstadoPublicarOferta(int pValue)
    {
        bool? resultado = WebService.CambiarEstadoPublicarOferta(pValue);
        return true;
    }
}