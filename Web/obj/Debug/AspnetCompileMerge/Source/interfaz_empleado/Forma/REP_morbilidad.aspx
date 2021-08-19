<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="REP_morbilidad.aspx.cs" Inherits="TPA.interfaz_admon.forma.REP_morbilidad" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
		
		function CheckBoxListSelectEmpresas(cbControl, state) {
            var chkBoxList = document.getElementById('<%= chkEmpresas.ClientID%>');
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            for (var i = 0; i < chkBoxCount.length; i++) {
                chkBoxCount[i].checked = state;
            }
            __doPostBack('btnReload','');
            return false;
        }
        
        	function CheckBoxListSelectCorporativos(cbControl, state) {
            var chkBoxList = document.getElementById('<%= chkCorporativo.ClientID%>');
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            for (var i = 0; i < chkBoxCount.length; i++) {
                chkBoxCount[i].checked = state;
            }
            __doPostBack('btnReload','');
            return false;

        }
        
    function CheckBoxListSelectchkSedes(cbControl, state) {
            var chkBoxList = document.getElementById('<%= chkSedes.ClientID%>');
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            for (var i = 0; i < chkBoxCount.length; i++) {
                chkBoxCount[i].checked = state;
            }
            return false;
        }
        
    function CheckBoxListSelectchkUsuarios(cbControl, state) {
            var chkBoxList = document.getElementById('<%= chkUsuarios.ClientID%>');
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            for (var i = 0; i < chkBoxCount.length; i++) {
                chkBoxCount[i].checked = state;
            }
            return false;
        }
        
     function CheckBoxListSelectchkchkMedicos(cbControl, state) {
            var chkBoxList = document.getElementById('<%= chkMedicos.ClientID%>');
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            for (var i = 0; i < chkBoxCount.length; i++) {
                chkBoxCount[i].checked = state;
            }
            return false;
        }					
			
		</script>
</HEAD>
	<body onload="CargarConfiguracion();">
		<form id="Form1" method="post" runat="server">
		<asp:ScriptManager ID="scrMng" runat="server"></asp:ScriptManager>
			<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
				<TR>
					<TD align="center" colSpan="2">&nbsp;
						<TABLE class="tableBorder" id="tblBuscar" cellSpacing="0" cellPadding="5" width="80%">
							<TR>
								<TD class="titleBackBlue" width="20%" colSpan="4">Criterios</TD>
							</TR>
							</tr>
                        <tr>
                            <td>
                            </td>
                            <td align="left">
                               
                                 Seleccionar: <a id="A1" href="#" onclick="javascript: CheckBoxListSelectCorporativos ('',true)">
                                    Todas</a> <a id="A2" href="#" onclick="javascript: CheckBoxListSelectCorporativos ('',false)">
                                        | Ninguna</a></td>
                        </tr>
                    <tr>
                        <td width="10%" align="left">
                            <b>Corporativo</b>
                        </td>
                        <td align="left" colspan="3" style="width: 60%">
                            <div style="overflow: auto; width: 469px; height: 100px">
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:CheckBoxList ID="chkCorporativo" runat="server" RepeatColumns="1" CssClass="textBoxSmall"
                                            Width="450px" AutoPostBack="True" OnSelectedIndexChanged="chkCorporativo_SelectedIndexChanged">
                                        </asp:CheckBoxList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </td>
                    </tr>
							    <tr>
                            <td>
                            </td>
                            <td align="left">
                                Seleccionar: <a id="LnkTodasEmpresa" href="#" onclick="javascript: CheckBoxListSelectEmpresas ('',true)">
                                    Todas</a> <a id="LnkNingunaEmpresa" href="#" onclick="javascript: CheckBoxListSelectEmpresas ('',false)">
                                        | Ninguna</a>
                            </td>
                        </tr>
							<tr>
                        <td width="10%" align="left">
                            <b>Empresa</b>
                        </td>
                        <td align="left" colspan="3" style="width: 60%">
                            <div style="overflow: auto; width: 469px; height: 100px">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:CheckBoxList ID="chkEmpresas" runat="server" RepeatColumns="1" CssClass="textBoxSmall"
                                            Width="450px" AutoPostBack="True" OnSelectedIndexChanged="chkEmpresas_SelectedIndexChanged">
                                        </asp:CheckBoxList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="left">
                            Seleccionar: <a id="LnkTodasSede" href="#" onclick="javascript: CheckBoxListSelectchkSedes ('',true)">
                                Todas</a> <a id="LnkNingunaSede" href="#" onclick="javascript: CheckBoxListSelectchkSedes ('',false)">
                                    | Ninguna</a>
                        </td>
                    </tr>
                    <tr>
                        <td width="10%" align="left">
                            <b>Sede</b>
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1">
                                <ProgressTemplate>
                                    <div align="center" style="font-weight: bold">
                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/iconos/LoadingBlue.gif" />  
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                        <td align="left" colspan="3" style="width: 60%">
                            <div style="overflow: auto; width: 469px; height: 100px">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:CheckBoxList ID="chkSedes" runat="server" RepeatColumns="1" CssClass="textBoxSmall"
                                            Width="450px">
                                        </asp:CheckBoxList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="left">
                            Seleccionar: <a id="LnkTodasUsuarios" href="#" onclick="javascript: CheckBoxListSelectchkUsuarios ('',true)">
                                Todas</a> <a id="LnkNingunaUsuarios" href="#" onclick="javascript: CheckBoxListSelectchkUsuarios ('',false)">
                                    | Ninguna</a>
                        </td>
                    </tr>
                    <tr>
                        <td width="10%" align="left">
                            <b>Usuarios</b>
                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="1">
                                <ProgressTemplate>
                                    <div align="center" style="font-weight: bold">
                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/iconos/LoadingBlue.gif" />  
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                        <td align="left" colspan="3" style="width: 60%">
                            <div style="overflow: auto; width: 469px; height: 100px">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:CheckBoxList ID="chkUsuarios" runat="server" RepeatColumns="1" CssClass="textBoxSmall"
                                            Width="450px">
                                        </asp:CheckBoxList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <hr />
                        </td>
                    </tr>
                     <tr>
                        <td>
                        </td>
                        <td align="left">
                            Seleccionar: <a id="LnkTodasMedicos" href="#" onclick="javascript: CheckBoxListSelectchkchkMedicos ('',true)">
                                Todas</a> <a id="LnkNingunaMedicos" href="#" onclick="javascript: CheckBoxListSelectchkchkMedicos ('',false)">
                                    | Ninguna</a>
                        </td>
                    </tr>
                    <tr>
                        <td width="10%" align="left">
                            <b>Medicos</b>
                            <asp:UpdateProgress ID="UpdateProgress3" runat="server" DisplayAfter="1">
                                <ProgressTemplate>
                                    <div align="center" style="font-weight: bold">
                                        <asp:Image ID="Image4" runat="server" ImageUrl="~/iconos/LoadingBlue.gif" />  
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                        <td align="left" colspan="3" style="width: 60%">
                            <div style="overflow: auto; width: 469px; height: 100px">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:CheckBoxList ID="chkMedicos" runat="server" RepeatColumns="1" CssClass="textBoxSmall"
                                            Width="450px">
                                        </asp:CheckBoxList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </td>
                    </tr>
							<TR>
								<TD align="left">Fecha Desde</TD>
								<TD width="30%" align="left"><asp:textbox id="txtFechaInicio" runat="server" Width="80px" CssClass="textBox"></asp:textbox>&nbsp;<A href="javascript:MostrarCalendario(Form1.txtFechaInicio,Form1.txtFechaInicio,'dd/mm/yyyy');"
										name="btnFechaInicio"><IMG src="../../images/icoCalendar.gif" border="0"></A>
								</TD>
								<TD align="left">Fecha Hasta</TD>
								<TD align="left"><asp:textbox id="txtFechaFin" runat="server" Width="80px" CssClass="textBox"></asp:textbox>&nbsp;<A href="javascript:MostrarCalendario(Form1.txtFechaFin,Form1.txtFechaFin,'dd/mm/yyyy');"
										name="btnFechaFin"><IMG src="../../images/icoCalendar.gif" border="0"></A>
								</TD>
							</TR>
							<TR>
								<TD align="center" colSpan="4"><asp:button id="btnEnviar" runat="server" 
                                        CssClass="button" CausesValidation="False" Text="Enviar" 
                                        onclick="btnEnviar_Click"></asp:button><asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnReload" Visible="false" runat="server" Text="Button" OnClick="btnReload_Click" />
                                    </ContentTemplate>
                                </asp:UpdatePanel></TD>                                         
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD align="left" colSpan="2" height="15">
                        <br />
                        <asp:ImageButton ID="imbExportar" runat="server" AlternateText="Imprimir" 
                            ImageUrl="../../iconos/icoExportar.gif" onclick="imbExportar_Click" />
                        <asp:LinkButton ID="lnkExportar" runat="server" onclick="lnkExportar_Click">Exportar a Excel</asp:LinkButton>
                        <br />
                        <br />
                    </TD>
				</TR>
				<TR>
					<TD align="center" colSpan="2"> 
                        <asp:Panel ID="pnlGrilla" runat="server">
                            <asp:GridView ID="gvReporte" runat="server">
                            </asp:GridView>
                        </asp:Panel>
                    </TD>
				</TR>
				<TR>
					<TD align="center" colSpan="2">
						<P>&nbsp;</P>
						<P>&nbsp;</P>
						<P>&nbsp;</P>
						<P>&nbsp;</P>
						<P>&nbsp;</P>
						<P>&nbsp;</P>
						<P>&nbsp;</P>
						<P>&nbsp;</P>
					</TD>
				</TR>
			</table>
			<iframe id="ifrMenu" style="DISPLAY: none; Z-INDEX: 1000; WIDTH: 200px; POSITION: absolute; HEIGHT: 122px"
				frameBorder="0" scrolling="no" src="pagina.htm"></iframe>
			<div id="dvMenu" style="BORDER-RIGHT: dimgray 1px outset; BORDER-TOP: dimgray 1px outset; DISPLAY: none; Z-INDEX: 1100; OVERFLOW: auto; BORDER-LEFT: dimgray 1px outset; WIDTH: 200px; BORDER-BOTTOM: dimgray 1px outset; POSITION: absolute; HEIGHT: 122px; BACKGROUND-COLOR: white">
				<table style="BORDER-RIGHT: gray 1px solid; BORDER-TOP: gray 1px solid; BORDER-LEFT: gray 1px solid; BORDER-BOTTOM: gray 1px solid; BORDER-COLLAPSE: collapse"
					cellSpacing="0" cellPadding="5" width="100%" border="1">
					<TBODY>
						<tr height="5">
							<td align="left"><asp:image id="Image1" style="CURSOR: pointer" onclick="closeLayer('Menu')" runat="server"
									ImageUrl="../../images/imgClose.gif"></asp:image></td>
						</tr>
						<tr>
							<td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplSolicitudes" href="#">Modificar 
									Órdenes</A>
							</td>
						</tr>
						<tr>
							<td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplEditar" href="#">Ver</A>
							</td>
						</tr>
						<tr>
							<td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplVerResumen" href="#">Ver 
									Resumen</A>
							</td>
						</tr>
						<tr id="trModificar" style="display:none">
							<td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplModificar" href="#">Modificar Consulta</A>
							</td>
						</tr></TBODY></table>
			</div>
		</form>
	</body>
</HTML>
