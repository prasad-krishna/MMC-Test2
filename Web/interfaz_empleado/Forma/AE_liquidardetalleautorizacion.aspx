<%@ Page language="c#" Codebehind="AE_liquidardetalleautorizacion.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_empleado.forma.AE_liquidardetalleautorizacion" %>
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
	<body>
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="10" width="100%" align="center">
				<TR>
					<TD align="center">
						<FIELDSET class="FieldSet" style="WIDTH: 450px"><LEGEND><IMG src="../../images/icoFactura.gif" border="0">
								Datos de la Factura</LEGEND><BR>
							<TABLE class="tableBorder" id="Table2" cellSpacing="0" cellPadding="6" width="80%">
								<TR>
									<TD width="20%">No. de Factura</TD>
									<TD width="30%"><asp:textbox MaxLength="50" id="txtNumFactura" runat="server" CssClass="textBox" Width="100px"></asp:textbox>&nbsp;
										<asp:requiredfieldvalidator id="rfvNoFactura" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
											ControlToValidate="txtNumFactura" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator></TD>
								</TR>
								<TR>
									<TD width="20%">No. Cuenta Cobro</TD>
									<TD width="30%"><asp:textbox MaxLength="50" id="txtCuentaCobro" runat="server" CssClass="textBox" Width="100px"></asp:textbox>&nbsp;
									</TD>
								</TR>
								<TR>
									<TD width="20%">Fecha de Factura</TD>
									<TD width="30%"><asp:textbox id="txtFechaFactura" runat="server" CssClass="textBox" Width="80px"></asp:textbox>&nbsp;<A href="javascript:MostrarCalendario(Form1.txtFechaFactura,Form1.txtFechaFactura,'dd/mm/yyyy');"
											name="btnFechaFactura"><IMG src="../../images/icoCalendar.gif" border="0"></A>&nbsp;
										<asp:requiredfieldvalidator id="rfvFechaFactura" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
											ControlToValidate="txtFechaFactura" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator>&nbsp;</TD>
								</TR>
								<TR>
									<TD width="20%">Fecha de Radicación</TD>
									<TD width="30%"><asp:textbox id="txtFechaRadicacion" runat="server" CssClass="textBox" Width="80px"></asp:textbox>&nbsp;<A href="javascript:MostrarCalendario(Form1.txtFechaRadicacion,Form1.txtFechaRadicacion,'dd/mm/yyyy');"
											name="btnFechaRadicacion"><IMG src="../../images/icoCalendar.gif" border="0"></A>&nbsp;
										<asp:requiredfieldvalidator id="rfvFechaRadicacion" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
											ControlToValidate="txtFechaRadicacion" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator>&nbsp;</TD>
								</TR>
								<TR>
									<TD width="20%">Fecha de Confirmación</TD>
									<TD width="30%"><asp:textbox id="txtFechaConfirmacion" runat="server" CssClass="textBox" Width="80px"></asp:textbox>&nbsp;<A href="javascript:MostrarCalendario(Form1.txtFechaConfirmacion,Form1.txtFechaConfirmacion,'dd/mm/yyyy');"
											name="btnFechaFactura"><IMG src="../../images/icoCalendar.gif" border="0"></A>&nbsp;
										<asp:requiredfieldvalidator id="rfvFechaConfirmacion" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
											ControlToValidate="txtFechaConfirmacion" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator>&nbsp;</TD>
								</TR>
								<TR>
									<TD width="20%">Total Valor Factura</TD>
									<TD width="30%">
										<asp:textbox onkeypress="return currencyFormatPopUp(this,event,true,false)" id="txtValorFactura"
											runat="server" CssClass="textBox" Width="100px"></asp:textbox>&nbsp;
										<asp:requiredfieldvalidator id="rfvValorFactura" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
											ControlToValidate="txtValorFactura" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator></TD>
								</TR>
								<TR>
									<TD width="20%">Proveedor</TD>
									<TD width="30%"><asp:dropdownlist id="ddlProveedor" runat="server" CssClass="textBox" Width="176px"></asp:dropdownlist>&nbsp;
										<asp:comparevalidator id="cmvProveedor" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
											ControlToValidate="ddlProveedor" ErrorMessage="Requerido" Operator="NotEqual" ValueToCompare="0" Enabled="False"></asp:comparevalidator></TD>
								</TR>
								<TR>
									<TD width="20%">Glosa</TD>
									<TD width="30%">
										<asp:CheckBox id="chkGlosa" runat="server"></asp:CheckBox></TD>
								</TR>
							</TABLE>
							<BR>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD align="center">
						<asp:button id="btnLiquidar" runat="server" CssClass="button" Text="Liquidar"></asp:button></TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
