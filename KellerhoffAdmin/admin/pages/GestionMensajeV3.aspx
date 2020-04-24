<%@ Page Title="" Language="C#" MasterPageFile="~/master/BaseAdminJs3.master" AutoEventWireup="true" CodeFile="GestionMensajeV3.aspx.cs" Inherits="admin_pages_GestionMensajeV3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <link href="../../includes/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../../includes/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../../includes/js/GestionMensajeV3.js?n=6" type="text/javascript"></script>
        <script type="text/javascript">
        jQuery(document).ready(function () {
            RecuperarTodosMensajeNew();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
         <h2 class="sub-header">Alertas</h2>
    <button type="button" class="btn btn-warning" onclick="return EditarMensaje(0);">Agregar</button>
     <div id="divContenedorGrilla"></div>
</asp:Content>

