<%@ Page Language="c#" CodeBehind="AE_Contrasena.aspx.cs" AutoEventWireup="false"
    Inherits="TPA.AE_Contrasena" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>HC-Historias Clínicas</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="css/admon.css" type="text/css" rel="stylesheet">

    <script language="javascript" type="text/javascript" src="scripts/Validaciones.js"></script>
 </head>
<body class="fondoApp">
    <form id="Form1" method="post" runat="server">
    <table id="tblPrincipal" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td bgcolor="">
                <p>
                    &nbsp;</p>
                <p>
                    &nbsp;</p>
                <p>
                    &nbsp;</p>
                <table id="table6" class="tableBorder" align="center" width="570" cellpadding="0"
                    bgcolor="#ffffff">
                    <tr>
                        <td>
                            <table id="table1" cellspacing="0" cellpadding="10" width="100%" align="center" border="0">
                                <tr>
                                    <td align="right" background="" colspan="2" height="60">
                                        <table height="60" cellspacing="1" cellpadding="12" width="100%">
                                            <tr>
                                                <td>
                                                    <img alt="" src="images/NuevoBanner.jpg" />
                                                </td>
                                                <td class="titleBigTPA" align="right">
                                                    HC
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle" align="center" width="40%">
                                        <asp:Image ID="imgLogoMercer" runat="server" ImageUrl="images/LogoMerceSmall.jpg"
                                            Visible="False" DESIGNTIMEDRAGDROP="89"></asp:Image>
                                    </td>
                                    <td align="center" width="60%">
                                        <asp:Image ID="imgLogoCentroseguros" runat="server" ImageUrl="images/LogoCentrosegurosSmall.jpg"
                                            Visible="False"></asp:Image>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle" align="center" width="40%">
                                        <table id="table2" cellspacing="0" cellpadding="0" width="60%" align="center" border="0">
                                            <tr>
                                                <td>
                                                    <img src="images/imgReembolso.jpg" width="250">
                                                </td>
                                            </tr>
                                        </table>
                                        <br>
                                    </td>
                                    <td width="60%" align="left">
                                        <table class="tableBorder" id="Table3" width="45%" align="center">
                                            <tr>
                                                <td>
                                                    <table id="Table5" cellspacing="0" cellpadding="3" width="100%" border="0">
                                                        <tr>
                                                            <td class="titleBackBlue" colspan="2">
                                                                Su contraseña ha caducado, modifíquela
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="textSmallBlack" width="50%" colspan="2" height="20">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 24px" width="50%">
                                                                Contraseña actual<span class="textRed">*</span>
                                                            </td>
                                                            <td style="height: 24px">
                                                                <asp:TextBox ID="txtActual" runat="server" MaxLength="15" CssClass="textBox" TextMode="Password" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvActual" runat="server" CssClass="textRed" Display="Dynamic"
                                                                    ControlToValidate="txtActual" ErrorMessage="Requerido" ForeColor=" "></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Nueva contraseña<span class="textRed">*</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtNueva" runat="server" MaxLength="10" CssClass="textBox" TextMode="Password" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvNueva" runat="server" CssClass="textRed" Display="Dynamic"
                                                                    ControlToValidate="txtNueva" ErrorMessage="Requerido" ForeColor=" "></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Confirmación&nbsp;contraseña nueva<span class="textRed">*</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtConfirmacion" runat="server" MaxLength="10" CssClass="textBox"
                                                                    TextMode="Password" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvConfirmacion" runat="server" CssClass="textRed"
                                                                    Display="Dynamic" ControlToValidate="txtConfirmacion" ErrorMessage="Requerido"
                                                                    ForeColor=" "></asp:RequiredFieldValidator>
                                                                <asp:CompareValidator ID="cmvConfirmacion" runat="server" CssClass="textRed" Display="Dynamic"
                                                                    ControlToValidate="txtConfirmacion" ErrorMessage="La confirmación es diferente a la nueva contraseña"
                                                                    ForeColor=" " ControlToCompare="txtNueva"></asp:CompareValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" height="20">
                                                                <asp:Label ID="lblMensaje" runat="server" CssClass="textRed"></asp:Label><br />
                                                                <asp:RegularExpressionValidator ID="revCaracteresEspeciales" runat="server" CssClass="textRed"
                                                                    ControlToValidate="txtNueva" Display="Dynamic" ForeColor=" " ErrorMessage="La contraseña debe estar entre 8 y 10 caracteres, contener al menos un número y una letra y no debe contener caracteres especiales."
                                                                    ValidationExpression="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,10})$" Enabled="false"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="textSmallBlack" colspan="2" height="20">
                                                                Los campos marcados con (<span class="textRed">*</span>) son obligatorios
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="20" colspan="2" class="textSmallBlack">
                                                                La nueva contraseña debe tener mínimo 8 y máximo 10 caracteres, por lo menos una
                                                                letra mayúscula y una minúscula, un número y al menos uno de los siguientes caracteres
                                                                especiales:.,:;()[]{}?!¿¡@-_/\#.
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="2">
                                                                <asp:Button ID="btnAceptar" runat="server" CssClass="button" Text="Aceptar"></asp:Button>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table id="table4" cellspacing="0" cellpadding="5" width="100%" align="center" border="0">
                                <tr>
                                    <td width="30%" style="border-top: #cccccc 1px solid">
                                        <img src="images/Footer.gif">
                                    </td>
                                    <td class="textSmallBlack" align="right" width="70%" style="border-top: #cccccc 1px solid">
                                        ©2015 Mercer LLC, Todos los derechos reservados.
                                        <br>
                                        <asp:LinkButton ID="lnkTerminos" runat="server" CssClass="textSmallBlue" CausesValidation="False">Términos de Uso</asp:LinkButton>&nbsp;|
                                        <asp:LinkButton ID="lnkPrivacidad" runat="server" CssClass="textSmallBlue" CausesValidation="False">Políticas de Privacidad</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
