<%@ Register TagPrefix="uc1" TagName="WC_BuscarPrestador" Src="../WebControls/WC_BuscarPrestador.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_BuscarProveedor" Src="../WebControls/WC_BuscarProveedor.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_BuscarDiagnosticoTipoServicio" Src="../WebControls/WC_BuscarDiagnosticoTipoServicio.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_AdicionarDiagnostico" Src="../WebControls/WC_AdicionarDiagnostico.ascx" %>
<%@ Page language="c#" Codebehind="AE_solicitudreembolso.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_empleado.forma.AE_solicitudreembolso" ValidateRequest="false" %>
<%@ Register TagPrefix="uc1" TagName="WC_BuscarPrestadorTipoServicio" Src="../WebControls/WC_BuscarPrestadorTipoServicio.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_AdicionarSolicitante" Src="../WebControls/WC_AdicionarSolicitante.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_AdicionarPrestador" Src="../WebControls/WC_AdicionarPrestador.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_RegistrarUVR" Src="../WebControls/WC_RegistrarUVR.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_DatosEmpleado" Src="../WebControls/WC_DatosEmpleado.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_BuscarDiagnostico" Src="../WebControls/WC_BuscarDiagnostico.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_BuscarServicioProducto" Src="../WebControls/WC_BuscarServicioProducto.ascx" %>

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
	<body leftMargin="5" topMargin="1" onload="CargarConfiguracion()" rightMargin="5">
		<form id="Form1" method="post" runat="server">
		<asp:ScriptManager ID="scrMng" runat="server"></asp:ScriptManager>
			<script type="text/javascript">		
			
			function BorrarItem(image)
			{
				var tr = image.parentNode.parentNode;				
				var inputs = tr.getElementsByTagName('input'); 
				for (var j = 0; j < inputs.length; j++)
				{
					if(inputs[j].getAttribute('type') == 'text' && inputs[j].getAttribute('id').indexOf('txtIdTipoServicio') == -1 && inputs[j].getAttribute('id').indexOf('txtIdSolicitudServicio') == -1) 
						inputs[j].value = "";	
				}				
				var inputs = tr.getElementsByTagName('textarea');				
				for (var j = 0; j < inputs.length; j++)
				{
					if(inputs[j].getAttribute('id').indexOf('txtIdTipoServicio') == -1 && inputs[j].getAttribute('id').indexOf('txtIdSolicitudServicio') == -1) 
						inputs[j].value = "";	
				}		
			}
			
			function CambiarPaciente(idEmpleado, idBeneficiario)
			{	
				
				if(idEmpleado != "" && idEmpleado != "0")
				{
					var txtIdEmpleado = document.getElementById('txtOtroEmpleado');
					txtIdEmpleado.value = idEmpleado;
				}
				if(idBeneficiario != "" && idBeneficiario != "0")
				{
					var txtIdBeneficiario = document.getElementById('txtOtroBeneficiario');
					txtIdBeneficiario.value = idBeneficiario;
				}					
				__doPostBack('lnkCargarNuevoPaciente', '');
			
			}			
		    
			</script>
			<table cellSpacing="0" cellPadding="5" width="100%" align="center" border="0">
				<TR>
					<TD align="center" colSpan="2">&nbsp;
						<uc1:wc_datosempleado id="WC_DatosEmpleado1" runat="server"></uc1:wc_datosempleado></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="2">&nbsp;
						<FIELDSET class="FieldSet" style="WIDTH: 99%"><legend><IMG src="../../images/icoSolicitud.gif" border="0">
								Solicitud</legend><br>
							<TABLE id="Table1" cellSpacing="0" cellPadding="1" width="100%" align="center">
								<TR>
									<TD>
										<TABLE id="tblDatosSolicitud" style="DISPLAY: none" cellSpacing="0" cellPadding="3" width="90%"
											align="center" runat="server">
											<TR>
												<TD class="textSmallBlack" width="20%" colSpan="4">Los campos marcados con (<SPAN class="textRed">*</SPAN>) 
													son obligatorios</TD>
											</TR>
											<TR>
												<TD width="20%"></TD>
												<TD width="30%">
													<asp:label id="lblNoSolicitud" runat="server" CssClass="titleBig" Visible="False"></asp:label><A href="javascript:MostrarCalendario(Form1.txtFecha,Form1.txtFecha,'dd/mm/yyyy');"
														name="btnFecha"></A></TD>
												<TD width="20%">Fecha y Usuario Creación</TD>
												<TD width="35%"><asp:label id="lblFechaCreacion" runat="server"></asp:label><BR>
													<asp:label id="lblUsuarioCreacion" runat="server"></asp:label></TD>
											</TR>
										</TABLE>
										<TABLE id="Table2" cellSpacing="0" cellPadding="3" width="90%" align="center">
											<TBODY>
												<TR>
													<TD width="20%">Paciente<SPAN class="textRed">*</SPAN></TD>
													<TD width="33%"><asp:dropdownlist id="ddlUsuario" runat="server" CssClass="textBoxSmall" Width="218px"></asp:dropdownlist><asp:comparevalidator id="cmvUsuario" runat="server" CssClass="textRed" Operator="NotEqual" ValueToCompare="-1"
															ForeColor=" " Display="Dynamic" ControlToValidate="ddlUsuario" ErrorMessage="Requerido"></asp:comparevalidator><BR>
														<asp:linkbutton id="lnkCambiarPaciente" runat="server" Visible="False">Seleccionar Otro Paciente</asp:linkbutton>
														<asp:linkbutton id="lnkCargarNuevoPaciente" runat="server"></asp:linkbutton>
														<asp:textbox id="txtOtroEmpleado" runat="server" Width="0px"></asp:textbox>
														<asp:textbox id="txtOtroBeneficiario" runat="server" Width="0px"></asp:textbox></TD>
													<TD width="17%">Fecha de Solicitud<SPAN class="textRed">*</SPAN></TD>
													<TD width="30%"><asp:textbox id="txtFecha" runat="server" CssClass="textBox" Width="80px"></asp:textbox>&nbsp;<A href="javascript:MostrarCalendario(Form1.txtFecha,Form1.txtFecha,'dd/mm/yyyy');"
															name="btnFecha"><IMG src="../../images/icoCalendar.gif" border="0"></A>
														<asp:requiredfieldvalidator id="rfvFecha" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="txtFecha" ErrorMessage="Requerido"></asp:requiredfieldvalidator></TD>
												</TR>
												<TR>
													<TD>Servicio Solicitado Por</TD>
													<TD><asp:updatePanel id="Ajaxpanel1" runat="server"><ContentTemplate>
															<asp:dropdownlist id="ddlPrestador" runat="server" Visible="False" CssClass="textBox" Width="200px"
																AutoPostBack="True"></asp:dropdownlist>
															<asp:textbox id="txtOtroPrestador" runat="server" Visible="False" CssClass="textBox" Width="200px"
																MaxLength="200"></asp:textbox>
															<asp:label id="lblEspecialidad" runat="server" CssClass="textSmallBlue"></asp:label>
														</ContentTemplate></asp:updatePanel><asp:updatePanel id="Ajaxpanel8" runat="server"><ContentTemplate>
<asp:textbox id="txtPrestador" runat="server" Visible="False" CssClass="textBox" Width="170px"></asp:textbox>&nbsp;<INPUT class="buttonSmall" id="btnBuscarPrestador" style="DISPLAY: none" onclick="javascript:ShowPrestador(this);"
																type="button" value="..." name="btnBuscarPrestador" runat="server"> 
<asp:textbox id="txtIdPrestador" runat="server" Width="0px"></asp:textbox></ContentTemplate></asp:updatePanel></TD>
													<TD>Mes Liquidación</TD>
													<TD><asp:dropdownlist id="ddlMes" runat="server" CssClass="textBox" Width="100px">
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
														</asp:dropdownlist><asp:dropdownlist id="ddlAno" runat="server" CssClass="textBox" Width="70px"></asp:dropdownlist><asp:updatePanel id="Ajaxpanel4" runat="server"></asp:updatePanel><asp:textbox id="txtDiagnostico" runat="server" Visible="False" CssClass="textBox" Width="170px"></asp:textbox>&nbsp;<asp:textbox id="txtIdDiagnostico" runat="server" Width="0px"></asp:textbox></TD>
										</ContentTemplate></asp:updatePanel></TD>
								</TR>
								<TR>
									<TD height="20">Forma de Pago<SPAN class="textRed">*</SPAN></TD>
									<TD><asp:dropdownlist id="ddlFormaPago" runat="server" CssClass="textBox" Width="200px"></asp:dropdownlist><asp:comparevalidator id="cmvFormaPago" runat="server" CssClass="textRed" Operator="NotEqual" ValueToCompare="0"
											ForeColor=" " Display="Dynamic" ControlToValidate="ddlFormaPago" ErrorMessage="Requerido"></asp:comparevalidator></TD>
									<TD><asp:label id="lblPlanesSolicitud" runat="server" Visible="False">Planes</asp:label></TD>
									<TD><asp:dropdownlist id="ddlPlanesSolicitud" runat="server" Visible="False" CssClass="textBox" Width="200px"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD height="20"><asp:label id="lblEstado" runat="server" Visible="False">Estado Solicitud</asp:label></TD>
									<TD><asp:dropdownlist id="ddlEstadoSoli" runat="server" Visible="False" CssClass="textBox" Width="200px"
											Enabled="False"></asp:dropdownlist></TD>
									<TD><asp:label id="lblMotivo" runat="server" Visible="False">Motivo Solicitud</asp:label></TD>
									<TD><asp:dropdownlist id="ddlMotivoSoli" runat="server" Visible="False" CssClass="textBox" Width="200px"></asp:dropdownlist></TD>
								</TR>
							</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>&nbsp;&nbsp;
						<asp:imagebutton id="imbAdicionarSolicitud" runat="server" ImageUrl="../../images/icoAdd.gif" CausesValidation="false"></asp:imagebutton><asp:linkbutton id="lnkAdicionarSolicitud" runat="server" CausesValidation="false">Adicionar Nueva Solicitud</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:DropDownList id="ddlAdicionarTipoServicio" runat="server" CssClass="textBoxSmall" Width="40px"
							AutoPostBack="True"></asp:DropDownList>
						<asp:imagebutton id="imbAdicionarTipoServicio" runat="server" ImageUrl="../../images/icoAdd.gif"
							CausesValidation="false" Visible="False"></asp:imagebutton><asp:linkbutton id="lnkAdicionarTipoServicio" runat="server" CausesValidation="false" Visible="False">Adicionar Prestador</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:imagebutton id="imbVerHistorial" runat="server" ImageUrl="../../images/icoHistorial.gif" CausesValidation="false"></asp:imagebutton><asp:linkbutton id="lnkVerHistorico" runat="server" CausesValidation="false">Ver Histórico del Paciente</asp:linkbutton>&nbsp;&nbsp;&nbsp;
						<asp:imagebutton id="imbGuardar" runat="server" ImageUrl="../../iconos/icoGuardar.gif" CausesValidation="false"></asp:imagebutton><asp:linkbutton id="lnkGuardar" runat="server" CausesValidation="false">Guardar</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					</TD>
				</TR>
				<TR>
					<TD align="center"><asp:updatePanel id="Ajaxpanel2" runat="server"><ContentTemplate>
							<asp:datalist id="dtlTipoServicio" runat="server" Width="100%" CellPadding="8">
								<ItemTemplate>
									<asp:Label id="lblNuevo" runat="server" CssClass="textBigGreen"></asp:Label>&nbsp;
									<asp:TextBox id="txtConsecutivoNombre" runat="server" CssClass="textBigGreen"></asp:TextBox>
									<asp:TextBox id="txtIdSolicitudEstado" runat="server" Width="0px"></asp:TextBox>
									<TABLE class="tableBorder" id="Table4" cellSpacing="0" cellPadding="3" width="100%" border="0">
										<TR class="headerGrid">
											<TD>Tipo de Servicio<SPAN class="textRed">*</SPAN></TD>
											<TD>No.Factura<SPAN class="textRed">*</SPAN></TD>
											<TD>No.Cuenta Cobro</TD>
											<TD>Fecha Factura<SPAN class="textRed">*</SPAN></TD>
											<TD>Prestadores<SPAN class="textRed">*</SPAN></TD>
											<TD>Valor Factura<SPAN class="textRed">*</SPAN></TD>
											<TD></TD>
										</TR>
										<TR vAlign="top">
											<TD>
												<asp:dropdownlist id="ddlTipoServicio" runat="server" CssClass="textBox" Width="150px" AutoPostBack="true"
													OnSelectedIndexChanged="ddlTipoServicio_SelectedIndexChanged"></asp:dropdownlist>
												</br>
												<asp:ImageButton id="imbHistoricoTipoServicio" runat="server" ImageUrl="../../iconos/icoHistorial.gif"
													CausesValidation="false" AlternateText="Histórico" CommandName="VerHistorico"></asp:ImageButton>
												<asp:TextBox id="txtIdSolicitudTipoServicio" runat="server" CssClass="textBox" Width="0px"></asp:TextBox></TD>
											<asp:TextBox id="txtConsecutivo" runat="server" CssClass="textBox" Width="0px"></asp:TextBox>
											<asp:TextBox id="txtImpresiones" runat="server" CssClass="textBox" Width="0px"></asp:TextBox>
											<TD width="62">
												<asp:TextBox id="txtNumFactura" runat="server" CssClass="textBox" Width="60px" MaxLength="50"></asp:TextBox></TD>
											<TD width="64">
												<asp:Label id="lblCuentaCobro" runat="server" CssClass="textSmallBlack"></asp:Label>
												<asp:TextBox id="txtCuentaCobro" runat="server" CssClass="textBox" Width="60px" MaxLength="50"></asp:TextBox></TD>
											<TD width="98">
												<asp:textbox id="txtFechaFactura" runat="server" CssClass="textBox" Width="76px"></asp:textbox>
												<asp:Image id="btnFechaFactura" runat="server" ImageUrl="../../images/icoCalendar.gif"></asp:Image></TD>
											<TD>
												<uc1:WC_AdicionarPrestador id="WC_AdicionarPrestador1" runat="server"></uc1:WC_AdicionarPrestador></TD>
											<TD width="55">
												<asp:TextBox onkeypress="return currencyFormat(this,event,true,false)" id="txtValorFactura" runat="server"
													CssClass="textBox" Width="63px"></asp:TextBox></TD>
											<TD>
												<asp:ImageButton id="imbBorrar" runat="server" Visible="false" CssClass="ImageTransparent" ImageUrl="../../iconos/ico_borrar.gif"
													CommandName="Eliminar"></asp:ImageButton></TD>
										</TR>
										<TR>
											<TD align="left" colSpan="7">
												<TABLE id="Table4" cellSpacing="0" cellPadding="3" width="100%" border="0">
													<TR>
														<TD vAlign="top" align="left">Atención</TD>
														<TD vAlign="top">
															<asp:DropDownList id="ddlTipoAtencion" runat="server" CssClass="textBoxSmall" Width="110px"></asp:DropDownList></TD>
														<TD vAlign="top">
															<asp:DropDownList id="ddlClaseAtencion" runat="server" CssClass="textBoxSmall" Width="110px"></asp:DropDownList></TD>
														<TD vAlign="top">
															<asp:DropDownList id="ddlContingencia" runat="server" CssClass="textBoxSmall" Width="110px"></asp:DropDownList></TD>
														<TD vAlign="top">Aprobada por:<BR>
															<asp:DropDownList id="ddlUnidadAprueba" runat="server" CssClass="textBoxSmall" Width="105px">
																<asp:ListItem Value="">--</asp:ListItem>
																<asp:ListItem Value="Coordinación de usuarios" Selected="True">Coordinación de usuarios</asp:ListItem>
																<asp:ListItem Value="División Médica">División Médica</asp:ListItem>
															</asp:DropDownList></TD>
														<TD vAlign="bottom" width="250"><SPAN class="textNegrita">Diagnósticos</SPAN><BR>
															<uc1:WC_AdicionarDiagnostico id="WC_AdicionarDiagnostico1" runat="server"></uc1:WC_AdicionarDiagnostico></TD>
													</TR>
													<TR>
														<TD vAlign="top" align="left" colspan="6">
															<uc1:WC_AdicionarSolicitante id="WC_AdicionarSolicitante1" runat="server"></uc1:WC_AdicionarSolicitante>
														</TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
										<TR>
											<TD colSpan="4">
												<asp:ImageButton id="imbAdicionarServicio" runat="server" ImageUrl="../../images/icoAdd.gif" CausesValidation="false"
													CommandName="Adicionar"></asp:ImageButton>
												<asp:LinkButton id="lnkAdicionarServicio" runat="server" CausesValidation="false" CommandName="Adicionar">Adicionar Servicio/Producto</asp:LinkButton>
												<asp:DropDownList id="ddlAdicionarServicios" runat="server" CssClass="textBoxSmall" AutoPostBack="true"
													OnSelectedIndexChanged="ddlAdicionarServicios_SelectedIndexChanged"></asp:DropDownList></TD>
										</TR>
									</TABLE>
									<TABLE class="tableBorderBack" id="tblInternet" runat="server" cellSpacing="0" cellPadding="3"
										width="100%" border="0" style="display:none">
										<TR>
											<TD width="20%">Diagnosticos
											</TD>
											<TD width="80%">
												<asp:Label id="lblDiagnosticos" Runat="server" CssClass="LabelNoModify" Width="450px"></asp:Label></TD>
										</TR>
										<TR>
											<TD width="20%">Servicios Solicitados
											</TD>
											<TD width="70%">
												<asp:Label id="lblServicios" Runat="server" CssClass="LabelNoModify" Width="450px"></asp:Label></TD>
										</TR>
									</TABLE>
									<asp:DataGrid id="dtgProductoServicio" runat="server" CssClass="grid" Width="100%" CellPadding="0"
										OnItemDataBound="dtgProductoServicio_ItemDataBound" OnItemCommand="dtgProductoServicio_ItemCommand"
										GridLines="Horizontal" AutoGenerateColumns="False" AllowPaging="False">
										<AlternatingItemStyle CssClass="altItemsSmall"></AlternatingItemStyle>
										<ItemStyle CssClass="norItemsSmall"></ItemStyle>
										<HeaderStyle CssClass="headerGrid"></HeaderStyle>
										<Columns>
											<asp:TemplateColumn HeaderText="Servicio/Producto">
												<ItemTemplate>
													<asp:TextBox Width="118px" runat="server" ID="txtProductoServicio" CssClass="textBoxSmall"></asp:TextBox>
													<asp:Button CausesValidation="false" id="btnBuscarProductoServicio" ToolTip="Seleccione para buscar el producto o servicio"
														runat="server" Width="20px" CssClass="buttonSmall" Text="..."></asp:Button>
													<asp:TextBox runat="server" ID="txtIdServicioProducto" CssClass="textBox" Width="0px"></asp:TextBox>
													<asp:TextBox runat="server" ID="txtIdTipoServicio" CssClass="textBox" Width="0px"></asp:TextBox>
													<asp:TextBox runat="server" ID="txtIdSolicitudServicio" CssClass="textBox" Width="0px"></asp:TextBox>
												</ItemTemplate>
												<ItemStyle Width="145px"></ItemStyle>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Cantidad">
												<ItemTemplate>
													<asp:TextBox MaxLength="50" Width="45px" runat="server" ID="txtCantidad" CssClass="textBoxSmall"></asp:TextBox>
													<br />
													<asp:Label id="lblDosis" CssClass="textSmallBlack" runat="server" Visible="false">Dosis</asp:Label>
													<asp:TextBox MaxLength="100" id="txtDosis" Visible="false" runat="server" Width="50px" CssClass="textBoxSmall"></asp:TextBox>
												</ItemTemplate>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Fecha Prestación">
												<ItemTemplate>
													<asp:textbox id="txtFechaPrestacion" runat="server" Width="60px" CssClass="textBoxSmall"></asp:textbox>
													<asp:Image id="btnFechaPrestacion" runat="server" ImageUrl="../../images/icoCalendar.gif"></asp:Image>
												</ItemTemplate>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Valor Solici.">
												<ItemTemplate>
													<asp:TextBox Width="55px" onkeypress="return currencyFormat(this,event,true,false)" runat="server"
														ID="txtValorConvenioSolicitado" CssClass="textBoxSmall"></asp:TextBox><br />
													<asp:Button CausesValidation="false" id="lnkUVRSolicitado" ToolTip="Seleccione para calcular el UVR"
														runat="server" Width="28px" CssClass="buttonSmall" Text="UVR"></asp:Button>
													<asp:TextBox Width="35px" runat="server" ID="txtUVRSolicitado" CssClass="textBoxSmall"></asp:TextBox>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Estado">
												<ItemTemplate>
													<asp:DropDownList OnSelectedIndexChanged="ddlEstadoServicio_SelectedIndexChanged" AutoPostBack="true"
														id="ddlEstadoServicio" runat="server" Width="80px" CssClass="textBoxSmall"></asp:DropDownList>
													<br />
													<asp:Label id="lblValorAprobado" CssClass="textSmallBlack" runat="server" Visible="false">Aprobado $</asp:Label>
													<asp:TextBox onkeypress="return currencyFormat(this,event,true,false)" Width="58px" runat="server"
														ID="txtValorAprobado" CssClass="textBoxSmall" Visible="false"></asp:TextBox>
													<br />
													<asp:Button CausesValidation="false" Visible="false" id="lnkRegistrarUVR" ToolTip="Seleccione para calcular el UVR"
														runat="server" Width="28px" CssClass="buttonSmall" Text="UVR"></asp:Button>
													<asp:TextBox Width="35px" runat="server" ID="txtUVR" CssClass="textBoxSmall" Visible="false"></asp:TextBox>
													<br />
													<asp:Label id="lblDescuento" CssClass="textSmallBlack" runat="server" Visible="false">Descuento %</asp:Label>
													<asp:TextBox onkeypress="return currencyFormat(this,event,true,true)" Width="40px" runat="server"
														ID="txtDescuento" CssClass="textBoxSmall" Visible="false"></asp:TextBox>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Motivo">
												<ItemTemplate>
													<asp:DropDownList id="ddlMotivo" runat="server" Width="95px" CssClass="textBoxSmall"></asp:DropDownList>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Comentario">
												<ItemTemplate>
													<asp:TextBox MaxLength="500" id="txtComentarioServicioProducto" runat="server" Width="110px"
														Rows="3" TextMode="MultiLine" CssClass="textBoxSmall"></asp:TextBox>
												</ItemTemplate>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
											</asp:TemplateColumn>
											<asp:TemplateColumn Visible="false" HeaderText="">
												<ItemTemplate>
													<asp:ImageButton CommandName="Eliminar" CssClass="ImageTransparent" id="imbEliminarServicio" runat="server"
														ImageUrl="../../iconos/ico_borrar.gif"></asp:ImageButton>
												</ItemTemplate>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="">
												<ItemTemplate>
													<img class="ImageTransparentLink" src="../../iconos/ico_limpiar.gif" onclick="BorrarItem(this);"
														border="0">
												</ItemTemplate>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
											</asp:TemplateColumn>
										</Columns>
									</asp:DataGrid>
									<TABLE class="tableBorder" id="Table4" cellSpacing="0" cellPadding="3" width="100%" border="0">
										<TR>
											<TD width="30%">Observaciones<BR>
												<SPAN class="textSmallBlack">(Estas observaciones se despliegan en el formato de 
													este tipo de servicio)</SPAN>
											</TD>
											<TD width="70%">
												<asp:TextBox id="txtComentariosTipoServicio" runat="server" CssClass="textBoxSmall" Width="300px"
													MaxLength="500" TextMode="MultiLine" Rows="2"></asp:TextBox></TD>
										</TR>
									</TABLE>
								</ItemTemplate>
							</asp:datalist>
						</ContentTemplate></asp:updatePanel></TD>
				</TR>
				<TR>
					<TD align="right">
						<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="300" border="0">
							<TR>
								<TD width="275">
									<TABLE class="tableBorder" id="Table5" cellSpacing="0" cellPadding="5" width="275">
										<TR class="tableBorder">
											<TD>Total Solicitado</TD>
											<TD><asp:label id="lblTotalProductosServicios" runat="server"></asp:label></TD>
										</TR>
										<TR>
											<TD>Total Aprobado</TD>
											<TD><asp:label id="lblTotalAprobado" runat="server"></asp:label></TD>
										</TR>
										<TR>
											<TD>Total Facturas</TD>
											<TD><asp:label id="lblTotalFacturas" runat="server"></asp:label></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<TABLE id="Table3" cellSpacing="0" cellPadding="5" width="85%" align="center">
							<TR>
								<TD width="20%">Observaciones<BR>
									<span class="textSmallBlack">(Estas observaciones se despliegan en todos los 
										formatos)</span></TD>
								<TD><asp:textbox id="txtComentarioSolicitud" runat="server" CssClass="textBox" Width="368px" MaxLength="500"
										Height="70px" TextMode="MultiLine"></asp:textbox></TD>
							</TR>
							<TR>
								<TD width="20%">Documentos<BR>
									<SPAN class="textSmallBlack">(Este listado se despliegan en la carta)</SPAN></TD>
								<TD><asp:textbox id="txtDocumentos" runat="server" CssClass="textBox" Width="368px" MaxLength="500"
										Height="40px" TextMode="MultiLine"></asp:textbox></TD>
							</TR>
							<TR>
								<TD width="20%">Anotaciones Fijas<BR>
									<SPAN class="textSmallBlack">(Estas anotaciones(Estas anotaciones se despliegan en la solicitud para 
										impresión)</SPAN></TD>
								<TD><asp:checkboxlist id="chkAnotacionesFijas" runat="server" Width="99%" CellPadding="0" CellSpacing="0"
										RepeatColumns="1"></asp:checkboxlist></TD>
							</TR>
							<TR>
								<TD width="20%"><asp:label id="lblAnulacion" runat="server" Visible="False">Observaciones Anulacion<BR>
									<SPAN class="textSmallBlack">(Ingrese si va a anular la orden original)</SPAN> </asp:label></TD>
								<TD>
                                    <asp:DropDownList ID="ddlMotivosAnulacion" runat="server" CssClass="textBox" 
                                        Visible="False" Width="350px">
                                    </asp:DropDownList>
                                    <br />
                                    <asp:textbox id="txtAnulacion" runat="server" Visible="False" CssClass="textBox" Width="368px"
										MaxLength="500" Height="40px" TextMode="MultiLine"></asp:textbox></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</table>
			</FIELDSET> </TD></TR>
			<TR>
				<TD align="center" colSpan="2"><asp:button id="btnGuardar" runat="server" CssClass="button" Text="Aceptar"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					<asp:button id="btnCancelar" runat="server" CssClass="button" CausesValidation="false" Text="Cancelar"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					<asp:button id="btnAnular" runat="server" Visible="False" CssClass="buttonBig" Text="Anular/Copiar"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;</TD>
			</TR>
			</TBODY></TABLE><uc1:wc_buscardiagnostico id="WC_BuscarDiagnostico1" runat="server"></uc1:wc_buscardiagnostico><uc1:wc_buscarservicioproducto id="WC_BuscarServicioProducto1" runat="server"></uc1:wc_buscarservicioproducto><uc1:wc_buscarprestador id="WC_BuscarPrestador1" runat="server"></uc1:wc_buscarprestador><uc1:wc_buscarprestadortiposervicio id="WC_BuscarPrestadorTipoServicio1" runat="server"></uc1:wc_buscarprestadortiposervicio><uc1:wc_buscarproveedor id="WC_BuscarProveedor1" runat="server"></uc1:wc_buscarproveedor><uc1:wc_registraruvr id="WC_RegistrarUVR1" runat="server"></uc1:wc_registraruvr><uc1:wc_buscardiagnosticotiposervicio id="WC_BuscarDiagnosticoTipoServicio1" runat="server"></uc1:wc_buscardiagnosticotiposervicio></form>
		<P align="center">&nbsp;</P>
	</body>
</HTML>
