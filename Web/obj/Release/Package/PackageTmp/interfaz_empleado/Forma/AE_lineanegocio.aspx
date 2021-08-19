<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AE_lineanegocio.aspx.cs" Inherits="TPA.interfaz_admon.forma.AE_lineanegocio" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>HC-Historias Clínicas</title>
    <link href="../../css/admon.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../../scripts/Base.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" method="post" runat="server">
    <table class="tableBorder" cellspacing="0" cellpadding="4" width="300" align="center">
        <tr>
            <td class="titleBackBlue" colspan="2">
                Línea de Negocio
            </td>
        </tr>
        <div id="divIdLineaNegocio" runat="server" visible="false">
            <tr>
                <td colspan="2">
                    Código
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:TextBox ID="txtIdLineaNegocio" runat="server" CssClass="textBox" Width="160px"
                        MaxLength="5"></asp:TextBox>
                </td>
            </tr>
        </div>
        <tr>
            <td colspan="2">
                Nombre Línea de Negocio<span class="textRed">*</span>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:TextBox ID="txtNombreLineaNegocio" runat="server" CssClass="textBox" Width="260px"
                    MaxLength="500"></asp:TextBox><br/>
                <asp:RequiredFieldValidator ID="Requiredfieldvalidator2" runat="server" ErrorMessage="Requerido"
                    Display="Dynamic" ControlToValidate="txtNombreLineaNegocio" CssClass="textRed"
                    ForeColor=" "></asp:RequiredFieldValidator>
            </td>
        </tr>
        <div id="divempresa_id" runat="server" visible="false">
            <tr>
                <td colspan="2">
                    ID Empresa
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:TextBox ID="txtempresa_id" runat="server" CssClass="textBox" Width="160px" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
        </div>
        <tr>
            <td height="20" colspan="2">
                Estado
            </td>
        </tr>
        <tr>
            <td height="20" colspan="2">
                <asp:RadioButtonList ID="rblactiva" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1" Selected="True">Activa</asp:ListItem>
                    <asp:ListItem Value="0">Inactiva</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <p align="center">
                    <asp:Button ID="Aceptar" runat="server" Text="Aceptar" CssClass="button" 
                        onclick="Aceptar_Click"></asp:Button>&nbsp;&nbsp;&nbsp;<asp:Button
                        ID="Cancelar" CausesValidation="false" runat="server" Text="Cancelar" 
                        CssClass="button" onclick="Cancelar_Click">
                    </asp:Button></p>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
