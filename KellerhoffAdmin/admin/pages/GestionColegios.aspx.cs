using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SitioBase.clases;
using SitioBase;

public partial class admin_pages_GestionColegios : cBaseAdmin
{
    public const string consPalabraClave = "gestionlinksinteres";
    protected void Page_Load(object sender, EventArgs e)
    {
        Seguridad(consPalabraClave);
        if (!IsPostBack)
        {
            Session["GestionSitioInteres_Filtro"] = null;
            Session["GestionLink_Tipo"] = 1;
            Session["GestionSitioInteres_lnk_codLinks"] = null;
        }
    }

    public override void Modificar(int pId)
    {
        Session["GestionSitioInteres_lnk_codLinks"] = pId;
        SitioBase.capaDatos.cNoticia noticia = WebService.RecuperarLinksPorId(pId);

        if (noticia != null)
        {
            txtTitulo.Text = noticia.lnk_titulo;
          
            
            txt_descripcion.Content = noticia.lnk_descripcion;
            pnl_grilla.Visible = false;
            pnl_formulario.Visible = true;
        }
    }
    public override void Insertar()
    {
        Session["GestionSitioInteres_lnk_codLinks"] = 0;
        pnl_grilla.Visible = false;
        pnl_formulario.Visible = true;
    
        txt_descripcion.Content = string.Empty;
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
        Session["GestionSitioInteres_Filtro"] = txt_buscar.Text;
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
        if (Session["GestionSitioInteres_lnk_codLinks"] != null && Session["BaseAdmin_Usuario"] != null)
        {
            int codigoColegio = Convert.ToInt32(Session["GestionSitioInteres_lnk_codLinks"]);
            if ((codigoColegio == 0 && SitioBase.clases.cBaseAdmin.isAgregar(consPalabraClave)) || (codigoColegio != 0 && SitioBase.clases.cBaseAdmin.isEditar(consPalabraClave)))
            {
                int codigoUsuarioEnSession = ((SitioBase.capaDatos.Usuario)Session["BaseAdmin_Usuario"]).id;
                WebService.InsertarActualizarLinks(codigoColegio, txtTitulo.Text, null,  txt_descripcion.Content, null, null, 1, codigoUsuarioEnSession);
            }
        }
        gv_datos.DataBind();
        pnl_grilla.Visible = true;
        pnl_formulario.Visible = false;
    }
}