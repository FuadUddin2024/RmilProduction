<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="ChangePasswordByUser.aspx.cs" Inherits="RMIL.Prod.Account.ChangePasswordByUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- input form data start -->
    <section class="wrapper">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading" style="background-color: #82e0aa">
                        <div class="pull-left"><strong>Change Password</strong></div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="panel-body" style="background-color: #99ccff">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-lg-4">Old Password</label>
                                        <div class="col-lg-8">
                                            <asp:TextBox ID="txtOldPass" placeholder="Enter Old Password" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="txtOldPass" ValidationGroup="vgSubmit" runat="server"
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
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-lg-4">New Password</label>
                                        <div class="col-lg-8">
                                            <asp:TextBox ID="txtNewPass" placeholder="Enter New Password" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" ControlToValidate="txtNewPass" ValidationGroup="vgSubmit" runat="server"
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
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-lg-4">Confirm Password</label>
                                        <div class="col-lg-8">
                                            <asp:TextBox ID="txtConfirm" placeholder="Enter Confirm Password" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" ControlToValidate="txtConfirm" ValidationGroup="vgSubmit" runat="server"
                                                ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="vgSubmit"
                                                ForeColor="Red" ErrorMessage="Password doesn't match" ControlToCompare="txtNewPass"
                                                ControlToValidate="txtConfirm"></asp:CompareValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-lg-4"></label>
                                        <div class="col-lg-8">
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
                                            <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="vgSubmit"
                                                OnClick="btnSave_Click" class="btn btn-primary pull-right" />
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
</asp:Content>
