using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SitioBase.clases;

public partial class admin_pages_GestionProductoDatosExtras : cBaseAdmin
{
    public const string consPalabraClave = "gestionproductodatosextras";
    protected void Page_Load(object sender, EventArgs e)
    {
        Seguridad(consPalabraClave);
        if (!IsPostBack)
        {
            //Session["GestionProductoDatosExtras_Filtro"] = null;
            //Session["GestionProductoDatosExtras_IdProducto"] = null;
            txt_Cantidad.Text = WebService.RecuperarProductoParametrizadoCantidad().ToString();
        }
     
    }
    protected void cmd_guardar_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txt_Cantidad.Text))
        {
           WebService.InsertarActualizarProductoParametrizadoCantidad( Convert.ToInt32(txt_Cantidad.Text));
        }
    }
}
