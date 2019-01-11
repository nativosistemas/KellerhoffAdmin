var listaEstadoPedidos = null;
var diasPedidos = 0;
//var isPedidosNoEncontrado = false;
var nroPendientesDeFacturar = 0;

jQuery(document).ready(function () {

    if (listaEstadoPedidos == null) {
        listaEstadoPedidos = eval('(' + $('#hiddenListaPedidos').val() + ')');
        if (typeof listaEstadoPedidos == 'undefined') {
            listaEstadoPedidos = null;
        }
    }
    CargarHtmlListaEstadoPedidos();
    nroPendientesDeFacturar = 0;
});

function CargarHtmlListaEstadoPedidos() {
    if (listaEstadoPedidos != null) {
        var strHtml = '';
        if (listaEstadoPedidos.length > 0) {
            strHtml += '<table class="tbl-buscador-productos"  style="width:100% !important;" border="0" cellspacing="0" cellpadding="0">';
            strHtml += '<tr>'; //bp-off-top-left
            strHtml += '<th  width="15%"  ><div class="bp-top-left">Factura</div></th>'; //class="bp-off-top-left bp-med-ancho"
            strHtml += '<th  width="15%" class="bp-med-ancho" >Remito</th>';
            strHtml += '<th  width="10%" class="bp-med-ancho" >Fecha</th>';
            strHtml += '<th  width="10%" class="bp-med-ancho" >Hora</th>';
            strHtml += '<th  width="15%" class="bp-med-ancho" >Importe</th>';
            strHtml += '<th  width="15%" class="bp-med-ancho" >Unidades</th>';
            strHtml += '<th  width="10%" class="bp-med-ancho" >Renglones</th>';
            strHtml += '<th  width="10%" class="bp-med-ancho" >Estado</th>';
            strHtml += '</tr>';
            var totalMontoPedidos = 0;
            for (var i = 0; i < listaEstadoPedidos.length; i++) {
                strHtml += '<tr>';
                var strHtmlColorFondo = '';
                if (i % 2 != 0) {
                    strHtmlColorFondo = ' bp-td-color';
                }

                if (listaEstadoPedidos[i].EstadoToString == 'PendienteDeFacturar') {
                    var strDescVerDetalle =  'Ver detalles';// + '&nbsp;(' + listaEstadoPedidos[i].DetalleSucursal + ')' '
                    if (listaEstadoPedidos[i].DetalleSucursal != '')
                        strDescVerDetalle += ' de ' + listaEstadoPedidos[i].DetalleSucursal;
                    strHtml += '<td class="first-td2' + strHtmlColorFondo + '">' + '<div class="txt_link_doc" >' + ' <a href="Documento.aspx?t=PENDINTE&id=' + nroPendientesDeFacturar + '" >' + strDescVerDetalle + '</a>' + '</div>' + '</td>';                 
                    //strHtml += '<td class="first-td2' + strHtmlColorFondo + '">' + '<div class="txt_link_doc" >' + ' <a href="Documento.aspx?t=PENDINTE&id=' + nroPendientesDeFacturar + '" >' + 'Ver detalles' + '&nbsp;(' + listaEstadoPedidos[i].DetalleSucursal + ')' + '</a>' + '</div>' + '</td>';
                    nroPendientesDeFacturar++;
                }
                else {
                    strHtml += '<td class="first-td2' + strHtmlColorFondo + '">' + '<div class="txt_link_doc" >' + ' <a href="Documento.aspx?t=FAC&id=' + listaEstadoPedidos[i].NumeroFactura + '" >' + listaEstadoPedidos[i].NumeroFactura + '</a>' + '</div>' + '</td>';
                }
                strHtml += '<td class="' + strHtmlColorFondo + '">' + listaEstadoPedidos[i].NumeroRemito + '</td>';
                strHtml += '<td class="' + strHtmlColorFondo + '">' + listaEstadoPedidos[i].FechaIngresoToString + '</td>';
                strHtml += '<td class="' + strHtmlColorFondo + '">' + listaEstadoPedidos[i].FechaIngresoHoraToString + '</td>';
                totalMontoPedidos += listaEstadoPedidos[i].MontoTotal;
                strHtml += '<td class="' + strHtmlColorFondo + '" style="text-align:right !important; padding-right:4px;">$&nbsp;' + FormatoDecimalConDivisorMiles(listaEstadoPedidos[i].MontoTotal) + '</td>';
                strHtml += '<td class="' + strHtmlColorFondo + '">' + listaEstadoPedidos[i].CantidadUnidad + '</td>';
                strHtml += '<td class="' + strHtmlColorFondo + '">' + listaEstadoPedidos[i].CantidadRenglones + '</td>';
                strHtml += '<td class="' + strHtmlColorFondo + '">' + listaEstadoPedidos[i].EstadoToString + '</td>';
                strHtml += '</tr>';
            }
            // Monto total
            strHtml += '<tr>';
            strHtml += '<th style="height:25px;">&nbsp;</th>';
            strHtml += '<th>&nbsp;</th>';
            strHtml += '<th>&nbsp;</th>';
            strHtml += '<th>Total:</th>'; //
            strHtml += '<th  style="text-align:right !important; padding-right:4px;">$&nbsp;' + FormatoDecimalConDivisorMiles(totalMontoPedidos.toFixed(2)) + '</th>';
            strHtml += '<th>&nbsp;</th>';
            strHtml += '<th>&nbsp;</th>';
            strHtml += '<th>&nbsp;</th>';
            strHtml += '</tr>';
            //fin monto total

            strHtml += '</table>';
            if (listaEstadoPedidos.length > cantFilaParaEnCabezado) {
                strHtml += '<br/>';
                strHtml += '<input type="button" onclick="volver()" value="VOLVER" class="btn_gral" />';
            }
        } else {
            strHtml = objMensajeNoEncontrado;
        }
        $('#divResultadoPedidos').html(strHtml);
    }
}
function onclickListaPedidos() {
    PageMethods.IsBanderaUsarDll(OnCallBackIsBanderaUsarDll, OnFail);
    return false;
}



function OnCallBackIsBanderaUsarDll(args) {
    if (args) {
        var myRadio = $('input[name=group1]');
        diasPedidos = myRadio.filter(':checked').val();
        PageMethods.ObtenerRangoFecha(diasPedidos, OnCallBackObtenerRangoFecha, OnFailCargandoContenedorGeneralFondo);
        $('#divCargandoContenedorGeneralFondo').css('display', 'block');
    } else {
        alert(objMensajeDllNoDisponible);
    }
}
function OnCallBackObtenerRangoFecha(args) {
    var intAñoDesde = args[2];
    var intMesDesde = args[1];
    var intDiaDesde = args[0];
    var intAñoHasta = args[5];
    var intMesHasta = args[4];
    var intDiaHasta = args[3];
    location.href = 'estadopedidosresultado.aspx';
}