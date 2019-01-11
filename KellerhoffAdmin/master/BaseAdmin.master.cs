using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SitioBase.capaDatos;


public partial class master_BaseAdmin : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["BaseAdmin_Usuario"] == null)
        {
            Response.Redirect("~/admin/Default.aspx");
        }
        else
        {
            lblNombreUsuario.Text = ((SitioBase.capaDatos.Usuario)Session["BaseAdmin_Usuario"]).NombreYApellido;
        }
    }
   
   
}
