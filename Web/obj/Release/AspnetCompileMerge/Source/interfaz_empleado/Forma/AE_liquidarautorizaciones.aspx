
<%@ Page language="c#" Codebehind="AE_liquidarautorizaciones.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_empleado.forma.AE_liquidarautorizaciones" %>
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
		<LINK href="../../css/Calendar.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../../scripts/Calendar.js" type="text/javascript"></script>
	</HEAD>
	<body onload="CargarConfiguracion()">
		<form id="Form1" method="post" runat="server">
			<script language="javascript">
			
			function ActualizarConfirmadas()
			{	
				__doPostBack('btnReload','');

			
			}
			</script>
			<table class="GG" cellSpacing="0" cellPadding="10" width="100%" align="center" border="0">
				<TR>
					<TD align="center" colSpan="2">
						<FIELDSET class="FieldSet" style="WIDTH: 70%"><LEGEND><IMG src="../../images/icoProcesar.gif" border="0">&nbsp;Procesar</LEGEND><BR>
							<TABLE id="Table3" cellSpacing="0" cellPadding="6" width="80%">
								<TR>
									<TD width="20%">Modificiar a Estado</TD>
									<TD width="30%"><asp:dropdownlist id="ddlEstados" runat="server" AutoPostBack="True" CssClass="textBox" Width="176px"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD width="20%"><asp:label id="lblMotivo" runat="server" Visible="False">Motivo</asp:label></TD>
									<TD width="30%"><asp:dropdownlist id="ddlMotivos" runat="server" AutoPostBack="True" CssClass="textBox" Width="176px"
											Visible="False"></asp:dropdownlist>&nbsp;
										<asp:comparevalidator id="cmvMotivo" runat="server" CssClass="textRed" ValueToCompare="0" Operator="NotEqual"
											ErrorMessage="Requerido" ControlToValidate="ddlMotivos" Display="Dynamic" ForeColor=" "></asp:comparevalidator></TD>
								</TR>
								<TR>
									<TD width="20%"><asp:label id="lblPlanesSolicitud" runat="server" Visible="False">Planes</asp:label></TD>
									<TD width="30%"><asp:dropdownlist id="ddlPlanesSolicitud" runat="server" CssClass="textBox" Width="200px" Visible="False"></asp:dropdownlist>&nbsp;
										<asp:comparevalidator id="Comparevalidator1" runat="server" CssClass="textRed" ValueToCompare="0" Operator="NotEqual"
											ErrorMessage="Requerido" ControlToValidate="ddlPlanesSolicitud" Display="Dynamic" ForeColor=" "></asp:comparevalidator></TD>
								</TR>
							</TABLE>
							<BR>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD id="tdFactura" style="DISPLAY: none" align="center" colSpan="2" runat="server">
						<FIELDSET class="FieldSet" style="WIDTH: 70%"><LEGEND><IMG src="../../images/icoFactura.gif" border="0">
								Datos de la Factura</LEGEND><BR>
							<TABLE class="tableBorder" id="Table2" cellSpacing="0" cellPadding="6" width="80%">
								<TR>
									<TD width="20%">No. de Factura</TD>
									<TD width="30%"><asp:textbox id="txtNumFactura" runat="server" CssClass="textBox" Width="100px" MaxLength="50"></asp:textbox>&nbsp;
										<asp:requiredfieldvalidator id="rfvNoFactura" runat="server" CssClass="textRed" ErrorMessage="Requerido" ControlToValidate="txtNumFactura"
											Display="Dynamic" ForeColor=" " Enabled="False"></asp:requiredfieldvalidator></TD>
								</TR>
								<TR>
									<TD width="20%">No. Cuenta Cobro</TD>
									<TD width="30%"><asp:textbox id="txtCuentaCobro" runat="server" CssClass="textBox" Width="100px" MaxLength="50"></asp:textbox>&nbsp;
									</TD>
								</TR>
								<TR>
									<TD width="20%">Fecha de Factura</TD>
									<TD width="30%"><asp:textbox id="txtFechaFactura" runat="server" CssClass="textBox" Width="80px"></asp:textbox>&nbsp;<A href="javascript:MostrarCalendario(Form1.txtFechaFactura,Form1.txtFechaFactura,'dd/mm/yyyy');"
											name="btnFechaFactura"><IMG src="../../images/icoCalendar.gif" border="0"></A>&nbsp;
										<asp:requiredfieldvalidator id="rfvFechaFactura" runat="server" CssClass="textRed" ErrorMessage="Requerido"
											ControlToValidate="txtFechaFactura" Display="Dynamic" ForeColor=" " Enabled="False"></asp:requiredfieldvalidator>&nbsp;</TD>
								</TR>
								<TR>
									<TD width="20%">Fecha de Radicación</TD>
									<TD width="30%"><asp:textbox id="txtFechaRadicacion" runat="server" CssClass="textBox" Width="80px"></asp:textbox>&nbsp;<A href="javascript:MostrarCalendario(Form1.txtFechaRadicacion,Form1.txtFechaRadicacion,'dd/mm/yyyy');"
											name="btnFechaRadicacion"><IMG src="../../images/icoCalendar.gif" border="0"></A>&nbsp;
										<asp:requiredfieldvalidator id="rfvFechaRadicacion" runat="server" CssClass="textRed" ErrorMessage="Requerido"
											ControlToValidate="txtFechaRadicacion" Display="Dynamic" ForeColor=" " Enabled="False"></asp:requiredfieldvalidator>&nbsp;</TD>
								</TR>
								<TR>
									<TD width="20%">Fecha de Confirmación</TD>
									<TD width="30%"><asp:textbox id="txtFechaConfirmacion" runat="server" CssClass="textBox" Width="80px"></asp:textbox>&nbsp;<A href="javascript:MostrarCalendario(Form1.txtFechaConfirmacion,Form1.txtFechaConfirmacion,'dd/mm/yyyy');"
											name="btnFechaFactura"><IMG src="../../images/icoCalendar.gif" border="0"></A>&nbsp;
										<asp:requiredfieldvalidator id="rfvFechaConfirmacion" runat="server" CssClass="textRed" ErrorMessage="Requerido"
											ControlToValidate="txtFechaConfirmacion" Display="Dynamic" ForeColor=" " Enabled="False"></asp:requiredfieldvalidator>&nbsp;</TD>
								</TR>
								<TR>
									<TD width="20%">Total Valor Factura</TD>
									<TD width="30%"><asp:textbox onkeypress="return currencyFormat(this,event,true,false)" id="txtValorFactura" runat="server"
											CssClass="textBox" Width="100px"></asp:textbox>&nbsp;
										<asp:requiredfieldvalidator id="rfvValorFactura" runat="server" CssClass="textRed" ErrorMessage="Requerido"
											ControlToValidate="txtValorFactura" Display="Dynamic" ForeColor=" " Enabled="False"></asp:requiredfieldvalidator></TD>
								</TR>
								<TR>
									<TD width="20%">Proveedor</TD>
									<TD width="30%"><asp:dropdownlist id="ddlProveedor" runat="server" CssClass="textBoxSmall" Width="230px"></asp:dropdownlist>&nbsp;
										<asp:comparevalidator id="cmvProveedor" runat="server" CssClass="textRed" ValueToCompare="0" Operator="NotEqual"
											ErrorMessage="Requerido" ControlToValidate="ddlProveedor" Display="Dynamic" ForeColor=" " Enabled="False"></asp:comparevalidator></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD align="left" colSpan="2" height="15">
						<TABLE class="tableBorder" id="tblBuscar" style="DISPLAY: none" cellSpacing="0" cellPadding="3"
							width="75%" runat="server">
							<TR>
								<TD width="20%">No. de Solicitud</TD>
								<TD width="30%"><asp:textbox id="txtNoSolicitud" runat="server" CssClass="textBox" Width="100px"></asp:textbox></TD>
								<TD width="18%">Prestador Asignado</TD>
								<TD width="32%"><asp:dropdownlist id="ddlProveedorBus" runat="server" CssClass="textBoxSmall" Width="210px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD width="20%">No. de Solicitud Desde</TD>
								<TD width="30%"><asp:textbox id="txtNoDesde" runat="server" CssClass="textBox" Width="100px"></asp:textbox>&nbsp;<BR>
									<SPAN class="textSmallBlack">(Escriba solamente el número)</SPAN></TD>
								<TD width="18%">No. de Solicitud Hasta</TD>
								<TD width="32%"><asp:textbox id="txtNoHasta" runat="server" CssClass="textBox" Width="100px"></asp:textbox><BR>
									<SPAN class="textSmallBlack">(Escriba solamente el número)</SPAN></TD>
							</TR>
							<TR>
								<TD>Fecha Desde</TD>
								<TD width="30%"> <asp:textbox id="txtFechaInicio" runat="server" CssClass="textBox" Width="80px"></asp:textbox>&nbsp;<A href="javascript:MostrarCalendario(Form1.txtFechaInicio,Form1.txtFechaInicio,'dd/mm/yyyy');"
										name="btnFechaInicio"><IMG src="../../images/icoCalendar.gif" border="0"></A>
								</TD>
								<TD>Fecha Hasta</TD>
								<TD><asp:textbox id="txtFechaFin" runat="server" CssClass="textBox" Width="80px"></asp:textbox>&nbsp;<A href="javascript:MostrarCalendario(Form1.txtFechaFin,Form1.txtFechaFin,'dd/mm/yyyy');"
										name="btnFechaFin"><IMG src="../../images/icoCalendar.gif" border="0"></A>
								</TD>
							</TR>
							<TR>
								<TD>Fecha Creación Desde</TD>
								<TD width="30%"><asp:textbox id="txtFechaCreacionInicio" runat="server" CssClass="textBox" Width="80px"></asp:textbox>&nbsp;<A href="javascript:MostrarCalendario(Form1.txtFechaCreacionInicio,Form1.txtFechaCreacionInicio,'dd/mm/yyyy');"
										name="btnFechaInicio"><IMG src="../../images/icoCalendar.gif" border="0"></A></TD>
								<TD>Fecha Creación Hasta</TD>
								<TD><asp:textbox id="txtFechaCreacionFin" runat="server" CssClass="textBox" Width="80px"></asp:textbox>&nbsp;<A href="javascript:MostrarCalendario(Form1.txtFechaCreacionFin,Form1.txtFechaCreacionFin,'dd/mm/yyyy');"
										name="btnFechaInicio"><IMG src="../../images/icoCalendar.gif" border="0"></A></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 22px">Mes Liquidación</TD>
								<TD style="HEIGHT: 22px" width="30%" colSpan="3"><asp:dropdownlist id="ddlMes" runat="server" CssClass="textBox" Width="100px">
										<asp:ListItem Value="0">--Mes--</asp:ListItem>
										<asp:ListItem Value="1">Enero</asp:ListItem>
										<asp:ListItem Value="2">Febrero</asp:ListItem>
										<asp:ListItem Value="3">Marzo</asp:ListItem>
										<asp:ListItem Value="4">Abril</asp:ListItem>
										<asp:ListItem Value="5">Mayo</asp:ListItem>
										<asp:ListItem Value="6">Junio</asp:ListItem>
										<asp:ListItem Value="7">Julio</asp:ListItem>
										<asp:ListItem Value="8">Agosto</asp:ListItem>
										<asp:ListItem Value="9">Septiembre</asp:ListItem>
										<asp:ListItem Value="10">Octubre</asp:ListItem>
										<asp:ListItem Value="11">Noviembre</asp:ListItem>
										<asp:ListItem Value="12">Diciembre</asp:ListItem>
									</asp:dropdownlist>&nbsp;
									<asp:dropdownlist id="ddlAno" runat="server" CssClass="textBox" Width="70px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD align="center" colSpan="4"><asp:button id="btnBuscar" runat="server" CssClass="buttonSmall" Text="Buscar" CausesValidation="False"></asp:button></TD>
							</TR>
						</TABLE>
						<asp:button id="btnReload" runat="server" CausesValidation="False"></asp:button></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="2">
							<asp:Label id="lblMensaje" runat="server" CssClass="titleBlue"></asp:Label>
							<br />
							<DIV style="OVERFLOW: auto; WIDTH: 98%; HEIGHT: 600px">
								<asp:datagrid id="dtgSolicitudes" runat="server" Width="100%" CssClass="grid" CellPadding="4"
									GridLines="Horizontal" AutoGenerateColumns="False" PageSize="20">
									<AlternatingItemStyle CssClass="altItems"></AlternatingItemStyle>
									<ItemStyle CssClass="norItems"></ItemStyle>
									<HeaderStyle CssClass="headerGrid"></HeaderStyle>
									<Columns>
										<asp:BoundColumn Visible="False" DataField="IdSolicitud"></asp:BoundColumn>
										<asp:BoundColumn Visible="False" DataField="id_solicitud_SICAU"></asp:BoundColumn>
										<asp:BoundColumn Visible="False" DataField="id_empleado" HeaderText="Empleado"></asp:BoundColumn>
										<asp:BoundColumn Visible="False" DataField="IdSolicitudTipoServicio" HeaderText="IdSolicitudTipoServicio"></asp:BoundColumn>
										<asp:BoundColumn Visible="False" DataField="beneficiario_id" HeaderText="Beneficiario"></asp:BoundColumn>
										<asp:TemplateColumn HeaderText="Procesar">
											<ItemStyle HorizontalAlign="Center" Width="12%"></ItemStyle>
											<ItemTemplate>
												<asp:LinkButton CausesValidation="true" CssClass="linkSmall" CommandArgument="IdSolicitud" CommandName="Procesar"
													id="lnkProcesar" runat="server">Procesar</asp:LinkButton>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn HeaderText="Procesar">
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
											<ItemTemplate>
												<asp:CheckBox id="chkProcesar" runat="server"></asp:CheckBox>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:BoundColumn DataField="NoSolicitud" HeaderText="No. Solic">
											<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
										</asp:BoundColumn>
										<asp:TemplateColumn HeaderText="Paciente">
											<ItemStyle Width="28%"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblPaciente" runat="server"></asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:BoundColumn DataField="NombreSolicitudEstado" HeaderText="Estado">
											<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="NombrePlanSolicitud" HeaderText="Plan">
											<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="CodigoDiagnostico" HeaderText="Diagnóstico">
											<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy} ">
											<ItemStyle HorizontalAlign="Center" Width="7%"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="FechaPrestacion" HeaderText="Fecha Prestación" DataFormatString="{0:dd/MM/yyyy} ">
											<ItemStyle HorizontalAlign="Center" Width="7%"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="ValorTotalConvenioSolicitado" HeaderText="Total Convenio o Solicitado"
											DataFormatString="{0:$ #,##0;($#,##0)}">
											<ItemStyle HorizontalAlign="Right" Width="15%"></ItemStyle>
										</asp:BoundColumn>
										<asp:TemplateColumn HeaderText="Valor Factura">
											<ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
											<ItemTemplate>
												<asp:TextBox Width="60px" onkeypress="return currencyFormat(this,event,true,false)" runat="server"
													ID="txtValorFacturado" CssClass="textBox"></asp:TextBox>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn HeaderText="Valor Aprobado">
											<ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
											<ItemTemplate>
												<asp:TextBox Width="60px" onkeypress="return currencyFormat(this,event,true,false)" runat="server"
													ID="txtValorAprobado" CssClass="textBox"></asp:TextBox>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn Visible="False" HeaderText="Glosa">
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
											<ItemTemplate>
												<asp:CheckBox id="chkGlosa" runat="server"></asp:CheckBox>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn Visible="False" HeaderText="Observaciones Anulaci&#243;n">
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
											<ItemTemplate>
												<asp:TextBox Width="140px" runat="server" MaxLength="200" ID="txtObservacionesAnulacion" TextMode="MultiLine"
													CssClass="textBox"></asp:TextBox>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn HeaderText="Detalle">
											<ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
											<ItemTemplate>
												<asp:LinkButton CausesValidation="false" CommandArgument="IdSolicitud" CommandName="Ver" id="lnkVer"
													runat="server">Ver</asp:LinkButton>
											</ItemTemplate>
										</asp:TemplateColumn>
									</Columns>
									<PagerStyle Mode="NumericPages"></PagerStyle>
								</asp:datagrid></DIV>
						</TD>
				</TR>
				<TR>
					<TD align="center" colSpan="2"><asp:button id="btnProcesar" runat="server" CssClass="button" Text="Procesar"></asp:button></TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
