<%@ Page Title="" Language="C#" MasterPageFile="~/Demo.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="GreenCo.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="./Styles/Gauges.css" rel="stylesheet" type="text/css" />
    <link href="./Styles/Boxes.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="scriptman">
    </asp:ScriptManager>
    <asp:Panel runat="server" ID="pnlLogo" CssClass="pnlLogoContainer" ><asp:Image runat="server" ID="imgLogo" ImageUrl="./Images/epg_logosmall.jpg" /></asp:Panel>
    <asp:Panel runat="server" ID="pnlCustomerTitle" CssClass="pnlCustomerTitle" >
        <asp:Label runat="server" ID="lblCustomerTitle" Text="Redmond Site" />
        <asp:Panel runat="server" ID="pnlOptionContainer" CssClass="pnlOptionContainer" ><asp:DropDownList runat="server" ID="cmbFacility" ><asp:ListItem Text="Choose Facility" /></asp:DropDownList></asp:Panel>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlDemo" CssClass="pnlGaugeGroupWrapper" >
        <asp:Panel runat="server" ID="pnlDemoInside" CssClass="pnlGaugeGroup" >
            <asp:Panel runat="server" ID="pnlDemoTitle" CssClass="pnlGaugeGroupTitle" ><asp:Label runat="server" ID="lblDemoTitle" CssClass="lblGaugeGroupTitle" Text="Pump Status" /></asp:Panel>
            <asp:Panel runat="server" ID="pnlDemo1" CssClass="pnlGaugeWrapper">
                <asp:Panel runat="server" ID="pnlDemo1T" CssClass="pnlGaugeTitle"><asp:Label runat="server" ID="lblDemo1" CssClass="lblGaugeTitle" Text="Cell 1" /></asp:Panel>
                <asp:Panel runat="server" ID="pnlDemo1F" CssClass="pnlGaugeNone">
                    <asp:Panel runat="server" ID="pnlDemo1I" CssClass="pnlGaugePanel" >
                        <asp:Image runat="server" ID="imgDemo1" ImageUrl="../Images/bigoffbutton.png" />
                    </asp:Panel>
                </asp:Panel>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlDemo2" CssClass="pnlGaugeWrapper">
                <asp:Panel runat="server" ID="pnlDemo2T" CssClass="pnlGaugeTitle"><asp:Label runat="server" ID="lblDemo2" CssClass="lblGaugeTitle" Text="Cell 2"/></asp:Panel>
                <asp:Panel runat="server" ID="pnlDemo2F" CssClass="pnlGaugeNone">
                    <asp:Panel runat="server" ID="pnlDemo2I" CssClass="pnlGaugePanel" >
                        <asp:Image runat="server" ID="imgDemo2" ImageUrl="../Images/bigoffbutton.png" />
                    </asp:Panel>
                </asp:Panel>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlDemo3" CssClass="pnlGaugeWrapper">
                <asp:Panel runat="server" ID="pnlDemo3T" CssClass="pnlGaugeTitle"><asp:Label runat="server" ID="lblDemo3" CssClass="lblGaugeTitle" Text="Cell 3"/></asp:Panel>
                <asp:Panel runat="server" ID="pnlDemo3F" CssClass="pnlGaugeNone">
                    <asp:Panel runat="server" ID="pnlDemo3I" CssClass="pnlGaugePanel" >
                        <asp:Image runat="server" ID="imgDemo3" ImageUrl="../Images/bigonbutton.png" />
                    </asp:Panel>
                </asp:Panel>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlDemoDate" CssClass="pnlDate" ><asp:Label runat="server" ID="lblDate" Text="" /></asp:Panel>
        </asp:Panel> 
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlTest" CssClass="pnlGaugeGroupWrapper" />
    <asp:Panel runat="server" ID="pnlTest2" CssClass="pnlGaugeGroupWrapperMini" >
        <asp:Panel runat="server" ID="pnlTest2A" CssClass="pnlGaugeGroupFloaterMini" />
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlTest3" CssClass="pnlBoxGroupWrapper" />
</asp:Content>
