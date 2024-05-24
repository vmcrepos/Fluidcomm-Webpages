<%@ Page Title="" Language="C#" MasterPageFile="~/Greenco.Master" AutoEventWireup="true" CodeBehind="Configure.aspx.cs" Inherits="GreenCo.Configure" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="./Styles/admin.css" rel="stylesheet" type="text/css" />
    <link href="./Styles/forms.css" rel="Stylesheet" type="text/css" />
    <script language="javascript" type="text/jscript">
        function fileSelected(hasfile) {
            var button = document.getElementById('<%= btnUpload.ClientID %>');
            button.disabled = hasfile;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="scriptman" EnablePartialRendering="true" />
    <div class="templateWrapper">
        <div class="row">
            <asp:Panel runat="server" ClientIDMode="Static" ID="manageReadingTemplates" GroupingText="Reading Template" CssClass="pnlGroup">
                <asp:Panel runat="server" ID="pnlButtons" CssClass="divCenter">
                    <telerik:RadButton runat="server" ID="btnDefaultTemplateDownload" Width="200" CssClass="btnStandard" OnCommand="btnDefaultTemplateDownload_Click" CommandArgument="ReadingReport" Text="Download Default Template" ToolTip="Download the default Greenco template" />
                    <telerik:RadButton runat="server" ID="btnCurrentTemplateDownload" Width="200" CssClass="btnStandard" OnCommand="btnCurrentTemplateDownload_Click" CommandArgument="ReadingReport" Text="Download Current Template" />
                </asp:Panel>
                <asp:Panel runat="server" CssClass="divCenter">
                    <telerik:RadAsyncUpload runat="server" ID="uplTemplate" AllowedFileExtensions=".xls,.xlsx" MultipleFileSelection="Disabled" HideFileInput="true" InputSize="0" CssClass="uplStandard">
                        <Localization Select="Select Excel Template For Upload" />
                    </telerik:RadAsyncUpload>

                    <telerik:RadButton runat="server" ID="btnUpload" OnCommand="btnUpload_Click" CommandArgument="ReadingReport" Text="Execute Upload" />
                    <asp:Label runat="server" ID="lblResult" Text="warning" CssClass="ValidationWarning" />
                    <br />
                    <br />
                </asp:Panel>
            </asp:Panel>
            <asp:Panel runat="server" GroupingText="Reading Summary Template" CssClass="pnlGroup">
                <asp:Panel runat="server" ID="pnlButtons4" CssClass="divCenter">
                    <telerik:RadButton runat="server" ID="btnDefaultSummaryTemplateDownload" Width="200" CssClass="btnStandard" OnCommand="btnDefaultTemplateDownload_Click" CommandArgument="SummaryReport" Text="Download Default Template" ToolTip="Download the default Greenco template" />
                    <telerik:RadButton runat="server" ID="btnCurrentSummaryTemplateDownload" Width="200" CssClass="btnStandard" OnCommand="btnCurrentTemplateDownload_Click" CommandArgument="SummaryReport" Text="Download Current Template" />
                </asp:Panel>
                <asp:Panel runat="server" CssClass="divCenter">
                    <telerik:RadAsyncUpload runat="server" ID="uplSummaryReportTemplate" AllowedFileExtensions=".xls,.xlsx" MultipleFileSelection="Disabled" HideFileInput="true" InputSize="0" CssClass="uplStandard">
                        <Localization Select="Select Excel Template For Upload" />
                    </telerik:RadAsyncUpload>

                    <telerik:RadButton runat="server" ID="btnSummaryReportUpload" OnCommand="btnUpload_Click" CommandArgument="SummaryReport" Text="Execute Upload" />
                    <asp:Label runat="server" ID="lblSummaryUploadResult" Text="" CssClass="ValidationWarning" />
                    <br />
                    <br />
                </asp:Panel>
            </asp:Panel>
        </div>
        <div class="row">
            <asp:Panel runat="server" GroupingText="AutoReport Template" CssClass="pnlGroup">
                <asp:Panel runat="server" ID="pnlButtons2" CssClass="divCenter">
                    <telerik:RadButton runat="server" ID="btnDefaultAutoReportTemplateDownload" Width="200" CssClass="btnStandard" OnCommand="btnDefaultAutoReportTemplateDownload_Click" CommandArgument="AutoReport" Text="Download Default Template" />
                    <telerik:RadButton runat="server" ID="btnCurrentAutoReportTemplateDownload" Width="200" CssClass="btnStandard" OnCommand="btnCurrentAutoReportTemplateDownload_Click" CommandArgument="AutoReport" Text="Download Current Template" ToolTip="Download the current auto reports template for fixed units" />
                </asp:Panel>
                <asp:Panel runat="server" CssClass="divCenter">
                    <telerik:RadAsyncUpload runat="server" ID="uplAutoReportTemplate" AllowedFileExtensions=".xls,.xlsx" MultipleFileSelection="Disabled" HideFileInput="true" InputSize="0" CssClass="uplStandard">
                        <Localization Select="Select Excel Template For Upload" />
                    </telerik:RadAsyncUpload>
                    <telerik:RadButton runat="server" ID="btnAutoReportUpload" OnCommand="btnAutoReportUpload_Click" CommandArgument="AutoReport" Text="Execute Upload" />
                    <asp:Label runat="server" ID="lblAutoReportResult" Text="" CssClass="ValidationWarning" />
                    <br />
                    <br />
                </asp:Panel>
            </asp:Panel>
            <asp:Panel runat="server" GroupingText="AutoReport Touch Screen Template" CssClass="pnlGroup">
                <asp:Panel runat="server" ID="pnlButtons3" CssClass="divCenter">
                    <telerik:RadButton runat="server" ID="btnTouchScreenDefaultDownload" Width="200" CssClass="btnStandard" OnCommand="btnDefaultAutoReportTemplateDownload_Click" CommandArgument="TouchScreen" Text="Download Default Template" />
                    <telerik:RadButton runat="server" ID="btnTouchScreenCurrentDownload" Width="200" CssClass="btnStandard" OnCommand="btnCurrentAutoReportTemplateDownload_Click" CommandArgument="TouchScreen" Text="Download Current Template" ToolTip="Download the current auto reports template for fixed units" />
                </asp:Panel>
                <asp:Panel runat="server" CssClass="divCenter">
                    <telerik:RadAsyncUpload runat="server" ID="uplTouchScreenTemplate" AllowedFileExtensions=".xls,.xlsx" MultipleFileSelection="Disabled" HideFileInput="true" InputSize="0" CssClass="uplStandard">
                        <Localization Select="Select Excel Template For Upload" />
                    </telerik:RadAsyncUpload>
                    <telerik:RadButton runat="server" ID="btnTouchScreenUpload" OnCommand="btnAutoReportUpload_Click" CommandArgument="TouchScreen" Text="Execute Upload" />
                    <asp:Label runat="server" ID="lblTouchScreenResult" Text="" CssClass="ValidationWarning" />
                    <br />
                    <br />
                </asp:Panel>
            </asp:Panel>
        </div>
    </div>
    <div class="clear2"></div>
    <% if (litError != null && !String.IsNullOrEmpty(litError.Text))
        { %>
    <div class="postResults">
        <asp:Literal runat="server" ID="litError" />
    </div>
    <% } %>
    <asp:GridView runat="server" ID="gridUnit"
        OnRowDataBound="gridUnit_RowDataBound"
        CssClass="tableUsers"
        RowStyle-CssClass="tableUsers-row"
        DataKeyNames="Sensors, UnitID, IsMobile"
        AutoGenerateColumns="false"
        AlternatingRowStyle-CssClass="tableUsers-altrow">
        <Columns>
            <asp:BoundField DataField="CompanyName" HeaderText="Customer" />
            <asp:TemplateField HeaderText="Asset Classes" />
            <asp:BoundField DataField="UnitCategory" HeaderText="UnitCategory" />
            <asp:TemplateField HeaderText="UnitType" />
            <asp:BoundField DataField="UnitID" HeaderText="Unit ID" />
            <asp:BoundField DataField="Name" HeaderText="Unit Name" />
            <asp:BoundField DataField="SerialNumber" HeaderText="Serial" />
            <asp:BoundField DataField="VLinkName" HeaderText="V-Link Name" />
            <asp:TemplateField HeaderText="Active">
                <ItemTemplate>
                    <asp:UpdatePanel ID="ActiveUnitUpdatePanel"
                        UpdateMode="Conditional"
                        runat="server">
                        <ContentTemplate>
                            <asp:CheckBox runat="server" ID="chkActive" OnCheckedChanged="chkActive_CheckedChanged" AutoPostBack="true" Checked='<%# Eval("Active") %>' />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="chkActive" EventName="CheckedChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sensors">
                <ItemTemplate></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:ImageButton runat="server" ID="btnConfigure" OnClick="btnConfigure_Click" ImageUrl="../Images/pencil.png" CommandArgument='<%# Eval("UnitID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
