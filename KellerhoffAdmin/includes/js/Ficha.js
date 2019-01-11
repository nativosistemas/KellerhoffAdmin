var listaFicha = null;
jQuery(document).ready(function () {

  //  PageMethods.ObtenerMovimientosDeFicha(50, OnCallBackObtenerMovimientosDeFicha, OnFail);
});

function OnCallBackObtenerMovimientosDeFicha(args) {
    if (args != '') {
        listaFicha = eval('(' + args + ')');
        CargarFichaEnTablaHtml();
    }
    //    else {
    //        listaFicha = null;
    //    }
}
//function CargarFichaEnTablaHtml() {
//    if (listaFicha != null) {
//        var strMotivo = '';
//        for (var i = 0; i < listaFicha.length; i++) {
//            strMotivo += ' + ' + listaFicha[i].Motivo;
//        }
//        alert(strMotivo);
//    }
//}
function CambiarSemana() {
var indexSemana =   $('#cmbSemana').val();
PageMethods.ObtenerMovimientosDeFicha(indexSemana, OnCallBackObtenerMovimientosDeFicha, OnFail);
}
function CargarFichaEnTablaHtml() {
    if (listaFicha != null) {
        var strHtml = '';
        if (listaFicha.length > 0) {
            strHtml += '<table class="tbl-buscador-productos"  style="width:750px !important;" border="0" cellspacing="0" cellpadding="0">';
            strHtml += '<tr>'; //bp-off-top-left
            strHtml += '<th class="bp-off-top-left bp-med-ancho" ><div class="bp-top-left">Fecha</div></th>'; //<div class="bp-top-left"></div> <div class="bp-top-left">Fecha</div>
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
                //strHtml += '<div>' + listaFicha[i].Fecha + '</div></td>';
                strHtml += '<td class="' + strHtmlColorFondo + '">' + listaFicha[i].Comprobante + '</td>';
                strHtml += '<td class="' + strHtmlColorFondo + '"><div>' + listaFicha[i].Motivo.replace(' ', '&nbsp;') + '</div></td>';
                strHtml += '<td class="' + strHtmlColorFondo + '">' + listaFicha[i].FechaVencimientoToString + '</td>';
                var strDebe = '&nbsp;';
                if (isNotNullEmpty(listaFicha[i].Debe)) {
                    strDebe = '$&nbsp;' + listaFicha[i].Debe;
                }
                strHtml += '<td class="' + strHtmlColorFondo + '">' + strDebe + '</td>';
                var strHaber = '&nbsp;';
                if (isNotNullEmpty(listaFicha[i].Haber)) {
                    strHaber = '$&nbsp;' + listaFicha[i].Haber;
                }
                strHtml += '<td class="' + strHtmlColorFondo + '">' + strHaber + '</td>';
                var strSaldo = '&nbsp;';
                if (isNotNullEmpty(listaFicha[i].Saldo)) {
                    strSaldo = '$&nbsp;' + listaFicha[i].Saldo;
                }
                strHtml += '<td class="' + strHtmlColorFondo + '">' + strSaldo + '</td>';
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
        }
        document.getElementById('divResultadoFicha').innerHTML = strHtml;
    }
}