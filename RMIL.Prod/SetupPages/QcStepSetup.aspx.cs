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
    public partial class QcStepSetup : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                if (!IsPostBack)
                {
                    LoadCheckingStepsGridView();
                    LoadCategory();
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

        private void LoadCheckingStepsGridView()
        {
            var checkStepsList = new QcStepDa(true).GetQcStepList();
            gvCheckingSteps.DataSource = checkStepsList;
            gvCheckingSteps.DataBind();
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
        protected void gvCheckingSteps_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCheckingSteps.PageIndex = e.NewPageIndex;
            this.LoadCheckingStepsGridView();
        }
        private void ClearAll()
        {
            txtStepsName.Text = "";
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
                    QcStep qcStep = new QcStep();
                    var stepsinfo = new QcStepDa().GetProdCategoryWiseQcStep(Convert.ToInt32(ddlCategory.SelectedValue), txtStepsName.Text);
                    if (stepsinfo != null)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('This Group Wise QC Steps Already Exists.');window.close();", true);
                        return;
                    }
                    qcStep.StepsName = txtStepsName.Text;
                    if (ddlCategory.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Product Group');window.close();", true);
                        return;
                    }
                    qcStep.CategoryId = Convert.ToInt32(ddlCategory.SelectedValue);
                    if (chkActive.Checked)
                    {
                        qcStep.Active = true;
                    }
                    else
                    {
                        qcStep.Active = false;
                    }

                    if (chkStart.Checked)
                    {
                        var q = new QcStepDa(false).GetCategoryWiseInitialQcStep(Convert.ToInt32(ddlCategory.SelectedValue));
                        if (q == null)
                        {
                            qcStep.InitialStep = true;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Already group wise selected one as a start step!');window.close();", true);
                            return;
                        }
                    }
                    else
                    {
                        qcStep.InitialStep = false;
                    }

                    qcStep.EntryBy = currentUser.UserId;
                    qcStep.EntryDate = DateTime.Now;
                    qcStep.EntryPC = WebUtility.GetIpAddress();

                    new QcStepDa(false).Insert(qcStep);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Inserted.');window.close();", true);
                    LoadCheckingStepsGridView();
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
                    if (Session["stpId"] != null)
                    {
                        int stpId = Convert.ToInt32(Session["stpId"]);
                        var selecteddata = new QcStepDa(false).GetQcStepById(stpId);
                        selecteddata.StepsName = txtStepsName.Text;
                        if (ddlCategory.SelectedValue == "0")
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Product Group');window.close();", true);
                            return;
                        }
                        selecteddata.CategoryId = Convert.ToInt32(ddlCategory.SelectedValue);
                        if (chkActive.Checked)
                        {
                            selecteddata.Active = true;
                        }
                        else
                        {
                            selecteddata.Active = false;
                        }
                        if (chkStart.Checked)
                        {
                            var q = new QcStepDa(false).GetCategoryWiseInitialQcStep(Convert.ToInt32(ddlCategory.SelectedValue));
                            if (q == null)
                            {
                                selecteddata.InitialStep = true;
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Already group wise selected one as a start step!');window.close();", true);
                                return;
                            }
                        }
                        else
                        {
                            selecteddata.InitialStep = false;
                        }
                        selecteddata.ModifiedBy = currentUser.UserId;
                        selecteddata.ModifiedDate = DateTime.Now;
                        selecteddata.ModifiedPC = WebUtility.GetIpAddress();

                        new QcStepDa(false).Update(selecteddata);
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Updated.');window.close();", true);
                        LoadCheckingStepsGridView();
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

        protected void btnEditCheckingSteps_Click(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            btnUpdate.Visible = true;

            try
            {
                int stpId = Convert.ToInt32(((ImageButton)sender).CommandArgument);
                Session["stpId"] = stpId;
                var selecteddata = new QcStepDa(false).GetQcStepById(stpId);
                txtStepsName.Text = selecteddata.StepsName;
                var pgid = selecteddata.CategoryId;
                if (pgid != null)
                {
                    var q = new CategoryDa(true).GetCategoryById((int)pgid);
                    var tname = q.CategoryName;
                    LoadCategory();
                    if (ddlCategory.Items.Contains(ddlCategory.Items.FindByText(tname)))
                        ddlCategory.SelectedIndex = ddlCategory.Items.IndexOf(ddlCategory.Items.FindByText(tname));
                }
                if (selecteddata.Active != null && (bool)selecteddata.Active)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                if (selecteddata.InitialStep != null && (bool)selecteddata.InitialStep)
                {
                    chkStart.Checked = true;
                }
                else
                {
                    chkStart.Checked = false;
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