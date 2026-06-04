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
    public partial class RmilDistributionReturn : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                if (!IsPostBack)
                {
                    LoadPackagingType();
                    submitall.Visible = false;
                    delaerdetails.Visible = false;
                    todepo.Visible = false;
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
            ddldealercode.Text = "";
            ddldealername.Text = "";
            ddldealeraddress.Text = "";
            ddlVehiclenumber.Text = "";
            ddlpackagingtype.SelectedValue = "0";
            ddlmaster.Text = "";
            submitall.Visible = false;
            todepo.Visible = false;
            delaerdetails.Visible = false;
        }
        #endregion
        #region Event Generated Method
        private void LoadPackagingType()
        {
            ddlpackagingtype.Items.Clear();
            ddlpackagingtype.Items.Add(new ListItem("--Select Product Packing--", ""));
            ddlpackagingtype.Items.Add(new ListItem("Master Cartoon", "1"));
            ddlpackagingtype.Items.Add(new ListItem("Single Products", "2"));
        }
        protected void txtBarCode_TextChanged(object sender, EventArgs e)
        {
            BindGrid();
            GetSingleProductDistributionDetails();
        }

        // SingleProductDetails
        private void GetSingleProductDistributionDetails()
        {
            var BarcodeNo = ddlmaster.Text;
            if (Convert.ToInt32(ddlpackagingtype.SelectedValue) == 1)
            {
                var BoxMaster = new DistributionDa(true).GetProductDetailsBox(BarcodeNo);
                var SingleProduct = new DistributionDa(true).GetProductDetails(BoxMaster.ProductModelBarcode);
                ddldealername.ReadOnly = true;
                ddldealeraddress.ReadOnly = true;
                ddlVehiclenumber.ReadOnly = true;
                ddldealercode.ReadOnly = true;
                ddldeponame.ReadOnly = true;
                if (SingleProduct.DealerCode != null)
                {
                    delaerdetails.Visible = true;
                    ddldealername.Text = SingleProduct.DealerName;
                    ddldealeraddress.Text = SingleProduct.DealerAddress;
                    ddlVehiclenumber.Text = SingleProduct.VehicleNumber;
                    ddldealercode.Text = SingleProduct.DealerCode.ToString();
                }
                else
                {
                    todepo.Visible = true;
                    delaerdetails.Visible = false;
                    ddldeponame.Text = SingleProduct.ToDepoName;
                }
            }
            else
            {
                var SingleProduct = new DistributionDa(true).GetProductDetails(BarcodeNo);
                ddldealername.ReadOnly = true;
                ddldealeraddress.ReadOnly = true;
                ddlVehiclenumber.ReadOnly = true;
                ddldealercode.ReadOnly = true;
                ddldeponame.ReadOnly = true;
                if(SingleProduct.DealerCode != null)
                {
                    delaerdetails.Visible = true;
                    todepo.Visible = false;
                    ddldealername.Text = SingleProduct.DealerName;
                    ddldealeraddress.Text = SingleProduct.DealerAddress;
                    ddlVehiclenumber.Text = SingleProduct.VehicleNumber;
                    ddldealercode.Text = SingleProduct.DealerCode.ToString();
                }
                else
                {
                    todepo.Visible = true;
                    delaerdetails.Visible = false;
                    ddldeponame.Text = SingleProduct.ToDepoName;
                }
            }
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

            if (!string.IsNullOrWhiteSpace(masterbarcode))
            {
                var assignedProductsList = new DistributionDa(true)
                    .GetAllAssingedBox(masterbarcode);
                Session["Cart"] = assignedProductsList;
                gvCart.DataSource = assignedProductsList;
                gvCart.DataBind();
                submitall.Visible = true;
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
                    using (var scope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        using (RMILCSDbEntities db = new RMILCSDbEntities())
                        {
                            UserInfo currentUser = (UserInfo)Session[WebUtility.SessionCurrentUserObj];
                            if (Convert.ToInt32(ddlpackagingtype.SelectedValue) == 1)
                            {
                                foreach (GridViewRow row in gvCart.Rows)
                                {
                                    var Packedbarcode = new DistributionDa(true).GetAllSingleDistribution(row.Cells[1].Text).FirstOrDefault();
                                    var deporeturnlog = new DepoReturnLog()
                                    {
                                        DeporeturnBarcode = row.Cells[1].Text,
                                        DealerCode = Packedbarcode.DealerCode.ToString(),
                                        DealearTruckNumber = Packedbarcode.VehicleNumber,
                                        DealerAddress = Packedbarcode.DealerAddress,
                                        DealerName = Packedbarcode.DealerName,
                                        DepoID = Packedbarcode.DepotId,
                                        DepoName = Packedbarcode.DepotName,
                                        Entryby = currentUser.UserId,
                                        EntryDate = DateTime.Now
                                    };
                                    new DistributionDa(true).InsertLog(deporeturnlog);
                                    new DistributionDa(true).UpdateReturnProductInfo(Packedbarcode.BarcodeNo);
                                }
                              }
                            else
                            {
                                var Packedbarcode = new DistributionDa(true).GetAllSingleDistribution(ddlmaster.Text).FirstOrDefault();
                                var deporeturnlog = new DepoReturnLog()
                                {
                                    DeporeturnBarcode = ddlmaster.Text,
                                    DealerCode = Packedbarcode.DealerCode.ToString(),
                                    DealearTruckNumber = Packedbarcode.VehicleNumber,
                                    DealerAddress = Packedbarcode.DealerAddress,
                                    DealerName = Packedbarcode.DealerName,
                                    DepoID = Packedbarcode.DepotId,
                                    DepoName = Packedbarcode.DepotName,
                                    Entryby = currentUser.UserId,
                                    EntryDate = DateTime.Now
                                };
                                new DistributionDa(true).InsertLog(deporeturnlog);
                                new DistributionDa(true).UpdateReturnProductInfo(Packedbarcode.BarcodeNo);
                            }
                           
                            scope.Complete();
                            todepo.Visible = false;
                            delaerdetails.Visible = false;
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Sucessfully Inserted.');window.close();", true);
                            ClearAll();
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