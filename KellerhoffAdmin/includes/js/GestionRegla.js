var isPostBack = false;
var listaArbolParaCombo = [];

jQuery(document).ready(function () {
    if (typeof (listaArbolParaComboLoad) != 'undefined') {
        listaArbolParaCombo = eval('(' + listaArbolParaComboLoad + ')');
        CargarCombo();
    } else {
        PageMethods.CargarArbolCombo(OnCallBackCargarArbolCombo, OnFail);
    }
    isPostBack = true;

});
function clickCerrarMensajes() {
    document.getElementById('divVentanaMensajes').style.display = 'none';
}
function LimpiarControlesSinComboReglaElegir() {
    $("#txtNombreReglaInput").val('');
    $("#txtPalabraClaveInput").val('');
    $('#txtPalabraClaveInput').removeAttr('disabled');
    $("#CheckBoxSoportadasAgregar").attr('checked', false);
    $("#CheckBoxSoportadasEditar").attr('checked', false);
    $("#CheckBoxSoportadasEliminar").attr('checked', false);
    $("#comboReglasContenidas").attr('selectedIndex', 0);
}
function CargarCombo() {
    var strHTMLcombo = '';
    var strHTMLcomboInicioContenida = '<select id="comboReglasContenidas"  class="anchoControlRegla" >';
    var strHTMLcomboInicioElegir = '<select id="comboReglasElegir" class="anchoControlRegla" onchange="clickComboElegir()">';
    if (varIsAgregar) {
        strHTMLcomboInicioElegir += "<option value=\'" + "0" + "\'>";
        strHTMLcomboInicioElegir += "<div >" + "<<( Nueva Regla )>>" + "</div>";
        strHTMLcomboInicioElegir += "</option>";
    }
    if (listaArbolParaCombo.length > 0) {
        for (var i = 0; i < listaArbolParaCombo.length; i++) {

            if (!listaArbolParaCombo[i].isGraficada) {
                strHTMLcombo += "<option value=\'" + listaArbolParaCombo[i].id + "\'>";
                strHTMLcombo += "<div >" + listaArbolParaCombo[i].descripcion + "</div>";
                strHTMLcombo += "</option>";
                listaArbolParaCombo[i].isGraficada = true;
                strHTMLcombo += RecuperarHTMLNodosHijosCombo(listaArbolParaCombo[i].listaIdHijas);
            }
        }
    }
    strHTMLcombo += '</select>';
    if (varIsAgregar) {
        document.getElementById('divCombo').innerHTML = strHTMLcomboInicioContenida + strHTMLcombo;
        $("#btnEliminar").attr('disabled', 'disabled');
    } else {
        document.getElementById('divCombo').innerHTML = "<select id=/'comboReglasContenidas/' class=/'anchoControlRegla/'  >" + obtenerRaizOptiom() + "</select>";
        PageMethods.RecuperarReglaRaiz(OnCallBackRecuperarReglaPorId, OnFail);
        $("#btnGuardar").attr('disabled', 'disabled');
        $("#btnEliminar").attr('disabled', 'disabled');
    }
    document.getElementById('divComboRegla').innerHTML = strHTMLcomboInicioElegir + strHTMLcombo;

}
function clickComboElegir() {
    document.getElementById('divVentanaMensajes').style.display = 'none';
    LimpiarControlesSinComboReglaElegir();   
    var idReglaSeleccionada = $("#comboReglasElegir").val();
    var CodigoPadreReglaSeleccionada = cargarComboContenidoModificar(idReglaSeleccionada);
    for (var i = 0; i < document.getElementById('comboReglasContenidas').length; i++) {
        if (document.getElementById('comboReglasContenidas')[i].value == CodigoPadreReglaSeleccionada) {
            document.getElementById('comboReglasContenidas')[i].selected = true;
            break;
        }
    }
    if (idReglaSeleccionada > 0) {
        PageMethods.RecuperarReglaPorId(idReglaSeleccionada, OnCallBackRecuperarReglaPorId, OnFail);
    } else {
//        $('#txtPalabraClaveInput').removeAttr('disabled');
        $("#btnEliminar").attr('disabled', 'disabled');
    }
}
function OnCallBackRecuperarReglaPorId(args) {
//    var fgg = args;
    if (args != null) {
        $("#txtNombreReglaInput").val(args.descripcion);
        $("#txtPalabraClaveInput").val(args.palabra);
        $('#txtPalabraClaveInput').attr('disabled', 'disabled');
        if (args.checkAgregar == 0) {
            $("#CheckBoxSoportadasAgregar").attr('checked', false);
        } else {
            $("#CheckBoxSoportadasAgregar").attr('checked', true);
        }
        if (args.checkEditar == 0) {
            $("#CheckBoxSoportadasEditar").attr('checked', false);
        } else {
            $("#CheckBoxSoportadasEditar").attr('checked', true);
        }
        if (args.checkEliminar == 0) {
            $("#CheckBoxSoportadasEliminar").attr('checked', false);
        } else {
            $("#CheckBoxSoportadasEliminar").attr('checked', true);
        }
        if (varIsEliminar) {
            if (args.listaIdHijas.length > 0) {
                $("#btnEliminar").attr('disabled', 'disabled');
            } else {
                $("#btnEliminar").removeAttr('disabled');
            }
        } else {
            $("#btnEliminar").attr('disabled', 'disabled');
        }
        if (varIsEditar) {
            $("#btnGuardar").removeAttr('disabled');
        } else {
            $("#btnGuardar").attr('disabled', 'disabled');
        }
    }
}
function cargarComboContenidoModificar(pIdRegla) {
    var isRaiz = false;
    var CodigoPadre = 0;
    for (var i = 0; i < listaArbolParaCombo.length; i++) {
        if (pIdRegla == listaArbolParaCombo[i].id) {
            if (listaArbolParaCombo[i].idPadreRegla == null) {
                isRaiz = true;
                break;
            }
        }
    }

    var strHTMLcombo = '';
    var strHTMLcomboInicioContenida = '<select id="comboReglasContenidas"  class="anchoControlRegla" >';
    if (!isRaiz) {
        for (var i = 0; i < listaArbolParaCombo.length; i++) {
            listaArbolParaCombo[i].isGraficada = false;
            if (listaArbolParaCombo[i].id == pIdRegla) {
                CodigoPadre = listaArbolParaCombo[i].idPadreRegla;
            }

        }
        if (listaArbolParaCombo.length > 0) {
            for (var i = 0; i < listaArbolParaCombo.length; i++) {
                if (!listaArbolParaCombo[i].isGraficada) {
                    if (listaArbolParaCombo[i].id != pIdRegla) {
                        strHTMLcombo += "<option value=\'" + listaArbolParaCombo[i].id + "\'>";
                        strHTMLcombo += "<div >" + listaArbolParaCombo[i].descripcion + "</div>";
                        strHTMLcombo += "</option>";
                        listaArbolParaCombo[i].isGraficada = true;
                        strHTMLcombo += RecuperarHTMLNodosHijosComboModificar(listaArbolParaCombo[i].listaIdHijas, pIdRegla);
                    } else {
                        listaArbolParaCombo[i].isGraficada = true;
                        MarcarComoGraficadas(listaArbolParaCombo[i].listaIdHijas);
                    }
                }
            }
        }
    } else {
    strHTMLcombo += obtenerRaizOptiom(); 
    }
    strHTMLcombo += '</select>';
    document.getElementById('divCombo').innerHTML = strHTMLcomboInicioContenida + strHTMLcombo;

    return CodigoPadre;
}

function obtenerRaizOptiom() {
    var strHTMLcombo = "";
    strHTMLcombo += "<option  value=\'" + "0" + "\'>";
    strHTMLcombo += "<div >" + "--- Root Node ---" + "</div>";
    strHTMLcombo += "</option>";
    return strHTMLcombo;
}


function MarcarComoGraficadas(pListaHijas) {
    for (var i = 0; i < pListaHijas.length; i++) {
        for (var x = 0; x < listaArbolParaCombo.length; x++) {
            if (!listaArbolParaCombo[x].isGraficada) {
                if (pListaHijas[i] == listaArbolParaCombo[x].id) {
                    listaArbolParaCombo[x].isGraficada = true;
                    MarcarComoGraficadas(listaArbolParaCombo[x].listaIdHijas);
                }
            }
        }
    }
}

function RecuperarHTMLNodosHijosComboModificar(pListaHijas, pIdRegla) {
    var strTextoCompleto = '';
    for (var i = 0; i < pListaHijas.length; i++) {
        for (var x = 0; x < listaArbolParaCombo.length; x++) {
            if (!listaArbolParaCombo[x].isGraficada) {

                if (pListaHijas[i] == listaArbolParaCombo[x].id) {
                    if (pIdRegla != pListaHijas[i]) {
                        strTextoCompleto += "<option value=\'" + listaArbolParaCombo[x].id + "\'";
                        strTextoCompleto += "<div>";
                        for (var xy = 0; xy < listaArbolParaCombo[x].Nivel; xy++) {
                            strTextoCompleto += "&nbsp; &nbsp;"
                        }
                        strTextoCompleto += listaArbolParaCombo[x].descripcion + "</div>";
                        strTextoCompleto += "</option>";
                        listaArbolParaCombo[x].isGraficada = true;
                        strTextoCompleto += RecuperarHTMLNodosHijosComboModificar(listaArbolParaCombo[x].listaIdHijas, pIdRegla);
                    } else {
                        listaArbolParaCombo[x].isGraficada = true;
                        MarcarComoGraficadas(listaArbolParaCombo[x].listaIdHijas);
                    }
                }

            }
        }
    }
    return strTextoCompleto;
}

function RecuperarHTMLNodosHijosCombo(pListaHijas) {
    var strTextoCompleto = '';
    for (var i = 0; i < pListaHijas.length; i++) {
        for (var x = 0; x < listaArbolParaCombo.length; x++) {
            if (!listaArbolParaCombo[x].isGraficada) {
                if (pListaHijas[i] == listaArbolParaCombo[x].id) {
                    strTextoCompleto += "<option value=\'" + listaArbolParaCombo[x].id + "\'";
                    strTextoCompleto += "<div >";
                    for (var xy = 0; xy < listaArbolParaCombo[x].Nivel; xy++) {
                        strTextoCompleto += " &nbsp; &nbsp;"
                    }
                    strTextoCompleto += listaArbolParaCombo[x].descripcion + "</div>";
                    strTextoCompleto += "</option>";
                    listaArbolParaCombo[x].isGraficada = true;
                    strTextoCompleto += RecuperarHTMLNodosHijosCombo(listaArbolParaCombo[x].listaIdHijas);
                }
            }
        }
    }
    return strTextoCompleto;
}

function clikGuardar() {
    var idReglaSeleccionada = $("#comboReglasElegir").val();
    if (idReglaSeleccionada == 0) {
        // Es nueva la regla
        ValidarRegla();
    } else {
        // Es una regla existente
        if (varIsEditar) {
            ValidarReglaParaModificar(idReglaSeleccionada);
        } else {
            alert('No tiene permiso para modificar');
        }
    }
    return false;
}
function ValidarReglaParaModificar(pIdRegla) {
    var strHtmlMensaje = '';
    var NombreRegla = $("#txtNombreReglaInput").val();
    if (NombreRegla == '') {
        strHtmlMensaje += "<p>" + "Descripción es campo requerido" + "</p>";
    }
    if ($("#comboReglasContenidas").val() == null) {
        strHtmlMensaje += "<p>" + "No hay regla raiz" + "</p>";
    }
    if (strHtmlMensaje != '') {
        $('#pMensaje').html(strHtmlMensaje);
        $('#divVentanaMensajes').show("slow");
    } else {
        PageMethods.IsNombreOPalabraNoSeRepite(pIdRegla, String($("#txtNombreReglaInput").val()), String($("#txtPalabraClaveInput").val()), OnCallBackIsNombreOPalabraNoSeRepiteModificar, OnFail);
    }
}
function OnCallBackIsNombreOPalabraNoSeRepiteModificar(args) {
    var strHtmlMensaje = '';
    if (!args[0]) {
        strHtmlMensaje += "<p>" + "Descripción no puede repetirse" + "</p>";
    }
    if (strHtmlMensaje != '') {
        $('#pMensaje').html(strHtmlMensaje);
        $('#divVentanaMensajes').show("slow");

    } else {
        $('#divVentanaMensajes').css('display', 'none');
        PageMethods.ActualizarRegla($("#comboReglasElegir").val(), $("#txtNombreReglaInput").val(), String($("#txtPalabraClaveInput").val()), $("#CheckBoxSoportadasAgregar").is(':checked'), $("#CheckBoxSoportadasEditar").is(':checked'), $("#CheckBoxSoportadasEliminar").is(':checked'), $("#comboReglasContenidas").val(), OnCallBackActualizarRegla, OnFailActualizarRegla);
    }

}
function OnCallBackActualizarRegla(args) {
    if (args) {
        LimpiarControlesSinComboReglaElegir();
        PageMethods.CargarArbolCombo(OnCallBackCargarArbolCombo, OnFail);
    }
    else {
        var strHtmlMensaje = '';
        strHtmlMensaje += "<p>" + "No se pudo actualizar la regla en el sistema" + "</p>";
        $('#pMensaje').html(strHtmlMensaje);
        $('#divVentanaMensajes').show("slow");
    }
}
function OnFailActualizarRegla(ex) {
    $('#txtPalabraClaveInput').removeAttr('disabled');
    alert(String(ex));
}
function ValidarRegla() {
    var strHtmlMensaje = '';
    var er = new RegExp(/\s/);
    var palaClave = $("#txtPalabraClaveInput").val();
    if (palaClave != '') {
        if (er.test(palaClave)) {
            strHtmlMensaje += "<p>" + "Palabra clave no tiene que tener espacio en blanco" + "</p>";
        }
    } else {
        strHtmlMensaje += "<p>" + "Palabra clave es campo requerido" + "</p>";
    }
    var NombreRegla = $("#txtNombreReglaInput").val();
    if (NombreRegla == '') {
        strHtmlMensaje += "<p>" + "Descripción es campo requerido" + "</p>";
    }
    if ($("#comboReglasContenidas").val() == null) {
        strHtmlMensaje += "<p>" + "No hay regla raiz" + "</p>";
    }

    if (strHtmlMensaje != '') {
        $('#pMensaje').html(strHtmlMensaje);
        $('#divVentanaMensajes').show("slow");
    } else {
        PageMethods.IsNombreOPalabraNoSeRepite(0, String($("#txtNombreReglaInput").val()), String($("#txtPalabraClaveInput").val()), OnCallBackIsNombreOPalabraNoSeRepite, OnFail);
    }
}

function OnCallBackIsNombreOPalabraNoSeRepite(args) {
    var strHtmlMensaje = '';
    if (!args[0]) {
        strHtmlMensaje += "<p>" + "Descripción no puede repetirse" + "</p>";
    }
    if (!args[1]) {
        strHtmlMensaje += "<p>" + "Palabra clave no puede repetirse" + "</p>";
    }
    if (strHtmlMensaje != '') {
        $('#pMensaje').html(strHtmlMensaje);
        $('#divVentanaMensajes').show("slow");

    } else {
        $('#divVentanaMensajes').css('display', 'none');
        PageMethods.InsertarRegla($("#txtNombreReglaInput").val(), $("#txtPalabraClaveInput").val(), $("#CheckBoxSoportadasAgregar").is(':checked'), $("#CheckBoxSoportadasEditar").is(':checked'), $("#CheckBoxSoportadasEliminar").is(':checked'), $("#comboReglasContenidas").val(), OnCallBackInsertarRegla, OnFail);
    }
}
function OnCallBackInsertarRegla(args) {
    if (args) {
        LimpiarControlesSinComboReglaElegir();
        PageMethods.CargarArbolCombo(OnCallBackCargarArbolCombo, OnFail);
    }
    else {
        var strHtmlMensaje = '';
        strHtmlMensaje += "<p>" + "No se pudo grabar en el sistema" + "</p>";
        $('#pMensaje').html(strHtmlMensaje);
        $('#divVentanaMensajes').show("slow");
    }
}
function OnCallBackCargarArbolCombo(args) {
    listaArbolParaCombo = eval('(' + args + ')');
    CargarCombo();
}
function clikEliminar() {
    if (varIsEliminar) {
        if (confirm('¿Desea eliminar la regla?')) {
            PageMethods.EliminarRegla($("#comboReglasElegir").val(), OnCallBackEliminarRegla, OnFail);
        }
    } else {
        alert('No tiene permiso de eliminar regla');
    }
    return false;
}
function OnCallBackEliminarRegla(args) {
    if (args) {
        LimpiarControlesSinComboReglaElegir();
        PageMethods.CargarArbolCombo(OnCallBackCargarArbolCombo, OnFail);
    } else {
        alert('No se pudo eliminar');
    }
}
