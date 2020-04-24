<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GestionMensajeV3VistaPreviaV2.aspx.cs" Inherits="admin_pages_GestionMensajeV3VistaPreviaV2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Droguería Kellerhoff</title>

    <meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
    <meta name="robots" content="noindex,nofollow,noarchive,noodp,nosnippet" />
    <link href="~/includes/css/general.css" rel="stylesheet" type="text/css" />
    <link href="~/includes/css/abm.css" rel="stylesheet" type="text/css" />
    <script src="../../includes/js/jquery-3.1.0.min.js" type="text/javascript"></script>

    <link rel="stylesheet" type="text/css"  href="~/includes/css/jquery-confirm.min.css"/>
    <script src="../../includes/js/jquery-confirm.min.js" type="text/javascript"></script>

    <script src="../../includes/js/Libreria.js?n=1" type="text/javascript"></script>
    <link rel="shortcut icon" href="../favicon.ico" />
        <link href="../../includes/css/VistaPrevia.css" rel="stylesheet" />    
           <script src="../../includes/js/bootstrap.min.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script src="../../includes/js/GestionMensajeV3.js?n=8" type="text/javascript"></script>
            <script type="text/javascript">
                jQuery(document).ready(function () {
                    MostrarMensaje_vistaPrevia();
            //MostrarMensaje_aux();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
             <div id="modalModulo" class="modal md-effect-1 md-content portfolio-modal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true"></div>

            <%  AgregarHtmlOculto(); %>
    </div>
    </form>
</body>
</html>
