var listaComprobantesObraSocial = null;
function volverObraSocialParametros() {
    location.href = 'ConsultaDeComprobantesObraSocial.aspx';
}
function CargarHtmlObraSocial() {

    if (listaComprobantesObraSocial == null) {
        listaComprobantesObraSocial = eval('(' + $('#hiddenListaPlanillasObraSocial').val() + ')');
        if (typeof listaComprobantesObraSocial == 'undefined') {
            listaComprobantesObraSocial = null;
        }
    }


    var strHtml = '';
    if (listaComprobantesObraSocial != null) {

        if (listaComprobantesObraSocial.length > 0) {
            // boton volver
            strHtml += '<input type="button" onclick="volverObraSocialParametros()" value="VOLVER" class="btn_gral" />';
            strHtml += '<div style="height:25px;">&nbsp;</div>';
            // fin boton volver
            strHtml += '<table class="tbl-ComposicionSaldo" border="0" cellspacing="0" cellpadding="0"  width="100%">';
            strHtml += '<tr>';
            strHtml += '<th  width="40%" ><div class="bp-top-left">Fecha</div></th>'; //<th class="bp-off-top-left bp-med-ancho">
            strHtml += '<th   class="bp-med-ancho">Año</th>';
            strHtml += '<th  width="20%"  class="bp-med-ancho" >Mes </th>';
            strHtml += '<th  width="10%"  class="bp-med-ancho" >Quincena </th>';
            strHtml += '<th  width="15%"  class="bp-med-ancho">Semana </th>';
            strHtml += '<th  width="15%"  class="bp-med-ancho">Importe</th>';
 
            strHtml += '</tr>';

            for (var i = 0; i < listaComprobantesObraSocial.length; i++) {
                var strHtmlColorFondo = '';
                if (i % 2 != 0) {
                    strHtmlColorFondo = ' bp-td-color';
                }
                
                strHtml += '<tr>';
                strHtml += '<td class="' + strHtmlColorFondo + '">' + listaComprobantesObraSocial[i].FechaToString.substring(0, 10) + '</td>'; // .substring(10) 28/02/2013 10:14:00 a.m. SOLO MOSTRAR FECHA
                strHtml += '<td class="' + strHtmlColorFondo + '">' + listaComprobantesObraSocial[i].Anio + '</td>';
                strHtml += '<td class="' + strHtmlColorFondo + '">' + listaComprobantesObraSocial[i].Mes + '</td>';
                strHtml += '<td class="' + strHtmlColorFondo + '">' + listaComprobantesObraSocial[i].Quincena + '</td>';
                strHtml += '<td class="' + strHtmlColorFondo + '">' + listaComprobantesObraSocial[i].Semana + '</td>';
                var strImporte = '&nbsp;';
                if (isNotNullEmpty(listaComprobantesObraSocial[i].Importe)) {
                    strImporte = '$&nbsp;' + FormatoDecimalConDivisorMiles(listaComprobantesObraSocial[i].Importe.toFixed(2));
                }
                strHtml += '<td style="text-align:right !important; white-space:nowrap;" class="' + strHtmlColorFondo + '">' + strImporte + '</td>';
                strHtml += '</tr>';
            }
            //// Inicio Importe Total
            //strHtml += '<tr>';
            //strHtml += '<th style="height:25px;">&nbsp;</th>';
            //strHtml += '<th>&nbsp;</th>';
            //strHtml += '<th>&nbsp;</th>';
            //strHtml += '<th>IMPORTE TOTAL: </th>';
            //var strImporteTotal = '&nbsp;';

            //if (SaldoChequesEnCartera != null) {
            //    strImporteTotal = '$&nbsp;' + FormatoDecimalConDivisorMiles(parseFloat(SaldoChequesEnCartera.replace(",", ".")).toFixed(2));
            //} else {
            //    strImporteTotal = '$&nbsp;' + FormatoDecimalConDivisorMiles('0.00');
            //}

            //strHtml += '<th  style="text-align:right !important; white-space:nowrap;" >' + strImporteTotal + '</th>';
            //strHtml += '</tr>';

            //// Fin Importe Total

            strHtml += '</table>';


            //if (listaChequesEnCartera.length > cantFilaParaEnCabezado) {
            //    strHtml += '<br/>';
            //    strHtml += '<input type="button" onclick="volverObraSocialParametros()" value="VOLVER" class="btn_gral" />';
            //}

        } else {
            strHtml += '<input type="button" onclick="volverObraSocialParametros()" value="VOLVER" class="btn_gral" />';
            strHtml += objMensajeNoEncontrado;
        }
    }
    $('#divResultadoObraSocial').html(strHtml);
}