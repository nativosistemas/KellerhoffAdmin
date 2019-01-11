<%@ Page Title="" Language="C#" MasterPageFile="~/master/BaseAdmin.master" AutoEventWireup="true"
    CodeFile="GestionSucursal.aspx.cs" Inherits="admin_pages_GestionSucursal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function someFunction() {
            var sucur = $('#jCmbSucursal').val();
            PageMethods.RecuperarSucursalesDependientes(sucur, OnCallBackRecuperarSucursalesDependientes, OnFail);
        }
        function OnCallBackRecuperarSucursalesDependientes(args) {
            var listaSucursal = eval('(' + args + ')');
            $('#jCmbSucursalDependiente')[0].options.length = 0;
            for (var i = 0; i < listaSucursal.length; i++) {
                $('#jCmbSucursalDependiente').append(new Option(listaSucursal[i], listaSucursal[i])); //, true, true
            }

            //            alert('OK');
        }
        function CargarSucursal() {
            var sucur = $('#jCmbSucursal').val();
            var sucurDependiente = $('#jCmbSucursalDependiente').val();
            if (sucur != null && sucurDependiente != null) {
                PageMethods.InsertarActualizarSucursal(sucur, sucurDependiente, OnCallBackInsertarActualizarSucursal, OnFail);
            } else {
                alert('Complete los combos de sucursales');
            }
            return false;
        }

        function OnCallBackInsertarActualizarSucursal(args) {
            location.href = 'GestionSucursal.aspx';

        }
        function ComboVacio() {
//            $('#jCmbSucursal')[0].selectedIndex = -1;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="titulo_pagina">
        Sucursales</div>
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
    </asp:UpdateProgress>--%>
    <%--    <asp:UpdatePanel ID="up_general" runat="server">
        <ContentTemplate>--%>
    <asp:Panel ID="pnl_grilla" runat="server">
        <asp:TextBox ID="txt_buscar" runat="server" CssClass="text_abm"></asp:TextBox>
        <asp:Button ID="cmd_buscar" runat="server" Text="BUSCAR" CssClass="btn_abm" CausesValidation="False"
            OnClick="cmd_buscar_Click" />
        <asp:Button ID="cmd_nuevo" runat="server" Text="NUEVO" CssClass="btn_abm" CausesValidation="False"
            OnClick="cmd_nuevo_Click" />
        <asp:GridView ID="gv_datos" runat="server" CssClass="grilla" AllowPaging="True" AllowSorting="True"
            PageSize="30" CellPadding="2" DataSourceID="odsSucursal" AutoGenerateColumns="False"
            OnRowCommand="gv_datos_RowCommand">
            <AlternatingRowStyle BackColor="#F9F9F9" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="modificar" runat="server" ImageUrl="~/img/iconos/delete2.png"
                            OnClientClick="return confirm('¿Confirma que desea eliminar?');" Width="16px"
                            Height="16px" CausesValidation="False" CommandArgument='<%# Bind("sde_codigo") %>'
                            CommandName="Eliminar" ToolTip="Eliminar"></asp:ImageButton>
                    </ItemTemplate>
                    <ItemStyle Width="20px" HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:BoundField DataField="sde_codigo" HeaderText="Codigo" SortExpression="sde_codigo"
                    Visible="false" />
                <asp:BoundField DataField="sde_sucursal" HeaderText="Sucursal" SortExpression="sde_sucursal" />
                <asp:BoundField DataField="sde_sucursalDependiente" HeaderText="Sucursal dependiente"
                    SortExpression="sde_sucursalDependiente" />
            </Columns>
            <HeaderStyle CssClass="enc_grilla" />
            <PagerStyle CssClass="pag_grilla" />
            <RowStyle CssClass="row_grilla" />
        </asp:GridView>
        <%--SortParameterName="sortExpression"--%>
        
     <asp:ObjectDataSource  ID="odsSucursal"  runat="server" SortParameterName="sortExpression"
                    SelectMethod="GetSucursales" TypeName="SitioBase.clases.AccesoGrilla">
                    <SelectParameters>
                        <asp:SessionParameter Name="pFiltro" SessionField="GestionSucursal_Filtro" 
                            Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
   <%--     <asp:ObjectDataSource ID="odsSucursal" runat="server" SelectMethod="RecuperarTodasSucursalesDependientes"
            TypeName="WebService"></asp:ObjectDataSource>--%>
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
                                Sucursal
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cmbSucursal"
                                            Display="Dynamic" ErrorMessage="Completar" CssClass="msg_error"></asp:RequiredFieldValidator>--%>
                            </div>
                            <%-- <asp:DropDownList ID="cmbSucursal" runat="server" DataSourceID="odsSucursalCombo"
                                        DataTextField="sde_sucursal" DataValueField="sde_codigo" Width="300px" AutoPostBack="true"
                                        OnSelectedIndexChanged="cmbSucursal_SelectedIndexChanged">
                                    </asp:DropDownList>--%>
                            <select id="jCmbSucursal" style="width: 300px;" onchange="someFunction();" >
                                <%                       
                                    string strHtmlCombo = string.Empty;
                                    List<cSucursal> listaSucursales = WebService.RecuperarTodasSucursales();
                                    foreach (cSucursal item in listaSucursales)
                                    {
                                        strHtmlCombo += "<option value=\"" + item.sde_sucursal + "\" >" + item.sde_sucursal + "</option>";
                                    }
                                    Response.Write(strHtmlCombo);                                        
                                %>
                            </select>
                        </div>
                        <div class="ele_abm">
                            <div class="lbl_abm">
                                Sucursal dependiente
                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cmbSucursalDependiente"
                                            Display="Dynamic" ErrorMessage="Completar" CssClass="msg_error"></asp:RequiredFieldValidator>--%>
                            </div>
                            <%-- <asp:DropDownList ID="cmbSucursalDependiente" runat="server" Width="300px">
                                    </asp:DropDownList>--%>
                            <select id="jCmbSucursalDependiente" style="width: 300px;" >
                            </select>
                        </div>
                    </div>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>
        <%--                <asp:ObjectDataSource ID="odsSucursalCombo" runat="server" SelectMethod="RecuperarTodasSucursales"
                    TypeName="WebService"></asp:ObjectDataSource>--%>
        <%--           <asp:ObjectDataSource ID="odsSucursalDependiente" runat="server" SelectMethod="RecuperarTodasSucursales"
                    TypeName="WebService"></asp:ObjectDataSource>--%>
    </asp:Panel>
    <%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
