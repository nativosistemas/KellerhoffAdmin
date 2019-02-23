function RecuperarTodosReCall() {
    PageMethods.RecuperarTodosReCall(OnCallBackRecuperarTodosReCall, OnFail);
}

function OnCallBackRecuperarTodosReCall(args) {
    if (args) {

        args = eval('(' + args + ')');
        if (args.length > 0) {
            var strHtml = '';
            strHtml += '<div class="table-responsive">';
            strHtml += '<table class="table table-striped">';
            strHtml += '<thead>';
            strHtml += '<tr>';
            strHtml += '<th>Título</th>';
            strHtml += '<th>Publicar </th>';
            strHtml += '<th>Fecha Creación </th>';
            strHtml += '<th></th>';
            strHtml += '</tr>';
            strHtml += '</thead>';
            strHtml += '<tbody>';
            for (var i = 0; i < args.length; i++) {
                strHtml += '<tr>';
                strHtml += '<td>' + args[i].rec_titulo + '</td>';
                strHtml += '<td>';
                strHtml += '<label class="checkbox-inline">';
                strHtml += '<input type="checkbox"  value="opcion_1"  onclick="return PublicarReCall(\'' + args[i].rec_id + '\');" ';
                if (args[i].rec_visible)
                    strHtml += ' checked="checked" /> Si';
                else
                    strHtml += ' /> No';
                strHtml += '</label>';
                strHtml += '</td>';
                strHtml += '<td>' + args[i].rec_FechaNoticiaToString + '</td>';
                strHtml += '<td>';
                strHtml += '<button type="button" class="btn btn-warning" onclick="return EditarReCall(\'' + args[i].rec_id + '\');">Modificar</button>' + '&nbsp;' + '&nbsp;';
                strHtml += '<button type="button" class="btn btn-danger" onclick="return EliminarReCall(\'' + args[i].rec_id + '\');">Eliminar</button>' + '&nbsp;' + '&nbsp;';
                //strHtml += '<button type="button" class="btn btn-info" onclick="return AgregarOfertaImagen(\'' + args[i].ofe_idOferta + '\');">Imagen</button>' + '&nbsp;' + '&nbsp;';
                strHtml += '<button type="button" class="btn btn-info" onclick="return AgregarReCallFolleto(\'' + args[i].rec_id + '\');">Documento</button>' + '&nbsp;' + '&nbsp;';
                //strHtml += '<button type="button" class="btn btn-warning" onclick="return IrVistaPreviaId(\'' + args[i].ofe_idOferta + '\');">Vista Previa</button>';  //
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
function AgregarReCallFolleto(pValor) {
    location.href = 'AgregarArchivoGenerico.aspx?id=' + pValor + '&t=recallpdf';
    return false;
}
function AgregarReCall() {
    location.href = 'GestionReCallAgregar.aspx?id=0';
    return false;
}
function EditarReCall(pValor) {
    location.href = 'GestionReCallAgregar.aspx?id=' + pValor;
    return false;
}
function PublicarReCall(pValor) {
    PageMethods.CambiarEstadoReCallPorId(pValor, CambiarEstadoReCallPorId, OnFail);
    return false;
}
function CambiarEstadoReCallPorId(args) {
    location.href = 'GestionReCall.aspx';
}
function EliminarReCall(pValor) {
    PageMethods.EliminarReCall(pValor, OnCallBackEliminarReCall, OnFail);
    return false;
}
function OnCallBackEliminarReCall(args) {
    location.href = 'GestionReCall.aspx';
}

function GuardarReCall() {
    var titulo = $('#txt_titulo').val();
    
    var descr = $('#textareaHtml').val();
    var descrReducido = $('#txt_descripcionReducido').val();

    var FechaNoticia_string = $('#txt_fecha').val();
    if (isNotNullEmpty(descr) && isNotNullEmpty(titulo) && isNotNullEmpty(descrReducido) && isNotNullEmpty(titulo)) {

        PageMethods.InsertarActualizarReCall(titulo, descr, descrReducido, FechaNoticia_string, OnCallBackInsertarActualizarReCall, OnFailInsertarActualizarReCall);
    }
    else {
        alert('Complete todos los campos');
    }
}
function VolverReCall() {
    location.href = 'GestionReCall.aspx';
    return false;
}
function OnFailInsertarActualizarReCall(er) { VolverReCall(); }
function OnCallBackInsertarActualizarReCall(args) {
    VolverReCall();
}