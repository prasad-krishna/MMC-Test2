<%@ Register TagPrefix="uc1" TagName="WC_EncabezadoFormato" Src="../WebControls/WC_EncabezadoFormato.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_AnotacionesFijasFormato" Src="../WebControls/WC_AnotacionesFijasFormato.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_PieFormato" Src="../WebControls/WC_PieFormato.ascx" %>
<%@ Page language="c#" Codebehind="FO_Reembolsos.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_empleado.formatos.FO_Reembolsos" %>
<%@ Register TagPrefix="uc1" TagName="WC_EncabezadoFormatoReembolso" Src="../WebControls/WC_EncabezadoFormatoReembolso.ascx" %>
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
	<BODY bottomMargin="5" topMargin="1" leftMargin="1" rightMargin="1">
		<form id="Form1" method="post" runat="server">
			<TABLE runat="server" class="tableFormato" id="tblPrincipal" cellSpacing="0" cellPadding="0"
				width="650" border="0">
				<TR>
					<TD align="center">
						<TABLE class="tableFormato" id="Table1" cellSpacing="0" cellPadding="0" width="620" border="0">
							<TBODY>
								<TR>
									<TD>
										<uc1:WC_EncabezadoFormatoReembolso id="WC_EncabezadoFormatoReembolso1" runat="server"></uc1:WC_EncabezadoFormatoReembolso></TD>
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
									<TD vAlign="top" align="right"><asp:datagrid id="dtgDetalle" runat="server" GridLines="Horizontal" AutoGenerateColumns="False"
											AllowPaging="False" CellPadding="2" CssClass="gridFormato" Width="100%">
											<AlternatingItemStyle CssClass="altItemsFormato"></AlternatingItemStyle>
											<ItemStyle Height="19px" CssClass="norItemsFormato"></ItemStyle>
											<HeaderStyle CssClass="headerGridFormato"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="Servicio/Producto">
													<ItemStyle Width="41%"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" ID="lblProductoServicio" text='<%# DataBinder.Eval(Container, "DataItem.NombreCompletoMedicamento").ToString() + DataBinder.Eval(Container, "DataItem.NombreCompletoServicio").ToString() +  " " + DataBinder.Eval(Container, "DataItem.Comentarios").ToString() %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Cantidad">
													<ItemStyle HorizontalAlign="Center" Width="7%"></ItemStyle>
													<ItemTemplate>
														<asp:Label text='<%# DataBinder.Eval(Container, "DataItem.Cantidad") %>' ID="lblCantidadProducto" Runat="server" >
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="UVR">
													<ItemStyle HorizontalAlign="Right" Width="7%"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" text='<%# !Convert.IsDBNull(DataBinder.Eval(Container, "DataItem.UVR")) && DataBinder.Eval(Container, "DataItem.UVR").ToString() != "0" ? string.Format("{0:0,0}",DataBinder.Eval(Container, "DataItem.UVR")) :  string.Format("{0:0,0}",DataBinder.Eval(Container, "DataItem.UVRConvenioSolicitado")) %>' ID="lblUVRProducto" >
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Valor Presentado">
													<ItemStyle HorizontalAlign="Right" Width="15%"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" text='<%# !Convert.IsDBNull(DataBinder.Eval(Container, "DataItem.ValorConvenioSolicitado")) && DataBinder.Eval(Container, "DataItem.ValorConvenioSolicitado").ToString() != "0" ? string.Format("{0:0,0}",DataBinder.Eval(Container, "DataItem.ValorConvenioSolicitado")) : ""%>' ID="lblValorSolicitado" >
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Valor Descontado">
													<ItemStyle HorizontalAlign="Right" Width="15%"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" text='<%# !Convert.IsDBNull(DataBinder.Eval(Container, "DataItem.ValorAprobado")) && !Convert.IsDBNull(DataBinder.Eval(Container, "DataItem.ValorConvenioSolicitado")) && (Convert.ToDecimal(DataBinder.Eval(Container, "DataItem.ValorConvenioSolicitado")) - Convert.ToDecimal(DataBinder.Eval(Container, "DataItem.ValorAprobado"))).ToString() != "0" ?  string.Format("{0:0,0}",(Convert.ToDecimal(DataBinder.Eval(Container, "DataItem.ValorConvenioSolicitado")) - Convert.ToDecimal(DataBinder.Eval(Container, "DataItem.ValorAprobado")))) : "" %>' ID="lblValorDescontado" >
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Valor Aprobado">
													<ItemStyle HorizontalAlign="Right" Width="15%"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" text='<%# !Convert.IsDBNull(DataBinder.Eval(Container, "DataItem.ValorAprobado")) && DataBinder.Eval(Container, "DataItem.ValorAprobado").ToString() != "0" ? string.Format("{0:0,0}",DataBinder.Eval(Container, "DataItem.ValorAprobado")) : ""%>' ID="lblValorAprobado">
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid>
										<TABLE class="tableFormato" id="Table15" height="40" cellSpacing="0" cellPadding="0" width="365"
											border="0">
											<TR>
												<TD><STRONG>TOTALES</STRONG></TD>
												<TD align="right" width="172">
													<asp:label id="lblTotalPresentado" runat="server" CssClass="lblFormato"></asp:label></TD>
												<TD align="right" width="172">
													<asp:label id="lblTotalDescontado" runat="server" CssClass="lblFormato"></asp:label></TD>
												<TD align="right" width="172"><asp:label id="lblTotalAprobado" runat="server" CssClass="lblFormato"></asp:label></TD>
												<TD width="5"></TD>
											</TR>
											<TR>
												<TD>&nbsp;</TD>
												<TD align="right" width="172"></TD>
												<TD align="right" width="172"></TD>
												<TD align="right" width="172"></TD>
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
												<TD align="center" class="tdEncabezadoFormato" width="10%" colSpan="6"><STRONG>OBSERVACIONES</STRONG></TD>
											</TR>
										</TABLE>
										<TABLE class="tableBorderFormato" id="Table14" height="70" cellSpacing="0" cellPadding="0"
											width="100%" border="0">
											<TR>
												<TD class="textFormatoSmall" vAlign="top" align="center" width="10%" colSpan="6"><asp:label id="lblObservaciones" runat="server"></asp:label><BR>
													<uc1:wc_anotacionesfijasformato id="WC_AnotacionesFijasFormato1" runat="server"></uc1:wc_anotacionesfijasformato>
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD height="5"></TD>
								</TR>
								<TR>
									<TD height="10">
										<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR>
												<TD align="left" width="75%">
													<TABLE class="tableBorderFormato" id="Table6" width="100%" border="0">
														<TR>
															<TD vAlign="top" height="10"></TD>
														</TR>
														<TR>
															<TD vAlign="bottom" height="30">&nbsp;Servicio Autorizado por 
																________________________________
																<asp:label id="lblUnidadAprobacion" runat="server"></asp:label></TD>
														</TR>
														<TR>
															<TD align="left" height="30" vAlign="bottom">&nbsp;Fecha 
																_________________________________</TD>
														</TR>
														<TR>
															<TD height="10"></TD>
														</TR>
													</TABLE>
												</TD>
												<TD width="10"></TD>
												<TD align="right" width="20%">
													<TABLE class="tableBorderFormato" id="Table8" width="100%" border="0">
														<TR>
															<TD vAlign="bottom" align="center" height="20">
																<asp:label id="lblValorTotal" runat="server" CssClass="lblFormato"></asp:label></TD>
														</TR>
														<TR>
															<TD align="center" height="30">_________________<BR>
																Total a Reembolsar</TD>
														</TR>
														<TR>
															<TD height="30"></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD>
									</TD>
								</TR>
								<TR>
									<TD class="textFormatoSmall" height="30"></TD>
								</TR>
								<TR>
									<TD><uc1:wc_pieformato id="WC_PieFormato1" runat="server"></uc1:wc_pieformato></TD>
								</TR>
				</TR>
			</TABLE>
			</TD></TR></TBODY></TABLE></form>
	</BODY>
</HTML>
