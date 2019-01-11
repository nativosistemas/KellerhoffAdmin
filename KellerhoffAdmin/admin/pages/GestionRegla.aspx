<%@ Page Title="" Language="C#" MasterPageFile="~/master/BaseAdmin.master" AutoEventWireup="true" CodeFile="GestionRegla.aspx.cs" Inherits="admin_pages_GestionRegla" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../../includes/js/GestionRegla.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
  <div class="titulo_pagina">
        Reglas</div>
    <div id="divVentanaMensajes" class="divVentanaMensajesRegla">
        <div style="float: right">
            <img src="../../img/iconos/delete.png" onclick="clickCerrarMensajes()" alt="Cerrar mensajes"
                title="Cerrar mensaje" style="cursor: pointer; width: 12px; height: 12px; padding: 2px;" />
        </div>
        <p id="pMensaje">
        </p>
        <br />
    </div>
    <div class="contenedorGeneralRegla">
        <table>
            <tr>
                <td align="right">
                    <asp:Label ID="lblRegla" runat="server" Text="Regla:"></asp:Label>
                </td>
                <td colspan="5">
                    &nbsp;
                    <div id="divComboRegla" style="float: left">
                    </div>
                    <p>
                    </p>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblNombreRegla" runat="server" Text="Descripción:"></asp:Label>
                </td>
                <td>
                    <input type="text" id="txtNombreReglaInput" class="anchoControlRegla" maxlength="255" />
                </td>
                <td>
                </td>
                <td align="right">
                    &nbsp;
                    <asp:Label ID="lblPalabraClave" runat="server" Text="Clave:"></asp:Label>
                </td>
                <td>
                    <input type="text" id="txtPalabraClaveInput" class="anchoControlRegla txtPalabraClaveInputRegla" maxlength="50" />
                    &nbsp;
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <p>
                    </p>
                </td>
            </tr>
            <tr>
                <td align="right">
                    &nbsp;
                    <asp:Label ID="lblContenida" runat="server" Text="Contenida en:"></asp:Label>
                </td>
                <td>
                    <div id='divCombo' class="ComboRegla">
                    </div>
                </td>
                <td colspan="4">
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <p>
                    </p>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    &nbsp;
                    <asp:Label ID="lblAcciones" runat="server" Text="Acciones:" Font-Size="Small" Font-Underline="True"></asp:Label>
                    <p>
                    </p>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <table>
                        <tr>
                            <td>
                            </td>
                            <td align="center" style="border: thin ridge">
                                <asp:Label ID="lblActiva" runat="server" Text="Activa" Width="80px"></asp:Label>
                            </td>
                            <td align="center" style="border: thin ridge">
                                <asp:Label ID="lblAgregar" runat="server" Text="Agregar" Width="80px"></asp:Label>
                            </td>
                            <td align="center" style="border: thin ridge">
                                <asp:Label ID="lblEditar" runat="server" Text="Editar" Width="80px"></asp:Label>
                            </td>
                            <td align="center" style="border: thin ridge">
                                <asp:Label ID="lblEliminar" runat="server" Text="Eliminar" Width="80px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="vertical-align: middle">
                                &nbsp;
                                <asp:Label ID="lblSoportadas" runat="server" Text="Soportadas"></asp:Label>
                                &nbsp;&nbsp;
                            </td>
                            <td align="center" style="border-style: groove">
                                <asp:CheckBox ID="CheckBoxSoportadasActiva" runat="server" Checked="true" Enabled="False"
                                    ClientIDMode="Static" />
                            </td>
                            <td align="center" style="border-style: groove">
                                <asp:CheckBox ID="CheckBoxSoportadasAgregar" runat="server" ClientIDMode="Static" />
                            </td>
                            <td align="center" style="border-style: groove">
                                <asp:CheckBox ID="CheckBoxSoportadasEditar" runat="server" ClientIDMode="Static" />
                            </td>
                            <td align="center" style="border-style: groove">
                                <asp:CheckBox ID="CheckBoxSoportadasEliminar" runat="server" ClientIDMode="Static" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <p>
                    </p>
                    <p>
                    </p>
                </td>
            </tr>
            <tr>
                <td colspan="6" class="styleBotoneraRegla">
                    <div style="float: right">
                        <button id="btnGuardar"  class="btn_abm" onclick="return clikGuardar()" >
                            Guardar</button>
                        &nbsp;
                    </div>
                    <div style="float: right">
                        <button id="btnEliminar" class="btn_abm" onclick="return clikEliminar()">
                            Eliminar</button>
                        &nbsp;
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

