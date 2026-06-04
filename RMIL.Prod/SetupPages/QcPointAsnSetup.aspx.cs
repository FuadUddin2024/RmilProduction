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
    public partial class QcPointAsnSetup : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                if (!IsPostBack)
                {
                    LoadQcPointsAssignGridView();
                    LoadCategory();
                    //LoadQcSteps();
                    LoadQcPoints();
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
        private void LoadQcPointsAssignGridView()
        {
            var ptsAssnList = new QcPointAsnDa(true).GetQcPointsAssignList();
            gvQcPointsAssign.DataSource = ptsAssnList;
            gvQcPointsAssign.DataBind();
        }
        protected void LoadCategory()
        {
            var categoryList = new CategoryDa(true).GetAllCategory();
            ddlCategory.DataSource = categoryList;
            this.ddlCategory.DataTextField = "CategoryName";
            this.ddlCategory.DataValueField = "CategoryId";
            ddlCategory.DataBind();
            ddlCategory.DataSource = categoryList;
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("---Please Select---", "0"));
        }
        protected void LoadQcSteps(int categoryid)
        {
            var stpList = new QcStepDa().GetGroupWiseQcStep(categoryid);
            ddlQcSteps.DataSource = stpList;
            this.ddlQcSteps.DataTextField = "StepsName";
            this.ddlQcSteps.DataValueField = "StpId";
            ddlQcSteps.DataBind();
            ddlQcSteps.DataSource = stpList;
            ddlQcSteps.DataBind();
            ddlQcSteps.Items.Insert(0, new ListItem("---Please Select---", "0"));
        }
        protected void LoadQcPoints()
        {
            var ptsList = new QcPointDa(true).GetAllQcCheckingPoints();
            ddlQcPoints.DataSource = ptsList.OrderBy(c => c.PointsName);
            this.ddlQcPoints.DataTextField = "PointsName";
            this.ddlQcPoints.DataValueField = "PtsId";
            ddlQcPoints.DataBind();
            ddlQcPoints.DataSource = ptsList.OrderBy(c => c.PointsName);
            ddlQcPoints.DataBind();
            ddlQcPoints.Items.Insert(0, new ListItem("---Please Select---", "0"));
        }
        protected void gvQcPointsAssign_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvQcPointsAssign.PageIndex = e.NewPageIndex;
            this.LoadQcPointsAssignGridView();
        }
        private void ClearAll()
        {
            ddlQcSteps.SelectedValue = "0";
            ddlQcPoints.SelectedValue = "0";

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
                    QcPointAsn qcPointsAssign = new QcPointAsn();

                    var assnInfo = new QcPointAsnDa(false).GetGroupWiseQcPointAsnThaByStpsId(Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlQcSteps.SelectedValue), Convert.ToInt32(ddlQcPoints.SelectedValue));
                    if (assnInfo != null)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('This Group Wise Points is already assigned.');window.close();", true);
                        return;
                    }
                    if (ddlCategory.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Product Group');window.close();", true);
                        return;
                    }
                    qcPointsAssign.CategoryId = Convert.ToInt32(ddlCategory.SelectedValue);
                    if (ddlQcSteps.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select QC Steps');window.close();", true);
                        return;
                    }
                    qcPointsAssign.StpId = Convert.ToInt32(ddlQcSteps.SelectedValue);

                    if (ddlQcPoints.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select QC Points');window.close();", true);
                        return;
                    }
                    qcPointsAssign.PtsId = Convert.ToInt32(ddlQcPoints.SelectedValue);

                    qcPointsAssign.EntryBy = currentUser.UserId;
                    qcPointsAssign.EntryDate = DateTime.Now;
                    qcPointsAssign.EntryPC = WebUtility.GetIpAddress();

                    new QcPointAsnDa(false).Insert(qcPointsAssign);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Inserted.');window.close();", true);
                    LoadQcPointsAssignGridView();
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
                    if (Session["paId"] != null)
                    {
                        int paId = Convert.ToInt32(Session["paId"]);
                        var selecteddata = new QcPointAsnDa(false).GetQcPointAsnThaById(paId);

                        var assnInfo = new QcPointAsnDa(false).GetGroupWiseQcPointAsnThaByStpsId(Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlQcSteps.SelectedValue), Convert.ToInt32(ddlQcPoints.SelectedValue));
                        if (assnInfo != null)
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('This Group Wise Points is already assigned.');window.close();", true);
                            return;
                        }
                        if (ddlCategory.SelectedValue == "0")
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Product Group');window.close();", true);
                            return;
                        }
                        selecteddata.CategoryId = Convert.ToInt32(ddlCategory.SelectedValue);
                        if (ddlQcSteps.SelectedValue == "0")
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select QC Steps');window.close();", true);
                            return;
                        }
                        selecteddata.StpId = Convert.ToInt32(ddlQcSteps.SelectedValue);

                        if (ddlQcPoints.SelectedValue == "0")
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select QC Points');window.close();", true);
                            return;
                        }
                        selecteddata.PtsId = Convert.ToInt32(ddlQcPoints.SelectedValue);

                        selecteddata.ModifiedBy = currentUser.UserId;
                        selecteddata.ModifiedDate = DateTime.Now;
                        selecteddata.ModifiedPC = WebUtility.GetIpAddress();

                        new QcPointAsnDa(false).Update(selecteddata);
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Updated.');window.close();", true);
                        LoadQcPointsAssignGridView();
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

        protected void btnEditQcPointsAssign_Click(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            btnUpdate.Visible = true;

            try
            {
                int paId = Convert.ToInt32(((ImageButton)sender).CommandArgument);
                Session["paId"] = paId;
                var selecteddata = new QcPointAsnDa(false).GetQcPointAsnThaById(paId);
                var categoryid = selecteddata.CategoryId;
                if (categoryid != null)
                {
                    var iq = new CategoryDa(true).GetCategoryById((int)categoryid);
                    var categoryName = iq.CategoryName;
                    LoadCategory();
                    if (ddlCategory.Items.Contains(ddlCategory.Items.FindByText(categoryName)))
                        ddlCategory.SelectedIndex = ddlCategory.Items.IndexOf(ddlCategory.Items.FindByText(categoryName));
                }
                var stpid = selecteddata.StpId;
                if (stpid != null)
                {
                    var q = new QcStepDa(true).GetQcStepById((int)stpid);
                    var stname = q.StepsName;
                    if (selecteddata.CategoryId != null) LoadQcSteps((int)selecteddata.CategoryId);
                    if (ddlQcSteps.Items.Contains(ddlQcSteps.Items.FindByText(stname)))
                        ddlQcSteps.SelectedIndex = ddlQcSteps.Items.IndexOf(ddlQcSteps.Items.FindByText(stname));
                }
                var ptsid = selecteddata.PtsId;
                if (ptsid != null)
                {
                    var q = new QcPointDa(true).GetQcCheckingPointsById((int)ptsid);
                    var ptsname = q.PointsName;
                    LoadQcPoints();
                    if (ddlQcPoints.Items.Contains(ddlQcPoints.Items.FindByText(ptsname)))
                        ddlQcPoints.SelectedIndex = ddlQcPoints.Items.IndexOf(ddlQcPoints.Items.FindByText(ptsname));
                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlQcSteps.Enabled = false;
                ddlQcSteps.Items.Clear();
                ddlQcSteps.Items.Insert(0, new ListItem("Select QC Steps", "0"));
                int pgId = int.Parse(ddlCategory.SelectedItem.Value);

                if (pgId > 0)
                {
                    var stpList = new QcStepDa(true).GetCategoryWiseQcStepList(pgId);
                    if (stpList.Any())
                    {
                        ddlQcSteps.DataSource = stpList.OrderBy(c => c.StepsName);
                        this.ddlQcSteps.DataTextField = "StepsName";
                        this.ddlQcSteps.DataValueField = "StpId";
                        ddlQcSteps.DataBind();
                        ddlQcSteps.DataSource = stpList.OrderBy(c => c.StepsName);
                        ddlQcSteps.DataBind();
                        ddlQcSteps.Items.Insert(0, new ListItem("Select QC Steps", "0"));
                        ddlQcSteps.Enabled = true;
                    }
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