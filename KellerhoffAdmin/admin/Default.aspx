<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="admin_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Iniciar sesión</title>
      <meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
      <meta name="robots" content="noindex,nofollow,noarchive,noodp,nosnippet" />
    <style type="text/css">
        body
        {
            font-size: 11px;
            font-family: "Helvetica Neue" , "Lucida Grande" , "Segoe UI" , Arial, Helvetica, Verdana, sans-serif;
        }
        .contenedorLogin
        {
            width: 380px;
            margin: 0px auto;
            margin-top: 200px;
        }
        .boton
        {
            border: #cccccc 1px solid;
            background: #f0f0f0;
            float: right;
        }
        .csslbl
        {
            text-align: right;
        }
        .divParte
        {
            padding: 5px;
        }
        .mensaje
        {
              margin-left:130px;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="contenedorLogin">
        <div class="divParte">
            <asp:Label ID="lblMail" runat="server" Text="Usuario:" Width="130" CssClass="csslbl"></asp:Label>
            <asp:TextBox ID="txtUsuario" runat="server" Width="230"></asp:TextBox>
        </div>
        <div class="divParte">
            <asp:Label ID="lblPassword" runat="server" Text="Contraseña:" Width="130" CssClass="csslbl"></asp:Label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="230"></asp:TextBox>
        </div>
        <div class="mensaje">
            <div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorNombre" runat="server" ErrorMessage="Usuario requerido"
                    ControlToValidate="txtUsuario" ForeColor="Red"></asp:RequiredFieldValidator></div>
            <div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Contraseña requerida"
                    ControlToValidate="txtPassword" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            <div>
                            <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
            </div>
    </div>
    <div class="divParte">
        <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" CssClass="boton" 
            onclick="btnIngresar_Click" />
    </div>
    </div>
    </form>
</body>
</html>
