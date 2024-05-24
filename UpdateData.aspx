<%@ Page Title="" Language="C#" MasterPageFile="~/Greenco.Master" AutoEventWireup="true" CodeBehind="UpdateData.aspx.cs" Inherits="GreenCo.UpdateData" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/jquery-ui-1.10.3.custom.min.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src='<%=ResolveClientUrl("~/scripts/jquery-1.9.1.js") %>' ></script>
    <script type="text/javascript" src='<%=ResolveClientUrl("~/scripts/jquery-ui-1.10.3.custom.min.js") %>' ></script>
    <script language="javascript" type="text/jscript">
        $(function () { $('#<%= txtDate1.ClientID %>').datepicker({ dateFormat: "yy-mm-dd" }); });
        $(function () { $('#<%= txtDate2.ClientID %>').datepicker({ dateFormat: "yy-mm-dd" }); });
        function pageLoad() {
            $(function () { $('#<%= txtDate1.ClientID %>').datepicker({ dateFormat: "yy-mm-dd" }); });
            $(function () { $('#<%= txtDate2.ClientID %>').datepicker({ dateFormat: "yy-mm-dd" }); });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="scriptMan" ></asp:ScriptManager>
    Note: It is not recommended to process more than a week at a time.<br />
    <div style="width: 100px; float: left;" >From:</div><asp:TextBox runat="server" ID="txtDate1" /><br />
    <div style="width: 100px; float: left;" >To:</div><asp:TextBox runat="server" ID="txtDate2" /><br />
    <asp:Button runat="server" ID="btnProcess" OnClientClick="this.disabled = true; this.value = 'Processing...';" UseSubmitBehavior="false" OnClick="btnSubmit_Click" Text="Process" />
    <asp:Label runat="server" ID="lblResult" Text=""/>
    <br />
    <br />
    <br />
    
    
</asp:Content>
