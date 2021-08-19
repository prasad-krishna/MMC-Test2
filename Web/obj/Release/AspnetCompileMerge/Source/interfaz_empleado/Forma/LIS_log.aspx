﻿<%@ Page language="c#" Codebehind="LIS_log.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_admon.forma.LIS_log" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title></title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../css/admon.css" type="text/css" rel="stylesheet">
		<LINK href="../../css/Calendar.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../../scripts/Calendar.js" type="text/javascript"></script>
		<script language="javascript" src="../../scripts/Base.js" type="text/javascript"></script>
	</HEAD>
	<body onload="CargarConfiguracion()">
		<form id="Form1" method="post" runat="server">
			<TABLE id="tblPrincipal" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td>
						<TABLE id="table6" width="99%" align="center">
							<tr>
								<td>
									<TABLE id="Table1" cellSpacing="0" cellPadding="3" width="100%" border="0">
										<TR>
											<TD align="center" width="50%" colSpan="2">
												<TABLE class="tableBorder" id="Table2" cellSpacing="0" cellPadding="6" width="60%">
													<TR>
														<TD class="titleBackBlue" width="20%" colSpan="4">Buscador Log de Auditoría</TD>
													</TR>
													<TR>
														<TD width="20%">Fecha Inicio</TD>
														<TD width="30%"><asp:textbox id="txtFechaInicio" runat="server" CssClass="textBox" Width="80px"></asp:textbox>&nbsp;<A href="javascript:MostrarCalendario(Form1.txtFechaInicio,Form1.txtFechaInicio,'dd/mm/yyyy');"
																name="btnFechaInicio"><IMG src="../../images/icoCalendar.gif" border="0"></A>
														</TD>
														<TD width="20%">Fecha Fin</TD>
														<TD width="30%"><asp:textbox id="txtFechaFin" runat="server" CssClass="textBox" Width="80px"></asp:textbox>&nbsp;<A href="javascript:MostrarCalendario(Form1.txtFechaFin,Form1.txtFechaFin,'dd/mm/yyyy');"
																name="btnFechaFin"><IMG src="../../images/icoCalendar.gif" border="0"></A>
														</TD>
													</TR>
													<TR>
														<TD onma style="HEIGHT: 31px">Usuario</TD>
														<TD style="HEIGHT: 31px"><asp:dropdownlist id="ddlUsuario" runat="server" CssClass="textBox" Width="160px"></asp:dropdownlist></TD>
														<TD style="HEIGHT: 31px">Acción</TD>
														<TD style="HEIGHT: 31px"><asp:dropdownlist id="ddlAccion" runat="server" CssClass="textBox" Width="160px"></asp:dropdownlist></TD>
													</TR>
													<TR>
														<TD align="center" colSpan="4"><asp:button id="btnBuscar" runat="server" CssClass="button" Text="Buscar"></asp:button></TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
										<TR>
											<TD align="center" colSpan="2" height="20"></TD>
										</TR>
										<TR>
											<TD align="center" colSpan="2" height="20"><asp:datagrid id="dgdLog" runat="server" CssClass="grid" Width="95%" CellPadding="4" GridLines="Horizontal"
													AutoGenerateColumns="False" AllowPaging="True" PageSize="20">
													<AlternatingItemStyle CssClass="altItems"></AlternatingItemStyle>
													<ItemStyle CssClass="norItems"></ItemStyle>
													<HeaderStyle CssClass="headerGrid"></HeaderStyle>
													<Columns>
														<asp:BoundColumn Visible="False" DataField="usuario_id"></asp:BoundColumn>
														<asp:BoundColumn Visible="False" DataField="IdLog"></asp:BoundColumn>
														<asp:TemplateColumn HeaderText="Usuario">
															<ItemStyle Width="25%"></ItemStyle>
															<ItemTemplate>
																<asp:Label id="lblUsuario" runat="server"></asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:BoundColumn DataField="NombreActionLog" HeaderText="Acci&#243;n">
															<ItemStyle Width="15%"></ItemStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="DateLog" HeaderText="Fecha">
															<ItemStyle Width="17%"></ItemStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="IP" HeaderText="IP">
															<ItemStyle Width="10%"></ItemStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="Detail" HeaderText="Detalle">
															<ItemStyle Width="33%"></ItemStyle>
														</asp:BoundColumn>
													</Columns>
													<PagerStyle CssClass="pagerGrid" Mode="NumericPages"></PagerStyle>
												</asp:datagrid></TD>
										</TR>
										<TR>
											<TD align="center" colSpan="2"></TD>
										</TR>
									</TABLE>
								</td>
							</tr>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
