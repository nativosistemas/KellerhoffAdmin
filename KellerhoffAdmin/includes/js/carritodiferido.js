var varPalabraBuscador = '';
var listaProductosBuscados = null;
var listaSucursal = null;
var cantMaxFila = -1;
var cantMaxColumna = -1;
var selectedInput = null;
var selectedInputTransfer = null;
var selectInputCarrito = null;
var listaCarritos = null;
var listaSucursales = null;
var listaSucursalesDependienteInfo = null;
var sucursalCliente = null;
var tipoEnvioCliente = null;
var clienteIsGLN = null;
var Ascender_pro_precio = true;
var Ascender_PrecioFinal = true;
var Ascender_PrecioConDescuentoOferta = true;
var isClienteTomaTransfer = null;
var isClienteTomaPerfumeria = null;
var isClienteTomaOferta = null;
var textTipoEnvioCarrito = '';
var textTipoEnvioCarritoTransfer = '';
var intPaginadorTipoDeRecuperar = 0; // 1 es oferta - 2 es transfer
var isHacerBorradoCarritos = false;
var mensajeCuandoSeMuestraError = 'Se produjo un error';

jQuery(document).ready(function () {

    $(document).keydown(function (e) {
        if (!e) {
            e = window.event;
        }
        teclaPresionada_enPagina(e);
    });

    $('#form1').submit(function () {
        return false;
    });
    if (listaCarritos == null) {
        listaCarritos = eval('(' + $('#hiddenListaCarritos').val() + ')');
        if (typeof listaCarritos == 'undefined') {
            listaCarritos = null;
        }
    }
    if (listaSucursales == null) {
        listaSucursales = eval('(' + $('#hiddenListaSucursalesInfo').val() + ')');
        if (typeof listaSucursales == 'undefined') {
            listaSucursales = null;
        }
    }
    if (sucursalCliente == null) {
        sucursalCliente = $('#hiddenCli_codsuc').val();
        if (typeof sucursalCliente == 'undefined') {
            sucursalCliente = null;
        }
    }
    //    if (listaTipoEnvio == null) {
    //        listaTipoEnvio = $('#hiddenListaTipoEnvio').val();
    //        if (typeof listaTipoEnvio == 'undefined') {
    //            listaTipoEnvio = null;
    //        }
    //    }
    if (tipoEnvioCliente == null) {
        tipoEnvioCliente = $('#hiddenCli_codtpoenv').val();
        if (typeof tipoEnvioCliente == 'undefined') {
            tipoEnvioCliente = null;
        }
    }
    if (listaSucursalesDependienteInfo == null) {
        listaSucursalesDependienteInfo = eval('(' + $('#hiddenListaSucursalesDependienteInfo').val() + ')');
        if (typeof listaSucursalesDependienteInfo == 'undefined') {
            listaSucursalesDependienteInfo = null;
        }
    }
    //    
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
    // Perfumeria
    if (isClienteTomaPerfumeria == null) {
        isClienteTomaPerfumeria = $('#hiddenCli_tomaPerfumeria').val();
        if (typeof isClienteTomaPerfumeria == 'undefined') {
            isClienteTomaPerfumeria = false;
        } else {
            if (isClienteTomaPerfumeria == 'true') {
                isClienteTomaPerfumeria = true;
            } else if (isClienteTomaPerfumeria == 'false') {
                isClienteTomaPerfumeria = false;
            } else {
                isClienteTomaPerfumeria = false;
            }
        }
    }
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
    }
    if (!isClienteTomaOferta) {
        $('#linkTodosOfertas').hide();
    }
    if (!isClienteTomaTransfer) {
        $('#linkTodosTransfer').hide();
    }
    //    
    jQuery("#txtBuscador").focus();
    CargarCarritos();
 
});

function onClickBusquedaAvanzada() {
    $('#opcionesBusquedaAvanzada').css('display', 'block');
    $('#linkBusquedaAvanzada').css('display', 'none');
    $('.tools-top').css('height', '67px');
    $('#divResultadoBuscador').css('padding-top', '35px'); //padding-top: 35px
}
function onClickCerrarBusquedaAvanzada() {
    $('#checkNombre').attr('checked', true);
    $('#checkBoxCodigoBarra').attr('checked', true);
    $('#checkBoxMonodroga').attr('checked', false);
    $('#checkBoxLaboratorio').attr('checked', true);
    $('#checkBoxCodigoAlfaBeta').attr('checked', false);

    $('#opcionesBusquedaAvanzada').css('display', 'none');
    $('#linkBusquedaAvanzada').css('display', 'block');
    $('.tools-top').css('height', '51px');
    $('#divResultadoBuscador').css('padding-top', '10px');
}
function HacerLimpiezaDeCarritosDspDeConfirmarPedido() {
    if (isHacerBorradoCarritos) {
        if (indexSucursalTransferSeleccionado == null) {
            var indexCarrito = $("#hiddenIndexCarrito").val();
            if (isNotNullEmpty(indexCarrito)) {
                $('#divContenedorCarrito_' + indexCarrito).remove();
                LimpiarTextBoxProductosBuscados(listaCarritos[indexCarrito].codSucursal);
                listaCarritos[indexCarrito].codSucursal = '';
                $("#hiddenIndexCarrito").val('');
            }
        } else {
            $('#divContenedorBaseTransfer_' + listaCarritoTransferPorSucursal[indexSucursalTransferSeleccionado].Sucursal).html('');
            indexSucursalTransferSeleccionado = null;
        }
        isHacerBorradoCarritos = false;
    }
}

function CargarCarritos() {
    if (listaCarritos != null) {
        for (var i = 0; i < listaCarritos.length; i++) {
            $('#divContenedorBase_' + listaCarritos[i].codSucursal).html(AgregarCarritoHtml(i));
        }
    }
}

function onClickVerOfertas() {
    intPaginadorTipoDeRecuperar = 1;
    pagActual = 1;
    PageMethods.RecuperarProductosEnOfertas(pagActual, OnCallBackRecuperarProductosEnOfertas, OnFail);
}

function onClickVerTransfer() {
    intPaginadorTipoDeRecuperar = 2;
    pagActual = 1;
    PageMethods.RecuperarProductosEnTransfer(pagActual, OnCallBackRecuperarProductosEnOfertas, OnFail);
}

//function OnCallBackRecuperarProductosEnOfertas(args) {
//    var varArgs = eval('(' + args + ')');
//    cantPaginaTotal = (varArgs.CantidadRegistroTotal / 100).toFixed(0);
//    var mod = varArgs.CantidadRegistroTotal % 100;
//    var cantPag = 0;
//    if (mod == 0) {
//        cantPag = varArgs.CantidadRegistroTotal / 100;
//    }
//    else {
//        cantPag = ((varArgs.CantidadRegistroTotal / 100) + 1).toFixed(0); //+ 1
//    }
//    cantPaginaTotal = cantPag;
//    GenerarPaginador();
//    OnCallBackRecuperarProductos(args);
//}
function OnCallBackRecuperarProductosEnOfertas(args) {

    var varArgs = eval('(' + args + ')');
    //    cantPaginaTotal = (varArgs.CantidadRegistroTotal / 100).toFixed(0);

    var mod = varArgs.CantidadRegistroTotal % 100;
    var cantPag = 0;
    if (mod == 0) {
        cantPag = varArgs.CantidadRegistroTotal / 100;
    }
    else {
        //        cantPag = ((varArgs.CantidadRegistroTotal / 100)).toFixed(0); //+ 1
        cantPag = Math.floor((varArgs.CantidadRegistroTotal / 100)); // Math.floor( ) redondea para abajo
    }
    cantPaginaTotal = cantPag;


    GenerarPaginador();
    OnCallBackRecuperarProductos(args);
}
function LlamarMetodoCargar(pagActual) {
    if (intPaginadorTipoDeRecuperar == 1) {
        PageMethods.RecuperarProductosEnOfertas(pagActual, OnCallBackRecuperarProductosEnOfertas, OnFail);
    } else if (intPaginadorTipoDeRecuperar == 2) {
        PageMethods.RecuperarProductosEnTransfer(pagActual, OnCallBackRecuperarProductosEnOfertas, OnFail);
    }
}
function AgregarCarritoHtml(pIndexCarrito) {
    var strHTML = '';

    strHTML += '<div id="divContenedorCarrito_' + pIndexCarrito + '" class="carro">';
    strHTML += '<div class="carro-titles">';
    strHTML += '<div class="ct-left">';
    strHTML += 'Sucursal<br />';
    var nombreSucursal = listaCarritos[pIndexCarrito].codSucursal;
    if (listaSucursales != null) {
        for (var i = 0; i < listaSucursales.length; i++) {
            if (listaSucursales[i].sde_sucursal == listaCarritos[pIndexCarrito].codSucursal) {
                nombreSucursal = listaSucursales[i].suc_nombre;
                break;
            }
        }
    }
    strHTML += nombreSucursal;
    strHTML += '</div>';
    //    strHTML += '<div class="ct-right">';
    //    strHTML += 'Próxima&nbsp;entrega<br />';
    //    strHTML += '<span>15:00 hs.</span>';
    //    strHTML += '</div>';
    strHTML += '<div class="clear">';
    strHTML += '</div>';
    strHTML += '</div>';

    //    strHTML += '<div class="cssDivContenedorTabla">';

    //    strHTML += '<div  class="table-wrapper">';
    //    strHTML += '<div  class="table-scroll">';

    strHTML += '<table  class="tbl-carro" border="0" cellspacing="0" cellpadding="0">'; // Encabezado
    //    strHTML += '<thead>';
    strHTML += '<tr>';

    strHTML += ' <th width="60%" class="tbl-carroTh">';
    //    strHTML += '<span class="textScroll" style="width:134px;left:0px;">' + 'Detalle&nbsp;Producto' + '</span>';
    strHTML += 'Detalle&nbsp;Producto';
    strHTML += '</th>';
    strHTML += '<th width="20%" class="tbl-carroTh">';
    //    strHTML += '<span class="textScroll"   style="width:44px;left:135px;">' + '  Cant.' + '</span>'; 
    strHTML += '  Cant.';
    strHTML += ' </th>';
    strHTML += '<th width="20%" class="tbl-carroTh">'; //tbl-carro-style-off
    //    strHTML += '<span class="textScroll"  style="width:75px;left:180px;">' + 'Precio' + '</span>';
    strHTML += 'Precio';
    strHTML += '</th>';

    strHTML += ' </tr>';
    //    strHTML += '</thead>';
    strHTML += '</table>'; // Fin Encabezado

    // Scroll 
    strHTML += '<div style="max-height:250px;overflow-y:scroll;overflow-x:hidden;">';

    // Cuerpo
    strHTML += '<table id="tb_' + pIndexCarrito + '"  style="width:240px;  !important;" class="tbl-carro" border="0" cellspacing="0" cellpadding="0">';
    strHTML += '<tbody>';
    var nroTotalCarrito = parseFloat(0);
    for (var iProductos = 0; iProductos < listaCarritos[pIndexCarrito].listaProductos.length; iProductos++) {
        // Precio Calcular por producto
        var nroTotalProducto = CalcularPrecioProductosEnCarrito(listaCarritos[pIndexCarrito].listaProductos[iProductos].PrecioFinal, listaCarritos[pIndexCarrito].listaProductos[iProductos].cantidad, listaCarritos[pIndexCarrito].listaProductos[iProductos].pro_ofeunidades, listaCarritos[pIndexCarrito].listaProductos[iProductos].pro_ofeporcentaje);
        strHTML += '<tr >';
        var strHtmlColor = '';
        if (iProductos % 2 != 0) {
            strHtmlColor = ' carro-td-color ';
        }
        var htmlColor = '';
        if (listaCarritos[pIndexCarrito].listaProductos[iProductos].stk_stock == 'N') {
            htmlColor = ' colorRojo ';
        }
        strHTML += '<td  width="85%" class="' + strHtmlColor + htmlColor + 'first-td">' + listaCarritos[pIndexCarrito].listaProductos[iProductos].pro_nombre + '</td>';
        strHTML += '<td width="7%"   class="' + strHtmlColor + '"><input id="inputCarrito' + pIndexCarrito + '_' + iProductos + '" type="text"  value="' + listaCarritos[pIndexCarrito].listaProductos[iProductos].cantidad + '" onblur="onblurInputCarrito(this)" onfocus="onfocusInputCarrito(this)" onkeypress="return onKeypressCantProductos(event)" />' + '</td>';
        var strHtmlPrecioProducto = '';
        if (listaCarritos[pIndexCarrito].listaProductos[iProductos].stk_stock == 'N') {
            nroTotalProducto = 0;
        } else {
            strHtmlPrecioProducto = '$&nbsp;' + FormatoDecimalConDivisorMiles(nroTotalProducto.toFixed(2));
        }
        strHTML += '<td width="7%"  id="tdPrecio' + pIndexCarrito + '_' + iProductos + '" class="' + strHtmlColor + '"> ' + strHtmlPrecioProducto + '</td> ';
        strHTML += '</tr>';
        nroTotalCarrito = nroTotalCarrito + nroTotalProducto;
    }
    strHTML += '</tbody>';
    strHTML += '</table>'; // Fin Cuerpo

    strHTML += '</div>'; // fin Scroll

    strHTML += '<table  class="tbl-carro" border="0" cellspacing="0" cellpadding="0">'; // Pie Pagina
    strHTML += '<tr>';
    strHTML += '<td width="80%" colspan="2" class="carro-total first-td">Total</td>';
    strHTML += '<td width="20%" id="tdTotal' + pIndexCarrito + '" class="carro-total">$&nbsp;' + FormatoDecimalConDivisorMiles(nroTotalCarrito.toFixed(2)) + '</td>';
    strHTML += '</tr>';
    strHTML += '</table>'; // Fin Pie Pagina


    //    strHTML += '</div>';

    //    strHTML += '</div>';  //fin '<div  class="table-wrapper">';
    //    strHTML += '</div>'; //fin '<div  class="table-scroll">';

    strHTML += ' <a class="carro-btn-confirmar" onclick="onclickConfirmarCarrito(' + pIndexCarrito + ')" href="#">Confirmar</a> ';
    strHTML += '<a class="carro-btn-vaciar" onclick="onclickVaciarCarrito(' + pIndexCarrito + ')"  href="#">Vaciar</a>';
    strHTML += ' <div class="clear">';
    strHTML += '  </div>';
    strHTML += ' </div>';

    return strHTML;
}

function onclickVaciarCarrito(pIndexCarrito) {
    $("#hiddenIndexCarrito").val(pIndexCarrito);
    PageMethods.BorrarCarrito(listaCarritos[pIndexCarrito].lrc_id, listaCarritos[pIndexCarrito].codSucursal, OnCallBackBorrarCarrito, OnFail);


}
function OnCallBackBorrarCarrito(args) {
    if (args) {
        var indexCarrito = $("#hiddenIndexCarrito").val();
        $('#divContenedorCarrito_' + indexCarrito).remove();
        LimpiarTextBoxProductosBuscados(listaCarritos[indexCarrito].codSucursal);
        listaCarritos[indexCarrito].codSucursal = '';
    }
}
function onfocusInputCarrito(pValor) {
    selectedInput = null;
    selectedInputTransfer = null;
    selectInputCarrito = pValor;
}
function onblurInputCarrito(pValor) {
    if (pValor.value != '') {
        var nombre = pValor.id;
        nombre = nombre.replace('inputCarrito', '');
        var palabrasBase = nombre.split("_");
        var fila = parseInt(palabrasBase[1]);
        var columna = parseInt(palabrasBase[0]);
        var cantidadProducto = pValor.value;

        var isNotMaximaCantidadSuperada = true;
        if (listaCarritos[columna].listaProductos[fila].pro_canmaxima != null) {
            if (listaCarritos[columna].listaProductos[fila].pro_canmaxima < cantidadProducto) {
                isNotMaximaCantidadSuperada = false;
            }
        }
        if (isNotMaximaCantidadSuperada) {
            CargarOActualizarListaCarrito(listaCarritos[columna].codSucursal, listaCarritos[columna].listaProductos[fila].codProducto, cantidadProducto, false);
            CargarHtmlCantidadDeCarritoABuscador(listaCarritos[columna].codSucursal, listaCarritos[columna].listaProductos[fila].codProducto, cantidadProducto);
        } else {
            //            var mytext = $("#inputSuc" + i + "_" + iEncabezadoSucursal);
            alert(MostrarTextoSuperaCantidadMaxima(listaCarritos[columna].listaProductos[fila].pro_nombre, listaCarritos[columna].listaProductos[fila].pro_canmaxima));
            //            alert(strMensajeCantidadMaximaSuperada + ' - - ' + listaCarritos[columna].listaProductos[fila].codProducto + ' -  ' + listaCarritos[columna].listaProductos[fila].pro_canmaxima);
            var cantidadAnterior = listaCarritos[columna].listaProductos[fila].cantidad;
            if (cantidadAnterior != '') {
                pValor.value = cantidadAnterior;
            }

        }
    }
}
function CargarHtmlCantidadDeCarritoABuscador(pIdSucursal, pIdProduco, pCantidad) {
    if (listaProductosBuscados != null) {
        for (var i = 0; i < listaProductosBuscados.length; i++) {
            if (listaProductosBuscados[i].pro_codigo == pIdProduco) {
                for (var iEncabezadoSucursal = 0; iEncabezadoSucursal < listaSucursal.length; iEncabezadoSucursal++) {
                    if (listaSucursal[iEncabezadoSucursal] == pIdSucursal) {//listaProductosBuscados[i].listaSucursalStocks[iSucursal].stk_codsuc
                        var mytext = $("#inputSuc" + i + "_" + iEncabezadoSucursal);
                        if (mytext.length <= 0) {
                            mytext = null;
                        }
                        if (mytext != null) {
                            mytext.val(pCantidad);
                        }
                        break;
                    }

                }
                break;
            }
        }
    }
}
function OnCallBackCargarCarritoDiferido(args) {

}
function onNullBuscar() {
    selectedInput = null;
    selectInputCarrito = null;
    selectedInputTransfer = null;
}
function onClickBuscar() {
    // Limpiar Paginador
    $('#divPaginador').html('');
    intPaginadorTipoDeRecuperar = 0; // setea
    var isBuscar = false;
    // Limpiar detalle producto si se encuentra abierto
    OnMouseOutProdructo();
    //
    varPalabraBuscador = jQuery("#txtBuscador").val().trim();
    if (varPalabraBuscador != '') {
        if (varPalabraBuscador.length > 2) {
            if ($('#checkNombre').is(':checked') || $('#checkBoxCodigoBarra').is(':checked') || $('#checkBoxMonodroga').is(':checked') || $('#checkBoxLaboratorio').is(':checked') || $('#checkBoxCodigoAlfaBeta').is(':checked')) {
                //
                Ascender_pro_precio = true;
                Ascender_PrecioFinal = true;
                Ascender_PrecioConDescuentoOferta = true;
                //
                if ($('#checkNombre').is(':checked') && $('#checkBoxCodigoBarra').is(':checked') && !$('#checkBoxMonodroga').is(':checked') && $('#checkBoxLaboratorio').is(':checked') && !$('#checkBoxCodigoAlfaBeta').is(':checked')) {
                    PageMethods.RecuperarProductos(varPalabraBuscador, OnCallBackRecuperarProductos, OnFail);
                } else {
                    var arrayListaColumna = new Array();
                    if ($('#checkNombre').is(':checked')) {
                        arrayListaColumna.push($('#checkNombre').val());
                    }
                    if ($('#checkBoxCodigoBarra').is(':checked')) {
                        arrayListaColumna.push($('#checkBoxCodigoBarra').val());
                    }
                    if ($('#checkBoxMonodroga').is(':checked')) {
                        arrayListaColumna.push($('#checkBoxMonodroga').val());
                    }
                    if ($('#checkBoxLaboratorio').is(':checked')) {
                        arrayListaColumna.push($('#checkBoxLaboratorio').val());
                    }
                    if ($('#checkBoxCodigoAlfaBeta').is(':checked')) {
                        arrayListaColumna.push($('#checkBoxCodigoAlfaBeta').val());
                    }
                    PageMethods.RecuperarProductosVariasColumnas(varPalabraBuscador, arrayListaColumna, OnCallBackRecuperarProductos, OnFail);
                }
            } else {
                alert('Seleccione por lo menos una opción de búsqueda');
            }
            isBuscar = true;
        }
    }
    if (!isBuscar) {
        jQuery("#divResultadoBuscador").html('');
        //        $('#divPaginador').html('');
    }
}
function onkeypressEnter(e, elemento) {
    tecla = (document.all) ? e.keyCode : e.which;
    if (tecla == 13) {
        onClickBuscar();
    }
    //    return false;
}
var timerProducto = null;
function OnMouseOverProdructo(pIndice) {
    if ($("#divMostradorProducto").css("display") == 'none') {
        //        $("#divMostradorProducto").css("display", "block");
        LimpiarTimeoutProducto();
        timerProducto = setTimeout(function () { AnimarPresentacionProducto(pIndice); }, 300);
    }
}
function OnMouseOutProdructo() {
    if ($("#divMostradorProducto").css("display") == 'block') {
        $("#divMostradorProducto").css("display", "none");
    }
    LimpiarTimeoutProducto();
}
function LimpiarTimeoutProducto() {
    if (timerProducto) {
        clearTimeout(timerProducto);
        timerProducto = null;
    }
}

function AnimarPresentacionProducto(pIndice) {
    CargarDatosProductos(pIndice);
    $("#divMostradorProducto").css("display", "block");
    LimpiarTimeoutProducto();
}

function OnMouseMoveProdructo(e) {
    if (typeof (e) == 'undefined') {
        e = event;
    }

    var bt = document.body.scrollTop;
    //    var bl = document.body.scrollLeft;
    var et = document.documentElement ? document.documentElement.scrollTop : null;
    //    var el = document.documentElement ?  document.documentElement.scrollLeft : null;
    //   (bl || el)
    //    var doc = document,
    //    vpt = doc.documentElement || doc.body;
    var top = e.clientY || e.pageY;
    var left = e.clientX || e.pageX;

    $("#divMostradorProducto").css("top", (top + (bt || et) + 20) + 'px');
    $("#divMostradorProducto").css("left", (left + 20) + 'px');
}

function CargarDatosProductos(pIndice) {

    if (listaProductosBuscados[pIndice].pro_nombre != null) {
        $('#tdNombre').html(AgregarMark(listaProductosBuscados[pIndice].pro_nombre));
    } else {
        $('#tdNombre').html('');
    }
    if (listaProductosBuscados[pIndice].pro_laboratorio != null) {
        $('#tdLaboratorio').html(AgregarMark(listaProductosBuscados[pIndice].pro_laboratorio));
    } else {
        $('#tdLaboratorio').html('');
    }
    if (listaProductosBuscados[pIndice].pro_monodroga != null) {
        $('#tdMonodroga').html(AgregarMark(listaProductosBuscados[pIndice].pro_monodroga));
    } else {
        $('#tdMonodroga').html('');
    }
    if (listaProductosBuscados[pIndice].pro_codigobarra != null) {
        $('#tdCodigoBarra').html(AgregarMark(listaProductosBuscados[pIndice].pro_codigobarra));
    } else {
        $('#tdCodigoBarra').html('');
    }
    if (listaProductosBuscados[pIndice].pro_codigoalfabeta != null) {
        $('#tdCodigoAlfaBeta').html(AgregarMark(listaProductosBuscados[pIndice].pro_codigoalfabeta));
    } else {
        $('#tdCodigoAlfaBeta').html('');
    }
    if (listaProductosBuscados[pIndice].pro_neto != null) {
        if (listaProductosBuscados[pIndice].pro_neto) {
            $('#tdTipoVenta').html('Gravado');
        } else {
            $('#tdTipoVenta').html('Exento');
        }
    } else {
        $('#tdTipoVenta').html('');
    }
    if (listaProductosBuscados[pIndice].pro_codtpopro != null) {
        var mostrarTipoProducto = '';
        switch (listaProductosBuscados[pIndice].pro_codtpopro) {
            case 'A':
                mostrarTipoProducto = 'Accesorio';
                break;
            case 'P':
                mostrarTipoProducto = 'Perfumeria';
                break;
            case 'F':
                mostrarTipoProducto = 'Perfumeria';
                break;
            case 'M':
                if (listaProductosBuscados[pIndice].pro_codtpovta == 'VL') {
                    mostrarTipoProducto = 'Venta libre';
                } else if (listaProductosBuscados[pIndice].pro_codtpovta == 'BR') {
                    mostrarTipoProducto = 'Bajo receta';
                } else if (listaProductosBuscados[pIndice].pro_codtpovta[0] == 'P') {
                    mostrarTipoProducto = 'Psicotrópico Lista ' + listaProductosBuscados[pIndice].pro_codtpovta[1];
                } else if (listaProductosBuscados[pIndice].pro_codtpovta[0] == 'E') {
                    mostrarTipoProducto = 'Estupefaciente Lista ' + listaProductosBuscados[pIndice].pro_codtpovta[1];
                }
                break;
            default:
                break;
        }
        $('#tdTipoProducto').html(mostrarTipoProducto);
    } else {
        $('#tdTipoProducto').html('');
    }
    if (listaProductosBuscados[pIndice].pro_isCadenaFrio) {
        $('#tdCadenaFrio').html('Si');
    } else {
        $('#tdCadenaFrio').html('No');
    }
    if (listaProductosBuscados[pIndice].pro_isTrazable) {
        $('#tdTrazable').html('Si');
    } else {
        $('#tdTrazable').html('No');
    }

}
function onclickOrdenarProducto(pValor) {
    //    alert(pValor);

    if (pValor == 0) {
        PageMethods.RecuperarProductosOrdenar('pro_precio', Ascender_pro_precio, OnCallBackRecuperarProductos, OnFail);
        Ascender_pro_precio = !Ascender_pro_precio;
    }
    if (pValor == 1) {
        PageMethods.RecuperarProductosOrdenar('PrecioFinal', Ascender_PrecioFinal, OnCallBackRecuperarProductos, OnFail);
        Ascender_PrecioFinal = !Ascender_PrecioFinal;
    }
    if (pValor == 2) {
        PageMethods.RecuperarProductosOrdenar('PrecioConDescuentoOferta', Ascender_PrecioConDescuentoOferta, OnCallBackRecuperarProductos, OnFail);
        Ascender_PrecioConDescuentoOferta = !Ascender_PrecioConDescuentoOferta;
    }
    //    listaProductosBuscados[i]. // 0
    //    listaProductosBuscados[i].PrecioFinal // 1
    //    listaProductosBuscados[i].PrecioConDescuentoOferta // 2
}


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
                strHtml += '<table class="tbl-buscador-productos" border="0" cellspacing="0" cellpadding="0">';
                strHtml += '<tr>';
                strHtml += '<th class="bp-off-top-left" rowspan="2"><div class="bp-top-left">Detalle Producto</div></th>';
                strHtml += '<th class="bp-med-ancho thOrdenar" rowspan="2"  onclick="onclickOrdenarProducto(0)"> Precio Público</th>';
                strHtml += '<th class="bp-med-ancho thOrdenar" rowspan="2"  onclick="onclickOrdenarProducto(1)">Precio Cliente</th>';
                strHtml += '<th class="bg-oferta" colspan="3">Oferta</th>';

                for (var iEncabezadoSucursal = 0; iEncabezadoSucursal < listaSucursal.length; iEncabezadoSucursal++) {
                    strHtml += '<th class="bp-ancho" rowspan="2">';
                    strHtml += ConvertirSucursalParaColumno(listaSucursal[iEncabezadoSucursal]);
                    strHtml += '</th>';
                }

                strHtml += '</tr>';

                strHtml += '<tr>';
                strHtml += '<th class="bp-min-ancho">%</th>';
                strHtml += '<th class="bp-min-ancho">Cant.</th>';
                strHtml += '<th class="bp-med-ancho thOrdenar" onclick="onclickOrdenarProducto(2)">Precio</th>';
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
                    strHtml += '<tr>';
                    strHtml += '<td class="first-td2';
                    var strHtmlColorFondo = '';
                    if (i % 2 != 0) {
                        strHtmlColorFondo = ' bp-td-color';
                    }
                    strHtml += strHtmlColorFondo + '">'; //'<td class="first-td2">' + class="trFunciones"  onclick="AnimarPresentacionProducto(' + i + ')"
                    //onclick="RecuperarTransfer(' + i + ')"
                    strHtml += '<div  OnMouseMove="OnMouseMoveProdructo(event)" OnMouseOver="OnMouseOverProdructo(' + i + ')" OnMouseOut="OnMouseOutProdructo()"  style="cursor:pointer;" >' + AgregarMark(listaProductosBuscados[i].pro_nombre);
                    // Agregar:
                    if (isNotGLNisTrazable) {
                        strHtml += '<span class="spanProductoTrazableCLiSinGLN" >&nbsp;&nbsp;&nbsp;Producto trazable. Farmacia sin GLN.</span>'; //style="padding-left:4px;"
                    }
                    if (isClienteTomaTransfer) {
//                        if (listaProductosBuscados[i].isTieneTransfer) {
//                            strHtml += '<span>&nbsp;&nbsp;&raquo;&nbsp;En transfer</span>'; //style="padding-left:4px;"
//                        }
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
                    // Ver si mostrar input solo producto Transfer 
                    if (listaProductosBuscados[i].pro_vtasolotransfer) {
                        isMostrarImput = false;
                    }
                    strHtml += '</div>';
                    strHtml += '</td>'; //<span>&raquo;En transfer</span>
                    strHtml += '<td style="text-align:right;"  class="' + strHtmlColorFondo + '">$&nbsp;' + FormatoDecimalConDivisorMiles(listaProductosBuscados[i].pro_precio.toFixed(2)) + '</td>';
                    strHtml += '<td style="text-align:right;"  class="' + strHtmlColorFondo + '">$&nbsp;' + FormatoDecimalConDivisorMiles(listaProductosBuscados[i].PrecioFinal.toFixed(2)) + '</td>'; // listaProductosBuscados[i].PrecioDescuentoFarmacia.toFixed(2)
                    var varOfeunidades = '';
                    var varOfeporcentaje = '';
                    var varPrecioConDescuentoOferta = '';
                    if (isClienteTomaOferta) {
                        if (listaProductosBuscados[i].pro_ofeunidades != 0 || listaProductosBuscados[i].pro_ofeporcentaje != 0) {
                            varOfeunidades = listaProductosBuscados[i].pro_ofeunidades;
                            varOfeporcentaje = listaProductosBuscados[i].pro_ofeporcentaje;
                            varPrecioConDescuentoOferta = '$&nbsp;' + FormatoDecimalConDivisorMiles(listaProductosBuscados[i].PrecioConDescuentoOferta.toFixed(2));
                        }
                    }
                    strHtml += '<td class="' + strHtmlColorFondo + '">' + varOfeporcentaje + '</td>';
                    strHtml += '<td class="' + strHtmlColorFondo + '">' + varOfeunidades + '</td>';
                    strHtml += '<td style="text-align:right;"  class="' + strHtmlColorFondo + '">' + varPrecioConDescuentoOferta + '</td>';
                    // Optimizar
                    for (var iEncabezadoSucursal = 0; iEncabezadoSucursal < listaSucursal.length; iEncabezadoSucursal++) {
                        strHtml += '<td  class="' + strHtmlColorFondo + '">';
                        if (intPaginadorTipoDeRecuperar != 2) {// todo transfer
                            for (var iSucursal = 0; iSucursal < listaProductosBuscados[i].listaSucursalStocks.length; iSucursal++) {
                                if (listaProductosBuscados[i].listaSucursalStocks[iSucursal].stk_codsuc == listaSucursal[iEncabezadoSucursal]) {
                                    strHtml += '<div class="cont-estado-input"><div class="estado-' + listaProductosBuscados[i].listaSucursalStocks[iSucursal].stk_stock.toLowerCase() + '"></div>';
                                    // if (!isNotGLNisTrazable) {
                                    if (isMostrarImput) {
                                        // Cargar Cantidad
                                        var cantidadDeProductoEnCarrito = ObtenerCantidadProducto(listaSucursal[iEncabezadoSucursal], listaProductosBuscados[i].pro_codigo);
                                        strHtml += '<input class="cssFocusCantProdCarrito" id="inputSuc' + i + "_" + iEncabezadoSucursal + '" type="text"  onfocus="onfocusSucursal(this)" onblur="onblurSucursal(this)" onkeypress="return onKeypressCantProductos(event)" value="' + cantidadDeProductoEnCarrito + '" ></input>';
                                    }
                                    strHtml += '</div>';
                                    break;
                                }
                            }
                        } // fin      if (intPaginadorTipoDeRecuperar != 2) { 
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
function ObtenerCantidadProducto(pIdSucursal, pIdProduco) {
    var resultado = '';
    if (listaCarritos != null) {
        for (var i = 0; i < listaCarritos.length; i++) {
            if (listaCarritos[i].codSucursal == pIdSucursal) {
                for (var iProducto = 0; iProducto < listaCarritos[i].listaProductos.length; iProducto++) {
                    if (listaCarritos[i].listaProductos[iProducto].codProducto == pIdProduco) {
                        resultado = listaCarritos[i].listaProductos[iProducto].cantidad;
                        break;
                    }
                }
                break;
            } // for (var i = 0; i < listaCarritos.length; i++) {
        }
    }
    return resultado;
}

//$(".cssFocusCantProdCarrito").focusin(function () {
//    selectedInput = $(this);
//    alert('css');
//});
function AgregarMark(pValor) {
    var valorTemp = pValor.toUpperCase();
    var tempPalabraBuscador = varPalabraBuscador.toUpperCase();
    var palabrasBase = tempPalabraBuscador.split(" ");
    var palabrasReales = [];
    for (var i = 0; i < palabrasBase.length; i++) {
        var p = palabrasBase[i].replace(/([\.\/\(\)\[\]\*\?\{\}\^\$])/g, "\\$1");
        if (p != '') {
            palabrasReales.push(p);
        }
    }
    for (var i = 0; i < palabrasReales.length; i++) {
        var re = new RegExp("(" + palabrasReales[i] + ")", 'g');
        valorTemp = valorTemp.replace(re, "<mark>$1</mark>");
    }
    return valorTemp;
}

function onfocusSucursal(pValor) {
    selectInputCarrito = null;
    selectedInputTransfer = null;
    selectedInput = pValor;
}
function onblurSucursal(pValor) {
    if (pValor.value != '') {
        var nombre = pValor.id;
        nombre = nombre.replace('inputSuc', '');
        var palabrasBase = nombre.split("_");
        var fila = parseInt(palabrasBase[0]);
        var columna = parseInt(palabrasBase[1]);
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
    }
}
function MostrarTextoSuperaCantidadMaxima(pNombreProducto, pCantidadMaxima) {
    return 'El producto: ' + pNombreProducto + ' \n' + 'Supera la cantidad máxima: ' + pCantidadMaxima;

}
function AgregarAlHistorialProductoCarrito(pIndexProducto, pIndexSucursal, pCantidadProducto, pIsSumarCantidad) {

    PageMethods.CargarCarritoDiferido(listaSucursal[pIndexSucursal], listaProductosBuscados[pIndexProducto].pro_codigo, pCantidadProducto, OnCallBackCargarCarritoDiferido, OnFail);
//    PageMethods.HistorialProductoCarrito(listaProductosBuscados[pIndexProducto].pro_codigo, listaProductosBuscados[pIndexProducto].pro_nombre, listaSucursal[pIndexSucursal], pCantidadProducto, pIsSumarCantidad, OnCallBackHistorialProductoCarrito, OnFail);
    CargarOActualizarListaCarrito(listaSucursal[pIndexSucursal], listaProductosBuscados[pIndexProducto].pro_codigo, pCantidadProducto, true);
}
//function ActualizarCarritos() {
//}
function OnCallBackHistorialProductoCarrito(args) {
    //HistorialProductoCarrito(string pIdProducto, string pPalabraElegiada)
}
//var tecla = pEvent.keycode ? pEvent.keycode : pEvent.which;
//alert(tecla);
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
//function agregarFilaAlCarrito(pIndiceCarrito, pIndiceProductoEnCarrito, pNombreProducto, pCantidad, pPrecio) {
//    /// Cambiar el estilo dependiendo la paridad
//    var strHtmlColor = '';
//    if (pIndiceProductoEnCarrito % 2 != 0) {
//        strHtmlColor += ' carro-td-color ';
//    }
//    var tableID = 'tb_' + pIndiceCarrito;
//    var pNombreInputProducto = 'inputCarrito' + pIndiceCarrito + '_' + pIndiceProductoEnCarrito;
//    var pNombreInputTotalPrecio = 'tdPrecio' + pIndiceCarrito + '_' + pIndiceProductoEnCarrito;
//    var table = document.getElementById(tableID);
//    var rowCount = table.rows.length - 1;
//    var row = table.insertRow(rowCount);
//    var cellNombreProducto = row.insertCell(0);
//    var colorProductoSinStock = '';
//    if (listaCarritos[pIndiceCarrito].listaProductos[pIndiceProductoEnCarrito - 1].stk_stock == 'N') {
//        colorProductoSinStock = ' colorRojo ';
//    }
//    cellNombreProducto.className = 'first-td ' + strHtmlColor + colorProductoSinStock;
//    //    cellNombreProducto.align = 'left';
//    var newContent = document.createTextNode(pNombreProducto);
//    //    newContent.className = colorProductoSinStock;
//    cellNombreProducto.appendChild(newContent);
//    var cellCantidad = row.insertCell(1);
//    cellCantidad.className = strHtmlColor;
//    var elementCantidad = document.createElement("input");
//    elementCantidad.id = pNombreInputProducto;
//    elementCantidad.type = "text";
//    elementCantidad.value = pCantidad;
//    //    elementCantidad.className = 'cssInputCarrito';
//    //    elementCantidad.onblur = "onblurInputCarrito(this)";
//    elementCantidad.onblur = function () {
//        onblurInputCarrito(this);
//    };
//    //    elementCantidad.onfocus = "onfocusInputCarrito(this)";
//    elementCantidad.onfocus = function () {
//        onfocusInputCarrito(this);
//    };
//    //    elementCantidad.onKeypress = "return onKeypressCantProductos(event)";
//    elementCantidad.onkeypress = function () {
//        return onKeypressCantProductos(event);
//    };
//    cellCantidad.appendChild(elementCantidad);
//    var cellPrecio = row.insertCell(2);
//    cellPrecio.className = strHtmlColor;
//    cellPrecio.id = pNombreInputTotalPrecio;
//    var strHtmlPrecioProducto = '$&nbsp;' + pPrecio;
//    if (listaCarritos[pIndiceCarrito].listaProductos[pIndiceProductoEnCarrito - 1].stk_stock == 'N') {
//        strHtmlPrecioProducto = '';
//        //        cellNombreProducto.className += ' colorRojo ';
//    }
//    var elementPrecio = document.createTextNode(strHtmlPrecioProducto);
//    cellPrecio.appendChild(elementPrecio);
//}
function agregarFilaAlCarrito(pIndiceCarrito, pIndiceProductoEnCarrito, pNombreProducto, pCantidad, pPrecio) {

    /// Cambiar el estilo dependiendo la paridad
    var strHtmlColor = '';
    if (pIndiceProductoEnCarrito % 2 != 0) {
        strHtmlColor += ' carro-td-color ';
    }
    var tableID = 'tb_' + pIndiceCarrito;
    var pNombreInputProducto = 'inputCarrito' + pIndiceCarrito + '_' + pIndiceProductoEnCarrito;
    var pNombreInputTotalPrecio = 'tdPrecio' + pIndiceCarrito + '_' + pIndiceProductoEnCarrito;
    var table = document.getElementById(tableID);
    //var rowCount = table.rows.length - 1;
    var rowCount = table.rows.length;
    var row = table.insertRow(rowCount);

    var cellNombreProducto = row.insertCell(0);
    var colorProductoSinStock = '';
    if (listaCarritos[pIndiceCarrito].listaProductos[pIndiceProductoEnCarrito].stk_stock == 'N') {
        colorProductoSinStock = ' colorRojo ';
    }
    cellNombreProducto.className = 'first-td ' + strHtmlColor + colorProductoSinStock;
    //    cellNombreProducto.align = 'left';
    var newContent = document.createTextNode(pNombreProducto);
    //    newContent.className = colorProductoSinStock;
    cellNombreProducto.appendChild(newContent);

    var cellCantidad = row.insertCell(1);
    cellCantidad.className = strHtmlColor;
    var elementCantidad = document.createElement("input");
    elementCantidad.id = pNombreInputProducto;
    elementCantidad.type = "text";
    elementCantidad.value = pCantidad;
    //    elementCantidad.className = 'cssInputCarrito';
    //    elementCantidad.onblur = "onblurInputCarrito(this)";
    elementCantidad.onblur = function () {
        onblurInputCarrito(this);
    };
    //    elementCantidad.onfocus = "onfocusInputCarrito(this)";
    elementCantidad.onfocus = function () {
        onfocusInputCarrito(this);
    };
    //    elementCantidad.onKeypress = "return onKeypressCantProductos(event)";
    elementCantidad.onkeypress = function () {
        return onKeypressCantProductos(event);
    };

    cellCantidad.appendChild(elementCantidad);

    var cellPrecio = row.insertCell(2);
    cellPrecio.className = strHtmlColor;
    cellPrecio.id = pNombreInputTotalPrecio;

    var strHtmlPrecioProducto = '$&nbsp;' + FormatoDecimalConDivisorMiles(pPrecio);
    if (listaCarritos[pIndiceCarrito].listaProductos[pIndiceProductoEnCarrito].stk_stock == 'N') {
        strHtmlPrecioProducto = '';
        //        cellNombreProducto.className += ' colorRojo ';
    }
    var elementPrecio = document.createTextNode(strHtmlPrecioProducto);
    cellPrecio.appendChild(elementPrecio);
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
                    jQuery("#txtBuscador").focus();
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
                }
            }
        } else if (selectInputCarrito != null) {


            var isActualizarActual_enter = false;
            var indiceCarrito = -1;
            var indiceCarritoProducto = -1;

            var nombre = selectInputCarrito.id;
            nombre = nombre.replace('inputCarrito', '');
            var palabrasBase = nombre.split("_");
            indiceCarrito = parseInt(palabrasBase[0]);
            indiceCarritoProducto = parseInt(palabrasBase[1]);
            var cantMaxFilaCarrito = listaCarritos[indiceCarrito].listaProductos.length;
            var isSalirWhileCarrito = false;
            switch (keyCode) {
                case 38: //arriba
                    if (indiceCarritoProducto != 0) {
                        indiceCarritoProducto--;
                    } else { isSalirWhileCarrito = true; }
                    break;
                case 40: //abajo
                    if (indiceCarritoProducto < cantMaxFilaCarrito - 1) {
                        indiceCarritoProducto++;
                    } else { isSalirWhileCarrito = true; }
                    break;
                case 13: // Enter
                    isActualizarActual_enter = true;
                    break;
                default:
                    break;
            }
            var objCarrito = null;
            if (!isSalirWhileCarrito) {
                objCarrito = $("#inputCarrito" + indiceCarrito + "_" + indiceCarritoProducto);
                if (objCarrito.length <= 0) {
                    objCarrito = null;
                }
            }
            if (isActualizarActual_enter) {
                //                if (!NoPasoUnaVez) {
                if (objCarrito != null) {

                    //                    var isNotMaximaCantidadSuperada = true;
                    //                    if (listaCarritos[indiceCarrito].listaProductos[indiceCarritoProducto].pro_canmaxima != null) {
                    //                        if (listaCarritos[indiceCarrito].listaProductos[indiceCarritoProducto].pro_canmaxima < objCarrito.val()) {
                    //                            isNotMaximaCantidadSuperada = false;
                    //                        }
                    //                    }
                    //                    if (isNotMaximaCantidadSuperada) {
                    //                        CargarOActualizarListaCarrito(listaCarritos[indiceCarrito].codSucursal, listaCarritos[indiceCarrito].listaProductos[indiceCarritoProducto].codProducto, objCarrito.val(), false);
                    //                        CargarHtmlCantidadDeCarritoABuscador(listaCarritos[indiceCarrito].codSucursal, listaCarritos[indiceCarrito].listaProductos[indiceCarritoProducto].codProducto, objCarrito.val());
                    //                    } else {
                    //                        alert(MostrarTextoSuperaCantidadMaxima(listaCarritos[indiceCarrito].listaProductos[indiceCarritoProducto].pro_nombre, listaCarritos[indiceCarrito].listaProductos[indiceCarritoProducto].pro_canmaxima));
                    //                        var cantidadAnterior = listaCarritos[indiceCarrito].listaProductos[indiceCarritoProducto].cantidad;
                    //                        if (cantidadAnterior != '') {
                    //                            objCarrito.value = cantidadAnterior;
                    //                        }
                    //                    }
                    onblurInputCarrito(selectInputCarrito);
                }
            } else {
                if (objCarrito != null) {
                    objCarrito.focus();
                }
            }

        } else if (selectedInputTransfer != null) {
            var indiceTransfer = -1;
            var indiceTransferProducto = -1;
            var nombre = selectedInputTransfer.id;
            nombre = nombre.replace('txtProdTransf', '');
            var palabrasBase = nombre.split("_");
            indiceTransfer = parseInt(palabrasBase[0]);
            indiceTransferProducto = parseInt(palabrasBase[1]);
            var cantMaxFilaTransfer = listaTransfer[indiceTransfer].listaDetalle.length;
            switch (keyCode) {
                case 38: //arriba
                    if (indiceTransferProducto != 0) {
                        indiceTransferProducto--;
                    }
                    break;
                case 40: //abajo
                    if (indiceTransferProducto < cantMaxFilaTransfer - 1) {
                        indiceTransferProducto++;
                    }
                    break;
                default:
                    break;
            }
            var objTransfer = $("#txtProdTransf" + indiceTransfer + "_" + indiceTransferProducto);
            if (objTransfer.length <= 0) {
                objTransfer = null;
            }
            if (objTransfer != null) {
                objTransfer.focus();
            }
        }
    }
    return true;
}
function CargarOActualizarListaCarrito(pIdSucursal, pIdProduco, pCantidadProducto, pIsDesdeBuscador) {
    //    var isInformarSiQuiereSuma = false;
    //    var CantidadOriginal = pCantidadProducto;
    if (listaCarritos == null) {
        listaCarritos = [];
    }
    var isCarritoFind = false;
    for (var i = 0; i < listaCarritos.length; i++) {
        if (listaCarritos[i].codSucursal == pIdSucursal) {
            var sumaTotal = ObtenerPrecioConFormato($('#tdTotal' + i).html());
            var isProductoFind = false;
            var indiceProducto = -1;
            for (var iProducto = 0; iProducto < listaCarritos[i].listaProductos.length; iProducto++) {
                if (listaCarritos[i].listaProductos[iProducto].codProducto == pIdProduco) {
                    if (listaCarritos[i].listaProductos[iProducto].stk_stock != 'N') {
                        sumaTotal = sumaTotal - CalcularPrecioProductosEnCarrito(listaCarritos[i].listaProductos[iProducto].PrecioFinal, listaCarritos[i].listaProductos[iProducto].cantidad, listaCarritos[i].listaProductos[iProducto].pro_ofeunidades, listaCarritos[i].listaProductos[iProducto].pro_ofeporcentaje);
                    }
                    // Se hace una suma si esta en el carrito
                    //                    if (pIsDesdeBuscador) {
                    //                        listaCarritos[i].listaProductos[iProducto].cantidad = pCantidadProducto;
                    //                    } else {
                    //                        listaCarritos[i].listaProductos[iProducto].cantidad = pCantidadProducto;
                    //                    }
                    listaCarritos[i].listaProductos[iProducto].cantidad = pCantidadProducto;
                    isProductoFind = true;
                    indiceProducto = iProducto;
                    /////
                    if (listaProductosBuscados != null) {
                        for (var iProductoBuscador = 0; iProductoBuscador < listaProductosBuscados.length; iProductoBuscador++) {
                            if (listaProductosBuscados[iProductoBuscador].pro_codigo == pIdProduco) {
                                for (var iStock = 0; iStock < listaProductosBuscados[iProductoBuscador].listaSucursalStocks.length; iStock++) {
                                    if (listaProductosBuscados[iProductoBuscador].listaSucursalStocks[iStock].stk_codsuc == pIdSucursal) {
                                        listaCarritos[i].listaProductos[iProducto].stk_stock = listaProductosBuscados[iProductoBuscador].listaSucursalStocks[iStock].stk_stock;
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                    }
                    /////
                    break;
                }
            }
            if (!isProductoFind) {
                var produc = new cProductosEnCarrito();
                produc.codProducto = pIdProduco;
                produc.cantidad = pCantidadProducto;
                for (var iProductoBuscador = 0; iProductoBuscador < listaProductosBuscados.length; iProductoBuscador++) {
                    if (listaProductosBuscados[iProductoBuscador].pro_codigo == pIdProduco) {
                        produc.pro_nombre = listaProductosBuscados[iProductoBuscador].pro_nombre;
                        produc.pro_precio = listaProductosBuscados[iProductoBuscador].pro_precio;
                        produc.pro_preciofarmacia = listaProductosBuscados[iProductoBuscador].pro_preciofarmacia;
                        produc.PrecioFinal = listaProductosBuscados[iProductoBuscador].PrecioFinal;
                        produc.pro_ofeunidades = listaProductosBuscados[iProductoBuscador].pro_ofeunidades;
                        produc.pro_ofeporcentaje = listaProductosBuscados[iProductoBuscador].pro_ofeporcentaje;
                        produc.pro_canmaxima = listaProductosBuscados[iProductoBuscador].pro_canmaxima;
                        for (var iStock = 0; iStock < listaProductosBuscados[iProductoBuscador].listaSucursalStocks.length; iStock++) {
                            if (listaProductosBuscados[iProductoBuscador].listaSucursalStocks[iStock].stk_codsuc == pIdSucursal) {
                                produc.stk_stock = listaProductosBuscados[iProductoBuscador].listaSucursalStocks[iStock].stk_stock;
                                break;
                            }
                        }
                        break;
                    }
                }
                //Agregar producto
                //                if (pIsDesdeBuscador) {
                //                    indiceProducto = listaCarritos[i].listaProductos.length; // ;
                //                    // 
                //                    agregarFilaAlCarrito(i, indiceProducto, produc.pro_nombre, pCantidadProducto, produc.PrecioFinal);
                //                }
                listaCarritos[i].listaProductos.push(produc);
                // Agregar producto
                if (pIsDesdeBuscador) {
                    indiceProducto = listaCarritos[i].listaProductos.length - 1; // ;
                    agregarFilaAlCarrito(i, indiceProducto, produc.pro_nombre, pCantidadProducto, produc.PrecioFinal);
                }
            } else {
                //Actualizar producto
                if (pIsDesdeBuscador) {
                    $('#inputCarrito' + i + '_' + indiceProducto).val(pCantidadProducto);
                }
            }
            // Actualizar Totales
            // Precio Calcular por producto
            var strHtmlPrecioProducto = '';
            if (listaCarritos[i].listaProductos[indiceProducto].stk_stock != 'N') {
                var totalTempProducto = CalcularPrecioProductosEnCarrito(listaCarritos[i].listaProductos[indiceProducto].PrecioFinal, listaCarritos[i].listaProductos[indiceProducto].cantidad, listaCarritos[i].listaProductos[indiceProducto].pro_ofeunidades, listaCarritos[i].listaProductos[indiceProducto].pro_ofeporcentaje);
                strHtmlPrecioProducto = '$&nbsp;' + FormatoDecimalConDivisorMiles(totalTempProducto.toFixed(2));
                sumaTotal = sumaTotal + totalTempProducto;
            }
            $('#tdPrecio' + i + '_' + indiceProducto).html(strHtmlPrecioProducto);
            // Precio Calcular de carrito

            $('#tdTotal' + i).html('$&nbsp;' + FormatoDecimalConDivisorMiles(sumaTotal.toFixed(2)));
            isCarritoFind = true;
            break;
        }
    }
    if (!isCarritoFind) {
        var produc = new cProductosEnCarrito();
        produc.codProducto = pIdProduco;
        produc.cantidad = pCantidadProducto;
        for (var iProductoBuscador = 0; iProductoBuscador < listaProductosBuscados.length; iProductoBuscador++) {
            if (listaProductosBuscados[iProductoBuscador].pro_codigo == pIdProduco) {
                produc.pro_nombre = listaProductosBuscados[iProductoBuscador].pro_nombre;
                produc.pro_precio = listaProductosBuscados[iProductoBuscador].pro_precio;
                produc.pro_preciofarmacia = listaProductosBuscados[iProductoBuscador].pro_preciofarmacia;
                produc.PrecioFinal = listaProductosBuscados[iProductoBuscador].PrecioFinal;
                produc.pro_ofeunidades = listaProductosBuscados[iProductoBuscador].pro_ofeunidades;
                produc.pro_ofeporcentaje = listaProductosBuscados[iProductoBuscador].pro_ofeporcentaje;
                produc.pro_canmaxima = listaProductosBuscados[iProductoBuscador].pro_canmaxima;
                for (var iStock = 0; iStock < listaProductosBuscados[iProductoBuscador].listaSucursalStocks.length; iStock++) {
                    if (listaProductosBuscados[iProductoBuscador].listaSucursalStocks[iStock].stk_codsuc == pIdSucursal) {
                        produc.stk_stock = listaProductosBuscados[iProductoBuscador].listaSucursalStocks[iStock].stk_stock;
                        break;
                    }
                }
                break;
            }
        }
        var carr = new cCarrito();
        carr.codSucursal = pIdSucursal;
        carr.listaProductos.push(produc);
        listaCarritos.push(carr);
        //Agregar Carrito
        if (pIsDesdeBuscador) {
            //            $('#divContenedorCarros').append(AgregarCarritoHtml(listaCarritos.length - 1));
            var indexCarritoAgregar = listaCarritos.length - 1;
            $('#divContenedorBase_' + listaCarritos[indexCarritoAgregar].codSucursal).html(AgregarCarritoHtml(indexCarritoAgregar));
        }
    }
    // Actualizar BD

    PageMethods.CargarCarritoDiferido(pIdSucursal, pIdProduco, pCantidadProducto, OnCallBackCargarCarritoDiferido, OnFail);
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
function onclickConfirmarCarrito(pIndexCarrito) {




    $('#txtMensajeFactura').val('');
    $('#txtMensajeRemito').val('');
    document.getElementById('checkboxIsUrgentePedido').checked = false;
    CargarHtmlTipoEnvio(listaCarritos[pIndexCarrito].codSucursal);
    document.getElementById('divConfirmarPedidoContenedorGeneral').style.display = 'block';
    document.getElementById('divTransferContenedorGeneralFondo').style.display = 'block';
    var arraySizeDocumento = SizeDocumento();
    document.getElementById('divTransferContenedorGeneralFondo').style.height = arraySizeDocumento[1] + 'px';
    $("#hiddenIndexCarrito").val(pIndexCarrito);



}
function onclickHacerPedido() {
    var indexCarrito = $("#hiddenIndexCarrito").val();
    ConfirmarCarrito(indexCarrito);
}

function ConfirmarCarrito(pIndexCarrito) {

    textTipoEnvioCarrito = $('#comboTipoEnvio option:selected').text();

    var isTomarPedido = true;
    if (textTipoEnvioCarrito == 'Mostrador') {
        var montoMinimo = '';
        var precio = ObtenerPrecioConFormato($('#tdTotal' + pIndexCarrito).html());
        if (listaSucursales != null) {
            for (var i = 0; i < listaSucursales.length; i++) {
                if (listaSucursales[i].suc_codigo == listaCarritos[pIndexCarrito].codSucursal) {
                    if (listaSucursales[i].suc_montoMinimo > 0) {
                        if (listaSucursales[i].suc_montoMinimo > precio) {
                            isTomarPedido = false;
                            montoMinimo = listaSucursales[i].suc_montoMinimo;
                        }
                    }
                    break;
                }
            }
        }
    }
    if (isTomarPedido) {
        var textFactura = $('#txtMensajeFactura').val();
        var textRemito = $('#txtMensajeRemito').val();
        var isUrgente = $('#checkboxIsUrgentePedido').is(":checked");
        var idTipoEnvio = $('#comboTipoEnvio').val();
        PageMethods.TomarPedidoCarrito(listaCarritos[pIndexCarrito].codSucursal, textFactura, textRemito, idTipoEnvio, isUrgente, OnCallBackTomarPedidoCarrito, OnFail);
    } else {
        alert('Para hacer el pedido se debe superar el monto mínimo de ' + '$ ' + montoMinimo);
    }
}

function OnCallBackTomarPedidoCarrito(args) {
    /// mostrar faltantes y problema crediticio
    if (args == null) {
        alert(mensajeCuandoSeMuestraError);
        location.href = 'PedidosBuscador.aspx';
    } else {
        isHacerBorradoCarritos = true;
        CargarRespuestaDePedido(args);

    }
    //    CerrarContenedorTransfer();
    //    location.href = 'PedidosBuscador.aspx';
}

function CargarRespuestaDePedido(pValor) {
    var listaFaltantes = pValor.Items;
    var listaProblemaCrediticio = pValor.ItemsConProblemasDeCreditos;

    var strHtmlProductosPedidos = '';
    var strHtmlFaltantes = '';
    var strHtmlProblemasCrediticios = '';
    var isFaltantes = false;
    var isProductosPedidos = false;
    if (listaFaltantes.length > 0) {
        //strHtmlProductosPedidos =
        strHtmlProductosPedidos += '<div style="font-size: 14px; margin-top: 10px; width: 100%;text-align:left;background-color: #E5F3E4;color:#0B890A;">';
        strHtmlProductosPedidos += ' <b>PRODUCTOS FACTURADOS</b>';
        strHtmlProductosPedidos += '</div>';
        strHtmlProductosPedidos += '<table class="tbl-buscador-productos"  style="width:100% !important;" border="0" cellspacing="0" cellpadding="0">';
        strHtmlProductosPedidos += '<tr>';
        strHtmlProductosPedidos += '<th  align="left" style="background-color:#EBEBEB;color:#000;">';
        strHtmlProductosPedidos += 'Nombre producto';
        strHtmlProductosPedidos += '</th>';
        strHtmlProductosPedidos += '<th   style="background-color:#EBEBEB;color:#000;">';
        strHtmlProductosPedidos += 'Cantidad';
        strHtmlProductosPedidos += '</th>';
        strHtmlProductosPedidos += '</tr>';
        //

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
        var cantFaltantes = 0;
        var cantFacturados = 0;
        for (var iFaltantes = 0; iFaltantes < listaFaltantes.length; iFaltantes++) {
            var strHtmlColorFondo = '';
            //            if (iFaltantes % 2 != 0) {
            //                strHtmlColorFondo = ' bp-td-color';
            //            }
            if (listaFaltantes[iFaltantes].Faltas > 0) {
                if (cantFaltantes % 2 != 0) {
                    strHtmlColorFondo = ' bp-td-color';
                }
                isFaltantes = true;
                strHtmlFaltantes += '<tr>';

                strHtmlFaltantes += '<td align="left" style="text-align:left !important;padding-left:5px;"  class="' + strHtmlColorFondo + '">';
                strHtmlFaltantes += listaFaltantes[iFaltantes].NombreObjetoComercial;
                strHtmlFaltantes += '</td>';
                strHtmlFaltantes += '<td  class="' + strHtmlColorFondo + '">';
                strHtmlFaltantes += listaFaltantes[iFaltantes].Faltas;
                strHtmlFaltantes += '</td>';

                strHtmlFaltantes += '</tr>';
                cantFaltantes++;
            }
            if (listaFaltantes[iFaltantes].Cantidad > 0) {
                if (cantFacturados % 2 != 0) {
                    strHtmlColorFondo = ' bp-td-color';
                }
                isProductosPedidos = true;
                strHtmlProductosPedidos += '<tr>';

                strHtmlProductosPedidos += '<td align="left" style="text-align:left !important;padding-left:5px;" class="' + strHtmlColorFondo + '">';
                strHtmlProductosPedidos += listaFaltantes[iFaltantes].NombreObjetoComercial;
                strHtmlProductosPedidos += '</td>';
                strHtmlProductosPedidos += '<td  class="' + strHtmlColorFondo + '">';
                strHtmlProductosPedidos += listaFaltantes[iFaltantes].Cantidad;
                strHtmlProductosPedidos += '</td>';

                strHtmlProductosPedidos += '</tr>';
                cantFacturados++;
            }
        }
        //
        strHtmlProductosPedidos += '</table>';
        strHtmlProductosPedidos += '<div style="font-size: 12px;text-align:left;margin-top:2px;background-color: #E5F3E4;"><b>MONTO TOTAL:</b> <span style="color:#0B890A;"> $ ' + FormatoDecimalConDivisorMiles(pValor.MontoTotal.toFixed(2)) + '</span> </div>';
        strHtmlProductosPedidos += '<div style="font-size: 12px;text-align:left; ">TIPO DE ENVIO: ' + textTipoEnvioCarrito + ' </div>';
        //
        strHtmlFaltantes += '</table>';
    }
    if (!isFaltantes) {
        strHtmlFaltantes = '';
    }
    if (!isProductosPedidos) {
        strHtmlProductosPedidos = '';
    }
    var isProblemaCrediticio = false;
    if (listaProblemaCrediticio.length > 0) {
        strHtmlProblemasCrediticios += '<div style="font-size: 14px; margin-top: 10px; width: 100%;text-align:left;background-color:#F7E7E8;color:#B3000C;">';
        strHtmlProblemasCrediticios += '<b>PRODUCTOS CON PROBLEMAS DE CREDITO</b>';
        strHtmlProblemasCrediticios += '</div>';
        strHtmlProblemasCrediticios += '<table  class="tbl-buscador-productos"  style="width:100% !important;" border="0" cellspacing="0" cellpadding="0">';
        strHtmlProblemasCrediticios += '<tr>';
        strHtmlProblemasCrediticios += '<th  align="left" style="background-color:#EBEBEB;color:#000;">';
        strHtmlProblemasCrediticios += 'Nombre producto';
        strHtmlProblemasCrediticios += '</th>';
        strHtmlProblemasCrediticios += '<th  style="background-color:#EBEBEB;color:#000;">';
        strHtmlProblemasCrediticios += 'Cantidad';
        strHtmlProblemasCrediticios += '</th>';
        strHtmlProblemasCrediticios += '</tr>';
        for (var iProblemaCrediticio = 0; iProblemaCrediticio < listaProblemaCrediticio.length; iProblemaCrediticio++) {
            var strHtmlColorFondo = '';
            if (iProblemaCrediticio % 2 != 0) {
                strHtmlColorFondo = ' bp-td-color';
            }
            var cantidadProblemasCrediticios = listaProblemaCrediticio[iProblemaCrediticio].Cantidad + listaProblemaCrediticio[iProblemaCrediticio].Faltas;
            if (cantidadProblemasCrediticios > 0) {
                isProblemaCrediticio = true;
                strHtmlProblemasCrediticios += '<tr>';

                strHtmlProblemasCrediticios += '<td align="left" style="text-align:left !important;padding-left:5px;" class="' + strHtmlColorFondo + '">';
                strHtmlProblemasCrediticios += listaProblemaCrediticio[iProblemaCrediticio].NombreObjetoComercial;
                strHtmlProblemasCrediticios += '</td>';
                strHtmlProblemasCrediticios += '<td class="' + strHtmlColorFondo + '">';
                strHtmlProblemasCrediticios += cantidadProblemasCrediticios;
                strHtmlProblemasCrediticios += '</td>';

                strHtmlProblemasCrediticios += '</tr>';
            }
        }
        strHtmlProblemasCrediticios += '</table>';
    }
    if (!isProblemaCrediticio) {
        strHtmlProblemasCrediticios = '';
    }


    if (!isProblemaCrediticio && !isFaltantes) {
        strHtmlFaltantes += '<div style="font-size: 14px; padding: 10px; width: 100%;">';
        strHtmlFaltantes += 'El pedido se realizo correctamente';
        strHtmlFaltantes += '</div>';
    }

    if (textTipoEnvioCarrito == 'Reparto') {
        if (pValor.Login != null) {
            strHtmlProductosPedidos += '<div style="font-size:12px;text-align:left;margin-top:10px;margin-bottom:10px;">Su pedido cerrará a las ' + pValor.Login + '</div>';
        }
    }

    $('#divRespuestaProductosPedidos').html(strHtmlProductosPedidos);
    $('#divRespuestaFaltantes').html(strHtmlFaltantes);
    $('#divRespuestaProblemasCrediticios').html(strHtmlProblemasCrediticios);

    document.getElementById('divConfirmarPedidoContenedorGeneral').style.display = 'none';
    document.getElementById('divRespuestaPedidoContenedorGeneral').style.display = 'block';

}
function CargarHtmlTipoEnvio(pSucursal) {
    var strHtml = '';
    strHtml += '<select id="comboTipoEnvio" class="select_gral" onchange="onChangeTipoEnvio()">';
    strHtml += CargarHtmlOptionTipoEnvio(pSucursal);
    strHtml += '</select>';
    $('#tdTipoEnvio').html(strHtml);
    onChangeTipoEnvio();
}

function CargarHtmlOptionTipoEnvio(pSucursal) {
    var strHtml = '';
    if (sucursalCliente == 'CC') {
        switch (tipoEnvioCliente) {
            case 'R':
                strHtml += '<option value="R">Reparto</option>';
                strHtml += '<option value="C">Cadeteria</option>';
                strHtml += '<option value="E">Encomienda</option>';
                strHtml += '<option value="M">Mostrador</option>';
                break;
            case 'M':
                strHtml += '<option value="M">Mostrador</option>';
                strHtml += '<option value="C">Cadeteria</option>';
                break;
            case 'C':
                strHtml += '<option value="C">Cadeteria</option>';
                strHtml += '<option value="M">Mostrador</option>';
                break;
            case 'E':
                strHtml += '<option value="E">Encomienda</option>';
                strHtml += '<option value="M">Mostrador</option>';
                break;
            default:
                break;
        }
    } else {
        if (sucursalCliente == 'CD') {
            if (pSucursal == 'CC') {
                strHtml += '<option value="R">Reparto</option>';
            } else if (pSucursal == 'CO') {
                strHtml += '<option value="R">Reparto</option>';
            } else {
                strHtml += '<option value="R">Reparto</option>';
                strHtml += '<option value="C">Cadeteria</option>';
                strHtml += '<option value="M">Mostrador</option>';
            }
        } else {
            if (pSucursal == 'CC') {
                strHtml += '<option value="R">Reparto</option>';
            } else {
                strHtml += '<option value="R">Reparto</option>';
                strHtml += '<option value="C">Cadeteria</option>';
                strHtml += '<option value="M">Mostrador</option>';
            }
        }
    }
    return strHtml;
}
function onChangeTipoEnvio() {
    var isOcutar = true;
    if (sucursalCliente == 'CC') {
        if (tipoEnvioCliente == 'C') {
            var tipoEnvio = $('#comboTipoEnvio').val();
            if (tipoEnvio == 'C') {
                $('#tdIsUrgente').css('visibility', 'visible'); //visibility:hidden
                isOcutar = false;
            }
        }
    }
    if (isOcutar) {
        $('#checkboxIsUrgentePedido').removeAttr("checked");
        $('#tdIsUrgente').css('visibility', 'hidden');
    }
}

//function LimpiarTextBoxProductosBuscados() {
//    if (listaProductosBuscados != null) {
//        for (var i = 0; i < listaProductosBuscados.length; i++) {
//            for (var iEncabezadoSucursal = 0; iEncabezadoSucursal < listaSucursal.length; iEncabezadoSucursal++) {
//                $('#inputSuc' + i + "_" + iEncabezadoSucursal).val('');
//            }
//        }
//    }
//}
function LimpiarTextBoxProductosBuscados(pIdSucursal) {
    if (listaProductosBuscados != null) {
        for (var iEncabezadoSucursal = 0; iEncabezadoSucursal < listaSucursal.length; iEncabezadoSucursal++) {
            if (listaSucursal[iEncabezadoSucursal] == pIdSucursal) {
                for (var i = 0; i < listaProductosBuscados.length; i++) {
                    $('#inputSuc' + i + "_" + iEncabezadoSucursal).val('');
                }
                break;
            }
        }
    }
}
function ConvertirSucursalParaColumno(pValor) {
    var resultado = pValor;
    switch (pValor) {
        case 'CB':
            resultado = 'CBA';
            break;
        case 'SF':
            resultado = 'SFE';
            break;
        case 'CH':
            resultado = 'CHL';
            break;
        case 'CO':
            resultado = 'CDU';
            break;
        case 'CD':
            resultado = 'CON';
            break;
        default:
            break;
    }
    return resultado;
}

function cCarrito() {
    this.lrc_id = -1;
    this.codSucursal = '';
    this.suc_nombre = '';
    this.listaProductos = [];
}
function cProductosEnCarrito() {
    this.codProducto = '';
    this.cantidad = -1;
    this.pro_nombre = '';
    this.pro_precio = -1;
    this.pro_preciofarmacia = -1;
    this.PrecioFinal = -1;
    this.pro_ofeunidades = -1;
    this.pro_ofeporcentaje = -1;
    this.idUsuario = null;
}
