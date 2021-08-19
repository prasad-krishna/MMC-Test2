<%@ Page language="c#" Codebehind="AE_asociarsolicitudes.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_empleado.forma.AE_asociarsolicitudes" %>
<%@ Register TagPrefix="uc1" TagName="WC_Report" Src="../WebControls/WC_Report.ascx" %>

<HTML>
	<HEAD>
		<title>HC-Historias Clínicas</title>
		<meta content="False" name="vs_showGrid">
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
		<asp:ScriptManager ID="scrMng" runat="server"></asp:ScriptManager>
			<table class="GG" cellSpacing="0" cellPadding="10" width="100%" align="center" border="0">
				<TR>
					<TD align="center" colSpan="2">&nbsp;
						<FIELDSET class="FieldSet" style="WIDTH: 70%"><LEGEND><IMG src="../../images/icoProcesar.gif" border="0">&nbsp;Procesar</LEGEND><BR>
							<TABLE id="Table3" cellSpacing="0" cellPadding="6" width="90%">
								<TR>
									<TD width="20%">Asociar a</TD>
									<TD width="30%"><asp:radiobuttonlist id="rdlOpcionAsociacion" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"
											Width="95%">
											<asp:ListItem Value="1">Nuevo Reporte</asp:ListItem>
											<asp:ListItem Value="0">Reporte Existente</asp:ListItem>
										</asp:radiobuttonlist></TD>
								</TR>
								<TR>
									<TD width="20%"><asp:label id="lblNoReporte" runat="server" Visible="False">No. Reporte</asp:label></TD>
									<TD width="30%">&nbsp;
										<asp:textbox id="txtNoReporte" runat="server" Width="104px" Visible="False" CssClass="textBox"></asp:textbox>&nbsp;&nbsp;
										<asp:button id="btnBuscarReporte" runat="server" Visible="False" CssClass="buttonSmall" Text="Buscar"></asp:button>&nbsp;
										<asp:requiredfieldvalidator id="rfvNoReporte" runat="server" CssClass="textRed" Enabled="False" ForeColor=" "
											Display="Dynamic" ControlToValidate="txtNoReporte" ErrorMessage="Requerido"></asp:requiredfieldvalidator></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD align="left" colSpan="2">
						<TABLE class="tableBorder" id="tblBuscar" style="DISPLAY: none" cellSpacing="0" cellPadding="3"
							width="70%" runat="server">
							<TR>
								<TD width="20%">No. de Solicitud</TD>
								<TD width="30%"><asp:textbox id="txtNoSolicitud" runat="server" Width="100px" CssClass="textBox"></asp:textbox></TD>
								<TD width="20%">Prestador Asignado</TD>
								<TD width="30%"><asp:dropdownlist id="ddlProveedorBus" runat="server" Width="140px" CssClass="textBox"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD>Fecha Desde</TD>
								<TD width="30%"><asp:textbox id="txtFechaInicio" runat="server" Width="80px" CssClass="textBox"></asp:textbox>&nbsp;<A href="javascript:MostrarCalendario(Form1.txtFechaInicio,Form1.txtFechaInicio,'dd/mm/yyyy');"
										name="btnFechaInicio"><IMG src="../../images/icoCalendar.gif" border="0"></A>
								</TD>
								<TD>Fecha Hasta</TD>
								<TD><asp:textbox id="txtFechaFin" runat="server" Width="80px" CssClass="textBox"></asp:textbox>&nbsp;<A href="javascript:MostrarCalendario(Form1.txtFechaFin,Form1.txtFechaFin,'dd/mm/yyyy');"
										name="btnFechaFin"><IMG src="../../images/icoCalendar.gif" border="0"></A>
								</TD>
							</TR>
							<TR>
								<TD>Mes Liquidación</TD>
								<TD width="30%" colSpan="3"><asp:dropdownlist id="ddlMes" runat="server" Width="100px" CssClass="textBox">
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
									<asp:dropdownlist id="ddlAno" runat="server" Width="70px" CssClass="textBox"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD align="center" colSpan="4"><asp:button id="btnBuscar" runat="server" CssClass="buttonSmall" Text="Buscar" CausesValidation="False"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD align="center" colSpan="2"><asp:updatePanel id="Ajaxpanel2" runat="server"><ContentTemplate>
<asp:Label id="lblMensaje" runat="server" CssClass="titleBlue"></asp:Label>&nbsp; 
<uc1:WC_Report id="WC_Report1" runat="server" Visible="False"></uc1:WC_Report><BR><BR>
      <DIV style="OVERFLOW: auto; WIDTH: 98%; HEIGHT: 800px">
								<asp:datagrid id="dtgSolicitudes" runat="server" Width="100%" CssClass="grid" AllowCustomPaging="True"
									PageSize="40" AutoGenerateColumns="False" GridLines="Horizontal" CellPadding="0">
									<AlternatingItemStyle CssClass="altItems"></AlternatingItemStyle>
									<ItemStyle CssClass="norItems"></ItemStyle>
									<HeaderStyle CssClass="headerGrid"></HeaderStyle>
									<Columns>
										<asp:BoundColumn Visible="False" DataField="IdSolicitud"></asp:BoundColumn>
										<asp:BoundColumn Visible="False" DataField="id_empleado"></asp:BoundColumn>
										<asp:TemplateColumn HeaderText="Asociar">
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
											<ItemTemplate>
												<asp:CheckBox id="chkAsociar" runat="server"></asp:CheckBox>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn HeaderText="No. Solicitud">
											<ItemStyle Width="7%"></ItemStyle>
											<ItemTemplate>
												<asp:HyperLink CssClass="textSmallBlack" id="hplName" NavigateUrl='<%# Convert.ToInt32(DataBinder.Eval(Container, "DataItem.IdTipoSolicitud")) == 1 ? "AE_solicitudreembolso.aspx?IdSolicitud=" + DataBinder.Eval(Container, "DataItem.IdSolicitud") + "&employee_id=" + DataBinder.Eval(Container, "DataItem.id_empleado") : "AE_solicitudautorizacion.aspx?IdSolicitud=" + DataBinder.Eval(Container, "DataItem.IdSolicitud") + "&employee_id=" + DataBinder.Eval(Container, "DataItem.id_empleado")  %>' runat="server">
												</asp:HyperLink>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:BoundColumn DataField="NombreTipoSolicitud" HeaderText="Tipo">
											<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="NombreSolicitudEstado" HeaderText="Estado">
											<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="NombrePlanSolicitud" HeaderText="Plan">
											<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
										</asp:BoundColumn>
										<asp:TemplateColumn HeaderText="Empleado">
											<ItemStyle Width="18%"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblEmpleado" runat="server"></asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:BoundColumn DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy} ">
											<ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="ValorTotalConvenioSolicitado" HeaderText="Total Convenio o Solicitado"
											DataFormatString="{0:$ #,##0;($#,##0)}">
											<ItemStyle HorizontalAlign="Right" Width="14%"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="ValorTotalAprobado" HeaderText="Total Aprobado" DataFormatString="{0:$ #,##0;($#,##0)}">
											<ItemStyle HorizontalAlign="Right" Width="13%"></ItemStyle>
										</asp:BoundColumn>
									</Columns>
									<PagerStyle Mode="NumericPages"></PagerStyle>
								</asp:datagrid></DIV></ContentTemplate></asp:updatePanel></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="2"><asp:button id="btnAsociar" runat="server" CssClass="button" Text="Asociar"></asp:button></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="2"></TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
