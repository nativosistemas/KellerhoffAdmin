<%@ WebHandler Language="C#" Class="FileUpload" %>

using System;
using System.Web;
using System.IO;

public class FileUpload : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");
        const string path = "../temp/upload";
        String filename = HttpContext.Current.Request.Headers["X-File-Name"];

        if (string.IsNullOrEmpty(filename) && HttpContext.Current.Request.Files.Count <= 0)
        {
            context.Response.Write("{success:false}");
        }
        else
        {
            string mapPath = HttpContext.Current.Server.MapPath(path);
            if (System.IO.Directory.Exists(mapPath) == false)
            {
                System.IO.Directory.CreateDirectory(mapPath);
            }
            if (filename == null)
            {
                //This work for IE
                try
                {
                    SitioBase.capaDatos.cNombreArchivos objNombreArchivo = new SitioBase.capaDatos.cNombreArchivos();
                    HttpPostedFile uploadedfile = context.Request.Files[0];
                    string[] listaNombre = uploadedfile.FileName.Split('\\');
                    string NombreArchivo = string.Empty;
                    string ExtencionArchivo = string.Empty;
                    NombreArchivo += listaNombre[listaNombre.Length - 1];
                    objNombreArchivo.NombreOriginal = NombreArchivo;
                    filename = SitioBase.capaDatos.capaRecurso.nombreArchivoSinRepetir(mapPath, NombreArchivo);
                    objNombreArchivo.NombreGrabado = filename;
                    uploadedfile.SaveAs(mapPath + "\\" + filename);
                    context.Response.Write("{success:true, name:\"" + filename + "\", path:\"" + path + "/" + filename + "\"}");

                    if (HttpContext.Current.Application["ListaArchivos"] == null)
                    {
                        HttpContext.Current.Application["ListaArchivos"] = new System.Collections.Generic.List<SitioBase.capaDatos.cNombreArchivos>();
                    }
                    ((System.Collections.Generic.List<SitioBase.capaDatos.cNombreArchivos>)HttpContext.Current.Application["ListaArchivos"]).Add(objNombreArchivo);
                }
                catch (Exception generatedExceptionName)
                {
                    context.Response.Write("{success:false}");
                }
            }
            else
            {
                //This work for Firefox and Chrome.
                SitioBase.capaDatos.cNombreArchivos objNombreArchivo = new SitioBase.capaDatos.cNombreArchivos();
                objNombreArchivo.NombreOriginal = filename;
                filename = SitioBase.capaDatos.capaRecurso.nombreArchivoSinRepetir(mapPath, filename);
                objNombreArchivo.NombreGrabado = filename;
                FileStream fileStream = new FileStream(mapPath + "\\" + filename, FileMode.OpenOrCreate);
                try
                {
                    Stream inputStream = HttpContext.Current.Request.InputStream;
                    inputStream.CopyTo(fileStream);
                    context.Response.Write("{success:true, name:\"" + filename + "\", path:\"" + path + "/" + filename + "\"}");

                    if (HttpContext.Current.Application["ListaArchivos"] == null)
                    {
                        HttpContext.Current.Application["ListaArchivos"] = new System.Collections.Generic.List<SitioBase.capaDatos.cNombreArchivos>();
                    }
                    ((System.Collections.Generic.List<SitioBase.capaDatos.cNombreArchivos>)HttpContext.Current.Application["ListaArchivos"]).Add(objNombreArchivo);
                }
                catch (Exception generatedExceptionName)
                {
                    context.Response.Write("{success:false}");
                }
                finally
                {
                    fileStream.Close();

                }
            }
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}