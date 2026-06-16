using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RMIL.Prod.DAL;
using RMIL.Prod.EntityFramework;
using RMIL.Prod.Utility;
using System.Web.Services;
using RMIL.Prod.Dashboard.Model;

namespace RMIL.Prod.Dashboard
{
    public partial class IndexWeightScale : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                if (!IsPostBack)
                {
                    //LoadCurrentDate();
                    //LoadRmDashboardData();
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
        protected void btnShow_Click(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                UserInfo currentUser = (UserInfo)Session[WebUtility.SessionCurrentUserObj];

                try
                {
                    //LoadRmDashboardData();
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
        [WebMethod]
        public static DayShiftViewModel GetALLDayShiftData()
        {
            RMILDashboardDa DayshiftData = new RMILDashboardDa();
            var tableDetails = DayshiftData.GetDayShiftData();

            return new DayShiftViewModel
            {
                ShiftData = tableDetails,
                TotalProduction = tableDetails.Sum(x => x.TotalProduction)
            };
        }
        [WebMethod]
        public static NightShiftViewModel GetALLNightShiftData()
        {
            RMILDashboardDa DayshiftData = new RMILDashboardDa();
            var tableDetails = DayshiftData.GetNightShiftData();

            return new NightShiftViewModel
            {
                ShiftData = tableDetails,
                TotalProduction = tableDetails.Sum(x => x.TotalProduction)
            };
        }
    }
}