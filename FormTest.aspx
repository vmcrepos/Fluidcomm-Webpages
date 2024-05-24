<%@ Page Title="" Language="C#" MasterPageFile="~/Greenco.Master" AutoEventWireup="true" CodeBehind="FormTest.aspx.cs" Inherits="GreenCo.FormTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="./Styles/Forms.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="scriptMan">
    </asp:ScriptManager>
    <asp:Panel runat="server" ID="pnlFormWrapper" CssClass="divFormWrapper" >
        <asp:Panel runat="server" ID="pnlFormFloater" CssClass="divFormFloater" >
            <div class="divFormItemFull">
                Textbox: <telerik:RadTextBox ID="txtTest" runat="server" Width="300" />
            </div>
            <div class="divFormItemHalf">
                Dropdown: <telerik:RadDropDownList runat="server" ID="cmbTest" AutoPostBack="true" OnSelectedIndexChanged="cmbTest_SelectedIndexChanged" />
            </div>
            <div class="divFormItemHalf">
                Button: <telerik:RadButton runat="server" ID="btnTest" OnClick="btnTest_Click" Text="Change Color" />
            </div>
        </asp:Panel>
    </asp:Panel>
    

</asp:Content>
