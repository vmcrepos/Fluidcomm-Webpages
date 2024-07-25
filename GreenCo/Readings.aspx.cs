// Decompiled with JetBrains decompiler
// Type: GreenCo.Readings
// Assembly: GreenCo, Version=0.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F63B31C9-6FC1-4AED-A804-2C1309A8A158
// Assembly location: C:\Users\gregg\Desktop\Fluidcomm-Webpages\bin\GreenCo.dll

using GreenCo.Constants;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Web.UI.HtmlChart;

#nullable disable
namespace GreenCo
{
  public class Readings : Page
  {
    private bool altrow;
    protected System.Web.UI.ScriptManager scriptMan;
    protected HiddenField hidDisplayIndex;
    protected Panel pnlLeft;
    protected Panel pnlLeftTitle;
    protected Label lblSideTitle;
    protected Panel pnlAdminCheckBox;
    protected Panel Panel2;
    protected Panel Panel3;
    protected CheckBox chkAllUnits;
    protected Panel pnlLeftSelect1;
    protected Panel pnlLeftSelect1A;
    protected Label lblCompany;
    protected Panel pnlLeftSelect1B;
    protected RadDropDownList cmbCompany;
    protected Panel pnlLeftSelect2;
    protected Panel pnlLeftSelect2A;
    protected Label lblLocation;
    protected Panel pnlLeftSelect2B;
    protected RadDropDownList cmbLocation;
    protected Panel pnlLeftSelect3;
    protected Panel pnlLeftSelect3A;
    protected Label lblAsset;
    protected Panel pnlLeftSelect3B;
    protected RadDropDownList cmbAsset;
    protected Panel pnlLeftSelect4;
    protected Panel pnlLeftSelect4A;
    protected Label lblFluidType;
    protected Panel pnlLeftSelect4B;
    protected RadDropDownList cmbFluidType;
    protected Panel pnlLeftSelect5;
    protected Panel pnlLeftSelect5A;
    protected Label lblSensor;
    protected Panel pnlLeftSelect5B;
    protected RadDropDownList cmbSensor;
    protected Panel pnlLeftSelect6;
    protected Panel pnlLeftSelect6A;
    protected Label lblDataType;
    protected Panel pnlLeftSelect6B;
    protected RadDropDownList cmbDataType;
    protected Panel pnlRight;
    protected Panel pnlRightTitle;
    protected Label lblMainTitle;
    protected Panel pnlReadingMenu;
    protected Panel pnlOption0;
    protected Panel pnlOption1;
    protected Panel pnlOption2;
    protected Panel pnlRefresh;
    protected Panel pnlAutoRefresh;
    protected ImageButton btnRefresh;
    protected ImageButton btnAutoRefresh;
    protected Panel pnlContent0;
    protected Panel pnlContent1;
    protected UpdatePanel updContent1;
    protected Panel pnlOption1A;
    protected TextBox txtDate1A;
    protected TextBox txtDate1B;
    protected Panel pnlOption1B;
    protected RadButton btnReadingsExcel;
    protected CheckBox chkZeroes;
    protected Panel pnlContentBody1;
    protected GridView gridReadings;
    protected GridView gridReadings2;
    protected Panel pnlContent2;
    protected Panel pnlOption2A;
    protected TextBox txtDate2A;
    protected TextBox txtDate2B;
    protected Panel pnlOption2B;
    protected RadDropDownList cmbReportMode;
    protected Panel pnlReportContainer1;
    protected Panel pnlChartOptionsWrapper;
    protected Panel pnlChartOptionsFloater;
    protected Panel pnlChartOption0;
    protected Panel pnlChartOption1;
    protected Panel pnlChartOption2;
    protected Panel pnlChartOption3;
    protected Panel pnlChartOption4;
    protected Panel pnlChartOption5;
    protected RadHtmlChart chrtReport;
    protected GridView gridReport;
    protected Panel pnlReportContainer2;
    protected Panel pnlChartOptionsWrapper2;
    protected Panel pnlChartOptionsFloater2;
    protected Panel pnlChartOption6;
    protected Panel pnlChartOption7;
    protected Panel pnlChartOption8;
    protected Panel pnlChartOption9;
    protected RadHtmlChart chrtReport2;
    protected GridView gridReport2;
    protected Panel pnlContent3;
    protected Panel Panel1;
    protected TextBox txtDate3A;
    protected TextBox txtDate3B;
    protected Panel Panel4;
    protected CheckBox chkCurrentAlarms;
    protected RadButton btnSummaryExcel;
    protected GridView gridAlarms;
    protected Panel pnlContent4;
    protected Panel pnlDevice1;
    protected Panel pnlDevice2;

    private DataTable CurrentUnits
    {
      get
      {
        if (this.Session[nameof (CurrentUnits)] == null)
          this.Session[nameof (CurrentUnits)] = (object) Utils.RefreshUnits(this.chkAllUnits.Checked);
        return (DataTable) this.Session[nameof (CurrentUnits)];
      }
      set => this.Session[nameof (CurrentUnits)] = (object) value;
    }

    private DataTable CurrentReadings
    {
      get
      {
        return this.Session[nameof (CurrentReadings)] == null ? (DataTable) null : (DataTable) this.Session[nameof (CurrentReadings)];
      }
      set => this.Session[nameof (CurrentReadings)] = (object) value;
    }

    private DataTable CurrentReadings2
    {
      get
      {
        return this.Session[nameof (CurrentReadings2)] == null ? (DataTable) null : (DataTable) this.Session[nameof (CurrentReadings2)];
      }
      set => this.Session[nameof (CurrentReadings2)] = (object) value;
    }

    private string sortexpr
    {
      get
      {
        if (this.Session["Readings_sortexpr"] == null)
          this.Session["Readings_sortexpr"] = (object) "date DESC";
        return this.Session["Readings_sortexpr"].ToString();
      }
      set => this.Session["Readings_sortexpr"] = (object) value;
    }

    private string sortexpr2
    {
      get
      {
        if (this.Session["Readings_sortexpr2"] == null)
          this.Session["Readings_sortexpr2"] = (object) "date DESC";
        return this.Session["Readings_sortexpr2"].ToString();
      }
      set => this.Session["Readings_sortexpr2"] = (object) value;
    }

    private Dictionary<string, int> ReadingsFilters
    {
      get
      {
        if (this.Session[nameof (ReadingsFilters)] == null)
          this.Session[nameof (ReadingsFilters)] = (object) SessionHelper.GetReadingsFilters();
        return (Dictionary<string, int>) this.Session[nameof (ReadingsFilters)];
      }
      set => this.Session[nameof (ReadingsFilters)] = (object) value;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      this.CurrentUnits = Utils.RefreshUnits(this.chkAllUnits.Checked);
      this.pnlAdminCheckBox.Visible = Roles.IsUserInRole("CompanyAdmin") || Roles.IsUserInRole("Admin");
      if (!this.IsPostBack)
      {
        this.chkZeroes.Visible = Roles.IsUserInRole("Admin");
        this.chkZeroes.Checked = false;
        if (this.CurrentUnits.Rows.Count > 0)
        {
          this.txtDate1A.Text = DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd");
          this.txtDate2A.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
          TextBox txtDate3A = this.txtDate3A;
          DateTime dateTime = DateTime.Now;
          dateTime = dateTime.AddDays(-1.0);
          string str = dateTime.ToString("yyyy-MM-dd");
          txtDate3A.Text = str;
          this.txtDate1B.Text = DateTime.Now.ToString("yyyy-MM-dd");
          this.txtDate2B.Text = DateTime.Now.ToString("yyyy-MM-dd");
          this.txtDate3B.Text = DateTime.Now.ToString("yyyy-MM-dd");
          this.PopulateCompanyDropDown();
          this.PopulateDataTypeDropDown();
          this.cmbCompany_SelectedIndexChanged((object) null, (EventArgs) null);
          this.pnlAdminCheckBox.Visible = Roles.IsUserInRole("Admin") || Roles.IsUserInRole("CompanyAdmin");
          int result;
          this.hidDisplayIndex.Value = !int.TryParse(this.hidDisplayIndex.Value, out result) ? "0" : Math.Max(0, Math.Min(4, result)).ToString();
          this.UpdateQuickView();
          this.UpdateReport();
          this.UpdateAlarms();
        }
        else
          this.ShowNoUnitsForUserMessage();
      }
      else if (this.CurrentUnits.Rows.Count > 0)
      {
        int result1;
        if (int.TryParse(this.hidDisplayIndex.Value, out result1))
          this.SetDisplayIndex(result1);
        if (this.Request.Form["__EVENTTARGET"] != null)
        {
          string str = this.Request.Form["__EVENTTARGET"];
          if (str == "ChartOption0")
            this.TriggerChartOption(0);
          if (str == "ChartOption1")
            this.TriggerChartOption(1);
          if (str == "ChartOption2")
            this.TriggerChartOption(2);
          if (str == "ChartOption3")
            this.TriggerChartOption(3);
          if (str == "ChartOption4")
            this.TriggerChartOption(4);
          if (str == "ChartOption5")
            this.TriggerChartOption(5);
          if (str == "ChartOption6")
            this.TriggerChartOption(6);
          int result2;
          if (str.Length > 5 && str.Substring(0, 5) == "Alarm" && int.TryParse(str.Substring(5), out result2))
            this.NavigateAlarm(result2);
          if (str.Length > 8 && str.Substring(0, 8) == nameof (Readings))
          {
            string[] strArray = str.Substring(8).Split('_');
            int result3;
            if (int.TryParse(strArray[0], out result2) && int.TryParse(strArray[1], out result3))
              this.NavigateReadings(result2, result3);
          }
          int num = str == "refresh" ? 1 : 0;
        }
        this.UpdateAll();
      }
      else
        this.ShowNoUnitsForUserMessage();
    }

    private void ShowNoUnitsForUserMessage()
    {
      Panel child1 = new Panel();
      child1.CssClass = "readingsInfoBox";
      Panel child2 = new Panel();
      child2.CssClass = "textInfo";
      child2.Controls.Add((Control) new Literal()
      {
        Text = "No units are assigned to this user account. Please contact your company admin for assistance."
      });
      child1.Controls.Add((Control) child2);
      this.pnlContent0.Controls.Add((Control) child1);
    }

    private void TriggerChartOption(int index)
    {
      Panel[] panelArray = new Panel[7]
      {
        this.pnlChartOption0,
        this.pnlChartOption1,
        this.pnlChartOption2,
        this.pnlChartOption3,
        this.pnlChartOption4,
        this.pnlChartOption5,
        this.pnlChartOption6
      };
      if (panelArray[index].CssClass == "pnlChartOptionDisabled")
        panelArray[index].CssClass = "pnlChartOption" + index.ToString();
      else
        panelArray[index].CssClass = "pnlChartOptionDisabled";
    }

    private void UpdateAll()
    {
      this.UpdateQuickView();
      this.UpdateReadings();
      this.UpdateReport();
      this.UpdateAlarms();
    }

    private void NavigateAlarm(int SensorID)
    {
      this.SetDisplayIndex(3);
      this.chkCurrentAlarms.Checked = true;
      this.cmbSensor.SelectedValue = SensorID.ToString();
      this.txtDate3A.Text = "";
      this.txtDate3B.Text = "";
    }

    private void NavigateReadings(int ReadingIndex, int SensorID)
    {
      this.SetDisplayIndex(1);
      this.chkZeroes.Checked = true;
      this.cmbSensor.SelectedValue = SensorID.ToString();
      this.cmbDataType.SelectedIndex = ReadingIndex;
      TextBox txtDate1A = this.txtDate1A;
      DateTime dateTime = DateTime.Now;
      dateTime = dateTime.AddDays(-1.0);
      string str = dateTime.ToString("yyyy-MM-dd");
      txtDate1A.Text = str;
      this.txtDate1B.Text = DateTime.Now.ToString("yyyy-MM-dd");
    }

    private void SetDisplayIndex(int DisplayIndex)
    {
      this.hidDisplayIndex.Value = DisplayIndex.ToString();
      this.pnlContent0.CssClass = "pnlContentHidden";
      this.pnlContent1.CssClass = "pnlContentHidden";
      this.pnlContent2.CssClass = "pnlContentHidden";
      this.pnlContent3.CssClass = "pnlContentHidden";
      this.pnlContent4.CssClass = "pnlContentHidden";
      this.pnlOption0.CssClass = "pnlMenuOption";
      this.pnlOption1.CssClass = "pnlMenuOption";
      this.pnlOption2.CssClass = "pnlMenuOption";
      if (DisplayIndex == 0)
      {
        this.pnlContent0.CssClass = "pnlContentVisible";
        this.pnlOption0.CssClass = "pnlMenuOptionSelected";
      }
      if (DisplayIndex == 1)
      {
        this.pnlContent1.CssClass = "pnlContentVisible";
        this.pnlOption1.CssClass = "pnlMenuOptionSelected";
      }
      if (DisplayIndex != 2)
        return;
      this.pnlContent2.CssClass = "pnlContentVisible";
      this.pnlOption2.CssClass = "pnlMenuOptionSelected";
    }

    private void PopulateCompanyDropDown()
    {
      List<string> stringList = new List<string>();
      for (int index = 0; index < this.CurrentUnits.Rows.Count; ++index)
      {
        if (!stringList.Contains(this.CurrentUnits.Rows[index]["CompanyName"].ToString()))
          stringList.Add(this.CurrentUnits.Rows[index]["CompanyName"].ToString());
      }
      this.cmbCompany.DataSource = (object) stringList;
      this.cmbCompany.DataBind();
      if (this.cmbCompany.Items.Count > 0)
        this.cmbCompany.SelectedIndex = this.ReadingsFilters != null ? this.ReadingsFilters["CompanyIndex"] : 0;
      this.PopulateLocationDropDown();
    }

    private void PopulateLocationDropDown()
    {
      this.cmbLocation.Enabled = true;
      if (this.cmbCompany.SelectedIndex < 0)
      {
        this.cmbLocation.DataSource = (object) null;
        this.cmbLocation.DataBind();
        this.cmbLocation.Enabled = false;
      }
      else
      {
        string text = this.cmbCompany.SelectedItem.Text;
        List<string> stringList = new List<string>();
        for (int index = 0; index < this.CurrentUnits.Rows.Count; ++index)
        {
          if (!(this.CurrentUnits.Rows[index]["CompanyName"].ToString() != text) && !stringList.Contains(this.CurrentUnits.Rows[index]["UnitCategory"].ToString()))
            stringList.Add(this.CurrentUnits.Rows[index]["UnitCategory"].ToString());
        }
        this.cmbLocation.DataSource = (object) stringList;
        this.cmbLocation.DataBind();
        if (this.cmbLocation.Items.Count > 0)
          this.cmbLocation.SelectedIndex = this.ReadingsFilters != null ? this.ReadingsFilters["LocationIndex"] : 0;
        this.PopulateAssetDropDown();
      }
    }

    private void PopulateAssetDropDown()
    {
      this.cmbAsset.Enabled = true;
      if (this.cmbLocation.SelectedIndex < 0)
      {
        this.cmbAsset.DataSource = (object) null;
        this.cmbAsset.DataBind();
        this.cmbAsset.Enabled = false;
      }
      else
      {
        string text1 = this.cmbCompany.SelectedItem.Text;
        string text2 = this.cmbLocation.SelectedItem.Text;
        List<string> stringList = new List<string>();
        for (int index = 0; index < this.CurrentUnits.Rows.Count; ++index)
        {
          if (!(this.CurrentUnits.Rows[index]["CompanyName"].ToString() != text1) && !(this.CurrentUnits.Rows[index]["UnitCategory"].ToString() != text2))
          {
            foreach (Sensor sensor in (List<Sensor>) this.CurrentUnits.Rows[index]["Sensors"])
            {
              if (!stringList.Contains(sensor.SensorCategory1))
                stringList.Add(sensor.SensorCategory1);
            }
          }
        }
        this.cmbAsset.DataSource = (object) stringList;
        this.cmbAsset.DataBind();
        if (this.cmbAsset.Items.Count > 0)
          this.cmbAsset.SelectedIndex = this.ReadingsFilters != null ? this.ReadingsFilters["AssetIndex"] : 0;
        this.PopulateFluidTypeDropDown();
      }
    }

    private void PopulateFluidTypeDropDown()
    {
      this.cmbSensor.Enabled = true;
      if (this.cmbAsset.SelectedIndex < 0)
      {
        this.cmbFluidType.DataSource = (object) null;
        this.cmbFluidType.DataBind();
        this.cmbFluidType.Enabled = false;
      }
      else
      {
        string text1 = this.cmbCompany.SelectedItem.Text;
        string text2 = this.cmbLocation.SelectedItem.Text;
        string text3 = this.cmbAsset.SelectedItem.Text;
        List<string> stringList = new List<string>();
        stringList.Add("All Fluid Types");
        int num = -1;
        if (this.cmbSensor.SelectedIndex >= 0)
          num = int.Parse(this.cmbSensor.SelectedValue);
        for (int index = 0; index < this.CurrentUnits.Rows.Count; ++index)
        {
          if (!(this.CurrentUnits.Rows[index]["CompanyName"].ToString() != text1) && !(this.CurrentUnits.Rows[index]["UnitCategory"].ToString() != text2) && Utils.UnitHasAsset(this.CurrentUnits.Rows[index], text3))
          {
            foreach (Sensor sensor in (List<Sensor>) this.CurrentUnits.Rows[index]["Sensors"])
            {
              if ((num == -1 || sensor.SensorID == num) && !stringList.Contains(sensor.SensorCategory2))
                stringList.Add(sensor.SensorCategory2);
            }
          }
        }
        this.cmbFluidType.DataSource = (object) stringList;
        this.cmbFluidType.DataBind();
        this.cmbFluidType.SelectedIndex = this.ReadingsFilters != null ? this.ReadingsFilters["FluidTypeIndex"] : 0;
        this.PopulateSensorDropDown();
      }
    }

    private void PopulateSensorDropDown()
    {
      this.cmbSensor.Enabled = true;
      if (this.cmbAsset.SelectedIndex < 0)
      {
        this.cmbSensor.DataSource = (object) null;
        this.cmbSensor.DataBind();
        this.cmbSensor.Items.Insert(0, new DropDownListItem("All Sensors", "-1"));
        this.cmbSensor.Enabled = false;
      }
      else
      {
        string text1 = this.cmbCompany.SelectedItem.Text;
        string text2 = this.cmbLocation.SelectedItem.Text;
        string text3 = this.cmbAsset.SelectedItem.Text;
        string text4 = this.cmbFluidType.SelectedItem.Text;
        List<Sensor> sensorList = new List<Sensor>();
        for (int index = 0; index < this.CurrentUnits.Rows.Count; ++index)
        {
          if (!(this.CurrentUnits.Rows[index]["CompanyName"].ToString() != text1) && !(this.CurrentUnits.Rows[index]["UnitCategory"].ToString() != text2))
          {
            foreach (Sensor sensor in (List<Sensor>) this.CurrentUnits.Rows[index]["Sensors"])
            {
              if (!(sensor.SensorCategory1 != text3) && (this.cmbFluidType.SelectedIndex == 0 || this.cmbFluidType.SelectedText == sensor.SensorCategory2))
                sensorList.Add(sensor);
            }
          }
        }
        this.cmbSensor.DataSource = (object) sensorList;
        this.cmbSensor.DataBind();
        this.cmbSensor.Items.Insert(0, new DropDownListItem("All Sensors", "-1"));
        this.cmbSensor.SelectedIndex = this.ReadingsFilters != null ? this.ReadingsFilters["SensorIndex"] : -1;
      }
    }

    private void PopulateDataTypeDropDown()
    {
      this.cmbDataType.Items.Clear();
      this.cmbDataType.Items.Add("All Data Types");
      this.cmbDataType.Items.Add("Particle Count");
      this.cmbDataType.Items.Add("Physical Properties");
      this.cmbDataType.SelectedIndex = this.ReadingsFilters != null ? this.ReadingsFilters["DataTypeIndex"] : 1;
    }

    protected void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.PopulateLocationDropDown();
      this.lblSideTitle.Text = this.cmbCompany.SelectedIndex >= 0 ? this.cmbCompany.SelectedItem.Text : "";
      this.ReadingsFilters["CompanyIndex"] = this.cmbCompany.SelectedIndex;
      this.cmbLocation_SelectedIndexChanged((object) null, (EventArgs) null);
    }

    protected void cmbLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.PopulateAssetDropDown();
      this.ReadingsFilters["LocationIndex"] = this.cmbLocation.SelectedIndex;
      this.cmbAsset_SelectedIndexChanged((object) null, (EventArgs) null);
    }

    protected void cmbAsset_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.PopulateFluidTypeDropDown();
      this.ReadingsFilters["AssetIndex"] = this.cmbAsset.SelectedIndex;
      this.cmbFluidType_SelectedIndexChanged((object) null, (EventArgs) null);
    }

    protected void cmbFluidType_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.PopulateSensorDropDown();
      this.lblMainTitle.Text = this.cmbLocation.SelectedIndex < 0 || this.cmbAsset.SelectedIndex < 0 ? "No Units Available" : this.cmbLocation.SelectedItem.Text + ": " + this.cmbAsset.SelectedItem.Text;
      this.ReadingsFilters["FluidTypeIndex"] = this.cmbFluidType.SelectedIndex;
      this.cmbUnit_SelectedIndexChanged((object) null, (EventArgs) null);
    }

    protected void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.PopulateSensorDropDown();
      this.cmbSensor_SelectedIndexChanged((object) null, (EventArgs) null);
    }

    protected void cmbSensor_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.ReadingsFilters["SensorIndex"] = this.cmbSensor.SelectedIndex;
      this.UpdateQuickView();
      this.UpdateReadings();
      this.UpdateReport();
      this.UpdateAlarms();
    }

    protected void cmbDataType_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.ReadingsFilters["DataTypeIndex"] = this.cmbDataType.SelectedIndex;
      this.cmbCompany_SelectedIndexChanged((object) null, (EventArgs) null);
    }

    protected void chkAllUnits_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.PopulateCompanyDropDown();
      this.cmbCompany_SelectedIndexChanged((object) null, (EventArgs) null);
    }

    private void ClearPanels(int index)
    {
      if (index == 0 || index < 0)
      {
        this.pnlContent0.Controls.Clear();
        Panel child = new Panel();
        child.CssClass = "pnlError";
        child.Controls.Add((Control) new Label()
        {
          Text = "Select an asset category on the left"
        });
        this.pnlContent0.Controls.Add((Control) child);
      }
      if (index == 1 || index < 0)
      {
        this.gridReadings.DataSource = (object) null;
        this.gridReadings.DataBind();
      }
      if (index == 2 || index < 0)
      {
        this.chrtReport.DataSource = (object) null;
        this.chrtReport.DataBind();
      }
      if (index != 3 && index >= 0)
        return;
      this.gridAlarms.DataSource = (object) null;
      this.gridAlarms.DataBind();
    }

    private DataRow FindReading(DataTable tbl, int SensorID)
    {
      foreach (DataRow row in (InternalDataCollectionBase) tbl.Rows)
      {
        int result;
        if (row[nameof (SensorID)] != null && row[nameof (SensorID)] != DBNull.Value && int.TryParse(row[nameof (SensorID)].ToString(), out result) && result == SensorID)
          return row;
      }
      return (DataRow) null;
    }

    private DateTime DateMax(DateTime date1, DateTime? date2)
    {
      return !date2.HasValue || date1 >= date2.Value ? date1 : date2.Value;
    }

    private Panel GenerateParticleCountBox(
      Channel[] cs,
      int sensorID,
      int ReadingTypeIndex,
      DateTime maxDate,
      Channel AltChannel)
    {
      if (cs.Length < 4)
        return (Panel) null;
      Panel particleCountBox = new Panel();
      particleCountBox.CssClass = "pnlBoxGroup";
      Panel child1 = new Panel();
      child1.CssClass = "pnlBoxTitle";
      particleCountBox.Controls.Add((Control) child1);
      Label child2 = new Label();
      child2.CssClass = "lblBoxTitle";
      child2.Text = "ISO PARTICLE READING";
      child1.Controls.Add((Control) child2);
      for (int index = 0; index < 3; ++index)
        particleCountBox.Controls.Add((Control) cs[index].GeneratePanel(sensorID, ReadingTypeIndex, -1.0M, (string) null));
      Panel child3 = new Panel();
      child3.CssClass = "pnlBoxDate";
      particleCountBox.Controls.Add((Control) child3);
      Label child4 = new Label() { Text = "" };
      child4.Text = "Last Reading: " + maxDate.ToString(Utils.MasterDateTimeDisplayString) + " ";
      if (AltChannel != null && AltChannel.CurrentValue.HasValue)
      {
        Label label = child4;
        label.Text = label.Text + "Flow: " + Utils.ConvertFlowRate((int) AltChannel.CurrentValue.Value).ToString() + " ml/min";
      }
      child3.Controls.Add((Control) child4);
      return particleCountBox;
    }

    private Panel GenerateStandardBox(
      Channel cs,
      int sensorID,
      int ReadingTypeIndex,
      string title,
      Decimal conversionFactor,
      string precision)
    {
      Panel standardBox = new Panel();
      standardBox.CssClass = "pnlBoxSingle";
      Panel child1 = new Panel();
      child1.CssClass = "pnlBoxTitle";
      standardBox.Controls.Add((Control) child1);
      Label child2 = new Label();
      child2.CssClass = "lblBoxTitle";
      child2.Text = title;
      child1.Controls.Add((Control) child2);
      standardBox.Controls.Add((Control) cs.GeneratePanel(sensorID, ReadingTypeIndex, conversionFactor, precision));
      Panel child3 = new Panel();
      child3.CssClass = "pnlBoxDate";
      standardBox.Controls.Add((Control) child3);
      Label child4 = new Label();
      child4.Text = "";
      string str = "None";
      if (cs.LastReadingDate.HasValue)
        str = Utils.HowLongAgo(cs.LastReadingDate.Value);
      child4.Text = str;
      child3.Controls.Add((Control) child4);
      return standardBox;
    }

    private void UpdateQuickView()
    {
      this.pnlContent0.Controls.Clear();
      if (this.cmbCompany.SelectedIndex < 0 || this.cmbLocation.SelectedIndex < 0 || this.cmbAsset.SelectedIndex < 0)
      {
        this.ClearPanels(0);
      }
      else
      {
        for (int index = 0; index < this.CurrentUnits.Rows.Count; ++index)
        {
          DataRow row = this.CurrentUnits.Rows[index];
          Utils.PopulateAlarms(row);
          if (!(row["CompanyName"].ToString() != this.cmbCompany.SelectedItem.Text) && !(row["UnitCategory"].ToString().ToLower() != this.cmbLocation.SelectedItem.Text.ToLower()))
          {
            List<Sensor> sensorList = (List<Sensor>) row["Sensors"];
            bool flag1 = false;
            if (this.cmbSensor.SelectedIndex == 0)
            {
              flag1 = true;
            }
            else
            {
              foreach (Sensor sensor in sensorList)
              {
                if (sensor.SensorID == int.Parse(this.cmbSensor.SelectedValue))
                  flag1 = true;
              }
            }
            if (flag1)
            {
              foreach (Sensor sensor in sensorList)
              {
                if ((this.cmbSensor.SelectedIndex == 0 || !(sensor.SensorID.ToString() != this.cmbSensor.SelectedValue)) && !(sensor.SensorCategory1 != this.cmbAsset.SelectedItem.Text) && (this.cmbFluidType.SelectedIndex == 0 || !(this.cmbFluidType.SelectedItem.Text != sensor.SensorCategory2)) && sensor.Active)
                {
                  sensor.RefreshLatestReading(Utils.GetConn());
                  Panel fullPanel = sensor.GenerateFullPanel(false);
                  bool flag2 = false;
                  if ((this.cmbDataType.SelectedIndex == 0 || this.cmbDataType.SelectedIndex == 1) && sensor.ParticleCountChannels != null)
                  {
                    DateTime? lastReadingDate = sensor.LastReadingDate;
                    if (lastReadingDate.HasValue)
                    {
                      Channel[] particleCountChannels = sensor.ParticleCountChannels;
                      int sensorId = sensor.SensorID;
                      lastReadingDate = sensor.LastReadingDate;
                      DateTime maxDate = lastReadingDate.Value;
                      Channel altChannel = sensor.AltChannel;
                      Panel particleCountBox = this.GenerateParticleCountBox(particleCountChannels, 1, sensorId, maxDate, altChannel);
                      fullPanel.Controls.Add((Control) particleCountBox);
                      flag2 = true;
                    }
                  }
                  if ((this.cmbDataType.SelectedIndex == 0 || this.cmbDataType.SelectedIndex == 2) && sensor.PhysicalChannels != null && sensor.LastReadingDate.HasValue)
                  {
                    Panel standardBox1 = this.GenerateStandardBox(sensor.PhysicalChannels[0], sensor.SensorID, 2, "Relative Humidity %", ScaleConstants.RH_FACTOR, "0.0");
                    fullPanel.Controls.Add((Control) standardBox1);
                    Panel standardBox2 = this.GenerateStandardBox(sensor.PhysicalChannels[1], sensor.SensorID, 2, "Temperature C°", ScaleConstants.TEMPERATURE_FACTOR, "0.0");
                    fullPanel.Controls.Add((Control) standardBox2);
                    flag2 = true;
                  }
                  if (flag2 || this.chkAllUnits.Checked)
                    this.pnlContent0.Controls.Add((Control) fullPanel);
                }
              }
            }
          }
        }
      }
    }

    protected void UpdateReadingDate(object sender, EventArgs e) => this.UpdateReadings();

    private void UpdateReadings()
    {
      if (this.cmbCompany.SelectedIndex < 0 || this.cmbLocation.SelectedIndex < 0 || this.cmbAsset.SelectedIndex < 0)
      {
        this.ClearPanels(1);
      }
      else
      {
        DateTime? nullable1 = new DateTime?();
        DateTime? nullable2 = new DateTime?();
        DateTime result;
        if (this.txtDate1A.Text != "" && DateTime.TryParse(this.txtDate1A.Text, out result))
          nullable1 = new DateTime?(result);
        if (this.txtDate1B.Text != "" && DateTime.TryParse(this.txtDate1B.Text, out result))
          nullable2 = new DateTime?(result.AddDays(1.0));
        if (this.cmbAsset.SelectedIndex < 0)
        {
          this.ClearPanels(1);
          this.gridReadings_Bind();
        }
        else
        {
          this.CurrentReadings = (DataTable) null;
          List<int> intList1 = new List<int>();
          List<int> intList2 = new List<int>();
          if (this.cmbSensor.SelectedIndex > 0)
          {
            foreach (DataRow row in (InternalDataCollectionBase) this.CurrentUnits.Rows)
            {
              foreach (Sensor sensor in (List<Sensor>) row["Sensors"])
              {
                if (sensor.SensorID.ToString() == this.cmbSensor.SelectedValue)
                {
                  intList1.Add((int) row["UnitID"]);
                  intList2.Add(sensor.SensorID);
                  break;
                }
              }
            }
          }
          else
          {
            string text1 = this.cmbCompany.SelectedItem.Text;
            string text2 = this.cmbLocation.SelectedItem.Text;
            string text3 = this.cmbAsset.SelectedItem.Text;
            for (int index = 0; index < this.CurrentUnits.Rows.Count; ++index)
            {
              if (!(this.CurrentUnits.Rows[index]["CompanyName"].ToString() != text1) && !(this.CurrentUnits.Rows[index]["UnitCategory"].ToString() != text2))
              {
                bool flag = false;
                foreach (Sensor sensor in (List<Sensor>) this.CurrentUnits.Rows[index]["Sensors"])
                {
                  if (!(sensor.SensorCategory1 != this.cmbAsset.SelectedItem.Text) && (this.cmbFluidType.SelectedIndex == 0 || !(sensor.SensorCategory2 != this.cmbFluidType.SelectedItem.Text)))
                  {
                    flag = true;
                    intList2.Add(sensor.SensorID);
                  }
                }
                if (flag)
                  intList1.Add((int) this.CurrentUnits.Rows[index]["UnitID"]);
              }
            }
          }
          bool flag1 = false;
          bool flag2 = false;
          this.CurrentReadings = new DataTable();
          this.CurrentReadings.Columns.Add("id", typeof (long));
          this.CurrentReadings.Columns.Add("UnitID", typeof (int));
          this.CurrentReadings.Columns.Add("UnitName", typeof (string));
          this.CurrentReadings.Columns.Add("SensorID", typeof (int));
          this.CurrentReadings.Columns.Add("SensorName", typeof (string));
          this.CurrentReadings.Columns.Add("Date", typeof (DateTime));
          this.CurrentReadings.Columns.Add("ISO4", typeof (int));
          this.CurrentReadings.Columns.Add("ISO4P", typeof (double));
          this.CurrentReadings.Columns.Add("ISO6", typeof (int));
          this.CurrentReadings.Columns.Add("ISO6P", typeof (double));
          this.CurrentReadings.Columns.Add("ISO14", typeof (int));
          this.CurrentReadings.Columns.Add("ISO14P", typeof (double));
          this.CurrentReadings.Columns.Add("ISO21", typeof (int));
          this.CurrentReadings.Columns.Add("ISO21P", typeof (double));
          this.CurrentReadings.Columns.Add("Alt", typeof (int));
          this.CurrentReadings2 = new DataTable();
          this.CurrentReadings2.Columns.Add("id", typeof (long));
          this.CurrentReadings2.Columns.Add("UnitID", typeof (int));
          this.CurrentReadings2.Columns.Add("UnitName", typeof (string));
          this.CurrentReadings2.Columns.Add("SensorID", typeof (int));
          this.CurrentReadings2.Columns.Add("SensorName", typeof (string));
          this.CurrentReadings2.Columns.Add("Date", typeof (DateTime));
          this.CurrentReadings2.Columns.Add("RH", typeof (double));
          this.CurrentReadings2.Columns.Add("Temp", typeof (double));
          string cmdText = "SELECT d.readingid AS id, u.UnitID, u.UnitName, d.SensorID, s.SensorName, d.Date, d.Alt, d.meta1, d.meta2, d.meta3, d.meta4, d.ISO4, d.ISO6, d.ISO14, d.ISO21, d.RH, d.Temp" + " FROM Data d" + " INNER JOIN Sensor s ON s.SensorID=d.SensorID AND s.SensorID IN (" + string.Join<int>(", ", (IEnumerable<int>) intList2.ToArray()) + ")" + " INNER JOIN Unit u ON s.UnitID=u.UnitID" + " WHERE 1=1";
          if (nullable1.HasValue)
            cmdText = cmdText + " AND d.Date > '" + nullable1.Value.ToString(Utils.MasterDateTimeQueryString) + "'";
          if (nullable2.HasValue)
            cmdText = cmdText + " AND d.Date < '" + nullable2.Value.ToString(Utils.MasterDateTimeQueryString) + "'";
          DataTable dataTable = new DataTable();
          using (SqlConnection conn = Utils.GetConn())
          {
            using (SqlCommand selectCommand = new SqlCommand(cmdText, conn))
            {
              using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
              {
                if (intList2.Count > 0)
                  sqlDataAdapter.Fill(dataTable);
              }
            }
            conn.Close();
          }
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            if (row["ISO4"] != DBNull.Value && row["ISO6"] != DBNull.Value && (row["ISO14"] != DBNull.Value || row["ISO21"] != DBNull.Value) && (this.chkZeroes.Checked || !(row["ISO4"].ToString() == "0") || !(row["ISO6"].ToString() == "0") || !(row["ISO14"].ToString() == "0") || !(row["ISO21"].ToString() == "0")))
            {
              this.CurrentReadings.Rows.Add(row["id"], row["UnitID"], row["UnitName"], row["SensorID"], row["SensorName"], row["Date"], Utils.GetIsoCodeTableObject(row["ISO4"]), Utils.GetReadingTableObject(row["ISO4"]), Utils.GetIsoCodeTableObject(row["ISO6"]), Utils.GetReadingTableObject(row["ISO6"]), Utils.GetIsoCodeTableObject(row["ISO14"]), Utils.GetReadingTableObject(row["ISO14"]), Utils.GetIsoCodeTableObject(row["ISO21"]), Utils.GetReadingTableObject(row["ISO21"]), (object) Utils.ConvertFlowRate(row["Alt"]));
              flag1 = true;
            }
          }
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            if (row["RH"] != DBNull.Value && row["Temp"] != DBNull.Value)
            {
              this.CurrentReadings2.Rows.Add(row["id"], row["UnitID"], row["UnitName"], row["SensorID"], row["SensorName"], row["Date"], Utils.GetReadingTableRHObject(row["RH"]), Utils.GetReadingTableTempObject(row["Temp"]));
              flag2 = true;
            }
          }
          if (!flag1)
            this.CurrentReadings = (DataTable) null;
          if (!flag2)
            this.CurrentReadings2 = (DataTable) null;
          if (this.cmbDataType.SelectedIndex == 0 || this.cmbDataType.SelectedIndex == 1)
            this.gridReadings.DataSource = (object) this.CurrentReadings;
          else
            this.gridReadings.DataSource = (object) null;
          this.gridReadings_Bind();
          if (this.cmbDataType.SelectedIndex == 0 || this.cmbDataType.SelectedIndex == 2)
            this.gridReadings2.DataSource = (object) this.CurrentReadings2;
          else
            this.gridReadings2.DataSource = (object) null;
          this.gridReadings2_Bind();
        }
      }
    }

    protected void gridReadings_DataBound(object sender, EventArgs e)
    {
      GridViewRow child1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
      TableHeaderCell child2 = new TableHeaderCell();
      child2.Text = "";
      child2.ColumnSpan = 3;
      child2.CssClass = "tblReadingsHeaderInvisible";
      child1.Controls.Add((Control) child2);
      TableHeaderCell child3 = new TableHeaderCell();
      child3.Text = "ISO-4";
      child3.ColumnSpan = 2;
      child1.Controls.Add((Control) child3);
      TableHeaderCell child4 = new TableHeaderCell();
      child4.Text = "ISO-6";
      child4.ColumnSpan = 2;
      child1.Controls.Add((Control) child4);
      TableHeaderCell child5 = new TableHeaderCell();
      child5.Text = "ISO-14";
      child5.ColumnSpan = 2;
      child1.Controls.Add((Control) child5);
      TableHeaderCell child6 = new TableHeaderCell();
      child6.Text = "ISO-21";
      child6.ColumnSpan = 2;
      child1.Controls.Add((Control) child6);
      TableHeaderCell child7 = new TableHeaderCell();
      child7.Text = "";
      child7.CssClass = "tblReadingsHeaderInvisible";
      child1.Controls.Add((Control) child7);
      if (this.gridReadings.Controls.Count <= 0)
        return;
      this.gridReadings.Controls[0].Controls.AddAt(0, (Control) child1);
    }

    protected void gridReadings2_DataBound(object sender, EventArgs e)
    {
    }

    protected void btnReadingsExcel_Click(object sender, EventArgs e)
    {
      int currentUserCompanyId = Utils.GetCurrentUserCompanyId(Membership.GetUser());
      if (currentUserCompanyId == -1)
      {
        this.Response.Redirect("~/Defualt.aspx", false);
      }
      else
      {
        string templateServerPath1 = TemplatePaths.GetExistingTemplateServerPath(Templates.Reading, Utils.GetCompanyList()[currentUserCompanyId], this.Server);
        string templateServerPath2 = TemplatePaths.GetExistingTemplateServerPath(Templates.Reading, (string) null, this.Server);
        try
        {
          if (!string.IsNullOrEmpty(templateServerPath1))
            this.GenerateReadingsReportExcelFromTemplate(templateServerPath1);
          else if (!string.IsNullOrEmpty(templateServerPath2))
            this.GenerateReadingsReportExcelFromTemplate(templateServerPath2);
          else
            this.GenerateReadingsReportExcelNoTemplate();
        }
        catch (ArgumentException ex)
        {
          throw ex;
        }
      }
    }

    private void GenerateReadingsReportExcelFromTemplate(string path)
    {
      IWorkbook workbook;
      using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
      {
        switch (Path.GetExtension(fileStream.Name))
        {
          case ".xls":
            workbook = (IWorkbook) new HSSFWorkbook((Stream) fileStream);
            break;
          case ".xlsx":
            workbook = (IWorkbook) new XSSFWorkbook((Stream) fileStream);
            break;
          default:
            throw new ArgumentException("Report template not in xls or xlsx file format");
        }
      }
      ISheet sheet = workbook.GetSheet("Table");
      if (sheet == null)
        throw new ArgumentException("Cannot find a sheet named 'Table' in the excel file");
      IDataFormat dataFormat = workbook.CreateDataFormat();
      IRow row1 = (IRow) null;
      for (int rownum = 0; rownum < 15; ++rownum)
      {
        IRow row2 = sheet.GetRow(rownum);
        if (row2 != null && row2.RowNum == 13)
        {
          row1 = row2;
          break;
        }
      }
      if (row1 != null)
        sheet.RemoveRow(row1);
      sheet.ShiftRows(14, 20, -1);
      int selectedIndex = this.cmbSensor.SelectedIndex;
      List<string> stringList1 = new List<string>();
      List<string> stringList2 = new List<string>();
      List<string> stringList3 = new List<string>();
      List<string> stringList4 = new List<string>();
      if (this.cmbSensor.SelectedIndex > 0)
      {
        foreach (DataRow row3 in (InternalDataCollectionBase) this.CurrentUnits.Rows)
        {
          foreach (Sensor sensor in (List<Sensor>) row3["Sensors"])
          {
            if (sensor.SensorID.ToString() == this.cmbSensor.SelectedValue)
            {
              stringList1.Add((string) row3["CompanyName"]);
              stringList2.Add((string) row3["UnitCategory"]);
              stringList3.Add(sensor.SensorCategory1);
              stringList4.Add(sensor.SensorCategory2);
            }
          }
        }
      }
      else
      {
        string text1 = this.cmbCompany.SelectedItem.Text;
        string text2 = this.cmbLocation.SelectedItem.Text;
        string text3 = this.cmbAsset.SelectedItem.Text;
        for (int index = 0; index < this.CurrentUnits.Rows.Count; ++index)
        {
          if (!(this.CurrentUnits.Rows[index]["CompanyName"].ToString() != text1) && !(this.CurrentUnits.Rows[index]["UnitCategory"].ToString() != text2))
          {
            foreach (Sensor sensor in (List<Sensor>) this.CurrentUnits.Rows[index]["Sensors"])
            {
              if (!(sensor.SensorCategory1 != this.cmbAsset.SelectedItem.Text) && (this.cmbFluidType.SelectedIndex == 0 || !(sensor.SensorCategory2 != this.cmbFluidType.SelectedItem.Text)))
              {
                if (!stringList1.Contains((string) this.CurrentUnits.Rows[index]["CompanyName"]))
                  stringList1.Add((string) this.CurrentUnits.Rows[index]["CompanyName"]);
                if (!stringList2.Contains((string) this.CurrentUnits.Rows[index]["UnitCategory"]))
                  stringList2.Add((string) this.CurrentUnits.Rows[index]["UnitCategory"]);
                if (!stringList3.Contains(sensor.SensorCategory1))
                  stringList3.Add(sensor.SensorCategory1);
                if (!stringList4.Contains(sensor.SensorCategory2))
                  stringList4.Add(sensor.SensorCategory2);
              }
            }
          }
        }
      }
      Dictionary<string, object> dictionary1 = new Dictionary<string, object>();
      dictionary1.Add("{ReportDate}", (object) DateTime.Now);
      dictionary1.Add("{ReportTime}", (object) DateTime.Now);
      dictionary1.Add("{ReportDateTime}", (object) DateTime.Now);
      dictionary1.Add("{Company}", (object) string.Join(", ", stringList1.ToArray()));
      dictionary1.Add("{Location}", (object) string.Join(", ", stringList2.ToArray()));
      dictionary1.Add("{Asset}", (object) string.Join(", ", stringList3.ToArray()));
      dictionary1.Add("{Fluid}", (object) string.Join(", ", stringList4.ToArray()));
      dictionary1.Add("{Username}", (object) Membership.GetUser().UserName);
      for (int rownum = 0; rownum < 20; ++rownum)
      {
        if (rownum <= sheet.LastRowNum)
        {
          IRow row4 = sheet.GetRow(rownum);
          if (row4 != null)
          {
            for (int index = 0; index < 12; ++index)
            {
              ICell cell = row4.GetCell(index) ?? row4.CreateCell(index);
              if (cell != null && cell.CellType == CellType.String && cell.StringCellValue.Contains("{"))
              {
                string stringCellValue = row4.Cells[index].StringCellValue;
                if (dictionary1.ContainsKey(stringCellValue))
                {
                  object v = dictionary1[stringCellValue];
                  if (v != null)
                  {
                    string dateformat = "";
                    if (stringCellValue == "{ReportDate}")
                      dateformat = "yyyy-MM-dd";
                    if (stringCellValue == "{ReportTime}")
                      dateformat = "HH:mm:ss";
                    if (stringCellValue == "{ReportDateTime}")
                      dateformat = "yyyy-MM-dd HH:mm:ss";
                    this.SetCellValue(cell, v, dataFormat, dateformat);
                  }
                }
              }
            }
          }
        }
      }
      int num1 = 12;
      IRow row5 = sheet.GetRow(num1 - 1);
      IRow row6 = sheet.GetRow(num1);
      for (int index = 0; index < 12; ++index)
        row6.Cells[index].SetCellValue(row5.Cells[index].StringCellValue);
      int num2 = num1 + 1;
      bool flag = false;
      foreach (GridViewRow row7 in this.gridReadings.Rows)
      {
        if (row7.RowType == DataControlRowType.DataRow)
        {
          if (flag)
            sheet.CopyRow(num1, num2);
          else
            sheet.CopyRow(num1 - 1, num2);
          flag = !flag;
          IRow row8 = sheet.GetRow(num2);
          Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
          dictionary2.Add("{SensorName}", (object) row7.Cells[0].Text);
          dictionary2.Add("{ReadingDate}", (object) (DateTime) this.gridReadings.DataKeys[row7.RowIndex]["Date"]);
          dictionary2.Add("{ReadingTime}", (object) (DateTime) this.gridReadings.DataKeys[row7.RowIndex]["Date"]);
          dictionary2.Add("{ReadingDateTime}", (object) (DateTime) this.gridReadings.DataKeys[row7.RowIndex]["Date"]);
          dictionary2.Add("{ISO4Code}", this.DictionaryHelper(row7.Cells[3].Text));
          dictionary2.Add("{ISO4Count}", this.DictionaryHelper(row7.Cells[4].Text));
          dictionary2.Add("{ISO6Code}", this.DictionaryHelper(row7.Cells[5].Text));
          dictionary2.Add("{ISO6Count}", this.DictionaryHelper(row7.Cells[6].Text));
          dictionary2.Add("{ISO14Code}", this.DictionaryHelper(row7.Cells[7].Text));
          dictionary2.Add("{ISO14Count}", this.DictionaryHelper(row7.Cells[8].Text));
          dictionary2.Add("{ISO21Code}", this.DictionaryHelper(row7.Cells[9].Text));
          dictionary2.Add("{ISO21Count}", this.DictionaryHelper(row7.Cells[10].Text));
          dictionary2.Add("{FlowRate}", this.DictionaryHelper(row7.Cells[11].Text));
          for (int index = 0; index < 12; ++index)
          {
            if (row8.GetCell(index) == null)
              row8.CreateCell(index);
            string stringCellValue = row8.Cells[index].StringCellValue;
            if (dictionary2.ContainsKey(stringCellValue) && dictionary2.ContainsKey(stringCellValue))
            {
              object v = dictionary2[stringCellValue];
              string dateformat = "";
              if (row8.Cells[index].StringCellValue == "{ReadingDate}")
                dateformat = "yyyy-MM-dd";
              if (row8.Cells[index].StringCellValue == "{ReadingTime}")
                dateformat = "HH:mm:ss";
              if (row8.Cells[index].StringCellValue == "{ReadingDateTime")
                dateformat = "yyyy-MM-dd HH:mm:ss";
              this.SetCellValue(row8.Cells[index], v, dataFormat, dateformat);
            }
          }
          ++num2;
        }
      }
      sheet.ShiftRows(num1, num2 + 8, -1);
      sheet.ShiftRows(num1, num2 + 8, -1);
      string str = this.Server.MapPath("~/Excel/CurrentExcelFile" + (workbook.GetType() == typeof (HSSFWorkbook) ? ".xls" : ".xlsx"));
      MemoryStream memoryStream = new MemoryStream();
      workbook.Write((Stream) memoryStream);
      using (FileStream fileStream = new FileStream(str, FileMode.Create))
        fileStream.Write(memoryStream.ToArray(), 0, (int) memoryStream.Length);
      FileInfo file = new FileInfo(str);
      this.TriggerDownload("FluidComm_ReadingReport_" + DateTime.Now.ToString("s", (IFormatProvider) DateTimeFormatInfo.InvariantInfo) + file.Extension, str, file);
    }

    private object DictionaryHelper(string text)
    {
      double result;
      return double.TryParse(text, out result) ? (object) result : (object) null;
    }

    private void GenerateReadingsReportExcelNoTemplate()
    {
      IWorkbook workbook = (IWorkbook) new HSSFWorkbook();
      ISheet sheet = workbook.CreateSheet("Test");
      IFont font = workbook.CreateFont();
      font.Boldweight = (short) 700;
      IDataFormat dataFormat = workbook.CreateDataFormat();
      ICellStyle cellStyle1 = workbook.CreateCellStyle();
      cellStyle1.Alignment = HorizontalAlignment.Center;
      cellStyle1.SetFont(font);
      ICellStyle cellStyle2 = workbook.CreateCellStyle();
      ICellStyle cellStyle3 = workbook.CreateCellStyle();
      cellStyle3.Alignment = HorizontalAlignment.Center;
      ICellStyle cellStyle4 = workbook.CreateCellStyle();
      cellStyle4.Alignment = HorizontalAlignment.Center;
      cellStyle4.DataFormat = dataFormat.GetFormat("yyyy-MM-dd HH:mm:ss");
      IRow row1 = sheet.CreateRow(0);
      row1.CreateCell(0).CellStyle = cellStyle1;
      row1.CreateCell(1).CellStyle = cellStyle1;
      ICell cell1 = row1.CreateCell(2);
      cell1.CellStyle = cellStyle1;
      row1.CreateCell(3);
      ICell cell2 = row1.CreateCell(4);
      cell2.CellStyle = cellStyle1;
      row1.CreateCell(5);
      ICell cell3 = row1.CreateCell(6);
      cell3.CellStyle = cellStyle1;
      row1.CreateCell(7);
      ICell cell4 = row1.CreateCell(8);
      cell4.CellStyle = cellStyle1;
      row1.CreateCell(9).CellStyle = cellStyle1;
      ICell cell5 = row1.CreateCell(10);
      cell1.SetCellValue("ISO-4");
      cell2.SetCellValue("ISO-6");
      cell3.SetCellValue("ISO-14");
      cell4.SetCellValue("ISO-21");
      cell5.SetCellValue("Flow Rate (ml/min");
      CellRangeAddress region1 = new CellRangeAddress(0, 0, 2, 3);
      CellRangeAddress region2 = new CellRangeAddress(0, 0, 4, 5);
      CellRangeAddress region3 = new CellRangeAddress(0, 0, 6, 7);
      CellRangeAddress region4 = new CellRangeAddress(0, 0, 8, 9);
      sheet.AddMergedRegion(region1);
      sheet.AddMergedRegion(region2);
      sheet.AddMergedRegion(region3);
      sheet.AddMergedRegion(region4);
      IRow row2 = sheet.CreateRow(1);
      row2.CreateCell(0).SetCellValue("Name");
      row2.CreateCell(1).SetCellValue("Date");
      row2.CreateCell(2).SetCellValue("ISO Code");
      row2.CreateCell(3).SetCellValue("ct/ml");
      row2.CreateCell(4).SetCellValue("ISO Code");
      row2.CreateCell(5).SetCellValue("ct/ml");
      row2.CreateCell(6).SetCellValue("ISO Code");
      row2.CreateCell(7).SetCellValue("ct/ml");
      row2.CreateCell(8).SetCellValue("ISO Code");
      row2.CreateCell(9).SetCellValue("ct/ml");
      row2.CreateCell(10);
      CellRangeAddress region5 = new CellRangeAddress(0, 1, 10, 10);
      sheet.AddMergedRegion(region5);
      for (int index1 = 0; index1 < this.gridReadings.Rows.Count; ++index1)
      {
        GridViewRow row3 = this.gridReadings.Rows[index1];
        IRow row4 = sheet.CreateRow(index1 + 2);
        for (int index2 = 0; index2 < row3.Cells.Count; ++index2)
        {
          ICell cell6 = row4.CreateCell(index2);
          switch (index2)
          {
            case 0:
              cell6.SetCellValue(row3.Cells[0].Text);
              break;
            case 1:
              IEnumerator enumerator = row3.Cells[index2].Controls.GetEnumerator();
              try
              {
                while (enumerator.MoveNext())
                {
                  Control current = (Control) enumerator.Current;
                  if (current is Label)
                  {
                    DateTime result;
                    if (DateTime.TryParse(((Label) current).Text, out result))
                      cell6.SetCellValue(result);
                    else
                      cell6.SetCellValue(((Label) current).Text);
                  }
                }
                break;
              }
              finally
              {
                if (enumerator is IDisposable disposable)
                  disposable.Dispose();
              }
            default:
              double result1;
              if (double.TryParse(row3.Cells[index2].Text, out result1))
              {
                cell6.SetCellValue(result1);
                break;
              }
              cell6.SetCellValue(row3.Cells[index2].Text);
              break;
          }
          switch (index2)
          {
            case 0:
              cell6.CellStyle = cellStyle2;
              break;
            case 1:
              cell6.CellStyle = cellStyle4;
              break;
            default:
              cell6.CellStyle = cellStyle3;
              break;
          }
        }
      }
      string str = this.Server.MapPath("~/Excel/CurrentExcelFile.xls");
      MemoryStream memoryStream = new MemoryStream();
      workbook.Write((Stream) memoryStream);
      using (FileStream fileStream = new FileStream(str, FileMode.Create))
        fileStream.Write(memoryStream.ToArray(), 0, (int) memoryStream.Length);
      FileInfo file = new FileInfo(str);
      "FluidTraxReport_" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString();
      this.TriggerDownload("CurrentExcelFile.xls", str, file);
    }

    private void SetCellValue(
      ICell cell,
      object v,
      IDataFormat dataFormatCustom,
      string dateformat)
    {
      switch (v)
      {
        case DateTime dateTime:
          cell.SetCellValue(dateTime);
          cell.CellStyle.DataFormat = dataFormatCustom.GetFormat(dateformat);
          break;
        case int num1:
          cell.SetCellValue((double) num1);
          break;
        case double num2:
          cell.SetCellValue(num2);
          break;
        case string _:
          cell.SetCellValue((string) v);
          break;
      }
    }

    private void TriggerDownload(string fileName, string filepath, FileInfo file)
    {
      if (file.Exists)
      {
        this.Response.Clear();
        this.Response.ClearHeaders();
        this.Response.ClearContent();
        this.Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
        this.Response.AddHeader("Content-Type", "application/Excel");
        this.Response.ContentType = "application/vnd.xls";
        this.Response.AddHeader("Content-Length", file.Length.ToString());
        this.Response.TransmitFile(filepath);
        this.Response.End();
      }
      else
        this.Response.Write("This file does not exist");
    }

    protected void gridReadings_Bind()
    {
      if (this.CurrentReadings == null)
      {
        this.gridReadings.DataSource = (object) null;
        this.gridReadings.DataBind();
      }
      else
      {
        DataView dataView = new DataView(this.CurrentReadings);
        dataView.Sort = this.sortexpr;
        string str = "";
        if (this.cmbSensor.SelectedIndex > 0)
          str = "SensorID=" + this.cmbSensor.SelectedValue;
        dataView.RowFilter = str;
        this.gridReadings.DataSource = (object) dataView;
        this.gridReadings.DataBind();
      }
    }

    protected void gridReadings2_Bind()
    {
      if (this.CurrentReadings2 == null)
      {
        this.gridReadings2.DataSource = (object) null;
        this.gridReadings2.DataBind();
      }
      else
      {
        DataView dataView = new DataView(this.CurrentReadings2);
        dataView.Sort = this.sortexpr2;
        string str = "";
        if (this.cmbSensor.SelectedIndex > 0)
          str = "SensorID=" + this.cmbSensor.SelectedValue;
        dataView.RowFilter = str;
        this.gridReadings2.DataSource = (object) dataView;
        this.gridReadings2.DataBind();
      }
    }

    protected void gridReadings_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType != DataControlRowType.DataRow)
        return;
      string str = "cellHighlight";
      if (this.altrow)
        str = "cellHighlight_alt";
      e.Row.Cells[3].CssClass = str;
      e.Row.Cells[5].CssClass = str;
      e.Row.Cells[7].CssClass = str;
      e.Row.Cells[9].CssClass = str;
      this.altrow = !this.altrow;
    }

    protected void gridReadings2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      int rowType = (int) e.Row.RowType;
    }

    protected void gridReadings_Sorting(object sender, GridViewSortEventArgs e)
    {
      string str = "ASC";
      if (this.sortexpr != "")
      {
        if (this.sortexpr.Split(' ')[0] == e.SortExpression)
        {
          if (this.sortexpr.Split(' ')[1] == "ASC")
            str = "DESC";
        }
      }
      this.sortexpr = e.SortExpression + " " + str;
      this.gridReadings_Bind();
    }

    protected void gridReadings2_Sorting(object sender, GridViewSortEventArgs e)
    {
      string str = "ASC";
      if (this.sortexpr2 != "")
      {
        if (this.sortexpr2.Split(' ')[0] == e.SortExpression)
        {
          if (this.sortexpr.Split(' ')[1] == "ASC")
            str = "DESC";
        }
      }
      this.sortexpr2 = e.SortExpression + " " + str;
      this.gridReadings2_Bind();
    }

    protected void UpdateReportDate(object sender, EventArgs e) => this.UpdateReport();

    protected void chkISO_CheckedChanged(object sender, EventArgs e) => this.UpdateReport();

    private void UpdateReport()
    {
      if (this.cmbCompany.SelectedIndex < 0 || this.cmbLocation.SelectedIndex < 0 || this.cmbAsset.SelectedIndex < 0)
      {
        this.ClearPanels(1);
      }
      else
      {
        DateTime? nullable1 = new DateTime?();
        DateTime? nullable2 = new DateTime?();
        DataTable dataTable1 = new DataTable();
        bool flag1 = false;
        bool flag2 = false;
        DateTime result;
        if (this.txtDate2A.Text != "" && DateTime.TryParse(this.txtDate2A.Text, out result))
          nullable1 = new DateTime?(result);
        if (this.txtDate2B.Text != "" && DateTime.TryParse(this.txtDate2B.Text, out result))
          nullable2 = new DateTime?(result.AddDays(1.0));
        DataTable dataTable2 = new DataTable();
        dataTable2.Columns.Add("Date", typeof (DateTime));
        dataTable2.Columns.Add("RH", typeof (double));
        dataTable2.Columns.Add("Temp", typeof (double));
        if (this.cmbAsset.SelectedIndex < 0)
        {
          this.ClearPanels(1);
        }
        else
        {
          List<int> intList = new List<int>();
          if (this.cmbSensor.SelectedIndex > 0)
          {
            int num = int.Parse(this.cmbSensor.SelectedValue);
            foreach (DataRow row in (InternalDataCollectionBase) this.CurrentUnits.Rows)
            {
              foreach (Sensor sensor in (List<Sensor>) row["Sensors"])
              {
                if (sensor.SensorID == num)
                  intList.Add(sensor.SensorID);
              }
            }
          }
          else
          {
            string text1 = this.cmbCompany.SelectedItem.Text;
            string text2 = this.cmbLocation.SelectedItem.Text;
            string text3 = this.cmbAsset.SelectedItem.Text;
            for (int index = 0; index < this.CurrentUnits.Rows.Count; ++index)
            {
              if (!(this.CurrentUnits.Rows[index]["CompanyName"].ToString() != text1) && !(this.CurrentUnits.Rows[index]["UnitCategory"].ToString() != text2) && Utils.UnitHasAsset(this.CurrentUnits.Rows[index], text3))
              {
                foreach (Sensor sensor in (List<Sensor>) this.CurrentUnits.Rows[index]["Sensors"])
                {
                  DateTime? lastReadingDate = sensor.LastReadingDate;
                  DateTime? nullable3 = nullable1;
                  if ((lastReadingDate.HasValue & nullable3.HasValue ? (lastReadingDate.GetValueOrDefault() < nullable3.GetValueOrDefault() ? 1 : 0) : 0) == 0)
                    intList.Add(sensor.SensorID);
                }
              }
            }
          }
          string str1 = "AVG";
          if (this.cmbReportMode.SelectedIndex == 1)
            str1 = "MIN";
          if (this.cmbReportMode.SelectedIndex == 2)
            str1 = "MAX";
          string str2 = "SELECT " + str1 + "(d.ISO4) AS ISO4, " + str1 + "(d.ISO6) AS ISO6," + " " + str1 + "(d.ISO14) AS ISO14, " + str1 + "(d.ISO21) AS ISO21, " + str1 + "(d.RH) AS RH, " + str1 + "(d.Temp) AS Temp, " + " CONVERT(date, d.date) AS Date" + " FROM Data d" + " INNER JOIN Sensor s ON d.sensorID=s.SensorID AND s.SensorID IN (" + string.Join<int>(", ", (IEnumerable<int>) intList.ToArray()) + ")" + " INNER JOIN Unit u ON u.unitID=s.UnitID" + " WHERE 1=1";
          if (nullable1.HasValue)
            str2 = str2 + " AND d.Date > '" + nullable1.Value.ToString(Utils.MasterDateTimeQueryString) + "'";
          if (nullable2.HasValue)
            str2 = str2 + " AND d.Date < '" + nullable2.Value.ToString(Utils.MasterDateTimeQueryString) + "'";
          string cmdText = str2 + " GROUP BY CONVERT(date, d.date)";
          this.chrtReport.PlotArea.Series.Clear();
          this.chrtReport2.PlotArea.Series.Clear();
          int selectedIndex = this.cmbReportMode.SelectedIndex;
          List<string> stringList = new List<string>();
          using (SqlConnection conn = Utils.GetConn())
          {
            using (SqlCommand selectCommand = new SqlCommand(cmdText, conn))
            {
              using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
              {
                if (intList.Count > 0)
                  sqlDataAdapter.Fill(dataTable1);
              }
            }
            conn.Close();
          }
          if (dataTable1 == null || dataTable1.Rows.Count < 1)
            return;
          foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
          {
            if (row["RH"] != DBNull.Value && row["Temp"] != DBNull.Value)
              dataTable2.Rows.Add(row["Date"], Utils.GetReadingTableRHObject(row["RH"]), Utils.GetReadingTableTempObject(row["Temp"]));
          }
          for (int index = 0; index < dataTable1.Columns.Count; ++index)
            stringList.Add(dataTable1.Columns[index].ColumnName);
          foreach (string columnName in stringList)
          {
            if (columnName.Contains("ISO"))
            {
              foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
                row[columnName] = Utils.GetIsoCodeTableObject(row[columnName]);
              flag1 = true;
            }
            else
            {
              switch (columnName)
              {
                case "RH":
                  flag2 = true;
                  continue;
                case "Temp":
                  flag2 = true;
                  continue;
                default:
                  continue;
              }
            }
          }
          dataTable1.DefaultView.Sort = "Date ASC";
          DataTable table = dataTable1.DefaultView.ToTable();
          if (((this.cmbDataType.SelectedIndex == 0 ? 1 : (this.cmbDataType.SelectedIndex == 1 ? 1 : 0)) & (flag1 ? 1 : 0)) != 0)
          {
            if (!table.Columns.Contains("ISO4"))
              table.Columns.Add("ISO4");
            if (!table.Columns.Contains("ISO6"))
              table.Columns.Add("ISO6");
            if (!table.Columns.Contains("ISO14"))
              table.Columns.Add("ISO14");
            if (!table.Columns.Contains("ISO21"))
              table.Columns.Add("ISO21");
            this.pnlReportContainer1.Visible = true;
            ScatterLineSeries[] scatterLineSeriesArray = new ScatterLineSeries[4];
            Color[] colorArray = new Color[4]
            {
              Color.FromArgb((int) byte.MaxValue, 240, 179, 15),
              Color.FromArgb((int) byte.MaxValue, 75, 76, 83),
              Color.FromArgb((int) byte.MaxValue, 132, 135, 147),
              Color.FromArgb((int) byte.MaxValue, 204, 192, 192)
            };
            for (int index = 0; index < 4; ++index)
            {
              scatterLineSeriesArray[index] = new ScatterLineSeries();
              scatterLineSeriesArray[index].DataFieldX = "Date";
              scatterLineSeriesArray[index].DataFieldY = Utils.IsoLabels[index].Replace("-", "");
              scatterLineSeriesArray[index].Name = Utils.IsoLabels[index];
              scatterLineSeriesArray[index].LabelsAppearance.Visible = new bool?(false);
              scatterLineSeriesArray[index].MarkersAppearance.MarkersType = MarkersType.Square;
              scatterLineSeriesArray[index].MarkersAppearance.Size = new Decimal?(12.0M);
              scatterLineSeriesArray[index].MarkersAppearance.BackgroundColor = colorArray[index];
              scatterLineSeriesArray[index].MarkersAppearance.BorderColor = colorArray[index];
              scatterLineSeriesArray[index].Appearance.FillStyle.BackgroundColor = colorArray[index];
              scatterLineSeriesArray[index].LineAppearance.Width = new Unit("4px");
              scatterLineSeriesArray[index].TooltipsAppearance.DataFormatString = "ISO Code: {1} Date: {0:d}";
            }
            if (this.pnlChartOption0.CssClass != "pnlChartOptionDisabled")
              this.chrtReport.PlotArea.Series.Add((SeriesBase) scatterLineSeriesArray[0]);
            if (this.pnlChartOption1.CssClass != "pnlChartOptionDisabled")
              this.chrtReport.PlotArea.Series.Add((SeriesBase) scatterLineSeriesArray[1]);
            if (this.pnlChartOption2.CssClass != "pnlChartOptionDisabled")
              this.chrtReport.PlotArea.Series.Add((SeriesBase) scatterLineSeriesArray[2]);
            if (this.pnlChartOption3.CssClass != "pnlChartOptionDisabled")
              this.chrtReport.PlotArea.Series.Add((SeriesBase) scatterLineSeriesArray[3]);
            string str3 = "Daily Maximum ISO Reading";
            if (this.cmbReportMode.SelectedIndex == 0)
              str3 = "Daily Average ISO Reading";
            if (this.cmbReportMode.SelectedIndex == 1)
              str3 = "Daily Minimum ISO Reading";
            if (this.cmbReportMode.SelectedIndex == 2)
              str3 = "Daily Maximum ISO Reading";
            this.chrtReport.ChartTitle.Text = str3;
            this.chrtReport.PlotArea.XAxis.TitleAppearance.Text = "Date";
            this.chrtReport.PlotArea.XAxis.LabelsAppearance.DataFormatString = "d";
            this.chrtReport.PlotArea.YAxis.TitleAppearance.Text = "Particle Count ISO Code";
            this.chrtReport.DataSource = (object) new DataSet()
            {
              Tables = {
                table
              }
            };
            this.chrtReport.DataBind();
            table.DefaultView.Sort = "Date DESC";
            table = table.DefaultView.ToTable();
            this.gridReport.DataSource = (object) table;
            this.gridReport.DataBind();
          }
          else
            this.pnlReportContainer1.Visible = false;
          if (((this.cmbDataType.SelectedIndex == 0 ? 1 : (this.cmbDataType.SelectedIndex == 2 ? 1 : 0)) & (flag2 ? 1 : 0)) != 0)
          {
            if (!table.Columns.Contains("RH"))
              table.Columns.Add("RH");
            if (!table.Columns.Contains("Temp"))
              table.Columns.Add("Temp");
            this.pnlReportContainer2.Visible = true;
            ScatterLineSeries[] scatterLineSeriesArray = new ScatterLineSeries[2];
            Color[] colorArray = new Color[2]
            {
              Color.FromArgb((int) byte.MaxValue, 240, 179, 15),
              Color.FromArgb((int) byte.MaxValue, 75, 76, 83)
            };
            for (int index = 0; index < 2; ++index)
            {
              scatterLineSeriesArray[index] = new ScatterLineSeries();
              scatterLineSeriesArray[index].DataFieldX = "Date";
              scatterLineSeriesArray[index].DataFieldY = Utils.IsoLabels[index + 4].Replace("-", "");
              scatterLineSeriesArray[index].Name = Utils.IsoLabels[index + 4];
              scatterLineSeriesArray[index].LabelsAppearance.Visible = new bool?(false);
              scatterLineSeriesArray[index].MarkersAppearance.MarkersType = MarkersType.Square;
              scatterLineSeriesArray[index].MarkersAppearance.Size = new Decimal?(12.0M);
              scatterLineSeriesArray[index].MarkersAppearance.BackgroundColor = colorArray[index];
              scatterLineSeriesArray[index].MarkersAppearance.BorderColor = colorArray[index];
              scatterLineSeriesArray[index].Appearance.FillStyle.BackgroundColor = colorArray[index];
              scatterLineSeriesArray[index].LineAppearance.Width = new Unit("4px");
              if (index == 4)
                scatterLineSeriesArray[index].TooltipsAppearance.DataFormatString = "Relative Humidity: {1}% Date: {0:d}";
              else if (index == 5)
                scatterLineSeriesArray[index].TooltipsAppearance.DataFormatString = "Temperature: {1}°C Date: {0:d}";
            }
            if (this.pnlChartOption6.CssClass != "pnlChartOptionDisabled")
              this.chrtReport2.PlotArea.Series.Add((SeriesBase) scatterLineSeriesArray[0]);
            if (this.pnlChartOption7.CssClass != "pnlChartOptionDisabled")
              this.chrtReport2.PlotArea.Series.Add((SeriesBase) scatterLineSeriesArray[1]);
            string str4 = "Daily Maximum Readings";
            if (this.cmbReportMode.SelectedIndex == 0)
              str4 = "Daily Average Readings";
            if (this.cmbReportMode.SelectedIndex == 1)
              str4 = "Daily Minimum Readings";
            if (this.cmbReportMode.SelectedIndex == 2)
              str4 = "Daily Maximum Readings";
            this.chrtReport2.ChartTitle.Text = str4;
            this.chrtReport2.PlotArea.XAxis.TitleAppearance.Text = "Date";
            this.chrtReport2.PlotArea.XAxis.LabelsAppearance.DataFormatString = "d";
            this.chrtReport2.PlotArea.YAxis.TitleAppearance.Text = "Reading (°C or %RH)";
            this.chrtReport2.DataSource = (object) new DataSet()
            {
              Tables = {
                dataTable2
              }
            };
            this.chrtReport2.DataBind();
            dataTable2.DefaultView.Sort = "Date DESC";
            this.gridReport2.DataSource = (object) dataTable2.DefaultView.ToTable();
            this.gridReport2.DataBind();
          }
          else
            this.pnlReportContainer2.Visible = false;
        }
      }
    }

    private void AddScatterLineSeries(
      RadHtmlChart chart,
      string DataFieldX,
      string DataFieldY,
      string name)
    {
      ScatterLineSeries series = new ScatterLineSeries();
      series.DataFieldX = DataFieldX;
      series.DataFieldY = DataFieldY;
      series.Name = name;
      series.LabelsAppearance.Visible = new bool?(false);
      chart.PlotArea.Series.Add((SeriesBase) series);
    }

    private void CheckTable(List<int> allowedUnits, DataTable requestedUnits)
    {
      List<DataRow> dataRowList = new List<DataRow>();
      foreach (DataRow row in (InternalDataCollectionBase) requestedUnits.Rows)
      {
        int result;
        if (!int.TryParse(row[0].ToString(), out result) || !allowedUnits.Contains(result))
          dataRowList.Add(row);
      }
      foreach (DataRow row in dataRowList)
        requestedUnits.Rows.Remove(row);
    }

    protected void btnSummaryExcel_Click(object sender, EventArgs e)
    {
      int currentUserCompanyId = Utils.GetCurrentUserCompanyId(Membership.GetUser());
      if (currentUserCompanyId == -1)
      {
        this.Response.Redirect("~/Defualt.aspx", false);
      }
      else
      {
        string templateServerPath1 = TemplatePaths.GetExistingTemplateServerPath(Templates.ReadingSummary, Utils.GetCompanyList()[currentUserCompanyId], this.Server);
        string templateServerPath2 = TemplatePaths.GetExistingTemplateServerPath(Templates.ReadingSummary, (string) null, this.Server);
        if (!string.IsNullOrEmpty(templateServerPath1))
          this.GenerateSummaryReportExcelFromTemplate(templateServerPath1);
        else if (!string.IsNullOrEmpty(templateServerPath2))
          this.GenerateSummaryReportExcelFromTemplate(templateServerPath2);
        else
          this.GenerateSummaryReportExcelNoTemplate();
      }
    }

    private void GenerateSummaryReportExcelFromTemplate(string path)
    {
      IWorkbook workbook;
      using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
      {
        switch (Path.GetExtension(fileStream.Name))
        {
          case ".xls":
            workbook = (IWorkbook) new HSSFWorkbook((Stream) fileStream);
            break;
          case ".xlsx":
            workbook = (IWorkbook) new XSSFWorkbook((Stream) fileStream);
            break;
          default:
            throw new ArgumentException("Summary Report Template not in xls or xlsx format");
        }
      }
      ISheet sheet = workbook.GetSheet("Table");
      if (sheet == null)
        throw new ArgumentException("Cannot find a sheet name 'Table' in the excel file");
      IDataFormat dataFormat = workbook.CreateDataFormat();
      IRow row1 = (IRow) null;
      for (int rownum = 0; rownum < 15; ++rownum)
      {
        IRow row2 = sheet.GetRow(rownum);
        if (row2 != null && row2.RowNum == 13)
        {
          row1 = row2;
          break;
        }
      }
      if (row1 != null)
        sheet.RemoveRow(row1);
      sheet.ShiftRows(14, 20, -1);
      int selectedIndex = this.cmbSensor.SelectedIndex;
      List<string> stringList1 = new List<string>();
      List<string> stringList2 = new List<string>();
      List<string> stringList3 = new List<string>();
      List<string> stringList4 = new List<string>();
      if (this.cmbSensor.SelectedIndex > 0)
      {
        foreach (DataRow row3 in (InternalDataCollectionBase) this.CurrentUnits.Rows)
        {
          foreach (Sensor sensor in (List<Sensor>) row3["Sensors"])
          {
            if (sensor.SensorID.ToString() == this.cmbSensor.SelectedValue)
            {
              stringList1.Add((string) row3["CompanyName"]);
              stringList2.Add((string) row3["UnitCategory"]);
              stringList3.Add(sensor.SensorCategory1);
              stringList4.Add(sensor.SensorCategory2);
            }
          }
        }
      }
      else
      {
        string text1 = this.cmbCompany.SelectedItem.Text;
        string text2 = this.cmbLocation.SelectedItem.Text;
        string text3 = this.cmbAsset.SelectedItem.Text;
        for (int index = 0; index < this.CurrentUnits.Rows.Count; ++index)
        {
          if (!(this.CurrentUnits.Rows[index]["CompanyName"].ToString() != text1) && !(this.CurrentUnits.Rows[index]["UnitCategory"].ToString() != text2))
          {
            foreach (Sensor sensor in (List<Sensor>) this.CurrentUnits.Rows[index]["Sensors"])
            {
              if (!(sensor.SensorCategory1 != this.cmbAsset.SelectedItem.Text) && (this.cmbFluidType.SelectedIndex == 0 || !(sensor.SensorCategory2 != this.cmbFluidType.SelectedItem.Text)))
              {
                if (!stringList1.Contains((string) this.CurrentUnits.Rows[index]["CompanyName"]))
                  stringList1.Add((string) this.CurrentUnits.Rows[index]["CompanyName"]);
                if (!stringList2.Contains((string) this.CurrentUnits.Rows[index]["UnitCategory"]))
                  stringList2.Add((string) this.CurrentUnits.Rows[index]["UnitCategory"]);
                if (!stringList3.Contains(sensor.SensorCategory1))
                  stringList3.Add(sensor.SensorCategory1);
                if (!stringList4.Contains(sensor.SensorCategory2))
                  stringList4.Add(sensor.SensorCategory2);
              }
            }
          }
        }
      }
      Dictionary<string, object> dictionary1 = new Dictionary<string, object>();
      dictionary1.Add("{ReportDate}", (object) DateTime.Now);
      dictionary1.Add("{ReportTime}", (object) DateTime.Now);
      dictionary1.Add("{ReportDateTime}", (object) DateTime.Now);
      dictionary1.Add("{Company}", (object) string.Join(", ", stringList1.ToArray()));
      dictionary1.Add("{Location}", (object) string.Join(", ", stringList2.ToArray()));
      dictionary1.Add("{Asset}", (object) string.Join(", ", stringList3.ToArray()));
      dictionary1.Add("{Fluid}", (object) string.Join(", ", stringList4.ToArray()));
      dictionary1.Add("{Username}", (object) Membership.GetUser().UserName);
      if (this.cmbReportMode.SelectedIndex == 0)
        dictionary1.Add("{ReportType}", (object) "Daily Average");
      else if (this.cmbReportMode.SelectedIndex == 1)
        dictionary1.Add("{ReportType}", (object) "Daily Minimum");
      else if (this.cmbReportMode.SelectedIndex == 2)
        dictionary1.Add("{ReportType}", (object) "Daily Maximum");
      for (int rownum = 0; rownum < 20; ++rownum)
      {
        if (rownum <= sheet.LastRowNum)
        {
          IRow row4 = sheet.GetRow(rownum);
          if (row4 != null)
          {
            for (int index = 0; index < 12; ++index)
            {
              ICell cell = row4.GetCell(index) ?? row4.CreateCell(index);
              if (cell != null && cell.CellType == CellType.String && cell.StringCellValue.Contains("{"))
              {
                string stringCellValue = row4.Cells[index].StringCellValue;
                if (dictionary1.ContainsKey(stringCellValue))
                {
                  object v = dictionary1[stringCellValue];
                  if (v != null)
                  {
                    string dateformat = "";
                    if (stringCellValue == "{ReportDate}")
                      dateformat = "yyyy-MM-dd";
                    if (stringCellValue == "{ReportTime}")
                      dateformat = "HH:mm:ss";
                    if (stringCellValue == "{ReportDateTime")
                      dateformat = "yyyy-MM-dd HH:mm:ss";
                    this.SetCellValue(cell, v, dataFormat, dateformat);
                  }
                }
              }
            }
          }
        }
      }
      int num1 = 12;
      IRow row5 = sheet.GetRow(num1 - 1);
      IRow row6 = sheet.GetRow(num1);
      for (int index = 0; index < 12; ++index)
        row6.Cells[index].SetCellValue(row5.Cells[index].StringCellValue);
      int num2 = num1 + 1;
      bool flag = false;
      foreach (GridViewRow row7 in this.gridReport.Rows)
      {
        if (row7.RowType == DataControlRowType.DataRow)
        {
          if (flag)
            sheet.CopyRow(num1, num2);
          else
            sheet.CopyRow(num1 - 1, num2);
          flag = !flag;
          IRow row8 = sheet.GetRow(num2);
          Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
          dictionary2.Add("{SummaryDate}", (object) (DateTime) this.gridReport.DataKeys[row7.RowIndex]["Date"]);
          dictionary2.Add("{ISO4Code}", this.DictionaryHelper(row7.Cells[1].Text));
          dictionary2.Add("{ISO6Code}", this.DictionaryHelper(row7.Cells[2].Text));
          dictionary2.Add("{ISO14Code}", this.DictionaryHelper(row7.Cells[3].Text));
          dictionary2.Add("{ISO21Code}", this.DictionaryHelper(row7.Cells[4].Text));
          for (int index = 0; index < 12; ++index)
          {
            if (row8.GetCell(index) == null)
              row8.CreateCell(index);
            string stringCellValue = row8.Cells[index].StringCellValue;
            if (dictionary2.ContainsKey(stringCellValue) && dictionary2.ContainsKey(stringCellValue))
            {
              object v = dictionary2[stringCellValue];
              string dateformat = "";
              if (row8.Cells[index].StringCellValue == "{SummaryDate}")
                dateformat = "yyyy-MM-dd";
              this.SetCellValue(row8.Cells[index], v, dataFormat, dateformat);
            }
          }
          ++num2;
        }
      }
      sheet.ShiftRows(num1, num2 + 8, -1);
      sheet.ShiftRows(num1, num2 + 8, -1);
      string str = this.Server.MapPath("~/Excel/CurrentExcelFile" + (workbook.GetType() == typeof (HSSFWorkbook) ? ".xls" : ".xlsx"));
      MemoryStream memoryStream = new MemoryStream();
      workbook.Write((Stream) memoryStream);
      using (FileStream fileStream = new FileStream(str, FileMode.Create))
        fileStream.Write(memoryStream.ToArray(), 0, (int) memoryStream.Length);
      FileInfo file = new FileInfo(str);
      this.TriggerDownload("FluidComm_SummaryReadingReport_" + DateTime.Now.ToString("s", (IFormatProvider) DateTimeFormatInfo.InvariantInfo) + file.Extension, str, file);
    }

    private void GenerateSummaryReportExcelNoTemplate()
    {
      throw new NotImplementedException("Readings Summary No Template Method not implemented");
    }

    protected void UpdateAlarmDate(object sender, EventArgs e)
    {
    }

    private void UpdateAlarms()
    {
      if (this.cmbCompany.SelectedIndex < 0 || this.cmbLocation.SelectedIndex < 0 || this.cmbAsset.SelectedIndex < 0)
      {
        this.ClearPanels(2);
      }
      else
      {
        DateTime? nullable1 = new DateTime?();
        DateTime? nullable2 = new DateTime?();
        DataTable dataTable = new DataTable();
        List<int> intList1 = new List<int>();
        dataTable.Columns.Add("SensorID", typeof (int));
        dataTable.Columns.Add("SensorName", typeof (string));
        dataTable.Columns.Add("StartDate", typeof (DateTime));
        dataTable.Columns.Add("AlarmText", typeof (string));
        dataTable.Columns.Add("Severity", typeof (bool));
        DateTime result;
        if (this.txtDate2A.Text != "" && DateTime.TryParse(this.txtDate2A.Text, out result))
          nullable1 = new DateTime?(result);
        if (this.txtDate2B.Text != "" && DateTime.TryParse(this.txtDate2B.Text, out result))
          nullable2 = new DateTime?(result.AddDays(1.0));
        if (this.cmbAsset.SelectedIndex < 0)
        {
          this.ClearPanels(1);
        }
        else
        {
          List<int> intList2 = new List<int>();
          if (this.cmbSensor.SelectedIndex > 0)
          {
            intList1.Add(int.Parse(this.cmbSensor.SelectedValue));
          }
          else
          {
            string text1 = this.cmbCompany.SelectedItem.Text;
            string text2 = this.cmbLocation.SelectedItem.Text;
            string text3 = this.cmbAsset.SelectedItem.Text;
            for (int index = 0; index < this.CurrentUnits.Rows.Count; ++index)
            {
              if (!(this.CurrentUnits.Rows[index]["CompanyName"].ToString() != text1) && !(this.CurrentUnits.Rows[index]["UnitCategory"].ToString() != text2) && Utils.UnitHasAsset(this.CurrentUnits.Rows[index], text3))
              {
                foreach (Sensor sensor in (List<Sensor>) this.CurrentUnits.Rows[index]["Sensors"])
                {
                  if (!(sensor.SensorCategory1 != this.cmbAsset.SelectedItem.Text) && (this.cmbFluidType.SelectedIndex == 0 || !(sensor.SensorCategory2 != this.cmbFluidType.SelectedItem.Text)))
                    intList1.Add(sensor.SensorID);
                }
              }
            }
          }
          if (intList1.Count < 1)
          {
            this.ClearPanels(1);
          }
          else
          {
            string cmdText = "SELECT a.AlarmID, a.SensorID, s.SensorName, a.BeginDate, a.AlarmString AS AlarmText, a.AlarmValue" + " FROM Alarm a INNER JOIN Sensor s ON s.SensorID=a.SensorID" + " WHERE a.SensorID IN (" + string.Join<int>(", ", (IEnumerable<int>) intList1.ToArray()) + ")";
            if (nullable1.HasValue)
              cmdText = cmdText + " AND a.BeginDate > '" + nullable1.Value.ToString(Utils.MasterDateTimeQueryString) + "'";
            if (nullable2.HasValue)
              cmdText = cmdText + " AND a.BeginDate < '" + nullable2.Value.ToString(Utils.MasterDateTimeQueryString) + "'";
            using (SqlConnection conn = Utils.GetConn())
            {
              using (SqlCommand selectCommand = new SqlCommand(cmdText, conn))
              {
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                  sqlDataAdapter.Fill(dataTable);
              }
              conn.Close();
            }
            dataTable.DefaultView.Sort = "StartDate DESC";
            this.gridAlarms.DataSource = (object) dataTable.DefaultView.ToTable();
            this.gridAlarms.DataBind();
          }
        }
      }
    }
  }
}
