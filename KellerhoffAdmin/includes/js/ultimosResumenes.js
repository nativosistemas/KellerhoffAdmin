var listaUltimosResumenes = null;
var listaComprobantesAImprimirEnBaseAResumen = null;
var httpRaiz = null;
var isButtonDescargarTodos = false;
var contadorDescargarTodosPDF = 0;

jQuery(document).ready(function () {
    if (httpRaiz == null) {
        httpRaiz = $('#hiddenRaiz').val();
        if (typeof httpRaiz == 'undefined') {
            httpRaiz = null;
        }
    }

    if (listaUltimosResumenes == null) {
        listaUltimosResumenes = eval('(' + $('#hiddenListaUltimosResumenes').val() + ')');
        if (typeof listaUltimosResumenes == 'undefined') {
            listaUltimosResumenes = null;
        }
    }
    if (listaComprobantesAImprimirEnBaseAResumen == null) {
        listaComprobantesAImprimirEnBaseAResumen = eval('(' + $('#hiddenListaComprobantesAImprimirEnBaseAResumen').val() + ')');
        if (typeof listaComprobantesAImprimirEnBaseAResumen == 'undefined') {
            listaComprobantesAImprimirEnBaseAResumen = null;
        }
    }
    if (listaUltimosResumenes != null) {
        CargarUltimosResumenes();
    }
    if (listaComprobantesAImprimirEnBaseAResumen != null) {
        for (var i = 0; i < listaComprobantesAImprimirEnBaseAResumen.length; i++) {
            listaComprobantesAImprimirEnBaseAResumen[i].isArchivoGenerado = false;
            listaComprobantesAImprimirEnBaseAResumen[i].isLlamarArchivoPDF = true;
            listaComprobantesAImprimirEnBaseAResumen[i].isDescargarTodos = false;
            listaComprobantesAImprimirEnBaseAResumen[i].contadorPDF = 0;
        }

        CargarComprobantesAImprimirEnBaseAResumen();
    }

});
function CargarComprobantesAImprimirEnBaseAResumen() {

    if (listaComprobantesAImprimirEnBaseAResumen != null) {
        var strHtml = '';
        strHtml += '<input type="button" onclick="volver()" value="VOLVER" class="btn_gral" />';
        //strHtml += '<input type="button" onclick="funDescargarTodos()" value="DESCARGAR TODOS" class="btn_gral" />';
        //box_down.png

        strHtml += '<div style="text-align:left;font-size:12px; margin-bottom:5px;"><b>COMPROBANTES POR IMPRIMIR EN BASE A RESUMEN</b></div>';

        strHtml += '<div style="height:25px;">&nbsp;</div>';
        strHtml += '<table style="float: right;">';
        strHtml += '<tr>';
        strHtml += '<th style="vertical-align: bottom;cursor: pointer;"  onclick="funDescargarTodos(); return false;" >DESCARGAR TODOS:</th>';
        strHtml += '<th>';
        strHtml += '<a href="#"  onclick="funDescargarTodos(); return false;" style="cursor: pointer;float: right;">';
        strHtml += '<img  class="cssImagenDescarga"  src="../../img/iconos/box_down.png" alt="txt" title="DESCARGAR TODOS" height="32" width="32" style="height:32px !important;width:32px !important;" />';
        strHtml += '</a>';
        strHtml += '</th>';
        strHtml += '</tr>';
        strHtml += '</table>';

        if (listaComprobantesAImprimirEnBaseAResumen.length > 0) {
            //// boton volver
            //strHtml += '<input type="button" onclick="volver()" value="VOLVER" class="btn_gral" />';
            strHtml += '<div style="height:25px;">&nbsp;</div>';
            ////// fin boton volver
            strHtml += '<table class="tbl-buscador-productos"  style="width:100% !important;" border="0" cellspacing="0" cellpadding="0">';
            strHtml += '<tr>';
            strHtml += '<th >Tipo Comprobante</th>';
            strHtml += '<th >Número</th>';
            strHtml += '<th >Fecha</th>';
            strHtml += '<th >Descarga</th>';
            strHtml += '</tr>';


            for (var i = 0; i < listaComprobantesAImprimirEnBaseAResumen.length; i++) {
                strHtml += '<tr>';
                var strHtmlColorFondo = '';
                if (i % 2 != 0) {
                    strHtmlColorFondo = ' bp-td-color';
                }
                strHtml += '<td class="' + strHtmlColorFondo + '">' + listaComprobantesAImprimirEnBaseAResumen[i].TipoComprobante + '</td>';
                strHtml += '<td class="' + strHtmlColorFondo + '">' + listaComprobantesAImprimirEnBaseAResumen[i].NumeroComprobante + '</td>';
                strHtml += '<td class="' + strHtmlColorFondo + '">' + listaComprobantesAImprimirEnBaseAResumen[i].FechaComprobanteToString + '</td>';
                strHtml += '<td  class="' + strHtmlColorFondo + '">';

                //'<a href="descargaResumenesDetalle.aspx?id=' + listaUltimosResumenes[i].Numero + '" >' + 'Comprobantes' + '</a>';
                strHtml += '<a href="' + httpRaiz + 'servicios/generar_archivoPdf.aspx?tipo=' + listaComprobantesAImprimirEnBaseAResumen[i].TipoComprobante + '&nro=' + listaComprobantesAImprimirEnBaseAResumen[i].NumeroComprobante + '"  onclick="return funImprimirComprobantePdf_Resumenes(' + '\'' + i + '\'' + ');" >' + '<img  class="cssImagenDescarga"  id="imgPdf_' + i + '" src="../../img/iconos/PDF.png" alt="txt" title="Descarga pdf" height="34" width="32" style="height:34px !important;width:32px !important;" />' + '</a>';
                strHtml += '</td>';

                strHtml += '</tr>';
            }

        } else {
            strHtml = objMensajeNoEncontrado;
        }

        $('#divContenedorDocumento').html(strHtml);
    }


}


function funDescargarTodos() {
    if (!isButtonDescargarTodos) {
        isButtonDescargarTodos = true;
        for (var i = 0; i < listaComprobantesAImprimirEnBaseAResumen.length; i++) {
            $('#imgPdf_' + i).attr('src', '../../img/varios/ajax-loader.gif');
            listaComprobantesAImprimirEnBaseAResumen[i].isArchivoGenerado = false;
            listaComprobantesAImprimirEnBaseAResumen[i].isLlamarArchivoPDF = true;
            listaComprobantesAImprimirEnBaseAResumen[i].isDescargarTodos = false;
            listaComprobantesAImprimirEnBaseAResumen[i].contadorPDF = 0;
        }
        contadorDescargarTodosPDF = 0;
        //var nombreArchivoPDF = 'RCO' + '_' + listaComprobantesAImprimirEnBaseAResumen[0].NumeroComprobante;
        var nombreArchivoPDF = 'RCO' + '_' + getUrlParameter('id');
        setTimeout(function () { PageMethods.IsExistenciaComprobanteResumenes_todos(nombreArchivoPDF, contadorDescargarTodosPDF, OnCallBackIsExistenciaComprobanteResumenes_todos, OnFail); }, 10);
        return false;
    } else { return false; }

}
function OnCallBackIsExistenciaComprobanteResumenes_todos(args) {
    if (args.isOk) {
        for (var i = 0; i < listaComprobantesAImprimirEnBaseAResumen.length; i++) {
            listaComprobantesAImprimirEnBaseAResumen[i].isDescargarTodos = true;
            listaComprobantesAImprimirEnBaseAResumen[i].isArchivoGenerado = false;
            listaComprobantesAImprimirEnBaseAResumen[i].isLlamarArchivoPDF = true;
            $('#imgPdf_' + i).attr('src', '../../img/iconos/PDF.png');
        }
        isButtonDescargarTodos = false;
        var NumeroComprobante = getUrlParameter('id');
        window.open('../../servicios/generar_archivoPdf.aspx?tipo=' + 'RCO' + '&nro=' +NumeroComprobante, '_parent');
    } else {
        if (contadorDescargarTodosPDF <= 300) {
            setTimeout(function () { PageMethods.IsExistenciaComprobanteResumenes_todos(args.nombreArchivo, contadorDescargarTodosPDF, OnCallBackIsExistenciaComprobanteResumenes_todos, OnFail); }, 1000);
        } else {
            for (var i = 0; i < listaComprobantesAImprimirEnBaseAResumen.length; i++) {
                listaComprobantesAImprimirEnBaseAResumen[i].isDescargarTodos = true;
                listaComprobantesAImprimirEnBaseAResumen[i].isArchivoGenerado = false;
                listaComprobantesAImprimirEnBaseAResumen[i].isLlamarArchivoPDF = true;
                $('#imgPdf_' + i).attr('src', '../../img/iconos/PDF.png');
            }
            isButtonDescargarTodos = false;
            alert('No se pudo descargar el archivo, inténtelo nuevamente.');
        }
        contadorDescargarTodosPDF++
    }
}
//function funDescargarTodos() {
//    if (!isButtonDescargarTodos) {
//        isButtonDescargarTodos = true;
//        for (var i = 0; i < listaComprobantesAImprimirEnBaseAResumen.length; i++) {
//            $('#imgPdf_' + i).attr('src', '../../img/varios/ajax-loader.gif');
//            listaComprobantesAImprimirEnBaseAResumen[i].isArchivoGenerado = false;
//            listaComprobantesAImprimirEnBaseAResumen[i].isLlamarArchivoPDF = true;
//            listaComprobantesAImprimirEnBaseAResumen[i].isDescargarTodos = false;
//            listaComprobantesAImprimirEnBaseAResumen[i].contadorPDF = 0;
//        }
//        procesoDescargarTodos();
//    }
//}
//function procesoDescargarTodos() {
//    var listaNombre = [];
//    var listaIndex = [];
//    var isFinish = true;
//    for (var i = 0; i < listaComprobantesAImprimirEnBaseAResumen.length; i++) {
//        if (!listaComprobantesAImprimirEnBaseAResumen[i].isDescargarTodos) {
//            isFinish = false;
//            var nombreArchivoPDF = listaComprobantesAImprimirEnBaseAResumen[i].TipoComprobante + '_' + listaComprobantesAImprimirEnBaseAResumen[i].NumeroComprobante;
//            listaNombre.push(nombreArchivoPDF);
//            listaIndex.push(i);
//        }
//    }
//    if (isFinish) {
//        for (var i = 0; i < listaComprobantesAImprimirEnBaseAResumen.length; i++) {
//            listaComprobantesAImprimirEnBaseAResumen[i].isDescargarTodos = false;
//        }
//        isButtonDescargarTodos = false;
//    } else {
//        PageMethods.IsExistenciaComprobanteResumenes_DescargarTodos(listaNombre, listaIndex, OnCallBackIsExistenciaComprobanteResumenes_DescargarTodos, OnFail);
//    }
//}
//var listaDescargar = [];
//var indexDescargar = 0;
//function funDescargaDeArchivos() {
//    if (indexDescargar >= listaDescargar.length) {
//        setTimeout(function () { procesoDescargarTodos(); }, 100);
//    } else {

//        listaComprobantesAImprimirEnBaseAResumen[listaDescargar[indexDescargar]].isDescargarTodos = true;
//        listaComprobantesAImprimirEnBaseAResumen[listaDescargar[indexDescargar]].isArchivoGenerado = true;
//        listaComprobantesAImprimirEnBaseAResumen[listaDescargar[indexDescargar]].isLlamarArchivoPDF = true;
//        $('#imgPdf_' + listaDescargar[indexDescargar]).attr('src', '../../img/iconos/PDF.png');
//        window.open('../../servicios/generar_archivoPdf.aspx?tipo=' + listaComprobantesAImprimirEnBaseAResumen[listaDescargar[indexDescargar]].TipoComprobante + '&nro=' + listaComprobantesAImprimirEnBaseAResumen[listaDescargar[indexDescargar]].NumeroComprobante, '_parent');
//        indexDescargar++

//        setTimeout(function () { funDescargaDeArchivos() }, 300);
//    }
//}

//function OnCallBackIsExistenciaComprobanteResumenes_DescargarTodos(args) {
//    listaDescargar = [];
//    indexDescargar = 0;
//    for (var i = 0; i < args.length; i++) {
//        if (args[i].isOk) {
//            listaDescargar.push(args[i].index);
//        } else {
//            listaComprobantesAImprimirEnBaseAResumen[args[i].index].contadorPDF++;
//        }
//    }
//    contadorDescargarTodosPDF++;
//    setTimeout(function () { funDescargaDeArchivos(); }, 10);
//}
function funImprimirComprobantePdf_Resumenes(pValor) {
    if (!isButtonDescargarTodos) {
        if (listaComprobantesAImprimirEnBaseAResumen[pValor].isArchivoGenerado) {
            return true;
        } else {
            if (listaComprobantesAImprimirEnBaseAResumen[pValor].isLlamarArchivoPDF) {
                listaComprobantesAImprimirEnBaseAResumen[pValor].contadorPDF = 0;
                var nombreArchivoPDF = listaComprobantesAImprimirEnBaseAResumen[pValor].TipoComprobante + '_' + listaComprobantesAImprimirEnBaseAResumen[pValor].NumeroComprobante;
                $('#imgPdf_' + pValor).attr('src', '../../img/varios/ajax-loader.gif');
                listaComprobantesAImprimirEnBaseAResumen[pValor].isLlamarArchivoPDF = false;
                setTimeout(function () { PageMethods.IsExistenciaComprobanteResumenes(nombreArchivoPDF, pValor, listaComprobantesAImprimirEnBaseAResumen[pValor].contadorPDF, OnCallBackIsExistenciaComprobanteResumenes, OnFail); }, 10);
            }
            return false;
        }
    } else { return false; }
}
function OnCallBackIsExistenciaComprobanteResumenes(args) {
    if (args.isOk) {
        listaComprobantesAImprimirEnBaseAResumen[args.index].isArchivoGenerado = true;
        listaComprobantesAImprimirEnBaseAResumen[args.index].isLlamarArchivoPDF = true;
        $('#imgPdf_' + args.index).attr('src', '../../img/iconos/PDF.png');
        window.open('../../servicios/generar_archivoPdf.aspx?tipo=' + listaComprobantesAImprimirEnBaseAResumen[args.index].TipoComprobante + '&nro=' + listaComprobantesAImprimirEnBaseAResumen[args.index].NumeroComprobante, '_parent');
    } else {
        if (listaComprobantesAImprimirEnBaseAResumen[args.index].contadorPDF <= 300) {
            //var nombreArchivoPDF = objTipoDocumento + '_' + nroDocumento;
            setTimeout(function () { PageMethods.IsExistenciaComprobanteResumenes(args.nombreArchivo, args.index, listaComprobantesAImprimirEnBaseAResumen[args.index].contadorPDF, OnCallBackIsExistenciaComprobanteResumenes, OnFail); }, 1000);
        } else {
            listaComprobantesAImprimirEnBaseAResumen[args.index].isLlamarArchivoPDF = true;
            $('#imgPdf_' + args.index).attr('src', '../../img/iconos/PDF.png');
            alert('No se pudo descargar el archivo, inténtelo nuevamente.');
        }
        listaComprobantesAImprimirEnBaseAResumen[args.index].contadorPDF++;
    }
}
function CargarUltimosResumenes() {
    if (listaUltimosResumenes != null) {
        var strHtml = '';
        strHtml += '<div style="text-align:left;font-size:12px; margin-bottom:5px;"><b>ÚLTIMOS RESÚMENES</b></div>';


        if (listaUltimosResumenes.length > 0) {
            // boton volver
            //strHtml += '<input type="button" onclick="volverComposicionSaldo()" value="VOLVER" class="btn_gral" />';
            strHtml += '<div style="height:25px;">&nbsp;</div>';
            //// fin boton volver
            strHtml += '<table class="tbl-buscador-productos"  style="width:100% !important;" border="0" cellspacing="0" cellpadding="0">';
            strHtml += '<tr>';
            strHtml += '<th class="bp-med-ancho"><div>Número</div></th>'; // class="bp-top-left"
            strHtml += '<th class="bp-med-ancho">Fecha</th>';
            strHtml += '<th class="bp-med-ancho">Semana</th>';
            strHtml += '<th class="bp-med-ancho">Monto</th>';
            strHtml += '<th class="bp-med-ancho">Link Descarga</th>';
            strHtml += '</tr>';


            for (var i = 0; i < listaUltimosResumenes.length; i++) {
                strHtml += '<tr>';
                var strHtmlColorFondo = '';
                if (i % 2 != 0) {
                    strHtmlColorFondo = ' bp-td-color';
                }
                strHtml += '<td class="' + strHtmlColorFondo + '">' + listaUltimosResumenes[i].Numero + '</td>';
                strHtml += '<td class="' + strHtmlColorFondo + '">' + listaUltimosResumenes[i].PeriodoDesdeToString + ' / ' + listaUltimosResumenes[i].PeriodoHastaToString + '</td>';
                strHtml += '<td class="' + strHtmlColorFondo + '">' + listaUltimosResumenes[i].NumeroSemana + '</td>';
                var strImporte = '&nbsp;';
                if (isNotNullEmpty(listaUltimosResumenes[i].TotalResumen)) {
                    strImporte = '$&nbsp;' + FormatoDecimalConDivisorMiles(listaUltimosResumenes[i].TotalResumen.toFixed(2));
                }
                strHtml += '<td style="text-align:right !important; white-space:nowrap;" class="' + strHtmlColorFondo + '">' + strImporte + '</td>';
                strHtml += '<td  class="' + strHtmlColorFondo + '">' + '<a href="descargaResumenesDetalle.aspx?id=' + listaUltimosResumenes[i].Numero + '" >';

                strHtml += '<img  class="cssImagenDescarga"  src="../../img/iconos/bullet_go.png" alt="txt" title="Ir" height="32" width="32" style="height:32px !important;width:32px !important;" />';
                strHtml += '</a>' + '</td>';

                strHtml += '</tr>';
            }

            strHtml += '</table>';

        } else {
            strHtml = objMensajeNoEncontrado;
        }


        $('#divContenedorDocumento').html(strHtml);
    }
}


