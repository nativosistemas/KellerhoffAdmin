using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SitioBase.clases;
using System.Data;
using System.Collections;

public partial class admin_pages_GestionProcedimientos : cBaseAdmin
{
    public const string consPalabraClave = "gestionprocedimientos";

    protected void Page_Load(object sender, EventArgs e)
    {
        Seguridad(consPalabraClave);
        if (!IsPostBack)
        {
            Session["GestionProcedimientos"] = null;
            Session["GestionProcedimientos_filtro"] = null;
        }
        btnClientes.Attributes.Add("onclick", "javascritp:return DeseaSincronizar();");
        btnProducto.Attributes.Add("onclick", "javascritp:return DeseaSincronizar();");
        btnProductoOfertas.Attributes.Add("onclick", "javascritp:return DeseaSincronizar();");
        btnClientesTodos.Attributes.Add("onclick", "javascritp:return DeseaSincronizar();");
        btnProductoTodos.Attributes.Add("onclick", "javascritp:return DeseaSincronizar();");
        btnProductoOfertasTodos.Attributes.Add("onclick", "javascritp:return DeseaSincronizar();");
        btnProductoPrecios.Attributes.Add("onclick", "javascritp:return DeseaSincronizar();");
        btnProductoStocks.Attributes.Add("onclick", "javascritp:return DeseaSincronizar();");
        btnProductoValesTodos.Attributes.Add("onclick", "javascritp:return DeseaSincronizar();");
        btnTransfers.Attributes.Add("onclick", "javascritp:return DeseaSincronizar();");
        btnTransfersTodos.Attributes.Add("onclick", "javascritp:return DeseaSincronizar();");
        CargarFechasUltimaActualizacion();
        cmd_buscar.Focus();
    }
    protected void btnClientes_Click(object sender, EventArgs e)
    {
        if (Session["GestionProcedimientos"] == null)
        {
            Session["GestionProcedimientos"] = string.Empty;
            WebService.SincronizarClientes();
            panelMensaje.Visible = true;
            panelBotonesSicronizar.Visible = false;
            Session["GestionProcedimientos"] = null;
        }
    }
    protected void btnProducto_Click(object sender, EventArgs e)
    {
        if (Session["GestionProcedimientos"] == null)
        {
            Session["GestionProcedimientos"] = string.Empty;
            WebService.SincronizarProductos();
            panelMensaje.Visible = true;
            panelBotonesSicronizar.Visible = false; Session["GestionProcedimientos"] = null;
        }
    }
    protected void btnProductoOfertas_Click(object sender, EventArgs e)
    {
        if (Session["GestionProcedimientos"] == null)
        {
            Session["GestionProcedimientos"] = string.Empty;
            WebService.SincronizarOfertas();
            panelMensaje.Visible = true;
            panelBotonesSicronizar.Visible = false; Session["GestionProcedimientos"] = null;
        }
    }
    protected void btnProductoPrecios_Click(object sender, EventArgs e)
    {
        if (Session["GestionProcedimientos"] == null)
        {
            Session["GestionProcedimientos"] = string.Empty;
            WebService.SincronizarPrecios();
            panelMensaje.Visible = true;
            panelBotonesSicronizar.Visible = false; Session["GestionProcedimientos"] = null;
        }
    }
    protected void btnProductoStocks_Click(object sender, EventArgs e)
    {
        if (Session["GestionProcedimientos"] == null)
        {
            Session["GestionProcedimientos"] = string.Empty;
            WebService.SincronizarStocks();
            panelMensaje.Visible = true;
            panelBotonesSicronizar.Visible = false; Session["GestionProcedimientos"] = null;
        }
    }
    protected void btnProductoValesTodos_Click(object sender, EventArgs e)
    {
        if (Session["GestionProcedimientos"] == null)
        {
            Session["GestionProcedimientos"] = string.Empty;
            WebService.SincronizarVales_Todos();
            panelMensaje.Visible = true;
            panelBotonesSicronizar.Visible = false; Session["GestionProcedimientos"] = null;
        }
    }
    protected void btnTransfers_Click(object sender, EventArgs e)
    {
        if (Session["GestionProcedimientos"] == null)
        {
            Session["GestionProcedimientos"] = string.Empty;
            WebService.SincronizarTransfers();
            panelMensaje.Visible = true;
            panelBotonesSicronizar.Visible = false; Session["GestionProcedimientos"] = null;
        }
    }
    protected void btnTransfersTodos_Click(object sender, EventArgs e)
    {
        if (Session["GestionProcedimientos"] == null)
        {
            Session["GestionProcedimientos"] = string.Empty;
            WebService.SincronizarTransfers_Todos();
            panelMensaje.Visible = true;
            panelBotonesSicronizar.Visible = false; Session["GestionProcedimientos"] = null;
        }
    }
    protected void btnVolver_Click(object sender, EventArgs e)
    {
        if (Session["GestionProcedimientos"] == null)
        {
            Session["GestionProcedimientos"] = string.Empty;
            panelMensaje.Visible = false;
            panelBotonesSicronizar.Visible = true;
            CargarFechasUltimaActualizacion(); Session["GestionProcedimientos"] = null;
        }
    }
    private void CargarFechasUltimaActualizacion()
    {
        lblClientes.Text = string.Empty;
        lblProductos.Text = string.Empty;
        lblProductoPrecios.Text = string.Empty;
        lblProductoStocks.Text = string.Empty;
        lblProductoOfertas.Text = string.Empty;
        lblProductoValesTodos.Text = string.Empty;
        lblTransfers.Text = string.Empty;
        lblTransfersTodos.Text = string.Empty;
        List<string> fechasUltimaActualizacion = new List<string>();
        List<cHistorialProcesos> lista = WebService.RecuperarTodasLasSincronizaciones();

        var query = from item in lista
                    group item by
                            new
                            {
                                Nombre = item.his_NombreProcedimiento
                            }
                        into g
                        select new
                        {
                            Nombre = g.Key.Nombre,
                            Fecha = g.Max(x => x.his_Fecha)
                        };
        var Resultado = query.ToList();
        foreach (var item in Resultado)
        {
            switch (item.Nombre)
            {
                case "Clientes.spSincronizarClientes":
                    lblClientes.Text = "Última actualización: " + item.Fecha.Value.ToString();
                    break;
                case "Clientes.spSincronizarClientes_Todos":
                    lblClientesTodos.Text = "Última actualización: " + item.Fecha.Value.ToString();
                    break;
                case "Productos.spSincronizarProductos":
                    lblProductos.Text = "Última actualización: " + item.Fecha.Value.ToString();
                    break;
                case "Productos.spSincronizarProductos_Todos":
                    lblProductosTodos.Text = "Última actualización: " + item.Fecha.Value.ToString();
                    break;
                case "Productos.spSincronizarOfertas":
                    lblProductoOfertas.Text = "Última actualización: " + item.Fecha.Value.ToString();
                    break;
                case "Productos.spSincronizarOfertas_Todos":
                    lblProductoOfertasTodos.Text = "Última actualización: " + item.Fecha.Value.ToString();
                    break;
                case "Productos.spSincronizarPrecios":
                    lblProductoPrecios.Text = "Última actualización: " + item.Fecha.Value.ToString();
                    break;
                case "Productos.spSincronizarStocks":
                    lblProductoStocks.Text = "Última actualización: " + item.Fecha.Value.ToString();
                    break;
                case "Productos.spSincronizarVales_Todos":
                    lblProductoValesTodos.Text = "Última actualización: " + item.Fecha.Value.ToString();
                    break;
                case "Transfers.spSincronizarTransfers":
                    lblTransfers.Text = "Última actualización: " + item.Fecha.Value.ToString();
                    break;
                case "Transfers.spSincronizarTransfers_Todos":
                    lblTransfersTodos.Text = "Última actualización: " + item.Fecha.Value.ToString();
                    break;
                //case "Transfers.spTransfers_Clientes_Todos":
                //    lblTransfersClientesTodos.Text = "Última actualización: " + item.Fecha.Value.ToString();
                //    break; 
                default:
                    break;
            }
        }
        gv_datos.DataBind();
    }
    protected void cmd_buscar_Click(object sender, EventArgs e)
    {
       Session["GestionProcedimientos_filtro"] = txt_buscar.Text;
       gv_datos.DataBind();
    }
    protected void btnClientesTodos_Click(object sender, EventArgs e)
    {
        if (Session["GestionProcedimientos"] == null)
        {
            Session["GestionProcedimientos"] = string.Empty;
            WebService.SincronizarClientes_Todos();
            panelMensaje.Visible = true;
            panelBotonesSicronizar.Visible = false; Session["GestionProcedimientos"] = null;
        }
    }
    protected void btnProductoTodos_Click(object sender, EventArgs e)
    {
        if (Session["GestionProcedimientos"] == null)
        {
            Session["GestionProcedimientos"] = string.Empty;
            WebService.SincronizarProductos_Todos();
            panelMensaje.Visible = true;
            panelBotonesSicronizar.Visible = false; Session["GestionProcedimientos"] = null;
        }
    }
    protected void btnProductoOfertasTodos_Click(object sender, EventArgs e)
    {
        if (Session["GestionProcedimientos"] == null)
        {
            Session["GestionProcedimientos"] = string.Empty;
            WebService.SincronizarOfertas_Todos();
            panelMensaje.Visible = true;
            panelBotonesSicronizar.Visible = false; Session["GestionProcedimientos"] = null;
        }
    }
    //protected void btnTransfersClientesTodos_Click(object sender, EventArgs e)
    //{
    //    if (Session["GestionProcedimientos"] == null)
    //    {
    //        Session["GestionProcedimientos"] = string.Empty;
    //        WebService.SincronizarTransfers_Clientes_Todos();
    //        panelMensaje.Visible = true;
    //        panelBotonesSicronizar.Visible = false; Session["GestionProcedimientos"] = null;
    //    }
    //}
    
}