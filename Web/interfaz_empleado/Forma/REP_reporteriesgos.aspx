<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="REP_reporteriesgos.aspx.cs" Inherits="TPA.interfaz_admon.forma.REP_reporteriesgos" %>

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
		
		
	  <STYLE type="text/css">.gridFormatoSmall { BORDER-RIGHT: #000000 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; PADDING-BOTTOM: 2px; BORDER-LEFT: #000000 1px solid; PADDING-TOP: 2px; BORDER-BOTTOM: #000000 1px solid; cellpadding: 1px }
	    .norItemsFormatoSmall { BORDER-RIGHT: #000000 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; FONT-SIZE: 8pt; PADDING-BOTTOM: 2px; BORDER-LEFT: #000000 1px solid; COLOR: #000000; PADDING-TOP: 2px; BORDER-BOTTOM: #000000 1px solid; FONT-FAMILY: Verdana, Verdana, Helvetica, sans-serif; BORDER-COLLAPSE: collapse }
	    .headerGridFormatoSmall { BORDER-RIGHT: #000000 1px solid; PADDING-RIGHT: 3px; BORDER-TOP: #000000 1px solid; PADDING-LEFT: 3px; FONT-WEIGHT: bold; FONT-SIZE: 8pt; PADDING-BOTTOM: 3px; BORDER-LEFT: #000000 1px solid; COLOR: #ffffff; PADDING-TOP: 3px; BORDER-BOTTOM: #000000 1px solid; FONT-FAMILY: Verdana, Verdana, Helvetica, sans-serif; BORDER-COLLAPSE: collapse; BACKGROUND-COLOR: Gray; TEXT-ALIGN: center }
	    .tdTituloSeccionReporte { FONT-WEIGHT: bold; FONT-SIZE: 14px; VERTICAL-ALIGN: bottom; COLOR: #003399; FONT-FAMILY: Verdana, Helvetica, sans-serif; HEIGHT: 30px }
	  </STYLE>
</HEAD>
	<body onload="CargarConfiguracion();">
		<form id="Form1" method="post" runat="server">
		
		<script language="javascript" type="text/javascript">
		
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
    
     function CheckBoxListSelectchkTiposConsulta(cbControl, state) {
            var chkBoxList = document.getElementById('<%= chkTiposConsulta.ClientID%>');
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
		<asp:ScriptManager ID="scrMng" runat="server"></asp:ScriptManager>
			<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
				<TR>
					<TD align="center">&nbsp;
						<TABLE class="tableBorder" id="tblBuscar" cellSpacing="0" cellPadding="5" width="80%">
							<TR>
								<TD class="titleBackBlue" width="20%" colSpan="4">Criterios</TD>
							</TR>
							
							<tr>
                        <td>
                        </td>
                        <td align="left">
                            Seleccionar: <a id="A1" href="#" onclick="javascript: CheckBoxListSelectchkTiposConsulta ('',true)">
                                Todas</a> <a id="A2" href="#" onclick="javascript: CheckBoxListSelectchkTiposConsulta ('',false)">
                                    | Ninguna</a>
                        </td>
                    </tr>
                    <tr>
                        <td width="10%" align="left">         <b>Tipos Consulta                                   
                        </b>                                   
                        </td>
                        <td align="left" colspan="3" style="width: 60%">
                            <div style="overflow: auto; width: 469px; height: 100px">                                
                                        <asp:CheckBoxList ID="chkTiposConsulta" runat="server" RepeatColumns="1" CssClass="textBoxSmall"
                                            Width="450px">
                                        </asp:CheckBoxList>
                                    
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
                               
                                 Seleccionar: <a id="A3" href="#" onclick="javascript: CheckBoxListSelectCorporativos ('',true)">
                                    Todas</a> <a id="A4" href="#" onclick="javascript: CheckBoxListSelectCorporativos ('',false)">
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
                            <b>Empresa
                        </b>
                        <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="UpdatePanel6" DisplayAfter="1">
                                <ProgressTemplate>
                                    <div align="center" style="font-weight: bold">
                                        Cargando
                                        
                                           <asp:Image ID="Image4" runat="server" ImageUrl="~/iconos/LoadingBlue.gif" />  
                                     
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
                                        Cargando
                                        
                                           <asp:Image ID="Image5" runat="server" ImageUrl="~/iconos/LoadingBlue.gif" />  
                                     
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
                                        Cargando
                                      <asp:Image ID="Image2" runat="server" ImageUrl="~/iconos/LoadingBlue.gif" />  
                                    
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
                            <b>Médicos</b>
                            <asp:UpdateProgress ID="UpdateProgress3" runat="server" DisplayAfter="1">
                                <ProgressTemplate>
                                    <div align="center" style="font-weight: bold">
                                        Cargando
                                        <asp:Image ID="Image" runat="server" ImageUrl="~/iconos/LoadingBlue.gif" />   
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
								<TD align="left"><b>Reporte Para</b></TD>
								<TD width="30%" align="left" colspan="3">
                                    <asp:RadioButtonList ID="rblReporte" runat="server" RepeatColumns="3" 
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Value="0">Empleados</asp:ListItem>
                                        <asp:ListItem Value="1">Beneficiarios</asp:ListItem>
                                        <asp:ListItem Value="-1">Empleados y Beneficiarios</asp:ListItem>
                                    </asp:RadioButtonList>
								</TD>
							</TR>
							<TR>
								<TD align="left"><b>Fecha Desde</b></TD>
								<TD width="30%" align="left"><asp:textbox id="txtFechaInicio" runat="server" Width="80px" CssClass="textBox"></asp:textbox>&nbsp;<A href="javascript:MostrarCalendario(Form1.txtFechaInicio,Form1.txtFechaInicio,'dd/mm/yyyy');"
										name="btnFechaInicio"><IMG src="../../images/icoCalendar.gif" border="0"></A>
								</TD>
								<TD align="left"><b>Fecha Hasta</b></TD>
								<TD align="left"><asp:textbox id="txtFechaFin" runat="server" Width="80px" CssClass="textBox"></asp:textbox>&nbsp;<A href="javascript:MostrarCalendario(Form1.txtFechaFin,Form1.txtFechaFin,'dd/mm/yyyy');"
										name="btnFechaFin"><IMG src="../../images/icoCalendar.gif" border="0"></A>
								</TD>
							</TR>
							<TR>
								<TD align="center" colSpan="4"><asp:button id="btnEnviar" runat="server" 
                                        CssClass="button" CausesValidation="False" Text="Enviar" 
                                        onclick="btnEnviar_Click"></asp:button><asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnReload" Visible="false" runat="server" Text="Button" />
                                    </ContentTemplate>
                                </asp:UpdatePanel></TD>                                         
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD align="left" height="15">
                        <br />
                        <asp:ImageButton ID="imbExportar" runat="server" AlternateText="Imprimir" 
                            ImageUrl="../../iconos/icoExportar.gif" onclick="imbExportar_Click" />
                        <asp:LinkButton ID="lnkExportar" runat="server" onclick="lnkExportar_Click">Exportar a Excel</asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:ImageButton ID="imbExportarDetalle" runat="server" AlternateText="Imprimir" 
                            ImageUrl="../../iconos/icoExportar.gif" onclick="imbExportarDetalle_Click" 
                            Visible="False" />
                        <asp:LinkButton ID="lnkExportarDetalle" runat="server" 
                            onclick="lnkExportarDetalle_Click" Visible="False">Exportar Detalle por 
                        Paciente</asp:LinkButton>
                        <br />
                        <br />
                    </TD>
				</TR>
				<tr><td>
				<table cellSpacing="0" id="tblReporte" runat="server" cellPadding="0" width="100%" align="center" border="0">
				<TR>
					<TD align="left" class="tdTituloSeccionReporte"> 
                        DEFINICIÓN DE LA POBLACIÓN</TD>
				</TR>
				<TR>
					<TD align="left">                       
                            <asp:GridView ID="gvReporte1" runat="server">
                            
                                <AlternatingRowStyle CssClass="norItemsFormatoSmall"></AlternatingRowStyle>                                
								<RowStyle CssClass="norItemsFormatoSmall"></RowStyle>							
								<HeaderStyle CssClass="headerGridFormatoSmall"></HeaderStyle>
                            </asp:GridView>
                     </TD>
				</TR>
				<TR>
					<TD align="left" class="tdTituloSeccionReporte"> 
                        RIESGO DE ACCIDENTABILIDAD</TD>
				</TR>
				<TR>
					<TD align="left"> 
                            <asp:GridView ID="gvReporte2" runat="server">
                            <AlternatingRowStyle CssClass="norItemsFormatoSmall"></AlternatingRowStyle>                                
								<RowStyle CssClass="norItemsFormatoSmall"></RowStyle>							
								<HeaderStyle CssClass="headerGridFormatoSmall"></HeaderStyle>
                            </asp:GridView>
                    </TD>
				</TR>
				<TR>
					<TD align="left" class="tdTituloSeccionReporte"> 
                        RIESGO EN EL NIVEL DE DEPENDENCIA DE ALCOHOL</TD>
				</TR>
				<TR>
					<TD align="left"> 
                            <asp:GridView ID="gvReporte3" runat="server">
                            <AlternatingRowStyle CssClass="norItemsFormatoSmall"></AlternatingRowStyle>                                
								<RowStyle CssClass="norItemsFormatoSmall"></RowStyle>							
								<HeaderStyle CssClass="headerGridFormatoSmall"></HeaderStyle>
                            </asp:GridView>
                    </TD>
				</TR>
				<TR>
					<TD align="left" class="tdTituloSeccionReporte"> 
                        RIESGO EN EL NIVEL DE DEPENDENCIA DE TABACO</TD>
				</TR>
				<TR>
					<TD align="left"> 
                            <asp:GridView ID="gvReporte4" runat="server">
                            <AlternatingRowStyle CssClass="norItemsFormatoSmall"></AlternatingRowStyle>                                
								<RowStyle CssClass="norItemsFormatoSmall"></RowStyle>							
								<HeaderStyle CssClass="headerGridFormatoSmall"></HeaderStyle>
                            </asp:GridView>        
                            <br />   <br />                    
                            <asp:GridView ID="gvReporte5" runat="server">
                            <AlternatingRowStyle CssClass="norItemsFormatoSmall"></AlternatingRowStyle>                                
								<RowStyle CssClass="norItemsFormatoSmall"></RowStyle>							
								<HeaderStyle CssClass="headerGridFormatoSmall"></HeaderStyle>
                            </asp:GridView>
                    </TD>
				</TR>
				<TR>
					<TD align="left" class="tdTituloSeccionReporte"> 
                        RIESGO EMOCIONAL</TD>
				</TR>
				<TR>
					<TD align="left"> 
                            <asp:GridView ID="gvReporte6" runat="server">
                            <AlternatingRowStyle CssClass="norItemsFormatoSmall"></AlternatingRowStyle>                                
								<RowStyle CssClass="norItemsFormatoSmall"></RowStyle>							
								<HeaderStyle CssClass="headerGridFormatoSmall"></HeaderStyle>
                            </asp:GridView>
                    </TD>
				</TR>
				
				<TR>
					<TD align="left" class="tdTituloSeccionReporte"> 
                        RIESGO METABÓLICO</TD>
				</TR>
				<TR>
					<TD align="left"> 
                             <asp:GridView ID="gvReporte7" runat="server">
                             <AlternatingRowStyle CssClass="norItemsFormatoSmall"></AlternatingRowStyle>                                
								<RowStyle CssClass="norItemsFormatoSmall"></RowStyle>							
								<HeaderStyle CssClass="headerGridFormatoSmall"></HeaderStyle>
                            </asp:GridView>        
                            <br />   <br />                    
                            <asp:GridView ID="gvReporte8" runat="server">
                            <AlternatingRowStyle CssClass="norItemsFormatoSmall"></AlternatingRowStyle>                                
								<RowStyle CssClass="norItemsFormatoSmall"></RowStyle>							
								<HeaderStyle CssClass="headerGridFormatoSmall"></HeaderStyle>
                            </asp:GridView>
                    </TD>
				</TR>
				<TR>
					<TD align="left" class="tdTituloSeccionReporte"> 
                        CLASIFICACIÓN DEL ESTADO NUTRICIONAL CON BASE EN EL ÍNDICE DE MASA CORPORAL</TD>
				</TR>
				<TR>
					<TD align="left"> 
                            <asp:GridView ID="gvReporte9" runat="server">
                            <AlternatingRowStyle CssClass="norItemsFormatoSmall"></AlternatingRowStyle>                                
								<RowStyle CssClass="norItemsFormatoSmall"></RowStyle>							
								<HeaderStyle CssClass="headerGridFormatoSmall"></HeaderStyle>
                            </asp:GridView>
                    </TD>
				</TR>
				
				<TR>
					<TD align="left" class="tdTituloSeccionReporte"> 
                        RIESGO CARDIOVASCULAR</TD>
				</TR>
				<TR>
					<TD align="left"> 
                            <asp:GridView ID="gvReporte10" runat="server">
                            <AlternatingRowStyle CssClass="norItemsFormatoSmall"></AlternatingRowStyle>                                
								<RowStyle CssClass="norItemsFormatoSmall"></RowStyle>							
								<HeaderStyle CssClass="headerGridFormatoSmall"></HeaderStyle>
                            </asp:GridView>
                             <br />   <br />                    
                            <asp:GridView ID="gvReporte11" runat="server">
                            <AlternatingRowStyle CssClass="norItemsFormatoSmall"></AlternatingRowStyle>                                
								<RowStyle CssClass="norItemsFormatoSmall"></RowStyle>							
								<HeaderStyle CssClass="headerGridFormatoSmall"></HeaderStyle>
                            </asp:GridView>
                             <br />   <br />                    
                            <asp:GridView ID="gvReporte12" runat="server">
                            <AlternatingRowStyle CssClass="norItemsFormatoSmall"></AlternatingRowStyle>                                
								<RowStyle CssClass="norItemsFormatoSmall"></RowStyle>							
								<HeaderStyle CssClass="headerGridFormatoSmall"></HeaderStyle>
                            </asp:GridView>
                    </TD>
				</TR>
				<TR>
					<TD align="left" class="tdTituloSeccionReporte"> 
                        RIESGO SEDENTARISMO</TD>
				</TR>
				<TR>
					<TD align="left"> 
                            <asp:GridView ID="gvReporte13" runat="server">
                            <AlternatingRowStyle CssClass="norItemsFormatoSmall"></AlternatingRowStyle>                                
								<RowStyle CssClass="norItemsFormatoSmall"></RowStyle>							
								<HeaderStyle CssClass="headerGridFormatoSmall"></HeaderStyle>
                            </asp:GridView>
                            <br />   <br />  
                            <span class="tdTituloSeccionReporte">Frecuencia Actividad Deportiva</span>  
                            <asp:GridView ID="gvReporte14" runat="server">
                            <AlternatingRowStyle CssClass="norItemsFormatoSmall"></AlternatingRowStyle>                                
								<RowStyle CssClass="norItemsFormatoSmall"></RowStyle>							
								<HeaderStyle CssClass="headerGridFormatoSmall"></HeaderStyle>
                            </asp:GridView>
                            <br />   <br />  
                            <span class="tdTituloSeccionReporte">Promedio Tiempo en Actividad Deportiva</span>  
                            <asp:GridView ID="gvReporte15" runat="server">
                            <AlternatingRowStyle CssClass="norItemsFormatoSmall"></AlternatingRowStyle>                                
								<RowStyle CssClass="norItemsFormatoSmall"></RowStyle>							
								<HeaderStyle CssClass="headerGridFormatoSmall"></HeaderStyle>
                            </asp:GridView>
                            <br />   <br />  
                            <span class="tdTituloSeccionReporte">Tipo de Actividad Deportiva</span>  
                            <asp:GridView ID="gvReporte16" runat="server">
                            <AlternatingRowStyle CssClass="norItemsFormatoSmall"></AlternatingRowStyle>                                
								<RowStyle CssClass="norItemsFormatoSmall"></RowStyle>							
								<HeaderStyle CssClass="headerGridFormatoSmall"></HeaderStyle>
                            </asp:GridView>
                            <br />   <br />  
                            <span class="tdTituloSeccionReporte">Horas de TV Diarias</span>  
                            <asp:GridView ID="gvReporte17" runat="server">
                            <AlternatingRowStyle CssClass="norItemsFormatoSmall"></AlternatingRowStyle>                                
								<RowStyle CssClass="norItemsFormatoSmall"></RowStyle>							
								<HeaderStyle CssClass="headerGridFormatoSmall"></HeaderStyle>
                            </asp:GridView>
                    </TD>
                </TR>
				
				</table>
				</td></tr></table>
			
		</form>
	<p>
        &nbsp;</p>
<p>
    &nbsp;</p>
<p>
    &nbsp;</p>

	</body>
</HTML>
