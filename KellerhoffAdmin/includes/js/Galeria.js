var listaImagenes = null;
var ruta = 'noticia';
var intAnchoImagenGaleria = 760;
var intAltoImagenGaleria = 507;
var intScrollImagenGaleria = 20;
var intAnchoImagenBarra = 113;
var intAltoImagenBarra = 96;
var colorImagenGaleria = '000000';
var indexSeleccionado = -1;
var objGaleria = null;
var timerSlider = null;

jQuery(document).ready(function () {

    window.onresize = function (event) {
        RedimensionarGaleria();
    }

    RedimensionarGaleria();
});


function clickCargarGaleria(pValor) {
    objGaleria = null;
    listaImagenes = null;
    PageMethods.CargarNoticiaConImagenes(pValor, OnCallBackCargarNoticiaConImagenes, OnFail);
}
function OnCallBackCargarNoticiaConImagenes(args) {
    if (objGaleria == null) {
        objGaleria = eval('(' + args + ')');
        if (typeof objGaleria == 'undefined') {
            objGaleria = null;
        }
    }
    if (objGaleria != null) {
        listaImagenes = objGaleria.listaArchivo;
    }
    CargarImagenes();
}

function onclickPlayPause() {
    if ($(".ws_playpause").hasClass("ws_pause")) {
        funDetenerAnimarSlider();
        $('.ws_playpause').addClass('ws_play');
        $('.ws_playpause').removeClass('ws_pause');
    } else {
        funComenzarAnimarSlider();
        $('.ws_playpause').addClass('ws_pause');
        $('.ws_playpause').removeClass('ws_play');
    }
}

function funComenzarAnimarSlider() {
    if (timerSlider) {
        clearInterval(timerSlider);
        timerSlider = null;
    }
    timerSlider = setInterval(AnimarSlider, 5000);
}
function AnimarSlider() {
    onclickMoverSliderObras(1);
}
function funDetenerAnimarSlider() {
    if (timerSlider) {
        clearInterval(timerSlider);
        timerSlider = null;
    }
}

function RedimensionarGaleria() {
    if (document.getElementById('tbGaleria')) {
        var LisTamanio = SizeVentana();
        var pxLeft = parseInt((LisTamanio[0] - parseInt(String($('#tbGaleria').css('width')).replace('px', ''))) / 2);
        document.getElementById('tbGaleria').style.left = String(pxLeft) + 'px';
        document.getElementById('divCerrar').style.left = String(pxLeft) + 'px';
        //        document.getElementById('moverDeUnaObraAtrasar').style.left = String(pxLeft - 50) + 'px';
        //        document.getElementById('moverDeUnaObraAdelante').style.left = String(pxLeft + anchoSliderBarraConObra + 26) + 'px';
    }
}

function CargarImagenes() {
    if (objGaleria != null) {
        if (listaImagenes != null) {

            if (objGaleria.noticia != null) {
                var strHtmlDatos = '  <div class="caption">';
                strHtmlDatos += '<div class="h1Galeria">' + objGaleria.noticia.not_titulo + '</div>';
                strHtmlDatos += '<div class="separador">' ;
                strHtmlDatos += ' <span class="bajada">' + objGaleria.noticia.not_bajada + '</span>';
                strHtmlDatos += '  <span class="descripcion">' + objGaleria.noticia.not_descripcion + '</span>';
                strHtmlDatos += '<span class="destacado">' + objGaleria.noticia.not_destacado + '</span>';
                strHtmlDatos += '</div>';
                strHtmlDatos += '</div>';
                $('#divDatosNotica').html(strHtmlDatos);
             }

            if (listaImagenes.length > 0) {
                indexSeleccionado = 0;
                var strHtml = ' <a class="ws_next"  onclick="PasarAnteriorImagen()"></a> <a class="ws_prev" onclick="PasarSiguienteImagen()"></a><a href="#" class="ws_playpause ws_pause" onclick="onclickPlayPause()"></a>';
                var strHtmlBarra = '';
                strHtml += '<ul style="width:' + (listaImagenes.length * (intAnchoImagenGaleria + intScrollImagenGaleria)) + 'px;">'; //id="ulImagenesPrincipal"
                strHtmlBarra += '<ul class="cssListaBarra" style="width:' + (listaImagenes.length * (intAnchoImagenBarra + intScrollImagenGaleria)) + 'px;">';
                for (var i = 0; i < listaImagenes.length; i++) {
                    strHtml += '<li > <img src="thumbnail.aspx?r=' + ruta + '&extencion=' + listaImagenes[i].arc_tipo + '&n=' + listaImagenes[i].arc_nombre + '&an=' + intAnchoImagenGaleria + '&al=' + intAltoImagenGaleria + '&c=' + colorImagenGaleria + '"  width="' + intAnchoImagenGaleria + 'px" height="' + intAltoImagenGaleria + 'px" alt=""   title="" /> </li>';
                    var varClass = '';
                    if (i == 0) {
                        varClass = ' cuadro_galeriaGaleriaSeleccionado ';
                    }
                    strHtmlBarra += '<li > <img  id="imgPie' + i + '" class="cssImgBarra ' + varClass + '" onclick="MoverSliderObrasPorIndex(' + i + ')" src="thumbnail.aspx?r=' + ruta + '&extencion=' + listaImagenes[i].arc_tipo + '&n=' + listaImagenes[i].arc_nombre + '&an=' + intAnchoImagenBarra + '&al=' + intAltoImagenBarra + '&c=' + colorImagenGaleria + '"  width="' + intAnchoImagenBarra + 'px" height="' + intAltoImagenBarra + 'px" alt=""   title="" /> </li>';
                }
                strHtml += '</ul>' + '<div id="divMostrarPagina"> 1 / ' + listaImagenes.length + '</div>';
                strHtmlBarra += '</ul>';
                $('#divGaleria').html(strHtml);
                $('#barra').html(strHtmlBarra);
                $('#divGaleriaObrasFondo').css('display', 'block');
                $('#tbGaleria').css('display', 'block');
                $('#divCerrar').css('display', 'block');
                if (listaImagenes.length > 1) {
                    funComenzarAnimarSlider();
                }
            }
        }
    } // fin  if (objGaleria != null) {
}

function onclickMoverSliderObras(pValor) {
    var indexProximo = -1;
    if (pValor == 0) {
        if (0 == indexSeleccionado) {
            indexProximo = listaImagenes.length - 1;
        } else {
            indexProximo = indexSeleccionado - 1;
        }
    }
    else if (pValor == 1) {
        if ((listaImagenes.length - 1) <= indexSeleccionado) {
            indexProximo = 0;
        } else {
            indexProximo = indexSeleccionado + 1;
        }
    }
    MoverSliderObrasPorIndex(indexProximo);
    return false;
}

function MoverSliderObrasPorIndex(pValor) {
    var cantLeft = (pValor * (intAnchoImagenGaleria + intScrollImagenGaleria)) - (pValor * 2);
    if (pValor != indexSeleccionado) {
        $('#imgPie' + pValor).addClass('cuadro_galeriaGaleriaSeleccionado');
        $('#imgPie' + indexSeleccionado).removeClass('cuadro_galeriaGaleriaSeleccionado');
        indexSeleccionado = pValor;

        if (listaImagenes != null) {
            $('#divMostrarPagina').html((pValor  + 1 ) + ' / ' + listaImagenes.length);
        }
    }
    document.getElementById('divGaleria').scrollLeft = cantLeft;
}

function PasarSiguienteImagen() {

    onclickMoverSliderObras(0);
}

function PasarAnteriorImagen() {
    onclickMoverSliderObras(1);
}
function onmousemoveImagenPrincipal() {
    $('.ws_next').css('display', 'block');
    $('.ws_prev').css('display', 'block');
    $('.ws_playpause').css('display', 'block');
}
function onmouseoutImagenPrincipal() {
    $('.ws_next').css('display', 'none');
    $('.ws_prev').css('display', 'none');
    $('.ws_playpause').css('display', 'none');
}

function CerrarGaleria() {
    $('#divGaleriaObrasFondo').css('display', 'none');
    $('#tbGaleria').css('display', 'none');
    $('#divCerrar').css('display', 'none');
}
