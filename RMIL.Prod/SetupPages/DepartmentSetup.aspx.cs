using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using RMIL.Prod.DAL;
using RMIL.Prod.EntityFramework;
using RMIL.Prod.Utility;

namespace RMIL.Prod.SetupPages
{
    public partial class DepartmentSetup : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                if (!IsPostBack)
                {
                    LoadDepartmenttGridView();
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
        private void LoadDepartmenttGridView()
        {
            var departmentlist = new DepartmentDa(true).GetAllDepartment();
            gvDepartment.DataSource = departmentlist;
            gvDepartment.DataBind();
        }
        protected void gvDepartment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDepartment.PageIndex = e.NewPageIndex;
            this.LoadDepartmenttGridView();
        }
        private void ClearAll()
        {
            txtDepartmentName.Text = "";
        }
        #endregion
        #region Event Generated Method
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                UserInfo currentUser = (UserInfo)Session[WebUtility.SessionCurrentUserObj];

                try
                {
                    Department department = new Department();
                    department.DeptName = txtDepartmentName.Text;

                    department.EntryBy = currentUser.UserId;
                    department.EntryDate = DateTime.Now;
                    department.EntryPC = WebUtility.GetIpAddress();

                    new DepartmentDa(false).Insert(department);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Inserted.');window.close();", true);
                    LoadDepartmenttGridView();
                    ClearAll();
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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                UserInfo currentUser = (UserInfo)Session[WebUtility.SessionCurrentUserObj];

                try
                {
                    if (Session["deptId"] != null)
                    {
                        int deptId = Convert.ToInt32(Session["deptId"]);
                        var selecteddata = new DepartmentDa(false).GetDepartmentById(deptId);
                        selecteddata.DeptName = txtDepartmentName.Text;

                        selecteddata.ModifiedBy = currentUser.UserId;
                        selecteddata.ModifiedDate = DateTime.Now;
                        selecteddata.ModifiedPC = WebUtility.GetIpAddress();

                        new DepartmentDa(false).Update(selecteddata);
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Updated.');window.close();", true);
                        LoadDepartmenttGridView();
                        ClearAll();
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

        protected void btnEditDepartment_Click(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            btnUpdate.Visible = true;

            try
            {
                int deptId = Convert.ToInt32(((ImageButton)sender).CommandArgument);
                Session["deptId"] = deptId;
                var selecteddata = new DepartmentDa(false).GetDepartmentById(deptId);
                txtDepartmentName.Text = selecteddata.DeptName;

            }
            catch (Exception ex)
            {

                Response.Write(ex.Message);
            }
        }
        protected void btnDeleteDepartment_Click(object sender, EventArgs e)
        {
            try
            {
                int deptId = Convert.ToInt32(((ImageButton)sender).CommandArgument);
                var selectedData = new DepartmentDa(false).GetDepartmentById(deptId);
                new DepartmentDa(false).Delete(selectedData);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Deleted.');window.close();", true);
                LoadDepartmenttGridView();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }

        }
        #endregion
    }
}