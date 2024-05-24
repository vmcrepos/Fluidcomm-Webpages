<%@ Page Title="" Language="C#" MasterPageFile="~/Greenco.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GreenCo.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="./Styles/Gauges.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="scriptMan" ></asp:ScriptManager>
    <asp:Panel runat="server" ID="pnlPlaceholder" ></asp:Panel>
</asp:Content>
