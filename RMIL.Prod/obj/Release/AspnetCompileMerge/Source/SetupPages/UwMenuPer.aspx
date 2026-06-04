<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="UwMenuPer.aspx.cs" Inherits="RMIL.Prod.SetupPages.UwMenuPer" %>
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
    <script type="text/javascript">
        function CheckBoxListSelect(cbControl, state) {
            var chkBoxList = document.getElementById(cbControl);
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            for (var i = 0; i < chkBoxCount.length; i++) {
                chkBoxCount[i].checked = state;
            }

            return false;
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
                                <div class="pull-left"><strong>User Wise Menu Permission</strong></div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="panel-body" style="background-color: #99ccff">
                                <div class="row">
                                    <div class="col-md-5">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-lg-4">User ID</label>
                                                <div class="col-lg-8">
                                                    <asp:DropDownList ID="ddlUserId" BackColor="#FFFFFF" CssClass="select2" runat="server" class="form-control" OnSelectedIndexChanged="ddlUserId_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-lg-4">ParentMenu</label>
                                                <div class="col-lg-8">
                                                    <asp:DropDownList ID="ddlParentMenu" runat="server" class="form-control" OnSelectedIndexChanged="ddlParentMenu_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-lg-3">Active</label>
                                                <div class="col-lg-8">
                                                    <asp:CheckBox ID="chkActive" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="col-lg-8">
                                                    Select <a id="A1" href="#" onclick="javascript: CheckBoxListSelect ('<%= chkBoxMenu.ClientID %>',true);">All</a>
                                                    | <a id="A2" href="#" onclick="javascript: CheckBoxListSelect ('<%= chkBoxMenu.ClientID %>',false);">None</a>
                                                    <br />
                                                    <asp:CheckBoxList ID="chkBoxMenu" RepeatColumns="3" runat="server" Width="1200px">
                                                    </asp:CheckBoxList>
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
                                                    <asp:Button ID="btnSave" runat="server" Text="Add" ValidationGroup="vgSubmit"
                                                        OnClick="btnSave_Click" class="btn btn-primary pull-left" />
                                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" ValidationGroup="vgSubmit"
                                                        OnClick="btnUpdate_Click" class="btn btn-danger pull-right" />
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
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            <!-- Row End -->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
