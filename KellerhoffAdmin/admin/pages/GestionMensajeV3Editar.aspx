<%@ Page Title="" Language="C#" MasterPageFile="~/master/BaseAdminJs3.master" AutoEventWireup="true" CodeFile="GestionMensajeV3Editar.aspx.cs" Inherits="admin_pages_GestionMensajeV3Editar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="lib/css/bootstrap.min.css" />
    <%--    <link href="../../includes/css/VistaPrevia.css" rel="stylesheet" />--%>
    <%--    <link href="../../includes/css/vistaPreviaReducido.css" rel="stylesheet" />--%>
    <link rel="stylesheet" type="text/css" href="lib/css/prettify.css" />
    <link rel="stylesheet" type="text/css" href="lib/css/bootstrap-wysihtml5.css" />
    <style type="text/css">
        .labelSucursales {
            display: inline-block;
            max-width: 100%;
            margin-bottom: 5px;
            font-weight: 700;
        }
        .cssMayuscula {
        font-size:16px ;
        }
    </style>

    <script src="../../includes/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="lib/js/wysihtml5-0.3.0.js" type="text/javascript"></script>
    <script src="lib/js/prettify.js" type="text/javascript"></script>
    <script src="lib/js/bootstrap-wysihtml5.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script src="../../includes/js/GestionMensajeV3.js?n=7" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            listaTodasSucursales = eval('(' + $('#hiddenTodasSucursales').val() + ')');
            if (typeof listaTodasSucursales == 'undefined') {
                listaTodasSucursales = null;
            }
            var varTmn_todosSucursales = $('#hidden_todosSucursales').val();
            var asunto = $('#hidden_asunto').val();
            if (typeof asunto == 'undefined') {
                asunto = null;
            } else {
                $('#txtAsunto').val(asunto);
            }
            var varHtml = $('#hidden_mensaje').val();
            if (typeof varHtml == 'undefined') {
                varHtml = '';
            }
            $('#textareaHtml').wysihtml5({
                "html": true, //Button which allows you to edit the generated HTML. Default false
                "color": true
            }).html(varHtml);

            $('#txtFechaDesde').datepicker();
            $('#txtFechaHasta').datepicker();
            listaSucursalesSeleccionada = getSucursalesSeleccionada(varTmn_todosSucursales);
            CargarHtmlSucursales();
          
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h2 class="sub-header">Editar/Agregar
    </h2>
    <button type="button" class="btn btn-info" onclick="GuardarMensaje();  return false;">Guardar</button>
    &nbsp;&nbsp;
    <button type="button" class="btn btn-info" onclick="return VolverGestionMensaje();">Volver</button>
    &nbsp;&nbsp;
    <br />
    <br />

    <button type="button" class="btn btn-warning" onclick="return MostrarMensaje();">Vista previa</button>
    <br />
    <div class="form-group">
        <label for="txtAsunto">Asunto:</label>
        <input type="text" class="form-control" id="txtAsunto" />
    </div>
    <div class="form-group">
        <label for="textareaHtml">Mensaje:</label>
        <textarea id="textareaHtml" class="textarea" placeholder="" style="width: 100%; height: 200px"></textarea>
    </div>

    <div id="divContenedorTodasSucursales" class="form-group">
        <input type="hidden" id="hiddenSucursales" name="hiddenSucursales"  />
        <input type="radio" checked="checked" id="radioTodasSucursales" name="radioSucursales" value="todasSucursales" onclick="onclickRadioSucursal()" />
        <label for="radioTodasSucursales" class="labelSucursales cssMayuscula" onclick="onclickRadioSucursal()">Todas las sucursales</label><br />
        <input type="radio" id="radioElegirSucursal" name="radioSucursales" value="elegirSucursales" onclick="onclickRadioSucursal()" />
        <label for="radioElegirSucursal" class="labelSucursales cssMayuscula" onclick="onclickRadioSucursal()">Elegir sucursales:</label><br />
        <div id="divTodasSucursales"></div>
    </div>

    <div class="form-group">
        <label for="txtFechaDesde">Fecha Desde:</label>
        <input type="text" class="form-control" id="txtFechaDesde" />
    </div>
    <div class="form-group">
        <label for="txtFechaHasta">Fecha Hasta:</label>
        <input type="text" class="form-control" id="txtFechaHasta" />
    </div>




    <%  AgregarHtmlOculto(); %>

    <script type="text/javascript">
        jQuery(document).ready(function () {
            cargarFechasEditar();
        });

    </script>
</asp:Content>

