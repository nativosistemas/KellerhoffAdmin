<%@ Page Title="" Language="C#" MasterPageFile="~/master/BaseAdmin.master" AutoEventWireup="true"
    CodeFile="GestionProcedimientos.aspx.cs" Inherits="admin_pages_GestionProcedimientos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css" >
.lblFechasUltimasActualizacion
{
    color:Green;    
    }
</style>
    <script type="text/javascript" language="javascript">
        function DeseaSincronizar() {
            return confirm('¿Desea sincronizar?');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="titulo_pagina">
        Procedimientos</div>
    <asp:UpdateProgress ID="up_prog_0" AssociatedUpdatePanelID="up_general" runat="server">
        <ProgressTemplate>
            <div class="div_loading">
                <table border="0">
                    <tr>
                        <td>
                            <asp:Image ID="img_loader" runat="server" ImageUrl='~/img/varios/ajax-loader.gif' />
                        </td>
                        <td>
                            Espere por favor!
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="up_general" runat="server">
        <ContentTemplate>
            <asp:Panel ID="panelMensaje" runat="server" Visible="false">
                <div style="width: 400px; margin: auto; font-size: 20px;">
                    <div style="text-align: right;">
                        <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="btn_abm" OnClick="btnVolver_Click" />
                    </div>
                    <div>
                        Se proceso la sincronización
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="panelBotonesSicronizar" runat="server">
                <div style="width: 600px; margin: auto;">
                    <asp:Button ID="btnClientes" runat="server" Text="Sincronizar clientes" CssClass="btn_abm"
                        Width="250" OnClick="btnClientes_Click" /><asp:Label ID="lblClientes" runat="server" CssClass="lblFechasUltimasActualizacion"  Text=""></asp:Label>
                    <br />
                    <br />
                       <asp:Button ID="btnClientesTodos" runat="server" Text="Sincronizar clientes todos" CssClass="btn_abm"
                        Width="250" onclick="btnClientesTodos_Click"  /><asp:Label ID="lblClientesTodos" runat="server" CssClass="lblFechasUltimasActualizacion"  Text=""></asp:Label>
                    <br />
                    <br />
                    <asp:Button ID="btnProducto" runat="server" Text="Sincronizar productos" CssClass="btn_abm"
                        Width="250" OnClick="btnProducto_Click" /><asp:Label ID="lblProductos" runat="server" CssClass="lblFechasUltimasActualizacion"  Text=""></asp:Label>
                                            <br />
                    <br />
                    <asp:Button ID="btnProductoTodos" runat="server" 
                        Text="Sincronizar productos todos" CssClass="btn_abm"
                        Width="250" onclick="btnProductoTodos_Click" /><asp:Label ID="lblProductosTodos" runat="server" CssClass="lblFechasUltimasActualizacion"  Text=""></asp:Label>
                    <br />
                    <br />
                    <asp:Button ID="btnProductoOfertas" runat="server" CssClass="btn_abm" Width="250"
                        Text="Sincronizar productos ofertas" OnClick="btnProductoOfertas_Click" /><asp:Label ID="lblProductoOfertas" runat="server" CssClass="lblFechasUltimasActualizacion"  Text=""></asp:Label>
                    <br />
                    <br />
                    <asp:Button ID="btnProductoOfertasTodos" runat="server" CssClass="btn_abm" Width="250"
                        Text="Sincronizar productos ofertas todos" 
                        onclick="btnProductoOfertasTodos_Click" /><asp:Label ID="lblProductoOfertasTodos" runat="server" CssClass="lblFechasUltimasActualizacion"  Text=""></asp:Label>
                    <br />
                    <br />
                    <asp:Button ID="btnProductoPrecios" runat="server" CssClass="btn_abm" Width="250"
                        Text="Sincronizar productos precios" OnClick="btnProductoPrecios_Click" /><asp:Label ID="lblProductoPrecios" runat="server" CssClass="lblFechasUltimasActualizacion"  Text=""></asp:Label>
                    <br />
                    <br />
                    <asp:Button ID="btnProductoStocks" runat="server" CssClass="btn_abm" Width="250"
                        Text="Sincronizar productos stocks" OnClick="btnProductoStocks_Click" /><asp:Label ID="lblProductoStocks" runat="server" CssClass="lblFechasUltimasActualizacion"  Text=""></asp:Label>
                    <br />
                    <br />
                    <asp:Button ID="btnProductoValesTodos" runat="server" CssClass="btn_abm" Width="250" Text="Sincronizar productos vales todos"
                        OnClick="btnProductoValesTodos_Click" /><asp:Label ID="lblProductoValesTodos" runat="server" CssClass="lblFechasUltimasActualizacion"  Text=""></asp:Label>
                    <br />
                    <br />
                    <asp:Button ID="btnTransfers" runat="server" Text="Sincronizar transfers" CssClass="btn_abm"
                        Width="250" OnClick="btnTransfers_Click" /><asp:Label ID="lblTransfers" runat="server" CssClass="lblFechasUltimasActualizacion"  Text=""></asp:Label>
                    <br />
                    <br />
                    <asp:Button ID="btnTransfersTodos" runat="server" CssClass="btn_abm" Width="250"
                        Text="Sincronizar transfers todos" OnClick="btnTransfersTodos_Click" /><asp:Label ID="lblTransfersTodos" runat="server" CssClass="lblFechasUltimasActualizacion"  Text=""></asp:Label>
                <%--     <br />
                    <br />
                    <asp:Button ID="Button1" runat="server" CssClass="btn_abm" Width="250"
                        Text="Sincronizar transfers Clientes todos" OnClick="btnTransfersClientesTodos_Click" /><asp:Label ID="lblTransfersClientesTodos" runat="server" CssClass="lblFechasUltimasActualizacion"  Text=""></asp:Label>--%>

                </div>
                   <div style="width: 700px; margin: auto; padding-top:40px;">
                       <hr />
                                           <br />
                <asp:Panel ID="pnl_grilla" runat="server" Width="600">
                    <asp:TextBox ID="txt_buscar" runat="server" CssClass="text_abm"></asp:TextBox>
                    <asp:Button ID="cmd_buscar" runat="server" Text="BUSCAR" CssClass="btn_abm" 
                        CausesValidation="False" onclick="cmd_buscar_Click" />     
                    <asp:GridView ID="gv_datos" runat="server" CssClass="grilla" AllowPaging="True" AllowSorting="True"
                        PageSize="30" CellPadding="2" DataSourceID="odsHistoricos" AutoGenerateColumns="False">
                        <AlternatingRowStyle BackColor="#F9F9F9" />
                        <Columns>
                            <asp:BoundField DataField="his_id" HeaderText="Codigo" SortExpression="usu_codigo" Visible="false" />
                            <asp:BoundField DataField="his_NombreProcedimiento" HeaderText="Nombre" SortExpression="his_NombreProcedimiento" />
                            <asp:BoundField DataField="his_Descripcion" HeaderText="Descripcion" SortExpression="his_Descripcion" Visible="false" />
                            <asp:BoundField DataField="his_FechaToString" HeaderText="Fecha" SortExpression="his_FechaToString" />
                        </Columns>
                        <HeaderStyle CssClass="enc_grilla" />
                        <PagerStyle CssClass="pag_grilla" />
                        <RowStyle CssClass="row_grilla" />
                    </asp:GridView>
                    <asp:ObjectDataSource ID="odsHistoricos" runat="server" SortParameterName="sortExpression"
                        SelectMethod="GetHistorialProcesos" 
                        TypeName="SitioBase.clases.AccesoGrilla">
                        <SelectParameters>
                            <asp:Parameter Name="sortExpression" Type="String" />
                            <asp:SessionParameter Name="pFiltro" 
                                SessionField="GestionProcedimientos_filtro" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </asp:Panel>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
