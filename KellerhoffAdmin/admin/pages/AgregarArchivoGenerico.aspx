<%@ Page Title="" Language="C#" MasterPageFile="~/master/BaseAdminJs3.master" AutoEventWireup="true" CodeFile="AgregarArchivoGenerico.aspx.cs" Inherits="admin_pages_AgregarArchivoGenerico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../includes/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../../includes/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../../includes/js/AgregarArchivoGenerico.js?n=3" type="text/javascript"></script>
    <script type="text/javascript">

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h2 class="sub-header">Agregar archivo
    </h2>
    <div class="form-group">
        <label for="txt_titulo">Título:</label>
        <input type="text" class="form-control" id="txt_titulo" name="txt_titulo" />
    </div>
    <div class="form-group" style="display: none">
        <label for="txt_descr">Descripción:</label>
        <input type="text" class="form-control" id="txt_descr" name="txt_descr" />
    </div>

    <div id="divContenedorTodasSucursales" class="form-group" style="display: none">
        <input type="hidden" id="hiddenSucursales" name="hiddenSucursales" />
        <label>Sucursales:</label>
       <div id="divTodasSucursales" ></div>
    </div>
    <div id="divContenedorArchivoGenerico" class="form-group" style="display: none">
        <label for="txt_descr">Archivo:</label>
        <div id="divArchivoGenerico"></div>
    </div>
    <asp:FileUpload ID="FileUpload1" runat="server" />
    <br />
    <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
    <br />
    <button type="submit" class="btn btn-primary" onclick="javascript:sendFormSubir();">Subir</button>
    <button type="button" class="btn btn-primary" onclick="onclickVolverAgregarArchivo(); return false;">Volver</button>
    <% AgregarHtmlOculto(); %>
</asp:Content>

