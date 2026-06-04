using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RMIL.Prod.DAL;
using RMIL.Prod.EntityFramework;
using RMIL.Prod.Utility;

namespace RMIL.Prod.SetupPages
{
    public partial class UserWiseLine : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                if (!IsPostBack)
                {
                    LoadUserWiseLinePerGridView();
                    LoadUser();
                    LoadLine();
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

        private void LoadUserWiseLinePerGridView()
        {
            var lList = new RmilUserWiseLinePerDa(true).GetUserWiseLinePerList();
            gvUserLine.DataSource = lList;
            gvUserLine.DataBind();
        }
        protected void LoadUser()
        {
            var userList = new UserInfoDa(true).GetAllUserInfo();
            if (userList.Any())
            {
                var q = userList.Select(p => new { p.UserId, p.UserName, DisplayText = p.UserId + " (" + p.UserName + ")" });
                ddlUserId.DataSource = q.OrderBy(c => c.UserName);
                this.ddlUserId.DataTextField = "DisplayText";
                this.ddlUserId.DataValueField = "UserId";
                ddlUserId.DataBind();
                ddlUserId.DataSource = q.OrderBy(c => c.UserName);
                ddlUserId.DataBind();
                ddlUserId.Items.Insert(0, new ListItem("---Please Select---", "0"));
            }
        }
        protected void LoadLine()
        {
            var lList = new ProductionLineDa().GetAllProductionLine();
            ddlLine.DataSource = lList.OrderBy(c => c.LineName);
            this.ddlLine.DataTextField = "LineName";
            this.ddlLine.DataValueField = "LineId";
            ddlLine.DataBind();
            ddlLine.DataSource = lList.OrderBy(c => c.LineName);
            ddlLine.DataBind();
            ddlLine.Items.Insert(0, new ListItem("---Please Select---", "0"));
        }
        protected void gvUserLine_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUserLine.PageIndex = e.NewPageIndex;
            this.LoadUserWiseLinePerGridView();
        }
        private void ClearAll()
        {
            ddlUserId.SelectedValue = "0";
            ddlLine.SelectedValue = "0";
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
                    RmilUserWiseLinePer userWiseLinePer = new RmilUserWiseLinePer();
                    var qInfo = new RmilUserWiseLinePerDa().GetUserWiseLinePerById(ddlUserId.SelectedValue, Convert.ToInt32(ddlLine.SelectedValue));
                    if (qInfo != null)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('This User Wise Line Already Exists.');window.close();", true);
                        return;
                    }
                    if (ddlUserId.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select User ID');window.close();", true);
                        return;
                    }
                    userWiseLinePer.UserId = ddlUserId.SelectedValue;
                    if (ddlLine.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Line');window.close();", true);
                        return;
                    }
                    userWiseLinePer.LineId = Convert.ToInt32(ddlLine.SelectedValue);
                    if (chkActive.Checked)
                    {
                        userWiseLinePer.Active = true;
                    }
                    else
                    {
                        userWiseLinePer.Active = false;
                    }

                    userWiseLinePer.EntryBy = currentUser.UserId;
                    userWiseLinePer.EntryDate = DateTime.Now;
                    userWiseLinePer.EntryPC = WebUtility.GetIpAddress();

                    new RmilUserWiseLinePerDa(false).Insert(userWiseLinePer);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Inserted.');window.close();", true);
                    LoadUserWiseLinePerGridView();
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
                    if (Session["uId"] != null)
                    {
                        int uId = Convert.ToInt32(Session["uId"]);
                        var selecteddata = new RmilUserWiseLinePerDa(true).GetUserWiseLinePerById(uId);
                        //var qInfo = new RmilUserWiseLinePerDa(false).GetUserWiseLinePerById(ddlUserId.SelectedValue, Convert.ToInt32(ddlLine.SelectedValue));
                        //if (qInfo != null)
                        //{
                        //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('This User Wise Line Already Exists.');window.close();", true);
                        //    return;
                        //}
                        if (ddlUserId.SelectedValue == "0")
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select User ID');window.close();", true);
                            return;
                        }
                        selecteddata.UserId = ddlUserId.SelectedValue;
                        if (ddlLine.SelectedValue == "0")
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Line');window.close();", true);
                            return;
                        }
                        selecteddata.LineId = Convert.ToInt32(ddlLine.SelectedValue);
                        if (chkActive.Checked)
                        {
                            selecteddata.Active = true;
                        }
                        else
                        {
                            selecteddata.Active = false;
                        }
                        selecteddata.ModifiedBy = currentUser.UserId;
                        selecteddata.ModifiedDate = DateTime.Now;
                        selecteddata.ModifiedPC = WebUtility.GetIpAddress();

                        new RmilUserWiseLinePerDa(false).Update(selecteddata);
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Updated.');window.close();", true);
                        LoadUserWiseLinePerGridView();
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

        protected void btnEditUserLine_Click(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            btnUpdate.Visible = true;

            try
            {
                int uId = Convert.ToInt32(((ImageButton)sender).CommandArgument);
                Session["uId"] = uId;
                var selecteddata = new RmilUserWiseLinePerDa(false).GetUserWiseLinePerById(uId);

                var uid = selecteddata.UserId;
                if (uid != null)
                {
                    var q = new UserInfoDa(true).GetUserByUserId(uid);
                    var uname = q.UserName;
                    LoadUser();
                    if (ddlUserId.Items.Contains(ddlUserId.Items.FindByText(uname)))
                        ddlUserId.SelectedIndex = ddlUserId.Items.IndexOf(ddlUserId.Items.FindByText(uname));
                }
                var lid = selecteddata.LineId;
                if (lid != null)
                {
                    var q = new ProductionLineDa(true).GetProductionLineById((int)lid);
                    var lname = q.LineName;
                    LoadLine();
                    if (ddlLine.Items.Contains(ddlLine.Items.FindByText(lname)))
                        ddlLine.SelectedIndex = ddlLine.Items.IndexOf(ddlLine.Items.FindByText(lname));
                }
                if (selecteddata.Active != null && (bool)selecteddata.Active)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        #endregion
    }
}