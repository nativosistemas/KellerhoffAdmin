<%@ Page Title="" Language="C#" MasterPageFile="~/master/BaseAdminJs3.master" AutoEventWireup="true" CodeFile="GestionOferta.aspx.cs" Inherits="admin_pages_GestionOferta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../includes/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../../includes/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../../includes/js/GestionOferta.js?n=2" type="text/javascript"></script>
        <script type="text/javascript">
        jQuery(document).ready(function () {
            RecuperarTodasOfertas();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
     <h2 class="sub-header">Ofertas</h2>
    <button type="button" class="btn btn-warning" onclick="return EditarOferta(0);">Agregar</button>
     <div id="divContenedorGrilla"></div>
     <%--<button type="button" class="btn btn-warning" onclick="return IrVistaPrevia();">Vista Previa</button>--%>
</asp:Content>

