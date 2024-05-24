<%@ Page Title="" Language="C#" MasterPageFile="~/Greenco.Master" AutoEventWireup="true" CodeBehind="NewUser.aspx.cs" Inherits="GreenCo.Admin.NewUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/admin.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divFullColumn">
        <div class="NewUserCenter">
            <div class="NewUserWrapper" >
                <div class="divTitle"><asp:Label runat="server" Text="New User" ID="lblTitle" /></div>
                <div class="newUserLabel"><asp:Label runat="server" ID="lblName" Text="User Name" /></div>
                <div class="newUserControl">
                    <asp:TextBox runat="server" Width="150" ID="txtName" />
                </div>
                <asp:RequiredFieldValidator runat="server" ID="vldNameRequired" ControlToValidate="txtName" ErrorMessage="Name required" CssClass="ValidationWarning" ValidationGroup="valgrpNewUser" Display="None" />
                <asp:RegularExpressionValidator runat="server" ID="vldNameRegular" ControlToValidate="txtName" ErrorMessage="Must be at least 3 characters" ValidationExpression="^.*(?=.{3,}).*$" CssClass="ValidationWarning" ValidationGroup="valgrpNewUser" Display="None" />
                <div class="newUserLabel"><asp:Label runat="server" ID="lblEmail" Text="Email" /></div>
                <div class="newUserControl">
                    <asp:TextBox runat="server" Width="150" ID="txtEmail" />
                </div>
                <div class="newUserLabel"><asp:Label runat="server" ID="lblPassword1" Text="Password" /></div>
                <div class="newUserControl">
                    <asp:TextBox runat="server" Width="150" ID="txtPassword1" TextMode="Password" />
                </div>
                <div class="newUserLabel"><asp:Label runat="server" ID="lblPassword2" Text="Confirm Password" /></div>
                <div class="newUserControl">
                    <asp:TextBox runat="server" Width="150" ID="txtPassword2" TextMode="Password" />
                </div>
                <asp:RequiredFieldValidator runat="server" ID="vldPasswordRequired1" ControlToValidate="txtPassword1" ErrorMessage="Password required" CssClass="ValidationWarning" ValidationGroup="valgrpNewUser" Display="None" />
                <asp:RequiredFieldValidator runat="server" ID="vldPasswordRequired2" ControlToValidate="txtPassword2" ErrorMessage="Password Confirmation required" CssClass="ValidationWarning" ValidationGroup="valgrpNewUser" Display="None" />
                <asp:CompareValidator runat="server" ID="vldPasswordCompare" Type="String" ControlToValidate="txtPassword1" ControlToCompare="txtPassword2" ErrorMessage="Password Mismatch" CssClass="ValidationWarning" ValidationGroup="valgrpNewUser" Display="None" />
                <div class="newUserLabel"><asp:Label runat="server" ID="lblCompany" Text="Company" /></div>
                <div class="newUserControl"><asp:DropDownList runat="server" ID="cmbCompany" AutoPostBack="false" /></div>
                <div class="newUserLabel">
                    <asp:Button runat="server" ID="btnUserOK" Text="OK" OnClick="btnUserOK_Click" ValidationGroup="valgrpNewUser" />
                    <asp:Button runat="server" ID="btnUserCancel" Text="Cancel" OnClick="btnUserCancel_Click" CausesValidation="false" />
                </div>
                <div class="clear"></div>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList" CssClass="ValidationWarning" ValidationGroup="valgrpNewUser" ShowSummary="true" />
            </div>
            <asp:Label runat="server" ID="lblError" Text="" />
        </div>
    </div>
    <div class="clear"/>
    
</asp:Content>
