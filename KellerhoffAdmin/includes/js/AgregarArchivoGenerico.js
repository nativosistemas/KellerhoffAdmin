var file = null;
var listaTodasSucursales = null;
var listaSucursalesSeleccionada = null;

jQuery(document).ready(function () {
    listaTodasSucursales = eval('(' + $('#hiddenTodasSucursales').val() + ')');
    if (typeof listaTodasSucursales == 'undefined') {
        listaTodasSucursales = null;
    }

    file = eval('(' + $('#hiddenFile').val() + ')');
    if (typeof file == 'undefined') {
        file = null;
    }

    if (file != null) {
        $('#txt_titulo').val(file.titulo);
        //$('#txt_descr').val(file.descr);

        if (isNotNullEmpty(file.arc_nombre)) {
            var strHtml = '<a href="../../servicios/descargarArchivo.aspx?t=' + file.tipo + '&n=' + file.arc_nombre + '">' + file.arc_nombre + '</a>';
            if (file.tipo == 'ofertaspdf') {
                strHtml += '&nbsp;&nbsp;&nbsp;<button type="button" class="btn btn-danger" onclick="EliminarArchivoPorId(' + file.codRecurso + '); return false;">Eliminar</button>';
            }
            $('#divArchivoGenerico').html(strHtml);
            $('#divContenedorArchivoGenerico').css('display', 'block');
        }
        if (file.tipo == 'popup') {
            $('#divContenedorTodasSucursales').css('display', 'block');
            listaSucursalesSeleccionada = getSucursalesSeleccionada();
            CargarHtmlSucursales();
        }
    }

});
function sendFormSubir() {
    var valido = true;
    var str = "";
    for (var i = 0; i < listaTodasSucursales.length; i++) {
        if (document.getElementById("input" + listaTodasSucursales[i].sde_sucursal).checked) {
            var separador = '-';
            if (str == '') {
                separador = '';
            }
            str += separador + document.getElementById("input" + listaTodasSucursales[i].sde_sucursal).value;
        }
    }
    document.getElementById("hiddenSucursales").value = str;
    //if (valido) {
    //    document.getElementById("Form2").submit();
    //} 
    return valido;
}
function getSucursalesSeleccionada() {
    var listaSucursalesSeleccionada_aux = [];
    if (file.descr != null) {
        var res = file.descr.split(">");
        for (var i = 0; i < res.length; i++) {
            if (res[i] != '') {
                listaSucursalesSeleccionada_aux.push(res[i].replace('<', ''));
            }
        }
    }
    return listaSucursalesSeleccionada_aux;
}

function isCheck(pValue) {
    for (var i = 0; i < listaSucursalesSeleccionada.length; i++) {
        if (listaSucursalesSeleccionada[i] == pValue) {
            return true;
        }
    }
    return false;
}
function getHtmlCheck(pValue) {
    if (isCheck(pValue)) {
        return 'checked';
    }
    return '';
}
function CargarHtmlSucursales() {
    var strHtml = '';
    if (listaTodasSucursales != null) {
        for (var i = 0; i < listaTodasSucursales.length; i++) {
            strHtml += '<span><input ' + getHtmlCheck(listaTodasSucursales[i].sde_sucursal) + ' id="input' + listaTodasSucursales[i].sde_sucursal + '" name="input' + listaTodasSucursales[i].sde_sucursal + '" type="checkbox" value="' + listaTodasSucursales[i].sde_sucursal + '"><label for="input' + listaTodasSucursales[i].sde_sucursal + '">' + listaTodasSucursales[i].suc_nombre + '</label></span>' + '<br/>';
        }
    }
    $('#divTodasSucursales').html(strHtml);
}
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
