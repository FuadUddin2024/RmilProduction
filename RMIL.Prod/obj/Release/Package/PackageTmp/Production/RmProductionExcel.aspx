<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="RmProductionExcel.aspx.cs" Inherits="RMIL.Prod.Production.RmProductionExcel" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI, Version=2013.3.1015.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../assets/css/gridPaging.css" rel="stylesheet" />
    <style type="text/css">
        .HeaderCenterStyle {
            text-align: Center !important;
        }
    </style>
    <style type="text/css">
        .CenterHeader {
            text-align: center;
        }
        .tablediv table
        {
            border-collapse: collapse;
             width: 100%;
            text-align: center;
            color: black !important;
        }
        .tablediv table tr th
        {
            text-align: center;
        }
    </style>
    <script type="text/javascript">
        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        specialKeys.push(9); //Tab
        function IsNumeric(e) {
            var keyCode = e.which ? e.which : e.keyCode;
            var ret = ((keyCode >= 45 && keyCode <= 61 && keyCode != 191) || specialKeys.indexOf(keyCode) != -1);
            return ret;
        }
    </script>
    <script type="text/javascript">
        window.onload = function () {
            document.getElementById('menuProdMgt').className = 'active';
        };
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        inputList[i].checked = true;
                    }
                    else {
                        if (row.rowIndex % 2 == 0) {
                            row.style.backgroundColor = "#C2D69B";
                        }
                        else {
                            row.style.backgroundColor = "white";
                        }
                        inputList[i].checked = false;
                    }
                }
            }
        }
    </script>
    <script type="text/javascript">
        function SetTarget() {
            document.forms[1].target = "_blank";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- Row Start -->
            <section class="wrapper">
                <div align="right">
                    <b>Production ID</b> <span class="badge">
                        <%--<asp:TextBox ID="txtLastTrId" runat="server" class="form-control"></asp:TextBox></span>--%>
                       <asp:DropDownList ID="ddllasttrans" BackColor="#FFFFFF" CssClass="select2" runat="server" class="form-control">
                        </asp:DropDownList>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-default">
                            <div class="panel-heading" style="background-color: #82e0aa">
                                <div class="pull-left"><strong>Production Barcode Generation</strong></div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="panel-body" style="background-color: #99ccff">
                                <div class="row">
       
                                    <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-lg-4">Product Name</label>
                                                <div class="col-lg-8">
                                                    <asp:DropDownList ID="ddlProduct" BackColor="#FFFFFF" CssClass="select2" runat="server" class="form-control" ></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-lg-4">Production Date</label>
                                                <div class="col-lg-8">
                                                    <telerik:RadDatePicker ID="dtpProduction" TabIndex="11" runat="server">
                                                        <DateInput DateFormat="dd-MM-yyyy"
                                                            DisplayDateFormat="dd-MM-yyyy" runat="server" TabIndex="11">
                                                        </DateInput>
                                                        <DatePopupButton HoverImageUrl="" ImageUrl="" TabIndex="11" />
                                                    </telerik:RadDatePicker>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" ControlToValidate="dtpProduction" ValidationGroup="vgSubmit" runat="server"
                                                        ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                   <%-- <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-lg-4">Product Model</label>
                                                <div class="col-lg-8">
                                                    <asp:DropDownList ID="ddlProductModel" BackColor="#FFFFFF" CssClass="select2" runat="server" class="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>
                                </div>
                                <div class="row">
                                   <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-lg-4">Select Excel File</label>
                                                <div class="col-lg-8">
                                            <%--<asp:FileUpload ID="exlUpload" accept=".xls, .xlsx" multiple="false" runat="server" CssClass="fileUpload" />--%>
                                                    <asp:FileUpload ID="exlUpload" runat="server" CssClass="fileUpload" />

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
 <%--                               <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-lg-4">Qty</label>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="txtQty" runat="server" class="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="txtQty" ValidationGroup="vgSubmit" runat="server"
                                                        ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-lg-4">Production Date</label>
                                                <div class="col-lg-8">
                                                    <telerik:RadDatePicker ID="dtpProduction" TabIndex="11" runat="server">
                                                        <DateInput DateFormat="dd-MM-yyyy"
                                                            DisplayDateFormat="dd-MM-yyyy" runat="server" TabIndex="11">
                                                        </DateInput>
                                                        <DatePopupButton HoverImageUrl="" ImageUrl="" TabIndex="11" />
                                                    </telerik:RadDatePicker>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" ControlToValidate="dtpProduction" ValidationGroup="vgSubmit" runat="server"
                                                        ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>--%>
                                <div class="row">
 <%--                                    <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-lg-4">Supplier</label>
                                                <div class="col-lg-8">
                                                     <asp:TextBox ID="txtSuplier" MaxLength="2" placeholder="Enter Supplier Name (Only 2 Letter)" runat="server" class="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" ControlToValidate="txtSuplier" ValidationGroup="vgSubmit" runat="server"
                                                        ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>
                                    <%--<div class="col-md-2">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-lg-4"></label>
                                                <div class="col-lg-8">
                                                    
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>
                                    <div class="col-md-12">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-lg-4"></label>
                                                <div class="col-lg-8" style="float:right; gap: 2%; display: flex;">
                                                     <asp:Button ID="btnUploadFile" runat="server" Text="Show" CssClass="btn btn-light pull-right" OnClick="btnUploadFile_Click" />
                                                     <asp:Button ID="btnImport" runat="server" Text="Upload" CssClass="btn btn-warning pull-right" OnClick="btnSave_Click" />
<%--                                                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="vgSubmit"
                                                        OnClick="btnSave_Click" class="btn btn-primary pull-right" />--%>
                                                     <asp:Button ID="btnPrint" OnClick="btnPrint_Click" OnClientClick="SetTarget();" runat="server" Text="BarcodePrint"
                                                        class="btn btn-danger pull-right" />
                                                     <asp:Button ID="btnBarcode" OnClick="btnBarcode_Click" OnClientClick="SetTarget();" runat="server" Text="BarcodeNoPrint"
                                                        class="btn btn-success pull-right" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row tablediv">
                                     <asp:Label ID="lblError" Text="" runat="server" BackColor="#ff6666" ForeColor="White" Font-Bold="true"></asp:Label>
                                            <asp:GridView ID="gvDetails" runat="server"> </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            <!-- Row End -->
        </ContentTemplate>
         <Triggers>
            <asp:PostBackTrigger ControlID="btnPrint" />
            <asp:PostBackTrigger ControlID="btnBarcode" />
            <asp:PostBackTrigger ControlID="btnUploadFile" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
