using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RMIL.Prod.DAL;
using RMIL.Prod.EntityFramework;
using RMIL.Prod.Utility;

namespace RMIL.Prod.QC
{
    public partial class RmilQcWeighingScale : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                if (!IsPostBack)
                {
                    pnlProb.Visible = false;
                }
                txtBarCode.Focus();
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
        protected void btnShow_Click(object sender, EventArgs e)
        {
            pnlProb.Visible = true;
            btnQcSave.Visible = false;
            btnProbSave.Visible = true;
            btnShow.Visible = false;
            btnHide.Visible = true;
        }
        protected void btnHide_Click(object sender, EventArgs e)
        {
            pnlProb.Visible = false;
            btnQcSave.Visible = true;
            btnProbSave.Visible = false;
            btnShow.Visible = true;
            btnHide.Visible = false;
        }
        private void ClearAll()
        {
            txtBarCode.Text = "";
            ddlproblem.Text = "";
        }
        #endregion
        #region Event Generated Method
        //protected void txtBarCode_TextChanged(object sender, EventArgs e)
        //{
        //    CheckBarcodeExist();
        //   // ReadOnly();
        //}
        //private void CheckBarcodeExist()
        //{
        //    if (Session[WebUtility.SessionCurrentUserObj] != null)
        //    {
        //        if()
        //        {

        //        }
        //        else
        //        {

        //        }
        //    }
        //    else
        //    {
        //        Response.Redirect("~/Account/Logout.aspx");
        //    }

        //}
        private void ClearALL()
        {
            txtBarCode.Text = "";
            ddlproblem.Text = "";
        }
        protected void btnProbSave_Click(object sender, EventArgs e)
        {
            QcSaveProblem();
        }
        protected void btnQcSave_Click(object sender, EventArgs e)
        {
            QcSave();
        }

        private void QcSave()
        {
            // Save here
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                try
                {
                    using (var scope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        using (RMILCSDbEntities db = new RMILCSDbEntities())
                        {
                            QcMaster qcThaMaster = new QcMaster();
                            var barcode = txtBarCode.Text;
                            if(barcode != null)
                            {
                                var prDetInfo = new RmilProdDetailsDa(false).GetRmilProdDetailsByBarcode(barcode);
                                if(prDetInfo != null)
                                {
                                    var qcMaster = new QcMasterDa(true).GetQcStepByBarcode(barcode);
                                    if (qcMaster == null)
                                    {
                                        UserInfo currentUser = (UserInfo)Session[WebUtility.SessionCurrentUserObj];
                                        RmilUserWiseLinePerDa UserWiseData = new RmilUserWiseLinePerDa();
                                        var UserPermission = UserWiseData.GetUserWiseLineInfo(currentUser.UserId);
                                        if (UserPermission != null)
                                        {
                                            var ProductModelDetails = new RmilProdDetailsDa(true).GetRmilProdDetailsByBarcode(barcode);
                                            QcMaster QcMaster = new QcMaster();
                                            QcMaster.BarcodeNo = barcode;
                                            QcMaster.LineId = UserPermission.LineId;
                                            QcMaster.IsProb = "N";
                                            QcMaster.Qty = 1;
                                            QcMaster.PrModelId = ProductModelDetails.PrModelId;
                                            QcMaster.Active = true;
                                            QcMaster.EntryBy = currentUser.UserId;
                                            QcMaster.EntryDate = DateTime.Now;
                                           var IsAdded= new QcMasterDa(true).Insert(QcMaster);
                                        //    scope.Complete();
                                            if (IsAdded==true)
                                            {
                                                scope.Complete();
                                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                                                 "alert('Successfully records inserted! ');window.close();", true);
                                                ClearALL();
                                                return;
                                            }
                                            else
                                            {
                                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                                                "alert('ERROR. Please Try Again');window.close();", true);
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                                                "alert('Please assign user wise line!');window.close();", true);
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                                         "alert('Already saved!');window.close();", true);
                                        ClearALL();
                                        return;
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                                        "alert('Invalid barcode!.');window.close();", true);
                                    ClearALL();
                                    return;
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                            "alert('Please input barcode no.');window.close();", true);
                                return;
                            }
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

        private void QcSaveProblem()
        {
            // Save here
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                try
                {
                    using (var scope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        using (RMILCSDbEntities db = new RMILCSDbEntities())
                        {
                            QcMaster qcThaMaster = new QcMaster();
                            var barcode = txtBarCode.Text;
                            if (barcode != null)
                            {
                                var prDetInfo = new RmilProdDetailsDa(false).GetRmilProdDetailsByBarcode(barcode);
                                if (prDetInfo != null)
                                {
                                    var qcMaster = new QcMasterDa(true).GetQcStepByBarcode(barcode);
                                    if (qcMaster == null)
                                    {
                                        UserInfo currentUser = (UserInfo)Session[WebUtility.SessionCurrentUserObj];
                                        RmilUserWiseLinePerDa UserWiseData = new RmilUserWiseLinePerDa();
                                        var UserPermission = UserWiseData.GetUserWiseLineInfo(currentUser.UserId);
                                        if (UserPermission != null)
                                        {
                                            var problemName = ddlproblem.Text;
                                            if (problemName != null)
                                            {
                                                var ProductModelDetails = new RmilProdDetailsDa(true).GetRmilProdDetailsByBarcode(barcode);
                                                QcMaster QcMaster = new QcMaster();
                                                QcMaster.BarcodeNo = barcode;
                                                QcMaster.LineId = UserPermission.LineId;
                                                QcMaster.IsProb = "P";
                                                QcMaster.Qty = 1;
                                                QcMaster.PrModelId = ProductModelDetails.PrModelId;
                                                QcMaster.QcInfo = problemName;
                                                QcMaster.Active = true;
                                                QcMaster.EntryBy = currentUser.UserId;
                                                QcMaster.EntryDate = DateTime.Now;
                                                var IsAdded = new QcMasterDa(true).Insert(QcMaster);
                                                if (IsAdded == true)
                                                {
                                                    scope.Complete();
                                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                                                     "alert('Successfully records inserted! ');window.close();", true);
                                                    ClearALL();
                                                    return;
                                                }
                                                else
                                                {
                                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                                                    "alert('ERROR. Please Try Again');window.close();", true);
                                                    return;
                                                }
                                            }
                                            else
                                            {
                                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                                                   "alert('Please Enter The Problem');window.close();", true);
                                                return;
                                            }
                                           
                                        }
                                        else
                                        {
                                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                                                "alert('Please assign user wise line!');window.close();", true);
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                                         "alert('Already saved!');window.close();", true);
                                        ClearALL();
                                        return;
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                                        "alert('Invalid barcode!.');window.close();", true);
                                    ClearALL();
                                    return;
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                            "alert('Please input barcode no.');window.close();", true);
                                return;
                            }
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
        #endregion
    }
}