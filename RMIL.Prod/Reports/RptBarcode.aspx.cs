using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Web;
using RMIL.Prod.DAL;
using RMIL.Prod.EntityFramework;
using RMIL.Prod.Production;
using RMIL.Prod.Utility;
using Telerik.Web.UI;

namespace RMIL.Prod.Reports
{
    public partial class RptBarcode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetValue();
        }

        private void GetValue()
        {
            if (!string.IsNullOrWhiteSpace(Request.QueryString["pmId"]))
            {
                long tPmId = Convert.ToInt64(Request.QueryString["pmId"]);
                try
                {
                    var barcodeList = new RmilProdDetailsDa(true).GetRmilBarcodeListById(tPmId);
                    if (barcodeList.Any())
                    {
                        gvBarcode.DataSource = barcodeList;
                        gvBarcode.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
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
        protected void ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            var imageButton = sender as ImageButton;
            if (imageButton != null)
            {
                string alternateText = imageButton.AlternateText;
                gvBarcode.ExportSettings.Excel.Format = (GridExcelExportFormat)Enum.Parse(typeof(GridExcelExportFormat), alternateText);
            }
            gvBarcode.ExportSettings.ExportOnlyData = true;
            gvBarcode.ExportSettings.OpenInNewWindow = true;
            gvBarcode.MasterTableView.ExportToExcel();
        }
    }
}