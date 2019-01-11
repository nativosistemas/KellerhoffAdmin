<%@ Page Title="" Language="C#" MasterPageFile="~/master/BaseAdminJs3.master" AutoEventWireup="true" CodeFile="GestionOfertaEditarAgregar_NO.aspx.cs" Inherits="admin_pages_GestionOfertaEditarAgregar" %>

<asp:content id="Content1" contentplaceholderid="head" runat="Server">
    <link href="../../includes/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../../includes/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../../includes/js/GestionOferta.js" type="text/javascript"></script>
        <script type="text/javascript">
           // var arc_codRecurso_oferta = 0;
            jQuery(document).ready(function () {
                $('#txt_titulo').val($('#hiddenTitulo').val());
                $('#txt_descr').val($('#hiddenDescr').val());

                var nameImagen = $('#hiddenNameImage').val();
                if (typeof nameImagen == 'undefined') {
                    nameImagen = null;
                } else {
                    var strHtml = '<img   src="' + '../../../servicios/thumbnail.aspx?r=' + 'ofertas' + '&n=' + nameImagen + '&an=' + String(300) + '&al=' + String(300) + '&c=FFFFFF" />';
                    $('#divImg').html(strHtml);
                    $('#divContenedorImg').css('display', 'block');
                  //  arc_codRecurso_oferta = $('#hiddenIdImage').val();


                }
             

            });
    </script>
</asp:content>
<asp:content id="Content2" contentplaceholderid="MainContent" runat="Server">
     <h2 class="sub-header">Agregar imagen a la oferta
    </h2>
    <div class="form-group">
        <label for="txt_titulo">Título:</label>
        <input type="text" class="form-control" id="txt_titulo" disabled="disabled" />
    </div>
    <div class="form-group">
        <label for="txt_descr">Descripción:</label>
        <input type="text" class="form-control" id="txt_descr" disabled="disabled" />
    </div>
    <div id="divContenedorImg" class="form-group" style="display:none">
        <label for="txt_descr">Imagen:</label>
         <div id="divImg"></div>
      <%--  <img   src="../../../servicios/thumbnail.aspx?r=ofertas&n=' + args[i].pri_nombreArchivo + '&an=250&al=250" />--%>
    </div>
    <asp:FileUpload ID="FileUpload1" runat="server"  />
        <br /> 
    <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
    <br />
    <button type="submit" class="btn btn-primary">Subir</button>
    <button type="button" class="btn btn-primary" onclick="onclickVolverOfertaImagen(); return false;">Volver</button>
    <% AgregarHtmlOculto(); %>
</asp:content>

