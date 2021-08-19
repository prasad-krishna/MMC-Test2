<%@ Page language="c#" Codebehind="LIS_solicitudfactura.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_empleado.forma.LIS_solicitudfactura" %>
<HTML>
	<HEAD>
		<title>HC-Historias Clínicas</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../css/admon.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../../scripts/Base.js" type="text/javascript"></script>
		<LINK href="../../css/Calendar.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../../scripts/Calendar.js" type="text/javascript"></script>
	</HEAD>
	<body onload="CargarConfiguracion();">
		<form id="Form1" method="post" runat="server">
			<table class="GG" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
				<TR>
					<TD align="center" colSpan="2">&nbsp;
						<TABLE class="tableBorder" id="tblBuscar" cellSpacing="0" cellPadding="3" width="40%">
							<TR>
								<TD class="titleBackBlue" width="20%" colSpan="4">Buscador de Solicitudes por 
									Factura</TD>
							</TR>
							<TR>
								<TD width="20%">Número Factura</TD>
								<TD width="30%" colSpan="3"><asp:textbox id="txtFactura" runat="server" CssClass="textBox" Width="130px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD width="20%">Prestador</TD>
								<TD width="30%" colSpan="3"><asp:dropdownlist id="ddlProveedorBus" runat="server" CssClass="textBoxSmall" Width="240px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD width="30%" colSpan="3"></TD>
							</TR>
							<TR>
								<TD align="center" colSpan="4"><asp:button id="btnBuscar" runat="server" CssClass="button" Text="Buscar" CausesValidation="False"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD align="left" colSpan="2" height="15"></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="2"><asp:label id="lblMensaje" runat="server" CssClass="titleBlue"></asp:label><asp:datagrid id="dtgSolicitudes" runat="server" CssClass="grid" Width="100%" CellPadding="1"
							GridLines="Horizontal" AutoGenerateColumns="False" AllowPaging="True" PageSize="50">
							<AlternatingItemStyle CssClass="altItems"></AlternatingItemStyle>
							<ItemStyle CssClass="norItems"></ItemStyle>
							<HeaderStyle CssClass="headerGrid"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="IdSolicitud"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="id_solicitud_SICAU"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="id_empleado" HeaderText="Empleado"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="IdSolicitudTipoServicio" HeaderText="IdSolicitudTipoServicio"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="beneficiario_id" HeaderText="Beneficiario"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Ver">
									<ItemStyle HorizontalAlign="Center" Width="9%"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton CausesValidation="false" CssClass="textSmallBlack" CommandArgument="IdSolicitud"
											CommandName="Ver" id="lnkVer" runat="server">
											<%#  DataBinder.Eval(Container,"DataItem.NoSolicitud")%>
										</asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Paciente">
									<ItemStyle Width="18%"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblPaciente" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="NombrePlanSolicitud" HeaderText="Plan">
									<ItemStyle HorizontalAlign="Center" Width="6%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy} ">
									<ItemStyle HorizontalAlign="Center" Width="7%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="FechaPrestacion" HeaderText="Fecha Prestaci&#243;n" DataFormatString="{0:dd/MM/yyyy} ">
									<ItemStyle HorizontalAlign="Center" Width="7%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="NombreProveedor" HeaderText="Prestador">
									<ItemStyle Width="19%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="NumeroFactura" HeaderText="Factura">
									<ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ValorConvenioSolicitado" HeaderText="Valor Solicitado" DataFormatString="{0:#,##0;($#,##0)}">
									<ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ValorAprobado" HeaderText="Valor Aprobado" DataFormatString="{0:#,##0;($#,##0)}">
									<ItemStyle HorizontalAlign="Right" Width="12%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ValorFactura" HeaderText="Total Factura" DataFormatString="{0:#,##0;($#,##0)}">
									<ItemStyle HorizontalAlign="Right" Width="15%"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</asp:datagrid><BR>
						<BR>
					</TD>
				</TR>
				<TR>
					<TD align="center" colSpan="2"><asp:button id="btnReversar" runat="server" CssClass="button" Text="Reversar" CausesValidation="False"
							Visible="False"></asp:button></TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
