<%@ Page language="c#" Codebehind="AE_proveedorprestador.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_admon.forma.AE_proveedorprestador" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title></title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../css/admon.css" type="text/css" rel="stylesheet">
		<LINK href="../../css/Calendar.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../../scripts/Base.js" type="text/javascript"></script>
		<script language="javascript" src="../../scripts/Calendar.js" type="text/javascript"></script>
	</HEAD>
	<body onload="CargarConfiguracion()">
		<form id="Form1" method="post" runat="server">
			<TABLE class="tableBorder" id="table6" width="98%" align="center">
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="0" cellPadding="5" width="100%" border="0">
							<TR>
								<TD class="titleBackBlue" colSpan="4">Solicitante / Prestadores</TD>
							</TR>
							<TR>
								<TD colSpan="4"></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 29px" width="17%">Adicionar <SPAN class="textRed">*</SPAN></TD>
								<TD style="HEIGHT: 29px" width="36%">Prestador<INPUT id="checkproveedor" type="checkbox" value="1" name="checkproveedor" runat="server">
									Solicitante<INPUT id="checkprestador" type="checkbox" value="2" name="checkprestador" runat="server">
								</TD>
								<TD style="HEIGHT: 29px" width="15%">Código Anterior</TD>
								<TD style="HEIGHT: 29px" align="left" width="32%"><asp:textbox id="txtTpaAnterior" runat="server" CssClass="textBox" Width="100px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD width="15%">Nombre <SPAN class="textRed">*</SPAN></TD>
								<TD width="35%"><asp:textbox id="txtNombre" runat="server" CssClass="textBox" Width="200px"></asp:textbox><asp:requiredfieldvalidator id="rfvNombre" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
										ControlToValidate="txtNombre" ErrorMessage="Requerido"></asp:requiredfieldvalidator></TD>
								<TD width="15%">Nit</TD>
								<TD align="left" width="35%"><asp:textbox id="txtNit" runat="server" CssClass="textBox" Width="150px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>Especialidad</TD>
								<TD><asp:dropdownlist id="ddlTipoProveedor" runat="server" CssClass="textBox" Width="200px"></asp:dropdownlist></TD>
								<TD>Supraespecialidad</TD>
								<TD><asp:textbox id="txtSupraespecialidad" runat="server" CssClass="textBox" Width="180px" TextMode="MultiLine"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>Teléfono</TD>
								<TD><asp:textbox id="txtTelefonos" runat="server" CssClass="textBox" Width="150px"></asp:textbox>&nbsp;
								</TD>
								<TD>Dirección</TD>
								<TD><asp:textbox id="txtDirecciones" runat="server" CssClass="textBox" Width="180px" TextMode="MultiLine"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>Horario</TD>
								<TD><asp:textbox id="txtHorario" runat="server" CssClass="textBox" Width="200px"></asp:textbox></TD>
								<TD>Fax</TD>
								<TD><asp:textbox id="txtFax" runat="server" CssClass="textBox" Width="150px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>Email</TD>
								<TD><asp:textbox id="txtEmail" runat="server" CssClass="textBox" Width="200px"></asp:textbox></TD>
								<TD>Ciudad <SPAN class="textRed">*</SPAN></TD>
								<TD><asp:dropdownlist id="ddlCiudad" runat="server" CssClass="textBox" Width="150px"></asp:dropdownlist><asp:comparevalidator id="cmvCiudad" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
										ControlToValidate="ddlCiudad" ErrorMessage="Requerido" Operator="NotEqual" ValueToCompare="0"></asp:comparevalidator></TD>
							</TR>
							<TR>
								<TD>
									<P>Fecha de ingreso<SPAN class="textRed">*</SPAN></P>
								</TD>
								<TD><asp:textbox id="txtFechaIngreso" runat="server" CssClass="textBox" Width="80px"></asp:textbox>&nbsp;<A href="javascript:MostrarCalendario(Form1.txtFechaIngreso,Form1.txtFechaIngreso,'dd/mm/yyyy');"
										name="btnFechaIngreso"><IMG src="../../images/icoCalendar.gif" border="0"></A>&nbsp;
									<asp:requiredfieldvalidator id="rfvFecha" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
										ControlToValidate="txtFechaIngreso" ErrorMessage="Requerido"></asp:requiredfieldvalidator>&nbsp;</TD>
								<TD>Persona que autoriza el ingreso</TD>
								<TD><asp:textbox id="txtPersonaIngreso" runat="server" CssClass="textBox" Width="180px" MaxLength="100"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>Retirado de Empresa</TD>
								<TD><asp:checkbox id="chkRetirar" runat="server" AutoPostBack="True"></asp:checkbox></TD>
								<TD><asp:label id="lblMotivo" runat="server" Visible="False">Motivo Retiro</asp:label></TD>
								<TD><asp:textbox id="txtMotivoRetiro" runat="server" CssClass="textBox" Width="180px" TextMode="MultiLine"
										Visible="False" MaxLength="100"></asp:textbox><asp:requiredfieldvalidator id="rfvMotivoRetiro" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
										ControlToValidate="txtMotivoRetiro" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR id="trRetiro" style="DISPLAY: none" runat="server">
								<TD>Fecha de retiro<SPAN class="textRed">*</SPAN></TD>
								<TD>
									<P><asp:textbox id="txtFechaRetiro" runat="server" CssClass="textBox" Width="80px"></asp:textbox>&nbsp;<A href="javascript:MostrarCalendario(Form1.txtFechaRetiro,Form1.txtFechaRetiro,'dd/mm/yyyy');"
											name="btnFechaRetiro"><IMG src="../../images/icoCalendar.gif" border="0"></A>&nbsp;
										<asp:requiredfieldvalidator id="rfvFechaRetiro" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
											ControlToValidate="txtFechaRetiro" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator>&nbsp;
									</P>
								</TD>
								<TD>Persona que autoriza el retiro</TD>
								<TD><asp:textbox id="txtPersonaRetiro" runat="server" CssClass="textBox" Width="180px" MaxLength="100"></asp:textbox><asp:requiredfieldvalidator id="rfvPersonaRetiro" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
										ControlToValidate="txtFechaRetiro" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD>N° Cédula profesional<SPAN class="textRed">*</SPAN>
								<!--<asp:label id="lblCedula" runat="server">N° Cédula profesional:</asp:label>-->
								</TD>
								<TD>
									<asp:textbox id="txtCedula" runat="server" CssClass="textBox" Width="200px" 
                                        MaxLength="200"></asp:textbox>
								    <asp:requiredfieldvalidator id="rfvCedula" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
										ControlToValidate="txtCedula" ErrorMessage="Requerido"></asp:requiredfieldvalidator>
								</TD>
								<TD>Institución que otorgó título profesional<SPAN class="textRed">*</SPAN></TD>
								<TD>
									<P>&nbsp;<asp:textbox id="txtInstitucion" runat="server" CssClass="textBox" Width="200px" 
                                        MaxLength="200"></asp:textbox>
								    <asp:requiredfieldvalidator id="rfvInstitucion" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
										ControlToValidate="txtInstitucion" ErrorMessage="Requerido"></asp:requiredfieldvalidator>
									</P>
								</TD>
							</TR>
							<TR>
								<TD>&nbsp;</TD>
								<TD>&nbsp;</TD>
								<TD>&nbsp;</TD>
								<TD>
									&nbsp;</TD>
							</TR>
							<TR>
								<TD class="textSmallBlack" colSpan="4" height="20">Los campos marcados con (<SPAN class="textRed">*</SPAN>) 
									son obligatorios
									<asp:dropdownlist id="ddlEstado" runat="server" CssClass="textBox" Width="150px" Visible="False"></asp:dropdownlist>
                                    <asp:textbox id="txtFechaExpedicion" runat="server" CssClass="textBox" 
                                        Width="80px" Visible="False"></asp:textbox></TD>
							</TR>
							<TR>
								<TD align="center" colSpan="4">
                                    <asp:button id="btnAceptar" runat="server" 
                                        CssClass="button" Text="Aceptar" onclick="btnAceptar_Click1"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
