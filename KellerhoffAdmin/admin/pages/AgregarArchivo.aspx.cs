using SitioBase;
using SitioBase.capaDatos;
using SitioBase.clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_pages_AgregarArchivo : cBaseAdmin
{
    public const string consPalabraClave = "gestionhome";
    protected void Page_Load(object sender, EventArgs e)
    {
        Seguridad(consPalabraClave);
        if (!IsPostBack)
        {
            if (Request.QueryString.AllKeys.Contains("id") && Request.QueryString.AllKeys.Contains("t") && Request.QueryString.AllKeys.Contains("an") && Request.QueryString.AllKeys.Contains("al"))
            {
                htmlArchivo obj = new htmlArchivo();
                obj.id = Convert.ToInt32(Request.QueryString.Get("id"));
                obj.tipo = Request.QueryString.Get("t");
                obj.ancho = Convert.ToInt32(Request.QueryString.Get("an"));
                obj.alto = Convert.ToInt32(Request.QueryString.Get("al"));
                switch (obj.tipo)
                {
                    case "slider":
                        tbl_HomeSlide o = WebService.RecuperarTodasHomeSlide().Where(x => x.hsl_idHomeSlide == obj.id).FirstOrDefault();
                        if (o != null)
                        {
                            switch (obj.ancho)
                            {
                                case 1920:
                                    obj.codRecurso = o.hsl_idRecursoImgPC == null ? 0 : o.hsl_idRecursoImgPC.Value;
                                    break;
                                case 700:
                                    obj.codRecurso = o.hsl_idRecursoImgMobil == null ? 0 : o.hsl_idRecursoImgMobil.Value;
                                    break;
                                default:
                                    break;
                            }
                            if (obj.codRecurso != 0)
                            {
                                cArchivo oAr = WebService.RecuperarArchivoPorId(obj.codRecurso);
                                if (oAr != null)
                                    obj.arc_nombre = oAr.arc_nombre;
                            }
                        }
                        obj.titulo = "";
                        obj.descr = "";
                        break;
                    case "ofertas":
                        List<SitioBase.capaDatos.cArchivo> listaArchivo = WebService.RecuperarTodosArchivos(obj.id, obj.tipo, string.Empty);
                        if (listaArchivo != null)
                        {
                            if (listaArchivo.Count > 0)
                            {
                                obj.arc_nombre = listaArchivo[0].arc_nombre;
                                obj.codRecurso = listaArchivo[0].arc_codRecurso;
                            }
                        }

                        break;
                    default:
                        List<SitioBase.capaDatos.cArchivo> listaArchivo_default = WebService.RecuperarTodosArchivos(obj.id, obj.tipo, string.Empty);
                        if (listaArchivo_default != null)
                        {
                            if (listaArchivo_default.Count > 0)
                            {
                                obj.arc_nombre = listaArchivo_default[0].arc_nombre;
                                obj.codRecurso = listaArchivo_default[0].arc_codRecurso;
                            }
                        }

                        break;
                }

                HttpContext.Current.Session["AgregarArchivo_obj"] = obj;
            }
        }
        else if (HttpContext.Current.Session["AgregarArchivo_obj"] != null)
        {
            htmlArchivo obj = (htmlArchivo)HttpContext.Current.Session["AgregarArchivo_obj"];
            Label1.Text = "";
            Boolean fileOK = false;
            String path = SitioBase.Constantes.cRaizArchivos + @"\archivos\" + obj.tipo + @"\";
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
                    //if (HttpContext.Current.Session["GestionOfertaEditarAgregar_id"] != null && HttpContext.Current.Session["GestionOfertaEditarAgregar_codRecurso"] != null)
                    //{
                    string nombre = FileUpload1.FileName;
                    nombre = nombre.Replace(" ", "");
                    nombre = SitioBase.clases.Texto.limpiarNombreArchivo(nombre);
                    nombre = "a" + nombre;

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

                    cThumbnail.obtenerImagen(obj.tipo, nombreFinal, obj.ancho.ToString(), obj.alto.ToString(), "", false);

                    obj.codRecurso = WebService.InsertarActualizarArchivo(obj.codRecurso, obj.id, obj.tipo, CacheExtencionArchivo, FileUpload1.PostedFile.ContentType, nombreFinal, string.Empty, string.Empty, string.Empty, Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["codigoUsuarioSinRegistrar"]));
                    HttpContext.Current.Session["AgregarArchivo_obj"] = null;

                    string parametro = string.Empty;


                    switch (obj.tipo)
                    {
                        case "ofertas":
                            Response.Redirect("GestionOferta.aspx");
                            break;
                        case "slider":
                            WebService.ActualizarImagenHomeSlide(obj.id, obj.codRecurso, obj.ancho == 700 ? 2 : 1);
                            Response.Redirect("GestionHomeSlide.aspx");
                            break;
                        default:
                            break;
                    }
                    //}
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
        if (HttpContext.Current.Session["AgregarArchivo_obj"] != null)
        {
            string resultado = string.Empty;
            htmlArchivo obj = (htmlArchivo)HttpContext.Current.Session["AgregarArchivo_obj"];
            resultado += "<input type=\"hidden\" id=\"hiddenFile\" value=\"" + Server.HtmlEncode(SitioBase.clases.Serializador.SerializarAJson(obj)) + "\" />";
            Response.Write(resultado);
        }
    }
}