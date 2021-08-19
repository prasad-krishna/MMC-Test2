<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AE_preguntarespuestaconsulta.aspx.cs" Inherits="TPA.interfaz_admon.forma.AE_preguntarespuestaconsulta" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
   <title>HC-Historias Clínicas</title>
    <LINK href="../../css/admon.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../../scripts/Base.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>    
			<table class="tableBorder" cellSpacing="0" cellPadding="4" width="40%" 
                align="center">
				<tr>
					<td class="titleBackBlue" colSpan="2">Pregunta Respuesta</td>
				</tr>
				<tr>
					<td colspan="2">Pregunta:                         <asp:Label ID="lblPregunta" runat="server"></asp:Label>
                    </td>
				</tr>
				<tr>
					<td colspan="2"><br>
						Respuesta:
                        <asp:Label ID="lblRespuesta" runat="server"></asp:Label>
                        <br>
						</td>
				</tr>
				<tr>
					<td><br>
						Activa<span class="textRed">*</span> </td>
					<td><br>
						Puntuación<span class="textRed">*</span></td>
				</tr>
				<tr>
					<td>
                        <asp:RadioButtonList ID="rblActiva" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">Si</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:RadioButtonList>
                        <br>
						</td>
					<td><asp:textbox onkeypress="return currencyFormat(this,event,true,false)" id="txtPuntuacion"
							runat="server" CssClass="textBox" MaxLength="50" Width="80px"></asp:textbox></td>
				</tr>
				<tr>
					<td colSpan="2">
						<P align="center">
                            <asp:button id="btnAceptar" runat="server" Text="Aceptar" 
                                CssClass="button" onclick="btnAceptar_Click"></asp:button>&nbsp;&nbsp;&nbsp; 
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
