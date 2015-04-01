<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="UserLogin.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <header>Username:</header>
        <asp:TextBox runat="server"  ID ="Username" value="" TextMode="SingleLine" ></asp:TextBox>
        <header>Password:</header>
        <asp:TextBox runat="server"  ID ="Password" value="" TextMode="SingleLine"></asp:TextBox>
        <asp:Button runat="server"   ID ="button_insert" Text="Login" OnClick="Button_Insert" />
     </div>
    </form>
</body>
</html>
