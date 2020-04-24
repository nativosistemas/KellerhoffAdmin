<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GestionMensajeV3EditarV2.aspx.cs" Inherits="admin_pages_GestionMensajeV3EditarV2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Droguería Kellerhoff</title>
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
    <meta name="robots" content="noindex,nofollow,noarchive,noodp,nosnippet" />
    <script src="../../includes/js/jquery-3.1.0.min.js" type="text/javascript"></script>
    <%--    <link rel="stylesheet" type="text/css"  href="~/includes/css/jquery-confirm.min.css"/>
    <script src="../../includes/js/jquery-confirm.min.js" type="text/javascript"></script>--%>
    <script src="../../includes/js/Libreria.js?n=1" type="text/javascript"></script>
    <link rel="shortcut icon" href="../favicon.ico" />
     <link rel="stylesheet" type="text/css" href="lib/css/bootstrap.min.css" />
   <%-- <link href="../../includes/css/VistaPrevia.css" rel="stylesheet" />--%>
        <link href="../../includes/css/vistaPreviaReducido.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="lib/css/prettify.css" />
    <link rel="stylesheet" type="text/css" href="lib/css/bootstrap-wysihtml5.css" />
    <script src="../../includes/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="lib/js/wysihtml5-0.3.0.js" type="text/javascript"></script>
    <script src="lib/js/prettify.js" type="text/javascript"></script>
    <script src="lib/js/bootstrap-wysihtml5.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script src="../../includes/js/GestionMensajeV3.js?n=2" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {
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
        });
        function modalModuloHide() {
            $('#modalModulo').modal('hide');
        }
    </script>

</head>
<body>
    <div id="modalModulo" class="modal md-effect-1 md-content portfolio-modal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true"></div>
    <form id="form1" runat="server">
        <div>
            <h2 class="sub-header">Editar/Agregar
            </h2>
            <button type="button" class="btn btn-info" onclick="GuardarMensaje();  return false;">Guardar</button>
            &nbsp;&nbsp;
    <button type="button" class="btn btn-info" onclick="return VolverGestionMensaje();">Volver</button>
            &nbsp;&nbsp;
    <button type="button" class="btn btn-info" onclick="return MostrarMensaje();">Vista previa</button>

            <div class="form-group">
                <label for="txtAsunto">Asunto:</label>
                <input type="text" class="form-control" id="txtAsunto" />
            </div>
            <div class="form-group">
                <label for="textareaHtml">Mensaje:</label>
                <textarea id="textareaHtml" class="textarea" placeholder="" style="width: 100%; height: 200px"></textarea>
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
        </div>
    </form>
    <script type="text/javascript">
        jQuery(document).ready(function () {


            var cargarFechaDesde = $('#hidden_fechaDesde').val();
            if (typeof cargarFechaDesde == 'undefined') {
                cargarFechaDesde = null;
            }
            var cargarFechaHasta = $('#hidden_fechaHasta').val();
            if (typeof cargarFechaHasta == 'undefined') {
                cargarFechaHasta = null;
            }

            var $datepickerFechaDesde = $('#txtFechaDesde');
            $datepickerFechaDesde.datepicker("option", "dateFormat", 'dd/mm/yy');
            if (cargarFechaDesde != null) {
                $datepickerFechaDesde.datepicker('setDate', cargarFechaDesde);
            }

            var $datepickerFechaHasta = $('#txtFechaHasta');
            $datepickerFechaHasta.datepicker("option", "dateFormat", 'dd/mm/yy');
            if (cargarFechaDesde != null) {
                $datepickerFechaHasta.datepicker('setDate', cargarFechaHasta);
            }
        });

    </script>
</body>
</html>
