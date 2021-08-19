<%@ Page language="c#" Codebehind="FO_MedicamentosOrden.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_empleado.formatos.FO_MedicamentosOrden" %>
<%@ Register TagPrefix="uc1" TagName="WC_PieFormatoOrden" Src="../WebControls/WC_PieFormatoOrden.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_EncabezadoFormatoOrden" Src="../WebControls/WC_EncabezadoFormatoOrden.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_PieFormato" Src="../WebControls/WC_PieFormato.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_AnotacionesFijasFormato" Src="../WebControls/WC_AnotacionesFijasFormato.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_DiagnosticosFormato" Src="../WebControls/WC_DiagnosticosFormato.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_EncabezadoFormato" Src="../WebControls/WC_EncabezadoFormato.ascx" %>
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
									<TD><uc1:wc_encabezadoformatoorden id="WC_EncabezadoFormatoOrden1" runat="server"></uc1:wc_encabezadoformatoorden></TD>
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
									<TD align="right"><asp:datagrid id="dtgDetalle" runat="server" PageSize="20" GridLines="Horizontal" AutoGenerateColumns="False"
											AllowPaging="False" CellPadding="1" CssClass="gridFormato" Width="100%">
											<AlternatingItemStyle CssClass="altItemsFormato"></AlternatingItemStyle>
											<ItemStyle Height="19px" CssClass="norItemsFormato"></ItemStyle>
											<HeaderStyle CssClass="headerGridFormato"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="Medicamento">
													<ItemStyle Width="35%"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" ID="lblProductoServicio" text='<%# DataBinder.Eval(Container, "DataItem.NombreCompletoMedicamento").ToString() + DataBinder.Eval(Container, "DataItem.NombreCompletoServicio").ToString() + " <br/>" + DataBinder.Eval(Container, "DataItem.Comentarios").ToString() %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Dosis/Posología">
													<ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
													<ItemTemplate>
														<asp:Label text='<%# DataBinder.Eval(Container, "DataItem.Dosis") %>' ID="lblDosis" Runat="server" >
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Vía de Administración">
													<ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
													<ItemTemplate>
														<asp:Label text='<%# DataBinder.Eval(Container, "DataItem.ViaAdministracion") %>' ID="lblViaAdministracion" Runat="server" >
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Duración">
													<ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
													<ItemTemplate>
														<asp:Label text='<%# DataBinder.Eval(Container, "DataItem.Duracion") %>' ID="Label1" Runat="server" >
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Cantidad">
													<ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
													<ItemTemplate>
														<asp:Label text='<%# DataBinder.Eval(Container, "DataItem.Cantidad") %>' ID="lblCantidadProducto" Runat="server" >
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Valor">
													<ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" text='<%# string.Format("{0:0,0}",DataBinder.Eval(Container, "DataItem.ValorAprobado")) %>' ID="lblValorAprobado" >
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid>
										<TABLE class="tableFormato" id="Table15" height="25" cellSpacing="0" cellPadding="0" width="200"
											border="0">
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
												<TD class="tdEncabezadoFormato" align="center" width="10%" colSpan="6"><asp:label id="lblTituloObservaciones" runat="server">OBSERVACIONES</asp:label><STRONG></STRONG></TD>
											</TR>
										</TABLE>
										<TABLE class="tableBorderFormato" id="Table14" height="50" cellSpacing="0" cellPadding="0"
											width="100%" border="0">
											<TR>
												<TD vAlign="top" align="center" width="10%" colSpan="6">
													<P align="left"><asp:label id="lblObservaciones" runat="server"></asp:label><BR>
														<uc1:wc_anotacionesfijasformato id="WC_AnotacionesFijasFormato1" runat="server"></uc1:wc_anotacionesfijasformato></P>
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
										<table id="Table5" border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td align="left" width="30%">
                                                    <table id="Table6" border="0" class="tableBorderFormato" width="100%">
                                                        <tr>
                                                            <td height="25" valign="top">
                                                                Servicio Autorizado por</td>
                                                        </tr>
                                                        <tr>
                                                            <td align="middle">
                                                                ________________________________<br />
                                                                <asp:Label ID="lblMedico" runat="server"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="lblIdentificacion" runat="server"></asp:Label>
                                                                <br />
													            <asp:Label id="lblInstitucion" runat="server"></asp:Label>
                                                                <br />
													            <asp:Label id="lblCedula" runat="server"></asp:Label>
													            <br />
													
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="5">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="30">
                                                </td>
                                                <td align="middle" width="45%">
                                                    <table id="Table9" border="0" class="tableBorderFormato" width="100%">
                                                        <tr>
                                                            <td height="25" valign="top">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="middle">
                                                                ________________________________<br />
                                                                Firma del Paciente</td>
                                                        </tr>
                                                        <tr>
                                                            <td height="25" valign="bottom">
                                                                CC<br />
                                                                <br />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="30">
                                                </td>
                                                <td align="right" width="12%">
                                                    <table id="Table8" border="0" class="tableBorderFormato" width="100%">
                                                        <tr>
                                                            <td height="25" valign="top">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="middle">
                                                                _________________<br />
                                                                Valor Total</td>
                                                        </tr>
                                                        <tr>
                                                            <td height="25">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
									</TD>
								</TR>
								<TR>
									<TD class="textFormatoSmall" height="60"><asp:label id="lblTextoCiudad" runat="server"></asp:label><BR>
										VIGENCIA DE&nbsp;
										<asp:label id="lblVigencia" runat="server"></asp:label>&nbsp;DÍAS A PARTIR DE LA 
										FECHA DE EXPEDICIÓN
										<BR>
									</TD>
								</TR>
								<TR>
									<TD><uc1:wc_pieformatoorden id="WC_PieFormatoOrden1" runat="server"></uc1:wc_pieformatoorden></TD>
								</TR>
				</TR>
			</TABLE>
			</TD></TR></TBODY></TABLE></form>
	</BODY>
</HTML>
