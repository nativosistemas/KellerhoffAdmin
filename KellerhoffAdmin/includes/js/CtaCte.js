var listaFicha = null;
var listaComprobantesEntreFecha = null;
var nroSemanaFicha = null;
//var tipoComprobante = null;


jQuery(document).ready(function () {

    if (listaComprobantesEntreFecha == null) {
        listaComprobantesEntreFecha = eval('(' + $('#hiddenListaComprobantesEntreFecha').val() + ')');
        if (typeof listaComprobantesEntreFecha == 'undefined') {
            listaComprobantesEntreFecha = null;
        }
    }

    if (nroSemanaFicha == null) {
        nroSemanaFicha = $('#hiddenNroSemana').val();
        if (typeof nroSemanaFicha == 'undefined') {
            nroSemanaFicha = null;
        }
    }
    //    if (tipoComprobante == null) {
    //        tipoComprobante = $('#hiddenTipoComprobante').val();
    //        if (typeof tipoComprobante == 'undefined') {
    //            tipoComprobante = null;
    //        }
    //    }
    if (nroSemanaFicha != null) {
        //        $('#cmbSemana').
        //        $('#cmbSemana').attr('selectedIndex', nroSemanaFicha);
        //        
        PageMethods.ObtenerNroSemana(OnCallBackObtenerNroSemana, OnFail);
    }

    //        CargarHtmlResumenAbierto();
    CargarHtmlComprobanteEntreFecha();

});
function OnCallBackObtenerNroSemana(args) {
    $('#cmbSemana')[0].selectedIndex = (args - 1);
    var indexSemana = $('#cmbSemana').val();
    PageMethods.ObtenerMovimientosDeFicha(indexSemana, OnCallBackObtenerMovimientosDeFicha, OnFail);
}
function onclickSeleccionarNroSemana() {
    PageMethods.SetearNroSemana(1, OnCallBackSetearNroSemana, OnFail);
    return false;
}
function OnCallBackSetearNroSemana(args) {
    location.href = 'FichaDebeHaberSaldo.aspx';
}
function OnCallBackObtenerMovimientosDeFicha(args) {
    if (args != '') {
        listaFicha = eval('(' + args + ')');
        CargarFichaEnTablaHtml();
    }
}
function CambiarSemanaFichaDebeHaber() {
    PageMethods.IsBanderaUsarDll(OnCallBackIsBanderaUsarDll_CambiarSemanaFichaDebeHaber, OnFail);

}
function OnCallBackIsBanderaUsarDll_CambiarSemanaFichaDebeHaber(args) {
    if (args) {
        var indexSemana = $('#cmbSemana').val();
        PageMethods.ObtenerMovimientosDeFicha(indexSemana, OnCallBackObtenerMovimientosDeFicha, OnFail);
    } else {
        alert(objMensajeDllNoDisponible);
    }
}

function CargarFichaEnTablaHtml() {
    if (listaFicha != null) {
        var strHtml = '';
        if (listaFicha.length > 0) {
            strHtml += '<table class="tbl-buscador-productos"  style="width:100% !important;" border="0" cellspacing="0" cellpadding="0">';
            strHtml += '<tr>'; //bp-off-top-left
            strHtml += '<th ><div class="bp-top-left">Fecha</div></th>'; // class="bp-off-top-left bp-med-ancho"
            strHtml += '<th class="bp-med-ancho" >Tipo Comprobante</th>';
            strHtml += '<th class="bp-med-ancho" >Comprobante</th>';
            strHtml += '<th class="" >Motivo</th>'; //bp-med-ancho
            strHtml += '<th class="bp-med-ancho" >Fecha Vencimiento</th>';
            strHtml += '<th class="bp-med-ancho" >Debe</th>';
            strHtml += '<th class="bp-med-ancho" >Haber</th>';
            strHtml += '<th class="bp-med-ancho" >Saldo</th>';
            strHtml += '</tr>';
            for (var i = 0; i < listaFicha.length; i++) {
                strHtml += '<tr>';
                // strHtml += '<td class="first-td2';
                var strHtmlColorFondo = '';
                if (i % 2 != 0) {
                    strHtmlColorFondo = ' bp-td-color';
                } //first-td2
                strHtml += '<td class="first-td2' + strHtmlColorFondo + '">' + listaFicha[i].FechaToString + '</td>';
                //                strHtml += '<td class="' + strHtmlColorFondo + '">' + listaFicha[i].Comprobante + '</td>';
                strHtml += '<td class="' + strHtmlColorFondo + '">' + listaFicha[i].TipoComprobanteToString + '</td>';
                strHtml += '<td class="' + strHtmlColorFondo + '">';
                strHtml += '<div>';
                if (isDetalleComprobante(listaFicha[i].TipoComprobanteToString)) {
                    strHtml += '<div class="txt_link_doc"><a href="Documento.aspx?t=' + listaFicha[i].TipoComprobanteToString + '&id=' + listaFicha[i].Comprobante + '" >' + listaFicha[i].Comprobante + '</a></div>';
                } else {
                    strHtml += listaFicha[i].Comprobante;
                }
                strHtml += '</div>';
                strHtml += '</td>';

                strHtml += '<td class="' + strHtmlColorFondo + '"><div>' + listaFicha[i].Motivo.replace(' ', '&nbsp;') + '</div></td>';
                // Fecha vencimiento vacia cuando es el primer registro
                if (i == 0) {
                    strHtml += '<td class="' + strHtmlColorFondo + '"></td>';
                } else {
                    strHtml += '<td class="' + strHtmlColorFondo + '">' + listaFicha[i].FechaVencimientoToString + '</td>';
                }
                // FIN Fecha vencimiento vacia cuando es el primer registro
                var strDebe = '&nbsp;';
                if (isNotNullEmpty(listaFicha[i].Debe)) {
                    strDebe = '$&nbsp;' + FormatoDecimalConDivisorMiles(listaFicha[i].Debe.toFixed(2)); //'<div style="word-wrap:normal !important;">' + +'</div>';
                }
                strHtml += '<td style="text-align:right !important; white-space:nowrap; " align="right" class="' + strHtmlColorFondo + '">' + strDebe + '</td>';
                var strHaber = '&nbsp;';
                if (isNotNullEmpty(listaFicha[i].Haber)) {
                    strHaber = '$&nbsp;' + FormatoDecimalConDivisorMiles(listaFicha[i].Haber.toFixed(2)); //'<div style="word-wrap:normal !important;">' +  + '</div>'; //weight:40px;
                }
                strHtml += '<td style="text-align:right !important; white-space:nowrap;" align="right" class="' + strHtmlColorFondo + '">' + strHaber + '</td>';
                var strSaldo = '&nbsp;';
                if (isNotNullEmpty(listaFicha[i].Saldo)) {
                    strSaldo = '$&nbsp;' + FormatoDecimalConDivisorMiles(listaFicha[i].Saldo.toFixed(2));
                }
                strHtml += '<td style="text-align:right !important; white-space:nowrap;" align="right" class="' + strHtmlColorFondo + '">' + strSaldo + '</td>';
                // Optimizar
                //                    for (var iEncabezadoSucursal = 0; iEncabezadoSucursal < listaSucursal.length; iEncabezadoSucursal++) {
                //                        strHtml += '<td  class="' + strHtmlColorFondo + '">';
                //                        for (var iSucursal = 0; iSucursal < listaProductosBuscados[i].listaSucursalStocks.length; iSucursal++) {
                //                            if (listaProductosBuscados[i].listaSucursalStocks[iSucursal].stk_codsuc == listaSucursal[iEncabezadoSucursal]) {
                //                                strHtml += '<div class="cont-estado-input"><div class="estado-' + listaProductosBuscados[i].listaSucursalStocks[iSucursal].stk_stock.toLowerCase() + '"></div>' + '<input id="inputSuc' + i + "_" + iEncabezadoSucursal + '" type="text"  onfocus="onfocusSucursal(this)" onblur="onblurSucursal(this)" onkeypress="return onKeypressCantProductos(event)" ></input> </div>';
                //                                break;
                //                            }
                //                        }
                //                        strHtml += '</td>';
                //                    }
                strHtml += '</tr>';
            }
            strHtml += '</table>';

            if (listaFicha.length > cantFilaParaEnCabezado) {
                strHtml += '<br/>';
                strHtml += '<input type="button" onclick="volver()" value="VOLVER" class="btn_gral" />';
            }

        }
        document.getElementById('divResultadoFicha').innerHTML = strHtml;
    }
}

//function CargarHtmlResumenAbierto() {
//    if (ResumenAbierto != null) {
//        var strHtml = '';
//        strHtml += '<div style="text-align:left;font-size:14px;"><b>Resumen Abierto</b></div>';
//        strHtml += '<table class="tbl-buscador-productos"  style="width:750px !important;" border="0" cellspacing="0" cellpadding="0">';
//        strHtml += '<tr>'; //bp-off-top-left
//        strHtml += '<th class="bp-off-top-left bp-med-ancho" ><div class="bp-top-left">Fecha</div></th>';
//        strHtml += '<th class="bp-med-ancho" >Tipo</th>';
//        strHtml += '<th class="bp-med-ancho" >Comprobante</th>';
//        strHtml += '<th class="bp-med-ancho" >Importe</th>';
//        strHtml += '</tr>';
//        for (var i = 0; i < ResumenAbierto.lista.length; i++) {
//            strHtml += '<tr>';
//            var strHtmlColorFondo = '';
//            if (i % 2 != 0) {
//                strHtmlColorFondo = ' bp-td-color';
//            }
//            strHtml += '<td class="first-td2' + strHtmlColorFondo + '">' + ResumenAbierto.lista[i].FechaToString + '</td>';
//            strHtml += '<td class="' + strHtmlColorFondo + '">' + ResumenAbierto.lista[i].TipoComprobanteToString + '</td>';
//            strHtml += '<td class="' + strHtmlColorFondo + '">';
//            strHtml += '<div>';
//            if (isDetalleComprobante(ResumenAbierto.lista[i].TipoComprobanteToString)) {
//                strHtml += ' <a href="Documento.aspx?t=' + ResumenAbierto.lista[i].TipoComprobanteToString + '&id=' + ResumenAbierto.lista[i].NumeroComprobante + '" >' + ResumenAbierto.lista[i].NumeroComprobante + '</a>';
//            } else {
//                strHtml += ResumenAbierto.lista[i].NumeroComprobante;
//            }
//            strHtml += '</div>';
//            strHtml += ' </td>';
//            strHtml += '<td class="' + strHtmlColorFondo + '">$&nbsp;' + ResumenAbierto.lista[i].Importe + '</td>';
//            strHtml += '</tr>';
//        }
//        strHtml += '<tr>';
//        strHtml += '<th  ></th>';
//        strHtml += '<th   > </th>';
//        strHtml += '<th  >Importe Total: </th>';
//        strHtml += '<th   >$&nbsp;' + ResumenAbierto.ImporteTotal + '</th>';
//        strHtml += '</tr>';
//        strHtml += '</table>';
//        $('#divResultadoResumenAbierto').html(strHtml);
//    }
//}


function onclickComprobanteNro() {
    //    var nro = $('#txtNroComprobante').val();
    //    var parteAdelante = '';
    //    parteAdelante = $("#cmbTipoComprobante option:selected").text().substring(4);
    //    location.href = 'Documento.aspx?t=' + $('#cmbTipoComprobante').val().substring(0, 3) + '&id=' + String(parteAdelante) + String(nro);
    PageMethods.IsBanderaUsarDll(OnCallBackIsBanderaUsarDll_ComprobanteNro, OnFail);
    return false;
}
function OnCallBackIsBanderaUsarDll_ComprobanteNro(args) {
    if (args) {
        var nro = $('#txtNroComprobante').val();
        var parteAdelante = '';
        parteAdelante = $("#cmbTipoComprobante option:selected").text().substring(4);
        location.href = 'Documento.aspx?t=' + $('#cmbTipoComprobante').val().substring(0, 3) + '&id=' + String(parteAdelante) + String(nro);
    } else {
        alert(objMensajeDllNoDisponible);
    }
}
function CargarHtmlComprobanteEntreFecha() {
    if (listaComprobantesEntreFecha != null) {
        var strHtml = '';
        if (listaComprobantesEntreFecha.length > 0) {
            strHtml += '<table class="tbl-buscador-productos"  style="width:100% !important;" border="0" cellspacing="0" cellpadding="0">';
            strHtml += '<tr>';
            strHtml += '<th  width="30%"   ><div class="bp-top-left">Fecha</div></th>'; //class="bp-off-top-left bp-med-ancho"
            strHtml += '<th  width="20%"  class="bp-med-ancho" >Tipo</th>';
            strHtml += '<th width="30%"  class="bp-med-ancho" >Comprobante</th>';
            strHtml += '<th  width="20%" class="bp-med-ancho" >Importe</th>';
            strHtml += '</tr>';

            for (var i = 0; i < listaComprobantesEntreFecha.length; i++) {
                strHtml += '<tr>';
                var strHtmlColorFondo = '';
                if (i % 2 != 0) {
                    strHtmlColorFondo = ' bp-td-color';
                }
                strHtml += '<td class="first-td2' + strHtmlColorFondo + '">' + listaComprobantesEntreFecha[i].FechaToString + '</td>'; // listaComprobantesEntreFecha[i].FechaToString
                strHtml += '<td class="' + strHtmlColorFondo + '">' + listaComprobantesEntreFecha[i].Comprobante + '</td>';
                strHtml += '<td class="' + strHtmlColorFondo + '">';
                strHtml += '<div>';
                if (isDetalleComprobante(listaComprobantesEntreFecha[i].Comprobante)) {
                    strHtml += '<div class="txt_link_doc"><a href="Documento.aspx?t=' + listaComprobantesEntreFecha[i].Comprobante + '&id=' + listaComprobantesEntreFecha[i].NumeroComprobante + '" >' + listaComprobantesEntreFecha[i].NumeroComprobante + '</a></div>';
                } else {
                    strHtml += listaComprobantesEntreFecha[i].NumeroComprobante;
                }
                strHtml += '</div>';
                strHtml += ' </td>';
                strHtml += '<td style="text-align:right !important; white-space:nowrap;" class="' + strHtmlColorFondo + '">$&nbsp;' + FormatoDecimalConDivisorMiles(listaComprobantesEntreFecha[i].MontoTotal.toFixed(2)) + '</td>';
                strHtml += '</tr>';
            }

            strHtml += '</table>';

            if (listaComprobantesEntreFecha.length > cantFilaParaEnCabezado) {
                strHtml += '<br/>';
                strHtml += '<input type="button" onclick="volver()" value="VOLVER" class="btn_gral" />';
            }

        } else {
            strHtml = objMensajeNoEncontrado;
        }
        $('#divResultadoComprobanteEntreFecha').html(strHtml);
    }
}


/////////////////////////////////


//function CargarHtmlCompocisionSaldo() {
//    if (listaCompocisionSaldo != null) {
//        var strHtml = '';
//        var isEncabezadoDetalle = false;
//        var countFila = 0;
//        for (var i = 0; i < listaCompocisionSaldo.length; i++) {

//            if (listaCompocisionSaldo[i].NumeroComprobante != '' && listaCompocisionSaldo[i].TipoComprobante < 14) {

//                isEncabezadoDetalle = true;
//                strHtml += '<table class="tbl-ComposicionSaldoPrimeraParte"  border="0" cellpadding="0" cellspacing="1" width="100%">';
//                strHtml += '<tr>';
//                strHtml += '<td align="center" valign="middle" style="height:16px;" width="15%" > Fecha: ' + listaCompocisionSaldo[i].FechaToString + '</td>';
//                strHtml += '<td align="center" valign="middle" style="height:16px;" width="10%" >Semana:' + listaCompocisionSaldo[i].Semana + '</td>';
//                strHtml += '<td align="center" valign="middle" style="height:16px;" width="24%">Comprobante: ' + listaCompocisionSaldo[i].TipoComprobanteToString + ' ' + listaCompocisionSaldo[i].NumeroComprobante + '</td>';
//                strHtml += '<td align="center" valign="middle" style="height:16px;" width="24%" >Fecha Vencimiento: ' + listaCompocisionSaldo[i].FechaVencimientoToString + '</td>';
//                strHtml += '<td align="center" valign="middle" style="height:16px;" width="15%" >Importe: ' + listaCompocisionSaldo[i].Importe + '</div></td>';
//                strHtml += '<td align="center" valign="middle" style="height:16px;" width="12%">Saldo: ' + listaCompocisionSaldo[i].Saldo + '</td>';
//                strHtml += '</tr>';
//                strHtml += '</table>';
//            } else {
//                if (isEncabezadoDetalle) {
//                    strHtml += '<table border="0" cellpadding="0" cellspacing="1" width="100%">';
//                    strHtml += '<tr>';
//                    strHtml += '<td align="center" valign="middle" class="bp-td-color-ComposicionSaldoEncabezadoDetalle" width="10%"><b>Fecha</b></td>';
//                    strHtml += '<td align="center" valign="middle"" class="bp-td-color-ComposicionSaldoEncabezadoDetalle" width="30%"><b>Forma de Pago</b></td>';
//                    strHtml += '<td align="center" valign="middle"" class="bp-td-color-ComposicionSaldoEncabezadoDetalle" width="20%"><b>Comprobante ' + 'N&ordm;</b></td>';
//                    strHtml += '<td align="center" valign="middle" class="bp-td-color-ComposicionSaldoEncabezadoDetalle" width="10%"><b>Pago</b></td>';
//                    strHtml += '<td align="center" valign="middle" class="bp-td-color-ComposicionSaldoEncabezadoDetalle" width="10%"><b>Atraso</b></td>';
//                    strHtml += '</tr>';
//                    strHtml += '</table>';

//                    isEncabezadoDetalle = false;
//                    countFila = 0;
//                }
//                countFila++;
//                var strHtmlColorFondo = '';
//                if (countFila % 2 != 0) {
//                    strHtmlColorFondo = ' bp-td-color';
//                }
//                strHtml += '<table border="0" cellpadding="0" cellspacing="1" width="100%" >';
//                strHtml += '<tr>';
//                strHtml += '<td align="center" class="' + strHtmlColorFondo + '"  width="10%"><b>' + listaCompocisionSaldo[i].FechaPagoToString + '</b></td>';
//                strHtml += '<td align="center" class="' + strHtmlColorFondo + '" width="30%"><b>';
//                if (listaCompocisionSaldo[i].MedioPago == 'PESOS') {
//                    strHtml += 'EFECTIVO';
//                } else {
//                    strHtml += listaCompocisionSaldo[i].MedioPago;
//                }
//                strHtml += '</b>';
//                strHtml += '</td>';
//                strHtml += '<td align="center" class="' + strHtmlColorFondo + '" width="20%"><b>';

//                if (listaCompocisionSaldo[i].NumeroRecibo.substring(0, 3) == 'NDE') {
//                    //                    strHtml += '<a href="/servicios/notadebito.asp?NroNotaDeDebito=<%=mid(objComprobantes.NumeroRecibo,5,5) & right(objComprobantes.NumeroRecibo,8)%>" class="formnombrelink"><%=objComprobantes.NumeroRecibo%></a>';
//                    strHtml += listaCompocisionSaldo[i].NumeroRecibo;
//                } else if (listaCompocisionSaldo[i].NumeroRecibo.substring(0, 3) == 'NCR') {
//                    //                    strHtml += '<a href="/servicios/notacredito.asp?NroNotaDeCredito=<%=mid(objComprobantes.NumeroRecibo,5,5) & right(objComprobantes.NumeroRecibo,8)%>" class="formnombrelink"><%=objComprobantes.NumeroRecibo%></a>';
//                    strHtml += listaCompocisionSaldo[i].NumeroRecibo;
//                } else {
//                    strHtml += listaCompocisionSaldo[i].NumeroRecibo;
//                }
//                strHtml += '</b></td>';
//                if (listaCompocisionSaldo[i].Pago != '') {
//                    strHtml += '<td align="right" class="' + strHtmlColorFondo + '" width="10%"><b>$' + listaCompocisionSaldo[i].Pago + '</b>&nbsp;</td>';
//                } else {
//                    strHtml += '<td align="right" class="' + strHtmlColorFondo + '" width="10%"><b>$ 0</b>&nbsp;</td>';
//                }

//                strHtml += '<td align="center" class="' + strHtmlColorFondo + '" width="10%"><b>' + listaCompocisionSaldo[i].Atraso + '</b></td>';
//                strHtml += '</table>';
//            }
//        }
//        if (SaldoCtaCteActual != null) {
//            strHtml += '<table width="100%"  border="0" cellpadding="0" cellspacing="1">';
//            strHtml += '<tr>';
//            strHtml += '<td align="right"   width="100%">SUBTOTAL CUENTA CORRIENTE: <font color="#333366">$&nbsp;';
//            strHtml += SaldoCtaCteActual;
//            strHtml += '</td>';
//            strHtml += '</tr>';
//            strHtml += '</table>';
//        }
//        CargarHtmlChequesEnCartera();

//        $('#divResultadoCompocisionSaldo').html(strHtml);
//    }
//}


//function CargarHtmlChequesEnCartera() {
//    var strHtml = '';
//    if (listaChequesEnCartera != null) {

//        if (listaChequesEnCartera.length > 0) {
//            strHtml += '<table class="tbl-buscador-productos" >';
//            strHtml += '<tr>';
//            strHtml += '<th class="bp-off-top-left bp-med-ancho"><div class="bp-top-left">Fecha</div></th>';
//            strHtml += '<th class="bp-med-ancho" >Banco</th>';
//            strHtml += '<th class="bp-med-ancho" >Cheque Nº</th>';
//            strHtml += '<th class="bp-med-ancho">Depósito</th>';
//            strHtml += '<th class="bp-med-ancho">Importe</th>';
//            strHtml += '<th class="bp-med-ancho">Estado</th>';
//            strHtml += '</tr>';

//            for (var iChequesEnCartera = 0; iChequesEnCartera < listaChequesEnCartera.length; iChequesEnCartera++) {
//                var strHtmlColorFondo = '';
//                if (iChequesEnCartera % 2 != 0) {
//                    strHtmlColorFondo = ' bp-td-color';
//                }
//                strHtml += '<tr>';
//                strHtml += '<td class="' + strHtmlColorFondo + '">' + listaChequesEnCartera[iChequesEnCartera].Fecha + '</td>';
//                strHtml += '<td class="' + strHtmlColorFondo + '">' + listaChequesEnCartera[iChequesEnCartera].Banco + '</td>';
//                strHtml += '<td class="' + strHtmlColorFondo + '">' + listaChequesEnCartera[iChequesEnCartera].Numero + '</td>';
//                strHtml += '<td class="' + strHtmlColorFondo + '">' + listaChequesEnCartera[iChequesEnCartera].FechaVencimientoToString + '</td>';
//                strHtml += '<td class="' + strHtmlColorFondo + '">' + listaChequesEnCartera[iChequesEnCartera].Importe + '</td>';
//                strHtml += '<td class="' + strHtmlColorFondo + '">' + listaChequesEnCartera[iChequesEnCartera].EstadoToString + '</td>';
//                strHtml += '</tr>';
//            }
//            strHtml += '</table>';


//        }
//    }
//    return strHtml;
//}

//function CargarHtmlCompocisionSaldo_CtaCte() {
//    if (listaCompocisionSaldo != null) {
//        var strHtml = '';
//        var isEncabezadoDetalle = false;
//        var countFila = 0;
//        var countFilaEncabesado = 0;
//        if (listaCompocisionSaldo.length > 0) {
//            //width="24%"
//            strHtml += '<table class="tbl-ComposicionSaldo" border="0" cellspacing="0" cellpadding="0"  width="100%">';
//            strHtml += '<tr>';
//            strHtml += '<th class="bp-off-top-left bp-med-ancho"><div class="bp-top-left">Fecha</div></th>';
//            strHtml += '<th >Vencimiento</th>';
//            strHtml += '<th >Comprobante</th>';
//            strHtml += '<th >Semana</th>';
//            strHtml += '<th >Importe</th>';
//            strHtml += '<th >Recibo N&ordm;</th>';
//            strHtml += '<th >Pago</th>';
//            strHtml += '<th >Fecha</th>';
//            strHtml += '<th >Medio de Pago</th>';
//            strHtml += '<th>Atraso</th>';
//            strHtml += '<th>Saldo</th>';
//            strHtml += '</tr>';

//            for (var i = 0; i < listaCompocisionSaldo.length; i++) {
//                var strHtmlColorFondo = '';
//                if (i % 2 != 0) {
//                    strHtmlColorFondo = ' bp-td-color';
//                }
//                if (listaCompocisionSaldo[i].NumeroComprobante != '' && listaCompocisionSaldo[i].TipoComprobante < 14) {
//                    //                    isEncabezadoDetalle = true;
//                    //                    var strHtmlColorFondo = '';
//                    //                    if (countFilaEncabesado % 2 != 0) {
//                    //                        strHtmlColorFondo = ' bp-td-color-ComposicionSaldo-CabeceraPar';
//                    //                    } else {
//                    //                        strHtmlColorFondo = ' bp-td-color-ComposicionSaldo-CabeceraImpar';
//                    //                    }
//                    //                    countFilaEncabesado++;
//                    strHtml += '<tr>';
//                    strHtml += '<td class="' + strHtmlColorFondo + '">' + listaCompocisionSaldo[i].FechaToString + '</td>'; //Fecha
//                    strHtml += '<td class="' + strHtmlColorFondo + '">' + listaCompocisionSaldo[i].FechaVencimientoToString + '</td>'; //Vencimiento
//                    strHtml += '<td class="' + strHtmlColorFondo + '">' + listaCompocisionSaldo[i].TipoComprobanteToString + ' ' + listaCompocisionSaldo[i].NumeroComprobante + '</td>'; //Comprobante
//                    strHtml += '<td class="' + strHtmlColorFondo + '">' + listaCompocisionSaldo[i].Semana + '</td>'; //Semana
//                    strHtml += '<td class="' + strHtmlColorFondo + '">' + listaCompocisionSaldo[i].Importe + '</td>'; //Importe
//                    strHtml += '<td class="' + strHtmlColorFondo + '">' + listaCompocisionSaldo[i].NumeroRecibo + '</td>'; //Recibo Nro
//                    strHtml += '<td class="' + strHtmlColorFondo + '">' + listaCompocisionSaldo[i].Pago + '</td>'; //Pago
//                    strHtml += '<td class="' + strHtmlColorFondo + '">' + listaCompocisionSaldo[i].FechaPagoToString + '</td>'; //Fecha
//                    strHtml += '<td class="' + strHtmlColorFondo + '">' + listaCompocisionSaldo[i].MedioPago + '</td>'; //Medio de Pago
//                    strHtml += '<td class="' + strHtmlColorFondo + '">' + listaCompocisionSaldo[i].Atraso + '</td>'; //Atraso
//                    strHtml += '<td class="' + strHtmlColorFondo + '">' + listaCompocisionSaldo[i].Saldo + '</td>'; //Saldo
//                    strHtml += '</tr>';
//                } else {
//                    //                    if (isEncabezadoDetalle) {
//                    //                        isEncabezadoDetalle = false;
//                    //                        countFila = 0;
//                    //                    }
//                    //                    countFila++;
//                    //                    var strHtmlColorFondo = '';
//                    //                    if (countFila % 2 != 0) {
//                    //                        strHtmlColorFondo = ' bp-td-color';
//                    //                    }
//                    strHtml += '<tr>';
//                    strHtml += '<td class="' + strHtmlColorFondo + '">' + '' + '</td>'; //Fecha
//                    strHtml += '<td class="' + strHtmlColorFondo + '">' + '' + '</td>'; //Vencimiento
//                    strHtml += '<td class="' + strHtmlColorFondo + '">' + '' + '</td>'; //Comprobante
//                    strHtml += '<td class="' + strHtmlColorFondo + '">' + '' + '</td>'; //Semana
//                    strHtml += '<td class="' + strHtmlColorFondo + '">' + '' + '</td>'; //Importe
//                    strHtml += '<td class="' + strHtmlColorFondo + '">' + '' + '</td>'; //Recibo Nro
//                    strHtml += '<td class="' + strHtmlColorFondo + '">' + listaCompocisionSaldo[i].Pago + '</td>'; //Pago
//                    strHtml += '<td class="' + strHtmlColorFondo + '">' + listaCompocisionSaldo[i].FechaPagoToString + '</td>'; //Fecha
//                    strHtml += '<td class="' + strHtmlColorFondo + '">' + listaCompocisionSaldo[i].MedioPago + '</td>'; //Medio de Pago
//                    strHtml += '<td class="' + strHtmlColorFondo + '">' + listaCompocisionSaldo[i].Atraso + '</td>'; //Atraso
//                    strHtml += '<td class="' + strHtmlColorFondo + '">' + '' + '</td>'; //Saldo
//                    strHtml += '</tr>';
//                }
//            }
//            strHtml += '</table>';
//            $('#divResultadoCompocisionSaldo').html(strHtml);
//        } // fin  if (listaCompocisionSaldo.length > 0) {}
//    }
//}
