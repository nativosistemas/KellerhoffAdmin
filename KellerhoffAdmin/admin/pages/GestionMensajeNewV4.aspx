<%@ Page Title="" Language="C#" MasterPageFile="~/master/BaseAdmin.master" AutoEventWireup="true" CodeFile="GestionMensajeNewV4.aspx.cs" Inherits="admin_pages_GestionMensajeNewV4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="HTMLEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
       <link href="../../includes/css/jquery-ui.css" rel="stylesheet"
        type="text/css" />
       <script src="../../includes/js/jquery-ui.js" type="text/javascript"></script>
        <script type="text/javascript">
            var isPrimero = true;
            jQuery(document).ready(function () {

            });
            function MostrarMensajeTest(pAsunto, pCuerpo) {
                if (isPrimero) {
                    $("#dialogMensaje")[0].title = pAsunto;
                    isPrimero = false;
                }
                else
                    $("#ui-id-1").html(pAsunto);
                $("#dialogMensajeTexto").html(pCuerpo);
                $("#dialogMensaje").dialog({
                    width: 500,
                    height: 230,
                    modal: true
                });
            }
            function onclickMensajeTest() {
                //$('#MainContent_Tabs_tab1_txt_mensaje').val()
               // var cuerpo = $find("txt_mensaje").get_content();
                var editorControl = $get('MainContent_Tabs_tab1_txt_mensaje').control;
                var cuerpo = editorControl.get_content();

                MostrarMensajeTest($('#MainContent_Tabs_tab1_txt_asunto').val(), cuerpo);


            }
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div id="dialogMensaje" title="" style="display:none;">
        <pre id="dialogMensajeTexto" style="text-align:left;padding-bottom:0px;font-size: 12px; font-family: Arial, Helvetica, sans-serif;">
            </pre>
    </div>
    <div class="titulo_pagina">
        Alertas
    </div>
    <asp:UpdateProgress ID="up_prog_0" AssociatedUpdatePanelID="up_general" runat="server">
        <ProgressTemplate>
            <div class="div_loading">
                <table border="0">
                    <tr>
                        <td>
                            <asp:Image ID="img_loader" runat="server" ImageUrl='~/img/varios/ajax-loader.gif' />
                        </td>
                        <td>Espere por favor!
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
                    PageSize="30" CellPadding="2" DataSourceID="odsMensaje" AutoGenerateColumns="False"
                    OnRowCommand="gv_datos_RowCommand">
                    <AlternatingRowStyle BackColor="#F9F9F9" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="modificar" runat="server" ImageUrl="~/img/iconos/edit.png" Width="16px"
                                    Height="16px" CausesValidation="False" CommandArgument='<%# Bind("tme_codigo") %>'
                                    CommandName="Modificar" ToolTip="Modificar"></asp:ImageButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="eliminar" runat="server" ImageUrl="~/img/iconos/delete2.png"
                                    Width="16px" Height="16px" CausesValidation="False" OnClientClick="return confirm('¿Confirma que desea eliminar?');"
                                    CommandArgument='<%# Bind("tme_codigo") %>' CommandName="Eliminar" ToolTip="Eliminar"></asp:ImageButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="tme_codigo" HeaderText="Código" SortExpression="not_codNoticia"
                            Visible="false" />
                        <asp:BoundField DataField="tme_asunto" HeaderText="Asunto" SortExpression="tme_asunto" />
<%--                        <asp:BoundField DataField="cli_nombre" HeaderText="Cliente" SortExpression="cli_nombre" />--%>
                  <%--      <asp:BoundField DataField="tme_fechaToString" HeaderText="Fecha" SortExpression="tme_fechaToString" />--%>
<%--                        <asp:BoundField DataField="est_nombre" HeaderText="Estado" SortExpression="est_nombre" />--%>
                    <%--    <asp:BoundField DataField="tme_importanteToString" HeaderText="Importante" SortExpression="tme_importanteToString" />--%>
                        <asp:BoundField DataField="tme_fechaDesdeToString" HeaderText="Fecha Desde" SortExpression="tme_fechaDesdeToString" />
                        <asp:BoundField DataField="tme_fechaHastaToString" HeaderText="Fecha Hasta" SortExpression="tme_fechaHastaToString" />
                    </Columns>
                    <HeaderStyle CssClass="enc_grilla" />
                    <PagerStyle CssClass="pag_grilla" />
                    <RowStyle CssClass="row_grilla" />
                </asp:GridView>
                <asp:ObjectDataSource ID="odsMensaje" runat="server" SortParameterName="sortExpression"
                    SelectMethod="GetMensajesV4" TypeName="SitioBase.clases.AccesoGrilla">
                    <SelectParameters>
                        <asp:SessionParameter Name="pFiltro" SessionField="GestionMensaje_Filtro" Type="String" />
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
                                    <td class="txt_11">Ficha de datos
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
                                      Preview Mensaje
                                    </div>
                                    <div class="html_editor">
                                        <button onclick="onclickMensajeTest();return false;" >Vista previa</button>
                                    </div>
                                </div>
                                <div class="ele_sep">
                                </div>
                                <div class="ele_abm">
                                    <div class="lbl_abm">
                                        Asunto
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_asunto"
                                            Display="Dynamic" ErrorMessage="Completar" CssClass="msg_error"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="html_editor">
                                        <asp:TextBox ID="txt_asunto" CssClass="text_abm" runat="server" MaxLength="300" Width="450px"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="ele_sep">
                                </div>
                                <div class="ele_abm">
                                    <div class="lbl_abm">
                                        Mensaje
                                    </div>
                                    <div class="html_editor">
                                            <HTMLEditor:Editor runat="server" ID="txt_mensaje" NoUnicode="True" Height="200px"
                                            Width="100%" />
                                    </div>
                                </div>
                                <div class="ele_sep">
                                </div>
                                <table>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">

                                        </td>
                                        <td valign="top">

                                            <div class="html_editor">
<asp:HiddenField  runat="server"></asp:HiddenField>
                                                <asp:CheckBoxList ID="CheckBoxListSucursales" runat="server" DataSourceID="ObjectDataSource1" DataTextField="suc_nombre" DataValueField="suc_codigo" OnLoad="CheckBoxListSucursales_Load1" ></asp:CheckBoxList>
                                                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="RecuperarTodasSucursales"
                                                    TypeName="WebService" OnDataBinding="ObjectDataSource1_DataBinding"></asp:ObjectDataSource>

                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <div class="ele_sep">
                                </div>
                                <div class="ele_abm">
                                    <div class="lbl_abm">
                                        Importante
                                    </div>
                                    <div class="html_editor">
                                        <asp:CheckBox ID="checkboxImportante" runat="server" AutoPostBack="True"
                                            OnCheckedChanged="checkboxImportante_CheckedChanged" />
                                    </div>
                                </div>
                    
                                <asp:Panel ID="PanelFecha" runat="server" Visible="False">
                                    <div class="ele_sep">
                                    </div>
                                    <div class="ele_abm">
                                        <div class="lbl_abm">
                                            Fecha desde
                                        </div>
                                        <div class="html_editor">
                                            <asp:Calendar ID="CalendarFechaDesde" runat="server" ></asp:Calendar>
                                        </div>
                                    </div>
                                    <div class="ele_sep">
                                    </div>
                                    <div class="ele_abm">
                                        <div class="lbl_abm">
                                            Fecha hasta
                                        </div>
                                        <div class="html_editor">
                                            <asp:Calendar ID="CalendarFechaHasta" runat="server"></asp:Calendar>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

