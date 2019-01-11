using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SitioBase.clases;

public partial class admin_pages_GestionUsuario : cBaseAdmin
{
    public const string consPalabraClave = "gestionusuario";
    protected void Page_Load(object sender, EventArgs e)
    {
        Seguridad(consPalabraClave);
        if (!IsPostBack)
        {
            Session["GestionUsuario_Filtro"] = null;
            Session["GestionUsuario_Usu_codigo"] = null;
      
        }
    }
    //public bool HabilitarBotonAccion(int pAccion, string pPalabraClave)
    //{
    //    bool resultado = false;
    //    if (SitioBase.Constantes.cACCION_ALTA == pAccion)
    //    {
    //        resultado = SitioBase.clases.cBaseAdmin.isAgregar(pPalabraClave);
    //    }
    //    else if (SitioBase.Constantes.cACCION_MODIFICACION == pAccion)
    //    {
    //        resultado = SitioBase.clases.cBaseAdmin.isEditar(pPalabraClave);
    //    } 
    //    else if (SitioBase.Constantes.cACCION_CAMBIOESTADO == pAccion)
    //    {
    //        resultado = SitioBase.clases.cBaseAdmin.isEditar(pPalabraClave);
    //    }
    //    return resultado;
    //}
    protected void cmd_nuevo_Click(object sender, EventArgs e)
    {
        LlamarMetodosAcciones(SitioBase.Constantes.cSQL_INSERT, null, consPalabraClave);
    }

    protected void cmd_buscar_Click(object sender, EventArgs e)
    {
        Session["GestionUsuario_Filtro"] = txt_buscar.Text;
        gv_datos.DataBind();
    }

    protected void gv_datos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modificar")
        {
            LlamarMetodosAcciones(SitioBase.Constantes.cSQL_UPDATE, Convert.ToInt32(e.CommandArgument), consPalabraClave);
        }
        else if (e.CommandName == "Estado")
        {
            LlamarMetodosAcciones(SitioBase.Constantes.cSQL_ESTADO, Convert.ToInt32(e.CommandArgument), consPalabraClave);
        }
        else if (e.CommandName == "Eliminar")
        {
            SitioBase.clases.Seguridad.EliminarUsuario(Convert.ToInt32(e.CommandArgument));
            gv_datos.DataBind();
        }
        else if (e.CommandName == "Contraseña")
        {
            Session["GestionUsuario_Usu_codigo"] = e.CommandArgument;
            pnl_grilla.Visible = false;
            pnl_Contraseña.Visible = true;
        }
    }
    public override void Modificar(int pIdUsuario)
    {
        Session["GestionUsuario_Usu_codigo"] = pIdUsuario;
        SitioBase.capaDatos.cUsuario usuario = SitioBase.clases.Seguridad.RecuperarUsuarioPorId(pIdUsuario);
        txtNombre.Text = usuario.usu_nombre;
        txtApellido.Text = usuario.usu_apellido;
        txtObservaciones1.Text = usuario.usu_observacion;
        txtMail.Text = usuario.usu_mail;
        txtLogin.Text = usuario.usu_login;
        cmbRol.DataBind();
        cmbRol.SelectedIndex = cmbRol.Items.IndexOf(cmbRol.Items.FindByValue(usuario.usu_codRol.ToString()));
        if (usuario.usu_codRol == SitioBase.Constantes.cROL_ADMINISTRADORCLIENTE || usuario.usu_codRol == SitioBase.Constantes.cROL_OPERADORCLIENTE)
        {
            cmbCliente.Enabled = true;
        }
        else
        {
            cmbCliente.SelectedIndex = -1;
            cmbCliente.Enabled = false;
        }
        cmbCliente.DataBind();
        if (usuario.usu_codCliente != null)
        {
            cmbCliente.SelectedIndex = cmbCliente.Items.IndexOf(cmbCliente.Items.FindByValue(usuario.usu_codCliente.ToString()));
        }
        else
        {
            cmbCliente.SelectedIndex = -1;
        }
        //cmbCliente.SelectedIndex =cmbRol.Items.IndexOf(cmbRol.Items.FindByValue(usuario.usu_codCliente.ToString()));
        PanelContraseña.Visible = false;
        pnl_grilla.Visible = false;
        pnl_formulario.Visible = true;
    }
    public override void Insertar()
    {
        Session["GestionUsuario_Usu_codigo"] = 0;
        txtNombre.Text = string.Empty;
        txtApellido.Text = string.Empty;
        txtContraseña.Text = string.Empty;
        txtRepetirContraseña.Text = string.Empty;
        txtLogin.Text = string.Empty;
        txtObservaciones1.Text = string.Empty;
        txtMail.Text = string.Empty;
        cmbRol.SelectedIndex = -1;
        //if (Convert.ToInt32(cmbRol.SelectedValue) == SitioBase.Constantes.cROL_ADMINISTRADORCLIENTE || Convert.ToInt32(cmbRol.SelectedValue) == SitioBase.Constantes.cROL_OPERADORCLIENTE)
        //{
        //    cmbCliente.Enabled = true;
        //}
        //else
        //{
        cmbCliente.Enabled = false;
        //}
        cmbCliente.SelectedIndex = -1;
        PanelContraseña.Visible = true;
        pnl_grilla.Visible = false;
        pnl_formulario.Visible = true;
    }
    public override void CambiarEstado(int pIdUsuario)
    {
        if (Session["BaseAdmin_Usuario"] != null)
        {
            int codigoUsuarioEnSession = ((SitioBase.capaDatos.Usuario)Session["BaseAdmin_Usuario"]).id;
            SitioBase.capaDatos.cUsuario usuario = SitioBase.clases.Seguridad.RecuperarUsuarioPorId(pIdUsuario);
            int estadoUsuario = usuario.usu_estado == SitioBase.Constantes.cESTADO_ACTIVO ? SitioBase.Constantes.cESTADO_INACTIVO : SitioBase.Constantes.cESTADO_ACTIVO;
            SitioBase.clases.Seguridad.CambiarEstadoUsuarioPorId(usuario.usu_codigo, estadoUsuario, codigoUsuarioEnSession);
            gv_datos.DataBind();
        }
    }
    protected void cmd_cancelar_Click(object sender, EventArgs e)
    {
        pnl_grilla.Visible = true;
        pnl_formulario.Visible = false;
        pnl_Contraseña.Visible = false;
    }

    protected void cmd_guardar_Click(object sender, EventArgs e)
    {
        //CustomValidatorMail.IsValid &&
        if ( CustomValidatorLogin.IsValid)
        {
            if (Session["GestionUsuario_Usu_codigo"] != null && Session["BaseAdmin_Usuario"] != null)
            {
                int codUsuario = Convert.ToInt32(Session["GestionUsuario_Usu_codigo"]);
                if ((codUsuario == 0 && SitioBase.clases.cBaseAdmin.isAgregar(consPalabraClave)) || (codUsuario != 0 && SitioBase.clases.cBaseAdmin.isEditar(consPalabraClave)))
                {
                    int? codCliente = Convert.ToInt32(cmbCliente.SelectedValue) != -1 ? (int?)Convert.ToInt32(cmbCliente.SelectedValue) : null;
                    int? codigoUsuarioEnSession = ((SitioBase.capaDatos.Usuario)Session["BaseAdmin_Usuario"]).id;
                    SitioBase.clases.Seguridad.InsertarActualizarUsuario(codUsuario, Convert.ToInt32(cmbRol.SelectedValue), codCliente, txtNombre.Text, txtApellido.Text, txtMail.Text, txtLogin.Text, txtContraseña.Text, txtObservaciones1.Text, codigoUsuarioEnSession);
                }
            }
            gv_datos.DataBind();
            pnl_grilla.Visible = true;
            pnl_formulario.Visible = false;
        }
    }
    protected void CustomValidatorLogin_ServerValidate(object source, ServerValidateEventArgs args)
    {
        bool resultado = true;
        if (Session["GestionUsuario_Usu_codigo"] != null)
        {
            resultado = !SitioBase.clases.Seguridad.IsRepetidoLogin(Convert.ToInt32(Session["GestionUsuario_Usu_codigo"]), args.Value);
        }
        CustomValidatorLogin.ErrorMessage = "Login repetido";
        args.IsValid = resultado;
    }
    //protected void CustomValidatorMail_ServerValidate(object source, ServerValidateEventArgs args)
    //{
    //    bool resultado = true;
    //    if (!SitioBase.clases.Validaciones.isMail(args.Value))
    //    {
    //        resultado = false;
    //        CustomValidatorMail.ErrorMessage = "Mail incorrecto";
    //    }
    //    if (resultado)
    //    {
    //        if (Session["GestionUsuario_Usu_codigo"] != null)
    //        {
    //            List<SitioBase.capaDatos.cUsuario> listaUsuario = SitioBase.clases.Seguridad.RecuperarTodosUsuarios(string.Empty);
    //            foreach (SitioBase.capaDatos.cUsuario item in listaUsuario)
    //            {
    //                if (item.usu_mail == args.Value)
    //                {
    //                    if (Convert.ToInt32(Session["GestionUsuario_Usu_codigo"]) == 0 || Convert.ToInt32(Session["GestionUsuario_Usu_codigo"]) != item.usu_codigo)
    //                    {
    //                        resultado = false;
    //                        CustomValidatorMail.ErrorMessage = "Mail repetido";
    //                        break;
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    args.IsValid = resultado;
    //}

    protected void cmbRol_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(cmbRol.SelectedValue) == SitioBase.Constantes.cROL_ADMINISTRADORCLIENTE || Convert.ToInt32(cmbRol.SelectedValue) == SitioBase.Constantes.cROL_OPERADORCLIENTE)
        {
            cmbCliente.Enabled = true;
        }
        else
        {
            cmbCliente.SelectedIndex = -1;
            cmbCliente.Enabled = false;
        }
    }
    protected void btnGuardarContraseña_Click(object sender, EventArgs e)
    {
        if (Session["GestionUsuario_Usu_codigo"] != null && Session["BaseAdmin_Usuario"] != null)
        {
            int codigoUsuarioEnSession = ((SitioBase.capaDatos.Usuario)Session["BaseAdmin_Usuario"]).id;
            SitioBase.capaDatos.cUsuario objUsuario = null;
            cClientes objCliente = null;
            objUsuario = SitioBase.clases.Seguridad.RecuperarUsuarioPorId(Convert.ToInt32(Session["GestionUsuario_Usu_codigo"]));       
            SitioBase.clases.Seguridad.CambiarContraseñaUsuario(Convert.ToInt32(Session["GestionUsuario_Usu_codigo"]), txtContraseñaCambiar.Text, codigoUsuarioEnSession);
            if (objUsuario.usu_codRol == SitioBase.Constantes.cROL_ADMINISTRADORCLIENTE)
            {
                objCliente = WebService.RecuperarClienteAdministradorPorIdUsuarios(objUsuario.usu_codigo);
                //WebService.ModificarPasswordWEB(objCliente.cli_login, objUsuario.usu_pswDesencriptado, txtContraseñaCambiar.Text);
            }
            gv_datos.DataBind();
            pnl_grilla.Visible = true;
            pnl_Contraseña.Visible = false;
        }
    }
}