using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SitioBase.clases;

public partial class admin_pages_GestionMontoMinimo : cBaseAdmin
{
    public const string consPalabraClave = "gestionmontominimo";
    protected void Page_Load(object sender, EventArgs e)
    {
        Seguridad(consPalabraClave);
        if (!IsPostBack)
        {
            Session["GestionMontoMinimo_Filtro"] = null;
            Session["GestionMontoMinimo_Suc"] = null;
        }
    }
    protected void cmd_guardar_Click(object sender, EventArgs e)
    {
        if (Session["GestionMontoMinimo_Suc"] != null)
        {
            lblMensajeErrorMontoMinimo.Text = string.Empty;
            bool isIngreso = true;
            decimal nro = 0;
            try
            {
                 nro = Convert.ToDecimal(txt_nombre.Text);
            }
            catch
            {
                isIngreso = false;
                lblMensajeErrorMontoMinimo.Text = "Ingrese un valor decimal";
            }
            if (isIngreso)
            {
                WebService.AgregarMontoMinimo(Session["GestionMontoMinimo_Suc"].ToString(), nro);
                gv_datos.DataBind();
                pnl_grilla.Visible = true;
                pnl_formulario.Visible = false;
            }
      
        }
    }
    protected void cmd_cancelar_Click(object sender, EventArgs e)
    {
        lblMensajeErrorMontoMinimo.Text = string.Empty;
        pnl_grilla.Visible = true;
        pnl_formulario.Visible = false;
    }
    protected void gv_datos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modificar")
        {
            Session["GestionMontoMinimo_Suc"] = e.CommandArgument.ToString();
            var query = WebService.RecuperarTodasSucursales().Where(x => x.suc_codigo == e.CommandArgument.ToString()).ToList();

            //var query = WebService.RecuperarTodosHorariosSucursalDependiente().Where(x => x.sdh_sucursal == Session["GestionHorarioSucursal_Suc"].ToString() && x.sdh_sucursalDependiente == Session["GestionHorarioSucursal_SucDependiente"].ToString() && x.sdh_diaSemana == e.CommandArgument.ToString()).ToList();
            //Session["GestionHorarioSucursal_Dia"] = e.CommandArgument.ToString();
            //txt_nombre.Text = string.Empty;
            if (query.Count > 0)
            {
                txt_nombre.Text = query[0].suc_montoMinimo.ToString();
                lblSucursalDatos.Text = query[0].suc_nombre;
            }

            pnl_grilla.Visible = false;
            pnl_formulario.Visible = true;
        }
        
    }
    protected void cmd_nuevo_Click(object sender, EventArgs e)
    {

    }
    protected void cmd_buscar_Click(object sender, EventArgs e)
    {

    }
}