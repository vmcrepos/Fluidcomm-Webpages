<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Greenco.Master" CodeBehind="MyAccount.aspx.cs" Inherits="GreenCo.MyAccount" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="./Styles/Forms.css" rel="stylesheet" type="text/css" />
    <link href="./Styles/admin.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="scriptMan">
    </asp:ScriptManager>

    <div class="columns is-centered settings-wrapper">
        <div class="column is-3">
            <div class="formSectionTitle">
                <asp:Label runat="server" CssClass="" Text="Change Password" ID="lblTitle" />
            </div>
            <div class="field ">
                <div class="label is-normal">
                    <asp:Label runat="server" ID="lblPassword1" Text="Current Password" CssClass="label" />
                </div>
                <div class="field-body">
                    <div class="field">
                        <div class="control">
                            <asp:TextBox runat="server"  ID="txtPassword1" TextMode="Password" CssClass="input is-small" />
                        </div>
                        <asp:RequiredFieldValidator runat="server" ID="vldPasswordRequired1" ControlToValidate="txtPassword1" ErrorMessage="Current Password required" CssClass="ValidationWarning" ValidationGroup="valgrpNewUser" Display="None" />
                    </div>
                </div>
            </div>
            <div class="field">
                <div class="label is-normal">
                    <asp:Label runat="server" ID="lblPassword2" Text="New Password" CssClass="label" />

                </div>
                <div class="field-body">
                    <div class="field">
                        <div class="control">
                            <asp:TextBox runat="server"  ID="txtPassword2" TextMode="Password" CssClass="input is-small" />
                        </div>
                        <asp:RequiredFieldValidator runat="server" ID="vldPasswordRequired2" ControlToValidate="txtPassword2" ErrorMessage="New Password required" CssClass="ValidationWarning" ValidationGroup="valgrpNewUser" Display="None" />
                    </div>
                </div>
            </div>
            <div class="field">
                <div class="label is-normal">
                    <asp:Label runat="server" ID="lblPassword3" Text="Confirm Password" CssClass="label" />

                </div>
                <div class="field-body">
                    <div class="field">
                        <div class="control">
                            <asp:TextBox runat="server"  ID="txtConfirmPassword" TextMode="Password" CssClass="input is-small" />
                        </div>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtConfirmPassword" ErrorMessage="New Password required" CssClass="ValidationWarning" ValidationGroup="valgrpNewUser" Display="None" />
                        <asp:CompareValidator runat="server" ID="ComparePasswordValidator" ControlToValidate="txtPassword2" ControlToCompare="txtConfirmPassword" ErrorMessage="Password mismatch" CssClass="ValidationWarning" ValidationGroup="valgrpNewUser" Display="None" />
                    </div>
                </div>
            </div>
            <div class="field">
                
                <div class="field-body">
                    <div class="field">
                        <div class="control">
                            <%--<telerik:RadButton runat="server" CssClass="button is-primary"  ID="btnPasswordSave" Text="Save" OnClick="btnSavePassword_Click" ValidationGroup="valgrpNewUser" />--%>
                            <asp:Button runat="server" CssClass="button is-primary has-text-weight-bold" ID="btnSave" Text="Save Password" OnClick="btnSavePassword_Click" ValidationGroup="valgrpNewUser" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="clear"></div>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList" CssClass="ValidationWarning" ValidationGroup="valgrpNewUser" ShowSummary="true" />
            <% if (litError != null && !String.IsNullOrEmpty(litError.Text))
                { %>
            <div class="postResults">
                <asp:Literal runat="server" ID="litError" />
            </div>
            <% } %>
        </div>
    </div>
</asp:Content>
