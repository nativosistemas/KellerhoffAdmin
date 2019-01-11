var objDocumento = null;
var objTipoDocumento = null;
//
var nombreArchivoPDF = '';
var nroDocumento = '';
var isLlamarArchivoPDF = true;
var contadorPDF = 0;
var isArchivoGenerado = null;
//
// inicio trz
var nombreArchivoPDF_trz = '';
var nroDocumento_trz = '';
var isLlamarArchivoPDF_trz = true;
var contadorPDF_trz = 0;
var isArchivoGenerado_trz = null;
// fin trz

jQuery(document).ready(function () {
    if (isArchivoGenerado == null) {
        isArchivoGenerado = $('#hiddenIsPdfExiste').val();
        if (typeof objTipoDocumento == 'undefined') {
            isArchivoGenerado = null;
        }
        if (isArchivoGenerado != null) {
            if (isArchivoGenerado == 'true') {
                isArchivoGenerado = true;
            } else {
                isArchivoGenerado = false;
            }
        }
    }

    if (objTipoDocumento == null) {
        objTipoDocumento = $('#hiddenTipoDocumento').val();
        if (typeof objTipoDocumento == 'undefined') {
            objTipoDocumento = null;
        }
    }
    if (objDocumento == null) {
        objDocumento = eval('(' + $('#hiddenDocumento').val() + ')');
        if (typeof objDocumento == 'undefined') {
            objDocumento = null;
        }
    }

    if (objTipoDocumento != null && objDocumento != null) {
        switch (objTipoDocumento) {
            case 'FAC':
                $('#divTituloDocumento').html('Factura');
                CargarHtmlFactura();
                break;
            case 'PENDINTE':
                $('#divTituloDocumento').html('Pendiente de facturar');
                CargarHtmlPendienteDeFacturar();
                break;
            case 'NCR':
                $('#divTituloDocumento').html('Nota de crédito');
                CargarHtmlNotaCredito();
                break;
            case 'NDE':
                $('#divTituloDocumento').html('Nota de débito');
                CargarHtmlNotaDebito();
                break;
            case 'RES':
                $('#divTituloDocumento').html('Resumen');
                CargarHtmlResumen();
                break;
            case 'OSC':
                $('#divTituloDocumento').html('Obra Social');
                CargarHtmlObraSocialCliente();
                break;

            default:
                break;
        }
    } else {
        $('#divContenedorDocumento').html(objMensajeNoEncontrado);
    }
});


///////////// PENDIENTE DE FACTURAR

function CargarHtmlCabeceraPendienteDeFacturar(objDocumento) {

    var strHtml = '';
    strHtml += '<table style="width:100% !important; text-align:left;" border="0" cellspacing="0" cellpadding="0">';
    strHtml += '<tr>';
    strHtml += '<td>';
    strHtml += '<div style="float:left;padding:5px;">';
    strHtml += '<div class="thDocumentoEncabezado">Fecha:</div>' + '<div class="thDocumentoEncabezadoDetalle">' + objDocumento.FechaIngresoToString + '</div>';
    strHtml += '</div>';
    strHtml += '<div style="float:left;padding:5px;">';

    var strMontoTotal = '&nbsp;';
    if (isNotNullEmpty(objDocumento.MontoTotal)) {
        strMontoTotal = '$&nbsp;' + FormatoDecimalConDivisorMiles(objDocumento.MontoTotal.toFixed(2));
    }
    strHtml += '<div class="thDocumentoEncabezado">Total:</div>' + '<div class="thDocumentoEncabezadoDetalle">' + strMontoTotal + '</div>';
    strHtml += '</div>';
    strHtml += '</td>';
    strHtml += '</tr>';

    strHtml += '<tr>';
    strHtml += '<td>';
    strHtml += '</td>';
    strHtml += '</tr>';
    strHtml += '</table>';
    return strHtml;
}


function CargarHtmlPendienteDeFacturar() {
    if (objDocumento != null) {
        var strHtml = '';

        strHtml += CargarHtmlCabeceraPendienteDeFacturar(objDocumento);

        strHtml += '<table class="tbl-buscador-productos"  style="width:100% !important;" border="0" cellspacing="0" cellpadding="0">';
        strHtml += '<tr>';
        strHtml += '<th width="26%"  class="bp-med-ancho">';
        strHtml += 'Producto';
        strHtml += '</th>';
        strHtml += '<th width="10%" class="bp-med-ancho">';
        strHtml += 'Cantidad';
        strHtml += '</th>';
        strHtml += '</tr>';
        if (objDocumento.Items != null) {
            for (var i = 0; i < objDocumento.Items.length; i++) {
                if (isVizualizarDetalleResumenLlendoDescripcion(objDocumento.Items[i].NombreObjetoComercial)) {
                    var strHtmlColorFondo = '';
                    if (i % 2 != 0) {
                        strHtmlColorFondo = ' bp-td-color';
                    }
                    strHtml += '<tr>';
                    strHtml += '<td class="' + strHtmlColorFondo + '" style="text-align:left; padding-left:5px;">';
                    strHtml += objDocumento.Items[i].NombreObjetoComercial;
                    strHtml += '</td>';
                    strHtml += '<td class="' + strHtmlColorFondo + '">';
                    strHtml += objDocumento.Items[i].Cantidad;
                    strHtml += '</td>';
                    strHtml += '</tr>';
                }
            }
        }
        strHtml += '</table>';

        //var httpRaiz = $('#hiddenRaiz').val();
        //strHtml += '<div style="text-align:right;margin-top:10px;">' + '<a href="../../archivos/Diseño TXT Factura.pdf" title="FORMATO" target="_blank">' + 'FORMATO TXT' + '</a>' + '&nbsp;&nbsp;&nbsp;' + '<a  href="' + httpRaiz + 'servicios/generar_archivo.aspx?factura=' + objDocumento.Numero + '"  >' + '<img src="../../img/iconos/disk.png" alt="txt" title="Descarga txt" />' + '</a></div>'; //  objDocumento.Numero;+ objDocumento.Numero + 
        strHtml += '</br>';

        if (objDocumento.lista != null) {
            if (objDocumento.lista.length > cantFilaParaEnCabezado) {
                strHtml += CargarHtmlCabeceraPendienteDeFacturar(objDocumento);
                strHtml += '<input type="button" onclick="volver()" value="VOLVER" class="btn_gral" />';
            }
        }
        $('#divContenedorDocumento').html(strHtml);
    } else {
        $('#divContenedorDocumento').html(objMensajeNoEncontrado);
    }
}

///////////// FIN PENDIENTE DE FACTURAR
function CargarHtmlCabeceraFactura(objDocumento) {
    var isMostrarEnCabezadoTodo = true;
    if (objDocumento.Numero.substr(0, 1) == 'B') {
        isMostrarEnCabezadoTodo = false;
    }
    var strHtml = '';
    strHtml += '<table style="width:100% !important; text-align:left;" border="0" cellspacing="0" cellpadding="0">';
    strHtml += '<tr>';
    strHtml += '<td>';
    strHtml += '<div style="float:left;padding:5px;">';
    strHtml += '<div class="thDocumentoEncabezado">Número:</div>' + '<div class="thDocumentoEncabezadoDetalle">' + objDocumento.Numero + '</div>';
    strHtml += '</div>';
    strHtml += '<div style="float:left;padding:5px;">';
    strHtml += '<div class="thDocumentoEncabezado">Fecha:</div>' + '<div class="thDocumentoEncabezadoDetalle">' + objDocumento.FechaToString + '</div>';
    strHtml += '</div>';
    strHtml += '<div style="float:left;padding:5px;">';

    var strMontoTotal = '&nbsp;';
    if (isNotNullEmpty(objDocumento.MontoTotal)) {
        strMontoTotal = '$&nbsp;' + FormatoDecimalConDivisorMiles(objDocumento.MontoTotal.toFixed(2));
    }
    strHtml += '<div class="thDocumentoEncabezado">Total:</div>' + '<div class="thDocumentoEncabezadoDetalle">' + strMontoTotal + '</div>';
    strHtml += '</div>';
    strHtml += '</td>';
    strHtml += '</tr>';

    strHtml += '<tr>';
    strHtml += '<td>';
    if (isMostrarEnCabezadoTodo) {
        strHtml += '<div style="float:left;padding:5px;">';
        var strMontoExento = '&nbsp;';
        if (isNotNullEmpty(objDocumento.MontoExento)) {
            strMontoExento = '$&nbsp;' + FormatoDecimalConDivisorMiles(objDocumento.MontoExento.toFixed(2));
        }
        strHtml += '<div class="thDocumentoEncabezado">Monto Exento:</div>' + '<div class="thDocumentoEncabezadoDetalle">' + strMontoExento + '</div>';
        strHtml += '</div>';
    }
    if (isMostrarEnCabezadoTodo) {
        strHtml += '<div style="float:left;padding:5px;">';
        var strMontoGravado = '&nbsp;';
        if (isNotNullEmpty(objDocumento.MontoGravado)) {
            strMontoGravado = '$&nbsp;' + FormatoDecimalConDivisorMiles(objDocumento.MontoGravado.toFixed(2));
        }
        strHtml += '<div class="thDocumentoEncabezado" >Monto Gravado:</div>' + '<div class="thDocumentoEncabezadoDetalle">' + strMontoGravado + '</div>';
        strHtml += '</div>';
    }
    if (isMostrarEnCabezadoTodo) {
        strHtml += '<div style="float:left;padding:5px;">';
        var strMontoIvaInscripto = '&nbsp;';
        if (isNotNullEmpty(objDocumento.MontoIvaInscripto)) {
            strMontoIvaInscripto = '$&nbsp;' + FormatoDecimalConDivisorMiles(objDocumento.MontoIvaInscripto.toFixed(2));
        }
        strHtml += '<div class="thDocumentoEncabezado" >IVA Inscripto:</div>' + '<div class="thDocumentoEncabezadoDetalle">' + strMontoIvaInscripto + '</div>';
        strHtml += '</div>';
    }
    if (isMostrarEnCabezadoTodo) {
        strHtml += '<div style="float:left;padding:5px;">';
        var strMontoIvaNoInscripto = '&nbsp;';
        if (isNotNullEmpty(objDocumento.MontoIvaNoInscripto)) {
            strMontoIvaNoInscripto = '$&nbsp;' + FormatoDecimalConDivisorMiles(objDocumento.MontoIvaNoInscripto.toFixed(2));
        }
        strHtml += '<div class="thDocumentoEncabezado" >IVA No Inscripto: </div>' + '<div class="thDocumentoEncabezadoDetalle">' + strMontoIvaNoInscripto + '</div>';
        strHtml += '</div>';
    }
    strHtml += '<div style="float:left;padding:5px;">';
    var strMontoPercepcionDGR = '&nbsp;';
    if (isNotNullEmpty(objDocumento.MontoPercepcionDGR)) {
        strMontoPercepcionDGR = '$&nbsp;' + FormatoDecimalConDivisorMiles(objDocumento.MontoPercepcionDGR.toFixed(2));
    }
    strHtml += '<div class="thDocumentoEncabezado" >Percepción DGR:</div>' + '<div class="thDocumentoEncabezadoDetalle">' + strMontoPercepcionDGR + '</div>';
    // Inicio parte nueva
    var strMontoPercepcionMunicipal = '&nbsp;';
    if (isNotNullEmpty(objDocumento.MontoPercepcionMunicipal)) {
        strMontoPercepcionMunicipal = '$&nbsp;' + FormatoDecimalConDivisorMiles(objDocumento.MontoPercepcionMunicipal.toFixed(2));
    }
    strHtml += '<div class="thDocumentoEncabezado" >Percepción Municipal:</div>' + '<div class="thDocumentoEncabezadoDetalle">' + strMontoPercepcionMunicipal + '</div>';
    // Fin parte nueva
    strHtml += '</div>';
    strHtml += '</td>';
    strHtml += '</tr>';
    strHtml += '</table>';
    return strHtml;
}

function CargarHtmlFactura() {
    if (objDocumento != null) {
        var strHtml = '';

        strHtml += CargarHtmlCabeceraFactura(objDocumento);

        strHtml += '<table class="tbl-buscador-productos"  style="width:100% !important;" border="0" cellspacing="0" cellpadding="0">';
        strHtml += '<tr>';
        strHtml += '<th  width="10%" >'; // class="bp-off-top-left bp-med-ancho"
        strHtml += '<div class="bp-top-left">' + 'Troquel' + '</div>' + '</td>';
        strHtml += '</th>';
        strHtml += '<th width="26%"  class="bp-med-ancho">';
        strHtml += 'Producto';
        strHtml += '</th>';
        strHtml += '<th width="15%" class="bp-med-ancho">';
        strHtml += 'Características';
        strHtml += '</th>';
        strHtml += '<th width="10%" class="bp-med-ancho">';
        strHtml += 'Cantidad';
        strHtml += '</th>';
        strHtml += '<th width="12%" class="bp-med-ancho">';
        strHtml += 'Precio Público';
        strHtml += '</th>';
        strHtml += '<th width="12%" class="bp-med-ancho">';
        strHtml += 'Precio Unit.';
        strHtml += '</th>';
        strHtml += '<th width="15%" class="bp-med-ancho">';
        strHtml += 'Importe';
        strHtml += '</th>';
        strHtml += '</tr>';
        if (objDocumento.lista != null) {
            for (var i = 0; i < objDocumento.lista.length; i++) {
                if (isVizualizarDetalleResumenLlendoDescripcion(objDocumento.lista[i].Descripcion)) {
                    var strHtmlColorFondo = '';
                    if (i % 2 != 0) {
                        strHtmlColorFondo = ' bp-td-color';
                    }
                    strHtml += '<tr>';
                    strHtml += '<td class="' + strHtmlColorFondo + '">';
                    strHtml += objDocumento.lista[i].Troquel;
                    strHtml += '</td>';
                    strHtml += '<td class="' + strHtmlColorFondo + '" style="text-align:left; padding-left:5px;">';
                    strHtml += objDocumento.lista[i].Descripcion;
                    strHtml += '</td>';
                    strHtml += '<td class="' + strHtmlColorFondo + '">';
                    strHtml += objDocumento.lista[i].Caracteristica;
                    strHtml += '</td>';
                    strHtml += '<td class="' + strHtmlColorFondo + '">';
                    strHtml += objDocumento.lista[i].Cantidad;
                    strHtml += '</td>';
                    strHtml += '<td style="text-align:right !important; white-space:nowrap;" class="' + strHtmlColorFondo + '">';
                    var strPrecioPublico = '&nbsp;';
                    if (isNotNullEmpty(objDocumento.lista[i].PrecioPublico)) {
                        strPrecioPublico = '$&nbsp;' + FormatoDecimalConDivisorMiles(parseFloat(objDocumento.lista[i].PrecioPublico.replace(",", ".")).toFixed(2));
                    }
                    strHtml += strPrecioPublico; // objDocumento.lista[i].PrecioPublico;
                    strHtml += '</td>';
                    strHtml += '<td style="text-align:right !important; white-space:nowrap;" class="' + strHtmlColorFondo + '">';
                    var strPrecioUnitario = '&nbsp;';
                    if (isNotNullEmpty(objDocumento.lista[i].PrecioUnitario)) {
                        strPrecioUnitario = '$&nbsp;' + FormatoDecimalConDivisorMiles(parseFloat(objDocumento.lista[i].PrecioUnitario.replace(",", ".")).toFixed(2));
                    }
                    strHtml += strPrecioUnitario;
                    strHtml += '</td>';
                    strHtml += '<td style="text-align:right !important; white-space:nowrap;" class="' + strHtmlColorFondo + '">';
                    var strImporte = '&nbsp;';
                    if (isNotNullEmpty(objDocumento.lista[i].Importe)) {// parseFloat(listaCompocisionSaldo[i].Pago.replace(",", "."))
                        strImporte = '$&nbsp;' + FormatoDecimalConDivisorMiles(parseFloat(objDocumento.lista[i].Importe.replace(",", ".")).toFixed(2));
                    }
                    strHtml += strImporte;
                    strHtml += '</td>';
                    strHtml += '</tr>';
                }
            }
        }
        strHtml += '</table>';


        //
        var httpRaiz = $('#hiddenRaiz').val();
        strHtml += '<div class="cssDivDescarga" >';

        strHtml += '<a href="../../archivos/Diseño TXT Factura.pdf" title="FORMATO" target="_blank">' + 'FORMATO TXT' + '</a>';
        strHtml += '&nbsp;&nbsp;&nbsp;';
        strHtml += '<a href="' + httpRaiz + 'servicios/generar_archivo.aspx?factura=' + objDocumento.Numero + '"  >' + '<img class="cssImagenDescarga" src="../../img/iconos/disk.png" alt="txt" title="Descarga txt" />' + '</a>';
        strHtml += '<a href="' + httpRaiz + 'servicios/generar_archivoPdf.aspx?tipo=' + objTipoDocumento + '&nro=' + objDocumento.Numero + '"  onclick="return funImprimirComprobantePdf(' + '\'' + objDocumento.Numero + '\'' + ');" >' + '<img  class="cssImagenDescarga"  id="imgPdf" src="../../img/iconos/PDF.png" alt="txt" title="Descarga pdf" height="34" width="32" />' + '</a>';
        //begin Trazable
        if (objDocumento.FacturaTrazable) {
            strHtml += '&nbsp;';
            strHtml += '<a href="' + httpRaiz + 'servicios/generar_archivoPdf.aspx?tipo=' + 'REM_ANEXTRAZ' + '&nro=' + objDocumento.Numero + '"  onclick="return funImprimirComprobantePdf_trz(' + '\'' + objDocumento.NumeroRemito + '\'' + ');" >' + '<img  class="cssImagenDescarga"  id="imgPdf_Trz" src="../../img/iconos/PDF_TRZ.png" alt="txt" title="Descarga pdf trazable" height="34" width="32" />' + '</a>';
        }
        //end Trazable
        strHtml += '</div>';
        strHtml += '</br>';
        //
        if (objDocumento.lista != null) {
            if (objDocumento.lista.length > cantFilaParaEnCabezado) {
                strHtml += CargarHtmlCabeceraFactura(objDocumento);
                strHtml += '<input type="button" onclick="volver()" value="VOLVER" class="btn_gral" />';
            }
        }
        $('#divContenedorDocumento').html(strHtml);
    } else {
        $('#divContenedorDocumento').html(objMensajeNoEncontrado);
    }
}
function CargarHtmlCabeceraNotaDebito(objDocumento) {
    var strHtml = '';
    var isMostrarEnCabezadoTodo = true;
    if (objDocumento.Numero.substr(0, 1) == 'B') {
        isMostrarEnCabezadoTodo = false;
    }
    strHtml += '<table style="width:100% !important;" border="0" cellspacing="0" cellpadding="0">';
    strHtml += '<tr>';
    strHtml += '<td>';
    strHtml += '<div style="float:left;padding:5px;">';
    strHtml += '<div class="thDocumentoEncabezado">Número:</div>' + '<div class="thDocumentoEncabezadoDetalle">' + objDocumento.Numero + '</div>';
    strHtml += '</div>';
    strHtml += '<div style="float:left;padding:5px;">';
    strHtml += '<div class="thDocumentoEncabezado">Fecha:</div>' + '<div class="thDocumentoEncabezadoDetalle">' + objDocumento.FechaToString + '</div>';
    strHtml += '</div>';
    strHtml += '<div style="float:left;padding:5px;">';
    var strMontoTotal = '&nbsp;';
    if (isNotNullEmpty(objDocumento.MontoTotal)) {
        strMontoTotal = '$&nbsp;' + FormatoDecimalConDivisorMiles(objDocumento.MontoTotal.toFixed(2));
    }
    strHtml += '<div class="thDocumentoEncabezado">Total:</div>' + '<div class="thDocumentoEncabezadoDetalle">' + strMontoTotal + '</div>';
    strHtml += '</div>';
    strHtml += '</td>';
    strHtml += '</tr>';

    strHtml += '<tr>';
    strHtml += '<td>';
    if (isMostrarEnCabezadoTodo) {
        strHtml += '<div style="float:left;padding:5px;">';
        var strMontoExento = '&nbsp;';
        if (isNotNullEmpty(objDocumento.MontoExento)) {
            strMontoExento = '$&nbsp;' + FormatoDecimalConDivisorMiles(objDocumento.MontoExento.toFixed(2));
        }
        strHtml += '<div class="thDocumentoEncabezado">Monto Exento:</div>' + '<div class="thDocumentoEncabezadoDetalle">' + strMontoExento + '</div>';
        strHtml += '</div>';
    }
    if (isMostrarEnCabezadoTodo) {
        strHtml += '<div style="float:left;padding:5px;">';
        var strMontoGravado = '&nbsp;';
        if (isNotNullEmpty(objDocumento.MontoGravado)) {
            strMontoGravado = '$&nbsp;' + FormatoDecimalConDivisorMiles(objDocumento.MontoGravado.toFixed(2));
        }
        strHtml += '<div class="thDocumentoEncabezado" >Monto Gravado:</div>' + '<div class="thDocumentoEncabezadoDetalle">' + strMontoGravado + '</div>';
        strHtml += '</div>';
    }
    if (isMostrarEnCabezadoTodo) {
        strHtml += '<div style="float:left;padding:5px;">';
        var strMontoIvaInscripto = '&nbsp;';
        if (isNotNullEmpty(objDocumento.MontoIvaInscripto)) {
            strMontoIvaInscripto = '$&nbsp;' + FormatoDecimalConDivisorMiles(objDocumento.MontoIvaInscripto.toFixed(2));
        }
        strHtml += '<div class="thDocumentoEncabezado" >IVA Inscripto:</div>' + '<div class="thDocumentoEncabezadoDetalle">' + strMontoIvaInscripto + '</div>';
        strHtml += '</div>';
    }
    if (isMostrarEnCabezadoTodo) {
        strHtml += '<div style="float:left;padding:5px;">';
        var strMontoIvaNoInscripto = '&nbsp;';
        if (isNotNullEmpty(objDocumento.MontoIvaNoInscripto)) {
            strMontoIvaNoInscripto = '$&nbsp;' + FormatoDecimalConDivisorMiles(objDocumento.MontoIvaNoInscripto.toFixed(2));
        }
        strHtml += '<div class="thDocumentoEncabezado" >IVA No Inscripto: </div>' + '<div class="thDocumentoEncabezadoDetalle">' + strMontoIvaNoInscripto + '</div>';
        strHtml += '</div>';
    }
    strHtml += '<div style="float:left;padding:5px;">';
    var strMontoPercepcionDGR = '&nbsp;';
    if (isNotNullEmpty(objDocumento.MontoPercepcionDGR)) {
        strMontoPercepcionDGR = '$&nbsp;' + FormatoDecimalConDivisorMiles(objDocumento.MontoPercepcionDGR.toFixed(2));
    }
    strHtml += '<div class="thDocumentoEncabezado" >Percepción DGR:</div>' + '<div class="thDocumentoEncabezadoDetalle">' + strMontoPercepcionDGR + '</div>';
    strHtml += '</div>';
    strHtml += '</td>';
    strHtml += '</tr>';
    strHtml += '</table>';

    return strHtml;
}
function CargarHtmlNotaDebito() {
    if (objDocumento != null) {
        var strHtml = '';

        //
        strHtml += CargarHtmlCabeceraNotaDebito(objDocumento);
        strHtml += '<table class="tbl-ComposicionSaldo"  style="width:100% !important;" border="0" cellspacing="0" cellpadding="0">';
        strHtml += '<tr>';
        strHtml += '<th width="75%" >'; // class="bp-off-top-left bp-med-ancho"
        strHtml += '<div class="bp-top-left">' + 'Descripción' + '</td>';
        strHtml += '</th>';
        strHtml += '<th width="25%"   class="bp-med-ancho">';
        strHtml += 'Importe';
        strHtml += '</th>';
        strHtml += '</tr>';
        if (objDocumento.lista != null) {
            for (var i = 0; i < objDocumento.lista.length; i++) {
                if (isVizualizarDetalleResumenLlendoDescripcion(objDocumento.lista[i].Descripcion)) {
                    var strHtmlColorFondo = '';
                    if (i % 2 != 0) {
                        strHtmlColorFondo = ' bp-td-color';
                    }
                    strHtml += '<tr>';
                    strHtml += '<td class="' + strHtmlColorFondo + '" style="text-align:left; padding-left:5px;">';
                    strHtml += objDocumento.lista[i].Descripcion;
                    strHtml += '</td>';
                    strHtml += '<td style="text-align:right !important; white-space:nowrap;" class="' + strHtmlColorFondo + '">';
                    var strImporte = '&nbsp;';
                    if (isNotNullEmpty(objDocumento.lista[i].Importe)) {
                        strImporte = '$&nbsp;' + FormatoDecimalConDivisorMiles(parseFloat(objDocumento.lista[i].Importe.replace(",", ".")).toFixed(2));
                    }
                    strHtml += strImporte; //  objDocumento.lista[i].Importe;
                    strHtml += '</td>';
                    strHtml += '</tr>';
                }
            }
        }
        strHtml += '</table>';
        //       
        var httpRaiz = $('#hiddenRaiz').val();
        strHtml += '<div class="cssDivDescarga" >'; //onclick="funImprimirComprobante(' + '\'' + objDocumento.Numero + '\'' + ');"
        //        strHtml += '&nbsp;';
        strHtml += '<a   href="' + httpRaiz + 'servicios/generar_archivoPdf.aspx?tipo=' + objTipoDocumento + '&nro=' + objDocumento.Numero + '"  onclick="return funImprimirComprobantePdf(' + '\'' + objDocumento.Numero + '\'' + ');"  >' + '<img  class="cssImagenDescarga"  id="imgPdf" src="../../img/iconos/PDF.png" alt="txt" title="Descarga pdf" height="34" width="32" />' + '</a>';
        strHtml += '</div>';
        strHtml += '</br>';
        //       
        if (objDocumento.lista != null) {
            if (objDocumento.lista.length > cantFilaParaEnCabezado) {
                strHtml += CargarHtmlCabeceraNotaDebito(objDocumento);
                strHtml += '<input type="button" onclick="volver()" value="VOLVER" class="btn_gral" />';
            }
        }
        $('#divContenedorDocumento').html(strHtml);
    } else {
        $('#divContenedorDocumento').html(objMensajeNoEncontrado);
    }
}
function CargarHtmlCabeceraNotaCredito(objDocumento) {
    var strHtml = '';
    var isMostrarEnCabezadoTodo = true;
    if (objDocumento.Numero.substr(0, 1) == 'B') {
        isMostrarEnCabezadoTodo = false;
    }
    strHtml += '<table style="width:100% !important;" border="0" cellspacing="0" cellpadding="0">';
    strHtml += '<tr>';
    strHtml += '<td>';
    strHtml += '<div style="float:left;padding:5px;">';
    strHtml += '<div class="thDocumentoEncabezado">Número:</div>' + '<div class="thDocumentoEncabezadoDetalle">' + objDocumento.Numero + '</div>';
    strHtml += '</div>';
    strHtml += '<div style="float:left;padding:5px;">';
    strHtml += '<div class="thDocumentoEncabezado">Fecha:</div>' + '<div class="thDocumentoEncabezadoDetalle">' + objDocumento.FechaToString + '</div>';
    strHtml += '</div>';
    strHtml += '<div style="float:left;padding:5px;">';
    var strMontoTotal = '&nbsp;';
    if (isNotNullEmpty(objDocumento.MontoTotal)) {
        strMontoTotal = '$&nbsp;' + FormatoDecimalConDivisorMiles(objDocumento.MontoTotal.toFixed(2));
    }
    strHtml += '<div class="thDocumentoEncabezado">Total:</div>' + '<div class="thDocumentoEncabezadoDetalle">' + strMontoTotal + '</div>';
    strHtml += '</div>';
    strHtml += '</td>';
    strHtml += '</tr>';

    strHtml += '<tr>';
    strHtml += '<td>';
    if (isMostrarEnCabezadoTodo) {
        strHtml += '<div style="float:left;padding:5px;">';
        var strMontoExento = '&nbsp;';
        if (isNotNullEmpty(objDocumento.MontoExento)) {
            strMontoExento = '$&nbsp;' + FormatoDecimalConDivisorMiles(objDocumento.MontoExento.toFixed(2));
        }
        strHtml += '<div class="thDocumentoEncabezado">Monto Exento:</div>' + '<div class="thDocumentoEncabezadoDetalle">' + strMontoExento + '</div>';
        strHtml += '</div>';
    }
    if (isMostrarEnCabezadoTodo) {
        strHtml += '<div style="float:left;padding:5px;">';
        var strMontoGravado = '&nbsp;';
        if (isNotNullEmpty(objDocumento.MontoGravado)) {
            strMontoGravado = '$&nbsp;' + FormatoDecimalConDivisorMiles(objDocumento.MontoGravado.toFixed(2));
        }
        strHtml += '<div class="thDocumentoEncabezado" >Monto Gravado:</div>' + '<div class="thDocumentoEncabezadoDetalle">' + strMontoGravado + '</div>';
        strHtml += '</div>';
    }
    if (isMostrarEnCabezadoTodo) {
        strHtml += '<div style="float:left;padding:5px;">';
        var strMontoIvaInscripto = '&nbsp;';
        if (isNotNullEmpty(objDocumento.MontoIvaInscripto)) {
            strMontoIvaInscripto = '$&nbsp;' + FormatoDecimalConDivisorMiles(objDocumento.MontoIvaInscripto.toFixed(2));
        }
        strHtml += '<div class="thDocumentoEncabezado" >IVA Inscripto:</div>' + '<div class="thDocumentoEncabezadoDetalle">' + strMontoIvaInscripto + '</div>';
        strHtml += '</div>';
    }
    if (isMostrarEnCabezadoTodo) {
        strHtml += '<div style="float:left;padding:5px;">';
        var strMontoIvaNoInscripto = '&nbsp;';
        if (isNotNullEmpty(objDocumento.MontoIvaNoInscripto)) {
            strMontoIvaNoInscripto = '$&nbsp;' + FormatoDecimalConDivisorMiles(objDocumento.MontoIvaNoInscripto.toFixed(2));
        }
        strHtml += '<div class="thDocumentoEncabezado" >IVA No Inscripto: </div>' + '<div class="thDocumentoEncabezadoDetalle">' + strMontoIvaNoInscripto + '</div>';
        strHtml += '</div>';
    }
    strHtml += '<div style="float:left;padding:5px;">';
    var strMontoPercepcionDGR = '&nbsp;';
    if (isNotNullEmpty(objDocumento.MontoPercepcionDGR)) {
        strMontoPercepcionDGR = '$&nbsp;' + FormatoDecimalConDivisorMiles(objDocumento.MontoPercepcionDGR.toFixed(2));
    }
    strHtml += '<div class="thDocumentoEncabezado" >Percepción DGR:</div>' + '<div class="thDocumentoEncabezadoDetalle">' + strMontoPercepcionDGR + '</div>';
    strHtml += '</div>';
    strHtml += '</td>';
    strHtml += '</tr>';
    strHtml += '</table>';
    return strHtml;
}

function CargarHtmlNotaCredito() {
    if (objDocumento != null) {

        var isMostrarTodo = true;
        switch (objDocumento.Motivo) {
            case 'Monto':
                isMostrarTodo = false;
                break;
            case 'Ajuste':
                isMostrarTodo = false;
                break;
            case 'Obra Social':
                isMostrarTodo = false;
                break;
            default:
                break;
        }
        var strHtml = '';

        strHtml += CargarHtmlCabeceraNotaCredito(objDocumento);

        strHtml += '<table class="tbl-ComposicionSaldo"  style="width:100% !important; margin-top:10px;" border="0" cellspacing="0" cellpadding="0">';
        strHtml += '<tr>';

        if (isMostrarTodo) {
            strHtml += '<th  class="bp-off-top-left"><div class="bp-top-left">';
            strHtml += 'Troquel'; // Características
            strHtml += '</div></th>';
        }
        if (isMostrarTodo) {
            strHtml += '<th>';
            strHtml += 'Descripción';
            strHtml += '</th>';
        } else {
            strHtml += '<th class="bp-off-top-left"><div class="bp-top-left" >';
            strHtml += 'Descripción';
            strHtml += '</div></th>';
        }
        if (isMostrarTodo) {
            strHtml += '<th>';
            strHtml += 'Cantidad';
            strHtml += '</th>';
        }
        if (isMostrarTodo) {
            strHtml += '<th>';
            strHtml += 'Precio Público';
            strHtml += '</th>';
        }
        if (isMostrarTodo) {
            strHtml += '<th>';
            strHtml += 'Precio Unitario';
            strHtml += '</th>';
        }
        strHtml += '<th>';
        strHtml += 'Importe';
        strHtml += '</th>';
        strHtml += '</tr>';
        if (objDocumento.lista != null) {
            for (var i = 0; i < objDocumento.lista.length; i++) {
                if (isVizualizarDetalleResumenLlendoDescripcion(objDocumento.lista[i].Descripcion)) {
                    var strHtmlColorFondo = '';
                    if (i % 2 != 0) {
                        strHtmlColorFondo = ' bp-td-color';
                    }
                    strHtml += '<tr>';
                    if (isMostrarTodo) {
                        strHtml += '<td class="' + strHtmlColorFondo + '" style="text-align:left; padding-left:5px;">';
                        strHtml += objDocumento.lista[i].Troquel;
                        strHtml += '</td>';
                    }
                    strHtml += '<td class="' + strHtmlColorFondo + '" style="text-align:left; padding-left:5px;">';
                    strHtml += objDocumento.lista[i].Descripcion;
                    strHtml += '</td>';
                    if (isMostrarTodo) {
                        strHtml += '<td class="' + strHtmlColorFondo + '" style="text-align:left; padding-left:5px;">';
                        strHtml += objDocumento.lista[i].Cantidad;
                        strHtml += '</td>';
                    }
                    if (isMostrarTodo) {
                        strHtml += '<td class="' + strHtmlColorFondo + '" style="text-align:right !important; white-space:nowrap;">';
                        if (isNotNullEmpty(objDocumento.lista[i].PrecioPublico)) {
                            strHtml += '$&nbsp;' + FormatoDecimalConDivisorMiles(parseFloat(objDocumento.lista[i].PrecioPublico.replace(",", ".")).toFixed(2));
                        }
                        strHtml += '</td>';
                    }
                    if (isMostrarTodo) {
                        strHtml += '<td class="' + strHtmlColorFondo + '" style="text-align:right !important; white-space:nowrap;">';
                        if (isNotNullEmpty(objDocumento.lista[i].PrecioUnitario)) {
                            strHtml += '$&nbsp;' + FormatoDecimalConDivisorMiles(parseFloat(objDocumento.lista[i].PrecioUnitario.replace(",", ".")).toFixed(2));
                        }
                        strHtml += '</td>';
                    }
                    strHtml += '<td style="text-align:right !important; white-space:nowrap;" class="' + strHtmlColorFondo + '">';
                    var strImporte = '&nbsp;';
                    if (isNotNullEmpty(objDocumento.lista[i].Importe)) {
                        strImporte = '$&nbsp;' + FormatoDecimalConDivisorMiles(parseFloat(objDocumento.lista[i].Importe.replace(",", ".")).toFixed(2));
                    }
                    strHtml += strImporte;
                    strHtml += '</td>';
                    strHtml += '</tr>';
                }
            }
        }
        strHtml += '</table>';
        //
        var httpRaiz = $('#hiddenRaiz').val();
        strHtml += '<div class="cssDivDescarga">';
        strHtml += '<a   href="' + httpRaiz + 'servicios/generar_archivoPdf.aspx?tipo=' + objTipoDocumento + '&nro=' + objDocumento.Numero + '"  onclick="return funImprimirComprobantePdf(' + '\'' + objDocumento.Numero + '\'' + ');"  >' + '<img  class="cssImagenDescarga" id="imgPdf" src="../../img/iconos/PDF.png" alt="txt" title="Descarga pdf" height="34" width="32" />' + '</a>';
        strHtml += '</div>';
        strHtml += '</br>';
        //
        if (objDocumento.lista != null) {
            if (objDocumento.lista.length > cantFilaParaEnCabezado) {
                strHtml += CargarHtmlCabeceraNotaCredito(objDocumento);
                strHtml += '<input type="button" onclick="volver()" value="VOLVER" class="btn_gral" />';
            }
        }


        $('#divContenedorDocumento').html(strHtml);
    } else {
        $('#divContenedorDocumento').html(objMensajeNoEncontrado);
    }
}

////////////////////
function CargarHtmlResumen() {
    if (objDocumento != null) {
        var strHtml = '';

        strHtml += '<table style="width:100% !important;" border="0" cellspacing="0" cellpadding="0">';
        strHtml += '<tr>';
        strHtml += '<td>';
        strHtml += '<div style="float:left;padding:5px;">';
        strHtml += '<div class="thDocumentoEncabezado">Número:</div>' + '<div class="thDocumentoEncabezadoDetalle">' + objDocumento.Numero + '</div>';
        strHtml += '</div>';
        strHtml += '<div style="float:left;padding:5px;">';
        strHtml += '<div class="thDocumentoEncabezado">Semana:</div>' + '<div class="thDocumentoEncabezadoDetalle">' + objDocumento.NumeroSemana + '</div>';
        strHtml += '</div>';
        strHtml += '<div style="float:left;padding:5px;">';
        strHtml += '<div class="thDocumentoEncabezadoDetalle">' + '<b>Desde: </b>' + objDocumento.PeriodoDesdeToString + '<b> - Hasta: </b>' + objDocumento.PeriodoHastaToString + '</div>';
        strHtml += '</div>';
        strHtml += '<div style="float:left;padding:5px;">';
        var strTotalResumen = '&nbsp;';
        if (isNotNullEmpty(objDocumento.TotalResumen)) {
            strTotalResumen = '$&nbsp;' + FormatoDecimalConDivisorMiles(objDocumento.TotalResumen.toFixed(2));
        }
        strHtml += '<div class="thDocumentoEncabezado">Total:</div>' + '<div class="thDocumentoEncabezadoDetalle">' + strTotalResumen + '</div>';
        strHtml += '</div>';
        strHtml += '</td>';
        strHtml += '</tr>';
        strHtml += '</table>';

        strHtml += '<table  class="tbl-ComposicionSaldo"  style="width:100% !important;" border="0" cellspacing="0" cellpadding="0">';
        strHtml += '<tr>';
        //        strHtml += '<th  class="bp-off-top-left bp-med-ancho">';
        //        strHtml += '<div class="bp-top-left">' + 'Item' + '</div></td>';
        //        strHtml += '</th>';
        //        strHtml += '<th  class="bp-med-ancho">';
        //        strHtml += 'Número Hoja';
        //        strHtml += '</th>';
        //        strHtml += '<th  class="bp-med-ancho">';
        strHtml += '<th width="20%"  class="bp-off-top-left"><div class="bp-top-left" >';
        strHtml += 'Día';
        strHtml += '</div></th>';
        //        strHtml += '</th>';
        strHtml += '<th width="30%"  class="bp-med-ancho">';
        strHtml += 'Descripción';
        strHtml += '</th>';
        strHtml += '<th  width="25%"   class="bp-med-ancho">';
        strHtml += 'Tipo Comprobante';
        strHtml += '</th>';
        strHtml += '<th  width="25%"  class="bp-med-ancho">';
        strHtml += 'Importe';
        strHtml += '</th>';
        strHtml += '</tr>';

        for (var i = 0; i < objDocumento.lista.length; i++) {
            if (isVizualizarDetalleResumenLlendoDescripcion(objDocumento.lista[i].Descripcion)) {
                var strHtmlColorFondo = '';
                if (i % 2 != 0) {
                    strHtmlColorFondo = ' bp-td-color';
                }
                strHtml += '<tr>';
                //                strHtml += '<td class="' + strHtmlColorFondo + '">';
                //                strHtml += objDocumento.lista[i].NumeroItem;
                //                strHtml += '</td>';
                //                strHtml += '<td class="' + strHtmlColorFondo + '">';
                //                strHtml += objDocumento.lista[i].NumeroHoja;
                //                strHtml += '</td>';
                strHtml += '<td class="' + strHtmlColorFondo + '">';
                strHtml += objDocumento.lista[i].Dia;
                strHtml += '</td>';
                strHtml += '<td class="' + strHtmlColorFondo + '">';
                strHtml += ObtenerLinkDeDocumentoDesdeStr(objDocumento.lista[i].Descripcion);
                strHtml += '</td>';
                strHtml += '<td class="' + strHtmlColorFondo + '">';
                var strTipoComprobante = objDocumento.lista[i].TipoComprobante;
                //        FAC = 0,
                //        REC = 1,
                //        NDE = 2,
                //        NCR = 3,
                //        NDI = 4,
                //        NCI = 5,
                //        RES = 9,
                //        CIE = 10,
                //        OSC = 13,
                switch (objDocumento.lista[i].TipoComprobante) {
                    case '0':
                        strTipoComprobante = 'FAC';
                        break;
                    case '1':
                        strTipoComprobante = 'REC';
                        break;
                    case '2':
                        strTipoComprobante = 'NDE';
                        break;
                    case '3':
                        strTipoComprobante = 'NCR';
                        break;
                    case '4':
                        strTipoComprobante = 'NDI';
                        break;
                    case '5':
                        strTipoComprobante = 'NCI';
                        break;
                    case '9':
                        strTipoComprobante = 'RES';
                        break;
                    case '10':
                        strTipoComprobante = 'CIE';
                        break;
                    case '13':
                        strTipoComprobante = 'OSC';
                        break;
                    default:
                        break;
                }
                strHtml += strTipoComprobante; // objDocumento.lista[i].TipoComprobante; //       
                strHtml += '</td>';
                strHtml += '<td style="text-align:right !important; white-space:nowrap;" class="' + strHtmlColorFondo + '">';
                var strImporte = '&nbsp;';
                if (isNotNullEmpty(objDocumento.lista[i].Importe)) {
                    strImporte = '$&nbsp;' + FormatoDecimalConDivisorMiles(parseFloat(objDocumento.lista[i].Importe.replace(",", ".")).toFixed(2));
                }
                strHtml += strImporte;
                strHtml += '</td>';
                strHtml += '</tr>';
            }
        }
        strHtml += '</table>';
        //
        var httpRaiz = $('#hiddenRaiz').val();
        strHtml += '<div class="cssDivDescarga">';
        strHtml += '&nbsp;';
        strHtml += '<a   href="' + httpRaiz + 'servicios/generar_archivoPdf.aspx?tipo=' + objTipoDocumento + '&nro=' + objDocumento.Numero + '"  onclick="return funImprimirComprobantePdf(' + '\'' + objDocumento.Numero + '\'' + ');"  >' + '<img  class="cssImagenDescarga" id="imgPdf" src="../../img/iconos/PDF.png" alt="txt" title="Descarga pdf" height="34" width="32" />' + '</a>';
        strHtml += '</div>';
        strHtml += '</br>';
        //
        if (objDocumento.lista.length > cantFilaParaEnCabezado) {
            strHtml += '<br/>';
            strHtml += '<input type="button" onclick="volver()" value="VOLVER" class="btn_gral" />';
        }



        $('#divContenedorDocumento').html(strHtml);
    } else {
        $('#divContenedorDocumento').html(objMensajeNoEncontrado);
    }

}
function isVizualizarDetalleResumenLlendoDescripcion(pDescripcion) {
    var isRespuesta = true;
    if (pDescripcion.toUpperCase().search('SIGUE EN HOJA') > -1) {
        isRespuesta = false;
    }
    if (pDescripcion.toUpperCase().search('VIENE DE HOJA') > -1) {
        isRespuesta = false;
    }
    return isRespuesta;
}
/////
function funImprimirComprobantePdf(pValor) {
    if (isArchivoGenerado) {
        return true;
    } else {
        if (isLlamarArchivoPDF) {
            contadorPDF = 0;
            nroDocumento = pValor;
            $('#imgPdf').attr('src', '../../img/varios/ajax-loader.gif');
            isLlamarArchivoPDF = false;
            setTimeout(function () { PageMethods.IsExistenciaComprobante(nombreArchivoPDF, OnCallBackComprobantePDF, OnFail); }, 10);
        }
        return false;
    }
}
function funImprimirComprobantePdf_trz(pValor) {
    if (isArchivoGenerado_trz) {
        return true;
    } else {
        if (isLlamarArchivoPDF_trz) {
            contadorPDF_trz = 0;
            nroDocumento_trz = pValor;
            nombreArchivoPDF_trz = 'REM_ANEXTRAZ' + '_' + nroDocumento_trz;
            $('#imgPdf_Trz').attr('src', '../../img/varios/ajax-loader.gif');
            isLlamarArchivoPDF_trz = false;
            setTimeout(function () { PageMethods.IsExistenciaComprobante(nombreArchivoPDF_trz, OnCallBackComprobantePdf_trz, OnFail); }, 10);
        }
        return false;
    }
}
function OnCallBackComprobantePdf_trz(args) {
    if (args) {
        isArchivoGenerado_trz = true;
        isLlamarArchivoPDF_trz = true;
        $('#imgPdf_Trz').attr('src', '../../img/iconos/PDF_TRZ.png');
        window.open('../../servicios/generar_archivoPdf.aspx?tipo=' + 'REM_ANEXTRAZ' + '&nro=' + nroDocumento_trz, '_parent');
    } else {
        if (contadorPDF <= 300) {
            nombreArchivoPDF_trz = 'REM_ANEXTRAZ' + '_' + nroDocumento_trz;
            setTimeout(function () { PageMethods.IsExistenciaComprobante(nombreArchivoPDF_trz, OnCallBackComprobantePdf_trz, OnFail); }, 1000);
        } else {
            isLlamarArchivoPDF_trz = true;
            $('#imgPdf_Trz').attr('src', '../../img/iconos/PDF_TRZ.png');
            alert('No se pudo descargar el archivo, inténtelo nuevamente.');
        }
        contadorPDF_trz++;
    }
}
function OnCallBackComprobantePDF(args) {
    if (args) {
        isArchivoGenerado = true;
        isLlamarArchivoPDF = true;
        $('#imgPdf').attr('src', '../../img/iconos/PDF.png');
        window.open('../../servicios/generar_archivoPdf.aspx?tipo=' + objTipoDocumento + '&nro=' + nroDocumento, '_parent');
    } else {
        if (contadorPDF <= 300) {
            var nombreArchivoPDF = objTipoDocumento + '_' + nroDocumento;
            setTimeout(function () { PageMethods.IsExistenciaComprobante(nombreArchivoPDF, OnCallBackComprobantePDF, OnFail); }, 1000);
        } else {
            isLlamarArchivoPDF = true;
            $('#imgPdf').attr('src', '../../img/iconos/PDF.png');
            alert('No se pudo descargar el archivo, inténtelo nuevamente.');
        }
        contadorPDF++;
    }
}

function CargarHtmlCabeceraObraSocialCliente(objDocumento) {
    var strHtml = '';
    strHtml += '<table style="width:100% !important; text-align:left;" border="0" cellspacing="0" cellpadding="0">';
    strHtml += '<tr>';
    strHtml += '<td>';
    strHtml += '<div style="float:left;padding:5px;">';
    strHtml += '<div class="thDocumentoEncabezado">Número:</div>' + '<div class="thDocumentoEncabezadoDetalle">' + getParameterByName('id') + '</div>';
    strHtml += '</div>';
    strHtml += '<div style="float:left;padding:5px;">';
    strHtml += '<div class="thDocumentoEncabezado">Fecha:</div>' + '<div class="thDocumentoEncabezadoDetalle">' + objDocumento.FechaToString + '</div>';
    strHtml += '</div>';
    strHtml += '<div style="float:left;padding:5px;">';

    var strMontoTotal = '&nbsp;';
    if (isNotNullEmpty(objDocumento.MontoTotal)) {
        strMontoTotal = '$&nbsp;' + FormatoDecimalConDivisorMiles(objDocumento.MontoTotal.toFixed(2));
    }
    strHtml += '<div class="thDocumentoEncabezado">Total:</div>' + '<div class="thDocumentoEncabezadoDetalle">' + strMontoTotal + '</div>';
    strHtml += '</div>';

    strHtml += '</td>';
    strHtml += '</tr>';

    strHtml += '<tr>';
    strHtml += '<td>';
    strHtml += '<div style="float:left;padding:5px;">';
    strHtml += '<div class="thDocumentoEncabezado">Nombre Plan:</div>' + '<div class="thDocumentoEncabezadoDetalle">' + objDocumento.NombrePlan + '</div>';
    strHtml += '</div>';
    strHtml += '<div style="float:left;padding:5px;">';
    strHtml += '<div class="thDocumentoEncabezado">Numero Planilla:</div>' + '<div class="thDocumentoEncabezadoDetalle">' + objDocumento.NumeroPlanilla + '</div>';
    strHtml += '</div>';
    strHtml += '<div style="float:left;padding:5px;">';
    strHtml += '<div class="thDocumentoEncabezado">Destinatario:</div>' + '<div class="thDocumentoEncabezadoDetalle">' + objDocumento.Destinatario + '</div>';
    strHtml += '</div>';
    strHtml += '</td>';
    strHtml += '</tr>';
    strHtml += '</table>';
    return strHtml;
}

function CargarHtmlObraSocialCliente() {
    if (objDocumento != null) {
        var strHtml = '';

        strHtml += CargarHtmlCabeceraObraSocialCliente(objDocumento);

        strHtml += '<table class="tbl-buscador-productos"  style="width:100% !important;" border="0" cellspacing="0" cellpadding="0">';
        strHtml += '<tr>';
        //Número:

        //    A001304274019
        strHtml += '<th width="26%"  class="bp-med-ancho">';
        strHtml += 'Descripcion';
        strHtml += '</th>';
        strHtml += '<th width="10%" class="bp-med-ancho">';
        strHtml += 'Importe';
        strHtml += '</th>';
        //strHtml += '<th width="10%" class="bp-med-ancho">';
        //strHtml += 'NumeroObraSocialCliente';
        //strHtml += '</th>';
        strHtml += '</tr>';
        var strDocumentoNumero = '';
        if (objDocumento.lista != null) {
            for (var i = 0; i < objDocumento.lista.length; i++) {
                    var strHtmlColorFondo = '';
                    if (i % 2 != 0) {
                        strHtmlColorFondo = ' bp-td-color';
                    }
                    strHtml += '<tr>';
                    strHtml += '<td class="' + strHtmlColorFondo + '" style="text-align:left; padding-left:5px;">';
                    strHtml += objDocumento.lista[i].Descripcion;
                    strHtml += '</td>';
                    strHtml += '<td class="' + strHtmlColorFondo + '">';
                    //var strImporte = '&nbsp;';
                    //if (isNotNullEmpty(objDocumento.lista[i].Importe)) {
                    //    strImporte = '$&nbsp;' + FormatoDecimalConDivisorMiles(objDocumento.lista[i].Importe.toFixed(2));
                    //}
                    strHtml += objDocumento.lista[i].Importe;
                    strHtml += '</td>';
                    //strHtml += '<td class="' + strHtmlColorFondo + '" >';
                    //strHtml += objDocumento.lista[i].NumeroObraSocialCliente;
                    //strHtml += '</td>';
                    
                    strHtml += '</tr>';
                    strDocumentoNumero = objDocumento.lista[i].NumeroObraSocialCliente;
                
            }
        }
        strHtml += '</table>';

        //
        var httpRaiz = $('#hiddenRaiz').val();
        strHtml += '<div class="cssDivDescarga" >';

        //strHtml += '<a href="../../archivos/Diseño TXT Factura.pdf" title="FORMATO" target="_blank">' + 'FORMATO TXT' + '</a>';
        //strHtml += '&nbsp;&nbsp;&nbsp;';
        //strHtml += '<a href="' + httpRaiz + 'servicios/generar_archivo.aspx?factura=' + objDocumento.Numero + '"  >' + '<img class="cssImagenDescarga" src="../../img/iconos/disk.png" alt="txt" title="Descarga txt" />' + '</a>';
        strHtml += '<a href="' + httpRaiz + 'servicios/generar_archivoPdf.aspx?tipo=' + objTipoDocumento + '&nro=' + strDocumentoNumero + '"  onclick="return funImprimirComprobantePdf(' + '\'' + strDocumentoNumero + '\'' + ');" >' + '<img  class="cssImagenDescarga"  id="imgPdf" src="../../img/iconos/PDF.png" alt="txt" title="Descarga pdf" height="34" width="32" />' + '</a>';
        ////begin Trazable
        //if (objDocumento.FacturaTrazable) {
        //    strHtml += '&nbsp;';
        //    strHtml += '<a href="' + httpRaiz + 'servicios/generar_archivoPdf.aspx?tipo=' + 'REM_ANEXTRAZ' + '&nro=' + objDocumento.Numero + '"  onclick="return funImprimirComprobantePdf_trz(' + '\'' + objDocumento.NumeroRemito + '\'' + ');" >' + '<img  class="cssImagenDescarga"  id="imgPdf_Trz" src="../../img/iconos/PDF_TRZ.png" alt="txt" title="Descarga pdf trazable" height="34" width="32" />' + '</a>';
        //}
        ////end Trazable
        strHtml += '</div>';
        strHtml += '</br>';
        //

        if (objDocumento.lista != null) {
            if (objDocumento.lista.length > cantFilaParaEnCabezado) {
                strHtml += CargarHtmlCabeceraPendienteDeFacturar(objDocumento);
                strHtml += '<input type="button" onclick="volver()" value="VOLVER" class="btn_gral" />';
            }
        }
        $('#divContenedorDocumento').html(strHtml);
    } else {
        $('#divContenedorDocumento').html(objMensajeNoEncontrado);
    }
}
