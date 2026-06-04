<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="RptRmilProduction.aspx.cs" Inherits="RMIL.Prod.Production.RptRmilProduction" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript">
         window.onload = function () {
             document.getElementById('menuProdMgt').className = 'active';
         };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="row">
          <CR:CrystalReportViewer ID="CrystalReportViewer1" Width="100%" runat="server" AutoDataBind="true" />
    </div>
</asp:Content>
