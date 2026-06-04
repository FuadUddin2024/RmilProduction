using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using RMIL.Prod.DAL;
using RMIL.Prod.EntityFramework;
using RMIL.Prod.Utility;

namespace RMIL.Prod.SetupPages
{
    public partial class DesignationSetup : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                if (!IsPostBack)
                {
                    LoadDesignationtGridView();
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
        private void LoadDesignationtGridView()
        {
            var designationlist = new DesignationDa(true).GetAllDesignation();
            gvDesignation.DataSource = designationlist;
            gvDesignation.DataBind();
        }
        protected void gvDesignation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDesignation.PageIndex = e.NewPageIndex;
            this.LoadDesignationtGridView();
        }
        private void ClearAll()
        {
            txtDesignationName.Text = "";
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
                    Designation designation = new Designation();
                    designation.DesignationName = txtDesignationName.Text;

                    designation.EntryBy = currentUser.UserId;
                    designation.EntryDate = DateTime.Now;
                    designation.EntryPC = WebUtility.GetIpAddress();

                    new DesignationDa(false).Insert(designation);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Inserted.');window.close();", true);
                    LoadDesignationtGridView();
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
                    if (Session["desigId"] != null)
                    {
                        int desigId = Convert.ToInt32(Session["desigId"]);
                        var selecteddata = new DesignationDa(false).GetDesignationtById(desigId);
                        selecteddata.DesignationName = txtDesignationName.Text;

                        selecteddata.ModifiedBy = currentUser.UserId;
                        selecteddata.ModifiedDate = DateTime.Now;
                        selecteddata.ModifiedPC = WebUtility.GetIpAddress();

                        new DesignationDa(false).Update(selecteddata);
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Updated.');window.close();", true);
                        LoadDesignationtGridView();
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

        protected void btnEditDesignation_Click(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            btnUpdate.Visible = true;

            try
            {
                int desigId = Convert.ToInt32(((ImageButton)sender).CommandArgument);
                Session["desigId"] = desigId;
                var selecteddata = new DesignationDa(false).GetDesignationtById(desigId);
                txtDesignationName.Text = selecteddata.DesignationName;

            }
            catch (Exception ex)
            {

                Response.Write(ex.Message);
            }
        }
        protected void btnDeleteDesignation_Click(object sender, EventArgs e)
        {
            try
            {
                int desigId = Convert.ToInt32(((ImageButton)sender).CommandArgument);
                var selectedData = new DesignationDa(false).GetDesignationtById(desigId);
                new DesignationDa(false).Delete(selectedData);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Deleted.');window.close();", true);
                LoadDesignationtGridView();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }

        }
        #endregion
    }
}