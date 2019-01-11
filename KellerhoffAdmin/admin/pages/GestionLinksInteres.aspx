<%@ Page Title="" Language="C#" MasterPageFile="~/master/BaseAdmin.master" AutoEventWireup="true" CodeFile="GestionLinksInteres.aspx.cs" Inherits="admin_pages_GestionSitiosInteres" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="titulo_pagina">
        Links de Interés</div>
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
            <asp:Panel ID="pnl_grilla" runat="server">
                <asp:TextBox ID="txt_buscar" runat="server" CssClass="text_abm"></asp:TextBox>
                <asp:Button ID="cmd_buscar" runat="server" Text="BUSCAR" CssClass="btn_abm" CausesValidation="False" OnClick="cmd_buscar_Click" />
                <asp:Button ID="cmd_nuevo" runat="server" Text="NUEVO" CssClass="btn_abm" CausesValidation="False"  OnClick="cmd_nuevo_Click" />
                <asp:GridView ID="gv_datos" runat="server" CssClass="grilla" AllowPaging="True" AllowSorting="True" PageSize="30" CellPadding="2" DataSourceID="odsLinks" AutoGenerateColumns="False"
                    OnRowCommand="gv_datos_RowCommand">
                    <AlternatingRowStyle BackColor="#F9F9F9" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="modificar" runat="server" ImageUrl="~/img/iconos/edit.png" Width="16px" Height="16px" CausesValidation="False" CommandArgument='<%# Bind("lnk_codLinks") %>' CommandName="Modificar" ToolTip="Modificar"></asp:ImageButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="center" />
                        </asp:TemplateField>
                         <asp:TemplateField ShowHeader="False"> 
                        <ItemTemplate>
                            <asp:ImageButton ID="eliminar" runat="server" ImageUrl="~/img/iconos/delete2.png"
                                Width="16px" Height="16px" CausesValidation="False" OnClientClick="return confirm('¿Confirma que desea eliminar?');"
                                CommandArgument='<%# Bind("lnk_codLinks") %>' CommandName="Eliminar" ToolTip="Eliminar">
                            </asp:ImageButton>
                        </ItemTemplate>
                        <ItemStyle Width="20px" HorizontalAlign="center" />
                    </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="estado" runat="server" ImageUrl="~/img/iconos/Change.png" Width="16px"
                                    Height="16px" CausesValidation="False" OnClientClick="return confirm('¿Confirma que desea cambiar el estado?');"
                                    CommandArgument='<%# Bind("lnk_codLinks") %>' CommandName="Estado" ToolTip="Cambiar Estado">
                                </asp:ImageButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="publicar" runat="server" ImageUrl="~/img/iconos/events.png"
                                    Width="16px" Height="16px" CausesValidation="False" OnClientClick="return confirm('¿Confirma que desea mostrar/ocultar la noticia?');"
                                    CommandArgument='<%# Bind("lnk_codLinks") %>' CommandName="Publicar" ToolTip="Publicar">
                                </asp:ImageButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="lnk_codLinks" HeaderText="Código" SortExpression="lnk_codLinks"  Visible="false" />
                        <asp:BoundField DataField="lnk_titulo" HeaderText="Título" SortExpression="lnk_titulo" />
                        <asp:BoundField DataField="lnk_bajada" HeaderText="Bajada" SortExpression="lnk_bajada" />
                        <asp:BoundField DataField="lnk_web" HeaderText="Web" SortExpression="lnk_web" />
                        <asp:BoundField DataField="lnk_origen" HeaderText="" SortExpression="lnk_origen" Visible="false" />
                        <asp:BoundField DataField="lnk_estadoToString" HeaderText="Estado" SortExpression="lnk_estadoToString" />
                        <asp:BoundField DataField="lnk_isPublicarToString" HeaderText="Publicar" SortExpression="lnk_isPublicarToString" />
                    </Columns>
                    <HeaderStyle CssClass="enc_grilla" />
                    <PagerStyle CssClass="pag_grilla" />
                    <RowStyle CssClass="row_grilla" />
                </asp:GridView>
                <asp:ObjectDataSource ID="odsLinks" runat="server" SortParameterName="sortExpression" SelectMethod="GetLinkInteres" TypeName="SitioBase.clases.AccesoGrilla">
                     <SelectParameters>
                        <asp:SessionParameter Name="pFiltro" SessionField="GestionLink_Filtro" Type="String" />
                        <asp:SessionParameter Name="pTipo" SessionField="GestionLink_Tipo" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </asp:Panel>
            <asp:Panel ID="pnl_formulario" runat="server" Visible="false">
                <ajaxToolkit:TabContainer runat="server" ID="Tabs" ActiveTabIndex="0" Width="100%" BorderStyle="None" BorderWidth="0px" CssClass="MyTabStyle">
                    <ajaxToolkit:TabPanel runat="server" ID="tab1">
                        <HeaderTemplate>
                            <table border="0">
                                <tr>
                                    <td><img alt="" src='../../img/iconos/document_info.png' width='15' height='15' border='0' /></td>
                                    <td class="txt_11"> Ficha de datos</td>
                                </tr>
                            </table>
                        
</HeaderTemplate>
                        
<ContentTemplate>
                            <div class="botones_form">
                                <asp:Button ID="cmd_guardar" runat="server" Text="GUARDAR" CssClass="btn_abm" OnClick="cmd_guardar_Click" />
                                <asp:Button ID="cmd_cancelar" runat="server" Text="CANCELAR" CssClass="btn_abm" CausesValidation="False" OnClick="cmd_cancelar_Click" />
                            </div>
                            <div class="form_datos" style="width: 100%;">
                               
                                <div class="ele_abm">
                                    <div class="lbl_abm">
                                        Título
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitulo" Display="Dynamic" ErrorMessage="Completar" CssClass="msg_error"></asp:RequiredFieldValidator>
                                    </div>
                                    <asp:TextBox ID="txtTitulo" CssClass="text_abm" runat="server" MaxLength="300" Width="300"></asp:TextBox>
                                </div>
                                <div class="ele_sep">
                                </div>
                                <div class="ele_abm">
                                    <div class="lbl_abm">
                                        Bajada
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_bajada"  Display="Dynamic" ErrorMessage="Completar" CssClass="msg_error"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="html_editor">
                                        <asp:TextBox ID="txt_bajada" Width="500" CssClass="text_abm_100" runat="server" Height="100"  TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="ele_sep">
                                </div>
                                <div class="ele_abm">
                                    <div class="lbl_abm">
                                        Web</div>
                                     <div class="html_editor">

                                        <asp:TextBox ID="txt_web" CssClass="text_abm_100" runat="server"  Width="300"></asp:TextBox>
                                    </div>
                                </div>
                                  <div class="ele_sep"> </div>
                                <div class="ele_abm">
                                    <div class="lbl_abm">Origen
                                      </div>
                                     <asp:DropDownList ID="cmb_origen" runat="server"  CssClass="anchoControlRegla"  >
                                      <asp:ListItem Value="-1" Text="Seleccionar..."></asp:ListItem>
                                      <asp:ListItem Value="1" Text="Nacionales"></asp:ListItem>
                                      <asp:ListItem Value="2" Text="Internacionales - Español"></asp:ListItem>
                                      <asp:ListItem Value="3" Text="Internacionales - Inglés"></asp:ListItem>
                                      </asp:DropDownList>
                                </div>
                            </div>
                        
</ContentTemplate>
                    
</ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

