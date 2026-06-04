using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using RMIL.Prod.DAL;
using RMIL.Prod.EntityFramework;
using RMIL.Prod.Utility;
using System.Linq;

namespace RMIL.Prod.SetupPages
{
    public partial class DealerSetup : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                if (!IsPostBack)
                {
                 //   LoadCategory();
                    LoadDepoList();
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
            DealerSetupDa DealerSetup = new DealerSetupDa();
            var listProduct = DealerSetup.GetAllDealer();
            gvProduct.DataSource = listProduct;
            gvProduct.DataBind();
        }
        protected void gvProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProduct.PageIndex = e.NewPageIndex;
            this.LoadProductGridView();
        }
        protected void LoadDepoList()
        {
            var lList = new DepoDa().GetAllDepoList();
            ddldepo.DataSource = lList.OrderBy(c => c.DepotName);
            this.ddldepo.DataTextField = "DepotName";
            this.ddldepo.DataValueField = "DepotId";
            ddldepo.DataBind();
            ddldepo.DataSource = lList.OrderBy(c => c.DepotName);
            ddldepo.DataBind();
            ddldepo.Items.Insert(0, new ListItem("---Please Select---", "0"));
        }
        private void ClearAll()
        {
            txtProductName.Text = "";
            txtDescription.Text = "";
            ddlVehicle.Text = "";
            ddlName.Text = "";
            ddldepo.SelectedValue = "0";
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
                    DealerDetails product = new DealerDetails();
                    if (txtProductName.Text==null)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Enter Dealer Code');", true);
                        return;
                    }
                    else
                    {
                        product.DealerCode = Convert.ToInt32(txtProductName.Text);
                    }
                    if (ddlName.Text == null)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Enter Dealer Name');", true);
                        return;
                    }
                    else
                    {
                        product.DealerName = ddlName.Text;
                    }
                    if (txtDescription.Text == null)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Enter Phone Number');", true);
                        return;
                    }
                    else
                    {
                        product.ContactNo = txtDescription.Text;
                    }
                    if (ddldepo.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Depo Name');", true);
                        return;
                    }
                    else
                    {
                        product.DepoID =Convert.ToInt32(ddldepo.SelectedValue);
                    }
                    if (ddlVehicle.Text == null)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Depo Name');", true);
                        return;
                    }
                    else
                    {
                        product.trucknumber = ddlVehicle.Text;
                    }
                    if (ddladdress.Text == null)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Enter Address');", true);
                        return;
                    }
                    else
                    {
                        product.Address = ddladdress.Text;
                    }
                    product.EntryBy = currentUser.UserId;
                    product.EntryDate = DateTime.Now;
                    product.EntryPC = WebUtility.GetIpAddress();
                    DealerSetupDa DealerSetup = new DealerSetupDa();
                    DealerSetup.AddNewUser(product);
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
        protected void btnEditProduct_Click(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            btnUpdate.Visible = true;

            try
            {
                int productId = Convert.ToInt32(((ImageButton)sender).CommandArgument);
                Session["productId"] = productId;
                DealerSetupDa DealerSetup = new DealerSetupDa();
                var selecteddata = DealerSetup.GetSingleDealerList(productId);
                txtProductName.Text = selecteddata.DealerCode.ToString();
                ddlName.Text = selecteddata.DealerName;
                txtDescription.Text = selecteddata.ContactNo;
                ddlVehicle.Text = selecteddata.trucknumber;
                ddladdress.Text = selecteddata.Address;
                var compid = selecteddata.DepoID;
                if (compid != null)
                {
                    var cq = new DepoDa(true).GetSingleDepo((int)compid);
                    var compName = cq.DepotId.ToString();
                    LoadDepoList();
                    if (ddldepo.Items.Contains(ddldepo.Items.FindByValue(compName)))
                        ddldepo.SelectedIndex = ddldepo.Items.IndexOf(ddldepo.Items.FindByValue(compName));
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
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
                        DealerSetupDa DealerSetup = new DealerSetupDa();
                        var product = DealerSetup.GetSingleDealerList(productId);
                        product.DealerCode = Convert.ToInt32(txtProductName.Text);
                        product.DealerName = ddlName.Text;
                        product.ContactNo = txtDescription.Text;
                        product.DepoID = Convert.ToInt32(ddldepo.SelectedValue);
                        product.trucknumber = ddlVehicle.Text;
                        product.Address = ddladdress.Text;
                        product.ModifiedBy = currentUser.UserId;
                        product.ModifiedDate = DateTime.Now;
                        product.ModifiedPC = WebUtility.GetIpAddress();
                        DealerSetup.Update(product);
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Updated.');window.close();", true);
                        LoadProductGridView();
                        btnSave.Visible = true;
                        btnUpdate.Visible = false;
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

     
        ////protected void btnDeleteProduct_Click(object sender, EventArgs e)
        ////{
        ////    try
        ////    {
        ////        int productId = Convert.ToInt32(((ImageButton)sender).CommandArgument);
        ////        var selectedData = new ProductDa(false).GetProductById(productId);
        ////        new ProductDa(false).Delete(selectedData);

        ////        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Deleted.');window.close();", true);
        ////        LoadProductGridView();

        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        Response.Write(ex.Message);
        ////    }
        ////}
        #endregion
    }
}