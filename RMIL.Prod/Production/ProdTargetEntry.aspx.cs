using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RMIL.Prod.DAL;
using RMIL.Prod.EntityFramework;
using RMIL.Prod.Utility;

namespace RMIL.Prod.Production
{
    public partial class ProdTargetEntry : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                if (!IsPostBack)
                {
                    LoadRmProdTargetGridView();
                    LoadProduct();
                    LoadLine();
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
        private void LoadRmProdTargetGridView()
        {
            var trList = new ProdTargetDa(true).GetProdTargetList();
            gvProdTarget.DataSource = trList;
            gvProdTarget.DataBind();
        }
        protected void gvProdTarget_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProdTarget.PageIndex = e.NewPageIndex;
            this.LoadRmProdTargetGridView();
        }
        protected void LoadLine()
        {
            var lList = new ProductionLineDa(true).GetAllProductionLine();
            ddlLine.DataSource = lList.OrderBy(c => c.LineName);
            this.ddlLine.DataTextField = "LineName";
            this.ddlLine.DataValueField = "LineId";
            ddlLine.DataBind();
            ddlLine.DataSource = lList.OrderBy(c => c.LineName);
            ddlLine.DataBind();
            ddlLine.Items.Insert(0, new ListItem("---Please Select---", "0"));
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
        protected void LoadProductModel()
        {
            var modelList = new ProductModelDa(true).GetAllProductModel();
            ddlProductModel.DataSource = modelList;
            this.ddlProductModel.DataTextField = "PrModelName";
            this.ddlProductModel.DataValueField = "PrModelId";
            ddlProductModel.DataBind();
            ddlProductModel.DataSource = modelList;
            ddlProductModel.DataBind();
            ddlProductModel.Items.Insert(0, new ListItem("---Please Select---", "0"));
        }
        protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlProductModel.Enabled = false;
                ddlProductModel.Items.Clear();
                ddlProductModel.Items.Insert(0, new ListItem("Select Model", "0"));
                int productId = int.Parse(ddlProduct.SelectedItem.Value);
                if (productId > 0)
                {
                    var productModelList = new ProductModelDa(true).GetProductModelByProductId(productId);
                    if (productModelList.Any())
                    {
                        var q = productModelList.Select(p => new { p.PrModelId, p.PrModelName, DisplayText = p.PrModelName + " (" + p.ModelCode + ")" });
                        ddlProductModel.DataSource = q.OrderBy(c => c.PrModelName);
                        this.ddlProductModel.DataTextField = "DisplayText";
                        this.ddlProductModel.DataValueField = "PrModelId";
                        ddlProductModel.DataBind();
                        ddlProductModel.DataSource = q.OrderBy(c => c.PrModelName);
                        ddlProductModel.DataBind();
                        ddlProductModel.Items.Insert(0, new ListItem("Select Model", "0"));
                        ddlProductModel.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        private void ClearAll()
        {
            ddlLine.SelectedValue = "0";
            ddlProduct.SelectedValue = "0";
            ddlProductModel.SelectedValue = "0";
            txtQty.Text = "";
            dtpTarget.Clear();

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
                    ProdTarget prodTarget = new ProdTarget();

                    if (dtpTarget.SelectedDate != null)
                    {
                        var targetInfo = new ProdTargetDa(false).GetDailyTargetByIds(Convert.ToInt32(ddlLine.SelectedValue), Convert.ToInt32(ddlProductModel.SelectedValue), (DateTime)dtpTarget.SelectedDate);
                        if (targetInfo != null)
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('This line wise target is already inserted for selected date!.');window.close();", true);
                            return;
                        }
                    }

                    if (ddlLine.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Line');window.close();", true);
                        return;
                    }
                    prodTarget.LineId = Convert.ToInt32(ddlLine.SelectedValue);

                    if (ddlProduct.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Product');window.close();", true);
                        return;
                    }
                    prodTarget.ProductId = Convert.ToInt32(ddlProduct.SelectedValue);
                    if (ddlProductModel.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Product Model');window.close();", true);
                        return;
                    }
                    prodTarget.PrModelId = Convert.ToInt32(ddlProductModel.SelectedValue);
                    prodTarget.TQty = Convert.ToInt32(txtQty.Text);
                    prodTarget.TargetDate = dtpTarget.SelectedDate;

                    prodTarget.EntryBy = currentUser.UserId;
                    prodTarget.EntryDate = DateTime.Now;
                    prodTarget.EntryPC = WebUtility.GetIpAddress();

                    new ProdTargetDa(false).Insert(prodTarget);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Inserted.');window.close();", true);
                    LoadRmProdTargetGridView();
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
                    if (Session["tptId"] != null)
                    {
                        int tptId = Convert.ToInt32(Session["tptId"]);
                        var selecteddata = new ProdTargetDa(false).GetProdTargetById(tptId);

                        //if (dtpTarget.SelectedDate != null)
                        //{
                        //    var targetInfo = new ProdTargetDa(false).GetDailyTargetByIds(Convert.ToInt32(ddlLine.SelectedValue), Convert.ToInt32(ddlProductModel.SelectedValue), (DateTime)dtpTarget.SelectedDate);
                        //    if (targetInfo != null)
                        //    {
                        //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('This line wise target is already inserted for selected date!.');window.close();", true);
                        //        return;
                        //    }
                        //}

                        if (ddlLine.SelectedValue == "0")
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Line');window.close();", true);
                            return;
                        }
                        selecteddata.LineId = Convert.ToInt32(ddlLine.SelectedValue);

                        if (ddlProduct.SelectedValue == "0")
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Product');window.close();", true);
                            return;
                        }
                        selecteddata.ProductId = Convert.ToInt32(ddlProduct.SelectedValue);
                        if (ddlProductModel.SelectedValue == "0")
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Product Model');window.close();", true);
                            return;
                        }
                        selecteddata.PrModelId = Convert.ToInt32(ddlProductModel.SelectedValue);
                        selecteddata.TQty = Convert.ToInt32(txtQty.Text);
                        selecteddata.TargetDate = dtpTarget.SelectedDate;

                        selecteddata.ModifiedBy = currentUser.UserId;
                        selecteddata.ModifiedDate = DateTime.Now;
                        selecteddata.ModifiedPC = WebUtility.GetIpAddress();

                        new ProdTargetDa(false).Update(selecteddata);
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Updated.');window.close();", true);
                        LoadRmProdTargetGridView();
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

        protected void btnEditProdTarget_Click(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            btnUpdate.Visible = true;

            try
            {
                int tptId = Convert.ToInt32(((ImageButton)sender).CommandArgument);
                Session["tptId"] = tptId;
                var selecteddata = new ProdTargetDa(false).GetProdTargetById(tptId);
                var lineid = selecteddata.LineId;
                if (lineid != null)
                {
                    var q = new ProductionLineDa(true).GetProductionLineById((int)lineid);
                    var ltname = q.LineName;
                    LoadLine();
                    if (ddlLine.Items.Contains(ddlLine.Items.FindByText(ltname)))
                        ddlLine.SelectedIndex = ddlLine.Items.IndexOf(ddlLine.Items.FindByText(ltname));
                }
                var pid = selecteddata.ProductId;
                {
                    if (pid != null)
                    {
                        var cq = new ProductDa().GetProductById((int) pid);
                        var pName = cq.ProductName;
                        LoadProduct();
                        if (ddlProduct.Items.Contains(ddlProduct.Items.FindByText(pName)))
                            ddlProduct.SelectedIndex = ddlProduct.Items.IndexOf(ddlProduct.Items.FindByText(pName));
                    }
                }
                var modelId = selecteddata.PrModelId;
                if (modelId != null)
                {
                    var q = new ProductModelDa(true).GetProductModelById((int)modelId);
                    var mname = q.PrModelName;
                    LoadProductModel();
                    if (ddlProductModel.Items.Contains(ddlProductModel.Items.FindByText(mname)))
                        ddlProductModel.SelectedIndex = ddlProductModel.Items.IndexOf(ddlProductModel.Items.FindByText(mname));
                }
                txtQty.Text = selecteddata.TQty.ToString();
                dtpTarget.SelectedDate = selecteddata.TargetDate;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        protected void OnDataBound(object sender, EventArgs e)
        {
            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            for (int i = 0; i < gvProdTarget.Columns.Count; i++)
            {
                TableHeaderCell cell = new TableHeaderCell();
                TextBox txtSearch = new TextBox();
                txtSearch.Attributes["placeholder"] = gvProdTarget.Columns[i].HeaderText;
                txtSearch.CssClass = "search_textbox";
                cell.Controls.Add(txtSearch);
                row.Controls.Add(cell);
            }
            gvProdTarget.HeaderRow.Parent.Controls.AddAt(1, row);
        }
        #endregion
    }
}