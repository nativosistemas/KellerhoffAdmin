<%@ Page Title="" Language="C#" MasterPageFile="~/master/BaseAdmin.master" AutoEventWireup="true"
    CodeFile="GestionCategoriaRestriccion.aspx.cs" Inherits="admin_pages_GestionCategoriaRestriccion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="titulo_pagina">
       Cadeteria - Parámetro
    </div>
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
            <asp:ObjectDataSource ID="odsCadeteriaRestricciones" runat="server" SortParameterName="sortExpression"
                SelectMethod="GetCadeteriaRestricciones" TypeName="SitioBase.clases.AccesoGrilla">
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
                    PageSize="30" CellPadding="2" DataSourceID="odsCadeteriaRestricciones" AutoGenerateColumns="False"
                    OnRowCommand="gv_datos_RowCommand">
                    <AlternatingRowStyle BackColor="#F9F9F9" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="modificar" runat="server" ImageUrl="~/img/iconos/edit.png" Width="16px"
                                    Height="16px" CausesValidation="False" CommandArgument='<%# Bind("tcr_id") %>'
                                    CommandName="Modificar" ToolTip="Modificar"></asp:ImageButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="eliminar" runat="server" ImageUrl="~/img/iconos/delete2.png"
                                    Width="16px" Height="16px" CausesValidation="False" OnClientClick="return confirm('¿Confirma que desea cambiar eliminar?');"
                                    CommandArgument='<%# Bind("tcr_id") %>' CommandName="Eliminar" ToolTip="Eliminar">
                                </asp:ImageButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="tcr_id" HeaderText="tcr_id" SortExpression="tcr_id" Visible="false" />
                        <asp:BoundField DataField="suc_nombre" HeaderText="Sucursal" SortExpression="suc_nombre" />
                        <asp:BoundField DataField="tcr_UnidadesMinimas" HeaderText=" Unidades Mínimas" SortExpression="tcr_UnidadesMinimas" />
                        <asp:BoundField DataField="tcr_UnidadesMaximas" HeaderText="Unidades Máximas" SortExpression="tcr_UnidadesMaximas" />
                        <asp:BoundField DataField="tcr_MontoMinimo" HeaderText="Monto Mínimo" SortExpression="tcr_MontoMinimo" />
                        <asp:BoundField DataField="tcr_MontoIgnorar" HeaderText="Monto a Ignorar" SortExpression="tcr_MontoIgnorar" />
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
                                        Sucursal:
                                    </div>
                                    <asp:DropDownList ID="cmbSucursales" runat="server" DataSourceID="odsSucursales"
                                        DataTextField="suc_nombre" DataValueField="suc_codigo" Width="300px">
                                    </asp:DropDownList>
                                    <asp:ObjectDataSource ID="odsSucursales" runat="server" SelectMethod="RecuperarTodasSucursalesSinCategoriaRestriccion"
                                        TypeName="WebService"></asp:ObjectDataSource>
                                </div>
                                <div class="ele_sep">
                                </div>
                                <div class="ele_abm">
                                    <div class="lbl_abm">
                                        Unidades Mínimas:
                                    </div>
                                    <asp:TextBox ID="txtUnidadesMinimas" runat="server" Width="300px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorUnidadesMinimas" runat="server"
                                        ForeColor="Red" Display="Dynamic" ControlToValidate="txtUnidadesMinimas">   * </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorUnidadesMinimas" ValidationExpression="[0-9]*"
                                        ControlToValidate="txtUnidadesMinimas" ErrorMessage="Debe ser número entero"
                                        runat="server" ForeColor="Red" />
                                </div>
                                <div class="ele_sep">
                                </div>
                                <div class="ele_abm">
                                    <div class="lbl_abm">
                                        Unidades Máximas:
                                    </div>
                                    <asp:TextBox ID="txtUnidadesMaximas" runat="server" Width="300px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="txtUnidadesMaximas">   * </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="[0-9]*"
                                        ControlToValidate="txtUnidadesMaximas" ErrorMessage="Debe ser número entero"
                                        runat="server" ForeColor="Red" />
                                </div>
                                <div class="ele_sep">
                                </div>
                                <div class="ele_abm">
                                    <div class="lbl_abm">
                                        Monto Mínimo:
                                    </div>
                                    <asp:TextBox ID="txtMontoMinimo" runat="server" Width="300px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="txtMontoMinimo">   * </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationExpression="\d*\,?\d*"
                                        ControlToValidate="txtMontoMinimo" ErrorMessage="Debe ser número decimal" runat="server"
                                        ForeColor="Red" />
                                </div>
                                <div class="ele_sep">
                                </div>
                                <div class="ele_abm">
                                    <div class="lbl_abm">
                                        Monto a Ignorar:
                                    </div>
                                    <asp:TextBox ID="txtMontoIgnorar" runat="server" Width="300px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="txtMontoIgnorar">   * </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationExpression="\d*\,?\d*"
                                        ControlToValidate="txtMontoIgnorar" ErrorMessage="Debe ser número decimal" runat="server"
                                        ForeColor="Red" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
