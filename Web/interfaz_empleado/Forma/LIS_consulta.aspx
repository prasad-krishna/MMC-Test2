
<%@ Page language="c#" Codebehind="LIS_consulta.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_empleado.forma.LIS_consulta" %>

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
			
			function ShowMenu(sender, idConsulta, employee_id, idSolicitud, idTipoConsulta, finalizada)
			{
			    var hplVerResumen = document.getElementById("hplVerResumen");
			    var hplEditar = document.getElementById("hplEditar");
			    var hplSolicitudes = document.getElementById("hplSolicitudes");
			    var hplModificar = document.getElementById("hplModificar");

			    hplEditar.href = "AE_registroconsulta.aspx?IdConsulta=" + idConsulta + "&employee_id=" + employee_id + "&TipoConsulta=" + idTipoConsulta;
			    hplVerResumen.href = "javascript:openPopUp('AE_solicitudordenresumen.aspx?IdConsulta=" + idConsulta + "&employee_id=" + employee_id + "&Resumen=1',900,700)";
			    hplSolicitudes.href = "AE_solicitudorden.aspx?IdSolicitud=" + idSolicitud + "&IdConsulta=" + idConsulta + "&employee_id=" + employee_id + "&editar=1&adicion=1";
			    hplModificar.href = "AE_registroconsulta.aspx?IdConsulta=" + idConsulta + "&employee_id=" + employee_id + "&editar=1" + "&finalizada=" + finalizada + "&TipoConsulta=" + idTipoConsulta;
			    document.getElementById("trModificar").style.display = "";
//			    if (finalizada == 0)
//			        document.getElementById("trModificar").style.display = "";
//			    else
//			        document.getElementById("trModificar").style.display = "none";
			         
			    var left = Narg_GetPosX(sender) - 0;
			    var top = Narg_GetPosY(sender) + 15;
			    setTimeout('showLayer("Menu",' + left + ',' + top + ');', 50);	
			}
		</script>
</HEAD>
	<body onload="CargarConfiguracion();document.Form1.txtNoConsulta.focus();">
		<form id="Form1" method="post" runat="server">
		<asp:ScriptManager ID="scrMng" runat="server"></asp:ScriptManager>
			<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
				<TR>
					<TD align="center" colSpan="2">&nbsp;
						<TABLE class="tableBorder" id="tblBuscar" cellSpacing="0" cellPadding="5" width="80%">
							<TR>
								<TD class="titleBackBlue" width="20%" colSpan="4">Buscador de Consultas</TD>
							</TR>
							<TR>
								<TD width="20%">No. de Consulta</TD>
								<TD width="30%"><asp:textbox id="txtNoConsulta" runat="server" Width="100px" CssClass="textBox"></asp:textbox></TD>
								<TD width="20%">Médico</TD>
								<TD width="30%"><asp:dropdownlist id="ddlMedico" runat="server" Width="240px" CssClass="textBoxSmall"></asp:dropdownlist></TD>
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
								<TD align="center" colSpan="4"><asp:button id="btnBuscar" runat="server" CssClass="button" CausesValidation="False" Text="Buscar"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD align="left" colSpan="2" height="15"></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="2"><asp:updatePanel id="Ajaxpanel2" runat="server" ><ContentTemplate><BR><BR>
<ASP:DATAGRID id=dtgConsultas runat="server" CssClass="grid" Width="100%" PageSize="20" AllowPaging="True" AutoGenerateColumns="False" GridLines="Horizontal" CellPadding="1">
								<AlternatingItemStyle CssClass="altItems"></AlternatingItemStyle>
								<ItemStyle CssClass="norItems"></ItemStyle>
								<HeaderStyle CssClass="headerGrid"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="IdConsulta"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="id_empleado"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="beneficiario_id"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Menú">
										<ItemStyle HorizontalAlign="Center" Width="1%"></ItemStyle>
										<ItemTemplate>
											<IMG 
      onclick='<%# "javascript:ShowMenu(this," + DataBinder.Eval(Container,"DataItem.IdConsulta") + "," + DataBinder.Eval(Container,"DataItem.id_empleado") +  "," + DataBinder.Eval(Container,"DataItem.IdSolicitud") +  "," + DataBinder.Eval(Container,"DataItem.IdTipoConsulta") +  "," + DataBinder.Eval(Container,"DataItem.Finalizada") + ");" %>' 
      src="../../images/icoMenu.gif">
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="No. Consulta">
										<ItemStyle Width="4%"></ItemStyle>
										<ItemTemplate>
											<ASP:HYPERLINK id=hplName runat="server" CssClass="textSmallBlack" NavigateUrl='<%# "AE_registroconsulta.aspx?IdConsulta=" + DataBinder.Eval(Container, "DataItem.IdConsulta") + "&amp;employee_id=" + DataBinder.Eval(Container, "DataItem.id_empleado") + "&amp;TipoConsulta=" + DataBinder.Eval(Container, "DataItem.IdTipoConsulta")  %>'>
												<%# DataBinder.Eval(Container, "DataItem.ConsecutivoNombre") %>
											</ASP:HYPERLINK>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="TipoConsulta" HeaderText="Tipo">
										<ItemStyle HorizontalAlign="Center" Width="7%"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Empleado">
										<ItemStyle Width="9%"></ItemStyle>
										<ItemTemplate>
											<ASP:LABEL id="lblEmpleado" runat="server">
											</ASP:LABEL>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Paciente">
										<ItemStyle Width="9%"></ItemStyle>
										<ItemTemplate>
											<ASP:LABEL id="lblPaciente" runat="server"></ASP:LABEL>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="NombrePrestador" HeaderText="Médico">
										<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="FechaCreacion" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy} ">
										<ItemStyle HorizontalAlign="Center" Width="4%"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</ASP:DATAGRID>
						</ContentTemplate></asp:updatePanel></TD>
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
							<td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplSolicitudes" href="#">Modificar 
									Órdenes</A>
							</td>
						</tr>
						<tr>
							<td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplEditar" href="#">Ver</A>
							</td>
						</tr>
						<tr>
							<td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplVerResumen" href="#">Ver 
									Resumen</A>
							</td>
						</tr>
						<tr id="trModificar" style="display:none">
							<td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplModificar" href="#">Modificar Consulta</A>
							</td>
						</tr></TD></TR></TBODY></table>
			</div>
		</form>
	</body>
</HTML>
