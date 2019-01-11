using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SitioBase.clases;
using SitioBase;


public partial class admin_pages_GestionContacto :cBaseAdmin
{
    public const string consPalabraClave = "gestioncontacto";
   
    protected void Page_Load(object sender, EventArgs e)
    {
        Seguridad(consPalabraClave);
        if (!IsPostBack)
        {
            Session["GestionNoticia_Filtro"] = null;
            Session["GestionNoticia_Tipo"] = 4;
            Session["GestionContacto_Filtro"] = "NO"; ;
            Session["GestionContacto_con_codContacto"] = null;
        }

    }
    protected void gv_datos_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gv_datos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Leido")
        {
            Session["GestionContacto_con_codContacto"] = Convert.ToInt32(e.CommandArgument);
            if (Session["GestionContacto_con_codContacto"] != null && Session["BaseAdmin_Usuario"] != null)
                 
            {
                DateTime? fec = (DateTime?)null;
                int codigoUsuarioEnSession = ((SitioBase.capaDatos.Usuario)Session["BaseAdmin_Usuario"]).id;
                WebService.InsertarActualizarContacto(Convert.ToInt32(Session["GestionContacto_con_codContacto"]), fec  , "", "", "", "", "", "SI", codigoUsuarioEnSession);
            }
            gv_datos.DataBind();

           

           // claseCon.Proced_Leido_Contactos(Convert.ToInt32(cod), "SI");
            gv_datos.DataBind();
        }
    }
    
    protected void cmd_buscar_Click(object sender, EventArgs e)
    {
        Session["GestionContacto_Filtro"] = txt_buscar.Text;
    }
    protected void cmb_leidos_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["GestionContacto_Filtro"] = cmb_leidos.Text;
        gv_datos.DataBind();
    }
    protected void gv_datos2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
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

    public override void Modificar(int pId)
    {
        Session["GestionContacto_Not_codNoticia"] = pId;
        SitioBase.capaDatos.cNoticia noticia = WebService.RecuperarNoticiaPorId(pId);

        if (noticia != null)
        {
            txtTitulo.Text = noticia.not_titulo;

            txt_bajada.Text = noticia.not_bajada;
            txt_descripcion.Content = noticia.not_descripcion;
            tab2.Visible = true;
            lbl_iframe.Text = "<iframe name='files' class='form_datos' src='../../pages/filemanager.aspx?grupo=" + Constantes.cTABLA_NOTICIA + "&codrel=" + Session["GestionContacto_Not_codNoticia"].ToString() + "&ancho=98%&alto=480px&pag=6' frameborder='0' width='100%' scrolling='no' height='490' style='margin:0px; padding:5px;'>&nbsp</iframe>";
            pnl_grilla.Visible = false;
            pnl_formulario.Visible = true;
        }
    }

    public override void Insertar()
    {
        Session["GestionContacto_Not_codNoticia"] = 0;
        pnl_grilla.Visible = false;
        pnl_formulario.Visible = true;
        txt_bajada.Text = string.Empty;
        txt_descripcion.Content = string.Empty;

        txtTitulo.Text = string.Empty;
        lbl_iframe.Text = string.Empty;
        tab2.Visible = false;
    }

    public override void CambiarEstado(int pId)
    {
        if (Session["BaseAdmin_Usuario"] != null)
        {
            int codigoUsuarioEnSession = ((SitioBase.capaDatos.Usuario)Session["BaseAdmin_Usuario"]).id;
            SitioBase.capaDatos.cNoticia noticia = WebService.RecuperarNoticiaPorId(pId);
            int estadoNoticia = noticia.not_estado == Constantes.cESTADO_ACTIVO ? Constantes.cESTADO_INACTIVO : Constantes.cESTADO_ACTIVO;
            WebService.CambiarEstadoNoticiaPorId(noticia.not_codNoticia, estadoNoticia, codigoUsuarioEnSession);
            gv_datos.DataBind();
        }
    }

    public override void Publicar(int pId)
    {
        if (Session["BaseAdmin_Usuario"] != null)
        {
            int codigoUsuarioEnSession = ((SitioBase.capaDatos.Usuario)Session["BaseAdmin_Usuario"]).id;
            SitioBase.capaDatos.cNoticia noticia = WebService.RecuperarNoticiaPorId(pId);
            WebService.PublicarNoticiaPorId(noticia.not_codNoticia, !(bool)noticia.not_isPublicar, codigoUsuarioEnSession);
            gv_datos.DataBind();
        }
    }

    protected void cmd_guardar_Click(object sender, EventArgs e)
    {
        if (Session["GestionContacto_Not_codNoticia"] != null && Session["BaseAdmin_Usuario"] != null)
        {
            int codigoNoticia = Convert.ToInt32(Session["GestionContacto_Not_codNoticia"]);
            if ((codigoNoticia == 0 && SitioBase.clases.cBaseAdmin.isAgregar(consPalabraClave)) || (codigoNoticia != 0 && SitioBase.clases.cBaseAdmin.isEditar(consPalabraClave)))
            {
                int codigoUsuarioEnSession = ((SitioBase.capaDatos.Usuario)Session["BaseAdmin_Usuario"]).id;
                DateTime fechaDesde = DateTime.Now;
                DateTime? fechaHasta = null;
                //WebService.InsertarActualizarNoticia(codigoNoticia, fechaDesde, fechaHasta, txtTitulo.Text, txt_bajada.Text, txt_descripcion.Content, 4, codigoUsuarioEnSession);
                WebService.InsertarActualizarNoticia(codigoNoticia, fechaDesde, fechaHasta, txtTitulo.Text, txt_bajada.Text, txt_descripcion.Content,string.Empty, 4, codigoUsuarioEnSession);
            }
        }
        gv_datos2.DataBind();
        pnl_grilla.Visible = true;
        pnl_formulario.Visible = false;
    }

    protected void cmd_cancelar_Click(object sender, EventArgs e)
    {

        pnl_grilla.Visible = true;
        pnl_formulario.Visible = false;
    }
}