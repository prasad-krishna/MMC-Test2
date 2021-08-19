<%@ Page language="c#" Codebehind="AE_servicio.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_admon.forma.AE_servicio" ValidateRequest="true"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>HC-Historias Clínicas</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../css/admon.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../../scripts/Base.js" type="text/javascript"></script>
	</HEAD>
	<body onload="CargarConfiguracion()">
		<form id="Form1" method="post" runat="server">
			<TABLE id="tblPrincipal" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td>
						<TABLE class="tableBorder" id="table6" width="85%" align="center">
							<tr>
								<td>
									<TABLE id="Table1" cellSpacing="0" cellPadding="3" width="100%" border="0">
										<TR>
											<TD class="titleBackBlue" colSpan="4">
												SERVICIO</TD>
										</TR>
										<TR>
											<TD colSpan="4"></TD>
										</TR>
										<TR>
											<TD width="15%" style="HEIGHT: 25px">Nombre <span class="textRed">*</span></TD>
											<TD width="35%" style="HEIGHT: 25px" colSpan="3">
												<asp:textbox id="txtNombre" runat="server" CssClass="textBox" Width="500px"></asp:textbox>
												<asp:RequiredFieldValidator id="rfvNombre" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
													ControlToValidate="txtNombre" ErrorMessage="Requerido"></asp:RequiredFieldValidator></TD>
										</TR>
										<TR>
											<TD>
												Código de servicio<SPAN class="textRed">*</SPAN></TD>
											<TD><asp:textbox id="txtCodigoServicio" runat="server" Width="150px" CssClass="textBox"></asp:textbox>
												<asp:RequiredFieldValidator id="rfvCodigoServicio" runat="server" CssClass="textRed" ErrorMessage="Requerido"
													ControlToValidate="txtCodigoServicio" Display="Dynamic" ForeColor=" "></asp:RequiredFieldValidator>
											</TD>
											<TD>Valor convenio ($)</TD>
											<TD class="list"><asp:textbox id="txtValorConvenio" onkeypress="return currencyFormat(this,event,true,false)"
													runat="server" Width="150px" CssClass="textBox"></asp:textbox>
											</TD>
										</TR>
										<TR>
											<TD>
												Incluye
											</TD>
											<TD><asp:textbox id="txtIncluye" runat="server" Width="250px" CssClass="textBox" TextMode="MultiLine"></asp:textbox>
											</TD>
											<TD>Excluye</TD>
											<TD class="list"><asp:textbox id="txtExcluye" runat="server" Width="250px" CssClass="textBox" TextMode="MultiLine"></asp:textbox>
											</TD>
										</TR>
										<TR>
											<TD>
												Simultáneo</TD>
											<TD>
												<asp:TextBox ID="txtSimultaneo" runat="server" Width="250px" CssClass="textBox" TextMode="MultiLine"></asp:TextBox>
											</TD>
											<TD>Activo</TD>
											<TD class="list">
												<asp:CheckBox ID="chkActivo" runat="server" Checked="True" />
											</TD>
										</TR>
										<TR>
											<TD>
                                                <asp:Label ID="lblTipoServicio" runat="server" 
                                                    Text="Tipo de servicio asociado (únicamente si quiere que se despliegue a un tipo de servicio especial)"></asp:Label>
                                            </TD>
											<TD colSpan="3">
												<asp:CheckBoxList id="chkTipoServicio" runat="server" RepeatColumns="3"></asp:CheckBoxList></TD>
										</TR>
										<TR>
											<TD class="textSmallBlack" colSpan="4" height="20">Los campos marcados con (<SPAN class="textRed">*</SPAN>) 
												son obligatorios</TD>
										</TR>
										<TR>
											<TD align="center" colSpan="4"><asp:button id="btnAceptar" runat="server" CssClass="button" Text="Aceptar"></asp:button></TD>
										</TR>
									</TABLE>
								</td>
							</tr>
						</TABLE>
					</td>
				</tr>
			</TABLE>
			&nbsp;
		</form>
	</body>
</HTML>
