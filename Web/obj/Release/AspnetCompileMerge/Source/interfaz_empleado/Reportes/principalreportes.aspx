<%@ Page language="c#" Codebehind="principalreportes.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_empleado.reportes.principalreportes" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>HC-Historias Clínicas</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<LINK href="../../css/admon.css" type="text/css" rel="stylesheet">
			<script language="javascript" src="../../scripts/Base.js" type="text/javascript"></script>
			<LINK href="../../css/Calendar.css" type="text/css" rel="stylesheet">
				<script language="javascript" src="../../scripts/Calendar.js" type="text/javascript"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<TABLE height="941" cellSpacing="0" cellPadding="0" width="98" border="0" ms_2d_layout="TRUE">
			<TR vAlign="top">
				<TD width="98" height="941">
					<form id="Form1" method="post" runat="server">
						<TABLE height="81" cellSpacing="0" cellPadding="0" width="929" border="0" ms_2d_layout="TRUE">
							<TR vAlign="top">
								<TD width="10" height="15"></TD>
								<TD width="919"></TD>
							</TR>
							<TR vAlign="top">
								<TD height="66"></TD>
								<TD>
									<TABLE id="tblPrincipal" cellSpacing="0" cellPadding="0" width="918" border="0" height="65">
										<tr>
											<td align="center">
												<TABLE id="Table1" class="tableBorder" cellSpacing="0" cellPadding="3" width="60%" border="0">
													<TR>
														<TD class="titleBackBlue">REPORTES</TD>
													</TR>
													<TR>
														<TD>
															<asp:DropDownList id="ddlReportes" runat="server" CssClass="textBox" 
                                                                Width="500px" AutoPostBack="True"                                                               ></asp:DropDownList></TD>
													</TR>
												</TABLE>
											</td>
										</tr>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
					</form>
				</TD>
			</TR>
		</TABLE>
	</body>
</HTML>
