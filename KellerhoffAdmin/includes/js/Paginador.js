var pagActual = -1;
var cantPaginaTotal = -1;
var LimiteDePaginador = 14;

function GenerarPaginador() {

    var strHtmlPag = '';
    $('#divPaginador').html(strHtmlPag);
    if (cantPaginaTotal > 1) {

        // Anterior
        strHtmlPag += '<div class="AnteriorPaginadorExtremo"  onclick="clickAvanceRetrocesoEstremos(0)"><b>' + '|<<' + '</b></div>';
        strHtmlPag += '<div class="PaginadorSepararBarraVertical" > &nbsp; </div>';
        strHtmlPag += '<div class="AnteriorPaginador"  onclick="clickAvanceRetroceso(0)">' + '<' + '</div>';

        var pagDesde = ObtenerPaginaDesde(pagActual, cantPaginaTotal, LimiteDePaginador);
        var pagHasta = ObtenerPaginaHasta(pagDesde, pagActual, cantPaginaTotal, LimiteDePaginador);

        for (var i = pagDesde; i < pagHasta; i++) {
            strHtmlPag += '<div id="pag_' + (i).toString() + '" class="NroPaginador"  onclick="clickPaginador(this)" >' + (i).toString() + '</div>';
            if (i != pagHasta - 1) {
                strHtmlPag += '<div class="SeparadorPaginador" >.</div>';
            }
        }

        // Siguiente
        strHtmlPag += '<div class="SiguientePaginador"  onclick="clickAvanceRetroceso(1)">' + '>' + '</div>';
        strHtmlPag += '<div class="PaginadorSepararBarraVertical" > &nbsp; </div>';
        strHtmlPag += '<div class="SiguientePaginadorExtremo"  onclick="clickAvanceRetrocesoEstremos(1)"><b>' + '>>|' + '</b></div>';
    }
    if (strHtmlPag != '') {
        $('#divPaginador').html(strHtmlPag);

        $('#pag_' + pagActual).addClass("NroPaginadorSeleccionado");

        if (pagActual > 1 && pagActual < cantPaginaTotal) {
            $('.SiguientePaginador').css('visibility', 'visible');
            $('.AnteriorPaginador').css('visibility', 'visible');
        } else if (pagActual == 1) {
            $('.AnteriorPaginador').css('visibility', 'hidden');
            $('.SiguientePaginador').css('visibility', 'visible');
        } else if (pagActual == cantPaginaTotal) {
            $('.SiguientePaginador').css('visibility', 'hidden');
            $('.AnteriorPaginador').css('visibility', 'visible');
        }
    }
}

function clickPaginador(pValor) {
    pagActual = pValor.innerHTML;
    LlamarMetodoCargar(pagActual);
}

function clickAvanceRetrocesoEstremos(pValor) {
    if (pValor == 1) {
        // Final paginacior
        pagActual = cantPaginaTotal;
        LlamarMetodoCargar(pagActual);

    } else if (pValor == 0) {
        // Principio paginacior
        pagActual = 1;
        LlamarMetodoCargar(pagActual);
    }
}

function clickAvanceRetroceso(pValor) {
    if (pValor == 1) {
        // siguiente
        if (cantPaginaTotal > pagActual) {
            $('#pag_' + pagActual).removeClass("NroPaginadorSeleccionado");
            pagActual++;
            $('#pag_' + pagActual).addClass("NroPaginadorSeleccionado");
            LlamarMetodoCargar(pagActual);
        }
    } else if (pValor == 0) {
        // anterior
        if (pagActual > 1) {
            $('#pag_' + pagActual).removeClass("NroPaginadorSeleccionado");
            pagActual--;
            $('#pag_' + pagActual).addClass("NroPaginadorSeleccionado");

            LlamarMetodoCargar(pagActual);
        }

    }
}
function ObtenerPaginaDesde(pPagActual, pCantidadPagina, pLimiteDePagina) {


    if (pCantidadPagina > pLimiteDePagina) {
        var Mitad = (pLimiteDePagina / 2);
        if (pPagActual > Mitad) {
            var resultadoAux1 = pPagActual - Mitad;
            if ((resultadoAux1 + pLimiteDePagina) >= pCantidadPagina) {
                var resultadoAux2 = pCantidadPagina - (pLimiteDePagina - 1);
                if (resultadoAux2 == 0) {
                    return 1;
                } else {
                    return resultadoAux2;
                }
            } else {
                return resultadoAux1;
            }
        } else {
            return 1;
        }
    } else {
        return 1;
    }
}
function ObtenerPaginaHasta(pPagDesde, pPagActual, pCantidadPagina, pLimiteDePagina) {

    if (pCantidadPagina >= pLimiteDePagina) {
        return pPagDesde + pLimiteDePagina;
    } else {
        return parseInt( pCantidadPagina) + 1; 
    }
}
