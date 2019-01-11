function RecuperarTodasOfertas() {
    PageMethods.RecuperarTodasOfertas(OnCallBackRecuperarTodasOfertas, OnFail);
}

function OnCallBackRecuperarTodasOfertas(args) {
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
            strHtml += '<th>Descuento</th>';
            //strHtml += '<th>Tipo</th>';
            strHtml += '<th>Publicar </th>';
            strHtml += '<th>Rating</th>';
            strHtml += '<th></th>';
            //strHtml += '<th>Modi</th>';
            //strHtml += '<th>Eli</th>';
            strHtml += '</tr>';
            strHtml += '</thead>';
            strHtml += '<tbody>';
            for (var i = 0; i < args.length; i++) {
                strHtml += '<tr>';
                strHtml += '<td>' + args[i].ofe_titulo + '</td>';
                //strHtml += '<td>' + args[i].ofe_descr + '</td>';
                strHtml += '<td>' + args[i].ofe_descuento + '</td>';
                // strHtml += '<td>' + args[i].ofe_tipo + '</td>';
                strHtml += '<td>';
                strHtml += '<label class="checkbox-inline">';
                strHtml += '<input type="checkbox"  value="opcion_1"  onclick="return PublicarOferta(\'' + args[i].ofe_idOferta + '\');" ';
                if (args[i].ofe_publicar)
                    strHtml += ' checked="checked" /> Si';
                else
                    strHtml += ' /> No';
                strHtml += '</label>';
                //if (args[i].ofe_publicar)
                //    strHtml += 'Si';
                //else
                //    strHtml += 'No';
                strHtml += '</td>';
                strHtml += '<td>' + args[i].Rating + '</td>';
                strHtml += '<td>';
                strHtml += '<button type="button" class="btn btn-warning" onclick="return EditarOferta(\'' + args[i].ofe_idOferta + '\');">Modificar</button>' + '&nbsp;' + '&nbsp;';
                strHtml += '<button type="button" class="btn btn-danger" onclick="return ElimimarOferta(\'' + args[i].ofe_idOferta + '\');">Eliminar</button>' + '&nbsp;' + '&nbsp;';
                strHtml += '<button type="button" class="btn btn-info" onclick="return AgregarOfertaImagen(\'' + args[i].ofe_idOferta + '\');">Imagen</button>' + '&nbsp;' + '&nbsp;';
                strHtml += '<button type="button" class="btn btn-info" onclick="return AgregarOfertaFolleto(\'' + args[i].ofe_idOferta + '\');">Folleto</button>' + '&nbsp;' + '&nbsp;';
                strHtml += '<button type="button" class="btn btn-warning" onclick="return IrVistaPreviaId(\'' + args[i].ofe_idOferta + '\');">Vista Previa</button>';  //
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
function PublicarOferta(pValor)
{
    PageMethods.CambiarEstadoPublicarOferta(pValor, OnCallBackCambiarEstadoPublicarOferta, OnFail);
}
function OnCallBackCambiarEstadoPublicarOferta(args) {
    location.href = 'GestionOferta.aspx';
}
function EditarOferta(pValor) {
    location.href = 'GestionOfertaEditar.aspx?id=' + pValor;
    return false;
}

function ElimimarOferta(pValor) {
    $.confirm({
        title: 'Mensaje',
        content: '¿Desea eliminar oferta?',
        buttons: {
            Aceptar: function () {
                // $.alert('Confirmed!');
                PageMethods.ElimimarOfertaPorId(pValor, OnCallBackElimimarOfertaPorId, OnFail);
            },
            Cancelar: function () {
                //$.alert('Canceled!');
            }
            
        }
    });
  
    return false;
}
function OnCallBackElimimarOfertaPorId(args) {
    RecuperarTodasOfertas();
}
function busOferta() {
    var varPalabraBuscador = $('#txtBuscar').val();

    PageMethods.RecuperarProductos(varPalabraBuscador, OnCallBackRecuperarProductos, OnFail);
    return false;
}

function OnCallBackRecuperarProductos(args) {
    var isClearHtml = true;
    if (args) {

        args = eval('(' + args + ')');
        if (args.length > 0) {
            isClearHtml = false;
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
                strHtml += '<td>' + '<button type="button" class="btn btn-warning" onclick="return AgregarProductoOferta(\'' + args[i].pro_codigo + '\');">Agregar</button>' + '</td>';
                strHtml += '</tr>';
            }
            strHtml += '</tbody>';
            strHtml += '</table>';
            strHtml += '</div>';
            $('#divContenedorGrilla').html(strHtml);
        }
      
    }
    if (isClearHtml)
       $('#divContenedorGrilla').html('');
}
function AgregarProductoOferta(pValue) {
    PageMethods.AgregarProducto(pValue, OnCallBackAgregarProducto, OnFail);
    return false;
}
function OnCallBackAgregarProducto(args) {
    if (args) {

        args = eval('(' + args + ')');
        if (args.length > 0) {
            var strHtml = '';
            strHtml += '<div class="table-responsive">';
            strHtml += '<table class="table table-striped table-bordered">';
            strHtml += '<thead>';
            strHtml += '<tr>';
            strHtml += '<th>Código</th>';
            strHtml += '<th>Nombre</th>';
            strHtml += '<th></th>';
            //strHtml += '<th>Modi</th>';
            //strHtml += '<th>Eli</th>';
            strHtml += '</tr>';
            strHtml += '</thead>';
            strHtml += '<tbody>';
            for (var i = 0; i < args.length; i++) {
                strHtml += '<tr>';
                strHtml += '<td>' + args[i].codigo + '</td>';
                strHtml += '<td>' + args[i].nombre + '</td>';
                strHtml += '<td>' + '<button type="button" class="btn btn-warning" onclick="return EliminarProductoOferta(\'' + args[i].ofd_idOfertaDetalle + '\');">Quitar</button>' + '</td>';
                strHtml += '</tr>';
            }
            strHtml += '</tbody>';
            strHtml += '</table>';
            strHtml += '</div>';
            $('#divContenedorGrillaOfertaProducto').html(strHtml);
        }
        else {
            $('#divContenedorGrillaOfertaProducto').html('');
        }
    }
}
function EliminarProductoOferta(pValue) {
    PageMethods.ElimimarOfertaDetallePorId(pValue, OnCallBackAgregarProducto, OnFail);
    return false;
}
function RecuperarTodasOfertaDetalles() {
    PageMethods.RecuperarTodasOfertaDetalles(OnCallBackAgregarProducto, OnFail);
}
function VolverOferta() {
    location.href = 'GestionOferta.aspx';
    return false;
}
function IrVistaPrevia() {
    //location.href = '../../home/index.aspx';
    window.open(
  '../../home/index.aspx',
  '_blank' // <- This is what makes it open in a new window.
);
    return false;
}
function IrVistaPreviaId(pId) {
    //location.href = '../../home/index.aspx';
    window.open(
  '../../home/vistapreviaoferta.aspx?id=' + pId,
  '_blank' // <- This is what makes it open in a new window.
);
    return false;
}
function GuardarOferta() {
    var titulo = $('#txt_titulo').val();
    if (isNotNullEmpty(titulo))
        titulo = titulo.toUpperCase();
    var descr = $('#txt_descr').val();
    var descuento = $('#txt_descuento').val();
    if (isNotNullEmpty(descuento))
        descuento = descuento.toUpperCase();
    var etiqueta = $('#txt_etiqueta').val();
    if (isNotNullEmpty(etiqueta))
        etiqueta = etiqueta.toUpperCase();
    var etiquetaColor = $('#cmdEtiquetaColor').val();
    var tipo = $('input[name=opciones]:checked', '#Form2').val();
    var nombreTransfer = $('#cmdTransfer').val();
    var ofe_fechaFinOferta = $('#txt_fechaFin').val();

    PageMethods.InsertarActualizarOferta(titulo, descr, descuento, etiqueta, etiquetaColor, tipo, nombreTransfer, ofe_fechaFinOferta, OnCallBackInsertarActualizarOferta, OnFailInsertarActualizarOferta);

}
function OnFailInsertarActualizarOferta(er)
{ VolverOferta(); }
function OnCallBackInsertarActualizarOferta(args) {
    VolverOferta();
}
//function CargarDatosOfertaEditar() {
//    var titulo = $('#hiddenOfe_titulo').val();
//    if (typeof titulo == 'undefined') {
//        titulo = null;
//    } else {
//        $('#txt_titulo').val(titulo);
//    }
//    var descr = $('#hiddenOfe_descr').val();
//    if (typeof descr == 'undefined') {
//        descr = null;
//    } else {
//        $('#txt_descr').val(descr);
//    }
//    var descuento = $('#hiddenOfe_descuento').val();
//    if (typeof descuento == 'undefined') {
//        descuento = null;
//    } else {
//        $('#txt_descuento').val(descr);
//    }
//}
function AgregarOfertaFolleto(pValor) {
    location.href = 'AgregarArchivoGenerico.aspx?id=' + pValor + '&t=ofertaspdf';
    return false;
}

function AgregarOfertaImagen(pValor) {
    //location.href = 'GestionOfertaEditarAgregar.aspx?id=' + pValor;
    location.href = 'AgregarArchivo.aspx?id=' + pValor + '&t=ofertas&an=300&al=300';
    return false;
}
function onclickVolverOfertaImagen() {
    location.href = 'GestionOferta.aspx';//?text=' + $('#hiddenText').val();
    return false;
}
function GuardarOfertaHome() {

    PageMethods.InsertarActualizarOfertaHome(parseInt( $('#cmdOfertaHome_1').val()),parseInt( $('#cmdOfertaHome_2').val()),parseInt( $('#cmdOfertaHome_3').val()),parseInt( $('#cmdOfertaHome_4').val()),  OnCallBackInsertarActualizarOfertaHome, OnFail);
}
function OnCallBackInsertarActualizarOfertaHome(args)
{

}
function onkeypressAgregarBuscar(e) {
    if (!e) e = window.event;
    var keyCode = e.keyCode || e.which;
    if (keyCode == '13') {
        // Enter pressed
        busOferta();
        return false;
    }
}