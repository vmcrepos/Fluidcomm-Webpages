// Decompiled with JetBrains decompiler
// Type: GreenCo.Admin.Admin
// Assembly: GreenCo, Version=0.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F63B31C9-6FC1-4AED-A804-2C1309A8A158
// Assembly location: C:\Users\gregg\Desktop\Fluidcomm-Webpages\bin\GreenCo.dll

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace GreenCo.Admin
{
  public class Admin : Page
  {
    protected System.Web.UI.ScriptManager scriptman;
    protected ImageButton toolbarNew;
    protected ImageButton toolbarProcess;
    protected UpdatePanel updAdmin;
    protected GridView gridUsers;
    protected Label lblResult;
    protected Button btnTest;

    private Dictionary<int, string> Companies
    {
      get
      {
        if (this.Session["Admin_Companies"] == null)
          this.RefreshCompanies();
        return (Dictionary<int, string>) this.Session["Admin_Companies"];
      }
      set => this.Session["Admin_Companies"] = (object) value;
    }

    private Dictionary<string, string> Assignments
    {
      get
      {
        if (this.Session["Admin_Assignments"] == null)
          this.RefreshAssignments();
        return (Dictionary<string, string>) this.Session["Admin_Assignments"];
      }
      set => this.Session["Admin_Assignments"] = (object) value;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      this.RefreshCompanies();
      this.RefreshAssignments();
      this.lblResult.Text = "";
      this.btnTest.Visible = false;
      if (this.IsPostBack)
        return;
      this.UpdateUserTable();
    }

    private void RefreshCompanies()
    {
      this.Session["Admin_Companies"] = (object) Utils.GetCompanyList();
    }

    private void UpdateUserTable()
    {
      this.gridUsers.DataSource = (object) Membership.GetAllUsers();
      this.gridUsers.DataBind();
    }

    private void RefreshAssignments()
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      string cmdText = "SELECT u.LoweredUserName, us.Setting FROM aspnet_users u LEFT JOIN UserSettings us ON us.UserID=u.UserID AND us.Property = 'Company'";
      using (SqlConnection conn = Utils.GetConn())
      {
        using (SqlCommand sqlCommand = new SqlCommand(cmdText, conn))
        {
          using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
          {
            while (sqlDataReader.Read())
              dictionary.Add(sqlDataReader.GetString(0), sqlDataReader.GetString(1));
          }
        }
      }
      this.Session["Admin_Assignments"] = (object) dictionary;
    }

    protected void gridUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      int rowType = (int) e.Row.RowType;
      if (e.Row.RowType != DataControlRowType.DataRow)
        return;
      MembershipUser user = Membership.GetUser(e.Row.Cells[0].Text);
      ImageButton control1 = (ImageButton) e.Row.Cells[5].Controls[1];
      if (this.gridUsers.DataKeys[e.Row.RowIndex]["IsLockedOut"].ToString() == "True")
      {
        control1.ImageUrl = "~/Images/lock.png";
        control1.ToolTip = "This user is locked out. Click to unlock";
        control1.Enabled = true;
      }
      else
      {
        control1.ImageUrl = "~/Images/lock_open.png";
        control1.ToolTip = "This user is not locked out.";
        control1.Enabled = false;
      }
      ImageButton control2 = (ImageButton) e.Row.Cells[5].Controls[3];
      if (user.IsApproved)
      {
        control2.ImageUrl = "~/Images/Accept.png";
        control2.ToolTip = "This user is enabled. Click to disable.";
      }
      else
      {
        control2.ImageUrl = "~/Images/Cancel.png";
        control2.ToolTip = "This user is disabled. Click to enable.";
      }
      ((WebControl) e.Row.Cells[5].Controls[5]).ToolTip = "Delete user";
      ((WebControl) e.Row.Cells[5].Controls[7]).ToolTip = "Reset Password";
      DropDownList control3 = (DropDownList) e.Row.Cells[6].Controls[1];
      control3.DataSource = (object) this.Companies;
      control3.DataBind();
      control3.SelectedValue = this.Assignments[user.UserName.ToLower()].ToString();
      ((CheckBox) e.Row.Cells[7].Controls[0]).Checked = Roles.IsUserInRole(user.UserName, "CompanyAdmin");
      ((CheckBox) e.Row.Cells[8].Controls[0]).Checked = Roles.IsUserInRole(user.UserName, nameof (Admin));
    }

    protected void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList dropDownList = (DropDownList) sender;
      MembershipUser user = Membership.GetUser(((TableRow) dropDownList.NamingContainer).Cells[0].Text);
      if (user == null)
        return;
      try
      {
        Guid providerUserKey = (Guid) user.ProviderUserKey;
        string cmdText = "DELETE FROM UserSettings WHERE UserID = @userid AND Property='Company';" + " INSERT INTO UserSettings (UserID, Property, Setting) VALUES (@userid, 'Company', @companyId)";
        using (SqlConnection conn = Utils.GetConn())
        {
          using (SqlCommand sqlCommand = new SqlCommand(cmdText, conn))
          {
            sqlCommand.Parameters.AddWithValue("@userid", (object) providerUserKey);
            sqlCommand.Parameters.AddWithValue("@companyId", (object) dropDownList.SelectedValue);
            sqlCommand.ExecuteNonQuery();
          }
        }
      }
      catch (Exception ex)
      {
        this.lblResult.Text = "A problem occurred when assigning the company to this user. The user has not been created.";
        return;
      }
      this.UpdateUserTable();
    }

    protected void toolbarNew_Click(object sender, EventArgs e)
    {
      this.Response.Redirect("NewUser.aspx", false);
    }

    protected void toolbarProcess_Click(object sender, EventArgs e)
    {
      this.Response.Redirect("~/UpdateData.aspx");
    }

    protected void btnLock_Click(object sender, EventArgs e)
    {
      MembershipUser user = Membership.GetUser(((TableRow) ((Control) sender).NamingContainer).Cells[0].Text);
      if (user == null)
        return;
      user.UnlockUser();
      Membership.UpdateUser(user);
      this.UpdateUserTable();
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
      MembershipUser user = Membership.GetUser(((TableRow) ((Control) sender).NamingContainer).Cells[0].Text);
      if (user == null)
        return;
      user.IsApproved = !user.IsApproved;
      Membership.UpdateUser(user);
      user.LastLoginDate = DateTime.Now;
      this.UpdateUserTable();
    }

    protected void btnResetPassword_Click(object sender, EventArgs e)
    {
      string text = ((TableRow) ((Control) sender).NamingContainer).Cells[0].Text;
      MembershipUserCollection allUsers = Membership.GetAllUsers();
      string str = "";
      for (int index = 0; index < 100; ++index)
      {
        str = allUsers[text].ResetPassword();
        if (!char.IsLetter(str.Substring(0, 1).ToArray<char>()[0]))
        {
          if (index == 99)
            throw new VlinkException("Cannot generate account password");
        }
        else
          break;
      }
      try
      {
        string body = "The password for your FluidTrax account has been reset. Your new temporary password is: <b>" + str + "</b>.<br>Please log onto FluidTrax and change your password.";
        MailHelper.SendMailMessage("admin@vmcnet.com", allUsers[text].Email, (string) null, (string) null, "FluidTrax Password Change", body);
      }
      catch (Exception ex)
      {
        this.lblResult.Text = "Could not email user. Temporary password for user " + text + " is " + str;
      }
    }

    protected void btnRemove_Click(object sender, EventArgs e)
    {
      Membership.DeleteUser(((TableRow) ((Control) sender).NamingContainer).Cells[0].Text);
      this.UpdateUserTable();
    }

    protected void btnTest_Click(object sender, EventArgs e)
    {
      string email = Membership.GetUser(((TableRow) ((Control) sender).NamingContainer).Cells[0].Text).Email;
      try
      {
        MailHelper.SendMailMessage((string) null, email, (string) null, (string) null, "Test Email", "This is a test email from the VMC-TankerManagementPlatform web application");
        this.lblResult.Text = "Sent mail!";
      }
      catch (Exception ex)
      {
        this.lblResult.Text = ex.Message;
      }
    }

    protected void btnEmail_Click(object sender, EventArgs e)
    {
      this.Session["username"] = (object) ((TableRow) ((Control) sender).NamingContainer).Cells[0].Text;
      this.Response.Redirect("Email.aspx");
    }

    protected void chkCustomerAdmin_CheckedChanged(object sender, EventArgs e)
    {
      CheckBox checkBox = (CheckBox) sender;
      string text = ((TableRow) checkBox.NamingContainer).Cells[0].Text;
      if (checkBox.Checked)
        Roles.AddUserToRole(text, "CompanyAdmin");
      else
        Roles.RemoveUserFromRole(text, "CompanyAdmin");
    }

    protected void chkAdmin_CheckedChanged(object sender, EventArgs e)
    {
      CheckBox checkBox = (CheckBox) sender;
      string text = ((TableRow) checkBox.NamingContainer).Cells[0].Text;
      if (checkBox.Checked)
        Roles.AddUserToRole(text, nameof (Admin));
      else
        Roles.RemoveUserFromRole(text, nameof (Admin));
    }
  }
}
