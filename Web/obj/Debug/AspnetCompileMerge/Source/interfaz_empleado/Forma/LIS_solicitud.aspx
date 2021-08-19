<%@ Page language="c#" Codebehind="LIS_solicitud.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_empleado.forma.LIS_solicitud" %>
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
			
			function ShowMenu(sender, idSolicitud, employee_id, tipoSolicitud, idConsulta, idEstado, siniestrTPAAnterior)
			{
				
				
				var hplVerResumen = document.getElementById("hplVerResumen");
				var hplCopiar = document.getElementById("hplCopiar");
				var hplEditar =  document.getElementById("hplEditar"); 
				var hplAnular =  document.getElementById("hplAnular"); 	
				var trCopiar = document.getElementById("trCopiar");			
				
				if(siniestrTPAAnterior != "0")
				{					
					trCopiar.style.display = "none";					
				}	
				else
				{
					trCopiar.style.display = "";
				}	
				
				if(idEstado != 1 && idEstado != 3)
				{					
					trAnular.style.display = "none";						
				}
				else
				{
					trAnular.style.display = "";						
				}	
				
				if(tipoSolicitud == 1)
				{
					hplEditar.href = "AE_solicitudreembolso.aspx?IdSolicitud=" + idSolicitud  + "&employee_id=" + employee_id
					hplVerResumen.href = "javascript:openPopUp('AE_solicitudreembolsoresumen.aspx?IdSolicitud=" + idSolicitud  + "&employee_id=" + employee_id + "&Resumen=1',900,700)"
					hplCopiar.href = "AE_solicitudreembolso.aspx?IdSolicitudCopia=" + idSolicitud  + "&employee_id=" + employee_id
										
				}
				else
				{
					if(tipoSolicitud == 2)
					{
						hplEditar.href = "AE_solicitudautorizacion.aspx?IdSolicitud=" + idSolicitud  + "&employee_id=" + employee_id
						hplVerResumen.href = "javascript:openPopUp('AE_solicitudautorizacionresumen.aspx?IdSolicitud=" + idSolicitud  + "&employee_id=" + employee_id + "&Resumen=1',900,700)"
						hplCopiar.href = "AE_solicitudautorizacion.aspx?IdSolicitudCopia=" + idSolicitud  + "&employee_id=" + employee_id
					}
					else
					{
						hplEditar.href = "AE_solicitudorden.aspx?IdSolicitud=" + idSolicitud  + "&employee_id=" + employee_id + "&IdConsulta=" + idConsulta
						hplVerResumen.href = "javascript:openPopUp('AE_solicitudordenresumen.aspx?IdSolicitud=" + idSolicitud  + "&employee_id=" + employee_id + "&Resumen=1" + + "&IdConsulta=" + idConsulta + "',900,700)"
						hplCopiar.href = "AE_solicitudorden.aspx?IdSolicitudCopia=" + idSolicitud  + "&employee_id=" + employee_id + "&IdConsulta=" + idConsulta
					}
					
				}	
				
				hplAnular.href = "javascript:openPopUp('AE_solicitudestado.aspx?IdSolicitud=" + idSolicitud + "&IdEstado=5'" + " ,550,330)"	
				
				
				var left = Narg_GetPosX(sender) - 0;
				var top = Narg_GetPosY(sender) + 15;            
				setTimeout('showLayer("Menu",' + left + ',' +  top + ');', 50);	
				
				
																
			} 			
		</script>
	</HEAD>
	<body onload="CargarConfiguracion();document.Form1.txtNoSolicitud.focus();">
		<form id="Form1" method="post" runat="server">
			<table class="GG" cellSpacing="0" cellPadding="1" width="100%" align="center" border="0">
				<TR>
					<TD align="center" colSpan="2">&nbsp;
						<TABLE class="tableBorder" id="tblBuscar" cellSpacing="0" cellPadding="5" width="80%">
							<TR>
								<TD class="titleBackBlue" width="20%" colSpan="4">Buscador de Solicitudes</TD>
							</TR>
							<TR>
								<TD width="20%">No. de Solicitud</TD>
								<TD width="30%"><asp:textbox id="txtNoSolicitud" runat="server" Width="100px" CssClass="textBox"></asp:textbox></TD>
								<TD width="20%">Prestador Asignado</TD>
								<TD width="30%"><asp:dropdownlist id="ddlProveedorBus" runat="server" Width="240px" CssClass="textBoxSmall"></asp:dropdownlist></TD>
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
								<TD>Identificación Empleado</TD>
								<TD width="30%"><asp:textbox id="txtIdEmpleado" runat="server" Width="130px" CssClass="textBox"></asp:textbox></TD>
								<TD>Identificación Beneficiario</TD>
								<TD><asp:textbox id="txtIdBeneficiario" runat="server" Width="130px" CssClass="textBox"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>Estado</TD>
								<TD width="30%"><asp:dropdownlist id="ddlEstado" runat="server" Width="130px" CssClass="textBox"></asp:dropdownlist></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD align="center" colSpan="4"><asp:button id="btnBuscar" runat="server" CssClass="button" CausesValidation="False" Text="Buscar"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD align="left" colSpan="2" height="15"></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="2">
							<ASP:DATAGRID id="dtgSolicitudes" runat="server" CssClass="grid" Width="100%" CellPadding="3"
								GridLines="Horizontal" AutoGenerateColumns="False" AllowPaging="True" PageSize="20">
								<ALTERNATINGITEMSTYLE CssClass="altItems"></ALTERNATINGITEMSTYLE>
								<ITEMSTYLE CssClass="norItems"></ITEMSTYLE>
								<HEADERSTYLE CssClass="headerGrid"></HEADERSTYLE>
								<COLUMNS>
									<ASP:BOUNDCOLUMN DataField="IdSolicitud" Visible="False"></ASP:BOUNDCOLUMN>
									<ASP:BOUNDCOLUMN DataField="id_empleado" Visible="False"></ASP:BOUNDCOLUMN>
									<ASP:BOUNDCOLUMN DataField="beneficiario_id" Visible="False"></ASP:BOUNDCOLUMN>
									<ASP:TEMPLATECOLUMN HeaderText="Menú">
										<ITEMSTYLE Width="3%" HorizontalAlign="Center"></ITEMSTYLE>
										<ITEMTEMPLATE>
											<IMG 
      onclick='<%# "javascript:ShowMenu(this," + DataBinder.Eval(Container,"DataItem.IdSolicitud") + "," + DataBinder.Eval(Container,"DataItem.id_empleado") + "," + DataBinder.Eval(Container,"DataItem.IdTipoSolicitud") +  "," +  (Convert.IsDBNull(DataBinder.Eval(Container,"DataItem.IdConsulta")) ? "0" : DataBinder.Eval(Container,"DataItem.IdConsulta"))  + "," + DataBinder.Eval(Container,"DataItem.IdSolicitudEstado") + "," + DataBinder.Eval(Container,"DataItem.siniestrTPAAnterior") +  ");" %>' 
      src="../../images/icoMenu.gif">
										</ITEMTEMPLATE>
									</ASP:TEMPLATECOLUMN>
									<ASP:TEMPLATECOLUMN HeaderText="No. Solicitud">
										<ITEMSTYLE Width="6%"></ITEMSTYLE>
										<ITEMTEMPLATE>
											<ASP:HYPERLINK id=hplName runat="server" CssClass="textSmallBlack" NavigateUrl='<%# Convert.ToInt32(DataBinder.Eval(Container, "DataItem.IdTipoSolicitud")) == 1 ? "AE_solicitudreembolso.aspx?IdSolicitud=" + DataBinder.Eval(Container, "DataItem.IdSolicitud") + "&amp;employee_id=" + DataBinder.Eval(Container, "DataItem.id_empleado") : Convert.ToInt32(DataBinder.Eval(Container, "DataItem.IdTipoSolicitud")) == 2 ? "AE_solicitudautorizacion.aspx?IdSolicitud=" + DataBinder.Eval(Container, "DataItem.IdSolicitud") + "&amp;employee_id=" + DataBinder.Eval(Container, "DataItem.id_empleado") :  "AE_solicitudorden.aspx?IdSolicitud=" + DataBinder.Eval(Container, "DataItem.IdSolicitud") + "&amp;employee_id=" + DataBinder.Eval(Container, "DataItem.id_empleado") + "&amp;IdConsulta=" +  DataBinder.Eval(Container,"DataItem.IdConsulta")%>'>
												<%# DataBinder.Eval(Container, "DataItem.ConsecutivoNombre") %>
											</ASP:HYPERLINK>
										</ITEMTEMPLATE>
									</ASP:TEMPLATECOLUMN>
									<ASP:BOUNDCOLUMN DataField="NombreSolicitudEstado" HeaderText="Estado">
										<ITEMSTYLE Width="8%" HorizontalAlign="Center"></ITEMSTYLE>
									</ASP:BOUNDCOLUMN>
									<ASP:BOUNDCOLUMN DataField="NombrePlanSolicitud" HeaderText="Plan">
										<ITEMSTYLE Width="8%" HorizontalAlign="Center"></ITEMSTYLE>
									</ASP:BOUNDCOLUMN>
									<ASP:TEMPLATECOLUMN HeaderText="Empleado">
										<ITEMSTYLE Width="17%"></ITEMSTYLE>
										<ITEMTEMPLATE>
											<ASP:LABEL id="lblEmpleado" runat="server"></ASP:LABEL>
										</ITEMTEMPLATE>
									</ASP:TEMPLATECOLUMN>
									<ASP:TEMPLATECOLUMN HeaderText="Paciente">
										<ITEMSTYLE Width="17%"></ITEMSTYLE>
										<ITEMTEMPLATE>
											<ASP:LABEL id="lblPaciente" runat="server"></ASP:LABEL>
										</ITEMTEMPLATE>
									</ASP:TEMPLATECOLUMN>
									<ASP:BOUNDCOLUMN DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy} ">
										<ITEMSTYLE Width="5%" HorizontalAlign="Center"></ITEMSTYLE>
									</ASP:BOUNDCOLUMN>
									<ASP:BOUNDCOLUMN DataField="ValorTotalConvenioSolicitado" HeaderText="Total Solicitado" DataFormatString="{0:$ #,##0;($#,##0)}">
										<ITEMSTYLE Width="12%" HorizontalAlign="Right"></ITEMSTYLE>
									</ASP:BOUNDCOLUMN>
									<ASP:BOUNDCOLUMN DataField="ValorTotalFactura" HeaderText="Total Facturado" DataFormatString="{0:$ #,##0;($#,##0)}">
										<ITEMSTYLE Width="12%" HorizontalAlign="Right"></ITEMSTYLE>
									</ASP:BOUNDCOLUMN>
									<ASP:BOUNDCOLUMN DataField="ValorTotalAprobado" HeaderText="Total Aprobado" DataFormatString="{0:$ #,##0;($#,##0)}">
										<ITEMSTYLE Width="12%" HorizontalAlign="Right"></ITEMSTYLE>
									</ASP:BOUNDCOLUMN>
								</COLUMNS>
								<PAGERSTYLE Mode="NumericPages"></PAGERSTYLE>
							</ASP:DATAGRID>
						</TD>
				</TR>
				<TR>
					<TD align="center" colSpan="2">
						<P>&nbsp;</P>
						<P>&nbsp;</P>
						<P>&nbsp;</P>
						<P>&nbsp;</P>
						<P>&nbsp;</P>
						<P>&nbsp;</P>
						<P>&nbsp;</P>
						<P>&nbsp;</P>
					</TD>
				</TR>
			</table>
			<iframe id="ifrMenu" style="DISPLAY: none; Z-INDEX: 1000; WIDTH: 200px; POSITION: absolute; HEIGHT: 122px"
				frameBorder="0" scrolling="no" src="pagina.htm"></iframe>
			<div id="dvMenu" style="BORDER-RIGHT: dimgray 1px outset; BORDER-TOP: dimgray 1px outset; DISPLAY: none; Z-INDEX: 1100; OVERFLOW: auto; BORDER-LEFT: dimgray 1px outset; WIDTH: 200px; BORDER-BOTTOM: dimgray 1px outset; POSITION: absolute; HEIGHT: 122px; BACKGROUND-COLOR: white">
				<table style="BORDER-RIGHT: gray 1px solid; BORDER-TOP: gray 1px solid; BORDER-LEFT: gray 1px solid; BORDER-BOTTOM: gray 1px solid; BORDER-COLLAPSE: collapse"
					cellSpacing="0" cellPadding="5" width="100%" border="1">
					<TBODY>
						<tr height="5">
							<td align="left"><asp:image id="Image1" style="CURSOR: pointer" onclick="closeLayer('Menu')" runat="server"
									ImageUrl="../../images/imgClose.gif"></asp:image></td>
						</tr>
						<tr>
							<td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplEditar" href="#">Editar</A>
							</td>
						</tr>
						<tr id="trCopiar">
							<td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplCopiar" href="#">Copiar</A>
							</td>
						</tr>
						<tr id="trAnular">
							<td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplAnular" href="#">Anular</A>
							</td>
						</tr>
						<tr>
							<td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplVerResumen" href="#">Ver 
									Resumen</A>
							</td>
						</tr>
						</TD></TR></TBODY></table>
			</div>
		</form>
	</body>
</HTML>
