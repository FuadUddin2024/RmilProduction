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
    public partial class ModelSetup : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                if (!IsPostBack)
                {
                    LoadProduct();
                    LoadM1();
                    LoadM2();
                    LoadProductModelGridView();
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
        private void LoadProductModelGridView()
        {
            var prModelList = new ProductModelDa(true).GetProductModelList();
            gvProductModel.DataSource = prModelList;
            gvProductModel.DataBind();
        }
        protected void gvProductModel_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProductModel.PageIndex = e.NewPageIndex;
            this.LoadProductModelGridView();
        }
        protected void LoadProduct()
        {
            var productList = new ProductDa(true).GetAllProduct();
            ddlProduct.DataSource = productList;
            this.ddlProduct.DataTextField = "ProductName";
            this.ddlProduct.DataValueField = "ProductId";
            ddlProduct.DataBind();
            ddlProduct.DataSource = productList;
            ddlProduct.DataBind();
            ddlProduct.Items.Insert(0, new ListItem("---Please Select---", "0"));
        }
        protected void LoadM1()
        {
            var mnList = new MonthCalcDa(true).GetAllMonthCalc();
            ddlWpMf.DataSource = mnList;
            this.ddlWpMf.DataTextField = "MonthNo";
            this.ddlWpMf.DataValueField = "MonthNo";
            ddlWpMf.DataBind();
            ddlWpMf.DataSource = mnList;
            ddlWpMf.DataBind();
            ddlWpMf.Items.Insert(0, new ListItem("---Please Select Month---", "0"));
        }
        protected void LoadM2()
        {
            var mnList = new MonthCalcDa(true).GetAllMonthCalc();
            ddlWpSales.DataSource = mnList;
            this.ddlWpSales.DataTextField = "MonthNo";
            this.ddlWpSales.DataValueField = "MonthNo";
            ddlWpSales.DataBind();
            ddlWpSales.DataSource = mnList;
            ddlWpSales.DataBind();
            ddlWpSales.Items.Insert(0, new ListItem("---Please Select Month---", "0"));
        }
        private void ClearAll()
        {
            txtPrModelName.Text = "";
            txtModelCode.Text = "";
            ddlProduct.SelectedValue = "0";
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
                    int modelCode = Convert.ToInt32(txtModelCode.Text);
                    var modelInfo = new ProductModelDa(true).GetModelByModelCode(modelCode);
                    if (modelInfo != null)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('This Model Code is already exists.');window.close();", true);
                        return;
                    }

                    ProductModel productModel = new ProductModel();
                    productModel.ModelCode = Convert.ToInt32(txtModelCode.Text);
                    //productModel.UnitRate = Convert.ToDecimal(txtUnitRate.Text);
                    productModel.PrModelName = txtPrModelName.Text;

                    if (ddlProduct.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Product Name');window.close();", true);
                        return;
                    }
                    productModel.ProductId = Convert.ToInt32(ddlProduct.SelectedValue);

                    if (ddlWpMf.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Warranty Period From MF');window.close();", true);
                        return;
                    }
                    productModel.WarrantyPeriodFromMf = Convert.ToInt32(ddlWpMf.SelectedValue);

                    if (ddlWpSales.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Warranty Period From Sales');window.close();", true);
                        return;
                    }
                    productModel.WarrantyPeriodFromSales = Convert.ToInt32(ddlWpSales.SelectedValue);

                    productModel.EntryBy = currentUser.UserId;
                    productModel.EntryDate = DateTime.Now;
                    productModel.EntryPC = WebUtility.GetIpAddress();

                    new ProductModelDa(false).Insert(productModel);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Inserted.');window.close();", true);
                    LoadProductModelGridView();
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
                    if (Session["prModelId"] != null)
                    {
                        int prModelId = Convert.ToInt32(Session["prModelId"]);
                        var selecteddata = new ProductModelDa(false).GetProductModelById(prModelId);
                        selecteddata.ModelCode = Convert.ToInt32(txtModelCode.Text);
                        //selecteddata.UnitRate = Convert.ToDecimal(txtUnitRate.Text);
                        selecteddata.PrModelName = txtPrModelName.Text;

                        if (ddlProduct.SelectedValue == "0")
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Product Name');window.close();", true);
                            return;
                        }
                        selecteddata.ProductId = Convert.ToInt32(ddlProduct.SelectedValue);

                        if (ddlWpMf.SelectedValue == "0")
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Warranty Period From MF');window.close();", true);
                            return;
                        }
                        selecteddata.WarrantyPeriodFromMf = Convert.ToInt32(ddlWpMf.SelectedValue);

                        if (ddlWpSales.SelectedValue == "0")
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Warranty Period From Sales');window.close();", true);
                            return;
                        }
                        selecteddata.WarrantyPeriodFromSales = Convert.ToInt32(ddlWpSales.SelectedValue);

                        selecteddata.ModifiedBy = currentUser.UserId;
                        selecteddata.ModifiedDate = DateTime.Now;
                        selecteddata.ModifiedPC = WebUtility.GetIpAddress();

                        new ProductModelDa(false).Update(selecteddata);
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Updated.');window.close();", true);
                        LoadProductModelGridView();
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

        protected void btnEditProductModel_Click(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            btnUpdate.Visible = true;

            try
            {
                int prModelId = Convert.ToInt32(((ImageButton)sender).CommandArgument);
                Session["prModelId"] = prModelId;
                var selecteddata = new ProductModelDa(false).GetProductModelById(prModelId);
                txtModelCode.Text = selecteddata.ModelCode.ToString();
                //txtUnitRate.Text = selecteddata.UnitRate.ToString();
                txtPrModelName.Text = selecteddata.PrModelName;

                var pid = selecteddata.ProductId;
                {
                    var cq = new ProductDa().GetProductById(pid);
                    var pName = cq.ProductName;
                    LoadProduct();
                    if (ddlProduct.Items.Contains(ddlProduct.Items.FindByText(pName)))
                        ddlProduct.SelectedIndex = ddlProduct.Items.IndexOf(ddlProduct.Items.FindByText(pName));
                }
                ddlWpMf.SelectedValue = selecteddata.WarrantyPeriodFromMf.ToString();
                ddlWpSales.SelectedValue = selecteddata.WarrantyPeriodFromSales.ToString();
            }
            catch (Exception ex)
            {

                Response.Write(ex.Message);
            }
        }
        protected void btnDeleteProductModel_Click(object sender, EventArgs e)
        {
            try
            {
                int itemId = Convert.ToInt32(((ImageButton)sender).CommandArgument);
                var selectedData = new ProductModelDa(false).GetProductModelById(itemId);
                new ProductModelDa(false).Delete(selectedData);

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Deleted.');window.close();", true);
                LoadProductModelGridView();

            }
            catch (Exception ex)
            {

                Response.Write(ex.Message);
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var mcode = txtSearch.Text;
            try
            {
                if (mcode != "")
                {
                    var modelList = new ProductModelDa(true).GetProductModelByModelCode(Convert.ToInt32(mcode));

                    if (modelList.Any())
                    {
                        gvProductModel.DataSource = modelList;
                        gvProductModel.DataBind();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('This model code does not exists.');window.close();", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please input the model code.');window.close();", true);
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