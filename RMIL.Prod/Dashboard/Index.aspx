<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="RMIL.Prod.Dashboard.Index" %>

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
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- Row Start -->
            <section class="wrapper">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-default">
                            <div class="panel-heading" style="background-color: #82e0aa">
                                <div class="pull-left"><strong></strong></div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="panel-body" style="background-color: #99ccff">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-lg-4">Target Date</label>
                                                <div class="col-lg-8">
                                                    <telerik:RadDatePicker ID="dtpTarget" TabIndex="11" runat="server">
                                                        <DateInput DateFormat="dd-MM-yyyy"
                                                            DisplayDateFormat="dd-MM-yyyy" runat="server" TabIndex="11">
                                                        </DateInput>
                                                        <DatePopupButton HoverImageUrl="" ImageUrl="" TabIndex="11" />
                                                    </telerik:RadDatePicker>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="dtpTarget" ValidationGroup="vgSubmit" runat="server"
                                                        ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
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
            <!-- Row End -->
            <!-- Dashboard grid Row Start -->
            <section class="wrapper">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-default">
                            <div class="panel-heading" style="background-color: #82e0aa">
                                <div class="pull-left"><strong>Daily Production Summary</strong></div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="panel-body" style="background-color: #99ccff">
                                <div class="col-md-12">
                                    <asp:GridView ID="gvDashboard" CssClass="table table-striped dt-responsive display"
                                        AutoGenerateColumns="False" Style="font-family: calibri !important" runat="server">
                                        <Columns>
                                            <asp:BoundField HeaderText="Target Qty" DataField="TargetQty" ItemStyle-Font-Bold="true">
                                                <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Success Qty" DataField="SuccessQty" ItemStyle-Font-Bold="true">
                                                <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Succ Ptg" DataField="SuccPtg" DataFormatString="{0:0}%" ItemStyle-Font-Bold="true">
                                                <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Re-Success Qty" DataField="ReSuccessQty" ItemStyle-Font-Bold="true">
                                                <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Re-Succ Ptg" DataField="ReSuccPtg" DataFormatString="{0:0}%" ItemStyle-Font-Bold="true">
                                                <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Prob Qty" DataField="ProbQty" ItemStyle-Font-Bold="true">
                                                <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Prob Ptg" DataField="ProbPtg" DataFormatString="{0:0}%" ItemStyle-Font-Bold="true">
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
            <!-- Dashboard grid Row End -->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
