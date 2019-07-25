using SitioBase.clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_pages_GestionOfertaHome : cBaseAdmin
{
    public const string consPalabraClave = "gestionhome";
    public static string strHtmlOptionOferta = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        Seguridad(consPalabraClave);
        if (!IsPostBack)
        {
             strHtmlOptionOferta = null;
        }

    }
    public static string getHtmlOptionOferta()
    {
        if (strHtmlOptionOferta == null)
        {
            string result = string.Empty;
            List<cOferta> l = WebService.RecuperarTodasOfertas();
            if (l != null)
            {
                result += "<option value='-1'>((Sin Seleccionar))</option>";
                for (int i = 0; i < l.Count; i++)
                {
                    result += "<option value='" + l[i].ofe_idOferta.ToString() + "'>" + l[i].ofe_titulo + "</option>";
                }
            }
            strHtmlOptionOferta = result;
        }
        return strHtmlOptionOferta;

    }
    [WebMethod(EnableSession = true)]
    public static string InsertarActualizarOfertaHome(int p1, int p2, int p3, int p4)
    {
        if (HttpContext.Current.Session["GestionOfertaHome_listOfertaHome"] != null)
        {
            List<cOfertaHome> l = WebService.RecuperarTodasOfertaHome();
            //if (p1 > 0)
            //{
            cOfertaHome o1 = l.FirstOrDefault(x => x.ofh_orden == 1);
            if (o1 == null && p1 > 0)
                WebService.InsertarActualizarOfertaHome(0, 1, p1);
            else if (o1 != null && p1 != o1.ofh_idOferta && p1 > 0)
                WebService.InsertarActualizarOfertaHome(o1.ofh_idOfertaHome, 1, p1);
            else if (p1 == -1 && o1 != null)
                WebService.EliminarOfertaHome(o1.ofh_idOfertaHome);
            //}
            //if (p2 > 0)
            //{
            cOfertaHome o2 = l.FirstOrDefault(x => x.ofh_orden == 2);
            if (o2 == null && p2 > 0)
                WebService.InsertarActualizarOfertaHome(0, 2, p2);
            else if (o2 != null && p2 != o2.ofh_idOferta && p2 > 0)
                WebService.InsertarActualizarOfertaHome(o2.ofh_idOfertaHome, 2, p2);
            else if (p2 == -1 && o2 != null)
                WebService.EliminarOfertaHome(o2.ofh_idOfertaHome);
            //}
            //if (p3 > 0)
            //{
            cOfertaHome o3 = l.FirstOrDefault(x => x.ofh_orden == 3);
            if (o3 == null && p3 > 0)
                WebService.InsertarActualizarOfertaHome(0, 3, p3);
            else if (o3 != null && p3 != o3.ofh_idOferta && p3 > 0)
                WebService.InsertarActualizarOfertaHome(o3.ofh_idOfertaHome, 3, p3);
            else if (p3 == -1 && o3 != null)
                WebService.EliminarOfertaHome(o3.ofh_idOfertaHome);
            //}
            //if (p4 > 0)
            //{
            cOfertaHome o4 = l.FirstOrDefault(x => x.ofh_orden == 4);
            if (o4 == null && p4 > 0)
                WebService.InsertarActualizarOfertaHome(0, 4, p4);
            else if (o4 != null && p4 != o4.ofh_idOferta && p4 > 0)
                WebService.InsertarActualizarOfertaHome(o4.ofh_idOfertaHome, 4, p4);
            else if (p4 == -1 && o4 != null)
                WebService.EliminarOfertaHome(o4.ofh_idOfertaHome);


            //}
        }
        return "";
    }
    public void AgregarHtmlOculto()
    {
        string resultado = string.Empty;
        var l = WebService.RecuperarTodasOfertaHome();
        if (l != null)
        {
            HttpContext.Current.Session["GestionOfertaHome_listOfertaHome"] = l;
            resultado += "<input type=\"hidden\" id=\"hiddenListOfeta\" value=\"" + Server.HtmlEncode(SitioBase.clases.Serializador.SerializarAJson(l)) + "\" />";
        }

        Response.Write(resultado);
    }
}