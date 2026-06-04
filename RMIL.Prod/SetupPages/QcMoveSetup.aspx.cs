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
    public partial class QcMoveSetup : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                if (!IsPostBack)
                {
                    LoadQcMoveGridView();
                    LoadCategory();
                    //LoadQcFromSteps();
                    //LoadQcToSteps();
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
        private void LoadQcMoveGridView()
        {
            var qcsList = new QcMoveDa(true).GetQcMoveList();
            gvQcMove.DataSource = qcsList;
            gvQcMove.DataBind();
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
        protected void LoadQcFromSteps(int groupid)
        {
            var stpList = new QcStepDa().GetGroupWiseQcStep(groupid);
            ddlFromQc.DataSource = stpList;
            this.ddlFromQc.DataTextField = "StepsName";
            this.ddlFromQc.DataValueField = "StpId";
            ddlFromQc.DataBind();
            ddlFromQc.DataSource = stpList;
            ddlFromQc.DataBind();
            ddlFromQc.Items.Insert(0, new ListItem("---Please Select---", "0"));
        }
        protected void LoadQcToSteps(int groupid)
        {
            var stpList = new QcStepDa().GetGroupWiseQcStep(groupid);
            ddlToQc.DataSource = stpList;
            this.ddlToQc.DataTextField = "StepsName";
            this.ddlToQc.DataValueField = "StpId";
            ddlToQc.DataBind();
            ddlToQc.DataSource = stpList;
            ddlToQc.DataBind();
            ddlToQc.Items.Insert(0, new ListItem("---Please Select---", "0"));
        }
        protected void gvQcMove_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvQcMove.PageIndex = e.NewPageIndex;
            this.LoadQcMoveGridView();
        }
        private void ClearAll()
        {
            ddlCategory.SelectedValue = "0";
            ddlFromQc.SelectedValue = "0";
            ddlToQc.SelectedValue = "0";

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
                    QcMove qcMove = new QcMove();

                    var qc = new QcMoveDa(false).GetGroupWiseQcMoveByStpsId(Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlFromQc.SelectedValue), Convert.ToInt32(ddlToQc.SelectedValue));
                    if (qc != null)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('This is already exists.');window.close();", true);
                        return;
                    }
                    if (ddlCategory.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Product Group');window.close();", true);
                        return;
                    }
                    qcMove.CategoryId = Convert.ToInt32(ddlCategory.SelectedValue);
                    if (ddlFromQc.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select From QC');window.close();", true);
                        return;
                    }
                    qcMove.FromStId = Convert.ToInt32(ddlFromQc.SelectedValue);

                    if (ddlToQc.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select To QC');window.close();", true);
                        return;
                    }
                    qcMove.ToStId = Convert.ToInt32(ddlToQc.SelectedValue);

                    qcMove.EntryBy = currentUser.UserId;
                    qcMove.EntryDate = DateTime.Now;
                    qcMove.EntryPC = WebUtility.GetIpAddress();

                    new QcMoveDa(false).Insert(qcMove);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Inserted.');window.close();", true);
                    LoadQcMoveGridView();
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
                    if (Session["smvId"] != null)
                    {
                        int smvId = Convert.ToInt32(Session["smvId"]);
                        var selecteddata = new QcMoveDa(false).GetQcMoveById(smvId);

                        var qc = new QcMoveDa(false).GetGroupWiseQcMoveByStpsId(Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlFromQc.SelectedValue), Convert.ToInt32(ddlToQc.SelectedValue));
                        if (qc != null)
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('This is already exists.');window.close();", true);
                            return;
                        }
                        if (ddlCategory.SelectedValue == "0")
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Product Group');window.close();", true);
                            return;
                        }
                        selecteddata.CategoryId = Convert.ToInt32(ddlCategory.SelectedValue);
                        if (ddlFromQc.SelectedValue == "0")
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select From QC');window.close();", true);
                            return;
                        }
                        selecteddata.FromStId = Convert.ToInt32(ddlFromQc.SelectedValue);

                        if (ddlToQc.SelectedValue == "0")
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select To QC');window.close();", true);
                            return;
                        }
                        selecteddata.ToStId = Convert.ToInt32(ddlToQc.SelectedValue);

                        selecteddata.ModifiedBy = currentUser.UserId;
                        selecteddata.ModifiedDate = DateTime.Now;
                        selecteddata.ModifiedPC = WebUtility.GetIpAddress();

                        new QcMoveDa(false).Update(selecteddata);
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Updated.');window.close();", true);
                        LoadQcMoveGridView();
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

        protected void btnEditQcMove_Click(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            btnUpdate.Visible = true;

            try
            {
                int smvId = Convert.ToInt32(((ImageButton)sender).CommandArgument);
                Session["smvId"] = smvId;
                var selecteddata = new QcMoveDa(false).GetQcMoveById(smvId);
                var pgid = selecteddata.CategoryId;
                if (pgid != null)
                {
                    var iq = new CategoryDa(true).GetCategoryById((int)pgid);
                    var categoryName = iq.CategoryName;
                    LoadCategory();
                    if (ddlCategory.Items.Contains(ddlCategory.Items.FindByText(categoryName)))
                        ddlCategory.SelectedIndex = ddlCategory.Items.IndexOf(ddlCategory.Items.FindByText(categoryName));
                }
                var fromstpid = selecteddata.FromStId;
                if (fromstpid != null)
                {
                    var q = new QcStepDa(false).GetQcStepById((int)fromstpid);
                    var stname = q.StepsName;
                    if (selecteddata.CategoryId != null) LoadQcFromSteps((int)selecteddata.CategoryId);
                    if (ddlFromQc.Items.Contains(ddlFromQc.Items.FindByText(stname)))
                        ddlFromQc.SelectedIndex = ddlFromQc.Items.IndexOf(ddlFromQc.Items.FindByText(stname));
                }
                var tostpid = selecteddata.ToStId;
                if (tostpid != null)
                {
                    var q = new QcStepDa(false).GetQcStepById((int)tostpid);
                    var stname = q.StepsName;
                    if (selecteddata.CategoryId != null) LoadQcToSteps((int)selecteddata.CategoryId);
                    if (ddlToQc.Items.Contains(ddlToQc.Items.FindByText(stname)))
                        ddlToQc.SelectedIndex = ddlToQc.Items.IndexOf(ddlToQc.Items.FindByText(stname));
                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        protected void btnDeleteQcMove_Click(object sender, EventArgs e)
        {
            try
            {
                int smvId = Convert.ToInt32(((ImageButton)sender).CommandArgument);
                var selecteddata = new QcMoveDa(false).GetQcMoveById(smvId);
                new QcMoveDa(false).Delete(selecteddata);

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Deleted.');window.close();", true);
                LoadQcMoveGridView();

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
                ddlFromQc.Enabled = false;
                ddlFromQc.Items.Clear();
                ddlFromQc.Items.Insert(0, new ListItem("Select From QC Steps", "0"));
                int pgId = int.Parse(ddlCategory.SelectedItem.Value);

                if (pgId > 0)
                {
                    var stpList = new QcStepDa(false).GetCategoryWiseQcStepList(pgId);
                    if (stpList != null)
                    {
                        ddlFromQc.DataSource = stpList.OrderBy(c => c.StepsName);
                        this.ddlFromQc.DataTextField = "StepsName";
                        this.ddlFromQc.DataValueField = "StpId";
                        ddlFromQc.DataBind();
                        ddlFromQc.DataSource = stpList.OrderBy(c => c.StepsName);
                        ddlFromQc.DataBind();
                        ddlFromQc.Items.Insert(0, new ListItem("Select From QC Steps", "0"));
                        ddlFromQc.Enabled = true;
                    }
                }

                ddlToQc.Enabled = false;
                ddlToQc.Items.Clear();
                ddlToQc.Items.Insert(0, new ListItem("Select To QC Steps", "0"));
                int pg1Id = int.Parse(ddlCategory.SelectedItem.Value);

                if (pg1Id > 0)
                {
                    var stpList = new QcStepDa(false).GetCategoryWiseQcStepList(pg1Id);
                    if (stpList.Any())
                    {
                        ddlToQc.DataSource = stpList.OrderBy(c => c.StepsName);
                        this.ddlToQc.DataTextField = "StepsName";
                        this.ddlToQc.DataValueField = "StpId";
                        ddlToQc.DataBind();
                        ddlToQc.DataSource = stpList.OrderBy(c => c.StepsName);
                        ddlToQc.DataBind();
                        ddlToQc.Items.Insert(0, new ListItem("Select To QC Steps", "0"));
                        ddlToQc.Enabled = true;
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