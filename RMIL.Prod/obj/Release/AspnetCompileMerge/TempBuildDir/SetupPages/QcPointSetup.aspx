<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="QcPointSetup.aspx.cs" Inherits="RMIL.Prod.SetupPages.QcPointSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/gridPaging.css" rel="stylesheet" />
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
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- Row Start -->
            <section class="wrapper">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-default">
                            <div class="panel-heading" style="background-color: #82e0aa">
                                <div class="pull-left"><strong>QC Points Setup</strong></div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="panel-body" style="background-color: #99ccff">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-lg-4">Points Name</label>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="txtPointsName" placeholder="Enter Steps Name" runat="server" class="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="txtPointsName" ValidationGroup="vgSubmit" runat="server"
                                                        ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-lg-4">Active</label>
                                                <div class="col-lg-8">
                                                    <asp:CheckBox ID="chkActive" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-lg-4"></label>
                                                <div class="col-lg-8">
                                                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="vgSubmit"
                                                        OnClick="btnSave_Click" class="btn btn-primary pull-left" />
                                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" ValidationGroup="vgSubmit"
                                                        OnClick="btnUpdate_Click" class="btn btn-primary pull-left" Visible="false" />
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
            <!-- QC Points Grid View Row Start -->
            <section class="wrapper">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-default">
                            <div class="panel-heading" style="background-color: #82e0aa">
                                <div class="pull-left"><strong>QC Points List</strong></div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="panel-body" style="background-color: #99ccff">
                                <div class="col-md-12">
                                    <asp:GridView ID="gvCheckingPoints" CssClass="table table-striped dt-responsive display"
                                        AllowPaging="True" PageSize="20" AutoGenerateColumns="False" Style="font-family: calibri !important"
                                        DataKeyNames="PtsId" runat="server" OnPageIndexChanging="gvCheckingPoints_PageIndexChanging">
                                        <Columns>
                                            <asp:BoundField HeaderText="ID" DataField="PtsId">
                                                <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Checking Points Name" DataField="PointsName">
                                                <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Active" DataField="Active">
                                                <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEditCheckingPoints" runat="server" ImageUrl="~/Images/edit.png" ToolTip="Edit Record"
                                                        Width="22px" Height="22px" CommandArgument='<%# Eval("PtsId")%>' OnClick="btnEditCheckingPoints_Click" CausesValidation="False" />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                            </asp:TemplateField>
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
            <!-- QC Points Grid View Row End -->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
