<%@ Page Title="" Language="C#" MasterPageFile="~/master/BaseAdminJs3.master" AutoEventWireup="true" CodeFile="GestionHomeSlideEditar.aspx.cs" Inherits="admin_pages_GestionHomeSlideEditar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="lib/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="lib/css/prettify.css" />
    <link rel="stylesheet" type="text/css" href="lib/css/bootstrap-wysihtml5.css" />

    <script src="../../includes/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="lib/js/wysihtml5-0.3.0.js" type="text/javascript"></script>
    <script src="lib/js/prettify.js" type="text/javascript"></script>
    <script src="lib/js/bootstrap-wysihtml5.js" type="text/javascript"></script>
    <script src="../../includes/js/GestionHomeSlide.js" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            // RecuperarTodasOfertaDetalles();
            //CargarDatosOfertaEditar();
            var titulo = $('#hidden_titulo').val();
            if (typeof titulo == 'undefined') {
                titulo = null;
            } else {
                $('#txt_titulo').val(titulo);
            }
            var descr = $('#hidden_descr').val();
            if (typeof descr == 'undefined') {
                descr = null;
            } else {
                $('#txt_descr').val(descr);
            }
            var descrHtmlReducido = $('#hidden_descrHtmlReducido').val();//hsl_descrHtmlReducido
            if (typeof descrHtmlReducido == 'undefined') {
                descrHtmlReducido = null;
            } else {
                $('#txt_descrHtmlReducido').val(descrHtmlReducido);
            }
            var varHtml = $('#hidden_descrHtml').val();
            if (typeof varHtml == 'undefined') {
                varHtml = '';
            }

            var tipo = $('#hidden_tipo').val();
            if (typeof tipo == 'undefined') {
                tipo = null;
            } else {
                $('#cmdTipoSlide').val(tipo);
                onclickSelectTipoSlide();
            }
            var idRecurso = $('#hidden_idRecurso').val();
            if (typeof idRecurso == 'undefined') {
                idRecurso = null;
            } else if (idRecurso != '') {
                $('#cmdCatalogo').val(idRecurso);
            }
            var idOferta = $('#hidden_idOferta').val();
            if (typeof idOferta == 'undefined') {
                idOferta = null;
            } else if (idOferta != '') {
                $('#cmdOferta').val(idOferta);
            }
            //
            //

            $('#textareaHtml').wysihtml5({
                "html": true, //Button which allows you to edit the generated HTML. Default false
                "color": true
            }).html(varHtml);
            //var idOferta = $('#hiddenOfe_idOferta').val();
            //if (typeof idOferta == 'undefined') {
            //    idOferta = null;
            //} else {
            //    if (parseInt( idOferta) == 0)
            //    {
            //        $('#contenedorProductos').css('display','none');
            //    }
            //}

        });
    </script>
    <style type="text/css">
        .cssIdOferta {
            display: none;
        }
        .cssIdCatalogo{
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <h2 class="sub-header">Editar/Agregar
    </h2>
    <button type="button" class="btn btn-info" onclick="GuardarSlide();  return false;">Guardar</button>
    &nbsp;&nbsp;
    <button type="button" class="btn btn-info" onclick="return VolverSlide();">Volver</button>
    <div class="form-group">
        <label for="txt_titulo">Título:</label>
        <input type="text" class="form-control" id="txt_titulo" style="width: 810px;" />
    </div>
    <div class="form-group">
        <label for="txt_descr">Descrición:</label>
        <%--        <input type="text" class="form-control" id="txt_descr" />--%>
        <textarea id="txt_descr" class="textarea" placeholder="" style="width: 810px; height: 200px"></textarea>
    </div>

        <div class="form-group">
        <label for="txt_descr">Clases Html (evitar el uso):</label>
        <textarea id="txt_descrHtmlReducido" class="textarea" placeholder="" style="width: 810px; height: 80px"></textarea>
    </div>
    <div class="form-group">
        <label for="txtHtml">Html:</label>
        <textarea id="textareaHtml" class="textarea" placeholder="" style="width: 810px; height: 200px"></textarea>
    </div>
    <div class="form-group">
        <label for="txt_descuento">Tipo:</label>
        <%--<input type="text" class="form-control" id="txt_tipo" style="text-transform: uppercase;" />--%>

        <select id="cmdTipoSlide" class="selectpicker" onclick="onclickSelectTipoSlide(); return false;">
            <option value="1">Normal</option>
            <option value="2">Oferta</option>
            <option value="3">Revista</option>
        </select>

    </div>


    <div class="form-group  cssIdOferta">
        <label for="cmdOferta">Oferta:</label>
        <select id="cmdOferta" class="selectpicker">
            <%Response.Write(getHtmlOptionOferta()); %>
        </select>
    </div>
        <div class="form-group  cssIdCatalogo">
        <label for="cmdCatalogo">Catálogo:</label>
        <select id="cmdCatalogo" class="selectpicker">
            <%Response.Write(getHtmlOptionCatalogo()); %>
        </select>
    </div>
    <%  AgregarHtmlOculto(); %>
    <script type="text/javascript">
        //   $('.textarea').wysihtml5().html(varHtml);
</script>

</asp:Content>
