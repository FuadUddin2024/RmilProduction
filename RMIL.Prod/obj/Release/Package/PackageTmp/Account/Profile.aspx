<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="RMIL.Prod.Account.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function ShowErrorMsg() {

            alert(document.getElementById("ContentPlaceHolder1_hdMsg").value);

        }
        function validateCreateUser() {

            var imageName = document.getElementById("ContentPlaceHolder1_fldImage").value;
            alert(imageName);
            if (imageName == "" || imageName == null) {
                reset();
                alertify.alert("please Select an Image");
                return false;
            }
            else {
                reset();
                alertify.set({
                    delay: 2000
                });
                alertify.log("Login Successful");

                return true;

            }
        }


        function testt() {
            reset();
            alertify.alert("please Select an Image");
            return false;
        }
        function empty() {
            alert("Provide valid Email");
        }

        function ShowImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=img.ClientID%>').prop('src', e.target.result)
                        .width(240)
                        .height(150);
                };
                reader.readAsDataURL(input.files[0]);
                document.getElementById("hdImageChange").value = '1';

                } else {
                    document.getElementById("hdImageChange").value = '0';
                }
            }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdMsg" runat="server" />
    <asp:HiddenField ID="hdImageChange" Value="0" runat="server" />
    <div class='col-xs-12'>
        <div class="page-title">
            <div class="pull-left">
                <!-- PAGE HEADING TAG - START -->
                <h1 class="title">User Profile</h1>
                <!-- PAGE HEADING TAG - END -->
            </div>
            <div class="pull-right hidden-xs">
                <ol class="breadcrumb">
                    <li>
                        <a href="../Dashboard/Index.aspx"><i class="fa fa-home"></i>Home</a>
                    </li>
                    <li>
                        <a href="../Account/Profile.aspx">Social Media</a>
                    </li>
                    <li class="active">
                        <strong>User Profile</strong>
                    </li>
                </ol>
            </div>
        </div>
    </div>
    <div class="clearfix"></div>
    <!-- MAIN CONTENT AREA STARTS -->
    <div class="col-lg-12">
        <section class="box nobox ">
            <div class="content-body">
                <div class="row">
                    <div class="col-md-3 col-sm-4 col-xs-12">
                        <div class="uprofile-image">
                            <img runat="server" id="userRoundImage" alt="" src="" class="img-responsive" />
                        </div>
                        <div class="uprofile-name">
                            <h3>
                                <a href="#">
                                    <asp:Label ID="lblUsername" runat="server" Text=""></asp:Label></a>
                                <!-- Available statuses: online, idle, busy, away and offline -->
                                <span class="uprofile-status online"></span>
                            </h3>
                            <p class="uprofile-title">
                                <asp:Label ID="lblDesg" runat="server" Text=""></asp:Label>
                            </p>
                        </div>
                        <div class="uprofile-info">
                            <ul class="list-unstyled">
                                <li><i class='fa fa-user'></i>Staff ID-<asp:Label ID="lblStaffId" runat="server" Text=""></asp:Label></li>
                                <li><i class='fa fa-user'></i>Email-<asp:Label ID="lblEmail" runat="server" Text=""></asp:Label></li>
                                <li><i class='fa fa-user'></i>Contact No-<asp:Label ID="lblContactNo" runat="server" Text=""></asp:Label></li>
                                <li><i class='fa fa-home'></i>Address-<asp:Label ID="lblAddress" runat="server" Text=""></asp:Label></li>
                                <li><i class='fa fa-suitcase'></i>Company-<asp:Label ID="lblCompany" runat="server" Text=""></asp:Label></li>
                                <li><i class='fa fa-suitcase'></i>Department-<asp:Label ID="lblDept" runat="server" Text=""></asp:Label></li>
                            </ul>
                        </div>
                        <div class="uprofile-buttons">
                            <a class="btn btn-md btn-primary">Send Message</a>
                            <a class="btn btn-md btn-primary">Add as Friend</a>
                        </div>
                        <div class=" uprofile-social">

                            <a href="#" class="btn btn-primary btn-md facebook"><i class="fa fa-facebook icon-xs"></i></a>
                            <a href="#" class="btn btn-primary btn-md twitter"><i class="fa fa-twitter icon-xs"></i></a>
                            <a href="#" class="btn btn-primary btn-md google-plus"><i class="fa fa-google-plus icon-xs"></i></a>
                            <a href="#" class="btn btn-primary btn-md dribbble"><i class="fa fa-dribbble icon-xs"></i></a>

                        </div>
                    </div>
                    <div class="col-md-9 col-sm-8 col-xs-12">

                        <div class="uprofile-content row" style="background-color: #99ccff">
                            <%-- <div class="enter_post col-xs-12">

                                <div class="form-group">
                                    <div class="controls">
                                        <textarea class="form-control autogrow" id="field-7" placeholder="What's on your mind?"></textarea>
                                    </div>
                                </div>
                                <div class="enter_post_btns">
                                    <a href="#" class="btn btn-md pull-right btn-primary">Post</a>
                                    <a href="#" class="btn btn-md pull-right btn-link"><i class="fa fa-image"></i></a>
                                    <a href="#" class="btn btn-md pull-right btn-link"><i class="fa fa-map-marker"></i></a>
                                </div>
                            </div>--%>
                            <div class="tab-pane" id="pills-tab5">
                                <h4>Upload User Photo</h4>
                                <br>
                                <div class="col-lg-3 col-md-4 col-sm-12 col-xs-12">
                                    <div class="thumbnail">
                                        <img src="../Images/profile.png" runat="server" id="img" height="298" width="260" alt="" />
                                        <br>
                                        <h4 class="center-align-text">User Photo</h4>

                                        <p class="btn-stack center-align-text">
                                            <asp:FileUpload ID="fldImage" runat="server" onchange="ShowImagePreview(this);" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="fldImage" ValidationGroup="vgSubmit" runat="server"
                                                ForeColor="Red" ErrorMessage="Browse image"></asp:RequiredFieldValidator>
                                        </p>
                                    </div>
                                    <br>
                                </div>
                            </div>
                            <div class="col-xs-12">
                                <div class="pull-left ">
                                    <asp:Button ID="btnUpload" runat="server" Text="Upload" ValidationGroup="vgSubmit"
                                        OnClick="btnUpload_Click" class="btn btn-primary pull-left" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
