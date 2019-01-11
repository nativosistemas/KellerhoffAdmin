<%@ Page Title="" Language="C#" MasterPageFile="~/master/BaseAdmin.master" AutoEventWireup="true"
    CodeFile="GestionMontoMinimo.aspx.cs" Inherits="admin_pages_GestionMontoMinimo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="titulo_pagina">
        Monto Mínimo</div>
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
            <asp:ObjectDataSource ID="odsSucursalDependiente" runat="server" SelectMethod="GetSucursalesMontoMinimo"
                SortParameterName="sortExpression" TypeName="SitioBase.clases.AccesoGrilla">
                <SelectParameters>
                    <asp:SessionParameter Name="pFiltro" SessionField="GestionMontoMinimo_Filtro" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:Panel ID="pnl_grilla" runat="server">
             <%--   <asp:TextBox ID="txt_buscar" runat="server" CssClass="text_abm"></asp:TextBox>
                <asp:Button ID="cmd_buscar" runat="server" Text="BUSCAR" CssClass="btn_abm" CausesValidation="False"
                    OnClick="cmd_buscar_Click" />
                <asp:Button ID="cmd_nuevo" runat="server" Text="NUEVO" CssClass="btn_abm" CausesValidation="False"
                    OnClick="cmd_nuevo_Click" />--%>
                <asp:GridView ID="gv_datos" runat="server" CssClass="grilla" AllowPaging="True" AllowSorting="True"
                    DataSourceID="odsSucursalDependiente" PageSize="30" CellPadding="2" AutoGenerateColumns="False"
                    OnRowCommand="gv_datos_RowCommand">
                    <AlternatingRowStyle BackColor="#F9F9F9" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="modificar" runat="server" ImageUrl="~/img/iconos/edit.png" Width="16px"
                                    Height="16px" CausesValidation="False" CommandArgument='<%# Bind("suc_codigo") %>'
                                    CommandName="Modificar" ToolTip="Modificar"></asp:ImageButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="suc_codigo" HeaderText="Sucursal" SortExpression="suc_codigo" />
                        <asp:BoundField DataField="suc_nombre" HeaderText="Nombre" SortExpression="suc_codigo" />
                        <asp:BoundField DataField="suc_montoMinimo" HeaderText="Monto Mínimo" SortExpression="suc_montoMinimo" />
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
                            <div class="botones_form">
                                <asp:Button ID="cmd_guardar" runat="server" Text="GUARDAR" CssClass="btn_abm" OnClick="cmd_guardar_Click" />
                                <asp:Button ID="cmd_cancelar" runat="server" Text="CANCELAR" CssClass="btn_abm" CausesValidation="False"
                                    OnClick="cmd_cancelar_Click" />
                            </div>
                            <div class="form_datos" style="width: 100%;">
                                <div class="ele_abm">
                                    <div class="lbl_abm">
                                        Sucursal:
                                    </div>
                                    <asp:Label ID="lblSucursalDatos" runat="server"></asp:Label>      
                                </div>
                                <div class="ele_abm">
                                    <div class="lbl_abm">
                                        Monto Mínimo:
                                    </div>
                                    <table>
                                    <tr>
                                    <td>                          <asp:TextBox ID="txt_nombre" CssClass="text_abm" runat="server" MaxLength="250" 
                                        Width="100px"></asp:TextBox></td>
                                      <td> <asp:Label ID="lblMensajeErrorMontoMinimo" runat="server" ForeColor="Red" ></asp:Label> </td>
                                   
                                    </tr>
                                    </table>
          

                                </div>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
