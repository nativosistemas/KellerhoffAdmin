var objSucursalDependienteTipoEnvios = null;
var listaTipoEnvio = null;
var listaTodosTiposEnvios = null;
var listaTodosCodigoReparto = null;

function CargarDatosReparto() {

    listaTipoEnvio = eval('(' + $('#hiddenListaTodosTiposEnvios').val() + ')');
    listaTodosTiposEnvios = eval('(' + $('#hiddenListaTodosTiposEnvios').val() + ')');
    listaTodosCodigoReparto = eval('(' + $('#hiddenListaTodosCodigoReparto').val() + ')');
    if (listaTodosCodigoReparto != null) {
        var comboReparto = document.getElementById("cmdCodigoRepartos");
        for (var i = 0; i < listaTodosCodigoReparto.length; i++) {
            var option = document.createElement("option");
            option.text = listaTodosCodigoReparto[i];
            comboReparto.add(option);
        }
    }
    if (listaTipoEnvio != null) {
        var comboTipoEnvio = document.getElementById("cmdTipoEnvio");
        for (var i = 0; i < listaTipoEnvio.length; i++) {
            var option = document.createElement("option");
            option.text = listaTipoEnvio[i].env_nombre;
            option.value = listaTipoEnvio[i].env_id;
            comboTipoEnvio.add(option);
        }
    }
    objSucursalDependienteTipoEnvios = eval('(' + $('#hiddenSucursalDependienteTipoEnvios').val() + ')');
    if (objSucursalDependienteTipoEnvios != null) {
        $('#txtSucursalDependiente').val(objSucursalDependienteTipoEnvios.sde_sucursal + " - " + objSucursalDependienteTipoEnvios.sde_sucursalDependiente);
        $('#txtTipoEnvioCliente').val(objSucursalDependienteTipoEnvios.env_nombre);//env_nombre
        var varHtml = '<ul>';
        for (var i = 0; i < listaTipoEnvio.length; i++) {
            varHtml += '<li>' + listaTipoEnvio[i].env_nombre + '</li>';
        }
        varHtml += '</ul>';
        $('#divTipoEnvio').html(varHtml);
    }
    onchangeCodigoRepartos();
}
function VolverGestionTiposEnviosSucursal() {
    location.href = 'GestionTiposEnviosSucursal.aspx';
}

function AgregarRepartoExcepcion() {
    var comboTipoEnvio = document.getElementById("cmdTipoEnvio");
    var comboTipoEnvioMostrar = document.getElementById("cmdTipoEnvioMostrar");
    if (comboTipoEnvio.selectedIndex > -1) {
        var isGrabar = true;
        for (var i = 0; i < comboTipoEnvioMostrar.options.length; i++) {
            if (comboTipoEnvioMostrar.options[i].value == comboTipoEnvio.options[comboTipoEnvio.selectedIndex].value) {
                isGrabar = false;
                break;
            }
        }

        if (isGrabar) {
            var comboReparto = document.getElementById("cmdCodigoRepartos");

            PageMethods.InsertarExcepciones(objSucursalDependienteTipoEnvios.tsd_id, comboTipoEnvio.options[comboTipoEnvio.selectedIndex].value, comboReparto.options[comboReparto.selectedIndex].value, OnCallBackInsertarExcepciones, OnFailExcepcion);
            bloquearPantalla();
            var option = document.createElement("option");
            option.text = comboTipoEnvio.options[comboTipoEnvio.selectedIndex].text;
            option.value = comboTipoEnvio.options[comboTipoEnvio.selectedIndex].value;
            comboTipoEnvioMostrar.add(option);
        }
    }
    return false;
}
function EliminarRepartoExcepcion() {
    var comboTipoEnvioMostrar = document.getElementById("cmdTipoEnvioMostrar");
    if (comboTipoEnvioMostrar.selectedIndex > -1) {
        var comboReparto = document.getElementById("cmdCodigoRepartos");
        PageMethods.EliminarExcepciones(objSucursalDependienteTipoEnvios.tsd_id, comboTipoEnvioMostrar.options[comboTipoEnvioMostrar.selectedIndex].value, comboReparto.options[comboReparto.selectedIndex].value, OnCallBackEliminarExcepciones, OnFailExcepcion);
        bloquearPantalla();
        comboTipoEnvioMostrar.remove(comboTipoEnvioMostrar.selectedIndex);

    }
    return false;

}
function bloquearPantalla() {
    if (document.getElementById('divMasterContenedorGeneralFondo').style.display == 'none') {
        var arraySizeDocumento = SizeDocumento();
        document.getElementById('divMasterContenedorGeneralFondo').style.height = arraySizeDocumento[1] + 'px';
        document.getElementById('divMasterContenedorGeneralFondo').style.display = 'block';
    }
}
function desbloquearPantalla() {
    setTimeout("desbloquearPantalla_basic()", 500);

}

function desbloquearPantalla_basic() {

    document.getElementById('divMasterContenedorGeneralFondo').style.display = 'none';
}
function OnCallBackEliminarExcepciones(args) {
    //desbloquearPantalla();
    onchangeCodigoRepartos();
}
function OnCallBackInsertarExcepciones(args) {
    //desbloquearPantalla();
    onchangeCodigoRepartos();
}
function OnFailExcepcion(er) {
    desbloquearPantalla();
}
function onchangeCodigoRepartos() {
    var comboReparto = document.getElementById("cmdCodigoRepartos");
    PageMethods.RecuperarExcepciones(objSucursalDependienteTipoEnvios.tsd_id, comboReparto.options[comboReparto.selectedIndex].value, OnCallBackRecuperarExcepciones, OnFailExcepcion)
    bloquearPantalla();
}
function OnCallBackRecuperarExcepciones(args) {
    var comboTipoEnvioMostrar = document.getElementById("cmdTipoEnvioMostrar");
    var length = comboTipoEnvioMostrar.options.length;
    for (i = length - 1; i >= 0; i--) {
        comboTipoEnvioMostrar.options[i] = null;
    }
    var listaTemp = eval('(' + args + ')');

    for (var i = 0; i < listaTemp.length; i++) {
        var option = document.createElement("option");
        option.text = listaTemp[i].nombre;
        option.value = listaTemp[i].id;
        comboTipoEnvioMostrar.add(option);
    }
    RecuperarTodasExcepciones();
}

function RecuperarTodasExcepciones() {

    PageMethods.RecuperarTodasExcepciones(objSucursalDependienteTipoEnvios.tsd_id, OnCallBackRecuperarTodasExcepciones, OnFailExcepcion)
    //bloquearPantalla();
    //  desbloquearPantalla();
}
function OnCallBackRecuperarTodasExcepciones(args) {
    var listaTemp = eval('(' + args + ')');
    var varHtml = '<ul>';
    for (var i = 0; i < listaTemp.length; i++) {
        //var option = document.createElement("option");
        //option.text = listaTemp[i].nombre;
        //option.value = listaTemp[i].id;
        //comboTipoEnvioMostrar.add(option);
        varHtml += '<li>' + '<strong>' + listaTemp[i].tdr_codReparto + '</strong>' + ' - ' + listaTemp[i].env_nombre + '</li>';
    }
    varHtml += '</ul>';

    $('#divExcepciones').html(varHtml);

    desbloquearPantalla();
}