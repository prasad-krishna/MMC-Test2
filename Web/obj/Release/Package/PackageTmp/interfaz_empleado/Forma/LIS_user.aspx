<%@ Page language="c#" Codebehind="LIS_user.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_admon.forma.LIS_user" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title></title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../css/Calendar.css" type="text/css" rel="stylesheet">
		<LINK href="../../css/admon.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../../scripts/Calendar.js" type="text/javascript"></script>
		<script language="javascript" src="../../scripts/Base.js" type="text/javascript"></script>
	</HEAD>
	<body onload="CargarConfiguracion();document.Form1.txtNombre.focus();">
		<form id="Form1" method="post" runat="server">
			<TABLE id="tblPrincipal" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td>
						<TABLE id="table6" width="99%" align="center">
							<tr>
								<td>
									<TABLE id="Table1" cellSpacing="0" cellPadding="5" width="100%" border="0">
										<TR>
											<TD align="center" width="50%" colSpan="2">
												<TABLE class="tableBorder" id="Table2" cellSpacing="0" cellPadding="6" width="60%">
													<TR>
														<TD class="titleBackBlue" width="20%" colSpan="4" style="HEIGHT: 25px">Buscador de 
															Usuarios</TD>
													</TR>
													<TR>
														<TD width="20%">Nombre</TD>
														<TD width="30%"><asp:textbox id="txtNombre" runat="server" CssClass="textBox" Width="150px"></asp:textbox></TD>
														<TD width="20%">Login</TD>
														<TD width="30%"><asp:textbox id="txtLogin" runat="server" CssClass="textBox" Width="100px"></asp:textbox></TD>
													</TR>
													<TR>
														<TD align="center" colSpan="4"><asp:button id="btnBuscar" runat="server" CssClass="button" Text="Buscar"></asp:button></TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
										<TR>
											<TD style="HEIGHT: 26px" align="center" colSpan="2" height="26"></TD>
										</TR>
										<TR>
											<TD style="HEIGHT: 26px" align="left" colSpan="2" height="26"><A><asp:imagebutton id="imbAdicionar" runat="server" ImageUrl="../../iconos/ico_adicionar.gif"></asp:imagebutton></A>&nbsp;
												<asp:linkbutton id="lnkAdicionar" runat="server">Adicionar Usuario</asp:linkbutton></TD>
										</TR>
										<TR>
											<TD align="center" colSpan="2" height="20">
												<asp:datagrid id="dgdUsuario" runat="server" CssClass="grid" Width="90%" AllowPaging="True" AutoGenerateColumns="False"
													GridLines="Horizontal" CellPadding="4" PageSize="20">
													<AlternatingItemStyle CssClass="altItems"></AlternatingItemStyle>
													<ItemStyle CssClass="norItems"></ItemStyle>
													<HeaderStyle CssClass="headerGrid"></HeaderStyle>
													<Columns>
														<asp:BoundColumn Visible="False" DataField="IdUser"></asp:BoundColumn>
														<asp:TemplateColumn HeaderText="Usuario">
															<ItemStyle Width="28%"></ItemStyle>
															<ItemTemplate>
																<asp:HyperLink CssClass="textSmallBlack" id="hplName" NavigateUrl='<%# "AE_user.aspx?IdUser=" + DataBinder.Eval(Container, "DataItem.IdUser") %>' runat="server">
																	<%# DataBinder.Eval(Container, "DataItem.NameUser") %>
																</asp:HyperLink>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:BoundColumn DataField="Login" HeaderText="Login">
															<ItemStyle Width="18%"></ItemStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="Email" HeaderText="Correo Electr&#243;nico">
															<ItemStyle Width="25%"></ItemStyle>
														</asp:BoundColumn>
														<asp:TemplateColumn HeaderText="Activo">
															<ItemStyle Width="8%"></ItemStyle>
															<ItemTemplate>
																<asp:Label id="lblActivo" runat="server" Text='<%# (Convert.ToBoolean(DataBinder.Eval(Container, "DataItem.Active")) == true ? "Si" : "No") %>'>
																</asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Empresa(s)">
															<ItemStyle Width="28%" HorizontalAlign=Center></ItemStyle>
															<ItemTemplate>
																<asp:HyperLink CssClass="textSmallBlack" id="hplName" NavigateUrl='<%# "AE_empresasusuarios.aspx?IdUser=" + DataBinder.Eval(Container, "DataItem.IdUser") %>' runat="server">
																	Ver
																</asp:HyperLink>
															</ItemTemplate>
														</asp:TemplateColumn>
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
