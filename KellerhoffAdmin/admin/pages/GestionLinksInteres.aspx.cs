using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SitioBase.clases;
using SitioBase;

public partial class admin_pages_GestionSitiosInteres : cBaseAdmin
{
    public const string consPalabraClave = "gestionlinksinteres";
    protected void Page_Load(object sender, EventArgs e)
    {
        Seguridad(consPalabraClave);
        if (!IsPostBack)
        {
            Session["GestionLink_Filtro"] = null;
            Session["GestionLink_Tipo"] = 2;
            Session["GestionLink_Lnk_codLinks"] = null;
        }
    }

    public override void Modificar(int pId)
    {
        Session["GestionLink_Lnk_codLinks"] = pId;
        SitioBase.capaDatos.cNoticia noticia = WebService.RecuperarLinksPorId(pId);

        if (noticia != null)
        {
            txtTitulo.Text = noticia.lnk_titulo;
            cmb_origen.Text = noticia.lnk_origen;
            txt_bajada.Text = noticia.lnk_bajada;
            txt_web.Text = noticia.lnk_web;
            pnl_grilla.Visible = false;
            pnl_formulario.Visible = true;
        }
    }
    public override void Insertar()
    {
        Session["GestionLink_Lnk_codLinks"] = 0;
        pnl_grilla.Visible = false;
        pnl_formulario.Visible = true;
        txt_bajada.Text = string.Empty;
        txt_web.Text = string.Empty;
        txtTitulo.Text = string.Empty;
       
    }
    public override void CambiarEstado(int pId)
    {
        if (Session["BaseAdmin_Usuario"] != null)
        {
            int codigoUsuarioEnSession = ((SitioBase.capaDatos.Usuario)Session["BaseAdmin_Usuario"]).id;
            SitioBase.capaDatos.cNoticia noticia = WebService.RecuperarLinksPorId(pId);
            int estadoNoticia = noticia.lnk_estado == Constantes.cESTADO_ACTIVO ? Constantes.cESTADO_INACTIVO : Constantes.cESTADO_ACTIVO;
            WebService.CambiarEstadoLinksPorId(noticia.lnk_codLinks, estadoNoticia, codigoUsuarioEnSession);
            gv_datos.DataBind();
        }
    }
    public override void Publicar(int pId)
    {
        if (Session["BaseAdmin_Usuario"] != null)
        {
            int codigoUsuarioEnSession = ((SitioBase.capaDatos.Usuario)Session["BaseAdmin_Usuario"]).id;
            SitioBase.capaDatos.cNoticia noticia = WebService.RecuperarLinksPorId(pId);
            WebService.PublicarLinksPorId(noticia.lnk_codLinks, !(bool)noticia.lnk_isPublicar, codigoUsuarioEnSession);
            gv_datos.DataBind();
        }
    }
    public override void Eliminar(int pId)
    {
        if (Session["BaseAdmin_Usuario"] != null)
        {
            int codigoUsuarioEnSession = ((SitioBase.capaDatos.Usuario)Session["BaseAdmin_Usuario"]).id;
            SitioBase.capaDatos.cNoticia noticia = WebService.RecuperarLinksPorId(pId);
            WebService.EliminarLinksPorId(noticia.lnk_codLinks, codigoUsuarioEnSession);
            gv_datos.DataBind();
        }
    }
    protected void cmd_nuevo_Click(object sender, EventArgs e)
    {
        LlamarMetodosAcciones(SitioBase.Constantes.cSQL_INSERT, null, consPalabraClave);
    }
    protected void cmd_buscar_Click(object sender, EventArgs e)
    {
        Session["GestionLink_Filtro"] = txt_buscar.Text;
    }
    protected void gv_datos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Eliminar")
        {
            LlamarMetodosAcciones(SitioBase.Constantes.cSQL_DELETE, Convert.ToInt32(e.CommandArgument), consPalabraClave);
        }
      
        if (e.CommandName == "Modificar")
        {
            LlamarMetodosAcciones(SitioBase.Constantes.cSQL_UPDATE, Convert.ToInt32(e.CommandArgument), consPalabraClave);
        }
        else if (e.CommandName == "Estado")
        {
            LlamarMetodosAcciones(SitioBase.Constantes.cSQL_ESTADO, Convert.ToInt32(e.CommandArgument), consPalabraClave);
        }
        else if (e.CommandName == "Publicar")
        {
            LlamarMetodosAcciones(SitioBase.Constantes.cSQL_PUBLICAR, Convert.ToInt32(e.CommandArgument), consPalabraClave);
        }
    }
    protected void cmd_cancelar_Click(object sender, EventArgs e)
    {
        pnl_grilla.Visible = true;
        pnl_formulario.Visible = false;
    }
    protected void cmd_guardar_Click(object sender, EventArgs e)
    {
        if (Session["GestionLink_Lnk_codLinks"] != null && Session["BaseAdmin_Usuario"] != null)
        {
            int codigoLinks = Convert.ToInt32(Session["GestionLink_Lnk_codLinks"]);
            if ((codigoLinks == 0 && SitioBase.clases.cBaseAdmin.isAgregar(consPalabraClave)) || (codigoLinks != 0 && SitioBase.clases.cBaseAdmin.isEditar(consPalabraClave)))
            {
                int codigoUsuarioEnSession = ((SitioBase.capaDatos.Usuario)Session["BaseAdmin_Usuario"]).id;
                WebService.InsertarActualizarLinks(codigoLinks, txtTitulo.Text, txt_bajada.Text, null, txt_web.Text,cmb_origen.Text, 2,  codigoUsuarioEnSession);
            }  
        }
        gv_datos.DataBind();
        pnl_grilla.Visible = true;
        pnl_formulario.Visible = false;
    }
}