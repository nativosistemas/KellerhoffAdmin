using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SitioBase.clases;
using System.Web.Services;

public partial class admin_pages_GestionMensajeV3 : cBaseAdmin
{
    public const string consPalabraClave = "gestionmensaje";
    protected void Page_Load(object sender, EventArgs e)
    {
        Seguridad(consPalabraClave);
        if (!IsPostBack)
        {

        }

    }
    [WebMethod(EnableSession = true)]
    public static string RecuperarTodosMensajeNew()
    {
        List<cMensajeNew> resultado = WebService.RecuperarTodosMensajeNew();
        return resultado == null ? string.Empty : SitioBase.clases.Serializador.SerializarAJson(resultado);
    }
    [WebMethod(EnableSession = true)]
    public static string ElimimarMensajeNewPorId(int pValor)
    {
        WebService.ElimimarMensajeNewPorId(pValor);
        return string.Empty;
    }
}