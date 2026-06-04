<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="RmilQc.aspx.cs" Inherits="RMIL.Prod.QC.RmilQc" %>

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
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        inputList[i].checked = true;
                    }
                    else {
                        if (row.rowIndex % 2 == 0) {
                            row.style.backgroundColor = "#C2D69B";
                        }
                        else {
                            row.style.backgroundColor = "#C2D69B";
                        }
                        inputList[i].checked = false;
                    }
                }
            }
        }
    </script>
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
                                <div class="pull-left"><strong>QC Test Info</strong></div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="panel-body" style="background-color: #99ccff">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-lg-4">Barcode</label>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="txtBarCode" runat="server" class="form-control" OnTextChanged="txtBarCode_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="txtBarCode" ValidationGroup="vgSubmit" runat="server"
                                                        ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-lg-4">QC Steps</label>
                                                <div class="col-lg-8">
                                                    <asp:DropDownList ID="ddlQcSteps" runat="server" class="form-control" OnSelectedIndexChanged="ddlQcSteps_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <asp:Panel ID="pnlRightSide" runat="server" Visible="False">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label class="control-label col-lg-4">Product Group</label>
                                                    <div class="col-lg-8">
                                                        <asp:DropDownList ID="ddlProductGroup" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label class="control-label col-lg-4">Product</label>
                                                    <div class="col-lg-8">
                                                        <asp:DropDownList ID="ddlProduct" runat="server" class="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label class="control-label col-lg-4">Product Model</label>
                                                    <div class="col-lg-8">
                                                        <asp:DropDownList ID="ddlModel" runat="server" class="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label class="control-label col-lg-4">Model Code</label>
                                                    <div class="col-lg-8">
                                                        <asp:TextBox ID="txtModelCode" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label class="control-label col-lg-4">Current Position</label>
                                                    <div class="col-lg-8">
                                                        <asp:TextBox ID="txtStatus" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label class="control-label col-lg-4">Line</label>
                                                    <div class="col-lg-8">
                                                        <asp:DropDownList ID="ddlLine" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
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
                                                        <asp:Button ID="btnQcSave1" runat="server" Text="Save" ValidationGroup="vgSubmit"
                                                            OnClick="btnQcSave1_Click" class="btn btn-primary pull-right" Visible="False" />
                                                        <asp:Button ID="btnQcUpdate1" runat="server" Text="Update" ValidationGroup="vgSubmit"
                                                            OnClick="btnQcUpdate1_Click" class="btn btn-danger pull-right" Visible="False" />
                                                        <%--<asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="vgSubmit"
                                                            OnClick="btnSave_Click" class="btn btn-primary pull-right" />
                                                        <asp:Button ID="btnUpdate" runat="server" Text="Update" ValidationGroup="vgSubmit"
                                                            OnClick="btnUpdate_Click" class="btn btn-primary pull-right" Visible="false" />--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            <%-- <section class="wrapper">
                <!-- Row Start -->
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="widget">
                            <div class="widget-header">
                                <div class="title">
                                   QC Test Info
                                </div>
                            </div>
                            <div class="widget-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-lg-4">Barcode</label>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="txtBarCode" runat="server" BackColor="#66ffff" class="form-control" OnTextChanged="txtBarCode_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="txtBarCode" ValidationGroup="vgSubmit" runat="server"
                                                        ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-lg-4">QC Steps</label>
                                                <div class="col-lg-8">
                                                    <asp:DropDownList ID="ddlQcSteps" BackColor="#66ffff" runat="server" class="form-control" OnSelectedIndexChanged="ddlQcSteps_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <asp:Panel ID="pnlRightSide" runat="server" Visible="False">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label class="control-label col-lg-4">Product Group</label>
                                                    <div class="col-lg-8">
                                                        <asp:DropDownList ID="ddlProductGroup" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label class="control-label col-lg-4">Product</label>
                                                    <div class="col-lg-8">
                                                       <asp:DropDownList ID="ddlProduct" runat="server" class="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label class="control-label col-lg-4">Model</label>
                                                    <div class="col-lg-8">
                                                        <asp:DropDownList ID="ddlModel" runat="server" class="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label class="control-label col-lg-4">Model Code</label>
                                                    <div class="col-lg-8">
                                                        <asp:TextBox ID="txtModelCode" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label class="control-label col-lg-4">Current Status</label>
                                                    <div class="col-lg-8">
                                                        <asp:TextBox ID="txtStatus" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label class="control-label col-lg-4">Line</label>
                                                    <div class="col-lg-8">
                                                        <asp:DropDownList ID="ddlLine" BackColor="#66ffff" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
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
                                                         <asp:Button ID="btnQcSave1" runat="server" Text="Save" ValidationGroup="vgSubmit"
                                                            OnClick="btnQcSave1_Click" class="btn btn-primary pull-right" Visible="False" />
                                                        <asp:Button ID="btnQcUpdate1" runat="server" Text="Update" ValidationGroup="vgSubmit"
                                                            OnClick="btnQcUpdate1_Click" class="btn btn-danger pull-right" Visible="False" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Row End -->
            </section>--%>
            <section class="wrapper">
                <asp:Panel ID="pnlQcTest" runat="server" Visible="False">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-default">
                                <div class="panel-heading" style="background-color: #82e0aa">
                                    <div class="pull-left"><strong>QC Test Info List</strong></div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="panel-body" style="background-color: #99ccff">
                                    <div>
                                        <asp:Button ID="CheckAll" runat="server" Text="Check All" OnClick="CheckAll_Click" />
                                        <asp:Button ID="UncheckAll" runat="server" Text="Uncheck All" OnClick="UncheckAll_Click" />
                                    </div>
                                    <div class="col-md-12">
                                        <asp:GridView ID="gvQcTest" CssClass="table table-striped dt-responsive display"
                                            AutoGenerateColumns="False" Style="font-family: calibri !important" DataKeyNames="PaId" runat="server">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        QC
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkQcPoints" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                    <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="ID" DataField="PtsId">
                                                    <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                    <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Points Name" DataField="PointsName">
                                                    <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                    <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="QC Info">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtQcInfo" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                    <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        Problem
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkQcProblem" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                    <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle BackColor="#3D5D85" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                            <PagerStyle HorizontalAlign="Right" CssClass="GridPager" />
                                            <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div>
                                <asp:Button ID="btnQcSave" runat="server" Text="Save" ValidationGroup="vgSubmit"
                                    OnClick="btnQcSave_Click" class="btn btn-primary pull-left" Visible="False" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </section>
            <%--<section class="wrapper">
                <!-- QC Test Grid View Row Start -->
                <asp:Panel ID="pnlQcTest" runat="server" Visible="False">
                    <div class="row">
                        <div class="col-lg-12 col-md-12">
                            <div class="widget">
                                <div class="widget-header">
                                    <div class="title">
                                        QC Test Info List
                                    </div>
                                </div>
                                <div class="widget-body">
                                    <div>
                                        <asp:Button ID="CheckAll" runat="server" Text="Check All" OnClick="CheckAll_Click" />
                                        <asp:Button ID="UncheckAll" runat="server" Text="Uncheck All" OnClick="UncheckAll_Click" />
                                    </div>
                                    <div id="dt_example" class="example_alt_pagination">
                                        <asp:GridView ID="gvQcTest" CssClass="table table-condensed table-striped table-hover table-bordered pull-left" BackColor="#C2D69B"
                                            AutoGenerateColumns="False" Style="font-family: calibri !important"
                                            DataKeyNames="PaId" runat="server">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        QC
                                                       
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkQcPoints" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                    <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="ID" DataField="PtsId">
                                                    <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                    <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Points Name" DataField="PointsName">
                                                    <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                    <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="QC Info">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtQcInfo" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                    <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        Problem
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkQcProblem" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                    <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle BackColor="#3D5D85" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                            <PagerStyle HorizontalAlign="Right" CssClass="GridPager" />
                                            <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                                        </asp:GridView>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div>
                                <asp:Button ID="btnQcSave" runat="server" Text="Save" ValidationGroup="vgSubmit"
                                    OnClick="btnQcSave_Click" class="btn btn-primary pull-left" Visible="False" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <!-- QC Test Grid View Row End -->
            </section>--%>
            <section class="wrapper">
                <asp:Panel ID="pnlQcTestUpdate" runat="server" Visible="False">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-default">
                                <div class="panel-heading" style="background-color: #82e0aa">
                                    <div class="pull-left"><strong>QC Test update Info List</strong></div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="panel-body" style="background-color: #99ccff">
                                    <asp:Button ID="CheckAllUp" runat="server" Text="Check All" OnClick="CheckAllUp_Click" />
                                    <asp:Button ID="UncheckAllUp" runat="server" Text="Uncheck All" OnClick="UncheckAllUp_Click" />
                                    <div class="col-md-12">
                                        <asp:GridView ID="gvQcTestUpdate" CssClass="table table-striped dt-responsive display"
                                            AutoGenerateColumns="False" Style="font-family: calibri !important"
                                            DataKeyNames="DId" runat="server">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        QC
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkQcPointsUpdate" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                    <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="MID" DataField="MId">
                                                    <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                    <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="ID" DataField="PtsId">
                                                    <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                    <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Points Name" DataField="PointsName">
                                                    <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                    <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Existing Problem" DataField="QcDInfo">
                                                    <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                    <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="QC Info">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtQcInfo" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                    <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        Problem
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkQcProblemUp" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                    <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle BackColor="#3D5D85" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                            <PagerStyle HorizontalAlign="Right" CssClass="GridPager" />
                                            <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <asp:Button ID="btnQcUpdate" runat="server" Text="Update" ValidationGroup="vgSubmit"
                                OnClick="btnQcUpdate_Click" class="btn btn-danger pull-left" Visible="False" />
                        </div>
                    </div>
                </asp:Panel>
            </section>
            <%-- <section class="wrapper">
                <!-- QC Test Update Grid View Row Start -->
                <asp:Panel ID="pnlQcTestUpdate" runat="server" Visible="False">
                    <div class="row">
                        <div class="col-lg-12 col-md-12">
                            <div class="widget">
                                <div class="widget-header">
                                    <div class="title">
                                        QC Test update Info List
                                    </div>
                                </div>
                                <div class="widget-body">
                                    <div>
                                        <asp:Button ID="CheckAllUp" runat="server" Text="Check All" OnClick="CheckAllUp_Click" />
                                        <asp:Button ID="UncheckAllUp" runat="server" Text="Uncheck All" OnClick="UncheckAllUp_Click" />
                                    </div>
                                    <div id="dt_example1" class="example_alt_pagination">
                                        <asp:GridView ID="gvQcTestUpdate" CssClass="table table-condensed table-striped table-hover table-bordered pull-left" BackColor="#C2D69B"
                                            AutoGenerateColumns="False" Style="font-family: calibri !important"
                                            DataKeyNames="DId" runat="server">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        QC
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkQcPointsUpdate" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                    <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="MID" DataField="MId">
                                                    <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                    <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="ID" DataField="PtsId">
                                                    <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                    <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Points Name" DataField="PointsName">
                                                    <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                    <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Existing Problem" DataField="QcDInfo">
                                                    <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                    <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="QC Info">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtQcInfo" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                    <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        Problem
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkQcProblemUp" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                                    <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle BackColor="#3D5D85" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                            <PagerStyle HorizontalAlign="Right" CssClass="GridPager" />
                                            <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                                        </asp:GridView>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div>
                                <asp:Button ID="btnQcUpdate" runat="server" Text="Update" ValidationGroup="vgSubmit"
                                    OnClick="btnQcUpdate_Click" class="btn btn-danger pull-left" Visible="False" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <!-- QC Test Update Grid View Row End -->
            </section>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
