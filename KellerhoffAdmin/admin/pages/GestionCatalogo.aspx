<%@ Page Title="" Language="C#" MasterPageFile="~/master/BaseAdmin.master" AutoEventWireup="true"
    CodeFile="GestionCatalogo.aspx.cs" Inherits="admin_pages_GestionCatalogo" %>

<%--
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="HTMLEditor" %>--%>
<asp:content id="Content1" contentplaceholderid="head" runat="Server">
<style type="text/css">
    .Mayuscula {
        text-transform: uppercase;
    }
</style>
</asp:content>
<asp:content id="Content2" contentplaceholderid="MainContent" runat="Server">
    <div class="titulo_pagina">
        &nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblTitulo" runat="server" Text="Catálogo"></asp:Label></div>
<%--    <asp:UpdateProgress ID="up_prog_0" AssociatedUpdatePanelID="up_general" runat="server">
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
        <ContentTemplate>--%>
            <asp:Panel ID="pnl_grilla" runat="server">
                <asp:TextBox ID="txt_buscar" runat="server" CssClass="text_abm"></asp:TextBox>
                <asp:Button ID="cmd_buscar" runat="server" Text="BUSCAR" CssClass="btn_abm" CausesValidation="False"
                    OnClick="cmd_buscar_Click" />
                <asp:Button ID="cmd_nuevo" runat="server" Text="NUEVO" CssClass="btn_abm" CausesValidation="False"
                    OnClick="cmd_nuevo_Click" />
                <asp:GridView ID="gv_datos" runat="server" CssClass="grilla" AllowPaging="True" AllowSorting="True"
                    PageSize="30" CellPadding="2" DataSourceID="odsNoticia" AutoGenerateColumns="False"
                    OnRowCommand="gv_datos_RowCommand">
                    <AlternatingRowStyle BackColor="#F9F9F9" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="modificar" runat="server" ImageUrl="~/img/iconos/edit.png" Width="16px"
                                    Height="16px" CausesValidation="False" CommandArgument='<%# Bind("tbc_codigo") %>'
                                    CommandName="Modificar" ToolTip="Modificar"></asp:ImageButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="estado" runat="server" ImageUrl="~/img/iconos/Change.png" Width="16px"
                                    Height="16px" CausesValidation="False" OnClientClick="return confirm('¿Confirma que desea cambiar el estado?');"
                                    CommandArgument='<%# Bind("tbc_codigo") %>' CommandName="Estado" ToolTip="Cambiar Estado">
                                </asp:ImageButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="publicar" runat="server" ImageUrl="~/img/iconos/events.png"
                                    Width="16px" Height="16px" CausesValidation="False" OnClientClick="return confirm('¿Confirma que desea mostrar/ocultar catalogo en Home?');"
                                    CommandArgument='<%# Bind("tbc_codigo") %>' CommandName="Publicar" ToolTip="Publicar Home">
                                </asp:ImageButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="eliminar" runat="server" ImageUrl="~/img/iconos/delete2.png"
                                    Width="16px" Height="16px" CausesValidation="False" OnClientClick="return confirm('¿Confirma que desea eliminar?');"
                                    CommandArgument='<%# Bind("tbc_codigo") %>' CommandName="Eliminar" ToolTip="Eliminar">
                                </asp:ImageButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="tbc_codigo" HeaderText="Código" SortExpression="tbc_codigo"
                            Visible="false" />
                        <asp:BoundField DataField="tbc_titulo" HeaderText="Título" SortExpression="tbc_titulo" />
                        <asp:BoundField DataField="tbc_orden" HeaderText="Orden" SortExpression="tbc_orden"
                            Visible="false" />
                        <asp:BoundField DataField="tbc_estadoToString" HeaderText="Estado" SortExpression="tbc_estadoToString" />
                        <asp:BoundField DataField="tbc_publicarHomeToString" HeaderText="Publicar" SortExpression="tbc_publicarHomeToString" />                                            
                    </Columns>
                    <HeaderStyle CssClass="enc_grilla" />
                    <PagerStyle CssClass="pag_grilla" />
                    <RowStyle CssClass="row_grilla" />
                </asp:GridView>
                <asp:ObjectDataSource ID="odsNoticia" runat="server" SortParameterName="sortExpression"
                    SelectMethod="GetCatalogo" TypeName="SitioBase.clases.AccesoGrilla">
                    <SelectParameters>
                        <asp:SessionParameter Name="pFiltro" SessionField="GestionCatalogo_Filtro" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </asp:Panel>
            <asp:Panel ID="pnl_formulario" runat="server" Visible="false">
                <div class="botones_form">
                    <asp:Button ID="cmd_guardar" runat="server" Text="GUARDAR" CssClass="btn_abm" OnClick="cmd_guardar_Click" />
                    <asp:Button ID="cmd_cancelar" runat="server" Text="CANCELAR" CssClass="btn_abm" CausesValidation="False"
                        OnClick="cmd_cancelar_Click" />
                </div>
                <div class="form_datos" style="width: 100%;">
                    <div class="ele_abm">
                        <div class="lbl_abm">
                            Título
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitulo"
                                Display="Dynamic" ErrorMessage="Completar" CssClass="msg_error"></asp:RequiredFieldValidator>
                        </div>
                        <asp:TextBox ID="txtTitulo" CssClass="text_abm Mayuscula" runat="server" MaxLength="300"></asp:TextBox>
                    </div>
                    <div class="ele_sep">
                    </div>
                    <div class="ele_abm">
                        <div class="lbl_abm">
                            Archivo
                        </div>
                        <div>
                            <asp:Panel ID="PanelArchivoTexto" runat="server" Visible="False">
                                <asp:Label ID="lblArchivo" runat="server" Text="Label"></asp:Label>
                                <asp:Button ID="btnArchivo" runat="server" Text="Cambiar archivo" OnClick="btnArchivo_Click" />
                            </asp:Panel>
            
                             <asp:Panel ID="PanelArchivo" runat="server" Visible="False">   
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                                 </asp:Panel>
                        </div>
                    </div>
                </div>
                <%--    <ajaxToolkit:TabContainer runat="server" ID="Tabs" ActiveTabIndex="0" Width="100%"
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
                                        Título
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitulo"
                                            Display="Dynamic" ErrorMessage="Completar" CssClass="msg_error"></asp:RequiredFieldValidator>
                                    </div>
                                    <asp:TextBox ID="txtTitulo" CssClass="text_abm" runat="server" MaxLength="300"></asp:TextBox>
                                </div>
                                <div class="ele_sep">
                                </div>
                                <div class="ele_abm">
                                    <div class="lbl_abm">
                                        Archivo
                                    </div>
                                    <div>
                                        <asp:Panel ID="PanelArchivoTexto" runat="server" Visible="False">
                                            <asp:Label ID="lblArchivo" runat="server" Text="Label"></asp:Label>
                                            <asp:Button ID="btnArchivo" runat="server" Text="Cambiar archivo" OnClick="btnArchivo_Click" />
                                        </asp:Panel>
                                        <br />
                                            <asp:FileUpload ID="FileUpload1" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>--%>
                <%--              <ajaxToolkit:TabPanel runat="server" ID="tab2">
                        <HeaderTemplate>
                            <table border="0">
                                <tr>
                                    <td>
                                        <img alt="" src='../../img/iconos/folder.png' width='15' height='15' border='0' />
                                    </td>
                                    <td class="txt_11">
                                        Archivos relacionados
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div class="botones_form">
                                <asp:Label ID="lbl_iframe" runat="server" Text=""></asp:Label>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>--%>
            </asp:Panel>
<%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:content>
