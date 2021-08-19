<%@ Register TagPrefix="uc1" TagName="WC_EncabezadoFormato" Src="../WebControls/WC_EncabezadoFormato.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_DiagnosticosFormato" Src="../WebControls/WC_DiagnosticosFormato.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_AnotacionesFijasFormato" Src="../WebControls/WC_AnotacionesFijasFormato.ascx" %>
<%@ Page language="c#" Codebehind="FO_Consultas.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_empleado.formatos.FO_Consultas" %>
<%@ Register TagPrefix="uc1" TagName="WC_PieFormato" Src="../WebControls/WC_PieFormato.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>HC-Historias Clínicas</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../css/admon.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="5" leftMargin="1" topMargin="0" rightMargin="5" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE runat="server" class="tableFormato" id="tblPrincipal" cellSpacing="0" cellPadding="0"
				width="650" border="0">
				<TR>
					<TD align="center">
						<TABLE runat="server" class="tableFormato" id="tblInterior" cellSpacing="0" cellPadding="0"
							width="620" border="0">
							<TR>
								<TD><uc1:wc_encabezadoformato id="WC_EncabezadoFormato1" runat="server"></uc1:wc_encabezadoformato></TD>
							</TR>
							<TR>
								<TD>
									<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<TR>
											<TD align="left" width="20%">
												<TABLE class="tableBorderFormato" id="Table6" width="100%" border="0">
													<TR>
														<TD vAlign="top" height="30">Servicio Autorizado por</TD>
													</TR>
													<TR>
														<TD align="center" height="25">____________________________<BR>
															<asp:label id="lblUnidadAprobacion" runat="server"></asp:label></TD>
													</TR>
													<TR>
														<TD height="13" class="tdSmallFormato"></TD>
													</TR>
												</TABLE>
											</TD>
											<TD width="10"></TD>
											<TD align="center" width="20%">
												<TABLE class="tableBorderFormato" id="Table7" width="100%" border="0">
													<TR>
														<TD vAlign="top" height="30"></TD>
													</TR>
													<TR>
														<TD align="center" height="25">____________________________<BR>
															Firma del Médico</TD>
													</TR>
													<TR>
														<TD vAlign="bottom" height="13" class="tdSmallFormato">CC&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;RM</TD>
													</TR>
												</TABLE>
											</TD>
											<TD width="10">&nbsp;</TD>
											<TD align="center" width="20%">
												<TABLE class="tableBorderFormato" id="Table9" width="100%" border="0">
													<TR>
														<TD vAlign="top" height="30"></TD>
													</TR>
													<TR>
														<TD align="center" height="25">_____________________________<BR>
															Firma del Paciente</TD>
													</TR>
													<TR>
														<TD vAlign="bottom" height="13" class="tdSmallFormato">CC</TD>
													</TR>
												</TABLE>
											</TD>
											<TD width="10"></TD>
											<TD align="right" width="12%">
												<TABLE class="tableBorderFormato" id="Table8" width="100%" border="0">
													<TR>
														<TD vAlign="bottom" align="center" height="30"><asp:label id="lblValorTotal" runat="server"></asp:label></TD>
													</TR>
													<TR>
														<TD align="center" height="25">________________<BR>
															Valor Total</TD>
													</TR>
													<TR>
														<TD height="13" class="tdSmallFormato"></TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD class="textFormatoSmall" height="40" vAlign="top">VIGENCIA DE&nbsp;
									<asp:label id="lblVigencia" runat="server"></asp:label>DÍAS A PARTIR DE LA FECHA DE EXPEDICION
									<BR>
									<asp:label id="lblTextoFormato" runat="server" CssClass="tdSmallFormato"></asp:label></TD>
							</TR>
							<TR>
								<TD vAlign="top" align="center" height="8" class="tdSmallFormato">----------------------------------------------------------------------------------------------------------------------------------------------------------</TD>
							</TR>
							<TR>
								<TD>
									<TABLE class="tableFormato" id="Table10" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<TR>
											<TD>
												<TABLE class="tableBorderFormato" id="Table3" cellSpacing="0" cellPadding="0" width="100%"
													border="0">
													<TR>
														<TD height="10">
															<TABLE class="tableBorderFormato" id="tblOrden" cellSpacing="0" cellPadding="0" width="100%"
																border="0">
																<TR>
																	<TD align="left" width="10%">PACIENTE</TD>
																	<TD width="35%"><asp:label id="lblNombrePaciente" runat="server" CssClass="lblFormato"></asp:label></TD>
																	<TD align="right" width="10%">IDENTIFICACIÓN</TD>
																	<TD width="20%"><asp:label id="lblTipoIdentificacion" runat="server" CssClass="lblFormato"></asp:label>&nbsp;<asp:label id="lblNumero" runat="server" CssClass="lblFormato"></asp:label></TD>
																	<TD align="right" width="10%">LIQUIDACIÓN</TD>
																	<TD width="15%"><asp:label id="txtNoSolicitud" runat="server" CssClass="lblFormato"></asp:label></TD>
																</TR>
															</TABLE>
														</TD>
													</TR>
													<TR>
														<TD><asp:datagrid id="dtgDetalle" runat="server" CssClass="gridFormato" GridLines="Horizontal" AutoGenerateColumns="False"
																AllowPaging="False" CellPadding="1" Width="100%">
																<AlternatingItemStyle CssClass="altItemsFormato"></AlternatingItemStyle>
																<ItemStyle CssClass="norItemsFormato"></ItemStyle>
																<HeaderStyle CssClass="headerGridFormato"></HeaderStyle>
																<Columns>
																	<asp:TemplateColumn HeaderText="Servicio">
																		<ItemStyle Width="60%"></ItemStyle>
																		<ItemTemplate>
																			<asp:Label runat="server" ID="lblProductoServicio" text='<%# DataBinder.Eval(Container, "DataItem.NombreCompletoMedicamento").ToString() + DataBinder.Eval(Container, "DataItem.NombreCompletoServicio").ToString() + " " + DataBinder.Eval(Container, "DataItem.Comentarios").ToString()  %>'>
																			</asp:Label>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Cantidad">
																		<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
																		<ItemTemplate>
																			<asp:Label text='<%# DataBinder.Eval(Container, "DataItem.Cantidad") %>' ID="lblCantidadProducto" Runat="server" >
																			</asp:Label>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																</Columns>
															</asp:datagrid></TD>
													</TR>
													<TR>
														<TD><uc1:wc_diagnosticosformato id="WC_DiagnosticosFormato1" runat="server"></uc1:wc_diagnosticosformato></TD>
													</TR>
													<TR>
														<TD>
															<TABLE class="tableBorderFormato" id="Table18" height="10" cellSpacing="0" cellPadding="0"
																width="100%" border="0">
																<TR>
																	<TD align="center" class="tdEncabezadoFormato"><STRONG>RESUMEN DE LA ATENCIÓN</STRONG></TD>
																</TR>
															</TABLE>
														</TD>
													</TR>
													<TR>
													</TR>
													<TR>
														<TD>
															<TABLE class="tableNoBorderFormato" id="Table17" cellSpacing="0" cellPadding="0" width="99%">
																<TR>
																	<TD align="left" width="20%" vAlign="bottom">MOTIVO&nbsp;
																	</TD>
																	<TD width="43%" colSpan="3">
																		<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
																			<TR>
																				<TD class="tdLineaFormato" width="100%">&nbsp;</TD>
																			</TR>
																		</TABLE>
																	</TD>
																	<TD align="right" width="18%" vAlign="bottom">FECHA ATENCIÓN&nbsp;
																	</TD>
																	<TD width="20%">
																		<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
																			<TR>
																				<TD class="tdLineaFormato">&nbsp;</TD>
																			</TR>
																		</TABLE>
																	</TD>
																</TR>
																<TR>
																	<TD align="left" vAlign="bottom">HALLAZGOS PRINCIPALES&nbsp;
																	</TD>
																	<TD vAlign="bottom" width="80%" colSpan="5">
																		<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
																			<TR>
																				<TD class="tdLineaFormato">&nbsp;</TD>
																			</TR>
																			<TR>
																				<TD class="tdLineaFormato">&nbsp;</TD>
																			</TR>
																		</TABLE>
																	</TD>
																</TR>
																<TR>
																	<TD align="left" vAlign="bottom">
																		<P>DIAGNOSTICOS<BR>
																		</P>
																	</TD>
																	<TD width="22%" align="center">
																		<TABLE id="Table11" cellSpacing="0" cellPadding="0" width="95%" align="left" border="0">
																			<TR>
																				<TD class="tdLineaFormato">&nbsp;</TD>
																			</TR>
																			<TR>
																				<TD class="tdLineaFormato">&nbsp;</TD>
																			</TR>
																		</TABLE>
																	</TD>
																	<TD align="right" width="8%" vAlign="bottom">CÓDIGO (CIE 10)<BR>
																	</TD>
																	<TD width="13%">
																		<TABLE id="Table12" cellSpacing="0" cellPadding="0" width="95%" align="center" border="0">
																			<TR>
																				<TD class="tdLineaFormato">&nbsp;</TD>
																			</TR>
																			<TR>
																				<TD class="tdLineaFormato">&nbsp;</TD>
																			</TR>
																		</TABLE>
																	</TD>
																	<TD align="right" width="18%" vAlign="bottom">TIEMPO EVOLUCIÓN<BR>
																		&nbsp;
																	</TD>
																	<TD width="20%">
																		<TABLE id="Table13" cellSpacing="0" cellPadding="0" width="95%" align="right" border="0">
																			<TR>
																				<TD class="tdLineaFormato">&nbsp;</TD>
																			</TR>
																			<TR>
																				<TD class="tdLineaFormato">&nbsp;</TD>
																			</TR>
																		</TABLE>
																	</TD>
																</TR>
																<TR>
																	<TD align="left" vAlign="middle">PLAN DE TRATAMIENTO&nbsp;
																	</TD>
																	<TD width="80%" colSpan="5">
																		<TABLE id="Table14" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
																			<TR>
																				<TD class="tdLineaFormato">&nbsp;</TD>
																			</TR>
																			<TR>
																				<TD class="tdLineaFormato">&nbsp;</TD>
																			</TR>
																			<TR>
																				<TD class="tdLineaFormato">&nbsp;</TD>
																			</TR>
																			<TR>
																				<TD class="tdLineaFormato">&nbsp;</TD>
																			</TR>
																		</TABLE>
																	</TD>
																</TR>
																<TR>
																	<TD align="left">REQUIERE CONTROL&nbsp;
																	</TD>
																	<TD width="43%" colSpan="3">SI _______&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; No_______</TD>
																	<TD align="right" width="18%" vAlign="bottom">EN CUANTO TIEMPO?&nbsp; &nbsp;&nbsp;</TD>
																	<TD vAlign="bottom" width="20%">
																		<TABLE id="Table15" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
																			<TR>
																				<TD class="tdLineaFormato">&nbsp;</TD>
																			</TR>
																		</TABLE>
																	</TD>
																</TR>
																<TR>
																	<TD align="left">OBSERVACIONES&nbsp;
																	</TD>
																	<TD width="80%" colSpan="5">
																		<TABLE id="Table16" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
																			<TR>
																				<TD class="tdLineaFormato">&nbsp;</TD>
																			</TR>
																			<TR>
																				<TD class="tdLineaFormato">&nbsp;</TD>
																			</TR>
																		</TABLE>
																	</TD>
																</TR>
															</TABLE>
														</TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD vAlign="bottom" height="20">Firma
									<asp:label id="lblMedico" runat="server"></asp:label>&nbsp;&nbsp;_____________________________________
								</TD>
							</TR>
							<TR>
								<TD><uc1:wc_pieformato id="WC_PieFormato1" runat="server"></uc1:wc_pieformato></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
