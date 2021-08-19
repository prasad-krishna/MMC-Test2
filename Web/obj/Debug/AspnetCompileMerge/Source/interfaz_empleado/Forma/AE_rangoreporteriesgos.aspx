<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AE_rangoreporteriesgos.aspx.cs" Inherits="TPA.interfaz_admon.forma.AE_rangoreporteriesgos" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   <title>HC-Historias Clínicas</title>
    <LINK href="../../css/admon.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../../scripts/Base.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>    
			<table class="tableBorder" cellSpacing="0" cellPadding="4" width="500" 
                align="center">
				<tr>
					<td class="titleBackBlue" colSpan="2">Rango Riesgos</td>
				</tr>
				<tr>
					<td>Nombre<span class="textRed">*</span></td>
					<td>Reporte Riesgos<span class="textRed">*</span></td>
				</tr>
				<tr>
					<td><asp:textbox id="txtNombre" runat="server" CssClass="textBox" Width="200px" 
                            MaxLength="255"></asp:textbox>
                        <br />
						<asp:requiredfieldvalidator id="rfvNombre" runat="server" 
                            ErrorMessage="Requerido" Display="Dynamic"
							ControlToValidate="txtNombre" CssClass="textRed" ForeColor=" "></asp:requiredfieldvalidator></td>
					<td><asp:dropdownlist id="ddlReporte" runat="server" CssClass="textBox" 
                            Width="200px"></asp:dropdownlist><br>
						<asp:comparevalidator id="cmvReporte" runat="server" ErrorMessage="Requerido" Display="Dynamic"
							ControlToValidate="ddlReporte" Operator="NotEqual" ValueToCompare="0" CssClass="textRed" 
                            ForeColor=" "></asp:comparevalidator></td>
				</tr>
				<tr>
					<td><br>
						Límite Inferior<span class="textRed">*</span></td>
					<td><br>
						Límite Superior<span class="textRed">*</span></td>
				</tr>
				<tr>
					<td><asp:textbox onkeypress="return currencyFormat(this,event,true,true)" id="txtLimiteInferior"
							runat="server" CssClass="textBox" MaxLength="50" Width="80px"></asp:textbox>
                        <br />
						<asp:requiredfieldvalidator id="rfvLimiteInferior" runat="server" 
                            ErrorMessage="Requerido" Display="Dynamic"
							ControlToValidate="txtLimiteInferior" CssClass="textRed" ForeColor=" "></asp:requiredfieldvalidator></td>
					<td><asp:textbox onkeypress="return currencyFormat(this,event,true,true)" id="txtLimiteSuperior"
							runat="server" CssClass="textBox" MaxLength="50" Width="80px"></asp:textbox>
                        <br />
						<asp:requiredfieldvalidator id="rfvLimiteSuperior" runat="server" 
                            ErrorMessage="Requerido" Display="Dynamic"
							ControlToValidate="txtLimiteSuperior" CssClass="textRed" ForeColor=" "></asp:requiredfieldvalidator></td>
				</tr>
				<tr>
					<td><br>
						Puntuación<span class="textRed">*</span></td>
					<td><br>
						Orden</td>
				</tr>
				<tr>
					<td><asp:textbox onkeypress="return currencyFormat(this,event,true,false)" id="txtPuntuacion"
							runat="server" CssClass="textBox" MaxLength="50" Width="80px"></asp:textbox><br>
						</td>
					<td><asp:textbox onkeypress="return currencyFormat(this,event,true,false)" id="txtOrden"
							runat="server" CssClass="textBox" MaxLength="50" Width="50px"></asp:textbox>
                        <br />
						<asp:requiredfieldvalidator id="rfvOrden" runat="server" 
                            ErrorMessage="Requerido" Display="Dynamic"
							ControlToValidate="txtOrden" CssClass="textRed" ForeColor=" "></asp:requiredfieldvalidator></td>
				</tr>
				<tr>
					<td colSpan="2">
						<P align="center">
                            <asp:button id="btnAceptar" runat="server" Text="Aceptar" 
                                CssClass="button" onclick="btnAceptar_Click"></asp:button>&nbsp;&nbsp;&nbsp;<asp:button 
                                id="btnEliminar" CausesValidation="false" runat="server" Text="Eliminar" 
                                CssClass="button" onclick="btnEliminar_Click"></asp:button>&nbsp;&nbsp; 
                            <asp:button 
                                id="btnCancelar" CausesValidation="false" runat="server" Text="Cancelar" 
                                CssClass="button" onclick="btnCancelar_Click"></asp:button></P>
					</td>
				</tr>
			</table>
    
    </div>
    </form>
</body>
</html>
