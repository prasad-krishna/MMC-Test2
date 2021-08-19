<%@ Page language="c#" Codebehind="FO_RecomendacionesOrden.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_empleado.formatos.FO_RecomendacionesOrden" %>
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
									<TD align="left">
									</TD>
								</TR>
								<TR>
									<TD>
										<TABLE class="tableBorderFormato" id="Table13" height="15" cellSpacing="0" cellPadding="0"
											width="100%" border="0">
											<TR>
												<TD class="tdEncabezadoFormato" align="center" width="10%" colSpan="6"><STRONG>RECOMENDACIONES</STRONG></TD>
											</TR>
										</TABLE>
										<TABLE class="tableBorderFormato" id="Table14" height="150" cellSpacing="0" cellPadding="0"
											width="100%" border="0">
											<TR>
												<TD vAlign="top" align="center" width="10%" colSpan="6">
													<P align="left">
														<asp:label id="lblRecomendaciones" runat="server"></asp:label><BR>
													    <br />                                                     
                                                          <asp:BulletedList ID="btlRecomendaciones" DataTextField="Descripcion" 
                                                        runat="server" style="text-align: left">
                                                                                            
                                                          </asp:BulletedList>
													</P>
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD>
										<TABLE class="tableBorderFormato" id="Table6" cellPadding="5" width="100%" border="0">
											<TR>
												<TD vAlign="top" height="10"></TD>
											</TR>
											<TR>
												<TD align="left">________________________________<BR>
													<asp:Label id="lblMedico" runat="server"></asp:Label><BR>
													<asp:Label id="lblIdentificacion" runat="server"></asp:Label>
                                                    <br />
													            <asp:Label id="lblInstitucion" runat="server"></asp:Label>
                                                    <br />
													<asp:Label id="lblCedula" runat="server">Céd. Prof.: </asp:Label>
                                                                            &nbsp;
													            
                                                                            </TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD>
										<uc1:WC_PieFormatoOrden id="WC_PieFormatoOrden1" runat="server"></uc1:WC_PieFormatoOrden></TD>
								</TR>
				</TR>
			</TABLE>
			</TD></TR></TBODY></TABLE>
		</form>
	</BODY>
</HTML>
