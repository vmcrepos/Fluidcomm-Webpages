<%@ Page Title="" Language="C#" MasterPageFile="~/Greenco.Master" AutoEventWireup="true" CodeBehind="Readings.aspx.cs" Inherits="GreenCo.Readings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="./Styles/Gauges.css" rel="stylesheet" type="text/css" />
    <link href="./Styles/Boxes.css" rel="stylesheet" type="text/css" />
    <link href="./Styles/Readings.css" rel="Stylesheet" type="text/css" />
    <link href="./Styles/Table.css" rel="Stylesheet" type="text/css" />
    <link href="./Styles/Master.css" rel="Stylesheet" type="text/css" />
<%--    <link href="../Styles/jquery-ui-1.10.3.custom.min.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src='<%=ResolveClientUrl("~/scripts/jquery-1.9.1.js") %>' ></script>
    <script type="text/javascript" src='<%=ResolveClientUrl("~/scripts/jquery-ui-1.10.3.custom.min.js") %>' ></script>--%>
    <script language="javascript" type="text/jscript">
        function pnlVisibility(sender) {
            var pnl0 = document.getElementById("<%= pnlContent0.ClientID %>");
            var pnl1 = document.getElementById("<%= pnlContent1.ClientID %>");
            var pnl2 = document.getElementById("<%= pnlContent2.ClientID %>");
            var pnl3 = document.getElementById("<%= pnlContent3.ClientID %>");
            var pnl4 = document.getElementById("<%= pnlContent4.ClientID %>");

            var tab0 = document.getElementById("<%= pnlOption0.ClientID %>");
            var tab1 = document.getElementById("<%= pnlOption1.ClientID %>");
            var tab2 = document.getElementById("<%= pnlOption2.ClientID %>");
          <%--  var tab3 = document.getElementById("<%= pnlOption3.ClientID %>");
            var tab4 = document.getElementById("<%= pnlOption4.ClientID %>");--%>

            var hid = document.getElementById("<%= hidDisplayIndex.ClientID %>");

            pnl0.className = "pnlContentHidden";
            pnl1.className = "pnlContentHidden";
            pnl2.className = "pnlContentHidden";
            pnl3.className = "pnlContentHidden";
            pnl4.className = "pnlContentHidden";

            tab0.className = "pnlMenuOption";
            tab1.className = "pnlMenuOption";
            tab2.className = "pnlMenuOption";
            //tab3.className = "pnlMenuOption";
            //tab4.className = "pnlMenuOption";

            if (sender == 0) { pnl0.className = "pnlContentVisible"; tab0.className = "pnlMenuOptionSelected"; }
            if (sender == 1) { pnl1.className = "pnlContentVisible"; tab1.className = "pnlMenuOptionSelected"; }
            if (sender == 2) { pnl2.className = "pnlContentVisible"; tab2.className = "pnlMenuOptionSelected"; }
            //if (sender == 3) { pnl3.className = "pnlContentVisible"; tab3.className = "pnlMenuOptionSelected"; }
            //if (sender == 4) { pnl4.className = "pnlContentVisible"; tab4.className = "pnlMenuOptionSelected"; }

            var hiddenField = document.getElementById("<%= hidDisplayIndex.ClientID %>");
            hiddenField.value = sender;
        }

        function toggletimer() {
            var btn = document.getElementById("<%= btnAutoRefresh.ClientID %>");
            var pnl = document.getElementById("<%= pnlAutoRefresh.ClientID %>");
            if (timervalid) {
                timervalid = false;
                //pnl.className = "pnlMenuOptionAltDisabled";
                btn.src = './Images/reject_document.png';
                btn.title = "Auto-refresh is disabled. Click to enable.";
            }
            else {
                timervalid = true;
                //pnl.className = "pnlMenuOptionAlt";
                btn.src = './Images/accept_document.png';
                btn.title = "Auto-refresh is enabled. Click to disable.";
            }
            return false;
        }

        function checktimer() {
            if (timervalid) __doPostBack('refresh');
        }

        $(function () { $('#<%= txtDate1A.ClientID %>').datepicker({ dateFormat: "yy-mm-dd" }); });
        $(function () { $('#<%= txtDate1B.ClientID %>').datepicker({ dateFormat: "yy-mm-dd" }); });
        $(function () { $('#<%= txtDate2A.ClientID %>').datepicker({ dateFormat: "yy-mm-dd" }); });
        $(function () { $('#<%= txtDate2B.ClientID %>').datepicker({ dateFormat: "yy-mm-dd" }); });
        $(function () { $('#<%= txtDate3A.ClientID %>').datepicker({ dateFormat: "yy-mm-dd" }); });
        $(function () { $('#<%= txtDate3B.ClientID %>').datepicker({ dateFormat: "yy-mm-dd" }); });

        function pageLoad() {
            $(function () { $('#<%= txtDate1A.ClientID %>').datepicker({ dateFormat: "yy-mm-dd" }); });
            $(function () { $('#<%= txtDate1B.ClientID %>').datepicker({ dateFormat: "yy-mm-dd" }); });
            $(function () { $('#<%= txtDate2A.ClientID %>').datepicker({ dateFormat: "yy-mm-dd" }); });
            $(function () { $('#<%= txtDate2B.ClientID %>').datepicker({ dateFormat: "yy-mm-dd" }); });
            $(function () { $('#<%= txtDate3A.ClientID %>').datepicker({ dateFormat: "yy-mm-dd" }); });
            $(function () { $('#<%= txtDate3B.ClientID %>').datepicker({ dateFormat: "yy-mm-dd" }); });
        }

        function CallServer(command) {
            __doPostBack(command, '');
        }

        function ReportVisibility(sender)
        {
            var button = null;
            var div = null;
            if (sender == 0) {
                button = document.getElementById("<%= pnlChartOption4.ClientID %>");
                div = divReportChart;
            }
            if (sender == 1) {
                button = document.getElementById("<%= pnlChartOption5.ClientID %>");
                div = divReportTable; 
            }
            if (sender == 2) {
                button = document.getElementById("<%= pnlChartOption8.ClientID %>");
                div = divReportTable2; 
            }
            if (sender == 3) {
                button = document.getElementById("<%= pnlChartOption9.ClientID %>");
                div = divReportChart2; 
            }
            
            if (button.className == "pnlChartOptionDisabled") {
                if (sender == 0 || sender == 2) button.className = "pnlChartOption4";
                else button.className = "pnlChartOption5";
                div.className = "divVisible";
            }
            else {
                button.className = "pnlChartOptionDisabled";
                div.className = "divHidden";
            }
        }

        setInterval(checktimer, 300000);
        var timervalid = true;
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="scriptMan">
    </asp:ScriptManager>
    <asp:HiddenField runat="server" ID="hidDisplayIndex" />
    <asp:Panel runat="server" ID="pnlLeft" CssClass="pnlLeft">
        <asp:Panel runat="server" ID="pnlLeftTitle" CssClass="pnlLeftTitle"><asp:Label runat="server" ID="lblSideTitle" Text="Side Title" /></asp:Panel>
        <asp:Panel runat="server" ID="pnlAdminCheckBox" CssClass="pnlLeftSelect">
            <asp:Panel runat="server" ID="Panel2" CssClass="pnlHalfCenter">
                
            </asp:Panel>
            <asp:Panel runat="server" ID="Panel3" CssClass="pnlHalfCenter">
                <asp:CheckBox runat="server" ID="chkAllUnits" Text="Show All Units" AutoPostBack="true" OnCheckedChanged="chkAllUnits_SelectedIndexChanged" Checked="false" TextAlign="Left" />
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
    <asp:Panel runat="server" ID="pnlRight" CssClass="pnlRight">
        <asp:Panel runat="server" ID="pnlRightTitle" CssClass="pnlRightTitle" ><asp:Label runat="server" ID="lblMainTitle" Text="Main Title" /></asp:Panel>
        <asp:Panel runat="server" ID="pnlReadingMenu" CssClass="pnlReadingMenu">
            <asp:Panel runat="server" ID="pnlOption0" CssClass="pnlMenuOptionSelected" OnClick="pnlVisibility(0)" >Quick View</asp:Panel>
            <asp:Panel runat="server" ID="pnlOption1" CssClass="pnlMenuOption" OnClick="pnlVisibility(1)" >Readings</asp:Panel>
            <asp:Panel runat="server" ID="pnlOption2" CssClass="pnlMenuOption" OnClick="pnlVisibility(2)" >Quick Report</asp:Panel>
            <%--<asp:Panel runat="server" ID="pnlOption3" CssClass="pnlMenuOption" OnClick="pnlVisibility(3)" >Alerts</asp:Panel>
            <asp:Panel runat="server" ID="pnlOption4" CssClass="pnlMenuOption" OnClick="pnlVisibility(4)" >Devices</asp:Panel>--%>
            <asp:Panel runat="server" ID="pnlRefresh" CssClass="pnlMenuOptionAlt" OnClick="__doPostBack('refresh')" Visible="false" >Refresh</asp:Panel>
            <asp:Panel runat="server" ID="pnlAutoRefresh" CssClass="pnlMenuOptionAlt" Visible="false" ToolTip="Enable or disable auto-refresh, which updates the readings every 5 minutes" OnClick="toggletimer()" >Auto Refresh</asp:Panel>
            <asp:ImageButton runat="server" ID="btnRefresh" CssClass="btnRefresh" ToolTip="Refresh" ImageUrl="~/Images/refresh_all.png" OnClientClick="__doPostBack('refresh')" />
            <asp:ImageButton runat="server" ID="btnAutoRefresh" CssClass="btnRefresh" ToolTip="Auto-refresh is on. Click to turn off." ImageUrl="~/Images/accept_document.png" OnClientClick="toggletimer(); return false;" />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlContent0" CssClass="pnlContentVisible">
            
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlContent1" CssClass="pnlContentHidden">
            <asp:UpdatePanel runat="server" ID="updContent1" >
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnReadingsExcel" />
                </Triggers>
                <ContentTemplate>
                <asp:Panel runat="server" ID="pnlOption1A" CssClass="pnlOptionHalf">
                    <div style="clear: both;">
                        <div class="divHalfLineLeft">Start Date:</div>
                        <div class="divHalfLineRight"><asp:TextBox runat="server" ID="txtDate1A" AutoPostBack="true" OnTextChanged="UpdateReadingDate" /></div>
                    </div>
                    <div style="clear: both;">
                        <div class="divHalfLineLeft">End Date:</div>
                        <div class="divHalfLineRight"><asp:TextBox runat="server" ID="txtDate1B" AutoPostBack="true" OnTextChanged="UpdateReadingDate" /></div>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlOption1B" CssClass="pnlOptionHalf">
                    <div style="clear: both;">
                        <div class="divHalfLineLeft"><telerik:RadButton runat="server" ID="btnReadingsExcel" Text="Download Excel File" Skin="Silk" OnClick="btnReadingsExcel_Click" /><br /></div>
                        <div class="divHalfLineRight" style="text-align: right;"><asp:CheckBox runat="server" ID="chkZeroes" AutoPostBack="true" OnCheckedChanged="UpdateReadingDate" Text="Show Blank Values" Checked="false" /></div>
                    </div>
                    <div style="clear: both;">
                        <div class="divHalfLineLeft"></div>
                        <div class="divHalfLineRight"></div>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlContentBody1" CssClass="pnlContentBodySmall">
                    <asp:GridView runat="server" ID="gridReadings"
                        AutoGenerateColumns="false"
                        AllowSorting="true" 
                        CssClass="tblReadings" 
                        RowStyle-CssClass="tblReadings-row" 
                        AlternatingRowStyle-CssClass="tblReadings-altrow"
                        HeaderStyle-CssClass="tblReadings-header"
                        DataKeyNames="id, UnitID, SensorID, UnitName, Date" 
                        ShowHeaderWhenEmpty="true"
                        OnDataBound="gridReadings_DataBound"
                        OnRowDataBound="gridReadings_RowDataBound"
                        OnSorting="gridReadings_Sorting"  >
                        <Columns>
                            <asp:BoundField HeaderText="Name" DataField="SensorName" SortExpression="SensorName" HeaderStyle-CssClass="col15Center" ItemStyle-CssClass="col15Center" />
                            <asp:TemplateField HeaderText="Date" HeaderStyle-CssClass="col15Center" ItemStyle-CssClass="col15Center" SortExpression="Date" >
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Date", "{0:dd-MMM-yyyy}") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Time" HeaderStyle-CssClass="col15Center" ItemStyle-CssClass="col15Center" SortExpression="Date" >
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Date", "{0:HH:mm:ss}") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="ISO Code" DataField="ISO4" SortExpression="ISO4" ItemStyle-CssClass="col5Center" />
                            <asp:BoundField HeaderText="ct/ml" DataField="ISO4P" SortExpression="ISO4P" ItemStyle-CssClass="col5Center" DataFormatString="{0:0}" />
                            <asp:BoundField HeaderText="ISO Code" DataField="ISO6" SortExpression="ISO6" ItemStyle-CssClass="col5Center" />
                            <asp:BoundField HeaderText="ct/ml" DataField="ISO6P" SortExpression="ISO6P" ItemStyle-CssClass="col5Center" DataFormatString="{0:0}" />
                            <asp:BoundField HeaderText="ISO Code" DataField="ISO14" SortExpression="ISO14" ItemStyle-CssClass="col5Center" />
                            <asp:BoundField HeaderText="ct/ml" DataField="ISO14P" SortExpression="ISO14P" ItemStyle-CssClass="col5Center" DataFormatString="{0:0}" />
                            <asp:BoundField HeaderText="ISO Code" DataField="ISO21" SortExpression="ISO21" ItemStyle-CssClass="col5Center" />
                            <asp:BoundField HeaderText="ct/ml" DataField="ISO21P" SortExpression="ISO21P" ItemStyle-CssClass="col5Center" DataFormatString="{0:0}" />
                            <asp:BoundField HeaderText="Flow Rate (ml/min)" DataField="Alt" SortExpression="Alt" ItemStyle-CssClass="col5Center" />
                        </Columns>
                    </asp:GridView>
                    <asp:GridView runat="server" ID="gridReadings2"
                        AutoGenerateColumns="false"
                        AllowSorting="true" 
                        CssClass="tblReadings" 
                        RowStyle-CssClass="tblReadings-row" 
                        AlternatingRowStyle-CssClass="tblReadings-altrow"
                        HeaderStyle-CssClass="tblReadings-header"
                        DataKeyNames="id, UnitID, SensorID, UnitName, Date" 
                        ShowHeaderWhenEmpty="true"
                        OnDataBound="gridReadings2_DataBound"
                        OnRowDataBound="gridReadings2_RowDataBound"
                        OnSorting="gridReadings2_Sorting"  >
                        <Columns>
                            <asp:BoundField HeaderText="Name" DataField="SensorName" SortExpression="SensorName" HeaderStyle-CssClass="col15Center" ItemStyle-CssClass="col15Center" />
                            <asp:TemplateField HeaderText="Date" HeaderStyle-CssClass="col15Center" ItemStyle-CssClass="col15Center" SortExpression="Date" >
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Date", "{0:dd-MMM-yyyy}") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Time" HeaderStyle-CssClass="col15Center" ItemStyle-CssClass="col15Center" SortExpression="Date" >
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Date", "{0:HH:mm:ss}") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Relative Humidity (%)" DataField="RH" SortExpression="RH" ItemStyle-CssClass="col15Center" />
                            <asp:BoundField HeaderText="Temperature (C°)" DataField="Temp" SortExpression="Temp" ItemStyle-CssClass="col15Center" />
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </ContentTemplate></asp:UpdatePanel>
        </asp:Panel>
        <!-- QUICK REPORT -->
        <asp:Panel runat="server" ID="pnlContent2" CssClass="pnlContentHidden">
            <asp:Panel runat="server" ID="pnlOption2A" CssClass="pnlOptionHalf">
                <div style="clear: both;">
                    <div class="divHalfLineLeft">Start Date:</div>
                    <div class="divHalfLineRight"><asp:TextBox runat="server" ID="txtDate2A" AutoPostBack="true" OnTextChanged="UpdateReportDate" /></div>
                </div>
                <div style="clear: both;" >
                    <div class="divHalfLineLeft">End Date:</div>
                    <div class="divHalfLineRight"><asp:TextBox runat="server" ID="txtDate2B" AutoPostBack="true" OnTextChanged="UpdateReportDate" /></div>
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlOption2B" CssClass="pnlOptionHalf">
                <div style="clear: both;">
                    <div class="divHalfLineLeft">Calculation:</div>
                    <div class="divHalfLineRight">
                        <telerik:RadDropDownList runat="server" ID="cmbReportMode" AutoPostBack="true" OnSelectedIndexChanged="UpdateReportDate" >
                            <Items>
                                <telerik:DropDownListItem Text="Average" Value="Average" />
                                <telerik:DropDownListItem Text="Minimum" Value="Minimum" />
                                <telerik:DropDownListItem Text="Maximum" Value="Maximum" />
                            </Items>
                        </telerik:RadDropDownList>
                    </div>
                </div>
                <div style="clear: both;">
                    <div class="divHalfLineLeft"></div>
                    <div class="divHalfLineRight"><telerik:RadButton runat="server" Text="Download Excel File" Skin="Silk" OnClick="btnSummaryExcel_Click" /></div>
                </div>
            </asp:Panel>
            <div class="clear"></div>
            <asp:Panel runat="server" ID="pnlReportContainer1" CssClass="pnlReportContainerVisible" >
                <asp:Panel runat="server" ID="pnlChartOptionsWrapper" CssClass="pnlChartOptionsWrapper">
                    <asp:Panel runat="server" ID="pnlChartOptionsFloater" CssClass="pnlChartOptionsFloater">
                        <asp:Panel runat="server" ID="pnlChartOption0" CssClass="pnlChartOption0" onclick="CallServer('ChartOption0')">ISO-4</asp:Panel>
                        <asp:Panel runat="server" ID="pnlChartOption1" CssClass="pnlChartOption1" onclick="CallServer('ChartOption1')">ISO-6</asp:Panel>
                        <asp:Panel runat="server" ID="pnlChartOption2" CssClass="pnlChartOption2" onclick="CallServer('ChartOption2')">ISO-14</asp:Panel>
                        <asp:Panel runat="server" ID="pnlChartOption3" CssClass="pnlChartOption3" onclick="CallServer('ChartOption3')">ISO-21</asp:Panel>
                        <asp:Panel runat="server" ID="pnlChartOption4" CssClass="pnlChartOption4" onclick="ReportVisibility(0)">Chart</asp:Panel>
                        <asp:Panel runat="server" ID="pnlChartOption5" CssClass="pnlChartOption5" onclick="ReportVisibility(1)">Table</asp:Panel>
                    </asp:Panel>
                </asp:Panel>
                <div class="divVisible" style="min-width: 800px" id="divReportChart">
                    <telerik:RadHtmlChart runat="server" ID="chrtReport" Height="500" />
                </div>
                <div class="divVisible" id="divReportTable" >
                    <asp:GridView runat="server" ID="gridReport"
                        AutoGenerateColumns="false"
                        AllowSorting="true" 
                        CssClass="tblReadings" 
                        RowStyle-CssClass="tblReadings-row" 
                        AlternatingRowStyle-CssClass="tblReadings-altrow"
                        HeaderStyle-CssClass="tblReadings-header"
                        DataKeyNames="Date"
                        ShowHeaderWhenEmpty="false" >
                        <Columns>
                            <asp:BoundField HeaderText="Date" DataField="Date" ItemStyle-CssClass="col5Center" DataFormatString="{0:dd-MMM-yyyy}" />
                            <asp:BoundField HeaderText="ISO-4" DataField="ISO4" ItemStyle-CssClass="col5Center" />
                            <asp:BoundField HeaderText="ISO-6" DataField="ISO6" ItemStyle-CssClass="col5Center" />
                            <asp:BoundField HeaderText="ISO-14" DataField="ISO14" ItemStyle-CssClass="col5Center" />
                            <asp:BoundField HeaderText="ISO-21" DataField="ISO21" ItemStyle-CssClass="col5Center" />
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlReportContainer2" CssClass="pnlReportContainerVisible" >
                <asp:Panel runat="server" ID="pnlChartOptionsWrapper2" CssClass="pnlChartOptionsWrapper">
                    <asp:Panel runat="server" ID="pnlChartOptionsFloater2" CssClass="pnlChartOptionsFloater">
                        <asp:Panel runat="server" ID="pnlChartOption6" CssClass="pnlChartOption0" onclick="CallServer('ChartOption4')">RH</asp:Panel>
                        <asp:Panel runat="server" ID="pnlChartOption7" CssClass="pnlChartOption1" onclick="CallServer('ChartOption5')">Temp</asp:Panel>
                        <asp:Panel runat="server" ID="pnlChartOption8" CssClass="pnlChartOption4" onclick="ReportVisibility(2)">Table</asp:Panel>
                        <asp:Panel runat="server" ID="pnlChartOption9" CssClass="pnlChartOption4" onclick="ReportVisibility(3)">Chart</asp:Panel>
                    </asp:Panel>
                </asp:Panel>
                <div class="divVisible" style="min-width: 500px" id="divReportChart2">
                    <telerik:RadHtmlChart runat="server" ID="chrtReport2" Height="500" />
                </div>
                <div class="divVisible" id="divReportTable2" >
                    <asp:GridView runat="server" ID="gridReport2"
                        AutoGenerateColumns="false"
                        AllowSorting="true" 
                        CssClass="tblReadings" 
                        RowStyle-CssClass="tblReadings-row" 
                        AlternatingRowStyle-CssClass="tblReadings-altrow"
                        HeaderStyle-CssClass="tblReadings-header"
                        ShowHeaderWhenEmpty="false" >
                        <Columns>
                            <asp:BoundField HeaderText="Date" DataField="Date" ItemStyle-CssClass="col5Center" DataFormatString="{0:dd-MMM-yyyy}" />
                            <asp:BoundField HeaderText="Relative Humidity (%)" DataField="RH" ItemStyle-CssClass="col5Center" />
                            <asp:BoundField HeaderText="Temperature (C°)" DataField="Temp" ItemStyle-CssClass="col5Center" DataFormatString="{0:n1}"/>
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlContent3" CssClass="pnlContentHidden">
            <asp:Panel runat="server" ID="Panel1" CssClass="pnlOptionHalf">
                <div style="clear: both;">
                <div class="divHalfLineLeft">Start Date:</div>
                <div class="divHalfLineRight"><asp:TextBox runat="server" ID="txtDate3A" AutoPostBack="true" OnTextChanged="UpdateAlarmDate" /></div>
                </div>
                <div style="clear: both;">
                <div class="divHalfLineLeft">End Date:</div>
                <div class="divHalfLineRight"><asp:TextBox runat="server" ID="txtDate3B" AutoPostBack="true" OnTextChanged="UpdateAlarmDate" /></div>
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="Panel4" CssClass="pnlOptionHalf">
                <div class="divHalfLineLeft"><asp:CheckBox runat="server" ID="chkCurrentAlarms" Text="New Alarms" Checked="false" /></div>
                <div style="clear: both;">
                        <div class="divHalfLineLeft"><telerik:RadButton runat="server" ID="btnSummaryExcel" Text="Download Excel File" Skin="Silk" OnClick="btnSummaryExcel_Click" /><br /></div>
                    </div>
            </asp:Panel>
            <asp:GridView runat="server" ID="gridAlarms"
                AutoGenerateColumns="false"
                AllowSorting="true" 
                CssClass="tblReadings" 
                DataKeyNames="Severity"
                RowStyle-CssClass="tblReadings-row" 
                AlternatingRowStyle-CssClass="tblReadings-altrow"
                HeaderStyle-CssClass="tblReadings-header"
                ShowHeaderWhenEmpty="false" >
                <Columns>
                    <asp:BoundField HeaderText="Sensor" DataField="SensorName" ItemStyle-CssClass="col5Center" />
                    <asp:BoundField HeaderText="Date" DataField="StartDate" ItemStyle-CssClass="col5Center" DataFormatString="{0:yyyy-MMM-dd hh:mm}" />
                    <asp:BoundField HeaderText="Alarm Text" DataField="AlarmText" ItemStyle-CssClass="col5Center" />
                </Columns>
            </asp:GridView>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlContent4" CssClass="pnlContentHidden">
            <asp:Panel runat="server" ID="pnlDevice1" >
            
            </asp:Panel>

            <asp:Panel runat="server" ID="pnlDevice2" >
            
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
</asp:Content>

