using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SitioBase.clases;

public partial class admin_pages_GestionTiposEnvios : cBaseAdmin
{
    public const string consPalabraClave = "gestiontiposenvios";
    protected void Page_Load(object sender, EventArgs e)
    {
        Seguridad(consPalabraClave);
        if (!IsPostBack)
        {
            Session["GestionTiposEnvios_Filtro"] = null;
            Session["GestionTiposEnvios_Env_id"] = null;
        }
    }
    protected void cmd_nuevo_Click(object sender, EventArgs e)
    {
        Session["GestionTiposEnvios_Env_id"] = 0;
        txt_nombre.Text = string.Empty;
        txt_codigo.Text = string.Empty;
        pnl_grilla.Visible = false;
        pnl_formulario.Visible = true;
    }

    protected void cmd_buscar_Click(object sender, EventArgs e)
    {
        Session["GestionTiposEnvios_Filtro"] = txt_buscar.Text;
        gv_datos.DataBind();
    }
    protected void cmd_guardar_Click(object sender, EventArgs e)
    {
        if (Session["GestionTiposEnvios_Env_id"] != null)
        {
            int codTiposEnvios = Convert.ToInt32(Session["GestionTiposEnvios_Env_id"]);
            if (!string.IsNullOrEmpty(txt_codigo.Text) && !string.IsNullOrEmpty(txt_nombre.Text))
            {
                WebService.InsertarActualizarTiposEnvios(codTiposEnvios, txt_codigo.Text, txt_nombre.Text);
                pnl_grilla.Visible = true;
                pnl_formulario.Visible = false;
                gv_datos.DataBind();
            }
        }
    }
    protected void cmd_cancelar_Click(object sender, EventArgs e)
    {
        pnl_grilla.Visible = true;
        pnl_formulario.Visible = false;
    }

    protected void gv_datos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modificar")
        {
            Session["GestionTiposEnvios_Env_id"] = Convert.ToInt32(e.CommandArgument);
            cTiposEnvios obj = WebService.RecuperarTodosTiposEnvios().Where(x => x.env_id == Convert.ToInt32(e.CommandArgument)).First();
            txt_codigo.Text = obj.env_codigo;
            txt_nombre.Text = obj.env_nombre;
            pnl_grilla.Visible = false;
            pnl_formulario.Visible = true;
        }
        else if (e.CommandName == "Eliminar")
        {
            WebService.EliminarTiposEnvios(Convert.ToInt32(e.CommandArgument));
            gv_datos.DataBind();
        }
    }
}