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
using System.Data;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;



namespace RMIL.Prod.Production
{
    public partial class RmProductionExcel : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                if (!IsPostBack)
                {
                    LoadLastTrans();
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
        protected void LoadLastTrans()
        {
            var productModelList = new RMILProduction(true).GetAllUnPrintExcelBarcode();
            ddllasttrans.DataSource = productModelList.OrderBy(c => c.CombinedCode);
            this.ddllasttrans.DataTextField = "CombinedCode";
            this.ddllasttrans.DataValueField = "RmPmId";
            ddllasttrans.DataBind();
            ddllasttrans.DataSource = productModelList.OrderBy(c => c.CombinedCode);
            ddllasttrans.DataBind();
            ddllasttrans.Items.Insert(0, new ListItem("---Please Select---", "0"));
        }

        private void ClearAll()
        {
            ddlProduct.SelectedValue = "0";
          //  ddlProductModel.SelectedValue = "0";
        //    txtQty.Text = "";
         //   txtSuplier.Text = "";
            dtpProduction.Clear();

        }

        public int GetWeekNumber()
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return weekNum;
        }
        #endregion
        #region Event Generated Method
        protected void btnUploadFile_Click(object sender, EventArgs e)
        {
            try
            {
                gvDetails.DataSource = null;
                gvDetails.DataBind();
                lblError.Text = "";

                if (!exlUpload.HasFile)
                {
                    lblError.Text = "Please select an Excel file first";
                    return;
                }

                DataTable dt = new DataTable();

                using (Stream stream = exlUpload.PostedFile.InputStream)
                {
                    NPOI.SS.UserModel.IWorkbook workbook;

                    if (exlUpload.FileName.EndsWith(".xls"))
                    {
                        workbook = new NPOI.HSSF.UserModel.HSSFWorkbook(stream); // Excel 97-2003
                    }
                    else if (exlUpload.FileName.EndsWith(".xlsx"))
                    {
                        workbook = new NPOI.XSSF.UserModel.XSSFWorkbook(stream); // Excel 2007+
                    }
                    else
                    {
                        lblError.Text = "Invalid file type. Please upload .xls or .xlsx";
                        return;
                    }

                    var sheet = workbook.GetSheetAt(0); // first sheet

                    // Read header row
                    IRow headerRow = sheet.GetRow(0);
                    for (int i = 0; i < headerRow.LastCellNum; i++)
                    {
                        dt.Columns.Add(headerRow.GetCell(i)?.ToString() ?? "Column" + i);
                    }

                    // Read data rows
                    for (int i = 1; i <= sheet.LastRowNum; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue;

                        DataRow dr = dt.NewRow();
                        for (int j = 0; j < row.LastCellNum; j++)
                        {
                            dr[j] = row.GetCell(j)?.ToString() ?? "";
                        }
                        dt.Rows.Add(dr);
                    }
                }

                if (dt.Rows.Count > 50)
                {
                    lblError.Text = dt.Rows.Count + " rows total; Unable to upload more than 2000 rows";
                    return;
                }

                gvDetails.DataSource = dt;
                gvDetails.DataBind();

                lblError.Text = dt.Rows.Count + " rows uploaded successfully";
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
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
                            // Performance boost for EF5
                            db.Configuration.AutoDetectChangesEnabled = false;

                            List<RmilProdDetails> allProdDetails = new List<RmilProdDetails>();

                            foreach (GridViewRow dr in gvDetails.Rows)
                            {
                                // Taking Values From Excel File
                                int ModelCode = Int32.Parse(dr.Cells[0].Text);
                                int QTY = Int32.Parse(dr.Cells[1].Text);
                                string SupplierCode = dr.Cells[2].Text;

                                // Fetch Product Info
                                var productModelList = new ProductModelDa(true)
                                    .GetAllProductModel()
                                    .FirstOrDefault(x => x.ModelCode == ModelCode);
                                //if()
                                //{

                                //}
                                var modelInfo = new ProductModelDa(false)
                                    .GetProductModelById(productModelList.PrModelId);

                                // Insert Master Table
                                Random numberGenerator = new Random();
                                int ranNumber = numberGenerator.Next(1000000000, 2147483647);
                                var ranUniqueNo = DateTime.Now.Year.ToString().Substring(2) +
                                                   DateTime.Now.Month.ToString("00") +
                                                   DateTime.Now.Day.ToString("00") +
                                                   ranNumber;

                                RmilProdMaster rmilProdMaster = new RmilProdMaster();
                                rmilProdMaster.PrModelId = productModelList.PrModelId;
                                rmilProdMaster.Qty = QTY;
                                rmilProdMaster.PDate = dtpProduction.SelectedDate;
                                rmilProdMaster.IsPrint = false;
                                rmilProdMaster.MBarcode = ranUniqueNo;
                                rmilProdMaster.EntryBy = currentUser.UserId;
                                rmilProdMaster.EntryDate = DateTime.Now;
                                rmilProdMaster.EntryPC = WebUtility.GetIpAddress();

                                new RmilProdMasterDa(false).Insert(rmilProdMaster);
                                int masterId = Convert.ToInt32(rmilProdMaster.RmPmId);

                                // Generate unique barcodes for this model
                                Random dataGenerator = new Random();
                                HashSet<int> randomSet = new HashSet<int>();
                                while (randomSet.Count < QTY)
                                {
                                    randomSet.Add(dataGenerator.Next(10000000, 99999999));
                                }

                                int br = 0;
                                foreach (int rand in randomSet)
                                {
                                    RmilProdDetails d = new RmilProdDetails();
                                    d.RmPmId = masterId;
                                    d.PrModelId = productModelList.PrModelId;
                                    d.Wpfs = modelInfo.WarrantyPeriodFromSales;
                                    d.InByForm = false;
                                    d.EntryBy = currentUser.UserId;
                                    d.EntryDate = DateTime.Now;
                                    d.EntryPC = WebUtility.GetIpAddress();

                                    if (rmilProdMaster.PDate != null)
                                    {
                                        DateTime dt = (DateTime)rmilProdMaster.PDate;
                                        d.DBarcode = SupplierCode +
                                                     dt.Year.ToString().Substring(2) +
                                                     dt.Month.ToString("00") +
                                                     dt.Day.ToString("00") +
                                                     rand;
                                        d.MfDate = dt;
                                        d.MfWarDate = dt.AddMonths(Convert.ToInt32(modelInfo.WarrantyPeriodFromMf));
                                        d.ServiceWarDate = dt.AddMonths(Convert.ToInt32(modelInfo.WarrantyPeriodFromSales));
                                    }

                                    allProdDetails.Add(d);
                                    br++;
                                }
                            }

                            // ===========================
                            // SAVE ALL MODELS AT ONCE
                            // ===========================
                            foreach (var d in allProdDetails)
                            {
                                db.RmilProdDetails.Add(d);
                            }
                            db.SaveChanges(); // ONE single SaveChanges for all models

                            db.Configuration.AutoDetectChangesEnabled = true;

                            gvDetails.DataSource = null;
                            gvDetails.DataBind();
                            lblError.Text = "";
                            ScriptManager.RegisterStartupScript(
                                Page,
                                typeof(Page),
                                "ClientScript",
                                $"alert('Successfully {allProdDetails.Count} records inserted!');",
                                true
                            );

                            ClearAll();
                            LoadLastTrans();
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
            var mTrId =Convert.ToInt32(ddllasttrans.SelectedValue);
            Response.Redirect("~/Production/RptRmilProduction.aspx?pmId=" + mTrId);
        }
        protected void btnBarcode_Click(object sender, EventArgs e)
        {
            var mTrId = Convert.ToInt32(ddllasttrans.SelectedValue);
            var productModelList = new RMILProduction(true).BarcodePrintUpdate(mTrId);
            LoadLastTrans();
            Response.Redirect("~/Reports/RptBarcode.aspx?pmId=" + mTrId);
        }
        #endregion
    }
}