using SitioBase.capaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


/// <summary>
/// Descripción breve de cHomePage
/// </summary>
public class cHomePage : System.Web.UI.Page
{

    [WebMethod(EnableSession = true)]
    public static string loginCarrito(string pName, string pPass, int pIdOferta)
    {
        string resultado = null;
        resultado = login(pName, pPass);
        if (resultado == "Ok" && HttpContext.Current.Session["clientesDefault_Cliente"] != null)
        {
            cClientes oCliente = (cClientes)HttpContext.Current.Session["clientesDefault_Cliente"];
            WebService.InsertarOfertaRating(pIdOferta, oCliente.cli_codigo, true);
            cOferta o = WebService.RecuperarOfertaPorId(pIdOferta);
            if (o != null)
            {
                HttpContext.Current.Session["home_Tipo"] = o.ofe_tipo;
                if (o.tfr_codigo != null)
                    HttpContext.Current.Session["home_IdTransfer"] = o.tfr_codigo;
            }
            HttpContext.Current.Session["home_IdOferta"] = pIdOferta;
        }
        return resultado;
    }
    [WebMethod(EnableSession = true)]
    public static string login(string pName, string pPass)
    {
        string resultado = null;

        string userAgent = HttpContext.Current.Request.UserAgent;
        string ip = HttpContext.Current.Server.HtmlEncode(HttpContext.Current.Request.UserHostAddress);
        string hostName = HttpContext.Current.Request.UserHostName;
        SitioBase.capaDatos.Usuario user = SitioBase.clases.Seguridad.Login(pName, pPass, ip, hostName, userAgent);
        if (user != null)
        {
            if (user.id != -1)
            {
                if (user.usu_estado == SitioBase.Constantes.cESTADO_ACTIVO && user.usu_codCliente != null)
                {
                    if (user.idRol == SitioBase.Constantes.cROL_ADMINISTRADORCLIENTE || user.idRol == SitioBase.Constantes.cROL_OPERADORCLIENTE)
                    {
                        WebService.Autenticacion objAutenticacion = new WebService.Autenticacion();
                        objAutenticacion.UsuarioNombre = System.Configuration.ConfigurationManager.AppSettings["ws_usu"];
                        objAutenticacion.UsuarioClave = System.Configuration.ConfigurationManager.AppSettings["ws_psw"];
                        WebService.CredencialAutenticacion = objAutenticacion;
                        HttpContext.Current.Session["clientesDefault_Cliente"] = WebService.RecuperarClientePorId((int)user.usu_codCliente);

                        if (HttpContext.Current.Session["clientesDefault_Cliente"] != null)
                        {
                            HttpContext.Current.Session["clientesDefault_Usuario"] = user;
                            List<string> listaPermisoDenegados = FuncionesPersonalizadas.RecuperarSinPermisosSecciones(((SitioBase.capaDatos.Usuario)HttpContext.Current.Session["clientesDefault_Usuario"]).id);
                            CargarAccionesEnVariableSession();
                            HttpContext.Current.Session["ClientesBase_isLogeo"] = true;
                            //HttpContext.Current.Response.Redirect("~/clientes/pages/PedidosBuscador.aspx");// Response.Redirect("~/clientes/pages/PedidosBuscadorNew.aspx");
                            resultado = "Ok";
                        }
                        else
                        {
                            resultado = "Error al recuperar el cliente";
                        }
                    }
                    else
                    {
                        resultado = "Usuario con rol sin permiso";
                    }
                }
                else
                {
                    if (user.usu_codCliente == null)
                    {
                        resultado = "Usuario no asigando cliente";
                    }
                    else
                    {
                        resultado = "Usuario inactivo";
                    }
                }
            }
            else
            {
                resultado = "Mail o contraseña erróneo";
            }
        }
        else
        {
            resultado = "Error en el servidor";
        }


        return resultado;
    }
    public static void CargarAccionesEnVariableSession()
    {
        if (HttpContext.Current.Session["clientesDefault_Usuario"] != null)
        {
            ListaAcccionesRol listaAcciones = SitioBase.clases.Seguridad.RecuperarTodasAccionesPorIdRol((((SitioBase.capaDatos.Usuario)(HttpContext.Current.Session["clientesDefault_Usuario"])).idRol));
            HttpContext.Current.Session["BaseAdmin_PermisosRol"] = listaAcciones;
        }
    }
}