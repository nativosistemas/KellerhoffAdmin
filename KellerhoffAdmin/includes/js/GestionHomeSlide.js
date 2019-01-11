function RecuperarTodasSlide() {
    // PageMethods.RecuperarTodasHomeSlide(OnCallBackRecuperarTodasHomeSlide, OnFail);
    OnCallBackRecuperarTodasHomeSlide($('#hiddenListaSlider').val());
}


function OnCallBackRecuperarTodasHomeSlide(args) {
    if (args) {

        args = eval('(' + args + ')');
        if (args.length > 0) {
            var strHtml = '';
            strHtml += '<div class="table-responsive">';
            strHtml += '<table class="table table-striped">';
            strHtml += '<thead>';
            strHtml += '<tr>';
            strHtml += '<th>Título</th>';
            // strHtml += '<th>Descripción</th>';
            //strHtml += '<th>Descuento</th>';
            strHtml += '<th>Orden</th>';
            strHtml += '<th>Publicar </th>';
            //strHtml += '<th>Fecha Creación </th>';
            strHtml += '<th>Rating</th>';
            strHtml += '<th></th>';
            //strHtml += '<th>Modi</th>';
            //strHtml += '<th>Eli</th>';
            strHtml += '</tr>';
            strHtml += '</thead>';
            strHtml += '<tbody>';
            for (var i = 0; i < args.length; i++) {
                strHtml += '<tr>';
                strHtml += '<td>' + args[i].hsl_titulo + '</td>';
                //strHtml += '<td>' + args[i].hsl_descr + '</td>';
                //strHtml += '<td>' + args[i].ofe_descuento + '</td>';
                strHtml += '<td>' + args[i].hsl_orden + '</td>';
                strHtml += '<td>';


                strHtml += '<label class="checkbox-inline">';
                strHtml += '<input type="checkbox"  value="opcion_1"  onclick="return PublicarSlide(\'' + args[i].hsl_idHomeSlide + '\');" ';
                if (args[i].hsl_publicar)
                    strHtml += ' checked="checked" /> Si';
                else
                    strHtml += ' /> No';
                strHtml += '</label>';

                //if (args[i].hsl_publicar)
                //    strHtml += 'Si';
                //else
                //    strHtml += 'No';

                strHtml += '</td>';
                strHtml += '<td>' + args[i].hsl_RatingCount + '</td>';
                //strHtml += '<td>' + args[i].hsl_fecha + '</td>';//args[i].hsl_fechaToStrings 
                strHtml += '<td>';
                strHtml += '<button type="button" class="btn btn-warning" onclick="return SubirSlide(\'' + args[i].hsl_idHomeSlide + '\');">↑</button>' + '&nbsp;' + '&nbsp;';
                strHtml += '<button type="button" class="btn btn-danger" onclick="return BajarSlide(\'' + args[i].hsl_idHomeSlide + '\');">↓</button>' + '&nbsp;' + '&nbsp;';

                strHtml += '<button type="button" class="btn btn-warning" onclick="return EditarSlide(\'' + args[i].hsl_idHomeSlide + '\');">Modificar</button>' + '&nbsp;' + '&nbsp;';
                strHtml += '<button type="button" class="btn btn-danger" onclick="return ElimimarSlide(\'' + args[i].hsl_idHomeSlide + '\');">Eliminar</button>' + '&nbsp;' + '&nbsp;';
                strHtml += '<button type="button" class="btn btn-info" onclick="return AgregarImagen(\'' + args[i].hsl_idHomeSlide + '\');">Imagen </button>' + '&nbsp;' + '&nbsp;';
                strHtml += '<button type="button" class="btn btn-info" onclick="return AgregarImagenMobil(\'' + args[i].hsl_idHomeSlide + '\');">Imagen Mobil</button>' + '&nbsp;' + '&nbsp;';
                strHtml += '<button type="button" class="btn btn-warning" onclick="return IrVistaPreviaId(\'' + args[i].hsl_idHomeSlide + '\');">Vista Previa</button>';  //
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
//function AgregarOfertaImagen(pValor) {
//    location.href = 'GestionOfertaEditarAgregar.aspx?id=' + pValor;
//    return false;
//}
function IrVistaPreviaId(pId) {
    //location.href = '../../home/index.aspx';
    window.open(
  '../../home/vistapreviaslider.aspx?id=' + pId,
  '_blank' // <- This is what makes it open in a new window.
);
    return false;
}
function PublicarSlide(pValor)
{
    PageMethods.CambiarPublicarHomeSlide(pValor, OnCallBackCambiarPublicarHomeSlide, OnFail);
}
function OnCallBackCambiarPublicarHomeSlide(args)
{
    location.href = 'GestionHomeSlide.aspx';
}
function AgregarImagen(pValor) {
    location.href = 'AgregarArchivo.aspx?id=' + pValor + '&t=slider&an=1920&al=700';
        return false;
}
function AgregarImagenMobil(pValor) {
    location.href = 'AgregarArchivo.aspx?id=' + pValor + '&t=slider&an=700&al=700';
    return false;
}
function EditarSlide(pValor) {
    location.href = 'GestionHomeSlideEditar.aspx?id=' + pValor;
    return false;
}
function SubirSlide(pValor) {
    PageMethods.CambiarOrdenHomeSlide(pValor, false, OnCallBackCambiarOrdenHomeSlide, OnFail);

    return false;
}
function BajarSlide(pValor) {
    PageMethods.CambiarOrdenHomeSlide(pValor, true, OnCallBackCambiarOrdenHomeSlide, OnFail);

    return false;
}
function OnCallBackCambiarOrdenHomeSlide(args)
{
    location.href = 'GestionHomeSlide.aspx';
    if (args)
    { }
    else {

    }
}
function ElimimarSlide(pValor) {
    $.confirm({
        title: 'Mensaje',
        content: '¿Desea eliminar Slide?',
        buttons: { 
            Aceptar: function () {
                PageMethods.EliminarHomeSlide(pValor, OnCallBackEliminarHomeSlide, OnFail);
            },
            Cancelar: function () {
            }

        }
    });
    return false;
}
function OnCallBackEliminarHomeSlide(args)
{
    location.href = 'GestionHomeSlide.aspx';
}
function VolverSlide() {
    location.href = 'GestionHomeSlide.aspx';
    return false;
}
function GuardarSlide() {
   // hiddenOfe_idHomeSlide
    // hsl_idHomeSlide, string hsl_titulo, string hsl_descr, string hsl_descrHtml, int hsl_tipo
    var hsl_titulo = $('#txt_titulo').val();
    var hsl_descr = $('#txt_descr').val();
    var hsl_descrHtmlReducido = $('#txt_descrHtmlReducido').val();
    var hsl_descrHtml = $('#textareaHtml').val();
    var hsl_tipo = parseInt($('#cmdTipoSlide').val());
    var hsl_idOferta = null;
    if (hsl_tipo == "2") {
        hsl_idOferta = $('#cmdOferta').val();
    }
    var hsl_NombreRecursoDoc = null;
    if (hsl_tipo == "3") {
        hsl_NombreRecursoDoc = $('#cmdCatalogo').val();
    }
    var hsl_idRecursoImgPC = null;
    var hsl_idRecursoImgMobil = null;

    PageMethods.InsertarActualizarHomeSlide(hsl_titulo, hsl_descr, hsl_descrHtml, hsl_descrHtmlReducido, hsl_tipo, hsl_idOferta, hsl_NombreRecursoDoc, hsl_idRecursoImgPC, hsl_idRecursoImgMobil, OnCallBackInsertarActualizarHomeSlide, OnFail);

}
function OnCallBackInsertarActualizarHomeSlide(args)
{
    VolverSlide();
}
function onclickSelectTipoSlide() {
    $('.cssIdOferta').css('display', 'none');
    $('.cssIdCatalogo').css('display', 'none');
    var t = $('#cmdTipoSlide').val();
    switch (t) {
        case '1':
            break;
        case '2':
            $('.cssIdOferta').css('display', 'block');
            break;
        case '3':
            $('.cssIdCatalogo').css('display', 'block');
            
            break;
        default:
            break;
    }


}