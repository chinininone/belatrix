<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testLogger.aspx.cs" Inherits="web.testLogger" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Test logger</h1>
        <div>
            <p><b>Message</b><asp:TextBox ID="txtMessage" runat="server"></asp:TextBox></p>
            <p><b>Message type</b>
                <asp:CheckBox ID="chkError" Text="An error" runat="server" />
                <asp:CheckBox ID="chkWarning" Text="A warning" runat="server" />
                <asp:CheckBox ID="chkMessage" Text="Only a message" runat="server" />
            </p>
            <p><b>Send to</b>
                <asp:CheckBox ID="chkDataBase" Text="Database" runat="server" />
                <asp:CheckBox ID="chkFile" Text="File" runat="server" />
                <asp:CheckBox ID="chkConsole" Text="Console" runat="server" />
            </p>
            <p><asp:Button ID="btnSend" runat="server" Text="Send" OnClick="btnSend_Click" /></p>
        </div>
        <asp:Label ID="lblError" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
