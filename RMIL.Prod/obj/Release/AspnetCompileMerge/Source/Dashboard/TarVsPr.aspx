<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="TarVsPr.aspx.cs" Inherits="RMIL.Prod.Dashboard.TarVsPr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link id="MyStyleSheet" type="text/css" href="../css/white.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Dashboard Wrapper Start -->
    <div class="dashboard-wrapper-lg">
        <!-- Row starts -->
        <div class="row">
            <div class="col-lg-3 col-md-3 col-sm-6">
                <div class="mini-widget">
                    <div class="mini-widget-heading clearfix">
                        <div id="aName" runat="server" class="pull-left"></div>
                        <div class="pull-right"><i id="aptg" runat="server" class="fa fa-angle-up"></i><sup>%</sup></div>
                    </div>
                    <div class="mini-widget-body clearfix">
                        <div class="pull-left">
                            <div id="at" runat="server" class="pull-right number"></div>
                        </div>
                        <div id="ap" runat="server" class="pull-right number"></div>
                    </div>
                </div>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6">
                <div class="mini-widget">
                    <div class="mini-widget-heading clearfix">
                        <div id="bName" runat="server" class="pull-left"></div>
                        <div class="pull-right"><i id="bptg" runat="server" class="fa fa-angle-up"></i><sup>%</sup></div>
                    </div>
                    <div class="mini-widget-body clearfix">
                        <div class="pull-left">
                            <div id="bt" runat="server" class="pull-right number"></div>
                        </div>
                        <div id="bp" runat="server" class="pull-right number"></div>
                    </div>
                </div>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6">
                <div class="mini-widget">
                    <div class="mini-widget-heading clearfix">
                        <div id="cName" runat="server" class="pull-left"></div>
                        <div class="pull-right"><i id="cptg" runat="server" class="fa fa-angle-up"></i><sup>%</sup></div>
                    </div>
                    <div class="mini-widget-body clearfix">
                        <div class="pull-left">
                            <div id="ct" runat="server" class="pull-right number"></div>
                        </div>
                        <div id="cp" runat="server" class="pull-right number"></div>
                    </div>
                </div>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6">
                <div class="mini-widget">
                    <div class="mini-widget-heading clearfix">
                        <div id="dName" runat="server" class="pull-left"></div>
                        <div class="pull-right"><i id="dptg" runat="server" class="fa fa-angle-up"></i><sup>%</sup></div>
                    </div>
                    <div class="mini-widget-body clearfix">
                        <div class="pull-left">
                            <div id="dt" runat="server" class="pull-right number"></div>
                        </div>
                        <div id="dp" runat="server" class="pull-right number"></div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Row ends -->
    </div>
    <!-- Dashboard Wrapper End -->
</asp:Content>
