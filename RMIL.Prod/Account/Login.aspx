<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="RMIL.Prod.Account.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PRAN-RFL Group</title>
    <link href="../cssLogin/css/style.css" rel="stylesheet" />

</head>
<body>
    <h1>RMIL PRODUCTION</h1>
    <div class="container">
        <h2>Sign In</h2>
        <form id="form1" runat="server">
             <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
            <asp:TextBox ID="txtUserID" runat="server" placeholder="User ID" class="name" required=""></asp:TextBox>
            <asp:TextBox ID="txtPassword" TextMode="Password" placeholder="Password" required="" runat="server" class="password"></asp:TextBox>
            <ul>
                <li>
                    <input type="checkbox" id="brand1" runat="server" value=""/>
                    <label for="brand1"><span></span>Remember me</label>
                </li>
            </ul>
            <a href="#">Forgot Password?</a><br />
            <div class="clear"></div>
            <asp:Button runat="server" ID="btnLogin" ValidationGroup="LoginSubmit" Text="SIGN IN" OnClick="btnLogin_Click" />
        </form>
    </div>
    <div class="footer">
        <p>&copy; Sign In Form.| Design by MIS-SW-<a href="http://apps.pranrflgroup.com/">PRAN-RFL Group</a></p>
    </div>
</body>
</html>
