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
                //strHtml += '<button type="button" class="btn btn-info" onclick="return AgregarOfertaFolleto(\'' + args[i].ofe_idOferta + '\');">Folleto</button>' + '&nbsp;' + '&nbsp;';
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
function AgregarReCall() {
    location.href = 'GestionReCallAgregar.aspx';
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