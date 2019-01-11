<%@ Page Title="" Language="C#" MasterPageFile="~/master/BaseAdmin.master" AutoEventWireup="true" CodeFile="GestionNoticia.aspx.cs" Inherits="admin_pages_GestionNoticia" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="HTMLEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="titulo_pagina">
        Noticias</div>
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
                <asp:Button ID="cmd_buscar" runat="server" Text="BUSCAR" CssClass="btn_abm" CausesValidation="False"
                    OnClick="cmd_buscar_Click" />
                <asp:Button ID="cmd_nuevo" runat="server" Text="NUEVO" CssClass="btn_abm" CausesValidation="False" OnClick="cmd_nuevo_Click" />
                <asp:Label ID="lbl_mensaje" runat="server" CssClass="msg_error" Text="Esta noticia ya está seleccionada para mostrarse en home" Visible="false"></asp:Label>
                <asp:GridView ID="gv_datos" runat="server" CssClass="grilla" AllowPaging="True" AllowSorting="True"
                    PageSize="30" CellPadding="2" DataSourceID="odsNoticia" AutoGenerateColumns="False"
                    OnRowCommand="gv_datos_RowCommand">
                    <AlternatingRowStyle BackColor="#F9F9F9" />
                    <Columns>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="modificar" runat="server" ImageUrl="~/img/iconos/edit.png" Width="16px"
                                    Height="16px" CausesValidation="False" CommandArgument='<%# Bind("not_codNoticia") %>'
                                    CommandName="Modificar" ToolTip="Modificar"></asp:ImageButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="estado" runat="server" ImageUrl="~/img/iconos/Change.png" Width="16px"
                                    Height="16px" CausesValidation="False" OnClientClick="return confirm('¿Confirma que desea cambiar el estado?');"
                                    CommandArgument='<%# Bind("not_codNoticia") %>' CommandName="Estado" ToolTip="Cambiar Estado">
                                </asp:ImageButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="publicar" runat="server" ImageUrl="~/img/iconos/events.png"
                                    Width="16px" Height="16px" CausesValidation="False" OnClientClick="return confirm('¿Confirma que desea mostrar/ocultar la noticia?');"
                                    CommandArgument='<%# Bind("not_codNoticia") %>' CommandName="Publicar" ToolTip="Publicar">
                                </asp:ImageButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False"> 
                        <ItemTemplate>
                            <asp:ImageButton ID="eliminar" runat="server" ImageUrl="~/img/iconos/delete2.png" Width="16px" Height="16px" CausesValidation="False" OnClientClick="return confirm('¿Confirma que desea eliminar?');"  CommandArgument='<%# Bind("not_codNoticia") %>' CommandName="Eliminar" ToolTip="Eliminar">
                            </asp:ImageButton>
                        </ItemTemplate>
                        <ItemStyle Width="20px" HorizontalAlign="center" />
                    </asp:TemplateField>
                        <asp:BoundField DataField="not_codNoticia" HeaderText="Código" SortExpression="not_codNoticia"
                            Visible="false" />
                        <asp:BoundField DataField="not_fechaDesdeReducido" HeaderText="Fecha Desde" SortExpression="not_fechaDesde" />
                        <asp:BoundField DataField="not_fechaHastaReducido" HeaderText="Fecha Hasta" SortExpression="not_fechaHasta" />
                        <asp:BoundField DataField="not_titulo" HeaderText="Título" SortExpression="not_titulo" />
                        <asp:BoundField DataField="not_bajada" HeaderText="Bajada" SortExpression="not_bajada" >
                        <ItemStyle Width="800px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="not_estadoToString" HeaderText="Estado" SortExpression="not_estadoToString" />
                        <asp:BoundField DataField="not_isPublicarToString" HeaderText="Publicar" SortExpression="not_isPublicarToString" />
                   <asp:TemplateField HeaderText="Home Not 1">
                            <ItemTemplate>
                                <asp:ImageButton ID="home1" runat="server" ImageUrl='<%# MostrarEnHome1(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"not_codNoticia")), "SELECT" ) %>'
                                    Width="20px" Height="20px" CausesValidation="False" CommandName="Home1"  CommandArgument='<%# Bind("not_codNoticia") %>' ToolTip="Home">
                                </asp:ImageButton>
                              
                            </ItemTemplate>
                            <ItemStyle Width="40px" HorizontalAlign="center" />
                        </asp:TemplateField> 
                         <asp:TemplateField HeaderText="Home Not 2">
                            <ItemTemplate>
                                <asp:ImageButton ID="home2" runat="server" ImageUrl='<%# MostrarEnHome2(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"not_codNoticia")), "SELECT" ) %>'
                                    Width="20px" Height="20px" CausesValidation="False" CommandName="Home2"  CommandArgument='<%# Bind("not_codNoticia") %>' ToolTip="Home">
                                </asp:ImageButton>
                            </ItemTemplate>
                            <ItemStyle Width="40px" HorizontalAlign="center" />
                        </asp:TemplateField>
                  
                  <asp:TemplateField HeaderText="Home Not 3">
                            <ItemTemplate>
                                <asp:ImageButton ID="home3" runat="server" ImageUrl='<%# MostrarEnHome3(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"not_codNoticia")), "SELECT" ) %>'
                                    Width="20px" Height="20px" CausesValidation="False" CommandName="Home3"  CommandArgument='<%# Bind("not_codNoticia") %>' ToolTip="Home">
                                </asp:ImageButton>
                            </ItemTemplate>
                            <ItemStyle Width="40px" HorizontalAlign="center" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="enc_grilla" />
                    <PagerStyle CssClass="pag_grilla" />
                    <RowStyle CssClass="row_grilla" />
                </asp:GridView>
                <asp:ObjectDataSource ID="odsNoticia" runat="server" SortParameterName="sortExpression"
                    SelectMethod="GetNoticias" TypeName="SitioBase.clases.AccesoGrilla">
                    <SelectParameters>
                        <asp:SessionParameter Name="pFiltro" SessionField="GestionNoticia_Filtro" Type="String" />
                        <asp:SessionParameter Name="pTipo" SessionField="GestionNoticia_Tipo" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>

               

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
                                        Fecha Desde
                                        <asp:RequiredFieldValidator ID="rv_fechaDesde" runat="server" ControlToValidate="txt_fechaDesde"
                                            Display="Dynamic" ErrorMessage="Completar" CssClass="msg_error"></asp:RequiredFieldValidator>
                                    </div>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtenderFechaDesde" runat="server" TargetControlID="txt_fechaDesde">
                                    </ajaxToolkit:CalendarExtender>
                                    <asp:TextBox ID="txt_fechaDesde" CssClass="text_abm" runat="server"></asp:TextBox>
                                </div>                               
                                <div class="ele_abm">
                                    <div class="lbl_abm">
                                        Fecha Hasta<%-- EnableViewState="false" Enabled="true"--%>
                                        <asp:CompareValidator ID="CV1" runat="server" CssClass="msg_error" ErrorMessage="Fecha desde debe ser menor o igual fecha hasta"
                                            ControlToValidate="txt_fechaDesde" ControlToCompare="txt_fechaHasta" Operator="LessThanEqual"
                                            Type="Date" ></asp:CompareValidator>
                                    </div>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtenderFechaHasta" runat="server" TargetControlID="txt_fechaHasta">
                                    </ajaxToolkit:CalendarExtender>
                                    <asp:TextBox ID="txt_fechaHasta" CssClass="text_abm" runat="server"></asp:TextBox>
                                </div>
                               <div class="ele_sep">
                                </div>
                                <div class="ele_abm">
                                    <div class="lbl_abm">
                                        Producto
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitulo"
                                            Display="Dynamic" ErrorMessage="Completar" CssClass="msg_error"></asp:RequiredFieldValidator>
                                             <asp:Label ID="Label1" runat="server" Text="* Máximo 30 caracteres" ForeColor="Red"></asp:Label>
                                    </div>
                                     <div class="html_editor">
                                        <asp:TextBox ID="txtTitulo" CssClass="text_abm_100" runat="server" Height="70" Width="400" MaxLength="30"></asp:TextBox>                                                                              
                                     </div>
                                </div>
                               
                                <div class="ele_abm">
                                    <div class="lbl_abm">
                                        Nombre Promoción
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_bajada"
                                            Display="Dynamic" ErrorMessage="Completar" CssClass="msg_error"></asp:RequiredFieldValidator>
                                              
                                         <asp:Label ID="Label2" runat="server" Text="* Máximo 40 caracteres" ForeColor="Red"></asp:Label>
                                    </div>
                                    <div class="html_editor">
                                        <asp:TextBox ID="txt_bajada" CssClass="text_abm_100" runat="server" Height="70"
                                             Width="400" MaxLength="40"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="ele_sep"></div>
                               
                                <div class="ele_abm">
                                    <div class="lbl_abm">
                                        Descripción
                                         <asp:Label ID="Label3" runat="server" Text="* Máximo 50 caracteres" ForeColor="Red"></asp:Label>
                                      </div> 
                                    <div class="html_editor">
                                        <asp:TextBox ID="txt_descripcion" CssClass="text_abm_100" runat="server" Height="70"
                                            Width="400" MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>
                                  <div class="ele_abm">
                                    <div class="lbl_abm">
                                        Destacado
                                      <asp:Label ID="Label4" runat="server" Text="* Máximo 50 caracteres" ForeColor="Red"></asp:Label>
                                    </div>
                                    <div class="html_editor">
                                        <asp:TextBox ID="txt_destacado" CssClass="text_abm_100" runat="server" Height="70"
                                            Width="400" MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>
                               <%-- <div class="ele_abm">
                                    <div class="lbl_abm">
                                        Descripcion</div>
                                    <div class="html_editor">
                                        <HTMLEditor:Editor runat="server" ID="txt_descripcion" NoUnicode="True" Height="300px"
                                            Width="100%" />
                                    </div>
                                </div>--%>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="server" ID="tab2">
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
                </ajaxToolkit:TabContainer>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
