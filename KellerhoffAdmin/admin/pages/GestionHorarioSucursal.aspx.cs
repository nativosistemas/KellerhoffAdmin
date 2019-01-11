using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_pages_GestionHorarioSucursal : SitioBase.clases.cBaseAdmin
{
    public const string consPalabraClave = "gestionhorariosucursal";
    protected void Page_Load(object sender, EventArgs e)
    {
        Seguridad(consPalabraClave);
        if (!IsPostBack)
        {
            List<cSucursal> lista = WebService.RecuperarTodasSucursalesDependientes();
            if (lista.Count > 0)
            {
                 string listaCodReparto = WebService.RecuperarTodosCodigoReparto().First();
                 if (listaCodReparto != null)
                 {
                     gv_datos.DataSource = ObternerSucursales(lista[0].sde_sucursal, lista[0].sde_sucursalDependiente, listaCodReparto);
                     gv_datos.DataBind();
                 }
            }
            Session["GestionHorarioSucursal_Dia"] = null;
            Session["GestionHorarioSucursal_Suc"] = null;
            Session["GestionHorarioSucursal_SucDependiente"]= null;
            Session["GestionHorarioSucursal_CodReparto"] = null;
        }
    }
    public static List<string> ListaDiaSemana()
    {
        List<string> resultado = new List<string>();
        resultado.Add(SitioBase.Constantes.cDIASEMANA_Lunes);
        resultado.Add(SitioBase.Constantes.cDIASEMANA_Martes);
        resultado.Add(SitioBase.Constantes.cDIASEMANA_Miercoles);
        resultado.Add(SitioBase.Constantes.cDIASEMANA_Jueves);
        resultado.Add(SitioBase.Constantes.cDIASEMANA_Viernes);
        resultado.Add(SitioBase.Constantes.cDIASEMANA_Sabado);
        resultado.Add(SitioBase.Constantes.cDIASEMANA_Domingo);
        return resultado;
    }

    public static List<cGrillaHorarioSucursal> ObternerSucursales(string sde_sucursal, string sde_sucursalDependiente, string sdh_codReparto)
    {
        List<cGrillaHorarioSucursal> resultado = new List<cGrillaHorarioSucursal>();
        var query = WebService.RecuperarTodosHorariosSucursalDependiente().Where(x => x.sdh_sucursal == sde_sucursal && x.sdh_sucursalDependiente == sde_sucursalDependiente && x.sdh_codReparto == sdh_codReparto).ToList();
        List<string> lista = ListaDiaSemana();
        foreach (string item in lista)
        {
            cGrillaHorarioSucursal obj = new cGrillaHorarioSucursal();
            obj.dia = item;
            obj.hora = string.Empty;
            foreach (cHorariosSucursal itemHorariosSucursal in query)
            {
                if (itemHorariosSucursal.sdh_diaSemana == obj.dia)
                {
                    obj.hora = itemHorariosSucursal.sdh_horario;
                    break;
                }
            }
            resultado.Add(obj);
        }
        return resultado;
    }
    protected void gv_datos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modificar")
        {
            if (Session["GestionHorarioSucursal_Suc"] != null && Session["GestionHorarioSucursal_SucDependiente"] != null && Session["GestionHorarioSucursal_CodReparto"] != null)
            {
                var query = WebService.RecuperarTodosHorariosSucursalDependiente().Where(x => x.sdh_sucursal == Session["GestionHorarioSucursal_Suc"].ToString() && x.sdh_sucursalDependiente == Session["GestionHorarioSucursal_SucDependiente"].ToString() && x.sdh_codReparto == Session["GestionHorarioSucursal_CodReparto"].ToString() && x.sdh_diaSemana == e.CommandArgument.ToString()).ToList();
                Session["GestionHorarioSucursal_Dia"] = e.CommandArgument.ToString();
                txt_nombre.Text = string.Empty;
                if (query.Count > 0)
                {
                    txt_nombre.Text = query[0].sdh_horario;
                }
                pnl_grilla.Visible = false;
                pnl_formulario.Visible = true;
            }
        }
    }
    protected void cmd_guardar_Click(object sender, EventArgs e)
    {
        if (Session["GestionHorarioSucursal_Suc"] != null && Session["GestionHorarioSucursal_SucDependiente"] != null && Session["GestionHorarioSucursal_Dia"] != null && Session["GestionHorarioSucursal_CodReparto"] != null)
        {
            WebService.InsertarActualizarHorariosSucursalDependiente(0, Session["GestionHorarioSucursal_Suc"].ToString(), Session["GestionHorarioSucursal_SucDependiente"].ToString(), Session["GestionHorarioSucursal_CodReparto"].ToString(), Session["GestionHorarioSucursal_Dia"].ToString(), txt_nombre.Text);           
            if (cmbSucursalDependiente.SelectedIndex > -1)
            {
                gv_datos.DataSource = ObternerSucursales(Session["GestionHorarioSucursal_Suc"].ToString(), Session["GestionHorarioSucursal_SucDependiente"].ToString(), Session["GestionHorarioSucursal_CodReparto"].ToString());
                gv_datos.DataBind();
            }
            pnl_grilla.Visible = true;
            pnl_formulario.Visible = false;
        }
    }
    protected void cmd_cancelar_Click(object sender, EventArgs e)
    {
        pnl_grilla.Visible = true;
        pnl_formulario.Visible = false;
    }
    //protected void cmd_nuevo_Click(object sender, EventArgs e)
    //{
    //}
    protected void cmd_buscar_Click(object sender, EventArgs e)
    {

    }
    protected void cmbSucursalDependiente_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarGrilla();
    }
    public void CargarGrilla() {

        if (cmbSucursalDependiente.SelectedIndex > -1 && cmdCodigoReparto.SelectedIndex > -1)
        {
            String[] arraySuc = cmbSucursalDependiente.Items[cmbSucursalDependiente.SelectedIndex].Text.Split('-');
            if (arraySuc.Count() > 1)
            {
                string suc = arraySuc[0].Trim();
                string sucDependiente = arraySuc[1].Trim();
                string codReparto = cmdCodigoReparto.Items[cmdCodigoReparto.SelectedIndex].Text;
                Session["GestionHorarioSucursal_Suc"] = suc;
                Session["GestionHorarioSucursal_SucDependiente"] = sucDependiente;
                Session["GestionHorarioSucursal_CodReparto"] = codReparto;
                gv_datos.DataSource = ObternerSucursales(suc, sucDependiente, codReparto);
                gv_datos.DataBind();
            }

        }
    }
    protected void odsSucursalDependiente_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
      
    }
    protected void cmdCodigoReparto_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarGrilla();
    }
}