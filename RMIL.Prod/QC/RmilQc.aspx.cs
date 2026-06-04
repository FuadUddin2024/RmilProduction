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
    public partial class RmilQc : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                if (!IsPostBack)
                {
                    LoadQcSteps();
                    ToggleCheckState(true);
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
        protected void LoadQcSteps()
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                UserInfo currentUser = (UserInfo)Session[WebUtility.SessionCurrentUserObj];
                var userId = currentUser.UserId;
                var stpList = new QcStepDa(true).GetUserWiseStepPerList(userId);
                ddlQcSteps.DataSource = stpList;
                this.ddlQcSteps.DataTextField = "StepsName";
                this.ddlQcSteps.DataValueField = "StpId";
                ddlQcSteps.DataBind();
                ddlQcSteps.DataSource = stpList;
                ddlQcSteps.DataBind();
                ddlQcSteps.Items.Insert(0, new ListItem("---Please Select---", "0"));

            }
            else
            {
                Response.Redirect("~/Account/Logout.aspx");
            }
        }
        protected void LoadProductGroupById(int groupId)
        {
            var pList = new CategoryDa(true).GetCategoryListById(groupId);
            ddlProductGroup.DataSource = pList;
            this.ddlProductGroup.DataTextField = "CategoryName";
            this.ddlProductGroup.DataValueField = "CategoryId";
            ddlProductGroup.DataBind();
        }
        protected void LoadProductById(int productId)
        {
            var pList = new ProductDa(true).GetProductListById(productId);
            ddlProduct.DataSource = pList;
            this.ddlProduct.DataTextField = "ProductName";
            this.ddlProduct.DataValueField = "ProductId";
            ddlProduct.DataBind();
        }
       
        protected void LoadModelById(int modelId)
        {
            var modelList = new ProductModelDa(true).GetProductModelListById(modelId);
            ddlModel.DataSource = modelList;
            this.ddlModel.DataTextField = "PrModelName";
            this.ddlModel.DataValueField = "PrModelId";
            ddlModel.DataBind();
        }
        protected void LoadLine()
        {
            var lList = new ProductionLineDa(true).GetAllProductionLine();
            ddlLine.DataSource = lList.OrderBy(c => c.LineName);
            this.ddlLine.DataTextField = "LineName";
            this.ddlLine.DataValueField = "LineId";
            ddlLine.DataBind();
            ddlLine.DataSource = lList.OrderBy(c => c.LineName);
            ddlLine.DataBind();
            ddlLine.Items.Insert(0, new ListItem("---Please Select---", "0"));
        }
        protected void LoadLineById(int lineId)
        {
            var lineList = new ProductionLineDa(true).GetLineListbyId(lineId);
            ddlLine.DataSource = lineList;
            this.ddlLine.DataTextField = "LineName";
            this.ddlLine.DataValueField = "LineId";
            ddlLine.DataBind();
        }
        private void ClearAll()
        {
            ddlQcSteps.SelectedValue = "0";
            txtBarCode.Text = "";
        }
        private void ReadOnly()
        {
            txtModelCode.ReadOnly = true;
            txtStatus.ReadOnly = true;
        }
        #endregion
        #region Event Generated Method
        protected void btnQcSave_Click(object sender, EventArgs e)
        {
            QcSave();
        }
        protected void btnQcSave1_Click(object sender, EventArgs e)
        {
            QcSave();
        }

        private void QcSave()
        {
            // Save here
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                UserInfo currentUser = (UserInfo)Session[WebUtility.SessionCurrentUserObj];
                try
                {
                    using (var scope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        using (RMILCSDbEntities db = new RMILCSDbEntities())
                        {
                            QcMaster qcThaMaster = new QcMaster();
                            if (ddlLine.SelectedValue == "0")
                            {
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                                    "alert('Please Select Line');window.close();", true);
                                return;
                            }
                            var barcode = txtBarCode.Text;
                            var qcMaster = new QcMasterDa(true).GetQcStepByBarcode(barcode);
                            var stInfo =
                                new QcStepDa(false).GetCategoryWiseInitialQcStep(Convert.ToInt32(ddlProductGroup.SelectedValue));

                            if (barcode != "")
                            {
                                var prDetInfo = new RmilProdDetailsDa(false).GetRmilProdDetailsByBarcode(barcode);
                                if (prDetInfo == null)
                                {
                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                                        "alert('This barcode does not exists.');window.close();", true);
                                    return;
                                }
                                if (qcMaster != null)
                                {
                                    if (qcMaster.CurSteps != Convert.ToInt32(ddlQcSteps.SelectedValue))
                                    {
                                        if (qcMaster.CurSteps != null)
                                        {
                                            var step = new QcStepDa(false).GetQcStepById((int)qcMaster.CurSteps);
                                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                                                string.Format("alert('It is pending in {0} !');", step.StepsName), true);
                                            return;
                                        }
                                    }
                                }
                                else
                                {
                                    if (stInfo.StpId != Convert.ToInt32(ddlQcSteps.SelectedValue))
                                    {
                                        var step = new QcStepDa(false).GetQcStepById(stInfo.StpId);
                                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                                            string.Format("alert('It is pending in {0} !');", step.StepsName), true);
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                                    "alert('Please enter barcode.');window.close();", true);
                                return;
                            }

                            var checkedBoxes = 0;
                            var checkedProblem = 0;
                            foreach (GridViewRow row in gvQcTest.Rows)
                            {
                                CheckBox chkQcPoints = (CheckBox)row.FindControl("chkQcPoints");
                                //TextBox txtDInfo = (TextBox)row.FindControl("txtQcInfo");

                                if (chkQcPoints.Checked)
                                {
                                    checkedBoxes++;
                                }
                                CheckBox chkQcProblem = (CheckBox)row.FindControl("chkQcProblem");

                                if (chkQcProblem.Checked)
                                {
                                    checkedProblem++;
                                }
                            }
                            if (checkedBoxes == 0)
                            {
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                                    "alert('Please check the item.');window.close();", true);
                                return;
                            }

                            // Insert into QcThaMaster
                            if (qcMaster == null)
                            {
                                qcThaMaster.LineId = Convert.ToInt32(ddlLine.SelectedValue);
                                qcThaMaster.PrModelId = Convert.ToInt32(ddlModel.SelectedValue);
                                qcThaMaster.PrModelName = ddlModel.SelectedItem.Text;
                                qcThaMaster.BarcodeNo = txtBarCode.Text;
                                qcThaMaster.Qty = 1;
                                qcThaMaster.Active = false;

                                if (checkedProblem > 0)
                                {
                                    qcThaMaster.IsProb = "P";
                                    //QcThaMaster.QcInfo = txtDInfo.Text;
                                }
                                else
                                {
                                    qcThaMaster.IsProb = "N";
                                }

                                if (checkedBoxes > 0 && checkedProblem == 0)
                                {
                                    var qcMove =
                                        new QcMoveDa(false).GetGroupWiseQcMoveBySteps(
                                            Convert.ToInt32(ddlProductGroup.SelectedValue), stInfo.StpId);
                                    if (qcMove != null)
                                    {
                                        qcThaMaster.CurSteps = qcMove.ToStId;
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                                            "alert('There is no next step for moving!');window.close();", true);
                                        pnlQcTestUpdate.Visible = false;
                                        pnlQcTest.Visible = false;
                                        return;
                                    }
                                }
                                else if (checkedBoxes > 0 && checkedProblem > 0)
                                {
                                    var qcsw =
                                        new QcSwitchDa(false).GetGroupWiseQcSwitchBySteps(
                                            Convert.ToInt32(ddlProductGroup.SelectedValue),
                                            Convert.ToInt32(ddlQcSteps.SelectedValue));
                                    if (qcsw != null)
                                    {
                                        qcThaMaster.CurSteps = qcsw.ToStId;
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                                            "alert('There is no step for fault switching!');window.close();", true);
                                        pnlQcTestUpdate.Visible = false;
                                        pnlQcTest.Visible = false;
                                        return;
                                    }
                                }

                                qcThaMaster.EntryBy = currentUser.UserId;
                                qcThaMaster.EntryDate = DateTime.Now;
                                qcThaMaster.EntryPC = WebUtility.GetIpAddress();

                                new QcMasterDa(false).Insert(qcThaMaster);
                            }
                            else
                            {
                                if (checkedProblem > 0)
                                {
                                    qcMaster.IsProb = "P";
                                }
                                else
                                {
                                    if (qcMaster.IsProb == "S")
                                    {
                                        qcMaster.IsProb = "S";
                                    }
                                    else
                                    {
                                        qcMaster.IsProb = "N";
                                    }
                                }

                                if (checkedBoxes > 0 && checkedProblem == 0)
                                {
                                    if (qcMaster.CurSteps != null)
                                    {
                                        var qcMove =
                                            new QcMoveDa(false).GetGroupWiseQcMoveBySteps(
                                                Convert.ToInt32(ddlProductGroup.SelectedValue), (int)qcMaster.CurSteps);
                                        if (qcMove != null)
                                        {
                                            qcMaster.CurSteps = qcMove.ToStId;
                                        }
                                        else
                                        {
                                            qcMaster.Active = true;
                                        }
                                    }
                                }
                                else if (checkedBoxes > 0 && checkedProblem > 0)
                                {
                                    var qcsw =
                                        new QcSwitchDa(false).GetGroupWiseQcSwitchBySteps(
                                            Convert.ToInt32(ddlProductGroup.SelectedValue),
                                            Convert.ToInt32(ddlQcSteps.SelectedValue));
                                    if (qcsw != null)
                                    {
                                        qcMaster.CurSteps = qcsw.ToStId;
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                                            "alert('No problem occurs in complete step!');window.close();", true);
                                        return;
                                    }
                                }
                                new QcMasterDa(false).Update(qcMaster);
                            }

                            if (ddlQcSteps.SelectedValue == "0")
                            {
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                                    "alert('Please Select QC Steps');window.close();", true);
                                return;
                            }
                            if (checkedProblem > 0)
                            {
                                // Insert into QcDetails
                                List<QcDetails> qcDetailsList = new List<QcDetails>();
                                foreach (GridViewRow row in gvQcTest.Rows)
                                {
                                    CheckBox chkQcPoints = (CheckBox)row.FindControl("chkQcPoints");
                                    CheckBox chkQcProblem = (CheckBox)row.FindControl("chkQcProblem");
                                    if (chkQcPoints.Checked)
                                    {
                                        int pointsId = int.Parse(row.Cells[1].Text);
                                        TextBox txtDInfo = (TextBox)row.FindControl("txtQcInfo");
                                        if (chkQcProblem.Checked)
                                        {
                                            qcDetailsList.Add(new QcDetails
                                            {
                                                //MId = new QcMasterDa(true).GetLastRecord().MId,
                                                DBarcodeNo = txtBarCode.Text,
                                                LineId = Convert.ToInt32(ddlLine.SelectedValue),
                                                PrModelId = Convert.ToInt32(ddlModel.SelectedValue),
                                                PrModelName = ddlModel.SelectedItem.Text,
                                                StpId = Convert.ToInt32(ddlQcSteps.SelectedValue),
                                                PtsId = pointsId,
                                                QcDInfo = txtDInfo.Text,
                                                //Problem = txtDInfo.Text,
                                                Qty = 1,
                                                Active = false,
                                                IsProb = "P",
                                                EntryBy = currentUser.UserId,
                                                EntryDate = DateTime.Now,
                                                EntryPC = WebUtility.GetIpAddress()
                                            });
                                        }
                                        //else
                                        //{
                                        //    qcDetailsList.Add(new QcRacDetails
                                        //    {
                                        //        //MId = new QcMasterDa(true).GetLastRecord().MId,
                                        //        DBarcodeNo = txtBarCode.Text,
                                        //        LineId = Convert.ToInt32(ddlLine.SelectedValue),
                                        //        PrModelId = Convert.ToInt32(ddlModel.SelectedValue),
                                        //        PrModelName = ddlModel.SelectedItem.Text,
                                        //        StpId = Convert.ToInt32(ddlQcSteps.SelectedValue),
                                        //        PtsId = pointsId,
                                        //        QcDInfo = txtDInfo.Text,
                                        //        Problem = txtDInfo.Text,
                                        //        Qty = 1,
                                        //        Active = true,
                                        //        IsProb = "N",

                                        //        EntryBy = currentUser.UserId,
                                        //        EntryDate = DateTime.Now,
                                        //        EntryPC = WebUtility.GetIpAddress()

                                        //    });
                                        //}
                                    }
                                }
                                //Insert into QcThaDetails Table
                                foreach (var i in qcDetailsList)
                                {
                                    db.QcDetails.Add(i);
                                }
                                db.SaveChanges();
                            }

                            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", string.Format("alert('Successfully {0} records inserted!');", qcDetailsList.Count), true);
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                                "alert('Successfully saved.');window.close();", true);
                            ClearAll();
                            pnlQcTest.Visible = false;
                            pnlRightSide.Visible = false;
                            scope.Complete();
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

        protected void btnQcUpdate_Click(object sender, EventArgs e)
        {
            QcUpdate();
        }
        protected void btnQcUpdate1_Click(object sender, EventArgs e)
        {
            QcUpdate();
        }

        private void QcUpdate()
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                UserInfo currentUser = (UserInfo)Session[WebUtility.SessionCurrentUserObj];
                var barcodeNo = txtBarCode.Text;
                if (barcodeNo != "")
                {
                    var checkedBoxesUp = 0;
                    var checkedProbUp = 0;
                    bool isUpdate = false;
                    //int stepId = int.Parse(ddlQcSteps.SelectedItem.Value);
                    foreach (GridViewRow row in gvQcTestUpdate.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkQcPointsUp = (CheckBox)row.FindControl("chkQcPointsUpdate");
                            CheckBox chkQcProblemUp = (CheckBox)row.FindControl("chkQcProblemUp");
                            bool isChecked = row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                            if (isChecked)
                            {
                                int qcDId = Convert.ToInt32(gvQcTestUpdate.DataKeys[row.RowIndex].Value);
                                int qcMId = int.Parse(row.Cells[1].Text);
                                //int qcMId = Convert.ToInt32(row.Cells[1].Controls.OfType<Label>().FirstOrDefault().Text);
                                TextBox txtDInfo = (TextBox)row.FindControl("txtQcInfo");
                                var qcdInfo = new QcDetailsDa(false).GetQcDetailsById(qcDId);
                                var qcmInfo = new QcMasterDa(false).GetQcMasterById(qcMId);
                                if (chkQcPointsUp.Checked)
                                {
                                    checkedBoxesUp++;
                                    if (chkQcProblemUp.Checked)
                                    {
                                        checkedProbUp++;
                                        if (qcdInfo != null)
                                        {
                                            if (qcdInfo.IsProb == "P")
                                            {
                                                qcdInfo.IsProb = "P";
                                                qcdInfo.Problem = txtDInfo.Text;
                                                qcdInfo.Active = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (qcdInfo != null)
                                        {
                                            if (qcdInfo.IsProb == "P")
                                            {
                                                qcdInfo.IsProb = "S";
                                                //qcdInfo.Problem = txtDInfo.Text;
                                                qcdInfo.Active = true;
                                            }
                                        }
                                    }
                                }
                                if (checkedBoxesUp == 0)
                                {
                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                                        "alert('Please check the item.');window.close();", true);
                                    return;
                                }
                                if (checkedBoxesUp > 0 && checkedProbUp == 0)
                                {
                                    if (qcmInfo != null)
                                    {
                                        qcmInfo.IsProb = "S";
                                        if (qcmInfo.CurSteps != null)
                                        {
                                            if (isUpdate == false)
                                            {
                                                var qcMove =
                                                    new QcMoveDa(false).GetGroupWiseQcMoveBySteps(
                                                        Convert.ToInt32(ddlProductGroup.SelectedValue), (int)qcmInfo.CurSteps);
                                                if (qcMove != null)
                                                {
                                                    qcmInfo.CurSteps = qcMove.ToStId;
                                                    isUpdate = true;
                                                }
                                                else
                                                {
                                                    //qcmInfo.Active = true;
                                                    if (qcdInfo != null) qcdInfo.Active = true;
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (checkedBoxesUp > 0 && checkedProbUp > 0)
                                {
                                    if (qcmInfo != null)
                                    {
                                        qcmInfo.IsProb = "P";
                                        var qcsw =
                                            new QcSwitchDa(false).GetGroupWiseQcSwitchBySteps(
                                                Convert.ToInt32(ddlProductGroup.SelectedValue),
                                                Convert.ToInt32(ddlQcSteps.SelectedValue));
                                        qcmInfo.CurSteps = qcsw.ToStId;
                                    }
                                }
                            }
                        }
                    }
                    new QcDetailsDa(false).UpdateData();
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                        "alert('Successfully updated.');window.close();", true);
                    ClearAll();
                    btnQcUpdate.Visible = false;
                    btnQcUpdate1.Visible = false;
                    pnlQcTestUpdate.Visible = false;
                    pnlRightSide.Visible = false;
                }
            }
            else
            {
                Response.Redirect("~/Account/Logout.aspx");
            }
        }

        protected void ddlQcSteps_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetQcValue();
        }

        private void GetQcValue()
        {
            var barcodeNo = txtBarCode.Text;
            if (barcodeNo != "")
            {
                var prDetInfo = new RmilProdDetailsDa(false).GetRmilProdDetailsByBarcode(barcodeNo);
                if (prDetInfo == null)
                {
                    btnQcUpdate.Visible = false;
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                        "alert('This barcode does not exists.');window.close();", true);
                    return;
                }
                var qcInfo = new QcMasterDa(true).GetQcStepByBarcode(barcodeNo);
                var stInfo = new QcStepDa(false).GetCategoryWiseInitialQcStep(Convert.ToInt32(ddlProductGroup.SelectedValue));
                if (qcInfo != null)
                {
                    if (qcInfo.CurSteps != Convert.ToInt32(ddlQcSteps.SelectedValue))
                    {
                        if (qcInfo.CurSteps != null)
                        {
                            var step = new QcStepDa(false).GetQcStepById((int)qcInfo.CurSteps);
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                                string.Format("alert('It is pending in {0} !');", step.StepsName), true);
                            pnlQcTestUpdate.Visible = false;
                            pnlQcTest.Visible = false;
                            return;
                        }
                    }
                }
                else
                {
                    if (stInfo != null)
                    {
                        if (stInfo.StpId != Convert.ToInt32(ddlQcSteps.SelectedValue))
                        {
                            var step = new QcStepDa(false).GetQcStepById(stInfo.StpId);
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                                string.Format("alert('It is pending in {0} !');", step.StepsName), true);
                            pnlQcTestUpdate.Visible = false;
                            pnlQcTest.Visible = false;
                            return;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                            "alert('No group wise QC step found!.');window.close();", true);
                        return;
                    }
                }
            }
            try
            {
                int qcStId = int.Parse(ddlQcSteps.SelectedItem.Value);
                if (barcodeNo != "")
                {
                    var mInfo = new QcMasterDa(true).GetProbQcStepByBarcode(barcodeNo);
                    if (mInfo != null)
                    {
                        pnlQcTestUpdate.Visible = true;
                        pnlQcTest.Visible = false;
                        var qcptsList = new QcDetailsDa(true).GetQcProbListByBarcode(barcodeNo);
                        gvQcTestUpdate.DataSource = qcptsList;
                        gvQcTestUpdate.DataBind();
                        ToggleCheckUpState(true);
                        btnQcUpdate.Visible = true;
                        btnQcUpdate1.Visible = true;
                        btnQcSave1.Visible = false;
                    }
                    else
                    {
                        if (qcStId > 0)
                        {
                            var qcpList =
                                new QcPointAsnDa(true).GetGroupWiseQcPointsListByStId(
                                    Convert.ToInt32(ddlProductGroup.SelectedValue), qcStId);
                            if (qcpList.Any())
                            {
                                pnlQcTest.Visible = true;
                                pnlQcTestUpdate.Visible = false;
                                gvQcTest.DataSource = qcpList;
                                gvQcTest.DataBind();
                                ToggleCheckState(true);
                                btnQcSave.Visible = true;
                                btnQcSave1.Visible = true;
                            }
                            else
                            {
                                //gvQcTest.DataSource = null;
                                //gvQcTest.DataBind();
                                //btnQcSave.Visible = false;
                                //btnQcUpdate.Visible = false;
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                                    "alert('Group wise QC points did not assign yet!.');window.close();", true);
                                return;
                            }
                        }
                        else
                        {
                            pnlQcTest.Visible = false;
                            pnlQcTestUpdate.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void txtBarCode_TextChanged(object sender, EventArgs e)
        {
            CheckBarcodeExist();
            ReadOnly();
        }
        private void CheckBarcodeExist()
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                UserInfo currentUser = (UserInfo)Session[WebUtility.SessionCurrentUserObj];
                var barcodeNo = txtBarCode.Text;
                if (barcodeNo != "")
                {
                    var prDetInfo = new RmilProdDetailsDa(false).GetRmilProdDetailsByBarcode(barcodeNo);
                    if (prDetInfo != null)
                    {
                        var qcComplete = new QcMasterDa(true).GetQcFinishByBarcode(barcodeNo);
                        if (qcComplete != null)
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('QC completed for this barcode!.');window.close();", true);
                            ddlQcSteps.Enabled = false;
                            txtBarCode.Text = "";
                            return;
                        }
                        if (prDetInfo.PrModelId != null)
                        {
                            pnlRightSide.Visible = true;
                            var fgInfo = new ProductModelDa(false).GetProductModelById((int)prDetInfo.PrModelId);
                            var prodnfo = new ProductDa(true).GetProductById((int)fgInfo.ProductId);
                            if (prodnfo != null)
                            {

                                var pname = prodnfo.ProductName;
                                LoadProductById(prodnfo.ProductId);
                                if (ddlProduct.Items.Contains(ddlProduct.Items.FindByText(pname)))
                                    ddlProduct.SelectedIndex = ddlProduct.Items.IndexOf(ddlProduct.Items.FindByText(pname));

                                if (prodnfo.CategoryId != null)
                                {
                                    var group = new CategoryDa(true).GetCategoryById((int)prodnfo.CategoryId);
                                    var gname = group.CategoryName;
                                    LoadProductGroupById((int) prodnfo.CategoryId);
                                    if (ddlProductGroup.Items.Contains(ddlProductGroup.Items.FindByText(gname)))
                                        ddlProductGroup.SelectedIndex = ddlProductGroup.Items.IndexOf(ddlProductGroup.Items.FindByText(gname));
                                }
                           
                            }
                            var frId = fgInfo.PrModelId;
                            if (frId != null)
                            {
                                var q = new ProductModelDa(true).GetProductModelById((int)frId);
                                var tname = q.PrModelName;
                                LoadModelById(frId);
                                if (ddlModel.Items.Contains(ddlModel.Items.FindByText(tname)))
                                    ddlModel.SelectedIndex = ddlModel.Items.IndexOf(ddlModel.Items.FindByText(tname));

                                txtModelCode.Text = q.ModelCode.ToString();
                            }
                           
                            var qcInfo = new QcMasterDa(true).GetQcStepByBarcode(barcodeNo);

                            if (qcInfo != null)
                            {
                                if (qcInfo.CurSteps != null)
                                {
                                    var step = new QcStepDa().GetQcStepById((int)qcInfo.CurSteps);
                                    txtStatus.Text = step.StepsName;

                                    var thaStpId = step.StpId;
                                    if (thaStpId != null)
                                    {
                                        var tname = step.StepsName;
                                        LoadQcSteps();
                                        if (ddlQcSteps.Items.Contains(ddlQcSteps.Items.FindByText(tname)))
                                            ddlQcSteps.SelectedIndex = ddlQcSteps.Items.IndexOf(ddlQcSteps.Items.FindByText(tname));
                                        //btnQcSave1.Visible = true;
                                        //btnQcSave.Visible = true;
                                        GetQcValue();
                                    }
                                }
                                if (qcInfo.LineId != null) LoadLineById((int)qcInfo.LineId);
                            }
                            else
                            {
                                if (prodnfo != null && prodnfo.CategoryId != null)
                                {
                                    var stInfo = new QcStepDa(false).GetCategoryWiseInitialQcStep((int)prodnfo.CategoryId);
                                    if (stInfo != null)
                                    {
                                        txtStatus.Text = stInfo.StepsName;

                                        var stpId = stInfo.StpId;
                                        if (stpId != null)
                                        {
                                            var tname = stInfo.StepsName;
                                            LoadQcSteps();
                                            if (ddlQcSteps.Items.Contains(ddlQcSteps.Items.FindByText(tname)))
                                                ddlQcSteps.SelectedIndex = ddlQcSteps.Items.IndexOf(ddlQcSteps.Items.FindByText(tname));
                                            GetQcValue();
                                        }
                                    }
                                }
                                var lp = new RmilUserWiseLinePerDa(false).GetUserWiseLineInfo(currentUser.UserId);
                                if (lp.LineId != null)
                                {
                                    LoadLineById((int)lp.LineId);
                                }
                                else
                                {
                                    LoadLine();
                                }
                            }
                        }
                    }
                    else
                    {
                        pnlQcTest.Visible = false;
                        pnlRightSide.Visible = false;
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                            "alert('This barcode does not exists.');window.close();", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript",
                        "alert('Please input barcode no.');window.close();", true);
                }

            }
            else
            {
                Response.Redirect("~/Account/Logout.aspx");
            }

        }
        protected void CheckAll_Click(object sender, EventArgs e)
        {
            ToggleCheckState(true);
        }
        protected void UncheckAll_Click(object sender, EventArgs e)
        {
            ToggleCheckState(false);
        }
        private void ToggleCheckState(bool checkState)
        {
            // Iterate through the Qc.Rows property
            foreach (GridViewRow row in gvQcTest.Rows)
            {
                // Access the CheckBox
                CheckBox cb = (CheckBox)row.FindControl("chkQcPoints");
                if (cb != null)
                    cb.Checked = checkState;
            }
        }
        protected void CheckAllUp_Click(object sender, EventArgs e)
        {
            ToggleCheckUpState(true);
        }
        protected void UncheckAllUp_Click(object sender, EventArgs e)
        {
            ToggleCheckUpState(false);
        }
        private void ToggleCheckUpState(bool checkState)
        {
            // Iterate through the Qc.Rows property
            foreach (GridViewRow row in gvQcTestUpdate.Rows)
            {
                // Access the CheckBox
                CheckBox cb = (CheckBox)row.FindControl("chkQcPointsUpdate");
                if (cb != null)
                    cb.Checked = checkState;
            }
        }
        #endregion
    }
}