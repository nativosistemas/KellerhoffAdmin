<%@ Page Title="" Language="C#" MasterPageFile="~/master/BaseAdminJs3.master" AutoEventWireup="true" CodeFile="GestionCurriculumVitae_v2.aspx.cs" Inherits="admin_pages_GestionCurriculumVitae_v2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../includes/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../../includes/js/bootstrap.min.js" type="text/javascript"></script>
        <script src="../../includes/js/paginador_admin.js?n=3" type="text/javascript"></script>
    <script src="../../includes/js/GestionCurriculumVitae_v2.js?n=3" type="text/javascript"></script>

    <script type="text/javascript">
        jQuery(document).ready(function () {
            RecuperarTodasCurriculumVitae();
        });
        //$('#txtBuscador').keypress(function (e) {
        //    if (e.keyCode == '13') {
        //        e.preventDefault();
        //        onclickBuscarCV();
        //    }
        //});​
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h2 class="sub-header">Curriculum Vitae</h2>
    <%--    <div><label for="txtBuscador">Buscar</label><input id="txtBuscador" type="text" class="form-control" /></div>--%>
    <div>
        <table>
            <tr>
                <td>
                    <div class="form-group">
                        <label for="txtBuscador">Nombre</label>
                        <input id="txtBuscador" type="text" class="form-control" placeholder="Ingrese el texto" onkeypress="if (event.keyCode == 13) {onclickBuscarCV(); return false;}" />
                    </div>
                </td>
                <td>
                    <div class="form-group">
                        <label for="puesto_cv">Puesto</label>
                        <select name="puesto_cv" id="puesto_cv" class="form-control">
                            <option value=""></option>
                            <option value="VENTAS">VENTAS</option>
                            <option value="ADMINISTRACIÓN">ADMINISTRACIÓN</option>
                            <option value="CONTABLES">CONTABLES</option>
                            <option value="SISTEMAS">SISTEMAS</option>
                            <option value="DEPOSITO">DEPOSITO</option>
                            <option value="TOMA DE PEDIDOS">TOMA DE PEDIDOS</option>
                        </select>
                    </div>
                </td>
                <td>
                    <div class="form-group">
                        <label for="sucursal_cv">Sucursal</label>
                        <select name="sucursal_cv" id="sucursal_cv" class="form-control">
                            <option value=""></option>
                            <option value="ROSARIO">ROSARIO</option>
                            <option value="CHAÑAR LADEADO">CHAÑAR LADEADO</option>
                            <option value="CONCEPCIÓN DEL URUGUAY">CONCEPCIÓN DEL URUGUAY</option>
                            <option value="CONCORDIA">CONCORDIA</option>
                            <option value="CÓRDOBA">CÓRDOBA</option>
                            <option value="SAN NICOLAS">SAN NICOLAS</option>
                            <option value="SANTA FE">SANTA FE</option>
                            <option value="VILLA MARÍA">VILLA MARÍA</option>
                            <option value="RÍO CUARTO">RÍO CUARTO</option>
                        </select>
                    </div>
                </td>
                <td>
                    <div class="input-group-append">
                        <button class="btn " type="button" onclick="onclickBuscarCV(); return false;">Buscar</button>
                    </div>
                </td>
            </tr>
        </table>




    </div>


    <div id="divContenedorGrilla"></div>
        <div id="divPaginador">
    </div>
    <%AgregarHtmlOculto(); %>
</asp:Content>

