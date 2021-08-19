<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="Web.errorPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>ErrorPage</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <asp:Panel ID="Panel1"
            Style="z-index: 102; left: 112px; position: absolute; top: 72px; height: 35px;" runat="server"
            Width="474px" BorderColor="Black" BorderStyle="Solid" HorizontalAlign="Center">
            <p>
                <asp:Label ID="lblError" runat="server" ForeColor="Red">Por el momento no se puede realizar la consulta.</asp:Label>
            </p>
        </asp:Panel>
    </form>
</body>
</html>
