var listaTransfer = null;
var listaCarritoTransfer = null;
var indexTransferSeleccionado = -1;
var indiceCarritoTransferBorrar = null;
var listaCarritoTransferPorSucursal = null;
var indexSucursalTransferSeleccionado = null;
var productoSeleccionado = '';
var listaResultadoPedidoTransfer = null;
var idTipoEnvioPedidoTransfer = null;
var textFacturaPedidoTransfer = null;
var textRemitoPedidoTransfer = null;
var codSucursalPedidoTransfer = null;

function onclickMostrarUnTransferDeVarios(pValor) {
    document.getElementById('divTransferContenedorGeneralHtml').innerHTML = AgregarTransferHtmlAlPopUp(pValor);
}
function onmouseoutElijaTransfer(pValor) {
    pValor.style.color = "#444";
}
function onmouseoverElijaTransfer(pValor) {
    pValor.style.color = "Green";
}
function OnCallBackRecuperarTransfer(args) {
    listaTransfer = args;
    if (listaTransfer != null) {
        if (listaTransfer.length > 0) {
            var strHtmlTransfer = '';
            if (listaTransfer.length == 1) {
                strHtmlTransfer += AgregarTransferHtmlAlPopUp(0);
            } else {
                strHtmlTransfer += '<div style="font-size:16px; margin-top: 10px;"  >' + 'Seleccione un transfer:' + '</div>';//Elija un transfer:
                for (var i = 0; i < listaTransfer.length; i++) {
                    strHtmlTransfer += '<div class="transferComboLista" style="font-size:14px; margin-top: 50px; cursor:pointer;" onmouseover="onmouseoverElijaTransfer(this)" onmouseout="onmouseoutElijaTransfer(this)" onclick="onclickMostrarUnTransferDeVarios(' + i + ')">' + listaTransfer[i].tfr_nombre + '</div>';
                }
            }
            document.getElementById('divTransferContenedorGeneralHtml').innerHTML = strHtmlTransfer;
            document.getElementById('divTransferContenedorGeneral').style.display = 'block';
            document.getElementById('divTransferContenedorGeneralFondo').style.display = 'block';
            var arraySizeDocumento = SizeDocumento();
            document.getElementById('divTransferContenedorGeneralFondo').style.height = arraySizeDocumento[1] + 'px';
        }
    }
    //    if (listaTransfer.length > 0) {
    //        var strHtmlTransfer = '';
    //        for (var i = 0; i < listaTransfer.length; i++) {
    //            strHtmlTransfer += AgregarTransferHtmlAlPopUp(i);
    //        }
    //        document.getElementById('divTransferContenedorGeneralHtml').innerHTML = strHtmlTransfer;

    //        document.getElementById('divTransferContenedorGeneral').style.display = 'block';
    //        document.getElementById('divTransferContenedorGeneralFondo').style.display = 'block';
    //        var arraySizeDocumento = SizeDocumento();
    //        document.getElementById('divTransferContenedorGeneralFondo').style.height = arraySizeDocumento[1] + 'px';
    //    }
}

function AgregarTransferHtmlAlPopUp(pIndex) {

    var strHtmlTransfer = '';
    var strHtmlRedondo = '';
    if (pIndex == 0) {
        strHtmlRedondo = ' border-radius: 10px 0px 0px 0px;';
    }
    strHtmlTransfer += '<div style="background:#414141;color:#FFF;font-size:14px; padding-top:10px;padding-left:10px;text-align: left;' + strHtmlRedondo + '">'; // width: 550px;
    strHtmlTransfer += '<b>' + listaTransfer[pIndex].tfr_nombre + '</b>';
    strHtmlTransfer += '</div>';
    if (listaTransfer[pIndex].tfr_descripcion != null) {
        strHtmlTransfer += '<div style="font-size:11px; margin-top: 10px;margin-left: 10px;text-align: left;">'; //width: 100%;
        strHtmlTransfer += '<b>DESCRIPCION Y CONDICION: </b>' + listaTransfer[pIndex].tfr_descripcion;
        strHtmlTransfer += '</div>';
    }

    ///
    strHtmlTransfer += '<div style="margin-top: 2px;margin-left: 9px;">';
    strHtmlTransfer += '<table border="0">';
    strHtmlTransfer += '<tr>';

    var tituloTrfMinimoRenglon = 'Mínima Renglones:';
    var valorTrfMinimoRenglon = '-';
    if (listaTransfer[pIndex].tfr_minrenglones != null) {
        tituloTrfMinimoRenglon = '<b>' + 'Mínima Renglones:' + '</b>';
        valorTrfMinimoRenglon = '<b>' + listaTransfer[pIndex].tfr_minrenglones + '</b>';
    }
    strHtmlTransfer += '<td valign="middle" align="left">';
    strHtmlTransfer += tituloTrfMinimoRenglon;
    strHtmlTransfer += '</td>';
    strHtmlTransfer += '<td valign="middle" align="left">';
    strHtmlTransfer += valorTrfMinimoRenglon;
    strHtmlTransfer += '</td>';

    strHtmlTransfer += '<td  valign="middle" align="center">';

    strHtmlTransfer += '</td>';

    var tituloTrfUnidadMinima = 'Unidad Mínima:';
    var valorTrfUnidadMinima = '-';
    if (listaTransfer[pIndex].tfr_minunidades != null) {
        tituloTrfUnidadMinima = '<b>' + 'Unidad Mínima:' + '</b>';
        valorTrfUnidadMinima = '<b>' + listaTransfer[pIndex].tfr_minunidades + '</b>';
    }
    strHtmlTransfer += '<td valign="middle" align="left">';
    strHtmlTransfer += tituloTrfUnidadMinima;
    strHtmlTransfer += '</td>';
    strHtmlTransfer += '<td valign="middle" align="left">';
    strHtmlTransfer += valorTrfUnidadMinima;
    strHtmlTransfer += '</td>';

    strHtmlTransfer += '<td valign="middle" align="center">';

    strHtmlTransfer += '</td>';

    var tituloTrfUnidadMaxima = 'Unidad Máxima:';
    var valorTrfUnidadMaxima = '-';
    if (listaTransfer[pIndex].tfr_maxunidades != null) {
        tituloTrfUnidadMaxima = '<b>' + 'Unidad Máxima:' + '</b>';
        valorTrfUnidadMaxima = '<b>' + listaTransfer[pIndex].tfr_maxunidades + '</b>';
    }
    strHtmlTransfer += '<td valign="middle" align="left">';
    strHtmlTransfer += tituloTrfUnidadMaxima;
    strHtmlTransfer += '</td>';
    strHtmlTransfer += '<td valign="middle" align="left">';
    strHtmlTransfer += valorTrfUnidadMaxima;
    strHtmlTransfer += '</td>';

    strHtmlTransfer += '<td valign="middle" align="center">';

    strHtmlTransfer += '</td>';

    var tituloTrfMultiploUnidades = 'Múltiplo unidades:';
    var valorTrfMultiploUnidades = '-';
    if (listaTransfer[pIndex].tfr_mulunidades != null) {
        tituloTrfMultiploUnidades = '<b>' + 'Múltiplo unidades:' + '</b>';
        valorTrfMultiploUnidades = '<b>' + listaTransfer[pIndex].tfr_mulunidades + '</b>';
    }
    strHtmlTransfer += '<td valign="middle" align="left">';
    strHtmlTransfer += tituloTrfMultiploUnidades;
    strHtmlTransfer += '</td>';
    strHtmlTransfer += '<td valign="middle" align="left">';
    strHtmlTransfer += valorTrfMultiploUnidades;
    strHtmlTransfer += '</td>';

    strHtmlTransfer += '<td valign="middle" align="center">';

    strHtmlTransfer += '</td>';

    var tituloTrfUnidadesFijas = 'Unidades Fijas:';
    var valorTrfUnidadesFijas = '-';
    if (listaTransfer[pIndex].tfr_fijunidades != null) {
        tituloTrfUnidadesFijas = '<b>' + 'Unidades Fijas:' + '</b>';
        valorTrfUnidadesFijas = '<b>' + listaTransfer[pIndex].tfr_fijunidades + '</b>';
    }
    strHtmlTransfer += '<td valign="middle" align="left">';
    strHtmlTransfer += tituloTrfUnidadesFijas;
    strHtmlTransfer += '</td>';
    strHtmlTransfer += '<td valign="middle" align="left">';
    strHtmlTransfer += valorTrfUnidadesFijas;
    strHtmlTransfer += '</td>';
    strHtmlTransfer += '</tr>';

    strHtmlTransfer += '</table></div>';

    strHtmlTransfer += '<hr>'
    ///
    strHtmlTransfer += '<div style="padding-left:10px;width:100%;text-align: left;">';

    for (var y = 0; y < listaTransfer[pIndex].listaDetalle.length; y++) {
        var cssDivContenedorProducto = '';
        if (listaTransfer[pIndex].tfr_mospap) {// == 1
            if (productoSeleccionado == listaTransfer[pIndex].listaDetalle[y].tde_codpro) {
                cssDivContenedorProducto = '  class="cssMospapProductoVisible' + pIndex + '" ';
            } else {
                cssDivContenedorProducto = ' style="display:none;" class="cssMospapProductoOculto' + pIndex + '" ';
            }
        }
        strHtmlTransfer += '<div ' + cssDivContenedorProducto + ' style="margin-right: 7px;" >'; // contenedor general
        strHtmlTransfer += '<div style="margin-top:5px; color:#666666; margin-top:10px; font-size:12px;text-align: left;width:100%;">'; //padding-left:10px;padding-top:5px;
        strHtmlTransfer += '<div class="clear"></div>';
        strHtmlTransfer += '<b style="color:#FFF;background-color:#5CA8D9;  border-bottom:#999 1px solid;display: block;padding:2px;">&nbsp;&nbsp;&raquo; ' + listaTransfer[pIndex].listaDetalle[y].tde_codpro + '</b>';
        strHtmlTransfer += '</div>';

        // Nuevo
        strHtmlTransfer += '<div style="font-size:11px; padding:3px;text-align: left;">';
        if (listaTransfer[pIndex].listaDetalle[y].tde_descripcion != null) {
            strHtmlTransfer += '<b>Descripción: </b>&nbsp;' + listaTransfer[pIndex].listaDetalle[y].tde_descripcion;
            strHtmlTransfer += '<br/>';
        }
        strHtmlTransfer += '<b>Precio Público: </b>&nbsp;$&nbsp;' + FormatoDecimalConDivisorMiles(listaTransfer[pIndex].listaDetalle[y].tde_prepublico);

        strHtmlTransfer += '&nbsp;&nbsp;&nbsp;' + '<b>Precio con descuento: </b>&nbsp;$&nbsp;' + FormatoDecimalConDivisorMiles(listaTransfer[pIndex].listaDetalle[y].PrecioFinalTransfer.toFixed(2));

        strHtmlTransfer += '</div>';
        // Fin Nuevo

        strHtmlTransfer += '<div style="float:left">';
        strHtmlTransfer += '<table border="0">';

        strHtmlTransfer += '<tr>';
        // espacio entre celdas
        var strEspacioEntreCelda = '&nbsp;&nbsp;&nbsp;';


        var tituloDetalleUnidadMinima = 'Unidad Mínima:';
        var valorDetalleUnidadMinima = '-';
        if (listaTransfer[pIndex].listaDetalle[y].tde_minuni != null) {
            tituloDetalleUnidadMinima = '<b>' + 'Unidad Mínima: ' + '</b>';
            valorDetalleUnidadMinima = '<b>' + listaTransfer[pIndex].listaDetalle[y].tde_minuni + '</b>';
        }
        strHtmlTransfer += '<td valign="middle" align="left">';
        strHtmlTransfer += tituloDetalleUnidadMinima;
        strHtmlTransfer += '</td>';
        strHtmlTransfer += '<td valign="middle" align="left">';
        strHtmlTransfer += valorDetalleUnidadMinima;
        strHtmlTransfer += '</td>';
        //strHtmlTransfer += '<tr><td valign="middle" align="center">';
        //strHtmlTransfer += '</td></tr>';
        var tituloDetalleUnidadMaxima = 'Unidad Máxima:';
        var valorDetalleUnidadMaxima = '-';
        if (listaTransfer[pIndex].listaDetalle[y].tde_maxuni != null) {
            tituloDetalleUnidadMaxima = '<b>' + 'Unidad Máxima:' + '</b>';
            valorDetalleUnidadMaxima = '<b>' + listaTransfer[pIndex].listaDetalle[y].tde_maxuni + '</b>';
        }
        strHtmlTransfer += '<td valign="middle" align="left">';
        strHtmlTransfer += strEspacioEntreCelda + tituloDetalleUnidadMaxima;
        strHtmlTransfer += '</td>';
        strHtmlTransfer += '<td valign="middle" align="left">';
        strHtmlTransfer += valorDetalleUnidadMaxima;
        strHtmlTransfer += '</td>';
        //strHtmlTransfer += '<tr><td valign="middle" align="center">';
        //strHtmlTransfer += '</td></tr>';
        var tituloDetalleMultiploUnidades = 'Múltiplo Unidades:';
        var valorDetalleMultiploUnidades = '-';
        if (listaTransfer[pIndex].listaDetalle[y].tde_muluni != null) {
            tituloDetalleMultiploUnidades = '<b>' + 'Múltiplo Unidades:' + '</b>';
            valorDetalleMultiploUnidades = '<b>' + listaTransfer[pIndex].listaDetalle[y].tde_muluni + '</b>';
        }
        strHtmlTransfer += '<td valign="middle" align="left">';
        strHtmlTransfer += strEspacioEntreCelda + tituloDetalleMultiploUnidades;
        strHtmlTransfer += '</td>';
        strHtmlTransfer += '<td valign="middle" align="left">';
        strHtmlTransfer += valorDetalleMultiploUnidades;
        strHtmlTransfer += '</td>'; //</tr>
        var tituloDetalleUnidadesFijas = 'Unidades Fijas:';
        var valorDetalleUnidadesFijas = '-';
        if (listaTransfer[pIndex].listaDetalle[y].tde_fijuni != null) {
            tituloDetalleUnidadesFijas = '<b>' + 'Unidades Fijas:' + '</b>';
            valorDetalleUnidadesFijas = '<b>' + listaTransfer[pIndex].listaDetalle[y].tde_fijuni + '</b>';
        }
        //<tr>
        strHtmlTransfer += '<td valign="middle" align="left">';
        strHtmlTransfer += strEspacioEntreCelda + tituloDetalleUnidadesFijas;
        strHtmlTransfer += '</td>';
        strHtmlTransfer += '<td valign="middle" align="left">';
        strHtmlTransfer += valorDetalleUnidadesFijas;
        strHtmlTransfer += '</td>';
        // strHtmlTransfer += '<tr><td valign="middle" align="center">';
        //strHtmlTransfer += '</td></tr>';
        var valorDefaultCantidad = '';
        var tituloDetalleObligatorio = 'Obligatorio:';
        var valorDetalleObligatorio = '-';
        if (listaTransfer[pIndex].listaDetalle[y].tde_proobligatorio) {
            tituloDetalleObligatorio = '<b>' + 'Obligatorio:' + '</b>';
            valorDetalleObligatorio = '<b>' + 'Si' + '</b>';
            // Valor default
            if (listaTransfer[pIndex].listaDetalle[y].tde_minuni != null) {
                valorDefaultCantidad = listaTransfer[pIndex].listaDetalle[y].tde_minuni;
            } else if (listaTransfer[pIndex].listaDetalle[y].tde_muluni != null) {
                valorDefaultCantidad = listaTransfer[pIndex].listaDetalle[y].tde_muluni;
            }
        }
        else {
            valorDetalleObligatorio = 'No';
        }
        strHtmlTransfer += '<td valign="middle" align="left">';
        strHtmlTransfer += strEspacioEntreCelda + tituloDetalleObligatorio;
        strHtmlTransfer += '</td>';
        strHtmlTransfer += '<td valign="middle" align="left">';
        strHtmlTransfer += valorDetalleObligatorio; // listaTransfer[pIndex].listaDetalle[y].tde_proobligatorio;
        strHtmlTransfer += '</td>';
        strHtmlTransfer += '</tr>';
        strHtmlTransfer += '</table>';
        strHtmlTransfer += '</div>';

        strHtmlTransfer += '<div style="clear:both;" ></div>'; // Limpiar

        strHtmlTransfer += '<div >'; //style="float:left"

        strHtmlTransfer += '<table width="100%">';
        strHtmlTransfer += '<tr>';
        strHtmlTransfer += '<td valign="middle" style="color:#1F84C5; " width="60" >';
        strHtmlTransfer += '<b style=" margin-top: 7px;display:block;">CANTIDAD:</b>';
        strHtmlTransfer += '</td>';
        strHtmlTransfer += '<td>';


        strHtmlTransfer += '<table  border="0" width="100%"> '; // Cantidad
        strHtmlTransfer += '<tr>'; // Cantidad
        strHtmlTransfer += '<td ALIGN="LEFT" >';
        strHtmlTransfer += '<input id="txtProdTransf' + pIndex + '_' + y + '" type="text" onblur="onblurCantProductosTransfer(this)"  onfocus="onfocusInputTransfer(this)" onkeypress="return onKeypressCantProductos(event)" value="' + valorDefaultCantidad + '" class="text_gral" style="width:30px;" ></input>';
        strHtmlTransfer += '</td>';

        strHtmlTransfer += '<td ALIGN="RIGHT" >';
        strHtmlTransfer += '<table  border="0"> ';
        strHtmlTransfer += '<tr>';

        if (listaSucursalesDependienteInfo != null) {
            for (var iSucursalNombre = 0; iSucursalNombre < listaSucursalesDependienteInfo.length; iSucursalNombre++) {
                for (var iSucursal = 0; iSucursal < listaTransfer[pIndex].listaDetalle[y].listaSucursalStocks.length; iSucursal++) {
                    if (listaSucursalesDependienteInfo[iSucursalNombre].sde_sucursal == listaTransfer[pIndex].listaDetalle[y].listaSucursalStocks[iSucursal].stk_codsuc) {
                        // 25/02/2018
                        var strOcultar = false;
                        if (listaTransfer[pIndex].tfr_nombre == 'TRANSFER PAÑALES PAMI') {
                            if ((sucursalCliente == 'CO' || sucursalCliente == 'CD' || sucursalCliente == 'SF' || sucursalCliente == 'CB')
                                && listaSucursalesDependienteInfo[iSucursalNombre].sde_sucursal == 'CC') {
                                strOcultar = true;
                            }
                        }
                        // fin: 25/02/2018
                        if (!strOcultar) {
                            strHtmlTransfer += '<td>';
                            if (sucursalCliente != 'CC') {
                                strHtmlTransfer += '<b>' + ConvertirSucursalParaColumno(listaSucursalesDependienteInfo[iSucursalNombre].sde_sucursal) + '</b>';
                            }
                            strHtmlTransfer += '<div class="estado-' + listaTransfer[pIndex].listaDetalle[y].listaSucursalStocks[iSucursal].stk_stock.toLowerCase() + '"></div>';
                            strHtmlTransfer += '</td>';

                        }
                        break;
                    }
                }
            }
        }
        strHtmlTransfer += '</td>';
        strHtmlTransfer += '</tr>';
        strHtmlTransfer += '</table>';
        //        strHtmlTransfer += '<td valign="middle">';
        //        strHtmlTransfer += '<input id="txtProdTransf' + pIndex + '_' + y + '" type="text" onblur="onblurCantProductosTransfer(this)" onkeypress="return onKeypressCantProductos(event)" value="' + valorDefaultCantidad + '" class="text_gral" style="width:30px;" ></input>';
        //        strHtmlTransfer += '</td>';
        strHtmlTransfer += '</td>'; // fin '<td ALIGN="RIGHT" >';


        strHtmlTransfer += '</tr>'; // fin Cantidad
        strHtmlTransfer += '</table>'; // fin Cantidad

        // fin

        strHtmlTransfer += '</tr>';

        strHtmlTransfer += '<tr>';

        strHtmlTransfer += '<td >';
        strHtmlTransfer += '</td>';
        strHtmlTransfer += '<td id="tdError' + pIndex + '_' + y + '" style="color:red;" >'; // Mensaje Error
        strHtmlTransfer += '</td>';

        strHtmlTransfer += '</tr>';

        strHtmlTransfer += '</table>';
        strHtmlTransfer += '</div>';

        strHtmlTransfer += '</div>';  // fin contenedor general
    }
    strHtmlTransfer += '<div class="clear"></div>';

    if (listaTransfer[pIndex].tfr_mospap == 1) {//class="carro-btn-confirmarTransfer"
        strHtmlTransfer += ' <a style="margin-left:5px;" id="btnVerTransferCompleto' + pIndex + '" onclick="onClickVerTransferCompleto(' + pIndex + ')" href="#">Ver transfer completo</a>';
    }
    strHtmlTransfer += '<div class="clear"></div>';
    // botones confirmar
    strHtmlTransfer += '<table  border="0" style="float: right;"> ';
    strHtmlTransfer += '<tr>';
    for (var iSucursalNombre = 0; iSucursalNombre < listaSucursalesDependienteInfo.length; iSucursalNombre++) {
        // 25/02/2018
        var strOcultar = false;
        if (listaTransfer[pIndex].tfr_nombre == 'TRANSFER PAÑALES PAMI') {
            if ((sucursalCliente == 'CO' || sucursalCliente == 'CD' || sucursalCliente == 'SF' || sucursalCliente == 'CB')
                && listaSucursalesDependienteInfo[iSucursalNombre].sde_sucursal == 'CC') {
                strOcultar = true;
            }
        }
        // fin: 25/02/2018
        if (!strOcultar) {
            strHtmlTransfer += '<td align="center">';
            if (sucursalCliente != 'CC') {
                strHtmlTransfer += '<b>' + ConvertirSucursalParaColumno(listaSucursalesDependienteInfo[iSucursalNombre].sde_sucursal) + '</b><br/>';
            }
            strHtmlTransfer += ' <a class="carro-btn-confirmarTransfer" onclick="onClickTransfer(' + pIndex + ',' + iSucursalNombre + ')" href="#">Confirmar</a>';
            strHtmlTransfer += '</td>';
        }
    }
    strHtmlTransfer += '</tr>';
    strHtmlTransfer += '</table>';
    // fin botones confirmar
    strHtmlTransfer += '</div>';
    return strHtmlTransfer;
}
function onClickVerTransferCompleto(pIndex) {
    if ($('#btnVerTransferCompleto' + pIndex)[0].innerText == 'Ver transfer completo') {
        $('.cssMospapProductoOculto' + pIndex).css('display', 'block');
        $('#btnVerTransferCompleto' + pIndex)[0].innerText = 'Ocultar productos detalles';
    }
    else {
        $('.cssMospapProductoOculto' + pIndex).css('display', 'none');
        $('#btnVerTransferCompleto' + pIndex)[0].innerText = 'Ver transfer completo';
    }
}

//function onblurCantProductosTransfer(pValor, pIndiceSucursal) {
//    if (pValor.value != '') {
//        var nombre = pValor.id;
//        nombre = nombre.replace('txtProdTransf', '');
//        var palabrasBase = nombre.split("_");
//        var indiceTransfer = parseInt(palabrasBase[0]);
//        var indiceProductoTransfer = parseInt(palabrasBase[1]);
//        ValidarTransferPorProducto_sucursal(indiceTransfer, indiceProductoTransfer, pIndiceSucursal);
//    }
//}

//onfocus = "onfocusInputCarrito(this)"
function onfocusInputTransfer(pValor) {
    DesmarcarFilaSeleccionada();
    selectInputCarrito = null;
    selectedInput = null;
    selectedInputTransfer = pValor;
}
function onblurCantProductosTransfer(pValor) {
    if (pValor.value != '') {
        var nombre = pValor.id;
        nombre = nombre.replace('txtProdTransf', '');
        var palabrasBase = nombre.split("_");
        var indiceTransfer = parseInt(palabrasBase[0]);
        var indiceProductoTransfer = parseInt(palabrasBase[1]);
        ValidarTransferPorProducto(indiceTransfer, indiceProductoTransfer);
    }
}
//function onClickTransfer(pIndice) {
//    ValidarTransferTotal(pIndice);
//}
function onClickTransfer(pIndice, pIndiceSucursal) {

    ValidarTransferTotal_sucursal(pIndice, pIndiceSucursal);
}
function ValidarTransferPorProducto(pIndiceTransfer, pIndiceProducto) {
    // var RenglonesMinimos = listaTransfer[pIndiceTransfer].listaDetalle[pIndiceProducto].tde_minuni; // Cantidad Minimo De Producto 
    var UnidadesMinimas = listaTransfer[pIndiceTransfer].listaDetalle[pIndiceProducto].tde_minuni;
    var UnidadesMaximas = listaTransfer[pIndiceTransfer].listaDetalle[pIndiceProducto].tde_maxuni;
    var UnidadesMultiplo = listaTransfer[pIndiceTransfer].listaDetalle[pIndiceProducto].tde_muluni;
    var UnidadesFijas = listaTransfer[pIndiceTransfer].listaDetalle[pIndiceProducto].tde_fijuni;

    var intTipoMensaje = 0;
    $('#tdError' + pIndiceTransfer + '_' + pIndiceProducto).html('');
    var cantidad = $('#txtProdTransf' + pIndiceTransfer + '_' + pIndiceProducto).val();
    if (isNotNullEmpty(cantidad)) {
        cantidad = parseInt(cantidad);
        intTipoMensaje = cantidad;
        if (cantidad > 0) {

            if (isNotNullEmpty(UnidadesMinimas)) {
                if (UnidadesMinimas > cantidad) { //
                    intTipoMensaje = -1;
                    $('#tdError' + pIndiceTransfer + '_' + pIndiceProducto).html('La cantidad no iguala o no supera la unidad mínima');
                }
            }
            if (isNotNullEmpty(UnidadesMaximas)) {
                if (UnidadesMaximas < cantidad) { //
                    intTipoMensaje = -1;
                    $('#tdError' + pIndiceTransfer + '_' + pIndiceProducto).html('La cantidad no iguala o supera la unidad máxima');
                }
            }
            if (isNotNullEmpty(UnidadesMultiplo)) {
                if (cantidad % UnidadesMultiplo == 0) {
                    // es multiplo
                } else {
                    intTipoMensaje = -1;
                    $('#tdError' + pIndiceTransfer + '_' + pIndiceProducto).html('La cantidad no es multiplo de ' + UnidadesMultiplo);
                    //jAlert('En el transfer se debe rellenar todos los producto', '');
                }
            }
            if (isNotNullEmpty(UnidadesFijas)) {
                if (UnidadesFijas == cantidad) {
                    intTipoMensaje = -1;
                    $('#tdError' + pIndiceTransfer + '_' + pIndiceProducto).html('La cantidad es diferente de la unidad fija');
                }
            }
        }
        else {
            // producto es igual a 0

        }
    }
    return intTipoMensaje;
}


function ValidarTransferTotal_sucursal(pIndice, pIndiceSursal) {
    var RenglonesMinimos = listaTransfer[pIndice].tfr_minrenglones; // Cantidad Minimo De Producto 
    var UnidadesMinimas = listaTransfer[pIndice].tfr_minunidades;
    var UnidadesMaximas = listaTransfer[pIndice].tfr_maxunidades;
    var UnidadesMultiplo = listaTransfer[pIndice].tfr_mulunidades;
    var UnidadesFijas = listaTransfer[pIndice].tfr_fijunidades;

    var auxCantidadProductoElejido = 0;
    var isGrabar = true;
    var tempListaProductos = [];
    var cantTotalEnTransfer = 0;
    // tfr_tipo
    for (var i = 0; i < listaTransfer[pIndice].listaDetalle.length; i++) {
        var isGrabarProducto = true;
        var intMensajeProducto = ValidarTransferPorProducto(pIndice, i);
        if (intMensajeProducto > 0) {
            cantTotalEnTransfer += intMensajeProducto;
            auxCantidadProductoElejido++;
        } else if (intMensajeProducto == -1) {
            isGrabarProducto = false;
        }
        if (listaTransfer[pIndice].listaDetalle[i].tde_proobligatorio) {
            if (intMensajeProducto == 0) {
                isGrabarProducto = false;
                $('#tdError' + pIndice + '_' + i).html('Producto obligatorio');
                alert(listaTransfer[pIndice].listaDetalle[i].tde_codpro + ' es un producto obligatorio');
            }
        }
        if (isGrabarProducto) {
            if (intMensajeProducto > 0) {
                var objProducto = new jcTransfersProductos();
                objProducto.codProductoNombre = listaTransfer[pIndice].listaDetalle[i].tde_codpro; // Para la funcion en el servidor
                objProducto.tde_codpro = listaTransfer[pIndice].listaDetalle[i].tde_codpro;
                objProducto.cantidad = intMensajeProducto;
                objProducto.indexAuxProducto = i;
                objProducto.indexAuxTransfer = pIndice;
                tempListaProductos.push(objProducto);
            }
        } else {
            isGrabar = false;
            break;
        }
    }
    if (isGrabar) {
        if (RenglonesMinimos > auxCantidadProductoElejido) {
            isGrabar = false;
            alert('El transfer de superar los renglones mínimos');
        }
    }
    // Validacion Transfer

    if (isGrabar) {
        if (isNotNullEmpty(UnidadesMinimas)) {
            if (UnidadesMinimas > cantTotalEnTransfer) {
                alert('El transfer no supera la unidad mínima');
                isGrabar = false;
            }
        }
    }
    if (isGrabar) {
        if (isNotNullEmpty(UnidadesMaximas)) {
            if (UnidadesMaximas < cantTotalEnTransfer) {
                alert('El transfer supera la unidad máxima');
                isGrabar = false;
            }
        }
    }
    if (isGrabar) {
        if (isNotNullEmpty(UnidadesMultiplo)) {
            if (cantTotalEnTransfer % UnidadesMultiplo == 0) {
                // es multiplo
            } else {
                alert('El transfer no es multiplo de ' + UnidadesMultiplo);
                isGrabar = false;
            }
        }
    }
    if (isGrabar) {
        if (isNotNullEmpty(UnidadesFijas)) {
            if (UnidadesFijas == cantTotalEnTransfer) {
                alert('El transfer es diferente de la unidad fija');
                isGrabar = false;
            }
        }
    }
    if (isGrabar) {
        // validar cuando un transfer es del tipo como
        if (listaTransfer[pIndice].tfr_tipo == 'C') {
            if (auxCantidadProductoElejido < listaTransfer[pIndice].listaDetalle.length) {
                alert('En el transfer se debe rellenar todos los producto');

                isGrabar = false;
            }
        }
    }
    // fin validacion Transfer
    if (isGrabar) {
        PageMethods.AgregarProductosTransfersAlCarrito(tempListaProductos, listaTransfer[pIndice].tfr_codigo, listaSucursalesDependienteInfo[pIndiceSursal].sde_sucursal, OnCallBackAgregarProductosTransfersAlCarrito, OnFail);
        //        CargarListaTransferCliente_ActualizarCarritoTransferHTML(pIndice, tempListaProductos, listaSucursalesDependienteInfo[pIndiceSursal].sde_sucursal);
    } else {

    }
}

function OnCallBackAgregarProductosTransfersAlCarrito(args) {
    // Cargar
    var resultArgs = eval('(' + args + ')');
    listaCarritoTransferPorSucursal = resultArgs.listSucursalCarritoTransfer;
    if (resultArgs.isNotError) {
        //$('#hiddenListaCarritosTransferPorSucursal').val(args);
        CargarCarritosTransfersPorSucursal();
        CerrarContenedorTransfer();
    } else {
        var msgProductos = '';
        for (var i = 0; i < resultArgs.listProductosAndCantidadError.length; i++) {
            msgProductos += resultArgs.listProductosAndCantidadError[i].codProductoNombre + '<br/>'
        }
        var htmlMensaje = cuerpo_error + msgProductos;
        CerrarContenedorTransfer();
        MostrarMensajeGeneral(titulo_error, htmlMensaje);

    }
}
function CargarCarritosTransfersPorSucursal() {
    // listaCarritoTransferPorSucursal = eval('(' + $('#hiddenListaCarritosTransferPorSucursal').val() + ')');
    if (typeof listaCarritoTransferPorSucursal == 'undefined') {
        listaCarritoTransferPorSucursal = null;
    }
    if (listaCarritoTransferPorSucursal != null) {
        for (var i = 0; i < listaCarritoTransferPorSucursal.length; i++) {
            $('#divContenedorBaseTransfer_' + listaCarritoTransferPorSucursal[i].Sucursal).html(AgregarCarritoTransfersPorSucursalHtml(i));
        }
    }
}
function AgregarCarritoTransfersPorSucursalHtml(pIndice) {
    var strHTML = '';
    if (listaCarritoTransferPorSucursal[pIndice].listaTransfer != null) {
        if (listaCarritoTransferPorSucursal[pIndice].listaTransfer.length > 0) {
            strHTML += '<div class="carro">';
            strHTML += '<div class="carro-titles">';
            strHTML += '<div class="ct-left">';
            strHTML += 'Transfers<br />';
            for (var iNombreSucursal = 0; iNombreSucursal < listaSucursalesDependienteInfo.length; iNombreSucursal++) {
                if (listaSucursalesDependienteInfo[iNombreSucursal].sde_sucursal == listaCarritoTransferPorSucursal[pIndice].Sucursal) {
                    strHTML += listaSucursalesDependienteInfo[iNombreSucursal].suc_nombre;
                    break;
                }
            }
            strHTML += '</div>';
            // Horario
            var isMostrarHorarioCierre = false;
            var visibleCssHorarioCierreTitulo = ' style="visibility:hidden;" ';
            if (isNotNullEmpty(listaCarritoTransferPorSucursal[pIndice].proximoHorarioEntrega) && sucursalCliente != listaCarritoTransferPorSucursal[pIndice].Sucursal) {
                isMostrarHorarioCierre = true;
                visibleCssHorarioCierreTitulo = '';
            }
            //            if (isNotNullEmpty(listaCarritoTransferPorSucursal[pIndice].proximoHorarioEntrega)) {
            strHTML += '<div id="contenedorProximaEntregaTransfer_' + pIndice + '" ' + visibleCssHorarioCierreTitulo + '  class="ct-right">';
            strHTML += 'Próximo&nbsp;cierre<br />';
            strHTML += '<span id="proximaEntregaTransfer_' + pIndice + '">';
            if (isMostrarHorarioCierre) {
                strHTML += listaCarritoTransferPorSucursal[pIndice].proximoHorarioEntrega;
            }
            strHTML += '</span>';
            strHTML += '</div>';
            //            }
            // fin horario
            strHTML += '<div class="clear">';
            strHTML += '</div>';
            strHTML += '</div>';

            strHTML += '<table  class="tbl-carro" border="0" cellspacing="0" cellpadding="0">'; // Encabezado
            strHTML += '<tr>';
            strHTML += ' <th width="60%" class="tbl-carroTh" >';
            strHTML += 'Detalle&nbsp;Producto';
            strHTML += '</th>';
            strHTML += '<th width="20%" class="tbl-carroTh">';
            strHTML += '  Cant.';
            strHTML += ' </th>';
            strHTML += '<th width="20%" class=" tbl-carroTh tbl-carro-style-off">';
            strHTML += '<div class="carro-top-borde">';
            strHTML += 'Precio</div>';
            strHTML += '</th>';
            strHTML += ' </tr>';
            strHTML += '</table>'; // fin Encabezado

            // Scroll 
            strHTML += '<div style="max-height:250px;overflow-y:scroll;overflow-x:hidden;">';

            // Cuerpo
            strHTML += '<table id="tb_transfer_' + listaCarritoTransferPorSucursal[pIndice].Sucursal + '"  style="width:240px;  !important;"  class="tbl-carro" border="0" cellspacing="0" cellpadding="0">';
            var nroTotalCarrito = parseFloat(0);
            var contFilaColor = -1;
            var cantProductosTransferTotales = 0;
            var RenglonesTotales = 0;
            for (var iTransfer = 0; iTransfer < listaCarritoTransferPorSucursal[pIndice].listaTransfer.length; iTransfer++) {
                var nroTotalPrecioPorTransfer = 0;
                for (var iTransferProductos = 0; iTransferProductos < listaCarritoTransferPorSucursal[pIndice].listaTransfer[iTransfer].listaProductos.length; iTransferProductos++) {
                    RenglonesTotales++;
                    contFilaColor++;
                    var strHtmlColor = '';
                    if (contFilaColor % 2 != 0) {
                        strHtmlColor = ' carro-td-color ';
                    }
                    cantProductosTransferTotales += listaCarritoTransferPorSucursal[pIndice].listaTransfer[iTransfer].listaProductos[iTransferProductos].cantidad;
                    var PrecioTotalProductoTransfer = listaCarritoTransferPorSucursal[pIndice].listaTransfer[iTransfer].listaProductos[iTransferProductos].cantidad * listaCarritoTransferPorSucursal[pIndice].listaTransfer[iTransfer].listaProductos[iTransferProductos].PrecioFinalTransfer;
                    strHTML += '<tr>';
                    strHTML += '<td width="85%" id="tdTransferProducto_' + pIndice + '_' + iTransfer + '_' + iTransferProductos + '"  class="' + strHtmlColor + 'first-td"   onclick="RecuperarTransferPorId(this)" style="cursor:pointer;" >' + listaCarritoTransferPorSucursal[pIndice].listaTransfer[iTransfer].listaProductos[iTransferProductos].tde_codpro + '</td>';
                    strHTML += '<td width="7%" class="' + strHtmlColor + '">' + '<span id="spanCarritoTransfer_' + listaCarritoTransferPorSucursal[pIndice].Sucursal + '_' + iTransfer + '_' + iTransferProductos + '"  >' + listaCarritoTransferPorSucursal[pIndice].listaTransfer[iTransfer].listaProductos[iTransferProductos].cantidad + '</span>' + '</td>';
                    strHTML += '<td width="7%" id="tdPrecioTransfer_' + listaCarritoTransferPorSucursal[pIndice].Sucursal + '_' + iTransfer + '_' + iTransferProductos + '" class="' + strHtmlColor + '"> $&nbsp;' + FormatoDecimalConDivisorMiles(PrecioTotalProductoTransfer.toFixed(2)) + '</td> ';
                    strHTML += '</tr>';
                    nroTotalCarrito += PrecioTotalProductoTransfer;
                }
            }
            strHTML += '</table>'; // fin Cuerpo

            strHTML += '</div>'; // fin Scroll


            /////
            ////  'Unidades' y 'Renglones'
            strHTML += '<table  class="tbl-carro" border="0" cellspacing="0" cellpadding="0" >';
            strHTML += '<tr>';
            strHTML += '<td align="right">';
            strHTML += '<b>Renglones:</b>';
            strHTML += '</td>';
            strHTML += '<td  align="left">';
            strHTML += RenglonesTotales; // listaCarritoTransferPorSucursal[pIndice].listaTransfer.length;
            strHTML += '</td>';
            strHTML += '<td  align="right">';
            strHTML += '<b>Unidades:</b>';
            strHTML += '</td>';
            strHTML += '<td  align="left">';
            strHTML += cantProductosTransferTotales;
            strHTML += '</td>';
            strHTML += '</tr>';
            strHTML += '</table>';
            //// fin 'Unidades' y 'Renglones'
            /////

            strHTML += '<table  class="tbl-carro" border="0" cellspacing="0" cellpadding="0">'; // Pie Pagina
            strHTML += '<tr>';
            strHTML += '<td width="80%" colspan="2" class="carro-total first-td">Total</td>';
            strHTML += '<td width="20%" id="tdTotalTransfer" class="carro-total">$&nbsp;' + FormatoDecimalConDivisorMiles(nroTotalCarrito.toFixed(2)) + '</td>';
            strHTML += '</tr>';
            strHTML += '</table>'; // fin Pie Pagina
            //

            //strHTML += ' <a class="carro-btn-confirmar" href="#" onclick="onclickConfirmarCarritoTransfer(' + pIndice + ')">Confirmar</a> ';
            strHTML += ' <a class="carro-btn-confirmar" href="#" onclick="onclickIsPuedeUsarDllTransfer(' + pIndice + ')">Confirmar</a> ';
            strHTML += ' <a class="carro-btn-vaciar" href="#" onclick="onclickVaciarCarritoTransfer(' + pIndice + ');return false;">Vaciar</a>';
            strHTML += ' <div class="clear">';
            strHTML += '  </div>';
            strHTML += ' </div>';
        }
    }
    return strHTML;
}
function myFunction() {
    alert('cambio');
}
function onclickIsPuedeUsarDllTransfer(pIndice) {
    indexSucursalTransferSeleccionado = pIndice;
    //    PageMethods.IsBanderaUsarDll(OnCallBackIsBanderaUsarDllTransfer, OnFail);
    PageMethods.IsHacerPedidos(listaCarritoTransferPorSucursal[indexSucursalTransferSeleccionado].Sucursal, OnCallBackIsHacerPedidosTransfer, OnFail);
}

function OnCallBackIsHacerPedidosTransfer(args) {
    if (args == 0) {
        onclickConfirmarCarritoTransfer(indexSucursalTransferSeleccionado);
    } else if (args == 2) {
        location.href = 'PedidosBuscador.aspx';
    } else if (args == 1) {
        alert('En este momento estamos realizando tareas de mantenimiento, por favor confirme su pedido más tarde.');
    }
}
//function OnCallBackIsBanderaUsarDllTransfer(args) {
//    if (args) {
//        onclickConfirmarCarritoTransfer(indexSucursalTransferSeleccionado);
//    } else {
//        alert('En este momento estamos realizando tareas de mantenimiento, por favor confirme su pedido más tarde.');
//        //Ha ocurrido un error, por favor intente de nuevo más tarde.
//    }
//}
function onclickConfirmarCarritoTransfer(pIndice) {
    indexSucursalTransferSeleccionado = pIndice;
    $('#txtMensajeFacturaTransfer').val('');
    $('#txtMensajeRemitoTransfer').val('');
    CargarHtmlTipoEnvioTransfer(listaCarritoTransferPorSucursal[indexSucursalTransferSeleccionado].Sucursal);
    document.getElementById('divConfirmarPedidoTransferContenedorGeneral').style.display = 'block';
    var arraySizeDocumento = SizeDocumento();
    document.getElementById('divTransferContenedorGeneralFondo').style.height = arraySizeDocumento[1] + 'px';
    document.getElementById('divTransferContenedorGeneralFondo').style.display = 'block';
    // Actualizar horario cierre
    PageMethods.ObtenerHorarioCierre(listaCarritoTransferPorSucursal[indexSucursalTransferSeleccionado].Sucursal, OnCallBackObtenerHorarioCierreTransfer, OnFail);
    // fin actualizar horario cierre
}
function OnCallBackObtenerHorarioCierreTransfer(args) {
    if (args != null) {
        if (indexSucursalTransferSeleccionado != null) {
            if (sucursalCliente != listaCarritoTransferPorSucursal[indexSucursalTransferSeleccionado].Sucursal) {
                $('#proximaEntregaTransfer_' + indexSucursalTransferSeleccionado).html(listaCarritoTransferPorSucursal[indexSucursalTransferSeleccionado].proximoHorarioEntrega);
                $('#contenedorProximaEntregaTransfer_' + indexSucursalTransferSeleccionado).css('visibility', 'visible');
            }
        }
    }
}
function onclickConfimarTransferPedidoOk() {
    if (isBotonNoEstaEnProceso) {
        if (indexSucursalTransferSeleccionado != null) {
            idTipoEnvioPedidoTransfer = $('#comboTipoEnvioTransfer').val();
            textFacturaPedidoTransfer = $('#txtMensajeFacturaTransfer').val();
            textRemitoPedidoTransfer = $('#txtMensajeRemitoTransfer').val();
            codSucursalPedidoTransfer = listaCarritoTransferPorSucursal[indexSucursalTransferSeleccionado].Sucursal;
            textTipoEnvioCarritoTransfer = $('#comboTipoEnvioTransfer option:selected').text();
            PageMethods.TomarTransferPedidoCarrito(codSucursalPedidoTransfer, textFacturaPedidoTransfer, textRemitoPedidoTransfer, idTipoEnvioPedidoTransfer, OnCallBackTomarTransferPedidoCarrito, OnFailBotonEnProceso);
            //
            $('#divCargandoContenedorGeneralFondo').css('display', 'block');
            var arraySizeDocumento = SizeDocumento();
            document.getElementById('divCargandoContenedorGeneralFondo').style.height = arraySizeDocumento[1] + 'px';
            //
            isBotonNoEstaEnProceso = false;
        }
    }
}
function OnCallBackTomarTransferPedidoCarrito(args) {
    //    CerrarContenedorTransfer();
    isBotonNoEstaEnProceso = true;
    if (args == null) {
        alert(mensajeCuandoSeMuestraError);
        location.href = 'PedidosBuscador.aspx';
    } else {
        var isError = false;
        if (args.length > 0) {
            if (args[0].Error != '') {
                isError = true;
            }
        }
        if (isError) {
            alert(args[0].Error);
            location.href = 'PedidosBuscador.aspx';
        } else {
            isHacerBorradoCarritos = true;
            document.getElementById('divConfirmarPedidoTransferContenedorGeneral').style.display = 'none';
            CargarRespuestaDePedidoTransfer(args);
        }
    }
    //    alert('O');
}
function onclickVaciarCarritoTransfer(pIndice) {
    //if (listaCarritoTransferPorSucursal != null) {
    //    indiceCarritoTransferBorrar = pIndice;
    //    PageMethods.BorrarCarritoTransfer(listaCarritoTransferPorSucursal[pIndice].Sucursal, OnCallBackBorrarCarritoTransfer, OnFail);
    //}
    $("#dialog-confirm-Carrito").dialog({
        resizable: false,
        height: "auto",
        width: 400,
        modal: true,
        buttons: {
            "Ok": function () {
                if (listaCarritoTransferPorSucursal != null) {
                    indiceCarritoTransferBorrar = pIndice;
                    PageMethods.BorrarCarritoTransfer(listaCarritoTransferPorSucursal[pIndice].Sucursal, OnCallBackBorrarCarritoTransfer, OnFail);
                }

                $(this).dialog("close");
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        }
    });
}
function OnCallBackBorrarCarritoTransfer(args) {
    if (args) {
        if (indiceCarritoTransferBorrar != null) {
            $('#divContenedorBaseTransfer_' + listaCarritoTransferPorSucursal[indiceCarritoTransferBorrar].Sucursal).html('');
            var sucur = listaCarritoTransferPorSucursal[indiceCarritoTransferBorrar].Sucursal;
            listaCarritoTransferPorSucursal[indiceCarritoTransferBorrar].Sucursal = '';
            LimpiarTextBoxProductosBuscados(sucur);
        }
    }
}
//function AgregarCarritoTransfersPorSucursalHtml(pIndice) {
//    var strHTML = '';
//    if (listaCarritoTransfer != null) {
//        //        if (listaCarritoTransfer.length > 0) {
//        strHTML += '<div class="carro">';
//        strHTML += '<div class="carro-titles">';
//        strHTML += '<div class="ct-left">';
//        strHTML += 'Transfers<br />';
//        for (var iNombreSucursal = 0; iNombreSucursal < listaSucursalesDependienteInfo.length; iNombreSucursal++) {
//            if (listaSucursalesDependienteInfo[iNombreSucursal].sde_sucursal == listaCarritoTransfer[pIndice].ctr_codSucursal) {
//                strHTML += listaSucursalesDependienteInfo[iNombreSucursal].suc_nombre;
//                break;
//            }
//        }
//        strHTML += '</div>';
//        //    strHTML += '<div class="ct-right">'; .sde_sucursal
//        //    strHTML += 'Próxima&nbsp;entrega<br />';
//        //    strHTML += '<span>15:00 hs.</span>';
//        //    strHTML += '</div>';
//        strHTML += '<div class="clear">';
//        strHTML += '</div>';
//        strHTML += '</div>';
//        strHTML += '<table id="tb_transfer_' + listaCarritoTransfer[pIndice].ctr_codSucursal + '"  class="tbl-carro" border="0" cellspacing="0" cellpadding="0">';
//        strHTML += '<tr>';
//        strHTML += ' <th class="tbl-carroTh" >';
//        strHTML += 'Detalle&nbsp;Producto';
//        strHTML += '</th>';
//        strHTML += '<th  class="tbl-carroTh">';
//        strHTML += '  Cant.';
//        strHTML += ' </th>';
//        strHTML += '<th class=" tbl-carroTh tbl-carro-style-off">';
//        strHTML += '<div class="carro-top-borde">';
//        strHTML += 'Precio</div>';
//        strHTML += '</th>';
//        strHTML += ' </tr>';
//        var nroTotalCarrito = parseFloat(0);
//        var contFilaColor = -1;
//        var nroTotalPrecioPorTransfer = 0;
//        for (var iTransferProductos = 0; iTransferProductos < listaCarritoTransfer[pIndice].listaProductos.length; iTransferProductos++) {
//            contFilaColor++;
//            var strHtmlColor = '';
//            if (contFilaColor % 2 != 0) {
//                strHtmlColor = ' carro-td-color ';
//            }
//            var PrecioTotalProductoTransfer = listaCarritoTransfer[pIndice].listaProductos[iTransferProductos].cantidad * listaCarritoTransfer[pIndice].listaProductos[iTransferProductos].PrecioFinalTransfer;
//            strHTML += '<tr>';
//            strHTML += '<td id="tdTransferProducto_' + pIndice + '_' + iTransferProductos + '"  class="' + strHtmlColor + 'first-td"   onclick="RecuperarTransferPorId(this)" style="cursor:pointer;" >' + listaCarritoTransfer[pIndice].listaProductos[iTransferProductos].tde_codpro + '</td>';
//            strHTML += '<td  class="' + strHtmlColor + '">' + '<span id="spanCarritoTransfer_' + pIndice + '_' + iTransferProductos + '"  >' + listaCarritoTransfer[pIndice].listaProductos[iTransferProductos].cantidad + '</span>' + '</td>';
//            strHTML += '<td id="tdPrecioTransfer_' + pIndice + '_' + iTransferProductos + '" class="' + strHtmlColor + '"> $&nbsp;' + PrecioTotalProductoTransfer.toFixed(2).toString() + '</td> ';
//            strHTML += '</tr>';
//            nroTotalCarrito += PrecioTotalProductoTransfer;
//        }
//        strHTML += '<tr>';
//        strHTML += '<td colspan="2" class="carro-total first-td">Total</td>';
//        strHTML += '<td id="tdTotalTransfer_' + listaCarritoTransfer[pIndice].ctr_codSucursal + '" class="carro-total">$&nbsp;' + nroTotalCarrito.toFixed(2).toString() + '</td>';
//        strHTML += '</tr>';
//        //
//        strHTML += '</table>';
//        strHTML += ' <a class="carro-btn-confirmar" href="#" onclick="onclickConfirmarCarritoTransfer()">Confirmar</a> ';
//        strHTML += ' <a class="carro-btn-vaciar" href="#" onclick="onclickVaciarCarritoTransfer(' + pIndice + ')">Vaciar</a>';
//        strHTML += ' <div class="clear">';
//        strHTML += '  </div>';
//        strHTML += ' </div>';
//        //        }
//    }
//    return strHTML;
//}

//function onclickConfirmarCarritoTransfer() {
//    document.getElementById('divConfirmarPedidoTransferContenedorGeneral').style.display = 'block';
//    document.getElementById('divTransferContenedorGeneralFondo').style.display = 'block';
//    // onclickConfimarTransferPedidoOk();
//}
//function onclickConfimarTransferPedidoOk() {
//}

//function onclickVaciarCarritoTransfer(pIndice) {
//    if (listaCarritoTransfer != null) {
//        indiceCarritoTransferBorrar = pIndice;
//        PageMethods.BorrarCarritoTransfer(listaCarritoTransfer[pIndice].ctr_codSucursal, OnCallBackBorrarCarritoTransfer, OnFail);
//    }
//}

//function OnCallBackBorrarCarritoTransfer(args) {
//    if (args) {
//        if (indiceCarritoTransferBorrar != null) {
//            $('#divContenedorBaseTransfer_' + listaCarritoTransfer[indiceCarritoTransferBorrar].ctr_codSucursal).html('');
//            //            listaCarritoTransfer = [];
//            delete listaCarritoTransfer[indiceCarritoTransferBorrar];
//            indiceCarritoTransferBorrar = null;
//        }
//    }
//}

//function RecuperarTransferPorId(pValor) {
//    var arrayValor = pValor.id.replace('tdTransferProducto_', '').split('_');
//    indexTransferSeleccionado = arrayValor[0]; // pIndexTransferCarrito;
//    productoSeleccionado = listaCarritoTransfer[arrayValor[0]].listaProductos[arrayValor[1]].tde_codpro; //tde_codpro
//    PageMethods.RecuperarTransferPorId(listaCarritoTransfer[indexTransferSeleccionado].tfr_codigo, OnCallBackRecuperarTransferPorId, OnFail);
//} id="tdTransferProducto_' + listaCarritoTransferPorSucursal[pIndice].Sucursal + '_' + iTransfer + '_' + iTransferProductos + '"

function RecuperarTransferPorId(pValor) {
    var arrayValor = pValor.id.replace('tdTransferProducto_', '').split('_');
    indexSucursalTransferSeleccionado = arrayValor[0]; // pIndexTransferCarrito;
    indexTransferSeleccionado = arrayValor[1]; // pIndexTransferCarrito;
    productoSeleccionado = listaCarritoTransferPorSucursal[indexSucursalTransferSeleccionado].listaTransfer[indexTransferSeleccionado].listaProductos[arrayValor[2]].tde_codpro; //tde_codpro
    PageMethods.RecuperarTransferPorId(listaCarritoTransferPorSucursal[indexSucursalTransferSeleccionado].listaTransfer[indexTransferSeleccionado].tfr_codigo, OnCallBackRecuperarTransferPorId, OnFail);
}
function OnCallBackRecuperarTransferPorId(args) {
    var argsAux = [];
    argsAux.push(args);
    OnCallBackRecuperarTransfer(argsAux);

    //    //Inicio:01: Carga las cantidades de productos seleccionados del transfer
    //    if (indexTransferSeleccionado > -1) {
    //        //        listaCarritoTransfer[indexTransferSeleccionado]
    //        for (var i = 0; i < listaCarritoTransfer[indexTransferSeleccionado].listaProductos.length; i++) {
    //            if (listaCarritoTransfer[indexTransferSeleccionado].listaProductos[i].cantidad > 0) {

    //                for (var indiceProductoTransfer = 0; indiceProductoTransfer < listaTransfer[0].listaDetalle.length; indiceProductoTransfer++) {
    //                    if (listaTransfer[0].listaDetalle[indiceProductoTransfer].tde_codpro == listaCarritoTransfer[indexTransferSeleccionado].listaProductos[i].pro_nombre) {
    //                        $('#txtProdTransf0_' + indiceProductoTransfer).val(listaCarritoTransfer[indexTransferSeleccionado].listaProductos[i].cantidad);
    //                        break;
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    //Fin:01:
}
//function agregarFilaTransferAlCarrito(pIndiceTransferEnCarrito, pIndiceTransferProducto, pSucursal) {

//    /// Cambiar el estilo dependiendo la paridad 
//    var table = document.getElementById('tb_transfer_' + pSucursal);
//    var rowCount = table.rows.length - 1;

//    var strHtmlColor = '';
//    if (table.rows.length % 2 != 0) {
//        strHtmlColor += ' carro-td-color ';
//    }
//    var row = table.insertRow(rowCount);

//    var cellNombreProducto = row.insertCell(0);
//    cellNombreProducto.className = 'first-td ' + strHtmlColor;
//    cellNombreProducto.align = 'left';
//    cellNombreProducto.id = 'tdTransferProducto_' + pIndiceTransferEnCarrito + '_' + pIndiceTransferProducto;
//    cellNombreProducto.setAttribute('onclick', 'RecuperarTransferPorId(this)'); //RecuperarTransferPorId(' + iTransfer + ')
//    cellNombreProducto.setAttribute('style', 'cursor:pointer;');
//    var newContent = document.createTextNode(listaCarritoTransfer[pIndiceTransferEnCarrito].listaProductos[pIndiceTransferProducto].tde_codpro);
//    cellNombreProducto.appendChild(newContent);

//    var cellCantidad = row.insertCell(1);
//    cellCantidad.className = strHtmlColor;

//    //    cellCantidad.onclick = function () {
//    //        RecuperarTransferPorId(pIndiceTransferEnCarrito);
//    //    };

//    var newElementCantidad = document.createTextNode(listaCarritoTransfer[pIndiceTransferEnCarrito].listaProductos[pIndiceTransferProducto].cantidad);
//    var newElementCantidad = document.createElement("span");
//    newElementCantidad.innerHTML = listaCarritoTransfer[pIndiceTransferEnCarrito].listaProductos[pIndiceTransferProducto].cantidad;
//    newElementCantidad.id = 'spanCarritoTransfer_' + pIndiceTransferEnCarrito + '_' + pIndiceTransferProducto;
//    cellCantidad.appendChild(newElementCantidad);

//    var tempPrecioTotal = listaCarritoTransfer[pIndiceTransferEnCarrito].listaProductos[pIndiceTransferProducto].cantidad * listaCarritoTransfer[pIndiceTransferEnCarrito].listaProductos[pIndiceTransferProducto].PrecioFinalTransfer;
//    var cellPrecio = row.insertCell(2);
//    cellPrecio.className = strHtmlColor;
//    cellPrecio.id = 'tdPrecioTransfer_' + pIndiceTransferEnCarrito + '_' + pIndiceTransferProducto;
//    cellPrecio.innerHTML = '$&nbsp;' + tempPrecioTotal.toFixed(2);
//    var elementPrecio = document.createTextNode('$&nbsp;' + tempPrecioTotal.toFixed(2));
//    cellPrecio.appendChild(elementPrecio);
//}
function jcTransfersProductos() {
    //    this.codProductoNombre = -1;
    this.PrecioFinalTransfer;
    this.tde_codpro = -1;
    this.cantidad = -1;
    this.indexAuxProducto = -1; // para actualizar el carrito de compras
    this.indexAuxTransfer = -1; // para actualizar el carrito de compras
}

function CalcularPrecioProductosEnCarritoTransfer(pPrecioParcial, pDescuentoHabitualCliente, pPorcentajeDescuentoAdicional) {
    var resultado = 0.0;

    if (parseFloat(pDescuentoHabitualCliente) != parseFloat(0)) {
        resultado = parseFloat(pPrecioParcial) * (1 - parseFloat(pDescuentoHabitualCliente));
    } else {
        resultado = pPrecioParcial;
    }

    if (parseFloat(pPorcentajeDescuentoAdicional) != parseFloat(0)) {
        resultado = resultado * (1 - (parseFloat(pPorcentajeDescuentoAdicional) / parseFloat(100)));
    }

    return parseFloat(resultado);
}
////////
function CargarRespuestaDePedidoTransfer(pValor) {
    listaResultadoPedidoTransfer = pValor;
    var MontoTotal = 0;
    var isProductosTransferPedidos = false;
    var isProductosTransferPedidosFaltantes = false;
    var isProductosTransferPedidosRevision = false;
    var isProductosPedidoFacturarseHabitual = false;
    var strHtmlFaltantes = '';
    var strHtml = '';
    var strHtmlMensajeFinales = '';
    var strHtmlEnRevision = '';
    var strHtmlPedidoFacturarseHabitual = '';
    if (pValor != null) {
        if (pValor.length > 0) {
            // Mensaje
            //            if (indexSucursalTransferSeleccionado != null) {
            //                var sucursal = listaCarritoTransferPorSucursal[indexSucursalTransferSeleccionado].Sucursal;
            //                if (sucursal == 'CC') {
            //                    strHtmlMensajeFinales = '<div style="font-size: 14px;text-align:left;margin-top:10px;">' + 'Transfers a Revisión. Se ha generado un mail a pedidos@kellerhoff.com.ar' + ' </div>';
            //                } else {
            //                    strHtmlMensajeFinales = '<div style="font-size: 14px;text-align:left;margin-top:10px;">' + 'Su pedido ha sido enviado con éxito a la sucursal' + ' </div>';
            //                }
            //            }
            strHtmlMensajeFinales += '<div style="font-size: 12px;text-align:left; ">TIPO DE ENVIO: ' + textTipoEnvioCarritoTransfer + ' </div>';

            // fin Mensaje
            strHtml += '<div style="font-size: 14px; margin-top: 10px; width: 100%;text-align:left;background-color: #E5F3E4;color:#0B890A;">';
            strHtml += ' <b>PRODUCTOS FACTURADOS </b>';
            strHtml += '</div>';
            strHtml += '<table class="tbl-buscador-productos"  style="width:100% !important;" border="0" cellspacing="0" cellpadding="0">';
            strHtml += '<tr>';
            strHtml += '<th align="left" style="background-color:#EBEBEB;color:#000;">';
            strHtml += 'Nombre producto';
            strHtml += '</th>';
            strHtml += '<th  style="background-color:#EBEBEB;color:#000;">';
            strHtml += 'Cantidad';
            strHtml += '</th>';
            strHtml += '</tr>';

            // Encabezado PRODUCTOS EN FALTA
            strHtmlFaltantes += '<div style="font-size: 14px; margin-top: 10px; width: 100%;text-align:left;background-color:#F7E7E8;color:#B3000C;">';
            strHtmlFaltantes += '<b>PRODUCTOS EN FALTA</b>';
            strHtmlFaltantes += '</div>';
            strHtmlFaltantes += '<table class="tbl-buscador-productos"  style="width:100% !important;" border="0" cellspacing="0" cellpadding="0">';
            strHtmlFaltantes += '<tr>';
            strHtmlFaltantes += '<th  align="left" style="background-color:#EBEBEB;color:#000;">';
            strHtmlFaltantes += 'Nombre producto';
            strHtmlFaltantes += '</th>';
            strHtmlFaltantes += '<th  style="background-color:#EBEBEB;color:#000;">';
            strHtmlFaltantes += 'Cantidad';
            strHtmlFaltantes += '</th>';
            strHtmlFaltantes += '</tr>';
            // Fin Encabezado PRODUCTOS EN FALTA

            var cantFaltantes = 0;
            var cantFacturados = 0;
            for (var i = 0; i < pValor.length; i++) {

                // EnRevision
                if (pValor[i].Login == 'REVISION') {
                    //Inicio Mensaje solamente a revicion
                    if (indexSucursalTransferSeleccionado != null) {
                        var sucursal = listaCarritoTransferPorSucursal[indexSucursalTransferSeleccionado].Sucursal;
                        //                        if (sucursal == 'CC') {
                        //                            strHtmlMensajeFinales = '<div style="font-size: 14px;text-align:left;margin-top:10px;">' + 'Transfers a Revisión. Se ha generado un mail a pedidos@kellerhoff.com.ar' + ' </div>';
                        //                        } else  { 
                        //                            strHtmlMensajeFinales = '<div style="font-size: 14px;text-align:left;margin-top:10px;">' + 'Su pedido ha sido enviado con éxito a la sucursal' + ' </div>';
                        //                        }
                        strHtmlMensajeFinales = '';
                        strHtmlMensajeFinales += '<div style="font-size: 14px;text-align:left;margin-top:10px;">' + 'Transfers a Revisión. Se ha generado un mail a ';
                        if (sucursal == 'CC') {
                            strHtmlMensajeFinales += 'pedidos@kellerhoff.com.ar';
                        } else if (sucursal == 'CH') {
                            strHtmlMensajeFinales += 'sucursalchanarladeado@kellerhoff.com.ar';
                        } else if (sucursal == 'SF') {
                            strHtmlMensajeFinales += 'sucursalsantafe@kellerhoff.com.ar';
                        } else if (sucursal == 'CB') {
                            strHtmlMensajeFinales += 'sucursalcordoba@kellerhoff.com.ar';
                        } else if (sucursal == 'CO') {
                            strHtmlMensajeFinales += 'sucursalconcepcion@kellerhoff.com.ar';
                        } else if (sucursal == 'CD') {
                            strHtmlMensajeFinales += 'sucursalconcordia@kellerhoff.com.ar';
                        } else if (sucursal == 'VH') {
                            strHtmlMensajeFinales += 'terapiasespeciales@kellerhoff.com.ar';
                        }
                        strHtmlMensajeFinales += ' </div>';
                    }
                    //Fin Mensaje solamente a revicion


                    strHtmlEnRevision += '<div style="font-size: 14px; margin-top: 10px; width: 100%;text-align:left;background-color:#F7E7E8;color:#B3000C;">';
                    strHtmlEnRevision += ' <b>PRODUCTOS PENDIENTES EN FACTURACION </b>';
                    strHtmlEnRevision += '</div>';
                    strHtmlEnRevision += '<table class="tbl-buscador-productos"  style="width:100% !important;" border="0" cellspacing="0" cellpadding="0">';
                    strHtmlEnRevision += '<tr>';
                    strHtmlEnRevision += '<th align="left" style="background-color:#EBEBEB;color:#000;">';
                    strHtmlEnRevision += 'Nombre producto';
                    strHtmlEnRevision += '</th>';
                    strHtmlEnRevision += '<th style="background-color:#EBEBEB;color:#000;">';
                    strHtmlEnRevision += 'Cantidad';
                    strHtmlEnRevision += '</th>';
                    strHtmlEnRevision += '</tr>';

                    for (var x = 0; x < pValor[i].Items.length; x++) {
                        var strHtmlColorFondo = '';
                        if (x % 2 != 0) {
                            strHtmlColorFondo = ' bp-td-color';
                        }
                        isProductosTransferPedidosRevision = true;
                        strHtmlEnRevision += '<tr>';
                        strHtmlEnRevision += '<td align="left" style="text-align:left !important;" class="' + strHtmlColorFondo + '">';
                        strHtmlEnRevision += pValor[i].Items[x].NombreObjetoComercial;
                        strHtmlEnRevision += '</td>';
                        strHtmlEnRevision += '<td class="' + strHtmlColorFondo + '">';
                        strHtmlEnRevision += pValor[i].Items[x].Cantidad;
                        strHtmlEnRevision += '</td>';
                        strHtmlEnRevision += '</tr>';
                    }
                    strHtmlEnRevision += '</table>';
                    //Fin EnRevision
                } else if (pValor[i].Login == 'CONFIRMACION') {
                    // facturarse de forma Habitual 
                    strHtmlPedidoFacturarseHabitual += '<div style="font-size: 14px; margin-top: 10px; width: 100%;text-align:left;background-color:#E5F3E4;color:#0B890A;">';
                    //strHtmlPedidoFacturarseHabitual += '<b>PRODUCTOS QUE NO CUMPLEN CON LA CONDICIÓN DE TRANSFERS. CONFIRME SI LOS QUIERE CON SU DESCUENTO HABITUAL </b>';                    
                    //strHtmlPedidoFacturarseHabitual += '<b>Productos no procesados en transfer por no contar con stock suficiente para cumplimentar la condición mínima o por exceso en el cupo de unidades permitidas. Confirme cuántas unidades quiere con su descuento habitual.</b>';
                    strHtmlPedidoFacturarseHabitual += '<b>Productos en transfer no procesados por falta de stock para llegar a condición mínima o exceso en el cupo de unidades. Confirme cuantas unidades quiere con su descuento habitual.</b>';
                    strHtmlPedidoFacturarseHabitual += '</div>';
                    strHtmlPedidoFacturarseHabitual += '<table class="tbl-buscador-productos"  style="width:100% !important;" border="0" cellspacing="0" cellpadding="0">';
                    strHtmlPedidoFacturarseHabitual += '<tr>';
                    strHtmlPedidoFacturarseHabitual += '<th align="left" style="background-color:#EBEBEB;color:#000;">';
                    strHtmlPedidoFacturarseHabitual += 'Nombre producto';
                    strHtmlPedidoFacturarseHabitual += '</th>';
                    strHtmlPedidoFacturarseHabitual += '<th style="background-color:#EBEBEB;color:#000;">';
                    strHtmlPedidoFacturarseHabitual += 'Cantidad';
                    strHtmlPedidoFacturarseHabitual += '</th>';
                    strHtmlPedidoFacturarseHabitual += '</tr>';

                    for (var x = 0; x < pValor[i].Items.length; x++) {
                        var strHtmlColorFondo = '';
                        if (x % 2 != 0) {
                            strHtmlColorFondo = ' bp-td-color';
                        }
                        isProductosPedidoFacturarseHabitual = true;
                        strHtmlPedidoFacturarseHabitual += '<tr>';
                        strHtmlPedidoFacturarseHabitual += '<td align="left" style="text-align:left !important;" class="' + strHtmlColorFondo + '">';
                        strHtmlPedidoFacturarseHabitual += pValor[i].Items[x].NombreObjetoComercial;
                        strHtmlPedidoFacturarseHabitual += '</td>';
                        strHtmlPedidoFacturarseHabitual += '<td class="' + strHtmlColorFondo + '">';
                        //strHtmlPedidoFacturarseHabitual += pValor[i].Items[x].Cantidad;
                        strHtmlPedidoFacturarseHabitual += '<input class="cssFocusCantProdCarrito" id="inputPedidoCant' + i + "_" + x + '" type="text"   value="' + pValor[i].Items[x].Cantidad + '" ></input>';
                        strHtmlPedidoFacturarseHabitual += '</td>';
                        strHtmlPedidoFacturarseHabitual += '</tr>';
                    }
                    strHtmlPedidoFacturarseHabitual += '</table>';
                    // fin facturarse de forma Habitual 
                } else {
                    if (isProductosTransferPedidos) {
                        strHtml += '<tr>';
                        strHtml += '<td align="left"colspan="2">';
                        strHtml += '<div style="border-bottom: 1px solid #333333;line-height: 27px;width: 100%;"></div>';
                        strHtml += '</td>';
                        strHtml += '</tr>';
                    }
                    for (var x = 0; x < pValor[i].Items.length; x++) {

                        var strHtmlColorFondo = '';
                        if (pValor[i].Items[x].Cantidad > 0) {
                            strHtmlColorFondo = '';
                            if (cantFacturados % 2 != 0) {
                                strHtmlColorFondo = ' bp-td-color';
                            }
                            cantFacturados++;
                            isProductosTransferPedidos = true;
                            strHtml += '<tr>';
                            strHtml += '<td align="left" style="text-align:left !important;"  class="' + strHtmlColorFondo + '">';
                            strHtml += pValor[i].Items[x].NombreObjetoComercial;
                            strHtml += '</td>';
                            strHtml += '<td  class="' + strHtmlColorFondo + '">';
                            strHtml += pValor[i].Items[x].Cantidad;
                            strHtml += '</td>';
                            strHtml += '</tr>';
                        }
                        if (pValor[i].Items[x].Faltas > 0) {
                            strHtmlColorFondo = '';
                            if (cantFaltantes % 2 != 0) {
                                strHtmlColorFondo = ' bp-td-color';
                            }
                            cantFaltantes++;
                            isProductosTransferPedidosFaltantes = true;
                            strHtmlFaltantes += '<tr>';
                            strHtmlFaltantes += '<td align="left" style="text-align:left !important;"  class="' + strHtmlColorFondo + '">';
                            strHtmlFaltantes += pValor[i].Items[x].NombreObjetoComercial;
                            strHtmlFaltantes += '</td>';
                            strHtmlFaltantes += '<td  class="' + strHtmlColorFondo + '">';
                            strHtmlFaltantes += pValor[i].Items[x].Faltas;
                            strHtmlFaltantes += '</td>';
                            strHtmlFaltantes += '</tr>';
                        }
                    }
                    MontoTotal += pValor[i].MontoTotal;
                }
            }
            strHtml += '</table>';
            strHtml += '<div  style="font-size: 12px;text-align:left;margin-top:2px;background-color: #E5F3E4;"><b>MONTO TOTAL:</b>  <span style="color:#0B890A;"> $ ' + FormatoDecimalConDivisorMiles(MontoTotal.toFixed(2)) + ' </span>  </div>';

        } // fin  if (pValor.length > 0) 
        else {
            strHtmlMensajeFinales = '<div style="font-size: 14px;text-align:left;margin-top:10px;">' + 'Su pedido ha sido enviado con éxito a la sucursal' + ' </div>';
        }
    } else {  // fin if (pValor != null){
        // Se produjo un error
    }
    if (isProductosTransferPedidos) {
    } else {
        strHtml = '';
    }
    if (isProductosTransferPedidosFaltantes) {
    } else {
        strHtmlFaltantes = '';
    }
    if (isProductosTransferPedidosRevision) {
    } else {
        strHtmlEnRevision = '';
    }
    if (isProductosPedidoFacturarseHabitual) {
        strHtmlPedidoFacturarseHabitual += '<a class="carro-btn-confirmar" onclick="CerrarContenedorTransfer(); return false;"  href="#">Descartar</a>';
        strHtmlPedidoFacturarseHabitual += '<a class="carro-btn-confirmar" onclick="onclickPedidoFacturarseHabitualConfirmar()" href="#">Confirmar</a>';
        strHtmlPedidoFacturarseHabitual += '<div class="clear">';
        strHtmlPedidoFacturarseHabitual += '</div>';
        document.getElementById('resultadoPedidoBotonOk').style.display = 'none';
    } else {
        strHtmlPedidoFacturarseHabitual = '';
    }
    $('#divRespuestaProductosPedidos').html(strHtml);
    $('#divRespuestaMensajeFinales').html(strHtmlMensajeFinales);
    $('#divRespuestaFaltantes').html(strHtmlFaltantes);
    $('#divRespuestaPendienteDeFacturacion').html(strHtmlPedidoFacturarseHabitual);
    $('#divRespuestaProblemasCrediticios').html(strHtmlEnRevision);

    document.getElementById('divConfirmarPedidoContenedorGeneral').style.display = 'none';
    document.getElementById('divRespuestaPedidoContenedorGeneral').style.display = 'block';
    $('#divCargandoContenedorGeneralFondo').css('display', 'none');
    // fin actualizar horario cierre
}
function onclickPedidoFacturarseHabitualConfirmar() {
    if (listaResultadoPedidoTransfer != null) {
        for (var i = 0; i < listaResultadoPedidoTransfer.length; i++) {
            if (listaResultadoPedidoTransfer[i].Login == 'CONFIRMACION') {
                var listaCantidad = [];
                var listaNombreProducto = [];
                for (var x = 0; x < listaResultadoPedidoTransfer[i].Items.length; x++) {
                    var cantPedido = $('#inputPedidoCant' + i + "_" + x).val();
                    if (cantPedido > 0) {
                        // cargar pedido
                        listaCantidad.push(cantPedido);
                        listaNombreProducto.push(listaResultadoPedidoTransfer[i].Items[x].NombreObjetoComercial);
                    }
                }
                if (listaCantidad.length > 0 && listaNombreProducto.length > 0) {
                    PageMethods.TomarPedidoCarritoFacturarseFormaHabitual(codSucursalPedidoTransfer, textFacturaPedidoTransfer, textRemitoPedidoTransfer, idTipoEnvioPedidoTransfer, false, listaNombreProducto, listaCantidad, OnCallBackTomarPedidoCarritoFacturarseFormaHabitual, OnFail);
                }
                break;
            }
        }
    }
}
function OnCallBackTomarPedidoCarritoFacturarseFormaHabitual(args) {
    CerrarContenedorTransfer();
    var arraySizeDocumento = SizeDocumento();
    document.getElementById('divTransferContenedorGeneralFondo').style.height = arraySizeDocumento[1] + 'px';
    document.getElementById('divTransferContenedorGeneralFondo').style.display = 'block';
    OnCallBackTomarPedidoCarrito(args);

}
function onclickPedidoFacturarseHabitualDescartar() {

}
//////////
//function CargarRespuestaDePedidoTransfer_ANT(pValor) {
//    var MontoTotal = 0;
//    var isProductosTransferPedidos = false;
//    var isProductosTransferPedidosRevision = false;
//    var strHtml = '';
//    var strHtmlMensajeFinales = '';
//    var strHtmlEnRevision = '';
//    var LengthMenosUno = -1;
//    var MaxLength = -1;
//    if (pValor != null) {
//        if (pValor.length > 0) {
//            LengthMenosUno = pValor.length - 1;
//            var loginMensaje = '';
//            loginMensaje = pValor[LengthMenosUno].Login;
//            if (loginMensaje == 'REVISION') {
//                MaxLength = LengthMenosUno;
//            } else {
//                MaxLength = pValor.length;
//            }
//            // Mensaje
//            if (indexSucursalTransferSeleccionado != null) {
//                var sucursal = listaCarritoTransferPorSucursal[indexSucursalTransferSeleccionado].Sucursal;
//                if (sucursal == 'CC') {
//                    strHtmlMensajeFinales = '<div style="font-size: 14px;text-align:left;margin-top:10px;">' + 'Transfers a Revisión. Se ha generado un mail a pedidos@kellerhoff.com.ar' + ' </div>';
//                } else {
//                    strHtmlMensajeFinales = '<div style="font-size: 14px;text-align:left;margin-top:10px;">' + 'Su pedido ha sido enviado con éxito a la sucursal' + ' </div>';
//                }
//            }
//            strHtmlMensajeFinales += '<div style="font-size: 12px;text-align:left; ">TIPO DE ENVIO: ' + textTipoEnvioCarritoTransfer + ' </div>';
//            //            // Mostrar fecha de cierre
//            //            if (textTipoEnvioCarritoTransfer == 'Reparto') {
//            //                strHtmlMensajeFinales += '<div id="divRespuestaDePedidoTransferHorarioCierre" style="font-size:12px;text-align:left;margin-top:10px;margin-bottom:10px;">' + '</div>'; //Su pedido cerrará a las ' + pValor.Login +
//            //            }
//            //            // fin mostrar fecha cierre

//            // fin Mensaje
//            strHtml += '<div style="font-size: 14px; margin-top: 10px; width: 100%;text-align:left;background-color: #E5F3E4;color:#0B890A;">';
//            strHtml += ' <b>PRODUCTOS FACURADOS </b>';
//            strHtml += '</div>';
//            strHtml += '<table class="tbl-buscador-productos"  style="width:100% !important;" border="0" cellspacing="0" cellpadding="0">';
//            strHtml += '<tr>';
//            strHtml += '<th align="left" style="background-color:#EBEBEB;color:#000;">';
//            strHtml += 'Nombre producto';
//            strHtml += '</th>';
//            strHtml += '<th  style="background-color:#EBEBEB;color:#000;">';
//            strHtml += 'Cantidad';
//            strHtml += '</th>';
//            strHtml += '</tr>';
//            var cantDeInteracion = 0;
//            for (var i = 0; i < MaxLength; i++) {
//                for (var x = 0; x < pValor[i].Items.length; x++) {
//                    var strHtmlColorFondo = '';
//                    if (cantDeInteracion % 2 != 0) {
//                        strHtmlColorFondo = ' bp-td-color';
//                    }
//                    cantDeInteracion++;
//                    isProductosTransferPedidos = true;
//                    strHtml += '<tr>';

//                    strHtml += '<td align="left" style="text-align:left !important;"  class="' + strHtmlColorFondo + '">';
//                    strHtml += pValor[i].Items[x].NombreObjetoComercial;
//                    strHtml += '</td>';
//                    strHtml += '<td  class="' + strHtmlColorFondo + '">';
//                    strHtml += pValor[i].Items[x].Cantidad;
//                    strHtml += '</td>';
//                    strHtml += '</tr>';
//                }
//                if (i != (MaxLength - 1)) {
//                    strHtml += '<tr>';
//                    strHtml += '<td align="left"colspan="2">';
//                    strHtml += '<div style="border-bottom: 5px solid #333333;line-height: 27px;width: 100%;"></div>';
//                    strHtml += '</td>';
//                    strHtml += '</tr>';
//                }
//                MontoTotal += pValor[i].MontoTotal;
//            }
//            strHtml += '</table>';
//            strHtml += '<div  style="font-size: 12px;text-align:left;margin-top:2px;background-color: #E5F3E4;"><b>MONTO TOTAL:</b>  <span style="color:#0B890A;"> $ ' + FormatoDecimalConDivisorMiles(MontoTotal.toFixed(2)) + ' </span>  </div>';

//            if (MaxLength == LengthMenosUno) {
//                strHtmlEnRevision += '<div style="font-size: 14px; margin-top: 10px; width: 100%;text-align:left;background-color:#F7E7E8;color:#B3000C;">';
//                strHtmlEnRevision += ' <b>PRODUCTOS PENDIENTES EN FACTURACION </b>';
//                strHtmlEnRevision += '</div>';
//                strHtmlEnRevision += '<table class="tbl-buscador-productos"  style="width:100% !important;" border="0" cellspacing="0" cellpadding="0">';
//                strHtmlEnRevision += '<tr>';
//                strHtmlEnRevision += '<th align="left" style="background-color:#EBEBEB;color:#000;">';
//                strHtmlEnRevision += 'Nombre producto';
//                strHtmlEnRevision += '</th>';
//                strHtmlEnRevision += '<th style="background-color:#EBEBEB;color:#000;">';
//                strHtmlEnRevision += 'Cantidad';
//                strHtmlEnRevision += '</th>';
//                strHtmlEnRevision += '</tr>';

//                for (var x = 0; x < pValor[LengthMenosUno].Items.length; x++) {
//                    var strHtmlColorFondo = '';
//                    if (x % 2 != 0) {
//                        strHtmlColorFondo = ' bp-td-color';
//                    }
//                    isProductosTransferPedidosRevision = true;
//                    strHtmlEnRevision += '<tr>';
//                    strHtmlEnRevision += '<td align="left" style="text-align:left !important;" class="' + strHtmlColorFondo + '">';
//                    strHtmlEnRevision += pValor[LengthMenosUno].Items[x].NombreObjetoComercial;
//                    strHtmlEnRevision += '</td>';
//                    strHtmlEnRevision += '<td class="' + strHtmlColorFondo + '">';
//                    strHtmlEnRevision += pValor[LengthMenosUno].Items[x].Cantidad;
//                    strHtmlEnRevision += '</td>';
//                    strHtmlEnRevision += '</tr>';
//                }
//                strHtmlEnRevision += '</table>';
//            }
//            //}
//            // Fin Lista Revision 
//        } // fin  if (pValor.length > 0) 
//        else {
//            strHtmlMensajeFinales = '<div style="font-size: 14px;text-align:left;margin-top:10px;">' + 'Su pedido ha sido enviado con éxito a la sucursal' + ' </div>';
//        }
//    } else {  // fin if (pValor != null){
//        // Se produjo un error
//        //        $('#divRespuestaProductosPedidos').html('Error');
//    }
//    if (isProductosTransferPedidos) {
//        $('#divRespuestaProductosPedidos').html(strHtml);

//    } else {
//        $('#divRespuestaProductosPedidos').html('');
//    }


//    $('#divRespuestaFaltantes').html(strHtmlMensajeFinales);


//    $('#divRespuestaProblemasCrediticios').html(strHtmlEnRevision);

//    document.getElementById('divConfirmarPedidoContenedorGeneral').style.display = 'none';
//    document.getElementById('divRespuestaPedidoContenedorGeneral').style.display = 'block';
//    $('#divCargandoContenedorGeneralFondo').css('display', 'none');
//    // fin actualizar horario cierre
//}
function CargarHtmlTipoEnvioTransfer(pSucursal) {
    var strHtml = '';
    strHtml += '<table>';
    strHtml += '<tr>';
    strHtml += '<td>';
    strHtml += 'Tipo Envio:';
    strHtml += '</td>';
    strHtml += '<td>';
    strHtml += '<select id="comboTipoEnvioTransfer" class="select_gral">';
    strHtml += CargarHtmlOptionTipoEnvio(pSucursal);
    strHtml += '</select>';
    strHtml += '</td>';
    strHtml += '</tr>';
    strHtml += '</table>';
    $('#divTipoEnvioTransfer').html(strHtml);
}

