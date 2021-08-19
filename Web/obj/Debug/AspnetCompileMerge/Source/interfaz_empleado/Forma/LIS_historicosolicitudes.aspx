<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LIS_historicosolicitudes.aspx.cs" Inherits="TPA.interfaz_empleado.forma.LIS_historicosolicitudes" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>HC-Historias Clínicas</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../css/admon.css" type="text/css" rel="stylesheet">
		<LINK href="../../css/Calendar.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../../scripts/Calendar.js" type="text/javascript"></script>
		<script language="javascript" src="../../scripts/Base.js" type="text/javascript"></script>
	</HEAD>
	<body onload="CargarConfiguracion()" bottomMargin="5" topMargin="5">
		<form id="Form1" method="post" runat="server">
			<table id="Table1" cellSpacing="0" cellPadding="2" width="100%" align="center" border="0">
				<TR>
					<TD align="center">
						<TABLE class="tableBorder" id="Table3" cellSpacing="0" cellPadding="3" width="60%">
							<TR>
								<TD class="titleBackBlue" width="20%" colSpan="4">
									HISTÓRICO DE SOLICITUDES</TD>
							</TR>
							<TR>
								<TD>Fecha Inicio</TD>
								<TD width="30%"><asp:textbox id="txtFechaInicio" runat="server" CssClass="textBox" Width="80px"></asp:textbox>&nbsp;<A href="javascript:MostrarCalendario(Form1.txtFechaInicio,Form1.txtFechaInicio,'dd/mm/yyyy');"
										name="btnFechaInicio"><IMG src="../../images/icoCalendar.gif" border="0"></A>
								</TD>
								<TD width="20%">Fecha Fin</TD>
								<TD width="30%"><asp:textbox id="txtFechaFin" runat="server" CssClass="textBox" Width="80px"></asp:textbox>&nbsp;<A href="javascript:MostrarCalendario(Form1.txtFechaFin,Form1.txtFechaFin,'dd/mm/yyyy');"
										name="btnFechaFin"><IMG src="../../images/icoCalendar.gif" border="0"></A>
								</TD>
							</TR>
							<TR>
								<TD align="center" colSpan="4">
									<P align="center"><asp:button id="btnBuscar" runat="server" CssClass="button" Text="Buscar"></asp:button></P>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD align="center"><STRONG><asp:label id="lblMensaje" runat="server"></asp:label></STRONG></TD>
				</TR>
				<tr>
					<td align="center"><asp:datagrid id="dtgSolicitud" runat="server" CssClass="grid" 
                            Width="100%" CellPadding="2" AllowPaging="True"
							AutoGenerateColumns="False" GridLines="Horizontal" PageSize="15" 
                            >
							<AlternatingItemStyle CssClass="altItems"></AlternatingItemStyle>
							<ItemStyle CssClass="norItems"></ItemStyle>
							<HeaderStyle CssClass="headerGrid"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="IdSolicitud"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="id_empleado"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="IdConsulta"></asp:BoundColumn>
								<asp:BoundColumn DataField="ConsecutivoNombre" HeaderText="No. Consulta">
									<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy} ">
									<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="NombrePrestador" HeaderText="Médico">
									<ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Servicios" HeaderText="Servicios">
									<ItemStyle HorizontalAlign="Left" Width="55%"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Copiar">
									<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton CausesValidation="false" CommandArgument="IdSolicitud" CommandName="Copiar" id="lnkCopiar"
											runat="server">Copiar</asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Center" BackColor="White" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
				<TR>
					<TD align="center">
						<asp:button id="btnCerrar" runat="server" CssClass="button" Text="Cerrar"></asp:button></TD>
				</TR>
			</table>
			&nbsp;
		</form>
	</body>
</HTML>
