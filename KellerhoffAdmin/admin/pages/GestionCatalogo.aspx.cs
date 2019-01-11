using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SitioBase.clases;
using SitioBase;
using System.IO;

public partial class admin_pages_GestionCatalogo : cBaseAdmin
{
    public const string consPalabraClave = "gestioncatalogo";
    protected void Page_Load(object sender, EventArgs e)
    {
        Seguridad(consPalabraClave);
        if (!IsPostBack)
        {
            Session["GestionCatalogo_Tbc_codigo"] = null;
            Session["GestionCatalogo_Filtro"] = null;
        }

        //if (FileUpload1.HasFile)
        //{
        //    String fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
        //    String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg" };
        //    for (int i = 0; i < allowedExtensions.Length; i++)
        //    {
        //        if (fileExtension == allowedExtensions[i])
        //        {
        //            fileOK = true;
        //        }
        //    }
        //}
    }
    protected void cmd_nuevo_Click(object sender, EventArgs e)
    {
        LlamarMetodosAcciones(SitioBase.Constantes.cSQL_INSERT, null, consPalabraClave);
    }
    protected void cmd_buscar_Click(object sender, EventArgs e)
    {
        Session["GestionCatalogo_Filtro"] = txt_buscar.Text;
    }
    protected void cmd_guardar_Click(object sender, EventArgs e)
    {

        if (string.IsNullOrWhiteSpace(txtTitulo.Text))
            return;
        else
        {
            List<cCatalogo> l = WebService.RecuperarTodosCatalogos().Where(x => x.tbc_titulo == txtTitulo.Text.ToUpper()).ToList();
            int codigoCatalogoTemp = 0;
            if (Session["GestionCatalogo_Tbc_codigo"] != null)
                codigoCatalogoTemp = Convert.ToInt32(Session["GestionCatalogo_Tbc_codigo"]);
            if ((l.Count > 0 && codigoCatalogoTemp == 0) || (l.Count > 0 && codigoCatalogoTemp != l[0].tbc_codigo))
                return;
        }

        if (Session["GestionCatalogo_Tbc_codigo"] != null && Session["BaseAdmin_Usuario"] != null)
        {
            // String savePath = @"c:\temp\uploads\";
            int codigoUsuarioEnSession = ((SitioBase.capaDatos.Usuario)Session["BaseAdmin_Usuario"]).id;
            int codigoCatalogo = Convert.ToInt32(Session["GestionCatalogo_Tbc_codigo"]);
            string titulo = txtTitulo.Text.ToUpper();
            if (codigoCatalogo == 0)
            {
                int? resultadoInsertar = WebService.InsertarActualizarCatalogo(0, titulo, string.Empty, 0, DateTime.Now, SitioBase.Constantes.cESTADO_ACTIVO);
                if (resultadoInsertar != null)
                {
                    codigoCatalogo = (int)resultadoInsertar;
                }
            }
            else
            {
                List<cCatalogo> listaCatalogo = WebService.RecuperarTodosCatalogos();
                cCatalogo catalogo = null;
                catalogo = listaCatalogo.Where(x => x.tbc_codigo == codigoCatalogo).First();
                if (catalogo != null)
                {
                    WebService.InsertarActualizarCatalogo(codigoCatalogo, titulo, catalogo.tbc_descripcion, catalogo.tbc_orden, catalogo.tbc_fecha, catalogo.tbc_estado);
                }
            }
            if (codigoCatalogo > 0)
            {
                if (FileUpload1.HasFile)
                {
                    if (FileUpload1.PostedFile.ContentType == Constantes.cMIME_pdf)
                    {
                        string extencion = SitioBase.capaDatos.capaRecurso.obtenerExtencion(FileUpload1.FileName);
                        //string mime = SitioBase.capaDatos.capaRecurso.obtenerMIME(extencion);
                        //string pathOrigen = Constantes.cRaizArchivos + @"\" + Constantes.cArchivo_TempUpload + @"\"; //@"../" + Constantes.cArchivo_TempUpload;
                        string pathDestinoRaiz = Constantes.cRaizArchivos + @"\archivos\";// @"../../" + Constantes.cArchivo_Raiz;
                        string pathDestino = pathDestinoRaiz + Constantes.cTABLA_CATALOGO ;// +extencion + @"/";
                        //string mapPathDestino = HttpContext.Current.Server.MapPath(pathDestino);
                        if (Directory.Exists(pathDestino) == false)
                        {
                            Directory.CreateDirectory(pathDestino);
                        }
                        string filename = SitioBase.capaDatos.capaRecurso.nombreArchivoSinRepetir(pathDestino, FileUpload1.FileName);
                        string nombreArchivo = filename;
                        //string origen = HttpContext.Current.Server.MapPath(pathOrigen) + @"\" + FileUpload1.FileName;
                        string destino = pathDestino + @"\" + nombreArchivo; //HttpContext.Current.Server.MapPath(pathDestino) + nombreArchivo;//+ @"\" 
                        //bool isGraboArchivo = SitioBase.clases.AccesoDisco.CopiarArchivo(origen, destino);
                        //FileUpload1.FileName
                        FileUpload1.SaveAs(destino);
                        //if (isGraboArchivo)
                        //{
                        int codRecurso = 0;
                        List<SitioBase.capaDatos.cArchivo> listaArchivo = WebService.RecuperarTodosArchivos(codigoCatalogo, Constantes.cTABLA_CATALOGO, string.Empty);
                        if (listaArchivo != null)
                        {
                            if (listaArchivo.Count > 0)
                            {
                                codRecurso = listaArchivo[0].arc_codRecurso;
                            }
                        }
                        WebService.InsertarActualizarArchivo(codRecurso, codigoCatalogo, Constantes.cTABLA_CATALOGO, extencion, FileUpload1.PostedFile.ContentType, nombreArchivo, string.Empty, string.Empty, string.Empty, codigoUsuarioEnSession);
                        //SitioBase.clases.AccesoDisco.EliminarArchivo(origen);
                        //}
                    }
                }
            }

            gv_datos.DataBind();
            pnl_grilla.Visible = true;
            pnl_formulario.Visible = false;
        }
    }
    protected void cmd_cancelar_Click(object sender, EventArgs e)
    {
        pnl_grilla.Visible = true;
        pnl_formulario.Visible = false;
    }
    public override void Modificar(int pId)
    {
        Session["GestionCatalogo_Tbc_codigo"] = pId;
        List<cCatalogo> listaCatalogo = WebService.RecuperarTodosCatalogos();
        cCatalogo catalogo = null;
        catalogo = listaCatalogo.Where(x => x.tbc_codigo == pId).First();
        if (catalogo != null)
        {
            txtTitulo.Text = catalogo.tbc_titulo;
            //lbl_iframe.Text = "<iframe name='files' class='form_datos' src='../../pages/filemanager.aspx?grupo=" + SitioBase.Constantes.cTABLA_CATALOGO + "&codrel=" + pId.ToString() + "&ancho=170px&alto=&pag=6' frameborder='0' width='100%' scrolling='no' height='490' style='margin:0px; padding:5px;'>&nbsp</iframe>";
            List<SitioBase.capaDatos.cArchivo> listaArchivo = WebService.RecuperarTodosArchivos(pId, Constantes.cTABLA_CATALOGO, string.Empty);
            if (listaArchivo != null)
            {
                if (listaArchivo.Count > 0)
                {
                    // lblArchivo.Text = "<div><a id=\"a_" + listaArchivo[0].arc_codRecurso + "\" href=\"../../" + Constantes.cArchivo_Raiz + "/" + Constantes.cTABLA_CATALOGO + "/" + listaArchivo[0].arc_nombre + "\" >" + listaArchivo[0].arc_nombre + "</a>&nbsp; </div>";//<span class=\"spanPfEliminar\" onclick=\"onclickEliminarPDF(" + listaArchivo[0].arc_codRecurso + ")\">[X]</span>
                    lblArchivo.Text = "<div><a id=\"a_" + listaArchivo[0].arc_codRecurso + "\" href=\"../../servicios/" + "descargarArchivo.aspx?t="  + Constantes.cTABLA_CATALOGO + "&n=" + listaArchivo[0].arc_nombre + "\" >" + listaArchivo[0].arc_nombre + "</a>&nbsp; </div>";

                }
            }
            PanelArchivo.Visible = false;
            PanelArchivoTexto.Visible = true;
            pnl_grilla.Visible = false;
            pnl_formulario.Visible = true;
            //tab2.Visible = true;
        }
    }
    public string ArmarLink(string archivo, string grupo, string tipo)
    {
        string ruta = System.Configuration.ConfigurationManager.AppSettings["raiz"] + Constantes.cArchivo_Raiz + @"/" + grupo + "/" + tipo + "/" + Server.UrlEncode(archivo);
        string url = string.Empty;
        url = "<a href='" + ruta + "' target='_blank' />" + archivo + "</a>";
        return url;
    }
    public override void Eliminar(int pId)
    {
        WebService.ElininarCatalogo(pId);
    }
    public override void Insertar()
    {
        Session["GestionCatalogo_Tbc_codigo"] = 0;
        pnl_grilla.Visible = false;
        pnl_formulario.Visible = true;
        lblArchivo.Text = string.Empty;
        txtTitulo.Text = string.Empty;
        PanelArchivo.Visible = true;
        // btnArchivo.Visible = true;
        PanelArchivoTexto.Visible = false;
        //lbl_iframe.Text = string.Empty;
        //tab2.Visible = false;
    }
    public override void CambiarEstado(int pId)
    {
        Session["GestionCatalogo_Tbc_codigo"] = pId;
        List<cCatalogo> listaCatalogo = WebService.RecuperarTodosCatalogos();
        cCatalogo catalogo = null;
        catalogo = listaCatalogo.Where(x => x.tbc_codigo == pId).First();
        if (catalogo != null)
        {
            int estado = 0;
            if (catalogo.tbc_estado == SitioBase.Constantes.cESTADO_ACTIVO)
            {
                estado = SitioBase.Constantes.cESTADO_INACTIVO;
            }
            else
            {
                estado = SitioBase.Constantes.cESTADO_ACTIVO;
            }
            WebService.InsertarActualizarCatalogo(catalogo.tbc_codigo, catalogo.tbc_titulo, catalogo.tbc_descripcion, catalogo.tbc_orden, catalogo.tbc_fecha, estado);
        }
    }
    protected void gv_datos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modificar")
        {
            LlamarMetodosAcciones(SitioBase.Constantes.cSQL_UPDATE, Convert.ToInt32(e.CommandArgument), consPalabraClave);
        }
        else if (e.CommandName == "Estado")
        {
            LlamarMetodosAcciones(SitioBase.Constantes.cSQL_ESTADO, Convert.ToInt32(e.CommandArgument), consPalabraClave);
        }
        else if (e.CommandName == "Eliminar")
        {
            LlamarMetodosAcciones(SitioBase.Constantes.cSQL_DELETE, Convert.ToInt32(e.CommandArgument), consPalabraClave);
        }
        else if (e.CommandName == "Publicar")
        {
            LlamarMetodosAcciones(SitioBase.Constantes.cSQL_PUBLICAR, Convert.ToInt32(e.CommandArgument), consPalabraClave);
            //Convert.ToInt32(e.CommandArgument)
        }
        gv_datos.DataBind();
    }
    public override void Publicar(int pId)
    {
        if (Session["BaseAdmin_Usuario"] != null)
        {
            bool tbc_publicarHome = true;
            //int codigoUsuarioEnSession = ((SitioBase.capaDatos.Usuario)Session["BaseAdmin_Usuario"]).id;
            cCatalogo o = WebService.RecuperarTodosCatalogos().Where(x => x.tbc_codigo == pId).FirstOrDefault();
            cCatalogo oPublicarHome = WebService.RecuperarTodosCatalogos().Where(x => (x.tbc_publicarHome != null && x.tbc_publicarHome.Value)).FirstOrDefault();
            if (o != null && o.tbc_publicarHome != null)
                tbc_publicarHome = !o.tbc_publicarHome.Value;
            if (oPublicarHome != null && oPublicarHome.tbc_codigo != pId)
                WebService.PublicarHomeCatalogo(oPublicarHome.tbc_codigo, false);
            WebService.PublicarHomeCatalogo(pId, tbc_publicarHome);
            gv_datos.DataBind();
        }
    }
    protected void btnArchivo_Click(object sender, EventArgs e)
    {
        PanelArchivo.Visible = true;
        // btnArchivo.Visible = true;
        PanelArchivoTexto.Visible = false;
    }
}