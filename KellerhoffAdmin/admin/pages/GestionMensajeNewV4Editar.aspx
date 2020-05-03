<%@ Page Title="" Language="C#" MasterPageFile="~/master/BaseAdmin.master" AutoEventWireup="true" CodeFile="GestionMensajeNewV4Editar.aspx.cs" Inherits="admin_pages_GestionMensajeNewV4Editgar" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="HTMLEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
        function MostrarMensaje() {
            //var asunto = $('#txtAsunto').val();
            //var mensaje = $('#textareaHtml').val();
            var editorControl = $get('MainContent_txt_mensaje').control;
            var cuerpo = editorControl.get_content();
            var asunto = $('#MainContent_txt_asunto').val();
            var mensaje = cuerpo;
            if (isNotNullEmpty(asunto) && isNotNullEmpty(mensaje)) {
                vistaPreviaMensajeNew(asunto, mensaje);
            }

            return false;
        }
        function vistaPreviaMensajeNew(pAsunto, pMensaje) {
            PageMethods.vistaPreviaMensajeNew(pAsunto, pMensaje, OnCallBackvistaPreviaMensajeNew, OnFail);
        }

        function OnCallBackvistaPreviaMensajeNew(args) {
            window.open('GestionMensajeV3VistaPreviaV2.aspx', '_blank');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="botones_form">
        <asp:Button ID="cmd_guardar" runat="server" Text="GUARDAR" CssClass="btn_abm" OnClick="cmd_guardar_Click" />
        <asp:Button ID="cmd_cancelar" runat="server" Text="VOLVER" CssClass="btn_abm" CausesValidation="False"
            OnClick="cmd_cancelar_Click" />
    </div>
    <div class="form_datos" style="width: 100%;">
        <div class="ele_abm">
            <div class="lbl_abm">
                Preview Mensaje
            </div>
            <div class="html_editor">
                <button onclick="MostrarMensaje();return false;">Vista previa</button>
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
                    <div class="ele_abm">
                        <div class="lbl_abm">

                            <asp:RadioButton ID="RadioButtonCliente" runat="server" GroupName="selectClienteSucursal" AutoPostBack="True" Text="Cliente" OnCheckedChanged="RadioButtonCliente_CheckedChanged" Checked="True" />

                            Todas las sucursales
                        </div>

                    </div>
                </td>
                <td></td>
            </tr>
            <tr>
                <td valign="top">
                    <div class="ele_abm">
                        <div class="lbl_abm">

                            <asp:RadioButton ID="RadioButtonSucursal" runat="server" GroupName="selectClienteSucursal" AutoPostBack="True" Text="Sucursal" OnCheckedChanged="RadioButtonSucursal_CheckedChanged" />
                            Elegir sucursales:
                        </div>

                    </div>
                </td>
                <td valign="top">

                    <div class="html_editor">
                        <asp:HiddenField runat="server"></asp:HiddenField>
                        <asp:CheckBoxList ID="CheckBoxListSucursales" runat="server" DataSourceID="ObjectDataSource1" DataTextField="suc_nombre" DataValueField="suc_codigo" OnDataBound="CheckBoxListSucursales_DataBound"></asp:CheckBoxList>
                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="RecuperarTodasSucursales"
                            TypeName="WebService"></asp:ObjectDataSource>

                    </div>
                </td>
            </tr>
        </table>
        <div class="ele_sep">
        </div>
        <div class="ele_abm">
            <div class="lbl_abm">
                Con Fechas
            </div>
            <div class="html_editor">
                <asp:CheckBox ID="checkboxImportante" runat="server" AutoPostBack="True" OnCheckedChanged="checkboxImportante_CheckedChanged" />
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
                    <asp:Calendar ID="CalendarFechaDesde" runat="server"></asp:Calendar>
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
</asp:Content>

