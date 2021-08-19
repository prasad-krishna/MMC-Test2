<%@ Control Language="c#" AutoEventWireup="false" Codebehind="WC_DatosConsulta.ascx.cs" Inherits="TPA.interfaz_empleado.WebControls.WC_DatosConsulta" %>
<TABLE id="Table4" cellSpacing="0" cellPadding="1" width="99%" align="right">
	<TR>
		<TD>
			<FIELDSET class="FieldSet" style="WIDTH: 99%">
				<LEGEND onclick="FieldClick(this);"><IMG alt="" src="../../images/icoExpand.gif">&nbsp;<IMG src="../../images/icoHistoria.gif" border="0">
					&nbsp;Datos de la Consulta&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</LEGEND>
				<DIV id="dvConsulta" style="WIDTH: 98%" align="left" runat="server">
					<TABLE class="tableSmall" id="Table1" cellSpacing="0" cellPadding="1" width="95%">
						<TR>
							<TD width="10%">Fecha</TD>
							<TD align="left" width="20%">
								<asp:Label id="lblFecha" runat="server" CssClass="titleBig"></asp:Label></TD>
							<TD align="left" width="15%">No Consulta</TD>
							<TD width="20%">
								<asp:Label id="lblNoConsulta" runat="server" CssClass="titleBig"></asp:Label></TD>
						</TR>
						<TR>
							<TD>Médico</TD>
							<TD align="left" colSpan="3">
								<asp:Label id="lblSolicitante" runat="server"></asp:Label></TD>
						</TR>
						<TR>
							<TD width="10%">Tipo de Consulta</TD>
							<TD align="left" width="20%">
								<asp:Label id="lblTipoConsulta" runat="server"></asp:Label></TD>
							<TD align="left" width="15%">Tipo Enfermedad</TD>
							<TD width="20%">
								<asp:Label id="lblTipoEnfermedad" runat="server"></asp:Label></TD>
						</TR>
						<TR>
							<TD width="10%">Próximo Control</TD>
							<TD align="left" width="20%">
								<asp:Label id="lblControl" runat="server"></asp:Label></TD>
							<TD align="left" width="15%"></TD>
							<TD width="20%"></TD>
						</TR>
					</TABLE>
				</DIV>
			</FIELDSET>
		</TD>
	</TR>
</TABLE>
