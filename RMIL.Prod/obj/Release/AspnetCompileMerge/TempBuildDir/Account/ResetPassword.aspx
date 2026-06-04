<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="RMIL.Prod.Account.ResetPassword" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- input form data start -->
    <section class="wrapper">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading" style="background-color: #82e0aa">
                        <div class="pull-left"><strong>Reset Password</strong></div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="panel-body" style="background-color: #99ccff">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-lg-4">User ID</label>
                                        <div class="col-lg-8">
                                            <asp:TextBox ID="txtUserId" placeholder="Enter User ID" runat="server" class="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="txtUserId" ValidationGroup="vgSubmit" runat="server"
                                                ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-lg-4">New Password</label>
                                        <div class="col-lg-8">
                                            <asp:TextBox ID="txtPassword" TextMode="Password" placeholder="Enter New Password" runat="server" class="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" ControlToValidate="txtPassword" ValidationGroup="vgSubmit" runat="server"
                                                ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-lg-4"></label>
                                        <div class="col-lg-8">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-lg-4"></label>
                                        <div class="col-lg-8">
                                            <asp:Button ID="btnReset" runat="server" Text="Reset" ValidationGroup="vgSubmit"
                                                OnClick="btnReset_Click" class="btn btn-primary pull-right" />

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
    <!-- input form data end -->
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- Left Sidebar Start -->
            <div class="left-sidebar">
                <!-- Row Start -->
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="widget">
                            <div class="widget-header">
                                <div class="title">
                                    Reset Password
                                </div>
                            </div>
                            <div class="widget-body">
                                <div class="row">
                                    <div class="col-md-5">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-lg-4">User ID</label>
                                                <div class="col-lg-8">
                                                   <asp:TextBox ID="txtUserId" placeholder="Enter User ID" runat="server" class="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="txtUserId" ValidationGroup="vgSubmit" runat="server"
                                                        ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-lg-4">New Password</label>
                                                <div class="col-lg-8">
                                                   <asp:TextBox ID="txtPassword" TextMode="Password" placeholder="Enter New Password" runat="server" class="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" ControlToValidate="txtPassword" ValidationGroup="vgSubmit" runat="server"
                                                        ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="col-lg-8">
                                                    <asp:Button ID="btnReset" runat="server" Text="Reset" ValidationGroup="vgSubmit"
                                                      OnClick="btnReset_Click"   class="btn btn-primary pull-left" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Row End -->
            </div>
            <!-- Left Sidebar End -->
        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
