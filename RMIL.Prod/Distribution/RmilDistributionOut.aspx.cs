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
    public partial class RmilDistributionOut : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                if (!IsPostBack)
                {
                    LoadLine();
                    Deliverytype();
                    submitall.Visible = false;
                    LoadPackagingType();
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
            ddlmaster.Text = "";
            submitall.Visible = false;
            gvCart.DataSource = null;
            gvCart.DataBind();
        }
        #endregion
        #region Event Generated Method
        protected void ddlproductcodename_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ModelCode = ddlproductcode.Text;
            if (ModelCode != null)
            {
                var modelname = new ProductModelDa(true).GetAllProductModel().Where(x => x.ModelCode == Convert.ToInt32(ModelCode)).FirstOrDefault();
                ddlmodelname.Text = modelname.PrModelName;
            }
        }
        protected void ddldealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            DealerSetupDa DealerSetup = new DealerSetupDa();
            var dealercode = ddldealercode.Text;
            var dealerdetails = DealerSetup.GetSingleDealer(Convert.ToInt32(dealercode));
            if (dealerdetails != null)
            {
                ddldealername.Text = dealerdetails.DealerName;
                ddldealeraddress.Text = dealerdetails.Address;
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Enter Correct Dealer Code');", true);
            }
        }
        protected void Deliverytype()
        {
            var lList = new DAL.DistributionDa().GetAllDistributionType().Where(x=>x.Position== "Dealer");
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
        protected void txtBarCode_TextChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
        private void LoadPackagingType()
        {
            ddlpackagingtype.Items.Clear();
            ddlpackagingtype.Items.Add(new ListItem("--Select Product Packing---", "0"));
            ddlpackagingtype.Items.Add(new ListItem("Master Cartoon", "1"));
            ddlpackagingtype.Items.Add(new ListItem("Single Products", "2"));
        }
        private void BindGrid()
        {
            string masterbarcode = ddlmaster.Text;
            if(Convert.ToInt32(ddlpackagingtype.SelectedValue)==1)
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
                    else if(SingleProductBoxbarcode != null)
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
                        UserInfo currentUser = (UserInfo)Session[WebUtility.SessionCurrentUserObj];
                        var userassdepoid = new DepoDa().GetUserWiseDepoID(currentUser.UserId);
                        if(userassdepoid != null)
                        {
                            var Deponame = new DAL.DepoDa().GetAllDepoList().Where(x => x.DepotId == userassdepoid.DepoID).FirstOrDefault();
                            if (ddldeliverytype.SelectedValue == "Dealer")
                            {
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
                                        Packedbarcode.DealerCode = Convert.ToInt32(ddldealercode.Text);
                                        Packedbarcode.DealerName = ddldealername.Text;
                                        Packedbarcode.IsSend = true;
                                        Packedbarcode.Sender = currentUser.UserId;
                                        Packedbarcode.IsTO = true;
                                        Packedbarcode.OCSender = currentUser.UserId;
                                        Packedbarcode.OCSendDate = DateTime.Now;
                                        Packedbarcode.Position = "Dealer";
                                        Packedbarcode.StaffId = currentUser.StaffID;
                                        Packedbarcode.Username = currentUser.UserId;
                                        Packedbarcode.BarcodeNo = row.Cells[1].Text;
                                        Packedbarcode.DeliveryTypeID = Convert.ToInt32(ddldeliverytype.SelectedValue);
                                        Packedbarcode.EntryDate = DateTime.Now;
                                        Packedbarcode.EntryBy = currentUser.UserId;
                                        Packedbarcode.SendDate = DateTime.Now;
                                        Packedbarcode.DealerAddress = ddldealeraddress.Text;
                                        Packedbarcode.VehicleNumber = ddlVehiclenumber.Text;
                                        new DistributionDa(true).Update(Packedbarcode);
                                    }
                                }
                            }
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