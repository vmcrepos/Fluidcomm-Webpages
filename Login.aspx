<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="GreenCo.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="~/Styles/Master.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/Login.css" rel="stylesheet" type="text/css" />
    <script lang="javascript" type="text/javascript">
        function setOffset() {
            var d = new Date();
            var offset = d.getTimezoneOffset();
            document.getElementById('<%= hidDateOffset.ClientID %>').value = offset;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="scriptMan"></asp:ScriptManager>
    <asp:HiddenField runat="server" ID="hidDateOffset" Value=""/>
    <script lang="javascript" type="text/javascript">
        var d = new Date();
        var offset = d.getTimezoneOffset();
        document.getElementById('<%= hidDateOffset.ClientID %>').value = offset;
        </script>
    <div class="main">
        
        <asp:Panel runat="server" ID="pnlHeader" class="header"  >
            <asp:Panel runat="server" ID="pnlHeaderSub" class="headerSub" onclick='top.location="/default.aspx"' style="border-bottom: 3px double #c0c0c0;">
                <div class="headerdivleft"></div>
                <div class="headerdivright"></div>
            </asp:Panel>
        </asp:Panel>
        <div id="wrapper">
            <div id="container">
                <div class="loginLogoCenter">
                    <asp:Image runat="server" ID="imgLogo" ImageUrl="~/Images/fluidcommlogo.png" />
                </div>
                <asp:Login runat="server" ID="login" CssClass="login" DisplayRememberMe="false" LoginButtonText="" LoginButtonStyle-CssClass="loginButton" TextBoxStyle-CssClass="loginText" ValidatorTextStyle-CssClass="loginValidText" OnLoggedIn="login_OnLoggedIn" />
                <asp:Label runat="server" ID="lblLoginError" Text="" CssClass="loginError" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
