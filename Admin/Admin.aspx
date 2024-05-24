<%@ Page Title="" Language="C#" MasterPageFile="~/Greenco.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="GreenCo.Admin.Admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/admin.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="scriptman"></asp:ScriptManager>
    <div class="divFullColumn">
        <div class="toolbar">
            <asp:ImageButton runat="server" ID="toolbarNew" ImageUrl="~/images/user_add.png" ToolTip="New User" CssClass="toolbarbuttonleft" OnClick="toolbarNew_Click" />
            <asp:ImageButton runat="server" ID="toolbarProcess" ImageUrl="~/images/database_cleanup.png" ToolTip="Process Data" CssClass="toolbarbuttonleft" OnClick="toolbarProcess_Click" />
        </div>
        <div class="divTableUsers">
            <asp:UpdatePanel runat="server" ID="updAdmin" UpdateMode="Always" >
            <ContentTemplate>
            <asp:GridView runat="server"
                id="gridUsers"
                CssClass="tableUsers"
                RowStyle-CssClass="tableUsers-row"
                OnRowDataBound="gridUsers_RowDataBound" 
                DataKeyNames="PasswordQuestion, IsApproved, IsLockedOut, LastActivityDate, IsOnline, ProviderName" 
                AutoGenerateColumns="false"
                AlternatingRowStyle-CssClass="tableUsers-altrow">
                <Columns>
                    <asp:BoundField HeaderText="User Name" DataField="UserName" ItemStyle-CssClass="colName" />
                    <asp:BoundField HeaderText="E-mail Address" DataField="Email" ItemStyle-CssClass="colEmail" />
                    <asp:TemplateField HeaderText="Account Created" ItemStyle-CssClass="colName"><ItemTemplate><asp:Label ID="Label1" runat="server" Text='<%# Convert.ToDateTime(Eval("CreationDate")).ToString("yyyy-MM-dd") %>' /></ItemTemplate></asp:TemplateField>
                    <asp:TemplateField HeaderText="Last Login" ItemStyle-CssClass="colName"><ItemTemplate><asp:Label ID="Label2" runat="server" Text='<%# Convert.ToDateTime(Eval("LastLoginDate")).ToString("yyyy-MM-dd") %>' /></ItemTemplate></asp:TemplateField>
                    <asp:TemplateField HeaderText="Last Password Change" ItemStyle-CssClass="colName"><ItemTemplate><asp:Label ID="Label3" runat="server" Text='<%# Convert.ToDateTime(Eval("LastPasswordChangedDate")).ToString("yyyy-MM-dd") %>' /></ItemTemplate></asp:TemplateField>
                    <asp:TemplateField HeaderText="Settings" ItemStyle-CssClass="colSettings" >
                        <ItemTemplate>
                            <asp:ImageButton ID="imgLock" runat="server" CssClass="icon" ImageUrl="~/Images/lock.png" OnClick="btnLock_Click" />
                            <asp:ImageButton ID="imgApprove" runat="server" CssClass="icon" ImageUrl="~/Images/accept.png" OnClick="btnApprove_Click" />
                            <asp:ImageButton ID="imgDelete" runat="server" CssClass="icon" ImageUrl="~/Images/user_delete.png" OnClick="btnRemove_Click"  />
                            <asp:ImageButton ID="imgPassword" runat="server" CssClass="icon" ImageUrl="~/Images/question.png" OnClick="btnResetPassword_Click" />
                            <asp:ImageButton ID="imgTest" runat="server" CssClass="icon" ImageUrl="~/Images/email_go.png" OnClick="btnTest_Click" ToolTip="Send test email" />
                            <asp:ImageButton ID="imgEmail" runat="server" CssClass="icon" ImageUrl="~/Images/email_edit.png" OnClick="btnEmail_Click" ToolTip="Change email" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Company" ItemStyle-CssClass="colAccess" >
                        <ItemTemplate>
                            <asp:DropDownList runat="server" ID="cmbCompany" AutoPostBack="true" DataTextField="value" DataValueField="key" OnSelectedIndexChanged="cmbCompany_SelectedIndexChanged" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Customer Admin" ItemStyle-CssClass="colSettings" >
                        <ItemTemplate><asp:CheckBox runat="server" ID="chkCustomerAdmin" AutoPostBack="true" OnCheckedChanged="chkCustomerAdmin_CheckedChanged" /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FluidTrax Admin" ItemStyle-CssClass="colSettings" >
                        <ItemTemplate><asp:CheckBox runat="server" ID="chkAdmin" AutoPostBack="true" OnCheckedChanged="chkAdmin_CheckedChanged" /></ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <asp:Label runat="server" ID="lblResult" />
            </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
    </div>
    <div class="clear" />
    <asp:Button runat="server" ID="btnTest" OnClick="btnTest_Click" Text="Test Email Server" />
</asp:Content>
