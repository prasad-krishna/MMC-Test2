<%@ Page language="c#" Codebehind="LIS_solicitudgastos.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_empleado.forma.LIS_solicitudgastos" %>
<%@ Register TagPrefix="uc1" TagName="WC_DatosEmpleado" Src="../WebControls/WC_DatosEmpleado.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_Pie" Src="../WebControls/WC_Pie.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_Encabezado" Src="../WebControls/WC_Encabezado.ascx" %>
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
		<script language="javascript">			
			
			function ShowMenu(sender,idSolicitud,idSolcitudTipoServicio, tipoSolicitud, idEstado, employee_id, siniestrTPAAnterior, idConsulta)
			{
				var hplCopiar = document.getElementById("hplCopiar");			
				var hplCopiarCompleta = document.getElementById("hplCopiarCompleta");	
				var hplCopiarAnular = document.getElementById("hplCopiarAnular");	
				var hplCopiarAnularCompleta = document.getElementById("hplCopiarAnularCompleta");	
				var hplAnular = document.getElementById("hplAnular");	
				var hplAnularCompleta = document.getElementById("hplAnularCompleta");	
				var hplVer = document.getElementById("hplVer");	
				var hplVerCompleta = document.getElementById("hplVerCompleta");	
				var trCopiarAnular = document.getElementById("trCopiarAnular");	
				var trCopiarAnularCompleta = document.getElementById("trCopiarAnularCompleta");	
				var trAnular = document.getElementById("trAnular");	
				var trAnularCompleta = document.getElementById("trAnularCompleta");	
				var trCopiar = document.getElementById("trCopiar");	
				var trCopiarCompleta = document.getElementById("trCopiarCompleta");	
				var trVer = document.getElementById("trVer");	
				
				if(idEstado != 1 && idEstado != 3)
				{					
					trCopiarAnular.style.display = "none";
					trCopiarAnularCompleta.style.display = "none";
					trAnular.style.display = "none";
					trAnularCompleta.style.display = "none";
				}
				else
				{
					trCopiarAnular.style.display = "";
					trCopiarAnularCompleta.style.display = "";
					trAnular.style.display = "";
					trAnularCompleta.style.display = "";
				}
				if(siniestrTPAAnterior != "0")
				{
					trCopiarAnular.style.display = "none";
					trCopiarAnularCompleta.style.display = "none";								
					trCopiar.style.display = "none";
					trCopiarCompleta.style.display = "none";
				}	
				else
				{
					trCopiarAnular.style.display = "";
					trCopiarAnularCompleta.style.display = "";								
					trCopiar.style.display = "";
					trCopiarCompleta.style.display = "";
				}
										
				
				if(tipoSolicitud == 1)
				{
					hplCopiar.href =  "AE_solicitudreembolso.aspx?IdSolicitudCopia=" + idSolicitud  + "&employee_id=" + employee_id + "&IdSolicitudTipoServicioCopia=" + idSolcitudTipoServicio
					hplCopiarCompleta.href = "AE_solicitudreembolso.aspx?IdSolicitudCopia=" + idSolicitud  + "&employee_id=" + employee_id
					if(idEstado == 1 || idEstado == 3)
					{
						hplCopiarAnular.href = "AE_solicitudreembolso.aspx?IdSolicitudCopia=" + idSolicitud  + "&employee_id=" + employee_id + "&Anular=1" + "&IdSolicitudTipoServicioCopia=" + idSolcitudTipoServicio
						hplCopiarAnularCompleta.href =   "AE_solicitudreembolso.aspx?IdSolicitudCopia=" + idSolicitud  + "&employee_id=" + employee_id + "&Anular=1"
					}
			
					hplVer.href = "AE_solicitudreembolsoresumen.aspx?IdSolicitud=" + idSolicitud  + "&employee_id=" + employee_id + "&IdSolicitudTipoServicio=" + idSolcitudTipoServicio
					hplVerCompleta.href = "AE_solicitudreembolsoresumen.aspx?IdSolicitud=" + idSolicitud  + "&employee_id=" + employee_id
					
										
				}
				else
				{
					if(tipoSolicitud == 2)
					{
						hplCopiar.href =  "AE_solicitudautorizacion.aspx?IdSolicitudCopia=" + idSolicitud  + "&employee_id=" + employee_id + "&IdSolicitudTipoServicioCopia=" + idSolcitudTipoServicio
						hplCopiarCompleta.href = "AE_solicitudautorizacion.aspx?IdSolicitudCopia=" + idSolicitud  + "&employee_id=" + employee_id
						if(idEstado == 1 || idEstado == 3)
						{
							hplCopiarAnular.href = "AE_solicitudautorizacion.aspx?IdSolicitudCopia=" + idSolicitud  + "&employee_id=" + employee_id + "&Anular=1" + "&IdSolicitudTipoServicioCopia=" + idSolcitudTipoServicio
							hplCopiarAnularCompleta.href =   "AE_solicitudautorizacion.aspx?IdSolicitudCopia=" + idSolicitud  + "&employee_id=" + employee_id + "&Anular=1"
						}
						hplVer.href =  "AE_solicitudautorizacionresumen.aspx?IdSolicitud=" + idSolicitud  + "&employee_id=" + employee_id + "&IdSolicitudTipoServicio=" + idSolcitudTipoServicio
						hplVerCompleta.href = "AE_solicitudautorizacionresumen.aspx?IdSolicitud=" + idSolicitud  + "&employee_id=" + employee_id
						
					}
					else
					{
						trCopiarAnular.style.display = "none";
						trCopiarAnularCompleta.style.display = "none";
						trAnular.style.display = "none";
						trAnularCompleta.style.display = "none";
						trCopiar.style.display = "none";
						trCopiarCompleta.style.display = "none";
						trVer.style.display = "none";				
						
						hplVerCompleta.href = "AE_solicitudordenresumen.aspx?IdSolicitud=" + idSolicitud  + "&employee_id=" + employee_id + "&IdConsulta=" +  idConsulta
						
					}						
				}
				if(idEstado == 1 || idEstado == 3)
				{					
					hplAnular.href = "javascript:openPopUp('AE_solicitudestado.aspx?IdSolicitud=" + idSolicitud + "&IdSolicitudTipoServicio=" + idSolcitudTipoServicio + "&IdEstado=5'" + " ,550,330)"	
					hplAnularCompleta.href = "javascript:openPopUp('AE_solicitudestado.aspx?IdSolicitud=" + idSolicitud + "&IdEstado=5'" + " ,550,330)"	
				}	
				var left = Narg_GetPosX(sender) - 0;
				var top = Narg_GetPosY(sender) + 15;            
				setTimeout('showLayer("Menu",' + left + ',' +  top + ');', 50);					
																
			} 			
		</script>
</HEAD>
	<body onload="CargarConfiguracion()">
		<form id="Form1" method="post" runat="server">
			<table class="GG" cellSpacing="0" cellPadding="10" width="100%" align="center" border="0">
				<TR>
					<TD align="left" colSpan="2">
						<TABLE id="Table1" height="23" cellSpacing="0" cellPadding="0" border="0">
							<TR>
								<TD style="WIDTH: 15px"></TD>
								<TD class="titleToolbarLeft" style="WIDTH: 10px"></TD>
								<TD class="titleToolbarCenter" style="WIDTH: 800px">&nbsp;&nbsp;
									<asp:imagebutton id="imbBeneficiarios" runat="server" ImageUrl="../../images/icoBeneficiarios.gif"
										Height="14px" BackColor="Transparent"></asp:imagebutton>&nbsp;<asp:linkbutton id="lnkBeneficiarios" runat="server">Consultar Beneficiarios</asp:linkbutton>&nbsp; 
            |&nbsp;
<asp:linkbutton id=lnkCertificado runat="server"> Certificado Plan</asp:linkbutton>&nbsp;|&nbsp; 
<asp:linkbutton id=lnkConstancia runat="server">Constancia Ingreso</asp:linkbutton></TD>
								<TD class="titleToolbarRight" style="WIDTH: 15px"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD align="center" colSpan="2">&nbsp;
						<uc1:wc_datosempleado id="WC_DatosEmpleado1" runat="server"></uc1:wc_datosempleado></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="2">
						<FIELDSET class="FieldSet" style="WIDTH: 97%"><LEGEND><IMG src="../../images/imgPresupuesto.gif" border="0">
								Presupuesto</LEGEND><BR>
							<asp:datagrid id="dtgPresupuesto" runat="server" AutoGenerateColumns="False" CellPadding="3" GridLines="Horizontal"
								CssClass="grid" Width="98%">
								<AlternatingItemStyle CssClass="altItems"></AlternatingItemStyle>
								<ItemStyle CssClass="norItems"></ItemStyle>
								<HeaderStyle CssClass="headerGrid"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="beneficiario_id"></asp:BoundColumn>
									<asp:BoundColumn DataField="NombreTipoProceso" HeaderText="Tipo Proceso"></asp:BoundColumn>
									<asp:BoundColumn DataField="NombrePlanSolicitud" HeaderText="Plan"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Periodo">
										<ItemTemplate>
											<asp:Label ID="Label1" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.FechaInicio")).ToShortDateString()  + "-" +  Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.FechaFin")).ToShortDateString()%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="NombreTipoPresupuesto" HeaderText="Tipo de Presupuesto"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Usuario">
										<ItemTemplate>
											<asp:Label runat="server" ID="lblBeneficiario"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Presupuesto">
										<ItemTemplate>
											<asp:Label runat="server" ID="lblPresupuesto"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Utilizado">
										<ItemTemplate>
											<asp:Label runat="server" ID="lblUtilizado"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Disponilbe">
										<ItemTemplate>
											<asp:Label runat="server" ID="lblDisponible"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Exceso">
										<ItemTemplate>
											<asp:Label runat="server" ID="lblExceso"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Center" BackColor="White" Mode="NumericPages"></PagerStyle>
							</asp:datagrid><BR>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD align="center" colSpan="2">
						<fieldset class="FieldSet" style="WIDTH: 97%"><legend><IMG src="../../images/icoSolicitud.gif" border="0">
								Histórico de Solicitudes</legend><br>
							<P align="center">&nbsp;</P>
							<asp:datagrid id="dtgSolicitudes" runat="server" AutoGenerateColumns="False" CellPadding="3" GridLines="Horizontal"
								CssClass="grid" Width="98%" PageSize="15" AllowPaging="True">
								<AlternatingItemStyle CssClass="altItems"></AlternatingItemStyle>
								<ItemStyle CssClass="norItems"></ItemStyle>
								<HeaderStyle CssClass="headerGrid"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="IdSolicitud"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="id_solicitud_SICAU"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="id_empleado" HeaderText="Empleado"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="IdSolicitudTipoServicio" HeaderText="IdSolicitudTipoServicio"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="beneficiario_id" HeaderText="Beneficiario"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="IdTipoSolicitud" HeaderText="IdTipoSolicitud"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Menú">
										<ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
										<ItemTemplate>
											<IMG 
      onclick='<%# "javascript:ShowMenu(this," + DataBinder.Eval(Container,"DataItem.IdSolicitud") + "," + DataBinder.Eval(Container,"DataItem.IdSolicitudTipoServicio") + "," + DataBinder.Eval(Container,"DataItem.IdTipoSolicitud") + "," + DataBinder.Eval(Container,"DataItem.IdSolicitudEstado")  + "," + DataBinder.Eval(Container,"DataItem.id_empleado")  + "," + DataBinder.Eval(Container,"DataItem.siniestrTPAAnterior") + "," +  DataBinder.Eval(Container,"DataItem.IdConsulta") + ");" %>' 
      src="../../images/icoMenu.gif">
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="No. Solic">
										<ItemStyle Width="5%"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton CausesValidation="false" CssClass="textSmallBlack" CommandArgument="IdSolicitud" CommandName="Ver" id="lnkVer" runat="server">
												<%#  DataBinder.Eval(Container,"DataItem.NoSolicitud")%>
											</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="NombreTipoServicio" HeaderText="Tipo Servicio">
										<ItemStyle Width="20%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="NombreSolicitudEstado" HeaderText="Estado">
										<ItemStyle HorizontalAlign="Center" Width="4%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy} ">
										<ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Paciente">
										<ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPaciente" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="ValorConvenioSolicitado" HeaderText="Valor Solicitado" DataFormatString="{0:#,##0;($#,##0)}">
										<ItemStyle HorizontalAlign="Right" Width="8%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ValorAprobado" HeaderText="Valor Aprobado" DataFormatString="{0:#,##0;($#,##0)}">
										<ItemStyle HorizontalAlign="Right" Width="8%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="IdConsulta" HeaderText="IdConsulta"></asp:BoundColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Center" BackColor="White" Mode="NumericPages"></PagerStyle>
							</asp:datagrid>
							<P align="center"><asp:label id="Rmsg" runat="server" Font-Size="XX-Small" ForeColor="Brown" Font-Bold="True"></asp:label></P>
						</fieldset>
						<P align="center">&nbsp;</P>
					</TD>
				</TR>
				<TR>
					<TD colSpan="2">
						<P align="center">&nbsp;</P>
					</TD>
				</TR>
				<TR>
					<TD colSpan="2">
						<P align="right">Cantidad de Solicitudes
							<asp:label id="Rcount" runat="server" Font-Bold="True"></asp:label></P>
					</TD>
				</TR>
				<TR>
					<TD align="center" colSpan="2"><BR>
						<BR>
						<BR>
						<BR>
						<BR>
					</TD>
				</TR>
			</table>
			<iframe id="ifrMenu" style="DISPLAY: none; Z-INDEX: 1000; WIDTH: 220px; POSITION: absolute; HEIGHT: 182px"
				scrolling="no" frameBorder="0?" src="pagina.htm"></iframe>
			<div id="dvMenu" style="BORDER-RIGHT: dimgray 1px outset; BORDER-TOP: dimgray 1px outset; DISPLAY: none; Z-INDEX: 1100; OVERFLOW: auto; BORDER-LEFT: dimgray 1px outset; WIDTH: 220px; BORDER-BOTTOM: dimgray 1px outset; POSITION: absolute; HEIGHT: 182px; BACKGROUND-COLOR: white">
				<table style="BORDER-RIGHT: gray 1px solid; BORDER-TOP: gray 1px solid; BORDER-LEFT: gray 1px solid; BORDER-BOTTOM: gray 1px solid; BORDER-COLLAPSE: collapse"
					cellSpacing="0" cellPadding="3" width="100%" border="1">
					<TBODY>
						<tr height="5">
							<td align="left"></td>
						</tr>
						<tr id="trCopiar">
							<td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplCopiar" href="#">Copiar</A>
							</td>
						</tr>
						<tr id="trCopiarCompleta">
							<td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplCopiarCompleta" href="#">Copiar 
									solicitud completa</A>
							</td>
						</tr>
						<tr id="trCopiarAnular">
							<td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplCopiarAnular" href="#">Copiar 
									y anular</A>
							</td>
						</tr>
						<tr id="trCopiarAnularCompleta">
							<td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplCopiarAnularCompleta" href="#">Copiar 
									y anular solicitud completa</A>
							</td>
						</tr>
						<tr id="trAnular">
							<td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplAnular" href="#">Anular</A>
							</td>
						</tr>
						<tr id="trAnularCompleta">
							<td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplAnularCompleta" href="#">Anular 
									solicitud completa</A>
							</td>
						</tr>
						<tr id="trVer">
							<td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplVer" href="#">Ver</A>
							</td>
						</tr>
						<tr id="trVerCompleta">
							<td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplVerCompleta" href="#">Ver 
									solicitud completa</A>
							</td>
						</tr></TD></TR></TBODY></table>
			</div>
		</form>
	</body>
</HTML>
