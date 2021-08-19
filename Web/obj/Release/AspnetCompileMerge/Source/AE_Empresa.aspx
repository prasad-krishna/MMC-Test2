<%@ Page language="c#" Codebehind="AE_Empresa.aspx.cs" AutoEventWireup="false" Inherits="WebMedicamentos.AE_Empresa" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>HC-Historias Clínicas</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="css/admon.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body class="fondoApp">
		<form id="Form1" method="post" runat="server">
			<TABLE id="tblPrincipal" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD bgColor="">
						<P>&nbsp;</P>
						<P>&nbsp;</P>
						<P>&nbsp;</P>
						<TABLE id="table6" class="tableBorder" align="center" width="570" cellPadding="0" bgColor="#ffffff">
							<TR>
								<td>
									<TABLE id="table1" cellSpacing="0" cellPadding="10" width="100%" align="center" border="0">
										<TR>
											<TD align="right" background="" colSpan="2" height="60">
												<table height="60" cellSpacing="1" cellPadding="12" width="100%">
                                                    <tr>
                                                        <td style="padding-bottom:0px;">
                                                            <img alt="" src="images/NuevoBanner.jpg" />
                                                        </td>
                                                        <td class="titleBigTPA" align="right" style="padding-bottom:0px;">HC
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" height="10" style="padding:0px;">
                                                            <img alt="" src="images/separador.png" />
                                                        </td>
                                                    </tr>
												</table>
											</TD>
										</TR>
										<TR>
											<TD vAlign="middle" align="center" width="40%"><TABLE id="table2" cellSpacing="0" cellPadding="0" width="60%" align="center" border="0">
													<tr>
														<td><IMG src="images/imgReembolso.jpg" width="250">
														</td>
													</tr>
												</TABLE>
											</TD>
											<TD width="60%" align="left">
												<TABLE id="table3" cellSpacing="0" cellPadding="5" width="100%" align="center" border="0">
													<TR>
														<TD colSpan="2" height="23"></TD>
													</TR>
													<TR>
														<TD colSpan="2"><asp:Label runat="server" class="textLightGray" ID="lblSistema">Bienvenido al Sistema de Reembolsos  </asp:Label><BR>
															Seleccione la empresa con la que desea ingresar</TD>
													</TR>
													<TR>
														<TD>Empresa</TD>
														<TD>
															<asp:DropDownList id="ddlEmpresa" runat="server" Width="300px" CssClass="textBox"></asp:DropDownList>
															<asp:comparevalidator id="CompareValidator7" runat="server" CssClass="textRed" Operator="NotEqual" ValueToCompare="0"
																ErrorMessage="Requerido" Display="Dynamic" ControlToValidate="ddlEmpresa" ForeColor=" "></asp:comparevalidator></TD>
													</TR>
													<TR>
														<TD></TD>
														<TD></TD>
													</TR>
													<TR>
														<TD height="15"></TD>
														<TD height="15"></TD>
													</TR>
													<TR>
														<TD style="HEIGHT: 13px" align="center" colSpan="2"><asp:button id="btnIngresar" runat="server" CssClass="button" Text="Ingresar"></asp:button></TD>
													</TR>
													<TR>
														<TD height="15"></TD>
														<TD height="15"></TD>
													</TR>
													<TR>
														<TD align="right" colSpan="2"></TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
										<tr>
											<TD colSpan="2" height="20"></TD>
										</tr>
										<TR>
											<TD colSpan="2"></TD>
										</TR>
									</TABLE>
								</td>
							</TR>
							<TR>
								<TD>
									<TABLE id="table4" cellSpacing="0" cellPadding="5" width="100%" align="center" border="0">
										<tr>
											<td width="30%" style="border-top: #cccccc 1px solid">
                                             <img src="images/Footer.gif">
                                            </td>
											<td class="textSmallBlack" align="right" width="70%" style="BORDER-TOP: #cccccc 1px solid">©2015 
												Mercer LLC, Todos los derechos reservados.&nbsp;
												<BR>
											</td>
										</tr>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
