<%@ Page Title="" Language="C#" MasterPageFile="~/master/BaseAdmin.master" AutoEventWireup="true"
    CodeFile="GestionUsuario.aspx.cs" Inherits="admin_pages_GestionUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="titulo_pagina">
        Usuario</div>
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
                <asp:Button ID="cmd_nuevo" runat="server" Text="NUEVO" CssClass="btn_abm" CausesValidation="False"
                    OnClick="cmd_nuevo_Click" />
                <asp:GridView ID="gv_datos" runat="server" CssClass="grilla" AllowPaging="True" AllowSorting="True"
                    PageSize="30" CellPadding="2" DataSourceID="odsUsuarios" AutoGenerateColumns="False"
                    OnRowCommand="gv_datos_RowCommand">
                    <AlternatingRowStyle BackColor="#F9F9F9" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="modificar" runat="server" ImageUrl="~/img/iconos/edit.png" Width="16px"
                                    Height="16px" CausesValidation="False" CommandArgument='<%# Bind("usu_codigo") %>'
                                    CommandName="Modificar" ToolTip="Modificar"></asp:ImageButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="estado" runat="server" ImageUrl="~/img/iconos/Change.png" Width="16px"
                                    Height="16px" CausesValidation="False" OnClientClick="return confirm('¿Confirma que desea cambiar el estado?');"
                                    CommandArgument='<%# Bind("usu_codigo") %>' CommandName="Estado" ToolTip="Cambiar Estado">
                                </asp:ImageButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="Contraseña" runat="server" ImageUrl="~/img/iconos/bullet_key.png"
                                    Width="16px" Height="16px" CausesValidation="False" CommandArgument='<%# Bind("usu_codigo") %>'
                                    CommandName="Contraseña" ToolTip="Contraseña"></asp:ImageButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="eliminar" runat="server" ImageUrl="~/img/iconos/delete2.png"
                                    Width="16px" Height="16px" CausesValidation="False" OnClientClick="return confirm('¿Confirma que desea eliminar?');"
                                    CommandArgument='<%# Bind("usu_codigo") %>' CommandName="Eliminar" ToolTip="Eliminar">
                                </asp:ImageButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="usu_codigo" HeaderText="Codigo" SortExpression="usu_codigo"
                            Visible="false" />
                        <asp:BoundField DataField="usu_nombre" HeaderText="Nombre" SortExpression="usu_nombre" />
                        <asp:BoundField DataField="usu_apellido" HeaderText="Apellido" SortExpression="usu_apellido" />
                        <asp:BoundField DataField="usu_mail" HeaderText="Mail" SortExpression="usu_mail" />
                        <asp:BoundField DataField="usu_pswDesencriptado" HeaderText="Pass" SortExpression="usu_pswDesencriptado" />
                        <asp:BoundField DataField="usu_login" HeaderText="Login" SortExpression="usu_login" />
                        <asp:BoundField DataField="rol_Nombre" HeaderText="Rol" SortExpression="rol_Nombre" />
                         <asp:BoundField DataField="cli_nombre" HeaderText="Cliente" SortExpression="cli_nombre" /> 
                        <asp:BoundField DataField="usu_estadoToString" HeaderText="Estado" SortExpression="usu_estadoToString" />
                    </Columns>
                    <HeaderStyle CssClass="enc_grilla" />
                    <PagerStyle CssClass="pag_grilla" />
                    <RowStyle CssClass="row_grilla" />
                </asp:GridView>
                <asp:ObjectDataSource ID="odsUsuarios" runat="server" SortParameterName="sortExpression"
                    SelectMethod="GetUsuarios" TypeName="SitioBase.clases.AccesoGrilla">
                    <SelectParameters>
                        <asp:SessionParameter Name="pFiltro" SessionField="GestionUsuario_Filtro" Type="String" />
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
                                        Nombre
                                        <asp:RequiredFieldValidator ID="rv_nombre" runat="server" ControlToValidate="txtNombre"
                                            Display="Dynamic" ErrorMessage="Completar" CssClass="msg_error"></asp:RequiredFieldValidator>
                                    </div>
                                    <asp:TextBox ID="txtNombre" CssClass="text_abm" runat="server" MaxLength="50"></asp:TextBox>
                                </div>
                                <div class="ele_abm">
                                    <div class="lbl_abm">
                                        Apellido
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtApellido"
                                            Display="Dynamic" ErrorMessage="Completar" CssClass="msg_error"></asp:RequiredFieldValidator>
                                    </div>
                                    <asp:TextBox ID="txtApellido" CssClass="text_abm" runat="server" MaxLength="50"></asp:TextBox>
                                </div>
                                <div class="ele_abm">
                                    <div class="lbl_abm">
                                        Mail
                  <%--                      <asp:CustomValidator ID="CustomValidatorMail" runat="server" ErrorMessage="Mail repetido"
                                            CssClass="msg_error" ControlToValidate="txtMail" OnServerValidate="CustomValidatorMail_ServerValidate"></asp:CustomValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMail"
                                            Display="Dynamic" ErrorMessage="Completar" CssClass="msg_error"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <asp:TextBox ID="txtMail" CssClass="text_abm" runat="server" MaxLength="255"></asp:TextBox>
                                </div>
                                <div class="ele_abm">
                                    <div class="lbl_abm">
                                        Nombre login
                                        <asp:CustomValidator ID="CustomValidatorLogin" runat="server" ErrorMessage="Login repetido" CssClass="msg_error" ControlToValidate="txtLogin" OnServerValidate="CustomValidatorLogin_ServerValidate">
                                        </asp:CustomValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtLogin"
                                            Display="Dynamic" ErrorMessage="Completar" CssClass="msg_error"></asp:RequiredFieldValidator>
                                    </div>
                                    <asp:TextBox ID="txtLogin" CssClass="text_abm" runat="server" MaxLength="50"></asp:TextBox>
                                </div>
                                <asp:Panel ID="PanelContraseña" runat="server">
                                    <div class="ele_abm">
                                        <div class="lbl_abm">
                                            Contraseña
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtContraseña"
                                                Display="Dynamic" ErrorMessage="Completar" CssClass="msg_error"></asp:RequiredFieldValidator>
                                        </div>
                                        <asp:TextBox ID="txtContraseña" CssClass="text_abm" runat="server" MaxLength="255"></asp:TextBox>
                                    </div>
                                    <div class="ele_abm">
                                        <div class="lbl_abm">
                                            Repetir contraseña
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtRepetirContraseña"
                                                Display="Dynamic" ErrorMessage="Completar" CssClass="msg_error"></asp:RequiredFieldValidator>
                                        </div>
                                        <asp:TextBox ID="txtRepetirContraseña" CssClass="text_abm" runat="server" MaxLength="255"></asp:TextBox>
                                    </div>
                                </asp:Panel>
                                <div class="ele_abm">
                                    <div class="lbl_abm">
                                        Rol
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cmbRol"
                                            Display="Dynamic" ErrorMessage="Completar" CssClass="msg_error"></asp:RequiredFieldValidator>
                                    </div>
                                    <asp:DropDownList ID="cmbRol" runat="server" DataSourceID="odsRol" DataTextField="rol_Nombre"
                                        DataValueField="rol_codRol" Width="300px" OnSelectedIndexChanged="cmbRol_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                                <div class="ele_abm">
                                    <div class="lbl_abm">
                                        Clientes
                                    </div>
                                    <asp:DropDownList ID="cmbCliente" runat="server" Width="300px" DataSourceID="odsClientes"
                                        DataTextField="cli_nombre" DataValueField="cli_codigo">
                                    </asp:DropDownList>
                                </div>
                                <div class="ele_abm">
                                    <div class="lbl_abm">
                                        Observaciones
                                    </div>
                                    <asp:TextBox ID="txtObservaciones1" CssClass="text_abm_100" runat="server" Height="200px"
                                        TextMode="MultiLine" Width="900px"></asp:TextBox>
                                </div>
                            </div>
                            <div class="lbl_abm">
                                <asp:CompareValidator ID="CompareValidatorContraseña" runat="server" ErrorMessage="Las contraseñas no son iguales"
                                    ControlToCompare="txtContraseña" ControlToValidate="txtRepetirContraseña" CssClass="msg_error"></asp:CompareValidator>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
                <asp:ObjectDataSource ID="odsRol" runat="server" SelectMethod="RecuperarTodasRoles_Combo"
                    TypeName="SitioBase.clases.Seguridad"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="odsClientes" runat="server" SelectMethod="RecuperarTodosClientesActivosYSinClientes"
                    TypeName="FuncionesPersonalizadas"></asp:ObjectDataSource>
            </asp:Panel>
            <asp:Panel ID="pnl_Contraseña" runat="server" Visible="false">
                <div class="botones_form">
                    <asp:Button ID="btnGuardarContraseña" runat="server" Text="GUARDAR" CssClass="btn_abm"
                        OnClick="btnGuardarContraseña_Click" />
                    <asp:Button ID="Button2" runat="server" Text="CANCELAR" CssClass="btn_abm" CausesValidation="False"
                        OnClick="cmd_cancelar_Click" />
                </div>
                <div class="ele_abm">
                    <div class="lbl_abm">
                        Contraseña
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtContraseñaCambiar"
                            Display="Dynamic" ErrorMessage="Completar" CssClass="msg_error"></asp:RequiredFieldValidator>
                    </div>
                    <asp:TextBox ID="txtContraseñaCambiar" CssClass="text_abm" runat="server" MaxLength="255"
                        TextMode="Password"></asp:TextBox>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
