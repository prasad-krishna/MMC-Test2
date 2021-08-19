<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="Web.errorPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<HTML>
  <HEAD>
		<title>ErrorPage</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
  </HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:Panel id="Panel1" 
                style="Z-INDEX: 102; LEFT: 112px; POSITION: absolute; TOP: 72px; height: 35px;" runat="server"
				Width="474px" BorderColor="Black" BorderStyle="Solid" HorizontalAlign="Center">
<P>
<asp:Label id=lblError runat="server" ForeColor="Red">Por el momento no se puede realizar la consulta.</asp:Label></P>
			</asp:Panel>
		</form>
	</body>
</HTML>
