<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="QcMoveSetup.aspx.cs" Inherits="RMIL.Prod.SetupPages.QcMoveSetup" %>

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
                                <div class="pull-left"><strong>QC Move</strong></div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="panel-body" style="background-color: #99ccff">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-lg-4">Product Group</label>
                                                <div class="col-lg-8">
                                                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-lg-4">From QC</label>
                                                <div class="col-lg-8">
                                                    <asp:DropDownList ID="ddlFromQc" runat="server" class="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-lg-4">To QC</label>
                                                <div class="col-lg-8">
                                                    <asp:DropDownList ID="ddlToQc" runat="server" class="form-control"></asp:DropDownList>
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
                                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" ValidationGroup="vgSubmit"
                                                        OnClick="btnUpdate_Click" class="btn btn-primary pull-right" Visible="false" />
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
            <!-- QcMove grid Row Start -->
            <section class="wrapper">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-default">
                            <div class="panel-heading" style="background-color: #82e0aa">
                                <div class="pull-left"><strong>QC Move List</strong></div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="panel-body" style="background-color: #99ccff">
                                <div class="col-md-12">
                                    <asp:GridView ID="gvQcMove" CssClass="table table-striped dt-responsive display"
                                        AllowPaging="True" PageSize="20" AutoGenerateColumns="False" Style="font-family: calibri !important"
                                        DataKeyNames="MvId" runat="server" OnPageIndexChanging="gvQcMove_PageIndexChanging">
                                        <Columns>
                                            <asp:BoundField HeaderText="ID" DataField="MvId">
                                                <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="From QC" DataField="FromQc">
                                                <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="To QC" DataField="ToQc">
                                                <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Group Name" DataField="CategoryName">
                                                <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnDeleteQcMove" runat="server" ImageUrl="~/Images/delete.png" ToolTip="Delete Record"
                                                        Width="22px" Height="22px" CommandArgument='<%# Eval("MvId")%>' OnClientClick="return confirm('Are you sure to Delete?')"
                                                        OnClick="btnDeleteQcMove_Click" CausesValidation="False" />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEditQcMove" runat="server" ImageUrl="~/Images/edit.png" ToolTip="Edit Record"
                                                        Width="22px" Height="22px" CommandArgument='<%# Eval("MvId")%>' OnClick="btnEditQcMove_Click" CausesValidation="False" />
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
            <!-- QcMove grid Row End -->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
