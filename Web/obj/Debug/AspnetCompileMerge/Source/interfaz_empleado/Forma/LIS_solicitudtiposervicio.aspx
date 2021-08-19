<%@ Page language="c#" Codebehind="LIS_solicitudtiposervicio.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_empleado.forma.LIS_solicitudltiposervicio" %>
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
			
			function ShowMenu(sender,idSolicitud,idSolcitudTipoServicio, tipoSolicitud, idEstado, employee_id, siniestrTPAAnterior, idConsulta)
			{
				var hplCopiar = document.getElementById("hplCopiar");			
				var hplCopiarCompleta = document.getElementById("hplCopiarCompleta");	
				var hplCopiarAnular = document.getElementById("hplCopiarAnular");	
				var hplCopiarAnularCompleta = document.getElementById("hplCopiarAnularCompleta");	
				var hplAnular = document.getElementById("hplAnular");	
				var hplAnularCompleta = document.getElementById("hplAnularCompleta");	
				var hplVer = document.getElementById("hplVer");	
				var hplVerCompleta = document.getElementById("hplVerCompleta");	
				var trCopiarAnular = document.getElementById("trCopiarAnular");	
				var trCopiarAnularCompleta = document.getElementById("trCopiarAnularCompleta");	
				var trAnular = document.getElementById("trAnular");	
				var trAnularCompleta = document.getElementById("trAnularCompleta");	
				var trCopiar = document.getElementById("trCopiar");	
				var trCopiarCompleta = document.getElementById("trCopiarCompleta");	
				var trVer = document.getElementById("trVer");	
								
				if(idEstado != 1 && idEstado != 3)
				{					
					trCopiarAnular.style.display = "none";
					trCopiarAnularCompleta.style.display = "none";
					trAnular.style.display = "none";
					trAnularCompleta.style.display = "none";
					
					if(siniestrTPAAnterior != "0")
					{
						trCopiar.style.display = "none";
						trCopiarCompleta.style.display = "none";
					}	
					else
					{
						trCopiar.style.display = "";
						trCopiarCompleta.style.display = "";
					}
				}
				else
				{
					
					trCopiarAnular.style.display = "";
					trCopiarAnularCompleta.style.display = "";
					trAnular.style.display = "";
					trAnularCompleta.style.display = "";
					
					if(siniestrTPAAnterior != "0")
					{
						trCopiarAnular.style.display = "none";
						trCopiarAnularCompleta.style.display = "none";								
						trCopiar.style.display = "none";
						trCopiarCompleta.style.display = "none";
					}	
					else
					{
						trCopiarAnular.style.display = "";
						trCopiarAnularCompleta.style.display = "";								
						trCopiar.style.display = "";
						trCopiarCompleta.style.display = "";
					}
					
				}				
								
				
				if(tipoSolicitud == 1)
				{
					hplCopiar.href =  "AE_solicitudreembolso.aspx?IdSolicitudCopia=" + idSolicitud  + "&employee_id=" + employee_id + "&IdSolicitudTipoServicioCopia=" + idSolcitudTipoServicio
					hplCopiarCompleta.href = "AE_solicitudreembolso.aspx?IdSolicitudCopia=" + idSolicitud  + "&employee_id=" + employee_id
					if(idEstado == 1 || idEstado == 3)
					{
						hplCopiarAnular.href = "AE_solicitudreembolso.aspx?IdSolicitudCopia=" + idSolicitud  + "&employee_id=" + employee_id + "&Anular=1" + "&IdSolicitudTipoServicioCopia=" + idSolcitudTipoServicio
						hplCopiarAnularCompleta.href =   "AE_solicitudreembolso.aspx?IdSolicitudCopia=" + idSolicitud  + "&employee_id=" + employee_id + "&Anular=1"
					}
			
					hplVer.href = "AE_solicitudreembolsoresumen.aspx?IdSolicitud=" + idSolicitud  + "&employee_id=" + employee_id + "&IdSolicitudTipoServicio=" + idSolcitudTipoServicio
					hplVerCompleta.href = "AE_solicitudreembolsoresumen.aspx?IdSolicitud=" + idSolicitud  + "&employee_id=" + employee_id
					
										
				}
				else
				{
					if(tipoSolicitud == 2)
					{
						hplCopiar.href =  "AE_solicitudautorizacion.aspx?IdSolicitudCopia=" + idSolicitud  + "&employee_id=" + employee_id + "&IdSolicitudTipoServicioCopia=" + idSolcitudTipoServicio
						hplCopiarCompleta.href = "AE_solicitudautorizacion.aspx?IdSolicitudCopia=" + idSolicitud  + "&employee_id=" + employee_id
						if(idEstado == 1 || idEstado == 3)
						{
							hplCopiarAnular.href = "AE_solicitudautorizacion.aspx?IdSolicitudCopia=" + idSolicitud  + "&employee_id=" + employee_id + "&Anular=1" + "&IdSolicitudTipoServicioCopia=" + idSolcitudTipoServicio
							hplCopiarAnularCompleta.href =   "AE_solicitudautorizacion.aspx?IdSolicitudCopia=" + idSolicitud  + "&employee_id=" + employee_id + "&Anular=1"
						}
						hplVer.href =  "AE_solicitudautorizacionresumen.aspx?IdSolicitud=" + idSolicitud  + "&employee_id=" + employee_id + "&IdSolicitudTipoServicio=" + idSolcitudTipoServicio
						hplVerCompleta.href = "AE_solicitudautorizacionresumen.aspx?IdSolicitud=" + idSolicitud  + "&employee_id=" + employee_id
						
					}
					else
					{
						trCopiarAnular.style.display = "none";
						trCopiarAnularCompleta.style.display = "none";
						trAnular.style.display = "none";
						trAnularCompleta.style.display = "none";
						trCopiar.style.display = "none";
						trCopiarCompleta.style.display = "none";
						trVer.style.display = "none";				
						
						hplVerCompleta.href = "AE_solicitudordenresumen.aspx?IdSolicitud=" + idSolicitud  + "&employee_id=" + employee_id + "&IdConsulta=" +  idConsulta
						
					}					
				}
				if(idEstado == 1 || idEstado == 3)
				{					
					hplAnular.href = "javascript:openPopUp('AE_solicitudestado.aspx?IdSolicitud=" + idSolicitud + "&IdSolicitudTipoServicio=" + idSolcitudTipoServicio + "&IdEstado=5'" + " ,550,330)"	
					hplAnularCompleta.href = "javascript:openPopUp('AE_solicitudestado.aspx?IdSolicitud=" + idSolicitud + "&IdEstado=5'" + " ,550,330)"	
				}	
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
						<TABLE class="tableBorder" id="tblBuscar" cellSpacing="0" cellPadding="5" width="80%">
							<TR>
								<TD class="titleBackBlue" width="20%" colSpan="4">Buscador de Solicitudes</TD>
							</TR>
							<TR>
								<TD width="20%">No. de Solicitud</TD>
								<TD width="30%">
									<asp:textbox id="txtNoSolicitud" runat="server" CssClass="textBox" Width="100px"></asp:textbox></TD>
								<TD width="20%">Prestador Asignado</TD>
								<TD width="30%">
									<asp:dropdownlist id="ddlProveedorBus" runat="server" CssClass="textBoxSmall" Width="240px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD>Fecha Desde</TD>
								<TD width="30%">
									<asp:textbox id="txtFechaInicio" runat="server" CssClass="textBox" Width="80px"></asp:textbox>&nbsp;<A href="javascript:MostrarCalendario(Form1.txtFechaInicio,Form1.txtFechaInicio,'dd/mm/yyyy');"
										name="btnFechaInicio"><IMG src="../../images/icoCalendar.gif" border="0"></A>
								</TD>
								<TD>Fecha Hasta</TD>
								<TD>
									<asp:textbox id="txtFechaFin" runat="server" CssClass="textBox" Width="80px"></asp:textbox>&nbsp;<A href="javascript:MostrarCalendario(Form1.txtFechaFin,Form1.txtFechaFin,'dd/mm/yyyy');"
										name="btnFechaFin"><IMG src="../../images/icoCalendar.gif" border="0"></A>
								</TD>
							</TR>
							<TR>
								<TD>Identificación Empleado</TD>
								<TD width="30%">
									<asp:textbox id="txtIdEmpleado" runat="server" CssClass="textBox" Width="130px"></asp:textbox></TD>
								<TD>Identificación Beneficiario</TD>
								<TD>
									<asp:textbox id="txtIdBeneficiario" runat="server" CssClass="textBox" Width="130px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>Estado</TD>
								<TD width="30%">
									<asp:dropdownlist id="ddlEstado" runat="server" CssClass="textBox" Width="130px"></asp:dropdownlist></TD>
								<TD>Tipo Servicio</TD>
								<TD>
									<asp:dropdownlist id="ddlTipoServicio" runat="server" Width="240px" CssClass="textBoxSmall"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD align="center" colSpan="4">
									<asp:button id="btnBuscar" runat="server" CssClass="button" Text="Buscar" CausesValidation="False"></asp:button></TD>
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
								<asp:BoundColumn Visible="False" DataField="IdTipoSolicitud" HeaderText="IdTipoSolicitud"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Menú">
									<ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
									<ItemTemplate>
										<IMG 
      onclick='<%# "javascript:ShowMenu(this," + DataBinder.Eval(Container,"DataItem.IdSolicitud") + "," + DataBinder.Eval(Container,"DataItem.IdSolicitudTipoServicio") + "," + DataBinder.Eval(Container,"DataItem.IdTipoSolicitud") + "," + DataBinder.Eval(Container,"DataItem.IdSolicitudEstado")  + "," + DataBinder.Eval(Container,"DataItem.id_empleado") + "," + DataBinder.Eval(Container,"DataItem.siniestrTPAAnterior") + "," +  DataBinder.Eval(Container,"DataItem.IdConsulta") + ");" %>' 
      src="../../images/icoMenu.gif">
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Ver">
									<ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton CausesValidation="false" CssClass="textSmallBlack" CommandArgument="IdSolicitud"
											CommandName="Ver" id="lnkVer" runat="server">
											<%#  DataBinder.Eval(Container,"DataItem.NoSolicitud")%>
										</asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="NombreSolicitudEstado" HeaderText="Estado">
									<itemstyle Width="8%" HorizontalAlign="Center"></itemstyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="NombrePlanSolicitud" HeaderText="Plan">
									<itemstyle Width="8%" HorizontalAlign="Center"></itemstyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Empleado">
									<itemstyle Width="17%"></itemstyle>
									<ItemTemplate>
										<asp:label id="lblEmpleado" runat="server"></asp:label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Paciente">
									<ItemStyle Width="18%"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblPaciente" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy} ">
									<ItemStyle Width="6%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="NombreTipoServicio" HeaderText="Tipo Servicio">
									<ItemStyle Width="20%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ValorAprobado" HeaderText="Valor Aprobado" DataFormatString="{0:#,##0;($#,##0)}">
									<ItemStyle HorizontalAlign="Right" Width="7%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="IdConsulta" HeaderText="IdConsulta"></asp:BoundColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</asp:datagrid><BR>
						<BR>
					</TD>
				</TR>
			</table>
			<iframe id="ifrMenu" style="DISPLAY: none; Z-INDEX: 1000; WIDTH: 220px; POSITION: absolute; HEIGHT: 182px"
				scrolling="no" frameBorder="0?" src="pagina.htm"></iframe>
			<div id="dvMenu" style="BORDER-RIGHT: dimgray 1px outset; BORDER-TOP: dimgray 1px outset; DISPLAY: none; Z-INDEX: 1100; OVERFLOW: auto; BORDER-LEFT: dimgray 1px outset; WIDTH: 220px; BORDER-BOTTOM: dimgray 1px outset; POSITION: absolute; HEIGHT: 182px; BACKGROUND-COLOR: white">
				<table style="BORDER-RIGHT: gray 1px solid; BORDER-TOP: gray 1px solid; BORDER-LEFT: gray 1px solid; BORDER-BOTTOM: gray 1px solid; BORDER-COLLAPSE: collapse"
					cellSpacing="0" cellPadding="3" width="100%" border="1">
					<TBODY>
						<tr height="5">
							<td align="left"><asp:image id="Image1" style="CURSOR: pointer" onclick="closeLayer('Menu')" runat="server"
									ImageUrl="../../images/imgClose.gif"></asp:image></td>
						</tr>
						<tr id="trCopiar">
							<td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplCopiar" href="#">Copiar</A>
							</td>
						</tr>
						<tr id="trCopiarCompleta">
							<td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplCopiarCompleta" href="#">Copiar 
									solicitud completa</A>
							</td>
						</tr>
						<tr id="trCopiarAnular">
							<td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplCopiarAnular" href="#">Copiar 
									y anular</A>
							</td>
						</tr>
						<tr id="trCopiarAnularCompleta">
							<td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplCopiarAnularCompleta" href="#">Copiar 
									y anular solicitud completa</A>
							</td>
						</tr>
						<tr id="trAnular">
							<td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplAnular" href="#">Anular</A>
							</td>
						</tr>
						<tr id="trAnularCompleta">
							<td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplAnularCompleta" href="#">Anular 
									solicitud completa</A>
							</td>
						</tr>
						<tr id="trVer">
							<td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplVer" href="#">Ver</A>
							</td>
						</tr>
						<tr id="trVerCompleta">
							<td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplVerCompleta" href="#">Ver 
									solicitud completa</A>
							</td>
						</tr>
						</TD></TR></TBODY></table>
			</div>
		</form>
	</body>
</HTML>
