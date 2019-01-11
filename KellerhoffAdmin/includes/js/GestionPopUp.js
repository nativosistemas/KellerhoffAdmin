function RecuperarTodosArchivos() {
    PageMethods.RecuperarTodosArchivos(OnCallBackRecuperarTodosArchivos, OnFail);
}

function OnCallBackRecuperarTodosArchivos(args) {
    if (args) {

        args = eval('(' + args + ')');
        if (args.length > 0) {
            var strHtml = '';
            strHtml += '<div class="table-responsive">';
            strHtml += '<table class="table table-striped">';
            strHtml += '<thead>';
            strHtml += '<tr>';
            strHtml += '<th>Título</th>';
           // strHtml += '<th>Descuento</th>';
            strHtml += '<th>Publicar </th>';
            strHtml += '<th>Fecha Creación </th>';
            strHtml += '<th></th>';
            strHtml += '</tr>';
            strHtml += '</thead>';
            strHtml += '<tbody>';
            for (var i = 0; i < args.length; i++) {
                strHtml += '<tr>';
                strHtml += '<td>' + args[i].arc_titulo + '</td>';
                //strHtml += '<td>' + args[i].ofe_descuento + '</td>';
                strHtml += '<td>';
                strHtml += '<label class="checkbox-inline">';
                strHtml += '<input type="checkbox"  value="opcion_1"  onclick="return PublicarPopUp(\'' + args[i].arc_codRecurso + '\');" ';
                if (args[i].arc_estado == 2)
                    strHtml += ' checked="checked" /> Si';
                else
                    strHtml += ' /> No';
                strHtml += '</label>';
                strHtml += '</td>';
                strHtml += '<td>' + args[i].arc_fechaToString + '</td>';
                strHtml += '<td>';
                strHtml += '<button type="button" class="btn btn-warning" onclick="return EditarPopUp(\'' + args[i].arc_codRecurso + '\');">Modificar</button>' + '&nbsp;' + '&nbsp;';
                strHtml += '<button type="button" class="btn btn-danger" onclick="return ElimimarPopUp(\'' + args[i].arc_codRecurso + '\');">Eliminar</button>' + '&nbsp;' + '&nbsp;';
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
function AgregarPopUp() {
    location.href = 'AgregarArchivoGenerico.aspx?id=1&t=popup';
    return false;
}
function EditarPopUp(pValor) {
    location.href = 'AgregarArchivoGenerico.aspx?idRecurso=' + pValor;//+ '&t=popup'
    return false;
}
function PublicarPopUp(pValor) {
    PageMethods.CambiarEstadoArchivoPorId(pValor, OnCallBackCambiarEstadoArchivoPorId, OnFail);
    return false;
}
function OnCallBackCambiarEstadoArchivoPorId(args) {
    location.href = 'GestionPopUp.aspx';
}
function ElimimarPopUp(pValor) {
    PageMethods.EliminarArchivoPorId(pValor, OnCallBackEliminarArchivoPorId, OnFail);
    return false;
}
function OnCallBackEliminarArchivoPorId(args)
{
    location.href = 'GestionPopUp.aspx';
}