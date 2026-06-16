using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using RMIL.Prod.DAL;
using RMIL.Prod.EntityFramework;
using RMIL.Prod.Utility;

namespace RMIL.Prod.Account
{
    public partial class CurrentProductionModel : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                if (!IsPostBack)
                {
                    LoadUserTypetGridView();
                }
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
                else if (user.UserTypeId == ((int)Enums.Enums.EnumUserType.QcUser))
                {
                    this.MasterPageFile = "~/MasterPages/Qc.Master";
                }
                else if (user.UserTypeId == ((int)Enums.Enums.EnumUserType.ProductionUser))
                {
                    this.MasterPageFile = "~/MasterPages/Prod.Master";
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
        #region User Defined Methods
        private void LoadUserTypetGridView()
        {
            var listUserType = new UserTypeDa(true).GetAllUserType();
            gvUserType.DataSource = listUserType;
            gvUserType.DataBind();
        }
        protected void gvUserType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUserType.PageIndex = e.NewPageIndex;
            this.LoadUserTypetGridView();
        }
        private void ClearAll()
        {
            txtTypeName.Text = "";
        }
        #endregion
        #region Event Generated Method
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                UserInfo currentUser = (UserInfo)Session[WebUtility.SessionCurrentUserObj];

                try
                {
                    UserType userType = new UserType();

                    userType.TypeName = txtTypeName.Text;
                    userType.ServiceType = "P";

                    userType.EntryBy = currentUser.UserId;
                    userType.EntryDate = DateTime.Now;
                    userType.EntryPC = WebUtility.GetIpAddress();

                    new UserTypeDa().Insert(userType);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Inserted.');window.close();", true);
                    LoadUserTypetGridView();
                    ClearAll();
                }
                catch (Exception ex)
                {

                    Response.Write(ex.Message);
                }
            }
            else
            {
                Response.Redirect("~/Account/Logout.aspx");
            }

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                UserInfo currentUser = (UserInfo)Session[WebUtility.SessionCurrentUserObj];

                try
                {
                    if (Session["typeId"] != null)
                    {
                        int typeId = Convert.ToInt32(Session["typeId"]);
                        var selecteddata = new UserTypeDa(false).GetUserTypeById(typeId);
                        selecteddata.TypeName = txtTypeName.Text;

                        selecteddata.ModifiedBy = currentUser.UserId;
                        selecteddata.ModifiedDate = DateTime.Now;
                        selecteddata.ModifiedPC = WebUtility.GetIpAddress();

                        new UserTypeDa(false).Update(selecteddata);
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Updated.');window.close();", true);
                        LoadUserTypetGridView();
                        ClearAll();
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
            else
            {
                Response.Redirect("~/Account/Logout.aspx");
            }

        }

        protected void btnEditUserType_Click(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            btnUpdate.Visible = true;

            try
            {
                int typeId = Convert.ToInt32(((ImageButton)sender).CommandArgument);
                Session["typeId"] = typeId;
                var selecteddata = new UserTypeDa(false).GetUserTypeById(typeId);
                txtTypeName.Text = selecteddata.TypeName;

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        protected void btnDeleteUserType_Click(object sender, EventArgs e)
        {
            try
            {
                int typeId = Convert.ToInt32(((ImageButton)sender).CommandArgument);
                var selectedData = new UserTypeDa(false).GetUserTypeById(typeId);
                new UserTypeDa(false).Delete(selectedData);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Deleted.');window.close();", true);
                LoadUserTypetGridView();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }

        }
        #endregion
    }
}