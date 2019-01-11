<%@ Page Title="" Language="C#" MasterPageFile="~/master/BaseAdmin.master" AutoEventWireup="true"
    CodeFile="GestionHorarioSucursal.aspx.cs" Inherits="admin_pages_GestionHorarioSucursal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function CambiarSucursalDependiente(pValor) {
            //            var sucur = $('#jCmbSucursal').val();
            //            PageMethods.RecuperarSucursalesDependientes(sucur, OnCallBackRecuperarSucursalesDependientes, OnFail);
            alert('Ok');
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="titulo_pagina">
        Horarios</div>
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
            <table>
                <tr>
                    <td>
                        Sucursal y sucursal dependiente:
                    </td>
                    <td>
                        <asp:DropDownList ID="cmbSucursalDependiente" runat="server" DataSourceID="odsSucursalDependiente"
                            DataTextField="nombre" DataValueField="id" AutoPostBack="True" OnSelectedIndexChanged="cmbSucursalDependiente_SelectedIndexChanged">
                        </asp:DropDownList>

                    </td>
                     <td>
                     <div style="width:20px;"></div>
                      </td>
                      <td>
                       Código de reparto:
                    </td>
                    <td>
                        <asp:DropDownList ID="cmdCodigoReparto" runat="server" DataSourceID="odsCodigoReparto"
                            DataTextField="nombre" DataValueField="nombre" AutoPostBack="True" 
                            onselectedindexchanged="cmdCodigoReparto_SelectedIndexChanged">
                        </asp:DropDownList><%-- OnSelectedIndexChanged="cmbSucursalDependiente_SelectedIndexChanged"--%>
                    </td>
                </tr>
            </table>
            <asp:ObjectDataSource ID="odsSucursalDependiente" runat="server" SelectMethod="CargarComboSucursalesDependiente"
                TypeName="SitioBase.clases.cBaseAdmin" OnSelected="odsSucursalDependiente_Selected">
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="odsCodigoReparto" runat="server" SelectMethod="CargarComboTodosCodigoReparto"
                TypeName="SitioBase.clases.cBaseAdmin">
            </asp:ObjectDataSource>
            <asp:Panel ID="pnl_grilla" runat="server">
                <asp:GridView ID="gv_datos" runat="server" CssClass="grilla" AllowPaging="True" AllowSorting="False"
                    PageSize="30" CellPadding="2" AutoGenerateColumns="False" OnRowCommand="gv_datos_RowCommand">
                    <AlternatingRowStyle BackColor="#F9F9F9" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="modificar" runat="server" ImageUrl="~/img/iconos/edit.png" Width="16px"
                                    Height="16px" CausesValidation="False" CommandArgument='<%# Bind("dia") %>' CommandName="Modificar"
                                    ToolTip="Modificar"></asp:ImageButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="dia" HeaderText="dia" SortExpression="dia" />
                        <asp:BoundField DataField="hora" HeaderText="hora" SortExpression="hora" />
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
                                        Horarios:
                                    </div>
                                    <asp:TextBox ID="txt_nombre" CssClass="text_abm" runat="server" MaxLength="250" Width="300"></asp:TextBox>
                                </div>
                                <div class="ele_abm">
                                    <div class="lbl_abm" style="color: green !important">
                                        Ej:
                                        <br />
                                        --> 09:20-12:30-14:00 varios horarios
                                        <br />
                                        --> 12:30 un solo horario
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
