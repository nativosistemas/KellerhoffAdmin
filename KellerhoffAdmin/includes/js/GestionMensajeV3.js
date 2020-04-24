var listaTodasSucursales = null;
var listaSucursalesSeleccionada = null;

function RecuperarTodosMensajeNew() {
    PageMethods.RecuperarTodosMensajeNew(OnCallBackRecuperarTodosMensajeNew, OnFail);
}

function OnCallBackRecuperarTodosMensajeNew(args) {
    if (args) {

        args = eval('(' + args + ')');
        if (args.length > 0) {
            var strHtml = '';
            strHtml += '<div class="table-responsive">';
            strHtml += '<table class="table table-striped">';
            strHtml += '<thead>';
            strHtml += '<tr>';
            strHtml += '<th>Asunto</th>';
            strHtml += '<th>Fecha Desde</th>';
            strHtml += '<th>Fecha Hasta</th>';
            strHtml += '<th></th>';
            strHtml += '</tr>';
            strHtml += '</thead>';
            strHtml += '<tbody>';
            for (var i = 0; i < args.length; i++) {
                strHtml += '<tr>';
                strHtml += '<td>' + args[i].tmn_asunto + '</td>';
                strHtml += '<td>' + args[i].tmn_fechaDesdeToString + '</td>';
                strHtml += '<td>' + args[i].tmn_fechaHastaToString + '</td>';
                strHtml += '<td>';
                strHtml += '<button type="button" class="btn btn-warning" onclick="return EditarMensaje(\'' + args[i].tmn_codigo + '\');">Modificar</button>' + '&nbsp;' + '&nbsp;';
                strHtml += '<button type="button" class="btn btn-danger" onclick="return ElimimarMensaje(\'' + args[i].tmn_codigo + '\');">Eliminar</button>' + '&nbsp;' + '&nbsp;';
                strHtml += '</td>';

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



function EditarMensaje(pValor) {
    location.href = 'GestionMensajeV3Editar.aspx?id=' + pValor;
    return false;
}
function ElimimarMensaje(pValor) {
    PageMethods.ElimimarMensajeNewPorId(pValor, OnCallBackElimimarMensajeNewPorId, OnFail);
    return false;
}
function OnCallBackElimimarMensajeNewPorId(args) {
    VolverGestionMensaje();
}
function VolverGestionMensaje() {
    location.href = 'GestionMensajeV3.aspx';
    return false;
}

function validarFechas(pFechaDesde_string, pFechaHasta_string) {
    PageMethods.isValidoFechas(pFechaDesde_string, pFechaHasta_string, OnCallBackIsValidoFechas, OnFailInsertarActualizarMensaje);
}

function OnCallBackIsValidoFechas(args) {
    if (args == 'Ok') {
        funActualizarInsertarMensajeNew();
    } else {
        alert('La "Fechas Desde" es mayor a la "Fecha Hasta"');
    }
}
function GuardarMensaje() {
    var asunto = $('#txtAsunto').val();

    var mensaje = $('#textareaHtml').val();
    if (isNotNullEmpty(asunto) && isNotNullEmpty(mensaje)) {
        var FechaDesde_string = $('#txtFechaDesde').val();
        var FechaHasta_string = $('#txtFechaHasta').val();

        if (FechaDesde_string != '' && FechaHasta_string != '') {
            validarFechas(FechaDesde_string, FechaHasta_string);
        } else if (FechaDesde_string != '' || FechaHasta_string != '') {
            alert('Complete todos los campos fechas');
        } else {
            funActualizarInsertarMensajeNew();
        }
    }
    else {
        alert('Complete todos los campos: Asunto y/o Mensaje');
    }
}

function funActualizarInsertarMensajeNew() {
    var asunto = $('#txtAsunto').val();
    var mensaje = $('#textareaHtml').val();
    var FechaDesde_string = $('#txtFechaDesde').val();
    var FechaHasta_string = $('#txtFechaHasta').val();
    var sucursales = getSucursalesSeleccionas();
    PageMethods.ActualizarInsertarMensajeNew(asunto, mensaje, FechaDesde_string, FechaHasta_string, sucursales, OnCallBackInsertarActualizarMensaje, OnFailInsertarActualizarMensaje);
}
function MostrarMensaje() {
    //location.href = 'GestionMensajeV3VistaPreviaV2.aspx';
    //window.open('GestionMensajeV3VistaPreviaV2.aspx', '_blank');
    //return false;
    var asunto = $('#txtAsunto').val();

    var mensaje = $('#textareaHtml').val();
    if (isNotNullEmpty(asunto) && isNotNullEmpty(mensaje)) {
        vistaPreviaMensajeNew(asunto, mensaje);
    }
    //if (isNotNullEmpty(asunto) && isNotNullEmpty(mensaje)) {
    //    mensaje_EnInicio(asunto, mensaje);
    //   }
    return false;
}
function vistaPreviaMensajeNew(pAsunto, pMensaje) {
    PageMethods.vistaPreviaMensajeNew(pAsunto, pMensaje, OnCallBackvistaPreviaMensajeNew, OnFail);
}

function OnCallBackvistaPreviaMensajeNew(args) {
    window.open('GestionMensajeV3VistaPreviaV2.aspx', '_blank');
}
function MostrarMensaje_aux() {
    var asunto = 'lalalalalalal';

    var mensaje = 'hola mundo!!';

    if (isNotNullEmpty(asunto) && isNotNullEmpty(mensaje)) {
        mensaje_EnInicio(asunto, mensaje);
    }
    return false;
}
function MostrarMensaje_vistaPrevia() {
    var asunto = $('#hidden_asunto').val();
    if (typeof asunto == 'undefined') {
        asunto = null;
    } else {
        //$('#txtAsunto').val(asunto);
    }
    var mensaje = $('#hidden_mensaje').val();
    if (typeof mensaje == 'undefined') {
        mensaje = '';
    }
    if (isNotNullEmpty(asunto) && isNotNullEmpty(mensaje)) {
        mensaje_EnInicio(asunto, mensaje);
    }
}
function OnCallBackInsertarActualizarMensaje(args) {
    VolverGestionMensaje();
}
function OnFailInsertarActualizarMensaje(er) {
    VolverGestionMensaje();
}

function mensaje_EnInicio(pTitulo, pMensaje) {
    var strHtml = '';
    strHtml += '<div class="modal-background">&nbsp;</div>';
    strHtml += '<div class="modal-dialog modal-lg"><div class="modal-content">';
    strHtml += '<div class="modal-header no-padding-bottom">';
    strHtml += '<div class="row">';
    strHtml += '<div class="col-lg-12">';
    strHtml += '<h4>' + pTitulo + '</h4>';
    strHtml += '</div>';
    strHtml += '</div>';
    strHtml += '<div class="close-modal" data-dismiss="modal"><i class="fa fa-times"></i></div>';
    strHtml += '</div>';
    strHtml += '<div class="modal-body"><div class="col-lg-12">';
    strHtml += pMensaje;
    //strHtml += '<a class="btn_confirmar float-left" href="#" data-dismiss="modal">CERRAR</a>';
    strHtml += '</div></div>';
    strHtml += '<div class="clear"></div>';
    strHtml += '</div></div>';
    $('#modalModulo').html(strHtml);
    $('#modalModulo').modal();
}
//
function getSucursalesSeleccionada(pValue) {
    var listaSucursalesSeleccionada_aux = [];
    if (pValue != null) {//file.descr != null
        var res = pValue.split(">"); // file.descr.split(">"); 
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
    var isAlgunaSeleccionada = false;
    if (listaTodasSucursales != null) {
        for (var i = 0; i < listaTodasSucursales.length; i++) {
            var htmlCheck = getHtmlCheck(listaTodasSucursales[i].sde_sucursal);
            strHtml += '<span><input ' + htmlCheck + ' id="input' + listaTodasSucursales[i].sde_sucursal + '" name="input' + listaTodasSucursales[i].sde_sucursal + '" type="checkbox" value="' + listaTodasSucursales[i].sde_sucursal + '"><label class="labelSucursales" for="input' + listaTodasSucursales[i].sde_sucursal + '">' + listaTodasSucursales[i].suc_nombre + '</label></span>' + '<br/>';
            if (htmlCheck != '') {
                isAlgunaSeleccionada = true;
            }
        }
    }
    $('#divTodasSucursales').html(strHtml);
    //onclickRadioSucursal();
    if (isAlgunaSeleccionada) {
        document.getElementById("radioElegirSucursal").checked = true;
    } else {
        for (var i = 0; i < listaTodasSucursales.length; i++) {
            document.getElementById("input" + listaTodasSucursales[i].sde_sucursal).disabled = true;
        }
    }
}
function getSucursalesSeleccionas() {
    //var valido = true;
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
    return str;
}
function onclickRadioSucursal() {

    var radiovalue = Form2.radioSucursales.value;
    if (radiovalue.length == 0) {
    }
    else if (radiovalue == "todasSucursales") {
        for (var i = 0; i < listaTodasSucursales.length; i++) {
            document.getElementById("input" + listaTodasSucursales[i].sde_sucursal).checked = false;
            document.getElementById("input" + listaTodasSucursales[i].sde_sucursal).disabled = true;
        }

    } else {
        for (var i = 0; i < listaTodasSucursales.length; i++) {
            document.getElementById("input" + listaTodasSucursales[i].sde_sucursal).disabled = false;
        }
    }
}
function cargarFechasEditar() {
    var cargarFechaDesde = $('#hidden_fechaDesde').val();
    if (typeof cargarFechaDesde == 'undefined') {
        cargarFechaDesde = null;
    }
    var cargarFechaHasta = $('#hidden_fechaHasta').val();
    if (typeof cargarFechaHasta == 'undefined') {
        cargarFechaHasta = null;
    }

    var $datepickerFechaDesde = $('#txtFechaDesde');
    $datepickerFechaDesde.datepicker("option", "dateFormat", 'dd/mm/yy');
    if (cargarFechaDesde != null) {
        $datepickerFechaDesde.datepicker('setDate', cargarFechaDesde);
    }

    var $datepickerFechaHasta = $('#txtFechaHasta');
    $datepickerFechaHasta.datepicker("option", "dateFormat", 'dd/mm/yy');
    if (cargarFechaDesde != null) {
        $datepickerFechaHasta.datepicker('setDate', cargarFechaHasta);
    }
}