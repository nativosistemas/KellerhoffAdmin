var listaReglasRol = [];
var listaReglasRolSelecciondo = [];

$(document).ready(function () {
    document.getElementById('cmbRoles').selectedIndex = -1;
    CargarArbolEnInicio();
    $("#btnGuardar").attr('disabled', 'disabled');
    $("#divSectorArbol").attr('disabled', 'disabled');
});

function CargarArbolEnInicio() {
    var listaResultado = eval('(' + listaJsonReglasRol + ')');
    listaReglasRol = listaResultado;
    if (listaReglasRol.length > 0) {
        for (var xRaiz = 0; xRaiz < listaReglasRol.length; xRaiz++) {
            var strTexto = "<table class=\'cssTablaRolesReglas\'><tr class=\'cssFilaCabecera\'>  <td class=\'cssCeldaCabecera\'>  Reglas </td> <td class=\'cssCeldaCabeceraAccion\'><img src=\'../../img/varios/rule-view.gif\'/> </td><td class=\'cssCeldaCabeceraAccion\'><img src=\'../../img/varios/rule-insert.gif\' /></td> <td class=\'cssCeldaCabeceraAccion\'> <img src=\'../../img/varios/rule-update.gif\' /> </td> <td class=\'cssCeldaCabeceraAccion\'><img src=\'../../img/varios/rule-delete.gif\' /> </td><td  class=\'cssCeldaCabecera\'  > Clave</td></tr>";
            if (listaReglasRol[xRaiz].idPadreRegla == null) {
                strTexto += "<tr id=\'fila_" + String(listaReglasRol[xRaiz].id) + "\'><td >";
                strTexto += "<div id=\'divImgBoton_" + String(listaReglasRol[xRaiz].id) + "\' class=\'cssDivImagenContraer\'  onclick=\'ExpandirOContraerNodo(this)\'> </div>";
                strTexto += "<div class=\'cssCeldaDescripcionReglaRaiz\'>" + listaReglasRol[xRaiz].descripcion + "</div> </td> <td  class=\'cssCeldaCheckBox\'>";
                strTexto += "<input id=\'chkActivar_" + String(listaReglasRol[xRaiz].id) + "\' type=\'checkbox\' onClick=\'checkAllRaizActivar(this)\' /> </td> <td  class=\'cssCeldaCheckBox\'> </td><td  class=\'cssCeldaCheckBox\'> </td><td  class=\'cssCeldaCheckBox\'></td>"
                strTexto += "<td class=\'cssCeldaPalabraClave\'>" + listaReglasRol[xRaiz].palabra + "</td>";
                strTexto += "</tr>"
                listaReglasRol[xRaiz].isGraficada = true;
                break;
            }
        }
        for (var i = 0; i < listaReglasRol.length; i++) {
            if (!listaReglasRol[i].isGraficada) {

                var NivelCss = listaReglasRol[i].Nivel * 20;

                var strFila = "<tr id=\'fila_" + String(listaReglasRol[i].id) + "\'><td>";
                if (listaReglasRol[i].listaIdHijas.length > 0) {
                    strFila += "<div id=\'divImgBoton_" + String(listaReglasRol[i].id) + "\' style=\'margin-left: " + String(NivelCss) + "px\' class=\'cssDivImagenContraer\'  onclick=\'ExpandirOContraerNodo(this)\'> </div>";
                    NivelCss = NivelCss + 12;
                }

                strFila += "<div id=\'divImg_" + String(listaReglasRol[i].id) + "\' style=\'margin-left: " + String(NivelCss) + "px\'>" + listaReglasRol[i].descripcion + "</div></td>";

                strFila += "<td class=\'cssCeldaCheckBox\'><input id=\'chkActivar_" + String(listaReglasRol[i].id) + "\' type=\'checkbox\' onClick=\'checkAll(this)\' /></td>";
                if (listaReglasRol[i].checkAgregar == -1) {
                    strFila += "<td class=\'cssCeldaCheckBox\'> </td>";
                } else if (listaReglasRol[i].checkAgregar == 0) {
                    strFila += "<td class=\'cssCeldaCheckBox\'><input id=\'chkAgregar_" + String(listaReglasRol[i].id) + "\' type='checkbox\' onClick=\'checkAll(this)\' /></td>";
                }

                if (listaReglasRol[i].checkEditar == -1) {
                    strFila += "<td class=\'cssCeldaCheckBox\'></td>";
                } else if (listaReglasRol[i].checkEditar == 0) {
                    strFila += "<td class=\'cssCeldaCheckBox\'><input id=\'chkEditar_" + String(listaReglasRol[i].id) + "\' type='checkbox\' onClick=\'checkAll(this)\' /></td>";
                }
                if (listaReglasRol[i].checkEliminar == -1) {
                    strFila += "<td class=\'cssCeldaCheckBox\'></td>";
                } else if (listaReglasRol[i].checkEliminar == 0) {
                    strFila += "<td class=\'cssCeldaCheckBox\'><input id=\'chkEliminar_" + String(listaReglasRol[i].id) + "\' type='checkbox\' onClick=\'checkAll(this)\' /> </td>";
                }
                strFila += "<td class=\'cssCeldaPalabraClave\'>" + listaReglasRol[i].palabra + "</td>";
                strFila += "</tr>";
                strTexto += strFila;
                listaReglasRol[i].isGraficada = true;
                strTexto += GraficarNodosHijos(listaReglasRol[i].listaIdHijas);
            } // fin if (!listaReglasRol[i].isGraficada) 
        }
        strTexto += "</table>";
        document.getElementById('divSectorArbol').innerHTML = strTexto;
    }
    else {
        // hacer algo cuando no se encuetra reglas para el rol
        var strTextoSinReglas = "<table class=\'cssTablaRolesReglas\'><tr class=\'cssFilaCabecera\'>  <td class=\'cssCeldaCabecera\'>  Reglas </td> <td class=\'cssCeldaCabeceraAccion\'><img src=\'../img/varios/rule-view.gif\'/> </td><td class=\'cssCeldaCabeceraAccion\'><img src=\'../img/varios/rule-insert.gif\' /></td> <td class=\'cssCeldaCabeceraAccion\'> <img src=\'../img/varios/rule-update.gif\' /> </td> <td class=\'cssCeldaCabeceraAccion\'><img src=\'../img/varios/rule-delete.gif\' /> </td><td  class=\'cssCeldaCabecera\'  > Clave</td></tr>";
        strTextoSinReglas += "</table>";
        document.getElementById('divSectorArbol').innerHTML = strTextoSinReglas;
    }
}
function checkAllRaizActivar(pObj) {
    for (var i = 0; i < listaReglasRol.length; i++) {
        var ckActivar = document.getElementById("chkActivar_" + String(listaReglasRol[i].id));
        if (ckActivar != null) {
            ckActivar.checked = pObj.checked;
        }
        //
        var ckAgregar = document.getElementById("chkAgregar_" + String(listaReglasRol[i].id));
        if (ckAgregar != null) {
            ckAgregar.checked = pObj.checked;
        }
        var ckEditar = document.getElementById("chkEditar_" + String(listaReglasRol[i].id));
        if (ckEditar != null) {
            ckEditar.checked = pObj.checked;
        }
        var ckEliminar = document.getElementById("chkEliminar_" + String(listaReglasRol[i].id));
        if (ckEliminar != null) {
            ckEliminar.checked = pObj.checked;
        }
    }
}
function checkAll(pObj) {
    var lis = pObj.id.split('_');
    for (var i = 0; i < listaReglasRol.length; i++) {
        if (listaReglasRol[i].id == parseInt(lis[1])) {
            if (lis[0].toString() == 'chkActivar') {
                var ckAgregar = document.getElementById("chkAgregar_" + lis[1]);
                if (ckAgregar != null) {
                    ckAgregar.checked = pObj.checked;
                }
                var ckEditar = document.getElementById("chkEditar_" + lis[1]);
                if (ckEditar != null) {
                    ckEditar.checked = pObj.checked;
                }
                var ckEliminar = document.getElementById("chkEliminar_" + lis[1]);
                if (ckEliminar != null) {
                    ckEliminar.checked = pObj.checked;
                }
                ChekeoHijos(listaReglasRol[i].id, pObj.checked);
            }
            break;
        }
    }
}
function ChekeoHijos(pIdRegla, pValor) {
    for (var i = 0; i < listaReglasRol.length; i++) {
        if (listaReglasRol[i].id == pIdRegla) {
            for (var xy = 0; xy < listaReglasRol[i].listaIdHijas.length; xy++) {
                ChekearHijaYModificar(listaReglasRol[i].listaIdHijas[xy], pValor);
            }
            break;
        }
    }
}
function ChekearHijaYModificar(pIdRegla, pValor) {
    var ckActivar = document.getElementById("chkActivar_" + String(pIdRegla));
    if (ckActivar != null) {
        ckActivar.checked = pValor;
    }
    var ckAgregar = document.getElementById("chkAgregar_" + String(pIdRegla));
    if (ckAgregar != null) {
        ckAgregar.checked = pValor;
    }
    var ckEditar = document.getElementById("chkEditar_" + String(pIdRegla));
    if (ckEditar != null) {
        ckEditar.checked = pValor;
    }
    var ckEliminar = document.getElementById("chkEliminar_" + String(pIdRegla));
    if (ckEliminar != null) {
        ckEliminar.checked = pValor;
    }
    ChekeoHijos(pIdRegla, pValor);
}

function ChangeIndexComboRol(oList) {
    var sValue = oList.options[oList.selectedIndex].value;
    LimpiarReglas();
    listaReglasRolSelecciondo = [];
    PageMethods.RecuperarReglasPorRol(sValue, onCallBackRecuperarReglasPorRol, OnFail);
}
function LimpiarReglas() {
    for (var i = 0; i < listaReglasRol.length; i++) {
        var ckActivar = document.getElementById("chkActivar_" + String(listaReglasRol[i].id));
        if (ckActivar != null) {
            ckActivar.checked = false;
        }
        var ckAgregar = document.getElementById("chkAgregar_" + String(listaReglasRol[i].id));
        if (ckAgregar != null) {
            ckAgregar.checked = false;
        }
        var ckEditar = document.getElementById("chkEditar_" + String(listaReglasRol[i].id));
        if (ckEditar != null) {
            ckEditar.checked = false;
        }
        var ckEliminar = document.getElementById("chkEliminar_" + String(listaReglasRol[i].id));
        if (ckEliminar != null) {
            ckEliminar.checked = false;
        }
    }
}
function onCallBackRecuperarReglasPorRol(args) {
    var listaResultado = eval('(' + args + ')');
    listaReglasRolSelecciondo = listaResultado;

    if (listaReglasRolSelecciondo.length > 0) {
        for (var i = 0; i < listaReglasRolSelecciondo.length; i++) {

            var ckActivar = document.getElementById("chkActivar_" + String(listaReglasRolSelecciondo[i].idRegla));
            if (ckActivar != null) {
                listaReglasRolSelecciondo[i].isActivoModificado = listaReglasRolSelecciondo[i].isActivo;
                ckActivar.checked = listaReglasRolSelecciondo[i].isActivo;
            }
            var ckAgregar = document.getElementById("chkAgregar_" + String(listaReglasRolSelecciondo[i].idRegla));
            if (ckAgregar != null) {
                if (listaReglasRolSelecciondo[i].isAgregar != null) {
                    listaReglasRolSelecciondo[i].isAgregarModificado = listaReglasRolSelecciondo[i].isAgregar;
                    ckAgregar.checked = listaReglasRolSelecciondo[i].isAgregar;
                }
            }
            var ckEditar = document.getElementById("chkEditar_" + String(listaReglasRolSelecciondo[i].idRegla));
            if (ckEditar != null) {
                if (listaReglasRolSelecciondo[i].isEditar != null) {
                    listaReglasRolSelecciondo[i].isEditarModificado = listaReglasRolSelecciondo[i].isEditar;
                    ckEditar.checked = listaReglasRolSelecciondo[i].isEditar;
                }
            }
            var ckEliminar = document.getElementById("chkEliminar_" + String(listaReglasRolSelecciondo[i].idRegla));
            if (ckEliminar != null) {
                if (listaReglasRolSelecciondo[i].isEliminar != null) {
                    listaReglasRolSelecciondo[i].isEliminarModificado = listaReglasRolSelecciondo[i].isEliminar;
                    ckEliminar.checked = listaReglasRolSelecciondo[i].isEliminar;
                }
            }
        }
    }
    if (varIsEditar) {
        $("#divSectorArbol").removeAttr('disabled');
        $("#btnGuardar").removeAttr('disabled');
    }
}
function BuscarHijos(pIdRegla) {
    var resultado = [];

    for (var i = 0; i < listaReglasRol.length; i++) {
        if (listaReglasRol[i].idPadreRegla == pIdRegla) {
            resultado.push(listaReglasRol[i].id);
        }
    }
    return resultado;
}
function ExpandirOContraerNodo(pObj) {
    var lis = pObj.id.split('_');
    var listaRecorrido = [];

    listaRecorrido = BuscarHijos(parseInt(lis[1]));

    var strCss = '';
    var strCssModificar = '';

    strCss = document.getElementById("divImgBoton_" + String(lis[1])).className; // = 'cssDivImagenContraer';
    var strDisplay = '';
    if (strCss == 'cssDivImagenContraer') {
        strDisplay = 'none';
        strCssModificar = 'cssDivImagenExpandir';
    }
    else {
        strDisplay = '';
        strCssModificar = 'cssDivImagenContraer';
    }

    for (var i = 0; i < listaRecorrido.length; i++) {

        document.getElementById("divImgBoton_" + String(lis[1])).className = strCssModificar;
        document.getElementById("fila_" + String(listaRecorrido[i])).style.display = strDisplay;
        ExpandirOContraerNodoPorIdPadre(listaRecorrido[i], strDisplay, strCssModificar);
    }
}
function ExpandirOContraerNodoPorIdPadre(pIdReglaPadre, pStrDisplay, pStrCssModificar) {

    var listaRecorrido = BuscarHijos(pIdReglaPadre);
    for (var i = 0; i < listaRecorrido.length; i++) {

        document.getElementById("fila_" + String(listaRecorrido[i])).style.display = pStrDisplay;
        document.getElementById("divImgBoton_" + String(pIdReglaPadre)).className = pStrCssModificar;
        ExpandirOContraerNodoPorIdPadre(listaRecorrido[i], pStrDisplay, pStrCssModificar);
    }
}
function GraficarNodosHijos(pListaHijas) {

    var strTextoCompleto = '';
    for (var i = 0; i < pListaHijas.length; i++) {

        for (var x = 0; x < listaReglasRol.length; x++) {

            if (String(pListaHijas[i]) == String(listaReglasRol[x].id)) {
                if (!listaReglasRol[x].isGraficada) {

                    var NivelCss = listaReglasRol[x].Nivel * 20;
                    var strFila = "<tr id=\'fila_" + String(listaReglasRol[x].id) + "\'><td>";     
                    if (listaReglasRol[x].listaIdHijas.length > 0) {
                        strFila += "<div id=\'divImgBoton_" + String(listaReglasRol[x].id) + "\' style=\'margin-left: " + String(NivelCss) + "px\' class=\'cssDivImagenContraer\'  onclick=\'ExpandirOContraerNodo(this)\'> </div>";
                        NivelCss = NivelCss + 12;
                    }
                    strFila += "<div id=\'divImg_" + String(listaReglasRol[x].id) + "\' style=\'margin-left: " + String(NivelCss) + "px\'>" + listaReglasRol[x].descripcion + "</div></td>";

                    strFila += "<td class=\'cssCeldaCheckBox\'><input id=\'chkActivar_" + String(listaReglasRol[x].id) + "\' type=\'checkbox\' onClick=\'checkAll(this)\' /></td>";

                    if (listaReglasRol[x].checkAgregar == -1) {
                        strFila += "<td class=\'cssCeldaCheckBox\'> </td>";
                    } else if (listaReglasRol[x].checkAgregar == 0) {
                        strFila += "<td class=\'cssCeldaCheckBox\'><input id=\'chkAgregar_" + String(listaReglasRol[x].id) + "\' type='checkbox\' onClick=\'checkAll(this)\' /></td>";
                    }

                    if (listaReglasRol[x].checkEditar == -1) {
                        strFila += "<td class=\'cssCeldaCheckBox\'></td>";
                    } else if (listaReglasRol[x].checkEditar == 0) {
                        strFila += "<td class=\'cssCeldaCheckBox\'><input id=\'chkEditar_" + String(listaReglasRol[x].id) + "\' type='checkbox\' onClick=\'checkAll(this)\' /></td>";
                    }
                    if (listaReglasRol[x].checkEliminar == -1) {
                        strFila += "<td class=\'cssCeldaCheckBox\'></td>";
                    } else if (listaReglasRol[x].checkEliminar == 0) {
                        strFila += "<td class=\'cssCeldaCheckBox\'><input id=\'chkEliminar_" + String(listaReglasRol[x].id) + "\' type='checkbox\' onClick=\'checkAll(this)\' /> </td>";
                    }
                    strFila += "<td class=\'cssCeldaPalabraClave\'>" + listaReglasRol[x].palabra + "</td>";
                    strFila += "</tr>";

                    strTextoCompleto += strFila;
                    listaReglasRol[x].isGraficada = true;
                    strTextoCompleto += GraficarNodosHijos(listaReglasRol[x].listaIdHijas);
                }
                break;
            }
        }

    }
    return strTextoCompleto;
}
function ClickGuardar() {
    var listaReglaModificadas = [];
    for (var i = 0; i < listaReglasRol.length; i++) {
        var isEncontrado = false;
        for (var xy = 0; xy < listaReglasRolSelecciondo.length; xy++) {
            if (listaReglasRol[i].id == listaReglasRolSelecciondo[xy].idRegla) {
                var isReglaSeModifico = false;
                var re = new ReglaAGrabar();
                re.idRelacionReglaRol = listaReglasRolSelecciondo[xy].idRelacionReglaRol;
                re.idRegla = listaReglasRol[i].id;
                var ckActivar = document.getElementById("chkActivar_" + String(listaReglasRol[i].id));
                if (ckActivar != null) {
                    re.isActivo = ckActivar.checked;
                    if (ckActivar.checked != listaReglasRolSelecciondo[xy].isActivo) {
                        isReglaSeModifico = true;
                    }
                }
                //
                var ckAgregar = document.getElementById("chkAgregar_" + String(listaReglasRol[i].id));
                if (ckAgregar != null) {
                    re.isAgregado = ckAgregar.checked;
                    if (ckAgregar.checked != listaReglasRolSelecciondo[xy].isAgregar) {
                        isReglaSeModifico = true;
                    }
                }
                else {
                    re.isAgregado = null;
                }
                var ckEditar = document.getElementById("chkEditar_" + String(listaReglasRol[i].id));
                if (ckEditar != null) {
                    re.isEditado = ckEditar.checked;
                    if (ckEditar.checked != listaReglasRolSelecciondo[xy].isEditar) {
                        isReglaSeModifico = true;
                    }
                } else {
                    re.isEditado = null;
                }
                var ckEliminar = document.getElementById("chkEliminar_" + String(listaReglasRol[i].id));
                if (ckEliminar != null) {
                    re.isEliminado = ckEliminar.checked;
                    if (ckEliminar.checked != listaReglasRolSelecciondo[xy].isEliminar) {
                        isReglaSeModifico = true;
                    }
                } else {
                    re.isEliminado = null;
                }
                if (isReglaSeModifico) {
                    listaReglaModificadas.push(re);
                }
                isEncontrado = true;
                break;
            } // fin if (listaReglasRol[i].id == listaReglasRolSelecciondo[xy].idRegla) 
        } // fin for (var xy = 0; xy < listaReglasRolSelecciondo.length; xy++)

        if (!isEncontrado) {
            var isValeLaPenaGrabar = false;
            var reNueva = new ReglaAGrabar();
            reNueva.idRelacionReglaRol = -1;
            reNueva.idRegla = listaReglasRol[i].id;
            var ckActivar = document.getElementById("chkActivar_" + String(listaReglasRol[i].id));
            if (ckActivar != null) {
                reNueva.isActivo = ckActivar.checked;
                if (ckActivar.checked) {
                    isValeLaPenaGrabar = true;
                }
            }
            //
            var ckAgregar = document.getElementById("chkAgregar_" + String(listaReglasRol[i].id));
            if (ckAgregar != null) {
                reNueva.isAgregado = ckAgregar.checked;
                if (ckAgregar.checked) {
                    isValeLaPenaGrabar = true;
                }
            } else {
                reNueva.isAgregado = null;
            }
            var ckEditar = document.getElementById("chkEditar_" + String(listaReglasRol[i].id));
            if (ckEditar != null) {
                reNueva.isEditado = ckEditar.checked;
                if (ckEditar.checked) {
                    isValeLaPenaGrabar = true;
                }
            } else {
                reNueva.isEditado = null;
            }
            var ckEliminar = document.getElementById("chkEliminar_" + String(listaReglasRol[i].id));
            if (ckEliminar != null) {
                reNueva.isEliminado = ckEliminar.checked;
                if (ckEliminar.checked) {
                    isValeLaPenaGrabar = true;
                }
            } else {
                reNueva.isEliminado = null;
            }
            if (isValeLaPenaGrabar) {
                listaReglaModificadas.push(reNueva);
            }

        }
    } // fin for (var i = 0; i < listaReglasRol.length; i++)
    var lal = listaReglaModificadas;
    if (document.getElementById("cmbRoles").selectedIndex >= 0) {
        var sValue = document.getElementById("cmbRoles").options[document.getElementById("cmbRoles").selectedIndex].value;
        PageMethods.IsGrabarReglaRol(sValue, listaReglaModificadas, OnCallBackIsGrabarReglaRol, OnFail);
    }
    return false;
}
function OnCallBackIsGrabarReglaRol(args) {
    //    alert(String(args));
}
function ReglaAGrabar() {
    this.idRelacionReglaRol = 0;
    this.idRegla = 0;
    this.isActivo = false;
    this.isAgregado = false;
    this.isEditado = false;
    this.isEliminado = false;
}
