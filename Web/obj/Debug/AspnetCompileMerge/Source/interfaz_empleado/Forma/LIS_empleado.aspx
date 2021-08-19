<%@ Page language="c#" Codebehind="LIS_empleado.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_admon.forma.LIS_empleado" %>
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

		    function ActualizarPrivacidad() {
		    
		        __doPostBack('btnReload', '');


		    }
		    function ShowMenu(sender, idEmployee, idBeneficiario, estado) {
		        var hdnId = document.getElementById("hdnId");
		        var hdnIdBene = document.getElementById("hdnIdBene");
		        var hdnLiquidacion = document.getElementById("hdnLiquidacion");
		        var hdnLiquidacionValue = document.getElementById("hdnLiquidacionValue");
		        var hdnPermisos = document.getElementById("hdnPermisos");
		        var hdnRealizaAutorizaciones = document.getElementById("hdnRealizaAutorizaciones");
		        var hdnRealizaReembolsos = document.getElementById("hdnRealizaReembolsos");
		        var hdnRealizaConsultas = document.getElementById("hdnRealizaConsultas");
		        var hdnRealizaCitas = document.getElementById("hdnRealizaCitas");

		        var trConsulta = document.getElementById("trConsulta");
		        var trReembolso = document.getElementById("trReembolso");
		        var trAutorizacion = document.getElementById("trAutorizacion");

		        var hplConsultar = document.getElementById("hplConsultar");
		        var hplBeneficiarios = document.getElementById("hplBeneficiarios");
		        var hplSolicitudes = document.getElementById("hplSolicitudes");
		        var hplAutorizacion = document.getElementById("hplAutorizacion");
		        var hplReembolso = document.getElementById("hplReembolso");
		        var hplConsulta = document.getElementById("hplConsulta");
		        var hplAvisoPrivacidad = document.getElementById("hplAvisoPrivacidad");

		        hplConsultar.href = "javascript:openPopUp('AE_employee.aspx?acc=UX&employee_id=???',900,600);";
		        hplBeneficiarios.href = "javascript:openPopUp('LIS_beneficiarios.aspx?EMPLOYEE_ID=???',900,400);";
		        hplSolicitudes.href = "LIS_solicitudgastos.aspx?employee_id=???";
		        hplAutorizacion.href = "AE_solicitudautorizacion.aspx?employee_id=???&beneficiario_id=??&liquidacionConfirmacion=XXX";
		        hplReembolso.href = "AE_solicitudreembolso.aspx?employee_id=???&beneficiario_id=??&liquidacionConfirmacion=XXX";
		        hplConsulta.href = "AE_registroconsulta.aspx?employee_id=???&beneficiario_id=??";
		        hplAvisoPrivacidad.href = "javascript:openPopUp('AE_avisoPrivacidad.aspx?acc=UX&employee_id=???&beneficiario_id=??',530,340);"; 

		        hplConsultar.href = hplConsultar.href.replace(hdnId.value, idEmployee);
		        hplBeneficiarios.href = hplBeneficiarios.href.replace(hdnId.value, idEmployee);
		        hplSolicitudes.href = hplSolicitudes.href.replace(hdnId.value, idEmployee);
		        hplReembolso.href = hplReembolso.href.replace(hdnId.value, idEmployee);
		        hplAutorizacion.href = hplAutorizacion.href.replace(hdnId.value, idEmployee);
		        hplConsulta.href = hplConsulta.href.replace(hdnId.value, idEmployee);
		        hplAvisoPrivacidad.href = hplAvisoPrivacidad.href.replace(hdnId.value, idEmployee);

		        hplReembolso.href = hplReembolso.href.replace(hdnIdBene.value, idBeneficiario);
		        hplAutorizacion.href = hplAutorizacion.href.replace(hdnIdBene.value, idBeneficiario);
		        hplConsultar.href = hplConsultar.href.replace(hdnIdBene.value, idBeneficiario);
		        hplConsulta.href = hplConsulta.href.replace(hdnIdBene.value, idBeneficiario);
		        hplReembolso.href = hplReembolso.href.replace(hdnLiquidacion.value, hdnLiquidacionValue.Value);
		        hplAutorizacion.href = hplAutorizacion.href.replace(hdnLiquidacion.value, hdnLiquidacionValue.value);
		        hplAvisoPrivacidad.href = hplAvisoPrivacidad.href.replace(hdnIdBene.value, idBeneficiario);

		        /*Código para manejar items de agenda ( Modificado Marzo 15-2010
		        No es necesario tener los textbox escondidos si se guarda el link en un atributo data-link
		        del elemento html*/
		        var linkCrearCita = document.getElementById("hplNuevaCita");
		        var linkCrearCitaUrgencia = document.getElementById("hplNuevaCitaUrgencia");
		        //Obtener el link con los tokens del atributo data-link y asignar el link del vinculo
		        linkCrearCita.href = linkCrearCita.getAttribute("data-link").replace("param_idempleado", idEmployee).replace("param_idbeneficiario", idBeneficiario); ;
		        linkCrearCitaUrgencia.href = linkCrearCitaUrgencia.getAttribute("data-link").replace("param_idempleado", idEmployee).replace("param_idbeneficiario", idBeneficiario);

		        
                /*Fin de codigo para manejar items de agenda*/

		        if (estado == 2) {
		            if (hdnPermisos.value == "0") {
		                trReembolso.style.display = "none";
		                trAutorizacion.style.display = "none";
		                trConsulta.style.display = "none";

		            }
		            else {
		                if (hdnRealizaReembolsos.value != "0")
		                    trReembolso.style.display = "";
		                if (hdnRealizaAutorizaciones.value != "0")
		                    trAutorizacion.style.display = "";
		                if (hdnRealizaConsultas.value != "0")
		                    trConsulta.style.display = "";
		                if (hdnRealizaCitas.value != "0")
		                    trNuevaCita.style.display = "";
		            }
		        }
		        else {
		            if (hdnRealizaReembolsos.value != "0")
		                trReembolso.style.display = "";
		            if (hdnRealizaAutorizaciones.value != "0")
		                trAutorizacion.style.display = "";
		            if (hdnRealizaConsultas.value != "0")
		                trConsulta.style.display = "";
		            if (hdnRealizaCitas.value != "0")
		                trNuevaCita.style.display = "";
		        }

		        var left = Narg_GetPosX(sender) - 0;
		        var top = Narg_GetPosY(sender) + 15;

		        setTimeout('showLayer("Menu",' + left + ',' + top + ');', 50);   
		    } 			


		</script>
	</HEAD>
	<body onload="CargarConfiguracion();document.Form1.txtNombre.focus();">
		<form id="Form1" method="post" runat="server">
			<table class="GG" id="Table1" cellSpacing="0" cellPadding="10" width="100%" align="center"
				border="0">
				<TR>
					<TD>
						<table class="tableBorder" id="Table2" cellSpacing="0" cellPadding="5" width="50%" align="center">
							<TR>
								<TD class="titleBackBlue" colSpan="4">Buscador de Usuarios (Empleados y 
									Beneficiarios)</TD>
							</TR>
							<TR>
								<TD colSpan="4">Realice la búsqueda por algunos de los siguientes criterios.</TD>
							</TR>
							<TR>
								<TD><STRONG>Nombre</STRONG></TD>
								<TD><STRONG><asp:textbox id="txtNombre" runat="server" CssClass="textBox" Width="180px"></asp:textbox>
										<asp:TextBox id="hdnLiquidacionValue" runat="server" Width="0px"></asp:TextBox><asp:TextBox id="hdnPermisos" runat="server" Width="0px"></asp:TextBox>
										<asp:TextBox id="hdnRealizaAutorizaciones" runat="server" Width="0px"></asp:TextBox>
										<asp:TextBox id="hdnRealizaReembolsos" runat="server" Width="0px"></asp:TextBox>
										<asp:TextBox id="hdnRealizaConsultas" runat="server" Width="0px"></asp:TextBox>
										<asp:TextBox id="hdnRealizaCitas" runat="server" Width="0px"></asp:TextBox></STRONG></TD>
							</TR>
							<tr>
								<TD></TD>
								<td></td>
							</tr>
							<TR>
								<TD><STRONG>Identificación</STRONG></TD>
								<TD><STRONG><asp:textbox id="txtIdentificacion" runat="server" CssClass="textBox" Width="180px"></asp:textbox><INPUT id="hdnId" type="hidden" value="???" name="hdnId"><INPUT id="hdnIdBene" type="hidden" value="??" name="hdnId"><INPUT id="hdnLiquidacion" type="hidden" value="XXX" name="hdnLiquidacion"></STRONG></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD colSpan="4">
									<P align="center"><asp:button id="Bfinder" runat="server" CssClass="button" 
                                            Text="Buscar" onclick="Bfinder_Click"></asp:button></P>
								</TD>
							</TR>
						</table>
					</TD>
				</TR>
				<TR>
					<TD></TD>
				</TR>
				<TR>
					<TD>Cantidad
						<asp:label id="Rcount" runat="server" Font-Bold="True">[Sin Búsqueda definida]</asp:label>
                        <asp:Button ID="btnReload" style="DISPLAY: none" runat="server" onclick="btnReload_Click" 
                            Text="Button" />
                    </TD>
				</TR>
				<tr>
					<td align="center"><asp:datagrid id="dtgUsuarios" runat="server" CssClass="grid" Width="99%" CellPadding="3" AllowPaging="True"
							AutoGenerateColumns="False" GridLines="Horizontal" PageSize="20">
							<AlternatingItemStyle CssClass="altItems"></AlternatingItemStyle>
							<ItemStyle CssClass="norItems"></ItemStyle>
							<HeaderStyle CssClass="headerGrid"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="id_empleado" HeaderText="IdEmpleado" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="beneficiario_id" HeaderText="IdBeneficiario" Visible="False"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Menú">
									<ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
									<ItemTemplate>
										<img src="../../images/icoMenu.gif" onclick='<%# "javascript:ShowMenu(this," + DataBinder.Eval(Container,"DataItem.id_empleado")+ "," + DataBinder.Eval(Container,"DataItem.beneficiario_id") + "," +  DataBinder.Eval(Container,"DataItem.estado") + ");" %>'>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn Visible="False" HeaderText="Cambiar">
									<ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lnkCambiarPaciente" runat="server" CommandName="SeleccionarPaciente">Seleccionar</asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="identificacion" HeaderText="Identificaci&#243;n">
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="nombre_completo" HeaderText="Nombre"></asp:BoundColumn>
								<asp:BoundColumn DataField="parentesco" HeaderText="Parentesco">
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="NombreEstado" HeaderText="Estado">
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="fecha_nacimiento" HeaderText="Fecha de Nacimiento" DataFormatString="{0:dd/MM/yyyy}">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>							
								<asp:TemplateColumn HeaderText="Sexo">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblSexo" runat="server" Text='<%# (Convert.ToInt32(DataBinder.Eval(Container, "DataItem.Sexo")) == 1 ? "M" : "F") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>				
								<asp:BoundColumn DataField="Privacidad" HeaderText="Privacidad">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>								
							</Columns>
							<PagerStyle HorizontalAlign="Center" BackColor="White" Mode="NumericPages"></PagerStyle>
						</asp:datagrid><BR>
						<BR>
						<BR>
						<BR>
						<BR>
						<BR>
						<BR>
						<BR>
						<BR>
						<BR>
						<BR>
						<BR>
						<BR>
						<BR>
						<BR>
						<BR>
						<BR>
					</td>
				</tr>
			</table>
			<iframe id="ifrMenu" style="DISPLAY: none; Z-INDEX: 1000; WIDTH: 200px; POSITION: absolute"
				frameBorder="0" scrolling="no" src="pagina.htm"></iframe>
			<div id="dvMenu" style="BORDER-RIGHT: dimgray 1px outset; BORDER-TOP: dimgray 1px outset; DISPLAY: none; Z-INDEX: 1100; OVERFLOW: auto; BORDER-LEFT: dimgray 1px outset; WIDTH: 200px; BORDER-BOTTOM: dimgray 1px outset; POSITION: absolute; BACKGROUND-COLOR: white">
				<table style="BORDER-RIGHT: gray 1px solid; BORDER-TOP: gray 1px solid; BORDER-LEFT: gray 1px solid; BORDER-BOTTOM: gray 1px solid; BORDER-COLLAPSE: collapse"
					cellSpacing="0" cellPadding="5" width="100%" border="1">
					<TBODY>
						<tr height="5">
							<td align="left"><asp:image id="Image1" style="CURSOR: pointer" onclick="closeLayer('Menu')" runat="server"
									ImageUrl="../../images/imgClose.gif"></asp:image></td>
						</tr>
						<tr height="10">
							<td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplConsultar" href="javascript:openPopUp('AE_employee.aspx?acc=UX&amp;employee_id=???',900,600);">Consultar 
									Empleado</A>
							</td>
						</tr>
						<tr height="10">
							<td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplBeneficiarios" href="javascript:openPopUp('LIS_beneficiarios.aspx?EMPLOYEE_ID=???',900,400);">Consultar 
									Beneficiarios</A>
							</td>
						</tr>
						<tr>
							<td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplSolicitudes" href="LIS_solicitudgastos.aspx?employee_id=???">Histórico 
									de Solicitudes</A>
							</td>
						</tr>
						<tr id="trAutorizacion" runat="server">
							<td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplAutorizacion" href="AE_solicitudautorizacion.aspx?employee_id=???&amp;beneficiario_id=??&amp;liquidacionConfirmacion=XXX">Solicitar 
									Autorización</A>
							</td>
						</tr>
						<tr id="trReembolso" runat="server">
							<td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplReembolso" href="AE_solicitudreembolso.aspx?employee_id=???&amp;beneficiario_id=??&amp;liquidacionConfirmacion=XXX">Solicitar 
									Reembolso</A>
							</td>
						</tr>
						<tr id="trConsulta" runat="server">
							<td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplConsulta" href="AE_registroconsulta.aspx?employee_id=???&amp;beneficiario_id=??">Registrar 
									Consulta</A>
							</td>
						</tr>						
						<tr id="trNuevaCita" runat="server">
							 <td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplNuevaCita" data-link="../../agenda/UI/RegistroCitas/ModuloRegistroCitas.aspx?idempleado=param_idempleado&amp;idbeneficiario=param_idbeneficiario">Registrar Cita</A>
							</td>
<%--                    <td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplNuevaCita" data-link="http://localhost:49800/UI/RegistroCitas/ModuloRegistroCitas.aspx?idempleado=param_idempleado&amp;idbeneficiario=param_idbeneficiario">Registrar Cita</A>
                        </td>--%>
						</tr>
						<tr id="trNuevaCitaUrgencia" style="display:none" runat="server">
							<td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplNuevaCitaUrgencia" data-link="../../agenda/UI/RegistroCitas/ModuloRegistroCitas.aspx?idempleado=param_idempleado&amp;idbeneficiario=param_idbeneficiario&amp;tipo=urgencia">Registrar Cita Prioritaria</A>
							</td>
						</tr>
						<tr height="10">
							<td align="left"><IMG src="../../images/imgBullet.gif" border="0">&nbsp;<A id="hplAvisoPrivacidad" href="javascript:openPopUp('AE_avisoPrivacidad.aspx?acc=UX&amp;employee_id=???',450,340);">Aviso de privacidad</A>
							</td>
						</tr>
						
						</TD></TR></TBODY></table>
			</div>
		</form>
	</body>
</HTML>
