using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RMIL.Prod.DAL;
using RMIL.Prod.EntityFramework;
using RMIL.Prod.Utility;

namespace RMIL.Prod.Production
{
    public partial class RmProductionWeightScale : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                if (!IsPostBack)
                {
                    txtLastTrId.Text = new RmilProdMasterDa(true).LastTransactionId().ToString();
                    LoadProduct();
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
        protected void LoadProduct()
        {
            var productList = new ProductDa(true).GetAllProduct();
            ddlProduct.DataSource = productList.OrderBy(c => c.ProductName);
            this.ddlProduct.DataTextField = "ProductName";
            this.ddlProduct.DataValueField = "ProductId";
            ddlProduct.DataBind();
            ddlProduct.DataSource = productList.OrderBy(c => c.ProductName);
            ddlProduct.DataBind();
            ddlProduct.Items.Insert(0, new ListItem("---Please Select---", "0"));
        }
        protected void LoadProductModel()
        {
            var productModelList = new ProductModelDa(true).GetAllProductModel();
            ddlProductModel.DataSource = productModelList.OrderBy(c => c.PrModelName);
            this.ddlProductModel.DataTextField = "PrModelName";
            this.ddlProductModel.DataValueField = "PrModelId";
            ddlProductModel.DataBind();
            ddlProductModel.DataSource = productModelList.OrderBy(c => c.PrModelName);
            ddlProductModel.DataBind();
            ddlProductModel.Items.Insert(0, new ListItem("---Please Select---", "0"));
        }

        private void ClearAll()
        {
            ddlProduct.SelectedValue = "0";
            ddlProductModel.SelectedValue = "0";
            txtQty.Text = "";
            txtSuplier.Text = "";
            dtpProduction.Clear();
            ddlincharge.Text = "";
        }

        public int GetWeekNumber()
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return weekNum;
        }
        #endregion
        #region Event Generated Method
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
                    if (productModelList != null)
                    {
                        ddlProductModel.DataSource = productModelList.OrderBy(c => c.PrModelName);
                        this.ddlProductModel.DataTextField = "PrModelName";
                        this.ddlProductModel.DataValueField = "PrModelId";
                        ddlProductModel.DataBind();
                        ddlProductModel.DataSource = productModelList.OrderBy(c => c.PrModelName);
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                UserInfo currentUser = (UserInfo)Session[WebUtility.SessionCurrentUserObj];
                try
                {
                    using (var scope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        using (RMILCSDbEntities db = new RMILCSDbEntities())
                        {
                            // Insert into RmilProdMaster
                            RmilProdMaster rmilProdMaster = new RmilProdMaster();

                            Random numberGenerator = new Random();
                            int ranNumber = numberGenerator.Next(1000, 2647);

                            var ranUniqueNo = rmilProdMaster.RmPmId +
                                              DateTime.Now.Year.ToString().Substring(2) +
                                              DateTime.Now.Month.ToString().PadLeft(2, '0') +
                                              DateTime.Now.Day.ToString().PadLeft(2, '0') + ranNumber;


                            if (ddlProductModel.SelectedValue == "0")
                            {
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Product Model');window.close();", true);
                                return;
                            }
                            rmilProdMaster.PrModelId = Convert.ToInt32(ddlProductModel.SelectedValue);

                            rmilProdMaster.Qty = Convert.ToInt32(txtQty.Text);
                            rmilProdMaster.PDate = dtpProduction.SelectedDate;
                            rmilProdMaster.IsPrint = false;
                            rmilProdMaster.MBarcode = ranUniqueNo;
                            rmilProdMaster.EntryBy = currentUser.UserId;
                            rmilProdMaster.EntryDate = DateTime.Now;
                            rmilProdMaster.EntryPC = WebUtility.GetIpAddress();
                            //db.SaveChanges();
                            new RmilProdMasterDa(false).Insert(rmilProdMaster);

                            // Insert into RmilProdDetails
                            List<RmilProdDetails> prodDetailsList = new List<RmilProdDetails>();


                            var modelInfo = new ProductModelDa(false).GetProductModelById(Convert.ToInt32(ddlProductModel.SelectedValue));

                            int qty = Convert.ToInt32(txtQty.Text);

                            Random dataGenerator = new Random();
                            List<int> randomList = new List<int>();
                            int MyNumber = 0;
                            for (int i = 1; i <= qty; i++)
                            {
                                MyNumber = dataGenerator.Next(100000, 999999);
                                if (!randomList.Contains(MyNumber))
                                    randomList.Add(MyNumber);
                            }
                            int br = 0;

                            //Insert into RmilProdDetails Table
                            for (int i = 0; i < qty; i++)
                            {
                                RmilProdDetails d = new RmilProdDetails();
                                d.RmPdId = 0;
                                d.RmPmId = new RmilProdMasterDa(true).GetLastRecord().RmPmId;
                                d.PrModelId = Convert.ToInt32(ddlProductModel.SelectedValue);
                                d.Wpfs = modelInfo.WarrantyPeriodFromSales;
                                if (rmilProdMaster.PDate != null)
                                {
                                    DateTime dt = (DateTime)rmilProdMaster.PDate;
                                    //d.DBarcode = txtSuplier.Text + "" + dt.Year.ToString().Substring(2) + "" +
                                    //             dt.Month.ToString().PadLeft(2, '0') + "" +
                                    //             dt.Day.ToString().PadLeft(2, '0') + "" +
                                    //             randomList[br];

                                    d.DBarcode = dt.Year.ToString().Substring(2) + "" + dt.Month.ToString().PadLeft(2, '0') + "" +
                                                dt.Day.ToString().PadLeft(2, '0') + "" + txtSuplier.Text + "" + ddlincharge.Text + "" + randomList[br];
                                    d.MfDate = rmilProdMaster.PDate;

                                    d.MfWarDate = dt.AddMonths(Convert.ToInt32(modelInfo.WarrantyPeriodFromMf));
                                    d.ServiceWarDate = dt.AddMonths(Convert.ToInt32(modelInfo.WarrantyPeriodFromSales));
                                }
                                d.InByForm = false;
                                d.EntryBy = currentUser.UserId;
                                d.EntryDate = DateTime.Now;
                                d.EntryPC = WebUtility.GetIpAddress();
                                prodDetailsList.Add(d);
                                br++;
                            }
                            foreach (var i in prodDetailsList)
                            {
                                db.RmilProdDetails.Add(i);
                            }
                            db.SaveChanges();

                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", string.Format("alert('Successfully {0} records inserted!');", prodDetailsList.Count), true);
                            ClearAll();
                            txtLastTrId.Text = new RmilProdMasterDa(true).LastTransactionId().ToString();
                            scope.Complete();
                        }
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

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            var mTrId = txtLastTrId.Text;
            Response.Redirect("~/Production/RptRmilProduction.aspx?pmId=" + mTrId);
        }
        protected void btnBarcode_Click(object sender, EventArgs e)
        {
            var mTrId = txtLastTrId.Text;
            Response.Redirect("~/Reports/RptBarcode.aspx?pmId=" + mTrId);
        }
        #endregion
    }
}