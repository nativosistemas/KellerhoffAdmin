var tipo = null;
var listaCompocisionSaldo = null;
var SaldoCtaCteActual = null;
var SaldoResumenAbierto = null;
var listaChequesEnCartera = null;
var SaldoChequesEnCartera = null;
var ResumenAbierto = null;
var limiteSaldo = null;
var IsPoseeCuentaResumen = null;

jQuery(document).ready(function () {

    if (tipo == null) {
        tipo = $('#hiddenTipoInforme').val();
        if (typeof tipo == 'undefined') {
            tipo = null;
        }
    }
    if (listaCompocisionSaldo == null) {
        listaCompocisionSaldo = eval('(' + $('#hiddenListaCompocisionSaldo').val() + ')');
        if (typeof listaCompocisionSaldo == 'undefined') {
            listaCompocisionSaldo = null;
        }
    }
    if (SaldoCtaCteActual == null) {
        SaldoCtaCteActual = $('#hiddenSaldoCtaCteActual').val();
        if (typeof SaldoCtaCteActual == 'undefined') {
            SaldoCtaCteActual = null;
        }
    }
    if (SaldoResumenAbierto == null) {
        SaldoResumenAbierto = $('#hiddenSaldoResumenAbierto').val();
        if (typeof SaldoResumenAbierto == 'undefined') {
            SaldoResumenAbierto = null;
        }
    }
    if (SaldoChequesEnCartera == null) {
        SaldoChequesEnCartera = $('#hiddenSaldoChequesEnCartera').val();
        if (typeof SaldoChequesEnCartera == 'undefined') {
            SaldoChequesEnCartera = null;
        }
    }
    if (IsPoseeCuentaResumen == null) {
        IsPoseeCuentaResumen = $('#hiddenIsPoseeCuentaResumen').val();
        if (typeof IsPoseeCuentaResumen == 'undefined') {
            IsPoseeCuentaResumen = null;
        } else {
            if (IsPoseeCuentaResumen == 'false') {
                IsPoseeCuentaResumen = false;
            } else {
                IsPoseeCuentaResumen =true;
            }
        }
    }

    if (listaChequesEnCartera == null) {
        listaChequesEnCartera = eval('(' + $('#hiddenChequesEnCartera').val() + ')');
        if (typeof listaChequesEnCartera == 'undefined') {
            listaChequesEnCartera = null;
        }
    }
    if (ResumenAbierto == null) {
        ResumenAbierto = eval('(' + $('#hiddenListaResumenAbierto').val() + ')');
        if (typeof ResumenAbierto == 'undefined') {
            ResumenAbierto = null;
        }
    }
    if (limiteSaldo == null) {
        limiteSaldo = $('#hiddenLimiteSaldo').val();
        if (typeof limiteSaldo == 'undefined') {
            limiteSaldo = null;
        } else {
            limiteSaldo = parseFloat(limiteSaldo);
        }
    }
    if (tipo != null) {
        switch (tipo) {
            case '1':
                $('#main-menu-top-CuentaCorriente').addClass('main-menu-top-activeSubMenu');
                break;
            case '2':
                $('#main-menu-top-ResumenAbierto').addClass('main-menu-top-activeSubMenu');
                break;
            case '3':
                $('#main-menu-top-ChequeCartera').addClass('main-menu-top-activeSubMenu');
                break;
            default:
                break;
        }
    }

    CargarHtmlSaldos();
    CargarHtmlCompocisionSaldo_CtaCte();
    CargarHtmlResumenAbierto();
    CargarHtmlChequesEnCartera();
});

function volverComposicionSaldo() {
    location.href = 'composicionsaldo.aspx';
}
function CargarHtmlSaldos() {
    //divSaldoCtaCte
    //divSaldoResumenAbierto
    //divSaldoChequeCartera
    if (SaldoChequesEnCartera != null) {
        $('#divSaldoChequeCartera').html('$&nbsp;' + FormatoDecimalConDivisorMiles(parseFloat(SaldoChequesEnCartera.replace(",", ".")).toFixed(2)));
    } else {
        $('#divSaldoChequeCartera').html('$&nbsp;' + FormatoDecimalConDivisorMiles('0.00'));
    }
    if (IsPoseeCuentaResumen) {
        if (SaldoResumenAbierto != null) {
            $('#divSaldoResumenAbierto').html('$&nbsp;' + FormatoDecimalConDivisorMiles(parseFloat(SaldoResumenAbierto.replace(",", ".")).toFixed(2)));
        } else {
//            $('#divSaldoResumenAbierto').html('$&nbsp;' + FormatoDecimalConDivisorMiles('0.00'));
        } 
    }
    if (SaldoCtaCteActual != null) {
        $('#divSaldoCtaCte').html('$&nbsp;' + FormatoDecimalConDivisorMiles(parseFloat(SaldoCtaCteActual.replace(",", ".")).toFixed(2)));
    } else {
        $('#divSaldoCtaCte').html('$&nbsp;' + FormatoDecimalConDivisorMiles('0.00'));
    }
}

function CargarHtmlCompocisionSaldo_CtaCte() {
    if (listaCompocisionSaldo != null) {
        var strHtml = '';

        if (listaCompocisionSaldo.length > 0) {
            // boton volver
//            strHtml += '<input type="button" onclick="volverComposicionSaldo()" value="VOLVER" class="btn_gral" />';
//            strHtml += '<div style="height:25px;">&nbsp;</div>';
            // fin boton volver
            strHtml += '<div>';
            strHtml += '<table style="width:100%;"><tr><td align="left">';
            strHtml += '<table><tr> <td> <div style="height:16px;width:16px;background-color:green;"></div> </td><td style="color:green;font-size:14px;">Movimientos Vencidos  </td> </tr> </table>';
            strHtml += '</td><td >';
            strHtml += '<input type="button" onclick="volverComposicionSaldo()" value="VOLVER" class="btn_gral" />';
            strHtml += '</td> </tr></table>';
            strHtml += '</div>';
            strHtml += '<table class="tbl-ComposicionSaldo" border="0" cellspacing="0" cellpadding="0"  width="100%">';
            strHtml += '<tr>';
            strHtml += '<th><div class="bp-top-left">Fecha</div></th>'; // class="bp-off-top-left bp-med-ancho"
            strHtml += '<th >Vencimiento</th>';
            strHtml += '<th >Comprobante</th>';
            strHtml += '<th >Semana</th>';
            strHtml += '<th >Importe</th>';
            strHtml += '<th >Recibo N&ordm;</th>';
            strHtml += '<th >Pago</th>';
            strHtml += '<th >Fecha</th>';
            strHtml += '<th >Medio de Pago</th>';
            strHtml += '<th>Atraso</th>';
            strHtml += '<th>Saldo</th>';
            strHtml += '</tr>';
            var fechaActual = new Date();

            for (var i = 0; i < listaCompocisionSaldo.length; i++) {
                var strHtmlColorFondo = '';
                if (i % 2 != 0) {
                    strHtmlColorFondo = ' bp-td-color';
                }
                if (listaCompocisionSaldo[i].NumeroComprobante != '' && listaCompocisionSaldo[i].TipoComprobante < 14) {
                    strHtml += '<tr>';
                    strHtml += '<td class="' + strHtmlColorFondo + '">' + listaCompocisionSaldo[i].FechaToString + '</td>'; //Fecha

                    var strHtmlColorFondoFechaVencimiento = strHtmlColorFondo;
                    var strHtmlColorSaldoCabecera = 'bp-td-colorSaldo';
                    var splitFecha = listaCompocisionSaldo[i].FechaVencimientoToString.split("/");
                    var fechaVencimiento = new Date(splitFecha[2], (splitFecha[1] - 1), splitFecha[0]);
                    if (limiteSaldo != null) {
                        if (fechaVencimiento < fechaActual && listaCompocisionSaldo[i].Saldo > limiteSaldo) {
                            strHtmlColorFondoFechaVencimiento = 'bp-td-colorVencimiento';
                            strHtmlColorSaldoCabecera = 'bp-td-colorVencimiento';
                        }
                    }

                    strHtml += '<td class="' + strHtmlColorFondoFechaVencimiento + '">' + listaCompocisionSaldo[i].FechaVencimientoToString + '</td>'; //Vencimiento

                    //                    strHtml += '<td class="' + strHtmlColorFondo + '">' + listaCompocisionSaldo[i].TipoComprobanteToString + ' ' + listaCompocisionSaldo[i].NumeroComprobante + '</td>'; //Comprobante
                    if (isDetalleComprobante(listaCompocisionSaldo[i].TipoComprobanteToString)) {
                        strHtml += '<td class="' + strHtmlColorFondo + '">' + ' <div class="txt_link_doc"><a href="Documento.aspx?t=' + listaCompocisionSaldo[i].TipoComprobanteToString + '&id=' + listaCompocisionSaldo[i].NumeroComprobante + '" >' + listaCompocisionSaldo[i].TipoComprobanteToString + ' ' + listaCompocisionSaldo[i].NumeroComprobante + '</a></div>' + '</td>';
                    } else {
                        strHtml += '<td class="' + strHtmlColorFondo + '">' + listaCompocisionSaldo[i].TipoComprobanteToString + ' ' + listaCompocisionSaldo[i].NumeroComprobante + '</td>';
                    }
                    strHtml += '<td class="' + strHtmlColorFondo + '">' + listaCompocisionSaldo[i].Semana + '</td>'; //Semana

                    var strImporte = '&nbsp;';
                    if (isNotNullEmpty(listaCompocisionSaldo[i].Importe)) {
                        strImporte = '$&nbsp;' + FormatoDecimalConDivisorMiles(listaCompocisionSaldo[i].Importe.toFixed(2));
                    }
                    strHtml += '<td style="text-align:right !important; white-space:nowrap;" class="' + strHtmlColorFondo + '">' + strImporte + '</td>'; //Importe
                    strHtml += '<td class="' + strHtmlColorFondo + '"><div class="txt_link_doc">' + ObtenerLinkDeDocumentoDesdeStr(listaCompocisionSaldo[i].NumeroRecibo) + '</div></td>'; //Recibo Nro
                    var strPago = '&nbsp;';
                    if (isNotNullEmpty(listaCompocisionSaldo[i].Pago)) {
                        var doublePago = parseFloat(listaCompocisionSaldo[i].Pago.replace(".", "").replace(",", "."));
                        strPago = '$&nbsp;' + FormatoDecimalConDivisorMiles(doublePago.toFixed(2));
                    }
                    strHtml += '<td style="text-align:right !important; white-space:nowrap;" class="' + strHtmlColorFondo + '">' + strPago + '</td>'; //Pago
                    strHtml += '<td class="' + strHtmlColorFondo + '">' + listaCompocisionSaldo[i].FechaPagoToString + '</td>'; //Fecha
                    strHtml += '<td class="' + strHtmlColorFondo + '">' + listaCompocisionSaldo[i].MedioPago + '</td>'; //Medio de Pago
                    strHtml += '<td class="' + strHtmlColorFondo + '">' + listaCompocisionSaldo[i].Atraso + '</td>'; //Atraso
                    var strSaldo = '&nbsp;';
                    if (isNotNullEmpty(listaCompocisionSaldo[i].Saldo)) {
                        strSaldo = '$&nbsp;' + FormatoDecimalConDivisorMiles(listaCompocisionSaldo[i].Saldo.toFixed(2));
                    }
                    strHtml += '<td style="text-align:right !important; white-space:nowrap;" class="' + strHtmlColorSaldoCabecera + '">' + strSaldo + '</td>'; //Saldo
                    strHtml += '</tr>';
                } else {
                    strHtml += '<tr>';
                    strHtml += '<td class="' + strHtmlColorFondo + '">' + '' + '</td>'; //Fecha
                    strHtml += '<td class="' + strHtmlColorFondo + '">' + '' + '</td>'; //Vencimiento
                    strHtml += '<td class="' + strHtmlColorFondo + '">' + '' + '</td>'; //Comprobante
                    strHtml += '<td class="' + strHtmlColorFondo + '">' + '' + '</td>'; //Semana
                    strHtml += '<td class="' + strHtmlColorFondo + '">' + '' + '</td>'; //Importe
                    strHtml += '<td class="' + strHtmlColorFondo + '"><div class="txt_link_doc">' + ObtenerLinkDeDocumentoDesdeStr(listaCompocisionSaldo[i].NumeroRecibo) + '</div></td>'; //Recibo Nro
                    var strPago = '&nbsp;';
                    if (isNotNullEmpty(listaCompocisionSaldo[i].Pago)) {
                        var doublePago = parseFloat(listaCompocisionSaldo[i].Pago.replace(".", "").replace(",", "."));
                        strPago = '$&nbsp;' + FormatoDecimalConDivisorMiles(doublePago.toFixed(2));
                    }
                    strHtml += '<td style="text-align:right !important; white-space:nowrap;" class="' + strHtmlColorFondo + '">' + strPago + '</td>'; //Pago
                    strHtml += '<td class="' + strHtmlColorFondo + '">' + listaCompocisionSaldo[i].FechaPagoToString + '</td>'; //Fecha
                    strHtml += '<td class="' + strHtmlColorFondo + '">' + listaCompocisionSaldo[i].MedioPago + '</td>'; //Medio de Pago
                    strHtml += '<td class="' + strHtmlColorFondo + '">' + listaCompocisionSaldo[i].Atraso + '</td>'; //Atraso
                    strHtml += '<td class="bp-td-colorSaldo">' + '' + '</td>'; //Saldo
                    strHtml += '</tr>';
                }
            }



            // Inicio Importe Total
            strHtml += '<tr>';
            strHtml += '<th style="height:25px;">&nbsp;</th>';
            strHtml += '<th>&nbsp;</th>';
            strHtml += '<th>&nbsp;</th>';
            strHtml += '<th>&nbsp;</th>';
            strHtml += '<th>&nbsp;</th>';
            strHtml += '<th>&nbsp;</th>';
            strHtml += '<th>&nbsp;</th>';
            strHtml += '<th>&nbsp;</th>';
            strHtml += '<th>&nbsp;</th>';
            strHtml += '<th>IMPORTE TOTAL: </th>';
            var strImporteTotal = '&nbsp;';

            if (SaldoCtaCteActual != null) {
                  strImporteTotal = '$&nbsp;' + FormatoDecimalConDivisorMiles(parseFloat(SaldoCtaCteActual.replace(",", ".")).toFixed(2));
            } else {
                strImporteTotal = '$&nbsp;' +  FormatoDecimalConDivisorMiles('0.00');
            }
//            if (SaldoChequesEnCartera != null) {
//                strImporteTotal = '$&nbsp;' + FormatoDecimalConDivisorMiles(parseFloat(SaldoChequesEnCartera.replace(",", ".")).toFixed(2));
//            } else {
//                strImporteTotal = '$&nbsp;' + FormatoDecimalConDivisorMiles('0.00');
//            }

            strHtml += '<th  style="text-align:right !important; white-space:nowrap;" >' + strImporteTotal + '</th>';
            strHtml += '</tr>';

            // Fin Importe Total


            strHtml += '</table>';

            if (listaCompocisionSaldo.length > cantFilaParaEnCabezado) {
                strHtml += '<br/>';
                strHtml += '<input type="button" onclick="volverComposicionSaldo()" value="VOLVER" class="btn_gral" />';
            }

        } // fin  if (listaCompocisionSaldo.length > 0) {}
        else {
            strHtml = objMensajeNoEncontrado;
        }
        $('#divResultadoCompocisionSaldo').html(strHtml);
    }
}
//////////
function CargarHtmlChequesEnCartera() {
    var strHtml = '';
    if (listaChequesEnCartera != null) {

        if (listaChequesEnCartera.length > 0) {
            // boton volver
            strHtml += '<input type="button" onclick="volverComposicionSaldo()" value="VOLVER" class="btn_gral" />';
            strHtml += '<div style="height:25px;">&nbsp;</div>';
            // fin boton volver
            strHtml += '<table class="tbl-ComposicionSaldo" border="0" cellspacing="0" cellpadding="0"  width="100%">';
            strHtml += '<tr>';
            strHtml += '<th  width="40%" ><div class="bp-top-left">Fecha</div></th>'; //<th class="bp-off-top-left bp-med-ancho">
            strHtml += '<th  width="20%"  class="bp-med-ancho" >Banco</th>';
            strHtml += '<th  width="10%"  class="bp-med-ancho" >Cheque Nº</th>';
            strHtml += '<th  width="15%"  class="bp-med-ancho">Depósito</th>';
            strHtml += '<th  width="15%"  class="bp-med-ancho">Importe</th>';
            //            strHtml += '<th   class="bp-med-ancho">Estado</th>';
            strHtml += '</tr>';

            for (var iChequesEnCartera = 0; iChequesEnCartera < listaChequesEnCartera.length; iChequesEnCartera++) {
                var strHtmlColorFondo = '';
                if (iChequesEnCartera % 2 != 0) {
                    strHtmlColorFondo = ' bp-td-color';
                }
                strHtml += '<tr>';
                strHtml += '<td class="' + strHtmlColorFondo + '">' + listaChequesEnCartera[iChequesEnCartera].Fecha.substring(0, 10) + '</td>'; // .substring(10) 28/02/2013 10:14:00 a.m. SOLO MOSTRAR FECHA
                strHtml += '<td class="' + strHtmlColorFondo + '">' + listaChequesEnCartera[iChequesEnCartera].Banco + '</td>';
                strHtml += '<td class="' + strHtmlColorFondo + '">' + listaChequesEnCartera[iChequesEnCartera].Numero + '</td>';
                strHtml += '<td class="' + strHtmlColorFondo + '">' + listaChequesEnCartera[iChequesEnCartera].FechaVencimientoToString + '</td>';
                var strImporte = '&nbsp;';
                if (isNotNullEmpty(listaChequesEnCartera[iChequesEnCartera].Importe)) {
                    strImporte = '$&nbsp;' + FormatoDecimalConDivisorMiles(listaChequesEnCartera[iChequesEnCartera].Importe.toFixed(2));
                }
                strHtml += '<td style="text-align:right !important; white-space:nowrap;" class="' + strHtmlColorFondo + '">' + strImporte + '</td>';
                //                strHtml += '<td class="' + strHtmlColorFondo + '">' + listaChequesEnCartera[iChequesEnCartera].EstadoToString + '</td>';
                strHtml += '</tr>';
            }
            // Inicio Importe Total
            strHtml += '<tr>';
            strHtml += '<th style="height:25px;">&nbsp;</th>';
            strHtml += '<th>&nbsp;</th>';
            strHtml += '<th>&nbsp;</th>';
            strHtml += '<th>IMPORTE TOTAL: </th>';
            var strImporteTotal = '&nbsp;';

            if (SaldoChequesEnCartera != null) {
                strImporteTotal = '$&nbsp;' + FormatoDecimalConDivisorMiles(parseFloat(SaldoChequesEnCartera.replace(",", ".")).toFixed(2));
            } else {
              strImporteTotal = '$&nbsp;' + FormatoDecimalConDivisorMiles('0.00');
            }
 
            strHtml += '<th  style="text-align:right !important; white-space:nowrap;" >' + strImporteTotal + '</th>';
            strHtml += '</tr>';

            // Fin Importe Total

            strHtml += '</table>';


            if (listaChequesEnCartera.length > cantFilaParaEnCabezado) {
                strHtml += '<br/>';
                strHtml += '<input type="button" onclick="volverComposicionSaldo()" value="VOLVER" class="btn_gral" />';
            }

        } else {
            strHtml = objMensajeNoEncontrado;
        }
    }
    $('#divResultadoChequecuenta').html(strHtml);
}
function CargarHtmlResumenAbierto() {
    if (ResumenAbierto != null) {
        var strHtml = '';

        strHtml += '<div style="text-align:left;font-size:12px; margin-bottom:5px;"><b>RESUMEN ABIERTO</b></div>';


        if (ResumenAbierto.lista.length > 0) {
            // boton volver
            strHtml += '<input type="button" onclick="volverComposicionSaldo()" value="VOLVER" class="btn_gral" />';
            strHtml += '<div style="height:25px;">&nbsp;</div>';
            // fin boton volver
            strHtml += '<table class="tbl-buscador-productos"  style="width:100% !important;" border="0" cellspacing="0" cellpadding="0">';
            strHtml += '<tr>'; //bp-off-top-left
            strHtml += '<th width="20%" ><div class="bp-top-left">Fecha</div></th>'; //class="bp-off-top-left bp-med-ancho" 
            strHtml += '<th  width="20%"  class="bp-med-ancho" >Tipo</th>';
            strHtml += '<th  width="20%"  class="bp-med-ancho" >Comprobante</th>';
            strHtml += '<th width="20%"  class="bp-med-ancho" >Importe</th>';
            strHtml += '</tr>';

            for (var i = 0; i < ResumenAbierto.lista.length; i++) {
                strHtml += '<tr>';
                var strHtmlColorFondo = '';
                if (i % 2 != 0) {
                    strHtmlColorFondo = ' bp-td-color';
                }
                strHtml += '<td class="first-td2' + strHtmlColorFondo + '">' + ResumenAbierto.lista[i].FechaToString + '</td>';
                strHtml += '<td class="' + strHtmlColorFondo + '">' + ResumenAbierto.lista[i].TipoComprobanteToString + '</td>';
                strHtml += '<td class="' + strHtmlColorFondo + '">';
                strHtml += '<div>';
                if (isDetalleComprobante(ResumenAbierto.lista[i].TipoComprobanteToString)) {
                    strHtml += ' <div class="txt_link_doc"><a href="Documento.aspx?t=' + ResumenAbierto.lista[i].TipoComprobanteToString + '&id=' + ResumenAbierto.lista[i].NumeroComprobante + '" >' + ResumenAbierto.lista[i].NumeroComprobante + '</a></div>';
                } else {
                    strHtml += ResumenAbierto.lista[i].NumeroComprobante;
                }
                strHtml += '</div>';
                strHtml += ' </td>';
                var strImporte = '&nbsp;';
                if (isNotNullEmpty(ResumenAbierto.lista[i].Importe)) {
                    strImporte = '$&nbsp;' + FormatoDecimalConDivisorMiles(ResumenAbierto.lista[i].Importe.toFixed(2));
                }
                strHtml += '<td style="text-align:right !important; white-space:nowrap;" class="' + strHtmlColorFondo + '">' + strImporte + '</td>';
                strHtml += '</tr>';
            }
            strHtml += '<tr>';
            strHtml += '<th style="height:25px;">&nbsp;</th>';
            strHtml += '<th>&nbsp;</th>';
            strHtml += '<th>IMPORTE TOTAL: </th>';
            var strImporteTotal = '&nbsp;';
            if (isNotNullEmpty(ResumenAbierto.ImporteTotal)) {
                strImporteTotal = '$&nbsp;' + FormatoDecimalConDivisorMiles(ResumenAbierto.ImporteTotal.toFixed(2));
            }
            strHtml += '<th  style="text-align:right !important; white-space:nowrap;" >' + strImporteTotal + '</th>';
            strHtml += '</tr>';

            strHtml += '</table>';

            if (ResumenAbierto.lista.length > cantFilaParaEnCabezado) {
                strHtml += '<br/>';
                strHtml += '<input type="button" onclick="volverComposicionSaldo()" value="VOLVER" class="btn_gral" />';
            }
        } else {
            strHtml = objMensajeNoEncontrado;
        }


        $('#divResultadoResumenAbierto').html(strHtml);
    }
}
