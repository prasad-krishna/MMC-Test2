<%@ Page language="c#" Codebehind="LIS_historicosolicitudservicios.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_empleado.forma.LIS_historicosolicitudservicios" %>
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
		<script language="javascript">
			function ShowMenu(sender, idEmployee)
			{					
				var hdnId = document.getElementById("hdnId");
				
				var hplConsultar = document.getElementById("hplConsultar");
				var hplBeneficiarios =  document.getElementById("hplBeneficiarios"); 
				var hplSolicitudes =  document.getElementById("hplSolicitudes"); 
				var hplAutorizacion =  document.getElementById("hplAutorizacion");
				var hplReembolso =  document.getElementById("hplReembolso");
				
				hplConsultar.href = hplConsultar.href.replace(hdnId.value, idEmployee);
				hplBeneficiarios.href = hplBeneficiarios.href.replace(hdnId.value, idEmployee);	
				hplSolicitudes.href = hplSolicitudes.href.replace(hdnId.value, idEmployee);
				hplReembolso.href = hplReembolso.href.replace(hdnId.value, idEmployee);	
				hplAutorizacion.href = hplAutorizacion.href.replace(hdnId.value, idEmployee);						
				
				var left = Narg_GetPosX(sender) - 0;
				var top = Narg_GetPosY(sender) + 15;          
				
				hdnId.value = idEmployee;
				  
				setTimeout('showLayer("Menu",' + left + ',' +  top + ');', 50);	
																
			} 		


		</script>
	</HEAD>
	<body onload="CargarConfiguracion()" bottomMargin="5" topMargin="5">
		<form id="Form1" method="post" runat="server">
			<table id="Table1" cellSpacing="0" cellPadding="2" width="100%" align="center" border="0">
				<TR>
					<TD align="center">
						<TABLE class="tableBorder" id="Table3" cellSpacing="0" cellPadding="3" width="60%">
							<TR>
								<TD class="titleBackBlue" width="20%" colSpan="4">HISTÓRICO DE SERVICIOS Y 
									PRODUCTOS</TD>
							</TR>
							<TR>
								<TD width="25%">Tipo de Servicio</TD>
								<TD colSpan="2" style="HEIGHT: 26px"><STRONG>
										<asp:dropdownlist id="ddlTipoServicio" runat="server" Width="200px" CssClass="textBox"></asp:dropdownlist></STRONG></TD>
								<TD style="HEIGHT: 26px"></TD>
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
					<td align="center"><asp:datagrid id="dtgSolicitudServicios" runat="server" CssClass="grid" Width="100%" CellPadding="2"
							AllowPaging="True" AutoGenerateColumns="False" GridLines="Horizontal" PageSize="15">
							<AlternatingItemStyle CssClass="altItems"></AlternatingItemStyle>
							<ItemStyle CssClass="norItems"></ItemStyle>
							<HeaderStyle CssClass="headerGrid"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="IdSolicitud"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="IdSolicitudServicio"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="IdSolicitudServicio"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="No.">
									<ItemStyle HorizontalAlign="Center" Width="7%"></ItemStyle>
									<ItemTemplate>
										<asp:Label Runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ConsecutivoNombre") %>' ID="lblConsecutivoNombre" >
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Servicio/Producto">
									<ItemStyle Width="25%"></ItemStyle>
									<ItemTemplate>
										<asp:Label runat="server" ID="lblProductoServicio" text='<%# DataBinder.Eval(Container, "DataItem.NombreCompletoMedicamento").ToString() + DataBinder.Eval(Container, "DataItem.NombreCompletoServicio").ToString() %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Sesio/Cant">
									<ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
									<ItemTemplate>
										<asp:Label runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Cantidad") %>' ID="lblCantidad" >
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Fecha Solicitud">
									<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
									<ItemTemplate>
										<asp:Label runat="server" text='<%# string.Format("{0:dd/MM/yyyy}",DataBinder.Eval(Container, "DataItem.Fecha")) %>' ID="lblFecha" >
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Fecha Prestación">
									<ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
									<ItemTemplate>
										<asp:Label runat="server" text='<%# string.Format("{0:dd/MM/yyyy}",DataBinder.Eval(Container, "DataItem.FechaPrestacion")) %>' ID="lblFechaPrestacion" >
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Estado">
									<ItemStyle Width="8%"></ItemStyle>
									<ItemTemplate>
										<asp:Label runat="server" ID="lblEstado" text='<%# DataBinder.Eval(Container, "DataItem.NombreSolicitudEstado").ToString() %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Diagnósticos">
									<ItemStyle Width="20%"></ItemStyle>
									<ItemTemplate>
										<asp:Label CssClass="textSmallBlack" id="lblDiagnosticos" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Prestador">
									<ItemStyle Width="20%"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblPrestador" text='<%# DataBinder.Eval(Container, "DataItem.NombreProveedor") %>' runat="server">
										</asp:Label>
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
