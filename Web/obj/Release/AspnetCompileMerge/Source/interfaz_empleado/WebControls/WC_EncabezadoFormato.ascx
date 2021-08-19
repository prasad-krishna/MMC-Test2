<%@ Control Language="c#" AutoEventWireup="false" Codebehind="WC_EncabezadoFormato.ascx.cs" Inherits="TPA.interfaz_empleado.WebControls.WC_EncabezadoFormato" %>
<TABLE class="tableFormato" id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD class="titleFormato">
			<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titleFormato" align="center" height="45"><asp:label id="lblTitulo" runat="server" CssClass="titleFormato"></asp:label></TD>
					<TD vAlign="middle" width="20" height="45">
						<TABLE class="tableFormato" id="Table2" cellSpacing="0" cellPadding="0" width="170" border="0">
							<TR>
								<TD><STRONG>No.</STRONG></TD>
								<TD><asp:textbox id="txtNoSolicitud" runat="server" CssClass="textBoxBlackBig" Width="120px"></asp:textbox></TD>
								<TD width="10"></TD>
							</TR>
						</TABLE>
					</TD>
					<TD width="20" height="45"><asp:image id="imgLogo" runat="server"></asp:image></TD>
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
					<TD width="28%"><asp:label id="lblOrden" runat="server" CssClass="lblFormato" Font-Bold="True"></asp:label></TD>
					<TD align="right" width="10%">FECHA</TD>
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
						<TABLE class="tableNoBorderFormato" id="Table7" cellSpacing="0" cellPadding="0" width="100%"
							border="0">
							<TR>
								<TD width="80%"></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD align="right"><STRONG>VIGENTE HASTA&nbsp;</STRONG></TD>
								<TD><asp:label id="lblFechaLimite" runat="server" CssClass="lblFormato" Font-Bold="True"></asp:label></TD>
							</TR>
						</TABLE>
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
					<TD width="26%"></TD>
				</TR>
			</TABLE>
			<TABLE class="tableBorderFormato" id="Table4" cellSpacing="0" cellPadding="0" width="100%"
				border="0">
				<TBODY>
					<TR>
						<TD align="center" width="18%" colSpan="4"><STRONG>DATOS DEL PACIENTE</STRONG></TD>
						<TD align="center" width="22%" colSpan="2"><STRONG>DATOS DEL EMPLEADO</STRONG></TD>
					</TR>
					<TR>
						<TD align="left" width="13%">NOMBRE</TD>
						<TD width="85%" colSpan="3"><asp:label id="lblNombrePaciente" runat="server" CssClass="lblFormato"></asp:label></TD>
						<TD align="left" width="13%">NOMBRE</TD>
						<TD width="82%"><asp:label id="lblNombreEmpleado" runat="server" CssClass="lblFormato"></asp:label></TD>
					</TR>
					<TR>
						<TD align="left">IDENTIFICACIÓN</TD>
						<TD width="14%"><asp:label id="lblTipoIdentificacion" runat="server" CssClass="lblFormato"></asp:label>&nbsp;
							<asp:label id="lblNumero" runat="server" CssClass="lblFormato"></asp:label></TD>
						<TD align="right" width="8%">GÉNERO</TD>
						<TD align="left" width="11%"><asp:label id="lblGenero" runat="server" CssClass="lblFormato"></asp:label></TD>
						<TD align="left">IDENTIFICACIÓN</TD>
						<TD width="82%"><asp:label id="lblTipoIdentificacionEmpleado" runat="server" CssClass="lblFormato"></asp:label>&nbsp;<asp:label id="lblNumeroEmpleado" runat="server" CssClass="lblFormato"></asp:label></TD>
					</TR>
					<TR>
						<TD align="left">RESIDENCIA</TD>
						<TD><asp:label id="lblLugarResidencia" runat="server" CssClass="lblFormato"></asp:label></TD>
						<TD align="right">EDAD</TD>
						<TD align="left">
							<asp:label id="lblEdad" CssClass="lblFormato" runat="server"></asp:label></TD>
						<TD align="left"></TD>
						<TD align="left" width="82%"><asp:label id="lblTelefonoEmpleado" runat="server" CssClass="lblFormato" Visible="False"></asp:label></TD>
					</TR>
					<TR>
						<TD align="left">PARENTESCO</TD>
						<TD><asp:label id="lblParentesco" runat="server" CssClass="lblFormato"></asp:label></TD>
						<TD align="right"></TD>
						<TD align="left">
							<asp:label id="lblTelefonoPaciente" CssClass="lblFormato" runat="server" Visible="False"></asp:label></TD>
						<TD align="left">
							<asp:label id="lblLabelServicio" runat="server" Visible="False">SERVICIO</asp:label></TD>
						<TD width="82%">
							<asp:datagrid id="dtgDetalle" runat="server" Width="99%" CellPadding="0" AutoGenerateColumns="False"
								GridLines="None" ShowHeader="False">
								<AlternatingItemStyle CssClass="altItemsFormato"></AlternatingItemStyle>
								<ItemStyle CssClass="norItemsFormato"></ItemStyle>
								<HeaderStyle CssClass="headerGridFormato"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="Servicio">
										<ItemStyle Width="60%"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblProductoServicio" text='<%# DataBinder.Eval(Container, "DataItem.NombreCompletoMedicamento").ToString() + DataBinder.Eval(Container, "DataItem.NombreCompletoServicio").ToString() + " " + DataBinder.Eval(Container, "DataItem.Comentarios").ToString() %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid></TD>
					</TR>
				</TBODY>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD height="5"></TD>
	</TR>
</TABLE>
