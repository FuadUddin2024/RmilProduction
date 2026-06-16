<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="RptProductionMove.aspx.cs" Inherits="RMIL.Prod.Reports.RptProductionMove" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <section class="wrapper">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading" style="background-color: #82e0aa">
                        <div class="pull-left"><strong>Packaging Report</strong></div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="panel-body" style="background-color: #99ccff">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-lg-4">Start Date</label>
                                        <div class="col-lg-8">
                                            <telerik:RadDateTimePicker Width="230" ID="dtpStart" required RenderMode="Lightweight" TabIndex="11" runat="server">
                                                <DateInput DateFormat="dd-MM-yyyy H:mm:ss"
                                                    DisplayDateFormat="dd-MM-yyyy H:mm:ss" runat="server" TabIndex="11">
                                                </DateInput>
                                                <DatePopupButton HoverImageUrl="" ImageUrl="" TabIndex="11" />
                                            </telerik:RadDateTimePicker>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-lg-4">End Date</label>
                                        <div class="col-lg-8">
                                            <telerik:RadDateTimePicker Width="230" ID="dtpEnd" required RenderMode="Lightweight" TabIndex="11" runat="server">
                                                <DateInput DateFormat="dd-MM-yyyy H:mm:ss"
                                                    DisplayDateFormat="dd-MM-yyyy H:mm:ss" runat="server" TabIndex="11">
                                                </DateInput>
                                                <DatePopupButton HoverImageUrl="" ImageUrl="" TabIndex="11" />
                                            </telerik:RadDateTimePicker>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                        <%--    <div class="col-md-6">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-lg-4">Status</label>
                                        <div class="col-lg-8">
                                            <asp:DropDownList ID="ddlStatus" Width="224" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="---All---" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Success Without Problem" Value="N"></asp:ListItem>
                                                <asp:ListItem Text="Success With Problem" Value="S"></asp:ListItem>
                                                <asp:ListItem Text="Problem" Value="P"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>--%>
                            <div class="col-md-6">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-lg-4"></label>
                                        <div class="col-lg-8">
                                            <asp:Button ID="btnShow" runat="server" Text="Show" ValidationGroup="vgSubmit"
                                                OnClick="btnShow_Click" class="btn btn-primary pull-left" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <asp:Panel ID="pnlReport" runat="server" Visible="False">
        <section class="wrapper">
            <div>
                <asp:ImageButton ID="ImageButton1" class="btn btn-info pull-right" runat="server" ImageUrl="~/Images/excel.png"
                    OnClick="ImageButton_Click" ToolTip="Export to excel" AlternateText="ExcelML" />
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panel-default">
                        <div class="panel-heading" style="background-color: #82e0aa">
                            <div class="pull-left"><strong>Master Cartoon Assgin Report</strong></div>
                            <div class="clearfix"></div>
                        </div>
                        <div class="panel-body" style="background-color: #99ccff">
                            <div class="col-md-12">
                                <telerik:RadGrid AutoGenerateColumns="false" ID="gvQc" CssClass="table table-condensed table-striped table-hover table-bordered pull-left"
                                    AllowFilteringByColumn="True" AllowSorting="True" PageSize="5000"
                                    AllowPaging="True" runat="server" GridLines="None" EnableLinqExpressions="false">
                                    <HeaderStyle Width="150px" BackColor="#999966" ForeColor="#0000ff"></HeaderStyle>
                                    <PagerStyle Mode="NextPrevAndNumeric" />
                                    <ClientSettings>
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true"></Scrolling>
                                    </ClientSettings>
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView AutoGenerateColumns="false" DataKeyNames="AssignBoxID" EditMode="InPlace" AllowFilteringByColumn="True"
                                        ShowFooter="True" TableLayout="Auto">
                                        <Columns>
                                  <%--          <telerik:GridDateTimeColumn FilterControlWidth="120px" DataField="EntryDate" HeaderText="Entry Date"
                                                SortExpression="EntryDate" UniqueName="EntryDate" PickerType="DatePicker"
                                                DataFormatString="{0:d-MMM-yyyy}" EnableRangeFiltering="true">
                                                <HeaderStyle Width="340px" />
                                            </telerik:GridDateTimeColumn>--%>
<%--                                            <telerik:GridBoundColumn DataField="MId" HeaderText="MId" SortExpression="MId"
                                                UniqueName="MId" FilterControlWidth="105px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains"
                                                FilterDelay="4000" ShowFilterIcon="True">
                                            </telerik:GridBoundColumn>--%>
                                            <telerik:GridBoundColumn FilterControlWidth="105px" DataField="BoxBarCode" HeaderText="Master Cartoon  Barcode No"
                                                SortExpression="BoxBarCode" UniqueName="BoxBarCode" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="True">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn FilterControlWidth="105px" DataField="ProductModelBarcode" HeaderText="Product Barcode No"
                                                SortExpression="ProductModelBarcode" UniqueName="ProductModelBarcode" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="True">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn FilterControlWidth="105px" DataField="ProductName" HeaderText="Product Name"
                                                SortExpression="ProductName" UniqueName="ProductName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="True">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn FilterControlWidth="105px" DataField="ModelCode" HeaderText="Model  Code No"
                                                SortExpression="ModelCode" UniqueName="ModelCode" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="True">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn FilterControlWidth="105px" DataField="PrModelName" HeaderText="Product Name"
                                                SortExpression="PrModelName" UniqueName="PrModelName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="True">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridDateTimeColumn FilterControlWidth="120px" DataField="MfDate" HeaderText="Manufacturing Date"
                                                SortExpression="MfDate" UniqueName="MfDate" PickerType="DatePicker"
                                                DataFormatString="{0:d-MMM-yyyy}" EnableRangeFiltering="true">
                                                <HeaderStyle Width="340px" />
                                            </telerik:GridDateTimeColumn>
                                            <telerik:GridBoundColumn FilterControlWidth="105px" DataField="PackedBy" HeaderText="Packed By"
                                                SortExpression="PackedBy" UniqueName="PackedBy" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="True">
                                            </telerik:GridBoundColumn>
                                             <telerik:GridBoundColumn FilterControlWidth="105px" DataField="DepotName" HeaderText="Depo Name"
                                                SortExpression="DepotName" UniqueName="DepotName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="True">
                                            </telerik:GridBoundColumn>
                                             <telerik:GridBoundColumn FilterControlWidth="105px" DataField="EntryDate" HeaderText="Scanning Date"
                                                SortExpression="EntryDate" UniqueName="EntryDate" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="True">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </asp:Panel>
</asp:Content>
