<%@ Control Language="c#" AutoEventWireup="false" Codebehind="WC_EncabezadoFormatoIncapacidad.ascx.cs" Inherits="TPA.interfaz_empleado.WebControls.WC_EncabezadoFormatoIncapacidad" %>
<TABLE class="tableFormato" id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD class="titleFormato">
			<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titleFormato" align="center">
						<asp:label id="lblTitulo" runat="server" CssClass="titleFormato"></asp:label></TD>
					<TD vAlign="middle" width="25">
						<TABLE class="tableFormato" id="Table2" cellSpacing="0" cellPadding="0" width="150" border="0">
							<TR>
								<TD><STRONG>No.</STRONG></TD>
								<TD>
									<asp:textbox id="txtNoSolicitud" runat="server" CssClass="textBoxBlackBig" Width="100px"></asp:textbox></TD>
								<TD width="10"></TD>
							</TR>
						</TABLE>
					</TD>
					<TD width="20">
						<asp:image id="imgLogo" runat="server"></asp:image><BR>
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
					<TD align="left" width="43%" colSpan="2"><STRONG>
							<asp:Label id="lblTipoOrden" runat="server"></asp:Label></STRONG></TD>
					<TD align="right" width="10%">FECHA</TD>
					<TD width="10%">
						<asp:label id="lblFecha" runat="server" CssClass="lblFormato"></asp:label></TD>
					<TD align="right" width="15%">EXPEDIDA POR</TD>
					<TD width="25%">
						<asp:label id="lblExpedidaPor" runat="server" CssClass="lblFormato"></asp:label></TD>
				</TR>
			</TABLE>
			<TABLE class="tableBorderFormato" id="Table3" cellSpacing="0" cellPadding="0" width="100%"
				border="0">
				<TR>
					<TD>
					</TD>
					<TD width="26%"></TD>
				</TR>
			</TABLE>
			<TABLE class="tableBorderFormato" id="Table4" cellSpacing="0" cellPadding="0" width="100%"
				border="0">
				<TR>
					<TD align="center" width="18%" colSpan="4"><STRONG>DATOS DEL PACIENTE</STRONG></TD>
				</TR>
				<TR>
					<TD align="left" width="5%">NOMBRE</TD>
					<TD width="95%" colSpan="3">
						<asp:label id="lblNombrePaciente" runat="server" CssClass="lblFormato"></asp:label></TD>
				</TR>
				<TR>
					<TD align="left">
					<asp:label id="lblTituloIdentificación" runat="server">IDENTIFICACIÓN</asp:label>
					</TD>
					<TD width="95%" colSpan="3">
						<asp:label id="lblTipoIdentificacion" runat="server" CssClass="lblFormato"></asp:label>&nbsp;
						<asp:label id="lblNumero" runat="server" CssClass="lblFormato"></asp:label></TD>
				</TR>
				<TR>
					<TD align="left" height="18">GÉNERO</TD>
					<TD width="45%" height="18">
						<asp:label id="lblGenero" runat="server" CssClass="lblFormato"></asp:label></TD>
					<TD align="right" width="5%" height="18">EDAD</TD>
					<TD align="left" width="45%" height="18">
						<asp:label id="lblEdad" runat="server" CssClass="lblFormato"></asp:label></TD>
				</TR>
				<TR>
					<TD align="left">
                        <asp:label id="lblLugarResidenciaTitulo" runat="server" 
                            >RESIDENCIA</asp:label></TD>
					<TD>
						<asp:label id="lblLugarResidencia" runat="server" CssClass="lblFormato"></asp:label></TD>
					<TD align="right">
						<asp:label id="lblTelefonoPacienteTitulo" runat="server" CssClass="lblFormato">TELÉFONO</asp:label></TD>
					<TD align="left">
						<asp:label id="lblTelefonoPaciente" runat="server" CssClass="lblFormato"></asp:label></TD>
				</TR>
				<TR>
					<TD align="left">
						<asp:label id="lblEPSTitulo" runat="server" >EPS</asp:label></TD>
					<TD>
						<asp:label id="lblEPS" runat="server" CssClass="lblFormato"></asp:label></TD>
					<TD align="right">
						<asp:label id="lblBaseTitulo" CssClass="lblFormato" runat="server">BASE</asp:label></TD>
					<TD align="left">
						<asp:label id="lblBase" CssClass="lblFormato" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD align="left">
						<asp:label id="lblTituloAseguradora" Visible="False" runat="server">FILIAL</asp:label>
					</TD>
					<TD>
						<asp:label id="lblAseguradora" Visible="False" CssClass="lblFormato" 
                            runat="server"></asp:label>
					</TD>
					<TD align="right">
						<asp:label id="lblTituloSede" Visible="false" runat="server">SEDE</asp:label></TD>
					<TD align="left">
						<asp:label id="lblSede" Visible="false" CssClass="lblFormato" runat="server"></asp:label>
					</TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD height="5"></TD>
	</TR>
</TABLE>
