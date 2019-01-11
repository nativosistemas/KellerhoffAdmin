using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class servicios_thumbnail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ruta = Request.QueryString["r"];
        string nombre = Request.QueryString["n"];
        string strAncho = Request.QueryString["an"];
        string strAlto = Request.QueryString["al"];
        string strColor = string.Empty;
        if (Request.QueryString["c"] != null)
        {
            if (Request.QueryString["c"].Trim() != string.Empty)
            {
                strColor = "#" + Request.QueryString["c"];
            }
        }
        bool boolAlto = false;
        if (Request.QueryString["re"] != null)
        {
            boolAlto = true;
        }

        System.Drawing.Image oImg = cThumbnail.obtenerImagen(ruta, nombre, strAncho, strAlto, strColor, boolAlto);
        if (oImg != null)
        {
            oImg.Save(Response.OutputStream, ImageFormat.Jpeg);
            oImg.Dispose();
        }
    
    }
 
}