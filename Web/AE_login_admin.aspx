<%@ Page Language="c#" CodeBehind="AE_login_admin.aspx.cs" AutoEventWireup="false"
    Inherits="TPA.AE_login_admin" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>HC-Historias Clínicas</title>

    <script language="javascript" type="text/javascript" src="scripts/Validaciones.js"></script>
    <script src="scripts/hashtable.js" type="text/javascript"></script>
    <script src="scripts/rsa.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        if (top != self) {
            alert("Su sesión caducó, por favor registrese nuevamente");
            top.self.location = "AE_login_admin.aspx";
        }

    </script>

    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="css/admon.css" type="text/css" rel="stylesheet">
    <%--
    //Inicio
    //Autor: Diego Montejano Avila
    //Fecha: 2014/09/30
    //Proyecto: Auditoría
    //Observaciones: Agregar jquery
    //
    --%>

    <script src="scripts/jquery-1.4.1.js" type="text/javascript"></script>

    <%--
    //Fin
    //
    --%>
</head>
<%    	
    /*try
			{
				if (Request.QueryString["ssl"] == null || Request.QueryString["ssl"] == string.Empty)
                		{
					Response.Redirect("https://www.mercersicam.com.mx/hc/AE_login_admin.aspx?ssl=1");

				}
				
				
			}
			catch{}*/
		
           
%>
<body class="fondoApp">
    <form id="Form1" method="post" runat="server">
        <div id="dvDisclaimer">
        </div>
        <input id="deviceprintid" type="hidden" runat="server" />
        <input id="useragentid" type="hidden" runat="server" />
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
                                                        <span class="textLightGray">Bienvenido al Sistema HC
                                                        <br>
                                                        </span>Para ingresar,&nbsp;registre los siguientes datos
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Usuario
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtUser" runat="server" CssClass="textBox" Width="130px" MaxLength="25"
                                                            AutoCompleteType="Disabled"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvUser" runat="server" CssClass="textRed" ForeColor=" "
                                                            ErrorMessage="Requerido" ControlToValidate="txtUser" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr id="trContrasena" runat="server">
                                                    <td>Contraseña
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtPassword" runat="server" CssClass="textBox" Width="130px" Wrap="False"
                                                            TextMode="Password" MaxLength="32" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvPassword" CssClass="textRed" runat="server" ErrorMessage="Requerido"
                                                            ControlToValidate="txtPassword" Display="Dynamic" ForeColor=" "></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr id="trContrasenaAnterior" runat="server" style="display: none">
                                                    <td>Contraseña Anterior
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtContrasenaAnterior" runat="server" CssClass="textBox" Width="130px"
                                                            Wrap="False" TextMode="Password" MaxLength="32" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr id="trNuevaContrasena" runat="server" style="display: none">
                                                    <td>Nueva Contraseña
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtNuevaContrasena" runat="server" CssClass="textBox" Width="130px"
                                                            Wrap="False" TextMode="Password" MaxLength="32" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr id="trConfirmaNuevaContrasena" runat="server" style="display: none">
                                                    <td>Confirmaci&oacute;n de Contraseña
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtConfirmaNuevaContrasena" runat="server" CssClass="textBox" Width="130px"
                                                            Wrap="False" TextMode="Password" MaxLength="32" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <%--                                            <tr>
                                                <td height="15" colspan="2">
                                                    <asp:RegularExpressionValidator ID="revContrasena" runat="server" ControlToValidate="txtPassword"
                                                        CssClass="textRed" Display="Dynamic" ErrorMessage="La contraseña debe estar entre 8 y 10 caracteres, contener al menos un número y una letra y no debe contener caracteres especiales."
                                                        ForeColor=" " ValidationExpression="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,10})$"
                                                        Enabled="False"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>--%>
                                                <tr>
                                                    <td height="15" colspan="2"></td>
                                                </tr>
                                                <tr id="trRecuperaContrasena" runat="server">
                                                    <td colspan="2">
                                                        <asp:LinkButton ID="btnRecuperarContrasena" runat="server" CausesValidation="false"
                                                            OnClick="btnRecuperarContrasena_Click">Recuperar Contraseña</asp:LinkButton>
                                                    </td>
                                                </tr>
                                                <tr style="display: none">
                                                    <td colspan="2">
                                                        <asp:LinkButton ID="btnCambiarContrasena" runat="server" CausesValidation="false"
                                                            OnClick="btnCambiarContrasena_Click">Cambiar Contraseña</asp:LinkButton>
                                                    </td>
                                                </tr>
                                                <tr id="trAceptar" runat="server" style="display: none">
                                                    <td style="height: 13px" align="center" colspan="2">
                                                        <asp:Button ID="btnAceptar" runat="server" CssClass="button" Text="Aceptar" CausesValidation="false"
                                                            OnClick="btnAceptar_Click"></asp:Button>
                                                        <asp:LinkButton ID="lblCancelar" runat="server" OnClick="lblCancelar_Click" CausesValidation="false">Cancelar</asp:LinkButton>
                                                    </td>
                                                </tr>
                                                <tr id="trIngresar" runat="server">
                                                    <td style="height: 13px" align="center" colspan="2">
                                                        <asp:Button ID="btnIngresar" runat="server" CssClass="button" Text="Ingresar"></asp:Button>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="15"></td>
                                                    <td height="15"></td>
                                                </tr>
                                                <tr>
                                                    <td align="right" colspan="2">
                                                        <asp:ImageButton ID="imbOlvido" runat="server" ImageUrl="Images/icoQuestion.gif"
                                                            BorderStyle="None" Visible="False"></asp:ImageButton><asp:LinkButton ID="lnkOlvido"
                                                                runat="server" CssClass="textSmallBlue" Visible="False">Olvidó su contraseña?</asp:LinkButton>
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
        //Inicio
        //Autor: Diego Montejano Avila
        //Fecha: 2014/09/30
        //Proyecto: Auditoría
        //Observaciones:Cambiar el action del form, para forzar el uso de https
        //
        $(document).ready(function () {
            var ssl = "<%= ConfigurationSettings.AppSettings["isProtocolSecure"] %>";
        if (ssl.toUpperCase() != "FALSE")
            $("form").attr("action", "https://<%=Request.Url.Authority +Request.Url.AbsolutePath%>");
    });
    //Fin
    var devprint = encode_deviceprint();
    var useragent = navigator.userAgent;
    document.getElementById('<%=deviceprintid.ClientID%>').value = devprint;
        document.getElementById('<%=useragentid.ClientID%>').value = useragent; 

    </script>

</body>
</html>
