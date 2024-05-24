<%@ Page Title="" Language="C#" MasterPageFile="~/Greenco.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="GreenCo.Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="./Styles/Readings.css" rel="Stylesheet" type="text/css" />
    <link href="./Styles/Layouts.css" rel="Stylesheet" type="text/css" />
    <link href="./Styles/Table.css" rel="Stylesheet" type="text/css" />
    <link href="./Styles/Master.css" rel="Stylesheet" type="text/css" />
   <%-- <link href="../Styles/jquery-ui-1.10.3.custom.min.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src='<%=ResolveClientUrl("~/scripts/jquery-1.9.1.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveClientUrl("~/scripts/jquery-ui-1.10.3.custom.min.js") %>'></script>--%>
    <script language="javascript" type="text/javascript">
        function initDatePickers() {
            $(function () { $('#<%= txtDateStart.ClientID %>').datepicker({ dateFormat: "yy-mm-dd" }); });
            $(function () { $('#<%= txtDateEnd.ClientID %>').datepicker({ dateFormat: "yy-mm-dd" }); });
        }
        initDatePickers();
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="scriptMan">
    </asp:ScriptManager>
    <asp:UpdateProgress
        ID="ajaxUpdateProgress"
        runat="server">
        <ProgressTemplate>
            <div class="ajaxLoader">
                <img src="Images/Fluidcomm_loader_Radio.gif" />
                <div>Loading</div>
            </div>
            
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel
        ID="PrimaryFiltersPanel"
        UpdateMode="Conditional"
        runat="server"
        ChildrenAsTriggers="true">
        <ContentTemplate>
            <asp:Panel ID="pnlLeft" CssClass="pnlLeft" runat="server">
                <asp:Panel ID="pnlLeftTitle" CssClass="pnlLeftTitle" runat="server">
                    <asp:Label ID="lblSideTitle" runat="server" Text="Side Title"></asp:Label>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlAdminCheckBox" CssClass="pnlLeftSelect">
                    <asp:Panel runat="server" ID="Panel2" CssClass="pnlHalfCenter">
                    </asp:Panel>
                    <asp:Panel runat="server" ID="ChkAllUnitsWrapperPanel" CssClass="pnlHalfCenter">
                        <asp:CheckBox runat="server" ID="chkAllUnits" Text="Show All Units" AutoPostBack="true" OnCheckedChanged="chkAllUnits_CheckedChanged" Checked="false" TextAlign="Left" />
                    </asp:Panel>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlLeftSelect1" CssClass="pnlLeftSelect">
                    <asp:Panel runat="server" ID="pnlLeftSelect1A" CssClass="pnlHalfCenter">
                        <asp:Label runat="server" ID="lblCompany" CssClass="lblCaption" Text="Company" />
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlLeftSelect1B" CssClass="pnlHalfCenter">
                        <telerik:RadDropDownList runat="server" ID="cmbCompany" CssClass="cmbStandard" Width="150" OnSelectedIndexChanged="cmbCompany_SelectedIndexChanged" AutoPostBack="true" />
                    </asp:Panel>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlLeftSelect2" CssClass="pnlLeftSelect">
                    <asp:Panel runat="server" ID="pnlLeftSelect2A" CssClass="pnlHalfCenter">
                        <asp:Label runat="server" ID="lblLocation" CssClass="lblCaption" Text="Location" />
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlLeftSelect2B" CssClass="pnlHalfCenter">
                        <telerik:RadDropDownList runat="server" ID="cmbLocation" CssClass="cmbStandard" Width="150" OnSelectedIndexChanged="cmbLocation_SelectedIndexChanged" AutoPostBack="true" />
                    </asp:Panel>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlLeftSelect3" CssClass="pnlLeftSelect">
                    <asp:Panel runat="server" ID="pnlLeftSelect3A" CssClass="pnlHalfCenter">
                        <asp:Label runat="server" ID="lblAsset" CssClass="lblCaption" Text="Assets" />
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlLeftSelect3B" CssClass="pnlHalfCenter">
                        <telerik:RadDropDownList runat="server" ID="cmbAsset" CssClass="cmbStandard" Width="150" OnSelectedIndexChanged="cmbAsset_SelectedIndexChanged" AutoPostBack="true" />
                    </asp:Panel>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlLeftSelect4" CssClass="pnlLeftSelect">
                    <asp:Panel runat="server" ID="pnlLeftSelect4A" CssClass="pnlHalfCenter">
                        <asp:Label runat="server" ID="lblFluidType" CssClass="lblCaption" Text="Compartment/Fluid" />
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlLeftSelect4B" CssClass="pnlHalfCenter">
                        <telerik:RadDropDownList runat="server" ID="cmbFluidType" CssClass="cmbStandard" Width="150" OnSelectedIndexChanged="cmbFluidType_SelectedIndexChanged" AutoPostBack="true" />
                    </asp:Panel>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlLeftSelect5" CssClass="pnlLeftSelect">
                    <asp:Panel runat="server" ID="pnlLeftSelect5A" CssClass="pnlHalfCenter">
                        <asp:Label runat="server" ID="lblSensor" CssClass="lblCaption" Text="Sensors" />
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlLeftSelect5B" CssClass="pnlHalfCenter">
                        <telerik:RadDropDownList runat="server" ID="cmbSensor" CssClass="cmbStandard" Width="150" DataTextField="Name" DataValueField="SensorID" OnSelectedIndexChanged="cmbSensor_SelectedIndexChanged" AutoPostBack="true" />
                    </asp:Panel>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlLeftSelect6" CssClass="pnlLeftSelect">
                    <asp:Panel runat="server" ID="pnlLeftSelect6A" CssClass="pnlHalfCenter">
                        <asp:Label runat="server" ID="lblDataType" CssClass="lblCaption" Text="Reading Type" />
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlLeftSelect6B" CssClass="pnlHalfCenter">
                        <telerik:RadDropDownList runat="server" ID="cmbDataType" CssClass="cmbStandard" Width="150" DataTextField="Name" DataValueField="SensorID" OnSelectedIndexChanged="cmbDataType_SelectedIndexChanged" AutoPostBack="true" />
                    </asp:Panel>
                </asp:Panel>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel
        ID="MobilePanel"
        runat="server"
        UpdateMode="Always"
        OnLoad="MobilePanel_Load">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlRight" CssClass="pnlRight">
                <asp:Panel runat="server" ID="pnlRightTitle" CssClass="pnlRightTitle">
                    <asp:Label runat="server" ID="lblMainTitle" Text="Main Title" />
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlRightFilters">
                    <asp:Panel runat="server" ID="dateFilters" CssClass="dateFiltersWrapper">
                        <div style="clear: both;">
                            <div class="divHalfLineLeft">Start Date:</div>
                            <div class="divHalfLineRight">
                                <asp:TextBox runat="server" ID="txtDateStart" AutoPostBack="true" OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                            </div>
                        </div>
                        <div style="clear: both;">
                            <div class="divHalfLineLeft">End Date:</div>
                            <div class="divHalfLineRight">
                                <asp:TextBox runat="server" ID="txtDateEnd" AutoPostBack="true" OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                            </div>
                        </div>
                        <div>
                            <div class="divHalfLineLeft"></div>
                            <div class="divHalfLineRight" style="text-align: left;">
                                <asp:CheckBox runat="server" ID="chkZeroes" AutoPostBack="true" OnCheckedChanged="chkZeroes_CheckedChanged" Text="Show Zeroes" Checked="false" />
                            </div>
                        </div>

                    </asp:Panel>
                    <asp:Panel runat="server" ID="mobileFilters" CssClass="mobileFiltersWrapper hide">
                        <asp:Panel runat="server" ID="mobileFilters1" CssClass="pnlLeftSelect">
                            <asp:Panel runat="server" ID="pnlmMobileFilters1" CssClass="pnlHalfCenter">
                                <asp:Label runat="server" ID="lblWorkOrder" CssClass="lblCaption" Text="Work Order #" />
                            </asp:Panel>
                            <asp:Panel runat="server" ID="Panel1" CssClass="pnlHalfCenter">
                                <telerik:RadDropDownList runat="server" ID="ddlWorkOrder" CssClass="cmbStandard" Width="150" OnSelectedIndexChanged="ddlWorkOrder_SelectedIndexChanged" AutoPostBack="true" />
                            </asp:Panel>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="mobileFilters2" CssClass="pnlLeftSelect">
                            <asp:Panel runat="server" ID="pnlMobileFiltes2" CssClass="pnlHalfCenter">
                                <asp:Label runat="server" ID="lblTechnicianId" CssClass="lblCaption" Text="Technician ID" />
                            </asp:Panel>
                            <asp:Panel runat="server" ID="Panel6" CssClass="pnlHalfCenter">
                                <telerik:RadDropDownList runat="server" ID="ddlTechnicianId" CssClass="cmbStandard" Width="150" OnSelectedIndexChanged="ddlTechnicianId_SelectedIndexChanged" AutoPostBack="true" />
                            </asp:Panel>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="mobileFilters3" CssClass="pnlLeftSelect">
                            <asp:Panel runat="server" ID="Panel5" CssClass="pnlHalfCenter">
                                <asp:Label runat="server" ID="lblEquipmentNumber" CssClass="lblCaption" Text="Equipment #" />
                            </asp:Panel>
                            <asp:Panel runat="server" ID="Panel7" CssClass="pnlHalfCenter">
                                <telerik:RadDropDownList runat="server" ID="ddlEquipmentNumber" CssClass="cmbStandard" Width="150" OnSelectedIndexChanged="ddlEquipmentNumber_SelectedIndexChanged" AutoPostBack="true" />
                            </asp:Panel>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="mobileFilters4" CssClass="pnlLeftSelect">
                            <asp:Panel runat="server" ID="Panel8" CssClass="pnlHalfCenter">
                                <asp:Label runat="server" ID="lblFluideTypeMobile" CssClass="lblCaption" Text="Compartment/Fluid Type" />
                            </asp:Panel>
                            <asp:Panel runat="server" ID="Panel9" CssClass="pnlHalfCenter">
                                <telerik:RadDropDownList runat="server" ID="ddlFluidTypeMobile" CssClass="cmbStandard" Width="150" OnSelectedIndexChanged="ddlFluidTypeMobile_SelectedIndexChanged" AutoPostBack="true" />
                            </asp:Panel>
                        </asp:Panel>
                    </asp:Panel>
                    <br style="clear: both;" />
                </asp:Panel>
                <asp:Panel runat="server" ID="reportWrapper">
                    <asp:Panel runat="server" ID="reportResult" CssClass="queryResults">
                        <asp:Literal runat="server" ID="litQueryResult" />
                    </asp:Panel>
                    <div style="clear: both;">
                        <div class="divHalfLineRight">
                            <telerik:RadButton runat="server" ID="btnDownloadExcel" Text="Download Excel File" CssClass="downloadExcel" OnClick="btnDownloadExcel_Click" />
                        </div>

                    </div>
                </asp:Panel>
            </asp:Panel>
        </ContentTemplate>

    </asp:UpdatePanel>

</asp:Content>
