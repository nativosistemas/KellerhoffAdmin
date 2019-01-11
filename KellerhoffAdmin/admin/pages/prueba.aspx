<%@ Page Title="" Language="C#" MasterPageFile="~/master/BaseAdmin.master" AutoEventWireup="true" CodeFile="prueba.aspx.cs" Inherits="admin_pages_prueba" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:RadioButton ID="RadioButtonCliente" runat="server" GroupName="selectClienteSucursal"   Text="Cliente" OnCheckedChanged="RadioButtonCliente_CheckedChanged" Checked="true"/>
    <asp:RadioButton ID="RadioButtonSucursal" runat="server" GroupName="selectClienteSucursal"  Text="Sucursal" OnCheckedChanged="RadioButtonSucursal_CheckedChanged" AutoPostBack="true" />
    <asp:CheckBoxList ID="CheckBoxList1" runat="server" DataSourceID="ObjectDataSource1" DataTextField="suc_nombre" DataValueField="suc_codigo"></asp:CheckBoxList>
   <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="RecuperarTodasSucursales"
                                            TypeName="WebService">
                                        </asp:ObjectDataSource>


</asp:Content>



