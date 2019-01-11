<%@ Page Title="" Language="C#" MasterPageFile="~/master/BaseAdminJs3.master" AutoEventWireup="true" CodeFile="GestionOfertaEditar.aspx.cs" Inherits="admin_pages_GestionOfertaEditar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../includes/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../../includes/js/bootstrap.min.js" type="text/javascript"></script>
    <link  rel="stylesheet" type="text/css"  href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script> 
    <script src="../../includes/js/GestionOferta.js?n=2" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            RecuperarTodasOfertaDetalles();
            //CargarDatosOfertaEditar();
            var titulo = $('#hiddenOfe_titulo').val();
            if (typeof titulo == 'undefined') {
                titulo = null;
            } else {
                $('#txt_titulo').val(titulo);
            }
            var descr = $('#hiddenOfe_descr').val();
            if (typeof descr == 'undefined') {
                descr = null;
            } else {
                $('#txt_descr').val(descr);
            }
            var descuento = $('#hiddenOfe_descuento').val();
            if (typeof descuento == 'undefined') {
                descuento = null;
            } else {
                $('#txt_descuento').val(descuento);
            }

            var etiqueta = $('#hiddenOfe_etiqueta').val();
            if (typeof etiqueta == 'undefined') {
                etiqueta = null;
            } else {
                $('#txt_etiqueta').val(etiqueta);
            }

            var etiquetaColor = $('#hiddenOfe_etiquetaColor').val();
            if (typeof etiquetaColor == 'undefined') {
                etiquetaColor = null;
            } else {
                //  $('#txt_etiquetaColor').val(etiquetaColor);
                $('#cmdEtiquetaColor').val(etiquetaColor);
            }
            var idOferta = $('#hiddenOfe_idOferta').val();
            if (typeof idOferta == 'undefined') {
                idOferta = null;
            } else {
                if (parseInt(idOferta) == 0) {
                    $('#contenedorProductoTransfer').css('display', 'none');
                    //$('#contenedorTransfer').css('display', 'none');
                }
            }
            var nombreTransfer = $('#hiddenOfe_nombreTransfer').val();
            if (typeof nombreTransfer == 'undefined') {
                nombreTransfer = null;
            } else {
          
                $('#cmdTransfer').val(nombreTransfer);
            }

            if (parseInt(idOferta) != 0) {
                var idTipo = $('#hiddenOfe_tipo').val();
                if (typeof idTipo == 'undefined') {
                    idTipo = null;
                } else {
                    if (parseInt(idTipo) == 1) {
                        $('#opProducto').attr('checked', true);// $("#radio").attr('checked', true); 
                        $('#opTransfer').attr('checked', false);
                    } else if (parseInt(idTipo) == 2) {
                        $('#opTransfer').attr('checked', true);
                        $('#opProducto').attr('checked', false);
                    }
                }
                onclickRadio();
            }
            //var ofe_fechaFin = $('#hiddenOfe_fechaFin').val();
            //if (typeof ofe_fechaFin == 'undefined') {
            //    ofe_fechaFin = null;
            //} else {

            //   // $('#txt_fechaFin').val(ofe_fechaFin);
            //}

          
            $('#txt_fechaFin').datepicker();//"option", "dateFormat", 'dd/mm/yy'
         

        });
        function onclickRadio() {
            var val = $('input[name=opciones]:checked', '#Form2').val();
            if (val == 1) {
                $('#contenedorProductos').css('visibility', 'visible');
                $('#contenedorTransfer').css('visibility', 'hidden');
            }
            else if (val == 2) {
                $('#contenedorTransfer').css('visibility', 'visible');
                $('#contenedorProductos').css('visibility', 'hidden');
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h2 class="sub-header">Editar/Agregar
    </h2>
    <button type="button" class="btn btn-info" onclick="GuardarOferta();  return false;">Guardar</button>
    &nbsp;&nbsp;
    <button type="button" class="btn btn-info" onclick="return VolverOferta();">Volver</button>
    <div class="form-group">
        <label for="txt_titulo">Título:</label>
        <input type="text" class="form-control" id="txt_titulo" style="text-transform: uppercase;" />
    </div>
    <div class="form-group">
        <label for="txt_descr">Descrición:</label>
        <input type="text" class="form-control" id="txt_descr" />
    </div>
    <div class="form-group">
        <label for="txt_descuento">Descuento:</label>
        <input type="text" class="form-control" id="txt_descuento" style="text-transform: uppercase;" />
    </div>
    <div class="form-group">
        <label for="txt_etiqueta">Etiqueta:</label>
        <input type="text" class="form-control" id="txt_etiqueta" style="text-transform: uppercase;" />
    </div>
    <div class="form-group">
        <label for="txt_etiquetaColor">Etiqueta Color:</label>
        <%--  <input type="text" class="form-control" id="txt_etiquetaColor" />--%>
        <select id="cmdEtiquetaColor" class="selectpicker form-control">
            <option value="E74C3C" style="background-color: #E74C3C; color: #FFF;">Rojo</option>
            <option value="2ECC71" style="background-color: #2ECC71; color: #FFF;">Verde</option>
        </select>

    </div>
    <div class="form-group">
        <label for="txt_fechaFin">Fecha Fin:</label>
        <input type="text" class="form-control" id="txt_fechaFin"  />
    </div>

    <div id="contenedorProductoTransfer" class="row">
        <div class="col-md-6">
            <div class="radio">
                <label>
                    <input type="radio" name="opciones" id="opProducto" value="1" checked="checked" onclick="onclickRadio()" />
                    Producto
                </label>
            </div>
            <div id="contenedorProductos">
                <h3 class="sub-header">Productos de la oferta
                </h3>
                <div id="divContenedorGrillaOfertaProducto"></div>
                <hr />

                <h4 class="sub-header">Buscar productos
                </h4>
                <div class="form-group">
                    <label for="txtBuscar">Palabra clave:</label>
                    <input type="text" class="form-control" id="txtBuscar"  onkeypress="onkeypressAgregarBuscar(event)"/>
                </div>
                <button id="btnBuscar" type="button" class="btn btn-primary" onclick="return busOferta();">Buscar</button>

                <div id="divContenedorGrilla"></div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="radio">
                <label>
                    <input type="radio" name="opciones" id="opTransfer" value="2" onclick="onclickRadio()" />
                    Transfer
                </label>
            </div>
            <div id="contenedorTransfer">
                <h3 class="sub-header">Seleccionar Transfer
                </h3>

                <div class="form-group">
                    <label for="cmdTransfer">Transfer:</label>
                    <select id="cmdTransfer" class="selectpicker">
                        <%Response.Write(getHtmlOptionTransfer()); %>
                    </select>
                </div>
            </div>
        </div>
    </div>
    <%  AgregarHtmlOculto(); %>

     <script type="text/javascript">
        jQuery(document).ready(function () {

           // $('#txt_fechaFin').datepicker("option", "dateFormat", 'dd/mm/yy');
            var ofe_fechaFin = $('#hiddenOfe_fechaFin').val();
            if (typeof ofe_fechaFin == 'undefined') {
                ofe_fechaFin = null;
            } else {

                // $('#txt_fechaFin').val(ofe_fechaFin);
            }

            var $datepicker = $('#txt_fechaFin');
            $datepicker.datepicker("option", "dateFormat", 'dd/mm/yy');
            if (ofe_fechaFin != null)
                $datepicker.datepicker('setDate', ofe_fechaFin);
    
        });
    
    </script>
</asp:Content>

