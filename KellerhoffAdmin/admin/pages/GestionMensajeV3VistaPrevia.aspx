<%@ Page Title="" Language="C#" MasterPageFile="~/master/BaseAdminJs3.master" AutoEventWireup="true" CodeFile="GestionMensajeV3VistaPrevia.aspx.cs" Inherits="admin_pages_GestionMensajeV3VistaPrevia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<%--        <link rel="stylesheet" type="text/css" href="lib/css/bootstrap.min.css" />
        <link href="../../includes/css/vistaPreviaReducido.css" rel="stylesheet" />--%>
    <link href="../../includes/css/VistaPrevia.css" rel="stylesheet" />    
           <script src="../../includes/js/bootstrap.min.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script src="../../includes/js/GestionMensajeV3.js?n=2" type="text/javascript"></script>
            <script type="text/javascript">
        jQuery(document).ready(function () {
            MostrarMensaje_aux();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
         <div id="modalModulo" class="modal md-effect-1 md-content portfolio-modal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true"></div>

</asp:Content>

