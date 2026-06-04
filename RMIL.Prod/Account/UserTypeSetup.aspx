<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="UserTypeSetup.aspx.cs" Inherits="RMIL.Prod.Account.UserTypeSetup" %>

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
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <section class="wrapper">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-default">
                            <div class="panel-heading" style="background-color: #82e0aa">
                                <div class="pull-left"><strong>User Role Info</strong></div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="panel-body" style="background-color: #99ccff">
                                <div class="col-md-6">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label class="control-label col-lg-4">Role Name</label>
                                            <div class="col-lg-8">
                                                <asp:TextBox ID="txtTypeName" runat="server" class="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="txtTypeName" ValidationGroup="vgSubmit" runat="server"
                                                    ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-lg-offset-2 col-lg-9">
                                                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="vgSubmit"
                                                    OnClick="btnSave_Click" class="btn btn-primary pull-left" />
                                                <asp:Button ID="btnUpdate" runat="server" Text="Update" ValidationGroup="vgSubmit"
                                                    OnClick="btnUpdate_Click" class="btn btn-danger pull-left" Visible="false" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            <!-- UserRole grid Start -->
            <div class="col-lg-12">
                <section class="box ">
                    <header class="panel_header">
                        <h2 class="title pull-left">User Role Info List</h2>
                        <div class="actions panel_actions pull-right">
                            <a class="box_toggle fa fa-chevron-down"></a>
                            <a class="box_setting fa fa-cog" data-toggle="modal" href="#section-settings"></a>
                            <a class="box_close fa fa-times"></a>
                        </div>
                    </header>
                    <div class="content-body">
                        <div class="row">
                            <div class="col-xs-12">
                                <asp:GridView ID="gvUserType" CssClass="table table-striped dt-responsive display"
                                    AllowPaging="True" PageSize="20" AutoGenerateColumns="False" Style="font-family: calibri !important"
                                    DataKeyNames="TypeId" runat="server" OnPageIndexChanging="gvUserType_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField HeaderText="Role Id" DataField="TypeId">
                                            <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                            <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Role Name" DataField="TypeName">
                                            <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                            <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Delete" Visible="False">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnDeleteUserType" runat="server" ImageUrl="~/Images/delete.png" ToolTip="Delete Record"
                                                    Width="22px" Height="22px" CommandArgument='<%# Eval("TypeId")%>' OnClientClick="return confirm('Are you sure to Delete?')"
                                                    OnClick="btnDeleteUserType_Click" CausesValidation="False" />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                            <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEditUserType" runat="server" ImageUrl="~/Images/edit.png" ToolTip="Edit Record"
                                                    Width="22px" Height="22px" CommandArgument='<%# Eval("TypeId")%>' OnClick="btnEditUserType_Click" CausesValidation="False" />
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
                </section>
            </div>
            <!-- UserRole grid End -->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
