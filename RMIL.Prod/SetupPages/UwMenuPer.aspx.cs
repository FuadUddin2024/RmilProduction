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
    public partial class UwMenuPer : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                if (!IsPostBack)
                {
                    LoadUser();
                    LoadParentMenuList();
                    LoadMenuCheckBoxList();
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
        protected void LoadMenuCheckBoxList()
        {
            int parentMenuId = Convert.ToInt32(ddlParentMenu.SelectedValue);
            if (parentMenuId > 0)
            {
                var menuList = new MenusDa(true).GetMenuListByParentMenuId(parentMenuId);
                if (menuList.Any())
                {
                    chkBoxMenu.DataSource = menuList;
                    this.chkBoxMenu.DataTextField = "Title";
                    this.chkBoxMenu.DataValueField = "MenuId";
                    chkBoxMenu.DataBind();
                }
                else
                {
                    chkBoxMenu.DataSource = null;
                    this.chkBoxMenu.DataTextField = "Title";
                    this.chkBoxMenu.DataValueField = "MenuId";
                    chkBoxMenu.DataBind();
                }
            }
            else
            {
                var menuList = new MenusDa(true).GetAllMenus();
                if (menuList.Any())
                {
                    chkBoxMenu.DataSource = menuList;
                    this.chkBoxMenu.DataTextField = "Title";
                    this.chkBoxMenu.DataValueField = "MenuId";
                    chkBoxMenu.DataBind();
                }
                else
                {
                    chkBoxMenu.DataSource = null;
                    this.chkBoxMenu.DataTextField = "Title";
                    this.chkBoxMenu.DataValueField = "MenuId";
                    chkBoxMenu.DataBind();
                }
            }
        }
        protected void LoadParentMenuList()
        {
            var pmnList = new ParentMenusDa(true).GetAllParentMenus();
            ddlParentMenu.DataSource = pmnList;
            this.ddlParentMenu.DataTextField = "ParentMenuName";
            this.ddlParentMenu.DataValueField = "ParentMenuId";
            ddlParentMenu.DataBind();
            ddlParentMenu.DataSource = pmnList;
            ddlParentMenu.DataBind();
            ddlParentMenu.Items.Insert(0, new ListItem("---Select Parent Menu---", "0"));
        }
        protected void ddlParentMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            int parentMenuId = Convert.ToInt32(ddlParentMenu.SelectedValue);
            var menuList = new MenusDa(true).GetMenuListByParentMenuId(parentMenuId);
            if (menuList.Any())
            {
                chkBoxMenu.DataSource = menuList;
                this.chkBoxMenu.DataTextField = "Title";
                this.chkBoxMenu.DataValueField = "MenuId";
                chkBoxMenu.DataBind();
            }
            else
            {
                chkBoxMenu.DataSource = null;
                this.chkBoxMenu.DataTextField = "Title";
                this.chkBoxMenu.DataValueField = "MenuId";
                chkBoxMenu.DataBind();
            }
            UserWiseDataLoad();

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

                    List<UserWiseMenuPer> userWiseMenuList = new List<UserWiseMenuPer>();
                    for (int i = 0; i < chkBoxMenu.Items.Count; i++)
                    {
                        if (chkBoxMenu.Items[i].Selected)
                        {
                            var b = new UserWiseMenuPerDa(false).GetUserWiseMenuPerInfoById(ddlUserId.SelectedValue, Convert.ToInt32(chkBoxMenu.Items[i].Value));
                            if (b == null)
                            {
                                userWiseMenuList.Add(new UserWiseMenuPer
                                {
                                    UserId = ddlUserId.SelectedValue,
                                    MenuId = Convert.ToInt32(chkBoxMenu.Items[i].Value),
                                    Active = chkActive.Checked,
                                    EntryBy = currentUser.UserId,
                                    EntryDate = DateTime.Now,
                                    EntryPC = WebUtility.GetIpAddress()

                                });
                            }
                        }
                    }
                    if (userWiseMenuList.Any())
                    {
                        using (RMILCSDbEntities db = new RMILCSDbEntities())
                        {
                            foreach (var i in userWiseMenuList)
                            {
                                db.UserWiseMenuPer.Add(i);
                            }
                            db.SaveChanges();

                        }
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Saved.');window.close();", true);
                        LoadUser();
                        LoadMenuCheckBoxList();
                        ClearAll();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please select menu.');window.close();", true);
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
                    //if (chkActive.Checked == false)
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Check Active');window.close();", true);
                    //    return;
                    //}

                    //for (int i = 0; i < chkBoxMenu.Items.Count; i++)
                    //{
                    //    if (chkBoxMenu.Items[i].Selected)
                    //    {
                    //        var b = new UserWiseMenuPerDa(false).GetUserWiseMenuPerInfoById(ddlUserId.SelectedValue, Convert.ToInt32(chkBoxMenu.Items[i].Value));
                    //        if (b != null)
                    //        {
                    //            b.Active = chkActive.Checked;

                    //            b.ModifiedBy = currentUser.UserId;
                    //            b.ModifiedDate = DateTime.Now;
                    //            b.ModifiedPC = WebUtility.GetIpAddress();
                    //            new UserWiseMenuPerDa().Update(b);
                    //        }
                    //        else
                    //        {
                    //            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('No data found for update.');window.close();", true);
                    //            return;
                    //        }
                    //    }
                    //}
                    var DeletepreviousMenu = new UserWiseMenuPerDa(false).DeletePreviousPermission(ddlUserId.SelectedValue.ToString());
                    if (DeletepreviousMenu == true)
                    {
                        List<UserWiseMenuPer> userWiseMenuList = new List<UserWiseMenuPer>();
                        for (int i = 0; i < chkBoxMenu.Items.Count; i++)
                        {
                            if (chkBoxMenu.Items[i].Selected)
                            {
                                var b = new UserWiseMenuPerDa(false).GetUserWiseMenuPerInfoById(ddlUserId.SelectedValue, Convert.ToInt32(chkBoxMenu.Items[i].Value));
                                if (b == null)
                                {
                                    userWiseMenuList.Add(new UserWiseMenuPer
                                    {
                                        UserId = ddlUserId.SelectedValue,
                                        MenuId = Convert.ToInt32(chkBoxMenu.Items[i].Value),
                                        Active = chkActive.Checked,
                                        EntryBy = currentUser.UserId,
                                        EntryDate = DateTime.Now,
                                        EntryPC = WebUtility.GetIpAddress()

                                    });
                                }
                            }
                        }
                        if (userWiseMenuList.Any())
                        {
                            using (RMILCSDbEntities db = new RMILCSDbEntities())
                            {
                                foreach (var i in userWiseMenuList)
                                {
                                    db.UserWiseMenuPer.Add(i);
                                }
                                db.SaveChanges();

                            }
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully updated.');window.close();", true);
                            LoadUser();
                            LoadMenuCheckBoxList();
                            ClearAll();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Error.');window.close();", true);
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
        protected void ddlUserId_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserWiseDataLoad();
        }

        private void UserWiseDataLoad()
        {
            try
            {
                LoadMenuCheckBoxList();
                var nonactive = new UserWiseMenuPerDa(false).GetNonActiveUserWiseMenuPerListByUserId(ddlUserId.SelectedValue);
                var active = new UserWiseMenuPerDa(false).GetUserWiseMenuPerListByUserId(ddlUserId.SelectedValue);

                if (active.Any())
                {
                    GetActiveUserInfo();
                }
                else if (nonactive.Any())
                {
                    GetNonActiveUserInfo();
                }
                else
                {
                    GetNotExistUserInfo();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private void GetActiveUserInfo()
        {
            var userInfoList = new UserWiseMenuPerDa(false).GetUserWiseMenuPerListByUserId(ddlUserId.SelectedValue);
            if (userInfoList.Any())
            {
                foreach (var b in userInfoList)
                {
                    if (b.Active == true)
                    {
                        for (int i = 0; i < chkBoxMenu.Items.Count; i++)
                        {
                            if (Convert.ToInt32(chkBoxMenu.Items[i].Value) == b.MenuId)
                            {
                                chkBoxMenu.Items[i].Selected = true;
                            }
                        }
                    }
                    switch (b.Active)
                    {
                        case true:
                            chkActive.Checked = true;
                            break;
                        default:
                            chkActive.Checked = false;
                            break;
                    }
                }
            }
            else
            {
                chkActive.Checked = false;
            }
        }
        private void GetNonActiveUserInfo()
        {
            var userInfoList = new UserWiseMenuPerDa(false).GetNonActiveUserWiseMenuPerListByUserId(ddlUserId.SelectedValue);
            if (userInfoList.Any())
            {
                foreach (var b in userInfoList)
                {
                    if (b.Active == false)
                    {
                        for (int i = 0; i < chkBoxMenu.Items.Count; i++)
                        {
                            if (Convert.ToInt32(chkBoxMenu.Items[i].Value) == b.MenuId)
                            {
                                chkBoxMenu.Items[i].Selected = true;
                            }
                        }
                    }
                    chkActive.Checked = false;

                }
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                    "alert('This user has menu permission but inactive now!');window.close();", true);
            }
            else
            {
                chkActive.Checked = false;

            }
        }
        private void GetNotExistUserInfo()
        {
            var userInfoList = new UserWiseMenuPerDa(false).GetNotExistUserByUserId(ddlUserId.SelectedValue);
            if (userInfoList.Any())
            {

            }
            else
            {
                chkActive.Checked = false;
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                    "alert('This user has no menu permission yet!');window.close();", true);

            }
        }
        #endregion
    }
}