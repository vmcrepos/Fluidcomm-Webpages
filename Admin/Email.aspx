<%@ Page Title="" Language="C#" MasterPageFile="~/Greenco.Master" AutoEventWireup="true" CodeBehind="Email.aspx.cs" Inherits="GreenCo.Admin.Email" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/admin.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divFullColumn">
        <div class="NewUserCenter">
            <div class="NewUserWrapper" >
                <div class="divTitle"><asp:Label runat="server" Text="Change Email" ID="lblTitle" /></div>
                <div class="newUserLabel"><asp:Label runat="server" ID="lblName" Text="User Name" /></div>
                <div class="newUserControl">
                    <asp:Label runat="server" ID="lblUserName" Text="N/A" />
                </div>
                <div class="newUserLabel"><asp:Label runat="server" ID="lblEmail" Text="New Email" /></div>
                <div class="newUserControl">
                    <asp:TextBox runat="server" Width="150" ID="txtEmail" />
                </div>
                <div class="newUserLabel">
                    <asp:Button runat="server" ID="btnUserOK" Text="OK" OnClick="btnUserOK_Click" ValidationGroup="valgrpNewUser" />
                    <asp:Button runat="server" ID="btnUserCancel" Text="Cancel" OnClick="btnUserCancel_Click" CausesValidation="false" />
                </div>
                <div class="clear"></div>
            </div>
            <asp:Label runat="server" ID="lblError" Text="" />
        </div>
    </div>
    <div class="clear"/>
</asp:Content>
