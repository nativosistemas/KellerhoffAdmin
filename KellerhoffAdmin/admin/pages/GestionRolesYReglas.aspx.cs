using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Web.Services;
using SitioBase.clases;

public partial class admin_pages_GestionRolesYReglas : cBaseAdmin
{
    public const string consPalabraClave = "gestionrolesyreglas";
    protected void Page_Load(object sender, EventArgs e)
    {
        Seguridad(consPalabraClave);
        Page.ClientScript.RegisterStartupScript(this.GetType(), "clientscript", "var listaJsonReglasRol = \'" + ServiciosCargarArbolHTML() + "\';", true);
    }
    [WebMethod(EnableSession = true)]
    public static bool IsGrabarReglaRol(int pIdRol, List<SitioBase.capaDatos.ReglaAGrabar> lista)
    {
        if (SitioBase.clases.cBaseAdmin.isAgregar(consPalabraClave) || SitioBase.clases.cBaseAdmin.isEditar(consPalabraClave) || SitioBase.clases.cBaseAdmin.isEliminar(consPalabraClave))
        {
            if (lista.Count > 0)
            {
                string strXML = string.Empty;
                strXML += "<Root>";
                foreach (SitioBase.capaDatos.ReglaAGrabar item in lista)
                {
                    List<XAttribute> listaAtributos = new List<XAttribute>();

                    listaAtributos.Add(new XAttribute("idRegla", item.idRegla));
                    listaAtributos.Add(new XAttribute("idRelacionReglaRol", item.idRelacionReglaRol));
                    listaAtributos.Add(new XAttribute("isActivo", item.isActivo));

                    if (item.isAgregado == null)
                    {

                    }
                    else
                    {
                        listaAtributos.Add(new XAttribute("isAgregado", item.isAgregado));
                    }
                    if (item.isEditado == null)
                    {
                    }
                    else
                    {
                        listaAtributos.Add(new XAttribute("isEditado", item.isEditado));
                    }
                    if (item.isEliminado == null)
                    {
                    }
                    else
                    {
                        listaAtributos.Add(new XAttribute("isEliminado", item.isEliminado));
                    }

                    XElement nodo = new XElement("Regla", listaAtributos);
                    strXML += nodo.ToString();
                }
                strXML += "</Root>";
                string parameXML = strXML;

                SitioBase.clases.Seguridad.InsertarActualizarRelacionRolRegla(pIdRol, parameXML);
                SitioBase.clases.cBaseAdmin.CargarAccionesEnVariableSession();
                return true;
            }
        }
        return false;
    }
    [WebMethod(EnableSession = true)]
    public static string RecuperarReglasPorRol(int pIdRol)
    {
        List<SitioBase.capaDatos.ReglaPorRol> listaResultado = SitioBase.clases.Seguridad.RecuperarRelacionRolReglasPorRol(pIdRol);
        return SitioBase.clases.Serializador.SerializarAJson(listaResultado);
    }
    //[WebMethod(EnableSession = true)]
    public static string ServiciosCargarArbolHTML()
    {
        List<SitioBase.capaDatos.ListaCheck> listaResultado = SitioBase.clases.Seguridad.RecuperarTodasReglasPorNivel();
        return SitioBase.clases.Serializador.SerializarAJson(listaResultado);
    }
}
