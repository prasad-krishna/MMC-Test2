<%@ Page language="c#" Codebehind="AE_employee.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_admon.forma.AE_employee" %>
<%@ Register TagPrefix="uc1" TagName="WC_DatosEmpleado" Src="../WebControls/WC_DatosEmpleado.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>HC-Historias Clínicas</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../css/admon.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../../scripts/Base.js" type="text/javascript"></script>
</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table class="tableBorder" cellSpacing="0" cellPadding="10" width="700" align="center">
				<TR>
					<TD colSpan="4">
						<uc1:WC_DatosEmpleado id="WC_DatosEmpleado1" runat="server"></uc1:WC_DatosEmpleado></TD>
				</TR>
				<TR>
					<TD colSpan="4">
						<P align="center"><asp:button id="Aceptar" runat="server" CssClass="button" Text="Cerrar"></asp:button></P>
					</TD>
				</TR>
			</table>
			&nbsp;
		</form>
	</body>
</HTML>
