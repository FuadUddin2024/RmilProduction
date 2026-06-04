using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using RMIL.Prod.DAL;
using RMIL.Prod.EntityFramework;
using RMIL.Prod.Utility;

namespace RMIL.Prod.SetupPages
{
    public partial class CategorySetup : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                if (!IsPostBack)
                {
                    LoadGroup();
                    LoadCompany();
                    LoadCategoryGridView();
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
        private void LoadCategoryGridView()
        {
            var categoryList = new CategoryDa(true).GetCategoryList();
            gvCategory.DataSource = categoryList;
            gvCategory.DataBind();
        }
        protected void gvCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCategory.PageIndex = e.NewPageIndex;
            this.LoadCategoryGridView();
        }
        protected void LoadGroup()
        {
            var groupList = new GroupDa(true).GetAllGroup();
            ddlGroup.DataSource = groupList;
            this.ddlGroup.DataTextField = "GroupName";
            this.ddlGroup.DataValueField = "GroupId";
            ddlGroup.DataBind();
            ddlGroup.DataSource = groupList;
            ddlGroup.DataBind();
            ddlGroup.Items.Insert(0, new ListItem("---Please Select---", "0"));
        }
        protected void LoadCompany()
        {
            var companyList = new CompanyDa(true).GetAllCompany();
            ddlCompany.DataSource = companyList;
            this.ddlCompany.DataTextField = "CompanyName";
            this.ddlCompany.DataValueField = "CompanyId";
            ddlCompany.DataBind();
            ddlCompany.DataSource = companyList;
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, new ListItem("---Please Select---", "0"));
        }
        private void ClearAll()
        {
            txtCategoryName.Text = "";
            txtDescription.Text = "";
            ddlInstallation.SelectedValue = "0";
            ddlCompany.SelectedValue = "0";
            ddlGroup.SelectedValue = "0";
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
                    Category category = new Category();

                    category.CategoryName = txtCategoryName.Text;
                    category.Description = txtDescription.Text;

                    if (ddlInstallation.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Installation Type');window.close();", true);
                        return;
                    }
                    category.InstallationType = ddlInstallation.SelectedValue;

                    if (ddlCompany.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Company Name');window.close();", true);
                        return;
                    }
                    category.CompanyId = Convert.ToInt32((string) ddlCompany.SelectedValue);

                    if (ddlGroup.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Group Name');window.close();", true);
                        return;
                    }
                    category.GroupId = Convert.ToInt32((string) ddlGroup.SelectedValue);

                    category.EntryBy = currentUser.UserId;
                    category.EntryDate = DateTime.Now;
                    category.EntryPC = WebUtility.GetIpAddress();

                    new CategoryDa(false).Insert(category);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Inserted.');window.close();", true);
                    LoadCategoryGridView();
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
                    if (Session["categoryId"] != null)
                    {
                        int categoryId = Convert.ToInt32(Session["categoryId"]);
                        var selecteddata = new CategoryDa(false).GetCategoryById(categoryId);
                        selecteddata.CategoryName = txtCategoryName.Text;
                        selecteddata.Description = txtDescription.Text;

                        if (ddlInstallation.SelectedValue == "0")
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Installation Type');window.close();", true);
                            return;
                        }
                        selecteddata.InstallationType = ddlInstallation.SelectedValue;

                        if (ddlCompany.SelectedValue == "0")
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Company Name');window.close();", true);
                            return;
                        }
                        selecteddata.CompanyId = Convert.ToInt32((string) ddlCompany.SelectedValue);

                        if (ddlGroup.SelectedValue == "0")
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Group Name');window.close();", true);
                            return;
                        }
                        selecteddata.GroupId = Convert.ToInt32((string) ddlGroup.SelectedValue);

                        selecteddata.ModifiedBy = currentUser.UserId;
                        selecteddata.ModifiedDate = DateTime.Now;
                        selecteddata.ModifiedPC = WebUtility.GetIpAddress();

                        new CategoryDa(false).Update(selecteddata);
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Updated.');window.close();", true);
                        LoadCategoryGridView();
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

        protected void btnEditCategory_Click(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            btnUpdate.Visible = true;

            try
            {
                int categoryId = Convert.ToInt32(((ImageButton)sender).CommandArgument);
                Session["categoryId"] = categoryId;
                var selecteddata = new CategoryDa(false).GetCategoryById(categoryId);
                txtCategoryName.Text = selecteddata.CategoryName;
                txtDescription.Text = selecteddata.Description;

                var compid = selecteddata.CompanyId;
                if (compid != null)
                {
                    var cq = new CompanyDa(true).GetCompanyById((int)compid);
                    var compName = cq.CompanyName;
                    LoadCompany();
                    if (ddlCompany.Items.Contains(ddlCompany.Items.FindByText(compName)))
                        ddlCompany.SelectedIndex = ddlCompany.Items.IndexOf(ddlCompany.Items.FindByText(compName));
                }

                var groupid = selecteddata.GroupId;
                if (groupid != null)
                {
                    var gq = new GroupDa(true).GetGroupById((int)groupid);
                    var groupName = gq.GroupName;
                    LoadGroup();
                    if (ddlGroup.Items.Contains(ddlGroup.Items.FindByText(groupName)))
                        ddlGroup.SelectedIndex = ddlGroup.Items.IndexOf(ddlGroup.Items.FindByText(groupName));
                }
            }
            catch (Exception ex)
            {

                Response.Write(ex.Message);
            }
        }
        protected void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            try
            {
                int categoryId = Convert.ToInt32(((ImageButton)sender).CommandArgument);
                var selectedData = new CategoryDa(false).GetCategoryById(categoryId);
                new CategoryDa(false).Delete(selectedData);

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Deleted.');window.close();", true);
                LoadCategoryGridView();

            }
            catch (Exception ex)
            {

                Response.Write(ex.Message);
            }
        }
        #endregion
    }
}