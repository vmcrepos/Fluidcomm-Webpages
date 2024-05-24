<%@ Page Title="" Language="C#" MasterPageFile="~/Greenco.Master" AutoEventWireup="true" CodeBehind="ConfigureUnit.aspx.cs" Inherits="GreenCo.ConfigureUnit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="./Styles/Forms.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/jscript">

        //document.addEventListener("DOMContentLoaded", function (event) { alert('yup') });
        document.addEventListener("DOMContentLoaded", startup);

        function startup(event) {

        }


        function pnlLocationVisibility(sender, eventArgs) {
            var index = sender.selectedIndex;
            if (index == 0) { divLocation1.visibility = visible; }
            else { divLocation1.visibility = hidden; }
        }
        function showMessage(message) { alert(message); }
        function toggleSensor(sender, target) {

            var c = null;
            var hid = null;
            var clientId;

            var isMobile = document.getElementById("<%= chkBoxIsMobile.ClientID%>").checked;

            if (target == 1) {
                c = document.getElementById('<%= pnlSensorA.ClientID %>');
                hid = document.getElementById('<%= hidSensorA.ClientID %>');
                clientId = '<%= pnlSensorA.UniqueID%>';
            }
            if (target == 2) {
                c = document.getElementById('<%= pnlSensorB.ClientID %>');
                hid = document.getElementById('<%= hidSensorB.ClientID %>');
                clientId = '<%= pnlSensorB.UniqueID%>';
            }
            if (target == 3) {
                c = document.getElementById('<%= pnlSensorC.ClientID %>');
                hid = document.getElementById('<%= hidSensorC.ClientID %>');
                clientId = '<%= pnlSensorC.UniqueID%>';
            }
            if (target == 4) {
                c = document.getElementById('<%= pnlSensorD.ClientID %>');
                hid = document.getElementById('<%= hidSensorD.ClientID %>');
                clientId = '<%= pnlSensorD.UniqueID%>';
            }

            if (sender.className == "divToggleOn") {
                c.className = "divSensorHidden";
                hid.value = "0";
                sender.className = "divToggleOff";
                // postback to server deactivate sensor
                //__doPostBack(clientId, 0);
            }
            else {
                if (!isMobile || (isMobile && target == 1)) {
                    c.className = "divSensorVisible";
                    sender.className = "divToggleOn";
                    hid.value = "1";
                } else {
                    alert('Only sensor 1 can be activated on mobile units');
                }

                // postback to server activate sensor
                //__doPostBack(clientId, 1);
            }
        }

        function toggleMobileUnitTags(chkMobileUnit) {
            var switchConfiguration;
            if (chkMobileUnit.checked) {
                switchConfiguration = confirm("Click OK if you want to delete current unit configuration and switch to mobile unit configuration.");
            } else {
                switchConfiguration = confirm("Click OK if you want to delete current configuration and switch to fixed unit configuration.")
            }

            if (switchConfiguration) {
                __doPostBack('<%= chkBoxIsMobile.UniqueID %>', '');
            } else {
                if (chkMobileUnit.checked)
                    chkMobileUnit.checked = false;
                else
                    chkMobileUnit.checked = true;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="scriptMan">
    </asp:ScriptManager>
    <asp:HiddenField runat="server" ID="hidSensorA" />
    <asp:HiddenField runat="server" ID="hidSensorB" />
    <asp:HiddenField runat="server" ID="hidSensorC" />
    <asp:HiddenField runat="server" ID="hidSensorD" />
    <div >
        <div class="divFormTitle">
            <asp:Label runat="server" ID="lblTitle" CssClass="lblTitle" />
        </div>
        <div class="divCenter">
            <div class="divFormSection">
                <div class="divFormHeading100">V-Link Information</div>
                <div class="divFormRow">
                    <div class="divFormCaption20Right">Serial</div>
                    <div class="divFormItem30">
                        <telerik:RadTextBox runat="server" ID="txtVlinkSerial" ReadOnly="true" />
                    </div>
                    <div class="divFormCaption20Right">V-Link Name</div>
                    <div class="divFormItem30">
                        <telerik:RadTextBox runat="server" ID="txtVlinkName" ReadOnly="true" />
                    </div>
                </div>
                <div class="clear"></div>
                <div class="divFormSectionBorder"></div>
            </div>
            <div class="divFormSection">
                <div class="divFormRow">
                    <div class="divCenter">
                        <telerik:RadButton runat="server" ID="btnSave" CssClass="btnStandard" Skin="Silk" ToolTip="Save these settings to the database" Text="Save" OnClick="btnSave_Click" />
                        <telerik:RadButton runat="server" ID="btnReset" CssClass="btnStandard" Skin="Silk" ToolTip="Discard changes and reload settings from database" Text="Discard" OnClick="btnReset_Click" />
                        <telerik:RadButton runat="server" ID="btnBack" CssClass="btnStandard" Skin="Silk" ToolTip="Return to list of units" Text="Return" OnClick="btnReturn_Click" />
                    </div>
                </div>
                <div class="divFormRow"></div>
                <div class="divFormSectionBorder"></div>
            </div>
            <div class="divFormSection">
                <% if (litError != null && !String.IsNullOrEmpty(litError.Text))
                    { %>
                <div class="postResults">
                    <asp:Literal runat="server" ID="litError" />
                </div>
                <% } %>
                <div class="divFormHeading100">Device Information</div>
                <div class="divFormRow">
                    <div class="divFormCaption20Right">Device Name</div>
                    <div class="divFormItem30">
                        <telerik:RadTextBox runat="server" ID="txtUnitName" />
                    </div>
                    <div class="divFormCaption20Right">Company</div>
                    <div class="divFormItem30">
                        <telerik:RadDropDownList runat="server" ID="cmbCompany" DataTextField="Value" DataValueField="Key" />
                    </div>
                </div>
                <div class="divFormRow" id="divLocation">
                    <div class="divFormCaption20Right">Unit Category</div>
                    <div class="divFormItem30">
                        <telerik:RadComboBox runat="server" ID="cmbLocation1" Width="250" />
                    </div>
                </div>
                <div class="divFormRow" id="divIsMobile">
                    <asp:CheckBox ID="chkBoxIsMobile" runat="server" Text="Mobile Unit" AutoPostBack="false" onclick="toggleMobileUnitTags(this)" OnCheckedChanged="chkBoxIsMobile_CheckedChanged" />
                    <% if (chkBoxIsMobile.Checked)
                        { %>
                    <div class="mobileConfigWrapper">
                        <div class="divFormRow">
                            <div class="divFormCaption20Right">WorkOrder Process Tag:</div>
                            <div class="divFormItem30">
                                <telerik:RadTextBox ID="txtWorkOrderTag" runat="server" CssClass="txtSmall" Text="53" ReadOnly="true"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div class="divFormRow">
                            <div class="divFormCaption20Right">Technician ID Process Tag:</div>
                            <div class="divFormItem30">
                                <telerik:RadTextBox ID="txtTechnicianTag" runat="server" CssClass="txtSmall" Text="54" ReadOnly="true"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div class="divFormRow">
                            <div class="divFormCaption20Right">Serial # Process Tag:</div>
                            <div class="divFormItem30">
                                <telerik:RadTextBox ID="txtSerialNumberTag" runat="server" CssClass="txtSmall" Text="56" ReadOnly="true"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div class="divFormRow">
                            <div class="divFormCaption20Right">Compartment/Fluid Type Process Tag:</div>
                            <div class="divFormItem30">
                                <telerik:RadTextBox ID="txtFluidTypeTag" runat="server" CssClass="txtSmall" Text="55" ReadOnly="true"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div class="divFormRow">
                            <div class="divFormCaption20Right">Hours/Miles Process Tag:</div>
                            <div class="divFormItem30">
                                <telerik:RadTextBox ID="txtHoursMilesTag" runat="server" CssClass="txtSmall" Text="57" ReadOnly="true"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                    </div>
                    <% } %>
                </div>

                <div class="divFormRow">
                    <!-- This is where all checkboxes or buttons for the different data types go -->
                </div>
                <div class="divFormSectionBorder"></div>
            </div>
            <div class="divFormSection">
                <div class="divFormHeading100">
                    <asp:Panel runat="server" ID="pnlSensorAToggle" CssClass="pnlToggleOn" onclick="toggleSensor(this, 1);">Particle Count Sensor 1</asp:Panel>
                </div>
                <asp:Panel runat="server" CssClass="divSensorHidden" ID="pnlSensorA">
                    <asp:HiddenField ID="hidSensorAId" runat="server" />
                    <asp:HiddenField ID="hidSensorAIndex" runat="server" Value="0" />
                    <div class="divFormRow">
                        <div class="divFormCaption20Right">Sensor Name: </div>
                        <div class="divFormItem30">
                            <telerik:RadTextBox runat="server" ID="txtSensorAName" />
                        </div>
                        <div class="divFormCaption20Right">Asset Class: </div>
                        <div class="divFormItem30">
                            <telerik:RadComboBox runat="server" ID="cmbSensorAAsset" EmptyMessage="Select Asset Class" RenderMode="Lightweight" AllowCustomText="true" />
                        </div>
                    </div>
                    <div class="divFormRow">
                        <div class="divFormCaption10Left">ISO-4</div>
                        <div class="divFormCaption20Right">Process Tag: </div>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" ID="txtSensorA4Tag" CssClass="txtSmall" Text="2" />
                        </div>
                        <%--<div class="divFormCaption20Right">Warning Level: </div>--%>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" ID="txtSensorA4Warning" CssClass="txtSmallYellow hide" Text="18" />
                        </div>
                        <%--<div class="divFormCaption20Right">Alarm Level: </div>--%>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" ID="txtSensorA4Alarm" CssClass="txtSmallRed hide" Text="24" />
                        </div>
                    </div>
                    <div class="divFormRow">
                        <div class="divFormCaption10Left">ISO-6</div>
                        <div class="divFormCaption20Right">Process Tag:</div>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" ID="txtSensorA6Tag" CssClass="txtSmall" Text="3" />
                        </div>
                        <%--<div class="divFormCaption20Right">Warning Level: </div>--%>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" ID="txtSensorA6Warning" CssClass="txtSmallYellow hide" Text="18" />
                        </div>
                        <%--<div class="divFormCaption20Right">Alarm Level: </div>--%>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" ID="txtSensorA6Alarm" CssClass="txtSmallRed hide" Text="24" />
                        </div>
                    </div>
                    <div class="divFormRow">
                        <div class="divFormCaption10Left">ISO-14</div>
                        <div class="divFormCaption20Right">Process Tag:</div>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" ID="txtSensorA14Tag" CssClass="txtSmall" Text="4" />
                        </div>
                        <%--<div class="divFormCaption20Right">Warning Level: </div>--%>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" ID="txtSensorA14Warning" CssClass="txtSmallYellow hide" Text="18" />
                        </div>
                        <%--<div class="divFormCaption20Right">Alarm Level: </div>--%>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" ID="txtSensorA14Alarm" CssClass="txtSmallRed hide" Text="24" />
                        </div>
                    </div>
                    <div class="divFormRow">
                        <div class="divFormCaption10Left">ISO-21</div>
                        <div class="divFormCaption20Right">Process Tag:</div>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" ID="txtSensorA21Tag" CssClass="txtSmall" Text="5" />
                        </div>
                        <%--<div class="divFormCaption20Right">Warning Level: </div>--%>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" ID="txtSensorA21Warning" CssClass="txtSmallYellow hide" Text="18" />
                        </div>
                        <%--<div class="divFormCaption20Right">Alarm Level: </div>--%>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" ID="txtSensorA21Alarm" CssClass="txtSmallRed hide" Text="24" />
                        </div>
                    </div>
                    <div class="divFormRow">
                        <div class="divFormCaption20Right">Date Channel: </div>
                        <div class="divFormItem30">
                            <telerik:RadTextBox runat="server" ID="txtSensorADate" CssClass="txtSmall" Text="38" />
                        </div>
                        <div class="divFormCaption20Right">Flow Rate Channel: </div>
                        <div class="divFormItem30">
                            <telerik:RadTextBox runat="server" ID="txtSensorAFlow" CssClass="txtSmall" Text="7" />
                        </div>
                    </div>
                    <div class="divFormRow">
                        <div class="divFormCaption20Right">Temperature Channel: </div>
                        <div class="divFormItem30">
                            <telerik:RadTextBox runat="server" ID="txtSensorATemp" CssClass="txtSmall" Text="59" />
                        </div>
                        <div class="divFormCaption20Right">Relative Humidity Channel: </div>
                        <div class="divFormItem30">
                            <telerik:RadTextBox runat="server" ID="txtSensorARH" CssClass="txtSmall" Text="60" />
                        </div>
                    </div>
                    <div class="divFormRow">
                        <div class="divFormCaption20Right">Product: </div>
                        <div class="divFormItem30">
                            <telerik:RadComboBox runat="server" ID="cmbSensorAProduct" EmptyMessage="Select a Product" RenderMode="Lightweight" AllowCustomText="true" />
                        </div>
                    </div>
                    <div class="divFormRow">
                        <div class="divFormCaption20Right">Gauge Minimum: </div>
                        <div class="divFormItem30">
                            <telerik:RadTextBox runat="server" ID="txtSensorAMin" CssClass="txtSmall" Text="0" />
                        </div>
                        <div class="divFormCaption20Right">Gauge Maximum: </div>
                        <div class="divFormItem30">
                            <telerik:RadTextBox runat="server" ID="txtSensorAMax" CssClass="txtSmall" Text="40" />
                        </div>
                    </div>
                    <div class="divFormRow">
                        <div class="divFormCaption20Right">
                            <asp:Label runat="server" ID="lblSensorAUsers" Text="" />
                        </div>
                        <%--<telerik:RadButton runat="server" ID="btnSensorAUsers" Text="Define Users" OnClientClicked="" />--%>
                    </div>
                </asp:Panel>
                <div class="clear"></div>
                <div class="divFormSectionBorder"></div>
            </div>
            <div class="divFormSection">
                <div class="divFormHeading100">
                    <asp:Panel runat="server" ID="pnlSensorBToggle" CssClass="pnlToggleOn" onclick="toggleSensor(this, 2)">Particle Count Sensor 2</asp:Panel>
                </div>
                <asp:Panel runat="server" CssClass="divSensorHidden" ID="pnlSensorB">
                    <asp:HiddenField ID="hidSensorBId" runat="server" />
                    <asp:HiddenField ID="hidSensorBIndex" runat="server" Value="1" />
                    <div class="divFormRow">
                        <div class="divFormCaption20Right">Sensor Name: </div>
                        <div class="divFormItem30">
                            <telerik:RadTextBox runat="server" ID="txtSensorBName" />
                        </div>
                        <div class="divFormCaption20Right">Asset Class: </div>
                        <div class="divFormItem30">
                            <telerik:RadComboBox runat="server" ID="cmbSensorBAsset" EmptyMessage="Select Asset Class" RenderMode="Lightweight" AllowCustomText="true" />
                        </div>
                    </div>
                    <div class="divFormRow">
                        <div class="divFormCaption10Left">ISO-4</div>
                        <div class="divFormCaption20Right">Process Tag: </div>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" ID="txtSensorB4Tag" CssClass="txtSmall" Text="12" />
                        </div>
                        <%--<div class="divFormCaption20Right">Warning Level: </div>--%>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" CssClass="txtSmallYellow hide" ID="txtSensorB4Warning" Text="18" />
                        </div>
                        <%--<div class="divFormCaption20Right">Alarm Level: </div>--%>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" CssClass="txtSmallRed hide" ID="txtSensorB4Alarm" Text="24" />
                        </div>
                    </div>
                    <div class="divFormRow">
                        <div class="divFormCaption10Left">ISO-6</div>
                        <div class="divFormCaption20Right">Process Tag: </div>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" ID="txtSensorB6Tag" CssClass="txtSmall" Text="13" />
                        </div>
                        <%--<div class="divFormCaption20Right">Warning Level: </div>--%>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" CssClass="txtSmallYellow hide" ID="txtSensorB6Warning" Text="18" />
                        </div>
                        <%--<div class="divFormCaption20Right">Alarm Level: </div>--%>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" CssClass="txtSmallRed hide" ID="txtSensorB6Alarm" Text="24" />
                        </div>
                    </div>
                    <div class="divFormRow">
                        <div class="divFormCaption10Left">ISO-14</div>
                        <div class="divFormCaption20Right">Process Tag: </div>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" ID="txtSensorB14Tag" CssClass="txtSmall" Text="14" />
                        </div>
                        <%--<div class="divFormCaption20Right">Warning Level: </div>--%>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" CssClass="txtSmallYellow hide" ID="txtSensorB14Warning" Text="18" />
                        </div>
                        <%--<div class="divFormCaption20Right">Alarm Level: </div>--%>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" CssClass="txtSmallRed hide" ID="txtSensorB14Alarm" Text="24" />
                        </div>
                    </div>
                    <div class="divFormRow">
                        <div class="divFormCaption10Left">ISO-21</div>
                        <div class="divFormCaption20Right">Process Tag: </div>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" ID="txtSensorB21Tag" CssClass="txtSmall" Text="15" />
                        </div>
                        <%--<div class="divFormCaption20Right">Warning Level: </div>--%>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" CssClass="txtSmallYellow hide" ID="txtSensorB21Warning" Text="18" />
                        </div>
                        <%--<div class="divFormCaption20Right">Alarm Level: </div>--%>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" CssClass="txtSmallRed hide" ID="txtSensorB21Alarm" Text="24" />
                        </div>
                    </div>
                    <div class="divFormRow">
                        <div class="divFormCaption20Right">Date Channel: </div>
                        <div class="divFormItem30">
                            <telerik:RadTextBox runat="server" ID="txtSensorBDate" CssClass="txtSmall" Text="38" />
                        </div>
                        <div class="divFormCaption20Right">Flow Rate Channel: </div>
                        <div class="divFormItem30">
                            <telerik:RadTextBox runat="server" ID="txtSensorBFlow" CssClass="txtSmall" Text="17" />
                        </div>
                    </div>
                    
                    <div class="divFormRow">
                        <div class="divFormCaption20Right">Temperature Channel: </div>
                        <div class="divFormItem30">
                            <telerik:RadTextBox runat="server" ID="txtSensorBTemp" CssClass="txtSmall" Text="61" />
                        </div>
                        <div class="divFormCaption20Right">Relative Humidity Channel: </div>
                        <div class="divFormItem30">
                            <telerik:RadTextBox runat="server" ID="txtSensorBRH" CssClass="txtSmall" Text="62" />
                        </div>
                    </div>
                    <div class="divFormRow">
                        <div class="divFormCaption20Right">Product: </div>
                        <div class="divFormItem30">
                            <telerik:RadComboBox runat="server" ID="cmbSensorBProduct" EmptyMessage="Select a Product" RenderMode="Lightweight" AllowCustomText="true" />
                        </div>
                    </div>
                    <div class="divFormRow">
                        <div class="divFormCaption20Right">Gauge Minimum: </div>
                        <div class="divFormItem30">
                            <telerik:RadTextBox runat="server" ID="txtSensorBMin" CssClass="txtSmall" Text="0" />
                        </div>
                        <div class="divFormCaption20Right">Gauge Maximum: </div>
                        <div class="divFormItem30">
                            <telerik:RadTextBox runat="server" ID="txtSensorBMax" CssClass="txtSmall" Text="40" />
                        </div>
                    </div>
                    <div class="divFormRow">
                        <div class="divFormCaption20Right">
                            <asp:Label runat="server" ID="lblSensorBUsers" Text="" />
                        </div>
                        <%--<telerik:RadButton runat="server" ID="btnSensorBUsers" Text="Define Users" OnClientClicked="" />--%>
                    </div>
                </asp:Panel>
                <div class="clear"></div>
                <div class="divFormSectionBorder"></div>
            </div>
            <div class="divFormSection">
                <div class="divFormHeading100">
                    <asp:Panel runat="server" ID="pnlSensorCToggle" CssClass="pnlToggleOn" onclick="toggleSensor(this, 3)">Particle Count Sensor 3</asp:Panel>
                </div>
                <asp:Panel runat="server" CssClass="divSensorHidden" ID="pnlSensorC">
                    <asp:HiddenField ID="hidSensorCId" runat="server" />
                    <asp:HiddenField ID="hidSensorCIndex" runat="server" Value="2" />
                    <div class="divFormRow">
                        <div class="divFormCaption20Right">Sensor Name: </div>
                        <div class="divFormItem30">
                            <telerik:RadTextBox runat="server" ID="txtSensorCName" />
                        </div>
                        <div class="divFormCaption20Right">Asset Class: </div>
                        <div class="divFormItem30">
                            <telerik:RadComboBox runat="server" ID="cmbSensorCAsset" EmptyMessage="Select Asset Class" RenderMode="Lightweight" AllowCustomText="true" />
                        </div>
                    </div>
                    <div class="divFormRow">
                        <div class="divFormCaption10Left">ISO-4</div>
                        <div class="divFormCaption20Right">Process Tag: </div>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" ID="txtSensorC4Tag" CssClass="txtSmall" Text="22" />
                        </div>
                        <%--<div class="divFormCaption20Right">Warning Level: </div>--%>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" CssClass="txtSmallYellow hide" ID="txtSensorC4Warning" Text="18" />
                        </div>
                        <%--<div class="divFormCaption20Right">Alarm Level: </div>--%>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" CssClass="txtSmallRed hide" ID="txtSensorC4Alarm" Text="24" />
                        </div>
                    </div>
                    <div class="divFormRow">
                        <div class="divFormCaption10Left">ISO-6</div>
                        <div class="divFormCaption20Right">Process Tag: </div>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" ID="txtSensorC6Tag" CssClass="txtSmall" Text="23" />
                        </div>
                        <%--<div class="divFormCaption20Right">Warning Level: </div>--%>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" CssClass="txtSmallYellow hide" ID="txtSensorC6Warning" Text="18" />
                        </div>
                        <%--<div class="divFormCaption20Right">Alarm Level: </div>--%>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" CssClass="txtSmallRed hide" ID="txtSensorC6Alarm" Text="24" />
                        </div>
                    </div>
                    <div class="divFormRow">
                        <div class="divFormCaption10Left">ISO-14</div>
                        <div class="divFormCaption20Right">Process Tag: </div>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" ID="txtSensorC14Tag" CssClass="txtSmall" Text="24" />
                        </div>
                        <%--<div class="divFormCaption20Right">Warning Level: </div>--%>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" CssClass="txtSmallYellow hide" ID="txtSensorC14Warning" Text="18" />
                        </div>
                        <%--<div class="divFormCaption20Right">Alarm Level: </div>--%>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" CssClass="txtSmallRed hide" ID="txtSensorC14Alarm" Text="24" />
                        </div>
                    </div>
                    <div class="divFormRow">
                        <div class="divFormCaption10Left">ISO-21</div>
                        <div class="divFormCaption20Right">Process Tag: </div>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" ID="txtSensorC21Tag" CssClass="txtSmall" Text="25" />
                        </div>
                        <%--<div class="divFormCaption20Right">Warning Level: </div>--%>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" CssClass="txtSmallYellow hide" ID="txtSensorC21Warning" Text="18" />
                        </div>
                        <%--<div class="divFormCaption20Right">Alarm Level: </div>--%>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" CssClass="txtSmallRed hide" ID="txtSensorC21Alarm" Text="24" />
                        </div>
                    </div>
                    <div class="divFormRow">
                        <div class="divFormCaption20Right">Date Channel: </div>
                        <div class="divFormItem30">
                            <telerik:RadTextBox runat="server" ID="txtSensorCDate" CssClass="txtSmall" Text="38" />
                        </div>
                        <div class="divFormCaption20Right">Flow Rate Channel: </div>
                        <div class="divFormItem30">
                            <telerik:RadTextBox runat="server" ID="txtSensorCFlow" CssClass="txtSmall" Text="27" />
                        </div>
                    </div>
                    
                    <div class="divFormRow">
                        <div class="divFormCaption20Right">Temperature Channel: </div>
                        <div class="divFormItem30">
                            <telerik:RadTextBox runat="server" ID="txtSensorCTemp" CssClass="txtSmall" Text="63" />
                        </div>
                        <div class="divFormCaption20Right">Relative Humidity Channel: </div>
                        <div class="divFormItem30">
                            <telerik:RadTextBox runat="server" ID="txtSensorCRH" CssClass="txtSmall" Text="64" />
                        </div>
                    </div>
                    <div class="divFormRow">
                        <div class="divFormCaption20Right">Product: </div>
                        <div class="divFormItem30">
                            <telerik:RadComboBox runat="server" ID="cmbSensorCProduct" EmptyMessage="Select a Product" RenderMode="Lightweight" AllowCustomText="true" />
                        </div>
                    </div>
                    <div class="divFormRow">
                        <div class="divFormCaption20Right">Gauge Minimum: </div>
                        <div class="divFormItem30">
                            <telerik:RadTextBox runat="server" ID="txtSensorCMin" CssClass="txtSmall" Text="0" />
                        </div>
                        <div class="divFormCaption20Right">Gauge Maximum: </div>
                        <div class="divFormItem30">
                            <telerik:RadTextBox runat="server" ID="txtSensorCMax" CssClass="txtSmall" Text="40" />
                        </div>
                    </div>
                    <div class="divFormRow">
                        <div class="divFormCaption20Right">
                            <asp:Label runat="server" ID="lblSensorCUsers" Text="" />
                        </div>

                        <%--<telerik:RadButton runat="server" ID="btnSensorCUsers" Text="Define Users" OnClientClicked="" />--%>
                    </div>
                </asp:Panel>
                <div class="clear"></div>
                <div class="divFormSectionBorder"></div>
            </div>
            <div class="divFormSection">
                <div class="divFormHeading100">
                    <asp:Panel runat="server" ID="pnlSensorDToggle" CssClass="pnlToggleOn" onclick="toggleSensor(this, 4)">Particle Count Sensor 4</asp:Panel>
                </div>
                <asp:Panel runat="server" CssClass="divSensorHidden" ID="pnlSensorD">
                    <asp:HiddenField ID="hidSensorDId" runat="server" />
                    <asp:HiddenField ID="hidSensorDIndex" runat="server" Value="3" />
                    <div class="divFormRow">
                        <div class="divFormCaption20Right">Sensor Name: </div>
                        <div class="divFormItem30">
                            <telerik:RadTextBox runat="server" ID="txtSensorDName" />
                        </div>
                        <div class="divFormCaption20Right">Asset Class: </div>
                        <div class="divFormItem30">
                            <telerik:RadComboBox runat="server" ID="cmbSensorDAsset" EmptyMessage="Select Asset Class" RenderMode="Lightweight" AllowCustomText="true" />
                        </div>
                    </div>
                    <div class="divFormRow">
                        <div class="divFormCaption10Left">ISO-4</div>
                        <div class="divFormCaption20Right">Process Tag: </div>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" ID="txtSensorD4Tag" CssClass="txtSmall" Text="32" />
                        </div>
                        <%--<div class="divFormCaption20Right">Warning Level: </div>--%>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" CssClass="txtSmallYellow hide" ID="txtSensorD4Warning" Text="18" />
                        </div>
                        <%--<div class="divFormCaption20Right">Alarm Level: </div>--%>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" CssClass="txtSmallRed hide" ID="txtSensorD4Alarm" Text="24" />
                        </div>
                    </div>
                    <div class="divFormRow">
                        <div class="divFormCaption10Left">ISO-6</div>
                        <div class="divFormCaption20Right">Process Tag: </div>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" ID="txtSensorD6Tag" CssClass="txtSmall" Text="33" />
                        </div>
                        <%--<div class="divFormCaption20Right">Warning Level: </div>--%>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" CssClass="txtSmallYellow hide" ID="txtSensorD6Warning" Text="18" />
                        </div>
                        <%--<div class="divFormCaption20Right">Alarm Level: </div>--%>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" CssClass="txtSmallRed hide" ID="txtSensorD6Alarm" Text="24" />
                        </div>
                    </div>
                    <div class="divFormRow">
                        <div class="divFormCaption10Left">ISO-14</div>
                        <div class="divFormCaption20Right">Process Tag: </div>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" ID="txtSensorD14Tag" CssClass="txtSmall" Text="34" />
                        </div>
                        <%--<div class="divFormCaption20Right">Warning Level: </div>--%>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" CssClass="txtSmallYellow hide" ID="txtSensorD14Warning" Text="18" />
                        </div>
                        <%--<div class="divFormCaption20Right">Alarm Level: </div>--%>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" CssClass="txtSmallRed hide" ID="txtSensorD14Alarm" Text="24" />
                        </div>
                    </div>
                    <div class="divFormRow">
                        <div class="divFormCaption10Left">ISO-21</div>
                        <div class="divFormCaption20Right">Process Tag: </div>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" ID="txtSensorD21Tag" CssClass="txtSmall" Text="35" />
                        </div>
                        <%--<div class="divFormCaption20Right">Warning Level: </div>--%>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" CssClass="txtSmallYellow hide" ID="txtSensorD21Warning" Text="18" />
                        </div>
                        <%--<div class="divFormCaption20Right">Alarm Level: </div>--%>
                        <div class="divFormItem10">
                            <telerik:RadTextBox runat="server" CssClass="txtSmallRed hide" ID="txtSensorD21Alarm" Text="24" />
                        </div>
                    </div>
                    <div class="divFormRow">
                        <div class="divFormCaption20Right">Date Channel: </div>
                        <div class="divFormItem30">
                            <telerik:RadTextBox runat="server" ID="txtSensorDDate" CssClass="txtSmall" Text="38" />
                        </div>
                        <div class="divFormCaption20Right">Flow Rate Channel: </div>
                        <div class="divFormItem30">
                            <telerik:RadTextBox runat="server" ID="txtSensorDFlow" CssClass="txtSmall" Text="37" />
                        </div>
                    </div>
                    
                    <div class="divFormRow">
                        <div class="divFormCaption20Right">Temperature Channel: </div>
                        <div class="divFormItem30">
                            <telerik:RadTextBox runat="server" ID="txtSensorDTemp" CssClass="txtSmall" Text="65" />
                        </div>
                        <div class="divFormCaption20Right">Relative Humidity Channel: </div>
                        <div class="divFormItem30">
                            <telerik:RadTextBox runat="server" ID="txtSensorDRH" CssClass="txtSmall" Text="66" />
                        </div>
                    </div>
                    <div class="divFormRow">
                        <div class="divFormCaption20Right">Product: </div>
                        <div class="divFormItem30">
                            <telerik:RadComboBox runat="server" ID="cmbSensorDProduct" EmptyMessage="Select a Product" RenderMode="Lightweight" AllowCustomText="true" />
                        </div>
                    </div>
                    <div class="divFormRow">
                        <div class="divFormCaption20Right">Gauge Minimum: </div>
                        <div class="divFormItem30">
                            <telerik:RadTextBox runat="server" ID="txtSensorDMin" CssClass="txtSmall" Text="0" />
                        </div>
                        <div class="divFormCaption20Right">Gauge Maximum: </div>
                        <div class="divFormItem30">
                            <telerik:RadTextBox runat="server" ID="txtSensorDMax" CssClass="txtSmall" Text="40" />
                        </div>
                    </div>
                    <div class="divFormRow">
                        <div class="divFormCaption20Right">
                            <asp:Label runat="server" ID="lblSensorDUsers" Text="" />
                        </div>
                        <%--<telerik:RadButton runat="server" ID="btnSensorDUsers" Text="Define Users" OnClientClicked="" />--%>
                    </div>
                </asp:Panel>
                <div class="clear"></div>
                <div class="divFormSectionBorder"></div>
                <div class="divFormSection">
                    <asp:Label runat="server" ID="lblUsers" Text="Users with access to this unit" />
                    <asp:CheckBoxList runat="server" ID="lstUsers" />
                </div>
            </div>

        </div>
    </div>
</asp:Content>
