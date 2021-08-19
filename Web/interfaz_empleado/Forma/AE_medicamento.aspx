<%@ Page language="c#" Codebehind="AE_medicamento.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_admon.forma.AE_medicamento" ValidateRequest="true" %>
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
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table class="tableBorder" cellSpacing="0" cellPadding="4" width="500" 
                align="center">
				<TR>
					<TD class="titleBackBlue" colSpan="2">Medicamento</TD>
				</TR>
				<tr>
					<td>Código</td>
					<td>Nombre Comercial<SPAN class="textRed">*</SPAN></td>
				</tr>
				<TR>
					<TD><asp:textbox id="Fcodigo" runat="server" CssClass="textBox" Width="160px" MaxLength="255"></asp:textbox></TD>
					<TD><asp:textbox id="Fnombre" runat="server" CssClass="textBox" Width="160px" MaxLength="255"></asp:textbox><BR>
						<asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" ErrorMessage="Requerido" Display="Dynamic"
							ControlToValidate="Fnombre" CssClass="textRed" ForeColor=" "></asp:requiredfieldvalidator></TD>
				</TR>
				<TR>
					<TD><BR>
						Principio Activo<SPAN class="textRed">*</SPAN></TD>
					<TD><BR>
						Forma Farmaceutica</TD>
				</TR>
				<TR>
					<TD><asp:textbox id="Fprincipio" runat="server" CssClass="textBox" Width="200px" TextMode="MultiLine"
							MaxLength="255"></asp:textbox><BR>
						<asp:requiredfieldvalidator id="Requiredfieldvalidator3" runat="server" ErrorMessage="Requerido" Display="Dynamic"
							ControlToValidate="Fprincipio" CssClass="textRed" ForeColor=" "></asp:requiredfieldvalidator></TD>
					<TD><asp:textbox id="Fforma_farmaceutica" runat="server" CssClass="textBox" Width="160px" MaxLength="255"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><BR>
						Presentación<SPAN class="textRed">*</SPAN></TD>
					<TD><BR>
						Cantidad Presentación</TD>
				</TR>
				<TR>
					<TD><asp:textbox id="Fpresentacion" runat="server" CssClass="textBox" Width="200px" TextMode="MultiLine"
							MaxLength="255"></asp:textbox><BR>
						<asp:requiredfieldvalidator id="Requiredfieldvalidator5" runat="server" ErrorMessage="Requerido" Display="Dynamic"
							ControlToValidate="Fpresentacion" CssClass="textRed" ForeColor=" "></asp:requiredfieldvalidator></TD>
					<TD><asp:textbox id="Fcantidad_presentacion" runat="server" CssClass="textBox" Width="160px" MaxLength="255"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><BR>
						Concentración</TD>
					<TD><BR>
						Registro Sanitario</TD>
				</TR>
				<TR>
					<TD><asp:textbox id="Fconcentracion" runat="server" CssClass="textBox" Width="200px" TextMode="MultiLine"
							MaxLength="255"></asp:textbox></TD>
					<TD><asp:textbox id="Fregistro_sanitario" runat="server" CssClass="textBox" Width="160px" MaxLength="255"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><BR>
						Régimen</TD>
					<TD><BR>
						Laboratorio<SPAN class="textRed">*</SPAN></TD>
				</TR>
				<TR>
					<TD><asp:textbox id="Fregimen" runat="server" CssClass="textBox" Width="160px" MaxLength="255"></asp:textbox></TD>
					<TD><asp:dropdownlist id="Flaboratory" runat="server" CssClass="textBox"></asp:dropdownlist><BR>
						<asp:comparevalidator id="CompareValidator7" runat="server" ErrorMessage="Requerido" Display="Dynamic"
							ControlToValidate="Flaboratory" Operator="NotEqual" ValueToCompare="0" CssClass="textRed" ForeColor=" "></asp:comparevalidator></TD>
				</TR>
				<TR>
					<TD><BR>
						Precio Recomendado Público</TD>
					<TD><BR>
						Precio del Distribuidor</TD>
				</TR>
				<TR>
					<TD><asp:textbox onkeypress="return currencyFormatPopUp(this,event,true,false)" id="Fprecio" runat="server"
							CssClass="textBox" MaxLength="50"></asp:textbox></TD>
					<TD><asp:textbox onkeypress="return currencyFormatPopUp(this,event,true,false)" id="FprecioDistri"
							runat="server" CssClass="textBox" MaxLength="50"></asp:textbox></TD>
				</TR>
				<TR>
					<TD height="20">Estado</TD>
					<TD height="20"></TD>
				</TR>
				<TR>
					<TD height="20">
						<asp:radiobuttonlist id="Factivo" runat="server" RepeatDirection="Horizontal">
							<asp:ListItem Value="1" Selected="True">Activo</asp:ListItem>
							<asp:ListItem Value="0">Inactivo</asp:ListItem>
						</asp:radiobuttonlist></TD>
					<TD height="20"></TD>
				</TR>
				<TR>
					<TD>
						<asp:radiobuttonlist id="Freembolsable" runat="server" RepeatDirection="Horizontal" Visible="False">
							<asp:ListItem Value="1" Selected="True">Si</asp:ListItem>
							<asp:ListItem Value="0">No</asp:ListItem>
						</asp:radiobuttonlist></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD colSpan="2">
						<P align="center"><asp:button id="Aceptar" runat="server" Text="Aceptar" CssClass="button"></asp:button>&nbsp;&nbsp;&nbsp;<asp:button id="Cancelar" CausesValidation="false" runat="server" Text="Cancelar" CssClass="button"></asp:button></P>
					</TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
