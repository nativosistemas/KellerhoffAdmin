using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SitioBase.clases;

public partial class admin_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtUsuario.Focus();
            string isCerrarSesion = Request.QueryString["c"];
            if (isCerrarSesion != string.Empty)
            {
                if (Session["BaseAdmin_Usuario"] != null)
                {
                    SitioBase.clases.Seguridad.CerrarSession(((SitioBase.capaDatos.Usuario)Session["BaseAdmin_Usuario"]).idUsuarioLog);
                }
                lblMensaje.Text = string.Empty;
                txtPassword.Text = string.Empty;
                txtUsuario.Text = string.Empty;
                Session["BaseAdmin_Usuario"] = null;
            }
        }
    }
    protected void btnIngresar_Click(object sender, EventArgs e)
    {
        string userAgent = Request.UserAgent;
        string ip = Server.HtmlEncode(Request.UserHostAddress);
        string hostName = Request.UserHostName;
        SitioBase.capaDatos.Usuario user = SitioBase.clases.Seguridad.Login(txtUsuario.Text, txtPassword.Text, ip, hostName, userAgent);
        if (user != null)
        {
            if (user.id != -1)
            {
                if (user.usu_estado == SitioBase.Constantes.cESTADO_ACTIVO)
                {
                    if (user.idRol != SitioBase.Constantes.cROL_ADMINISTRADORCLIENTE && user.idRol != SitioBase.Constantes.cROL_OPERADORCLIENTE)
                    {
                        Session["BaseAdmin_Usuario"] = user;
                        WebService.Autenticacion objAutenticacion = new WebService.Autenticacion();
                        objAutenticacion.UsuarioNombre = System.Configuration.ConfigurationManager.AppSettings["ws_usu"];
                        objAutenticacion.UsuarioClave = System.Configuration.ConfigurationManager.AppSettings["ws_psw"];
                        WebService.CredencialAutenticacion = objAutenticacion;
                        cBaseAdmin.CargarAccionesEnVariableSession();
                        Response.Redirect("~/admin/pages/MenuPrincipal.aspx");
                    }
                    else 
                    {
                        lblMensaje.Text = "Usuario con rol sin permiso";
                    }
                }
                else 
                {
                    lblMensaje.Text = "Usuario inactivo";
                }
            }
            else
            {
                lblMensaje.Text = "Usuario o contraseña erróneo";
            }
        }
        else
        {
            lblMensaje.Text = "Error en el servidor";
        }
    }
}