<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="CreateUser.aspx.cs" Inherits="RMIL.Prod.Account.CreateUser" %>

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
        function showPass() {
            var txtpass = document.getElementById('<%=txtPassword.ClientID%>').value;
            var txtpassType = document.getElementById('<%=txtPassword.ClientID %>').type;

            var txtConfirmPass = document.getElementById('<%=txtConfirmPass.ClientID%>').value;
            var txtConfirmPassType = document.getElementById('<%=txtConfirmPass.ClientID %>').type;


            var pass = document.getElementById('eyeIcon');
            if (txtpass != "") {
                if (txtpassType == 'password') {
                    document.getElementById('<%= txtPassword.ClientID %>').type = 'text';
                    setTimeout(function () {
                        document.getElementById('<%= txtPassword.ClientID %>').type = 'password';
                    }, 3000);


                } else {
                    alert("password is already visible");
                }
            }

            if (txtConfirmPass != "") {
                if (txtConfirmPassType == 'password') {
                    document.getElementById('<%= txtConfirmPass.ClientID %>').type = 'text';
                    setTimeout(function () {
                        document.getElementById('<%= txtConfirmPass.ClientID %>').type = 'password';
                    }, 3000);
                }
            }

        }
    </script>
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

            window.onload = function () {
                document.getElementById('menuUserMgt').className = 'active';
            };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdMsg" runat="server" />
    <asp:HiddenField ID="hdImageChange" Value="0" runat="server" />
    <div class="col-lg-12">
        <section class="box ">
            <div class="panel-heading" style="background-color: #82e0aa">
                <div class="pull-left"><strong>User Information</strong></div>
                <div class="clearfix"></div>
            </div>
            <div class="content-body" style="background-color: #99ccff">
                <div class="row">
                    <div class="col-xs-12">
                        <div id="pills" class='wizardpills'>
                            <ul class="form-wizard">
                                <li><a href="#pills-tab1" data-toggle="tab"><span>Basic</span></a></li>
                                <li><a href="#pills-tab2" data-toggle="tab"><span>Contact</span></a></li>
                                <li><a href="#pills-tab3" data-toggle="tab"><span>Profile</span></a></li>
                                <li><a href="#pills-tab4" data-toggle="tab"><span>Password</span></a></li>
                                <li><a href="#pills-tab5" data-toggle="tab"><span>Photo</span></a></li>
                            </ul>
                            <div id="bar" class="progress active">
                                <div class="progress-bar progress-bar-primary " role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div>
                            </div>
                            <div class="tab-content" style="background-color: #99ccff">
                                <div class="tab-pane" id="pills-tab1">
                                    <h4>Basic Information</h4>
                                    <br>
                                    <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-lg-4">User ID</label>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="txtUserId" placeholder="User ID" runat="server" class="form-control" OnTextChanged="txtBarCode_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="txtUserId" ValidationGroup="vgSubmit" runat="server"
                                                        ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-lg-4">Staff ID</label>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="txtStaffId" placeholder="User Name" runat="server" class="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" ControlToValidate="txtStaffId" ValidationGroup="vgSubmit" runat="server"
                                                        ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-lg-4">User Name</label>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="txtUserName" placeholder="User Name" runat="server" class="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" ControlToValidate="txtUserName" ValidationGroup="vgSubmit" runat="server"
                                                        ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane" id="pills-tab2">
                                    <h4>Contact Information</h4>
                                    <br>
                                    <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-lg-4">Contact No</label>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="txtContactNo" placeholder="Contact No" onkeypress="return IsNumeric(event);" runat="server" class="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" ControlToValidate="txtContactNo" ValidationGroup="vgSubmit" runat="server"
                                                        ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-lg-4">Address</label>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="txtAddress" TextMode="MultiLine" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-lg-4">Email</label>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="TextEmail" placeholder="Email" runat="server" ValidationGroup="vgSubmit" class="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" ControlToValidate="TextEmail" ValidationGroup="vgSubmit" runat="server"
                                                        ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please Enter Valid Email ID"
                                                        ValidationGroup="vgSubmit" ControlToValidate="TextEmail" CssClass="requiredFieldValidateStyle"
                                                        Display="Dynamic" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane" id="pills-tab3">
                                    <h4>Profile Information</h4>
                                    <br>
                                    <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-lg-4">User Role</label>
                                                <div class="col-lg-8">
                                                    <asp:DropDownList ID="ddlUserType" runat="server" class="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-lg-4">Department</label>
                                                <div class="col-lg-8">
                                                    <asp:DropDownList ID="ddlDepartment" runat="server" class="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-lg-4">Company</label>
                                                <div class="col-lg-8">
                                                    <asp:DropDownList ID="ddlCompany" runat="server" class="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-lg-4">Designation</label>
                                                <div class="col-lg-8">
                                                    <asp:DropDownList ID="ddlDesignation" runat="server" class="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane" id="pills-tab4">
                                    <h4>Password Information</h4>
                                    <br>
                                    <asp:Panel ID="pnlPassword" runat="server">
                                        <div class="col-md-6">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label class="control-label col-lg-4">Password</label>
                                                    <div class="col-lg-8">
                                                        <asp:TextBox ID="txtPassword" placeholder="Password" runat="server" TextMode="Password" class="form-control"
                                                            autocomplete="off" MaxLength="12"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="Dynamic" ControlToValidate="txtPassword" ValidationGroup="vgSubmit" runat="server"
                                                            ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                        <div>
                                                            <span class="fa fa-eye" id="eyeIcon" onclick="showPass();"></span>
                                                        </div>
                                                        <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtPassword"
                                                            ValidationGroup="vgSubmit" ForeColor="Red" ValidationExpression="(?=^.{6,12}$)(?=(?:.*?\d){1})(?=.*[a-z])(?=(?:.*?[A-Z]){1})(?=(?:.*?[!@#$%*()_+^&}{:;?.]){1})(?!.*\s)[0-9a-zA-Z!@#$%*()_+^&]*$"
                                                            runat="server" ErrorMessage="Password length must be between 6 to 12 characters,where 1 uppercase,1 lowercase,1 numeric and 1 special character."></asp:RegularExpressionValidator>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label class="control-label col-lg-4">Confirm Password</label>
                                                    <div class="col-lg-8">
                                                        <asp:TextBox ID="txtConfirmPass" placeholder="Confirm Password" runat="server" TextMode="Password" autocomplete="off"
                                                            MaxLength="12" class="form-control"></asp:TextBox>
                                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="vgSubmit"
                                                            ForeColor="Red" ErrorMessage="Password doesn't match" ControlToCompare="txtPassword"
                                                            ControlToValidate="txtConfirmPass"></asp:CompareValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                                <div class="tab-pane" id="pills-tab5">
                                    <h4>Upload User Photo</h4>
                                    <br>
                                    <div class="col-lg-3 col-md-4 col-sm-12 col-xs-12">
                                        <div class="thumbnail">
                                            <img src="../Images/profile.png" runat="server" id="img" height="300" width="200" alt="" />
                                            <br>
                                            <h4 class="center-align-text">User Photo</h4>

                                            <p class="btn-stack center-align-text">
                                                <asp:FileUpload ID="fldImage" runat="server" onchange="ShowImagePreview(this);" />
                                            </p>
                                        </div>
                                        <br>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <ul class="pager wizard">
                                    <li class="previous first" style="display: none;"><a href="javascript:;">First</a></li>
                                    <li class="previous"><a href="javascript:;">Previous</a></li>
                                    <li class="next last" style="display: none;"><a href="javascript:;">Last</a></li>
                                    <li class="next"><a href="javascript:;">Next</a></li>
                                    <li class="finish"><a href="javascript:;">Finish</a></li>
                                </ul>
                                <div class="col-xs-12">
                                    <div class="pull-right ">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="vgSubmit"
                                            OnClick="btnSave_Click" class="btn btn-primary" />
                                        <asp:Button ID="btnUpdate" runat="server" Text="Update" ValidationGroup="vgSubmit"
                                            OnClick="btnUpdate_Click" class="btn btn-primary" Visible="false" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <!-- User Info grid Start -->
    <div class="col-lg-12">
        <section class="box ">
            <header class="panel_header">
                <h2 class="title pull-left">User List</h2>
                <div class="actions panel_actions pull-right">
                    <a class="box_toggle fa fa-chevron-down"></a>
                    <a class="box_setting fa fa-cog" data-toggle="modal" href="#section-settings"></a>
                    <a class="box_close fa fa-times"></a>
                </div>
            </header>
            <div class="content-body">
                <div class="row">
                    <div class="col-xs-12">
                        <asp:GridView ID="gvUserInfo" CssClass="table table-striped dt-responsive display"
                            AllowPaging="True" PageSize="20" AutoGenerateColumns="False" Style="font-family: calibri !important"
                            DataKeyNames="UserCode" runat="server" OnPageIndexChanging="gvUserInfo_PageIndexChanging">
                            <Columns>
                                <asp:BoundField HeaderText="UserCode" DataField="UserCode">
                                    <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                    <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="User Id" DataField="UserId">
                                    <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                    <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="User Name" DataField="UserName">
                                    <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                    <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Staff ID" DataField="StaffID">
                                    <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                    <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Contact No" DataField="ContactNo">
                                    <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                    <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Delete" Visible="False">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnDeleteUserInfo" runat="server" ImageUrl="~/Images/delete.png" ToolTip="Delete Record"
                                            Width="22px" Height="22px" CommandArgument='<%# Eval("UserCode")%>' OnClientClick="return confirm('Are you sure to Delete?')"
                                            OnClick="btnDeleteUserInfo_Click" CausesValidation="False" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="HeaderCenterStyle"></HeaderStyle>
                                    <ItemStyle CssClass="HeaderCenterStyle"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditUserInfo" runat="server" ImageUrl="~/Images/edit.png" ToolTip="Edit Record"
                                            Width="22px" Height="22px" CommandArgument='<%# Eval("UserCode")%>' OnClick="btnEditUserInfo_Click" CausesValidation="False" />
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
    <!-- User Info grid End -->
</asp:Content>
