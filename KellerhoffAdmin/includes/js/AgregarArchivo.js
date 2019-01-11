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
            var strHtml = '<img   src="' + '../../../servicios/thumbnail.aspx?r=' + file.tipo + '&n=' + file.arc_nombre + '&an=' + String(300) + '&al=' + String(300) + '&c=FFFFFF" />';
            $('#divImg').html(strHtml);
            $('#divContenedorImg').css('display', 'block');
        }
    }

});
function onclickVolverAgregarArchivo() {
    switch (file.tipo) {
        case 'ofertas':
            location.href = 'GestionOferta.aspx';
            break;
        case 'slider':
            location.href = 'GestionHomeSlide.aspx';
            break;
        default:
            break;
    }
    return false;
}
