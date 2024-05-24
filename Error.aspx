<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="GreenCo.Error" MasterPageFile="~/Greenco.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Use for style and script links in head--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="ContentWrapper" runat="server" CssClass="errorWrapper" >
        <h1>Error:</h1>
        <asp:Literal ID="litMessage" runat="server" ></asp:Literal>
    </asp:Panel>
</asp:Content>
