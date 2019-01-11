using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SitioBase.clases;

public partial class admin_pages_GestionFrases : cBaseAdmin
{
    public const string consPalabraClave = "gestionfrases";
    protected void Page_Load(object sender, EventArgs e)
    {
        Seguridad(consPalabraClave);
        if (!IsPostBack)
        {

            Session["GestionFrases_Filtro"] = null;

        }
    }
    protected void cmd_nuevo_Click(object sender, EventArgs e)
    {
        Session["GestionFrases_Codigo"] = 0;
        txt_nombre.Text = string.Empty;
        pnl_grilla.Visible = false;
        pnl_formulario.Visible = true;
    }

    protected void cmd_buscar_Click(object sender, EventArgs e)
    {
        //Session["GestionFrases_Filtro"] = txt_buscar.Text;
        //gv_datos.DataBind();
    }
    protected void cmd_guardar_Click(object sender, EventArgs e)
    {
        if (Session["GestionFrases_Codigo"] != null)
        {
            int codFrase = Convert.ToInt32(Session["GestionFrases_Codigo"]);
            WebService.InsertarActualizarFrasesFront(codFrase, txt_nombre.Text);
            pnl_grilla.Visible = true;
            pnl_formulario.Visible = false;
            gv_datos.DataBind();
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
            Session["GestionFrases_Codigo"] = Convert.ToInt32(e.CommandArgument);
            cFrasesFront obj = WebService.RecuperarTodasFrasesFront().Where(x => x.tff_id == Convert.ToInt32(e.CommandArgument)).First();
            txt_nombre.Text = obj.tff_nombre;
            pnl_grilla.Visible = false;
            pnl_formulario.Visible = true;
         //   gv_datos.DataBind();
        }
        else if (e.CommandName == "Estado")
        {
            Session["GestionFrases_Codigo"] = Convert.ToInt32(e.CommandArgument);
            cFrasesFront obj = WebService.RecuperarTodasFrasesFront().Where(x => x.tff_id == Convert.ToInt32(e.CommandArgument)).First();       
            WebService.CambiarEstadoPublicarFrasesFront(Convert.ToInt32(e.CommandArgument),!obj.tff_publicar);
            gv_datos.DataBind();
            pnl_grilla.Visible = true;
            pnl_formulario.Visible = false;
        }
        else if (e.CommandName == "Eliminar")
        {
            WebService.EliminarFrasesFront(Convert.ToInt32(e.CommandArgument));
            gv_datos.DataBind();
            pnl_grilla.Visible = true;
            pnl_formulario.Visible = false;
        }
    }
    protected void odsSucursalDependiente_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {

    }
    protected void cmdCodigoReparto_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarGrilla();
    }
    public void CargarGrilla()
    {

        //if (cmbSucursalDependiente.SelectedIndex > -1 && cmdCodigoReparto.SelectedIndex > -1)
        //{
        //    String[] arraySuc = cmbSucursalDependiente.Items[cmbSucursalDependiente.SelectedIndex].Text.Split('-');
        //    if (arraySuc.Count() > 1)
        //    {
        //        string suc = arraySuc[0].Trim();
        //        string sucDependiente = arraySuc[1].Trim();
        //        string codReparto = cmdCodigoReparto.Items[cmdCodigoReparto.SelectedIndex].Text;
        //        Session["GestionHorarioSucursal_Suc"] = suc;
        //        Session["GestionHorarioSucursal_SucDependiente"] = sucDependiente;
        //        Session["GestionHorarioSucursal_CodReparto"] = codReparto;
        //        //gv_datos.DataSource = ObternerSucursales(suc, sucDependiente, codReparto);
        //        gv_datos.DataBind();
        //    }

        //}
    }
}