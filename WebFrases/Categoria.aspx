<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMestre.Master" AutoEventWireup="true" CodeBehind="Categoria.aspx.cs" Inherits="WebFrases.Categoria" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" GroupingText="Cadastro/Alterações de Categorias">
        <asp:Label ID="Label1" runat="server" Text="ID: "></asp:Label>
        <br />
        <asp:TextBox ID="txtId" runat="server" Enabled="false"></asp:TextBox>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Nome da Categoria: "></asp:Label>
        <br />
        <asp:TextBox ID="txtNome" runat="server" Width="570px"></asp:TextBox>
        <br />
        <asp:Button ID="btnSalvar" runat="server" Text="Inserir" />
        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CausesValidation="False" />
    </asp:Panel>
</asp:Content>
