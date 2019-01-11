<%@ Page Title="" Language="C#" MasterPageFile="~/master/BaseAdmin.master" AutoEventWireup="true"
    CodeFile="GestionTiposEnviosSucursal.aspx.cs" Inherits="admin_pages_GestionTiposEnviosSucursal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="titulo_pagina">
        Tipos de Envíos por Sucursal</div>
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
            <asp:ObjectDataSource ID="ObjectDataSourceTiposEnvios" runat="server" SortParameterName="sortExpression"
                SelectMethod="GetTiposEnviosSucursal" TypeName="SitioBase.clases.AccesoGrilla">
                <SelectParameters>
                    <asp:Parameter Name="sortExpression" Type="String" />
                    <asp:SessionParameter Name="pFiltro" SessionField="GestionTiposEnviosSucursal_Filtro"
                        Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:Panel ID="pnl_grilla" runat="server">
                <asp:TextBox ID="txt_buscar" runat="server" CssClass="text_abm"></asp:TextBox>
                <asp:Button ID="cmd_buscar" runat="server" Text="BUSCAR" CssClass="btn_abm" CausesValidation="False"
                    OnClick="cmd_buscar_Click" />
                <asp:Button ID="cmd_nuevo" runat="server" Text="NUEVO" CssClass="btn_abm" CausesValidation="False"
                    OnClick="cmd_nuevo_Click" />
                <asp:GridView ID="gv_datos" runat="server" CssClass="grilla" AllowPaging="True" AllowSorting="True"
                    PageSize="30" CellPadding="2" DataSourceID="ObjectDataSourceTiposEnvios" AutoGenerateColumns="False"
                    OnRowCommand="gv_datos_RowCommand">
                    <AlternatingRowStyle BackColor="#F9F9F9" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="modificar" runat="server" ImageUrl="~/img/iconos/edit.png" Width="16px"
                                    Height="16px" CausesValidation="False" CommandArgument='<%# Bind("tsd_id") %>'
                                    CommandName="Modificar" ToolTip="Modificar"></asp:ImageButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="eliminar" runat="server" ImageUrl="~/img/iconos/delete2.png"
                                    Width="16px" Height="16px" CausesValidation="False" OnClientClick="return confirm('¿Confirma que desea cambiar eliminar?');"
                                    CommandArgument='<%# Bind("tsd_id") %>' CommandName="Eliminar" ToolTip="Eliminar">
                                </asp:ImageButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="tsd_id" HeaderText="tsd_id" SortExpression="tsd_id"
                            Visible="false" />
                        <asp:BoundField DataField="sde_sucursal" HeaderText="Sucursal" SortExpression="sde_sucursal" />
                        <asp:BoundField DataField="sde_sucursalDependiente" HeaderText="Sucursal Dependiente"
                            SortExpression="sde_sucursalDependiente" />
                        <asp:BoundField DataField="env_codigo" HeaderText="Código Tipo Envío Cliente" SortExpression="env_codigo" />
                        <asp:BoundField DataField="env_nombre" HeaderText="Nombre Tipo Envío Cliente" SortExpression="env_nombre" />
                    </Columns>
                    <HeaderStyle CssClass="enc_grilla" />
                    <PagerStyle CssClass="pag_grilla" />
                    <RowStyle CssClass="row_grilla" />
                </asp:GridView>
            </asp:Panel>
            <asp:Panel ID="pnl_formulario" runat="server" Visible="false">
                <ajaxToolkit:TabContainer runat="server" ID="Tabs" ActiveTabIndex="0" Width="100%"
                    BorderStyle="None" BorderWidth="0px" CssClass="MyTabStyle">
                    <ajaxToolkit:TabPanel runat="server" ID="tab1">
                        <HeaderTemplate>
                            <table border="0">
                                <tr>
                                    <td>
                                        <img alt="" src='../../img/iconos/document_info.png' width='15' height='15' border='0' />
                                    </td>
                                    <td class="txt_11">
                                        Ficha de datos
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ContentTemplate>
                             <div>
                                 <asp:Label ID="lblMensajeError" runat="server" Text="" ForeColor="Red"></asp:Label>
                             </div>
                            <div class="botones_form">
                                <asp:Button ID="cmd_guardar" runat="server" Text="GUARDAR" CssClass="btn_abm" OnClick="cmd_guardar_Click" />
                                <asp:Button ID="cmd_cancelar" runat="server" Text="CANCELAR" CssClass="btn_abm" CausesValidation="False"
                                    OnClick="cmd_cancelar_Click" />
                            </div>
                        
                            <div class="form_datos" style="width: 100%;">
                                <div class="ele_abm">
                                    <div class="lbl_abm">
                                        Sucursal y Sucursal dependiente:
                                    </div>
                                        <asp:DropDownList ID="cmbSucursalesDependientes" runat="server" DataSourceID="odsSucursalesDependientes" DataTextField="sucursal_sucursalDependiente"
                                        DataValueField="sde_codigo" Width="300px">
                                    </asp:DropDownList>
                                    <asp:ObjectDataSource ID="odsSucursalesDependientes" runat="server" SelectMethod="RecuperarTodasSucursalesDependientes"
                                        TypeName="WebService"></asp:ObjectDataSource>
                                </div>
                                <div class="ele_sep">
                                </div>
                                <div class="ele_abm">
                                    <div class="lbl_abm">
                                        Tipo envió cliente:
                                    </div>
                                   <asp:DropDownList ID="cmbTipoEnvioCliente" runat="server" DataSourceID="odsTipoEnvioCliente" DataTextField="env_nombre"
                                        DataValueField="env_id" Width="300px">
                                    </asp:DropDownList>
                                    <asp:ObjectDataSource ID="odsTipoEnvioCliente" runat="server" SelectMethod="RecuperarTodosTiposEnviosParaCombo"
                                        TypeName="WebService"></asp:ObjectDataSource>
                                </div>
                                <div class="ele_sep">
                                </div>
                                <div class="ele_abm">
                                    <div class="lbl_abm">
                                        Tipo Envío:
                                    </div>
                                    <asp:DropDownList ID="cmbTipoEnvio" runat="server" DataSourceID="odsTipoEnvio" DataTextField="env_nombre"
                                        DataValueField="env_id" Width="300px">
                                    </asp:DropDownList>
                                    <asp:ObjectDataSource ID="odsTipoEnvio" runat="server" SelectMethod="RecuperarTodosTiposEnvios"
                                        TypeName="WebService"></asp:ObjectDataSource>
                                    <asp:ListBox ID="listTipoEnviosAsociados" runat="server" Width="300"></asp:ListBox>
                                    <br />
                                    <asp:Button ID="btnAgregarTipoEnvio" runat="server" Text="Agregar" OnClick="btnAgregarTipoEnvio_Click" />
                                    <asp:Button ID="btnEliminarTipoEnvio" runat="server" Text="Eliminar" OnClick="btnEliminarTipoEnvio_Click" />
                                </div>
                                <div class="ele_sep">
                                </div>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
