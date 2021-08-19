<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="REP_ausentismo.aspx.cs"
    Inherits="TPA.interfaz_admon.forma.REP_ausentismo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>HC-Historias Clínicas</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../../css/admon.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../../scripts/Base.js" type="text/javascript"></script>
    <link href="../../css/Calendar.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../../scripts/Calendar.js" type="text/javascript"></script>
    <script language="javascript">		
	
	function CheckBoxListSelectCorporativos(cbControl, state) {
            var chkBoxList = document.getElementById('<%= chkCorporativo.ClientID%>');
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            for (var i = 0; i < chkBoxCount.length; i++) {
                chkBoxCount[i].checked = state;
            }
            __doPostBack('btnReload','');
            return false;

        }
        
	function CheckBoxListSelectEmpresas(cbControl, state) {
            var chkBoxList = document.getElementById('<%= chkEmpresas.ClientID%>');
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

</head>
<body onload="CargarConfiguracion();">
    <form id="Form1" method="post" runat="server">
    <asp:ScriptManager ID="scrMng" runat="server">
    </asp:ScriptManager>
    <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
        <tr>
            <td align="center" colspan="2">
                &nbsp;
                <table class="tableBorder" id="tblBuscar" cellspacing="0" cellpadding="5" width="80%">
                    <tr>
                        <td class="titleBackBlue" width="20%" colspan="4">
                            Criterios
                        </td>
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
                        <td colspan="4">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td width="10%" align="left">
                            &nbsp;</td>
                        <td align="left" colspan="3" style="width: 60%">
                             Seleccionar: <a id="LnkTodasEmpresa" href="#" onclick="javascript: CheckBoxListSelectEmpresas ('',true)">
                                    Todas</a> <a id="LnkNingunaEmpresa" href="#" onclick="javascript: CheckBoxListSelectEmpresas ('',false)">
                                        | Ninguna</a></td>
                    </tr>
                    <tr>
                        <td width="10%" align="left">
                            <b>Empresa</b>
                            <br />
                            <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="UpdatePanel6" DisplayAfter="1">
                                <ProgressTemplate>
                                    <div align="center" style="font-weight: bold">
                                        <asp:Image ID="Image5" runat="server" ImageUrl="~/iconos/LoadingBlue.gif" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
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
                    <tr>
                        <td colspan="4">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            Fecha Desde
                        </td>
                        <td width="30%" align="left">
                            <asp:TextBox ID="txtFechaInicio" runat="server" Width="80px" CssClass="textBox"></asp:TextBox>&nbsp;<a
                                href="javascript:MostrarCalendario(Form1.txtFechaInicio,Form1.txtFechaInicio,'dd/mm/yyyy');"
                                name="btnFechaInicio"><img src="../../images/icoCalendar.gif" border="0"></a>
                        </td>
                        <td align="left">
                            Fecha Hasta
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtFechaFin" runat="server" Width="80px" CssClass="textBox"></asp:TextBox>&nbsp;<a
                                href="javascript:MostrarCalendario(Form1.txtFechaFin,Form1.txtFechaFin,'dd/mm/yyyy');"
                                name="btnFechaFin"><img src="../../images/icoCalendar.gif" border="0"></a>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                            <asp:Button ID="btnEnviar" runat="server" CssClass="button" CausesValidation="False"
                                Text="Enviar" OnClick="btnEnviar_Click"></asp:Button>
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnReload" Visible="false" runat="server" Text="Button" OnClick="btnReload_Click" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td> </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2" height="15">
                <br />
                <asp:ImageButton ID="imbExportar" runat="server" AlternateText="Imprimir" ImageUrl="../../iconos/icoExportar.gif"
                    OnClick="imbExportar_Click" />
                <asp:LinkButton ID="lnkExportar" runat="server" OnClick="lnkExportar_Click">Exportar a Excel</asp:LinkButton>
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Panel ID="pnlGrilla" runat="server">
                    <asp:GridView ID="gvReporte" runat="server">
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <p>
                    &nbsp;</p>
                <p>
                    &nbsp;</p>
                <p>
                    &nbsp;</p>
                <p>
                    &nbsp;</p>
                <p>
                    &nbsp;</p>
                <p>
                    &nbsp;</p>
                <p>
                    &nbsp;</p>
                <p>
                    &nbsp;</p>
            </td>
        </tr>
    </table>
    <iframe id="ifrMenu" style="display: none; z-index: 1000; width: 200px; position: absolute;
        height: 122px" frameborder="0" scrolling="no" src="pagina.htm"></iframe>
    <div id="dvMenu" style="border-right: dimgray 1px outset; border-top: dimgray 1px outset;
        display: none; z-index: 1100; overflow: auto; border-left: dimgray 1px outset;
        width: 200px; border-bottom: dimgray 1px outset; position: absolute; height: 122px;
        background-color: white">
        <table style="border-right: gray 1px solid; border-top: gray 1px solid; border-left: gray 1px solid;
            border-bottom: gray 1px solid; border-collapse: collapse" cellspacing="0" cellpadding="5"
            width="100%" border="1">
            <tbody>
                <tr height="5">
                    <td align="left">
                        <asp:Image ID="Image1" Style="cursor: pointer" onclick="closeLayer('Menu')" runat="server"
                            ImageUrl="../../images/imgClose.gif"></asp:Image>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <img src="../../images/imgBullet.gif" border="0">&nbsp;<a id="hplSolicitudes" href="#">Modificar
                            Órdenes</a>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <img src="../../images/imgBullet.gif" border="0">&nbsp;<a id="hplEditar" href="#">Ver</a>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <img src="../../images/imgBullet.gif" border="0">&nbsp;<a id="hplVerResumen" href="#">Ver
                            Resumen</a>
                    </td>
                </tr>
                <tr id="trModificar" style="display: none">
                    <td align="left">
                        <img src="../../images/imgBullet.gif" border="0">&nbsp;<a id="hplModificar" href="#">Modificar
                            Consulta</a>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
