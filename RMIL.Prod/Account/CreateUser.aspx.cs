using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using RMIL.Prod.DAL;
using RMIL.Prod.EntityFramework;
using RMIL.Prod.Utility;

namespace RMIL.Prod.Account
{
    public partial class CreateUser : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                UserInfo user = (UserInfo)Session[WebUtility.SessionCurrentUserObj];
                if (user.UserTypeId == ((int)Enums.Enums.EnumUserType.SuperAdmin) ||
                    user.UserTypeId == ((int)Enums.Enums.EnumUserType.Admin))
                {
                    if (!IsPostBack)
                    {
                        LoadDepartment();
                        LoadDesignation();
                        LoadCompany();
                        LoadUserType();
                        LoadUserInfoGridView();
                        FillPage();
                    }
                }
                else
                {
                    Response.Redirect("~/Account/Logout.aspx");
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
        private void LoadUserInfoGridView()
        {
            var userInfiList = new UserInfoDa(true).GetAllUserInfo();
            gvUserInfo.DataSource = userInfiList;
            gvUserInfo.DataBind();
        }
        protected void gvUserInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUserInfo.PageIndex = e.NewPageIndex;
            this.LoadUserInfoGridView();
        }

        protected void LoadCompany()
        {
            var companyList = new CompanyDa(true).GetAllCompany();
            ddlCompany.DataSource = companyList;
            this.ddlCompany.DataTextField = "CompanyName";
            this.ddlCompany.DataValueField = "CompanyId";
            ddlCompany.DataBind();
            ddlCompany.DataSource = companyList;
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, new ListItem("---Please Select---", "0"));
        }
        protected void LoadDepartment()
        {
            var departmentList = new DepartmentDa(true).GetAllDepartment();
            ddlDepartment.DataSource = departmentList;
            this.ddlDepartment.DataTextField = "DeptName";
            this.ddlDepartment.DataValueField = "DeptId";
            ddlDepartment.DataBind();
            ddlDepartment.DataSource = departmentList;
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("---Please Select---", "0"));
        }
        protected void LoadDesignation()
        {
            var designationList = new DesignationDa(true).GetAllDesignation();
            ddlDesignation.DataSource = designationList;
            this.ddlDesignation.DataTextField = "DesignationName";
            this.ddlDesignation.DataValueField = "DesignationId";
            ddlDesignation.DataBind();
            ddlDesignation.DataSource = designationList;
            ddlDesignation.DataBind();
            ddlDesignation.Items.Insert(0, new ListItem("---Please Select---", "0"));
        }
        protected void LoadUserType()
        {
            var userTypeList = new UserTypeDa(true).GetAllUserType();
            ddlUserType.DataSource = userTypeList;
            this.ddlUserType.DataTextField = "TypeName";
            this.ddlUserType.DataValueField = "TypeId";
            ddlUserType.DataBind();
            ddlUserType.DataSource = userTypeList;
            ddlUserType.DataBind();
            ddlUserType.Items.Insert(0, new ListItem("---Please Select---", "0"));
        }
        private void ClearAll()
        {
            txtUserId.Text = "";
            txtUserName.Text = "";
            txtStaffId.Text = "";
            TextEmail.Text = "";
            txtContactNo.Text = "";
            txtAddress.Text = "";
            txtConfirmPass.Text = "";
            ddlCompany.SelectedValue = "0";
            ddlUserType.SelectedValue = "0";
            ddlDepartment.SelectedValue = "0";
            ddlDesignation.SelectedValue = "0";
        }
        private void ValidationMessage()
        {
            if (txtUserId.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please input user id');window.close();", true);
                return;
            }
            if (txtUserName.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please input user name');window.close();", true);
                return;
            }
            if (txtStaffId.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please input staff id');window.close();", true);
                return;
            }
            if (txtContactNo.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please input contact no');window.close();", true);
                return;
            }
            if (TextEmail.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please input email');window.close();", true);
                return;
            }
            if (ddlUserType.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select User Type');window.close();", true);
                return;
            }
            if (ddlCompany.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Company');window.close();", true);
                return;
            }
            if (ddlDepartment.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Department Name');window.close();", true);
                return;
            }
            if (ddlDesignation.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Designation Name');window.close();", true);
                return;
            }
            if (txtPassword.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please input password');window.close();", true);
                return;
            }
            if (txtConfirmPass.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please input confirm password');window.close();", true);
                return;
            }
        }
        private void FillPage()
        {
            if (Request.QueryString["ViewProfile"] != null)
            {
                fldImage.Visible = false;

                UserInfo userInfo = (UserInfo)Session[WebUtility.SessionCurrentUserObj];
                var selectedUser = userInfo;

                string filePathName = selectedUser.UserId + ".jpg";
                img.Src = "../Images/Users/" + filePathName;
                txtUserId.Text = selectedUser.UserId;
                txtUserName.Text = selectedUser.UserName;
                txtStaffId.Text = selectedUser.StaffID.ToString();
                TextEmail.Text = selectedUser.Email;
                txtContactNo.Text = selectedUser.ContactNo;
                txtAddress.Text = selectedUser.Address;

                var companyId = selectedUser.CompanyId;
                if (companyId != null)
                {
                    var cq = new CompanyDa(true).GetCompanyById((int)companyId);
                    var compName = cq.CompanyName;
                    LoadCompany();
                    if (ddlCompany.Items.Contains(ddlCompany.Items.FindByText(compName)))
                        ddlCompany.SelectedIndex = ddlCompany.Items.IndexOf(ddlCompany.Items.FindByText(compName));
                }
                var typeid = selectedUser.UserTypeId;
                if (typeid != null)
                {
                    var tq = new UserTypeDa(true).GetUserTypeById((int)typeid);
                    var grpName = tq.TypeName;
                    LoadUserType();
                    if (ddlUserType.Items.Contains(ddlUserType.Items.FindByText(grpName)))
                        ddlUserType.SelectedIndex = ddlUserType.Items.IndexOf(ddlUserType.Items.FindByText(grpName));
                }
                var deptid = selectedUser.DepartmentId;
                if (deptid != null)
                {
                    var dq = new DepartmentDa(true).GetDepartmentById((int)deptid);
                    var deptName = dq.DeptName;
                    LoadDepartment();
                    if (ddlDepartment.Items.Contains(ddlDepartment.Items.FindByText(deptName)))
                        ddlDepartment.SelectedIndex = ddlDepartment.Items.IndexOf(ddlDepartment.Items.FindByText(deptName));
                }
                var desgid = selectedUser.DesignationId;
                if (desgid != null)
                {
                    var desq = new DesignationDa(true).GetDesignationtById((int)desgid);
                    var desgName = desq.DesignationName;
                    LoadDesignation();
                    if (ddlDesignation.Items.Contains(ddlDesignation.Items.FindByText(desgName)))
                        ddlDesignation.SelectedIndex = ddlDesignation.Items.IndexOf(ddlDesignation.Items.FindByText(desgName));
                }

                txtUserId.ReadOnly = true;
                txtStaffId.ReadOnly = true;
                txtUserName.ReadOnly = true;
                txtContactNo.ReadOnly = true;
                txtAddress.ReadOnly = true;
                TextEmail.ReadOnly = true;
                ddlDepartment.Enabled = false;
                ddlDesignation.Enabled = false;

                ddlUserType.Enabled = false;
                ddlCompany.Enabled = false;

                //pnlGrid.Visible = false;


            }
        }

        private bool SaveImage()
        {
            if (!string.IsNullOrEmpty(this.fldImage.FileName))
            {
                //read the file in
                string filePath = Path.Combine(Request.PhysicalApplicationPath, "Images\\Users\\");

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                string fileName = txtUserId.Text + ".jpg";
                string nFile = Path.Combine(filePath, fileName);

                try
                {
                    if (System.IO.File.Exists(nFile))
                    {
                        System.IO.File.Delete(nFile);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                if (fldImage.PostedFile != null && fldImage.PostedFile.FileName != "")
                {

                    // 61440 bytes means 60 kb, You can change the value based on your requirement

                    if (fldImage.PostedFile.ContentLength > 102400)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Image size must be less than 100 KB..');window.close();", true);
                        return false;
                        //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('Image size must be less than 100 KB.')", true);

                    }

                    else
                    {
                        fldImage.SaveAs(nFile);
                        return true;
                    }
                }
            }
            return true;
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
                    ValidationMessage();
                    if (SaveImage())
                    {
                        UserInfo userInfo = new UserInfo();

                        var userId = txtUserId.Text;
                        var user = new UserInfoDa(true).GetUserByUserId(userId);
                        if (user != null)
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('This user id is already exists.');window.close();", true);
                            return;

                        }
                        userInfo.UserId = txtUserId.Text;
                        userInfo.UserName = txtUserName.Text;
                        userInfo.StaffID = Convert.ToInt32(txtStaffId.Text);
                        userInfo.Email = TextEmail.Text;
                        userInfo.ContactNo = txtContactNo.Text;
                        userInfo.Address = txtAddress.Text;
                        userInfo.Password = txtPassword.Text;
                        userInfo.IsActive = true;
                        userInfo.ServiceUser = "P";
                        userInfo.EntryDate = DateTime.Now;



                        if (ddlUserType.SelectedValue == "0")
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select User Type');window.close();", true);
                            return;
                        }
                        userInfo.UserTypeId = Convert.ToInt32(ddlUserType.SelectedValue);

                        if (ddlCompany.SelectedValue == "0")
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Company');window.close();", true);
                            return;
                        }
                        userInfo.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);

                        if (ddlDepartment.SelectedValue == "0")
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Department Name');window.close();", true);
                            return;
                        }
                        userInfo.DepartmentId = Convert.ToInt32(ddlDepartment.SelectedValue);

                        if (ddlDesignation.SelectedValue == "0")
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Designation Name');window.close();", true);
                            return;
                        }
                        userInfo.DesignationId = Convert.ToInt32(ddlDesignation.SelectedValue);

                        userInfo.EntryBy = currentUser.UserId;
                        userInfo.EntryDate = DateTime.Now;
                        userInfo.EntryPC = WebUtility.GetIpAddress();

                        new UserInfoDa(false).Insert(userInfo);
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Inserted.');window.close();", true);
                        LoadUserInfoGridView();
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
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                UserInfo currentUser = (UserInfo)Session[WebUtility.SessionCurrentUserObj];

                try
                {
                    if (Session["userCode"] != null)
                    {
                        if (SaveImage())
                        {
                            int userCode = Convert.ToInt32(Session["userCode"]);
                            var selecteddata = new UserInfoDa(false).GetUserInfoByCode(userCode);
                            //selecteddata.UserId = txtUserId.Text;
                            selecteddata.UserName = txtUserName.Text;
                            selecteddata.StaffID = Convert.ToInt32(txtStaffId.Text);
                            selecteddata.Email = TextEmail.Text;
                            selecteddata.ContactNo = txtContactNo.Text;
                            selecteddata.Address = txtAddress.Text;
                           
                            selecteddata.ModifiedDate = DateTime.Now;

                            if (ddlUserType.SelectedValue == "0")
                            {
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select User Type');window.close();", true);
                                return;
                            }
                            selecteddata.UserTypeId = Convert.ToInt32(ddlUserType.SelectedValue);

                            if (ddlCompany.SelectedValue == "0")
                            {
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Company');window.close();", true);
                                return;
                            }
                            selecteddata.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);

                            if (ddlDepartment.SelectedValue == "0")
                            {
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Department Name');window.close();", true);
                                return;
                            }
                            selecteddata.DepartmentId = Convert.ToInt32(ddlDepartment.SelectedValue);

                            if (ddlDesignation.SelectedValue == "0")
                            {
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Designation Name');window.close();", true);
                                return;
                            }
                            selecteddata.DesignationId = Convert.ToInt32(ddlDesignation.SelectedValue);

                            selecteddata.ModifiedBy = currentUser.UserId;
                            selecteddata.ModifiedDate = DateTime.Now;
                            selecteddata.ModifiedPC = WebUtility.GetIpAddress();

                            new UserInfoDa(false).Update(selecteddata);
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Updated.');window.close();", true);
                            LoadUserInfoGridView();
                            ClearAll();
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
        protected void btnEditUserInfo_Click(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            btnUpdate.Visible = true;
            pnlPassword.Visible = false;
            txtUserId.ReadOnly = true;

            try
            {
                int userCode = Convert.ToInt32(((ImageButton)sender).CommandArgument);
                Session["userCode"] = userCode;
                var selecteddata = new UserInfoDa(false).GetUserInfoByCode(userCode);
                txtUserId.Text = selecteddata.UserId;
                txtUserName.Text = selecteddata.UserName;
                txtStaffId.Text = selecteddata.StaffID.ToString();
                TextEmail.Text = selecteddata.Email;
                txtContactNo.Text = selecteddata.ContactNo;
                txtAddress.Text = selecteddata.Address;


                var compid = selecteddata.CompanyId;
                if (compid != null)
                {
                    var cq = new CompanyDa(true).GetCompanyById((int)compid);
                    var compName = cq.CompanyName;
                    LoadCompany();
                    if (ddlCompany.Items.Contains(ddlCompany.Items.FindByText(compName)))
                        ddlCompany.SelectedIndex = ddlCompany.Items.IndexOf(ddlCompany.Items.FindByText(compName));
                }

                var typeid = selecteddata.UserTypeId;
                if (typeid != null)
                {
                    var tq = new UserTypeDa(true).GetUserTypeById((int)typeid);
                    var grpName = tq.TypeName;
                    LoadUserType();
                    if (ddlUserType.Items.Contains(ddlUserType.Items.FindByText(grpName)))
                        ddlUserType.SelectedIndex = ddlUserType.Items.IndexOf(ddlUserType.Items.FindByText(grpName));
                }
                var deptid = selecteddata.DepartmentId;
                if (deptid != null)
                {
                    var dq = new DepartmentDa(true).GetDepartmentById((int)deptid);
                    var deptName = dq.DeptName;
                    LoadDepartment();
                    if (ddlDepartment.Items.Contains(ddlDepartment.Items.FindByText(deptName)))
                        ddlDepartment.SelectedIndex = ddlDepartment.Items.IndexOf(ddlDepartment.Items.FindByText(deptName));
                }
                var desgid = selecteddata.DesignationId;
                if (desgid != null)
                {
                    var desq = new DesignationDa(true).GetDesignationtById((int)desgid);
                    var desgName = desq.DesignationName;
                    LoadDesignation();
                    if (ddlDesignation.Items.Contains(ddlDesignation.Items.FindByText(desgName)))
                        ddlDesignation.SelectedIndex = ddlDesignation.Items.IndexOf(ddlDesignation.Items.FindByText(desgName));
                }
            }
            catch (Exception ex)
            {

                Response.Write(ex.Message);
            }
        }
        protected void btnDeleteUserInfo_Click(object sender, EventArgs e)
        {
            try
            {
                int userCode = Convert.ToInt32(((ImageButton)sender).CommandArgument);
                var selectedData = new UserInfoDa(false).GetUserInfoByCode(userCode);
                new UserInfoDa(false).Delete(selectedData);

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Deleted.');window.close();", true);
                LoadUserInfoGridView();

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        protected void txtBarCode_TextChanged(object sender, EventArgs e)
        {
            var UserName = txtUserId.Text;
            var GetSingleUserData = new UserInfoDa(false).GetUserByUserId(UserName);
            if(GetSingleUserData != null)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('User is already exist.');window.close();", true);
            }
        }
        #endregion
    }
}