<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="BarcodeSearch.aspx.cs" Inherits="RMIL.Prod.Production.BarcodeSearch" %>
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
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-6">
                    <asp:TextBox ID="txtBarcode" placeholder="Enter BarCode" runat="server" class="form-control"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text="Search" class="btn btn-primary pull-left" />
                </div>
            </div>
            <!-- Product Model grid Row Start -->
            <section class="wrapper">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-default">
                            <div class="panel-heading" style="background-color: #82e0aa">
                                <div class="pull-left"><strong>Product Model Info</strong></div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="panel-body" style="background-color: #99ccff">
                                <div class="col-md-12">
                                    <asp:GridView ID="gvModel" CssClass="table table-striped dt-responsive display"
                                        AllowPaging="True" PageSize="20" AutoGenerateColumns="False" Style="font-family: calibri !important"
                                        DataKeyNames="RmPdId" runat="server">
                                        <Columns>
                                            <asp:BoundField HeaderText="ID" DataField="RmPdId" Visible="False">
                                                <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                            </asp:BoundField>
                                             <asp:BoundField HeaderText="BarCode No" DataField="DBarcode">
                                                <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Product" DataField="ProductName">
                                                <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Pr Model" DataField="PrModelName">
                                                <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Model Code" DataField="ModelCode">
                                                <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                            </asp:BoundField>
                                             <asp:BoundField HeaderText="Production Date" DataField="MfDate" DataFormatString="{0:d-MMM-yyyy}">
                                                <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                            </asp:BoundField>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" CssClass="GridPager" />
                                        <HeaderStyle BackColor="#3D5D85" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                        <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            <!-- Product Model grid Row End -->
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="left-sidebar">
                <div class="row">
                    <div class="col-md-4">
                        <asp:TextBox ID="txtBarcode" placeholder="Enter Barcode No" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text="Search" class="btn btn-primary pull-left" />
                    </div>
                </div>
                <!-- Product Grid View Row Start -->
                <div class="row">
                    <div class="col-lg-12 col-md-12">
                        <div class="widget">
                            <div class="widget-header">
                                <div class="title">
                                    Product Model Info
                                </div>
                            </div>
                            <div class="widget-body">
                                <div id="dt_example" class="example_alt_pagination">
                                    <asp:GridView ID="gvModel" CssClass="table table-condensed table-striped table-hover table-bordered pull-left"
                                        AutoGenerateColumns="False" Style="font-family: calibri !important"
                                        DataKeyNames="RmPdId" runat="server">
                                        <Columns>
                                            <asp:BoundField HeaderText="ID" DataField="RmPdId" Visible="False">
                                                <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                            </asp:BoundField>
                                             <asp:BoundField HeaderText="BarCode No" DataField="DBarcode">
                                                <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Product" DataField="ProductName">
                                                <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Pr Model" DataField="PrModelName">
                                                <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Model Code" DataField="ModelCode">
                                                <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                            </asp:BoundField>
                                             <asp:BoundField HeaderText="Production Date" DataField="MfDate" DataFormatString="{0:d-MMM-yyyy}">
                                                <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                            </asp:BoundField>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" CssClass="GridPager" />
                                        <EmptyDataTemplate>No Pump Available</EmptyDataTemplate>
                                    </asp:GridView>
                                    <div class="clearfix">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Product Grid View Row End -->
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
