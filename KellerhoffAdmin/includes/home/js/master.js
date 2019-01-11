var isIngresarPageMethods = false;
var name = null;
var pass = null;
jQuery(document).ready(function () {
//        localStorage['name'] = name;
    //localStorage['pass'] = pass;
    	
 

    var myName = localStorage['name'] || '';
    var myPass = localStorage['pass'] || '';
    $('#login_password').val(myPass);
    $('#login_name').val(myName);

    $('#password_footer').val(myPass);
    $('#name_footer').val(myName);
});

function onkeypressIngresar(e) {
    if (!e) e = window.event;
    var keyCode = e.keyCode || e.which;
    if (keyCode == '13') {
        // Enter pressed
        onclickIngresar();
        return false;
    }
}
function onkeypressIngresarAbajo(e) {
    if (!e) e = window.event;
    var keyCode = e.keyCode || e.which;
    if (keyCode == '13') {
        // Enter pressed
        onclickIngresarAbajo();
        return false;
    }
}
function onkeypressIngresarDesdeAgregarCarrito(e) {
    if (!e) e = window.event;
    var keyCode = e.keyCode || e.which;
    if (keyCode == '13') {
        // Enter pressed
        onclickIngresarDesdeAgregarCarrito();
        return false;
    }
}

function onclickIngresar() {
    if (!isIngresarPageMethods) {
        isIngresarPageMethods = true;
         name = $('#login_name').val();
         pass = $('#login_password').val();

        PageMethods.login(name, pass, OnCallBackLogin, OnFailLogin);
    }
    return false;
}
function onclickIngresarAbajo() {
    if (!isIngresarPageMethods) {
        isIngresarPageMethods = true;
         name = $('#name_footer').val();
         pass = $('#password_footer').val();
        PageMethods.login(name, pass, OnCallBackLogin, OnFailLogin);
    }
    return false;
}
function OnFailLogin(ex) {
    isIngresarPageMethods = false;
    OnFail(ex);
}
var idOferta = null;

function IngresarDsdMobil() {
    idOferta = -1;
    var myName = localStorage['name'] || '';
    var myPass = localStorage['pass'] || '';
    $('#name_carrito').val(myName);
    $('#password_carrito').val(myPass);
    $('#myModal').modal();
}
function onclickIngresarDesdeAgregarCarrito() {
     name = $('#name_carrito').val();
     pass = $('#password_carrito').val();
    PageMethods.loginCarrito(name, pass, idOferta, OnCallBackLoginCarrito, OnFail);

    return false;
}
function OnCallBackLoginCarrito(args) {
    if (args == 'Ok') {
        localStorage['name'] = name;
        localStorage['pass'] = pass;
        // Logeo correcto
        idOferta = null;
        location.href = '../clientes/pages/PedidosBuscador.aspx';
    }
    else {
        // bootstrap_alert.warning(args);
        $.alert({
            title: 'Información',
            content: args,
        });
    }
}
function OnCallBackLogin(args) {
    isIngresarPageMethods = false;
    //$('#success').removeClass('alert-danger');
    //$('#success').html('');
    if (args == 'Ok') {
        localStorage['name'] = name;
        localStorage['pass'] = pass;
        // Logeo correcto
        location.href = '../clientes/pages/PedidosBuscador.aspx';
    }
    else {
        // bootstrap_alert.warning(args);
        $.alert({
            title: 'Información',
            content: args,
        });
        //BootstrapDialog.show({
        //    title: 'Información',
        //    message: args
        //});
        //$('#success').addClass('alert-danger');
        //$('#success').html(args);
        // Mostrar mensaje
    }
}
