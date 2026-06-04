using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RMIL.Prod.DAL;
using RMIL.Prod.EntityFramework;
using RMIL.Prod.Utility;

namespace RMIL.Prod.Dashboard
{
    public partial class TarVsPr : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                UserInfo user = (UserInfo)Session[WebUtility.SessionCurrentUserObj];
                if (!IsPostBack)
                {
                    GetValue();
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
        private void GetValue()
        {
            var lineList = new ProductionLineDa(true).GetAllProductionLine();
            var l = new ProdTargetDa(false).GetDailyProdTargetList();
            var l1Qty = l.Where(c => c.LineId == 1).Sum(c => c.TQty);
            var l2Qty = l.Where(c => c.LineId == 2).Sum(c => c.TQty);
            var l3Qty = l.Where(c => c.LineId == 3).Sum(c => c.TQty);
            var l4Qty = l.Where(c => c.LineId == 4).Sum(c => c.TQty);

            var p = new QcMasterDa(false).GetDailySuccessProdList();
            var pl1Qty = p.Where(a => Convert.ToDateTime(a.EntryDate).Date == DateTime.Today && a.LineId == 1).ToList().Count();
            var pl2Qty = p.Where(a => Convert.ToDateTime(a.EntryDate).Date == DateTime.Today && a.LineId == 2).ToList().Count();
            var pl3Qty = p.Where(a => Convert.ToDateTime(a.EntryDate).Date == DateTime.Today && a.LineId == 3).ToList().Count();
            var pl4Qty = p.Where(a => Convert.ToDateTime(a.EntryDate).Date == DateTime.Today && a.LineId == 4).ToList().Count();

            aName.InnerHtml = lineList[0].LineName;
            at.InnerHtml = l1Qty.ToString();
            ap.InnerHtml = pl1Qty.ToString();

            bName.InnerHtml = lineList[1].LineName;
            bt.InnerHtml = l2Qty.ToString();
            bp.InnerHtml = pl2Qty.ToString();

            cName.InnerHtml = lineList[2].LineName;
            ct.InnerHtml = l3Qty.ToString();
            cp.InnerHtml = pl3Qty.ToString();

            dName.InnerHtml = lineList[3].LineName;
            dt.InnerHtml = l4Qty.ToString();
            dp.InnerHtml = pl4Qty.ToString();
          
            if (l1Qty > 0)
            {
                var l1Ptg = (Convert.ToDecimal((Convert.ToDecimal(pl1Qty) / Convert.ToDecimal(l1Qty)) * 100));
                aptg.InnerHtml = Math.Round(l1Ptg, 2).ToString();
            }
            else
            {
                aptg.InnerHtml = 0.ToString();
            }
            if (l2Qty > 0)
            {
                var l2Ptg = Convert.ToDecimal(((Convert.ToDecimal(pl2Qty) / Convert.ToDecimal(l2Qty)) * 100));
                bptg.InnerHtml = Math.Round(l2Ptg, 2).ToString();
            }
            else
            {
                bptg.InnerHtml = 0.ToString();
            }
            if (l3Qty > 0)
            {
                var l3Ptg = Convert.ToDecimal(((Convert.ToDecimal(pl3Qty) / Convert.ToDecimal(l3Qty)) * 100));
                cptg.InnerHtml = Math.Round(l3Ptg, 2).ToString();
            }
            else
            {
                cptg.InnerHtml = 0.ToString();
            }
            if (l4Qty > 0)
            {
                var l4Ptg = Convert.ToDecimal(((Convert.ToDecimal(pl4Qty) / Convert.ToDecimal(l4Qty)) * 100));
                dptg.InnerHtml = Math.Round(l4Ptg, 2).ToString();
            }
            else
            {
                dptg.InnerHtml = 0.ToString();
            }
        }
    }
}