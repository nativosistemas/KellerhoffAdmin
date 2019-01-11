using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SitioBase.clases;
using SitioBase;

public partial class admin_pages_GestionCurriculumVitae : cBaseAdmin
{
    public const string consPalabraClave = "gestioncurriculumvitae";
    protected void Page_Load(object sender, EventArgs e)
    {
        Seguridad(consPalabraClave);
        if (!IsPostBack)
        {
            Session["GestionCurriculumVitae_Filtro"] = null;
        }
    }
    protected void cmd_buscar_Click(object sender, EventArgs e)
    {
        Session["GestionCurriculumVitae_Filtro"] = txt_buscar.Text;
        gv_datos.DataBind();
    }
    protected void cmd_cancelar_Click(object sender, EventArgs e)
    {
        txt_nombre.Text = string.Empty;
        txt_mail.Text = string.Empty;
        txt_comentario.Text = string.Empty;
        txt_dni.Text = string.Empty;
        txt_estado.Text = string.Empty;
        txt_fecha.Text = string.Empty;
        lbl_archivo.Text = string.Empty;
        pnl_grilla.Visible = true;
        pnl_formulario.Visible = false;
    }
    protected void gv_datos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modificar")
        {
            cCurriculumVitae obj = WebService.RecuperarCurriculumVitae(Convert.ToInt32(e.CommandArgument));
            if (obj != null)
            {
                txt_nombre.Text = obj.tcv_nombre;
                txt_mail.Text = obj.tcv_mail;
                txt_comentario.Text = obj.tcv_comentario;
                txt_dni.Text = obj.tcv_dni;
                txt_estado.Text = obj.tcv_estadoToString;
                txt_fecha.Text = obj.tcv_fechaToString;
                List<SitioBase.capaDatos.cArchivo> listaArchivo = WebService.RecuperarTodosArchivos(obj.tcv_codCV, Constantes.cTABLA_CV, string.Empty);
                if (listaArchivo != null)
                {
                    if (listaArchivo.Count > 0)
                    {
                        lbl_archivo.Text = "<div><a href=\"../../servicios/descargarArchivo.aspx?t=" + Constantes.cTABLA_CV + "&n=" + listaArchivo[0].arc_nombre + "\" >" + listaArchivo[0].arc_nombre + "</a>&nbsp; </div>";                       
                    }
                }
            }
            pnl_grilla.Visible = false;
            pnl_formulario.Visible = true;
        }
        else if (e.CommandName == "Estado")
        {          
           cCurriculumVitae obj = WebService.RecuperarCurriculumVitae(Convert.ToInt32(e.CommandArgument));
           if (obj != null)
           {
               int estado = 0;
               if (obj.tcv_estado == SitioBase.Constantes.cESTADO_SINLEER)
               {
                   estado = SitioBase.Constantes.cESTADO_LEIDO;
               }
               else
               {
                   estado = SitioBase.Constantes.cESTADO_SINLEER;
               }
               WebService.CambiarEstadoCurriculumVitae(Convert.ToInt32(e.CommandArgument), estado);
           }
            gv_datos.DataBind();
            pnl_grilla.Visible = true;
            pnl_formulario.Visible = false;
        }
        else if (e.CommandName == "Eliminar")
        {
            WebService.EliminarCurriculumVitae(Convert.ToInt32(e.CommandArgument));
            gv_datos.DataBind();
            pnl_grilla.Visible = true;
            pnl_formulario.Visible = false;
        }
    }
}