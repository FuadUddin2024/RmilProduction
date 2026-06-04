using System;
using System.Drawing;
using RMIL.Prod.DAL;
using RMIL.Prod.Utility;

namespace RMIL.Prod.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string userId = txtUserID.Text;
            string password = txtPassword.Text;
            var user = new UserInfoDa(true).GetUserByUserId(userId);
            if (user != null)
            {
                if (user.UserId == userId && user.Password == password)
                {
                    if (user.UserTypeId == ((int)Enums.Enums.EnumUserType.SuperAdmin))
                    {
                        Session[WebUtility.SessionCurrentUserObj] = user;
                        Session[WebUtility.Username] = user.UserId;
                        Response.Redirect("~/Dashboard/Index.aspx");
                    }
                    else if (user.UserTypeId == ((int)Enums.Enums.EnumUserType.Admin))
                    {
                        Session[WebUtility.SessionCurrentUserObj] = user;
                        Session[WebUtility.Username] = user.UserId;
                        Response.Redirect("~/Dashboard/Index.aspx");
                    }
                    else if (user.UserTypeId == ((int)Enums.Enums.EnumUserType.ReportUser))
                    {
                        Session[WebUtility.SessionCurrentUserObj] = user;
                        Session[WebUtility.Username] = user.UserId;
                        Response.Redirect("~/Dashboard/Index.aspx");
                    }
                    else if (user.UserTypeId == ((int)Enums.Enums.EnumUserType.QcUser))
                    {
                        Session[WebUtility.SessionCurrentUserObj] = user;
                        Session[WebUtility.Username] = user.UserId;
                        Response.Redirect("~/Dashboard/Index.aspx");
                    }
                    else if (user.UserTypeId == ((int)Enums.Enums.EnumUserType.ProductionUser))
                    {
                        Session[WebUtility.SessionCurrentUserObj] = user;
                        Session[WebUtility.Username] = user.UserId;
                        Response.Redirect("~/Dashboard/Index.aspx");
                    }
                    else
                    {
                        Response.Redirect("~/Account/Logout.aspx");
                    }
                }
                else
                {
                    lblMessage.Text = "Password doesn't match.";
                    lblMessage.ForeColor = Color.White;
                }
            }
            else
            {
                lblMessage.Text = "Invalid user";
                lblMessage.ForeColor = Color.White;
            }
        }
    }
}