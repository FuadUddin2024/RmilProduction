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
using System.Data;

namespace RMIL.Prod.QC
{
    public partial class RmilBoxAssingedProducts : System.Web.UI.Page
    {
        #region Page Load
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
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["modelcode"] != null)
            {
                CreateTextBoxes();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                if (!IsPostBack)
                {
                    Session["AssignProductBarcode"] = null;
                    LoadLine();
                    // Newly Added Code 11.5.26 start
                    string script = @"
        document.addEventListener('keydown', function(e) {
            if (e.key === 'Enter') {
                e.preventDefault();
                return false;
            }
        });
        ";
                    btnSubmit.Visible = false;
                    masterbarcode.Visible = false;
                    productbarcode.Visible = false;
                    ClientScript.RegisterStartupScript(this.GetType(), "PreventEnter", script, true);
                    submitallbutton.Visible = false;
                    // Newly Added Code 11.5.26 end
                }
                //txtBarCode.Focus();
                ddlmodelname.ReadOnly = true;
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
            ddlmasterbarcode.Text = "";
            foreach (TextBox txt in phInputs.Controls.OfType<TextBox>())
            {
                txt.Text = "";
            }
            submitallbutton.Visible = false;
        }
        #endregion
        #region Event Generated Method
        protected void LoadLine()
        {
            var lList = new DepoDa().GetAllDepoList();
            ddldepo.DataSource = lList.OrderBy(c => c.DepotName);
            this.ddldepo.DataTextField = "DepotName";
            this.ddldepo.DataValueField = "DepotId";
            ddldepo.DataBind();
            ddldepo.DataSource = lList.OrderBy(c => c.DepotName);
            ddldepo.DataBind();
            ddldepo.Items.Insert(0, new ListItem("---Please Select---", "0"));
        }
        protected void txtBarCode_TextChanged(object sender, EventArgs e)
        {
            var ModelCode = ddlproductcode.Text;

            if (!string.IsNullOrEmpty(ModelCode))
            {
                var modelname = new ProductModelDa(true)
                    .GetAllProductModel()
                    .FirstOrDefault(x => x.ModelCode == Convert.ToInt32(ModelCode));
                if (modelname != null)
                {
                    ddlmodelname.Text = modelname.PrModelName;
                   // Session["totalbox"] = Convert.ToInt32(modelname.SingleCarton);
                    Session["modelcode"] = ModelCode;
                    CreateTextBoxes();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page),"ClientScript", "alert('Please set Correct Product Code');", true);
                }
            }
        }
        protected void Txt_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;

            if (!string.IsNullOrEmpty(txt.Text))
            {
                RmilAssingedBoxMasterDa Checkbarcode = new RmilAssingedBoxMasterDa();
                var findingvalues = Checkbarcode.CheckAllBarcode(txt.Text);
                var Barcode = txt.Text;
                if (findingvalues == true)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Already Saved');", true);
                    txt.Text = "";
                }
                else
                {
                    txt.Focus();
                    txt.Text = Barcode;
                }
          }
        }
        private void CreateTextBoxes()
        {
            var ModelCode =Convert.ToInt32(Session["modelcode"]);
            if (ModelCode>0)
            {
                var MasterBox = new ProductModelDa(true).GetAllProductModel().FirstOrDefault(x => x.ModelCode == Convert.ToInt32(ModelCode));
                if(MasterBox != null)
                {
                    phInputs.Controls.Clear();

                    for (int i = 1; i <= MasterBox.SingleCarton; i++)
                    {
                        TextBox txt = new TextBox();

                        txt.ID = "txtInput_" + i;
                        txt.EnableViewState = true;
                        txt.Attributes.Add("maxlength", "16");
                        txt.TextChanged += Txt_TextChanged;
                        txt.Attributes["onkeyup"] =
                       "if(this.value.length==16){" +
                       Page.ClientScript.GetPostBackEventReference(txt, "") +
                       ";}";

                        phInputs.Controls.Add(txt);
                        phInputs.Controls.Add(new Literal { Text = "<br/>" });
                    }
                    phInputs.Focus();
                    btnSubmit.Visible = true;
                    masterbarcode.Visible = true;
                    productbarcode.Visible = true;
                }
            }
        }
        protected void TxtSingle_TextChanged(object sender, EventArgs e)
        {
            if (ddlmasterbarcode.Text != null)
            {
                RmilAssingedBoxMasterDa Checkbarcode = new RmilAssingedBoxMasterDa();
                var findingvalues = Checkbarcode.CheckAllMasterBarcode(ddlmasterbarcode.Text);
                if (findingvalues == true)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Already Saved');", true);
                    ddlmasterbarcode.Text = "";
                }
                else
                {
                    submitallbutton.Visible = true;
                }
                //int limit = Convert.ToInt32(Session["totalbox"]);
                //if(limit>1)
                //{
                //    var findingvalues = Checkbarcode.CheckAllBarcode(ddlmasterbarcode.Text);
                //    if (findingvalues == true)
                //    {
                //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Already Saved');", true);
                //        ddlmasterbarcode.Text = "";
                //    }
                //    else
                //    {
                //        submitallbutton.Visible = true;
                //    }
                //}
                //else
                //{
                //    var findingvalues = Checkbarcode.CheckAllMasterBarcode(ddlmasterbarcode.Text);
                //    if (findingvalues == true)
                //    {
                //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Already Saved');", true);
                //        ddlmasterbarcode.Text = "";
                //    }
                //    else
                //    {
                //        submitallbutton.Visible = true;
                //    }

                //}
            }
            // btnSubmit.Enabled = true;.
            
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SaveAssignedProducts();
        }
        private bool SaveAssignedProducts()
        {
            bool IsAdded = true;
           // CreateTextField();
            // Save here
            if (Session[WebUtility.SessionCurrentUserObj] != null)
            {
                try
                {
                    using (var scope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        using (RMILCSDbEntities db = new RMILCSDbEntities())
                        {
                            UserInfo currentUser = (UserInfo)Session[WebUtility.SessionCurrentUserObj];
                            var ModelCode = Convert.ToInt32(Session["modelcode"]);
                            var MasterBox = new ProductModelDa(true).GetAllProductModel().FirstOrDefault(x => x.ModelCode == Convert.ToInt32(ModelCode));
                            var MasterBarcode = ddlmasterbarcode.Text;
                            if(MasterBarcode != null)
                            {
                                if(MasterBarcode.Count()==16 && MasterBox.SingleCarton > 1)
                                {
                                    if (MasterBox.SingleCarton > 1)
                                    {
                                        for (int i = 1; i <= MasterBox.SingleCarton; i++)
                                        {
                                            TextBox txt = phInputs.Controls.OfType<TextBox>().FirstOrDefault(x => x.ID == "txtInput_" + i);
                                            if (txt != null && txt.Text.Count() == 16)
                                            {
                                                var MasterBoxMapping = new MasterBoxAssigned()
                                                {
                                                    BoxBarCode = MasterBarcode,
                                                    ProductModelBarcode = txt.Text,
                                                    PackedBy = currentUser.UserId,
                                                    IsSent = 1,
                                                    EntryBy = currentUser.UserId,
                                                    EntryDate = DateTime.Now,
                                                    DepoID = Convert.ToInt32(ddldepo.SelectedValue)
                                                };
                                                var DistributonIn = new DistributionWeight()
                                                {
                                                    BarcodeNo = txt.Text,
                                                    ModelCode = Convert.ToInt32(ddlproductcode.Text),
                                                    ModelName = ddlmodelname.Text,
                                                    Position = "Factory",
                                                    DepotId = Convert.ToInt32(ddldepo.SelectedValue),
                                                    DepotName = ddldepo.SelectedItem.Text,
                                                    IsSend = false
                                                };
                                                new RmilAssingedBoxMasterDa(true).Insert(MasterBoxMapping);
                                                new DistributionDa(true).Insert(DistributonIn);
                                            }
                                            else
                                            {
                                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Barcode Missing or Enter valid Barcode');", true);
                                                return false;
                                            }
                                        }
                                        scope.Complete();
                                        ClearAll();
                                    }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Master Barcode Missing or Enter valid Barcode');", true);
                                    return false;
                                }
                            }
                                else
                                {
                                    if (MasterBarcode.Count() == 16)
                                    {
                                        var MasterBoxMapping = new MasterBoxAssigned()
                                        {
                                            BoxBarCode = MasterBarcode,
                                            ProductModelBarcode = MasterBarcode,
                                            PackedBy = currentUser.UserId,
                                            IsSent = 1,
                                            EntryBy = currentUser.UserId,
                                            EntryDate = DateTime.Now,
                                            DepoID = Convert.ToInt32(ddldepo.SelectedValue)
                                        };
                                        var DistributonIn = new DistributionWeight()
                                        {
                                            BarcodeNo = MasterBarcode,
                                            ModelCode = Convert.ToInt32(ddlproductcode.Text),
                                            ModelName = ddlmodelname.Text,
                                            Position = "Factory",
                                            DepotId = Convert.ToInt32(ddldepo.SelectedValue),
                                            DepotName = ddldepo.SelectedItem.Text,
                                            IsSend=false
                                            //  TName= "Distribution Storage (In Factory)"
                                        };
                                        new RmilAssingedBoxMasterDa(true).Insert(MasterBoxMapping);
                                        new DistributionDa(true).Insert(DistributonIn);
                                        scope.Complete();
                                        ClearAll();
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Master Barcode Missing or Enter valid Barcode');", true);
                                        return false;
                                    }
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript", "alert('Please Enter Master Barcode');", true);
                                return false;
                            }
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientScript","alert('Sucessfully Inserted.');", true);
                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    IsAdded = false;
                    Response.Write(ex.Message);
                }
               
            }
            else
            {
                Response.Redirect("~/Account/Logout.aspx");
            }
            return IsAdded;
        }
        #endregion
    }
}