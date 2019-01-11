var tamanioOferta_img = '300';

function prepararListaOfertas(pValor) {
    listOferta = eval('(' + pValor + ')');
    if (typeof listOferta == 'undefined') {
        listOferta = null;
    }
    CargarHtmlOfertasEnHome();
}
function CargarHtmlOfertasEnHome() {
    if (listOferta != null) {
        var strHtml = '';       
        strHtml += '<div class="row">';      
        for (var i = 0; i < listOferta.length; i++) {
            if (multiple(i, 4) && i != 0) {
                strHtml += '</div>';
                strHtml += '<div class="row">';
            }
            strHtml += '<div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">';
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
                strHtml += '<img class="img-responsive"  src="' + '../../../servicios/thumbnail.aspx?r=' + 'ofertas' + '&n=' + listOferta[i].nameImagen + '&an=' + tamanioOferta_img + '&al=' + tamanioOferta_img + '&c=FFFFFF" alt="oferta" title="" />';
            else
                strHtml += '<img class="img-responsive"  src="' + '../../../servicios/thumbnail.aspx?r=' + 'ofertas' + '&n=' + 'productosinfoto.png' + '&an=' + tamanioOferta_img + '&al=' + tamanioOferta_img + '&c=FFFFFF" alt="oferta" title=""/>';
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
            //if (isNotNullEmpty(listOferta[i].namePdf))
            //{
            //    txtAñadirCarrito = 'AÑADIR';
            //    strHtml += '<a class="btn_ampliar  float-right" target ="_blank" href="../../archivos/' + 'ofertaspdf' + '/' + listOferta[i].namePdf + '">AMPLIAR</a>';//AMPLIAR
            //}
            if (listOferta[i].ofe_tipo == 1 && listOferta[i].countOfertaDetalles > 0)
                strHtml += '<a class="btn_carrito" onclick="onclickCarrito(' + listOferta[i].ofe_idOferta + ');" >' + txtAñadirCarrito + '</a>';//AÑADIR AL CARRITO
            else if (listOferta[i].ofe_tipo == 2 && listOferta[i].ofe_nombreTransfer != null)
                strHtml += '<a class="btn_carrito" onclick="onclickCarrito(' + listOferta[i].ofe_idOferta + ');" >' + txtAñadirCarrito + '</a>';

            strHtml += '</div>'; //<div class="price col-lg-12">
            strHtml += '<div class="clearfix"></div>';
            //strHtml += '</div>';
            strHtml += '</div>'; //<div class="row">
            strHtml += '</div>'; //<div class="info">
            strHtml += '</div>'; //'<div class="col-item">'
            strHtml += '</div>'; //'<div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">'
        }
        strHtml += '</div>'; // '<div class="row">';
        $('#divContenedorOfertas').html(strHtml);
    }
}
function onclickCarrito(pValor) {
    idOferta = pValor;
    //$('#name_carrito').val('');
    //$('#password_carrito').val('');
    var myName = localStorage['name'] || '';
    var myPass = localStorage['pass'] || '';
    $('#name_carrito').val(myName);
    $('#password_carrito').val(myPass);
    $('#myModal').modal();
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