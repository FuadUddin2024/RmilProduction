using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RMIL.Prod.DAL;
using RMIL.Prod.EntityFramework;
using RMIL.Prod.Utility;

namespace RMIL.Prod.MasterPages
{
    public partial class Prod : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                UserInfo user = (UserInfo)Session[WebUtility.SessionCurrentUserObj];
                if (!IsPostBack)
                {
                    if (user.DesignationId != null)
                    {
                        var desg = new DesignationDa().GetDesignationtById((int)user.DesignationId);
                        lblDesg.Text = desg.DesignationName;
                    }
                    //footerCpr.InnerHtml = string.Format("PRAN-RFL Group © {0}", DateTime.Now.Year.ToString());
                    string filePathName = user.UserId + ".jpg";
                    userRoundImage.Src = "../Images/Users/" + filePathName;
                    userRoundImage1.Src = "../Images/Users/" + filePathName;
                    lblUsername.Text = user.UserName;
                    lblUsername1.Text = user.UserName;

                    LoadProductionMenus();
                    LoadReportMenus();

                }
            }
            else
            {
                Response.Redirect("~/Account/Logout.aspx");
            }
        }
        private void LoadProductionMenus()
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                UserInfo currentUser = (UserInfo)Session[WebUtility.SessionCurrentUserObj];
                const int parentMenuId = 3;
                DataTable newsDataTable = new DataTable();
                // add some columns to our datatable
                newsDataTable.Columns.Add("href_li");
                newsDataTable.Columns.Add("DisplayText");
                var menuList = new MenusDa(false).GetMenuListByUser(currentUser.UserId, parentMenuId);
                foreach (var m in menuList)
                {
                    DataRow newsDataRow = newsDataTable.NewRow();
                    newsDataRow["href_li"] = m.Url;
                    newsDataRow["DisplayText"] = m.Title;
                    newsDataTable.Rows.Add(newsDataRow);
                }
                prod_menu.DataSource = newsDataTable;
                prod_menu.DataBind();
            }
            else
            {
                Response.Redirect("~/Account/Logout.aspx");
            }
        }
        private void LoadReportMenus()
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                UserInfo currentUser = (UserInfo)Session[WebUtility.SessionCurrentUserObj];
                const int parentMenuId = 5;
                DataTable newsDataTable = new DataTable();
                // add some columns to our datatable
                newsDataTable.Columns.Add("href_li");
                newsDataTable.Columns.Add("DisplayText");
                var menuList = new MenusDa(false).GetMenuListByUser(currentUser.UserId, parentMenuId);
                foreach (var m in menuList)
                {
                    DataRow newsDataRow = newsDataTable.NewRow();
                    newsDataRow["href_li"] = m.Url;
                    newsDataRow["DisplayText"] = m.Title;
                    newsDataTable.Rows.Add(newsDataRow);
                }
                report_menu.DataSource = newsDataTable;
                report_menu.DataBind();
            }
            else
            {
                Response.Redirect("~/Account/Logout.aspx");
            }
        }
    }
}