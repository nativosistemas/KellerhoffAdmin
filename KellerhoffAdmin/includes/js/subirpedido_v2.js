var listaArchivoSubidos = null;
var listaSucursal = null;
var listaProductosBuscados = null;
var clienteIsGLN = null;
var isClienteTomaTransfer = null;
var isClienteTomaOferta = null;
var selectedInput = null;
var listaProductosAPedir = null;
var diasPedidos = 0;
var historialPrimerRegistro = null;

jQuery(document).ready(function () {
    $(document).keydown(function (e) {
        if (!e) {
            e = window.event;
        }
        teclaPresionada_enPagina(e);
    });

    if (historialPrimerRegistro == null) {
        historialPrimerRegistro = eval('(' + $('#hiddenPrimerArchivoSubidos').val() + ')');
        if (typeof historialPrimerRegistro == 'undefined') {
            historialPrimerRegistro = null;
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
    //   CargarHtmlHistorialArchivos();
    //    EstablecerVariableTipoSucursal();
    CargarDatoPrimerHistorial();
    //PageMethods.RecuperarProductosSubirPedisos(OnCallBackRecuperarProductosSubirPedisos, OnFail);
});

function CargarDatoPrimerHistorial() {

    if (historialPrimerRegistro != null) {
        var strHtmlUltimoPedido = '';
        strHtmlUltimoPedido += '<table style="font-size:11px;padding-top:10px;" >'; //color:#000;
        strHtmlUltimoPedido += '<tr>';
        strHtmlUltimoPedido += '<td>' + 'Ultimo pedido realizado:' + '</td>';
        //            strHtmlUltimoPedido += '<td >' + '<a href="../../archivos/ArchivosPedidos/' + listaArchivoSubidos[0].has_NombreArchivo + '" TARGET="_blank">' + listaArchivoSubidos[0].has_NombreArchivoOriginal + '</a>' + '</td>';
        strHtmlUltimoPedido += '<td onclick="onclickCargar(' + historialPrimerRegistro.has_id + ')" style="color:#065BAB;cursor:pointer;" >' + historialPrimerRegistro.has_NombreArchivoOriginal + '</td>';
        var strNombreSucursal0 = '';
        if (historialPrimerRegistro.suc_nombre != null) {
            strNombreSucursal0 = historialPrimerRegistro.suc_nombre;
        }
        strHtmlUltimoPedido += '<td style="width:200px;">' + strNombreSucursal0 + '</td>';
        strHtmlUltimoPedido += '<td style="width:200px;">' + historialPrimerRegistro.has_fechaToString + '</td>';
        strHtmlUltimoPedido += '</tr>';
        strHtmlUltimoPedido += '</table>';
        $('#ultimoPedido').html(strHtmlUltimoPedido);
    }
}

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
    //    var isCargarDeNuevo = confirm('¿Desea subir el archivo de nuevo?');
    //    if (isCargarDeNuevo) {
    //        PageMethods.CargarArchivoPedidoDeNuevo(pValor, OnCallBackCargarArchivoPedidoDeNuevo, OnFail);
    //    }

    PageMethods.CargarArchivoPedidoDeNuevo(pValor, OnCallBackCargarArchivoPedidoDeNuevo, OnFailCargarArchivoPedidoDeNuevo);
    
    $('#divLoaderGeneralFondo').css('display', 'block');
    var arraySizeDocumento = SizeDocumento();
    document.getElementById('divCargandoContenedorGeneralFondo').style.height = arraySizeDocumento[1] + 'px';
}
function OnFailCargarArchivoPedidoDeNuevo(ex) {
    $('#divLoaderGeneralFondo').css('display', 'none');
    OnFail(ex);
}
function OnCallBackCargarArchivoPedidoDeNuevo(args) {
    $('#divLoaderGeneralFondo').css('display', 'none');
    if (args) {
        location.href = 'subirarchivoresultado_msg.aspx'; // 'subirarchivoresultado.aspx';
    } else {
        // $('#divCargandoContenedorGeneralFondo').css('display', 'none');
        alert('No se pudo subir archivo');
    }
}
function OnFailBackCargarArchivoPedidoDeNuevo(ex) {
    OnFail(ex);
    $('#divCargandoContenedorGeneralFondo').css('display', 'none');
}
function onclickCerrarSubirArchivo() {
    PageMethods.BorrarListaProductosSubirPedisos(OnCallBackBorrarListaProductosSubirPedisos, OnFail);
    //    $('#divCargandoContenedorGeneralFondo').css('display', 'none');
    //    document.getElementById('divResultadoBuscador').innerHTML = '';
}
//function OnCallBackBorrarListaProductosSubirPedisos(args) {

//}
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
                //  strHtml += '<a class="cerrarBotonTransferContenedor" href="#" onclick="onclickCerrarSubirArchivo()">  [CERRAR]</a>';  //onclick="CerrarContenedorTransfer()"

                strHtml += '<table class="tbl-buscador-productos"  border="0" cellspacing="0" cellpadding="0">';
                strHtml += '<tr>';
                strHtml += '<th class="bp-off-top-left thOrdenar" rowspan="2" ><div class="bp-top-left">Detalle Producto</div></th>'; //onclick="onclickOrdenarProducto(-1)"
                strHtml += '<th class="bp-med-ancho thOrdenar" rowspan="2"  > Precio Público</th>'; //onclick="onclickOrdenarProducto(0)"
                strHtml += '<th class="bp-med-ancho thOrdenar" rowspan="2"  >Precio Cliente</th>'; //onclick="onclickOrdenarProducto(1)"
                strHtml += '<th class="bg-oferta" colspan="3">Oferta</th>';
                //NUEVO
                strHtml += '<th class="bg-oferta" colspan="2">Transfer</th>';
                //FIN NUEVO
                for (var iEncabezadoSucursal = 0; iEncabezadoSucursal < listaSucursal.length; iEncabezadoSucursal++) {
                    strHtml += '<th class="bp-ancho" rowspan="2">';
                    strHtml += ConvertirSucursalParaColumno(listaSucursal[iEncabezadoSucursal]);
                    strHtml += '</th>';
                }

                strHtml += '</tr>';

                strHtml += '<tr>';
                strHtml += '<th class="bp-min-ancho">%</th>';
                strHtml += '<th class="bp-min-ancho">Cant.</th>';
                strHtml += '<th class="bp-med-ancho thOrdenar">Precio</th>'; // onclick="onclickOrdenarProducto(2)"
                //NUEVO
                strHtml += '<th class="bp-min-ancho ">&nbsp;&nbsp;Cond.&nbsp;&nbsp;</th>';
                strHtml += '<th class="bp-med-ancho thOrdenar" onclick="onclickOrdenarProducto(3)">Precio</th>';
                //FIN NUEVO
                strHtml += '</tr>';
                for (var i = 0; i < listaProductosBuscados.length; i++) {
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
                    strHtml += '<tr >';  //style="border: 1px solid black;"
                    strHtml += '<td class="first-td2 cssFilaBuscadorDesmarcar cssFilaBuscador_' + i + ' ';
                    var strHtmlColorFondo = '';
                    if (i % 2 != 0) {
                        strHtmlColorFondo = ' bp-td-color';
                    }
                    strHtml += strHtmlColorFondo + '">';
                    //                    strHtml += '<div  OnMouseMove="OnMouseMoveProdructo(event)" OnMouseOver="OnMouseOverProdructo(' + i + ')" OnMouseOut="OnMouseOutProdructo()" onclick="RecuperarTransfer(' + i + ')" style="cursor:pointer;" >' + listaProductosBuscados[i].pro_nombre;
                    strHtml += '<div   style="cursor:pointer;" >' + listaProductosBuscados[i].pro_nombre;
                    // Agregar:
                    if (isNotGLNisTrazable) {
                        strHtml += '<span class="spanProductoTrazableCLiSinGLN" >&nbsp;&nbsp;&nbsp;Producto trazable. Farmacia sin GLN.</span>'; //style="padding-left:4px;"
                    }
                    if (isClienteTomaTransfer) {
                        if (listaProductosBuscados[i].isTieneTransfer) {
                            strHtml += '<span>&nbsp;&nbsp;&raquo;&nbsp;En transfer</span>'; //style="padding-left:4px;"
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
                    // FIN Vale Psicotropicos
                    // + IVA
                    if (listaProductosBuscados[i].pro_neto) {//&nbsp;&nbsp;&nbsp;
                        strHtml += '<span class="spanProductoIVA" >&nbsp;&nbsp;&nbsp; + IVA</span>';
                    }
                    // FIN + IVA
                    // Ver si mostrar input solo producto Transfer 
                    if (listaProductosBuscados[i].pro_vtasolotransfer) {
                        isMostrarImput = false;
                    }
                    strHtml += '</div>';
                    strHtml += '</td>'; //<span>&raquo;En transfer</span>
                    strHtml += '<td style="text-align:right;"  class="' + strHtmlColorFondo + ' cssFilaBuscadorDesmarcar cssFilaBuscador_' + i + '">$&nbsp;' + FormatoDecimalConDivisorMiles(listaProductosBuscados[i].pro_precio.toFixed(2)) + '</td>';
                    strHtml += '<td style="text-align:right;"  class="' + strHtmlColorFondo + ' cssFilaBuscadorDesmarcar  cssFilaBuscador_' + i + '">$&nbsp;' + FormatoDecimalConDivisorMiles(listaProductosBuscados[i].PrecioFinal.toFixed(2)) + '</td>'; // listaProductosBuscados[i].PrecioDescuentoFarmacia.toFixed(2)
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
                    strHtml += '<td class="' + strHtmlColorFondo + ' cssFilaBuscadorDesmarcar  cssFilaBuscador_' + i + '">' + varOfeporcentaje + '</td>';
                    strHtml += '<td class="' + strHtmlColorFondo + ' cssFilaBuscadorDesmarcar  cssFilaBuscador_' + i + '">' + varOfeunidades + '</td>';
                    strHtml += '<td style="text-align:right;"  class="' + strHtmlColorFondo + ' cssFilaBuscadorDesmarcar  cssFilaBuscador_' + i + '">' + varPrecioConDescuentoOferta + '</td>';

                    // NUEVO Transfer facturacion directa
                        var varTransferFacturacionDirectaCondicion = '';
                        var varTransferFacturacionDirectaPrecio = '';
                        if (isClienteTomaTransfer) {
                            if (listaProductosBuscados[i].isProductoFacturacionDirecta) {
                                varTransferFacturacionDirectaCondicion = listaProductosBuscados[i].tde_unidadesbonificadasdescripcion;
                                varTransferFacturacionDirectaPrecio = '$&nbsp;' + FormatoDecimalConDivisorMiles(listaProductosBuscados[i].tde_predescuento.toFixed(2));
                            }
                        }
                        strHtml += '<td class="' + strHtmlColorFondo + ' cssFilaBuscadorDesmarcar  cssFilaBuscador_' + i + '">';
                        if (varTransferFacturacionDirectaCondicion != '') {
                           // strHtml += '<div  OnMouseMove="OnMouseMoveProdructoFacturacionDirecta(event)" OnMouseOver="OnMouseOverProdructoFacturacionDirecta(' + i + ')" OnMouseOut="OnMouseOutProdructoFacturacionDirecta()"  style="cursor:pointer;" >'
                            strHtml += varTransferFacturacionDirectaCondicion;
                           // strHtml += '</div>';
                        }
                        strHtml += '</td>'; //NUEVO
                        strHtml += '<td class="' + strHtmlColorFondo + ' cssFilaBuscadorDesmarcar  cssFilaBuscador_' + i + '">' + varTransferFacturacionDirectaPrecio + '</td>';  //NUEVO
                    // NUEVO Transfer facturacion directa

                    // Optimizar
                    for (var iEncabezadoSucursal = 0; iEncabezadoSucursal < listaSucursal.length; iEncabezadoSucursal++) {
                        strHtml += '<td  class="' + strHtmlColorFondo + ' cssFilaBuscadorDesmarcar  cssFilaBuscador_' + i + '"  >';
                        //                        if (intPaginadorTipoDeRecuperar != 2) {// todo transfer
                        for (var iSucursal = 0; iSucursal < listaProductosBuscados[i].listaSucursalStocks.length; iSucursal++) {
                            if (listaProductosBuscados[i].listaSucursalStocks[iSucursal].stk_codsuc == listaSucursal[iEncabezadoSucursal]) {
                                strHtml += '<div class="cont-estado-input"><div class="estado-' + listaProductosBuscados[i].listaSucursalStocks[iSucursal].stk_stock.toLowerCase() + '"></div>';

                                if (isMostrarImput) {
                                    // Cargar Cantidad
                                    var cantidadDeProductoEnCarrito = '';
                                    if (listaSucursal[iEncabezadoSucursal] == $('#HiddenFieldSucursalEleginda').val()) {
                                        cantidadDeProductoEnCarrito = listaProductosBuscados[i].cantidad; // ObtenerCantidadProducto(listaSucursal[iEncabezadoSucursal], listaProductosBuscados[i].pro_codigo);
                                    }
                                    //                                        strHtml += '<input class="cssFocusCantProdCarrito" id="inputSuc' + i + "_" + iEncabezadoSucursal + '" type="text"  onfocus="onfocusSucursal(this)" onblur="onblurSucursal(this)" onkeypress="return onKeypressCantProductos(event)" value="' + cantidadDeProductoEnCarrito + '" ></input>';
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
                strHtml += '</table>';
                strHtml += '<button onclick="CargarPedido()"> Cargar pedido </button>';
                strHtml += '<div id="divUnidadesRenglones"></div>';

            } else {
                strHtml += '<div style="margin: 10px; font-size:11px;color: #ed1125;" ><b>La búsqueda no arroja resultados</b></div>';
            }


            // Mostrar div resultado
            //            var arraySizeDocumento = SizeDocumento();
            //            document.getElementById('divCargandoContenedorGeneralFondo').style.height = arraySizeDocumento[1] + 'px';
            //            $('#divCargandoContenedorGeneralFondo').css('display', 'block'); //.css('display', 'none');

            document.getElementById('divResultadoBuscador').innerHTML = strHtml;
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
        } else {
            alert(MostrarTextoSuperaCantidadMaxima(listaProductosBuscados[fila].pro_nombre, listaProductosBuscados[fila].pro_canmaxima));
            var cantidadAnterior = ObtenerCantidadProducto(listaSucursal[columna], listaProductosBuscados[fila].pro_codigo);
            pValor.value = cantidadAnterior;
        }
    } else {
        // Borrar en el carrito o colocarlo en 0     
        //        var cantidad = ObtenerCantidadProducto(listaSucursal[columna], listaProductosBuscados[fila].pro_codigo);
        //        if (cantidad != '') {
        AgregarAlHistorialProductoCarrito(fila, columna, 0, true);
        //        }
    }
    CargarUnidadesRenglones();
}
//function ObtenerCantidadProducto(pIdSucursal, pIdProduco) {
//    var resultado = '';
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
//    return resultado;
//}
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

    //    PageMethods.HistorialProductoCarrito(listaProductosBuscados[pIndexProducto].pro_codigo, listaProductosBuscados[pIndexProducto].pro_nombre, listaSucursal[pIndexSucursal], pCantidadProducto, OnCallBackHistorialProductoCarrito, OnFail);   
    //    CargarOActualizarListaCarrito(listaSucursal[pIndexSucursal], listaProductosBuscados[pIndexProducto].pro_codigo, pCantidadProducto, true);
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
                        var pro = new cProductosAndCantidad();
                        pro.cantidad = mytext.val();
                        pro.codProducto = listaProductosBuscados[iProducto].pro_codigo;
                        pro.codSucursal = listaSucursal[iSucursal];
                        listaProductosAPedir.push(pro);
                        // alert(listaSucursal[iSucursal] + '  <-->  ' + mytext.val()); //listaProductosBuscados[iProducto].cantidad
                    }
                }
            } // fin  for (var iSucursal = 0; iSucursal < listaSucursal.length; iSucursal++) {
        } // fin for (var iProducto = 0; iProducto < listaProductosBuscados.length; iProducto++) {
        //
        //        CargarUnidadesRenglones();
        if (listaProductosAPedir.length > 0) {
            PageMethods.ActualizarProductoCarritoSubirArchivo(listaProductosAPedir, OnCallBackActualizarProductoCarritoSubirArchivo, OnFailActualizarProductoCarritoSubirArchivo);
        }
    } // fin if (listaProductosBuscados != null && listaSucursal != null) {
}
function OnFailActualizarProductoCarritoSubirArchivo(ex) {
    //location.href = 'PedidosBuscador.aspx';
    onclickCerrarSubirArchivo();
    OnFail(ex);
}
function OnCallBackActualizarProductoCarritoSubirArchivo(args) {
    //onclickCerrarSubirArchivo();

    PageMethods.BorrarListaProductosSubirPedisos(OnCallBackBorrarListaProductosSubirPedisos, OnFail);
}
function OnCallBackBorrarListaProductosSubirPedisos(args) {
    location.href = 'PedidosBuscador.aspx';
}
function cProductosAndCantidad() {
    this.codSucursal = '';
    this.codProducto = '';
    this.codProductoNombre = '';
    this.cantidad = 0;
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
        var strHtml = '';
        strHtml += '<table>';
        strHtml += '<tr>';
        strHtml += '<td>';
        strHtml += 'Unidades';
        strHtml += '</td>';
        strHtml += '<td>';
        strHtml += 'Renglones';
        strHtml += '</td>';
        strHtml += '<tr>';
        for (var iSucursal = 0; iSucursal < listaSucursalesUnidadesRenglones.length; iSucursal++) {
            //            if (listaSucursalesUnidadesRenglones[iSucursal].Renglones > 0) { }
            strHtml += '<tr>';
            strHtml += '<td>';
            strHtml += listaSucursalesUnidadesRenglones[iSucursal].Unidades;
            strHtml += '</td>';
            strHtml += '<td>';
            strHtml += listaSucursalesUnidadesRenglones[iSucursal].Renglones;
            strHtml += '</td>';
            strHtml += '<tr>';
        }
        strHtml += '</table>';
        $('#divUnidadesRenglones').html(strHtml);
        //        if (listaProductosAPedir.length > 0) {
        //            PageMethods.ActualizarProductoCarritoSubirArchivo(listaProductosAPedir, OnCallBackActualizarProductoCarritoSubirArchivo, OnFailActualizarProductoCarritoSubirArchivo);
        //        }
    }
}
function onclickHistorialSubirArchivo() {
    var myRadio = $('input[name=group1]');
    diasPedidos = myRadio.filter(':checked').val();
    PageMethods.ObtenerHistorialSubirArchivo(diasPedidos, OnCallBackObtenerHistorialSubirArchivo, OnFail);

    return false;
}
function OnCallBackObtenerHistorialSubirArchivo(args) {

    if (args != null) {
        listaArchivoSubidos = args;

        CargarHtmlHistorialArchivos();
    }
}
function cUnidadesAndRenglones() {
    this.codSucursal = '';
    this.Unidades = 0;
    this.Renglones = 0;
}
