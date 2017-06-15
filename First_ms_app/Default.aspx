<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="First_ms_app._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <br />

    <asp:GridView 
        ID="CompaniesGridView"
        runat="server">
    </asp:GridView>
    <asp:label id="myLabel" runat="server" />
</asp:Content>
