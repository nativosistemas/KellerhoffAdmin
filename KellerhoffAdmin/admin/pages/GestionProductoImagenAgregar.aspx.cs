using SitioBase;
using SitioBase.clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_pages_GestionProductoImagenAgregar : cBaseAdmin
{
    public const string consPalabraClave = "gestionproductoimagen";
    protected void Page_Load(object sender, EventArgs e)
    {
        Seguridad(consPalabraClave);
        if (!IsPostBack)
        {
            if (Request.QueryString.AllKeys.Contains("Numero"))//Request.QueryString.AllKeys.Contains("Nombre") && 
            {
                // HttpContext.Current.Session["GestionProductoImagenAgregar_Nombre"] = Request.QueryString.Get("Nombre");
                HttpContext.Current.Session["GestionProductoImagenAgregar_Numero"] = Request.QueryString.Get("Numero");
                cProductos objProducto = WebService.RecuperadorProductoPorCodigo(HttpContext.Current.Session["GestionProductoImagenAgregar_Numero"].ToString());
                if (objProducto != null)
                    HttpContext.Current.Session["GestionProductoImagenAgregar_Nombre"] = objProducto.pro_nombre;

            }
            else
            {
                HttpContext.Current.Session["GestionProductoImagenAgregar_Nombre"] = null;
                HttpContext.Current.Session["GestionProductoImagenAgregar_Numero"] = null;
            }
        }
        else// if (IsPostBack)
        {
            Boolean fileOK = false;
            String path = SitioBase.Constantes.cRaizArchivos + @"/archivos/productos/";
            if (FileUpload1.HasFile)
            {
                String fileExtension =
                    System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
                String[] allowedExtensions =
                    {".gif", ".png", ".jpeg", ".jpg"};
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOK = true;
                    }
                }
            }

            if (fileOK)
            {
                try
                {
                    if (HttpContext.Current.Session["GestionProductoImagenAgregar_Numero"] != null)
                    {
                        string nombre = FileUpload1.FileName;
                        //string RutaCompleta = AppDomain.CurrentDomain.BaseDirectory + @"\archivos\" + ruta + @"\";
                        // string RutaCompletaNombreArchivo = path + nombre;

                        string[] listaParteNombre = nombre.Split('.');
                        string CacheNombreArchivo = string.Empty;
                        string CacheExtencionArchivo = string.Empty;
                        for (int i = 0; i < listaParteNombre.Length - 1; i++)
                        {
                            CacheNombreArchivo += listaParteNombre[i];
                        }
                        CacheExtencionArchivo = listaParteNombre[listaParteNombre.Length - 1];
                        int cont = -1;
                        string parteNueva = string.Empty;
                        string nombreFinal = CacheNombreArchivo + parteNueva + "." + CacheExtencionArchivo;
                        while (System.IO.File.Exists(path + nombreFinal))
                        {
                            cont++;
                            parteNueva = cont.ToString();
                            nombreFinal = CacheNombreArchivo + parteNueva + "." + CacheExtencionArchivo;
                        }

                        FileUpload1.PostedFile.SaveAs(path + nombreFinal);

                        cThumbnail.obtenerImagen("productos", nombreFinal, Constantes.cWidth_Oferta.ToString(), Constantes.cHeight_Oferta.ToString(), "#FFFFFF", false);


                        WebService.ActualizarInsertarProductosImagen(HttpContext.Current.Session["GestionProductoImagenAgregar_Numero"].ToString(), nombreFinal);
                        //Label1.Text = "File uploaded!";

                        HttpContext.Current.Session["GestionProductoImagenAgregar_Nombre"] = null;
                        HttpContext.Current.Session["GestionProductoImagenAgregar_Numero"] = null;
                        string parametro = string.Empty;

                        if (HttpContext.Current.Session["GestionProductoImagen_Text"] != null && !string.IsNullOrWhiteSpace(HttpContext.Current.Session["GestionProductoImagen_Text"].ToString()))
                        {
                            parametro = "?text=" + HttpContext.Current.Session["GestionProductoImagen_Text"].ToString();
                        }
                        Response.Redirect("GestionProductoImagen.aspx" + parametro);
                    }
                }
                catch (Exception ex)
                {
                    //Label1.Text = "File could not be uploaded.";
                }
            }
            else
            {
                //Label1.Text = "Cannot accept files of this type.";
            }

        }
    }
    public void AgregarHtmlOculto()
    {
        if (HttpContext.Current.Session["GestionProductoImagenAgregar_Numero"] != null)
        {
            string resultado = string.Empty;
            resultado += "<input type=\"hidden\" id=\"hiddenNumero\" value=\"" + HttpContext.Current.Session["GestionProductoImagenAgregar_Numero"].ToString() + "\" />";
            string strText = string.Empty;
            if (HttpContext.Current.Session["GestionProductoImagen_Text"] != null && !string.IsNullOrWhiteSpace(HttpContext.Current.Session["GestionProductoImagen_Text"].ToString()))
                strText = HttpContext.Current.Session["GestionProductoImagen_Text"].ToString();
            resultado += "<input type=\"hidden\" id=\"hiddenText\" value=\"" + strText + "\" />";
            string strNombre = string.Empty;
            if (HttpContext.Current.Session["GestionProductoImagenAgregar_Nombre"] != null)
                strNombre = HttpContext.Current.Session["GestionProductoImagenAgregar_Nombre"].ToString();
            resultado += "<input type=\"hidden\" id=\"hiddenNombre\" value=\"" + strNombre + "\" />";
            Response.Write(resultado);
        }
    }



}