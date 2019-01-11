jQuery(document).ready(function () {
    // RecuperarTodasOfertas();
    prepararListaOfertas($('#hiddenListaOfertas').val());
});
function prepararListaOfertas(pValor) {


    listOferta = eval('(' + pValor + ')'); // $('#hiddenListaOfertas').val()
    if (typeof listOferta == 'undefined') {
        listOferta = null;
    }
    CargarHtmlOfertasEnHome();

}
var tamanio = 250;
function CargarHtmlOfertasEnHome() {
    if (listOferta != null) {
        var strHtml = '';
        strHtml += '<table>';
        var rowCount = 1;
        for (var i = 0; i < listOferta.length; i++) {
            if (rowCount == 1)
                strHtml += '<tr>';

            strHtml += '<td>';
            strHtml += '<div class="col-item">';
            strHtml += '<div class="photo">';
            if (isNotNullEmpty(listOferta[i].ofe_etiqueta)) {
                var colorEtiqueta = '';
                if (isNotNullEmpty(listOferta[i].ofe_etiquetaColor))
                    colorEtiqueta = 'style="background-color: #' + listOferta[i].ofe_etiquetaColor + '"';
                strHtml += '<div class="etiqueta" ' + colorEtiqueta + '>' + listOferta[i].ofe_etiqueta + '</div>';
            }
            if (isNotNullEmpty(listOferta[i].namePdf)) {
                //strHtml += '<a target="_blank" href="../../archivos/' + 'ofertaspdf' + '/' + listOferta[i].namePdf + '"><div class="bg_promo"></div><div class="btn_bg_promo">AMPLIAR</div>';
                strHtml += '<a target="_blank" href="../../servicios/descargarArchivo.aspx?t=' + 'ofertaspdf' + '&n=' + listOferta[i].namePdf + '"><div class="bg_promo"></div><div class="btn_bg_promo">AMPLIAR</div>';

            }
            if (isNotNullEmpty(listOferta[i].nameImagen))
                // strHtml += '<img class="img-responsive"  src="' + '../../../servicios/thumbnail.aspx?r=' + 'ofertas' + '&n=' + listOferta[i].nameImagen + '&an=' + String(300) + '&al=' + String(300) + '&c=FFFFFF" alt="oferta" title="" />';
                strHtml += '<div class="div_ImagenPromo" style="' + 'background-image: url(../../../servicios/thumbnail.aspx?r=ofertas&n=' + listOferta[i].nameImagen + '&an='+ tamanio +'&al='+ tamanio +'&c=FFFFFF);' + '"></div>';
            else
                //strHtml += '<img class="img-responsive"  src="' + '../../../servicios/thumbnail.aspx?r=' + 'ofertas' + '&n=' + 'productosinfoto.png' + '&an=' + String(300) + '&al=' + String(300) + '&c=FFFFFF" alt="oferta" title=""/>';
                strHtml += '<div class="div_ImagenPromo" style="' + 'background-image: url(../../../servicios/thumbnail.aspx?r=ofertas&n=' + 'productosinfoto.png' + '&an=' + tamanio + '&al=' + tamanio + '&c=FFFFFF);' + '"></div>';
            if (isNotNullEmpty(listOferta[i].namePdf)) {
                strHtml += '</a>';
            }
            strHtml += '</div>'; //<div class="photo">
            strHtml += '<div class="info">';
            strHtml += '<div class="row">';
            strHtml += '<div class="price col-lg-12">';
            strHtml += '<h5>' + listOferta[i].ofe_titulo + '</h5>';
            strHtml += '<p>' + listOferta[i].ofe_descr + '</p>';
            var descuento = '';
            if (isNotNullEmpty(listOferta[i].ofe_descuento))
                descuento = listOferta[i].ofe_descuento;
            strHtml += '<div class="desc">' + descuento + '</div>';
            var txtAñadirCarrito = 'AÑADIR AL CARRITO';
            //if (isNotNullEmpty(listOferta[i].namePdf)) {
            //    txtAñadirCarrito = 'AÑADIR';
            //    strHtml += '<a class="btn_ampliar  float-right" target ="_blank" href="../../archivos/' + 'ofertaspdf' + '/' + listOferta[i].namePdf + '">AMPLIAR</a>';//AMPLIAR
            //    //  var strHtml = '<a href="../../archivos/' + file.tipo + '/' + file.arc_nombre + '">' + file.arc_nombre + '</a>';
            //}
            if (listOferta[i].ofe_tipo == 1 && listOferta[i].countOfertaDetalles > 0)
                strHtml += '<a class="btn_carrito" onclick="onclickCarrito(' + listOferta[i].ofe_idOferta + ');" >' + txtAñadirCarrito + '</a>';
            else if (listOferta[i].ofe_tipo == 2 && listOferta[i].ofe_nombreTransfer != null)
                strHtml += '<a class="btn_carrito" onclick="onclickCarrito(' + listOferta[i].ofe_idOferta + ');" >' + txtAñadirCarrito + '</a>';

            strHtml += '</div>'; //<div class="price col-lg-12">
            strHtml += '<div class="clearfix"></div>';
            //strHtml += '</div>';
            strHtml += '</div>'; //<div class="row">
            strHtml += '</div>'; //<div class="info">
            strHtml += '</div>'; //'<div class="col-item">'

            strHtml += '</td>';

            if (rowCount == 3)
                strHtml += '</tr>';
            rowCount++;
            if (rowCount > 3)
                rowCount = 1;
        }
        if (rowCount == 2)
            strHtml += '<td></td><td></td></tr>';
        if (rowCount == 3)
            strHtml += '<td></td></tr>';

        strHtml += '</table>'; // '<div class="row">';
        $('#divContenedorOfertas').html(strHtml);
    }
}
function CargarHtmlOfertasEnHome_ant() {
    if (listOferta != null) {
        var strHtml = '';
        strHtml += '<div class="row">';
        for (var i = 0; i < listOferta.length; i++) {
            if (multiple(i, 2) && i != 0) {
                strHtml += '</div>';
                strHtml += '<div class="row">';
            }
            //strHtml += '<div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">';
            strHtml += '<div class="col-item">';
            strHtml += '<div class="photo">';
            if (isNotNullEmpty(listOferta[i].ofe_etiqueta)) {
                var colorEtiqueta = '';
                if (isNotNullEmpty(listOferta[i].ofe_etiquetaColor))
                    colorEtiqueta = 'style="background-color: #' + listOferta[i].ofe_etiquetaColor + '"';
                strHtml += '<div class="etiqueta" ' + colorEtiqueta + '>' + listOferta[i].ofe_etiqueta + '</div>';
            }
            if (isNotNullEmpty(listOferta[i].nameImagen))
                strHtml += '<img class="img-responsive"  src="' + '../../../servicios/thumbnail.aspx?r=' + 'ofertas' + '&n=' + listOferta[i].nameImagen + '&an=' + String(300) + '&al=' + String(300) + '&c=FFFFFF" alt="oferta" title="" />';
            else
                strHtml += '<img class="img-responsive"  src="' + '../../../servicios/thumbnail.aspx?r=' + 'ofertas' + '&n=' + 'productosinfoto.png' + '&an=' + String(300) + '&al=' + String(300) + '&c=FFFFFF" alt="oferta" title=""/>';

            strHtml += '</div>'; //<div class="photo">
            strHtml += '<div class="info">';
            strHtml += '<div class="row">';
            strHtml += '<div class="price col-lg-12">';
            strHtml += '<h5>' + listOferta[i].ofe_titulo + '</h5>';
            strHtml += '<p>' + listOferta[i].ofe_descr + '</p>';
            var descuento = '';
            if (isNotNullEmpty(listOferta[i].ofe_descuento))
                descuento = listOferta[i].ofe_descuento;
            strHtml += '<div class="desc">' + descuento + '</div>';
            if (listOferta[i].ofe_tipo == 1 && listOferta[i].countOfertaDetalles > 0)
                strHtml += '<a class="btn_carrito" onclick="onclickCarrito(' + listOferta[i].ofe_idOferta + ');" >AÑADIR AL CARRITO</a>';
            else if (listOferta[i].ofe_tipo == 2 && listOferta[i].ofe_nombreTransfer != null)
                strHtml += '<a class="btn_carrito" onclick="onclickCarrito(' + listOferta[i].ofe_idOferta + ');" >AÑADIR AL CARRITO</a>';

            strHtml += '</div>'; //<div class="price col-lg-12">
            strHtml += '<div class="clearfix"></div>';
            //strHtml += '</div>';
            strHtml += '</div>'; //<div class="row">
            strHtml += '</div>'; //<div class="info">
            strHtml += '</div>'; //'<div class="col-item">'
            //strHtml += '</div>'; //'<div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">'
        }
        strHtml += '</div>'; // '<div class="row">';
        $('#divContenedorOfertas').html(strHtml);
    }
}

function onclickCarrito(pValor) {
    idOferta = pValor;
    PageMethods.cargarOferta(pValor,OnCallBackcargarOferta, OnFail);
    //$('#name_carrito').val('');
    //$('#password_carrito').val('');
    //$('#myModal').modal();

    //  setTimeout(function () { $('#name_carrito').focus(); }, 10000);
}
function OnCallBackcargarOferta(args)
{
    if (args)
    location.href = 'PedidosBuscador.aspx';
}
function RecuperarTodasOfertas() {
    PageMethods.RecuperarTodasOfertas(OnCallBackRecuperarTodasOfertas, OnFail);
}
var listOferta = [];
function OnCallBackRecuperarTodasOfertas(args) {
    if (args) {
        prepararListaOfertas(args);
    }
}