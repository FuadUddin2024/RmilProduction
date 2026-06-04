using System;
using System.IO;
using System.Web.UI;
using RMIL.Prod.DAL;
using RMIL.Prod.EntityFramework;
using RMIL.Prod.Utility;

namespace RMIL.Prod.Account
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                UserInfo user = (UserInfo)Session[WebUtility.SessionCurrentUserObj];


                //footerCpr.InnerHtml = string.Format("RRAN-RFL Group © {0}", DateTime.Now.Year.ToString());
                string filePathName = user.UserId + ".jpg";
                userRoundImage.Src = "../Images/Users/" + filePathName;
                lblUsername.Text = user.UserName;
                lblStaffId.Text = user.StaffID.ToString();
                lblAddress.Text = user.Address;
                lblContactNo.Text = user.ContactNo;
                lblEmail.Text = user.Email;
                if (user.CompanyId != null)
                {
                    var compny = new CompanyDa().GetCompanyById((int)user.CompanyId);
                    lblCompany.Text = compny.CompanyName;
                }
                if (user.DepartmentId != null)
                {
                    var dept = new DepartmentDa().GetDepartmentById((int)user.DepartmentId);
                    lblDept.Text = dept.DeptName;
                }
                if (user.DesignationId != null)
                {
                    var desg = new DesignationDa().GetDesignationtById((int)user.DesignationId);
                    lblDesg.Text = desg.DesignationName;
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
        private bool SaveImage()
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                UserInfo currentUser = (UserInfo) Session[WebUtility.SessionCurrentUserObj];

                if (!string.IsNullOrEmpty(this.fldImage.FileName))
                {
                    //read the file in
                    string filePath = Path.Combine(Request.PhysicalApplicationPath, "Images\\Users\\");

                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    string fileName = currentUser.UserId + ".jpg";
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
                return false;
            }
            return false;
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                UserInfo currentUser = (UserInfo)Session[WebUtility.SessionCurrentUserObj];

                try
                {
                    if (SaveImage())
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Image uploaded successfully..');window.close();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Image upload failed!!!');window.close();", true);  
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Image upload failed!!!');window.close();", true); 
                }
            }
            else
            {
                Response.Redirect("~/Account/Logout.aspx");
            }

        }
    }
}