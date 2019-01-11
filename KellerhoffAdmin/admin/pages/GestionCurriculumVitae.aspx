<%@ Page Title="" Language="C#" MasterPageFile="~/master/BaseAdmin.master" AutoEventWireup="true"
    CodeFile="GestionCurriculumVitae.aspx.cs" Inherits="admin_pages_GestionCurriculumVitae" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="titulo_pagina">
        &nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblTitulo" runat="server" Text="Curriculum Vitae"></asp:Label></div>
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
            <asp:ObjectDataSource ID="ObjectDataSourceCV" runat="server" SortParameterName="sortExpression"
                SelectMethod="GetCurriculumVitae" TypeName="SitioBase.clases.AccesoGrilla">
                <SelectParameters>
                    <asp:Parameter Name="sortExpression" Type="String" />
                    <asp:SessionParameter Name="pFiltro" SessionField="GestionCurriculumVitae_Filtro"
                        Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:Panel ID="pnl_grilla" runat="server">
                <asp:TextBox ID="txt_buscar" runat="server" CssClass="text_abm"></asp:TextBox>
                <asp:Button ID="cmd_buscar" runat="server" Text="BUSCAR" CssClass="btn_abm" CausesValidation="False"
                    OnClick="cmd_buscar_Click" />
                <asp:GridView ID="gv_datos" runat="server" CssClass="grilla" AllowPaging="True" AllowSorting="True"
                    PageSize="30" CellPadding="2" DataSourceID="ObjectDataSourceCV" AutoGenerateColumns="False"
                    OnRowCommand="gv_datos_RowCommand">
                    <AlternatingRowStyle BackColor="#F9F9F9" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="modificar" runat="server" ImageUrl="~/img/iconos/edit.png" Width="16px"
                                    Height="16px" CausesValidation="False" CommandArgument='<%# Bind("tcv_codCV") %>'
                                    CommandName="Modificar" ToolTip="Ver"></asp:ImageButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="estado" runat="server" ImageUrl="~/img/iconos/Change.png" Width="16px"
                                    Height="16px" CausesValidation="False" OnClientClick="return confirm('¿Confirma que desea cambiar el estado?');"
                                    CommandArgument='<%# Bind("tcv_codCV") %>' CommandName="Estado" ToolTip="Cambiar Estado">
                                </asp:ImageButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="eliminar" runat="server" ImageUrl="~/img/iconos/delete2.png"
                                    Width="16px" Height="16px" CausesValidation="False" OnClientClick="return confirm('¿Confirma que desea eliminar?');"
                                    CommandArgument='<%# Bind("tcv_codCV") %>' CommandName="Eliminar" ToolTip="Eliminar">
                                </asp:ImageButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="tcv_codCV" HeaderText="tcv_codCV" SortExpression="tcv_codCV"
                            Visible="false" />
                        <asp:BoundField DataField="tcv_nombre" HeaderText="Nombre y apellido" SortExpression="tcv_nombre" />
                        <asp:BoundField DataField="tcv_dni" HeaderText="DNI" SortExpression="tcv_dni" />
                          <asp:BoundField DataField="tcv_mail" HeaderText="Mail" SortExpression="tcv_mail" />
                        <asp:BoundField DataField="tcv_fechaToString" HeaderText="Fecha" SortExpression="tcv_fechaToString" />
                        <asp:BoundField DataField="tcv_estadoToString" HeaderText="Estado" SortExpression="tcv_estadoToString" />
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
                                <%--                            <asp:Button ID="cmd_guardar" runat="server" Text="GUARDAR" CssClass="btn_abm" OnClick="cmd_guardar_Click" />--%>
                                <asp:Button ID="cmd_cancelar" runat="server" Text="VOLVER" CssClass="btn_abm" CausesValidation="False"
                                    OnClick="cmd_cancelar_Click" />
                            </div>
                            <div class="form_datos" style="width: 100%;">
                                <div class="ele_abm">
                                    <div class="lbl_abm">
                                        Nombre:
                                    </div>
                                    <asp:TextBox ID="txt_nombre" CssClass="text_abm" runat="server" Width="400"  Enabled="false"></asp:TextBox>
                                </div>
                                <div class="ele_sep">
                                </div>
                                <div class="ele_abm">
                                    <div class="lbl_abm">
                                        DNI:
                                    </div>
                                    <asp:TextBox ID="txt_dni" CssClass="text_abm" runat="server" Width="400"  Enabled="false"></asp:TextBox>
                                </div>
                                <div class="ele_sep">
                                </div>
                                <div class="ele_abm">
                                    <div class="lbl_abm">
                                        Mail:
                                    </div>
                                    <asp:TextBox ID="txt_mail" CssClass="text_abm" runat="server"  Width="400"  Enabled="false"></asp:TextBox>
                                </div>
                                <div class="ele_sep">
                                </div>
                                <div class="ele_abm">
                                    <div class="lbl_abm">
                                        Comentario:
                                    </div>
                                    <asp:TextBox ID="txt_comentario" CssClass="text_abm" runat="server" TextMode="MultiLine"
                                        Width="400" Height="300" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="ele_sep">
                                </div>
                                <div class="ele_abm">
                                    <div class="lbl_abm">
                                        Fecha:
                                    </div>
                                    <asp:TextBox ID="txt_fecha" CssClass="text_abm" runat="server" Width="400" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="ele_sep">
                                </div>
                                <div class="ele_abm">
                                    <div class="lbl_abm">
                                        Estado:
                                    </div>
                                    <asp:TextBox ID="txt_estado" CssClass="text_abm" runat="server"  Width="400" Enabled="false"></asp:TextBox>
                                </div>
                                  <div class="ele_sep">
                                </div>
                                <div class="ele_abm">
                                    <div class="lbl_abm">
                                        Archivo:
                                    </div>
                                    <asp:Label ID="lbl_archivo" runat="server" Text=""></asp:Label>                                  
                                </div>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
