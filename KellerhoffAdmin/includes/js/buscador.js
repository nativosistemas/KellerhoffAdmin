var isCarritoDiferido = false;
var clienteIsGLN = true;
var isClienteTomaTransfer = true;
var isNotGLNisTrazable = true;
var isClienteTomaOferta = true;
var intPaginadorTipoDeRecuperar = 0; // 1 es oferta - 2 es transfer
function OnCallBackRecuperarProductos(args) {
    if (args != null) {
        if (args != '') {
            var strHtml = '';
            args = eval('(' + args + ')');
            listaSucursal = args.listaSucursal;
            listaProductosBuscados = args.listaProductos;

            cantMaxFila = listaProductosBuscados.length;
            cantMaxColumna = listaSucursal.length;
            if (listaProductosBuscados.length > 0) {
                strHtml += '<table id="tbProductos" class="table table-hover table-condensed ">';
                strHtml += '<thead ">';// nuevo: thead 06/11/2016
                strHtml += '<tr>';
                strHtml += '<th rowspan="2">Detalle Producto</th>';
                //
                strHtml += '<th  rowspan="2"  > </th>';
                //
                strHtml += '<th  rowspan="2"  onclick="onclickOrdenarProducto(0)"> Precio Público</th>';
                strHtml += '<th  rowspan="2"  onclick="onclickOrdenarProducto(1)">Precio Cliente</th>';
                strHtml += '<th  colspan="3">Oferta</th>';
                //NUEVO
                if (!isCarritoDiferido) {
                    strHtml += '<th  colspan="2">Transfer</th>';
                }
                //FIN NUEVO
                for (var iEncabezadoSucursal = 0; iEncabezadoSucursal < listaSucursal.length; iEncabezadoSucursal++) {
                    strHtml += '<th  rowspan="2">';
                    strHtml += ConvertirSucursalParaColumno(listaSucursal[iEncabezadoSucursal]);
                    strHtml += '</th>';
                }
                strHtml += '</tr>';
                strHtml += '<tr>';
                strHtml += '<th >%</th>';
                strHtml += '<th >Mín.</th>'; //Cant. 
                strHtml += '<th  onclick="onclickOrdenarProducto(2)">Precio</th>';
                //NUEVO
                if (!isCarritoDiferido) {
                    strHtml += '<th >&nbsp;&nbsp;Cond.&nbsp;&nbsp;</th>';
                    strHtml += '<th  onclick="onclickOrdenarProducto(3)">Precio</th>';
                }
                //FIN NUEVO
                strHtml += '</tr>';
                strHtml += '</thead>';// nuevo: thead 06/11/2016
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
                    strHtml += '<tr >'; 
                    strHtml += '<td class=" cssFilaBuscadorDesmarcar cssFilaBuscador_' + i + ' ';
                    var strHtmlColorFondo = '';
                    if (i % 2 != 0) {
                        strHtmlColorFondo = ' bp-td-color';
                    }
                    strHtml += strHtmlColorFondo + ' porCamara_detalleProducto">'; //'<td class="first-td2">' + class="trFunciones"  onclick="AnimarPresentacionProducto(' + i + ')"
                    strHtml += '<div  OnMouseMove="OnMouseMoveProdructo(event)" OnMouseOver="OnMouseOverProdructo(' + i + ')" OnMouseOut="OnMouseOutProdructo()" onclick="RecuperarTransfer(' + i + ')" style="cursor:pointer;" >' + AgregarMark(listaProductosBuscados[i].pro_nombre);
                    // Agregar:
                    if (isNotGLNisTrazable) {
                        strHtml += '<span class="spanProductoTrazableCLiSinGLN" >&nbsp;&nbsp;&nbsp;Producto trazable. Farmacia sin GLN.</span>'; //style="padding-left:4px;"
                    }
                    if (isClienteTomaTransfer) {
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
                    strHtml += '</td>';
                    strHtml += '<td   OnMouseMove="OnMouseMoveProdructo(event)" OnMouseOver="OnMouseOverProdructo(' + i + ')" OnMouseOut="OnMouseOutProdructo()" onclick="RecuperarTransfer(' + i + ')" class="' + strHtmlColorFondo + ' cssFilaBuscadorDesmarcar cssFilaBuscador_' + i + ' porCamara_cabeceraYfila">';
                    if (listaProductosBuscados[i].pri_nombreArchivo != null) {
                        strHtml += '<img class="spanProductoFoto" src="../../img/produtos/camera6.png" title="Foto" alt="Foto"  />';//style="width:20px;height:20px; "
                    }
                    strHtml += '</td>';
                    //
                    var precioPublico = '$&nbsp;' + FormatoDecimalConDivisorMiles(listaProductosBuscados[i].pro_precio.toFixed(2));
                    if (listaProductosBuscados[i].pro_precio == 0) {
                        precioPublico = '';
                    }
                    strHtml += '<td   class=" cssFilaBuscadorDesmarcar cssFilaBuscador_' + i + '">' + precioPublico + '</td>';
                    strHtml += '<td style="text-align:right;"  class=" cssFilaBuscadorDesmarcar  cssFilaBuscador_' + i + '">$&nbsp;' + FormatoDecimalConDivisorMiles(listaProductosBuscados[i].PrecioFinal.toFixed(2)) + '</td>'; // listaProductosBuscados[i].PrecioDescuentoFarmacia.toFixed(2)
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
                    strHtml += '<td class=" cssFilaBuscadorDesmarcar  cssFilaBuscador_' + i + '">' + varOfeporcentaje + '</td>';
                    strHtml += '<td class=" cssFilaBuscadorDesmarcar  cssFilaBuscador_' + i + '">' + varOfeunidades + '</td>';
                    strHtml += '<td style="text-align:right;"  class=" cssFilaBuscadorDesmarcar  cssFilaBuscador_' + i + '">' + varPrecioConDescuentoOferta + '</td>'

                    // NUEVO Transfer facturacion directa
                    if (!isCarritoDiferido) {
                        var varTransferFacturacionDirectaCondicion = '';
                        var varTransferFacturacionDirectaPrecio = '';
                        if (isClienteTomaTransfer) {
                            if (listaProductosBuscados[i].isProductoFacturacionDirecta) {
                                if (listaProductosBuscados[i].tde_unidadesbonificadasdescripcion != null) {
                                    varTransferFacturacionDirectaCondicion = listaProductosBuscados[i].tde_unidadesbonificadasdescripcion;
                                }
                                if (listaProductosBuscados[i].PrecioFinalTransfer != null) {
                                    varTransferFacturacionDirectaPrecio = '$&nbsp;' + FormatoDecimalConDivisorMiles(listaProductosBuscados[i].PrecioFinalTransfer.toFixed(2));
                                }
                            }
                        }

                        strHtml += '<td class=" cssFilaBuscadorDesmarcar  cssFilaBuscador_' + i + '">';
                        if (varTransferFacturacionDirectaCondicion != '') {
                            strHtml += '<div  OnMouseMove="OnMouseMoveProdructoFacturacionDirecta(event)" OnMouseOver="OnMouseOverProdructoFacturacionDirecta(' + i + ')" OnMouseOut="OnMouseOutProdructoFacturacionDirecta()"   >'
                            strHtml += varTransferFacturacionDirectaCondicion;
                            strHtml += '</div>';
                        }
                        strHtml += '</td>'; //NUEVO
                        strHtml += '<td class="cssFilaBuscadorDesmarcar  cssFilaBuscador_' + i + '">' + varTransferFacturacionDirectaPrecio + '</td>';  //NUEVO
                    }
                    // NUEVO Transfer facturacion directa

                    // Optimizar
                    for (var iEncabezadoSucursal = 0; iEncabezadoSucursal < listaSucursal.length; iEncabezadoSucursal++) {
                        strHtml += '<td  class=" cssFilaBuscadorDesmarcar  cssFilaBuscador_' + i + '"  >';
                        var isDibujarStock = false;
                        if (intPaginadorTipoDeRecuperar != 2) {// todo transfer
                            isDibujarStock = true;
                        } // fin      if (intPaginadorTipoDeRecuperar != 2) { 
                        else { // si selecciona todo transfer y es producto facturacion directa
                            if (!isCarritoDiferido) {
                                if (listaProductosBuscados[i].isProductoFacturacionDirecta) {
                                    isDibujarStock = true;
                                }
                            }
                        }
                        if (isDibujarStock) {
                            for (var iSucursal = 0; iSucursal < listaProductosBuscados[i].listaSucursalStocks.length; iSucursal++) {
                                if (listaProductosBuscados[i].listaSucursalStocks[iSucursal].stk_codsuc == listaSucursal[iEncabezadoSucursal]) {
                                    strHtml += '<div class="cont-estado-input"><div class="estado-' + listaProductosBuscados[i].listaSucursalStocks[iSucursal].stk_stock.toLowerCase() + '"></div>';
                                    // if (!isNotGLNisTrazable) {
                                    if (isMostrarImput) {
                                        // Cargar Cantidad
                                        var cantidadDeProductoEnCarrito = ObtenerCantidadProductoMasTransfer(listaSucursal[iEncabezadoSucursal], listaProductosBuscados[i].pro_codigo, listaProductosBuscados[i].pro_nombre);
                                        strHtml += '<input class="form-control" id="inputSuc' + i + "_" + iEncabezadoSucursal + '" type="text"  onfocus="onfocusSucursal(this)" onblur="onblurSucursal(this)" onkeypress="return onKeypressCantProductos(event)" value="' + cantidadDeProductoEnCarrito + '" ></input>';
                                    }
                                    strHtml += '</div>';
                                    break;
                                }
                            }
                        }
                        strHtml += '</td>';
                    }
                    strHtml += '</tr>';
                }
                strHtml += '</table>';
            } else {
                strHtml += '<div style="margin: 10px; font-size:11px;color: #ed1125;" ><b>La búsqueda no arroja resultados</b></div>';
            }
            document.getElementById('divResultadoBuscador').innerHTML = strHtml;
            // Elejir el primer producto
            if ($('#inputSuc0_0').length) {
                $('#inputSuc0_0').focus();
                selectedInput = document.getElementById('inputSuc0_0');
            }
        }
    }
}
function AgregarMark(pValor) {
    var valorTemp = pValor.toUpperCase();
    //var tempPalabraBuscador = varPalabraBuscador.toUpperCase();
    //var palabrasBase = tempPalabraBuscador.split(" ");
    //var palabrasReales = [];
    //for (var i = 0; i < palabrasBase.length; i++) {
    //    var p = palabrasBase[i].replace(/([\.\/\(\)\[\]\*\?\{\}\^\$])/g, "\\$1");
    //    if (p != '') {
    //        palabrasReales.push(p);
    //    }
    //}
    //for (var i = 0; i < palabrasReales.length; i++) {
    //    var re = new RegExp("(" + palabrasReales[i] + ")", 'g');
    //    // valorTemp = valorTemp.replace(re, "<mark>$1</mark>");
    //    // if navagador explore
    //    var navegador = navigator.appName
    //    if (navegador == 'Microsoft Internet Explorer') {
    //        valorTemp = valorTemp.replace(re, '<span style="background-color:#C4E3F7;">$1</span>');
    //    } else {
    //        valorTemp = valorTemp.replace(re, "<mark>$1</mark>");
    //    }
    //    // fin if navegador explore
    //}
    return valorTemp;
}
function ObtenerCantidadProductoMasTransfer(pIdSucursal, pIdProduco, pNombreProducto) {
    var resultado = '';
    resultado = ObtenerCantidadProducto(pIdSucursal, pIdProduco);

    if (!isCarritoDiferido) {
        /// NUEVO facturacion directa
        var resultadoTransfer = ObtenerCantidadProductoTransfer(pIdSucursal, pNombreProducto);
        if (resultadoTransfer != 0) {
            if (resultado == '') {
                resultado = resultadoTransfer;
            } else {
                resultado += resultadoTransfer;
            }
        }
        /// FIN NUEVO facturacion directa
    }
    return resultado;
}
function ObtenerCantidadProductoTransfer(pIdSucursal, pNombreProducto) {
    var resultado = 0;
    //if (listaCarritoTransferPorSucursal != null) {
    //    if (listaCarritoTransferPorSucursal.length > 0) {
    //        for (var iSucursal = 0; iSucursal < listaCarritoTransferPorSucursal.length; iSucursal++) {
    //            if (listaCarritoTransferPorSucursal[iSucursal].Sucursal == pIdSucursal) {
    //                for (var iTransfer = 0; iTransfer < listaCarritoTransferPorSucursal[iSucursal].listaTransfer.length; iTransfer++) {
    //                    for (var iTransferProductos = 0; iTransferProductos < listaCarritoTransferPorSucursal[iSucursal].listaTransfer[iTransfer].listaProductos.length; iTransferProductos++) {
    //                        if (listaCarritoTransferPorSucursal[iSucursal].listaTransfer[iTransfer].listaProductos[iTransferProductos].tde_codpro == pNombreProducto) {
    //                            if (listaCarritoTransferPorSucursal[iSucursal].listaTransfer[iTransfer].listaProductos[iTransferProductos].isProductoFacturacionDirecta) {
    //                                resultado += listaCarritoTransferPorSucursal[iSucursal].listaTransfer[iTransfer].listaProductos[iTransferProductos].cantidad;
    //                            }
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}
    return resultado;
}
function ObtenerCantidadProducto(pIdSucursal, pIdProduco) {
    var resultado = '';
    //if (listaCarritos != null) {
    //    for (var i = 0; i < listaCarritos.length; i++) {
    //        if (listaCarritos[i].codSucursal == pIdSucursal) {
    //            for (var iProducto = 0; iProducto < listaCarritos[i].listaProductos.length; iProducto++) {
    //                if (listaCarritos[i].listaProductos[iProducto].codProducto == pIdProduco) {
    //                    resultado = listaCarritos[i].listaProductos[iProducto].cantidad;
    //                    break;
    //                }
    //            }
    //            break;
    //        } // for (var i = 0; i < listaCarritos.length; i++) {
    //    }
    //}
    return resultado;
}
function onfocusSucursal(pValor) {
    //selectInputCarrito = null;
    //selectedInputTransfer = null;
    //selectedInput = pValor;
    //setTimeout(function () { selectedInput.select(); MarcarFilaSeleccionada(pValor); }, 5);
}
function OnMouseMoveProdructo(e) {
    //if (typeof (e) == 'undefined') {
    //    e = event;
    //}
    //var bt = document.body.scrollTop;
    //var et = document.documentElement ? document.documentElement.scrollTop : null;
    //var top = e.clientY || e.pageY;
    //var left = e.clientX || e.pageX;

    //$("#divMostradorProducto").css("top", (top + (bt || et) + 20) + 'px');
    //$("#divMostradorProducto").css("left", (left + 20) + 'px');
}
function onblurSucursal(pValor) {
    //var nombre = pValor.id;
    //nombre = nombre.replace('inputSuc', '');
    //var palabrasBase = nombre.split("_");
    //var fila = parseInt(palabrasBase[0]);
    //var columna = parseInt(palabrasBase[1]);
    //if (pValor.value != '') {
    //    // Calcular si producto transfer
    //    var cantidadComparativa = ObtenerCantidadProductoMasTransfer(listaSucursal[columna], listaProductosBuscados[fila].pro_codigo, listaProductosBuscados[fila].pro_nombre);
    //    if (cantidadComparativa == pValor.value) {

    //    } else {
    //        var isNotMaximaCantidadSuperada = true;
    //        if (listaProductosBuscados[fila].pro_canmaxima != null) {
    //            if (listaProductosBuscados[fila].pro_canmaxima < pValor.value) {
    //                isNotMaximaCantidadSuperada = false;
    //            }
    //        }
    //        if (isNotMaximaCantidadSuperada) {
    //            //Inicio Cantidad producto parametrizada
    //            if (cantidadMaximaParametrizada != null) {
    //                if (parseInt(cantidadMaximaParametrizada) > 0) {
    //                    if (parseInt(cantidadMaximaParametrizada) < parseInt(pValor.value)) {
    //                        isExcedeImporte = true;
    //                        ExcedeImporteFila = fila;
    //                        ExcedeImporteColumna = columna;
    //                        ExcedeImporteValor = pValor.value;
    //                        funMostrarMensajeCantidadSuperada();
    //                    }
    //                }
    //            }
    //            //Fin Cantidad producto parametrizada
    //            if (isExcedeImporte) {

    //            } else {
    //                if (isCarritoDiferido) {
    //                    AgregarAlHistorialProductoCarrito(fila, columna, pValor.value, true);
    //                } else {
    //                    var resultadoCantidadCambio = CargarProductoCantidadDependiendoTransfer(fila, columna, pValor.value);
    //                    if (resultadoCantidadCambio != pValor.value) {
    //                        pValor.value = resultadoCantidadCambio;
    //                    }
    //                }
    //            }
    //        } else {
    //            alert(MostrarTextoSuperaCantidadMaxima(listaProductosBuscados[fila].pro_nombre, listaProductosBuscados[fila].pro_canmaxima));
    //            var cantidadAnterior = ObtenerCantidadProducto(listaSucursal[columna], listaProductosBuscados[fila].pro_codigo);
    //            pValor.value = cantidadAnterior;
    //        }
    //    } // else  if (cantidadComparativa == pValor.value) {
    //} else {
    //    // Borrar en el carrito o colocarlo en 0     
    //    var cantidad = ObtenerCantidadProducto(listaSucursal[columna], listaProductosBuscados[fila].pro_codigo);
    //    if (cantidad != '') {
    //        AgregarAlHistorialProductoCarrito(fila, columna, 0, true);
    //    }
    //}
    //if (!isExcedeImporte) {
    //    if (isEnterExcedeImporte) {
    //        isEnterExcedeImporte = false;
    //        jQuery("#txtBuscador").val('');
    //        onClickBuscar();
    //        document.getElementById('txtBuscador').focus();
    //    }
    //}
}
function OnMouseOverProdructo(pIndice) {
    //if ($("#divMostradorProducto").css("display") == 'none') {
    //    LimpiarTimeoutProducto();
    //    timerProducto = setTimeout(function () { AnimarPresentacionProducto(pIndice); }, 300);
    //}
}
function OnMouseOutProdructo() {
    //if ($("#divMostradorProducto").css("display") == 'block') {
    //    $("#divMostradorProducto").css("display", "none");
    //}
    //LimpiarTimeoutProducto();
}
/// Fin facturacion directa detalle muestra
function onclickOrdenarProducto(pValor) {

    //if (pValor == -1) {
    //    PageMethods.RecuperarProductosOrdenar('pro_nombre', Ascender_pro_nombre, OnCallBackRecuperarProductos, OnFail);
    //    Ascender_pro_nombre = !Ascender_pro_nombre;
    //}
    //if (pValor == 0) {
    //    PageMethods.RecuperarProductosOrdenar('pro_precio', Ascender_pro_precio, OnCallBackRecuperarProductos, OnFail);
    //    Ascender_pro_precio = !Ascender_pro_precio;
    //}
    //if (pValor == 1) {
    //    PageMethods.RecuperarProductosOrdenar('PrecioFinal', Ascender_PrecioFinal, OnCallBackRecuperarProductos, OnFail);
    //    Ascender_PrecioFinal = !Ascender_PrecioFinal;
    //}
    //if (pValor == 2) {
    //    PageMethods.RecuperarProductosOrdenar('PrecioConDescuentoOferta', Ascender_PrecioConDescuentoOferta, OnCallBackRecuperarProductos, OnFail);
    //    Ascender_PrecioConDescuentoOferta = !Ascender_PrecioConDescuentoOferta;
    //}
    //if (pValor == 3) {
    //    PageMethods.RecuperarProductosOrdenar('PrecioConTransfer', Ascender_PrecioConTransfer, OnCallBackRecuperarProductos, OnFail);
    //    Ascender_PrecioConTransfer = !Ascender_PrecioConTransfer;
    //}
}
function RecuperarTransfer(pIndice) {
    //if (!isCarritoDiferido) { // si no es carrito diferido
    //    if (isClienteTomaTransfer) {
    //        if (listaProductosBuscados[pIndice].isTieneTransfer) {
    //            productoSeleccionado = listaProductosBuscados[pIndice].pro_nombre;
    //            PageMethods.RecuperarTransfer(listaProductosBuscados[pIndice].pro_nombre, OnCallBackRecuperarTransfer, OnFail);
    //        }
    //    }
    //}
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
        return false;
    }
    return true;
}