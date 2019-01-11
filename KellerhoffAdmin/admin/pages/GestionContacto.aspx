<%@ Page Title="" Language="C#" MasterPageFile="~/master/BaseAdmin.Master" AutoEventWireup="true" CodeFile="GestionContacto.aspx.cs" Inherits="admin_pages_GestionContacto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="HTMLEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="titulo_pagina"> &nbsp;&nbsp;&nbsp; <asp:Label ID="lblTitulo" runat="server" Text="Contacto" ></asp:Label></div>
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
           <asp:GridView ID="gv_datos2" runat="server" CssClass="grilla" AllowPaging="True" AllowSorting="True"
                    PageSize="30" CellPadding="2" DataSourceID="odsNoticia" AutoGenerateColumns="False"
                    OnRowCommand="gv_datos2_RowCommand">
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
                        <asp:BoundField DataField="not_codNoticia" HeaderText="Código" SortExpression="not_codNoticia" Visible="false" />
                        
                        <asp:BoundField DataField="not_titulo" HeaderText="Título" SortExpression="not_titulo" />
                        <asp:BoundField DataField="not_bajada" Visible="false"  HeaderText="Bajada" SortExpression="not_bajada" />
                        <asp:BoundField DataField="not_codTipoNoticia" HeaderText="Tipo" SortExpression="not_codTipoNoticia" Visible="false" />
                        <asp:BoundField DataField="not_estadoToString" HeaderText="Estado" SortExpression="not_estadoToString" />
                        <asp:BoundField DataField="not_isPublicarToString" HeaderText="Publicar" SortExpression="not_isPublicarToString" />
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

                   <div style=" padding-top:30px;">
               <asp:DropDownList ID="cmb_leidos" runat="server"  AppendDataBoundItems="true"  AutoPostBack="True" CssClass="anchoControlRegla" onselectedindexchanged="cmb_leidos_SelectedIndexChanged"   >
                    <asp:ListItem Value="-1" Text="Seleccionar..."></asp:ListItem>
                   <asp:ListItem Value="NO" Text="No Leídos"></asp:ListItem>
                    <asp:ListItem Value="SI" Text="Leídos"></asp:ListItem>
                </asp:DropDownList>
               <asp:TextBox ID="txt_buscar" runat="server" CssClass="text_abm"></asp:TextBox>
                <asp:Button ID="cmd_buscar" runat="server" Text="BUSCAR" CssClass="btn_abm" CausesValidation="False" OnClick="cmd_buscar_Click" />
                
            <asp:GridView ID="gv_datos" runat="server" CssClass="grilla" AllowSorting="True"  AutoGenerateColumns="False" Width="100%"  PageSize="30" CellPadding="2" DataSourceID="odsContacto" OnSelectedIndexChanged="gv_datos_SelectedIndexChanged" OnRowCommand="gv_datos_RowCommand" AllowPaging="True">
              <Columns>
                   <asp:BoundField DataField="con_codContacto" HeaderText="codigo" Visible="False"  />
            
                   <asp:BoundField DataField="con_fecha" HeaderText="Fecha|Hora" Visible="False" />
                   <asp:BoundField DataField="con_nombre" HeaderText="Persona" Visible="False" />
                   <asp:BoundField DataField="con_mail" HeaderText="E-mail" Visible="False" />
                   <asp:BoundField DataField="con_asunto" HeaderText="Asunto" Visible="False" />
                  <asp:BoundField DataField="con_comentario" HeaderText="Comentario" Visible="False" />
                   <asp:BoundField DataField="con_leido" HeaderText="Le&#237;do" Visible="False"  />
                 
       
                    <asp:TemplateField HeaderText="Contacto">
                        <ItemTemplate>
                            <b>Nombre:</b> <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "con_nombre").ToString() %>'></asp:Label><br />
                            <b>Empresa:</b> <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "con_empresa").ToString() %>'></asp:Label><br />
                            <b>Fecha:</b> <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "con_fecha").ToString() %>'></asp:Label><br />
                            <b>E-mail:</b> <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "con_mail").ToString() %>'></asp:Label><br />
                              </ItemTemplate>
                       </asp:TemplateField>  
           
                    <asp:TemplateField HeaderText="Comentario">
                        <ItemTemplate>
                          <b>Asunto:</b> <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "con_asunto").ToString() %>'></asp:Label><br />
                          <b>Comentario:</b> <asp:Label ID="Label3b" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "con_comentario").ToString() %>'></asp:Label><br />
                        </ItemTemplate>
                       </asp:TemplateField>              
           
                  
                    <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:ImageButton ID="leido" runat="server" ImageUrl="~/img/iconos/tick.png" Height="16" Width="20" CausesValidation="False" onclientclick="return confirm('¿Confirma que desea pasar a leído el registro indicado?');" CommandArgument='<%# Bind("con_codContacto") %>' CommandName="Leido" ToolTip="Marcar como leído"></asp:ImageButton>
                    </ItemTemplate>
             <ItemStyle Width="25px" />
                                 </asp:TemplateField>   
                         
                   </Columns> 
                    <HeaderStyle CssClass="enc_grilla" />
                        <PagerStyle CssClass="pag_grilla" />
                        <RowStyle VerticalAlign="Top" />    
             </asp:GridView>
          <asp:ObjectDataSource ID="odsContacto" runat="server" SortParameterName="sortExpression" SelectMethod="GetContacto" TypeName="SitioBase.clases.AccesoGrilla">
                    <SelectParameters>
                        <asp:SessionParameter Name="pFiltro" SessionField="GestionContacto_Filtro" Type="String" />
                    
                    </SelectParameters>
                </asp:ObjectDataSource>
                </div>
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
                                        Título
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitulo"
                                            Display="Dynamic" ErrorMessage="Completar" CssClass="msg_error"></asp:RequiredFieldValidator>
                                    </div>
                                    <asp:TextBox ID="txtTitulo" CssClass="text_abm" runat="server" MaxLength="300" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="ele_sep">
                                </div>
                                <div class="ele_abm" style="display:none">
                                    <div class="lbl_abm">
                                        Bajada
                                    </div>
                                    <div class="html_editor">
                                        <asp:TextBox ID="txt_bajada" CssClass="text_abm_100" runat="server" Height="200"
                                            TextMode="MultiLine" Width="1200"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="ele_sep">
                                </div>
                                <div class="ele_abm">
                                    <div class="lbl_abm">
                                        Descripcion</div>
                                    <div class="html_editor">
                                        <HTMLEditor:Editor runat="server" ID="txt_descripcion" NoUnicode="True" Height="300px"
                                            Width="100%" />
                                    </div>
                                </div>
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

