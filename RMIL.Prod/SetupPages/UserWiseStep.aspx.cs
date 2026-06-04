using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RMIL.Prod.DAL;
using RMIL.Prod.EntityFramework;
using RMIL.Prod.Utility;

namespace RMIL.Prod.SetupPages
{
    public partial class UserWiseStep : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                if (!IsPostBack)
                {
                    LoadUser();
                    LoadStepsCheckBoxList();
                    chkActive.Checked = true;
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
        protected void LoadUser()
        {
            var userList = new UserInfoDa(true).GetAllUserInfo();
            if (userList.Any())
            {
                var q = userList.Select(p => new { p.UserId, p.UserName, DisplayText = p.UserId + " (" + p.UserName + ")" });
                ddlUserId.DataSource = q.OrderBy(c => c.UserName);
                this.ddlUserId.DataTextField = "DisplayText";
                this.ddlUserId.DataValueField = "UserId";
                ddlUserId.DataBind();
                ddlUserId.DataSource = q.OrderBy(c => c.UserName);
                ddlUserId.DataBind();
                ddlUserId.Items.Insert(0, new ListItem("---Please Select---", "0"));
            }
        }
        protected void LoadStepsCheckBoxList()
        {
            var stpList = new QcStepDa(true).GetAllQcStep();

            chkBoxSteps.DataSource = stpList.OrderBy(c => c.StepsName);
            this.chkBoxSteps.DataTextField = "StepsName";
            this.chkBoxSteps.DataValueField = "StpId";
            chkBoxSteps.DataBind();

        }
        private void ClearAll()
        {
            //chkActive.Checked = false;
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
                    if (ddlUserId.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select User ID');window.close();", true);
                        return;
                    }
                    if (chkActive.Checked == false)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Check Active');window.close();", true);
                        return;
                    }
                    List<QcUserWiseStep> stepsPerList = new List<QcUserWiseStep>();
                    for (int i = 0; i < chkBoxSteps.Items.Count; i++)
                    {
                        if (chkBoxSteps.Items[i].Selected)
                        {
                            var b = new QcUserWiseStepDa(false).GetUserWiseStepPerInfoById(ddlUserId.SelectedValue, Convert.ToInt32(chkBoxSteps.Items[i].Value));
                            if (b == null)
                            {
                                stepsPerList.Add(new QcUserWiseStep
                                {
                                    UserId = ddlUserId.SelectedValue,
                                    StpId = Convert.ToInt32(chkBoxSteps.Items[i].Value),
                                    Active = chkActive.Checked,
                                    EntryBy = currentUser.UserId,
                                    EntryDate = DateTime.Now,
                                    EntryPC = WebUtility.GetIpAddress()

                                });
                            }
                        }
                    }
                    if (stepsPerList.Any())
                    {
                        using (RMILCSDbEntities db = new RMILCSDbEntities())
                        {
                            foreach (var i in stepsPerList)
                            {
                                db.QcUserWiseStep.Add(i);
                            }
                            db.SaveChanges();

                        }
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Assigned.');window.close();", true);
                        LoadUser();
                        LoadStepsCheckBoxList();
                        ClearAll();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please select approver item (class).');window.close();", true);
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
                    if (ddlUserId.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select User ID');window.close();", true);
                        return;
                    }

                    for (int i = 0; i < chkBoxSteps.Items.Count; i++)
                    {
                        if (chkBoxSteps.Items[i].Selected)
                        {
                            var b = new QcUserWiseStepDa(false).GetUserWiseStepPerInfoById(ddlUserId.SelectedValue, Convert.ToInt32(chkBoxSteps.Items[i].Value));
                            if (b != null)
                            {
                                b.Active = chkActive.Checked;

                                b.ModifiedBy = currentUser.UserId;
                                b.ModifiedDate = DateTime.Now;
                                b.ModifiedPC = WebUtility.GetIpAddress();
                                new QcUserWiseStepDa().Update(b);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('No data found for update.');window.close();", true);
                                return;
                            }
                        }
                    }

                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully updated.');window.close();", true);
                    LoadUser();
                    LoadStepsCheckBoxList();
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
        protected void ddlUserId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadStepsCheckBoxList();

                var userInfoList = new QcUserWiseStepDa(false).GetUserWiseStepPerListByUserId(ddlUserId.SelectedValue);
                if (userInfoList.Any())
                {
                    foreach (var b in userInfoList)
                    {
                        for (int i = 0; i < chkBoxSteps.Items.Count; i++)
                        {
                            if (Convert.ToInt32(chkBoxSteps.Items[i].Value) == b.StpId)
                            {
                                chkBoxSteps.Items[i].Selected = true;
                            }
                        }
                        chkActive.Checked = b.Active == true;
                    }
                }
                else
                {
                    chkActive.Checked = false;
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('This user has no step permission yet!');window.close();", true);
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        #endregion
    }
}