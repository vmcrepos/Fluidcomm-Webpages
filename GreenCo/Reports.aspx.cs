// Decompiled with JetBrains decompiler
// Type: GreenCo.Reports
// Assembly: GreenCo, Version=0.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F63B31C9-6FC1-4AED-A804-2C1309A8A158
// Assembly location: C:\Users\gregg\Desktop\Fluidcomm-Webpages\bin\GreenCo.dll

using GreenCo.Constants;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

#nullable disable
namespace GreenCo
{
  public class Reports : Page
  {
    protected System.Web.UI.ScriptManager scriptMan;
    protected UpdateProgress ajaxUpdateProgress;
    protected UpdatePanel PrimaryFiltersPanel;
    protected Panel pnlLeft;
    protected Panel pnlLeftTitle;
    protected Label lblSideTitle;
    protected Panel pnlAdminCheckBox;
    protected Panel Panel2;
    protected Panel ChkAllUnitsWrapperPanel;
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
    protected UpdatePanel MobilePanel;
    protected Panel pnlRight;
    protected Panel pnlRightTitle;
    protected Label lblMainTitle;
    protected Panel pnlRightFilters;
    protected Panel dateFilters;
    protected TextBox txtDateStart;
    protected TextBox txtDateEnd;
    protected CheckBox chkZeroes;
    protected Panel mobileFilters;
    protected Panel mobileFilters1;
    protected Panel pnlmMobileFilters1;
    protected Label lblWorkOrder;
    protected Panel Panel1;
    protected RadDropDownList ddlWorkOrder;
    protected Panel mobileFilters2;
    protected Panel pnlMobileFiltes2;
    protected Label lblTechnicianId;
    protected Panel Panel6;
    protected RadDropDownList ddlTechnicianId;
    protected Panel mobileFilters3;
    protected Panel Panel5;
    protected Label lblEquipmentNumber;
    protected Panel Panel7;
    protected RadDropDownList ddlEquipmentNumber;
    protected Panel mobileFilters4;
    protected Panel Panel8;
    protected Label lblFluideTypeMobile;
    protected Panel Panel9;
    protected RadDropDownList ddlFluidTypeMobile;
    protected Panel reportWrapper;
    protected Panel reportResult;
    protected Literal litQueryResult;
    protected RadButton btnDownloadExcel;

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

    private DataTable MobileFilters
    {
      get
      {
        if (this.Session[nameof (MobileFilters)] == null)
          this.RefreshMobileFilters();
        return (DataTable) this.Session[nameof (MobileFilters)];
      }
      set => this.Session[nameof (MobileFilters)] = (object) value;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      this.CurrentUnits = Utils.RefreshUnits(this.chkAllUnits.Checked);
      System.Web.UI.ScriptManager.GetCurrent(this.Page).RegisterPostBackControl((Control) this.btnDownloadExcel);
      this.pnlAdminCheckBox.Visible = Roles.IsUserInRole("Admin") || Roles.IsUserInRole("CompanyAdmin");
      if (this.IsPostBack)
        return;
      this.txtDateStart.Text = DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd");
      this.txtDateEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
      this.PopulateCompanyDropDown();
      this.PopulateDataTypeDropDown();
      this.cmbCompany_SelectedIndexChanged((object) null, (DropDownListEventArgs) null);
      this.RefreshMobileFilters();
    }

    private void PopulateCompanyDropDown()
    {
      this.cmbCompany.DataSource = (object) this.CurrentUnits.AsEnumerable().Select<DataRow, string>((System.Func<DataRow, string>) (row => row.Field<string>("CompanyName"))).Distinct<string>().ToList<string>();
      this.cmbCompany.DataBind();
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
        string company = this.cmbCompany.SelectedItem.Text;
        this.cmbLocation.DataSource = (object) this.CurrentUnits.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (row => row.Field<string>("CompanyName") == company)).Select<DataRow, string>((System.Func<DataRow, string>) (row => row.Field<string>("UnitCategory"))).Distinct<string>().ToList<string>();
        this.cmbLocation.DataBind();
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
        string company = this.cmbCompany.SelectedItem.Text;
        string location = this.cmbLocation.SelectedItem.Text;
        this.cmbAsset.DataSource = (object) this.CurrentUnits.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (row => row.Field<string>("CompanyName").Equals(company) && row.Field<string>("UnitCategory").Equals(location))).Select<DataRow, List<string>>((System.Func<DataRow, List<string>>) (row => row.Field<List<Sensor>>("Sensors").Select<Sensor, string>((System.Func<Sensor, string>) (x => x.SensorCategory1)).Where<string>((System.Func<string, bool>) (x => !string.IsNullOrEmpty(x))).ToList<string>())).SelectMany<List<string>, string>((System.Func<List<string>, IEnumerable<string>>) (s => (IEnumerable<string>) s)).Distinct<string>().ToList<string>();
        this.cmbAsset.DataBind();
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
        string company = this.cmbCompany.SelectedItem.Text;
        string location = this.cmbLocation.SelectedItem.Text;
        string asset = this.cmbAsset.SelectedItem.Text;
        List<string> stringList = new List<string>();
        stringList.Add("All Fluid Types");
        int Sensor = -1;
        if (this.cmbSensor.SelectedIndex >= 0)
          Sensor = int.Parse(this.cmbSensor.SelectedValue);
        List<string> list = this.CurrentUnits.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (row => row.Field<string>("CompanyName").Equals(company) && row.Field<string>("UnitCategory").Equals(location) && row.Field<List<Sensor>>("Sensors").Where<Sensor>((System.Func<Sensor, bool>) (s => s.SensorCategory1.Equals(asset))).Count<Sensor>() > 0)).Where<DataRow>((System.Func<DataRow, bool>) (row => row.Field<List<Sensor>>("Sensors").Where<Sensor>((System.Func<Sensor, bool>) (s => s.SensorID == Sensor || Sensor == -1)).Count<Sensor>() > 0)).Select<DataRow, List<string>>((System.Func<DataRow, List<string>>) (row => row.Field<List<Sensor>>("Sensors").Select<Sensor, string>((System.Func<Sensor, string>) (x => x.SensorCategory2)).ToList<string>())).SelectMany<List<string>, string>((System.Func<List<string>, IEnumerable<string>>) (s => (IEnumerable<string>) s)).Distinct<string>().ToList<string>();
        stringList.AddRange((IEnumerable<string>) list);
        this.cmbFluidType.DataSource = (object) stringList;
        this.cmbFluidType.DataBind();
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
        string company = this.cmbCompany.SelectedItem.Text;
        string location = this.cmbLocation.SelectedItem.Text;
        string asset = this.cmbAsset.SelectedItem.Text;
        string fluidtype = this.cmbFluidType.SelectedItem.Text;
        this.cmbSensor.DataSource = (object) this.CurrentUnits.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (row => row.Field<string>("CompanyName").Equals(company) && row.Field<string>("UnitCategory").Equals(location))).Select<DataRow, List<Sensor>>((System.Func<DataRow, List<Sensor>>) (row => row.Field<List<Sensor>>("Sensors").Where<Sensor>((System.Func<Sensor, bool>) (s =>
        {
          if (!(s.SensorCategory1 == asset))
            return false;
          return this.cmbFluidType.SelectedIndex == 0 || s.SensorCategory2 == fluidtype;
        })).ToList<Sensor>())).SelectMany<List<Sensor>, Sensor>((System.Func<List<Sensor>, IEnumerable<Sensor>>) (x => (IEnumerable<Sensor>) x)).ToList<Sensor>();
        this.cmbSensor.DataBind();
        this.cmbSensor.Items.Insert(0, new DropDownListItem("All Sensors", "-1"));
      }
    }

    private void PopulateDataTypeDropDown()
    {
      this.cmbDataType.Items.Clear();
      this.cmbDataType.Items.Add("All Data Types");
      this.cmbDataType.Items.Add("Particle Count");
      this.cmbDataType.Items.Add("Physical Properties");
    }

    private List<string> GetSensorIDsForCommonFilters()
    {
      string company = this.cmbCompany.SelectedItem.Text;
      string location = this.cmbLocation.SelectedItem.Text;
      string asset = this.cmbAsset.SelectedItem.Text;
      return this.CurrentUnits.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (row => row.Field<string>("CompanyName").Equals(company) && row.Field<string>("UnitCategory").Equals(location))).Select<DataRow, List<string>>((System.Func<DataRow, List<string>>) (row => row.Field<List<Sensor>>("Sensors").Where<Sensor>((System.Func<Sensor, bool>) (s =>
      {
        if (this.cmbSensor.SelectedIndex != 0 && (this.cmbSensor.SelectedIndex <= 0 || !(s.SensorID.ToString() == this.cmbSensor.SelectedValue)) || !(s.SensorCategory1 == asset))
          return false;
        return this.cmbFluidType.SelectedIndex == 0 || s.SensorCategory2 == this.cmbFluidType.SelectedItem.Text;
      })).Select<Sensor, string>((System.Func<Sensor, string>) (x => x.SensorID.ToString())).ToList<string>())).SelectMany<List<string>, string>((System.Func<List<string>, IEnumerable<string>>) (x => (IEnumerable<string>) x)).ToList<string>();
    }

    private void RefreshMobileFilters()
    {
      DataTable dataTable = new DataTable();
      dataTable.Columns.Add("meta1", typeof (string));
      dataTable.Columns.Add("meta2", typeof (string));
      dataTable.Columns.Add("meta3", typeof (string));
      dataTable.Columns.Add("meta4", typeof (string));
      DateTime? nullable1 = new DateTime?();
      DateTime? nullable2 = new DateTime?();
      DateTime result;
      if (this.txtDateStart.Text != "" && DateTime.TryParse(this.txtDateStart.Text, out result))
        nullable1 = new DateTime?(result);
      if (this.txtDateEnd.Text != "" && DateTime.TryParse(this.txtDateEnd.Text, out result))
        nullable2 = new DateTime?(result.AddDays(1.0));
      List<string> forCommonFilters = this.GetSensorIDsForCommonFilters();
      string cmdText = "SELECT meta1, meta2, meta3, meta4 FROM Data WHERE SensorID IN (" + string.Join(", ", forCommonFilters.ToArray()) + ")";
      if (nullable1.HasValue)
        cmdText = cmdText + " AND Date > '" + nullable1.Value.ToString(Utils.MasterDateTimeQueryString) + "'";
      if (nullable2.HasValue)
        cmdText = cmdText + " AND Date < '" + nullable2.Value.ToString(Utils.MasterDateTimeQueryString) + "'";
      using (SqlConnection conn = Utils.GetConn())
      {
        using (SqlCommand selectCommand = new SqlCommand(cmdText, conn))
        {
          using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
          {
            if (forCommonFilters.Count > 0)
              sqlDataAdapter.Fill(dataTable);
          }
        }
      }
      this.MobileFilters = dataTable;
    }

    private void PopulateWorkOrderDropDown()
    {
      List<string> stringList = new List<string>();
      stringList.Add("All Work Orders");
      List<string> list = this.MobileFilters.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (row => !string.IsNullOrEmpty(row.Field<string>("meta1")))).Select<DataRow, string>((System.Func<DataRow, string>) (row => row.Field<string>("meta1"))).Distinct<string>().ToList<string>();
      stringList.AddRange((IEnumerable<string>) list);
      this.ddlWorkOrder.DataSource = (object) stringList;
      this.ddlWorkOrder.DataBind();
      this.PopulateTechnicianIdDropDown();
    }

    private void PopulateTechnicianIdDropDown()
    {
      List<string> stringList = new List<string>();
      stringList.Add("All Technician IDs");
      string workOrder = this.ddlWorkOrder.SelectedItem.Text;
      List<string> list = this.MobileFilters.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (row => (this.ddlWorkOrder.SelectedIndex == 0 || row.Field<string>("meta1") != null && row.Field<string>("meta1").Equals(workOrder)) && !string.IsNullOrEmpty(row.Field<string>("meta2")))).Select<DataRow, string>((System.Func<DataRow, string>) (row => row.Field<string>("meta2"))).Distinct<string>().ToList<string>();
      stringList.AddRange((IEnumerable<string>) list);
      this.ddlTechnicianId.DataSource = (object) stringList;
      this.ddlTechnicianId.DataBind();
      this.PopulateEquipmentNumberDropDown();
    }

    private void PopulateEquipmentNumberDropDown()
    {
      List<string> stringList = new List<string>();
      stringList.Add("All Equipment Numbers");
      string text1 = this.ddlWorkOrder.SelectedItem.Text;
      string text2 = this.ddlTechnicianId.SelectedItem.Text;
      List<string> list = this.MobileFilters.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (row => (this.ddlWorkOrder.SelectedIndex == 0 || row.Field<string>("meta1") != null && this.ddlWorkOrder.SelectedItem.Text == row.Field<string>("meta1")) && (this.ddlTechnicianId.SelectedIndex == 0 || row.Field<string>("meta2") != null && row.Field<string>("meta2") == this.ddlTechnicianId.SelectedItem.Text) && !string.IsNullOrEmpty(row.Field<string>("meta4")))).Select<DataRow, string>((System.Func<DataRow, string>) (row => row.Field<string>("meta4"))).Distinct<string>().ToList<string>();
      stringList.AddRange((IEnumerable<string>) list);
      this.ddlEquipmentNumber.DataSource = (object) stringList;
      this.ddlEquipmentNumber.DataBind();
      this.PopulateMobileFluidTypeDropDown();
    }

    private void PopulateMobileFluidTypeDropDown()
    {
      List<string> stringList = new List<string>();
      stringList.Add("All Fluid Types");
      string workOrder = this.ddlWorkOrder.SelectedItem.Text;
      string technicianId = this.ddlTechnicianId.SelectedItem.Text;
      string equipmentNumber = this.ddlEquipmentNumber.SelectedItem.Text;
      List<string> list = this.MobileFilters.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (row => (this.ddlWorkOrder.SelectedIndex == 0 || row.Field<string>("meta1") != null && row.Field<string>("meta1").Equals(workOrder)) && (this.ddlTechnicianId.SelectedIndex == 0 || row.Field<string>("meta2") != null && row.Field<string>("meta2").Equals(technicianId)) && (this.ddlEquipmentNumber.SelectedIndex == 0 || row.Field<string>("meta4") != null && row.Field<string>("meta4").Equals(equipmentNumber)) && !string.IsNullOrEmpty(row.Field<string>("meta3")))).Select<DataRow, string>((System.Func<DataRow, string>) (row => row.Field<string>("meta3"))).Distinct<string>().ToList<string>();
      stringList.AddRange((IEnumerable<string>) list);
      this.ddlFluidTypeMobile.DataSource = (object) stringList;
      this.ddlFluidTypeMobile.DataBind();
    }

    private bool PortableUnitSelected()
    {
      string[] source = new string[2]
      {
        "Portable",
        "ISOTote"
      };
      return this.cmbAsset.SelectedItem != null && ((IEnumerable<string>) source).Contains<string>(this.cmbAsset.SelectedItem.Text);
    }

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
      if (this.PortableUnitSelected())
      {
        this.RefreshMobileFilters();
        this.PopulateWorkOrderDropDown();
      }
      this.UpdateReportCount();
    }

    protected void chkAllUnits_CheckedChanged(object sender, EventArgs e)
    {
    }

    protected void cmbCompany_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
      this.PopulateLocationDropDown();
      this.lblSideTitle.Text = this.cmbCompany.SelectedIndex >= 0 ? this.cmbCompany.SelectedItem.Text : "";
      this.cmbLocation_SelectedIndexChanged((object) null, (DropDownListEventArgs) null);
    }

    protected void cmbLocation_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
      this.PopulateAssetDropDown();
      this.cmbAsset_SelectedIndexChanged((object) null, (DropDownListEventArgs) null);
    }

    protected void cmbAsset_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
      this.PopulateFluidTypeDropDown();
      if (this.PortableUnitSelected())
      {
        this.cmbFluidType.Enabled = false;
        this.cmbFluidType.CssClass = "cmbDisabled cmbStandard";
        this.cmbFluidType.SelectedIndex = 0;
        this.PopulateSensorDropDown();
        this.cmbSensor_SelectedIndexChanged((object) null, (DropDownListEventArgs) null);
        this.mobileFilters.CssClass = "mobileFiltersWrapper";
        this.RefreshMobileFilters();
        this.PopulateWorkOrderDropDown();
      }
      else
      {
        this.cmbFluidType.Enabled = true;
        this.cmbFluidType.CssClass = "cmbStandard";
        this.cmbFluidType_SelectedIndexChanged((object) null, (DropDownListEventArgs) null);
        this.mobileFilters.CssClass = "mobileFilters hide";
        this.PopulateFluidTypeDropDown();
        this.cmbFluidType_SelectedIndexChanged((object) null, (DropDownListEventArgs) null);
      }
      if (this.cmbLocation.SelectedIndex < 0 || this.cmbAsset.SelectedIndex < 0)
        this.lblMainTitle.Text = "No Units Available";
      else
        this.lblMainTitle.Text = this.cmbLocation.SelectedItem.Text + ": " + this.cmbAsset.SelectedItem.Text;
    }

    protected void cmbFluidType_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
      this.PopulateSensorDropDown();
      this.cmbSensor_SelectedIndexChanged((object) null, (DropDownListEventArgs) null);
    }

    protected void cmbSensor_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
      this.RefreshMobileFilters();
      this.PopulateWorkOrderDropDown();
      this.UpdateReportCount();
    }

    protected void cmbDataType_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
      this.cmbCompany_SelectedIndexChanged((object) null, (DropDownListEventArgs) null);
    }

    protected void ddlWorkOrder_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
      this.PopulateTechnicianIdDropDown();
      this.ddlTechnicianId_SelectedIndexChanged((object) null, (DropDownListEventArgs) null);
    }

    protected void ddlTechnicianId_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
      this.PopulateEquipmentNumberDropDown();
      this.ddlEquipmentNumber_SelectedIndexChanged((object) null, (DropDownListEventArgs) null);
    }

    protected void ddlEquipmentNumber_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
      this.PopulateMobileFluidTypeDropDown();
      this.ddlFluidTypeMobile_SelectedIndexChanged((object) null, (DropDownListEventArgs) null);
    }

    protected void ddlFluidTypeMobile_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
      this.UpdateReportCount();
    }

    private void UpdateReportCount()
    {
      DateTime? nullable1 = new DateTime?();
      DateTime? nullable2 = new DateTime?();
      DateTime result1;
      if (this.txtDateStart.Text != "" && DateTime.TryParse(this.txtDateStart.Text, out result1))
        nullable1 = new DateTime?(result1);
      if (this.txtDateEnd.Text != "" && DateTime.TryParse(this.txtDateEnd.Text, out result1))
        nullable2 = new DateTime?(result1.AddDays(1.0));
      List<string> forCommonFilters = this.GetSensorIDsForCommonFilters();
      string text1 = this.ddlWorkOrder.SelectedItem.Text;
      string text2 = this.ddlTechnicianId.SelectedItem.Text;
      string text3 = this.ddlEquipmentNumber.SelectedItem.Text;
      string text4 = this.ddlFluidTypeMobile.SelectedItem.Text;
      string cmdText = "SELECT COUNT(d.readingid)" + " FROM Data d" + " INNER JOIN Sensor s ON s.SensorID=d.SensorID AND s.SensorID IN (" + string.Join(", ", forCommonFilters.ToArray()) + ")" + " WHERE 1=1";
      if (!this.chkZeroes.Checked)
        cmdText += " AND NOT(d.ISO4 = 0 AND d.ISO6 = 0 AND d.ISO14 = 0 AND d.ISO21 = 0)";
      if (nullable1.HasValue)
        cmdText = cmdText + " AND d.Date > '" + nullable1.Value.ToString(Utils.MasterDateTimeQueryString) + "'";
      if (nullable2.HasValue)
        cmdText = cmdText + " AND d.Date < '" + nullable2.Value.ToString(Utils.MasterDateTimeQueryString) + "'";
      if (this.PortableUnitSelected())
      {
        if (this.ddlWorkOrder.SelectedIndex > 0)
          cmdText = cmdText + " AND d.meta1 = '" + text1 + "'";
        if (this.ddlTechnicianId.SelectedIndex > 0)
          cmdText = cmdText + " AND d.meta2 = '" + text2 + "'";
        if (this.ddlEquipmentNumber.SelectedIndex > 0)
          cmdText = cmdText + " AND d.meta4 = '" + text3 + "'";
        if (this.ddlFluidTypeMobile.SelectedIndex > 0)
          cmdText = cmdText + " AND d.meta3 = '" + text4 + "'";
      }
      object obj = (object) null;
      using (SqlConnection conn = Utils.GetConn())
      {
        using (SqlCommand sqlCommand = new SqlCommand(cmdText, conn))
          obj = sqlCommand.ExecuteScalar();
      }
      int result2;
      if (obj == null || !int.TryParse(obj.ToString(), out result2))
        this.litQueryResult.Text = "Error in retrieving data.";
      else if (result2 > 10000)
      {
        this.litQueryResult.Text = "Your query has more than 10,000 readings! Please change your parameters to retrieve fewer readings.";
        this.btnDownloadExcel.Enabled = false;
      }
      else
      {
        this.litQueryResult.Text = "Your query has " + result2.ToString() + " readings.";
        this.btnDownloadExcel.Enabled = true;
      }
    }

    private Dictionary<string, DataTable> GetReportsData()
    {
      if (this.cmbCompany.SelectedIndex < 0 || this.cmbLocation.SelectedIndex < 0 || this.cmbAsset.SelectedIndex < 0)
        return (Dictionary<string, DataTable>) null;
      DateTime? nullable1 = new DateTime?();
      DateTime? nullable2 = new DateTime?();
      DateTime result;
      if (this.txtDateStart.Text != "" && DateTime.TryParse(this.txtDateStart.Text, out result))
        nullable1 = new DateTime?(result);
      if (this.txtDateEnd.Text != "" && DateTime.TryParse(this.txtDateEnd.Text, out result))
        nullable2 = new DateTime?(result.AddDays(1.0));
      List<string> forCommonFilters = this.GetSensorIDsForCommonFilters();
      string text1 = this.ddlWorkOrder.SelectedItem.Text;
      string text2 = this.ddlTechnicianId.SelectedItem.Text;
      string text3 = this.ddlEquipmentNumber.SelectedItem.Text;
      string text4 = this.ddlFluidTypeMobile.SelectedItem.Text;
      DataTable dataTable1 = new DataTable();
      dataTable1.Columns.Add("id", typeof (long));
      dataTable1.Columns.Add("UnitID", typeof (int));
      dataTable1.Columns.Add("UnitName", typeof (string));
      dataTable1.Columns.Add("SensorID", typeof (int));
      dataTable1.Columns.Add("SensorName", typeof (string));
      dataTable1.Columns.Add("Date", typeof (DateTime));
      dataTable1.Columns.Add("ISO4", typeof (int));
      dataTable1.Columns.Add("ISO4P", typeof (double));
      dataTable1.Columns.Add("ISO6", typeof (int));
      dataTable1.Columns.Add("ISO6P", typeof (double));
      dataTable1.Columns.Add("ISO14", typeof (int));
      dataTable1.Columns.Add("ISO14P", typeof (double));
      dataTable1.Columns.Add("ISO21", typeof (int));
      dataTable1.Columns.Add("ISO21P", typeof (double));
      dataTable1.Columns.Add("Alt", typeof (int));
      dataTable1.Columns.Add("Meta1", typeof (string));
      dataTable1.Columns.Add("Meta2", typeof (string));
      dataTable1.Columns.Add("Meta3", typeof (string));
      dataTable1.Columns.Add("Meta4", typeof (string));
      dataTable1.Columns.Add("Meta5", typeof (string));
      DataTable dataTable2 = new DataTable();
      dataTable2.Columns.Add("id", typeof (long));
      dataTable2.Columns.Add("UnitID", typeof (int));
      dataTable2.Columns.Add("UnitName", typeof (string));
      dataTable2.Columns.Add("SensorID", typeof (int));
      dataTable2.Columns.Add("SensorName", typeof (string));
      dataTable2.Columns.Add("Date", typeof (DateTime));
      dataTable2.Columns.Add("RH", typeof (double));
      dataTable2.Columns.Add("Temp", typeof (double));
      string cmdText = "SELECT d.readingid AS id, u.UnitID, u.UnitName, d.SensorID, s.SensorName, d.Date, d.Alt, d.meta1, d.meta2, d.meta3, d.meta4, d.meta5, d.ISO4, d.ISO6, d.ISO14, d.ISO21, d.RH, d.Temp" + " FROM Data d" + " INNER JOIN Sensor s ON s.SensorID=d.SensorID AND s.SensorID IN (" + string.Join(", ", forCommonFilters.ToArray()) + ")" + " INNER JOIN Unit u ON u.UnitID = s.UnitID" + " WHERE 1=1";
      if (nullable1.HasValue)
        cmdText = cmdText + " AND d.Date > '" + nullable1.Value.ToString(Utils.MasterDateTimeQueryString) + "'";
      if (nullable2.HasValue)
        cmdText = cmdText + " AND d.Date < '" + nullable2.Value.ToString(Utils.MasterDateTimeQueryString) + "'";
      if (this.PortableUnitSelected())
      {
        if (this.ddlWorkOrder.SelectedIndex > 0)
          cmdText = cmdText + " AND d.meta1 = '" + text1 + "'";
        if (this.ddlTechnicianId.SelectedIndex > 0)
          cmdText = cmdText + " AND d.meta2 = '" + text2 + "'";
        if (this.ddlEquipmentNumber.SelectedIndex > 0)
          cmdText = cmdText + " AND d.meta4 = '" + text3 + "'";
        if (this.ddlFluidTypeMobile.SelectedIndex > 0)
          cmdText = cmdText + " AND d.meta3 = '" + text4 + "'";
      }
      DataTable dataTable3 = new DataTable();
      using (SqlConnection conn = Utils.GetConn())
      {
        using (SqlCommand selectCommand = new SqlCommand(cmdText, conn))
        {
          using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
          {
            if (forCommonFilters.Count > 0)
              sqlDataAdapter.Fill(dataTable3);
          }
        }
      }
      foreach (DataRow row in (InternalDataCollectionBase) dataTable3.Rows)
      {
        if (row["ISO4"] != DBNull.Value && row["ISO6"] != DBNull.Value && (row["ISO14"] != DBNull.Value || row["ISO21"] != DBNull.Value) && (this.chkZeroes.Checked || !(row["ISO4"].ToString() == "0") || !(row["ISO6"].ToString() == "0") || !(row["ISO14"].ToString() == "0") || !(row["ISO21"].ToString() == "0")))
          dataTable1.Rows.Add(row["id"], row["UnitID"], row["UnitName"], row["SensorID"], row["SensorName"], row["Date"], Utils.GetIsoCodeTableObject(row["ISO4"]), Utils.GetReadingTableObject(row["ISO4"]), Utils.GetIsoCodeTableObject(row["ISO6"]), Utils.GetReadingTableObject(row["ISO6"]), Utils.GetIsoCodeTableObject(row["ISO14"]), Utils.GetReadingTableObject(row["ISO14"]), Utils.GetIsoCodeTableObject(row["ISO21"]), Utils.GetReadingTableObject(row["ISO21"]), (object) Utils.ConvertFlowRate(row["Alt"]), row["meta1"], row["meta2"], row["meta3"], row["meta4"], row["meta5"]);
      }
      foreach (DataRow row in (InternalDataCollectionBase) dataTable3.Rows)
      {
        if (row["RH"] != DBNull.Value && row["Temp"] != DBNull.Value)
          dataTable2.Rows.Add(row["id"], row["UnitID"], row["UnitName"], row["SensorID"], row["SensorName"], row["Date"], Utils.GetReadingTableRHObject(row["RH"]), Utils.GetReadingTableTempObject(row["Temp"]));
      }
      return new Dictionary<string, DataTable>()
      {
        {
          "ParticleReadings",
          dataTable1
        },
        {
          "PhysicalReadings",
          dataTable2
        }
      };
    }

    protected void btnDownloadExcel_Click(object sender, EventArgs e)
    {
      int currentUserCompanyId = Utils.GetCurrentUserCompanyId(Membership.GetUser());
      if (currentUserCompanyId == -1)
      {
        this.Response.Redirect("~/Default.aspx", false);
      }
      else
      {
        string company = Utils.GetCompanyList()[currentUserCompanyId];
        Dictionary<string, object> metaData = new Dictionary<string, object>();
        metaData.Add("{Company}", (object) this.cmbCompany.SelectedItem.Text);
        metaData.Add("{Location}", (object) this.cmbLocation.SelectedItem.Text);
        metaData.Add("{Asset}", (object) this.cmbAsset.SelectedItem.Text);
        metaData.Add("{ReportDate}", (object) DateTime.Now);
        try
        {
          if (this.PortableUnitSelected())
          {
            string templateServerPath1 = TemplatePaths.GetExistingTemplateServerPath(Templates.TouchScreenAutoReport, company, this.Server);
            string templateServerPath2 = TemplatePaths.GetExistingTemplateServerPath(Templates.TouchScreenAutoReport, (string) null, this.Server);
            if (!string.IsNullOrEmpty(templateServerPath1))
              this.GenerateReadingsReportExcelFromTemplate(templateServerPath1, metaData, true);
            else if (!string.IsNullOrEmpty(templateServerPath2))
              this.GenerateReadingsReportExcelFromTemplate(templateServerPath2, metaData, true);
            else
              this.GenerateReadingsReportExcelNoTemplate(metaData);
          }
          else
          {
            List<string> sensorIds = this.GetSensorIDsForCommonFilters();
            List<string> list = this.CurrentUnits.AsEnumerable().Select<DataRow, IEnumerable<string>>((System.Func<DataRow, IEnumerable<string>>) (row => row.Field<List<Sensor>>("Sensors").Where<Sensor>((System.Func<Sensor, bool>) (s => sensorIds.Contains(s.SensorID.ToString()))).Select<Sensor, string>((System.Func<Sensor, string>) (s => s.SensorCategory2)))).SelectMany<IEnumerable<string>, string>((System.Func<IEnumerable<string>, IEnumerable<string>>) (s => s)).Distinct<string>().ToList<string>();
            metaData.Add("{FluidType}", (object) string.Join(", ", list.ToArray()));
            metaData.Add("{UserName}", (object) Membership.GetUser().UserName);
            string templateServerPath3 = TemplatePaths.GetExistingTemplateServerPath(Templates.AutoReport, company, this.Server);
            string templateServerPath4 = TemplatePaths.GetExistingTemplateServerPath(Templates.AutoReport, (string) null, this.Server);
            if (!string.IsNullOrEmpty(templateServerPath3))
              this.GenerateReadingsReportExcelFromTemplate(templateServerPath3, metaData, false);
            else if (!string.IsNullOrEmpty(templateServerPath4))
              this.GenerateReadingsReportExcelFromTemplate(templateServerPath4, metaData, false);
            else
              this.GenerateReadingsReportExcelNoTemplate(metaData);
          }
        }
        catch (ArgumentException ex)
        {
          throw ex;
        }
      }
    }

    private void GenerateReadingsReportExcelFromTemplate(
      string path,
      Dictionary<string, object> metaData,
      bool isPortableUnit)
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
        throw new ArgumentException("Cannot find a sheet name 'Table' in the excel file");
      int num1 = !isPortableUnit ? 12 : 10;
      IRow row1 = (IRow) null;
      for (int rownum = 0; rownum < 20; ++rownum)
      {
        IRow row2 = sheet.GetRow(rownum);
        if (row2 != null && row2.RowNum == num1)
        {
          row1 = row2;
          break;
        }
      }
      if (row1 != null)
        sheet.RemoveRow(row1);
      sheet.ShiftRows(num1 + 1, 20, -1);
      IDataFormat dataFormat = workbook.CreateDataFormat();
      for (int rownum = 0; rownum < 20; ++rownum)
      {
        if (rownum <= sheet.LastRowNum)
        {
          IRow row3 = sheet.GetRow(rownum);
          if (row3 != null)
          {
            for (int index = 0; index < 18; ++index)
            {
              ICell cell = row3.GetCell(index) ?? row3.CreateCell(index);
              if (cell != null && cell.CellType == CellType.String && cell.StringCellValue.Contains("{"))
              {
                string stringCellValue = row3.Cells[index].StringCellValue;
                if (metaData.ContainsKey(stringCellValue))
                {
                  object v = metaData[stringCellValue];
                  if (v != null)
                  {
                    string dateformat = "";
                    if (stringCellValue == "{ReportDate}")
                      dateformat = "yyyy-MM-dd";
                    if (stringCellValue == "{ReportTime}")
                      dateformat = "HH:mm:ss";
                    if (stringCellValue == "{ReportDateTime")
                      dateformat = "yyyy-MM-dd HH:mm:ss";
                    ExcelHelper.SetCellValue(cell, v, dataFormat, dateformat);
                  }
                }
              }
            }
          }
        }
      }
      int num2 = isPortableUnit ? 9 : 11;
      IRow row4 = sheet.GetRow(num2 - 1);
      IRow row5 = sheet.GetRow(num2);
      for (int index = 0; index < 18; ++index)
        row5.Cells[index].SetCellValue(row4.Cells[index].StringCellValue);
      int num3 = num2 + 1;
      bool flag = false;
      foreach (DataRow row6 in (InternalDataCollectionBase) this.GetReportsData()["ParticleReadings"].Rows)
      {
        if (flag)
          sheet.CopyRow(num2, num3);
        else
          sheet.CopyRow(num2 - 1, num3);
        flag = !flag;
        IRow row7 = sheet.GetRow(num3);
        Dictionary<string, object> dictionary = new Dictionary<string, object>();
        dictionary.Add("{ReadingDate}", (object) row6.Field<DateTime>("Date"));
        dictionary.Add("{ReadingTime}", (object) row6.Field<DateTime>("Date"));
        dictionary.Add("{ISO4Code}", (object) row6.Field<int>("ISO4"));
        dictionary.Add("{ISO4Count}", (object) Math.Round(row6.Field<double>("ISO4P")));
        dictionary.Add("{ISO6Code}", (object) row6.Field<int>("ISO6"));
        dictionary.Add("{ISO6Count}", (object) Math.Round(row6.Field<double>("ISO6P")));
        dictionary.Add("{ISO14Code}", (object) row6.Field<int>("ISO14"));
        dictionary.Add("{ISO14Count}", (object) Math.Round(row6.Field<double>("ISO14P")));
        dictionary.Add("{ISO21Code}", (object) row6.Field<int>("ISO21"));
        dictionary.Add("{ISO21Count}", (object) Math.Round(row6.Field<double>("ISO21P")));
        dictionary.Add("{WorkOrder}", (object) row6.Field<string>("meta1"));
        dictionary.Add("{TechnicianID}", (object) row6.Field<string>("meta2"));
        dictionary.Add("{Equipment#}", (object) row6.Field<string>("meta4"));
        dictionary.Add("{FluidType}", (object) row6.Field<string>("meta3"));
        dictionary.Add("{Hours/Miles}", (object) row6.Field<string>("meta5"));
        for (int index = 0; index < 18; ++index)
        {
          if (row7.GetCell(index) == null)
            row7.CreateCell(index);
          string stringCellValue = row7.Cells[index].StringCellValue;
          if (dictionary.ContainsKey(stringCellValue))
          {
            object v = dictionary[stringCellValue];
            string dateformat = "";
            if (row7.Cells[index].StringCellValue == "{ReadingDate}")
              dateformat = "yyyy-MM-dd";
            else if (row7.Cells[index].StringCellValue == "{ReadingTime}")
              dateformat = "HH:mm:ss";
            else if (row7.Cells[index].StringCellValue == "{ReadingDateTime")
              dateformat = "yyyy-MM-dd HH:mm:ss";
            ExcelHelper.SetCellValue(row7.Cells[index], v, dataFormat, dateformat);
          }
        }
        ++num3;
      }
      sheet.ShiftRows(num2, num3 + 8, -1);
      sheet.ShiftRows(num2, num3 + 8, -1);
      string str1 = this.Server.MapPath("~/Excel/CurrentExcelFile" + (workbook.GetType() == typeof (HSSFWorkbook) ? ".xls" : ".xlsx"));
      MemoryStream memoryStream = new MemoryStream();
      workbook.Write((Stream) memoryStream);
      using (FileStream fileStream = new FileStream(str1, FileMode.Create))
        fileStream.Write(memoryStream.ToArray(), 0, (int) memoryStream.Length);
      FileInfo file = new FileInfo(str1);
      DateTime now = DateTime.Now;
      string str2 = "FluidComm_AutoReport_";
      if (isPortableUnit)
        str2 += "TouchScreen_";
      this.TriggerDownload(str2 + DateTime.Now.ToString("s", (IFormatProvider) DateTimeFormatInfo.InvariantInfo) + file.Extension, str1, file);
    }

    private void GenerateReadingsReportExcelNoTemplate(Dictionary<string, object> metaData)
    {
      throw new NotImplementedException();
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
        this.Response.ContentType = "application/vnd" + file.Extension;
        this.Response.AddHeader("Content-Length", file.Length.ToString());
        this.Response.TransmitFile(filepath);
        this.Response.End();
      }
      else
        this.Response.Write("This file does not exist");
    }

    protected void chkZeroes_CheckedChanged(object sender, EventArgs e) => this.UpdateReportCount();

    protected void MobilePanel_Load(object sender, EventArgs e)
    {
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "text", "initDatePickers();", true);
    }
  }
}
