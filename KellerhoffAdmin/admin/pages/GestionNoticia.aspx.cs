using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SitioBase.clases;
using SitioBase;

public partial class admin_pages_GestionNoticia : cBaseAdmin
{
    public const string consPalabraClave = "gestionnoticia";
    protected void Page_Load(object sender, EventArgs e)
    {
        Seguridad(consPalabraClave);
        if (!IsPostBack)
        {
            Session["GestionNoticia_Filtro"] = null;
            Session["GestionNoticia_Tipo"] = 3;
            Session["GestionNoticia_Not_codNoticia"] = null;
        }
    }

    public override void Modificar(int pId)
    {
        Session["GestionNoticia_Not_codNoticia"] = pId;
        SitioBase.capaDatos.cNoticia noticia = WebService.RecuperarNoticiaPorId(pId);

        if (noticia != null)
        {
            txtTitulo.Text = noticia.not_titulo;
            if (noticia.not_fechaDesde != null)
            {
                //txt_fechaDesde.Text = noticia.not_fechaDesde.ToString();
                CalendarExtenderFechaDesde.SelectedDate = noticia.not_fechaDesde;
            }
            if (noticia.not_fechaHasta != null)
            {
                //txt_fechaHasta.Text = noticia.not_fechaHasta.ToString();
                CalendarExtenderFechaHasta.SelectedDate = noticia.not_fechaHasta;
            }
            txt_bajada.Text = noticia.not_bajada;
            txt_descripcion.Text = noticia.not_descripcion;
            txt_destacado.Text = noticia.not_destacado;
            tab2.Visible = true;
            lbl_iframe.Text = "<iframe name='files' class='form_datos' src='../../pages/filemanager.aspx?grupo=" + Constantes.cTABLA_NOTICIA + "&codrel=" + Session["GestionNoticia_Not_codNoticia"].ToString() + "&ancho=900px&alto=&pag=6' frameborder='0' width='100%' scrolling='no' height='490' style='margin:0px; padding:5px;'>&nbsp</iframe>";
            pnl_grilla.Visible = false;
            pnl_formulario.Visible = true;
        }
    }
    public override void Insertar()
    {
        Session["GestionNoticia_Not_codNoticia"] = 0;
        pnl_grilla.Visible = false;
        pnl_formulario.Visible = true;
        txt_bajada.Text = string.Empty;
        txt_descripcion.Text = string.Empty;
        txt_destacado.Text = string.Empty;
        txt_fechaDesde.Text = string.Empty;
        txt_fechaHasta.Text = string.Empty;
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
    public override void Eliminar(int pId)
    {
        List<SitioBase.capaDatos.cNoticia> home = WebService.RecuperarNoticiaHome("");
        bool isEliminar = false;

        foreach (SitioBase.capaDatos.cNoticia item in home)
        {
            if (pId != item.hom_codNoticia1 && pId != item.hom_codNoticia2 && pId != item.hom_codNoticia3)
            {
                isEliminar = true;
            }
            else
            {
                lbl_mensaje.Visible = true;
            }
            if (isEliminar)
            {
                WebService.EliminarNoticiaPorId(pId);
                gv_datos.DataBind();
            }

        }
    }
    protected void cmd_nuevo_Click(object sender, EventArgs e)
    {
        LlamarMetodosAcciones(SitioBase.Constantes.cSQL_INSERT, null, consPalabraClave);
    }
    protected void cmd_buscar_Click(object sender, EventArgs e)
    {
        Session["GestionNoticia_Filtro"] = txt_buscar.Text;
    }
    protected void gv_datos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lbl_mensaje.Visible = false;
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
        else if (e.CommandName == "Eliminar")
        {
            LlamarMetodosAcciones(SitioBase.Constantes.cSQL_DELETE, Convert.ToInt32(e.CommandArgument), consPalabraClave);
        }
        else if (e.CommandName == "Home1")
        {
            MostrarEnHome1(Convert.ToInt32(e.CommandArgument), SitioBase.Constantes.cSQL_UPDATE);
        }
        else if (e.CommandName == "Home2")
        {
            MostrarEnHome2(Convert.ToInt32(e.CommandArgument), SitioBase.Constantes.cSQL_UPDATE);
        }
        else if (e.CommandName == "Home3")
        {
            MostrarEnHome3(Convert.ToInt32(e.CommandArgument), SitioBase.Constantes.cSQL_UPDATE);
        }
    }
    protected void cmd_cancelar_Click(object sender, EventArgs e)
    {
        pnl_grilla.Visible = true;
        pnl_formulario.Visible = false;
    }
    protected void cmd_guardar_Click(object sender, EventArgs e)
    {
        if (Session["GestionNoticia_Not_codNoticia"] != null && Session["BaseAdmin_Usuario"] != null)
        {
            int codigoNoticia = Convert.ToInt32(Session["GestionNoticia_Not_codNoticia"]);
            if ((codigoNoticia == 0 && SitioBase.clases.cBaseAdmin.isAgregar(consPalabraClave)) || (codigoNoticia != 0 && SitioBase.clases.cBaseAdmin.isEditar(consPalabraClave)))
            {
                int codigoUsuarioEnSession = ((SitioBase.capaDatos.Usuario)Session["BaseAdmin_Usuario"]).id;
                DateTime fechaDesde = Convert.ToDateTime(txt_fechaDesde.Text);// DateTime.Now;
                DateTime? fechaHasta = txt_fechaHasta.Text != string.Empty ? Convert.ToDateTime(txt_fechaHasta.Text) : (DateTime?)null;
                WebService.InsertarActualizarNoticia(codigoNoticia, fechaDesde, fechaHasta, txtTitulo.Text, txt_bajada.Text, txt_descripcion.Text, txt_destacado.Text, 3, codigoUsuarioEnSession);
            }
        }
        gv_datos.DataBind();
        pnl_grilla.Visible = true;
        pnl_formulario.Visible = false;
    }

    public string MostrarEnHome1(int not, string accion)
    {
        string img = "";
        if (accion == "UPDATE")
        {
            List<SitioBase.capaDatos.cNoticia> home = WebService.RecuperarNoticiaHome("");


            foreach (SitioBase.capaDatos.cNoticia item in home)
            {
                if (not != item.hom_codNoticia2 && not != item.hom_codNoticia3)
                {
                    lbl_mensaje.Visible = false;
                    //WebService.InsertarActualizarHome(not, 0, accion);
                    WebService.InsertarActualizarHome(not, 0, 0, accion);
                    img = "~/img/iconos/award_star_gold_2.png";
                    gv_datos.DataBind();
                }
                else
                {
                    lbl_mensaje.Visible = true;

                }
            }
        }
        else
        {

            List<SitioBase.capaDatos.cNoticia> home = WebService.RecuperarNoticiaHome("");
            foreach (SitioBase.capaDatos.cNoticia item in home)
            {
                if (not == item.hom_codNoticia1)
                {
                    img = "~/img/iconos/award_star_gold_2.png";
                }
                else
                {
                    img = "~/img/iconos/award_star_silver_3.png";
                }
            }
        }
        return (img);


    }

    public string MostrarEnHome2(int not, string accion)
    {
        string img = "";
        if (accion == "UPDATE")
        {
            List<SitioBase.capaDatos.cNoticia> home = WebService.RecuperarNoticiaHome("");
            foreach (SitioBase.capaDatos.cNoticia item in home)
            {
                if (not != item.hom_codNoticia1 && not != item.hom_codNoticia3)
                {
                    lbl_mensaje.Visible = false;
                    //WebService.InsertarActualizarHome(0, not, accion);
                    WebService.InsertarActualizarHome(0, not, 0, accion);
                    img = "~/img/iconos/award_star_gold_2.png";
                    gv_datos.DataBind();
                }
                else
                {
                    lbl_mensaje.Visible = true;

                }
            }
        }
        else
        {
            List<SitioBase.capaDatos.cNoticia> home = WebService.RecuperarNoticiaHome("");


            foreach (SitioBase.capaDatos.cNoticia item in home)
            {
                if (not == item.hom_codNoticia2)
                {
                    img = "~/img/iconos/award_star_gold_2.png";
                }
                else
                {
                    img = "~/img/iconos/award_star_silver_3.png";
                }
            }
        }
        return (img);

    }
    public string MostrarEnHome3(int not, string accion)
    {
        string img = "";
        if (accion == "UPDATE")
        {
            List<SitioBase.capaDatos.cNoticia> home = WebService.RecuperarNoticiaHome("");
            foreach (SitioBase.capaDatos.cNoticia item in home)
            {
                if (not != item.hom_codNoticia1 && not != item.hom_codNoticia2)
                {
                    lbl_mensaje.Visible = false;
                    WebService.InsertarActualizarHome(0, 0, not, accion);
                    img = "~/img/iconos/award_star_gold_2.png";
                    gv_datos.DataBind();
                }
                else
                {
                    lbl_mensaje.Visible = true;

                }
            }
        }
        else
        {
            List<SitioBase.capaDatos.cNoticia> home = WebService.RecuperarNoticiaHome("");


            foreach (SitioBase.capaDatos.cNoticia item in home)
            {
                if (not == item.hom_codNoticia3)
                {
                    img = "~/img/iconos/award_star_gold_2.png";
                }
                else
                {
                    img = "~/img/iconos/award_star_silver_3.png";
                }
            }
        }
        return (img);

    }
}