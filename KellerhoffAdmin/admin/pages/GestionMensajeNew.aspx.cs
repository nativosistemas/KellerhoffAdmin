using SitioBase;
using SitioBase.clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_pages_GestionMensajeNew : cBaseAdmin
{
    public const string consPalabraClave = "gestionmensaje";
    protected void Page_Load(object sender, EventArgs e)
    {
        Seguridad(consPalabraClave);
        if (!IsPostBack)
        {
            cmbEstado.Items.Add(new ListItem("Sin leer", Constantes.cESTADO_SINLEER.ToString()));
            cmbEstado.Items.Add(new ListItem("Leido", Constantes.cESTADO_LEIDO.ToString()));
            Session["GestionMensaje_Filtro"] = null;
            Session["GestionMensaje_tme_codigo"] = null;
        }
    }
    public override void Modificar(int pId)
    {
        Session["GestionMensaje_tme_codigo"] = pId;
        cMensaje mensaje = WebService.RecuperarMensajePorId(pId);

        if (mensaje != null)
        {

            int clienteCombo = 0;
            Session["GestionMensaje_tme_todosSucursales"] = mensaje.tme_todosSucursales;
            Session["GestionMensaje_tme_todos"] = mensaje.tme_todos;
            Session["GestionMensaje_isNuevo"] = false;

            checkboxImportante.Checked = mensaje.tme_importante;
            if (mensaje.tme_importante)
            {
                PanelFecha.Visible = checkboxImportante.Checked;
                //CalendarFechaDesde.
                CalendarFechaDesde.SelectedDate = (DateTime)mensaje.tme_fechaDesde;
                CalendarFechaHasta.SelectedDate = (DateTime)mensaje.tme_fechaHasta;
            }
            else
            {
                PanelFecha.Visible = checkboxImportante.Checked;
                clienteCombo = mensaje.tme_codClienteDestinatario;
            }
            txt_asunto.Text = mensaje.tme_asunto;
            txt_mensaje.Content = mensaje.tme_mensaje;
            cmbClientes.SelectedIndex = cmbClientes.Items.IndexOf(cmbClientes.Items.FindByValue(clienteCombo.ToString()));
            cmbEstado.SelectedIndex = cmbEstado.Items.IndexOf(cmbEstado.Items.FindByValue(mensaje.tme_estado.ToString()));
            pnl_grilla.Visible = false;
            pnl_formulario.Visible = true;

            if (mensaje.tme_todosSucursales != null && mensaje.tme_todosSucursales.Value > 0)
            {
                RadioButtonSucursal.Checked = true;
                RadioButtonCliente.Checked = false;
                RadioButtonSucursal.Enabled = false;
                RadioButtonCliente.Enabled = false;
                cmbClientes.Enabled = false;
                CheckBoxListSucursales.Enabled = false;
                List<string> listSucursalesSelect = WebService.ObtenerTodasSucursalesPorMensajeSucursalId(mensaje.tme_todosSucursales.Value);
                if (CheckBoxListSucursales.Items != null && CheckBoxListSucursales.Items.Count > 0)
                {
                    for (int i = 0; i < CheckBoxListSucursales.Items.Count; i++)
                    {
                        if (listSucursalesSelect.Contains(CheckBoxListSucursales.Items[i].Value))
                            CheckBoxListSucursales.Items[i].Selected = true;
                        else
                            CheckBoxListSucursales.Items[i].Selected = false;
                    }
                }
                else
                {
                    CheckBoxListSucursales.DataBind();
                    for (int i = 0; i < CheckBoxListSucursales.Items.Count; i++)
                    {
                        if (listSucursalesSelect.Contains(CheckBoxListSucursales.Items[i].Value))
                            CheckBoxListSucursales.Items[i].Selected = true;
                        else
                            CheckBoxListSucursales.Items[i].Selected = false;
                    }
                }

            }
            else
            {
                RadioButtonCliente.Checked = true;
                RadioButtonSucursal.Checked = false;
                RadioButtonSucursal.Enabled = false;
                RadioButtonCliente.Enabled = false;
                cmbClientes.Enabled = true;
                CheckBoxListSucursales.Enabled = false;
                if (CheckBoxListSucursales.Items != null)
                {
                    for (int i = 0; i < CheckBoxListSucursales.Items.Count; i++)
                    {
                        CheckBoxListSucursales.Items[i].Selected = false;
                    }
                }
            }
        }
    }
    public override void Insertar()
    {
        Session["GestionMensaje_isNuevo"] = true;
        Session["GestionMensaje_tme_todos"] = 0;
        Session["GestionMensaje_tme_codigo"] = 0;
        Session["GestionMensaje_tme_todosSucursales"] = 0;
        PanelFecha.Visible = false;
        checkboxImportante.Checked = false;
        CalendarFechaDesde.SelectedDate = DateTime.Now;
        CalendarFechaHasta.SelectedDate = DateTime.Now;
        pnl_grilla.Visible = false;
        pnl_formulario.Visible = true;
        cmbClientes.SelectedIndex = -1;
        cmbEstado.SelectedIndex = -1;
        txt_asunto.Text = string.Empty;
        txt_mensaje.Content = string.Empty;
        //
        SetearComboSucursalCliente();
    }
    public void SetearComboSucursalCliente()
    {
        RadioButtonSucursal.Enabled = true;
        RadioButtonCliente.Enabled = true;
        RadioButtonCliente.Checked = true;
        RadioButtonSucursal.Checked = false;
        cmbClientes.Enabled = true;
        CheckBoxListSucursales.Enabled = true;
        if (CheckBoxListSucursales.Items != null)
        {
            for (int i = 0; i < CheckBoxListSucursales.Items.Count; i++)
            {
                CheckBoxListSucursales.Items[i].Selected = false;
            }
        }
        //CheckRadioButton();
    }
    public override void Eliminar(int pIdMensaje)
    {
        if (SitioBase.clases.cBaseAdmin.isEliminar(consPalabraClave))
        {
            cMensaje mensaje = WebService.RecuperarMensajePorId(pIdMensaje);
            if (mensaje.tme_todos != null)
            {
                WebService.ElimimarTodosMensajePorId((int)mensaje.tme_todos);
            }
            else if (mensaje.tme_todosSucursales != null)
            {
                WebService.ElimimarTodosMensajeSucursalesPorId(mensaje.tme_todosSucursales.Value);
            }
            else
            {
                WebService.ElimimarMensajePorId(pIdMensaje);
            }
            gv_datos.DataBind();
        }
    }
    protected void cmd_nuevo_Click(object sender, EventArgs e)
    {
        LlamarMetodosAcciones(SitioBase.Constantes.cSQL_INSERT, null, consPalabraClave);
        CheckRadioButton();
        if (CheckBoxListSucursales.Items != null)
        {
            for (int i = 0; i < CheckBoxListSucursales.Items.Count; i++)
            {
                CheckBoxListSucursales.Items[i].Selected = false;
            }
        }
    }
    protected void cmd_buscar_Click(object sender, EventArgs e)
    {
        Session["GestionMensaje_Filtro"] = txt_buscar.Text;
    }
    protected void gv_datos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modificar")
        {
            LlamarMetodosAcciones(SitioBase.Constantes.cSQL_UPDATE, Convert.ToInt32(e.CommandArgument), consPalabraClave);
        }
        else if (e.CommandName == "Eliminar")
        {
            LlamarMetodosAcciones(SitioBase.Constantes.cSQL_DELETE, Convert.ToInt32(e.CommandArgument), consPalabraClave);
        }
    }
    protected void cmd_cancelar_Click(object sender, EventArgs e)
    {
        pnl_grilla.Visible = true;
        pnl_formulario.Visible = false;
    }
    protected void cmd_guardar_Click(object sender, EventArgs e)
    {
        if (Session["GestionMensaje_tme_codigo"] != null && Session["BaseAdmin_Usuario"] != null)
        {
            int codigoMensaje = Convert.ToInt32(Session["GestionMensaje_tme_codigo"]);
            if ((codigoMensaje == 0 && SitioBase.clases.cBaseAdmin.isAgregar(consPalabraClave)) || (codigoMensaje != 0 && SitioBase.clases.cBaseAdmin.isEditar(consPalabraClave)))
            {

                int codigoUsuarioEnSession = ((SitioBase.capaDatos.Usuario)Session["BaseAdmin_Usuario"]).id;
                DateTime? fechaDesde = null;
                DateTime? fechaHasta = null;
                bool importante = checkboxImportante.Checked;

                if (checkboxImportante.Checked)
                {
                    fechaDesde = CalendarFechaDesde.SelectedDate;
                    fechaHasta = CalendarFechaHasta.SelectedDate;
                }
                List<string> listaSucursal = new List<string>();
                //fechaDesde, fechaHasta, importante
                bool isTodos = false;
                bool isTodosSucursal = false;
                if (Convert.ToBoolean(Session["GestionMensaje_isNuevo"]) == true)
                {
                    if (RadioButtonSucursal.Checked)
                    {
                        isTodosSucursal = true;
                        if (CheckBoxListSucursales.Items != null)
                        {
                            for (int i = 0; i < CheckBoxListSucursales.Items.Count; i++)
                            {
                                if (CheckBoxListSucursales.Items[i].Selected)
                                {
                                    listaSucursal.Add(CheckBoxListSucursales.Items[i].Value);
                                }
                            }
                        }
                    }
                    if (RadioButtonCliente.Checked && Convert.ToInt32(cmbClientes.SelectedValue) == 0)
                        isTodos = true;
                }
                else
                {
                    if (Convert.ToInt32(cmbClientes.SelectedValue) == 0 && Session["GestionMensaje_tme_todos"] != null)
                        isTodos = true;
                }
                if (isTodosSucursal || (Session["GestionMensaje_tme_todosSucursales"] != null && Convert.ToInt32(Session["GestionMensaje_tme_todosSucursales"]) > 0))
                {
                    WebService.InsertarActualizarMensajeParaTodasSucursales(codigoMensaje, txt_asunto.Text,txt_mensaje.Content, codigoUsuarioEnSession, Convert.ToInt32(cmbEstado.SelectedValue), fechaDesde, fechaHasta, importante, Convert.ToInt32(Session["GestionMensaje_tme_todosSucursales"]), listaSucursal);
                }
                else if (isTodos)
                {
                    int tme_todos = Convert.ToInt32(Session["GestionMensaje_tme_todos"]);//HttpUtility.HtmlEncode(
                    WebService.InsertarMensajeParaTodosClientes(codigoMensaje, txt_asunto.Text, txt_mensaje.Content, codigoUsuarioEnSession, Convert.ToInt32(cmbEstado.SelectedValue), fechaDesde, fechaHasta, importante, tme_todos);
                }
                else
                {
                    WebService.InsertarActualizarMensaje(codigoMensaje, txt_asunto.Text, txt_mensaje.Content, Convert.ToInt32(cmbClientes.SelectedValue), codigoUsuarioEnSession, Convert.ToInt32(cmbEstado.SelectedValue), fechaDesde, fechaHasta, importante);
                }
            }
        }
        gv_datos.DataBind();
        pnl_grilla.Visible = true;
        pnl_formulario.Visible = false;
    }
    //protected void checkCLiente_CheckedChanged(object sender, EventArgs e)
    //{
    //    var ll = 0;
    //}
    protected void RadioButtonSucursal_CheckedChanged(object sender, EventArgs e)
    {
        CheckRadioButton();
    }

    protected void RadioButtonCliente_CheckedChanged(object sender, EventArgs e)
    {
        CheckRadioButton();
    }
    public void CheckRadioButton()
    {
        if (RadioButtonSucursal.Checked)
        {
            cmbClientes.Enabled = false;
            CheckBoxListSucursales.Enabled = true;
        }
        else
        {
            cmbClientes.Enabled = true;
            CheckBoxListSucursales.Enabled = false;
        }
    }
    protected void checkboxImportante_CheckedChanged(object sender, EventArgs e)
    {
        PanelFecha.Visible = checkboxImportante.Checked;
        CalendarFechaDesde.SelectedDate = DateTime.Now;
        CalendarFechaHasta.SelectedDate = DateTime.Now;
        //if (checkboxImportante.Checked)
        //{            
        //}
        //else 
        //{        
        //}
    }



    protected void CheckBoxListSucursales_Load1(object sender, EventArgs e)
    {
        //if (Session["GestionMensaje_tme_codigo"] != null && Convert.ToInt32(Session["GestionMensaje_tme_codigo"]) > 0)
        //{
        //    cMensaje mensaje = WebService.RecuperarMensajePorId(Convert.ToInt32(Session["GestionMensaje_tme_codigo"]));

        //    if (mensaje != null)
        //    {
        //        if (mensaje.tme_todosSucursales != null && mensaje.tme_todosSucursales.Value > 0)
        //        {
        //            List<string> listSucursalesSelect = WebService.ObtenerTodasSucursalesPorMensajeSucursalId(mensaje.tme_todosSucursales.Value);
        //            if (CheckBoxListSucursales.Items != null)
        //            {
        //                for (int i = 0; i < CheckBoxListSucursales.Items.Count; i++)
        //                {
        //                    if (listSucursalesSelect.Contains(CheckBoxListSucursales.Items[i].Value))
        //                        CheckBoxListSucursales.Items[i].Selected = true;
        //                    else
        //                        CheckBoxListSucursales.Items[i].Selected = false;
        //                }
        //            }
        //        }
        //    }
        //}
    }

    protected void ObjectDataSource1_DataBinding(object sender, EventArgs e)
    {

    }
    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //var lll =    txt_descripcion.Content;
    //}
}