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
    public partial class MenuSetup : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                if (!IsPostBack)
                {
                    LoadMenuGridView();
                    LoadParentMenuList();
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
        private void LoadMenuGridView()
        {
            var menuList = new MenusDa(true).GetMenusList();
            gvMenu.DataSource = menuList;
            gvMenu.DataBind();
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
        protected void gvMenu_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMenu.PageIndex = e.NewPageIndex;
            this.LoadMenuGridView();
        }
        private void ClearAll()
        {
            txtMenuTitle.Text = "";
            txtUrl.Text = "";
            txtDescription.Text = "";
            ddlParentMenu.SelectedValue = "0";
        }
        #endregion
        #region Event Generated Method
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                UserInfo currentUser = (UserInfo)Session[WebUtility.SessionCurrentUserObj];
                if (currentUser.MenuPer == true)
                {
                    try
                    {
                        var m = new MenusDa(true).GetMenusByNameParentMenuId(txtMenuTitle.Text, Convert.ToInt32(ddlParentMenu.SelectedValue));
                        if (m != null)
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('This menu already exists!');window.close();", true);
                            return;
                        }
                        Menus menus = new Menus();
                        menus.Title = txtMenuTitle.Text.Trim();
                        menus.Url = txtUrl.Text.Trim();
                        menus.Description = txtDescription.Text.Trim();
                        if (ddlParentMenu.SelectedValue == "0")
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Parent Menu');window.close();", true);
                            return;
                        }
                        menus.ParentMenuId = Convert.ToInt32(ddlParentMenu.SelectedValue);
                        menus.Active = chkActive.Checked;

                        menus.EntryBy = currentUser.UserId;
                        menus.EntryDate = DateTime.Now;
                        menus.EntryPC = WebUtility.GetIpAddress();

                        new MenusDa(false).Insert(menus);
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Inserted.');window.close();", true);
                        LoadMenuGridView();
                        ClearAll();
                    }
                    catch (Exception ex)
                    {

                        Response.Write(ex.Message);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('You have no permission!');window.close();", true);
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
                if (currentUser.MenuPer == true)
                {
                    try
                    {
                        if (Session["menuId"] != null)
                        {
                            //var m = new MenusDa(true).GetMenusByNameParentMenuId(txtMenuTitle.Text, Convert.ToInt32(ddlParentMenu.SelectedValue));
                            //if (m != null)
                            //{
                            //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('This menu already exists!');window.close();", true);
                            //    return;
                            //}

                            int menuId = Convert.ToInt32(Session["menuId"]);
                            var selecteddata = new MenusDa(false).GetMenusById(menuId);
                            selecteddata.Title = txtMenuTitle.Text.Trim();
                            selecteddata.Url = txtUrl.Text.Trim();
                            selecteddata.Description = txtDescription.Text.Trim();
                            if (ddlParentMenu.SelectedValue == "0")
                            {
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Select Parent Menu');window.close();", true);
                                return;
                            }
                            selecteddata.ParentMenuId = Convert.ToInt32(ddlParentMenu.SelectedValue);
                            selecteddata.Active = chkActive.Checked;

                            selecteddata.ModifiedBy = currentUser.UserId;
                            selecteddata.ModifiedDate = DateTime.Now;
                            selecteddata.ModifiedPC = WebUtility.GetIpAddress();

                            new MenusDa(false).Update(selecteddata);
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Updated.');window.close();", true);
                            LoadMenuGridView();
                            ClearAll();
                            btnSave.Visible = true;
                            btnUpdate.Visible = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.Message);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('You have no permission!');window.close();", true);
                }
            }
            else
            {
                Response.Redirect("~/Account/Logout.aspx");
            }
        }

        protected void btnEditMenu_Click(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                UserInfo currentUser = (UserInfo)Session[WebUtility.SessionCurrentUserObj];
                if (currentUser.MenuPer == true)
                {
                    btnSave.Visible = false;
                    btnUpdate.Visible = true;

                    try
                    {
                        int menuId = Convert.ToInt32(((ImageButton)sender).CommandArgument);
                        Session["menuId"] = menuId;
                        var selecteddata = new MenusDa(false).GetMenusById(menuId);
                        txtMenuTitle.Text = selecteddata.Title;
                        txtUrl.Text = selecteddata.Url;
                        txtDescription.Text = selecteddata.Description;
                        var pmenuId = selecteddata.ParentMenuId;
                        if (pmenuId != null)
                        {
                            var q = new ParentMenusDa(true).GetParentMenusById((int)pmenuId);
                            var pname = q.ParentMenuName;
                            LoadParentMenuList();
                            if (ddlParentMenu.Items.Contains(ddlParentMenu.Items.FindByText(pname)))
                                ddlParentMenu.SelectedIndex = ddlParentMenu.Items.IndexOf(ddlParentMenu.Items.FindByText(pname));
                        }
                        if (selecteddata.Active != null && (bool)selecteddata.Active)
                        {
                            chkActive.Checked = true;
                        }
                        else
                        {
                            chkActive.Checked = false;
                        }

                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.Message);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('You have no permission!');window.close();", true);
                }
            }
            else
            {
                Response.Redirect("~/Account/Logout.aspx");
            }
        }
        protected void btnDeleteMenu_Click(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                UserInfo currentUser = (UserInfo)Session[WebUtility.SessionCurrentUserObj];
                if (currentUser.MenuPer == true)
                {
                    try
                    {
                        int menuId = Convert.ToInt32(((ImageButton)sender).CommandArgument);
                        var selectedData = new MenusDa(false).GetMenusById(menuId);
                        new MenusDa(false).Delete(selectedData);

                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Successfully Deleted.');window.close();", true);
                        LoadMenuGridView();

                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.Message);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('You have no permission!');window.close();", true);
                }
            }
            else
            {
                Response.Redirect("~/Account/Logout.aspx");
            }
        }
        #endregion
    }
}