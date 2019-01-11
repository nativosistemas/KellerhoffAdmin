<%@ Page Title="" Language="C#" MasterPageFile="~/master/BaseAdmin.master" AutoEventWireup="true" CodeFile="GestionRolesYReglas.aspx.cs" Inherits="admin_pages_GestionRolesYReglas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script src="../../includes/js/GestionRolesYReglas.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
  <div class="titulo_pagina">
       Roles y reglas</div>
 <div id="divSectorArbol" class="cssDivArbol">
    </div>
    <div>
        <asp:Label ID="lblRol" runat="server" Text="Rol:"></asp:Label>
        &nbsp;
        <asp:DropDownList ID="cmbRoles" runat="server" DataSourceID="odsRol" CssClass="anchoControlRegla" DataTextField="rol_Nombre"
            DataValueField="rol_codRol" onChange="ChangeIndexComboRol(this)" ClientIDMode="Static" >
        </asp:DropDownList>
        &nbsp; &nbsp; &nbsp; &nbsp;
        <button id="btnGuardar" class="btn_abm" onclick="return ClickGuardar()">
            Guardar</button>
    </div>
    <asp:ObjectDataSource ID="odsRol" runat="server" SelectMethod="RecuperarTodasRoles"
        TypeName="SitioBase.clases.Seguridad">
        <SelectParameters>
            <asp:Parameter DefaultValue="" Name="pFiltro" 
                Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

