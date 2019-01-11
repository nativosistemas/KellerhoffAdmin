using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SitioBase.clases;

public partial class admin_pages_GestionCategoriaRestriccion : cBaseAdmin
{
    public const string consPalabraClave = "gestioncategoriarestriccion";
    protected void Page_Load(object sender, EventArgs e)
    {
        Seguridad(consPalabraClave);
        if (!IsPostBack)
        {
            Session["GestionCategoriaRestriccion_Filtro"] = null;
            Session["GestionCategoriaRestriccion_Tcr_id"] = null;
        }
    }
    protected void cmd_nuevo_Click(object sender, EventArgs e)
    {
        odsSucursales.SelectMethod = "RecuperarTodasSucursalesSinCategoriaRestriccion";
        cmbSucursales.Enabled = true;
        cmbSucursales.DataBind();
        Session["GestionCategoriaRestriccion_Tcr_id"] = 0;
        cmbSucursales.SelectedIndex = -1;
        txtUnidadesMinimas.Text = string.Empty;
        txtUnidadesMaximas.Text = string.Empty;
        txtMontoMinimo.Text = string.Empty;
        txtMontoIgnorar.Text = string.Empty;
        pnl_grilla.Visible = false;
        pnl_formulario.Visible = true;
    }

    protected void cmd_buscar_Click(object sender, EventArgs e)
    {
        Session["GestionCategoriaRestriccion_Filtro"] = txt_buscar.Text;
        gv_datos.DataBind();
    }
    protected void cmd_guardar_Click(object sender, EventArgs e)
    {
        if (Session["GestionCategoriaRestriccion_Tcr_id"] != null && cmbSucursales.SelectedValue != null && !string.IsNullOrEmpty(txtUnidadesMinimas.Text) && !string.IsNullOrEmpty(txtUnidadesMaximas.Text) && !string.IsNullOrEmpty(txtMontoMinimo.Text) && !string.IsNullOrEmpty(txtMontoIgnorar.Text))
        {
            int idCategoriaRestriccion = Convert.ToInt32(Session["GestionCategoriaRestriccion_Tcr_id"]);

            string codSucursales = Convert.ToString(cmbSucursales.SelectedValue);
            int unidadMinima = Convert.ToInt32(txtUnidadesMinimas.Text);
            int unidadMaximas = Convert.ToInt32(txtUnidadesMaximas.Text);
            double montoMinimo = Convert.ToDouble(txtMontoMinimo.Text);
            double montoIgnorar = Convert.ToDouble(txtMontoIgnorar.Text);

            WebService.InsertarActualizarCadeteriaRestricciones(idCategoriaRestriccion, codSucursales, unidadMinima, unidadMaximas, montoMinimo, montoIgnorar);
            
            pnl_grilla.Visible = true;
            pnl_formulario.Visible = false;
            gv_datos.DataBind();

        }
    }
    protected void cmd_cancelar_Click(object sender, EventArgs e)
    {
        lblMensajeError.Text = string.Empty;
        pnl_grilla.Visible = true;
        pnl_formulario.Visible = false;
    }

    protected void gv_datos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modificar")
        {
            odsSucursales.SelectMethod = "RecuperarTodasSucursales";
            cmbSucursales.DataBind();
            Session["GestionCategoriaRestriccion_Tcr_id"] = Convert.ToInt32(e.CommandArgument);
            cCadeteriaRestricciones obj = WebService.RecuperarTodosCadeteriaRestricciones().Where(x => x.tcr_id == Convert.ToInt32(e.CommandArgument)).First();
            txtMontoIgnorar.Text = obj.tcr_MontoIgnorar.ToString();
            txtMontoMinimo.Text = obj.tcr_MontoMinimo.ToString();
            txtUnidadesMaximas.Text = obj.tcr_UnidadesMaximas.ToString();
            txtUnidadesMinimas.Text = obj.tcr_UnidadesMinimas.ToString();
            cmbSucursales.SelectedIndex = cmbSucursales.Items.IndexOf(cmbSucursales.Items.FindByValue(obj.tcr_codigoSucursal));
            cmbSucursales.Enabled = false;
            pnl_grilla.Visible = false;
            pnl_formulario.Visible = true;
        }
        else if (e.CommandName == "Eliminar")
        {
            WebService.EliminarCadeteriaRestricciones(Convert.ToInt32(e.CommandArgument));
            gv_datos.DataBind();
        }
    }
}