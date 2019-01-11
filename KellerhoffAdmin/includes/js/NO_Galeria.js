var intAnchoImagenGaleriaObra = 600;
var intAltoImagenGaleriaObra = 500;
var intAnchoImagenObrasBarra = 50;
var intAltoImagenObrasBarra = 50;
var objObra = null;
var observacion = '';
var descripcion = '';
var isClickImagen = false;
var isZoom = true;
var isCambiarDeImagen = false;
var isRecienSeAbreGaleria = true;
var isClickMegusta = false;
var isNotImgCompletaZoomSrcVacio = true;
var listaObrasGaleriaDeAbajo = [];
var indexSeleccionado = -1;
// Slider
var anchoSliderBarraConObra = 880;
var anchoSliderImagenObra = 61;
var estiloBordeObraSeleccionada = '#31B404 2px solid';
var timerSlider = null;
var direccionActual = 0;
var speedSlider = 0;
// fin Slider
var nombreActualImagen = '';
var imagenDeObraActual = null;
//
//var intAnchoGaleria = 880;
var intAltoGaleriaObra = 500;
var isCargandoObra = false;
//
jQuery(document).ready(function () {
    // alert('OK');

    window.onresize = function (event) {
        RedimensionarGaleria();
    }

    $('#divImagenObra').mouseover(function () {
        //$('#divFacebookMeGusta').css('display', 'block');
        $('#imgLupaEnImagen').css('display', 'block');
        $('#imgReproductorEnImagen').css('display', 'block');
    }).mouseout(function () {
        //$('#divFacebookMeGusta').css('display', 'none');
        $('#imgLupaEnImagen').css('display', 'none');
        $('#imgReproductorEnImagen').css('display', 'none');
    });

    RedimensionarGaleria();
});

function RedimensionarGaleria() {
    if (document.getElementById('idTablaGaleriaObras')) {
        var LisTamanio = SizeVentana();
        var pxLeft = parseInt((LisTamanio[0] - parseInt(String($('#idTablaGaleriaObras').css('width')).replace('px', ''))) / 2);
        document.getElementById('idTablaGaleriaObras').style.left = String(pxLeft) + 'px';
        document.getElementById('moverDeUnaObraAtrasar').style.left = String(pxLeft - 50) + 'px';
        document.getElementById('moverDeUnaObraAdelante').style.left = String(pxLeft + anchoSliderBarraConObra + 26) + 'px';
    }
}

function onclickMoverDeAUno(pValor) {
    isClickImagen = true;
    if (!isCargandoObra) {
        detenerReproductor();
        if (listaObrasGaleriaDeAbajo.lista.length > 1) {
            var indexProximo = indexSeleccionado + pValor;
            if (listaObrasGaleriaDeAbajo.lista.length - 1 < indexProximo) {
                indexProximo = 0;
            } else if (indexProximo == -1) {
                indexProximo = listaObrasGaleriaDeAbajo.lista.length - 1;
            }
            if (listaObrasGaleriaDeAbajo.lista[indexProximo].id != listaObrasGaleriaDeAbajo.lista[indexSeleccionado].id) {
                $('#obrGaleria_' + String(listaObrasGaleriaDeAbajo.lista[indexProximo].id)).addClass('cuadro_galeriaGaleriaSeleccionado');
                $('#obrGaleria_' + String(listaObrasGaleriaDeAbajo.lista[indexSeleccionado].id)).removeClass('cuadro_galeriaGaleriaSeleccionado');
                LlamarMetodoRecuperar(listaObrasGaleriaDeAbajo.lista[indexProximo].id);
            }
            indexSeleccionado = indexProximo;
        }
    }
    //    if (pValor == -1) {
    //    } else if (pValor == 1) { 
    //     
    //     }
}
function RestablecerPropiedades() {
    isNotImgCompletaZoomSrcVacio = false;
    $('#imgCompletaEnZoom').attr('src', '');
    $('#imgCompletaEnZoom').css('visibility', 'hidden');
    $('#divImagenCompleta').css('display', 'none');
    $('#idTablaGaleriaObras').css('display', 'block');
    document.getElementById('moverDeUnaObraAtrasar').style.display = 'block';
    document.getElementById('moverDeUnaObraAdelante').style.display = 'block';
    detenerReproductor();
}
function onclickBotonCerrarObra() {
    isCambiarDeImagen = false;
    RestablecerPropiedades();
    $('#divGaleriaObrasFondo').css('display', 'none');
}
function onClickGaleriaObrasCerrar() {
    if (!isClickImagen) {
        isCambiarDeImagen = false;
        RestablecerPropiedades();
        $('#divGaleriaObrasFondo').css('display', 'none');
    } else {
        isClickImagen = false;
    }
}
function onClickTablaObras() {
    isClickImagen = true;
}

function LlamarMetodoRecuperar(pValor) {
    // alert('OK 1');
    isCargandoObra = true;
    var LisTamanio = SizeVentana();
    PageMethods.RecuperarObraPorId(pValor, LisTamanio[0] - 300, LisTamanio[1] - 160, OnCallBackRecuperarObraPorId, OnFail);
}
function CargarObraGaleria(pValor) {
    isRecienSeAbreGaleria = true;
    LlamarMetodoRecuperar(pValor);
    return false;
}
function OnCallBackRecuperarObraPorId(args) {
    // alert('OK 2');
    if (isNotNullEmpty(args)) {
        //alert('OK 3');
        CargarObra(args);
    } else {
        $('#divGaleriaObrasFondo').css('display', 'none');
    }
}
function srcObtenerObra() {
    //    return '../thumbnailNew.aspx?r=' + constImagenObra + '&n=' + objObra.NombreImagen + '&an=' + String(intAnchoImagenGaleriaObra) + '&al=' + String(intAltoImagenGaleriaObra) + '&re=1&c=000000';
    return '../thumbnailNew.aspx?r=' + constImagenObra + '&n=' + nombreActualImagen + '&an=' + String(intAnchoImagenGaleriaObra) + '&al=' + String(intAltoImagenGaleriaObra) + '&re=1&c=000000';
}

function srcObtenerObraNoDisponible() {
    return '../thumbnailNew.aspx?r=' + constImagenObra + '&n=no_disponible.jpg&an=' + String(intAnchoImagenGaleriaObra) + '&al=' + String(intAltoImagenGaleriaObra) + '&re=1&c=000000';
}

function onClickNoZoomMeGusta() {
    isClickMegusta = true;
}
function CargarObra(pValor) {
    objObra = pValor;
    //    if (isRecienSeAbreGaleria) {
    //    } else {
    //    }
    //    alert('OK 1');
    nombreActualImagen = objObra.NombreImagen;
    $('#frameFacebook').attr('src', '//www.facebook.com/plugins/like.php?href=www.artefe.org.ar/Paginas/Obra.aspx?id=' + objObra.id + '&amp;send=false&amp;layout=button_count&amp;width=450&amp;show_faces=false&amp;action=like&amp;colorscheme=light&amp;font=arial&amp;height=24');
    $('#imgTwitter').bind('click', function () {
        window.open('http://twitter.com/home?status=www.artefe.org.ar/Paginas/Obra.aspx?id=' + objObra.id, 'social_win', 'width=626, height=436');
        return false;
    });
    $('#imgFacebook').bind('click', function () {
        window.open('http://www.facebook.com/sharer.php?u=www.artefe.org.ar/Paginas/Obra.aspx?id=' + objObra.id, 'social_win', 'width=686, height=436');
        return false;
    });
    if (isNotNullEmpty(nombreActualImagen)) {
        //        $('#imagenPrincipal').bind('error', function () {
        //            onerrorImagenPrincipal(this);
        //        });
        $('#imagenPrincipal')[0].onerror = function () { onerrorImagenPrincipal($('#imagenPrincipal')[0]); };
        $('#imagenPrincipal').attr('alt', objObra.tit_obra);
        $('#imagenPrincipal').attr('src', srcObtenerObra());
    } else {
        //        $('#imagenPrincipal').unbind('error');
        $('#imagenPrincipal')[0].onerror = null;
        $('#imagenPrincipal').attr('alt', objObra.tit_obra);
        $('#imagenPrincipal').attr('src', srcObtenerObraNoDisponible());
    }
    //    alert('OK 2');
    PageMethods.RecuperarObservacionObra(RecuperarObservacionObraOnCallBack, OnFail);
}
var isPlay = true;
var timerReproductor = null;
function onclickReproductor() {
    isClickMegusta = true;
    if (isPlay) {
        //
        if (timerReproductor) {
            clearInterval(timerReproductor);
            timerReproductor = null;
        }
        timerReproductor = setInterval(AnimarReproductor, 5000);
        //
        $('#imgReproductorEnImagen').attr('src', '../Imagenes/Obra/pause.png');
        $('#imgReproductorEnImagen').attr('title', 'Pausa');
        isPlay = false;
    } else {
        //
        detenerReproductor();
        //

    }
}
function ReproductorPlay() {
    if (listaObrasGaleriaDeAbajo.lista.length > 1) {
        var indexProximo = -1;
        if (listaObrasGaleriaDeAbajo.lista.length - 1 <= indexSeleccionado) {
            indexProximo = 0;
        } else {
            indexProximo = indexSeleccionado + 1;
        }
        if (listaObrasGaleriaDeAbajo.lista[indexProximo].id != listaObrasGaleriaDeAbajo.lista[indexSeleccionado].id) {
            $('#obrGaleria_' + String(listaObrasGaleriaDeAbajo.lista[indexProximo].id)).addClass('cuadro_galeriaGaleriaSeleccionado');
            $('#obrGaleria_' + String(listaObrasGaleriaDeAbajo.lista[indexSeleccionado].id)).removeClass('cuadro_galeriaGaleriaSeleccionado');
            LlamarMetodoRecuperar(listaObrasGaleriaDeAbajo.lista[indexProximo].id);
        }
        indexSeleccionado = indexProximo;
    }
}



///////////////
//if (timerReproductor) {
//    clearInterval(timerReproductor);
//    timerReproductor = null;
//    }
//timerReproductor = setInterval(AnimarReproductor, 500);


function detenerReproductor() {
    if (timerReproductor) {
        clearInterval(timerReproductor);
        timerReproductor = null;
    }
    $('#imgReproductorEnImagen').attr('src', '../Imagenes/Obra/play.png');
    $('#imgReproductorEnImagen').attr('title', 'Play');
    isPlay = true;
}

function AnimarReproductor() {
    ReproductorPlay();
}
/////////////////

function onclickZoomImagen() {
    if (!isClickMegusta) {
        detenerReproductor();
        isClickImagen = true;
        if (isZoom) {
            $('#divImagenCompleta').css('display', 'block');
            $('#idTablaGaleriaObras').css('display', 'none');
            document.getElementById('moverDeUnaObraAtrasar').style.display = 'none';
            document.getElementById('moverDeUnaObraAdelante').style.display = 'none';
            $('#imgCompletaEnZoom').attr('alt', objObra.tit_obra);
            if (isNotNullEmpty(nombreActualImagen)) {
                $('#imgCompletaEnZoom').attr('src', '../Archivos/' + constImagenObra + '/' + nombreActualImagen);
            } else {
                $('#imgCompletaEnZoom').attr('src', '../Archivos/' + constImagenObra + '/no_disponible.jpg');
            }
            isZoom = false;
        } else {
            RestablecerPropiedades();
            isZoom = true;
        }
    } else {
        isClickMegusta = false;
    }
}
function RecuperarObservacionObraOnCallBack(args) {
    observacion = args;
    PageMethods.RecuperarDescripcionObra(RecuperarDescripcionObraOnCallBack, OnFail);
}
function RecuperarDescripcionObraOnCallBack(args) {
    descripcion = args;
    if (objObra != null) {
        CargarDatosObras(objObra);
    }
}

function CargarDatosObras(pValor) {
    //    alert('OK 3');
    $('#lblTitulo').html(pValor.tit_obra);
    var strHtml = '';
    strHtml += '<table border="0" >';
    strHtml += '<tr>';
    strHtml += '</tr>';

    if (pValor.isActivoArtista) {
        strHtml += '<tr>';
        strHtml += '<td valign="top" class="txt_Arial_g3_11">';
        strHtml += '<b class="txt_Arial_g3_12 nowrap" >Artista </b><br/>';
        strHtml += '<a href="Artistas.aspx?id=' + pValor.id_artista_obra + '&t=g&i=' + pValor.id + '"  style="text-decoration:none; " ><span class="linkGaleriaObra" >' + pValor.NombreArtista + '</span></a>';
        strHtml += '</td>';
        strHtml += '</tr>';
    }
    if (isNotNullEmpty(pValor.str_disciplina_obra)) {
        strHtml += '<tr>';
        strHtml += '<td valign="top" class="txt_Arial_g3_11">';
        strHtml += '<b  class="txt_Arial_g3_12 nowrap">Disciplina </b><br/>';
        strHtml += pValor.str_disciplina_obra;
        strHtml += '</td>';
        strHtml += '</tr>';
    }
    if (isNotNullEmpty(pValor.str_tecnica_obra)) {
        strHtml += '<tr>';
        strHtml += '<td valign="top" class="txt_Arial_g3_11">';
        strHtml += '<b class="txt_Arial_g3_12 nowrap">Técnica </b><br/>';
        strHtml += pValor.str_tecnica_obra;
        strHtml += '</td>';
        strHtml += '</tr>';
    }
    if (isNotNullEmpty(pValor.str_soporte_obra)) {
        strHtml += '<tr>';
        strHtml += '<td valign="top" class="txt_Arial_g3_11">';
        strHtml += '<b class="txt_Arial_g3_12 nowrap">Soporte </b><br/>';
        strHtml += pValor.str_soporte_obra;
        strHtml += '</label>';
        strHtml += '</td>';
        strHtml += '</tr>';
    }
    if (isNotNullEmpty(pValor.año_obra)) {
        strHtml += '<tr>';
        strHtml += '<td valign="top" class="txt_Arial_g3_11">';
        strHtml += '<b  class="txt_Arial_g3_12 nowrap">Año </b><br/>';
        strHtml += pValor.año_obra;
        strHtml += '</td>';
        strHtml += '</tr>';
    }
    if (isNotNullEmpty(pValor.medidas_obra)) {
        strHtml += '<tr>';
        strHtml += '<td valign="top" class="txt_Arial_g3_11">';
        strHtml += '<b  class="txt_Arial_g3_12 nowrap">Medidas obras </b><br/>';
        strHtml += pValor.medidas_obra;
        strHtml += '</td>';
        strHtml += '</tr>';
    }
    if (pValor.isActivoMuseo) {
        strHtml += '<tr>';
        strHtml += '<td valign="top" class="txt_Arial_g3_11">';
        strHtml += '<b  class="txt_Arial_g3_12 nowrap">Museo </b><br/>';
        strHtml += '<a href="Museo.aspx?id=' + pValor.id_museo_obra + '&t=g&i=' + pValor.id + '" style="text-decoration:none; " ><span class="linkGaleriaObra" >' + pValor.NombreMuseo + '</span></a>';
        strHtml += '</td>';
        strHtml += '</tr>';
    }
    // Salas
    if (isNotNullEmpty(pValor.tsm_nombre)) {
        strHtml += '<tr>';
        strHtml += '<td valign="top" class="txt_Arial_g3_11">';
        strHtml += '<b class="txt_Arial_g3_12 nowrap" >Sala </b><br/>';
        strHtml += '<a href="SalaDeMuseo.aspx?id=' + pValor.tsm_id + '&o=' + pValor.id + '"  style="text-decoration:none; " ><span class="linkGaleriaObra" >' + pValor.tsm_nombre + '</span></a>';
        strHtml += '</td>';
        strHtml += '</tr>';
    }

    // fin sala

    if (isNotNullEmpty(descripcion)) {
        strHtml += '<tr>';
        strHtml += '<td valign="top" class="txt_Arial_g3_11">';
        strHtml += '<b class="txt_Arial_g3_12 nowrap">Descripción </b><br/>';
        strHtml += descripcion;
        strHtml += '</td>';
        strHtml += '</tr>';
    }
    if (isNotNullEmpty(observacion)) {
        strHtml += '<tr>';
        strHtml += '<td valign="top" class="txt_Arial_g3_11">';
        strHtml += '<b class="txt_Arial_g3_12 nowrap">Observación </b><br/>';
        strHtml += observacion;
        strHtml += '</td>';
        strHtml += '</tr>';
    }
    strHtml += '&nbsp;';
    if (isNotNullEmpty(pValor.FechaDesdeRestauracion)) {
        strHtml += '<tr>';
        strHtml += '<td valign="top" class="txt_Arial_g3_11">';
        strHtml += '<b class="txt_Arial_g3_12 nowrap"><u class="txt_Arial_g3_12 nowrap" >Desde</u>: </b><br/>';
        strHtml += pValor.FechaDesdeRestauracion;
        strHtml += '</td>';
        strHtml += '</tr>';
    }
    if (isNotNullEmpty(pValor.FechaHastaRestauracion)) {
        strHtml += '<tr>';
        strHtml += '<td valign="top" class="txt_Arial_g3_11">';
        strHtml += '<b class="txt_Arial_g3_12 nowrap"><u  class="txt_Arial_g3_12 nowrap">Hasta</u>:</b><br/>';
        strHtml += pValor.FechaHastaRestauracion;
        strHtml += '</td>';
        strHtml += '</tr>';
    }
    if (pValor.ListaArchivoPDF.length > 0) {
        strHtml += '<tr>';
        strHtml += '<td valign="top" class="txt_Arial_g3_11">';
        strHtml += '<b class="nowrap">Archivos: </b>';
        for (var iPdf = 0; iPdf < pValor.ListaArchivoPDF.length; iPdf++) {
            strHtml += '<div class="divLinkPDF"><a id="a_' + pValor.ListaArchivoPDF[iPdf].id + '" style="text-decoration:none;" target="_blank" href="../../Archivos/' + constImagenObra + '/' + pValor.ListaArchivoPDF[iPdf].nombre + '" ><span  class="cssSpanPDF" >' + pValor.ListaArchivoPDF[iPdf].nombre + '</span></a></div>';
        }
        strHtml += '</td>';
        strHtml += '</tr>';
    }
    if (pValor.listaImagenesObra.length > 1) {

        //        listaImagenesObra = pValor.listaImagenesObra;
        strHtml += '<tr>';
        strHtml += '<td valign="top" class="txt_Arial_g3_11">';
        strHtml += '<b class="txt_Arial_g3_12 nowrap">Imagenes de la obra </b><br/>';
        //
        strHtml += '<div style="width: 250px;overflow: hidden;overflow-x: auto;">';
        var anchoUl = ((((pValor.listaImagenesObra.length - 1) * anchoSliderImagenObra) + 71) + 'px');
        strHtml += ' <ul style="width:' + anchoUl + '">';
        for (var i = 0; i < pValor.listaImagenesObra.length; i++) {
            strHtml += '<li id="obrImagenesGaleria_' + i + '" class="cuadro_galeriaGaleria"  onclick="onclickCargarImagen(this)" style="overflow:hidden;width:' + String(intAnchoImagenObrasBarra) + 'px; height:' + String(intAltoImagenObrasBarra) + 'px;"><img   alt="' + pValor.tit_obra + '"  title="' + pValor.tit_obra + '" class="img_galeriaGaleria"   src = "' + '../thumbnailNew.aspx?r=' + constImagenObra + '&n=' + pValor.listaImagenesObra[i] + '&an=' + String(intAnchoImagenObrasBarra) + '&al=' + String(intAltoImagenObrasBarra) + '&c=EBEBEB' + '"   /></li>';
        }
        strHtml += ' </ul>';
        strHtml += '</div>';

        //

        strHtml += '</td>';
        strHtml += '</tr>';
    }

    strHtml += '</td>';
    strHtml += '</tr>';
    strHtml += '</table>';

    $('#datosObra').html(strHtml);

    //    alert('OK 4');

    //
    if (objObra.listaImagenesObra.length > 1) {
        imagenDeObraActual = 'obrImagenesGaleria_0';
        $('#' + imagenDeObraActual).addClass('cuadro_galeriaGaleriaSeleccionado');
    }
    //
    //    RedimencionarParteDatosObra();
    if (isRecienSeAbreGaleria) {
        RedimencionarParteDatosObra();
        if (pValor.id_artista_obra == -1) {
            if (pValor.id_museo_obra != -1) {
                PageMethods.RecuperarObrasPorMuseoParaBarraGaleria(pValor.id_museo_obra, OnCallBackRecuperarObrasParaBarraGaleria, OnFail);
                $('#bTituloBarraAbajo').html('Otras obras del museo');
            }
        } else {
            PageMethods.RecuperarObrasParaBarraGaleria(pValor.id_artista_obra, pValor.id, OnCallBackRecuperarObrasParaBarraGaleria, OnFail);
            $('#bTituloBarraAbajo').html('Otras obras del artista');
        }
        isRecienSeAbreGaleria = false;
    } else {
        for (var i = 0; i < listaObrasGaleriaDeAbajo.lista.length; i++) {
            if (pValor.id == listaObrasGaleriaDeAbajo.lista[i].id) {
                indexSeleccionado = i;
                break;
            }
        }
        RedimencionarParteDatosObra();
        isCargandoObra = false;
    }
    //    RedimencionarParteDatosObra();
    //    setTimeout(RedimencionarParteDatosObra(), 1000);
}
function onclickCargarImagen(args) {

    nombreActualImagen = objObra.listaImagenesObra[parseInt(args.id.replace('obrImagenesGaleria_', ''))];

    //    objObra.nombreArchivo = nombre;
    if (isNotNullEmpty(nombreActualImagen)) {
        $('#imagenPrincipal').attr('src', srcObtenerObra());
    } else {
        $('#imagenPrincipal').attr('src', srcObtenerObraNoDisponible());
    }


    ///
    if (imagenDeObraActual != null) {
        $('#' + imagenDeObraActual).removeClass('cuadro_galeriaGaleriaSeleccionado');
    }
    imagenDeObraActual = args.id;
    $('#' + imagenDeObraActual).addClass('cuadro_galeriaGaleriaSeleccionado');

}
function RedimencionarParteDatosObra() {
    intAltoGaleriaObra = ConvertMedidaCssToNro($('#tdDatosObraGaleria').css('height'));
    //    intAltoGaleriaObra = ConvertMedidaCssToNro($('#divImagenObra').css('height'));
    var altoTitulo = ConvertMedidaCssToNro($('#lblTitulo').css('height'));
    var tempAlto = intAltoGaleriaObra - altoTitulo;
    if (altoTitulo > 24) {
        tempAlto -= (altoTitulo - 24);
    }
    $('#contenedorDatosObra').css('height', tempAlto + 'px');
}
function OnCallBackRecuperarObrasParaBarraGaleria(args) {
    //    alert('OK');
    listaObrasGaleriaDeAbajo = eval('(' + args + ')');
    //    alert('OK    1');
    jQuery('#mycarousel').html('');

    var StrHtmlLI = '';
    for (var i = 0; i < listaObrasGaleriaDeAbajo.lista.length; i++) {
        StrHtmlLI += mycarousel_getItemHTML(listaObrasGaleriaDeAbajo.lista[i]);
        if (objObra.id == listaObrasGaleriaDeAbajo.lista[i].id) {
            indexSeleccionado = i;
        }
    }
    jQuery('#mycarousel').html(StrHtmlLI);
    if (listaObrasGaleriaDeAbajo.lista.length > 0) {
        $('#mycarousel').css('width', (((listaObrasGaleriaDeAbajo.lista.length - 1) * anchoSliderImagenObra) + 71) + 'px');
        $('#tituloOtraDeArtista').css('display', 'block');
    }
    else {
        $('#tituloOtraDeArtista').css('display', 'none');
    }
    if (ConvertMedidaCssToNro($('#mycarousel').css('width')) < anchoSliderBarraConObra) {
        $('#btnSliderAtras').css('display', 'none');
        $('#btnSliderSiguiente').css('display', 'none');
    } else {
        $('#btnSliderAtras').css('display', 'block');
        $('#btnSliderSiguiente').css('display', 'block');
    }
    //    $('#obrGaleria_' + String(objObra.id)).css('border', estiloBordeObraSeleccionada);
    $('#obrGaleria_' + String(objObra.id)).addClass('cuadro_galeriaGaleriaSeleccionado');
    if (!isCambiarDeImagen) {
        $('#divGaleriaObrasFondo').css('display', 'block');
    }
    RedimencionarParteDatosObra();
    isCargandoObra = false;
}
function mycarousel_getItemHTML(pValor) {
    var strHtml = '';
    if (isNotNullEmpty(pValor.nombreImagen)) {
        strHtml += '<li id="obrGaleria_' + pValor.id + '" class="cuadro_galeriaGaleria"  onclick="onclickLlevarPagObra(this)" style="overflow:hidden;width:' + String(intAnchoImagenObrasBarra) + 'px; height:' + String(intAltoImagenObrasBarra) + 'px;"><img   alt="' + pValor.nombre + '"  title="' + pValor.nombre + '" class="img_galeriaGaleria"   src = "' + '../thumbnailNew.aspx?r=' + constImagenObra + '&n=' + pValor.nombreImagen + '&an=' + String(intAnchoImagenObrasBarra) + '&al=' + String(intAltoImagenObrasBarra) + '&c=EBEBEB' + '" onerror="onerrorObraBarraGaleria(this)"  /></li>';
    } else {
        strHtml += '<li id="obrGaleria_' + pValor.id + '" class="cuadro_galeriaGaleria"    onclick="onclickLlevarPagObra(this)" style="overflow:hidden;width:' + String(intAnchoImagenObrasBarra) + 'px; height:' + String(intAltoImagenObrasBarra) + 'px;"><img   alt="' + pValor.nombre + '"  title="' + pValor.nombre + '" class="img_galeriaGaleria"   src = "' + '../thumbnailNew.aspx?r=' + constImagenObra + '&n=no_disponible.jpg&an=' + String(intAnchoImagenObrasBarra) + '&al=' + String(intAltoImagenObrasBarra) + '&c=EBEBEB' + '"  /></li>';
    }
    return strHtml;
};
var idObraAnterior = -1;
function onclickLlevarPagObra(pValor) {
    isClickImagen = true;
    isCambiarDeImagen = true;
    if (!isCargandoObra) {
        detenerReproductor();
        $('#' + String(pValor.id)).addClass('cuadro_galeriaGaleriaSeleccionado');
        idObraAnterior = pValor.id;
        if (pValor.id.replace('obrGaleria_', '') != String(objObra.id)) {
            $('#obrGaleria_' + String(objObra.id)).removeClass('cuadro_galeriaGaleriaSeleccionado');
        }
        LlamarMetodoRecuperar(pValor.id.replace('obrGaleria_', ''));
    }
}
function onLoadAmpliarFondo() {

    $('#divGaleriaObrasFondo').css('height', $(document).height());
    $('#divGaleriaObrasFondo').css('width', $(document).width());

    //    $('#obrGaleria_' + String(objObra.id)).css('border', estiloBordeObraSeleccionada);
    //    if (idObraAnterior != -1) {
    //        if (String(idObraAnterior) != String(objObra.id)) {
    //            $('#obrGaleria_' + String(idObraAnterior)).css('border', '#FFFFFF 1px solid');
    //        }
    //    }
}
function onLoadImagenCompletaEnZoom() {
    window.scrollTo(0, 0);
    $('#divGaleriaObrasFondo').css('height', $(document).height());
    $('#imgCompletaEnZoom').css('visibility', 'visible');
}


/* Inicio Slider */
function onclickMoverSliderObras(pValor) {
    var cantLeft = document.getElementById('pagImagines').scrollLeft;
    if (pValor == 0) {
        // Retroceder
        cantLeft += -(anchoSliderImagenObra * 3);
    }
    else if (pValor == 1) {
        // Avanzar
        cantLeft += (anchoSliderImagenObra * 3);
    }
    document.getElementById('pagImagines').scrollLeft = cantLeft;
    return false;
}


function onmouseoverComenzar(pDir) {
    speedSlider = 2;
    direccionActual = pDir;
    if (timerSlider) {
        clearInterval(timerSlider);
        timerSlider = null;
    }
    timerSlider = setInterval(AnimarSlider, 10);
}

function onmouseoutDetener() {
    if (timerSlider) {
        clearInterval(timerSlider);
        timerSlider = null;
    }
}

function AnimarSlider() {
    var cantLeft = document.getElementById('pagImagines').scrollLeft;
    cantLeft += (speedSlider * direccionActual);
    document.getElementById('pagImagines').scrollLeft = cantLeft;
}

function onmousedownAcelerar() {
    speedSlider = 10;
}

function onmouseupFrenar() {
    speedSlider = 2;
}

/* Fin Slider */

/* Inicio error cargar imagen */
function onerrorImagenCompletaEnZoom(pValor) {
    if (isNotImgCompletaZoomSrcVacio) {
        pValor.src = '../Archivos/' + constImagenObra + '/' + constImagenErrorObra;
    } else {
        isNotImgCompletaZoomSrcVacio = true;
    }
}

function onerrorImagenPrincipal(pValor) {
    pValor.src = '../thumbnailNew.aspx?r=' + constImagenObra + '&n=' + constImagenErrorObra + '&an=' + String(intAnchoImagenGaleriaObra) + '&al=' + String(intAltoImagenGaleriaObra) + '&re=1&c=000000';
}
function onerrorObraBarraGaleria(pValor) {
    pValor.src = '../thumbnailNew.aspx?r=' + constImagenObra + '&n=' + constImagenErrorObra + '&an=' + String(intAnchoImagenObrasBarra) + '&al=' + String(intAltoImagenObrasBarra) + '&c=EBEBEB';
}
/* Fin error cargar imagen */