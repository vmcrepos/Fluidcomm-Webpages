// Decompiled with JetBrains decompiler
// Type: GreenCo.Admin.NewUser
// Assembly: GreenCo, Version=0.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F63B31C9-6FC1-4AED-A804-2C1309A8A158
// Assembly location: C:\Users\gregg\Desktop\Fluidcomm-Webpages\bin\GreenCo.dll

using System;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace GreenCo.Admin
{
  public class NewUser : Page
  {
    protected Label lblTitle;
    protected Label lblName;
    protected TextBox txtName;
    protected RequiredFieldValidator vldNameRequired;
    protected RegularExpressionValidator vldNameRegular;
    protected Label lblEmail;
    protected TextBox txtEmail;
    protected Label lblPassword1;
    protected TextBox txtPassword1;
    protected Label lblPassword2;
    protected TextBox txtPassword2;
    protected RequiredFieldValidator vldPasswordRequired1;
    protected RequiredFieldValidator vldPasswordRequired2;
    protected CompareValidator vldPasswordCompare;
    protected Label lblCompany;
    protected DropDownList cmbCompany;
    protected Button btnUserOK;
    protected Button btnUserCancel;
    protected ValidationSummary ValidationSummary1;
    protected Label lblError;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblError.Text = "";
      if (this.IsPostBack)
        return;
      this.cmbCompany.Items.Clear();
      string cmdText = "SELECT CompanyID, CompanyName FROM Company";
      using (SqlConnection conn = Utils.GetConn())
      {
        using (SqlCommand sqlCommand = new SqlCommand(cmdText, conn))
        {
          using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
          {
            while (sqlDataReader.Read())
              this.cmbCompany.Items.Add(new ListItem(sqlDataReader.GetString(1), sqlDataReader.GetInt32(0).ToString()));
          }
        }
      }
      if (this.cmbCompany.Items.Count < 1)
        this.Error("No companies in the database. First add a company before adding users");
      else
        this.cmbCompany.SelectedIndex = 0;
    }

    protected void btnUserOK_Click(object sender, EventArgs e)
    {
      string str = "";
      if (this.txtName.Text.Length < 3)
        str = "User not added! Not enough characters (minimum 3) in user name.";
      if (this.txtEmail.Text.IndexOf('@') < 1)
        str = "User not added! Invalid e-mail address. Must include text before and after @ character.";
      if (this.txtPassword1.Text.Length < 8)
        str = "Password must be at least 8 characters.";
      if (this.txtName.Text == "")
        str = "You didn't fill in all the fields.";
      if (str != "")
      {
        this.lblError.Text = str;
      }
      else
      {
        MembershipUser user;
        try
        {
          user = Membership.CreateUser(this.txtName.Text, this.txtPassword1.Text, this.txtEmail.Text);
          Roles.AddUserToRole(user.UserName, "CompanyUser");
        }
        catch (MembershipCreateUserException ex)
        {
          this.Error("A problem occurred when creating a new user. Check your membership settings in web.config. Exception text: " + ex?.ToString());
          return;
        }
        catch (Exception ex)
        {
          this.Error("A problem occurred when creating a new user. Check your membership settings in web.config. Exception text: " + ex?.ToString());
          return;
        }
        try
        {
          Guid providerUserKey = (Guid) user.ProviderUserKey;
          string cmdText = "DELETE FROM UserSettings WHERE UserID = @userid AND Property='Company';" + " INSERT INTO UserSettings (UserID, Property, Setting) VALUES (@userid, 'Company', @companyId)";
          using (SqlConnection conn = Utils.GetConn())
          {
            using (SqlCommand sqlCommand = new SqlCommand(cmdText, conn))
            {
              sqlCommand.Parameters.AddWithValue("@userid", user.ProviderUserKey);
              sqlCommand.Parameters.AddWithValue("@companyId", (object) this.cmbCompany.SelectedValue);
              sqlCommand.ExecuteNonQuery();
            }
          }
        }
        catch (Exception ex)
        {
          Membership.DeleteUser(user.UserName);
          this.Error("A problem occurred when assigning the company to this user. The user has not been created.");
          return;
        }
        this.Response.Redirect("Admin.aspx", false);
      }
    }

    private void resetText()
    {
      this.txtName.Text = "";
      this.txtEmail.Text = "";
      this.txtPassword1.Text = "";
      this.txtPassword2.Text = "";
      this.cmbCompany.SelectedIndex = 0;
    }

    protected void btnUserCancel_Click(object sender, EventArgs e)
    {
      this.Response.Redirect("Admin.aspx", false);
    }

    private void Error(string text)
    {
      this.cmbCompany.Enabled = false;
      this.txtEmail.Enabled = false;
      this.txtName.Enabled = false;
      this.txtPassword1.Enabled = false;
      this.txtPassword2.Enabled = false;
      this.btnUserOK.Enabled = false;
      this.lblError.Text = text;
    }
  }
}
