<%@ Page language="c#" Codebehind="LIS_solicitudliquidaciones.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_empleado.forma.LIS_solicitudliquidaciones" %>
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
		<script language="javascript">			
			
			function ShowMenu(sender, idSolicitud, employee_id)
			{
				var hplVer = document.getElementById("hplVer");			
				var hplReversar =  document.getElementById("hplReversar"); 							
				
				hplVer.href = "javascript:openPopUp('AE_SolicitudAutorizacion.aspx?IdSolicitud=" + idSolicitud  + "&employee_id=" + employee_id + "&popup=1',950,850)"
								
				var left = Narg_GetPosX(sender) - 0;
				var top = Narg_GetPosY(sender) + 15;            
				setTimeout('showLayer("Menu",' + left + ',' +  top + ');', 50);					
																
			} 			
		</script>
	</HEAD>
	<body onload="CargarConfiguracion();">
		<form id="Form1" method="post" runat="server">
			<table class="GG" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
				<TR>
					<TD align="center" colSpan="2">&nbsp;
						<TABLE class="tableBorder" id="tblBuscar" cellSpacing="0" cellPadding="3" width="80%">
							<TR>
								<TD class="titleBackBlue" width="20%" colSpan="4">Buscador de Solicitudes</TD>
							</TR>
							<TR>
								<TD width="20%">Prestador Asignado</TD>
								<TD width="30%" colSpan="3">
									<asp:dropdownlist id="ddlProveedorBus" runat="server" Width="400px" CssClass="textBoxSmall"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD width="20%">No. de Solicitud</TD>
								<TD width="30%">
									<asp:textbox id="txtNoSolicitud" runat="server" Width="100px" CssClass="textBox"></asp:textbox></TD>
								<TD width="18%">Factura</TD>
								<TD width="32%">
									<asp:textbox id="txtFactura" runat="server" Width="130px" CssClass="textBox"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>Fecha Desde</TD>
								<TD width="30%">
									<asp:textbox id="txtFechaInicio" runat="server" Width="80px" CssClass="textBox"></asp:textbox>&nbsp;<A href="javascript:MostrarCalendario(Form1.txtFechaInicio,Form1.txtFechaInicio,'dd/mm/yyyy');"
										name="btnFechaInicio"><IMG src="../../images/icoCalendar.gif" border="0"></A>
								</TD>
								<TD>Fecha Hasta</TD>
								<TD>
									<asp:textbox id="txtFechaFin" runat="server" Width="80px" CssClass="textBox"></asp:textbox>&nbsp;<A href="javascript:MostrarCalendario(Form1.txtFechaFin,Form1.txtFechaFin,'dd/mm/yyyy');"
										name="btnFechaFin"><IMG src="../../images/icoCalendar.gif" border="0"></A>
								</TD>
							</TR>
							<TR>
								<TD>Fecha Creación Desde</TD>
								<TD width="30%">
									<asp:textbox id="txtFechaCreacionInicio" runat="server" Width="80px" CssClass="textBox"></asp:textbox>&nbsp;<A href="javascript:MostrarCalendario(Form1.txtFechaCreacionInicio,Form1.txtFechaCreacionInicio,'dd/mm/yyyy');"
										name="btnFechaInicio"><IMG src="../../images/icoCalendar.gif" border="0"></A></TD>
								<TD>Fecha Creación Hasta</TD>
								<TD>
									<asp:textbox id="txtFechaCreacionFin" runat="server" Width="80px" CssClass="textBox"></asp:textbox>&nbsp;<A href="javascript:MostrarCalendario(Form1.txtFechaCreacionFin,Form1.txtFechaCreacionFin,'dd/mm/yyyy');"
										name="btnFechaInicio"><IMG src="../../images/icoCalendar.gif" border="0"></A></TD>
							</TR>
							<TR>
								<TD>Identificación Empleado</TD>
								<TD width="30%">
									<asp:textbox id="txtIdEmpleado" runat="server" Width="130px" CssClass="textBox"></asp:textbox></TD>
								<TD>Identificación Beneficiario</TD>
								<TD>
									<asp:textbox id="txtIdBeneficiario" runat="server" Width="130px" CssClass="textBox"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>Fecha Confirmación</TD>
								<TD width="30%">
									<asp:textbox id="txtFechaConfirmacion" runat="server" CssClass="textBox" Width="80px"></asp:textbox>&nbsp;<A href="javascript:MostrarCalendario(Form1.txtFechaConfirmacion,Form1.txtFechaConfirmacion,'dd/mm/yyyy');"
										name="btnFechaInicio"><IMG src="../../images/icoCalendar.gif" border="0"></A></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD width="30%"></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 22px"></TD>
								<TD style="HEIGHT: 22px" width="30%" colSpan="3">
									<asp:dropdownlist id="ddlMes" runat="server" Width="100px" CssClass="textBox" Visible="False">
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
									<asp:dropdownlist id="ddlAno" runat="server" Width="70px" CssClass="textBox" Visible="False"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD align="center" colSpan="4">
									<asp:button id="btnBuscar" runat="server" CssClass="button" CausesValidation="False" Text="Buscar"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD align="left" colSpan="2" height="15">
					</TD>
				</TR>
				<TR>
					<TD colSpan="2" align="center">
						<asp:Label id="lblMensaje" runat="server" CssClass="titleBlue"></asp:Label>
						<asp:datagrid id="dtgSolicitudes" runat="server" Width="100%" CssClass="grid" PageSize="20" AllowPaging="True"
							AutoGenerateColumns="False" GridLines="Horizontal" CellPadding="1">
							<AlternatingItemStyle CssClass="altItems"></AlternatingItemStyle>
							<ItemStyle CssClass="norItems"></ItemStyle>
							<HeaderStyle CssClass="headerGrid"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="IdSolicitud"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="id_solicitud_SICAU"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="id_empleado" HeaderText="Empleado"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="IdSolicitudTipoServicio" HeaderText="IdSolicitudTipoServicio"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="beneficiario_id" HeaderText="Beneficiario"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Menú">
									<ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
									<ItemTemplate>
										<IMG 
      onclick='<%# "javascript:ShowMenu(this," + DataBinder.Eval(Container,"DataItem.IdSolicitud") + "," + DataBinder.Eval(Container,"DataItem.id_empleado") + ");" %>' 
      src="../../images/icoMenu.gif">
									</ItemTemplate>
								</asp:TemplateColumn>
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
			</table>
			<iframe id="ifrMenu" style="DISPLAY: none; Z-INDEX: 1000; WIDTH: 200px; POSITION: absolute; HEIGHT: 74px"
				scrolling="no" frameBorder="0?" src="pagina.htm"></iframe>
			<div id="dvMenu" style="BORDER-RIGHT: dimgray 1px outset; BORDER-TOP: dimgray 1px outset; DISPLAY: none; Z-INDEX: 1100; OVERFLOW: auto; BORDER-LEFT: dimgray 1px outset; WIDTH: 200px; BORDER-BOTTOM: dimgray 1px outset; POSITION: absolute; HEIGHT: 74px; BACKGROUND-COLOR: white">
				<table style="BORDER-RIGHT: gray 1px solid; BORDER-TOP: gray 1px solid; BORDER-LEFT: gray 1px solid; BORDER-BOTTOM: gray 1px solid; BORDER-COLLAPSE: collapse"
					cellSpacing="0" cellPadding="5" width="100%" border="1">
					<TBODY>
						<tr height="5">
							<td align="left"><asp:image id="Image1" style="CURSOR: pointer" onclick="closeLayer('Menu')" runat="server"
									ImageUrl="../../images/imgClose.gif"></asp:image></td>
						</tr>
						<tr>
							<td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplVer" href="#">Ver</A>
							</td>
						</tr>
						<tr style="DISPLAY:none">
							<td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplReversar" href="#">Reversar</A>
							</td>
						</tr>
						</TD></TR></TBODY></table>
			</div>
		</form>
	</body>
</HTML>
