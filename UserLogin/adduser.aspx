<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adduser.aspx.cs" Inherits="UserLogin.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>System Add User</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <header>Username:</header>
        <asp:TextBox runat="server"  ID ="Username"  TextMode="SingleLine" ></asp:TextBox>
        <header>Password:</header>
        <asp:TextBox runat="server"  ID ="Password"  TextMode="Password"></asp:TextBox>
        <asp:Button runat="server"   ID ="button_insert" Text="Create User" OnClick="Button_Insert" />
     </div>
    </form>
</body>
</html>
