<%@ Page Language="c#" AutoEventWireup="false" CodeBehind="AE_password.aspx.cs"  Inherits="TPA.interfaz_empleado.forma.AE_password" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title></title>

    <script type="text/javascript" language="javascript" src="../../scripts/Validaciones.js"></script>

    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="../../css/admon.css" type="text/css" rel="stylesheet">
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="tblPrincipal" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table id="table6" class="tableBorder" align="center" width="45%">
                    <tr>
                        <td>
                            <table id="Table1" cellspacing="0" cellpadding="3" width="100%" border="0">
                                <tr>
                                    <td class="titleBackBlue" colspan="2">
                                        CAMBIAR CONTRASEÑA
                                    </td>
                                </tr>
                                <tr>
                                    <td width="50%" height="20" class="textSmallBlack" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td width="50%" style="height: 24px">
                                        Contraseña actual<span class="textRed">*</span>
                                    </td>
                                    <td style="height: 24px">
                                        <asp:TextBox ID="txtActual" runat="server" TextMode="Password" MaxLength="32" CssClass="textBox" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvActual" runat="server" CssClass="textRed" ErrorMessage="Requerido"
                                            ControlToValidate="txtActual" Display="Dynamic" ForeColor=" "></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Nueva contraseña<span class="textRed">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNueva" runat="server" TextMode="Password" MaxLength="32" CssClass="textBox" AutoCompleteType="Disabled" autocomplete="off"> </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvNueva" runat="server" CssClass="textRed" ErrorMessage="Requerido"
                                            ControlToValidate="txtNueva" Display="Dynamic" ForeColor=" "></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Confirmación&nbsp;contraseña nueva<span class="textRed">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtConfirmacion" runat="server" TextMode="Password" MaxLength="32"
                                            CssClass="textBox" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvConfirmacion" runat="server" CssClass="textRed"
                                            ErrorMessage="Requerido" ControlToValidate="txtConfirmacion" Display="Dynamic"
                                            ForeColor=" "></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="cmvConfirmacion" runat="server" CssClass="textRed" ErrorMessage="La confirmación es diferente a la nueva contraseña"
                                            ControlToValidate="txtConfirmacion" Display="Dynamic" ControlToCompare="txtNueva"
                                            ForeColor=" "></asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" height="20">
                                        <asp:Label ID="lblMensaje" runat="server" CssClass="textRed"></asp:Label>
                                        <asp:RegularExpressionValidator ID="revContrasena" runat="server" CssClass="textRed"
                                            ErrorMessage="La contraseña debe estar entre 15 y 32 caracteres, contener al menos un número y una letra y no debe contener caracteres especiales."
                                            ControlToValidate="txtNueva" Display="Dynamic" ForeColor=" " ValidationExpression="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,10})$"
                                            Enabled="false"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" colspan="2" class="textSmallBlack">
                                        Los campos marcados con (<span class="textRed">*</span>) son obligatorios
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" colspan="2" class="textSmallBlack">
                                        La nueva contraseña debe tener mínimo 15 y máximo 32 caracteres, por lo menos una
                                        letra mayúscula y una minúscula, un número y al menos uno de los siguientes caracteres
                                        especiales:.,:;()[]{}?!¿¡@-_/#.
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="button"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    &nbsp;
    </form>
</body>
</html>
