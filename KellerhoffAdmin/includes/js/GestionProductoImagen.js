function bus() {
    var varPalabraBuscador = $('#txtBuscar').val();
    
    PageMethods.RecuperarProductos(varPalabraBuscador, OnCallBackRecuperarProductos, OnFail);
    return false;
}



function EliminarImagenProducto(pValor) {
    PageMethods.EliminarImagenProducto(pValor, OnCallBackEliminarImagenProducto, OnFail);
    return false;
}
function OnCallBackEliminarImagenProducto(args) {
    bus();
}


//function onkeypressEnter(e, elemento) {
//    tecla = (document.all) ? e.keyCode : e.which;
//    if (tecla == 13) {
//        bus();
//    }
//}
function OnCallBackRecuperarProductos(args) {
    if (args) {

        args = eval('(' + args + ')');
        if (args.length > 0) {
            var strHtml = '';
            strHtml += '<div class="table-responsive">';
            strHtml += '<table class="table table-striped">';
            strHtml += '<thead>';
            strHtml += '<tr>';
            strHtml += '<th>Número</th>';
            strHtml += '<th>Nombre</th>';
            strHtml += '<th>Archivo</th>';
            //strHtml += '<th>Modi</th>';
            //strHtml += '<th>Eli</th>';
            strHtml += '</tr>';
            strHtml += '</thead>';
            strHtml += '<tbody>';
            for (var i = 0; i < args.length; i++) {
                strHtml += '<tr>';
                strHtml += '<td>' + args[i].pro_codigo + '</td>';
                strHtml += '<td>' + args[i].pro_nombre + '</td>';
                if (args[i].pri_nombreArchivo == null)
                {
                    strHtml += '<td>' + '<button type="button" class="btn btn-info" onclick="return AgregarImagen(\'' + args[i].pro_codigo + '\');">Agregar</button>' + '</td>';
                } else {
                    //strHtml += '<td>' + args[i].pri_nombreArchivo + '</td>';

                    strHtml += '<td>' + '<img   src="' + '../../../servicios/thumbnail.aspx?r=' + 'productos' + '&n=' + args[i].pri_nombreArchivo + '&an=' + String(250) + '&al=' + String(250) + '"/>' + '&nbsp;' + '&nbsp;';
                    strHtml += '<button type="button" class="btn btn-warning" onclick="return AgregarImagen(\'' + args[i].pro_codigo + '\');">Modificar</button>' + '&nbsp;' + '&nbsp;';
                    strHtml += '<button type="button" class="btn btn-danger" onclick="return EliminarImagenProducto(\'' + args[i].pro_codigo + '\');">Eliminar</button>';
                    strHtml += '</td>';

                }
                strHtml += '</tr>';
            }
            strHtml += '</tbody>';
            strHtml += '</table>';
            strHtml += '</div>';
            $('#divContenedorGrilla').html(strHtml);
        }
        else {
            $('#divContenedorGrilla').html('');
        }
    }
}
function AgregarImagen(pValor) {
    location.href = 'GestionProductoImagenAgregar.aspx?Numero=' + pValor;
    return false;
}
function onclickVolverProductoImagen() {
    location.href = 'GestionProductoImagen.aspx?text=' + $('#hiddenText').val();
    return false; 
}