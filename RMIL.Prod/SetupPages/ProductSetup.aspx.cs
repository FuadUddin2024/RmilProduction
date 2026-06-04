using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using RMIL.Prod.DAL;
using RMIL.Prod.EntityFramework;
using RMIL.Prod.Utility;

namespace RMIL.Prod.SetupPages
{
    public partial class ProductSetup : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                if (!IsPostBack)
                {
                    LoadCategory();
                    LoadCompany();
                    LoadProductGridView();
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
        private void LoadProductGridView()
        {
            var listProduct = new ProductDa(true).GetProductList();
            gvProduct.DataSource = listProduct;
            gvProduct.DataBind();
        }
        protected void gvProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProduct.PageIndex = e.NewPageIndex;
            this.LoadProductGridView();
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
            txtProductName.Text = "";
            txtDescription.Text = "";
            txtSerialNo.Text = "";
            ddlCompany.SelectedValue = "0";
            ddlCategory.SelectedValue = "0";
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
                    Product product = new Product();

                    product.ProductName = txtProductName.Text;
                    product.Description = txtDescription.Text;
                    product.PrSerialNo = txtSerialNo.Text;
                    if (ddlInstallation.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Installation Type');window.close();", true);
                        return;
                    }
                    product.InstallationType = ddlInstallation.SelectedValue;
                    if (ddlCategory.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Category');window.close();", true);
                        return;
                    }
                    product.CategoryId = Convert.ToInt32((string) ddlCategory.SelectedValue);
                    if (ddlCompany.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Company Name');window.close();", true);
                        return;
                    }
                    product.CompanyId = Convert.ToInt32((string) ddlCompany.SelectedValue);

                    product.EntryBy = currentUser.UserId;
                    product.EntryDate = DateTime.Now;
                    product.EntryPC = WebUtility.GetIpAddress();

                    new ProductDa(false).Insert(product);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Inserted.');window.close();", true);
                    LoadProductGridView();
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
                    if (Session["productId"] != null)
                    {
                        int productId = Convert.ToInt32(Session["productId"]);
                        var selecteddata = new ProductDa(false).GetProductById(productId);
                        selecteddata.ProductName = txtProductName.Text;
                        selecteddata.Description = txtDescription.Text;
                        selecteddata.PrSerialNo = txtSerialNo.Text;
                        if (ddlInstallation.SelectedValue == "0")
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Installation Type');window.close();", true);
                            return;
                        }
                        selecteddata.InstallationType = ddlInstallation.SelectedValue;
                        if (ddlCategory.SelectedValue == "0")
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Category');window.close();", true);
                            return;
                        }
                        selecteddata.CategoryId = Convert.ToInt32((string) ddlCategory.SelectedValue);
                        if (ddlCompany.SelectedValue == "0")
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Company Name');window.close();", true);
                            return;
                        }
                        selecteddata.CompanyId = Convert.ToInt32((string) ddlCompany.SelectedValue);

                        selecteddata.ModifiedBy = currentUser.UserId;
                        selecteddata.ModifiedDate = DateTime.Now;
                        selecteddata.ModifiedPC = WebUtility.GetIpAddress();

                        new ProductDa(false).Update(selecteddata);
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Updated.');window.close();", true);
                        LoadProductGridView();
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

        protected void btnEditProduct_Click(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            btnUpdate.Visible = true;

            try
            {
                int productId = Convert.ToInt32(((ImageButton)sender).CommandArgument);
                Session["productId"] = productId;
                var selecteddata = new ProductDa(false).GetProductById(productId);
                txtProductName.Text = selecteddata.ProductName;
                txtDescription.Text = selecteddata.Description;
                txtSerialNo.Text = selecteddata.PrSerialNo;
              

                var compid = selecteddata.CompanyId;
                if (compid != null)
                {
                    var cq = new CompanyDa(true).GetCompanyById((int)compid);
                    var compName = cq.CompanyName;
                    LoadCompany();
                    if (ddlCompany.Items.Contains(ddlCompany.Items.FindByText(compName)))
                        ddlCompany.SelectedIndex = ddlCompany.Items.IndexOf(ddlCompany.Items.FindByText(compName));
                }

                var categoryid = selecteddata.CategoryId;
                if (categoryid != null)
                {
                    var iq = new CategoryDa(true).GetCategoryById((int)categoryid);
                    var categoryName = iq.CategoryName;
                    LoadCategory();
                    if (ddlCategory.Items.Contains(ddlCategory.Items.FindByText(categoryName)))
                        ddlCategory.SelectedIndex = ddlCategory.Items.IndexOf(ddlCategory.Items.FindByText(categoryName));
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        protected void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            try
            {
                int productId = Convert.ToInt32(((ImageButton)sender).CommandArgument);
                var selectedData = new ProductDa(false).GetProductById(productId);
                new ProductDa(false).Delete(selectedData);

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Deleted.');window.close();", true);
                LoadProductGridView();

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        #endregion
    }
}