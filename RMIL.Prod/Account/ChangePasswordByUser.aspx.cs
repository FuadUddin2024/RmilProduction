using System;
using System.Web.UI;
using RMIL.Prod.DAL;
using RMIL.Prod.EntityFramework;
using RMIL.Prod.Utility;

namespace RMIL.Prod.Account
{
    public partial class ChangePasswordByUser : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                //if (!IsPostBack)
                //{

                //}
            }
            else
            {
                Response.Redirect("~/Account/Logout.aspx");
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                UserInfo user = (UserInfo)Session[WebUtility.SessionCurrentUserObj];


                if (user.UserTypeId == ((int)Enums.Enums.EnumUserType.SuperAdmin))
                {
                    this.MasterPageFile = "~/MasterPages/Main.Master";
                }
                else if (user.UserTypeId == ((int)Enums.Enums.EnumUserType.Admin))
                {
                    this.MasterPageFile = "~/MasterPages/Admin.Master";
                }
                else if (user.UserTypeId == ((int)Enums.Enums.EnumUserType.ReportUser))
                {
                    this.MasterPageFile = "~/MasterPages/Report.Master";
                }
            }
            else
            {
                Response.Redirect("~/Account/Logout.aspx");
            }
        }
        #endregion
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                UserInfo currentUser = (UserInfo)Session[WebUtility.SessionCurrentUserObj];
                var userId = currentUser.UserId;



                if (string.IsNullOrEmpty(txtOldPass.Text) && string.IsNullOrEmpty(txtNewPass.Text) &&
                    string.IsNullOrEmpty(txtConfirm.Text))
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript",
                        "alert('Please enter all password field.');window.close();", true);
                }
                else
                {
                    string oldPass = txtOldPass.Text;

                    var userObj = new UserInfoDa(true).GetUserByUserId(userId);
                    if (userObj != null)
                    {
                        if ((userObj.UserId == userId) && (userObj.Password == oldPass))
                        {
                            userObj.Password = txtNewPass.Text;

                            new UserInfoDa(false).Update(userObj);
                            string redURL = "../Account/Logout.aspx";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Password changed successfully.'); window.location='" + redURL + "';", true);

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript",
                                "alert('Incorrect current password ');window.close();", true);
                        }
                    }
                }
            }
        }
    }
}