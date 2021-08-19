
<%@ Page language="c#" Codebehind="AE_liquidarautconfirmacion.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_empleado.forma.AE_liquidarautconfirmacion" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
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
		<asp:ScriptManager ID="scrMng" runat="server"></asp:ScriptManager>
			<script language="javascript">
			
			function ActualizarConfirmadas()
			{	
				//__doPostBack('btnReload','');

			
			}
			</script>
			<table class="GG" cellSpacing="0" cellPadding="1" width="100%" align="center" border="0">
				<TR>
					<TD align="center" colSpan="2" height="15">
						<TABLE class="tableBorder" id="tblBuscar" cellSpacing="0" cellPadding="3" width="80%">
							<TR>
								<TD class="titleBackBlue" width="20%" colSpan="4">Buscador de Solicitudes</TD>
							</TR>
							<TR>
								<TD width="20%">Prestador Asignado</TD>
								<TD width="30%" colSpan="3"><asp:dropdownlist id="ddlProveedorBus" runat="server" CssClass="textBoxSmall" Width="400px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD width="20%">No. de Solicitud</TD>
								<TD width="30%"><asp:textbox id="txtNoSolicitud" runat="server" CssClass="textBox" Width="100px"></asp:textbox></TD>
								<TD width="18%">Factura</TD>
								<TD width="32%"><asp:textbox id="txtFactura" runat="server" CssClass="textBox" Width="130px"></asp:textbox></TD>
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
								<TD width="30%"><asp:textbox id="txtFechaInicio" runat="server" CssClass="textBox" Width="80px"></asp:textbox>&nbsp;<A href="javascript:MostrarCalendario(Form1.txtFechaInicio,Form1.txtFechaInicio,'dd/mm/yyyy');"
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
								<TD>Identificación Empleado</TD>
								<TD width="30%"><asp:textbox id="txtIdEmpleado" runat="server" CssClass="textBox" Width="130px"></asp:textbox></TD>
								<TD>Identificación Beneficiario</TD>
								<TD><asp:textbox id="txtIdBeneficiario" runat="server" CssClass="textBox" Width="130px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>Tipo de Servicio</TD>
								<TD width="30%"><asp:dropdownlist id="ddlTipoServicio" runat="server" CssClass="textBoxSmall" Width="170px"></asp:dropdownlist></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 22px"></TD>
								<TD style="HEIGHT: 22px" width="30%" colSpan="3"><asp:dropdownlist id="ddlMes" runat="server" CssClass="textBox" Width="100px" Visible="False">
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
									<asp:dropdownlist id="ddlAno" runat="server" CssClass="textBox" Width="70px" Visible="False"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD align="center" colSpan="4"><asp:button id="btnBuscar" runat="server" CssClass="button" CausesValidation="False" Text="Buscar"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 35px" align="left" colSpan="2" height="35">&nbsp; <A>
							<asp:imagebutton id="imbAdicionar" runat="server" CausesValidation="False" ImageUrl="../../iconos/ico_adicionar.gif"></asp:imagebutton></A>&nbsp;
						<asp:linkbutton id="lnkAdicionar" runat="server" CausesValidation="False">Adicionar Autorización</asp:linkbutton></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="2"><asp:updatePanel id="Ajaxpanel2" runat="server"><ContentTemplate>
							<asp:Label id="lblMensaje" runat="server" CssClass="titleBlue"></asp:Label>
							<BR>
							<BR>
							<DIV style="OVERFLOW: auto; WIDTH: 98%; HEIGHT: 500px">
								<asp:datagrid id="dtgSolicitudes" runat="server" Width="99%" CssClass="grid" CellPadding="2" GridLines="Horizontal"
									AutoGenerateColumns="False" PageSize="20">
									<AlternatingItemStyle CssClass="altItems"></AlternatingItemStyle>
									<ItemStyle CssClass="norItems"></ItemStyle>
									<HeaderStyle CssClass="headerGrid"></HeaderStyle>
									<Columns>
										<asp:BoundColumn Visible="False" DataField="IdSolicitud"></asp:BoundColumn>
										<asp:BoundColumn Visible="False" DataField="id_solicitud_SICAU"></asp:BoundColumn>
										<asp:BoundColumn Visible="False" DataField="id_empleado" HeaderText="Empleado"></asp:BoundColumn>
										<asp:BoundColumn Visible="False" DataField="IdSolicitudTipoServicio" HeaderText="IdSolicitudTipoServicio"></asp:BoundColumn>
										<asp:BoundColumn Visible="False" DataField="beneficiario_id" HeaderText="Beneficiario"></asp:BoundColumn>
										<asp:TemplateColumn HeaderText="No. Solic">
											<ItemStyle HorizontalAlign="Center" Width="12%"></ItemStyle>
											<ItemTemplate>
												<asp:LinkButton CausesValidation="false" CommandArgument="IdSolicitud" CommandName="Ver" id="lnkSolicitud"
													runat="server" CssClass="textSmallBlack">
													<%# DataBinder.Eval(Container, "DataItem.NoSolicitud") %>
												</asp:LinkButton>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn HeaderText="Paciente">
											<ItemStyle Width="30%"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblPaciente" runat="server"></asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:BoundColumn DataField="NombreSolicitudEstado" HeaderText="Estado">
											<ItemStyle HorizontalAlign="Center" Width="9%"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="NombrePlanSolicitud" HeaderText="Plan">
											<ItemStyle HorizontalAlign="Center" Width="9%"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy} ">
											<ItemStyle HorizontalAlign="Center" Width="9%"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="ValorTotalConvenioSolicitado" HeaderText="Total Convenio o Solicitado"
											DataFormatString="{0:$ #,##0;($#,##0)}">
											<ItemStyle HorizontalAlign="Right" Width="17%"></ItemStyle>
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
										<asp:TemplateColumn HeaderText="Glosa">
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
									</Columns>
									<PagerStyle Mode="NumericPages"></PagerStyle>
								</asp:datagrid></DIV>
						</ContentTemplate></asp:updatePanel></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="2"><BR>
						<asp:button id="btnReload" runat="server" CssClass="button" Visible="False" CausesValidation="False"
							Text="Actualizar"></asp:button></TD>
				</TR>
				<tr>
					<td><asp:updatePanel id="Ajaxpanel1" runat="server"><ContentTemplate>
							<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
								<TR>
									<TD align="center" colSpan="2" height="10"></TD>
								</TR>
								<TR>
									<TD id="tdFactura" style="DISPLAY: none" align="center" colSpan="2" runat="server">
										<FIELDSET class="FieldSet" style="WIDTH: 70%"><LEGEND><IMG src="../../images/icoFactura.gif" border="0">
												Datos de la Factura</LEGEND><BR>
											<TABLE id="Table2" cellSpacing="0" cellPadding="6" width="90%">
												<TR>
													<TD width="30%">No. de Factura</TD>
													<TD width="70%">
														<asp:textbox id="txtNumFactura" runat="server" Width="100px" CssClass="textBox" MaxLength="50"></asp:textbox>&nbsp;
														<asp:requiredfieldvalidator id="rfvNoFactura" runat="server" CssClass="textRed" Enabled="False" ForeColor=" "
															Display="Dynamic" ControlToValidate="txtNumFactura" ErrorMessage="Requerido"></asp:requiredfieldvalidator></TD>
												</TR>
												<TR>
													<TD>No. Cuenta Cobro</TD>
													<TD width="30%">
														<asp:textbox id="txtCuentaCobro" runat="server" Width="100px" CssClass="textBox" MaxLength="50"></asp:textbox>&nbsp;
													</TD>
												</TR>
												<TR>
													<TD>Fecha de Factura</TD>
													<TD width="30%">
														<asp:textbox id="txtFechaFactura" runat="server" Width="80px" CssClass="textBox"></asp:textbox>&nbsp;<A href="javascript:MostrarCalendario(Form1.txtFechaFactura,Form1.txtFechaFactura,'dd/mm/yyyy');"
															name="btnFechaFactura"><IMG src="../../images/icoCalendar.gif" border="0"></A>&nbsp;
														<asp:requiredfieldvalidator id="rfvFechaFactura" runat="server" CssClass="textRed" Enabled="False" ForeColor=" "
															Display="Dynamic" ControlToValidate="txtFechaFactura" ErrorMessage="Requerido"></asp:requiredfieldvalidator>&nbsp;</TD>
												</TR>
												<TR>
													<TD>Fecha de Radicación</TD>
													<TD width="30%">
														<asp:textbox id="txtFechaRadicacion" runat="server" Width="80px" CssClass="textBox"></asp:textbox>&nbsp;<A href="javascript:MostrarCalendario(Form1.txtFechaRadicacion,Form1.txtFechaRadicacion,'dd/mm/yyyy');"
															name="btnFechaRadicacion"><IMG src="../../images/icoCalendar.gif" border="0"></A>&nbsp;
														<asp:requiredfieldvalidator id="rfvFechaRadicacion" runat="server" CssClass="textRed" Enabled="False" ForeColor=" "
															Display="Dynamic" ControlToValidate="txtFechaRadicacion" ErrorMessage="Requerido"></asp:requiredfieldvalidator>&nbsp;</TD>
												</TR>
												<TR>
													<TD>Fecha de Confirmación</TD>
													<TD width="30%">
														<asp:textbox id="txtFechaConfirmacion" runat="server" Width="80px" CssClass="textBox"></asp:textbox>&nbsp;<A href="javascript:MostrarCalendario(Form1.txtFechaConfirmacion,Form1.txtFechaConfirmacion,'dd/mm/yyyy');"
															name="btnFechaFactura"><IMG src="../../images/icoCalendar.gif" border="0"></A>&nbsp;
														<asp:requiredfieldvalidator id="rfvFechaConfirmacion" runat="server" CssClass="textRed" Enabled="False" ForeColor=" "
															Display="Dynamic" ControlToValidate="txtFechaConfirmacion" ErrorMessage="Requerido"></asp:requiredfieldvalidator>&nbsp;</TD>
												</TR>
												<TR>
													<TD>Total Valor Factura</TD>
													<TD width="30%">
														<asp:textbox onkeypress="return currencyFormat(this,event,true,false)" id="txtValorFactura" runat="server"
															Width="100px" CssClass="textBox"></asp:textbox>&nbsp;
														<asp:requiredfieldvalidator id="rfvValorFactura" runat="server" CssClass="textRed" Enabled="False" ForeColor=" "
															Display="Dynamic" ControlToValidate="txtValorFactura" ErrorMessage="Requerido"></asp:requiredfieldvalidator>
														<asp:CompareValidator id="cmvTotalFactura" runat="server" CssClass="textRed" ForeColor=" " ControlToValidate="txtValorFactura"
															ErrorMessage="El valor de la factura debe ser igual al Valor Total Aprobado" ControlToCompare="txtValorTotalAprobado"></asp:CompareValidator></TD>
												</TR>
												<TR>
													<TD>Proveedor</TD>
													<TD width="30%">
														<asp:dropdownlist id="ddlProveedor" runat="server" Width="330px" CssClass="textBoxSmall"></asp:dropdownlist>&nbsp;
														<asp:comparevalidator id="cmvProveedor" runat="server" CssClass="textRed" Enabled="False" ForeColor=" "
															Display="Dynamic" ControlToValidate="ddlProveedor" ErrorMessage="Requerido" Operator="NotEqual" ValueToCompare="0"></asp:comparevalidator></TD>
												</TR>
												<TR>
													<TD>Valor Total Aprobado</TD>
													<TD width="30%">
														<asp:TextBox id="txtValorTotalAprobado" runat="server"></asp:TextBox></TD>
												</TR>
												<TR>
													<TD>Valor Total con Descuento</TD>
													<TD width="30%">
														<asp:TextBox id="txtValorTotalDescuento" runat="server"></asp:TextBox></TD>
												</TR>
											</TABLE>
										</FIELDSET>
									</TD>
								</TR>
								<TR>
									<TD align="center" colSpan="2" height="10"></TD>
								</TR>
								<TR>
									<TD align="center" colSpan="2">
										<asp:datagrid id="dtgConfirmados" runat="server" Width="99%" CssClass="grid" CellPadding="2" GridLines="Horizontal"
											AutoGenerateColumns="False" PageSize="1000">
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
													<ItemStyle HorizontalAlign="Center" Width="9%"></ItemStyle>
													<ItemTemplate>
														<asp:CheckBox id="chkProcesar" runat="server" Checked="True"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="No. Solic">
													<ItemStyle HorizontalAlign="Center" Width="13%"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton CausesValidation="false" CommandArgument="IdSolicitud" CssClass="textSmallBlack"
															CommandName="Ver" id="lnkSolicitudProcesar" runat="server">
															<%# DataBinder.Eval(Container, "DataItem.NoSolicitud") %>
														</asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Paciente">
													<ItemStyle Width="28%"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblPacienteConf" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="FechaPrestacion" HeaderText="Fecha Prestaci&#243;n" DataFormatString="{0:dd/MM/yyyy} ">
													<ItemStyle HorizontalAlign="Center" Width="7%"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="ValorTotalConvenioSolicitado" HeaderText="Total Solicitado" DataFormatString="{0:$ #,##0;($#,##0)}">
													<ItemStyle HorizontalAlign="Right" Width="17%"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="ValorAprobado" HeaderText="Total Aprobado" DataFormatString="{0:$ #,##0;($#,##0)}">
													<ItemStyle HorizontalAlign="Right" Width="15%"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Descuento" HeaderText="Descuento" DataFormatString="{0:N2}%">
													<ItemStyle HorizontalAlign="Right" Width="9%"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Total Descontado">
													<ItemStyle HorizontalAlign="Right" Width="20%"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblDescontado" runat="server" text='<%# !Convert.IsDBNull(DataBinder.Eval(Container, "DataItem.ValorAprobado")) && !Convert.IsDBNull(DataBinder.Eval(Container, "DataItem.Descuento")) && Convert.ToDecimal(DataBinder.Eval(Container, "DataItem.Descuento")) != 0 ?  string.Format("{0:$ #,##0;($#,##0)}",((Convert.ToDecimal(DataBinder.Eval(Container, "DataItem.ValorAprobado")) * (Convert.ToDecimal(DataBinder.Eval(Container, "DataItem.Descuento")))/100))) : "" %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Total con Descuento">
													<ItemStyle HorizontalAlign="Right" Width="20%"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblDescuento" runat="server" text='<%# !Convert.IsDBNull(DataBinder.Eval(Container, "DataItem.ValorAprobado")) && !Convert.IsDBNull(DataBinder.Eval(Container, "DataItem.Descuento")) && Convert.ToDecimal(DataBinder.Eval(Container, "DataItem.Descuento")) != 0 ?  string.Format("{0:$ #,##0;($#,##0)}",(Convert.ToDecimal(DataBinder.Eval(Container, "DataItem.ValorAprobado")) - (Convert.ToDecimal(DataBinder.Eval(Container, "DataItem.ValorAprobado")) * (Convert.ToDecimal(DataBinder.Eval(Container, "DataItem.Descuento")))/100))) : (!Convert.IsDBNull(DataBinder.Eval(Container, "DataItem.ValorAprobado")) ? string.Format("{0:$ #,##0;($#,##0)}",Convert.ToDecimal(DataBinder.Eval(Container, "DataItem.ValorAprobado"))) : "") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle Mode="NumericPages"></PagerStyle>
										</asp:datagrid></TD>
								</TR>
								<TR>
									<TD align="center" colSpan="2" height="15"></TD>
								</TR>
								<TR>
									<TD align="center" colSpan="2">
										<asp:button id="btnProcesar" runat="server" CssClass="button" Visible="False" Text="Procesar"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										<asp:button id="btnCancelar" runat="server" CssClass="button" Visible="False" 
                                            Text="Cancelar" CausesValidation="False"></asp:button></TD>
								</TR>
							</TABLE>
						</ContentTemplate></asp:updatePanel></td>
				</tr>
				</TD></TR></table>
		</form>
	</body>
</HTML>
