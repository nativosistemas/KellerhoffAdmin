<%@ Page Title="" Language="C#" MasterPageFile="~/master/BaseAdminJs3.master" AutoEventWireup="true" CodeFile="GestionProductoImagen.aspx.cs" Inherits="admin_pages_GestionProductoImagen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../includes/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../../includes/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../../includes/js/GestionProductoImagen.js" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            $('#txtBuscar').val($('#hiddenText').val());
            bus();
            $('#Form2').submit(function () {
                bus();
            return false;
            });

        });
        //document.getElementById('btnBuscar').onkeypress = function (e) {
        //    if (!e) e = window.event;
        //    var keyCode = e.keyCode || e.which;
        //    if (keyCode == '13') {
        //        bus();
        //        return false;
        //    }
        //}
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
 <h2 class="sub-header">Imagen para los productos</h2>
 <div class="form-group">
  <label for="txtBuscar">Palabra clave:</label>
  <input type="text" class="form-control" id="txtBuscar" />
</div>
  <button id="btnBuscar" type="button" class="btn btn-primary"  onclick="return bus();"   >Buscar</button>

    <div id="divContenedorGrilla"></div>

        <% AgregarHtmlOculto(); %>

</asp:Content>
