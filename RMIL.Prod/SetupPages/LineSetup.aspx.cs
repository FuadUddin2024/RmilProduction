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
    public partial class LineSetup : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                if (!IsPostBack)
                {
                    LoadLineGridView();
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
        private void LoadLineGridView()
        {
            var lList = new ProductionLineDa().GetAllProductionLine();
            gvLine.DataSource = lList;
            gvLine.DataBind();
        }
        protected void gvLine_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLine.PageIndex = e.NewPageIndex;
            this.LoadLineGridView();
        }
        private void ClearAll()
        {
            txtLineName.Text = "";
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
                    ProductionLine productionLine = new ProductionLine();
                    productionLine.LineName = txtLineName.Text;

                    productionLine.EntryBy = currentUser.UserId;
                    productionLine.EntryDate = DateTime.Now;
                    productionLine.EntryPC = WebUtility.GetIpAddress();

                    new ProductionLineDa(false).Insert(productionLine);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Inserted.');window.close();", true);
                    LoadLineGridView();
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
                    if (Session["lineId"] != null)
                    {
                        int lineId = Convert.ToInt32(Session["lineId"]);
                        var selecteddata = new ProductionLineDa().GetProductionLineById(lineId);
                        selecteddata.LineName = txtLineName.Text;

                        selecteddata.ModifiedBy = currentUser.UserId;
                        selecteddata.ModifiedDate = DateTime.Now;
                        selecteddata.ModifiedPC = WebUtility.GetIpAddress();

                        new ProductionLineDa(false).Update(selecteddata);
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Updated.');window.close();", true);
                        LoadLineGridView();
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

        protected void btnEditLine_Click(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            btnUpdate.Visible = true;

            try
            {
                int lineId = Convert.ToInt32(((ImageButton)sender).CommandArgument);
                Session["lineId"] = lineId;
                var selecteddata = new ProductionLineDa(false).GetProductionLineById(lineId);
                txtLineName.Text = selecteddata.LineName;

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        protected void btnDeleteLine_Click(object sender, EventArgs e)
        {
            try
            {
                int lineId = Convert.ToInt32(((ImageButton)sender).CommandArgument);
                var selectedData = new ProductionLineDa(false).GetProductionLineById(lineId);
                new ProductionLineDa(false).Delete(selectedData);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Deleted.');window.close();", true);
                LoadLineGridView();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }

        }
        #endregion
    }
}