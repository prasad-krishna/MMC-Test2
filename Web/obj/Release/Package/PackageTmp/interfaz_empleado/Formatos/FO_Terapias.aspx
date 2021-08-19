<%@ Register TagPrefix="uc1" TagName="WC_EncabezadoFormato" Src="../WebControls/WC_EncabezadoFormato.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_AnotacionesFijasFormato" Src="../WebControls/WC_AnotacionesFijasFormato.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_PieFormato" Src="../WebControls/WC_PieFormato.ascx" %>
<%@ Page language="c#" Codebehind="FO_Terapias.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_empleado.formatos.FO_Terapias" %>
<%@ Register TagPrefix="uc1" TagName="WC_DiagnosticosFormato" Src="../WebControls/WC_DiagnosticosFormato.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>HC-Historias Clínicas</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<LINK href="../../css/admon.css" type="text/css" rel="stylesheet">
	</HEAD>
	<BODY bottomMargin="5" leftMargin="1" topMargin="1" rightMargin="5">
		<form id="Form1" method="post" runat="server">
			<TABLE class="tableFormato" id="tblPrincipal" cellSpacing="0" cellPadding="0" width="650"
				border="0" runat="server">
				<TR>
					<TD align="center">
						<TABLE class="tableFormato" id="tblInterior" cellSpacing="0" cellPadding="0" width="620"
							border="0" runat="server">
							<TBODY>
								<TR>
									<TD><uc1:wc_encabezadoformato id="WC_EncabezadoFormato1" runat="server"></uc1:wc_encabezadoformato></TD>
								</TR>
								<TR>
									<TD>
										<TABLE class="tableBorderFormato" id="Table10" height="15" cellSpacing="0" cellPadding="0"
											width="100%" border="0">
											<TR>
												<TD class="tdEncabezadoFormato" align="center" width="10%" colSpan="6"><STRONG>SERVICIO 
														SOLICITADO</STRONG></TD>
											</TR>
										</TABLE>
										<uc1:wc_diagnosticosformato id="WC_DiagnosticosFormato1" runat="server"></uc1:wc_diagnosticosformato></TD>
								</TR>
								<TR>
									<TD><asp:datalist id="dtlServicios" runat="server" CellSpacing="0" CellPadding="0" Width="100%">
											<ItemTemplate>
												<TABLE class="tableBorderFormato" id="Table2" cellSpacing="0" cellPadding="0" width="100%"
													border="0">
													<TR>
														<TD width="10%">SERVICIO</TD>
														<TD width="70%">
															<asp:Label id=lblServicio runat="server" text='<%# DataBinder.Eval(Container, "DataItem.NombreCompletoMedicamento").ToString() + DataBinder.Eval(Container, "DataItem.NombreCompletoServicio").ToString() %>'>
															</asp:Label></TD>
														<TD width="10%">CANTIDAD</TD>
														<TD width="10%">
															<asp:Label id=lblCantidad runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Cantidad") %>'>
															</asp:Label></TD>
													</TR>
													<TR>
														<TD align="center" colSpan="4">
															<asp:datagrid id="dtgServicios" runat="server" Width="100%" CellPadding="0" AllowPaging="False"
																AutoGenerateColumns="False" GridLines="Horizontal" CssClass="gridFormato">
																<AlternatingItemStyle CssClass="altItemsFormato"></AlternatingItemStyle>
																<ItemStyle Height="16px" CssClass="norItemsFormato"></ItemStyle>
																<HeaderStyle CssClass="headerGridFormato"></HeaderStyle>
																<Columns>
																	<asp:TemplateColumn HeaderText="Fecha">
																		<ItemStyle Width="30%" HorizontalAlign="Center"></ItemStyle>
																		<ItemTemplate>
																			<asp:Label runat="server" ID="lblFecha" Text='<%# DataBinder.Eval(Container, "DataItem.Id") == "" ? "" : "____________________" %>' >
																			</asp:Label>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Valor">
																		<ItemStyle Width="20%" HorizontalAlign="Center"></ItemStyle>
																		<ItemTemplate>
																			<asp:Label runat="server" ID="lblValor" Text='<%# DataBinder.Eval(Container, "DataItem.Id") == "" ? "" : "____________________" %>'>
																			</asp:Label>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Firma">
																		<ItemStyle HorizontalAlign="Center" Width="50%"></ItemStyle>
																		<ItemTemplate>
																			<asp:Label ID="lblFirma" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Id") == "" ? "" : "____________________" %>'>
																			</asp:Label>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																</Columns>
															</asp:datagrid></TD>
													</TR>
													<TR>
														<TD colSpan="4">
															<TABLE class="tableNoBorderFormato" id="Table15" cellSpacing="0" cellPadding="0" width="100%"
																border="0">
																<TR>
																	<TD width="24%"></TD>
																	<TD width="27%">TOTAL&nbsp; ________________</TD>
																	<TD width="50%"></TD>
																</TR>
															</TABLE>
														</TD>
													</TR>
												</TABLE>
											</ItemTemplate>
										</asp:datalist></TD>
								</TR>
								<TR>
									<TD>
										<TABLE class="tableBorderFormato" id="Table13" height="15" cellSpacing="0" cellPadding="0"
											width="100%" border="0">
											<TR>
												<TD class="tdEncabezadoFormato" align="center" width="10%" colSpan="6"><STRONG>OBSERVACIONES</STRONG></TD>
											</TR>
										</TABLE>
										<TABLE class="tableBorderFormato" id="Table14" height="45" cellSpacing="0" cellPadding="0"
											width="100%" border="0">
											<TR>
												<TD vAlign="top" align="center" width="10%" colSpan="6">
													<P align="left"><asp:label id="lblObservaciones" runat="server"></asp:label><BR>
														Recuerde, para efecto de cobro sólo la firma del paciente valida la sesión.<BR>
														<uc1:wc_anotacionesfijasformato id="WC_AnotacionesFijasFormato1" runat="server"></uc1:wc_anotacionesfijasformato></P>
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD height="5"></TD>
								</TR>
								<TR>
									<TD>
										<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR>
												<TD align="left" width="28%">
													<TABLE class="tableBorderFormato" id="Table6" width="100%" border="0">
														<TR>
															<TD vAlign="top" height="30">Servicio Autorizado por</TD>
														</TR>
														<TR>
															<TD align="center" height="25">________________________________<BR>
																<asp:label id="lblUnidadAprobacion" runat="server"></asp:label></TD>
														</TR>
														<TR>
															<TD height="13"></TD>
														</TR>
													</TABLE>
												</TD>
												<TD width="30"></TD>
												<TD align="center" width="42%">
													<TABLE class="tableBorderFormato" id="Table7" width="100%" border="0">
														<TR>
															<TD vAlign="top" height="30"></TD>
														</TR>
														<TR>
															<TD align="center" height="25">____________________________________________<BR>
																Firma del Prestador</TD>
														</TR>
														<TR>
															<TD class="tdSmallFormato" vAlign="bottom" height="13">CC&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;RM</TD>
														</TR>
													</TABLE>
												</TD>
												<TD width="30">&nbsp;</TD>
												<TD align="right" width="12%">
													<TABLE class="tableBorderFormato" id="Table8" width="100%" border="0">
														<TR>
															<TD vAlign="bottom" align="center" height="30"><asp:label id="lblValorTotal" runat="server"></asp:label></TD>
														</TR>
														<TR>
															<TD align="center" height="25">_________________<BR>
																Valor Total</TD>
														</TR>
														<TR>
															<TD height="13"></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD class="textFormatoSmall" height="30">VIGENCIA DE&nbsp;
										<asp:label id="lblVigencia" runat="server"></asp:label>DÍAS A PARTIR DE LA FECHA DE EXPEDICION
										<BR>
										<asp:label id="lblTextoFormato" runat="server" CssClass="tdSmallFormato"></asp:label></TD>
								</TR>
								<TR>
									<TD><uc1:wc_pieformato id="WC_PieFormato1" runat="server"></uc1:wc_pieformato></TD>
								</TR>
				</TR>
			</TABLE>
			</TD></TR></TBODY></TABLE></form>
	</BODY>
</HTML>
