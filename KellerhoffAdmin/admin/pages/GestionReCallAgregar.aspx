<%@ Page Title="" Language="C#" MasterPageFile="~/master/BaseAdminJs3.master" AutoEventWireup="true" CodeFile="GestionReCallAgregar.aspx.cs" Inherits="admin_pages_GestionReCallAgregar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" type="text/css" href="lib/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="lib/css/prettify.css" />
    <link rel="stylesheet" type="text/css" href="lib/css/bootstrap-wysihtml5.css" />

    <script src="../../includes/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="lib/js/wysihtml5-0.3.0.js" type="text/javascript"></script>
    <script src="lib/js/prettify.js" type="text/javascript"></script>
    <script src="lib/js/bootstrap-wysihtml5.js" type="text/javascript"></script>
        <link  rel="stylesheet" type="text/css"  href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script> 
    <script src="../../includes/js/GestionReCall.js" type="text/javascript"></script>
   <script type="text/javascript">
       jQuery(document).ready(function () {
 
           var titulo = $('#hidden_titulo').val();
           if (typeof titulo == 'undefined') {
               titulo = null;
           } else {
               $('#txt_titulo').val(titulo);
           }

           
           var descrReducido = $('#hidden_descrReducido').val();
           if (typeof descrReducido == 'undefined') {
               descrReducido = null;
           } else {
               $('#txt_descripcionReducido').val(descrReducido);
           }

           var varHtml = $('#hidden_descr').val();
           if (typeof varHtml == 'undefined') {
               varHtml = '';
           }


           $('#textareaHtml').wysihtml5({
               "html": true, //Button which allows you to edit the generated HTML. Default false
               "color": true
           }).html(varHtml);

           $('#txt_fecha').datepicker();
       });
    </script>
    <style type="text/css">
        /*.cssIdOferta {
            display: none;
        }
        .cssIdCatalogo{
            display: none;
        }*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2 class="sub-header">Editar/Agregar
    </h2>
    <button type="button" class="btn btn-info" onclick="GuardarReCall();  return false;">Guardar</button>
    &nbsp;&nbsp;
    <button type="button" class="btn btn-info" onclick="return VolverReCall();">Volver</button>
    <div class="form-group">
        <label for="txt_titulo">Título:</label>
        <input type="text" class="form-control" id="txt_titulo"  />
    </div>
        <div class="form-group">
        <label for="txt_fecha">Fecha de noticia:</label>
       <%-- <input type="date" class="form-control" id="txt_fecha" name="txt_fecha" />--%>
        <input type="text" class="form-control" id="txt_fecha"  />
    </div>
        <div class="form-group">
        <label for="txt_descripcionReducido">Descrición Reducido:</label>
        <textarea id="txt_descripcionReducido" class="textarea" placeholder="" style="width: 100%; height: 150px"></textarea>
    </div>
    <div class="form-group">
       <label for="txtHtml">Descrición:</label>
        <textarea id="textareaHtml" class="textarea" placeholder="" style="width: 100%; height: 200px"></textarea>
    </div>

     <%  AgregarHtmlOculto(); %>

     <script type="text/javascript">
         jQuery(document).ready(function () {

             // $('#txt_fechaFin').datepicker("option", "dateFormat", 'dd/mm/yy');
             var cargarFechaNoticia = $('#hidden_FechaNoticia').val();
             if (typeof cargarFechaNoticia == 'undefined') {
                 cargarFechaNoticia = null;
             } else {

                 // $('#txt_fechaFin').val(ofe_fechaFin);
             }

             var $datepicker = $('#txt_fecha');
             $datepicker.datepicker("option", "dateFormat", 'dd/mm/yy');
             if (cargarFechaNoticia != null)
                 $datepicker.datepicker('setDate', cargarFechaNoticia);

         });

    </script>
</asp:Content>

