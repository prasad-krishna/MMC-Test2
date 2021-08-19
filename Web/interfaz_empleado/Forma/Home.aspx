
<%@ Page language="c#" Codebehind="Home.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_empleado.forma.Home" ValidateRequest="false" %>
<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="../WebControls/WC_Menu.ascx" %>
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
        <script language="javascript" src="../../scripts/jquery-1.4.1.js" type="text/javascript"></script>
        <script type="text/javascript">
            $(document).ready(function () {
                $("a").click(function () {
                    if ($(this).attr('href') == "AE_cargahistorias.aspx") {
                        $("#ifrPageContent").attr("scrolling", "yes");
                    }
                    else {
                        $("#ifrPageContent").attr("scrolling", "no");
                    }
                });

            });

            var myclose = false;
            window.addEventListener("beforeunload", function (e) {
                var confirmationMessage = "\o/";

                var xmlHttp = new XMLHttpRequest();
                xmlHttp.open("POST", "WfPage_Load.aspx", false); // false for synchronous request
                xmlHttp.send(null);

                //(e || window.event).returnValue = confirmationMessage; //Gecko + IE
                //return confirmationMessage;                            //Webkit, Safari, Chrome
            });
            
            

        </script>

	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="tblPrincipal" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<td align="left">
						<TABLE id="table1" cellSpacing="0" cellPadding="0" width="975" border="0">
							<TR>
								<TD align="right" background="" colSpan="2" height="60">
									<table height="80" cellSpacing="1" cellPadding="12" width="100%">
                                <tr>
                                    <td>
                                        <img alt="" src="../../images/NuevoBanner.jpg" />
                                    </td>
                                    <td class="titleBigTPA" align="right">
                                        HC
                                    </td>
                                </tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD>
									<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<TR>
											<TD class="titleBackTop" align="left" width="50%" vAlign="middle">&nbsp;&nbsp;
												<asp:linkbutton id="lnkInicio" runat="server" CssClass="titleWhite">Inicio</asp:linkbutton>&nbsp;&nbsp;&nbsp;
												<asp:linkbutton id="lnkSalir" runat="server" CssClass="titleWhite">Salir</asp:linkbutton>
											</TD>
											<TD class="titleBackTopReapeter" align="right" width="50%">
												<asp:label id="lblFecha" runat="server" CssClass="titleWhite"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
												<asp:label id="lblUsuario" runat="server" CssClass="titleWhite"></asp:label>&nbsp;&nbsp;&nbsp;</TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD>
									<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<TR>
											<TD class="tdLightGray" vAlign="top" align="left" width="158"><BR>
												<uc1:WC_Menu id="WC_Menu1" runat="server"></uc1:WC_Menu><BR>
												<INPUT id="hdnSeparadorDecimales" type="hidden" name="hdnSeparadorDecimales" runat="server"><INPUT id="hdnNumeroDecimales" type="hidden" name="hdnNumeroDecimales" runat="server"><INPUT id="hdnSeparadorMiles" type="hidden" name="hdnSeparadorMiles" runat="server"></TD>
											<TD align="center"><iframe id="ifrPageContent" src="blank.html" style="WIDTH: 100%; HEIGHT: 600px" 
                                                name="ifrPageContent" frameborder="0" scrolling="No" runat="server"></iframe>
											</TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD>
									<TABLE id="table4" cellSpacing="0" cellPadding="5" width="100%" align="center" border="0">
                                    <tr>
                                        <td style="border-top: #cccccc 1px solid" width="30%">
                                            <img src="../../images/Footer.gif">
                                        </td>
                                        <td class="textSmallBlack" style="border-top: #cccccc 1px solid" align="right" width="70%">
                                            ©2015 Mercer LLC, Todos los derechos reservados.&nbsp;
                                            <br>
                                        </td>
                                    </tr>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
					</td>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
