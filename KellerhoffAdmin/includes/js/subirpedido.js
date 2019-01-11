var listaArchivoSubidos = null;


jQuery(document).ready(function () {

    if (listaArchivoSubidos == null) {
        listaArchivoSubidos = eval('(' + $('#hiddenListaArchivoSubidos').val() + ')');
        if (typeof listaArchivoSubidos == 'undefined') {
            listaArchivoSubidos = null;
        }
    }
    CargarHtmlHistorialArchivos();
    //    EstablecerVariableTipoSucursal();
});



//function EstablecerVariableTipoSucursal() {
//    var elementos = document.getElementsByName('RadioTipoSucursal');
//    var sucursal = '';
//    for (var i = 0; i < elementos.length; i++) {
//        if (elementos[i].checked) {
//            sucursal = elementos[i].value;
//            break;
//        }
//    }
//    $('#HiddenFieldSucursalEleginda').val(sucursal);
// }

function CargarHtml_ElejirSucursal() {
    var elementos = document.getElementsByName('RadioTipoSucursal');
    var sucursal = '';
    for (var i = 0; i < elementos.length; i++) {
        if (elementos[i].checked) {
            sucursal = elementos[i].value;
            break;
        }
    }
    $('#HiddenFieldSucursalEleginda').val(sucursal);
    //    alert(sucursal);
}

function CargarHtmlHistorialArchivos() {
    var strHtml = '';
    if (listaArchivoSubidos != null) {
        if (listaArchivoSubidos.length > 0) {
            var strHtmlUltimoPedido = '';
            strHtmlUltimoPedido += '<table style="font-size:11px;padding-top:10px;" >'; //color:#000;
            strHtmlUltimoPedido += '<tr>';
            strHtmlUltimoPedido += '<td>' + 'Ultimo pedido realizado:' + '</td>';
            //            strHtmlUltimoPedido += '<td >' + '<a href="../../archivos/ArchivosPedidos/' + listaArchivoSubidos[0].has_NombreArchivo + '" TARGET="_blank">' + listaArchivoSubidos[0].has_NombreArchivoOriginal + '</a>' + '</td>';
            strHtmlUltimoPedido += '<td onclick="onclickCargar(' + listaArchivoSubidos[0].has_id + ')" style="color:#065BAB;cursor:pointer;" >' + listaArchivoSubidos[0].has_NombreArchivoOriginal + '</td>';
            var strNombreSucursal0 = '';
            if (listaArchivoSubidos[0].suc_nombre != null) {
                strNombreSucursal0 = listaArchivoSubidos[0].suc_nombre;
            }
            strHtmlUltimoPedido += '<td style="width:200px;">' + strNombreSucursal0 + '</td>';
            strHtmlUltimoPedido += '<td style="width:200px;">' + listaArchivoSubidos[0].has_fechaToString + '</td>';
            strHtmlUltimoPedido += '</tr>';
            strHtmlUltimoPedido += '</table>';
            $('#ultimoPedido').html(strHtmlUltimoPedido);

            // boton volver
            //            strHtml += '<input type="button" onclick="volverComposicionSaldo()" value="VOLVER" class="btn_gral" />';
            //            strHtml += '<div style="height:25px;">&nbsp;</div>';
            // fin boton volver
            if (listaArchivoSubidos.length > 1) {
                strHtml += '<div style="font-size:16px;">' + 'Historial' + '</div>';
                strHtml += '<table class="tbl-ComposicionSaldo" border="0" cellspacing="0" cellpadding="0"  width="100%">';
                strHtml += '<tr>';
                strHtml += '<th  width="40%" ><div class="bp-top-left">Archivo</div></th>'; //<th class="bp-off-top-left bp-med-ancho">
                strHtml += '<th  width="20%"  class="bp-med-ancho" >Sucursal</th>';
                strHtml += '<th  width="20%"  class="bp-med-ancho" >Fecha</th>';
                strHtml += '</tr>';

                for (var i = 1; i < listaArchivoSubidos.length; i++) {
                    var strHtmlColorFondo = '';
                    if (i % 2 != 0) {
                        strHtmlColorFondo = ' bp-td-color';
                    }
                    strHtml += '<tr>';
                    //                    strHtml += '<td class="' + strHtmlColorFondo + '" onclick="onclickCargar()">' + '<a href="../../archivos/ArchivosPedidos/' + listaArchivoSubidos[i].has_NombreArchivo + '" TARGET="_blank">' + listaArchivoSubidos[i].has_NombreArchivoOriginal + '</a>' + '</td>';
                    strHtml += '<td class="' + strHtmlColorFondo + '" onclick="onclickCargar(' + listaArchivoSubidos[i].has_id + ')" style="color:#065BAB;cursor:pointer;">' + listaArchivoSubidos[i].has_NombreArchivoOriginal + '</td>';
                    var strNombreSucursal = '';
                    if (listaArchivoSubidos[i].suc_nombre != null) {
                        strNombreSucursal = listaArchivoSubidos[i].suc_nombre;
                    }
                    strHtml += '<td class="' + strHtmlColorFondo + '">' + strNombreSucursal + '</td>';
                    strHtml += '<td class="' + strHtmlColorFondo + '">' + listaArchivoSubidos[i].has_fechaToString + '</td>';
                    strHtml += '</tr>';
                }
                strHtml += '</table>';
            } // fin 
        }

    }
    $('#divTablaHistorial').html(strHtml);
}

function onclickCargar(pValor) {
    var isCargarDeNuevo = confirm('¿Desea subir el archivo de nuevo?');
    if (isCargarDeNuevo) {
        //alert(pValor);
        PageMethods.CargarArchivoPedidoDeNuevo(pValor, OnCallBackCargarArchivoPedidoDeNuevo, OnFailBackCargarArchivoPedidoDeNuevo);
        $('#divCargandoContenedorGeneralFondo').css('display', 'block');
        var arraySizeDocumento = SizeDocumento();
        document.getElementById('divCargandoContenedorGeneralFondo').style.height = arraySizeDocumento[1] + 'px';
    }
}
function OnCallBackCargarArchivoPedidoDeNuevo(args) {
    if (args) {
        location.href = 'PedidosBuscador.aspx';
    } else {
        $('#divCargandoContenedorGeneralFondo').css('display', 'none');
        alert('No se pudo subir archivo');
    }
}
function OnFailBackCargarArchivoPedidoDeNuevo(ex) {
    OnFail(ex);
    $('#divCargandoContenedorGeneralFondo').css('display', 'none');
}