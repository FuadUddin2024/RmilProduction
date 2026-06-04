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
    public partial class RmilQcWeighingScaleRecheck : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                if (!IsPostBack)
                {
                   
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
        private void ClearAll()
        {
            txtBarCode.Text = "";
            ddlproblem.Text = "";
        }
        #endregion
        #region Event Generated Method
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
                                    if (qcMaster != null)
                                    {
                                        UserInfo currentUser = (UserInfo)Session[WebUtility.SessionCurrentUserObj];
                                        //QcMaster QcMaster = new QcMaster();
                                        //  QcMaster.BarcodeNo = barcode;
                                        //   QcMaster.LineId = UserPermission.LineId;
                                        qcMaster.IsProb = "S";
                                        //  QcMaster.Qty = 1;
                                        //  QcMaster.PrModelId = ProductModelDetails.PrModelId;
                                        qcMaster.Active = true;
                                        qcMaster.QcInfo = ddlproblem.Text;
                                        qcMaster.EntryBy = currentUser.UserId;
                                        qcMaster.EntryDate = DateTime.Now;
                                        var IsAdded = new QcMasterDa(true).Update(qcMaster);
                                        scope.Complete();
                                        if (IsAdded == true)
                                        {
                                            // scope.Complete();
                                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                                             "alert('Successfully records Updated! ');window.close();", true);
                                            ClearAll();
                                            return;
                                        }
                                        else
                                        {
                                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                                            "alert('ERROR. Please Try Again');window.close();", true);
                                            return;
                                        }
                                        //RmilUserWiseLinePerDa UserWiseData = new RmilUserWiseLinePerDa();
                                        //var UserPermission = UserWiseData.GetUserWiseLineInfo(currentUser.UserId);
                                        //if (UserPermission != null)
                                        //{
                                        //    var ProductModelDetails = new RmilProdDetailsDa(true).GetRmilProdDetailsByBarcode(barcode);

                                        //}
                                        //else
                                        //{
                                        //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                                        //        "alert('Please assign user wise line!');window.close();", true);
                                        //    return;
                                        //}
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                                         "alert('The Product can not be found !');window.close();", true);
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                                        "alert('Invalid barcode!.');window.close();", true);
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