var listaCV = [];
function RecuperarTodasCurriculumVitae() {
    //PageMethods.RecuperarTodasCurriculumVitae(OnCallBackRecuperarTodasCurriculumVitae, OnFail);
    RecuperarCurriculumVitae('','','');
}

function onclickBuscarCV() {
    var text = $('#txtBuscador').val();
    var puesto = $('#puesto_cv').val();  
    var sucursal = $('#sucursal_cv').val();  
    RecuperarCurriculumVitae(text, puesto, sucursal);
}
function RecuperarCurriculumVitae(pValor, pPuesto, pSucursal) {
    pagActual = 1;
    PageMethods.RecuperarCurriculumVitae(pValor, pPuesto, pSucursal,OnCallBackRecuperarTodasCurriculumVitae, OnFail);
}
function LlamarMetodoCargar(pagActual) {
    
    PageMethods.RecuperarPaginador(pagActual, OnCallBackRecuperarTodasCurriculumVitae, OnFail);
}

function OnCallBackRecuperarTodasCurriculumVitae(args) {
    if (args) {

        args = eval('(' + args + ')');
        //
    
        var mod = args.CantidadRegistroTotal % CantidadFilaPorPagina_const;
        var cantPag = 0;
        if (mod == 0) {
            cantPag = args.CantidadRegistroTotal / CantidadFilaPorPagina_const;
        }
        else {
            cantPag = Math.ceil((args.CantidadRegistroTotal / CantidadFilaPorPagina_const));
        }
        cantPaginaTotal = cantPag;
         GenerarPaginador();
        
        //
         if (args.listaCurriculumVitae.length > 0) {
            listaCV = args.listaCurriculumVitae;
            var strHtml = '';
            strHtml += '<div class="table-responsive">';
            strHtml += '<table class="table table-striped">';
            strHtml += '<thead>';
            strHtml += '<tr>';
            strHtml += '<th>Nombre y apellido</th>';
            // strHtml += '<th>Descripción</th>';
            strHtml += '<th>DNI</th>';
            //strHtml += '<th>Tipo</th>';
            strHtml += '<th>Mail </th>';
            strHtml += '<th>Fecha</th>';
            strHtml += '<th>Puesto  </th>';
            strHtml += '<th>Sucursal</th>';
            strHtml += '<th></th>';
            //strHtml += '<th>Modi</th>';
            //strHtml += '<th>Eli</th>';
            strHtml += '</tr>';
            strHtml += '</thead>';
            strHtml += '<tbody>';
            for (var i = 0; i < listaCV.length; i++) {
                strHtml += '<tr>';
                strHtml += '<td>' + listaCV[i].tcv_nombre + '</td>';
                //strHtml += '<td>' + args[i].ofe_descr + '</td>';
                strHtml += '<td>' + listaCV[i].tcv_dni + '</td>';
                strHtml += '<td>' + listaCV[i].tcv_mail + '</td>';
                strHtml += '<td>' + listaCV[i].tcv_fechaToString + '</td>';
                
                strHtml += '<td>' + listaCV[i].tcv_puesto + '</td>'; 
                strHtml += '<td>' + listaCV[i].tcv_sucursal + '</td>';
               // strHtml += '<td>' + args[i].tcv_fechaToString + '</td>';
                //strHtml += '<td>';
                //strHtml += '<label class="checkbox-inline">';
                //strHtml += '<input type="checkbox"  value="opcion_1"  onclick="return PublicarOferta(\'' + args[i].ofe_idOferta + '\');" ';
                //if (args[i].ofe_publicar)
                //    strHtml += ' checked="checked" /> Si';
                //else
                //    strHtml += ' /> No';
                //strHtml += '</label>';
                //strHtml += '</td>';
               // strHtml += '<td>' + args[i].Rating + '</td>';
                strHtml += '<td>';
                strHtml += '<button type="button" class="btn btn-info" onclick="return ComentarioCV(\'' + i + '\');">Comentario</button>' + '&nbsp;' + '&nbsp;';

                //strHtml += '<button type="button" class="btn btn-warning" onclick="return EditarOferta(\'' + args[i].ofe_idOferta + '\');">Modificar</button>' + '&nbsp;' + '&nbsp;';
                strHtml += '<button type="button" class="btn btn-danger" onclick="return ElimimarCV(\'' + listaCV[i].tcv_codCV + '\');">Eliminar</button>' + '&nbsp;' + '&nbsp;';
                strHtml += '<button type="button" class="btn btn-info" onclick="return DescargarCV(\'' + i + '\');">Archivo</button>' + '&nbsp;' + '&nbsp;';

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
function ComentarioCV(pValor) {
    $.confirm({
        title: 'Mensaje',
        content: listaCV[pValor].tcv_comentario,
        buttons: {
            Aceptar: function () {
      
            }
        }
    });
    return false;
}
function ElimimarCV(pValor) {
    $.confirm({
        title: 'Mensaje',
        content: '¿Desea eliminar?',
        buttons: {
            Aceptar: function () {
                PageMethods.EliminarCurriculumVitae(pValor, OnCallBackEliminarCurriculumVitae, OnFail);
            },
            Cancelar: function () {
            }

        }
    });
    return false;
}
function DescargarCV(pValor) {
    var nombreArchivo = listaCV[pValor].arc_nombre
  window.open('../../servicios/descargarArchivo.aspx?t=curriculumvitae&n=' + nombreArchivo, '_parent');
    return false;
}
function OnCallBackEliminarCurriculumVitae(args) {
    location.href = 'GestionCurriculumVitae_v2.aspx';
}
function ElimimarCV(pValor) {
    $.confirm({
        title: 'Mensaje',
        content: '¿Desea eliminar?',
        buttons: {
            Aceptar: function () {
                PageMethods.EliminarCurriculumVitae(pValor, OnCallBackEliminarCurriculumVitae, OnFail);
            },
            Cancelar: function () {
            }

        }
    });
    return false;
}