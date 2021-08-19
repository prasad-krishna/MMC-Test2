<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="verifyToken.aspx.cs" Inherits="Web.verifyToken" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>HC-Historias Clínicas</title>

    <script language="javascript" type="text/javascript" src="scripts/Validaciones.js"></script>
    <script language="javascript" type="text/javascript">

        

    </script>

    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="css/admon.css" type="text/css" rel="stylesheet">


    <script src="scripts/jquery-1.4.1.js" type="text/javascript"></script>


    <style type="text/css">
        .auto-style1 {
            width: 115px;
        }

        .headhighlight {
            font-weight: bold;
        }
    </style>


</head>

<body class="fondoApp">
    <form id="Form1" method="post" runat="server">
        <div id="dvDisclaimer">
        </div>
        <table id="tblPrincipal" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td bgcolor="">
                    <p>
                        &nbsp;
                    </p>
                    <p>
                        &nbsp;
                    </p>
                    <p>
                        &nbsp;
                    </p>
                    <table id="table6" class="tableBorder" align="center" width="570" cellpadding="0"
                        bgcolor="#ffffff">
                        <tr>
                            <td>
                                <table id="table1" cellspacing="0" cellpadding="10" width="100%" align="center" border="0">
                                    <tr>
                                        <td align="right" background="" colspan="2" height="60">
                                            <table height="60" cellspacing="1" cellpadding="12" width="100%">
                                                <tr>
                                                    <td style="padding-bottom: 0px;">
                                                        <img alt="" src="images/NuevoBanner.jpg" />
                                                    </td>
                                                    <td class="titleBigTPA" align="right" style="padding-bottom: 0px;">HC
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" height="10" style="padding: 0px;">
                                                        <img alt="" src="images/separador.png" />
                                                    </td>
                                                </tr>
                                            </table>
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
                                        <td width="60%" align="left" runat="server">
                                            <asp:Label ID="lblPanelLogin" runat="server"></asp:Label>
                                            <table id="tblLogin" cellspacing="0" cellpadding="5" width="100%" align="center" border="0" runat="server">
                                                <tr>
                                                    <td colspan="2" height="23"></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <span class="textLightGray">
                                                            <asp:Label ID="lblOtptitle" runat="server" Text="" Font-Bold="True"></asp:Label>
                                                        <br>
                                                        </span>
                                                    </td>
                                                    
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="lblheading" runat="server"></asp:Label>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style1">
                                                        <asp:Label ID="Label1" runat="server" Text="Token de Seguridad*" Font-Bold="True"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtToken" runat="server" CssClass="textBox" Width="130px" MaxLength="6"
                                                            AutoCompleteType="Disabled"></asp:TextBox>                                                        
                                                    </td>
                                                </tr>
                                                <tr id="btnsubmitid" runat="server">
                                                    <td class="auto-style1"></td>
                                                    <td>
                                                        <asp:Button ID="btnSubmit" runat="server" CssClass="button" OnClick="btnSubmit_Click" Text="Enviar"
                                                            UseSubmitBehavior="false" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="center">
                                                        <asp:Label ID="lblmessage" runat="server" ForeColor="#FF3300">   </asp:Label>
                                                        <asp:RequiredFieldValidator runat="server" ID="reqName" ControlToValidate="txtToken" ErrorMessage="Por favor ingrese el token de seguridad." />
                                                    </td>

                                                </tr>
                                                <tr runat="server" style="display: none">
                                                    <td colspan="2"></td>

                                                </tr>

                                                <tr runat="server" style="display: none">
                                                    <td colspan="2"></td>
                                                </tr>

                                                <tr>
                                                    <td height="15" colspan="2"></td>
                                                </tr>
                                                <tr runat="server">
                                                    <td colspan="2"></td>
                                                </tr>
                                                <tr style="display: none">
                                                    <td colspan="2"></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">Nota:
                                                    </td>
                                                </tr>
                                                <tr runat="server">
                                                    <td colspan="2">1. No cierre esta ventana hasta que haya ingresado el token de seguridad.
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="15" colspan="2">2. El token seguridad caducará en 10 minutos. Si esto sucede, cierre todas las ventanas del navegador y comience de nuevo. 
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td colspan="2">3. Si no recibe el correo electrónico, revise su carpeta de correo no deseado.
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">4. Si no reconoce o es incorrecto el correo electrónico al que fue enviado el token por favor comuníquese con su ejecutivo de soporte.
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2"></td>
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
                                        <td class="textSmallBlack" align="right" width="70%" style="border-top: #cccccc 1px solid">©2015 Mercer LLC, Todos los derechos reservados.
                                        <br>
                                            <asp:HyperLink ID="hplTerminos" runat="server" Target="_blank" NavigateUrl="http://www.mercer.com/termsofuse.htm?siteLanguage=100">Términos de Uso</asp:HyperLink>&nbsp;|
                                        <asp:HyperLink ID="hplPrivacidad" runat="server" Target="_blank" NavigateUrl="http://www.mercer.com/privacy.htm?siteLanguage=100">Políticas de Privacidad</asp:HyperLink>&nbsp;|
                                        <asp:HyperLink ID="hplAvisoPrivacidad" runat="server" Target="_blank" NavigateUrl="http://www.mercer.com/referencecontent.htm?idContent=1420040">Aviso de Privacidad</asp:HyperLink>
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

    <script type="text/javascript">
       

    </script>

</body>
</html>

