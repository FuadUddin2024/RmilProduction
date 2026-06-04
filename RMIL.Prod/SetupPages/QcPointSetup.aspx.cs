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
    public partial class QcPointSetup : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                if (!IsPostBack)
                {
                    LoadCheckingPointsGridView();
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

        private void LoadCheckingPointsGridView()
        {
            var checkPtsList = new QcPointDa(true).GetAllQcCheckingPoints();
            gvCheckingPoints.DataSource = checkPtsList;
            gvCheckingPoints.DataBind();
        }
        protected void gvCheckingPoints_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCheckingPoints.PageIndex = e.NewPageIndex;
            this.LoadCheckingPointsGridView();
        }
        private void ClearAll()
        {
            txtPointsName.Text = "";
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
                    QcPoint qcPointTha = new QcPoint();

                    var ptsinfo = new QcPointDa().GetQcCheckingPointsByName(txtPointsName.Text);
                    if (ptsinfo != null)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('This Points Name Already Exists.');window.close();", true);
                        return;
                    }

                    qcPointTha.PointsName = txtPointsName.Text;
                    if (chkActive.Checked)
                    {
                        qcPointTha.Active = true;
                    }
                    else
                    {
                        qcPointTha.Active = false;
                    }

                    qcPointTha.EntryBy = currentUser.UserId;
                    qcPointTha.EntryDate = DateTime.Now;
                    qcPointTha.EntryPC = WebUtility.GetIpAddress();

                    new QcPointDa(false).Insert(qcPointTha);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Inserted.');window.close();", true);
                    LoadCheckingPointsGridView();
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
                    if (Session["ptsId"] != null)
                    {
                        int ptsId = Convert.ToInt32(Session["ptsId"]);
                        var selecteddata = new QcPointDa(false).GetQcCheckingPointsById(ptsId);
                        selecteddata.PointsName = txtPointsName.Text;
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

                        new QcPointDa(false).Update(selecteddata);
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Updated.');window.close();", true);
                        LoadCheckingPointsGridView();
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

        protected void btnEditCheckingPoints_Click(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            btnUpdate.Visible = true;

            try
            {
                int ptsId = Convert.ToInt32(((ImageButton)sender).CommandArgument);
                Session["ptsId"] = ptsId;
                var selecteddata = new QcPointDa(false).GetQcCheckingPointsById(ptsId);
                txtPointsName.Text = selecteddata.PointsName;
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