<%@ Page language="c#" Codebehind="FO_IncapacidadOrden.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_empleado.formatos.FO_IncapacidadOrden" %>
<%@ Register TagPrefix="uc1" TagName="WC_PieFormatoOrden" Src="../WebControls/WC_PieFormatoOrden.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_EncabezadoFormatoIncapacidad" Src="../WebControls/WC_EncabezadoFormatoIncapacidad.ascx" %>
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
	<BODY bottomMargin="5" topMargin="1" leftMargin="1" rightMargin="5">
		<form id="Form1" method="post" runat="server">
			<TABLE runat="server" class="tableFormato" id="tblPrincipal" cellSpacing="0" cellPadding="0"
				width="650" border="0">
				<TR>
					<TD align="center">
						<TABLE runat="server" class="tableFormato" id="tblInterior" cellSpacing="0" cellPadding="0"
							width="620" border="0">
							<TBODY>
								<TR>
									<TD>
										<uc1:WC_EncabezadoFormatoIncapacidad id="WC_EncabezadoFormatoIncapacidad1" runat="server"></uc1:WC_EncabezadoFormatoIncapacidad></TD>
								</TR>
								<TR>
									<TD height="60" vAlign="middle">
										<TABLE class="tableBorderFormato" id="Table10" height="15" cellSpacing="0" cellPadding="0"
											width="100%" border="0">
											<TR>
												<TD class="tdEncabezadoFormato" align="center" width="10%" colSpan="6"><STRONG>SERVICIO 
														SOLICITADO</STRONG></TD>
											</TR>
										</TABLE>
										<BR>
										Tipo de Enfermedad:&nbsp;
										<asp:Label id="lblTipoEnfermedad" runat="server"></asp:Label>
									</TD>
								</TR>
								<TR>
									<TD align="left">
										<asp:DataGrid id="dtgDiagnosticos" Width="100%" CellPadding="1" AllowPaging="False" AutoGenerateColumns="False"
											GridLines="Horizontal" CssClass="gridFormato" runat="server">
											<AlternatingItemStyle CssClass="altItemsFormato"></AlternatingItemStyle>
											<ItemStyle CssClass="norItemsFormato"></ItemStyle>
											<HeaderStyle CssClass="headerGridFormato"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="C&#243;digo Diagn&#243;stico">
													<ItemStyle Width="20%"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" ID="lblDiagnostico" text='<%# DataBinder.Eval(Container, "DataItem.CodigoDiagnostico").ToString()%>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Tiempo Evoluci&#243;n">
													<ItemStyle HorizontalAlign="Center" Width="20%"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" ID="lblEvolucion" text='<%# DataBinder.Eval(Container, "DataItem.TiempoEvolucion").ToString() + " " + DataBinder.Eval(Container, "DataItem.PeriodoEvolucion").ToString() %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:DataGrid>
									</TD>
								</TR>
								<TR>
									<TD align="left">
										<TABLE class="gridFormato" id="Table1" cellSpacing="0" cellPadding="0" border="1" style="BORDER-COLLAPSE: collapse"
											width="100%">
											<TR class="headerGridFormato">
												<TD>Fecha de Inicio</TD>
												<TD>Fecha de Fin</TD>
												<TD>Días Incapacidad</TD>
												<TD>Continuación</TD>
												<TD>Transcripción</TD>
											</TR>
											<TR class="norItemsFormato">
												<TD align="center">
													<asp:Label id="lblFechaInicio" runat="server"></asp:Label></TD>
												<TD align="center">
													<asp:Label id="lblFechaFin" runat="server"></asp:Label></TD>
												<TD align="center">
													<asp:Label id="lblDias" runat="server"></asp:Label></TD>
												<TD align="center">
													<asp:Label id="lblContinuacion" runat="server"></asp:Label></TD>
												<TD align="center">
													<asp:Label id="lblTranscripcion" runat="server"></asp:Label></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD align="right">
									</TD>
								</TR>
								<TR>
									<TD>
										<TABLE class="tableBorderFormato" id="Table13" height="15" cellSpacing="0" cellPadding="0"
											width="100%" border="0">
											<TR>
												<TD class="tdEncabezadoFormato" align="center" width="10%" colSpan="6"><asp:Label id="lblTituloObservaciones" runat="server">OBSERVACIONES</asp:Label><STRONG></STRONG></TD>
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
										<TABLE class="tableBorderFormato" id="Table6" width="100%" border="0" cellPadding="5">
											<TR>
												<TD vAlign="top" height="10"></TD>
											</TR>
											<TR>
												<TD vAlign="top">
													<table id="Table5" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td align="left" width="60%">
                                                                <table id="Table2" border="0" class="tableBorderFormato" width="100%">
                                                                    <tr>
                                                                        <td height="25" valign="top">
                                                                            Solicitado por:</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="middle">
                                                                            ___________________________________________________<br />
                                                                            <asp:Label ID="lblMedico" runat="server"></asp:Label>
                                                                            <br />
                                                                            <asp:Label ID="lblIdentificacion" runat="server"></asp:Label>
                                                                            <br />
													            <asp:Label id="lblInstitucion" runat="server"></asp:Label>
                                                                            <br />
													<asp:Label id="lblCedula" runat="server">Céd. Prof.: </asp:Label>
                                                                            &nbsp;
                                                                            
                                                                            <br />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td height="15">
                                                                            Nombre del Médico: ______________________________________</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td height="15">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td height="15">
													<asp:Label id="lblRegistroMedico" runat="server">Registro Médico No. ________________________________</asp:Label></td>
                                                                    </tr>
                                                                    </table>
                                                            </td>
                                                            <td width="30">
                                                            </td>
                                                            <td align="middle" width="35%">
                                                                <table id="Table9" border="0" class="tableBorderFormato" width="100%">
                                                                    <tr>
                                                                        <td height="25" valign="top">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="middle">
                                                                            __________________________________<br />
                                                                            Firma del Paciente<br />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td height="25" valign="bottom">
                                                                            CC</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td height="25" valign="bottom">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td height="25" valign="bottom">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td width="30">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </TD>
											</TR>
											<TR>
												<TD height="25">&nbsp;</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD class="textFormatoSmall" height="60">
										<P><asp:label ID="lblFooterIncapacidad" runat="server">Original: Departamento administración salarios<BR>
											Copia: División médica&nbsp;ocupacional<BR>										
											Es responsabilidad de usted llamar a control de vuelo para reportar su 
											incapacidad</asp:Label><BR>
										</P>
									</TD>
								</TR>
								<TR>
									<TD>
										<uc1:WC_PieFormatoOrden id="WC_PieFormatoOrden1" runat="server"></uc1:WC_PieFormatoOrden></TD>
								</TR>
				</TR>
			</TABLE></TD></TR></TBODY></TABLE>
		</form>
	</BODY>
</HTML>
