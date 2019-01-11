using SitioBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class servicios_descargarArchivo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string tipo = Request.QueryString["t"];
        string name = Request.QueryString["n"];
        if (!string.IsNullOrEmpty(tipo) && !string.IsNullOrEmpty(name))
        {
            string nombreArchivo = name;
            String path = Constantes.cRaizArchivos + @"/archivos/" + tipo + @"/" + nombreArchivo;

            System.IO.FileInfo toDownload = new System.IO.FileInfo(path);
            if (toDownload.Exists)
            {
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + toDownload.Name);
                Response.AddHeader("Content-Length", toDownload.Length.ToString());
                //string contentType_aux = MimeMapping.GetMimeMapping(path);
                //Response.ContentType = contentType_aux;// Constantes.cMIME_pdf;

                try
                {
                    Response.WriteFile(path);
                }
                catch (Exception ex)
                {
                    //Thread.Sleep(1000);
                    //Response.WriteFile(Constantes.cArchivo_ImpresionesComprobante + nombrePDF);
                }

                Response.End();
            }
        }
    }
}