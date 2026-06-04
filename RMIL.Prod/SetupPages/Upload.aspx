<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="RMIL.Prod.SetupPages.Upload" %>

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
    <section class="wrapper">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading" style="background-color: #82e0aa">
                        <div class="pull-left"><strong>Document Upload Setup</strong></div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="panel-body" style="background-color: #99ccff">
                        <div class="col-md-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-lg-4">Document Upload</label>
                                    <div class="col-lg-8">
                                        <asp:FileUpload ID="FileUpload1" runat="server" required />
                                        <br />
                                        <asp:Button ID="btnUpload" runat="server" class="btn btn-danger pull-left" Text="Upload" OnClick="UploadFile" ValidationGroup="vgSubmit" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!-- Document grid Row Start -->
    <section class="wrapper">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading" style="background-color: #82e0aa">
                        <div class="pull-left"><strong>Document Upload List</strong></div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="panel-body" style="background-color: #99ccff">
                        <div class="col-md-12">
                            <asp:GridView ID="gvUpload" CssClass="table table-striped dt-responsive display"
                                AllowPaging="True" PageSize="20" AutoGenerateColumns="False" Style="font-family: calibri !important"
                                EmptyDataText="No files uploaded" runat="server" OnPageIndexChanging="gvUpload_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField HeaderText="File Name" DataField="Text">
                                        <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                        <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Download File">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDownload" Text="Download" CommandArgument='<%# Eval("Value") %>' runat="server" OnClick="DownloadFile"></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                        <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete File" Visible="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" Text="Delete" CommandArgument='<%# Eval("Value") %>' runat="server" OnClick="DeleteFile" />
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
    <!-- Document grid Row End -->
</asp:Content>
