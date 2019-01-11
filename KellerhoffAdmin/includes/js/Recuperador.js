var tipo = null;
var listaRecuperardor = null;

jQuery(document).ready(function () {

    if (tipo == null) {
        tipo = $('#hiddenTipoRecuperador').val();
        if (typeof tipo == 'undefined') {
            tipo = null;
        }
    }
    //    if (listaRecuperardor == null) {
    //        listaRecuperardor = eval('(' + $('#hiddenListaRecuperador').val() + ')');
    //        if (typeof listaRecuperardor == 'undefined') {
    //            listaRecuperardor = null;
    //        }
    //    }
    if (tipo != null) {
        switch (tipo) {
            case '1':
                $('#main-menu-top-RecuperadorFaltas').addClass('main-menu-top-activeSubMenu');
                break;
            case '2':
                $('#main-menu-top-ProblemasCrediticios').addClass('main-menu-top-activeSubMenu');
                break;
            default:
                break;
        }
    }
    //CargarRecuperadorFaltasYCrediticios();
    funLlenarGrillaFaltasProblemasCrediticios();
});
function onclickSeleccionarTodosRecuperador(pValor) {
    if (listaRecuperardor != null) {
        var isChecked = $('#checkRecuperador_' + pValor + '_' + 'Todos').is(":checked");
        for (var iProductos = 0; iProductos < listaRecuperardor[pValor].listaProductos.length; iProductos++) {
            //            if (listaRecuperardor[pValor].listaProductos[iProductos].stk_stock != 'N') {
            $('#checkRecuperador_' + pValor + '_' + iProductos).attr('checked', isChecked);
            //            }
        }
    }
}
function CargarRecuperadorFaltasYCrediticios() {
    var strHtml = '';
    if (listaRecuperardor != null) {
        if (listaRecuperardor.length > 0) {
            var isBanderaTitulo = true;
            for (var i = 0; i < listaRecuperardor.length; i++) {
                if (isBanderaTitulo) {
                    if (tipo != null) {
                        switch (tipo) {
                            case '1':
                                // $('#main-menu-top-RecuperadorFaltas').addClass('main-menu-top-activeSubMenu');
                                if (!form1.inputEstado.checked) {
                                    strHtml += '<div style="margin:20px;font-size:16px;color:#008000;text-align:left;text-decoration: underline;">Estos productos dados en falta, ahora están en stock</div>';
                                }
                                break;
                            case '2':
                                // $('#main-menu-top-ProblemasCrediticios').addClass('main-menu-top-activeSubMenu');
                                strHtml += '<div style="margin:20px;font-size:16px;color:#008000; text-align:left;text-decoration: underline;">Estos productos no fueron facturados por falta de crédito</div>';
                                break;
                            default:
                                break;
                        }
                    }
                    isBanderaTitulo = false;
                }
                strHtml += '<div   style="clear:both;text-align:left;">';
                strHtml += '<div style="font-size: 16px; padding: 10px;text-align:left; ">';
                strHtml += listaRecuperardor[i].suc_nombre;
                strHtml += '</div>';
                strHtml += '<table>';
                strHtml += '<tr>';
                strHtml += '<th>';
                strHtml += '</th>';
                strHtml += '<th align="left" width="400px">';
                strHtml += 'Nombre producto';
                strHtml += '</th>';
                strHtml += '<th  align="right" width="50px">';
                strHtml += 'Cantidad';
                strHtml += '</th>';
                strHtml += '<th width="50px" >';
                strHtml += 'Stock';
                strHtml += '</th>';
                strHtml += '</tr>';
                //
                strHtml += '<tr >';
                strHtml += '<td align="justify">';
                strHtml += '<input class="inputRadio" type="checkbox" id="checkRecuperador_' + i + '_' + 'Todos' + '" onclick="onclickSeleccionarTodosRecuperador(' + i + ')" />';
                strHtml += '</td>';
                strHtml += '<td align="left" style="background-color:#D2D2D2">';
                strHtml += 'TODOS';
                strHtml += '</td>';
                strHtml += '<td  align="right">';
                strHtml += '</td>';
                strHtml += '<td >';
                strHtml += '</td>';
                strHtml += '</tr>';
                //
                for (var iProductos = 0; iProductos < listaRecuperardor[i].listaProductos.length; iProductos++) {
                    isFaltantes = true;
                    strHtml += '<tr>';
                    strHtml += '<td align="justify">';
                    //                    strHtml += listaRecuperardor.listaProductos[iProductos].fpc_nombreProducto;
                    var SoloLecturaCheckBox = '';
                    //                    if (listaRecuperardor[i].listaProductos[iProductos].stk_stock.toLowerCase() == 'n') {
                    //                        SoloLecturaCheckBox = ' disabled="disabled" ';
                    //                    }
                    strHtml += '<input class="inputRadio" type="checkbox" id="checkRecuperador_' + i + '_' + iProductos + '" ' + SoloLecturaCheckBox + ' />';
                    strHtml += '</td>';
                    strHtml += '<td align="left">';
                    strHtml += listaRecuperardor[i].listaProductos[iProductos].fpc_nombreProducto;
                    strHtml += '</td>';
                    strHtml += '<td id="tdCantidadRecuperador_' + i + '_' + iProductos + '" align="right">';
                    strHtml += listaRecuperardor[i].listaProductos[iProductos].fpc_cantidad;
                    strHtml += '</td>';
                    strHtml += '<td >';
                    //                    strHtml += listaRecuperardor[i].listaProductos[iProductos].stk_stock
                    strHtml += '<div style="width:15px;height:10px;float:left;"></div>';
                    strHtml += '<div class="estado-' + listaRecuperardor[i].listaProductos[iProductos].stk_stock.toLowerCase() + '"></div>';
                    strHtml += '</td>';
                    strHtml += '</tr>';
                }
                strHtml += '</table>';
                strHtml += '<div style="width:330px;text-align:center; ">';
                strHtml += ' <a class="carro-btn-confirmar" onclick="onclickConfirmarRecuperador(' + i + ')" href="#">Confirmar</a>';
                strHtml += ' <a class="carro-btn-confirmar" onclick="onclickDescartarRecuperador(' + i + ')" href="#">Descartar</a>';
                strHtml += '</div>';
                strHtml += '</div>'; // fin div style="clear:both;"
            }
        } else { // fin   if (listaRecuperardor.length > 0) {
            strHtml += objMensajeNoEncontrado;
        }
    }

    $('#divRecuperador').html(strHtml);
}
function onclickDescartarRecuperador(pIndexCarrito) {
    var contadorArray = -1;
    var ArrayProductos = new Array();
    for (var iProductos = 0; iProductos < listaRecuperardor[pIndexCarrito].listaProductos.length; iProductos++) {
        if ($('#checkRecuperador_' + pIndexCarrito + '_' + iProductos).is(":checked")) {
            contadorArray++;
            ArrayProductos[contadorArray] = listaRecuperardor[pIndexCarrito].listaProductos[iProductos].fpc_nombreProducto;
        }
    }
    if (contadorArray != -1) {
        PageMethods.BorrarPorProductosFaltasProblemasCrediticios(listaRecuperardor[pIndexCarrito].fpc_codSucursal, ArrayProductos, OnCallBackBorrarPorProductosFaltasProblemasCrediticios, OnFail);
    }
}
function OnCallBackBorrarPorProductosFaltasProblemasCrediticios(args) {
    funLlenarGrillaFaltasProblemasCrediticios();
}
function onclickConfirmarRecuperador(pIndexCarrito) {
    var contadorArray = -1;
    var ArrayProductos = new Array();
    var ArrayCantidad = new Array();
    var ArrayOferta = new Array();
    var ArrayCantidadTransfer = new Array();
    var isNoStock = false;
    for (var iProductos = 0; iProductos < listaRecuperardor[pIndexCarrito].listaProductos.length; iProductos++) {
        if ($('#checkRecuperador_' + pIndexCarrito + '_' + iProductos).is(":checked") && listaRecuperardor[pIndexCarrito].listaProductos[iProductos].stk_stock.toUpperCase() != 'N') {
            contadorArray++;
            ArrayProductos[contadorArray] = listaRecuperardor[pIndexCarrito].listaProductos[iProductos].fpc_nombreProducto;
            ArrayCantidad[contadorArray] = $('#tdCantidadRecuperador_' + pIndexCarrito + '_' + iProductos).html();
            if (listaRecuperardor[pIndexCarrito].listaProductos[iProductos].pro_ofeunidades == 0 && listaRecuperardor[pIndexCarrito].listaProductos[iProductos].pro_ofeporcentaje == 0) {
                ArrayOferta[contadorArray] = false;
            } else {
                ArrayOferta[contadorArray] = true;
            }
        } else if ($('#checkRecuperador_' + pIndexCarrito + '_' + iProductos).is(":checked") && listaRecuperardor[pIndexCarrito].listaProductos[iProductos].stk_stock.toUpperCase() == 'N') {
            isNoStock = true;
        }
    }
    if (contadorArray != -1) {
        PageMethods.AgregarProductosDelRecuperardorAlCarrito(listaRecuperardor[pIndexCarrito].fpc_codSucursal, ArrayProductos, ArrayCantidad, ArrayOferta, OnCallBackAgregarProductosDelRecuperardorAlCarrito, OnFail);
    } else {
        if (isNoStock) {
            alert('Sólo se pueden confirmar productos con Stock.');
            funLlenarGrillaFaltasProblemasCrediticios();
        }
    }
}
function OnCallBackAgregarProductosDelRecuperardorAlCarrito(args) {
    PageMethods.RecuperarFaltasProblemasCrediticios(form1.cmbDia.options[form1.cmbDia.selectedIndex].value, OnCallBackRecuperarFaltasProblemasCrediticios, OnFail);
}
function CerrarContenedores() {
    document.getElementById('divContenedorGeneralFondo').style.display = 'none';
    document.getElementById('divRespuestaPedidoContenedorGeneral').style.display = 'none';
}
function onclickRespuestaPedido() {
    CargarRecuperadorFaltasYCrediticios();
    CerrarContenedores();
}
function OnCallBackRecuperarFaltasProblemasCrediticios(args) {
    listaRecuperardor = args;
    //CargarRecuperadorFaltasYCrediticios();
    funLlenarGrillaFaltasProblemasCrediticios();
}
function OnCallBackLlenarGrillaFaltasProblemasCrediticios(args) {
    listaRecuperardor = args;
    CargarRecuperadorFaltasYCrediticios();

}
function funLlenarGrillaFaltasProblemasCrediticios() {
    if (form1.inputEstado.checked) {
        PageMethods.RecuperarFaltasProblemasCrediticiosTodosEstados(form1.cmbDia.options[form1.cmbDia.selectedIndex].value, OnCallBackLlenarGrillaFaltasProblemasCrediticios, OnFail);
    } else {
        PageMethods.RecuperarFaltasProblemasCrediticios(form1.cmbDia.options[form1.cmbDia.selectedIndex].value, OnCallBackLlenarGrillaFaltasProblemasCrediticios, OnFail);
    }
}