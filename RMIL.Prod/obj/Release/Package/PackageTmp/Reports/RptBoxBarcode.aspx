<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="RptBoxBarcode.aspx.cs" Inherits="RMIL.Prod.Reports.RptBoxBarcode" %>

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
        <div>
            <asp:ImageButton ID="ImageButton1" class="btn btn-info pull-right" runat="server" ImageUrl="~/Images/excel.png"
                OnClick="ImageButton_Click" ToolTip="Export to excel" AlternateText="ExcelML" />
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading" style="background-color: #82e0aa">
                        <div class="pull-left"><strong>Product Barcode</strong></div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="panel-body" style="background-color: #99ccff">
                        <div class="col-md-12">
                            <telerik:RadGrid AutoGenerateColumns="false" ID="gvBarcode" CssClass="table table-condensed table-striped table-hover table-bordered pull-left"
                                AllowFilteringByColumn="True" AllowSorting="True" PageSize="5000"
                                AllowPaging="True" runat="server" GridLines="None" EnableLinqExpressions="false">
                                <HeaderStyle Width="150px" BackColor="#999966" ForeColor="#0000ff"></HeaderStyle>
                                <PagerStyle Mode="NextPrevAndNumeric" />
                                <ClientSettings>
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true"></Scrolling>
                                </ClientSettings>
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView AutoGenerateColumns="false" DataKeyNames="RmPdId" EditMode="InPlace" AllowFilteringByColumn="True"
                                    ShowFooter="True" TableLayout="Auto">
                                    <Columns>
                                        <telerik:GridDateTimeColumn FilterControlWidth="105px" DataField="EntryDate" HeaderText="Entry Date"
                                            SortExpression="EntryDate" UniqueName="EntryDate" PickerType="DatePicker"
                                            DataFormatString="{0:d-MMM-yyyy}" EnableRangeFiltering="true">
                                            <HeaderStyle Width="340px" />
                                        </telerik:GridDateTimeColumn>
                                        <telerik:GridBoundColumn DataField="RmPdId" HeaderText="Box ID" SortExpression="RmPdId"
                                            UniqueName="RmPdId" FilterControlWidth="105px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains"
                                            FilterDelay="4000" ShowFilterIcon="True">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn FilterControlWidth="105px" DataField="DBarcode" HeaderText="Box Barcode No"
                                            SortExpression="DBarcode" UniqueName="DBarcode" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                            ShowFilterIcon="True">
                                        </telerik:GridBoundColumn>

                                       <telerik:GridBoundColumn FilterControlWidth="105px" DataField="ModelCode" HeaderText="Model Code"
                                            SortExpression="ModelCode" UniqueName="ModelCode" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                            ShowFilterIcon="True">
                                        </telerik:GridBoundColumn>
                                         <telerik:GridBoundColumn FilterControlWidth="105px" DataField="PrModelName" HeaderText="Model Name"
                                            SortExpression="PrModelName" UniqueName="PrModelName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                            ShowFilterIcon="True">
                                        </telerik:GridBoundColumn>
                                         <telerik:GridBoundColumn FilterControlWidth="105px" DataField="ProductName" HeaderText="Product Name"
                                            SortExpression="ProductName" UniqueName="ProductName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
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
</asp:Content>
