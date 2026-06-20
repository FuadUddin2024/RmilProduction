using RMIL.Prod.DAL;
using RMIL.Prod.EntityFramework;
using RMIL.Prod.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RMIL.Prod.Distribution
{
    public partial class RmilDistributionDepoOut : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                if (!IsPostBack)
                {
                    LoadLine();
                    LoadPackagingType();
                    Deliverytype();
                    submitall.Visible = false;
                    ToLoadLine();
                }
            }
            else
            {
                Response.Redirect("~/Account/Logout.aspx");
            }
        }
        #endregion
        #region User Defined Methods
        private void ClearAll()
        {
            ddlpackagingtype.SelectedValue = "0";
            ddlmaster.Text = "";
            submitall.Visible = false;
            gvCart.DataSource = null;
            gvCart.DataBind();
        }
        #endregion
        #region Event Generated Method
        private void LoadPackagingType()
        {
            ddlpackagingtype.Items.Clear();
            ddlpackagingtype.Items.Add(new ListItem("--Select Select Product Packing---", "0"));
            ddlpackagingtype.Items.Add(new ListItem("Master Cartoon", "1"));
            ddlpackagingtype.Items.Add(new ListItem("Single Products", "2"));
        }
        protected void ddlproductcodename_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ModelCode = ddlproductcode.Text;
            if (ModelCode != null)
            {
                var modelname = new ProductModelDa(true).GetAllProductModel().Where(x => x.ModelCode == Convert.ToInt32(ModelCode)).FirstOrDefault();
                ddlmodelname.Text = modelname.PrModelName;
            }
        }
        protected void Deliverytype()
        {
            var lList = new DAL.DistributionDa().GetAllDistributionType().Where(x=>x.Position== "Depo");
            ddldeliverytype.DataSource = lList.OrderBy(c => c.DeliveryTypeName);
            this.ddldeliverytype.DataTextField = "DeliveryTypeName";
            this.ddldeliverytype.DataValueField = "Position";
            ddldeliverytype.DataBind();
            ddldeliverytype.DataSource = lList.OrderBy(c => c.DeliveryTypeName);
            ddldeliverytype.DataBind();
        }
        protected void LoadLine()
        {
            var lList = new DAL.DepoDa().GetAllDepoList();
            ddldepo.DataSource = lList.OrderBy(c => c.DepotName);
            this.ddldepo.DataTextField = "DepotName";
            this.ddldepo.DataValueField = "DepotId";
            ddldepo.DataBind();
            ddldepo.DataSource = lList.OrderBy(c => c.DepotName);
            ddldepo.DataBind();
            ddldepo.Items.Insert(0, new ListItem("---Please Select---", "0"));
        }
        protected void ToLoadLine()
        {
            var lList = new DAL.DepoDa().GetAllDepoList();
            ddltodepo.DataSource = lList.OrderBy(c => c.DepotName);
            this.ddltodepo.DataTextField = "DepotName";
            this.ddltodepo.DataValueField = "DepotId";
            ddltodepo.DataBind();
            ddltodepo.DataSource = lList.OrderBy(c => c.DepotName);
            ddltodepo.DataBind();
            ddltodepo.Items.Insert(0, new ListItem("---Please Select---", "0"));
        }
        protected void txtBarCode_TextChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
        private DataTable CreateTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("masterbarcode");
            dt.Columns.Add("singlebarcode");
            return dt;
        }
        private void BindGrid()
        {
            string masterbarcode = ddlmaster.Text;
            if (Convert.ToInt32(ddlpackagingtype.SelectedValue) == 1)
            {
                var assignedProductsList = new DistributionDa(true).GetAllAssingedBox(masterbarcode);
                var boxbarcodedetails = new DistributionDa(true).GetProductDetailsMaster(masterbarcode).FirstOrDefault();
                if (assignedProductsList.Count > 0)
                {
                    ddlproductcode.Text = boxbarcodedetails.ModelCode.ToString();
                    ddlmodelname.Text = boxbarcodedetails.PrModelName;
                    Session["Cart"] = assignedProductsList;
                    gvCart.DataSource = assignedProductsList;
                    gvCart.DataBind();
                    submitall.Visible = true;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Please Enter Correct Barcode');", true);
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(masterbarcode))
                {
                    var assignedProductsList = new DistributionDa(true).GetAllAssingedBoxSingle(masterbarcode).ToList();
                    var boxbarcodedetails = new DistributionDa(true).GetProductDetailsMaster(assignedProductsList[0].BoxBarCode).FirstOrDefault();
                    var SingleProductBoxbarcode = new DistributionDa(true).GetProductDetailsMasterProduct(assignedProductsList[0].BoxBarCode).FirstOrDefault();
                    if (boxbarcodedetails != null)
                    {
                        ddlproductcode.Text = boxbarcodedetails.ModelCode.ToString();
                        ddlmodelname.Text = boxbarcodedetails.PrModelName;
                        Session["Cart"] = assignedProductsList;
                        gvCart.DataSource = assignedProductsList;
                        gvCart.DataBind();
                        submitall.Visible = true;
                    }
                    else if (SingleProductBoxbarcode != null)
                    {
                        ddlproductcode.Text = SingleProductBoxbarcode.ModelCode.ToString();
                        ddlmodelname.Text = SingleProductBoxbarcode.PrModelName;
                        Session["Cart"] = assignedProductsList;
                        gvCart.DataSource = assignedProductsList;
                        gvCart.DataBind();
                        submitall.Visible = true;
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Please Enter Correct Barcode');", true);
                    }
                }
            }
        }

        protected void gvCart_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteRow")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                var dt = Session["Cart"] as List<MasterBoxAssigned>;

                if (dt != null && dt.Count > index)
                {
                    dt.RemoveAt(index);

                    Session["Cart"] = dt;

                    gvCart.DataSource = dt;
                    gvCart.DataBind();
                }
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SaveAssignedProducts();
        }
        private bool SaveAssignedProducts()
        {
            bool IsAdded = true;
            // Save here
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {

                try
                {
                    using (RMILCSDbEntities db = new RMILCSDbEntities())
                    {
                        if (ddlproductcode.Text=="")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Please Provide Product Code.');", true);
                        }
                        if (ddldepo.SelectedValue == "0")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Please select From Depo.');", true);
                        }
                        if (ddldeliverytype.SelectedValue == "0")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Please select Delivery Type.');", true);
                        }
                        if (ddlpackagingtype.SelectedValue == "0")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Please select Packaging Type.');", true);
                        }
                        UserInfo currentUser = (UserInfo)Session[WebUtility.SessionCurrentUserObj];
                        var userassdepoid = new DepoDa().GetUserWiseDepoID(currentUser.UserId);
                        if(userassdepoid != null)
                        {
                            var Deponame = new DAL.DepoDa().GetAllDepoList().Where(x => x.DepotId == userassdepoid.DepoID).FirstOrDefault();
                            DepoToDepoLogFileWeight LogFile = new DepoToDepoLogFileWeight();
                            foreach (GridViewRow row in gvCart.Rows)
                            {
                                var Packedbarcode = new DistributionDa(true).GetAllSingleDistribution(row.Cells[1].Text).FirstOrDefault();
                                if (Packedbarcode.IsSend == true)
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Already Sent - " + row.Cells[1].Text + "');", true);
                                    return false;
                                }
                                else
                                {
                                    Packedbarcode.TodepoID = Convert.ToInt32(ddltodepo.SelectedValue);
                                    Packedbarcode.ToDepoName = ddltodepo.SelectedItem.Text;
                                    Packedbarcode.SendDate = DateTime.Now;
                                    Packedbarcode.Sender = currentUser.UserId;
                                    Packedbarcode.Position = "Depot";
                                    Packedbarcode.BarcodeNo = row.Cells[1].Text;
                                    Packedbarcode.DeliveryType = ddldeliverytype.SelectedValue;
                                    Packedbarcode.EntryDate = DateTime.Now;
                                    Packedbarcode.EntryBy = currentUser.UserId;
                                    Packedbarcode.DepotId = Deponame.DepotId;
                                    Packedbarcode.DepotName = Deponame.DepotName;
                                    Packedbarcode.Sender= currentUser.UserId;
                                    Packedbarcode.SendDate = DateTime.Now;
                                    new DistributionDa(true).Update(Packedbarcode);
                                    LogFile.BarcodeNo= row.Cells[1].Text;
                                    LogFile.FromDepoID = Deponame.DepotId;
                                    LogFile.ToDepoID = Convert.ToInt32(ddltodepo.SelectedValue);
                                    LogFile.SentBy = currentUser.UserId;
                                    LogFile.SentDate = DateTime.Now;
                                    LogFile.EntryBy= currentUser.UserId;
                                    LogFile.EntryDate = DateTime.Now;
                                    new DistributionDa(true).InsertLog(LogFile);
                                }
                            }
                            ClearAll();
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Successfully Inserted.');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('User is not Assign yet');", true);
                        }
                    }
                  
                }
                catch (Exception ex)
                {
                    IsAdded = false;
                    Response.Write(ex.Message);
                }

            }
            else
            {
                Response.Redirect("~/Account/Logout.aspx");
            }
            return IsAdded;
        }
        #endregion

    }
}