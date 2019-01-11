var file = null;
jQuery(document).ready(function () {
    file = eval('(' + $('#hiddenFile').val() + ')');
    if (typeof file == 'undefined') {
        file = null;
    }
    if (file != null) {
        $('#txt_titulo').val(file.titulo);
        $('#txt_descr').val(file.descr);

        if (isNotNullEmpty(file.arc_nombre)) {            
            var strHtml = '<a href="../../servicios/descargarArchivo.aspx?t=' + file.tipo + '&n=' + file.arc_nombre + '">' + file.arc_nombre + '</a>';
            if (file.tipo == 'ofertaspdf') {
                strHtml += '&nbsp;&nbsp;&nbsp;<button type="button" class="btn btn-danger" onclick="EliminarArchivoPorId(' + file.codRecurso + '); return false;">Eliminar</button>';
            }

            $('#divArchivoGenerico').html(strHtml);
            $('#divContenedorArchivoGenerico').css('display', 'block');
        }
    }

});

function EliminarArchivoPorId(pValue) {
    PageMethods.EliminarArchivoPorId(pValue, OnCallBackEliminarArchivoPorId, OnFail);
}
function OnCallBackEliminarArchivoPorId(args) {
    //$('#divArchivoGenerico').html('');
    onclickVolverAgregarArchivo();
}
function onclickVolverAgregarArchivo() {
    switch (file.tipo) {
        case 'ofertaspdf':
            location.href = 'GestionOferta.aspx';
            break;
        case 'popup':
            location.href = 'GestionPopUp.aspx';
            break;
        default:
            break;
    }
    return false;
}
