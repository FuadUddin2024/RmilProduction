<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="IndexWeightScale.aspx.cs" Inherits="RMIL.Prod.Dashboard.IndexWeightScale" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI, Version=2013.3.1015.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4" %>
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

    window.onload = function () {

        // Load data immediately
        FetchAllTheDataDay();
        FetchAllTheDataNight();
        // Start live clock
        updateClock();
        setInterval(updateClock, 1000);

        // Auto refresh page after 10 minutes
        setTimeout(function () {
            location.reload();
        }, 600000);
    };

    function updateClock() {

        const now = new Date();

        let hours = now.getHours();
        const minutes = now.getMinutes();
        const seconds = now.getSeconds();

        const ampm = hours >= 12 ? 'PM' : 'AM';
        hours = hours % 12 || 12;

        const timeString =
            String(hours).padStart(2, '0') + ':' +
            String(minutes).padStart(2, '0') + ':' +
            String(seconds).padStart(2, '0') + ' ' + ampm;

        const dateString =
            now.getDate().toString().padStart(2, '0') + '/' +
            (now.getMonth() + 1).toString().padStart(2, '0') + '/' +
            now.getFullYear();

        const clocks = document.getElementsByClassName('liveClock');
        const dates = document.getElementsByClassName('livedate');

        for (let i = 0; i < clocks.length; i++) {
            clocks[i].innerText = timeString;
        }

        for (let i = 0; i < dates.length; i++) {
            dates[i].innerText = dateString;
        }
    }

    // Update every second
    setInterval(updateClock, 1000);
    updateClock();
    function FetchAllTheDataDay() {

        $.ajax({
            url: 'IndexWeightScale.aspx/GetALLDayShiftData',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: '{}',

            success: function (response) {

                console.log(response);

                if (response.d) {

                    var data = response.d;

                    // Total Production
                    document.getElementById("dayshift").innerText =
                        data.TotalProduction || 0;

                    // Table Data
                    let html = '';

                    if (data.ShiftData && data.ShiftData.length > 0) {

                        data.ShiftData.forEach(function (item) {

                            html += `
                                <tr>
                                    <td style="font-size: 21px;"><b>${item.PrModelName}</b></td>
                                    <td style="font-size: 21px;"> <b>${item.ModelCode}</b></td>
                                    <td style="font-size: 21px;"> <b>${item.TotalProduction}</b></td>
                                </tr>`;
                        });
                    }
                    else {

                        html = `
                            <tr>
                                <td colspan="3" style="text-align:center;">
                                    No Data Found
                                </td>
                            </tr>`;
                    }

                    document.getElementById("preassemblyQCdashboard").innerHTML = html;
                }
            },

            error: function (xhr, status, error) {

                console.log("AJAX Error");
                console.log("Status:", status);
                console.log("Error:", error);
                console.log("Response:", xhr.responseText);

                alert("Failed to load data. Check browser console (F12).");
            }
        });
    }
    function FetchAllTheDataNight() {

        $.ajax({
            url: 'IndexWeightScale.aspx/GetALLNightShiftData',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: '{}',

            success: function (response) {

                console.log(response);

                if (response.d) {

                    var data = response.d;

                    // Total Production
                    document.getElementById("nightshift").innerText =
                        data.TotalProduction || 0;

                    // Table Data
                    let html = '';

                    if (data.ShiftData && data.ShiftData.length > 0) {

                        data.ShiftData.forEach(function (item) {

                            html += `
                                <tr>
                                    <td style="font-size: 21px;"><b>${item.PrModelName}</b></td>
                                    <td style="font-size: 21px;"> <b>${item.ModelCode}</b></td>
                                    <td style="font-size: 21px;"> <b>${item.TotalProduction}</b></td>
                                </tr>`;
                        });
                    }
                    else {

                        html = `
                            <tr>
                                <td colspan="3" style="text-align:center;">
                                    No Data Found
                                </td>
                            </tr>`;
                    }

                    document.getElementById("preassemblyQCdashboardnight").innerHTML = html;
                }
            },

            error: function (xhr, status, error) {

                console.log("AJAX Error");
                console.log("Status:", status);
                console.log("Error:", error);
                console.log("Response:", xhr.responseText);

                alert("Failed to load data. Check browser console (F12).");
            }
        });
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- Row Start -->
         <%--   <section class="wrapper">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-default">
                            <div class="panel-heading" style="background-color: #82e0aa">
                                <div class="pull-left"><strong></strong></div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="panel-body" style="background-color: #99ccff">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-lg-4">Target Date</label>
                                                <div class="col-lg-8">
                                                    <telerik:RadDatePicker ID="dtpTarget" TabIndex="11" runat="server">
                                                        <DateInput DateFormat="dd-MM-yyyy"
                                                            DisplayDateFormat="dd-MM-yyyy" runat="server" TabIndex="11">
                                                        </DateInput>
                                                        <DatePopupButton HoverImageUrl="" ImageUrl="" TabIndex="11" />
                                                    </telerik:RadDatePicker>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="dtpTarget" ValidationGroup="vgSubmit" runat="server"
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
                                                    <asp:Button ID="btnShow" runat="server" Text="Show" ValidationGroup="vgSubmit"
                                                        OnClick="btnShow_Click" class="btn btn-primary pull-left" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>--%>
            <!-- Row End -->
            <!-- Dashboard grid Row Start -->
            <section class="wrapper">
                <div class="row">
                    <div class="col-md-6">
                        <div class="panel panel-default">
                            <div class="panel-heading" style="background-color: #82e0aa">
                                <div class="pull-left"><strong>Daily Day Shift Production Summary</strong></div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="panel-body" style="background-color: #99ccff">
                                <div class="col-md-12">
                                    <h4 style="text-align:center;"><b>Day Shift</b></h4>
                                    <h4 style="text-align:center;"><b>Current Time:</b> <p class="liveClock"></p></h4>
                                    <h4 style="text-align:center;"><b>Current Date:</b> <p class="livedate"></p></h4>
                                </div>
                                <div class="col-md-12">
                                   <%-- <h5><b>Last Update Time:</b></h5>--%>
                                     <h5><b>Total:</b><span id="dayshift"></span></h5>
                                </div>
                                <div class="col-md-12">
                                    <table class="table table-bordered">
                                    <thead>
                                    <tr class="gray-bg">
                                        <th style="font-size: 21px;"><b>Model Name </b></th>
                                        <th style="font-size: 21px;"><b>Model Code</b></th>
                                        <th style="font-size: 21px;"><b>Total Production</b></th>
                                    </tr>
                                    </thead>
                                    <tbody id="preassemblyQCdashboard">
                                    </tbody>
                                </table>
                                </div>
                            </div>
                        </div>
                    </div>
                             <div class="col-md-6">
                        <div class="panel panel-default">
                            <div class="panel-heading" style="background-color: #82e0aa">
                                <div class="pull-left"><strong>Daily Night Shift Production Summary</strong></div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="panel-body" style="background-color: #99ccff">
                               <div class="col-md-12">
                                    <h4 style="text-align:center"><b>Night Shift</b></h4>
                                    <h4 style="text-align:center;"><b>Current Time: </b><p class="liveClock"></p></h4>
                                    <h4 style="text-align:center;"><b>Current Date:</b> <p class="livedate"></p></h4>
                                </div>
                                <div class="col-md-12">
                                     <%-- <h5><b>Last Update Time:</b></h5>--%>
                                     <h5><b>Total:</b><span id="nightshift"></span></h5>
                                </div>
                                <div class="col-md-12">
                                          <div class="col-md-12">
                                    <table class="table table-bordered">
                                    <thead>
                                    <tr class="gray-bg">
                                        <th style="font-size: 21px;"><b>Model Name </b></th>
                                        <th style="font-size: 21px;"><b>Model Code</b></th>
                                        <th style="font-size: 21px;"><b>Total Production</b></th>
                                    </tr>
                                    </thead>
                                    <tbody id="preassemblyQCdashboardnight">
                                    </tbody>
                                </table>
                                </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            <!-- Dashboard grid Row End -->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
