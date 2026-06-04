using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RMIL.Prod.DAL;
using RMIL.Prod.EntityFramework;
using RMIL.Prod.Utility;
using Telerik.Web.UI;

namespace RMIL.Prod.Reports
{
    public partial class RptQcHist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                if (!IsPostBack)
                {
                    LoadCurrentDate();
                }
                else
                {
                    if (Session["ListData"] != null)
                    {
                        if (dtpStart.SelectedDate != null)
                        {
                            if (dtpEnd.SelectedDate != null)
                            {
                                //if (ddlStatus.SelectedValue == "0")
                                //{
                                //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Status');window.close();", true);
                                //    return;
                                //}
                                var srList = Session["ListData"];
                                pnlReport.Visible = true;
                                gvQc.DataSource = srList;
                                gvQc.DataBind();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please select end date.');window.close();", true);
                            }

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please select start date.');window.close();", true);
                        }

                    }
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
        private void LoadCurrentDate()
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                //var after1Date = DateTime.Now.Date.AddDays(1);
                //var currentDate = DateTime.Now.Date;
                //dtpEnd.SelectedDate = after1Date;
                //dtpStart.SelectedDate = currentDate;
                var after1Date = DateTime.Now.Date.AddDays(1);
                var before30Date = DateTime.Now.Date.AddDays(-30);
                dtpEnd.SelectedDate = after1Date;
                dtpStart.SelectedDate = before30Date;

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
                if (dtpStart.SelectedDate != null)
                {
                    if (dtpEnd.SelectedDate != null)
                    {
                        //if (ddlStatus.SelectedValue == "0")
                        //{
                        //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Status');window.close();", true);
                        //    return;
                        //}
                        if (ddlStatus.SelectedValue != "0")
                        {
                            if (ddlStatus.SelectedValue == "P")
                            {
                                var qcList = new QcMasterDa(true).GetQcInfoBetweenTwoDateWithProb((DateTime)dtpStart.SelectedDate, (DateTime)dtpEnd.SelectedDate, ddlStatus.SelectedValue);
                                if (qcList.Any())
                                {
                                    pnlReport.Visible = true;
                                    gvQc.DataSource = qcList;
                                    gvQc.DataBind();
                                    Session["ListData"] = qcList;
                                }
                                else
                                {
                                    pnlReport.Visible = false;
                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('No data found.');window.close();", true);
                                    return;
                                }
                            }
                            else
                            {
                                var qcList = new QcMasterDa(true).GetQcInfoBetweenTwoDate((DateTime)dtpStart.SelectedDate, (DateTime)dtpEnd.SelectedDate, ddlStatus.SelectedValue);
                                if (qcList.Any())
                                {
                                    pnlReport.Visible = true;
                                    gvQc.DataSource = qcList;
                                    gvQc.DataBind();
                                    Session["ListData"] = qcList;
                                }
                                else
                                {
                                    pnlReport.Visible = false;
                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('No data found.');window.close();", true);
                                    return;
                                }
                            }
                        }
                        else
                        {
                            var qcList = new QcMasterDa(true).GetAllQcInfoBetweenTwoDate((DateTime)dtpStart.SelectedDate, (DateTime)dtpEnd.SelectedDate);
                            if (qcList.Any())
                            {
                                pnlReport.Visible = true;
                                gvQc.DataSource = qcList;
                                gvQc.DataBind();
                                Session["ListData"] = qcList;
                            }
                            else
                            {
                                pnlReport.Visible = false;
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('No data found.');window.close();", true);
                                return;
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please select end date.');window.close();", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please select start date.');window.close();", true);
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
                gvQc.ExportSettings.Excel.Format = (GridExcelExportFormat)Enum.Parse(typeof(GridExcelExportFormat), alternateText);
            }
            gvQc.ExportSettings.ExportOnlyData = true;
            gvQc.ExportSettings.OpenInNewWindow = true;
            gvQc.MasterTableView.ExportToExcel();
        }
    }
}