<%@ Control Language="c#" AutoEventWireup="false" Codebehind="WC_EncabezadoFormatoOrden.ascx.cs" Inherits="TPA.interfaz_empleado.WebControls.WC_EncabezadoFormatoOrden" %>
<TABLE class="tableFormato" id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD class="titleFormato">
			<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titleFormato" align="center"><asp:label id="lblTitulo" runat="server" CssClass="titleFormato"></asp:label></TD>
					<TD vAlign="middle" width="30">
						<TABLE class="tableFormato" id="Table2" cellSpacing="0" cellPadding="0" width="170" border="0">
							<TR>
								<TD><STRONG>No.</STRONG></TD>
								<TD><asp:textbox id="txtNoSolicitud" runat="server" CssClass="textBoxBlackBig" Width="100px"></asp:textbox></TD>
								<TD width="10"></TD>
							</TR>
							<TR>
								<TD><STRONG><asp:label id="lblTituloAutorizacion" runat="server">Autorización&nbsp; SURA No.</asp:label><BR>
									</STRONG>
								</TD>
								<TD vAlign="middle">
									_____________</TD>
								<TD width="10"></TD>
							</TR>
						</TABLE>
					</TD>
					<TD width="100" align="right"><asp:image id="imgLogo" runat="server"></asp:image>&nbsp;
					</TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD height="5">
			<TABLE class="tableBorderFormato" id="tblOrden" height="33" cellSpacing="0" cellPadding="0"
				width="100%" border="0">
				<TR>
					<TD align="left" width="15%"><STRONG title="15%">ORDEN DE</STRONG></TD>
					<TD width="24%"><asp:label id="lblOrden" runat="server" CssClass="lblFormato" Font-Bold="True"></asp:label></TD>
					<TD align="right" width="15%">FECHA EXPEDICIÓN</TD>
					<TD width="10%"><asp:label id="lblFecha" runat="server" CssClass="lblFormato"></asp:label></TD>
					<TD align="right" width="15%">EXPEDIDA POR</TD>
					<TD width="25%"><asp:label id="lblExpedidaPor" runat="server" CssClass="lblFormato"></asp:label></TD>
				</TR>
			</TABLE>
			<TABLE class="tableBorderFormato" id="Table3" cellSpacing="0" cellPadding="0" width="100%"
				border="0">
				<TR>
					<TD width="70%">
						<TABLE class="tableNoBorderFormato" id="tblPrestador" cellSpacing="0" cellPadding="0" width="100%"
							border="0" runat="server">
							<TR>
								<TD width="16%"><STRONG>PRESTADOR</STRONG></TD>
								<TD colSpan="3">&nbsp;
									<asp:label id="lblPrestador" runat="server" CssClass="lblFormato" Font-Bold="True"></asp:label>&nbsp;
									<asp:label id="lblEspecialidadPres" runat="server" CssClass="lblFormato"></asp:label></TD>
							</TR>
							<TR>
								<TD width="16%">DIRECCIÓN</TD>
								<TD width="45%">&nbsp;
									<asp:label id="lblDireccion" runat="server" CssClass="lblFormato"></asp:label></TD>
								<TD width="15%">TELÉFONO</TD>
								<TD>&nbsp;
									<asp:label id="lblTelefono" runat="server" CssClass="lblFormato"></asp:label></TD>
							</TR>
						</TABLE>
					</TD>
					<TD>
					</TD>
				</TR>
				<TR>
					<TD>
						<TABLE class="tableNoBorderFormato" id="Table8" cellSpacing="0" cellPadding="0" width="100%"
							border="0">
							<TR>
								<TD align="left" width="16%">SOLICITANTE</TD>
								<TD>&nbsp;<asp:label id="lblMedicoTra" runat="server" CssClass="lblFormato"></asp:label>&nbsp;
									<asp:label id="lblEspecialidad" runat="server" CssClass="lblFormato"></asp:label></TD>
								<TD width="10%">TELÉFONO</TD>
								<TD width="23%">&nbsp;
									<asp:label id="lblTelefonoTra" runat="server" CssClass="lblFormato"></asp:label></TD>
							</TR>
						</TABLE>
					</TD>
					<TD width="26%">
						<TABLE class="tableNoBorderFormato" id="Table7" cellSpacing="0" cellPadding="0" width="100%"
							border="0">
							<TR>
								<TD align="right"><STRONG>VIGENTE HASTA&nbsp;</STRONG></TD>
								<TD>
									<asp:label id="lblFechaLimite" CssClass="lblFormato" runat="server" Font-Bold="True"></asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE class="tableBorderFormato" id="Table4" cellSpacing="0" cellPadding="0" width="100%"
				border="0">
				<TBODY>
					<TR>
						<TD align="center" width="18%" colSpan="4"><STRONG>DATOS DEL PACIENTE</STRONG></TD>
					</TR>
					<TR>
						<TD align="left" width="5%">NOMBRE</TD>
						<TD width="95%" colSpan="3"><asp:label id="lblNombrePaciente" runat="server" CssClass="lblFormato"></asp:label></TD>
					</TR>
					<TR>
						<TD align="left">
						<asp:label id="lblTituloIdentificacion" runat="server">IDENTIFICACIÓN</asp:label></TD>
						<TD width="95%" colSpan="3"><asp:label id="lblTipoIdentificacion" runat="server" CssClass="lblFormato"></asp:label>&nbsp;
							<asp:label id="lblNumero" runat="server" CssClass="lblFormato"></asp:label></TD>
					</TR>
					<TR>
						<TD align="left">GÉNERO</TD>
						<TD width="45%"><asp:label id="lblGenero" runat="server" CssClass="lblFormato"></asp:label></TD>
						<TD align="right" width="5%">EDAD</TD>
						<TD align="left" width="45%"><asp:label id="lblEdad" runat="server" CssClass="lblFormato"></asp:label></TD>
					</TR>
					<TR>
						<TD align="left"><asp:label id="lblTituloResidencia" runat="server" >RESIDENCIA</asp:label></TD>
						<TD><asp:label id="lblLugarResidencia" runat="server" CssClass="lblFormato"></asp:label></TD>
						<TD align="right"><asp:label id="lblTituloTelefono" runat="server" CssClass="lblFormato">TELÉFONO</asp:label></TD>
						<TD align="left"><asp:label id="lblTelefonoPaciente" runat="server" CssClass="lblFormato"></asp:label></TD>
					</TR>
					<TR>
						<TD align="left"><asp:label id="lblTituloEPS" runat="server">EPS</asp:label></TD>
						<TD><asp:label id="lblEPS" runat="server" CssClass="lblFormato"></asp:label></TD>
						<TD align="right"><asp:label id="lblTituloBase" runat="server" CssClass="lblFormato">
							BASE</asp:label></TD>
						<TD align="left"><asp:label id="lblBase" runat="server" CssClass="lblFormato"></asp:label></TD>
					</TR>
					<TR>
					<TD align="left">
						<asp:label id="lblTituloAseguradora" Visible="False"  runat="server">FILIAL</asp:label>
					</TD>
					<TD>
						<asp:label id="lblAseguradora" Visible="False" CssClass="lblFormato" 
                            runat="server"></asp:label>
					</TD>
					<TD align="right">
						<asp:label id="lblTituloSede" Visible="false"  runat="server">SEDE</asp:label></TD>
					<TD align="left">
						<asp:label id="lblSede" Visible="false" CssClass="lblFormato" runat="server"></asp:label>
					</TD>
				</TR>
				</TBODY>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD height="5"></TD>
	</TR>
</TABLE>
