<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AE_avisoPrivacidad.aspx.cs" Inherits="TPA.interfaz_admon.forma.AE_avisoPrivacidad" %>
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
		<script language="javascript">	

        function OcultarFechaPrivacidad() {

            var chkAvisoPrivacidad = document.getElementById('chkAvisoPrivacidad')
            var txtFechaAvisoPrivacidad = document.getElementById('txtFechaAvisoPrivacidad');

            if (chkAvisoPrivacidad.checked == true) {

                ValidatorEnable(document.getElementById("rfvFechaAvisoPrivacidad"), true);

            }
            else {

                ValidatorEnable(document.getElementById("rfvFechaAvisoPrivacidad"), false);
                txtFechaAvisoPrivacidad.value = "";

            }

        }
	    
    </script>    
   
</head>
<body onload="CargarConfiguracion()">
    <form id="form1" method="post" runat="server">
    <table cellspacing="0" cellpadding="10" width="100%" align="center" border="0">
    <tr>                
                <td align="center">
                <asp:Panel ID="pnlAvisoPrivacidad" runat="server">
                    <fieldset class="FieldSet"  id="FIELDSET2" runat="server">
                        <legend>
                            <img src="../../iconos/label_ok.gif" border="0">&nbsp; Aviso de privacidad</legend>
                        <br>
                        <table id="Table2">
                            <tr>
                                <td>
                                    <asp:Label ID="lblAvisoPrivacidad" runat="server" Text="¿Firmó el aviso de privacidad?"></asp:Label>
                                </td>
                                <td width="9%">
                                    <asp:CheckBox ID="chkAvisoPrivacidad" runat="server" Text="" Checked=false
                                        onClick="javascript:OcultarFechaPrivacidad()"/>
                                </td>
                                <td width="5%">
                                     
                                </td>
                                <td width="19%">
                                <asp:Label ID="lblFechaPrivacidad" runat="server" Text="Fecha"></asp:Label>
                                </td>
                                <td width="60%" align="left">  
                                <asp:TextBox ID="txtFechaAvisoPrivacidad" runat="server" CssClass="textBox" Width="80px"></asp:TextBox>&nbsp;<a
                                        href="javascript:MostrarCalendario(form1.txtFechaAvisoPrivacidad,form1.txtFechaAvisoPrivacidad,'dd/mm/yyyy');"
                                        name="btnFechaAvisoPrivacidad"><img src="../../images/icoCalendar.gif" border="0"
                                            id="imgPapanicolau" runat="server"></a>
                                            <asp:requiredfieldvalidator id="rfvFechaAvisoPrivacidad" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="txtFechaAvisoPrivacidad" ErrorMessage="" Enabled="False">Requerido</asp:requiredfieldvalidator></td>                                  
                                </td>
                            </tr>
                            <tr>
                            <td colspan=5 align="left">
                                <asp:Label ID="lblfechafirma" runat="server" Visible=false Text="El paciente ya firmó el aviso de privacidad el dia: "></asp:Label>
                                <asp:Label ID="lblNoFechaFirma" runat="server" Visible=false Text="El paciente no ha firmado el aviso de privacidad" ForeColor="#990000"></asp:Label>
                                <asp:Label ID="LblUltimaFirma" runat="server" Visible=False></asp:Label>
                            </td>
                            </tr> 
                            <tr>
                            <td colspan=5 align="center">
                               
                                <asp:LinkButton ID="lnkBorrarPrivacidad" runat="server" 
                                    onclick="lnkBorrarPrivacidad_Click" Visible="False">Eliminar las firmas del paciente</asp:LinkButton>
                               
                            </td>
                            </tr>                                                       
                        </table>
                    </fieldset></asp:Panel>
            </td>    
           </tr> 
           <TR>           
					<TD colSpan="6">
						<p align=center><asp:button id="Aceptar" runat="server" CssClass="button" 
                                Text="Aceptar" onclick="Aceptar_Click"></asp:button>
						<asp:button id="Cerrar" runat="server" CssClass="button" Text="Cerrar" 
                                onclick="Cerrar_Click"></asp:button>	
                                <asp:ValidationSummary ID="valSum" runat="server" Font-Names="verdana" Font-Size="12"
                ShowSummary="false"  HeaderText="Para poder continuar debe llenar los campos obligatorios"
                ShowMessageBox="true" />	
						</p>
					</TD>
				</TR>
    </table>
    </form>
</body>
</html>
