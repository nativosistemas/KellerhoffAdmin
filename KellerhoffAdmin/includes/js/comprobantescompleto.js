var objListaComprobante = null;

jQuery(document).ready(function () {

    if (objListaComprobante == null) {
        objListaComprobante = eval( '(' + $('#hiddenListaComprobanteCompleto').val() + ')' );
        if (typeof objListaComprobante == 'undefined') {
            objListaComprobante = null;
        }
    }
    CargarListaComprobanteCompleto();
});

function CargarListaComprobanteCompleto() {
    if (objListaComprobante != null) {
        var strHtml = '';
        if (objListaComprobante.length > 0) {

            strHtml += '<div>';
            strHtml += '<table style="width:100%;"><tr><td align="left">';
            //strHtml += '<table><tr> <td> <div style="height:16px;width:16px;background-color:green;"></div> </td><td style="color:green;font-size:14px;">Comprobantes Discriminados </td> </tr> </table>';
            strHtml += '</td><td >';
            strHtml += '<input type="button" onclick="volver()" value="VOLVER" class="btn_gral" />';
            strHtml += '</td> </tr></table>';
            strHtml += '</div>';
            strHtml += '<table class="tbl-ComposicionSaldo" border="0" cellspacing="0" cellpadding="0"  width="100%">';
            strHtml += '<tr>';
            strHtml += '<th  width="15%" ><div class="bp-top-left">Fecha</div></th>'; // class="bp-off-top-left bp-med-ancho"
            strHtml += '<th >Comprobante</th>';
            strHtml += '<th >Número Comprobante</th>';
            strHtml += '<th >Monto Exento</th>';
            strHtml += '<th >Monto Gravado</th>';
            strHtml += '<th >Monto Iva Inscripto</th>';
            strHtml += '<th >Monto Iva No Inscripto</th>';
            strHtml += '<th >Monto Percepciones DGR</th>';
            strHtml += '<th >Monto Percepciones Municipal</th>';//MontoPercepcionesMunicipal
            strHtml += '<th >Monto Total</th>';
            strHtml += '</tr>';
            for (var i = 0; i < objListaComprobante.length; i++) {
                var strHtmlColorFondo = '';
                if (i % 2 != 0) {
                    strHtmlColorFondo = ' bp-td-color';
                }
                strHtml += '<tr>';
                strHtml += '<td class="' + strHtmlColorFondo + '">' + objListaComprobante[i].FechaToString + '</td>';
                strHtml += '<td class="' + strHtmlColorFondo + '">' + objListaComprobante[i].Comprobante + '</td>';
                strHtml += '<td class="' + strHtmlColorFondo + '">';
                if (isDetalleComprobante(objListaComprobante[i].Comprobante)) {
                    strHtml += '<div class="txt_link_doc"><a href="Documento.aspx?t=' + objListaComprobante[i].Comprobante + '&id=' + objListaComprobante[i].NumeroComprobante + '" >' + objListaComprobante[i].NumeroComprobante + '</a></div>';
                } else {
                    strHtml += listaFicha[i].Comprobante;
                }
                strHtml +=  '</td>';
//                strHtml += '<td class="' + strHtmlColorFondo + '">' + objListaComprobante[i].NumeroComprobante + '</td>';
                strHtml += '<td class="' + strHtmlColorFondo + '">' + '$&nbsp;' + FormatoDecimalConDivisorMiles(objListaComprobante[i].MontoExento.toFixed(2)) + '</td>';
                strHtml += '<td class="' + strHtmlColorFondo + '">' + '$&nbsp;' + FormatoDecimalConDivisorMiles(objListaComprobante[i].MontoGravado.toFixed(2)) + '</td>';
                strHtml += '<td class="' + strHtmlColorFondo + '">' + '$&nbsp;' + FormatoDecimalConDivisorMiles(objListaComprobante[i].MontoIvaInscripto.toFixed(2)) + '</td>';
                strHtml += '<td class="' + strHtmlColorFondo + '">' + '$&nbsp;' + FormatoDecimalConDivisorMiles(objListaComprobante[i].MontoIvaNoInscripto.toFixed(2)) + '</td>';
                strHtml += '<td class="' + strHtmlColorFondo + '">' + '$&nbsp;' + FormatoDecimalConDivisorMiles(objListaComprobante[i].MontoPercepcionesDGR.toFixed(2)) + '</td>';
                strHtml += '<td class="' + strHtmlColorFondo + '">' + '$&nbsp;' + FormatoDecimalConDivisorMiles(objListaComprobante[i].MontoPercepcionesMunicipal.toFixed(2)) + '</td>';
                strHtml += '<td class="' + strHtmlColorFondo + '">' + '$&nbsp;' + FormatoDecimalConDivisorMiles(objListaComprobante[i].MontoTotal.toFixed(2)) + '</td>';
                strHtml += '</tr>';
            } // fin for (var i = 0; i < objListaComprobante.length; i++) {
            strHtml += '</table>';

            var httpRaiz = $('#hiddenRaiz').val(); 
            strHtml += '<div style="text-align:right;margin-top:10px;">' + '<a  href="' + httpRaiz + 'servicios/generar_comprobantes_discriminado.aspx"  >' + '<img src="../../img/iconos/disk.png" alt="txt" title="Descarga csv" />' + '</a></div>';
            //+ '&nbsp;&nbsp;&nbsp;' + 
            strHtml += '</br>';

            if (objListaComprobante.length > cantFilaParaEnCabezado) {
                strHtml += '<br/>';
                strHtml += '<input type="button" onclick="volver()" value="VOLVER" class="btn_gral" />';
            }
        } // fin  if (objListaComprobante.length > 0) 
        else {
            strHtml = objMensajeNoEncontrado;
        }
        $('#divResultadoComprobanteCompleto').html(strHtml);
    } // fin  if (objListaComprobante != null) 
}