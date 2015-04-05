<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="UserLogin.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>System Login</title>
    <script src="http://crypto-js.googlecode.com/svn/tags/3.0.2/build/rollups/md5.js"></script>
    <script>
        function AttemptLogin(){
            document.getElementById("password").value = CryptoJS.MD5(document.getElementById("password").value);
            document.getElementById("loginform").submit();
        }
    </script>
</head>
<body>
    <form id="loginform" runat="server" action="login.aspx" method="post">
    <div>
        <h3>Username:</h3>
        <asp:TextBox runat="server" ID="username" TextMode="SingleLine"></asp:TextBox>
        <h3>Password:</h3>
        <asp:TextBox runat="server" ID="password" TextMode="password"></asp:TextBox>
        <button type="button" name="LoginBtn" onclick="AttemptLogin()">Login</button>
    </div>
    </form>
    <div id="postresults" runat="server"></div>
    <div id="DivHash" runat="server"></div>
</body>
</html>
