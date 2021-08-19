<%@ Control Language="c#" AutoEventWireup="false" Codebehind="WC_EncabezadoFormatoReembolso.ascx.cs" Inherits="TPA.interfaz_empleado.WebControls.WC_EncabezadoFormatoReembolso" %>
<TABLE class="tableFormato" id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD class="titleFormato">
			<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titleFormato" align="center">
						<asp:Label id="lblTitulo" CssClass="titleFormato" runat="server"></asp:Label></TD>
					<TD vAlign="middle" width="20">
						<TABLE class="tableFormato" id="Table2" cellSpacing="0" cellPadding="0" width="150" border="0">
							<TR>
								<TD><STRONG> No.</STRONG></TD>
								<TD><asp:textbox id="txtNoSolicitud" CssClass="textBoxBlackBig" Width="120px" runat="server"></asp:textbox></TD>
								<TD width="10"></TD>
							</TR>
						</TABLE>
					</TD>
					<TD width="20"><asp:image id="imgLogo" runat="server"></asp:image></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD height="5">
			<TABLE class="tableBorderFormato" id="tblOrden" height="40" cellSpacing="0" cellPadding="0"
				width="100%" border="0">
				<TR>
					<TD align="left" width="15%"><STRONG title="25%">REEMBOLSO DE</STRONG></TD>
					<TD width="23%"><asp:label id="lblOrden" CssClass="lblFormato" runat="server" Font-Bold="True"></asp:label></TD>
					<TD align="right" width="10%">FECHA</TD>
					<TD width="10%"><asp:label id="lblFecha" CssClass="lblFormato" runat="server"></asp:label></TD>
					<TD align="right" width="15%">EXPEDIDA POR</TD>
					<TD width="23%"><asp:label id="lblExpedidaPor" CssClass="lblFormato" runat="server"></asp:label></TD>
				</TR>
			</TABLE>
			<TABLE class="tableBorderFormato" id="Table4" height="40" cellSpacing="0" cellPadding="0"
				width="100%" border="0">
				<TBODY>
					<TR>
						<TD align="center" width="22%" colSpan="2"><STRONG>DATOS DEL EMPLEADO</STRONG></TD>
						<TD align="center" width="18%" colSpan="4"><STRONG>DATOS DEL PACIENTE</STRONG></TD>
					</TR>
					<TR>
						<TD align="left" width="16%"><STRONG>NOMBRE</STRONG></TD>
						<TD width="82%"><asp:label id="lblNombreEmpleado" CssClass="lblFormato" runat="server" Font-Bold="True"></asp:label></TD>
						<TD align="left" width="18%">NOMBRE</TD>
						<TD width="90%" colSpan="3"><asp:label id="lblNombrePaciente" CssClass="lblFormato" runat="server"></asp:label></TD>
					</TR>
					<TR>
						<TD align="left" width="16%"><STRONG>IDENTIFICACIÓN</STRONG></TD>
						<TD width="82%"><asp:label id="lblTipoIdentificacionEmpleado" CssClass="lblFormato" runat="server" Font-Bold="True"></asp:label>&nbsp;<asp:label id="lblNumeroEmpleado" CssClass="lblFormato" runat="server" Font-Bold="True"></asp:label></TD>
						<TD align="left">IDENTIFICACIÓN</TD>
						<TD width="15%"><asp:label id="lblTipoIdentificacion" CssClass="lblFormato" runat="server"></asp:label>&nbsp;
							<asp:label id="lblNumero" CssClass="lblFormato" runat="server"></asp:label></TD>
						<TD align="right" width="8%">PARENTESCO</TD>
						<TD align="left" width="10%"><asp:label id="lblParentesco" CssClass="lblFormato" runat="server"></asp:label></TD>
					</TR>
					<TR>
						<TD align="left" width="16%"><STRONG></STRONG></TD>
						<TD align="left" width="82%"><asp:label id="lblTelefonoEmpleado" CssClass="lblFormato" runat="server" Font-Bold="True" Visible="False"></asp:label></TD>
						<TD align="left">
							RESIDENCIA</TD>
						<TD><asp:label id="lblLugarResidencia" CssClass="lblFormato" runat="server"></asp:label></TD>
						<TD align="right">EDAD</TD>
						<TD align="left"><asp:label id="lblEdad" CssClass="lblFormato" runat="server"></asp:label></TD>
					</TR>
					<TR>
						<TD align="left" width="16%"></TD>
						<TD width="82%"></TD>
						<TD align="left">GÉNERO</TD>
						<TD><asp:label id="lblGenero" CssClass="lblFormato" runat="server"></asp:label></TD>
						<TD align="right"></TD>
						<TD align="left"><asp:label id="lblTelefonoPaciente" CssClass="lblFormato" runat="server" Visible="False"></asp:label></TD>
					</TR>
				</TBODY>
			</TABLE>
			<TABLE class="tableBorderFormato" id="Table3" cellSpacing="0" cellPadding="0" width="100%"
				border="0">
				<TR>
					<TD width="55%">
						<TABLE class="tableNoBorderFormato" id="tblPrestador" cellSpacing="0" cellPadding="0" width="100%"
							border="0" runat="server">
							<TR>
								<TD width="16%"><STRONG>PRESTADOR</STRONG></TD>
								<TD colSpan="3">&nbsp;
									<asp:label id="lblPrestador" CssClass="lblFormato" runat="server" Font-Bold="True"></asp:label>&nbsp;
									<asp:label id="lblEspecialidadPres" runat="server" CssClass="lblFormato"></asp:label></TD>
							</TR>
							<TR>
								<TD width="16%">DIRECCIÓN</TD>
								<TD width="45%">&nbsp;
									<asp:label id="lblDireccion" CssClass="lblFormato" runat="server"></asp:label></TD>
								<TD width="15%">TELÉFONO</TD>
								<TD>&nbsp;
									<asp:label id="lblTelefono" CssClass="lblFormato" runat="server"></asp:label></TD>
							</TR>
						</TABLE>
					</TD>
					<TD>
						<TABLE class="tableNoBorderFormato" id="Table7" cellSpacing="0" cellPadding="0" width="100%"
							border="0">
							<TR>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD align="right" width="60%"><STRONG>FECHA DEL SERVICIO</STRONG></TD>
								<TD>&nbsp;
									<asp:label id="lblFechaServicio" CssClass="lblFormato" runat="server" Font-Bold="True"></asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<TABLE class="tableNoBorderFormato" id="Table8" cellSpacing="0" cellPadding="0" width="100%"
							border="0">
							<TR>
								<TD align="left" width="16%">
									SOLICITANTE</TD>
								<TD>&nbsp;<asp:label id="lblMedicoTra" CssClass="lblFormato" runat="server"></asp:label>&nbsp;
									<asp:label id="lblEspecialidad" runat="server" CssClass="lblFormato"></asp:label></TD>
								<TD width="10%">TELÉFONO</TD>
								<TD width="23%">&nbsp;
									<asp:label id="lblTelefonoTra" runat="server" CssClass="lblFormato"></asp:label></TD>
							</TR>
						</TABLE>
					</TD>
					<TD width="26%">
					</TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD height="5">
		</TD>
	</TR>
</TABLE>
