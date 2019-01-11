using SitioBase.clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_pages_GestionOfertaEditarAgregar : cBaseAdmin
{
    public const string consPalabraClave = "gestionhome";
    protected void Page_Load(object sender, EventArgs e)
    {
        Seguridad(consPalabraClave);
        if (!IsPostBack)
        {
            if (Request.QueryString.AllKeys.Contains("id"))
            {
                HttpContext.Current.Session["GestionOfertaEditarAgregar_codRecurso"] = 0;
                HttpContext.Current.Session["GestionOfertaEditarAgregar_id"] = Request.QueryString.Get("id");
                cOferta objOferta = WebService.RecuperarOfertaPorId(Convert.ToInt32(HttpContext.Current.Session["GestionOfertaEditarAgregar_id"]));
                HttpContext.Current.Session["GestionOfertaEditarAgregar_obj"] = objOferta;

            }
            else
            {
                HttpContext.Current.Session["GestionOfertaEditarAgregar_Nombre"] = null;
                HttpContext.Current.Session["GestionOfertaEditarAgregar_id"] = null;
            }
        }
        else// if (IsPostBack)
        {
            Label1.Text = "";
            Boolean fileOK = false;
            String path = Server.MapPath("~/archivos/ofertas/");
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
                    if (HttpContext.Current.Session["GestionOfertaEditarAgregar_id"] != null && HttpContext.Current.Session["GestionOfertaEditarAgregar_codRecurso"] != null)
                    {
                        string nombre = FileUpload1.FileName;
                        nombre = nombre.Replace(" ", "");
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

                        cThumbnail.obtenerImagen("ofertas", nombreFinal, "283", "185", "", false);

                        WebService.InsertarActualizarArchivo(Convert.ToInt32(HttpContext.Current.Session["GestionOfertaEditarAgregar_codRecurso"]), Convert.ToInt32(HttpContext.Current.Session["GestionOfertaEditarAgregar_id"]), "ofertas", CacheExtencionArchivo, FileUpload1.PostedFile.ContentType, nombreFinal, string.Empty, string.Empty, string.Empty, Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["codigoUsuarioSinRegistrar"]));
                        //WebService.ActualizarInsertarProductosImagen(HttpContext.Current.Session["GestionOfertaEditarAgregar_id"].ToString(), nombreFinal);
                        //Label1.Text = "File uploaded!";

                        HttpContext.Current.Session["GestionOfertaEditarAgregar_Nombre"] = null;
                        HttpContext.Current.Session["GestionOfertaEditarAgregar_id"] = null;
                        string parametro = string.Empty;

                        //if (HttpContext.Current.Session["GestionProductoImagen_Text"] != null && !string.IsNullOrWhiteSpace(HttpContext.Current.Session["GestionProductoImagen_Text"].ToString()))
                        //{
                        //    parametro = "?text=" + HttpContext.Current.Session["GestionProductoImagen_Text"].ToString();
                        //}
                        Response.Redirect("GestionOferta.aspx");
                    }
                }
                catch (Exception ex)
                {
                    //Label1.Text = "File could not be uploaded.";
                }
            }
            else
            {
                Label1.Text = "Error: No se subió ningún archivo o archivo incorrecto.";
            }

        }
    }
    public void AgregarHtmlOculto()
    {

        if (HttpContext.Current.Session["GestionOfertaEditarAgregar_obj"] != null)
        {
            cOferta objOferta = (cOferta)HttpContext.Current.Session["GestionOfertaEditarAgregar_obj"];
            string resultado = string.Empty;
            resultado += "<input type=\"hidden\" id=\"hiddenTitulo\" value=\"" + objOferta.ofe_titulo + "\" />";
            resultado += "<input type=\"hidden\" id=\"hiddenDescr\" value=\"" + objOferta.ofe_descr + "\" />";
            List<SitioBase.capaDatos.cArchivo> listaArchivo = WebService.RecuperarTodosArchivos(objOferta.ofe_idOferta, "ofertas", string.Empty);
            if (listaArchivo != null)
            {
                if (listaArchivo.Count > 0)
                {
                    resultado += "<input type=\"hidden\" id=\"hiddenNameImage\" value=\"" + listaArchivo[0].arc_nombre + "\" />";
                    //resultado += "<input type=\"hidden\" id=\"hiddenIdImage\" value=\"" + listaArchivo[0].arc_codRecurso + "\" />";
                    HttpContext.Current.Session["GestionOfertaEditarAgregar_codRecurso"] = listaArchivo[0].arc_codRecurso;
                }
            }
            //string strText = string.Empty;
            //if (HttpContext.Current.Session["GestionProductoImagen_Text"] != null && !string.IsNullOrWhiteSpace(HttpContext.Current.Session["GestionProductoImagen_Text"].ToString()))
            //    strText = HttpContext.Current.Session["GestionProductoImagen_Text"].ToString();
            //resultado += "<input type=\"hidden\" id=\"hiddenText\" value=\"" + strText + "\" />";
            //string strNombre = string.Empty;
            //if (HttpContext.Current.Session["GestionOfertaEditarAgregar_Nombre"] != null)
            //    strNombre = HttpContext.Current.Session["GestionOfertaEditarAgregar_Nombre"].ToString();
            //resultado += "<input type=\"hidden\" id=\"hiddenNombre\" value=\"" + strNombre + "\" />";
            Response.Write(resultado);
        }
    }
}