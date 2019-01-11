<%@ Page Title="" Language="C#" MasterPageFile="~/master/BaseAdminJs3.master" AutoEventWireup="true" CodeFile="GestionPopUp.aspx.cs" Inherits="admin_pages_GestionPopUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../includes/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../../includes/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../../includes/js/GestionPopUp.js" type="text/javascript"></script>
            <script type="text/javascript">
        jQuery(document).ready(function () {
            RecuperarTodosArchivos();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2 class="sub-header">PopUp</h2>
    <button type="button" class="btn btn-warning" onclick="return AgregarPopUp();">Agregar</button>
     <div id="divContenedorGrilla"></div>

</asp:Content>

