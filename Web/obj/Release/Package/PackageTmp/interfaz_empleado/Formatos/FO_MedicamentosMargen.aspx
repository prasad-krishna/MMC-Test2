<%@ Register TagPrefix="uc1" TagName="WC_EncabezadoFormato" Src="../WebControls/WC_EncabezadoFormato.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_DiagnosticosFormato" Src="../WebControls/WC_DiagnosticosFormato.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_AnotacionesFijasFormato" Src="../WebControls/WC_AnotacionesFijasFormato.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_PieFormato" Src="../WebControls/WC_PieFormato.ascx" %>
<%@ Page language="c#" Codebehind="FO_MedicamentosMargen.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_empleado.formatos.FO_MedicamentosMargen" %>
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
	<BODY bottomMargin="5" topMargin="5">
		<form id="Form1" method="post" runat="server">
			<TABLE class="tableFormato" id="Table0" cellSpacing="0" cellPadding="0" width="680" border="0">
				<TR>
					<TD align="center">
						<TABLE runat="server" class="tableFormato" id="tblPrincipal" cellSpacing="0" cellPadding="0"
							width="640" border="0">
							<TBODY>
								<TR>
									<TD height="50"></TD>
								</TR>
								<TR>
									<TD>
										<uc1:WC_EncabezadoFormato id="WC_EncabezadoFormato1" runat="server"></uc1:WC_EncabezadoFormato></TD>
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
										<uc1:WC_DiagnosticosFormato id="WC_DiagnosticosFormato1" runat="server"></uc1:WC_DiagnosticosFormato>
									</TD>
								</TR>
								<TR>
									<TD align="right">
										<asp:DataGrid id="dtgDetalle" runat="server" Width="100%" CssClass="gridFormato" CellPadding="2"
											AllowPaging="False" AutoGenerateColumns="False" GridLines="Horizontal">
											<AlternatingItemStyle CssClass="altItemsFormato"></AlternatingItemStyle>
											<ItemStyle Height="19px" CssClass="norItemsFormato"></ItemStyle>
											<HeaderStyle CssClass="headerGridFormato"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="Servicio/Producto">
													<ItemStyle Width="50%"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" ID="lblProductoServicio" text='<%# DataBinder.Eval(Container, "DataItem.NombreCompletoMedicamento").ToString() + DataBinder.Eval(Container, "DataItem.NombreCompletoServicio").ToString() %>'>
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
												<asp:TemplateColumn HeaderText="Valor">
													<ItemStyle HorizontalAlign="Right" Width="20%"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" text='<%# string.Format("{0:0,0}",DataBinder.Eval(Container, "DataItem.ValorAprobado")) %>' ID="lblValorAprobado" >
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:DataGrid>
										<TABLE class="tableFormato" id="Table15" cellSpacing="0" cellPadding="0" width="200" border="0"
											height="25">
											<TR>
												<TD><STRONG>TOTAL</STRONG></TD>
												<TD></TD>
												<TD width="20"></TD>
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
										<TABLE class="tableBorderFormato" id="Table14" height="60" cellSpacing="0" cellPadding="0"
											width="100%" border="0">
											<TR>
												<TD vAlign="top" align="center" width="10%" colSpan="6">
													<P align="left">
														<asp:label id="lblObservaciones" runat="server"></asp:label><BR>
														<uc1:WC_AnotacionesFijasFormato id="WC_AnotacionesFijasFormato1" runat="server"></uc1:WC_AnotacionesFijasFormato></P>
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
												<TD align="left" width="30%">
													<TABLE class="tableBorderFormato" id="Table6" width="100%" border="0">
														<TR>
															<TD vAlign="top" height="50">Servicio Autorizado por</TD>
														</TR>
														<TR>
															<TD align="center">________________________________<BR>
																<asp:Label id="lblUnidadAprobacion" runat="server"></asp:Label></TD>
														</TR>
														<TR>
															<TD height="20"></TD>
														</TR>
													</TABLE>
												</TD>
												<TD width="30"></TD>
												<TD align="center" width="45%">
													<TABLE class="tableBorderFormato" id="Table9" width="100%" border="0">
														<TR>
															<TD vAlign="top" height="50"></TD>
														</TR>
														<TR>
															<TD align="center">________________________________<BR>
																Firma del Paciente</TD>
														</TR>
														<TR>
															<TD vAlign="bottom" height="20">CC</TD>
														</TR>
													</TABLE>
												</TD>
												<TD width="30"></TD>
												<TD align="right" width="12%">
													<TABLE class="tableBorderFormato" id="Table8" width="100%" border="0">
														<TR>
															<TD vAlign="top" height="50"></TD>
														</TR>
														<TR>
															<TD align="center">_________________<BR>
																Valor Total</TD>
														</TR>
														<TR>
															<TD height="20"></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD class="textFormatoSmall" height="60">
										<P>VIGENCIA DE&nbsp;
											<asp:Label id="lblVigencia" runat="server"></asp:Label>
											DÍAS A PARTIR DE LA FECHA DE EXPEDICIÓN
											<BR>
											<asp:Label id="lblTextoFormato" runat="server" CssClass="tdSmallFormato"></asp:Label></P>
									</TD>
								</TR>
								<TR>
									<TD>
										<uc1:WC_PieFormato id="WC_PieFormato1" runat="server"></uc1:WC_PieFormato></TD>
								</TR>
				</TR>
			</TABLE>
			</TD></TR></TBODY></TABLE>
		</form>
	</BODY>
</HTML>
