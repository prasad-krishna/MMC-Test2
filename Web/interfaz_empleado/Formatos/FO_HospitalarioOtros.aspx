<%@ Register TagPrefix="uc1" TagName="WC_EncabezadoFormato" Src="../WebControls/WC_EncabezadoFormato.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_AnotacionesFijasFormato" Src="../WebControls/WC_AnotacionesFijasFormato.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_PieFormato" Src="../WebControls/WC_PieFormato.ascx" %>
<%@ Page language="c#" Codebehind="FO_HospitalarioOtros.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_empleado.formatos.FO_HospitalarioOtros" %>
<%@ Register TagPrefix="uc1" TagName="WC_DiagnosticosFormato" Src="../WebControls/WC_DiagnosticosFormato.ascx" %>
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
	<BODY bottomMargin="5" leftMargin="1" topMargin="1" rightMargin="1">
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
									<TD vAlign="top" align="right"><asp:datagrid id="dtgDetalle" runat="server" Width="100%" CssClass="gridFormato" CellPadding="1"
											AllowPaging="False" AutoGenerateColumns="False" GridLines="Horizontal" PageSize="20">
											<AlternatingItemStyle CssClass="altItemsFormato"></AlternatingItemStyle>
											<ItemStyle Height="19px" CssClass="norItemsFormato"></ItemStyle>
											<HeaderStyle CssClass="headerGridFormato"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="Servicio/Producto">
													<ItemStyle Width="62%"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" ID="lblProductoServicio" text='<%# DataBinder.Eval(Container, "DataItem.NombreCompletoMedicamento").ToString() + DataBinder.Eval(Container, "DataItem.NombreCompletoServicio").ToString() %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Cantidad">
													<ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
													<ItemTemplate>
														<asp:Label text='<%# DataBinder.Eval(Container, "DataItem.Cantidad") %>' ID="lblCantidadProducto" Runat="server" >
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="UVR">
													<ItemStyle HorizontalAlign="Right" Width="15%"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" text='<%# !Convert.IsDBNull(DataBinder.Eval(Container, "DataItem.UVR")) && DataBinder.Eval(Container, "DataItem.UVR").ToString() != "0" ? string.Format("{0:0,0}",DataBinder.Eval(Container, "DataItem.UVR")) :  string.Format("{0:0,0}",DataBinder.Eval(Container, "DataItem.UVRConvenioSolicitado")) %>' ID="lblUVRProducto" >
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Valor">
													<ItemStyle HorizontalAlign="Right" Width="15%"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" text='<%# !Convert.IsDBNull(DataBinder.Eval(Container, "DataItem.ValorAprobado")) && DataBinder.Eval(Container, "DataItem.ValorAprobado").ToString() != "0" ? string.Format("{0:0,0}",DataBinder.Eval(Container, "DataItem.ValorAprobado")) :  string.Format("{0:0,0}",DataBinder.Eval(Container, "DataItem.ValorConvenioSolicitado")) %>' ID="lblValorAprobado" >
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid>
										<TABLE class="tableFormato" id="Table15" height="25" cellSpacing="0" cellPadding="0" width="200"
											border="0">
											<TR>
												<TD><STRONG>TOTAL</STRONG></TD>
												<TD align="right"><asp:label id="lblTotal" runat="server" CssClass="lblFormato"></asp:label></TD>
												<TD width="5"></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD>
										<TABLE class="tableBorderFormato" id="Table13" height="15" cellSpacing="0" cellPadding="0"
											width="100%" border="0">
											<TR>
												<TD class="tdEncabezadoFormato" align="center" width="10%" colSpan="6"><STRONG>OBSERVACIONES</STRONG></TD>
											</TR>
										</TABLE>
										<TABLE class="tableBorderFormato" id="Table14" height="70" cellSpacing="0" cellPadding="0"
											width="100%" border="0">
											<TR>
												<TD class="textFormatoSmall" vAlign="top" align="center" width="10%" colSpan="6"><asp:label id="lblObservaciones" runat="server"></asp:label><BR>
													<uc1:wc_anotacionesfijasformato id="WC_AnotacionesFijasFormato1" runat="server"></uc1:wc_anotacionesfijasformato><BR>
													BP no se hace cargo de gastos por :
													<BR>
													Suministro o alquiler de prótesis o de aparatos o equipos médicos, 
													hospitalarios y ortopédicos<BR>
													Servicio de Cafetería, llamadas telefónicas, Parqueadero, Cama de acompañante 
													en mayores de 16 años
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD height="10"></TD>
								</TR>
								<TR>
									<TD>
										<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR>
												<TD align="left" width="20%">
													<TABLE class="tableBorderFormato" id="Table6" width="100%" border="0">
														<TR>
															<TD vAlign="top" height="40">Servicio Autorizado por</TD>
														</TR>
														<TR>
															<TD align="center">________________________________<BR>
																<asp:label id="lblUnidadAprobacion" runat="server"></asp:label></TD>
														</TR>
														<TR>
															<TD height="15"></TD>
														</TR>
													</TABLE>
												</TD>
												<TD width="30"></TD>
												<TD align="center" width="45%">
													<TABLE class="tableBorderFormato" id="Table9" width="100%" border="0">
														<TR>
															<TD vAlign="top" height="40"></TD>
														</TR>
														<TR>
															<TD align="center">________________________________<BR>
																Firma del Paciente</TD>
														</TR>
														<TR>
															<TD vAlign="bottom" height="15">CC</TD>
														</TR>
													</TABLE>
												</TD>
												<TD width="30"></TD>
												<TD align="right" width="12%">
													<TABLE class="tableBorderFormato" id="Table8" width="100%" border="0">
														<TR>
															<TD vAlign="bottom" align="center" height="40"><asp:label id="lblValorTotal" runat="server" CssClass="lblFormato"></asp:label></TD>
														</TR>
														<TR>
															<TD align="center">_________________<BR>
																Valor Total</TD>
														</TR>
														<TR>
															<TD height="15"></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD class="textFormatoSmall" style="HEIGHT: 58px" height="58">VIGENCIA DE&nbsp;
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
