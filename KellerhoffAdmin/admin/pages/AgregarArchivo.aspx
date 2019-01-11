<%@ Page Title="" Language="C#" MasterPageFile="~/master/BaseAdminJs3.master" AutoEventWireup="true" CodeFile="AgregarArchivo.aspx.cs" Inherits="admin_pages_AgregarArchivo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
       <link href="../../includes/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../../includes/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../../includes/js/AgregarArchivo.js" type="text/javascript"></script>
    <script type="text/javascript">

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
        <h2 class="sub-header">Agregar imagen
    </h2>
    <div class="form-group">
        <label for="txt_titulo">Título:</label>
        <input type="text" class="form-control" id="txt_titulo" disabled="disabled" />
    </div>
    <div class="form-group">
        <label for="txt_descr">Descripción:</label>
        <input type="text" class="form-control" id="txt_descr" disabled="disabled" />
    </div>
    <div id="divContenedorImg" class="form-group" style="display:none">
        <label for="txt_descr">Imagen:</label>
         <div id="divImg"></div>
    </div>
    <asp:FileUpload ID="FileUpload1" runat="server"  />
        <br /> 
    <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
    <br />
    <button type="submit" class="btn btn-primary">Subir</button>
    <button type="button" class="btn btn-primary" onclick="onclickVolverAgregarArchivo(); return false;">Volver</button>
    <% AgregarHtmlOculto(); %>
</asp:Content>

