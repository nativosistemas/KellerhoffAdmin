using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SitioBase.clases;
using System.Web.Services;

public partial class admin_pages_GestionSucursal : cBaseAdmin
{
    public const string consPalabraClave = "gestionsucursal";
    protected void Page_Load(object sender, EventArgs e)
    {
        Seguridad(consPalabraClave);
        if (!IsPostBack)
        {
            Session["GestionSucursal_Filtro"] = null;
            //Session["GestionSucursal_IdSucursal"] = null;
            //cmbSucursal.Attributes.Add("onchange", "someFunction();");
            cmd_guardar.Attributes.Add("onclick", "return CargarSucursal();");
            cmd_nuevo.Attributes.Add("onclick", "ComboVacio();");
        }
    }
    protected void cmd_nuevo_Click(object sender, EventArgs e)
    {
        LlamarMetodosAcciones(SitioBase.Constantes.cSQL_INSERT, null, consPalabraClave);
    }
    public override void Insertar()
    {
        //cmbSucursal.SelectedIndex = -1;
        //cmbSucursalDependiente.SelectedIndex = -1;
        pnl_grilla.Visible = false;
        pnl_formulario.Visible = true;
    }
    protected void cmd_buscar_Click(object sender, EventArgs e)
    {
        Session["GestionSucursal_Filtro"] = txt_buscar.Text;
        gv_datos.DataBind();
    }
    protected void gv_datos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Eliminar")
        {
            LlamarMetodosAcciones(SitioBase.Constantes.cSQL_DELETE, Convert.ToInt32(e.CommandArgument), consPalabraClave);
        }
    }
    public override void Eliminar(int pIdSucursal)
    {
        WebService.EliminarSucursal(pIdSucursal);
        gv_datos.DataBind();
    }
    protected void cmd_cancelar_Click(object sender, EventArgs e)
    {
        pnl_grilla.Visible = true;
        pnl_formulario.Visible = false;
    }

    protected void cmd_guardar_Click(object sender, EventArgs e)
    {
        //string codigoSucursal = cmbSucursal.Items[ cmbSucursal.SelectedIndex].Text;
        //string codigoSucursalDependiente = cmbSucursalDependiente.Items[cmbSucursalDependiente.SelectedIndex].Text;
        //WebService.InsertarActualizarSucursal(0, codigoSucursal, codigoSucursalDependiente);
        gv_datos.DataBind();
        pnl_grilla.Visible = true;
        pnl_formulario.Visible = false;

    }
    protected void cmbSucursal_SelectedIndexChanged(object sender, EventArgs e)
    {
   
    string codigoSurcursalSeleccionado  = (sender as DropDownList).Items[(sender as DropDownList).SelectedIndex].Value;
    List<string> listaTodasSucursales = WebService.RecuperarTodasSucursales().Select(x => x.sde_sucursal).Distinct().ToList();
    List<cSucursal> listaTodasSucursalesConDependencia = WebService.RecuperarTodasSucursalesDependientes().Where(x => x.sde_sucursal == codigoSurcursalSeleccionado).ToList();
    var listaDependiente = (from suc in  listaTodasSucursales
                             where !(listaTodasSucursalesConDependencia.Select(x => x.sde_sucursalDependiente) ).Contains(suc)
                             select suc).ToList();
      //cmbSucursalDependiente.Items.Clear();
      //foreach (string item in listaDependiente)
      //{
      //    cmbSucursalDependiente.Items.Add(item);
      //}    
    }

    [WebMethod(EnableSession = true)]
    public static string RecuperarSucursalesDependientes(string pSucursal)
    {
        List<string> listaTodasSucursales = WebService.RecuperarTodasSucursales().Select(x => x.sde_sucursal).Distinct().ToList();
        List<cSucursal> listaTodasSucursalesConDependencia = WebService.RecuperarTodasSucursalesDependientes().Where(x => x.sde_sucursal == pSucursal).ToList();
        var listaDependiente = (from suc in listaTodasSucursales
                                where !(listaTodasSucursalesConDependencia.Select(x => x.sde_sucursalDependiente)).Contains(suc)
                                select suc).ToList();

        return SitioBase.clases.Serializador.SerializarAJson(listaDependiente);      
    }


    [WebMethod(EnableSession = true)]
    public static string InsertarActualizarSucursal(string pSucursal ,string pSucursalDependiente)
    {
        WebService.InsertarActualizarSucursal(0, pSucursal, pSucursalDependiente);
        return "Ok";
    }
}