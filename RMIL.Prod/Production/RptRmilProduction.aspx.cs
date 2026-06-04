using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Web;
using RMIL.Prod.EntityFramework;
using RMIL.Prod.Utility;

namespace RMIL.Prod.Production
{
    public partial class RptRmilProduction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Request.QueryString["pmId"]))
            {
                long tPmId = Convert.ToInt64(Request.QueryString["pmId"]);
                try
                {
                    using (RMILCSDbEntities db = new RMILCSDbEntities())
                    {
                        var d = (from s in db.vw_RmilProdBarCode.Where(s => s.RmPmId == tPmId)
                                 select new
                                 {
                                     s.RmPdId,
                                     s.PrModelName,
                                     s.ModelCode,
                                     s.DBarcode

                                 }).ToArray();

                        rptRmilBarcode c = new rptRmilBarcode();
                        c.SetDataSource(d);
                        CrystalReportViewer1.ReportSource = c;
                        CrystalReportViewer1.ToolPanelView = ToolPanelViewType.None;
                        CrystalReportViewer1.Zoom(100);
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
    }
}