var listaArchivoSubidos = null;
var listaSucursal = null;
var listaProductosBuscados = null;
var clienteIsGLN = null;
var isClienteTomaTransfer = null;
var isClienteTomaOferta = null;
var selectedInput = null;
var listaProductosAPedir = null;
var listaProductosMostrarTransfer = [];
var sucursalEleginda = null;
//
var Ascender_pro_nombre = true;
var Ascender_pro_precio = true;
var Ascender_PrecioFinal = true;
var Ascender_PrecioConDescuentoOferta = true;
var Ascender_nroordenamiento = true;
var Ascender_PrecioConTransfer = true;
//
jQuery(document).ready(function () {
    $(document).keydown(function (e) {
        if (!e) {
            e = window.event;
        }
        teclaPresionada_enPagina(e);
    });

    if (listaArchivoSubidos == null) {
        listaArchivoSubidos = eval('(' + $('#hiddenListaArchivoSubidos').val() + ')');
        if (typeof listaArchivoSubidos == 'undefined') {
            listaArchivoSubidos = null;
        }
    }
    if (sucursalEleginda == null) {
        sucursalEleginda = $('#hiddenSucursalEleginda').val();
        if (typeof sucursalEleginda == 'undefined') {
            sucursalEleginda = null;
        }
    }

    //     GLN
    if (clienteIsGLN == null) {
        clienteIsGLN = $('#hiddenCli_isGLN').val();
        if (typeof clienteIsGLN == 'undefined') {
            clienteIsGLN = null;
        } else {
            if (clienteIsGLN == 'true') {
                clienteIsGLN = true;
            } else if (clienteIsGLN == 'false') {
                clienteIsGLN = false;
            } else {
                clienteIsGLN = null;
            }
        }
    }
    //  fin   GLN
    // Transfer
    if (isClienteTomaTransfer == null) {
        isClienteTomaTransfer = $('#hiddenCli_tomaTransfers').val();
        if (typeof isClienteTomaTransfer == 'undefined') {
            isClienteTomaTransfer = false;
        } else {
            if (isClienteTomaTransfer == 'true') {
                isClienteTomaTransfer = true;
            } else if (isClienteTomaTransfer == 'false') {
                isClienteTomaTransfer = false;
            } else {
                isClienteTomaTransfer = false;
            }
        }
    }
    // fin Transfer
    // Oferta
    if (isClienteTomaOferta == null) {
        isClienteTomaOferta = $('#hiddenCli_tomaOfertas').val();
        if (typeof isClienteTomaOferta == 'undefined') {
            isClienteTomaOferta = false;
        } else {
            if (isClienteTomaOferta == 'true') {
                isClienteTomaOferta = true;
            } else if (isClienteTomaOferta == 'false') {
                isClienteTomaOferta = false;
            } else {
                isClienteTomaOferta = false;
            }
        }
    } // fin oferta
    // CargarHtmlHistorialArchivos();
    // EstablecerVariableTipoSucursal();

    PageMethods.RecuperarProductosSubirPedisos(OnCallBackRecuperarProductosSubirPedisos, OnFail);
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

//function CargarHtml_ElejirSucursal() {
//    var elementos = document.getElementsByName('RadioTipoSucursal');
//    var sucursal = '';
//    for (var i = 0; i < elementos.length; i++) {
//        if (elementos[i].checked) {
//            sucursal = elementos[i].value;
//            break;
//        }
//    }
//    $('#HiddenFieldSucursalEleginda').val(sucursal);
//    //    alert(sucursal);
//}


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
//function onclickCerrarSubirArchivo() {
//    PageMethods.BorrarListaProductosSubirPedisos(OnCallBackBorrarListaProductosSubirPedisos, OnFail);
//    //    $('#divCargandoContenedorGeneralFondo').css('display', 'none');
//    //    document.getElementById('divResultadoBuscador').innerHTML = '';
//}
//function OnCallBackBorrarListaProductosSubirPedisos(args) {
//}
function volverSubirArchivo() {
    location.href = 'subirpedido_v2.aspx';
}
function OnCallBackRecuperarProductosSubirPedisos(args) {
    listaProductosBuscados = null;
    if (args != null) {
        if (args != '') {
            var strHtml = '';
            args = eval('(' + args + ')');
            listaSucursal = args.listaSucursal;
            listaProductosBuscados = args.listaProductos;

            cantMaxFila = listaProductosBuscados.length;
            cantMaxColumna = listaSucursal.length;
            if (listaProductosBuscados.length > 0) {
                //
                strHtml += '<br />';
                strHtml += '<table style="width:100%;" border="0" cellspacing="0" cellpadding="0">';
                strHtml += '<tr>';
                strHtml += '<td > ';
                strHtml += ' <button class="btn_gral_120" onclick="CargarOfertas();return false;" style="float:left;"> Ordenar por Ofertas </button>'; //+ '</th>'
                strHtml += ' <button class="btn_gral_120" onclick="OrdenarTransfer();return false;" style="float:left;"> Ordenar por Transfer </button>';
                strHtml += ' <button class="btn_gral" onclick="CargarListaSinStock();return false;" style="float:left;"> Listar Faltantes </button>';
                strHtml += '</td>'; //
                strHtml += '<td > ';
                strHtml += '<input type="button" onclick="volverSubirArchivo()" value="VOLVER" class="btn_gral" />'; //+ '</th>'
                strHtml += '</td>'; //
                strHtml += '</tr>';
                strHtml += '</table>';
                strHtml += '<br />';
                //
                //                strHtml += '<br /><input type="button" onclick="volverSubirArchivo()" value="VOLVER" class="btn_gral" />';
                //                strHtml += '<br /><br /><br />';
                strHtml += '<table class="tbl-buscador-productos tableSubirArchivo"  border="0" cellspacing="0" cellpadding="0">';
                strHtml += '<thead  class="theadSubirArchivo">';
                strHtml += '<tr>'; //bp-off-top-left 
                strHtml += '<th class="thOrdenar colNombre " rowspan="2" onclick="onclickOrdenarProducto(-1)" ><div class="bp-top-leftSubir">Detalle Producto</div></th>'; //
                strHtml += '<th class=" thOrdenar colOrden " rowspan="2" onclick="onclickOrdenarProducto(3)" > Orden</th>'; //
                strHtml += '<th class=" thOrdenar colPrecioPublico " rowspan="2" onclick="onclickOrdenarProducto(0)" > Precio Público</th>'; //
                strHtml += '<th class=" thOrdenar colPrecioCliente " rowspan="2" onclick="onclickOrdenarProducto(1)" >Precio Cliente</th>'; //
                strHtml += '<th class="bg-oferta" colspan="3">Oferta</th>';
                //NUEVO
                strHtml += '<th class="bg-oferta" colspan="2">Transfer</th>';
                //FIN NUEVO
                for (var iEncabezadoSucursal = 0; iEncabezadoSucursal < listaSucursal.length; iEncabezadoSucursal++) {
                    strHtml += '<th class="bp-ancho colSucursal" rowspan="2">';
                    strHtml += ConvertirSucursalParaColumno(listaSucursal[iEncabezadoSucursal]);
                    strHtml += '</th>';
                }

                strHtml += '</tr>';

                strHtml += '<tr>';
                strHtml += '<th class=" colOfertaProcentaje ">%</th>';
                strHtml += '<th class=" colOfertaCantidad ">Cant.</th>';
                strHtml += '<th class=" thOrdenar colOfertaPrecio " onclick="onclickOrdenarProducto(2)">Precio</th>'; //
                //NUEVO
                strHtml += '<th class=" colTransferCondicion ">&nbsp;&nbsp;Cond.&nbsp;&nbsp;</th>';
                strHtml += '<th class=" thOrdenar colTransferPrecio " onclick="onclickOrdenarProducto(4)">Precio</th>';
                //FIN NUEVO 
                strHtml += '</tr>';
                strHtml += '</thead>';
                strHtml += '</table>';

                strHtml += '<table class="tbl-buscador-productos tableSubirArchivo"  border="0" cellspacing="0" cellpadding="0">';
                strHtml += '<tbody class="tbodySubirArchivo">';
                for (var i = 0; i < listaProductosBuscados.length; i++) {
                    var isRemarcarFila = false;
                    for (var iEncabezadoSucursal = 0; iEncabezadoSucursal < listaSucursal.length; iEncabezadoSucursal++) {
                        var varValores = ObtenerCantidadProductoDependiendoTransfer(i, listaSucursal[iEncabezadoSucursal], listaProductosBuscados[i].cantidad);
                        if ((varValores[1] == 0 && varValores[2]) || (varValores[0] > 0 && varValores[3])) {
                            isRemarcarFila = true;
                            break;
                        }
                    }

                    var isNotGLNisTrazable = false;
                    var isMostrarImput = true;
                    if (clienteIsGLN != null) {
                        if (!clienteIsGLN) {
                            if (listaProductosBuscados[i].pro_isTrazable) {
                                isNotGLNisTrazable = true;
                                isMostrarImput = false;
                            }
                        }
                    }
                    strHtml += '<tr >';
                    var strHtmlColorFondo = '';
                    if (i % 2 != 0) {
                        strHtmlColorFondo = ' bp-td-color';
                    }
                    if (listaProductosBuscados[i].isProductoNoEncontrado) {
                        strHtmlColorFondo = ' bp-td-colorProductoNoEncontrado';
                    }
                    if (isRemarcarFila && isClienteTomaTransfer && listaProductosBuscados[i].isMostrarTransfersEnClientesPerf && listaProductosBuscados[i].isPermitirPedirProducto) {
                        strHtmlColorFondo = ' bp-td-colorProductoDestacar';
                    }

                    strHtml += '<td class="first-td2 cssFilaBuscadorDesmarcar cssFilaBuscador_' + i + ' ' + strHtmlColorFondo + ' colNombreTd">';
                    strHtml += listaProductosBuscados[i].pro_nombre;
                    // Agregar:
                    if (isNotGLNisTrazable) {
                        strHtml += '<span class="spanProductoTrazableCLiSinGLN" >&nbsp;&nbsp;&nbsp;Producto trazable. Farmacia sin GLN.</span>'; //style="padding-left:4px;"
                    }
                    if (isClienteTomaTransfer && listaProductosBuscados[i].isMostrarTransfersEnClientesPerf) {

                        if (listaProductosBuscados[i].isTieneTransfer) {
                            strHtml += '<span>&nbsp;&nbsp;&raquo;&nbsp;Transfer Combo</span>';
                        } else if (listaProductosBuscados[i].isProductoFacturacionDirecta) {
                            strHtml += '<span>&nbsp;&nbsp;&raquo;&nbsp;TRF</span>';
                        }
                        // Mostrar solo producto Transfer 
                        if (listaProductosBuscados[i].pro_vtasolotransfer) {
                            strHtml += '<span class="spanProductoTrazableCLiSinGLN" >&nbsp;&nbsp;&nbsp;Se vende solo por transfer</span>';
                        }
                    }
                    // Vale Psicotropicos
                    if (listaProductosBuscados[i].isValePsicotropicos) {
                        strHtml += '<span class="spanProductoTrazableCLiSinGLN" >&nbsp;&nbsp;&nbsp; Requiere Vale</span>';
                        isMostrarImput = false;
                    }
                    // Producto erroneo
                    if (listaProductosBuscados[i].isProductoNoEncontrado) {
                        strHtml += '<span class="spanProductoErroneo" >&nbsp;&nbsp;&nbsp; Registro erróneo</span>';
                    }
                    // FIn Producto erroneo
                    // FIN Vale Psicotropicos
                    // + IVA
                    if (listaProductosBuscados[i].pro_neto) {//&nbsp;&nbsp;&nbsp;
                        strHtml += '<span class="spanProductoIVA" >&nbsp;&nbsp;&nbsp; + IVA</span>';
                    }
                    // FIN + IVA
                    //
                    if (!listaProductosBuscados[i].isProductoNoEncontrado && !listaProductosBuscados[i].isPermitirPedirProducto) {
                        strHtml += '<span class="spanProductoErroneo" >&nbsp;&nbsp;&nbsp; Producto no habilitado</span>';
                    }
                    //
                    // Ver si mostrar input solo producto Transfer 
                    if (listaProductosBuscados[i].pro_vtasolotransfer || !listaProductosBuscados[i].isPermitirPedirProducto) {
                        isMostrarImput = false;
                    }
                    // strHtml += '</div>';
                    strHtml += '</td>'; //<span>&raquo;En transfer</span>
                    // ORDEN
                    strHtml += '<td style="text-align:center;"  class="' + strHtmlColorFondo + ' cssFilaBuscadorDesmarcar cssFilaBuscador_' + i + ' colOrdenTd">&nbsp; ' + listaProductosBuscados[i].nroordenamiento + '&nbsp; </td>';
                    // FIN ORDEN
                    var precioColumna = '';
                    var precioFinalColumna = '';
                    // Ver si un producto esta
                    if (!listaProductosBuscados[i].isProductoNoEncontrado) {
                        // Producto ncontrado
                        precioColumna = '$&nbsp;' + FormatoDecimalConDivisorMiles(listaProductosBuscados[i].pro_precio.toFixed(2));
                        if (listaProductosBuscados[i].pro_precio == 0) {
                            precioColumna = '';
                        }

                        precioFinalColumna = '$&nbsp;' + FormatoDecimalConDivisorMiles(listaProductosBuscados[i].PrecioFinal.toFixed(2));
                        // 15/02/2018 mail de luciana para el producto
                        if (listaProductosBuscados[i].pro_nombre.match("^PAÑAL PAMI AD")) {
                            precioFinalColumna = '$&nbsp;' + FormatoDecimalConDivisorMiles(listaProductosBuscados[i].pro_preciofarmacia.toFixed(2));
                        }
                        else {
                            precioFinalColumna = '$&nbsp;' + FormatoDecimalConDivisorMiles(listaProductosBuscados[i].PrecioFinal.toFixed(2));
                        }
                        // fin 15/02/2018 mail de luciana para el producto
                    }
                    strHtml += '<td style="text-align:right;"  class="' + strHtmlColorFondo + ' cssFilaBuscadorDesmarcar cssFilaBuscador_' + i + ' colPrecioPublicoTd">' + precioColumna + '</td>';
                    strHtml += '<td style="text-align:right;"  class="' + strHtmlColorFondo + ' cssFilaBuscadorDesmarcar  cssFilaBuscador_' + i + ' colPrecioClienteTd">' + precioFinalColumna + '</td>';

                    var varOfeunidades = ' &nbsp; ';
                    var varOfeporcentaje = ' &nbsp; ';
                    var varPrecioConDescuentoOferta = ' &nbsp; ';
                    if (isClienteTomaOferta) {
                        if (listaProductosBuscados[i].pro_ofeunidades != 0 || listaProductosBuscados[i].pro_ofeporcentaje != 0) {
                            varOfeunidades = listaProductosBuscados[i].pro_ofeunidades;
                            varOfeporcentaje = listaProductosBuscados[i].pro_ofeporcentaje;
                            varPrecioConDescuentoOferta = '$&nbsp;' + FormatoDecimalConDivisorMiles(listaProductosBuscados[i].PrecioConDescuentoOferta.toFixed(2));
                        }
                    }
                    strHtml += '<td class="' + strHtmlColorFondo + ' cssFilaBuscadorDesmarcar  cssFilaBuscador_' + i + ' colOfertaProcentajeTd">' + varOfeporcentaje + '</td>';
                    strHtml += '<td class="' + strHtmlColorFondo + ' cssFilaBuscadorDesmarcar  cssFilaBuscador_' + i + ' colOfertaCantidadTd">' + varOfeunidades + '</td>';
                    strHtml += '<td style="text-align:right;"  class="' + strHtmlColorFondo + ' cssFilaBuscadorDesmarcar  cssFilaBuscador_' + i + ' colOfertaPrecioTd">' + varPrecioConDescuentoOferta + '</td>';
                    // NUEVO Transfer facturacion directa
                    var varTransferFacturacionDirectaCondicion = '';
                    var varTransferFacturacionDirectaPrecio = '';
                    if (isClienteTomaTransfer && listaProductosBuscados[i].isMostrarTransfersEnClientesPerf) {
                        if (listaProductosBuscados[i].isProductoFacturacionDirecta) {
                            varTransferFacturacionDirectaCondicion = listaProductosBuscados[i].tde_unidadesbonificadasdescripcion;
                            varTransferFacturacionDirectaPrecio = '$&nbsp;' + FormatoDecimalConDivisorMiles(listaProductosBuscados[i].PrecioFinalTransfer.toFixed(2));
                        }
                    }
                    strHtml += '<td class="' + strHtmlColorFondo + ' cssFilaBuscadorDesmarcar  cssFilaBuscador_' + i + ' colTransferCondicionTd">';
                    if (varTransferFacturacionDirectaCondicion != '') {
                        strHtml += '<div  OnMouseMove="OnMouseMoveProdructoFacturacionDirecta(event)" OnMouseOver="OnMouseOverProdructoFacturacionDirecta(' + i + ')" OnMouseOut="OnMouseOutProdructoFacturacionDirecta()"  style="cursor:pointer;" >'
                        strHtml += varTransferFacturacionDirectaCondicion;
                        strHtml += '</div>';
                        //   strHtml += varTransferFacturacionDirectaCondicion;
                    }
                    strHtml += '</td>'; //NUEVO
                    strHtml += '<td class="' + strHtmlColorFondo + ' cssFilaBuscadorDesmarcar  cssFilaBuscador_' + i + ' colTransferPrecioTd">' + varTransferFacturacionDirectaPrecio + '</td>';  //NUEVO
                    // NUEVO Transfer facturacion directa

                    // Optimizar
                    for (var iEncabezadoSucursal = 0; iEncabezadoSucursal < listaSucursal.length; iEncabezadoSucursal++) {
                        strHtml += '<td  class="' + strHtmlColorFondo + ' cssFilaBuscadorDesmarcar  cssFilaBuscador_' + i + ' colSucursalTd"  >';
                        //                        if (intPaginadorTipoDeRecuperar != 2) {// todo transfer
                        for (var iSucursal = 0; iSucursal < listaProductosBuscados[i].listaSucursalStocks.length; iSucursal++) {
                            if (listaProductosBuscados[i].listaSucursalStocks[iSucursal].stk_codsuc == listaSucursal[iEncabezadoSucursal]) {
                                strHtml += '<div class="cont-estado-input">';
                                strHtml += '<div id="estado' + i + '_' + iEncabezadoSucursal + '" class="estado-' + listaProductosBuscados[i].listaSucursalStocks[iSucursal].stk_stock.toLowerCase() + '"></div>';
                                if (isMostrarImput) {

                                    var cantidadDeProductoEnCarrito = '';
                                    cantidadDeProductoEnCarrito = listaProductosBuscados[i].listaSucursalStocks[iSucursal].cantidadSucursal; //listaProductosBuscados[i].cantidad; // ObtenerCantidadProducto(listaSucursal[iEncabezadoSucursal], listaProductosBuscados[i].pro_codigo);
                                    strHtml += '<input class="cssFocusCantProdCarrito" id="inputSuc' + i + "_" + iEncabezadoSucursal + '" type="text"  onfocus="onfocusSucursal(this)" onblur="onblurSucursal(this)" onkeypress="return onKeypressCantProductos(event)"   value="' + cantidadDeProductoEnCarrito + '" ></input>';
                                }
                                strHtml += '</div>';
                                break;
                            }
                        }
                        //                        } // fin      if (intPaginadorTipoDeRecuperar != 2) { 
                        strHtml += '</td>';
                    }
                    strHtml += '</tr>';
                }
                strHtml += '</tbody>';
                strHtml += '</table>';
                ///////
                strHtml += '<table class="tbl-buscador-productosParteBaja"  border="0" cellspacing="0" cellpadding="0">';
                strHtml += '<tr>';
                strHtml += '<td > ';
                //                strHtml += ' <button class="btn_gral_120" onclick="CargarOfertas();return false;" style="float:left;"> Ordenar por Ofertas </button>'; //+ '</th>'
                //                strHtml += ' <button class="btn_gral" onclick="CargarListaSinStock();return false;" style="float:left;"> Listar Faltantes </button>';
                strHtml += '</td>'; //
                strHtml += '<td  > </td>'; //
                strHtml += '<td ></td>'; //
                strHtml += '<td   style="text-align:right;" > <b>Renglones:</b></td>'; //colspan="3"
                for (var iEncabezadoSucursal = 0; iEncabezadoSucursal < listaSucursal.length; iEncabezadoSucursal++) {
                    strHtml += '<td id="tdRenglones_' + iEncabezadoSucursal + '" style="width:50px;border-right:1px solid #dedede;border-left:1px solid #dedede;" class="colSucursalPie">'; // rowspan="2"

                    strHtml += '</td>';
                }
                strHtml += '</tr>';

                strHtml += '<tr>';
                strHtml += '<td > ';
                strHtml += '</td>'; //
                strHtml += '<td  > </td>'; //
                strHtml += '<td ></td>'; //
                strHtml += '<td align="right" style="text-align:right;" ><b>Unidades:</b></td>'; //colspan="3"
                for (var iEncabezadoSucursal = 0; iEncabezadoSucursal < listaSucursal.length; iEncabezadoSucursal++) {
                    strHtml += '<td id="tdUnidades_' + iEncabezadoSucursal + '" style="width:50px;border-right:1px solid #dedede;border-left:1px solid #dedede;" class="colSucursalPie">'; // rowspan="2"
                    strHtml += '</td>';
                }

                strHtml += '</tr>';

                strHtml += '</table>';
                strHtml += '<div style="text-align:right;margin: 10px;" >' + '<button class="btn_gral" style="float: none;margin-top:10px;" onclick="CargarPedido(); return false;"> Cargar pedido </button>' + '</div>';
                //////

            } else {
                strHtml += '<div style="margin: 10px; font-size:11px;color: #ed1125;" ><b>La búsqueda no arroja resultados</b></div>';
            }


            // Mostrar div resultado
            //            var arraySizeDocumento = SizeDocumento();
            //            document.getElementById('divCargandoContenedorGeneralFondo').style.height = arraySizeDocumento[1] + 'px';
            //            $('#divCargandoContenedorGeneralFondo').css('display', 'block'); //.css('display', 'none');

            document.getElementById('divResultadoBuscador').innerHTML = strHtml;

            setTimeout(function () { CargarUnidadesRenglones(); }, 300);
            //            $('#divResultadoBuscador').css('display', 'block');
            //            $('#divResultadoBuscador').css('position', 'absolute');
            //
            // Elejir el primer producto
            //            if ($('#inputSuc0_0').length) {
            //                $('#inputSuc0_0').focus();
            //                //$('#inputSuc0_0').select();
            //                selectedInput = document.getElementById('inputSuc0_0');
            //            }
        }
    }
}

function CargarOfertas() {
    PageMethods.RecuperarProductosOrdenar('PrecioConDescuentoOferta', false, OnCallBackRecuperarProductosSubirPedisos, OnFail);

}

function OrdenarTransfer() {
    PageMethods.RecuperarProductosOrdenar('PrecioConTransfer', false, OnCallBackRecuperarProductosSubirPedisos, OnFail);

}

function onKeypressCantProductos(pEvent) {

    pEvent = pEvent || window.event;
    var code = pEvent.charCode || pEvent.keyCode;
    //    alert(code);
    //    code == 8  borrar <---
    //    code == 9 tab
    //    code == 46 Supr
    //    code == 37 <-
    //    code == 39 ->
    if (code == 8 || code == 9 || code == 46 || code == 37 || code == 39) {
        return true;
    }
    if (code < 48 || code > 57) {
        return false; // pEvent.returnValue
    }
    return true;
}


function onfocusSucursal(pValor) {

    selectInputCarrito = null;
    selectedInputTransfer = null;
    selectedInput = pValor;
    setTimeout(function () { selectedInput.select(); MarcarFilaSeleccionada(pValor); }, 20);

}
function onblurSucursal(pValor) {
    var isCambioValor = false;
    var nombre = pValor.id;
    nombre = nombre.replace('inputSuc', '');
    var palabrasBase = nombre.split("_");
    var fila = parseInt(palabrasBase[0]);

    var columna = parseInt(palabrasBase[1]);
    if (pValor.value != '') {
        var isNotMaximaCantidadSuperada = true;
        if (listaProductosBuscados[fila].pro_canmaxima != null) {
            if (listaProductosBuscados[fila].pro_canmaxima < pValor.value) {
                isNotMaximaCantidadSuperada = false;
            }
        }
        if (isNotMaximaCantidadSuperada) {
            AgregarAlHistorialProductoCarrito(fila, columna, pValor.value, true);
            isCambioValor = true;
        } else {
            alert(MostrarTextoSuperaCantidadMaxima(listaProductosBuscados[fila].pro_nombre, listaProductosBuscados[fila].pro_canmaxima));
            var cantidadAnterior = ObtenerCantidadProducto(listaSucursal[columna], listaProductosBuscados[fila].pro_codigo);
            pValor.value = cantidadAnterior;
        }
    } else {
        AgregarAlHistorialProductoCarrito(fila, columna, 0, true);
        isCambioValor = true;
    }
    CargarUnidadesRenglones();

    // Actualizar bp-td-colorProductoDestacar
    if (isCambioValor && listaProductosBuscados[fila].isPermitirPedirProducto && listaProductosBuscados[fila].isMostrarTransfersEnClientesPerf) {
        var isRemarcarFila = false;
        var varValores = ObtenerCantidadProductoDependiendoTransfer(fila, listaSucursal[columna], pValor.value);
        if ((varValores[1] == 0 && varValores[2]) || (varValores[0] > 0 && varValores[3])) {
            isRemarcarFila = true;
        }
        if (isRemarcarFila) {
            $('.cssFilaBuscador_' + fila).addClass('bp-td-colorProductoDestacar');
        } else {
            $('.cssFilaBuscador_' + fila).removeClass('bp-td-colorProductoDestacar');
        }
    }
    // Fin Actualizar bp-td-colorProductoDestacar
}


function ObtenerCantidadProducto(pIdSucursal, pIdProduco) {
    var resultado = '';
    //    if (listaCarritos != null) {
    //        for (var i = 0; i < listaCarritos.length; i++) {
    //            if (listaCarritos[i].codSucursal == pIdSucursal) {
    //                for (var iProducto = 0; iProducto < listaCarritos[i].listaProductos.length; iProducto++) {
    //                    if (listaCarritos[i].listaProductos[iProducto].codProducto == pIdProduco) {
    //                        resultado = listaCarritos[i].listaProductos[iProducto].cantidad;
    //                        break;
    //                    }
    //                }
    //                break;
    //            } // for (var i = 0; i < listaCarritos.length; i++) {
    //        }
    //    }
    return resultado;
}

function MostrarTextoSuperaCantidadMaxima(pNombreProducto, pCantidadMaxima) {
    return 'El producto: ' + pNombreProducto + ' \n' + 'Supera la cantidad máxima: ' + pCantidadMaxima;
}
function DesmarcarFilaSeleccionada() {
    $('.cssFilaBuscadorDesmarcar').removeClass('borderFilaBuscadorSeleccionada');
}
function MarcarFilaSeleccionada(pValor) {
    DesmarcarFilaSeleccionada();
    var nombre = pValor.id;
    nombre = nombre.replace('inputSuc', '');
    var palabrasBase = nombre.split("_");
    var fila = parseInt(palabrasBase[0]);
    $('.cssFilaBuscador_' + fila).addClass('borderFilaBuscadorSeleccionada');
}

function AgregarAlHistorialProductoCarrito(pIndexProducto, pIndexSucursal, pCantidadProducto, pIsSumarCantidad) {
    for (var iSucursal = 0; iSucursal < listaProductosBuscados[pIndexProducto].listaSucursalStocks.length; iSucursal++) {
        if (listaProductosBuscados[pIndexProducto].listaSucursalStocks[iSucursal].stk_codsuc == listaSucursal[pIndexSucursal]) {

            listaProductosBuscados[pIndexProducto].listaSucursalStocks[iSucursal].cantidadSucursal = pCantidadProducto;


            PageMethods.ModificarCantidadProductos(pIndexProducto, iSucursal, pCantidadProducto, OnCallBackModificarCantidadProductos, OnFail);

            //            var mytext = $("#inputSuc" + pIndexProducto + "_" + pIndexSucursal);
            //            if (mytext.length > 0) {
            //                // 
            //            } else {
            //                mytext = null;
            //            }
            //            if (mytext != null) {
            //                // mytext.focus();
            //                var valor = mytext.val();
            //                if (valor != '') {
            //                    var nroValor = parseInt(valor);
            //                    listaProductosBuscados[pIndexProducto].listaSucursalStocks[iSucursal].cantidadSucursal = nroValor;
            //                } else {
            //                    listaProductosBuscados[pIndexProducto].listaSucursalStocks[iSucursal].cantidadSucursal = '';
            //                }
            //            }
            break;
        } // fin        if (listaProductosBuscados[pIndexProducto].listaSucursalStocks[iSucursal].stk_codsuc == listaSucursal[pIndexProducto]) {

    } // fin  for (var iSucursal = 0; iSucursal < listaProductosBuscados[pIndexProducto].listaSucursalStocks.length; iSucursal++) {
}
function OnCallBackModificarCantidadProductos(args) {

}
function teclaPresionada_enPagina(e) {
    if (typeof (e) == 'undefined') {
        e = event;
    }
    var keyCode = document.all ? e.which : e.keyCode;
    if (keyCode == 37 || keyCode == 39 || keyCode == 40 || keyCode == 38 || keyCode == 13) {
        //  alert(selectedInput);
        if (selectedInput != null) {
            if (selectedInput.id != undefined) {
                if (keyCode == 13) {
                    onblurSucursal(selectedInput);
                    jQuery("#txtBuscador").val('');
                    onClickBuscar();
                    //                    jQuery("#txtBuscador").focus();
                    document.getElementById('txtBuscador').focus();
                    //                    var cat = document.getElementById("datalistAutocompletar");
                    //                    cat.style.display = "none";
                    return;
                }
                var fila = 0;
                var columna = 0;

                var nombre = selectedInput.id;
                nombre = nombre.replace('inputSuc', '');
                var palabrasBase = nombre.split("_");
                fila = parseInt(palabrasBase[0]);
                columna = parseInt(palabrasBase[1]);
                var mytext = null;
                while (mytext == null) {
                    var isSalirWhile = false;
                    switch (keyCode) {
                        case 37: //izquierda
                            if (columna != 0) {
                                columna--;
                            }
                            else {
                                isSalirWhile = true;
                            }
                            break;
                        case 38: //arriba
                            if (fila != 0) {
                                fila--;
                            } else {
                                isSalirWhile = true;
                            }
                            break;
                        case 39: //derecha
                            if (columna < cantMaxColumna - 1) {
                                columna++;
                            } else {
                                isSalirWhile = true;
                            }
                            break;
                        case 40: //abajo
                            if (fila < cantMaxFila - 1) {
                                fila++;
                            } else {
                                isSalirWhile = true;
                            }
                            break;
                        default:
                            break;
                    }
                    if (isSalirWhile) {
                        break;
                    }
                    mytext = $("#inputSuc" + fila + "_" + columna); //.length //document.getElementById("inputSuc" + fila + "_" + columna);
                    if (mytext.length > 0) {
                        //                        var ff = 0;
                        //                        alert('si ' + fila + ' '+ columna);
                    } else {
                        //                        alert('no ' + fila + ' ' + columna);
                        mytext = null;
                    }
                }
                if (mytext != null) {
                    mytext.focus();
                    //                    mytext.select();
                }
            }
        }
    }
    return true;
}

function CargarPedido() {

    if (listaProductosBuscados != null && listaSucursal != null) {
        listaProductosAPedir = [];
        listaProductosMostrarTransfer = [];
        var strHtmlMensaje = '';
        for (var iProducto = 0; iProducto < listaProductosBuscados.length; iProducto++) {
            for (var iSucursal = 0; iSucursal < listaSucursal.length; iSucursal++) {
                var mytext = $("#inputSuc" + iProducto + "_" + iSucursal);
                if (mytext.length > 0) {
                    // 
                } else {
                    mytext = null;
                }
                if (mytext != null) {
                    // mytext.focus();
                    var valor = mytext.val();
                    //                    if (valor != '') {
                    //                        var pro = new cProductosAndCantidad();
                    //                        pro.cantidad = mytext.val();
                    //                        pro.codProducto = listaProductosBuscados[iProducto].pro_codigo;
                    //                        pro.codSucursal = listaSucursal[iSucursal];
                    //                        listaProductosAPedir.push(pro);
                    //                    }
                    var varValores = [];
                    if (valor != '') {
                        varValores = ObtenerCantidadProductoDependiendoTransfer(iProducto, iSucursal, parseInt(mytext.val()));
                    }
                    if (varValores.length > 0) {
                        if (varValores[1] == 0 && varValores[2]) {
                            strHtmlMensaje += '<div>' + listaProductosBuscados[iProducto].pro_nombre + '</div>';
                        }
                        if (varValores[0] > 0) {
                            var proComun = new cProductosAndCantidad();
                            proComun.cantidad = varValores[0];
                            proComun.codProducto = listaProductosBuscados[iProducto].pro_codigo;
                            proComun.isMostrarTransfersEnClientesPerf = listaProductosBuscados[iProducto].isMostrarTransfersEnClientesPerf;
                            proComun.codSucursal = listaSucursal[iSucursal];
                            listaProductosAPedir.push(proComun);
                        }
                        if (varValores[1] > 0) {
                            var proTransfer = new cProductosAndCantidad();
                            proTransfer.cantidad = varValores[1];
                            proTransfer.codProducto = listaProductosBuscados[iProducto].pro_codigo;
                            proTransfer.codProductoNombre = listaProductosBuscados[iProducto].pro_nombre;
                            proTransfer.codSucursal = listaSucursal[iSucursal];
                            proTransfer.isTransferFacturacionDirecta = true;
                            proTransfer.isMostrarTransfersEnClientesPerf = listaProductosBuscados[iProducto].isMostrarTransfersEnClientesPerf;
                            proTransfer.tde_codtfr = listaProductosBuscados[iProducto].tde_codtfr;
                            listaProductosAPedir.push(proTransfer);
                        }
                        if (varValores[1] == 0 && varValores[2]) {
                            var proTransferMsg = new cProductosAndCantidad();
                            proTransferMsg.isTransfer = true;
                            proTransferMsg.cantidadMostrar = parseInt(mytext.val());
                            proTransferMsg.cantidad = varValores[1];
                            proTransferMsg.codProducto = listaProductosBuscados[iProducto].pro_codigo;
                            proTransferMsg.pro_nombre = listaProductosBuscados[iProducto].pro_nombre;
                            proTransferMsg.tde_unidadesbonificadasdescripcion = listaProductosBuscados[iProducto].tde_unidadesbonificadasdescripcion;
                            proTransferMsg.codSucursal = listaSucursal[iSucursal];
                            proTransferMsg.isTransferFacturacionDirecta = true;
                            proTransferMsg.isMostrarTransfersEnClientesPerf = listaProductosBuscados[iProducto].isMostrarTransfersEnClientesPerf;
                            proTransferMsg.tde_codtfr = listaProductosBuscados[iProducto].tde_codtfr;
                            listaProductosMostrarTransfer.push(proTransferMsg);
                        }
                        if (varValores[0] > 0 && varValores[3]) {
                            var proTransferMsg = new cProductosAndCantidad();
                            proTransferMsg.isTransfer = false;
                            proTransferMsg.cantidadMostrar = parseInt(mytext.val());
                            //proTransferMsg.cantidad = varValores[1];
                            proTransferMsg.codProducto = listaProductosBuscados[iProducto].pro_codigo;
                            proTransferMsg.pro_nombre = listaProductosBuscados[iProducto].pro_nombre;
                            //proTransferMsg.tde_unidadesbonificadasdescripcion = listaProductosBuscados[iProducto].tde_unidadesbonificadasdescripcion;
                            proTransferMsg.pro_ofeunidades = listaProductosBuscados[iProducto].pro_ofeunidades;
                            proTransferMsg.codSucursal = listaSucursal[iSucursal];
                            //proTransferMsg.isTransferFacturacionDirecta = true;
                            proTransferMsg.isMostrarTransfersEnClientesPerf = listaProductosBuscados[iProducto].isMostrarTransfersEnClientesPerf;
                            proTransferMsg.tde_codtfr = listaProductosBuscados[iProducto].tde_codtfr;
                            listaProductosMostrarTransfer.push(proTransferMsg);
                        }
                    }
                }
            } // fin  for (var iSucursal = 0; iSucursal < listaSucursal.length; iSucursal++) {
        } // fin for (var iProducto = 0; iProducto < listaProductosBuscados.length; iProducto++) {
        if (listaProductosAPedir.length > 0) {
            var strHtmlMensaje = '';
            if (isClienteTomaTransfer) {
                strHtmlMensaje = CargarListaProductosFaltaCantidadParaTransfer(listaProductosMostrarTransfer);
            }
            if (strHtmlMensaje == '') {
                $('#divLoaderGeneralFondo').css('display', 'block');
                var arraySizeDocumento = SizeDocumento();
                document.getElementById('divCargandoContenedorGeneralFondo').style.height = arraySizeDocumento[1] + 'px';

                PageMethods.ActualizarProductoCarritoSubirArchivo(listaProductosAPedir, OnCallBackActualizarProductoCarritoSubirArchivo, OnFailActualizarProductoCarritoSubirArchivo);
            } else {

            }
        }
        else {
            MostrarMensajeGeneral("Información", "Necesita cargar algún producto");
        }
    } // fin if (listaProductosBuscados != null && listaSucursal != null) {
}
function OnFailActualizarProductoCarritoSubirArchivo(ex) {
    OnFail(ex);
    location.href = 'PedidosBuscador.aspx';
}
function OnCallBackActualizarProductoCarritoSubirArchivo(args) {
    PageMethods.BorrarListaProductosSubirPedisos(OnCallBackBorrarListaProductosSubirPedisos, OnFail);
}
function OnCallBackBorrarListaProductosSubirPedisos(args) {
    location.href = 'PedidosBuscador.aspx';
}
function CargarUnidadesRenglones() {
    if (listaProductosBuscados != null && listaSucursal != null) {
        $('#divUnidadesRenglones').html('');
        listaProductosAPedir = [];
        var listaSucursalesUnidadesRenglones = [];
        for (var iSucursal = 0; iSucursal < listaSucursal.length; iSucursal++) {
            var obj = new cUnidadesAndRenglones();
            obj.Unidades = 0;
            obj.Renglones = 0;
            obj.codSucursal = listaSucursal[iSucursal];
            listaSucursalesUnidadesRenglones.push(obj);
        }
        for (var iProducto = 0; iProducto < listaProductosBuscados.length; iProducto++) {
            for (var iSucursal = 0; iSucursal < listaSucursal.length; iSucursal++) {
                var mytext = $("#inputSuc" + iProducto + "_" + iSucursal);
                if (mytext.length > 0) {
                    // 
                } else {
                    mytext = null;
                }

                if (mytext != null) {
                    // mytext.focus();
                    var valor = mytext.val();
                    if (valor != '') {

                        var nroValor = parseInt(valor);
                        if (nroValor > 0) {
                            listaSucursalesUnidadesRenglones[iSucursal].Unidades += nroValor;
                            listaSucursalesUnidadesRenglones[iSucursal].Renglones += 1;
                        }
                    }
                } // fin  for (var iSucursal = 0; iSucursal < listaSucursal.length; iSucursal++) {
            }  // fin for (var iProducto = 0; iProducto < listaProductosBuscados.length; iProducto++) {
        } // fin if (listaProductosBuscados != null && listaSucursal != null) {
        //
        for (var iSucursal = 0; iSucursal < listaSucursalesUnidadesRenglones.length; iSucursal++) {


            $('#tdUnidades_' + iSucursal).html(listaSucursalesUnidadesRenglones[iSucursal].Unidades);
            $('#tdRenglones_' + iSucursal).html(listaSucursalesUnidadesRenglones[iSucursal].Renglones);

        }
    }
}
function CargarListaProductosFaltaCantidadParaTransfer(pListaProductosFaltaCantidadParaTransfer) {
    var strHtml = '';
    var isMostrar = false;
    strHtml += '<a class="cerrarBotonTransferContenedor" href="#" onclick="onclickCerrarFondoTransfer(); ">';
    strHtml += '[CERRAR] </a>';
    strHtml += '<div style="font-size: 20px; padding: 10px;text-align:left;color:green;">';
    strHtml += ' Productos que no cumplen con la condición mínima de Ofertas/Transfers';
    strHtml += '</div>';
    for (var iEncabezadoSucursal = 0; iEncabezadoSucursal < listaSucursal.length; iEncabezadoSucursal++) {
        var isMostrarSucursal = false;
        var strHtmlSuc = '';
        strHtmlSuc += '<div style="font-size: 16px; padding: 10px;text-align:left; ">';
        strHtmlSuc += ConvertirSucursalParaColumno(listaSucursal[iEncabezadoSucursal]);
        strHtmlSuc += '</div>';
        strHtmlSuc += '<table  class="tbl-buscador-productos"  border="0" cellspacing="0" cellpadding="0" style="padding: 10px;">';
        strHtmlSuc += '<tr>';
        strHtmlSuc += '<th class="bp-med-ancho"  align="left" >'; //width="400px"
        strHtmlSuc += 'Nombre producto';
        strHtmlSuc += '<th class="bp-med-ancho"  align="left"  >'; // width="50px"
        strHtmlSuc += 'Condición';
        strHtmlSuc += '</th>';
        strHtmlSuc += '<th class="bp-med-ancho"  align="left"  >'; // width="50px"
        strHtmlSuc += 'Cantidad';
        strHtmlSuc += '</th>';
        strHtmlSuc += '</tr>';
        for (var iProductosParaTransfer = 0; iProductosParaTransfer < pListaProductosFaltaCantidadParaTransfer.length; iProductosParaTransfer++) {
            if (pListaProductosFaltaCantidadParaTransfer[iProductosParaTransfer].codSucursal == listaSucursal[iEncabezadoSucursal] && pListaProductosFaltaCantidadParaTransfer[iProductosParaTransfer].isMostrarTransfersEnClientesPerf) {
                var strHtmlColorFondo = '';
                if (iProductosParaTransfer % 2 != 0) {
                    strHtmlColorFondo = ' bp-td-color';
                }
                isMostrar = true;
                isMostrarSucursal = true;
                if (pListaProductosFaltaCantidadParaTransfer[iProductosParaTransfer].isTransfer ) {

                    strHtmlSuc += '<tr>';
                    strHtmlSuc += '<td  class="' + strHtmlColorFondo + '" style="text-align:left;width:400px;">';
                    strHtmlSuc += pListaProductosFaltaCantidadParaTransfer[iProductosParaTransfer].pro_nombre; //  + '<br/>';
                    strHtmlSuc += '</td>';
                    strHtmlSuc += '<td class="' + strHtmlColorFondo + '" style="text-align:left;width:200px;">';
                    strHtmlSuc += pListaProductosFaltaCantidadParaTransfer[iProductosParaTransfer].tde_unidadesbonificadasdescripcion; //  + '<br/>';
                    strHtmlSuc += '</td>';
                    strHtmlSuc += '<td class="' + strHtmlColorFondo + '" style="text-align:left;width:200px;">';
                    strHtmlSuc += pListaProductosFaltaCantidadParaTransfer[iProductosParaTransfer].cantidadMostrar; //  + '<br/>';
                    strHtmlSuc += '</td>';
                    strHtmlSuc += '</tr>';
                } else {

                    strHtmlSuc += '<tr>';
                    strHtmlSuc += '<td  class="' + strHtmlColorFondo + '" style="text-align:left;width:400px;">';
                    strHtmlSuc += pListaProductosFaltaCantidadParaTransfer[iProductosParaTransfer].pro_nombre; //  + '<br/>';
                    strHtmlSuc += '</td>';
                    strHtmlSuc += '<td class="' + strHtmlColorFondo + '" style="text-align:left;width:200px;">';
                    strHtmlSuc += 'Mín.' + pListaProductosFaltaCantidadParaTransfer[iProductosParaTransfer].pro_ofeunidades; //  + '<br/>';
                    strHtmlSuc += '</td>';
                    strHtmlSuc += '<td class="' + strHtmlColorFondo + '" style="text-align:left;width:200px;">';
                    strHtmlSuc += pListaProductosFaltaCantidadParaTransfer[iProductosParaTransfer].cantidadMostrar; //  + '<br/>';
                    strHtmlSuc += '</td>';
                    strHtmlSuc += '</tr>';
                }
            }
        }
        strHtmlSuc += '</table>';
        if (isMostrarSucursal) {
            strHtml += strHtmlSuc;
        }
    }

    if (isMostrar) {
        strHtml += '<div style="text-align:right;margin: 10px;" >' + '<button class="btn_gral" style="float: none;margin-top:10px;" onclick="CargarPedidoIgualmente(); return false;"> Cargar pedido </button>' + '&nbsp;&nbsp;&nbsp;' + '<button class="btn_gral" style="float: none;margin-top:10px;" onclick="onclickCerrarFondoTransfer(); return false;"> Modificar pedido </button>' + '</div>';
        $('#divProductosFaltaCantidadParaTransfer').html(strHtml);
        $('#divProductosFaltaCantidadParaTransfer').css('display', 'block');
        var arraySizeDocumento = SizeDocumento();
        document.getElementById('divCargandoContenedorGeneralFondoTransfer').style.height = arraySizeDocumento[1] + 'px';
        $('#divCargandoContenedorGeneralFondoTransfer').css('display', 'block');
    } else {
        strHtml = '';
    }
    return strHtml;
}
function CargarPedidoIgualmente() {
    if (listaProductosAPedir.length > 0) {
        $('#divLoaderGeneralFondo').css('display', 'block');
        var arraySizeDocumento = SizeDocumento();
        document.getElementById('divCargandoContenedorGeneralFondo').style.height = arraySizeDocumento[1] + 'px';

        PageMethods.ActualizarProductoCarritoSubirArchivo(listaProductosAPedir, OnCallBackActualizarProductoCarritoSubirArchivo, OnFailActualizarProductoCarritoSubirArchivo);
    }
}

function CargarListaSinStock() {
    var strHtml = '';
    var listaSucursalFaltantes = [];
    for (var iEncabezadoSucursal = 0; iEncabezadoSucursal < listaSucursal.length; iEncabezadoSucursal++) {
        var obj = [];
        listaSucursalFaltantes.push(obj);
    }
    for (var i = 0; i < listaProductosBuscados.length; i++) {
        for (var iEncabezadoSucursal = 0; iEncabezadoSucursal < listaSucursal.length; iEncabezadoSucursal++) {
            for (var iSucursal = 0; iSucursal < listaProductosBuscados[i].listaSucursalStocks.length; iSucursal++) {
                if (listaProductosBuscados[i].listaSucursalStocks[iSucursal].stk_codsuc == listaSucursal[iEncabezadoSucursal]) {
                    if (listaProductosBuscados[i].listaSucursalStocks[iSucursal].stk_stock.toLowerCase() == 'n') {
                        // Cantidad
                        // var mytext = $("#estado" + i + "_" + iEncabezadoSucursal);inputSuc' + i + "_" + iEncabezadoSucursal + 
                        var mytext = $("#inputSuc" + i + "_" + iEncabezadoSucursal);
                        if (mytext.length > 0) {
                            // 
                        } else {
                            mytext = null;
                        }
                        if (mytext != null) {

                            if (listaProductosBuscados[i].listaSucursalStocks[iSucursal].cantidadSucursal != '') {
                                if (listaProductosBuscados[i].listaSucursalStocks[iSucursal].cantidadSucursal > 0) {
                                    var objProductoStock = '';
                                    objProductoStock = listaProductosBuscados[i];
                                    listaSucursalFaltantes[iEncabezadoSucursal].push(objProductoStock);
                                }
                            }
                        }
                        // fin cantidad

                    }
                    break;
                }
            }
        }
    }
    //
    strHtml += '<a class="cerrarBotonTransferContenedor" href="#" onclick="onclickCerrarFondo()">';
    strHtml += '[CERRAR]</a>';
    strHtml += '<div style="font-size: 20px; padding: 10px;text-align:left;color:green; ">';
    strHtml += ' Productos en Falta';
    strHtml += '</div>';
    var isMostrar = false;
    for (var iEncabezadoSucursal = 0; iEncabezadoSucursal < listaSucursal.length; iEncabezadoSucursal++) {
        var isMostrarSucursal = false;
        var strHtmlSuc = '';
        strHtmlSuc += '<div style="font-size: 16px; padding: 10px;text-align:left; ">';
        strHtmlSuc += ConvertirSucursalParaColumno(listaSucursal[iEncabezadoSucursal]); // listaSucursal[iEncabezadoSucursal];
        strHtmlSuc += '</div>';
        strHtmlSuc += '<table  class="tbl-buscador-productos"  border="0" cellspacing="0" cellpadding="0" style="padding: 10px;">';
        strHtmlSuc += '<tr>';
        strHtmlSuc += '<th class="bp-med-ancho"  align="left" >'; //width="400px"
        strHtmlSuc += 'Nombre producto';
        strHtmlSuc += '</th>';
        strHtmlSuc += '<th class="bp-med-ancho"  align="left"  >'; // width="50px"
        strHtmlSuc += 'Cantidad';
        strHtmlSuc += '</th>';
        strHtmlSuc += '</tr>';


        for (var iProducto = 0; iProducto < listaSucursalFaltantes[iEncabezadoSucursal].length; iProducto++) {
            var strHtmlColorFondo = '';
            if (iProducto % 2 != 0) {
                strHtmlColorFondo = ' bp-td-color';
            }
            isMostrar = true;
            isMostrarSucursal = true;
            strHtmlSuc += '<tr>';
            strHtmlSuc += '<td  class="' + strHtmlColorFondo + '" style="text-align:left;width:400px;">';
            strHtmlSuc += listaSucursalFaltantes[iEncabezadoSucursal][iProducto].pro_nombre; //  + '<br/>';
            strHtmlSuc += '</td>';
            strHtmlSuc += '<td class="' + strHtmlColorFondo + '" style="text-align:left;width:200px;">';
            strHtmlSuc += listaSucursalFaltantes[iEncabezadoSucursal][iProducto].listaSucursalStocks[iEncabezadoSucursal].cantidadSucursal; //  + '<br/>';
            strHtmlSuc += '</td>';
            strHtmlSuc += '</tr>';
        }
        strHtmlSuc += '</table>';
        //strHtml += '<br/>';
        if (isMostrarSucursal) {

            strHtml += strHtmlSuc;
        }
    }


    if (isMostrar) {
        strHtml += '';
        $('#divFaltantes').html(strHtml);
        $('#divFaltantes').css('display', 'block');
        var arraySizeDocumento = SizeDocumento();
        document.getElementById('divCargandoContenedorGeneralFondo').style.height = arraySizeDocumento[1] + 'px';
        $('#divCargandoContenedorGeneralFondo').css('display', 'block');
    }

}
function onclickCerrarFondoTransfer() {

    $('#divProductosFaltaCantidadParaTransfer').css('display', 'none');
    $('#divCargandoContenedorGeneralFondoTransfer').css('display', 'none');
}
function onclickCerrarFondo() {
    $('#divFaltantes').css('display', 'none');
    $('#divCargandoContenedorGeneralFondo').css('display', 'none');
}
function cProductosAndCantidad() {
    this.codSucursal = '';
    this.codProducto = '';
    this.codProductoNombre = '';
    this.cantidad = 0;
    this.isTransferFacturacionDirecta = false;
    this.isMostrarTransfersEnClientesPerf = true;
    this.tde_codtfr = 0;
}
function cUnidadesAndRenglones() {
    this.codSucursal = '';
    this.Unidades = 0;
    this.Renglones = 0;
}

function onclickOrdenarProducto(pValor) {
    //    alert(pValor);
    if (pValor == -1) {
        PageMethods.RecuperarProductosOrdenar('pro_nombre', Ascender_pro_nombre, OnCallBackRecuperarProductosSubirPedisos, OnFail);
        Ascender_pro_nombre = !Ascender_pro_nombre;
    }
    if (pValor == 0) {
        PageMethods.RecuperarProductosOrdenar('pro_precio', Ascender_pro_precio, OnCallBackRecuperarProductosSubirPedisos, OnFail);
        Ascender_pro_precio = !Ascender_pro_precio;
    }
    if (pValor == 1) {
        PageMethods.RecuperarProductosOrdenar('PrecioFinal', Ascender_PrecioFinal, OnCallBackRecuperarProductosSubirPedisos, OnFail);
        Ascender_PrecioFinal = !Ascender_PrecioFinal;
    }
    if (pValor == 2) {
        PageMethods.RecuperarProductosOrdenar('PrecioConDescuentoOferta', Ascender_PrecioConDescuentoOferta, OnCallBackRecuperarProductosSubirPedisos, OnFail);
        Ascender_PrecioConDescuentoOferta = !Ascender_PrecioConDescuentoOferta;
    }
    if (pValor == 3) {
        PageMethods.RecuperarProductosOrdenar('nroordenamiento', Ascender_nroordenamiento, OnCallBackRecuperarProductosSubirPedisos, OnFail);
        Ascender_nroordenamiento = !Ascender_nroordenamiento;
    }
    if (pValor == 4) {
        PageMethods.RecuperarProductosOrdenar('PrecioConTransfer', Ascender_PrecioConTransfer, OnCallBackRecuperarProductosSubirPedisos, OnFail);
        Ascender_PrecioConTransfer = !Ascender_PrecioConTransfer;
    }


    //    listaProductosBuscados[i]. // 0
    //    listaProductosBuscados[i].PrecioFinal // 1
    //    listaProductosBuscados[i].PrecioConDescuentoOferta // 2
}
function ObtenerCantidadProductoDependiendoTransfer(pFila, pSucursal, pCantidad) {
    var resultadoReturn = [];
    var isPasarDirectamente = false;
    var cantidadCarritoTransfer = 0;
    var cantidadCarritoComun = 0;
    var ProductoIsTransferDirecto = false;
    var IsProductoMostrarFaltaCantidadOferta = false;

    if (listaProductosBuscados[pFila].isProductoFacturacionDirecta) { // facturacion directa
        // Combiene transfer o promocion
        var precioConDescuentoDependiendoCantidad = CalcularPrecioProductosEnCarrito(listaProductosBuscados[pFila].PrecioFinal, pCantidad, listaProductosBuscados[pFila].pro_ofeunidades, listaProductosBuscados[pFila].pro_ofeporcentaje);
        if (pCantidad == 0) {
            //
        } else {
            precioConDescuentoDependiendoCantidad = precioConDescuentoDependiendoCantidad / pCantidad;
        }
        if (parseFloat(precioConDescuentoDependiendoCantidad) > parseFloat(listaProductosBuscados[pFila].PrecioFinalTransfer)) {
            var isSumarTransfer = false;

            if (listaProductosBuscados[pFila].tde_muluni != null && listaProductosBuscados[pFila].tde_unidadesbonificadas != null) {
                ProductoIsTransferDirecto = true;
                /// UNIDAD MULTIPLO Y BONIFICADA
                if ((pCantidad >= listaProductosBuscados[pFila].tde_muluni) && (pCantidad <= (listaProductosBuscados[pFila].tde_muluni + listaProductosBuscados[pFila].tde_unidadesbonificadas))) {
                    // es multiplo
                    isSumarTransfer = true;
                    cantidadCarritoTransfer = listaProductosBuscados[pFila].tde_muluni + listaProductosBuscados[pFila].tde_unidadesbonificadas;
                } else if (pCantidad > (listaProductosBuscados[pFila].tde_muluni + listaProductosBuscados[pFila].tde_unidadesbonificadas)) {
                    isSumarTransfer = true;
                    var cantidadMultiplicar = parseInt(pCantidad / listaProductosBuscados[pFila].tde_muluni);
                    cantidadCarritoTransfer = cantidadMultiplicar * (listaProductosBuscados[pFila].tde_muluni + listaProductosBuscados[pFila].tde_unidadesbonificadas);
                    //
                    for (var iCantMulti = 0; iCantMulti < cantidadMultiplicar; iCantMulti++) {
                        var cantTemp = iCantMulti * (listaProductosBuscados[pFila].tde_muluni + listaProductosBuscados[pFila].tde_unidadesbonificadas);
                        if (cantTemp >= pCantidad) {
                            cantidadCarritoTransfer = cantTemp; //  iCantMulti * (listaProductosBuscados[pFila].tde_muluni + listaProductosBuscados[pFila].tde_unidadesbonificadas);
                            break;
                        }
                    }
                    //
                    if (cantidadCarritoTransfer == pCantidad) {

                    } else {
                        if (pCantidad < cantidadCarritoTransfer) {
                            cantidadCarritoComun = 0;
                        } else {
                            cantidadCarritoComun = pCantidad - cantidadCarritoTransfer;
                        }
                        if ((cantidadCarritoComun >= listaProductosBuscados[pFila].tde_muluni) && (cantidadCarritoComun <= (listaProductosBuscados[pFila].tde_muluni + listaProductosBuscados[pFila].tde_unidadesbonificadas))) {
                            cantidadCarritoTransfer += listaProductosBuscados[pFila].tde_muluni + listaProductosBuscados[pFila].tde_unidadesbonificadas;
                            cantidadCarritoComun = 0;
                        }
                    }
                }
                if (isSumarTransfer) {

                } else {
                    isPasarDirectamente = true;
                }
                /// FIN UNIDAD MULTIPLO Y BONIFICADA
            } // fin if (listaProductosBuscados[pFila].tde_muluni != null && listaProductosBuscados[pFila].tde_unidadesbonificadas != null){
            else if (listaProductosBuscados[pFila].tde_fijuni != null) {
                ProductoIsTransferDirecto = true;
                // UNIDAD FIJA
                if (pCantidad == listaProductosBuscados[pFila].tde_fijuni) {
                    isSumarTransfer = true;
                    cantidadCarritoTransfer = listaProductosBuscados[pFila].tde_fijuni;
                } else if (pCantidad > listaProductosBuscados[pFila].tde_fijuni) {
                    isSumarTransfer = true;
                    cantidadCarritoTransfer = listaProductosBuscados[pFila].tde_fijuni;
                    cantidadCarritoComun = pCantidad - listaProductosBuscados[pFila].tde_fijuni;
                }
                if (isSumarTransfer) {

                } else {
                    isPasarDirectamente = true;
                }
                // FIN UNIDAD FIJA
            } else if (listaProductosBuscados[pFila].tde_minuni != null && listaProductosBuscados[pFila].tde_maxuni != null) {
                ProductoIsTransferDirecto = true;
                // UNIDAD MAXIMA Y MINIMA
                if (listaProductosBuscados[pFila].tde_minuni <= pCantidad && listaProductosBuscados[pFila].tde_maxuni >= pCantidad) {
                    isSumarTransfer = true;
                    cantidadCarritoTransfer = pCantidad;
                } else if (listaProductosBuscados[pFila].tde_maxuni < pCantidad) {
                    isSumarTransfer = true;
                    cantidadCarritoTransfer = listaProductosBuscados[pFila].tde_maxuni;
                    cantidadCarritoComun = pCantidad - listaProductosBuscados[pFila].tde_maxuni;
                }

                if (isSumarTransfer) {

                } else {
                    isPasarDirectamente = true;
                }
                // FIN UNIDAD MAXIMA Y MINIMA
            }
            else if (listaProductosBuscados[pFila].tde_minuni != null) {
                ProductoIsTransferDirecto = true;
                // UNIDAD MINIMA
                if (listaProductosBuscados[pFila].tde_minuni <= pCantidad) {
                    isSumarTransfer = true;
                    cantidadCarritoTransfer = pCantidad;
                }
                if (isSumarTransfer) {

                } else {
                    isPasarDirectamente = true;
                }
                // FIN UNIDAD MINIMA
            }
        } // fin if (listaProductosBuscados[pFila].PrecioConDescuentoOferta > listaProductosBuscados[pFila].PrecioFinalTransfer){
        else {
            isPasarDirectamente = true;
            // is oferta
            if (pCantidad < listaProductosBuscados[pFila].pro_ofeunidades) {
                IsProductoMostrarFaltaCantidadOferta = true;
            }
        }
    } else {
        isPasarDirectamente = true;
        if (pCantidad < listaProductosBuscados[pFila].pro_ofeunidades) {
            IsProductoMostrarFaltaCantidadOferta = true;
        }
    }
    if (isPasarDirectamente) {
        cantidadCarritoComun = parseInt(pCantidad);
    }
    resultadoReturn.push(cantidadCarritoComun);
    resultadoReturn.push(cantidadCarritoTransfer);
    resultadoReturn.push(ProductoIsTransferDirecto);
    resultadoReturn.push(IsProductoMostrarFaltaCantidadOferta);
    return resultadoReturn;
}
function CalcularPrecioProductosEnCarrito(pPrecioFinal, pCantidad, pOfertaPorUnidad, pOfertaPorcentaje) {
    var resultado = 0.0;
    if (isClienteTomaOferta) {
        if (pOfertaPorUnidad == 0 || pOfertaPorcentaje == 0) {
            resultado = parseFloat(pCantidad) * pPrecioFinal;
        } else {
            if (pOfertaPorUnidad > pCantidad) {
                resultado = parseFloat(pCantidad) * pPrecioFinal;
            } else {
                resultado = parseFloat(pCantidad) * (pPrecioFinal * (1 - (parseFloat(pOfertaPorcentaje) / parseFloat(100))));
            }
        }
    } else {
        // Cliente si permiso para tomar oferta
        resultado = parseFloat(pCantidad) * pPrecioFinal;
    }
    return resultado;
}

/// Facturacion directa detalle muestra
var timerProductoFacturacionDirecta = null;
function OnMouseOverProdructoFacturacionDirecta(pIndice) {
    if ($("#divMostradorDetalleTransferFacturacionDirecta").css("display") == 'none') {
        LimpiarTimeoutProductoFacturacionDirecta();
        timerProductoFacturacionDirecta = setTimeout(function () { AnimarPresentacionProductoFacturacionDirecta(pIndice); }, 300);
    }
}
function OnMouseOutProdructoFacturacionDirecta() {
    if ($("#divMostradorDetalleTransferFacturacionDirecta").css("display") == 'block') {
        $("#divMostradorDetalleTransferFacturacionDirecta").css("display", "none");
    }
    LimpiarTimeoutProductoFacturacionDirecta();
}
function LimpiarTimeoutProductoFacturacionDirecta() {
    if (timerProductoFacturacionDirecta) {
        clearTimeout(timerProductoFacturacionDirecta);
        timerProductoFacturacionDirecta = null;
    }
}
function AnimarPresentacionProductoFacturacionDirecta(pIndice) {
    CargarDatosProductosFacturacionDirecta(pIndice);
    $("#divMostradorDetalleTransferFacturacionDirecta").css("display", "block");
    LimpiarTimeoutProductoFacturacionDirecta();
}
function OnMouseMoveProdructoFacturacionDirecta(e) {
    if (typeof (e) == 'undefined') {
        e = event;
    }
    var bt = document.body.scrollTop;
    var et = document.documentElement ? document.documentElement.scrollTop : null;
    var top = e.clientY || e.pageY;
    var left = e.clientX || e.pageX;

    $("#divMostradorDetalleTransferFacturacionDirecta").css("top", (top + (bt || et) + 20) + 'px');
    $("#divMostradorDetalleTransferFacturacionDirecta").css("left", (left + 20) + 'px');
}
function CargarDatosProductosFacturacionDirecta(pIndice) {
    if (listaProductosBuscados[pIndice].tde_minuni != null) {
        $('#tdUnidadMinima').html(listaProductosBuscados[pIndice].tde_minuni); //listaProductosBuscados
    } else {
        $('#tdUnidadMinima').html('');
    }
    if (listaProductosBuscados[pIndice].tde_maxuni != null) {
        $('#tdUnidadMaxima').html(listaProductosBuscados[pIndice].tde_maxuni);
    } else {
        $('#tdUnidadMaxima').html('');
    }
    if (listaProductosBuscados[pIndice].tde_muluni != null) {
        $('#tdMultiploUnidades').html(listaProductosBuscados[pIndice].tde_muluni);
    } else {
        $('#tdMultiploUnidades').html('');
    }
    if (listaProductosBuscados[pIndice].tde_fijuni != null) {
        $('#tdUnidadesFijas').html(listaProductosBuscados[pIndice].tde_fijuni);
    } else {
        $('#tdUnidadesFijas').html('');
    }
    if (listaProductosBuscados[pIndice].tde_unidadesbonificadas != null) {
        $('#tdUnidadesBonificadas').html(listaProductosBuscados[pIndice].tde_unidadesbonificadas);
    } else {
        $('#tdUnidadesBonificadas').html('');
    }
    //    if (listaProductosBuscados[pIndice].tde_unidadesbonificadasdescripcion != null) {
    //        $('#tdDescripcionUnidadesBonificadas').html(listaProductosBuscados[pIndice].tde_unidadesbonificadasdescripcion);
    //    } else {
    //        $('#tdDescripcionUnidadesBonificadas').html('');
    //    }
    if (listaProductosBuscados[pIndice].tde_descripcion != null) {
        $('#tdDescripcionUnidadesBonificadas').html(listaProductosBuscados[pIndice].tde_descripcion);
    } else {
        $('#tdDescripcionUnidadesBonificadas').html('');
    }
    if (listaProductosBuscados[pIndice].tfr_descripcion != null) {
        $('#tdDescripcionTransfer').html(listaProductosBuscados[pIndice].tfr_descripcion);
    } else {
        $('#tdDescripcionTransfer').html('');
    }
}
/// Fin facturacion directa detalle muestra
