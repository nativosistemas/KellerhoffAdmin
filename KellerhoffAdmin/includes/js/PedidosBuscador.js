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
var Ascender_pro_nombre = true;
var Ascender_pro_precio = true;
var Ascender_PrecioFinal = true;
var Ascender_PrecioConDescuentoOferta = true;
var Ascender_PrecioConTransfer = true;
var isClienteTomaTransfer = null;
var isClienteTomaPerfumeria = null;
var isClienteTomaOferta = null;
var textTipoEnvioCarrito = '';
var textTipoEnvioCarritoTransfer = '';
var intPaginadorTipoDeRecuperar = 0; // 1 es oferta - 2 es transfer
var isHacerBorradoCarritos = false;
var mensajeCuandoSeMuestraError = 'Se produjo un error';
var indexCarritoHorarioCierre = null;
var isCarritoDiferido = null;
var isBotonNoEstaEnProceso = true;
var maxLengthMensajeFacturaRemito = 40;
var cantidadMaximaParametrizada = null;
var mensajeCantidadSuperaElMaximoParametrizado1 = '¿Seguro que desea pedir más de ';
var mensajeCantidadSuperaElMaximoParametrizado2 = ' unidades?';
var isExcedeImporte = false;
var isEnterExcedeImporte = false;
var ExcedeImporteFila = null;
var ExcedeImporteColumna = null;
var ExcedeImporteValor = null;
var ExcedeImporteSiguienteFila = 0;
var ExcedeImporteSiguienteColumna = 0;
var ExcedeImporteFilaCarrito = null;
var ExcedeImporteColumnaCarrito = null;
var ExcedeImporteValorCarrito = null;
var ExcedeImporteSiguienteIndiceCarrito = 0;
var ExcedeImporteSiguienteIndiceCarritoProducto = 0;
var ExcedeImporteIndiceCarrito = 0;
var ExcedeImporteIndiceCarritoProducto = 0;
var isMoverCursor = true;
var listaTipoEnviosSucursal = null;
var listaCadeteriaRestricciones = null;
var nombreSucursalDefault = '';
var homeIdOferta = null;
var homeIdTransfer = null;
var homeTipo = null;
//
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

    if (isCarritoDiferido == null) {
        isCarritoDiferido = $('#hiddenIsCarritoDiferido').val();
        if (typeof isCarritoDiferido == 'undefined') {
            isCarritoDiferido = false;
        } else {
            if (isCarritoDiferido == 'true') {
                isCarritoDiferido = true;
            } else if (isCarritoDiferido == 'false') {
                isCarritoDiferido = false;
            } else {
                isCarritoDiferido = false;
            }
        }
    }
    if (listaTipoEnviosSucursal == null) {
        listaTipoEnviosSucursal = eval('(' + $('#hiddenListaTipoEnviosSucursales').val() + ')');
        if (typeof listaTipoEnviosSucursal == 'undefined') {
            listaTipoEnviosSucursal = null;
        }
    }
    if (listaCadeteriaRestricciones == null) {
        listaCadeteriaRestricciones = eval('(' + $('#hiddenListaCadeteriaRestricciones').val() + ')');
        if (typeof listaCadeteriaRestricciones == 'undefined') {
            listaCadeteriaRestricciones = null;
        }
    }
    //Inicio Cantidad Parametrizada
    if (cantidadMaximaParametrizada == null) {
        cantidadMaximaParametrizada = $('#hiddenCantidadProductoParametrizado').val();
        if (typeof cantidadMaximaParametrizada == 'undefined') {
            cantidadMaximaParametrizada = null;
        }
    }
    //    if (cantidadMaximaParametrizada == null) {
    //        cantidadMaximaParametrizada = 0;
    //    }
    //Fin Cantidad Parametrizada

    // Home oferta
    homeIdOferta = $('#hiddenHomeIdOferta').val();
    if (typeof homeIdOferta == 'undefined') {
        homeIdOferta = null;
    }
    else
        homeIdOferta = parseInt($('#hiddenHomeIdOferta').val());

    homeIdTransfer = $('#hiddenhomeIdTransfer').val();
    if (typeof homeIdTransfer == 'undefined') {
        homeIdTransfer = null;
    }
    else
        homeIdTransfer = parseInt($('#hiddenhomeIdTransfer').val());

    homeTipo = $('#hiddenhomeTipo').val();
    if (typeof homeTipo == 'undefined') {
        homeTipo = null;
    }
    else
        homeTipo = parseInt($('#hiddenhomeTipo').val());

    //fin Home oferta





    if (!isClienteTomaOferta) {
        $('#linkTodosOfertas').hide();
        $('#tdSoloOfertas').hide();

    }
    //$('#tdSoloOfertas').hide(); // capa
    if (!isClienteTomaTransfer) {
        $('#linkTodosTransfer').hide();
        $('#tdSoloTransfers').hide();
    }
    //    
    jQuery("#txtBuscador").focus();
    CargarCarritos();

    if (typeof CargarCarritosTransfersPorSucursal == 'function') {
        listaCarritoTransferPorSucursal = eval('(' + $('#hiddenListaCarritosTransferPorSucursal').val() + ')');
        CargarCarritosTransfersPorSucursal();
    }
    //
    onClickBusquedaAvanzada();
    //

    //setInterval(CuentaRegresivaSucursalDefault, 500);
    nombreSucursalDefault = sucursalCliente;
    if (listaSucursales != null) {
        for (var i = 0; i < listaSucursales.length; i++) {
            if (listaSucursales[i].sde_sucursal == sucursalCliente) {
                nombreSucursalDefault = listaSucursales[i].suc_nombre;
                break;
            }
        }
    }
    PageMethods.ObtenerHorarioCierreAndSiguiente(sucursalCliente, OnCallBackObtenerHorarioCierreCuentaRegresiva, OnFail);
    //if (homeTipo != null) {
    //    if (homeTipo == 1 && homeIdOferta != null)
    //        PageMethods.RecuperarProductosHomeOferta(homeIdOferta, OnCallBackRecuperarProductos, OnFail);
    //    else if (homeTipo == 2 && homeIdTransfer != null)
    //        PageMethods.RecuperarTransferPorId(homeIdTransfer, OnCallBackRecuperarTransferPorId_home, OnFail);
    //}
});
//  
function OnCallBackRecuperarTransferPorId_home(args) {
    var argsAux = [];
    argsAux.push(args);
    OnCallBackRecuperarTransfer(argsAux);
    onClickVerTransferCompleto(0);
}
var new_fechaHorarioCierre = '';
var new_fechaHorarioCierreSiguiente = '';
function OnCallBackObtenerHorarioCierreCuentaRegresiva(args) {
    if (args != null) {

        var strHtml = args[0] == null ? '' : args[0];
        new_fechaHorarioCierre = strHtml;
        if (args.length = 2)
            new_fechaHorarioCierreSiguiente = args[1];
        else
            new_fechaHorarioCierreSiguiente = '';
        if (strHtml != '') {
            var hoy = new Date();
            if (strHtml.length == 12) {
                var diaSemana = strHtml.substring(10, 12);
                var diaSemanaNro = -1;
                //Note: Sunday is 0, Monday is 1, and so on || from 0 to 6
                // LU = 1
                // MA = 2
                // MI = 3
                // JU = 4
                // VI = 5
                // SA = 6
                // DO = 0
                switch (diaSemana) {
                    case 'LU':
                        diaSemanaNro = 1;
                        break;
                    case 'MA':
                        diaSemanaNro = 2;
                        break;
                    case 'MI':
                        diaSemanaNro = 3;
                        break;
                    case 'JU':
                        diaSemanaNro = 4;
                        break;
                    case 'VI':
                        diaSemanaNro = 5;
                        break;
                    case 'SA':
                        diaSemanaNro = 6;
                        break;
                    case 'DO':
                        diaSemanaNro = 0;
                        break;
                    default:
                        break;
                }

                strHtml = strHtml.replace(' hs. ' + diaSemana, '');
                var values = strHtml.split(':');
                var d = new Date(hoy.getFullYear(), hoy.getMonth(), hoy.getDate(), values[0], values[1], '00');// mes 0 = enero
                var n = d.getDay();
                var sumaDia = 0;
                while (d.getDay() != diaSemanaNro) {
                    sumaDia++;
                    d = new Date(hoy.getFullYear(), hoy.getMonth(), hoy.getDate() + sumaDia, values[0], values[1], '00');// mes 0 = enero
                    if (sumaDia > 7 || d.getDay() == diaSemanaNro)
                        break;
                }
                fechaCuentaRegresiva = d;

            } else {
                strHtml = strHtml.replace(' hs.', '');
                var values = strHtml.split(':');
                fechaCuentaRegresiva = new Date(hoy.getFullYear(), hoy.getMonth(), hoy.getDate(), values[0], values[1], '00');// mes 0 = enero
            }

            countdown();
            //setTimeout(countdown, 500);
        }
    }
    if (homeTipo != null) {
        if (homeTipo == 1 && homeIdOferta != null)
            PageMethods.RecuperarProductosHomeOferta(homeIdOferta, OnCallBackRecuperarProductos, OnFail);
        else if (homeTipo == 2 && homeIdTransfer != null)
            PageMethods.RecuperarTransferPorId(homeIdTransfer, OnCallBackRecuperarTransferPorId_home, OnFail);
    }
}
function countdown() {
    var hoy = new Date();
    var fecha = fechaCuentaRegresiva;

    var dias = 0;
    var horas = 0;
    var minutos = 0;
    var segundos = 0;

    if (fecha > hoy) {
        var diferencia = (fecha.getTime() - hoy.getTime()) / 1000;
        dias = Math.floor(diferencia / 86400);
        diferencia = diferencia - (86400 * dias);
        horas = Math.floor(diferencia / 3600);
        diferencia = diferencia - (3600 * horas);
        minutos = Math.floor(diferencia / 60);
        diferencia = diferencia - (60 * minutos);
        segundos = Math.floor(diferencia);


        var strSucursal = 'Sucursal ' + nombreSucursalDefault;
        if (document.getElementById('CuentaRegresiva_sucursal').innerHTML != strSucursal)
            document.getElementById('CuentaRegresiva_sucursal').innerHTML = strSucursal;


        var strProximoCierre = 'CIERRE DE REPARTO: ' + new_fechaHorarioCierre;
        if (document.getElementById('CuentaRegresiva_proximoCierre').innerHTML != strProximoCierre)
            document.getElementById('CuentaRegresiva_proximoCierre').innerHTML = strProximoCierre;
        //

        var strFaltan = 'Faltan ';
        if (dias != 0) {
            strFaltan += String(dias) + ' dias ';
        }
        strFaltan += horas + ':' + toString00(minutos) + ':' + toString00(segundos) + ' hs';

        if (document.getElementById('CuentaRegresiva_Faltan').innerHTML != strFaltan)
            document.getElementById('CuentaRegresiva_Faltan').innerHTML = strFaltan;



        if (new_fechaHorarioCierre != '') {
            if (document.getElementById('CuentaRegresiva_Siguiente').innerHTML != 'Próximo cierre: ' + new_fechaHorarioCierreSiguiente)
                document.getElementById('CuentaRegresiva_Siguiente').innerHTML = 'Próximo cierre: ' + new_fechaHorarioCierreSiguiente;
        }
        else
            document.getElementById('CuentaRegresiva_Siguiente').innerHTML = '';


        //document.getElementById('divContenedorBaseCuentaRegresiva').innerHTML = strHtml;
        if ($('#divContenedorBaseCuentaRegresiva').css('display') == 'none') {
            $('#divContenedorBaseCuentaRegresiva').css('display', 'block');
        }
        setTimeout(countdown, 500);
        //if (dias > 0 || horas > 0 || minutos > 0 || segundos > 0) {
        //    setTimeout("countdown(\"" + id + "\")", 1000);
        //}
    }
    else {
        //document.getElementById('divContenedorBaseCuentaRegresiva').innerHTML = 'Quedan ' + dias + ' D&iacute;as, ' + horas + ' Horas, ' + minutos + ' Minutos, ' + segundos + ' Segundos';
        document.getElementById('divContenedorBaseCuentaRegresiva').innerHTML = '';
        $('#divContenedorBaseCuentaRegresiva').css('display', 'none');
        PageMethods.ObtenerHorarioCierreAndSiguiente(sucursalCliente, OnCallBackObtenerHorarioCierreCuentaRegresiva, OnFail);
    }
}
function RecuperarTransfer(pIndice) {
    if (!isCarritoDiferido) { // si no es carrito diferido
        if (isClienteTomaTransfer) {
            if (listaProductosBuscados[pIndice].isTieneTransfer) {
                productoSeleccionado = listaProductosBuscados[pIndice].pro_nombre;
                PageMethods.RecuperarTransfer(listaProductosBuscados[pIndice].pro_nombre, OnCallBackRecuperarTransfer, OnFail);
            }
        }
    }
}
function CerrarContenedorTransfer() {
    $('#divRespuestaProductosPedidos').html('');
    $('#divRespuestaMensajeFinales').html('');
    $('#divRespuestaFaltantes').html('');
    $('#divRespuestaPendienteDeFacturacion').html('');
    $('#divRespuestaProblemasCrediticios').html('');

    // OJO
    HacerLimpiezaDeCarritosDspDeConfirmarPedido();
    // FIN OJO

    document.getElementById('resultadoPedidoBotonOk').style.display = 'block';
    document.getElementById('divTransferContenedorGeneral').style.display = 'none';
    document.getElementById('divConfirmarPedidoContenedorGeneral').style.display = 'none';
    document.getElementById('divTransferContenedorGeneralFondo').style.display = 'none';
    document.getElementById('divRespuestaPedidoContenedorGeneral').style.display = 'none';
    if ($('#divConfirmarPedidoTransferContenedorGeneral').length) {//$('#identifier').length
        document.getElementById('divConfirmarPedidoTransferContenedorGeneral').style.display = 'none';
    }
}
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
    $('#checkBoxTroquel').attr('checked', false);
    //
    $('#checkBoxTodosOfertas').attr('checked', false);
    $('#checkBoxTodosTransfer').attr('checked', false);
    //

    $('#opcionesBusquedaAvanzada').css('display', 'none');
    $('#linkBusquedaAvanzada').css('display', 'block');
    $('.tools-top').css('height', '51px');
    $('#divResultadoBuscador').css('padding-top', '10px');
}
function HacerLimpiezaDeCarritosDspDeConfirmarPedido() {
    if (isHacerBorradoCarritos) {
        if (isCarritoDiferido) {
            var indexCarrito = $("#hiddenIndexCarrito").val();
            if (isNotNullEmpty(indexCarrito)) {
                $('#divContenedorCarrito_' + indexCarrito).remove();
                var sucur = listaCarritos[indexCarrito].codSucursal;
                listaCarritos[indexCarrito].codSucursal = '';
                LimpiarTextBoxProductosBuscados(sucur);
                $("#hiddenIndexCarrito").val('');
            }
        } else {
            if (indexSucursalTransferSeleccionado == null) {
                var indexCarrito = $("#hiddenIndexCarrito").val();
                if (isNotNullEmpty(indexCarrito)) {
                    $('#divContenedorCarrito_' + indexCarrito).remove();
                    var sucur = listaCarritos[indexCarrito].codSucursal;
                    listaCarritos[indexCarrito].codSucursal = '';
                    LimpiarTextBoxProductosBuscados(sucur);
                    $("#hiddenIndexCarrito").val('');
                }
            } else {
                var sucur = listaCarritoTransferPorSucursal[indexSucursalTransferSeleccionado].Sucursal;
                listaCarritoTransferPorSucursal[indexSucursalTransferSeleccionado].Sucursal = '';
                LimpiarTextBoxProductosBuscados(sucur);
                $('#divContenedorBaseTransfer_' + sucur).html('');
                indexSucursalTransferSeleccionado = null;
            }
        }
        isHacerBorradoCarritos = false;
    }
}
//function HacerLimpiezaDeCarritosDspDeConfirmarPedido_AUX() {
//    if (isHacerBorradoCarritos) {
//        if (isCarritoDiferido) {
//            var indexCarrito = $("#hiddenIndexCarrito").val();
//            if (isNotNullEmpty(indexCarrito)) {
//                $('#divContenedorCarrito_' + indexCarrito).remove();
//                LimpiarTextBoxProductosBuscados(listaCarritos[indexCarrito].codSucursal);
//                listaCarritos[indexCarrito].codSucursal = '';
//                $("#hiddenIndexCarrito").val('');
//            }
//        } else {
//            if (indexSucursalTransferSeleccionado == null) {
//                var indexCarrito = $("#hiddenIndexCarrito").val();
//                if (isNotNullEmpty(indexCarrito)) {
//                    $('#divContenedorCarrito_' + indexCarrito).remove();
//                    LimpiarTextBoxProductosBuscados(listaCarritos[indexCarrito].codSucursal);
//                    listaCarritos[indexCarrito].codSucursal = '';
//                    $("#hiddenIndexCarrito").val('');
//                }
//            } else {
//                $('#divContenedorBaseTransfer_' + listaCarritoTransferPorSucursal[indexSucursalTransferSeleccionado].Sucursal).html('');
//                indexSucursalTransferSeleccionado = null;
//            }
//        }
//        isHacerBorradoCarritos = false;
//    }
//}
function CargarCarritos() {
    if (listaCarritos != null) {
        for (var i = 0; i < listaCarritos.length; i++) {
            $('#divContenedorBase_' + listaCarritos[i].codSucursal).html(AgregarCarritoHtml(i));
            setScrollFinDeCarrito(i);
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
function setScrollFinDeCarrito(pIndexCarrito) {
    setTimeout(function () { $('#Scroll_' + pIndexCarrito).scrollTop($('#Scroll_' + pIndexCarrito).prop('scrollHeight')); }, 40);

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
    var isMostrarHorarioCierre = false;
    var visibleCssHorarioCierreTitulo = ' style="visibility:hidden;" ';
    if (isNotNullEmpty(listaCarritos[pIndexCarrito].proximoHorarioEntrega) && sucursalCliente != listaCarritos[pIndexCarrito].codSucursal) {
        isMostrarHorarioCierre = true;
        visibleCssHorarioCierreTitulo = '';
    }
    strHTML += '<div id="contenedorProximaEntrega_' + pIndexCarrito + '" ' + visibleCssHorarioCierreTitulo + '  class="ct-right">';
    strHTML += '<span >';
    strHTML += 'Próximo&nbsp;cierre'; //Próximo cierre  -- 'Próxima&nbsp;entrega<br />'; 
    strHTML += '</span>';
    strHTML += '<br />';
    strHTML += '<span id="proximaEntrega_' + pIndexCarrito + '">';
    if (isMostrarHorarioCierre) {
        strHTML += listaCarritos[pIndexCarrito].proximoHorarioEntrega;
    }
    strHTML += '</span>';
    strHTML += '</div>';
    //    }
    strHTML += '<div class="clear">';
    strHTML += '</div>';
    strHTML += '</div>';
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
    strHTML += '<div id="Scroll_' + pIndexCarrito + '" style="max-height:250px;overflow-y:scroll;overflow-x:hidden;">';

    // Cuerpo
    strHTML += '<table id="tb_' + pIndexCarrito + '"  style="width:240px;  !important;" class="tbl-carro" border="0" cellspacing="0" cellpadding="0">';
    strHTML += '<tbody>';
    var nroTotalCarrito = parseFloat(0);
    var cantProductosTotales = 0;
    for (var iProductos = 0; iProductos < listaCarritos[pIndexCarrito].listaProductos.length; iProductos++) {
        cantProductosTotales += listaCarritos[pIndexCarrito].listaProductos[iProductos].cantidad;
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




    /////
    ////  'Unidades' y 'Renglones'
    strHTML += '<table  class="tbl-carro" border="0" cellspacing="0" cellpadding="0" >';
    strHTML += '<tr>';
    strHTML += '<td align="right">';
    strHTML += '<b>Renglones:</b>';
    strHTML += '</td>';
    strHTML += '<td id="tdRenglones' + pIndexCarrito + '" align="left">';
    strHTML += listaCarritos[pIndexCarrito].listaProductos.length;
    strHTML += '</td>';
    strHTML += '<td  align="right">';
    strHTML += '<b>Unidades:</b>';
    strHTML += '</td>';
    strHTML += '<td id="tdUnidades' + pIndexCarrito + '" align="left">';
    strHTML += cantProductosTotales;
    strHTML += '</td>';
    strHTML += '</tr>';
    strHTML += '</table>';
    //// fin 'Unidades' y 'Renglones'
    /////

    strHTML += '<table  class="tbl-carro" border="0" cellspacing="0" cellpadding="0">'; // Pie Pagina
    //

    //
    strHTML += '<tr>';
    strHTML += '<td width="80%" colspan="2" class="carro-total first-td">Total</td>';
    strHTML += '<td width="20%" id="tdTotal' + pIndexCarrito + '" class="carro-total">$&nbsp;' + FormatoDecimalConDivisorMiles(nroTotalCarrito.toFixed(2)) + '</td>';
    strHTML += '</tr>';
    strHTML += '</table>'; // Fin Pie Pagina


    strHTML += ' <a class="carro-btn-confirmar" onclick="onclickIsPuedeUsarDll(' + pIndexCarrito + ')" href="#">Confirmar</a> ';
    strHTML += '<a class="carro-btn-vaciar" onclick="onclickVaciarCarrito(' + pIndexCarrito + '); return false;"  href="#">Vaciar</a>';
    strHTML += ' <div class="clear">';
    strHTML += '  </div>';
    strHTML += ' </div>';

    return strHTML;
}
function onclickVaciarCarrito(pIndexCarrito) {
    $("#dialog-confirm-Carrito").dialog({
        resizable: false,
        height: "auto",
        width: 400,
        modal: true,
        buttons: {
            "Ok": function () {
                $("#hiddenIndexCarrito").val(pIndexCarrito);
                PageMethods.BorrarCarrito(listaCarritos[pIndexCarrito].lrc_id, listaCarritos[pIndexCarrito].codSucursal, OnCallBackBorrarCarrito, OnFail);

                $(this).dialog("close");
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        }
    });

    //   MostrarMensajeGeneral_vaciarCarrito('Información', '¿Desea vaciar el carrito?');

    //var txt;
    //var r = confirm("¿Desea vaciar el carrito?");
    //if (r == true) {
    //    // txt = "You pressed OK!";
    //    $("#hiddenIndexCarrito").val(pIndexCarrito);
    //    PageMethods.BorrarCarrito(listaCarritos[pIndexCarrito].lrc_id, listaCarritos[pIndexCarrito].codSucursal, OnCallBackBorrarCarrito, OnFail);

    //} else {
    //   // txt = "You pressed Cancel!";
    //}
}
function OnCallBackBorrarCarrito(args) {
    if (args) {
        var indexCarrito = $("#hiddenIndexCarrito").val();
        $('#divContenedorCarrito_' + indexCarrito).remove();
        var sucur = listaCarritos[indexCarrito].codSucursal;
        listaCarritos[indexCarrito].codSucursal = '';
        LimpiarTextBoxProductosBuscados(sucur);

    }
}
function onfocusInputCarrito(pValor) {
    DesmarcarFilaSeleccionada();
    selectedInput = null;
    selectedInputTransfer = null;
    selectInputCarrito = pValor;
    setTimeout(function () { selectInputCarrito.select(); }, 20);
}
function onblurInputCarrito(pValor) {
    if (isMoverCursor) {
        if (pValor.value != '') {
            var nombre = pValor.id;
            nombre = nombre.replace('inputCarrito', '');
            var palabrasBase = nombre.split("_");
            var fila = parseInt(palabrasBase[1]);
            var columna = parseInt(palabrasBase[0]);
            var cantidadProducto = parseInt(pValor.value);
            var cantidadAnterior_temp = listaCarritos[columna].listaProductos[fila].cantidad;
            if (cantidadProducto != cantidadAnterior_temp) {
                var isNotMaximaCantidadSuperada = true;
                if (listaCarritos[columna].listaProductos[fila].pro_canmaxima != null) {
                    if (listaCarritos[columna].listaProductos[fila].pro_canmaxima < cantidadProducto) {
                        isNotMaximaCantidadSuperada = false;
                    }
                }
                if (isNotMaximaCantidadSuperada) {

                    ExcedeImporteFilaCarrito = fila;
                    ExcedeImporteColumnaCarrito = columna;
                    ExcedeImporteValorCarrito = cantidadProducto;
                    var isCantidadMaximaParametrizada = false;
                    //Inicio Cantidad producto parametrizada
                    if (listaCarritos[columna].listaProductos[fila].cantidad != cantidadProducto) {
                        if (cantidadMaximaParametrizada != null) {
                            if (parseInt(cantidadMaximaParametrizada) > 0) {
                                if (parseInt(cantidadMaximaParametrizada) < cantidadProducto) {
                                    isCantidadMaximaParametrizada = true;
                                    funMostrarMensajeCantidadSuperadaCarrito();
                                }
                            }
                        }
                    }
                    //Fin Cantidad producto parametrizada
                    if (!isCantidadMaximaParametrizada) {
                        if (isCarritoDiferido) {
                            CargarOActualizarListaCarrito(listaCarritos[columna].codSucursal, listaCarritos[columna].listaProductos[fila].codProducto, cantidadProducto, false);
                            CargarHtmlCantidadDeCarritoABuscador(listaCarritos[columna].codSucursal, listaCarritos[columna].listaProductos[fila].codProducto, cantidadProducto);
                        } else {
                            cantidadAnterior_carrito = listaCarritos[columna].listaProductos[fila].cantidad;
                            var cantidadFinalCarrito = CargarProductoCantidadDependiendoTransferCarrito(fila, columna, cantidadProducto);
                            if (cantidadFinalCarrito != cantidadProducto) {
                                pValor.value = cantidadFinalCarrito;
                            }
                        }
                    }
                } else {
                    alert(MostrarTextoSuperaCantidadMaxima(listaCarritos[columna].listaProductos[fila].pro_nombre, listaCarritos[columna].listaProductos[fila].pro_canmaxima));
                    var cantidadAnterior = listaCarritos[columna].listaProductos[fila].cantidad;
                    if (cantidadAnterior != '') {
                        pValor.value = cantidadAnterior;
                    }

                }
            }
            ///
        }
    }
}
var cantidadAnterior_carrito = null;
function CargarHtmlCantidadDeCarritoABuscador(pIdSucursal, pIdProduco, pCantidad) {
    if (listaProductosBuscados != null) {
        for (var i = 0; i < listaProductosBuscados.length; i++) {
            if (listaProductosBuscados[i].pro_codigo == pIdProduco) {
                //pCantidad = ObtenerCantidadProductoMasTransfer(pIdSucursal, pIdProduco, listaProductosBuscados[i].pro_nombre);
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

function onNullBuscar() {
    DesmarcarFilaSeleccionada();
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
        if (varPalabraBuscador.length > 1) {
            var isTransferSeleccionado = false;
            if (isCarritoDiferido) {
                isTransferSeleccionado = false;
            } else {
                isTransferSeleccionado = $('#checkBoxTodosTransfer').is(':checked');
            }

            if ($('#checkNombre').is(':checked') || $('#checkBoxCodigoBarra').is(':checked') || $('#checkBoxMonodroga').is(':checked') || $('#checkBoxLaboratorio').is(':checked') || $('#checkBoxCodigoAlfaBeta').is(':checked') || $('#checkBoxTroquel').is(':checked') || $('#checkBoxTodosOfertas').is(':checked') || isTransferSeleccionado) {
                //
                Ascender_pro_precio = true;
                Ascender_PrecioFinal = true;
                Ascender_PrecioConDescuentoOferta = true;
                //
                if ($('#checkNombre').is(':checked') && $('#checkBoxCodigoBarra').is(':checked') && !$('#checkBoxMonodroga').is(':checked') && $('#checkBoxLaboratorio').is(':checked') && !$('#checkBoxCodigoAlfaBeta').is(':checked') && !$('#checkBoxTroquel').is(':checked') && !$('#checkBoxTodosOfertas').is(':checked') && !isTransferSeleccionado) {
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
                    if ($('#checkBoxTroquel').is(':checked')) {
                        arrayListaColumna.push($('#checkBoxTroquel').val());
                    }
                    PageMethods.RecuperarProductosVariasColumnas(varPalabraBuscador, arrayListaColumna, $('#checkBoxTodosOfertas').is(':checked'), isTransferSeleccionado, OnCallBackRecuperarProductos, OnFail);
                }
            } else {
                alert('Seleccione por lo menos una opción de búsqueda');
            }
            isBuscar = true;
        }
    }
    if (!isBuscar) {
        jQuery("#divResultadoBuscador").html('');
    }
}
function onkeypressEnter(e, elemento) {
    tecla = (document.all) ? e.keyCode : e.which;
    if (tecla == 13) {
        onClickBuscar();
    }
}
var timerProducto = null;
function OnMouseOverProdructo(pIndice) {
    if ($("#divMostradorProducto").css("display") == 'none') {
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
    var et = document.documentElement ? document.documentElement.scrollTop : null;
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
    if (listaProductosBuscados[pIndice].pro_troquel != null) {
        $('#tdTroquel').html(AgregarMark(listaProductosBuscados[pIndice].pro_troquel));
    } else {
        $('#tdTroquel').html('');
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
                } else if (listaProductosBuscados[pIndice].pro_codtpovta == 'SC') {
                    mostrarTipoProducto = 'Sustancias Controladas';
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
    // Inicio Imagen Producto
    if (listaProductosBuscados[pIndice].pri_nombreArchivo == null) {
        $('#imgProductoDatos').attr('src', '');
        // $('#imgProductoDatos').css('display', 'none');
        $('#tdImgProductoDatos').css('display', 'none');
    } else {
        $('#imgProductoDatos').attr('src', '../../../servicios/thumbnail.aspx?r=' + 'productos' + '&n=' + listaProductosBuscados[pIndice].pri_nombreArchivo + '&an=' + String(250) + '&al=' + String(250));
        // $('#imgProductoDatos').css('display', 'block');
        $('#tdImgProductoDatos').css('display', 'inline');
    }
    // Fin Imagen Producto
}
//var nroImagenTemp = 0;
//function getNameImagen() {
//    nroImagenTemp++;
//    if (nroImagenTemp > 6)
//    {
//        nroImagenTemp = 1;
//    }
//    var result = '';
//    result = '0' + String(nroImagenTemp) + '.jpg';
//    return result;
//}

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
        $('#tdUnidadMinima').html(listaProductosBuscados[pIndice].tde_minuni);
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

    if (listaProductosBuscados[pIndice].tde_descripcion != null) {
        $('#tdDescripcionUnidadesBonificadas').html(listaProductosBuscados[pIndice].tde_descripcion);
    } else {
        $('#tdDescripcionUnidadesBonificadas').html('');
    }
    //    if (listaProductosBuscados[pIndice].tde_unidadesbonificadasdescripcion != null) {
    //        $('#tdDescripcionUnidadesBonificadas').html(listaProductosBuscados[pIndice].tde_unidadesbonificadasdescripcion);
    //    } else {
    //        $('#tdDescripcionUnidadesBonificadas').html('');
    //    }
    if (listaProductosBuscados[pIndice].tfr_descripcion != null) {
        $('#tdDescripcionTransfer').html(listaProductosBuscados[pIndice].tfr_descripcion);
    } else {
        $('#tdDescripcionTransfer').html('');
    }
}
/// Fin facturacion directa detalle muestra
function onclickOrdenarProducto(pValor) {
    //    alert(pValor);
    if (pValor == -1) {
        PageMethods.RecuperarProductosOrdenar('pro_nombre', Ascender_pro_nombre, OnCallBackRecuperarProductos, OnFail);
        Ascender_pro_nombre = !Ascender_pro_nombre;
    }
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
    if (pValor == 3) {
        PageMethods.RecuperarProductosOrdenar('PrecioConTransfer', Ascender_PrecioConTransfer, OnCallBackRecuperarProductos, OnFail);
        Ascender_PrecioConTransfer = !Ascender_PrecioConTransfer;
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
                strHtml += '<table class="tbl-buscador-productos"  border="0" cellspacing="0" cellpadding="0">';
                strHtml += '<tr>';
                strHtml += '<th class="bp-off-top-left thOrdenar porCamara_cabecera" rowspan="2" onclick="onclickOrdenarProducto(-1)"><div class="bp-top-left">Detalle Producto</div></th>';
                //
                strHtml += '<th class="bp-med-ancho porCamara_cabecera" rowspan="2"  > </th>';
                //
                strHtml += '<th class="bp-med-ancho thOrdenar" rowspan="2"  onclick="onclickOrdenarProducto(0)"> Precio Público</th>';
                strHtml += '<th class="bp-med-ancho thOrdenar" rowspan="2"  onclick="onclickOrdenarProducto(1)">Precio Cliente</th>';
                strHtml += '<th class="bg-oferta" colspan="3">Oferta</th>';
                //NUEVO
                if (!isCarritoDiferido) {
                    strHtml += '<th class="bg-oferta" colspan="2">Transfer</th>';
                }
                //FIN NUEVO
                for (var iEncabezadoSucursal = 0; iEncabezadoSucursal < listaSucursal.length; iEncabezadoSucursal++) {
                    strHtml += '<th class="bp-ancho" rowspan="2">';
                    strHtml += ConvertirSucursalParaColumno(listaSucursal[iEncabezadoSucursal]);
                    strHtml += '</th>';
                }
                strHtml += '</tr>';
                strHtml += '<tr>';
                strHtml += '<th class="bp-min-ancho">%</th>';
                strHtml += '<th class="bp-min-ancho">Mín.</th>'; //Cant. 
                strHtml += '<th class="bp-med-ancho thOrdenar" onclick="onclickOrdenarProducto(2)">Precio</th>';
                //NUEVO
                if (!isCarritoDiferido) {
                    strHtml += '<th class="bp-min-ancho ">&nbsp;&nbsp;Cond.&nbsp;&nbsp;</th>';
                    strHtml += '<th class="bp-med-ancho thOrdenar" onclick="onclickOrdenarProducto(3)">Precio</th>';
                }
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
                    strHtml += strHtmlColorFondo + ' porCamara_detalleProducto">'; //'<td class="first-td2">' + class="trFunciones"  onclick="AnimarPresentacionProducto(' + i + ')"
                    strHtml += '<div  OnMouseMove="OnMouseMoveProdructo(event)" OnMouseOver="OnMouseOverProdructo(' + i + ')" OnMouseOut="OnMouseOutProdructo()" onclick="RecuperarTransfer(' + i + ')" style="cursor:pointer;" >' + AgregarMark(listaProductosBuscados[i].pro_nombre);
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

                    //if (listaProductosBuscados[i].pri_nombreArchivo != null)
                    //{
                    //    strHtml += '&nbsp;<img class="spanProductoIVA" src="../../img/produtos/camera4.png" title="Foto" alt="Foto"  />';//style="width:20px;height:20px; "
                    //    // '../../../servicios/thumbnail.aspx?r=' + 'productos' + '&n=' + listaProductosBuscados[pIndice].pri_nombreArchivo + '&an=' + String(250) + '&al=' + String(250))
                    //    //strHtml += '&nbsp;<img class="spanProductoIVA" src="' + '../../../servicios/thumbnail.aspx?r=productos&n=Camera-Moto-icon.png' +'&an=' + String(20) + '&al=' + String(20) + '" title="Foto" alt="Foto" style="width:20px;height:20px; " />';
                    //}
                    strHtml += '</div>';
                    strHtml += '</td>'; //<span>&raquo;En transfer</span>


                    // style="cursor:pointer;"
                    strHtml += '<td style="text-align:right;cursor:pointer;"  OnMouseMove="OnMouseMoveProdructo(event)" OnMouseOver="OnMouseOverProdructo(' + i + ')" OnMouseOut="OnMouseOutProdructo()" onclick="RecuperarTransfer(' + i + ')" class="' + strHtmlColorFondo + ' cssFilaBuscadorDesmarcar cssFilaBuscador_' + i + ' porCamara_cabeceraYfila">';
                    if (listaProductosBuscados[i].pri_nombreArchivo != null) {
                        strHtml += '<img class="spanProductoFoto" src="../../img/produtos/camera6.png" title="Foto" alt="Foto"  />';//style="width:20px;height:20px; "
                    }
                    strHtml += '</td>';
                    //
                    var precioPublico = '$&nbsp;' + FormatoDecimalConDivisorMiles(listaProductosBuscados[i].pro_precio.toFixed(2));
                    if (listaProductosBuscados[i].pro_precio == 0) {
                        precioPublico = '';
                    }
                    strHtml += '<td style="text-align:right;"  class="' + strHtmlColorFondo + ' cssFilaBuscadorDesmarcar cssFilaBuscador_' + i + '">' + precioPublico + '</td>';
                    //strHtml += '<td style="text-align:right;"  class="' + strHtmlColorFondo + ' cssFilaBuscadorDesmarcar  cssFilaBuscador_' + i + '">$&nbsp;' + FormatoDecimalConDivisorMiles(listaProductosBuscados[i].PrecioFinal.toFixed(2)) + '</td>'; 
                    // 15/02/2018 mail de luciana para el producto
                    //
                    //   if (listaProductosBuscados[i].pro_nombre.match("^PAÑAL PAMI AD")) { //VICK VITAPYRENA MANZ/CANELA 50 SOB
                    if (listaProductosBuscados[i].pro_nombre.match("^PAÑAL PAMI AD")) {
                        strHtml += '<td style="text-align:right;"  class="' + strHtmlColorFondo + ' cssFilaBuscadorDesmarcar  cssFilaBuscador_' + i + '">$&nbsp;' + FormatoDecimalConDivisorMiles(listaProductosBuscados[i].pro_preciofarmacia.toFixed(2)) + '</td>';
                       // strHtml += '<td style="text-align:right;"  class="' + strHtmlColorFondo + ' cssFilaBuscadorDesmarcar  cssFilaBuscador_' + i + '">$&nbsp;' + FormatoDecimalConDivisorMiles(listaProductosBuscados[i].PrecioFinal.toFixed(2)) + '</td>';
                    }
                    else {
                        strHtml += '<td style="text-align:right;"  class="' + strHtmlColorFondo + ' cssFilaBuscadorDesmarcar  cssFilaBuscador_' + i + '">$&nbsp;' + FormatoDecimalConDivisorMiles(listaProductosBuscados[i].PrecioFinal.toFixed(2)) + '</td>';
                    }
                    
                    // fin 15/02/2018 mail de luciana para el producto
                    
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
                    strHtml += '<td style="text-align:right;"  class="' + strHtmlColorFondo + ' cssFilaBuscadorDesmarcar  cssFilaBuscador_' + i + '">' + varPrecioConDescuentoOferta + '</td>'

                    // NUEVO Transfer facturacion directa
                    if (!isCarritoDiferido) {
                        var varTransferFacturacionDirectaCondicion = '';
                        var varTransferFacturacionDirectaPrecio = '';
                        if (isClienteTomaTransfer && listaProductosBuscados[i].isMostrarTransfersEnClientesPerf) {
                            if (listaProductosBuscados[i].isProductoFacturacionDirecta) {
                                if (listaProductosBuscados[i].tde_unidadesbonificadasdescripcion != null) {
                                    varTransferFacturacionDirectaCondicion = listaProductosBuscados[i].tde_unidadesbonificadasdescripcion;
                                }
                                if (listaProductosBuscados[i].PrecioFinalTransfer != null) {
                                    varTransferFacturacionDirectaPrecio = '$&nbsp;' + FormatoDecimalConDivisorMiles(listaProductosBuscados[i].PrecioFinalTransfer.toFixed(2));
                                }
                            }
                        }

                        strHtml += '<td class="' + strHtmlColorFondo + ' cssFilaBuscadorDesmarcar  cssFilaBuscador_' + i + '">';
                        if (varTransferFacturacionDirectaCondicion != '') {
                            strHtml += '<div  OnMouseMove="OnMouseMoveProdructoFacturacionDirecta(event)" OnMouseOver="OnMouseOverProdructoFacturacionDirecta(' + i + ')" OnMouseOut="OnMouseOutProdructoFacturacionDirecta()"  style="cursor:pointer;" >'
                            strHtml += varTransferFacturacionDirectaCondicion;
                            strHtml += '</div>';
                        }
                        strHtml += '</td>'; //NUEVO
                        strHtml += '<td class="' + strHtmlColorFondo + ' cssFilaBuscadorDesmarcar  cssFilaBuscador_' + i + '">' + varTransferFacturacionDirectaPrecio + '</td>';  //NUEVO
                    }
                    // NUEVO Transfer facturacion directa

                    // Optimizar
                    for (var iEncabezadoSucursal = 0; iEncabezadoSucursal < listaSucursal.length; iEncabezadoSucursal++) {
                        strHtml += '<td  class="' + strHtmlColorFondo + ' cssFilaBuscadorDesmarcar  cssFilaBuscador_' + i + '"  >';
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
                                        // var cantidadDeProductoEnCarrito = ObtenerCantidadProducto(listaSucursal[iEncabezadoSucursal], listaProductosBuscados[i].pro_codigo);
                                        var cantidadDeProductoEnCarrito = ObtenerCantidadProductoMasTransfer(listaSucursal[iEncabezadoSucursal], listaProductosBuscados[i].pro_codigo, listaProductosBuscados[i].pro_nombre);
                                        strHtml += '<input class="cssFocusCantProdCarrito" id="inputSuc' + i + "_" + iEncabezadoSucursal + '" type="text"  onfocus="onfocusSucursal(this)" onblur="onblurSucursal(this)" onkeypress="return onKeypressCantProductos(event)" value="' + cantidadDeProductoEnCarrito + '" ></input>';
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
function ObtenerCantidadProductoTransfer(pIdSucursal, pNombreProducto) {
    var resultado = 0;
    if (listaCarritoTransferPorSucursal != null) {
        if (listaCarritoTransferPorSucursal.length > 0) {
            for (var iSucursal = 0; iSucursal < listaCarritoTransferPorSucursal.length; iSucursal++) {
                if (listaCarritoTransferPorSucursal[iSucursal].Sucursal == pIdSucursal) {
                    for (var iTransfer = 0; iTransfer < listaCarritoTransferPorSucursal[iSucursal].listaTransfer.length; iTransfer++) {
                        for (var iTransferProductos = 0; iTransferProductos < listaCarritoTransferPorSucursal[iSucursal].listaTransfer[iTransfer].listaProductos.length; iTransferProductos++) {
                            if (listaCarritoTransferPorSucursal[iSucursal].listaTransfer[iTransfer].listaProductos[iTransferProductos].tde_codpro == pNombreProducto) {
                                if (listaCarritoTransferPorSucursal[iSucursal].listaTransfer[iTransfer].listaProductos[iTransferProductos].isProductoFacturacionDirecta) {
                                    resultado += listaCarritoTransferPorSucursal[iSucursal].listaTransfer[iTransfer].listaProductos[iTransferProductos].cantidad;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    return resultado;
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
        // valorTemp = valorTemp.replace(re, "<mark>$1</mark>");
        // if navagador explore
        var navegador = navigator.appName
        if (navegador == 'Microsoft Internet Explorer') {
            valorTemp = valorTemp.replace(re, '<span style="background-color:#C4E3F7;">$1</span>');
        } else {
            valorTemp = valorTemp.replace(re, "<mark>$1</mark>");
        }
        // fin if navegador explore
    }
    return valorTemp;
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
function onfocusSucursal(pValor) {
    selectInputCarrito = null;
    selectedInputTransfer = null;
    selectedInput = pValor;
    setTimeout(function () { selectedInput.select(); MarcarFilaSeleccionada(pValor); }, 5);
}

function onblurSucursal(pValor) {
    var nombre = pValor.id;
    nombre = nombre.replace('inputSuc', '');
    var palabrasBase = nombre.split("_");
    var fila = parseInt(palabrasBase[0]);
    var columna = parseInt(palabrasBase[1]);
    if (pValor.value != '') {
        // Calcular si producto transfer
        var cantidadComparativa = ObtenerCantidadProductoMasTransfer(listaSucursal[columna], listaProductosBuscados[fila].pro_codigo, listaProductosBuscados[fila].pro_nombre);
        if (cantidadComparativa == pValor.value) {

        } else {
            var isNotMaximaCantidadSuperada = true;
            if (listaProductosBuscados[fila].pro_canmaxima != null) {
                if (listaProductosBuscados[fila].pro_canmaxima < pValor.value) {
                    isNotMaximaCantidadSuperada = false;
                }
            }
            if (isNotMaximaCantidadSuperada) {
                //Inicio Cantidad producto parametrizada
                if (cantidadMaximaParametrizada != null) {
                    if (parseInt(cantidadMaximaParametrizada) > 0) {
                        if (parseInt(cantidadMaximaParametrizada) < parseInt(pValor.value)) {
                            isExcedeImporte = true;
                            ExcedeImporteFila = fila;
                            ExcedeImporteColumna = columna;
                            ExcedeImporteValor = pValor.value;
                            funMostrarMensajeCantidadSuperada();
                        }
                    }
                }
                //Fin Cantidad producto parametrizada
                if (isExcedeImporte) {

                } else {
                    if (isCarritoDiferido) {
                        AgregarAlHistorialProductoCarrito(fila, columna, pValor.value, true);
                    } else {
                        var resultadoCantidadCambio = CargarProductoCantidadDependiendoTransfer(fila, columna, pValor.value);
                        if (resultadoCantidadCambio != pValor.value) {
                            pValor.value = resultadoCantidadCambio;
                        }
                    }
                }
            } else {
                alert(MostrarTextoSuperaCantidadMaxima(listaProductosBuscados[fila].pro_nombre, listaProductosBuscados[fila].pro_canmaxima));
                var cantidadAnterior = ObtenerCantidadProducto(listaSucursal[columna], listaProductosBuscados[fila].pro_codigo);
                pValor.value = cantidadAnterior;
            }
        } // else  if (cantidadComparativa == pValor.value) {
    } else {
        // Borrar en el carrito o colocarlo en 0     
        var cantidad = ObtenerCantidadProducto(listaSucursal[columna], listaProductosBuscados[fila].pro_codigo);
        if (cantidad != '') {
            AgregarAlHistorialProductoCarrito(fila, columna, 0, true);
        }
        ////// Begin 07/04/2016
        //var cantidadTransfer = ObtenerCantidadProductoTransfer(listaSucursal[columna], listaProductosBuscados[fila].pro_nombre);
        //if (cantidadTransfer != '') {
        //    var tempListaProductos = [];
        //    var objProducto = new jcTransfersProductos();
        //    objProducto.codProductoNombre = listaProductosBuscados[fila].tde_codpro; // Para la funcion en el servidor
        //    objProducto.tde_codpro = listaProductosBuscados[fila].tde_codpro;
        //    objProducto.cantidad = 0;
        //    tempListaProductos.push(objProducto);
        //    PageMethods.AgregarProductosTransfersAlCarrito(tempListaProductos, listaProductosBuscados[fila].tde_codtfr, listaSucursal[columna], OnCallBackAgregarProductosTransfersAlCarritoDesdeBuscador, OnFail);
        //}
        ////// End 07/04/2016
    }
    if (!isExcedeImporte) {
        if (isEnterExcedeImporte) {
            isEnterExcedeImporte = false;
            jQuery("#txtBuscador").val('');
            onClickBuscar();
            document.getElementById('txtBuscador').focus();
        }
    }
}

function OnCallBackAgregarProductosTransfersAlCarritoDesdeBuscador(args) {
    // Cargar
    var resultArgs = eval('(' + args + ')');
    listaCarritoTransferPorSucursal = resultArgs.listSucursalCarritoTransfer;
    CargarCarritosTransfersPorSucursal();
    //if (resultArgs.isNotError) {
    //    CargarCarritosTransfersPorSucursal();
    //} else {
    if (!resultArgs.isNotError) {


        var msgProductos = '';
        for (var i = 0; i < resultArgs.listProductosAndCantidadError.length; i++) {
            msgProductos += resultArgs.listProductosAndCantidadError[i].codProductoNombre + '<br/>'
        }
        var htmlMensaje = cuerpo_error + msgProductos;
        MostrarMensajeGeneral(titulo_error, htmlMensaje);
        setTimeout(function () { volverCantidadAnterior_desdeTransfer(); }, 40);


    }
}

function volverCantidadAnterior_desdeTransfer() {
    if (listaCarritoTransferPorSucursal != null) {
        for (var i = 0; i < listaCarritoTransferPorSucursal.length; i++) {
            volverCantidadAnterior_buscadorTodaSucursal(listaCarritoTransferPorSucursal[i].Sucursal);
        }
    }
}

function MostrarTextoSuperaCantidadMaxima(pNombreProducto, pCantidadMaxima) {
    return 'El producto: ' + pNombreProducto + ' \n' + 'Supera la cantidad máxima: ' + pCantidadMaxima;
}
function AgregarAlHistorialProductoCarrito(pIndexProducto, pIndexSucursal, pCantidadProducto, pIsSumarCantidad) {
    if (!isCarritoDiferido) {
        PageMethods.HistorialProductoCarrito(listaProductosBuscados[pIndexProducto].pro_codigo, listaProductosBuscados[pIndexProducto].pro_nombre, listaSucursal[pIndexSucursal], pCantidadProducto, OnCallBackHistorialProductoCarrito, OnFail);
        //listaProductosBuscados[pIndexProducto].pro_codigo = pCantidadProducto;
    }
    CargarOActualizarListaCarrito(listaSucursal[pIndexSucursal], listaProductosBuscados[pIndexProducto].pro_codigo, pCantidadProducto, true);
}
function OnCallBackHistorialProductoCarrito(args) {
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
    var rowCount = table.rows.length;
    var row = table.insertRow(rowCount);

    var cellNombreProducto = row.insertCell(0);
    var colorProductoSinStock = '';
    if (listaCarritos[pIndiceCarrito].listaProductos[pIndiceProductoEnCarrito].stk_stock == 'N') {
        colorProductoSinStock = ' colorRojo ';
    }
    cellNombreProducto.className = 'first-td ' + strHtmlColor + colorProductoSinStock;
    var newContent = document.createTextNode(pNombreProducto);
    cellNombreProducto.appendChild(newContent);
    var cellCantidad = row.insertCell(1);
    cellCantidad.className = strHtmlColor;
    var elementCantidad = document.createElement("input");
    elementCantidad.id = pNombreInputProducto;
    elementCantidad.type = "text";
    elementCantidad.value = pCantidad;
    elementCantidad.onblur = function () {
        onblurInputCarrito(this);
    };
    elementCantidad.onfocus = function () {
        onfocusInputCarrito(this);
    };
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
    }
    var elementPrecio = document.createTextNode(strHtmlPrecioProducto);
    cellPrecio.appendChild(elementPrecio);
}
var isEnterExcedeImporte_msgError = false;
function teclaPresionada_enPagina(e) {
    if (typeof (e) == 'undefined') {
        e = event;
    }
    var keyCode = document.all ? e.which : e.keyCode;

    if (keyCode == 37 || keyCode == 39 || keyCode == 40 || keyCode == 38 || keyCode == 13) {
        if (selectedInput != null) {
            if (selectedInput.id != undefined) {
                if (isMoverCursor) {
                    if (keyCode == 13) {
                        isEnterExcedeImporte = true;
                        isEnterExcedeImporte_msgError = true;
                        onblurSucursal(selectedInput);

                        //                                            if (!isExcedeImporte) {
                        //                                                onblurSucursal(selectedInput);
                        //                                                jQuery("#txtBuscador").val('');
                        //                                                onClickBuscar();
                        //                                                document.getElementById('txtBuscador').focus();
                        //                                            }
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
                        mytext = $("#inputSuc" + fila + "_" + columna);
                        if (mytext.length > 0) {
                        } else {
                            mytext = null;
                        }
                    }
                    ExcedeImporteSiguienteFila = null;
                    ExcedeImporteSiguienteColumna = null;
                    if (mytext != null) {
                        ExcedeImporteSiguienteFila = fila;
                        ExcedeImporteSiguienteColumna = columna;
                        mytext.focus();
                    }
                } // if (isMoverCursor) {
            }
        } else if (selectInputCarrito != null) {

            if (isMoverCursor) {
                var isActualizarActual_enter = false;
                var indiceCarrito = -1;
                var indiceCarritoProducto = -1;

                var nombre = selectInputCarrito.id;
                nombre = nombre.replace('inputCarrito', '');
                var palabrasBase = nombre.split("_");
                indiceCarrito = parseInt(palabrasBase[0]);
                indiceCarritoProducto = parseInt(palabrasBase[1]);
                ExcedeImporteIndiceCarrito = indiceCarrito;
                ExcedeImporteIndiceCarritoProducto = indiceCarritoProducto;

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
                //
                ExcedeImporteSiguienteIndiceCarrito = indiceCarrito;
                ExcedeImporteSiguienteIndiceCarritoProducto = indiceCarritoProducto;
                //
                if (!isSalirWhileCarrito) {
                    objCarrito = $("#inputCarrito" + indiceCarrito + "_" + indiceCarritoProducto);
                    if (objCarrito.length <= 0) {
                        objCarrito = null;
                    }
                }
                if (isActualizarActual_enter) {
                    if (objCarrito != null) {
                        onblurInputCarrito(selectInputCarrito);
                    }
                } else {
                    if (objCarrito != null) {
                        objCarrito.focus();
                    }
                }
            }

        } else if (selectedInputTransfer != null) {
            if (isMoverCursor) {
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
    }
    return true;
}
var tempIdSucursal = null;
var tempIdProduco = null;
var tempCantidadProducto = null;
var tempIsDesdeBuscador = null;

function CargarOActualizarListaCarrito(pIdSucursal, pIdProduco, pCantidadProducto, pIsDesdeBuscador) {
    tempIdSucursal = pIdSucursal;
    tempIdProduco = pIdProduco;
    tempCantidadProducto = pCantidadProducto;
    tempIsDesdeBuscador = pIsDesdeBuscador;

    if (isCarritoDiferido) {
        PageMethods.CargarCarritoDiferido(pIdSucursal, pIdProduco, pCantidadProducto, OnCallBackCargarCarritoDiferido, OnFail);
    } else {
        PageMethods.ActualizarProductoCarrito(pIdProduco, pIdSucursal, pCantidadProducto, OnCallBackActualizarProductoCarrito, OnFail);
    }
}
//tempIdSucursal = pIdSucursal;
//tempIdProduco = pIdProduco;
//tempCantidadProducto = pCantidadProducto;
//tempIsDesdeBuscador = pIsDesdeBuscador;
function OnCallBackActualizarProductoCarrito(args) {
    if (args) {
        if (tempIdSucursal != null && tempIdProduco != null && tempCantidadProducto != null && tempIsDesdeBuscador != null) {
            if (listaCarritos == null) {
                listaCarritos = [];
            }
            var isCarritoFind = false;
            for (var i = 0; i < listaCarritos.length; i++) {
                if (listaCarritos[i].codSucursal == tempIdSucursal) {
                    var reglonesTotal = parseInt($('#tdRenglones' + i).html());
                    var unidadesTotal = parseInt($('#tdUnidades' + i).html());


                    var sumaTotal = ObtenerPrecioConFormato($('#tdTotal' + i).html());
                    var isProductoFind = false;
                    var indiceProducto = -1;
                    for (var iProducto = 0; iProducto < listaCarritos[i].listaProductos.length; iProducto++) {
                        if (listaCarritos[i].listaProductos[iProducto].codProducto == tempIdProduco) {
                            if (listaCarritos[i].listaProductos[iProducto].stk_stock != 'N') {
                                sumaTotal = sumaTotal - CalcularPrecioProductosEnCarrito(listaCarritos[i].listaProductos[iProducto].PrecioFinal, listaCarritos[i].listaProductos[iProducto].cantidad, listaCarritos[i].listaProductos[iProducto].pro_ofeunidades, listaCarritos[i].listaProductos[iProducto].pro_ofeporcentaje);
                            }
                            //
                            unidadesTotal -= listaCarritos[i].listaProductos[iProducto].cantidad;
                            unidadesTotal += parseInt(tempCantidadProducto);
                            //

                            listaCarritos[i].listaProductos[iProducto].cantidad = tempCantidadProducto;
                            isProductoFind = true;
                            indiceProducto = iProducto;
                            /////
                            if (listaProductosBuscados != null) {
                                for (var iProductoBuscador = 0; iProductoBuscador < listaProductosBuscados.length; iProductoBuscador++) {
                                    if (listaProductosBuscados[iProductoBuscador].pro_codigo == tempIdProduco) {
                                        for (var iStock = 0; iStock < listaProductosBuscados[iProductoBuscador].listaSucursalStocks.length; iStock++) {
                                            if (listaProductosBuscados[iProductoBuscador].listaSucursalStocks[iStock].stk_codsuc == tempIdSucursal) {
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
                        produc.codProducto = tempIdProduco;
                        produc.cantidad = tempCantidadProducto;
                        //
                        unidadesTotal += parseInt(tempCantidadProducto);
                        reglonesTotal += 1;
                        //
                        for (var iProductoBuscador = 0; iProductoBuscador < listaProductosBuscados.length; iProductoBuscador++) {
                            if (listaProductosBuscados[iProductoBuscador].pro_codigo == tempIdProduco) {
                                produc.pro_nombre = listaProductosBuscados[iProductoBuscador].pro_nombre;
                                produc.pro_precio = listaProductosBuscados[iProductoBuscador].pro_precio;
                                produc.pro_preciofarmacia = listaProductosBuscados[iProductoBuscador].pro_preciofarmacia;
                                produc.PrecioFinal = listaProductosBuscados[iProductoBuscador].PrecioFinal;
                                produc.PrecioConDescuentoOferta = listaProductosBuscados[iProductoBuscador].PrecioConDescuentoOferta;
                                produc.pro_ofeunidades = listaProductosBuscados[iProductoBuscador].pro_ofeunidades;
                                produc.pro_ofeporcentaje = listaProductosBuscados[iProductoBuscador].pro_ofeporcentaje;
                                produc.pro_canmaxima = listaProductosBuscados[iProductoBuscador].pro_canmaxima;
                                //
                                produc.isProductoFacturacionDirecta = listaProductosBuscados[iProductoBuscador].isProductoFacturacionDirecta;
                                produc.tde_unidadesbonificadas = listaProductosBuscados[iProductoBuscador].tde_unidadesbonificadas;
                                produc.tde_unidadesbonificadasdescripcion = listaProductosBuscados[iProductoBuscador].tde_unidadesbonificadasdescripcion;
                                produc.tde_codpro = listaProductosBuscados[iProductoBuscador].tde_codpro;
                                produc.tde_codtfr = listaProductosBuscados[iProductoBuscador].tde_codtfr;
                                produc.tde_predescuento = listaProductosBuscados[iProductoBuscador].tde_predescuento;
                                produc.PrecioFinalTransfer = listaProductosBuscados[iProductoBuscador].PrecioFinalTransfer;
                                produc.tde_muluni = listaProductosBuscados[iProductoBuscador].tde_muluni;
                                produc.tde_fijuni = listaProductosBuscados[iProductoBuscador].tde_fijuni;
                                produc.tde_maxuni = listaProductosBuscados[iProductoBuscador].tde_maxuni;
                                produc.tde_minuni = listaProductosBuscados[iProductoBuscador].tde_minuni;
                                //
                                for (var iStock = 0; iStock < listaProductosBuscados[iProductoBuscador].listaSucursalStocks.length; iStock++) {
                                    if (listaProductosBuscados[iProductoBuscador].listaSucursalStocks[iStock].stk_codsuc == tempIdSucursal) {
                                        produc.stk_stock = listaProductosBuscados[iProductoBuscador].listaSucursalStocks[iStock].stk_stock;
                                        break;
                                    }
                                }
                                break;
                            }
                        }

                        listaCarritos[i].listaProductos.push(produc);
                        // Agregar producto
                        if (tempIsDesdeBuscador) {
                            indiceProducto = listaCarritos[i].listaProductos.length - 1; // ;
                            agregarFilaAlCarrito(i, indiceProducto, produc.pro_nombre, tempCantidadProducto, produc.PrecioFinal);
                        }
                    } else {
                        //Actualizar producto
                        if (tempIsDesdeBuscador) {
                            $('#inputCarrito' + i + '_' + indiceProducto).val(tempCantidadProducto);
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
                    //
                    $('#tdRenglones' + i).html(reglonesTotal);
                    $('#tdUnidades' + i).html(unidadesTotal);
                    //
                    isCarritoFind = true;
                    setScrollFinDeCarrito(i);
                    break;
                }
            }
            if (!isCarritoFind) {
                var produc = new cProductosEnCarrito();
                produc.codProducto = tempIdProduco;
                produc.cantidad = parseInt(tempCantidadProducto);
                for (var iProductoBuscador = 0; iProductoBuscador < listaProductosBuscados.length; iProductoBuscador++) {
                    if (listaProductosBuscados[iProductoBuscador].pro_codigo == tempIdProduco) {
                        produc.pro_nombre = listaProductosBuscados[iProductoBuscador].pro_nombre;
                        produc.pro_precio = listaProductosBuscados[iProductoBuscador].pro_precio;
                        produc.pro_preciofarmacia = listaProductosBuscados[iProductoBuscador].pro_preciofarmacia;
                        produc.PrecioFinal = listaProductosBuscados[iProductoBuscador].PrecioFinal;
                        produc.PrecioConDescuentoOferta = listaProductosBuscados[iProductoBuscador].PrecioConDescuentoOferta;
                        produc.pro_ofeunidades = listaProductosBuscados[iProductoBuscador].pro_ofeunidades;
                        produc.pro_ofeporcentaje = listaProductosBuscados[iProductoBuscador].pro_ofeporcentaje;
                        produc.pro_canmaxima = listaProductosBuscados[iProductoBuscador].pro_canmaxima;
                        //
                        produc.isProductoFacturacionDirecta = listaProductosBuscados[iProductoBuscador].isProductoFacturacionDirecta;
                        produc.tde_unidadesbonificadas = listaProductosBuscados[iProductoBuscador].tde_unidadesbonificadas;
                        produc.tde_unidadesbonificadasdescripcion = listaProductosBuscados[iProductoBuscador].tde_unidadesbonificadasdescripcion;
                        produc.tde_codpro = listaProductosBuscados[iProductoBuscador].tde_codpro;
                        produc.tde_codtfr = listaProductosBuscados[iProductoBuscador].tde_codtfr;
                        produc.tde_predescuento = listaProductosBuscados[iProductoBuscador].tde_predescuento;
                        produc.PrecioFinalTransfer = listaProductosBuscados[iProductoBuscador].PrecioFinalTransfer;
                        produc.tde_muluni = listaProductosBuscados[iProductoBuscador].tde_muluni;
                        produc.tde_fijuni = listaProductosBuscados[iProductoBuscador].tde_fijuni;
                        produc.tde_maxuni = listaProductosBuscados[iProductoBuscador].tde_maxuni;
                        produc.tde_minuni = listaProductosBuscados[iProductoBuscador].tde_minuni;
                        //
                        for (var iStock = 0; iStock < listaProductosBuscados[iProductoBuscador].listaSucursalStocks.length; iStock++) {
                            if (listaProductosBuscados[iProductoBuscador].listaSucursalStocks[iStock].stk_codsuc == tempIdSucursal) {
                                produc.stk_stock = listaProductosBuscados[iProductoBuscador].listaSucursalStocks[iStock].stk_stock;
                                break;
                            }
                        }
                        break;
                    }
                }
                var carr = new cCarrito();
                carr.codSucursal = tempIdSucursal;
                carr.listaProductos.push(produc);
                listaCarritos.push(carr);
                //Agregar Carrito
                if (tempIsDesdeBuscador) {
                    var indexCarritoAgregar = listaCarritos.length - 1;
                    $('#divContenedorBase_' + listaCarritos[indexCarritoAgregar].codSucursal).html(AgregarCarritoHtml(indexCarritoAgregar));
                    setScrollFinDeCarrito(indexCarritoAgregar);
                    // Actualizar horario cierre
                    indexCarritoHorarioCierre = indexCarritoAgregar;
                    PageMethods.ObtenerHorarioCierre(listaCarritos[indexCarritoHorarioCierre].codSucursal, OnCallBackObtenerHorarioCierre, OnFail);
                    // fin  Actualizar horario cierre
                }
            }
        }
        //(pIdSucursal, pIdProduco, pCantidadProducto, pIsDesdeBuscador)
    }
    else {
        // error
        if (tempIdSucursal != null && tempIdProduco != null && tempCantidadProducto != null && tempIsDesdeBuscador != null) {
            if (tempIsDesdeBuscador) {
                volverCantidadAnterior_buscador(tempIdSucursal, tempIdProduco);
            } else {
                volverCantidadAnterior_carrito(tempIdSucursal, tempIdProduco);
            }
        }
        var htmlMensaje = cuerpo_error + obtenerNombreProducto_buscador(tempIdProduco, tempIdSucursal, tempIsDesdeBuscador);
        MostrarMensajeGeneral(titulo_error, htmlMensaje);
    }
    //    document.getElementById('divContenedorExcedeImporte').style.display = 'none';
    //    document.getElementById('divConfirmarExcedeImporteContenedorGeneralCarrito').style.display = 'none';
}
var titulo_error = "Ha ocurrido un error, por favor intente de nuevo.";
var cuerpo_error = "<b>Los siguientes productos no se pudieron agregar correctamente:</b> <br/>";
var isVolverCantidadAnterior_carrito = false;
//ObtenerCantidadProductoMasTransfer(pIdSucursal, pIdProduco, pNombreProducto)
function obtenerNombreProducto_buscador(pIdProducto, pIdSucursal, pIsDesdeBuscador) {
    var result = '';
    if (pIsDesdeBuscador) {
        if (listaProductosBuscados != null) {
            for (var iEncabezadoSucursal = 0; iEncabezadoSucursal < listaSucursal.length; iEncabezadoSucursal++) {
                if (listaSucursal[iEncabezadoSucursal] == pIdSucursal) {
                    for (var i = 0; i < listaProductosBuscados.length; i++) {
                        if (listaProductosBuscados[i].pro_codigo == pIdProducto) {
                            result = listaProductosBuscados[i].pro_nombre;
                            break;
                        }
                    }
                    break;
                }
            }
        }
    } else {
        if (listaCarritos != null) {
            for (var iIndexCarritoSucursal = 0; iIndexCarritoSucursal < listaCarritos.length; iIndexCarritoSucursal++) {
                if (listaCarritos[iIndexCarritoSucursal].codSucursal == pIdSucursal) {
                    for (var iProductos = 0; iProductos < listaCarritos[iIndexCarritoSucursal].listaProductos.length; iProductos++) {
                        if (listaCarritos[iIndexCarritoSucursal].listaProductos[iProductos].codProducto == pIdProducto) {
                            result = listaCarritos[iIndexCarritoSucursal].listaProductos[iProductos].pro_nombre;
                            break;
                        }
                    }
                }
            }
        }
    }
    return result;
}

function volverCantidadAnterior_buscadorTodaSucursal(pIdSucursal) {
    if (listaProductosBuscados != null) {
        for (var iEncabezadoSucursal = 0; iEncabezadoSucursal < listaSucursal.length; iEncabezadoSucursal++) {
            if (listaSucursal[iEncabezadoSucursal] == pIdSucursal) {
                for (var i = 0; i < listaProductosBuscados.length; i++) {
                    var valorCantidad = ObtenerCantidadProductoMasTransfer(pIdSucursal, listaProductosBuscados[i].pro_codigo, listaProductosBuscados[i].pro_nombre);
                    $('#inputSuc' + i + "_" + iEncabezadoSucursal).val(valorCantidad);
                }
                break;
            }
        }
    }
}


function volverCantidadAnterior_buscador(pIdSucursal, pIdProducto) {
    if (listaProductosBuscados != null) {
        for (var iEncabezadoSucursal = 0; iEncabezadoSucursal < listaSucursal.length; iEncabezadoSucursal++) {
            if (listaSucursal[iEncabezadoSucursal] == pIdSucursal) {
                for (var i = 0; i < listaProductosBuscados.length; i++) {
                    if (listaProductosBuscados[i].pro_codigo == pIdProducto) {
                        var valorCantidad = ObtenerCantidadProductoMasTransfer(pIdSucursal, listaProductosBuscados[i].pro_codigo, listaProductosBuscados[i].pro_nombre);
                        $('#inputSuc' + i + "_" + iEncabezadoSucursal).val(valorCantidad);
                        break;
                    }
                }
                break;
            }
        }
    }

}

function volverCantidadAnterior_carrito(pIdSucursal, pIdProducto) {
    PageMethods.RecuperarCarritoSucursal(pIdSucursal, OnCallBackRecuperarCarritoSucursal, OnFail);
}
function OnCallBackRecuperarCarritoSucursal(args) {
    for (var iIndexCarritoSucursal = 0; iIndexCarritoSucursal < listaCarritos.length; iIndexCarritoSucursal++) {
        if (listaCarritos[iIndexCarritoSucursal].codSucursal == args.codSucursal) {
            listaCarritos[iIndexCarritoSucursal] = args;
            $('#divContenedorBase_' + listaCarritos[iIndexCarritoSucursal].codSucursal).html(AgregarCarritoHtml(iIndexCarritoSucursal));
            setScrollFinDeCarrito(iIndexCarritoSucursal);
        }
    }
}
function OnCallBackCargarCarritoDiferido(args) {
    OnCallBackActualizarProductoCarrito(args);
}
function OnCallBackObtenerHorarioCierre(args) {
    if (args != null) {
        if (indexCarritoHorarioCierre != null) {
            listaCarritos[indexCarritoHorarioCierre].proximoHorarioEntrega = args;
            if (sucursalCliente != listaCarritos[indexCarritoHorarioCierre].codSucursal) {
                $('#proximaEntrega_' + indexCarritoHorarioCierre).html(listaCarritos[indexCarritoHorarioCierre].proximoHorarioEntrega);
                $('#contenedorProximaEntrega_' + indexCarritoHorarioCierre).css('visibility', 'visible');
            }
        }
    }
    indexCarritoHorarioCierre = null;
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

function onclickIsPuedeUsarDll(pIndexCarrito) {
    indexCarritoHorarioCierre = pIndexCarrito;
    PageMethods.IsHacerPedidos(listaCarritos[pIndexCarrito].codSucursal, OnCallBackIsHacerPedidos, OnFail);
}
function OnCallBackIsHacerPedidos(args) {
    if (args == 0) {
        onclickConfirmarCarrito(indexCarritoHorarioCierre);
    } else if (args == 2) {
        if (isCarritoDiferido) {
            location.href = 'carrodiferido.aspx';
        } else {
            location.href = 'PedidosBuscador.aspx';
        }
    } else if (args == 1) {
        alert('En este momento estamos realizando tareas de mantenimiento, por favor confirme su pedido más tarde.');
    }
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
    // Acutalizar horario reparto
    indexCarritoHorarioCierre = pIndexCarrito;
    PageMethods.ObtenerHorarioCierre(listaCarritos[indexCarritoHorarioCierre].codSucursal, OnCallBackObtenerHorarioCierre, OnFail);
    // fin Acutalizar horario reparto

}
function onclickHacerPedido() {
    if (isBotonNoEstaEnProceso) {
        var indexCarrito = $("#hiddenIndexCarrito").val();
        ConfirmarCarrito(indexCarrito);
        //isBotonNoEstaEnProceso = false;
    }
}

function ConfirmarCarrito_ANT(pIndexCarrito) {

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
        PageMethods.TomarPedidoCarrito(listaCarritos[pIndexCarrito].codSucursal, textFactura, textRemito, idTipoEnvio, isUrgente, OnCallBackTomarPedidoCarrito, OnFailBotonEnProceso);
        //
        $('#divCargandoContenedorGeneralFondo').css('display', 'block');
        var arraySizeDocumento = SizeDocumento();
        document.getElementById('divCargandoContenedorGeneralFondo').style.height = arraySizeDocumento[1] + 'px';
        //
    } else {
        alert('Para hacer el pedido se debe superar el monto mínimo de ' + '$ ' + montoMinimo);
    }
}
function ConfirmarCarrito(pIndexCarrito) {
    isBotonNoEstaEnProceso = false;
    var montoMinimo = '';
    var precio = ObtenerPrecioConFormato($('#tdTotal' + pIndexCarrito).html());
    var isTomarPedido = true;
    textTipoEnvioCarrito = $('#comboTipoEnvio option:selected').text();
    var codTipoEnvioCarrito = $('#comboTipoEnvio option:selected').val();
    var isOkCadeteriaRestricciones = true;
    var msgCategoriaRestricciones = '';
    if (tipoEnvioCliente != 'C') { // C -> cadeteria
        if (codTipoEnvioCarrito == 'C') {
            for (var i = 0; i < listaCadeteriaRestricciones.length; i++) {
                if (listaCadeteriaRestricciones[i].tcr_codigoSucursal == listaCarritos[pIndexCarrito].codSucursal) {
                    if (listaCadeteriaRestricciones[i].tcr_MontoIgnorar <= precio) {
                        isOkCadeteriaRestricciones = true;
                    } else {
                        var unidades = $('#tdUnidades' + pIndexCarrito).html();
                        if (listaCadeteriaRestricciones[i].tcr_UnidadesMinimas > unidades) {
                            isOkCadeteriaRestricciones = false;
                            msgCategoriaRestricciones = 'Para seleccionar Cadetería como Tipo de Envío el pedido debe tener ' + listaCadeteriaRestricciones[i].tcr_UnidadesMinimas + ' unidades mínimas y ' + listaCadeteriaRestricciones[i].tcr_UnidadesMaximas + ' unidades máximas'; //'UnidadesMinimas';
                        } else if (listaCadeteriaRestricciones[i].tcr_UnidadesMaximas < unidades) {
                            isOkCadeteriaRestricciones = false;
                            msgCategoriaRestricciones = 'Para seleccionar Cadetería como Tipo de Envío el pedido debe tener ' + listaCadeteriaRestricciones[i].tcr_UnidadesMinimas + ' unidades mínimas y ' + listaCadeteriaRestricciones[i].tcr_UnidadesMaximas + ' unidades máximas'; //'UnidadesMaximas';
                        } else if (listaCadeteriaRestricciones[i].tcr_MontoMinimo > precio) {
                            isOkCadeteriaRestricciones = false;
                            msgCategoriaRestricciones = 'Para seleccionar Cadetería como Tipo de Envío el pedido debe superar los $ ' + listaCadeteriaRestricciones[i].tcr_MontoMinimo;  //'MontoMinimo';
                        }
                    }
                    break;
                }
            }
        }
    }
    if (isOkCadeteriaRestricciones) {
        if (textTipoEnvioCarrito == 'Mostrador') {
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
                    } // fin  if (listaSucursales[i].suc_codigo == listaCarritos[pIndexCarrito].codSucursal) {
                }
            }
        }
        if (isTomarPedido) {
            var textFactura = $('#txtMensajeFactura').val();
            var textRemito = $('#txtMensajeRemito').val();
            var isUrgente = $('#checkboxIsUrgentePedido').is(":checked");
            var idTipoEnvio = $('#comboTipoEnvio').val();
            PageMethods.TomarPedidoCarrito(listaCarritos[pIndexCarrito].codSucursal, textFactura, textRemito, idTipoEnvio, isUrgente, OnCallBackTomarPedidoCarrito, OnFailBotonEnProceso);
            //
            $('#divCargandoContenedorGeneralFondo').css('display', 'block');
            var arraySizeDocumento = SizeDocumento();
            document.getElementById('divCargandoContenedorGeneralFondo').style.height = arraySizeDocumento[1] + 'px';
            //
        } else {
            alert('Para hacer el pedido se debe superar el monto mínimo de ' + '$ ' + montoMinimo);
            isBotonNoEstaEnProceso = true;
        }
    } // fin   if (isOkCadeteriaRestricciones) {
    else {
        alert(msgCategoriaRestricciones);
        isBotonNoEstaEnProceso = true;
    }
}


function OnFailBotonEnProceso(args) {
    OnFail(args);
    $('#divCargandoContenedorGeneralFondo').css('display', 'none');
    isBotonNoEstaEnProceso = true;
}
function OnCallBackTomarPedidoCarrito(args) {
    isBotonNoEstaEnProceso = true;
    /// mostrar faltantes y problema crediticio
    if (args == null) {
        alert(mensajeCuandoSeMuestraError);
        if (isCarritoDiferido) {
            location.href = 'carrodiferido.aspx';
        } else {
            location.href = 'PedidosBuscador.aspx';
        }
    } else {
        // Error dsd dll pedido
        if (args.Error != '') {
            alert(args.Error);
            if (isCarritoDiferido) {
                location.href = 'carrodiferido.aspx';
            } else {
                location.href = 'PedidosBuscador.aspx';
            }
            // Fin Error dsd dll pedido
        } else {

            isHacerBorradoCarritos = true;
            CargarRespuestaDePedido(args);
        }
    }
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

        // Mensaje de problema crediticio
        strHtmlProblemasCrediticios += '<div style="font-size: 14px;font-align:left;  width: 100%;">'; //padding: 2px;
        strHtmlProblemasCrediticios += 'Le recordamos que estos productos se encuentran en el RECUPERADOR DE CREDITO para ser reprocesados.';
        strHtmlProblemasCrediticios += '</div>';
        // Fin Mensaje de problema crediticio
    }
    if (!isProblemaCrediticio) {
        strHtmlProblemasCrediticios = '';
    }


    if (!isProblemaCrediticio && !isFaltantes) {
        strHtmlFaltantes += '<div style="font-size: 14px; padding: 10px; width: 100%;">';
        strHtmlFaltantes += 'El pedido se realizo correctamente';
        strHtmlFaltantes += '</div>';
    }

    $('#divRespuestaProductosPedidos').html(strHtmlProductosPedidos);
    $('#divRespuestaFaltantes').html(strHtmlFaltantes);
    $('#divRespuestaProblemasCrediticios').html(strHtmlProblemasCrediticios);

    document.getElementById('divConfirmarPedidoContenedorGeneral').style.display = 'none';
    document.getElementById('divRespuestaPedidoContenedorGeneral').style.display = 'block';
    $('#divCargandoContenedorGeneralFondo').css('display', 'none');
}
function CargarHtmlTipoEnvio(pSucursal) {
    var strHtml = '';
    strHtml += '<select id="comboTipoEnvio" class="select_gral" onchange="onChangeTipoEnvio()">';
    strHtml += CargarHtmlOptionTipoEnvio(pSucursal);
    strHtml += '</select>';
    $('#tdTipoEnvio').html(strHtml);
    onChangeTipoEnvio();
}

function CargarHtmlOptionTipoEnvio_ANT(pSucursal) {
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
// Armar tipo envio
function CargarHtmlOptionTipoEnvio(pSucursal) {
    var strHtml = '';
    if (listaTipoEnviosSucursal != null) {
        var isSeEncontro = false;
        for (var i = 0; i < listaTipoEnviosSucursal.length; i++) {
            if (listaTipoEnviosSucursal[i].sucursal == pSucursal && listaTipoEnviosSucursal[i].tipoEnvio == tipoEnvioCliente) {
                isSeEncontro = true;
                for (var y = 0; y < listaTipoEnviosSucursal[i].lista.length; y++) {
                    strHtml += '<option value="' + listaTipoEnviosSucursal[i].lista[y].env_codigo + '">' + listaTipoEnviosSucursal[i].lista[y].env_nombre + '</option>';
                }
                break;
            }
        }
        if (!isSeEncontro) {
            for (var i = 0; i < listaTipoEnviosSucursal.length; i++) {
                if (listaTipoEnviosSucursal[i].sucursal == pSucursal && listaTipoEnviosSucursal[i].tipoEnvio == null) {
                    for (var y = 0; y < listaTipoEnviosSucursal[i].lista.length; y++) {
                        strHtml += '<option value="' + listaTipoEnviosSucursal[i].lista[y].env_codigo + '">' + listaTipoEnviosSucursal[i].lista[y].env_nombre + '</option>';
                    }
                    break;
                }
            }
        }
    }
    return strHtml;
}
// fin armar tipo envio
function onChangeTipoEnvio() {
    $('#checkboxIsUrgentePedido').removeAttr("checked");
    $('#tdIsUrgente').css('visibility', 'hidden');
}

function LimpiarTextBoxProductosBuscados_ANT(pIdSucursal) {
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
function LimpiarTextBoxProductosBuscados(pIdSucursal) {
    if (listaProductosBuscados != null) {
        for (var iEncabezadoSucursal = 0; iEncabezadoSucursal < listaSucursal.length; iEncabezadoSucursal++) {
            if (listaSucursal[iEncabezadoSucursal] == pIdSucursal) {
                for (var i = 0; i < listaProductosBuscados.length; i++) {

                    var valorCantidad = ObtenerCantidadProductoMasTransfer(pIdSucursal, listaProductosBuscados[i].pro_codigo, listaProductosBuscados[i].pro_nombre);
                    $('#inputSuc' + i + "_" + iEncabezadoSucursal).val(valorCantidad);
                }
                break;
            }
        }
    }
}

function MensajeFacturaRemitoLength(ta) {
    if (ta.value.length > maxLengthMensajeFacturaRemito) {
        ta.value = ta.value.substring(0, maxLengthMensajeFacturaRemito);
    }
}
function funMostrarMensajeCantidadSuperada() {
    //    alert(mensajeCantidadSuperaElMaximoParametrizado1 + cantidadMaximaParametrizada + mensajeCantidadSuperaElMaximoParametrizado2);
    // divContenedorExcedeImporte
    isMoverCursor = false;
    $('#divMensajeExcedeImporte').html(mensajeCantidadSuperaElMaximoParametrizado1 + cantidadMaximaParametrizada + mensajeCantidadSuperaElMaximoParametrizado2);
    var arraySizeDocumento = SizeDocumento();
    document.getElementById('divContenedorExcedeImporte').style.height = arraySizeDocumento[1] + 'px';
    document.getElementById('divContenedorExcedeImporte').style.display = 'block';
    document.getElementById('divConfirmarExcedeImporteContenedorGeneral').style.display = 'block';
    // document.getElementById('btnExcedeImporte').focus();
}
function CerrarContenedorExcedeImporte() {

}
function funExcedeImporteAceptar() {
    if (ExcedeImporteFila != null && ExcedeImporteColumna != null && ExcedeImporteValor != null) {
        if (isCarritoDiferido) {
            AgregarAlHistorialProductoCarrito(ExcedeImporteFila, ExcedeImporteColumna, ExcedeImporteValor, true);
        } else {
            var resultadoCantidadCambio = CargarProductoCantidadDependiendoTransfer(ExcedeImporteFila, ExcedeImporteColumna, ExcedeImporteValor);
            if (ExcedeImporteValor != resultadoCantidadCambio) {
                var mytext = $("#inputSuc" + ExcedeImporteFila + "_" + ExcedeImporteColumna);
                if (mytext.length > 0) {
                } else {
                    mytext = null;
                }
                if (mytext != null) {
                    mytext.val(resultadoCantidadCambio);
                }
            }
        }
        document.getElementById('divContenedorExcedeImporte').style.display = 'none';
        document.getElementById('divConfirmarExcedeImporteContenedorGeneral').style.display = 'none';
        if (isEnterExcedeImporte) {
            jQuery("#txtBuscador").val('');
            onClickBuscar();
            document.getElementById('txtBuscador').focus();
        } else {
            if (ExcedeImporteSiguienteFila != null && ExcedeImporteSiguienteColumna != null) {
                var mytext = $("#inputSuc" + ExcedeImporteSiguienteFila + "_" + ExcedeImporteSiguienteColumna);
                if (mytext.length > 0) {
                } else {
                    mytext = null;
                }
                if (mytext != null) {
                    mytext.select();
                }
            }
        }
        isEnterExcedeImporte = false;
        isExcedeImporte = false;
        isMoverCursor = true;
        ExcedeImporteFila = null;
        ExcedeImporteColumna = null;
        ExcedeImporteValor = null;
    }

}
function funExcedeImporteCancelar() {
    if (ExcedeImporteFila != null && ExcedeImporteColumna != null && ExcedeImporteValor != null) {
        var mytext = $("#inputSuc" + ExcedeImporteFila + "_" + ExcedeImporteColumna);
        if (mytext.length > 0) {
        } else {
            mytext = null;
        }
        if (mytext != null) {
            mytext.select();
        }

        document.getElementById('divContenedorExcedeImporte').style.display = 'none';
        document.getElementById('divConfirmarExcedeImporteContenedorGeneral').style.display = 'none';
        isEnterExcedeImporte = false;
        isExcedeImporte = false;
        isMoverCursor = true;
        ExcedeImporteFila = null;
        ExcedeImporteColumna = null;
        ExcedeImporteValor = null;
    }
}
function funMostrarMensajeCantidadSuperadaCarrito() {
    isMoverCursor = false;
    $('#divMensajeExcedeImporteCarrito').html(mensajeCantidadSuperaElMaximoParametrizado1 + cantidadMaximaParametrizada + mensajeCantidadSuperaElMaximoParametrizado2);
    var arraySizeDocumento = SizeDocumento();
    document.getElementById('divContenedorExcedeImporte').style.height = arraySizeDocumento[1] + 'px';
    document.getElementById('divContenedorExcedeImporte').style.display = 'block';
    document.getElementById('divConfirmarExcedeImporteContenedorGeneralCarrito').style.display = 'block';
    //document.getElementById('btnExcedeImporteAceptarCarrito').focus();
}
function funExcedeImporteAceptarCarrito() {
    if (ExcedeImporteColumnaCarrito != null && ExcedeImporteFilaCarrito != null && ExcedeImporteValorCarrito != null) {
        if (isCarritoDiferido) {
            CargarOActualizarListaCarrito(listaCarritos[ExcedeImporteColumnaCarrito].codSucursal, listaCarritos[ExcedeImporteColumnaCarrito].listaProductos[ExcedeImporteFilaCarrito].codProducto, ExcedeImporteValorCarrito, false);
            CargarHtmlCantidadDeCarritoABuscador(listaCarritos[ExcedeImporteColumnaCarrito].codSucursal, listaCarritos[ExcedeImporteColumnaCarrito].listaProductos[ExcedeImporteFilaCarrito].codProducto, ExcedeImporteValorCarrito);
        } else {
            var cantidadFinalCarrito = CargarProductoCantidadDependiendoTransferCarrito(ExcedeImporteFilaCarrito, ExcedeImporteColumnaCarrito, ExcedeImporteValorCarrito);
            if (cantidadFinalCarrito != ExcedeImporteValorCarrito) {
                //pValor.value = cantidadFinalCarrito;
                var objCarrito = $("#inputCarrito" + ExcedeImporteIndiceCarrito + "_" + ExcedeImporteIndiceCarritoProducto);
                if (objCarrito.length > 0) {
                } else {
                    objCarrito = null;
                }
                if (objCarrito != null) {
                    objCarrito.val(cantidadFinalCarrito);
                }
            }
        }
        document.getElementById('divContenedorExcedeImporte').style.display = 'none';
        document.getElementById('divConfirmarExcedeImporteContenedorGeneralCarrito').style.display = 'none';


        setTimeout('ActualizarFocusCarritoExcedeImporte()', 5);
        isMoverCursor = true;
        ExcedeImporteColumnaCarrito = null;
        ExcedeImporteFilaCarrito = null;
        ExcedeImporteValorCarrito = null;
    }

}
function ActualizarFocusCarritoExcedeImporte() {
    var objCarrito = $("#inputCarrito" + ExcedeImporteSiguienteIndiceCarrito + "_" + ExcedeImporteSiguienteIndiceCarritoProducto);
    if (objCarrito.length > 0) {
    } else {
        objCarrito = null;
    }
    if (objCarrito != null) {
        objCarrito.select();
    }
}
function funExcedeImporteCancelarCarrito() {
    if (ExcedeImporteColumnaCarrito != null && ExcedeImporteFilaCarrito != null && ExcedeImporteValorCarrito != null) {
        document.getElementById('divContenedorExcedeImporte').style.display = 'none';
        document.getElementById('divConfirmarExcedeImporteContenedorGeneralCarrito').style.display = 'none';
        var objCarrito = $("#inputCarrito" + ExcedeImporteIndiceCarrito + "_" + ExcedeImporteIndiceCarritoProducto);
        if (objCarrito.length > 0) {
        } else {
            objCarrito = null;
        }
        if (objCarrito != null) {
            objCarrito.select();
        }
        isMoverCursor = true;
        ExcedeImporteColumnaCarrito = null;
        ExcedeImporteFilaCarrito = null;
        ExcedeImporteValorCarrito = null;
    }
}
function CargarProductoCantidadDependiendoTransferCarrito(pFila, pColumna, pCantidad) {
    var resultadoReturn = pCantidad;
    var isPasarDirectamente = false;
    var cantTransferViejo = ObtenerCantidadProductoTransfer(listaCarritos[pColumna].codSucursal, listaCarritos[pColumna].listaProductos[pFila].pro_nombre);
    var cantidadMasCantTransferViejo = cantTransferViejo + pCantidad;
    var cantidadCarritoTransfer = 0;
    var cantidadCarritoComun = 0;

    if (listaCarritos[pColumna].listaProductos[pFila].isProductoFacturacionDirecta) { // facturacion directa
        // Combiene transfer o promocion                      
        var precioConDescuentoDependiendoCantidad = CalcularPrecioProductosEnCarrito(listaCarritos[pColumna].listaProductos[pFila].PrecioFinal, pCantidad, listaCarritos[pColumna].listaProductos[pFila].pro_ofeunidades, listaCarritos[pColumna].listaProductos[pFila].pro_ofeporcentaje);
        if (pCantidad == 0) {
            //
        } else {
            precioConDescuentoDependiendoCantidad = precioConDescuentoDependiendoCantidad / pCantidad;
        }
        if (parseFloat(precioConDescuentoDependiendoCantidad) > parseFloat(listaCarritos[pColumna].listaProductos[pFila].PrecioFinalTransfer)) {
            var isSumarTransfer = false;

            if (listaCarritos[pColumna].listaProductos[pFila].tde_muluni != null && listaCarritos[pColumna].listaProductos[pFila].tde_unidadesbonificadas != null) {
                if ((cantidadMasCantTransferViejo >= listaCarritos[pColumna].listaProductos[pFila].tde_muluni) && (cantidadMasCantTransferViejo <= (listaCarritos[pColumna].listaProductos[pFila].tde_muluni + listaCarritos[pColumna].listaProductos[pFila].tde_unidadesbonificadas))) {
                    // es multiplo
                    isSumarTransfer = true;
                    cantidadCarritoTransfer = listaCarritos[pColumna].listaProductos[pFila].tde_muluni + listaCarritos[pColumna].listaProductos[pFila].tde_unidadesbonificadas;
                } else if (cantidadMasCantTransferViejo > (listaCarritos[pColumna].listaProductos[pFila].tde_muluni + listaCarritos[pColumna].listaProductos[pFila].tde_unidadesbonificadas)) {
                    isSumarTransfer = true;
                    var cantidadMultiplicar = parseInt(cantidadMasCantTransferViejo / listaCarritos[pColumna].listaProductos[pFila].tde_muluni);
                    cantidadCarritoTransfer = cantidadMultiplicar * (listaCarritos[pColumna].listaProductos[pFila].tde_muluni + listaCarritos[pColumna].listaProductos[pFila].tde_unidadesbonificadas);
                    //
                    for (var iCantMulti = 0; iCantMulti < cantidadMultiplicar; iCantMulti++) {
                        var cantTemp = iCantMulti * (listaCarritos[pColumna].listaProductos[pFila].tde_muluni + listaCarritos[pColumna].listaProductos[pFila].tde_unidadesbonificadas);
                        if (cantTemp >= cantidadMasCantTransferViejo) {
                            cantidadCarritoTransfer = cantTemp;
                            break;
                        }
                    }
                    //
                    if (cantidadCarritoTransfer == cantidadMasCantTransferViejo) {

                    } else {
                        if (cantidadMasCantTransferViejo < cantidadCarritoTransfer) {
                            cantidadCarritoComun = 0;
                        } else {
                            cantidadCarritoComun = cantidadMasCantTransferViejo - cantidadCarritoTransfer;
                        }
                        //resultadoReturn = cantidadCarritoComun;
                        if ((cantidadCarritoComun >= listaCarritos[pColumna].listaProductos[pFila].tde_muluni) && (cantidadCarritoComun <= (listaCarritos[pColumna].listaProductos[pFila].tde_muluni + listaCarritos[pColumna].listaProductos[pFila].tde_unidadesbonificadas))) {
                            cantidadCarritoTransfer += listaCarritos[pColumna].listaProductos[pFila].tde_muluni + listaCarritos[pColumna].listaProductos[pFila].tde_unidadesbonificadas;
                            cantidadCarritoComun = 0;
                        }
                    }
                }
                if (isSumarTransfer) {
                    // sumar a transfer
                    if (cantTransferViejo != cantidadCarritoTransfer) {
                        var tempListaProductos = [];
                        var objProducto = new jcTransfersProductos();
                        objProducto.codProductoNombre = listaCarritos[pColumna].listaProductos[pFila].tde_codpro; // Para la funcion en el servidor
                        objProducto.tde_codpro = listaCarritos[pColumna].listaProductos[pFila].tde_codpro;
                        // var cantidadTransfer = ObtenerCantidadProductoTransfer(listaCarritos[pColumna].codSucursal, listaCarritos[pColumna].listaProductos[pFila].pro_nombre);
                        objProducto.cantidad = cantidadCarritoTransfer; //  cantidadCarritoTransfer + cantidadTransfer;
                        tempListaProductos.push(objProducto);
                        PageMethods.AgregarProductosTransfersAlCarrito(tempListaProductos, listaCarritos[pColumna].listaProductos[pFila].tde_codtfr, listaCarritos[pColumna].codSucursal, OnCallBackAgregarProductosTransfersAlCarritoDesdeBuscador, OnFail);
                    }
                    if (cantidadCarritoComun == 0) {
                        var cantidad = ObtenerCantidadProducto(listaCarritos[pColumna].codSucursal, listaCarritos[pColumna].listaProductos[pFila].codProducto);
                        if (cantidad != '') {
                            CargarOActualizarListaCarrito(listaCarritos[pColumna].codSucursal, listaCarritos[pColumna].listaProductos[pFila].codProducto, 0, false);
                        }
                    } else {
                        CargarOActualizarListaCarrito(listaCarritos[pColumna].codSucursal, listaCarritos[pColumna].listaProductos[pFila].codProducto, cantidadCarritoComun, false);
                    }
                    CargarHtmlCantidadDeCarritoABuscador(listaCarritos[pColumna].codSucursal, listaCarritos[pColumna].listaProductos[pFila].codProducto, parseInt(cantidadCarritoComun) + parseInt(cantidadCarritoTransfer));
                } else {
                    isPasarDirectamente = true;
                }
                /// fin NUEVO facturacion directa
            }
            else if (listaCarritos[pColumna].listaProductos[pFila].tde_fijuni != null) {

                // UNIDAD FIJA
                if (cantidadMasCantTransferViejo == listaCarritos[pColumna].listaProductos[pFila].tde_fijuni) {
                    isSumarTransfer = true;
                    cantidadCarritoTransfer = listaCarritos[pColumna].listaProductos[pFila].tde_fijuni;
                } else if (cantidadMasCantTransferViejo > listaCarritos[pColumna].listaProductos[pFila].tde_fijuni) {
                    isSumarTransfer = true;
                    cantidadCarritoTransfer = listaCarritos[pColumna].listaProductos[pFila].tde_fijuni;
                    cantidadCarritoComun = cantidadMasCantTransferViejo - listaCarritos[pColumna].listaProductos[pFila].tde_fijuni;
                }
                if (isSumarTransfer) {
                    // sumar a transfer
                    var tempListaProductos = [];
                    var objProducto = new jcTransfersProductos();
                    objProducto.codProductoNombre = listaCarritos[pColumna].listaProductos[pFila].tde_codpro; // Para la funcion en el servidor
                    objProducto.tde_codpro = listaCarritos[pColumna].listaProductos[pFila].tde_codpro;
                    objProducto.cantidad = cantidadCarritoTransfer;
                    tempListaProductos.push(objProducto);
                    PageMethods.AgregarProductosTransfersAlCarrito(tempListaProductos, listaCarritos[pColumna].listaProductos[pFila].tde_codtfr, listaCarritos[pColumna].codSucursal, OnCallBackAgregarProductosTransfersAlCarritoDesdeBuscador, OnFail);
                    if (cantidadCarritoComun == 0) {
                        var cantidad = ObtenerCantidadProducto(listaCarritos[pColumna].codSucursal, listaCarritos[pColumna].listaProductos[pFila].codProducto);
                        if (cantidad != '') {
                            CargarOActualizarListaCarrito(listaCarritos[pColumna].codSucursal, listaCarritos[pColumna].listaProductos[pFila].codProducto, 0, false);
                        }
                    } else {
                        CargarOActualizarListaCarrito(listaCarritos[pColumna].codSucursal, listaCarritos[pColumna].listaProductos[pFila].codProducto, cantidadCarritoComun, false);
                    }
                    CargarHtmlCantidadDeCarritoABuscador(listaCarritos[pColumna].codSucursal, listaCarritos[pColumna].listaProductos[pFila].codProducto, parseInt(cantidadCarritoComun) + parseInt(cantidadCarritoTransfer));
                } else {
                    if (cantTransferViejo != 0) {
                        var tempListaProductos = [];
                        var objProducto = new jcTransfersProductos();
                        objProducto.codProductoNombre = listaCarritos[pColumna].listaProductos[pFila].tde_codpro;
                        objProducto.tde_codpro = listaCarritos[pColumna].listaProductos[pFila].tde_codpro;
                        objProducto.cantidad = 0;
                        tempListaProductos.push(objProducto);
                        PageMethods.AgregarProductosTransfersAlCarrito(tempListaProductos, listaCarritos[pColumna].listaProductos[pFila].tde_codtfr, listaCarritos[pColumna].codSucursal, OnCallBackAgregarProductosTransfersAlCarritoDesdeBuscador, OnFail);
                        $('#divContenedorBaseTransfer_' + listaSucursal[pColumna]).html('');
                    }
                    isPasarDirectamente = true;
                }
                // FIN UNIDAD FIJA

            }
            else if (listaCarritos[pColumna].listaProductos[pFila].tde_minuni != null && listaCarritos[pColumna].listaProductos[pFila].tde_maxuni != null) {
                // UNIDAD MAXIMA Y MINIMA
                if (listaCarritos[pColumna].listaProductos[pFila].tde_minuni <= cantidadMasCantTransferViejo && listaCarritos[pColumna].listaProductos[pFila].tde_maxuni >= cantidadMasCantTransferViejo) {
                    isSumarTransfer = true;
                    cantidadCarritoTransfer = cantidadMasCantTransferViejo;
                } else if (listaCarritos[pColumna].listaProductos[pFila].tde_maxuni < cantidadMasCantTransferViejo) {
                    isSumarTransfer = true;
                    cantidadCarritoTransfer = listaCarritos[pColumna].listaProductos[pFila].tde_maxuni;
                    cantidadCarritoComun = cantidadMasCantTransferViejo - listaCarritos[pColumna].listaProductos[pFila].tde_maxuni;
                }
                if (isSumarTransfer) {
                    // sumar a transfer
                    var tempListaProductos = [];
                    var objProducto = new jcTransfersProductos();
                    objProducto.codProductoNombre = listaCarritos[pColumna].listaProductos[pFila].tde_codpro; // Para la funcion en el servidor
                    objProducto.tde_codpro = listaCarritos[pColumna].listaProductos[pFila].tde_codpro;
                    objProducto.cantidad = cantidadCarritoTransfer;
                    tempListaProductos.push(objProducto);
                    PageMethods.AgregarProductosTransfersAlCarrito(tempListaProductos, listaCarritos[pColumna].listaProductos[pFila].tde_codtfr, listaCarritos[pColumna].codSucursal, OnCallBackAgregarProductosTransfersAlCarritoDesdeBuscador, OnFail);
                    if (cantidadCarritoComun == 0) {
                        var cantidad = ObtenerCantidadProducto(listaCarritos[pColumna].codSucursal, listaCarritos[pColumna].listaProductos[pFila].codProducto);
                        if (cantidad != '') {
                            CargarOActualizarListaCarrito(listaCarritos[pColumna].codSucursal, listaCarritos[pColumna].listaProductos[pFila].codProducto, 0, false);
                        }
                    } else {
                        CargarOActualizarListaCarrito(listaCarritos[pColumna].codSucursal, listaCarritos[pColumna].listaProductos[pFila].codProducto, cantidadCarritoComun, false);
                    }
                    CargarHtmlCantidadDeCarritoABuscador(listaCarritos[pColumna].codSucursal, listaCarritos[pColumna].listaProductos[pFila].codProducto, parseInt(cantidadCarritoComun) + parseInt(cantidadCarritoTransfer));
                } else {
                    if (cantTransferViejo != 0) {
                        var tempListaProductos = [];
                        var objProducto = new jcTransfersProductos();
                        objProducto.codProductoNombre = listaCarritos[pColumna].listaProductos[pFila].tde_codpro;
                        objProducto.tde_codpro = listaCarritos[pColumna].listaProductos[pFila].tde_codpro;
                        objProducto.cantidad = 0;
                        tempListaProductos.push(objProducto);
                        PageMethods.AgregarProductosTransfersAlCarrito(tempListaProductos, listaCarritos[pColumna].listaProductos[pFila].tde_codtfr, listaCarritos[pColumna].codSucursal, OnCallBackAgregarProductosTransfersAlCarritoDesdeBuscador, OnFail);
                        $('#divContenedorBaseTransfer_' + listaSucursal[pColumna]).html('');
                    }
                    isPasarDirectamente = true;
                }
                // FIN UNIDAD MAXIMA Y MINIMA

            }
            else if (listaCarritos[pColumna].listaProductos[pFila].tde_minuni != null) {

                // UNIDAD MINIMA
                if (listaCarritos[pColumna].listaProductos[pFila].tde_minuni <= cantidadMasCantTransferViejo) {
                    isSumarTransfer = true;
                    cantidadCarritoTransfer = cantidadMasCantTransferViejo;
                }
                if (isSumarTransfer) {
                    // sumar a transfer
                    var tempListaProductos = [];
                    var objProducto = new jcTransfersProductos();
                    objProducto.codProductoNombre = listaCarritos[pColumna].listaProductos[pFila].tde_codpro; // Para la funcion en el servidor
                    objProducto.tde_codpro = listaCarritos[pColumna].listaProductos[pFila].tde_codpro;
                    objProducto.cantidad = cantidadCarritoTransfer;
                    tempListaProductos.push(objProducto);
                    PageMethods.AgregarProductosTransfersAlCarrito(tempListaProductos, listaCarritos[pColumna].listaProductos[pFila].tde_codtfr, listaCarritos[pColumna].codSucursal, OnCallBackAgregarProductosTransfersAlCarritoDesdeBuscador, OnFail);
                    if (cantidadCarritoComun == 0) {
                        var cantidad = ObtenerCantidadProducto(listaCarritos[pColumna].codSucursal, listaCarritos[pColumna].listaProductos[pFila].codProducto);
                        if (cantidad != '') {
                            CargarOActualizarListaCarrito(listaCarritos[pColumna].codSucursal, listaCarritos[pColumna].listaProductos[pFila].codProducto, 0, false);
                        }
                    } else {
                        CargarOActualizarListaCarrito(listaCarritos[pColumna].codSucursal, listaCarritos[pColumna].listaProductos[pFila].codProducto, cantidadCarritoComun, false);
                    }
                    CargarHtmlCantidadDeCarritoABuscador(listaCarritos[pColumna].codSucursal, listaCarritos[pColumna].listaProductos[pFila].codProducto, parseInt(cantidadCarritoComun) + parseInt(cantidadCarritoTransfer));
                } else {
                    if (cantTransferViejo != 0) {
                        var tempListaProductos = [];
                        var objProducto = new jcTransfersProductos();
                        objProducto.codProductoNombre = listaCarritos[pColumna].listaProductos[pFila].tde_codpro;
                        objProducto.tde_codpro = listaCarritos[pColumna].listaProductos[pFila].tde_codpro;
                        objProducto.cantidad = 0;
                        tempListaProductos.push(objProducto);
                        PageMethods.AgregarProductosTransfersAlCarrito(tempListaProductos, listaCarritos[pColumna].listaProductos[pFila].tde_codtfr, listaCarritos[pColumna].codSucursal, OnCallBackAgregarProductosTransfersAlCarritoDesdeBuscador, OnFail);
                        $('#divContenedorBaseTransfer_' + listaSucursal[pColumna]).html('');
                    }
                    isPasarDirectamente = true;
                }
                // FIN UNIDAD MINIMA
            }

        } // fin if (listaProductosBuscados[pFila].PrecioConDescuentoOferta > listaProductosBuscados[pFila].PrecioFinalTransfer){
        else {
            isPasarDirectamente = true;
        }
    } else {
        isPasarDirectamente = true;
    }
    if (isPasarDirectamente) {
        CargarOActualizarListaCarrito(listaCarritos[pColumna].codSucursal, listaCarritos[pColumna].listaProductos[pFila].codProducto, pCantidad, false);
        CargarHtmlCantidadDeCarritoABuscador(listaCarritos[pColumna].codSucursal, listaCarritos[pColumna].listaProductos[pFila].codProducto, parseInt(pCantidad) + parseInt(cantTransferViejo));
    }
    else {
        resultadoReturn = parseInt(cantidadCarritoComun);
    }
    return resultadoReturn;
}
function CargarProductoCantidadDependiendoTransfer(pFila, pColumna, pCantidad) {
    var resultadoReturn = pCantidad;
    var isPasarDirectamente = false;
    var cantidadCarritoTransfer = 0;
    var cantidadCarritoComun = 0;

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
                            //resultadoReturn = cantidadCarritoTransfer;
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
                    // sumar a transfer
                    var tempListaProductos = [];
                    var objProducto = new jcTransfersProductos();
                    objProducto.codProductoNombre = listaProductosBuscados[pFila].tde_codpro; // Para la funcion en el servidor
                    objProducto.tde_codpro = listaProductosBuscados[pFila].tde_codpro;
                    objProducto.cantidad = cantidadCarritoTransfer;
                    tempListaProductos.push(objProducto);
                    PageMethods.AgregarProductosTransfersAlCarrito(tempListaProductos, listaProductosBuscados[pFila].tde_codtfr, listaSucursal[pColumna], OnCallBackAgregarProductosTransfersAlCarritoDesdeBuscador, OnFail);
                    if (cantidadCarritoComun == 0) {
                        var cantidad = ObtenerCantidadProducto(listaSucursal[pColumna], listaProductosBuscados[pFila].pro_codigo);
                        if (cantidad != '') {
                            AgregarAlHistorialProductoCarrito(pFila, pColumna, 0, true);
                        }
                    } else {
                        AgregarAlHistorialProductoCarrito(pFila, pColumna, cantidadCarritoComun, true);
                    }
                } else {
                    isPasarDirectamente = true;
                }
                /// FIN UNIDAD MULTIPLO Y BONIFICADA
            } // fin if (listaProductosBuscados[pFila].tde_muluni != null && listaProductosBuscados[pFila].tde_unidadesbonificadas != null){
            else if (listaProductosBuscados[pFila].tde_fijuni != null) {
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
                    // sumar a transfer
                    var tempListaProductos = [];
                    var objProducto = new jcTransfersProductos();
                    objProducto.codProductoNombre = listaProductosBuscados[pFila].tde_codpro; // Para la funcion en el servidor
                    objProducto.tde_codpro = listaProductosBuscados[pFila].tde_codpro;
                    objProducto.cantidad = cantidadCarritoTransfer;
                    tempListaProductos.push(objProducto);
                    PageMethods.AgregarProductosTransfersAlCarrito(tempListaProductos, listaProductosBuscados[pFila].tde_codtfr, listaSucursal[pColumna], OnCallBackAgregarProductosTransfersAlCarritoDesdeBuscador, OnFail);
                    if (cantidadCarritoComun == 0) {
                        var cantidad = ObtenerCantidadProducto(listaSucursal[pColumna], listaProductosBuscados[pFila].pro_codigo);
                        if (cantidad != '') {
                            AgregarAlHistorialProductoCarrito(pFila, pColumna, 0, true);
                        }
                    } else {
                        AgregarAlHistorialProductoCarrito(pFila, pColumna, cantidadCarritoComun, true);
                    }
                } else {
                    isPasarDirectamente = true;
                }
                // FIN UNIDAD FIJA
            } else if (listaProductosBuscados[pFila].tde_minuni != null && listaProductosBuscados[pFila].tde_maxuni != null) {
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
                    // sumar a transfer
                    var tempListaProductos = [];
                    var objProducto = new jcTransfersProductos();
                    objProducto.codProductoNombre = listaProductosBuscados[pFila].tde_codpro; // Para la funcion en el servidor
                    objProducto.tde_codpro = listaProductosBuscados[pFila].tde_codpro;
                    objProducto.cantidad = cantidadCarritoTransfer;
                    tempListaProductos.push(objProducto);
                    PageMethods.AgregarProductosTransfersAlCarrito(tempListaProductos, listaProductosBuscados[pFila].tde_codtfr, listaSucursal[pColumna], OnCallBackAgregarProductosTransfersAlCarritoDesdeBuscador, OnFail);
                    if (cantidadCarritoComun == 0) {
                        var cantidad = ObtenerCantidadProducto(listaSucursal[pColumna], listaProductosBuscados[pFila].pro_codigo);
                        if (cantidad != '') {
                            AgregarAlHistorialProductoCarrito(pFila, pColumna, 0, true);
                        }
                    } else {
                        AgregarAlHistorialProductoCarrito(pFila, pColumna, cantidadCarritoComun, true);
                    }
                } else {
                    isPasarDirectamente = true;
                }
                // FIN UNIDAD MAXIMA Y MINIMA
            }
            else if (listaProductosBuscados[pFila].tde_minuni != null) {
                // UNIDAD MINIMA
                if (listaProductosBuscados[pFila].tde_minuni <= pCantidad) {
                    isSumarTransfer = true;
                    cantidadCarritoTransfer = pCantidad;
                }
                if (isSumarTransfer) {
                    // sumar a transfer
                    var tempListaProductos = [];
                    var objProducto = new jcTransfersProductos();
                    objProducto.codProductoNombre = listaProductosBuscados[pFila].tde_codpro; // Para la funcion en el servidor
                    objProducto.tde_codpro = listaProductosBuscados[pFila].tde_codpro;
                    objProducto.cantidad = cantidadCarritoTransfer;
                    tempListaProductos.push(objProducto);
                    PageMethods.AgregarProductosTransfersAlCarrito(tempListaProductos, listaProductosBuscados[pFila].tde_codtfr, listaSucursal[pColumna], OnCallBackAgregarProductosTransfersAlCarritoDesdeBuscador, OnFail);
                    if (cantidadCarritoComun == 0) {
                        var cantidad = ObtenerCantidadProducto(listaSucursal[pColumna], listaProductosBuscados[pFila].pro_codigo);
                        if (cantidad != '') {
                            AgregarAlHistorialProductoCarrito(pFila, pColumna, 0, true);
                        }
                    } else {
                        AgregarAlHistorialProductoCarrito(pFila, pColumna, cantidadCarritoComun, true);
                    }
                } else {
                    isPasarDirectamente = true;
                }
                // FIN UNIDAD MINIMA
            }
        } // fin if (listaProductosBuscados[pFila].PrecioConDescuentoOferta > listaProductosBuscados[pFila].PrecioFinalTransfer){
        else {
            isPasarDirectamente = true;
        }
    } else {
        isPasarDirectamente = true;
    }
    if (isPasarDirectamente) {
        cantidadCarritoComun = parseInt(pCantidad);
        AgregarAlHistorialProductoCarrito(pFila, pColumna, pCantidad, true);
        var cantidadTransfer = ObtenerCantidadProductoTransfer(listaSucursal[pColumna], listaProductosBuscados[pFila].pro_nombre); // ObtenerCantidadProducto(listaSucursal[pColumna], listaProductosBuscados[pFila].pro_codigo);
        if (cantidadTransfer != '') {
            //AgregarAlHistorialProductoCarrito(pFila, pColumna, 0, true);
            var tempListaProductos = [];
            var objProducto = new jcTransfersProductos();
            objProducto.codProductoNombre = listaProductosBuscados[pFila].tde_codpro; // Para la funcion en el servidor
            objProducto.tde_codpro = listaProductosBuscados[pFila].tde_codpro;
            objProducto.cantidad = 0;
            tempListaProductos.push(objProducto);
            PageMethods.AgregarProductosTransfersAlCarrito(tempListaProductos, listaProductosBuscados[pFila].tde_codtfr, listaSucursal[pColumna], OnCallBackAgregarProductosTransfersAlCarritoDesdeBuscador, OnFail);
            $('#divContenedorBaseTransfer_' + listaSucursal[pColumna]).html('');
        }
    }
    resultadoReturn = parseInt(cantidadCarritoTransfer) + parseInt(cantidadCarritoComun);
    return resultadoReturn;
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
    this.PrecioConDescuentoOferta = -1;
    this.pro_ofeunidades = -1;
    this.pro_ofeporcentaje = -1;
    this.idUsuario = null;
    //
    this.isProductoFacturacionDirecta = false;
    this.tde_unidadesbonificadas = -1;
    this.tde_unidadesbonificadasdescripcion = '';
    this.tde_muluni = -1;
    this.tde_codpro = '';
    this.tde_codtfr = -1;
    this.tde_predescuento = -1;
    this.PrecioFinalTransfer = -1;
    //
}
