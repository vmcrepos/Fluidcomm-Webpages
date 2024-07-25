// Decompiled with JetBrains decompiler
// Type: GreenCo.ConfigureUnit
// Assembly: GreenCo, Version=0.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F63B31C9-6FC1-4AED-A804-2C1309A8A158
// Assembly location: C:\Users\gregg\Desktop\Fluidcomm-Webpages\bin\GreenCo.dll

using GreenCo.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

#nullable disable
namespace GreenCo
{
  public class ConfigureUnit : Page
  {
    private int unit_id;
    private Panel[] toggles;
    private HiddenField[] hids;
    private HiddenField[] sensorIdHids;
    private HiddenField[] sensorIndexHids;
    private Panel[] panels;
    private RadComboBox[] assetboxes;
    private RadComboBox[] productboxes;
    private static Dictionary<string, string> assets;
    private static Dictionary<string, string> fluids;
    private static Dictionary<string, string> locations;
    private bool IsFluidTraxAdmin;
    protected System.Web.UI.ScriptManager scriptMan;
    protected HiddenField hidSensorA;
    protected HiddenField hidSensorB;
    protected HiddenField hidSensorC;
    protected HiddenField hidSensorD;
    protected Label lblTitle;
    protected RadTextBox txtVlinkSerial;
    protected RadTextBox txtVlinkName;
    protected RadButton btnSave;
    protected RadButton btnReset;
    protected RadButton btnBack;
    protected Literal litError;
    protected RadTextBox txtUnitName;
    protected RadDropDownList cmbCompany;
    protected RadComboBox cmbLocation1;
    protected CheckBox chkBoxIsMobile;
    protected RadTextBox txtWorkOrderTag;
    protected RadTextBox txtTechnicianTag;
    protected RadTextBox txtSerialNumberTag;
    protected RadTextBox txtFluidTypeTag;
    protected RadTextBox txtHoursMilesTag;
    protected Panel pnlSensorAToggle;
    protected Panel pnlSensorA;
    protected HiddenField hidSensorAId;
    protected HiddenField hidSensorAIndex;
    protected RadTextBox txtSensorAName;
    protected RadComboBox cmbSensorAAsset;
    protected RadTextBox txtSensorA4Tag;
    protected RadTextBox txtSensorA4Warning;
    protected RadTextBox txtSensorA4Alarm;
    protected RadTextBox txtSensorA6Tag;
    protected RadTextBox txtSensorA6Warning;
    protected RadTextBox txtSensorA6Alarm;
    protected RadTextBox txtSensorA14Tag;
    protected RadTextBox txtSensorA14Warning;
    protected RadTextBox txtSensorA14Alarm;
    protected RadTextBox txtSensorA21Tag;
    protected RadTextBox txtSensorA21Warning;
    protected RadTextBox txtSensorA21Alarm;
    protected RadTextBox txtSensorADate;
    protected RadTextBox txtSensorAFlow;
    protected RadTextBox txtSensorATemp;
    protected RadTextBox txtSensorARH;
    protected RadComboBox cmbSensorAProduct;
    protected RadTextBox txtSensorAMin;
    protected RadTextBox txtSensorAMax;
    protected Label lblSensorAUsers;
    protected Panel pnlSensorBToggle;
    protected Panel pnlSensorB;
    protected HiddenField hidSensorBId;
    protected HiddenField hidSensorBIndex;
    protected RadTextBox txtSensorBName;
    protected RadComboBox cmbSensorBAsset;
    protected RadTextBox txtSensorB4Tag;
    protected RadTextBox txtSensorB4Warning;
    protected RadTextBox txtSensorB4Alarm;
    protected RadTextBox txtSensorB6Tag;
    protected RadTextBox txtSensorB6Warning;
    protected RadTextBox txtSensorB6Alarm;
    protected RadTextBox txtSensorB14Tag;
    protected RadTextBox txtSensorB14Warning;
    protected RadTextBox txtSensorB14Alarm;
    protected RadTextBox txtSensorB21Tag;
    protected RadTextBox txtSensorB21Warning;
    protected RadTextBox txtSensorB21Alarm;
    protected RadTextBox txtSensorBDate;
    protected RadTextBox txtSensorBFlow;
    protected RadTextBox txtSensorBTemp;
    protected RadTextBox txtSensorBRH;
    protected RadComboBox cmbSensorBProduct;
    protected RadTextBox txtSensorBMin;
    protected RadTextBox txtSensorBMax;
    protected Label lblSensorBUsers;
    protected Panel pnlSensorCToggle;
    protected Panel pnlSensorC;
    protected HiddenField hidSensorCId;
    protected HiddenField hidSensorCIndex;
    protected RadTextBox txtSensorCName;
    protected RadComboBox cmbSensorCAsset;
    protected RadTextBox txtSensorC4Tag;
    protected RadTextBox txtSensorC4Warning;
    protected RadTextBox txtSensorC4Alarm;
    protected RadTextBox txtSensorC6Tag;
    protected RadTextBox txtSensorC6Warning;
    protected RadTextBox txtSensorC6Alarm;
    protected RadTextBox txtSensorC14Tag;
    protected RadTextBox txtSensorC14Warning;
    protected RadTextBox txtSensorC14Alarm;
    protected RadTextBox txtSensorC21Tag;
    protected RadTextBox txtSensorC21Warning;
    protected RadTextBox txtSensorC21Alarm;
    protected RadTextBox txtSensorCDate;
    protected RadTextBox txtSensorCFlow;
    protected RadTextBox txtSensorCTemp;
    protected RadTextBox txtSensorCRH;
    protected RadComboBox cmbSensorCProduct;
    protected RadTextBox txtSensorCMin;
    protected RadTextBox txtSensorCMax;
    protected Label lblSensorCUsers;
    protected Panel pnlSensorDToggle;
    protected Panel pnlSensorD;
    protected HiddenField hidSensorDId;
    protected HiddenField hidSensorDIndex;
    protected RadTextBox txtSensorDName;
    protected RadComboBox cmbSensorDAsset;
    protected RadTextBox txtSensorD4Tag;
    protected RadTextBox txtSensorD4Warning;
    protected RadTextBox txtSensorD4Alarm;
    protected RadTextBox txtSensorD6Tag;
    protected RadTextBox txtSensorD6Warning;
    protected RadTextBox txtSensorD6Alarm;
    protected RadTextBox txtSensorD14Tag;
    protected RadTextBox txtSensorD14Warning;
    protected RadTextBox txtSensorD14Alarm;
    protected RadTextBox txtSensorD21Tag;
    protected RadTextBox txtSensorD21Warning;
    protected RadTextBox txtSensorD21Alarm;
    protected RadTextBox txtSensorDDate;
    protected RadTextBox txtSensorDFlow;
    protected RadTextBox txtSensorDTemp;
    protected RadTextBox txtSensorDRH;
    protected RadComboBox cmbSensorDProduct;
    protected RadTextBox txtSensorDMin;
    protected RadTextBox txtSensorDMax;
    protected Label lblSensorDUsers;
    protected Label lblUsers;
    protected CheckBoxList lstUsers;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (Roles.IsUserInRole("Admin"))
        this.IsFluidTraxAdmin = true;
      else if (Roles.IsUserInRole("CompanyAdmin"))
        this.IsFluidTraxAdmin = true;
      else
        this.ShowError("You do not have appropriate permission for this page.");
      this.toggles = new Panel[4]
      {
        this.pnlSensorAToggle,
        this.pnlSensorBToggle,
        this.pnlSensorCToggle,
        this.pnlSensorDToggle
      };
      this.panels = new Panel[4]
      {
        this.pnlSensorA,
        this.pnlSensorB,
        this.pnlSensorC,
        this.pnlSensorD
      };
      this.hids = new HiddenField[4]
      {
        this.hidSensorA,
        this.hidSensorB,
        this.hidSensorC,
        this.hidSensorD
      };
      this.sensorIdHids = new HiddenField[4]
      {
        this.hidSensorAId,
        this.hidSensorBId,
        this.hidSensorCId,
        this.hidSensorDId
      };
      this.sensorIndexHids = new HiddenField[4]
      {
        this.hidSensorAIndex,
        this.hidSensorBIndex,
        this.hidSensorCIndex,
        this.hidSensorDIndex
      };
      this.assetboxes = new RadComboBox[4]
      {
        this.cmbSensorAAsset,
        this.cmbSensorBAsset,
        this.cmbSensorCAsset,
        this.cmbSensorDAsset
      };
      this.productboxes = new RadComboBox[4]
      {
        this.cmbSensorAProduct,
        this.cmbSensorBProduct,
        this.cmbSensorCProduct,
        this.cmbSensorDProduct
      };
      int[] source = new int[0];
      for (int index1 = 0; index1 < 4; ++index1)
      {
        RadTextBox[] control = this.ControlList[index1];
        for (int index2 = 0; index2 < ((IEnumerable<RadTextBox>) control).Count<RadTextBox>(); ++index2)
        {
          if (index2 > 0)
          {
            RadTextBox radTextBox = control[index2];
            if (!((IEnumerable<int>) source).Contains<int>(index2))
              radTextBox.Enabled = false;
          }
        }
      }
      if (this.Request.Params["unit_id"] == null || !int.TryParse(this.Request.Params["unit_id"], out this.unit_id))
        this.ShowError("No unit parameter");
      if (this.IsPostBack)
        return;
      this.PopulateFields(Utils.RefreshUnits(this.IsFluidTraxAdmin));
      Dictionary<string, string> usersForSelectList = Utils.GetValidUsersForSelectList();
      List<string> unitUsers = Utils.GetUnitUsers(this.unit_id);
      bool flag = Roles.IsUserInRole("Admin");
      foreach (string key in usersForSelectList.Keys)
      {
        ListItem listItem = !flag ? new ListItem(key, usersForSelectList[key]) : new ListItem(key ?? "", usersForSelectList[key]);
        if (unitUsers.Contains(key))
          listItem.Selected = true;
        this.lstUsers.Items.Add(listItem);
      }
    }

    private void RefreshUnits()
    {
      DataTable dataTable = Utils.RefreshUnits(true);
      ConfigureUnit.assets = new Dictionary<string, string>();
      ConfigureUnit.fluids = new Dictionary<string, string>();
      ConfigureUnit.locations = new Dictionary<string, string>();
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        if (!ConfigureUnit.assets.ContainsKey((string) row["AssetName"]))
          ConfigureUnit.assets.Add((string) row["AssetName"], (string) row["AssetName"]);
        if (!ConfigureUnit.locations.ContainsKey((string) row["AssetLocation"]))
          ConfigureUnit.locations.Add((string) row["AssetLocation"], (string) row["AssetLocation"]);
        if (!ConfigureUnit.fluids.ContainsKey((string) row["FluidType"]))
          ConfigureUnit.fluids.Add((string) row["FluidType"], (string) row["FluidType"]);
      }
    }

    private void SetDropDown(RadDropDownList cmb, string setting)
    {
      foreach (DropDownListItem dropDownListItem in (StateManagedCollection) cmb.Items)
      {
        if (dropDownListItem.Text == setting)
        {
          dropDownListItem.Selected = true;
          return;
        }
      }
      cmb.SelectedIndex = 0;
    }

    private void SetDropDown(RadComboBox cmb, string setting)
    {
      foreach (RadComboBoxItem radComboBoxItem in (StateManagedCollection) cmb.Items)
      {
        if (radComboBoxItem.Text == setting)
        {
          radComboBoxItem.Selected = true;
          return;
        }
      }
      cmb.Text = setting;
    }

    private List<RadTextBox[]> ControlList
    {
      get
      {
        return new List<RadTextBox[]>()
        {
          new RadTextBox[19]
          {
            this.txtSensorAName,
            this.txtSensorA4Tag,
            this.txtSensorA4Warning,
            this.txtSensorA4Alarm,
            this.txtSensorA6Tag,
            this.txtSensorA6Warning,
            this.txtSensorA6Alarm,
            this.txtSensorA14Tag,
            this.txtSensorA14Warning,
            this.txtSensorA14Alarm,
            this.txtSensorA21Tag,
            this.txtSensorA21Warning,
            this.txtSensorA21Alarm,
            this.txtSensorADate,
            this.txtSensorAFlow,
            this.txtSensorAMin,
            this.txtSensorAMax,
            this.txtSensorATemp,
            this.txtSensorARH
          },
          new RadTextBox[19]
          {
            this.txtSensorBName,
            this.txtSensorB4Tag,
            this.txtSensorB4Warning,
            this.txtSensorC4Alarm,
            this.txtSensorB6Tag,
            this.txtSensorB6Warning,
            this.txtSensorB6Alarm,
            this.txtSensorB14Tag,
            this.txtSensorB14Warning,
            this.txtSensorB14Alarm,
            this.txtSensorB21Tag,
            this.txtSensorB21Warning,
            this.txtSensorB21Alarm,
            this.txtSensorBDate,
            this.txtSensorBFlow,
            this.txtSensorBMin,
            this.txtSensorBMax,
            this.txtSensorBTemp,
            this.txtSensorBRH
          },
          new RadTextBox[19]
          {
            this.txtSensorCName,
            this.txtSensorC4Tag,
            this.txtSensorC4Warning,
            this.txtSensorB4Alarm,
            this.txtSensorC6Tag,
            this.txtSensorC6Warning,
            this.txtSensorC6Alarm,
            this.txtSensorC14Tag,
            this.txtSensorC14Warning,
            this.txtSensorC14Alarm,
            this.txtSensorC21Tag,
            this.txtSensorC21Warning,
            this.txtSensorC21Alarm,
            this.txtSensorCDate,
            this.txtSensorCFlow,
            this.txtSensorCMin,
            this.txtSensorCMax,
            this.txtSensorCTemp,
            this.txtSensorCRH
          },
          new RadTextBox[19]
          {
            this.txtSensorDName,
            this.txtSensorD4Tag,
            this.txtSensorD4Warning,
            this.txtSensorD4Alarm,
            this.txtSensorD6Tag,
            this.txtSensorD6Warning,
            this.txtSensorD6Alarm,
            this.txtSensorD14Tag,
            this.txtSensorD14Warning,
            this.txtSensorD14Alarm,
            this.txtSensorD21Tag,
            this.txtSensorD21Warning,
            this.txtSensorD21Alarm,
            this.txtSensorDDate,
            this.txtSensorDFlow,
            this.txtSensorDMin,
            this.txtSensorDMax,
            this.txtSensorDTemp,
            this.txtSensorDRH
          }
        };
      }
    }

    private void PopulateFields(DataTable tbl)
    {
      List<string> stringList1 = new List<string>();
      List<string> stringList2 = new List<string>();
      List<string> stringList3 = new List<string>();
      foreach (DataRow row in (InternalDataCollectionBase) tbl.Rows)
      {
        foreach (Sensor sensor in (List<Sensor>) row["Sensors"])
        {
          if (sensor.SensorCategory1 != null && sensor.SensorCategory1 != "" && !stringList1.Contains(sensor.SensorCategory1))
            stringList1.Add(sensor.SensorCategory1);
          if (sensor.SensorCategory2 != null && sensor.SensorCategory2 != "" && !stringList3.Contains(sensor.SensorCategory2))
            stringList3.Add(sensor.SensorCategory2);
        }
        string str = (string) row["UnitCategory"];
        if (str != null && str != "" && !stringList2.Contains(str))
          stringList2.Add(str);
      }
      stringList2.Sort();
      stringList1.Sort();
      stringList3.Sort();
      foreach (RadComboBox assetbox in this.assetboxes)
      {
        assetbox.DataSource = (object) stringList1.ToArray();
        assetbox.DataBind();
      }
      foreach (RadComboBox productbox in this.productboxes)
      {
        productbox.DataSource = (object) stringList3.ToArray();
        productbox.DataBind();
      }
      this.cmbLocation1.DataSource = (object) stringList2;
      this.cmbLocation1.DataBind();
      this.cmbLocation1.AllowCustomText = true;
      this.cmbCompany.DataSource = (object) Utils.GetCompanyList();
      this.cmbCompany.DataBind();
      DataRow dataRow = (DataRow) null;
      foreach (DataRow row in (InternalDataCollectionBase) tbl.Rows)
      {
        if ((int) row["UnitID"] == this.unit_id)
          dataRow = row;
      }
      if (dataRow == null)
        this.ShowError("Could not retrieve unit with id " + this.unit_id.ToString());
      this.txtUnitName.Text = (string) dataRow["Name"];
      this.chkBoxIsMobile.Checked = dataRow["IsMobile"] != null && (bool) dataRow["IsMobile"];
      this.SetDropDown(this.cmbCompany, (string) dataRow["CompanyName"]);
      if (Roles.IsUserInRole("CompanyAdmin"))
        this.cmbCompany.Enabled = false;
      this.SetDropDown(this.cmbLocation1, (string) dataRow["UnitCategory"]);
      this.txtVlinkName.Text = (string) dataRow["VLinkName"];
      this.txtVlinkSerial.Text = (string) dataRow["SerialNumber"];
      for (int index = 0; index < 4; ++index)
        this.TogglePanel(false, this.toggles[index], this.panels[index], this.hids[index], this.sensorIdHids[index], -1);
      int index1 = 0;
      foreach (Sensor sensor in ((IEnumerable<Sensor>) dataRow["Sensors"]).OrderBy<Sensor, short?>((System.Func<Sensor, short?>) (x => x.SensorIndex)).ToList<Sensor>())
      {
        Decimal num1 = 0.0M;
        Decimal num2 = 40.0M;
        if (sensor.SType == SensorType.ParticleCount)
        {
          RadTextBox[] control = this.ControlList[index1];
          control[0].Text = sensor.Name;
          if (this.chkBoxIsMobile.Checked)
          {
            this.productboxes[index1].DefaultItem.Text = "Mobile";
            this.productboxes[index1].DefaultItem.Value = "-1";
            this.SetDropDown(this.productboxes[index1], "Mobile");
            this.productboxes[index1].Enabled = false;
            this.assetboxes[index1].DefaultItem.Text = "ISOTote";
            this.assetboxes[index1].DefaultItem.Value = "ISOTote";
            this.SetDropDown(this.assetboxes[index1], "ISOTote");
            this.assetboxes[index1].Enabled = false;
          }
          else
          {
            this.productboxes[index1].Text = sensor.SensorCategory2;
            this.SetDropDown(this.productboxes[index1], sensor.SensorCategory2);
            this.productboxes[index1].Enabled = true;
            this.productboxes[index1].AllowCustomText = true;
            this.SetDropDown(this.assetboxes[index1], sensor.SensorCategory1);
            this.assetboxes[index1].Enabled = true;
            this.assetboxes[index1].AllowCustomText = true;
          }
          foreach (Channel channel in sensor.Channels)
          {
            int index2 = -1;
            if (channel.Name == "ISO-4")
              index2 = 1;
            if (channel.Name == "ISO-6")
              index2 = 4;
            if (channel.Name == "ISO-14")
              index2 = 7;
            if (channel.Name == "ISO-21")
              index2 = 10;
            if (index2 != -1)
            {
              SensorRange sensorRange1 = (SensorRange) null;
              SensorRange sensorRange2 = (SensorRange) null;
              foreach (SensorRange range in channel.Ranges)
              {
                if (range.LimitType == (byte) 10)
                  sensorRange2 = range;
                if (range.LimitType == (byte) 11)
                  sensorRange1 = range;
              }
              control[index2].Text = channel.ChannelIndex.ToString();
              if (sensorRange1 != null)
                control[index2 + 1].Text = sensorRange1.LimitValue.ToString("0");
              else
                control[index2 + 1].Text = "18";
              if (sensorRange2 != null)
                control[index2 + 2].Text = sensorRange2.LimitValue.ToString("0");
              else
                control[index2 + 2].Text = "24";
              num1 = channel.Min;
              num2 = channel.Max;
              short channelIndex;
              if (channel.Name == "Temp")
              {
                RadTextBox radTextBox = control[17];
                channelIndex = channel.ChannelIndex;
                string str = channelIndex.ToString();
                radTextBox.Text = str;
              }
              if (channel.Name == "RH")
              {
                RadTextBox radTextBox = control[18];
                channelIndex = channel.ChannelIndex;
                string str = channelIndex.ToString();
                radTextBox.Text = str;
              }
            }
          }
          control[15].Text = num1.ToString("0.0");
          control[16].Text = num2.ToString("0.0");
          if (this.chkBoxIsMobile.Checked)
          {
            short? sensorIndex = sensor.SensorIndex;
            int num3 = sensorIndex.HasValue ? 1 : 0;
            sensorIndex = sensor.SensorIndex;
            int? nullable = sensorIndex.HasValue ? new int?((int) sensorIndex.GetValueOrDefault()) : new int?();
            int num4 = 0;
            int num5 = nullable.GetValueOrDefault() > num4 & nullable.HasValue ? 1 : 0;
            if ((num3 & num5) != 0)
            {
              this.TogglePanel(false, this.toggles[index1], this.panels[index1], this.hids[index1], this.sensorIdHids[index1], sensor.SensorID);
              goto label_78;
            }
          }
          this.TogglePanel(sensor.Active, this.toggles[index1], this.panels[index1], this.hids[index1], this.sensorIdHids[index1], sensor.SensorID);
label_78:
          ++index1;
        }
      }
    }

    private void TogglePanel(
      bool toggle,
      Panel pnlToggle,
      Panel pnlSensor,
      HiddenField hid,
      HiddenField sensorIdField,
      int sensorId)
    {
      if (toggle)
      {
        pnlToggle.CssClass = "divToggleOn";
        pnlSensor.CssClass = "divSensorVisible";
        hid.Value = "1";
      }
      else
      {
        pnlToggle.CssClass = "divToggleOff";
        pnlSensor.CssClass = "divSensorHidden";
        hid.Value = "0";
      }
      sensorIdField.Value = sensorId.ToString();
    }

    private bool GetToggleStatus(int index)
    {
      if (this.hids[index].Value == "1")
        return true;
      if (this.hids[index].Value == "0")
        return false;
      throw new ArgumentOutOfRangeException("Hidden Value Not Set");
    }

    private void ShowError(string message)
    {
      this.Response.Redirect("~/Error.aspx?message=" + message);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      List<string> errors = new List<string>();
      if (Utils.CleanText(this.cmbLocation1.Text).Length < 3)
        errors.Add("Must include a location name longer than 2 valid characters");
      List<string> stringList = new List<string>();
      int result1;
      for (int index1 = 0; index1 < 4; ++index1)
      {
        if (this.GetToggleStatus(index1))
        {
          RadTextBox[] control = this.ControlList[index1];
          control[0].Text = Utils.CleanText(control[0].Text);
          if (stringList.Contains(control[0].Text))
            errors.Add("Sensor " + (index1 + 1).ToString() + " does not have a name");
          if (Utils.CleanText(this.assetboxes[index1].Text).Length < 3)
            errors.Add("Sensor " + (index1 + 1).ToString() + " does not have a valid asset class (longer than 2 valid characters)");
          if (Utils.CleanText(this.productboxes[index1].Text).Length < 3)
            errors.Add("Sensor " + (index1 + 1).ToString() + " does not have a valid product name (longer than 2 valid characters");
          stringList.Add(control[0].Text);
          if (control[0].Text == "")
            errors.Add("Sensor " + (index1 + 1).ToString() + " does not have a name");
          for (int index2 = 1; index2 < 17; ++index2)
          {
            if (index2 != 2 && index2 != 3 && index2 != 5 && index2 != 6 && index2 != 8 && index2 != 9 && index2 != 11 && index2 != 12 || !(control[index2].Text == ""))
            {
              if (index2 == 15 || index2 == 16)
              {
                if (!Decimal.TryParse(control[index2].Text, out Decimal _))
                  errors.Add("Invalid min/max value in Sensor " + (index1 + 1).ToString());
              }
              else if (!int.TryParse(control[index2].Text, out result1))
                errors.Add("Invalid value in Sensor " + (index1 + 1).ToString());
              else if (result1 < 0 || result1 > 256)
                errors.Add("Value out of range in Sensor " + (index1 + 1).ToString());
            }
          }
        }
      }
      if (errors.Count > 0)
      {
        this.litError.Text = Utils.MakeErrorList(errors, "The configuration could not be saved. The following errors were found");
      }
      else
      {
        List<string> userIds1 = new List<string>();
        List<string> userIds2 = new List<string>();
        foreach (ListItem listItem in this.lstUsers.Items)
        {
          userIds2.Add(listItem.Value);
          if (listItem.Selected)
            userIds1.Add(listItem.Value);
        }
        UnitUserRepo.RemoveUsersFromUnit(this.unit_id, userIds2);
        UnitUserRepo.AddUsersToUnit(this.unit_id, userIds1);
        string text = this.cmbLocation1.Text;
        if (text == "")
        {
          this.litError.Text = "You must enter a valid location";
        }
        else
        {
          int num1 = int.Parse(this.cmbCompany.SelectedValue);
          int num2 = int.Parse(this.txtWorkOrderTag.Text);
          int num3 = int.Parse(this.txtTechnicianTag.Text);
          int num4 = int.Parse(this.txtSerialNumberTag.Text);
          int num5 = int.Parse(this.txtFluidTypeTag.Text);
          int num6 = int.Parse(this.txtHoursMilesTag.Text);
          using (SqlConnection conn = Utils.GetConn())
          {
            string cmdText1 = "DELETE FROM Range WHERE RangeID IN (SELECT RangeID FROM Range r INNER JOIN Channel c ON r.ChannelID=c.ChannelID INNER JOIN Sensor s ON s.SensorID=c.SensorID WHERE s.UnitID=" + this.unit_id.ToString() + ")";
            string cmdText2 = "DELETE FROM Channel WHERE ChannelID IN (SELECT ChannelID FROM Channel c INNER JOIN Sensor s ON s.SensorID=c.SensorID WHERE s.UnitID=" + this.unit_id.ToString() + ")";
            SqlConnection connection = conn;
            using (SqlCommand sqlCommand = new SqlCommand(cmdText1, connection))
              sqlCommand.ExecuteNonQuery();
            using (SqlCommand sqlCommand = new SqlCommand(cmdText2, conn))
              sqlCommand.ExecuteNonQuery();
            string str = "UPDATE Unit SET UnitName=@unitname, UnitCategory=@location, CompanyID=@cid, Active=@active";
            if (this.chkBoxIsMobile.Checked)
              str += " ,WorkOrderTag=@workOrderTag, TechnicianIdTag=@technicianIdTag, FluidTypeTag=@fluidTypeTag, SerialNumberTag=@serialNumberTag, HoursMilesTag=@hoursMilesTag";
            using (SqlCommand sqlCommand = new SqlCommand(str + " WHERE UnitID=@unitid", conn))
            {
              sqlCommand.Parameters.AddWithValue("@location", (object) text);
              sqlCommand.Parameters.AddWithValue("@unitname", (object) Utils.CleanText(this.txtUnitName.Text));
              sqlCommand.Parameters.AddWithValue("@cid", (object) num1);
              sqlCommand.Parameters.AddWithValue("@unitid", (object) this.unit_id);
              sqlCommand.Parameters.AddWithValue("@active", (object) true);
              if (this.chkBoxIsMobile.Checked)
              {
                sqlCommand.Parameters.AddWithValue("@workOrderTag", (object) num2);
                sqlCommand.Parameters.AddWithValue("@technicianIdTag", (object) num3);
                sqlCommand.Parameters.AddWithValue("@fluidTypeTag", (object) num5);
                sqlCommand.Parameters.AddWithValue("@serialNumberTag", (object) num4);
                sqlCommand.Parameters.AddWithValue("@hoursMilesTag", (object) num6);
              }
              sqlCommand.ExecuteNonQuery();
            }
            for (int index3 = 0; index3 < 4; ++index3)
            {
              num9 = int.Parse(this.sensorIdHids[index3].Value);
              RadTextBox[] control = this.ControlList[index3];
              bool toggleStatus = this.GetToggleStatus(index3);
              short num7 = short.Parse(this.sensorIndexHids[index3].Value);
              if (num9 > 0)
              {
                string cmdText3;
                if (toggleStatus)
                  cmdText3 = "UPDATE Sensor SET UnitID=@unitid, SensorName=@sn" + index3.ToString() + ", SensorCategory1=@a" + index3.ToString() + ", SensorCategory2=@p" + index3.ToString() + ", Active=@active" + index3.ToString() + ", SensorIndex=@sIndex" + index3.ToString() + " WHERE SensorID=@sid" + index3.ToString() + "; SELECT sensorid FROM Sensor WHERE unitid=@unitid AND SensorID=@sid" + index3.ToString();
                else
                  cmdText3 = "UPDATE Sensor SET UnitID=@unitid, Active=@active" + index3.ToString() + ", SensorIndex=@sIndex" + index3.ToString() + " WHERE SensorID=@sid" + index3.ToString() + "; SELECT sensorid FROM Sensor WHERE unitid=@unitid AND SensorID=@sid" + index3.ToString();
                using (SqlCommand sqlCommand = new SqlCommand(cmdText3, conn))
                {
                  sqlCommand.Parameters.AddWithValue("@active" + index3.ToString(), (object) (toggleStatus ? 1 : 0));
                  sqlCommand.Parameters.AddWithValue("@sIndex" + index3.ToString(), (object) num7);
                  sqlCommand.Parameters.AddWithValue("@sid" + index3.ToString(), (object) num9);
                  sqlCommand.Parameters.AddWithValue("@unitid", (object) this.unit_id);
                  if (toggleStatus)
                  {
                    sqlCommand.Parameters.AddWithValue("@sn" + index3.ToString(), (object) Utils.CleanText(control[0].Text));
                    sqlCommand.Parameters.AddWithValue("@p" + index3.ToString(), (object) Utils.CleanText(this.productboxes[index3].Text));
                    sqlCommand.Parameters.AddWithValue("@a" + index3.ToString(), (object) Utils.CleanText(this.assetboxes[index3].Text));
                  }
                  object obj = sqlCommand.ExecuteScalar();
                  if (obj == DBNull.Value || !(obj is int num9))
                  {
                    this.litError.Text = "Database error: could not find asset id";
                    return;
                  }
                }
              }
              else
              {
                using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Sensor (UnitId, SensorName, SensorCategory1, SensorCategory2, SensorIndex, Active) VALUES(@unitid, @sn" + index3.ToString() + ", @a" + index3.ToString() + ", @p" + index3.ToString() + ", @sIndex" + index3.ToString() + ", @active" + index3.ToString() + "); SELECT sensorid FROM Sensor WHERE unitid=@unitid AND SensorIndex=@sIndex" + index3.ToString(), conn))
                {
                  sqlCommand.Parameters.AddWithValue("@sn" + index3.ToString(), (object) Utils.CleanText(control[0].Text));
                  sqlCommand.Parameters.AddWithValue("@p" + index3.ToString(), (object) Utils.CleanText(this.productboxes[index3].Text));
                  sqlCommand.Parameters.AddWithValue("@a" + index3.ToString(), (object) Utils.CleanText(this.assetboxes[index3].Text));
                  sqlCommand.Parameters.AddWithValue("@active" + index3.ToString(), (object) (toggleStatus ? 1 : 0));
                  sqlCommand.Parameters.AddWithValue("@unitid", (object) this.unit_id);
                  sqlCommand.Parameters.AddWithValue("@sIndex" + index3.ToString(), (object) num7);
                  object obj = sqlCommand.ExecuteScalar();
                  if (obj == DBNull.Value || !(obj is int num9))
                  {
                    this.litError.Text = "Database error: could not find asset id";
                    return;
                  }
                }
              }
              for (int index4 = 0; index4 < 4; ++index4)
              {
                num10 = -1;
                using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Channel (SensorID, ChannelName, ChannelIndex, Min, Max) VALUES (@sensorid, @cn, @ci, @min, @max); SELECT ChannelID FROM Channel WHERE SensorID=@sensorid AND ChannelName=@cn;", conn))
                {
                  sqlCommand.Parameters.AddWithValue("@sensorid", (object) num9);
                  sqlCommand.Parameters.AddWithValue("@cn", (object) Utils.IsoLabels[index4]);
                  sqlCommand.Parameters.AddWithValue("@ci", (object) short.Parse(control[1 + index4 * 3].Text));
                  sqlCommand.Parameters.AddWithValue("@min", (object) Decimal.Parse(control[15].Text));
                  sqlCommand.Parameters.AddWithValue("@max", (object) Decimal.Parse(control[16].Text));
                  object obj = sqlCommand.ExecuteScalar();
                  if (obj == DBNull.Value || !(obj is int num10))
                  {
                    this.litError.Text = "Database error: could not find asset id";
                    return;
                  }
                }
                int val1 = int.MaxValue;
                string cmdText4 = "INSERT INTO Range (ChannelID, LimitType, LimitValue) VALUES (@channelid, @lt, @lv)";
                if (control[index4 * 3 + 2].Text != "" && int.TryParse(control[index4 * 3 + 2].Text, out result1))
                {
                  val1 = Math.Min(val1, result1);
                  using (SqlCommand sqlCommand = new SqlCommand(cmdText4, conn))
                  {
                    sqlCommand.Parameters.AddWithValue("@channelid", (object) num10);
                    sqlCommand.Parameters.AddWithValue("@lt", (object) (byte) 11);
                    sqlCommand.Parameters.AddWithValue("@lv", (object) (Decimal) result1);
                  }
                }
                if (control[index4 * 3 + 3].Text != "" && int.TryParse(control[index4 * 3 + 3].Text, out result1))
                {
                  val1 = Math.Min(val1, result1);
                  using (SqlCommand sqlCommand = new SqlCommand(cmdText4, conn))
                  {
                    sqlCommand.Parameters.AddWithValue("@channelid", (object) num10);
                    sqlCommand.Parameters.AddWithValue("@lt", (object) (byte) 10);
                    sqlCommand.Parameters.AddWithValue("@lv", (object) (Decimal) result1);
                  }
                }
                if (val1 != int.MaxValue)
                {
                  using (SqlCommand sqlCommand = new SqlCommand(cmdText4, conn))
                  {
                    sqlCommand.Parameters.AddWithValue("@channelid", (object) num10);
                    sqlCommand.Parameters.AddWithValue("@lt", (object) (byte) 3);
                    sqlCommand.Parameters.AddWithValue("@lv", (object) (Decimal) val1);
                  }
                }
              }
              short result2;
              if (control[14].Text != "" && short.TryParse(control[14].Text, out result2))
              {
                using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Channel (SensorID, ChannelName, ChannelIndex, Min, Max) VALUES (@sensorid, @cn, @ci, @min, @max); SELECT ChannelID FROM Channel WHERE SensorID=@sensorid AND ChannelName=@cn;", conn))
                {
                  sqlCommand.Parameters.AddWithValue("@sensorid", (object) num9);
                  sqlCommand.Parameters.AddWithValue("@cn", (object) "alt");
                  sqlCommand.Parameters.AddWithValue("@ci", (object) result2);
                  sqlCommand.Parameters.AddWithValue("@min", (object) 0);
                  sqlCommand.Parameters.AddWithValue("@max", (object) 100);
                  object obj = sqlCommand.ExecuteScalar();
                  if (obj == DBNull.Value || !(obj is int _))
                  {
                    this.litError.Text = "Database error: could not insert flow channel";
                    return;
                  }
                }
              }
              short result3;
              if (control[17].Text != "" && short.TryParse(control[17].Text, out result3))
              {
                using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Channel (SensorID, ChannelName, ChannelIndex, Min, Max) VALUES (@sensorid, @cn, @ci, @min, @max); SELECT ChannelID FROM Channel WHERE SensorID=@sensorid AND ChannelName=@cn;", conn))
                {
                  sqlCommand.Parameters.AddWithValue("@sensorid", (object) num9);
                  sqlCommand.Parameters.AddWithValue("@cn", (object) "Temp");
                  sqlCommand.Parameters.AddWithValue("@ci", (object) result3);
                  sqlCommand.Parameters.AddWithValue("@min", (object) 0);
                  sqlCommand.Parameters.AddWithValue("@max", (object) 100);
                  object obj = sqlCommand.ExecuteScalar();
                  if (obj == DBNull.Value || !(obj is int _))
                  {
                    this.litError.Text = "Database error: could not insert Temp channel";
                    return;
                  }
                }
              }
              short result4;
              if (control[18].Text != "" && short.TryParse(control[18].Text, out result4))
              {
                using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Channel (SensorID, ChannelName, ChannelIndex, Min, Max) VALUES (@sensorid, @cn, @ci, @min, @max); SELECT ChannelID FROM Channel WHERE SensorID=@sensorid AND ChannelName=@cn;", conn))
                {
                  sqlCommand.Parameters.AddWithValue("@sensorid", (object) num9);
                  sqlCommand.Parameters.AddWithValue("@cn", (object) "RH");
                  sqlCommand.Parameters.AddWithValue("@ci", (object) result4);
                  sqlCommand.Parameters.AddWithValue("@min", (object) 0);
                  sqlCommand.Parameters.AddWithValue("@max", (object) 100);
                  object obj = sqlCommand.ExecuteScalar();
                  if (obj == DBNull.Value || !(obj is int _))
                  {
                    this.litError.Text = "Database error: could not insert RH channel";
                    return;
                  }
                }
              }
            }
          }
          this.litError.Text = "The configuration has been saved";
          this.PopulateFields(Utils.RefreshUnits(this.IsFluidTraxAdmin));
        }
      }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
      this.PopulateFields(Utils.RefreshUnits(true));
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
      this.Response.Redirect("~/Configure.aspx");
    }

    protected void chkBoxIsMobile_CheckedChanged(object sender, EventArgs e)
    {
      using (SqlConnection conn = Utils.GetConn())
      {
        using (SqlCommand sqlCommand = new SqlCommand("UPDATE Unit SET IsMobile=@isMobile, WorkOrderTag=@workOrderTag, TechnicianIdTag=@techIdTag, FluidTypeTag=@fluidTypeTag, SerialNumberTag=@serialNumTag, HoursMilesTag=@hoursMilesTag WHERE UnitID=@uid; SELECT UnitID FROM Unit WHERE UnitID=@uid", conn))
        {
          sqlCommand.Parameters.AddWithValue("@isMobile", (object) this.chkBoxIsMobile.Checked);
          sqlCommand.Parameters.AddWithValue("@uid", (object) this.unit_id);
          sqlCommand.Parameters.AddWithValue("@workOrderTag", this.chkBoxIsMobile.Checked ? (object) 53 : (object) DBNull.Value);
          sqlCommand.Parameters.AddWithValue("@techIdTag", this.chkBoxIsMobile.Checked ? (object) 54 : (object) DBNull.Value);
          sqlCommand.Parameters.AddWithValue("@fluidTypeTag", this.chkBoxIsMobile.Checked ? (object) 55 : (object) DBNull.Value);
          sqlCommand.Parameters.AddWithValue("@serialNumTag", this.chkBoxIsMobile.Checked ? (object) 56 : (object) DBNull.Value);
          sqlCommand.Parameters.AddWithValue("@hoursMilesTag", this.chkBoxIsMobile.Checked ? (object) 57 : (object) DBNull.Value);
          object obj = sqlCommand.ExecuteScalar();
          if (obj != DBNull.Value)
          {
            if (obj is int)
              goto label_8;
          }
          this.litError.Text = "Database error: could not find unit id";
          return;
        }
label_8:
        using (SqlCommand sqlCommand = new SqlCommand("UPDATE Sensor SET SensorCategory2=@category2, SensorCategory1=@category1 WHERE UnitID=@uid; SELECT UnitID FROM Sensor WHERE UnitID=@uid", conn))
        {
          sqlCommand.Parameters.AddWithValue("@category2", this.chkBoxIsMobile.Checked ? (object) "Mobile" : (object) "Unknown");
          sqlCommand.Parameters.AddWithValue("@category1", this.chkBoxIsMobile.Checked ? (object) "ISOTote" : (object) "Unkown");
          sqlCommand.Parameters.AddWithValue("@uid", (object) this.unit_id);
          object obj = sqlCommand.ExecuteScalar();
          if (obj != DBNull.Value)
          {
            if (obj is int)
              goto label_18;
          }
          this.litError.Text = "Database error: could not find unit id";
          return;
        }
      }
label_18:
      this.PopulateFields(Utils.RefreshUnits(true));
    }

    private enum ControlListIndex
    {
      Name,
      Iso4Tag,
      Iso4Warning,
      Iso4Alarm,
      Iso6Tag,
      Iso6Warning,
      Iso6Alarm,
      Iso14Tag,
      Iso14Warning,
      Iso14Alarm,
      Iso21Tag,
      Iso21Warning,
      Iso21Alarm,
      Date,
      Flow,
      Min,
      Max,
      Temp,
      RH,
    }
  }
}
