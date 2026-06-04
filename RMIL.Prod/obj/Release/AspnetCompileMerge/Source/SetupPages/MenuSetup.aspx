<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="MenuSetup.aspx.cs" Inherits="RMIL.Prod.SetupPages.MenuSetup" %>
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
            <!-- Menu start -->
            <section class="wrapper">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-default">
                            <div class="panel-heading" style="background-color: #82e0aa">
                                <div class="pull-left"><strong>Menu Setup</strong></div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="panel-body" style="background-color: #99ccff">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-lg-4">Menu Title</label>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="txtMenuTitle" placeholder="Enter Menu Title" runat="server" class="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="txtMenuTitle" ValidationGroup="vgSubmit" runat="server"
                                                        ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-lg-4">Parent Menu</label>
                                                <div class="col-lg-8">
                                                    <asp:DropDownList ID="ddlParentMenu" runat="server" class="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-lg-4">URL</label>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="txtUrl" placeholder="Enter Page URL" runat="server" class="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" ControlToValidate="txtUrl" ValidationGroup="vgSubmit" runat="server"
                                                        ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-lg-4">Description</label>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="txtDescription" placeholder="Enter Menu Description" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-lg-4">Active</label>
                                                <div class="col-lg-8">
                                                    <asp:CheckBox ID="chkActive" runat="server" />
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
                                                        OnClick="btnUpdate_Click" class="btn btn-danger pull-right" Visible="false" />
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
            <!-- Menu end -->
            <!-- Menu grid Start -->
            <section class="wrapper">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-default">
                            <div class="panel-heading" style="background-color: #82e0aa">
                                <div class="pull-left"><strong>Menu List</strong></div>
                                <div class="clearfix"></div>
                            </div>
                        </div>
                        <div class="panel-body" style="background-color: #99ccff">
                            <div class="col-md-12">
                                <asp:GridView ID="gvMenu" CssClass="table table-striped dt-responsive display"
                                    AllowPaging="True" PageSize="20" AutoGenerateColumns="False" Style="font-family: calibri !important"
                                    DataKeyNames="MenuId" runat="server" OnPageIndexChanging="gvMenu_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField HeaderText="Menu Id" DataField="MenuId">
                                            <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                            <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Menu Title" DataField="Title">
                                            <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                            <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Menu URL" DataField="Url" Visible="False">
                                            <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                            <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Parent Menu" DataField="ParentMenuName">
                                            <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                            <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Active" DataField="Active">
                                            <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                            <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Description" DataField="Description">
                                            <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                            <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Delete" Visible="False">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnDeleteMenu" runat="server" ImageUrl="~/Images/delete.png" ToolTip="Delete Record"
                                                    Width="22px" Height="22px" CommandArgument='<%# Eval("MenuId")%>' OnClientClick="return confirm('Are you sure to Delete?')"
                                                    OnClick="btnDeleteMenu_Click" CausesValidation="False" />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                            <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEditMenu" runat="server" ImageUrl="~/Images/edit.png" ToolTip="Edit Record"
                                                    Width="22px" Height="22px" CommandArgument='<%# Eval("MenuId")%>' OnClick="btnEditMenu_Click" CausesValidation="False" />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                            <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" CssClass="GridPager" />
                                    <HeaderStyle BackColor="#663300" ForeColor="#ffffff" />
                                    <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            <!-- Menu grid End -->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
