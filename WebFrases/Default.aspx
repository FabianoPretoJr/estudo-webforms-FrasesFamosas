<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMestre.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebFrases.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Seja-bem Vindo!!!</h2>
    <br />
    <asp:Label ID="Label1" runat="server" Text="Nome: "></asp:Label>
    <asp:Label ID="lblNome" runat="server" Text=""></asp:Label>
    <br />
    <asp:Label ID="Label3" runat="server" Text="E-mail: "></asp:Label>
    <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
    <br />
</asp:Content>
