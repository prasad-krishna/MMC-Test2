
<%@ Register TagPrefix="uc1" TagName="WC_DatosEmpleado" Src="../WebControls/WC_DatosEmpleado.ascx" %>
<%@ Page language="c#" Codebehind="AE_solicitudautorizacionresumen.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_empleado.forma.AE_solicitudautorizacionresumen" ValidateRequest="false" %>
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
	</HEAD>
	<body onload="CargarConfiguracion();" leftMargin="5" topMargin="5" rightMargin="5">
		<form id="Form1" method="post" runat="server">
			<table class="GG" cellSpacing="0" cellPadding="2" width="100%" align="center" border="0">
				<TR>
					<TD align="center" colSpan="2"><uc1:wc_datosempleado id="WC_DatosEmpleado1" runat="server"></uc1:wc_datosempleado>&nbsp;
					</TD>
				</TR>
				<TR>
					<TD align="center" colSpan="2">&nbsp;&nbsp;
						<FIELDSET class="FieldSet" style="WIDTH: 99%"><LEGEND><IMG src="../../images/icoSolicitud.gif" border="0">
								Solicitud</LEGEND><BR>
							<TABLE id="Table1" cellSpacing="0" cellPadding="2" width="99%" align="center">
								<TBODY>
									<TR>
										<TD>
											<TABLE id="tblDatosSolicitud" style="DISPLAY: none" cellSpacing="0" cellPadding="3" width="95%"
												align="center" runat="server">
												<TR>
													<TD class="titleBig" width="20%"></TD>
													<TD width="30%"><asp:label id="lblNoSolicitud" runat="server" CssClass="titleBig" Visible="False"></asp:label><A href="javascript:MostrarCalendario(Form1.txtFecha,Form1.txtFecha,'dd/mm/yyyy');"
															name="btnFecha"></A></TD>
													<TD width="20%">Fecha y Usuario Creación</TD>
													<TD width="35%">
														<P><asp:label id="lblFechaCreacion" runat="server" CssClass="LabelNoModify" Width="200px"></asp:label><BR>
															<asp:label id="lblUsuarioCreacion" runat="server" CssClass="LabelNoModify"></asp:label></P>
													</TD>
												</TR>
											</TABLE>
											<TABLE id="Table2" name="Table2" cellSpacing="0" cellPadding="1" width="95%" align="center">
												<TBODY>
													<TR>
														<TD>Fecha de Solicitud</TD>
														<TD width="30%"><asp:textbox id="txtFecha" runat="server" CssClass="LabelNoModify" Width="170px"></asp:textbox>&nbsp;<A href="javascript:MostrarCalendario(Form1.txtFecha,Form1.txtFecha,'dd/mm/yyyy');"
																name="btnFecha"></A></TD>
														<TD width="20%">Usuario</TD>
														<TD width="30%"><asp:textbox id="txtUsuario" runat="server" CssClass="LabelNoModify" Width="200px"></asp:textbox></TD>
													</TR>
													<TR>
														<TD>Servicio Solicitado Por</TD>
														<TD>&nbsp;<asp:textbox id="txtPrestador" runat="server" CssClass="LabelNoModify" Width="200px"></asp:textbox>
															<asp:label id="lblEspecialidad" runat="server" CssClass="textSmallBlue"></asp:label></TD>
										</TD>
										<TD>Mes Liquidación</TD>
										<TD><BR>
											<asp:textbox id="txtMes" runat="server" CssClass="LabelNoModify" Width="30px"></asp:textbox>&nbsp;
											<asp:textbox id="txtAno" runat="server" CssClass="LabelNoModify" Width="50px"></asp:textbox></TD>
					</TD>
					</TD></TR>
				<TR>
					<TD>Estado de la Solicitud</TD>
					<TD><asp:textbox id="txtEstadoSoli" runat="server" CssClass="LabelNoModify" Width="170px"></asp:textbox></TD>
					<TD></TD>
					<TD>
						<asp:textbox id="txtIdDiagnostico" runat="server" Width="0px"></asp:textbox><asp:textbox id="txtDiagnostico" runat="server" CssClass="LabelNoModify" Width="170px"
							Visible="False"></asp:textbox></TD>
				</TR>
				<TR>
					<TD>
						<P>Observaciones</P>
					</TD>
					<TD colSpan="3"><asp:label id="txtComentarioSolicitud" runat="server" CssClass="LabelNoModify"></asp:label></TD>
				</TR>
				<TR>
					<TD>Comentarios Empleado/<br />
                        Documentos Carta</TD>
					<TD colSpan="3">
						<asp:label id="txtDocumentos" runat="server" CssClass="LabelNoModify"></asp:label></TD>
				</TR>
				<TR>
					<TD>Anotaciones Fijas</TD>
					<TD colSpan="3">
						<asp:CheckBoxList id="chkAnotaciones" runat="server" Width="80%" Enabled="False" RepeatColumns="1"></asp:CheckBoxList></TD>
				</TR>
			</table>
			</TD></TR>
			<TR>
				<TD align="center"><asp:datalist id="dtlTipoServicio" runat="server" Width="100%" CellPadding="8">
						<ItemTemplate>
							<div id="divDataList" runat="server">
								<asp:TextBox id="txtConsecutivoNombre" runat="server" CssClass="textBigGreen" text='<%# DataBinder.Eval(Container, "DataItem.ConsecutivoTipoServicioNombre") %>'>
								</asp:TextBox>
								<TABLE class="tableBorder" id="Table4" cellSpacing="0" cellPadding="8" width="100%" border="0">
									<TR class="headerGrid">
										<TD>Tipo de Servicio</TD>
										<TD>Diagnosticos</TD>
										<TD>Prestadores</TD>
										<TD>Atención</TD>
									</TR>
									<TR vAlign="top">
										<TD>
											<asp:Label id="lblTipoServicio" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.NombreTipoServicio") %>'>
											</asp:Label>
											<asp:Label id="lblIdTipoServicio" Visible="false" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.IdTipoServicio") %>'>
											</asp:Label>
										<td>
											<asp:Label id="lblDiagnosticos" CssClass="textSmallBlack" runat="server"></asp:Label>
										</td>
										<TD>
											<asp:Label id="lblProveedor" runat="server" CssClass="textSmallBlack"></asp:Label>
										</TD>
										<TD>
											Tipo Atención:&nbsp;
											<asp:Label id="lblTipoAtencion" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.NombreTipoAtencion") %>'>
												<br />
											</asp:Label><br />
											Clase Atención:&nbsp;
											<asp:Label id="lblClaseAtencion" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.NombreClaseAtencion") %>'>
												<br />
											</asp:Label><br />
											Contigencia:&nbsp;
											<asp:Label id="lblContingencia" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.NombreContingencia") %>'>
											</asp:Label></TD>
									</TR>
									<tr>
										<td colspan="4">
											<asp:Label id="lblPrestadorTipoServicio" runat="server" text='<%# !Convert.IsDBNull(DataBinder.Eval(Container, "DataItem.NombrePrestador")) ? "Solicitante: " + DataBinder.Eval(Container, "DataItem.NombrePrestador") : "" %>'>
											</asp:Label>
										</td>
									</tr>
								</TABLE>
								<asp:DataGrid id="dtgProductoServicio" runat="server" Width="100%" CssClass="grid" CellPadding="0"
									AllowPaging="False" AutoGenerateColumns="False" GridLines="Horizontal" OnItemDataBound="dtgProductoServicio_ItemDataBound">
									<AlternatingItemStyle CssClass="altItems"></AlternatingItemStyle>
									<ItemStyle CssClass="norItems"></ItemStyle>
									<HeaderStyle CssClass="headerGrid"></HeaderStyle>
									<Columns>
										<asp:TemplateColumn HeaderText="Servicio/Producto">
											<ItemTemplate>
												<asp:Label runat="server" ID="lblProductoServicio" text='<%# DataBinder.Eval(Container, "DataItem.NombreCompletoMedicamento").ToString() + DataBinder.Eval(Container, "DataItem.NombreCompletoServicio").ToString() %>'>
												</asp:Label>
											</ItemTemplate>
											<ItemStyle Width="190px"></ItemStyle>
										</asp:TemplateColumn>
										<asp:TemplateColumn HeaderText="Sesio./Cant.">
											<ItemTemplate>
												<asp:Label text='<%# DataBinder.Eval(Container, "DataItem.Cantidad") %>' runat="server" ID="lblCantidad" />
												<br />
												<asp:Label id="lblDosis" Visible="false" text='<%# "Dosis:" + DataBinder.Eval(Container, "DataItem.Dosis").ToString() %>' runat="server">
												</asp:Label>
											</ItemTemplate>
											<ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
										</asp:TemplateColumn>
										<asp:TemplateColumn HeaderText="Fecha Prestación">
											<ItemTemplate>
												<asp:Label runat="server" text='<%# string.Format("{0:dd/MM/yyyy}",DataBinder.Eval(Container, "DataItem.FechaPrestacion")) %>' ID="lblFechaPrestacion" >
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn HeaderText="Valor Solicitado">
											<ItemTemplate>
												<asp:Label runat="server" text='<%# string.Format("{0:0,0}",DataBinder.Eval(Container, "DataItem.ValorConvenioSolicitado")) %>' ID="lblValorConvenioSolicitado" >
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn HeaderText="Estado">
											<ItemTemplate>
												<asp:Label id="lblEstadoServicio" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.NombreSolicitudEstado") %>' >
												</asp:Label><br />
												<asp:Label runat="server" text='<%# "Aprobado $ " + string.Format("{0:0,0}",DataBinder.Eval(Container, "DataItem.ValorAprobado")) %>' ID="lblValorAprobado" Visible="false">
												</asp:Label>
												<br />
												<asp:Label runat="server" text='<%# "Descuento % " + string.Format("{0:#,##}",DataBinder.Eval(Container, "DataItem.Descuento")) %>' ID="lblDescuento" Visible="false">
												</asp:Label>
												<br />
												<asp:Label runat="server" text='<%# "UVR " + string.Format("{0:0,0}",DataBinder.Eval(Container, "DataItem.UVR")) %>' ID="lblUVR" Visible="false">
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn HeaderText="Motivo">
											<ItemTemplate>
												<asp:Label id="lblMotivo" text='<%# DataBinder.Eval(Container, "DataItem.NombreSolicitudMotivo") %>' runat="server">
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn HeaderText="Comentario">
											<ItemTemplate>
												<asp:Label id="lblComentarioServicioProducto" text='<%# DataBinder.Eval(Container, "DataItem.Comentarios") %>' runat="server">
												</asp:Label>
											</ItemTemplate>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
										</asp:TemplateColumn>
									</Columns>
								</asp:DataGrid>
								<TABLE class="tableBorder" id="Table4" cellSpacing="0" cellPadding="3" width="100%" border="0">
									<TR>
										<TD width="30%">
											Observaciones<BR>
											<span class="textSmallBlack">(Estas observaciones se despliegan en el formato de 
												este tipo de servicio)</span>
										</TD>
										<TD width="70%">
											<asp:TextBox MaxLength="500" id="txtComentariosTipoServicio" CssClass="LabelNoModify" runat="server" Width="300px" text='<%# DataBinder.Eval(Container, "DataItem.Comentarios") %>'>
											</asp:TextBox>
										</TD>
									</TR>
									<TR id="trComentariosAnulacion" runat="server" style="display:none">
										<TD width="30%">Comentarios Anulación
										</TD>
										<TD width="70%">
											<asp:TextBox id="txtComentariosAnulacion" runat="server" CssClass="LabelNoModify" Width="300px"
												MaxLength="500"></asp:TextBox></TD>
									</TR>
								</TABLE>
							</div>
						</ItemTemplate>
					</asp:datalist></TD>
			</TR>
			<TR>
				<TD align="right">
					<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="300" border="0">
						<TR>
							<TD width="275">
								<TABLE class="tableBorder" id="Table5" cellSpacing="0" cellPadding="5" width="275">
									<TR class="tableBorder">
										<TD>Total Solicitado</TD>
										<TD><asp:label id="lblTotalProductosServicios" runat="server"></asp:label></TD>
									</TR>
									<TR>
										<TD>Total Aprobado</TD>
										<TD><asp:label id="lblTotalAprobado" runat="server"></asp:label></TD>
									</TR>
									<TR>
										<TD>Total Facturas</TD>
										<TD><asp:label id="lblTotalFacturas" runat="server"></asp:label></TD>
									</TR>
								</TABLE>
							</TD>
						</TR>
					</TABLE>
				</TD>
			</TR>
			</TBODY></TABLE></FIELDSET> </TD></TR>
			<TR>
				<TD align="center" colSpan="2">
					<FIELDSET class="FieldSet" style="WIDTH: 95%" id="fldFormatos" runat="server"><LEGEND><IMG src="../../iconos/icoPrint.gif" border="0">&nbsp;Formatos</LEGEND><BR>
						<TABLE id="Table7" cellSpacing="0" cellPadding="5" width="80%" align="center">
							<TR>
								<TD>
									<TABLE class="tableAllBorder" id="Table8" cellSpacing="0" width="95%" border="0">
										<TR>
											<TD width="70%">Pantalla Resumen</TD>
											<TD>
												<asp:HyperLink id="hplFormato" runat="server">Imprimir</asp:HyperLink></TD>
										</TR>
										<TR>
											<TD>Resumen Solicitud</TD>
											<TD>
												<asp:HyperLink id="hplResumen" runat="server">Imprimir</asp:HyperLink></TD>
										</TR>
									</TABLE>
									<asp:datalist id="dtlFormatos" runat="server" Width="95%" CellPadding="0">
										<ItemTemplate>
											<TABLE class="tableAllBorder" id="Table3" cellSpacing="0" cellPadding="8" width="100%"
												align="center">
												<TR>
													<TD width="70%">Orden de
														<asp:Label id=lblTipoServicioFormato runat="server" text='<%# DataBinder.Eval(Container, "DataItem.NombreTipoServicio").ToString() + " - " + DataBinder.Eval(Container, "DataItem.NombreProveedor").ToString() %>'>
														</asp:Label>
														<asp:Label id=lblIdTipoServicioFormato runat="server" Visible="false" text='<%# DataBinder.Eval(Container, "DataItem.IdTipoServicio") %>'>
														</asp:Label>
														<asp:Label id=lblIdProveedor runat="server" Visible="false" text='<%# DataBinder.Eval(Container, "DataItem.IdProveedor") %>'>
														</asp:Label>
														<asp:Label id=lblUrlFormato runat="server" Visible="false" text='<%# DataBinder.Eval(Container, "DataItem.URLFormato") %>'>
														</asp:Label></TD>
													<TD>
														<asp:linkbutton id=lnkImprimirFormato runat="server" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.IdSolicitudTipoServicio") %>' CommandName="Imprimir" ForeColor='<%#  !Convert.IsDBNull(DataBinder.Eval(Container, "DataItem.Impresiones")) && Convert.ToInt16(DataBinder.Eval(Container, "DataItem.Impresiones")) > 0 ? System.Drawing.Color.Purple : System.Drawing.Color.FromName("#002E63")%>'>Imprimir</asp:linkbutton>&nbsp;
														<asp:Label id="lblImpresiones" runat="server" CssClass="textSmallBlack" Text='<%#!Convert.IsDBNull(DataBinder.Eval(Container, "DataItem.Impresiones")) ? "(" +  DataBinder.Eval(Container, "DataItem.Impresiones") + " impresiones)" : ""  %>'>
														</asp:Label><BR>
														<asp:linkbutton id="linkExportarFormato" runat="server" Visible="false">Exportar Excel</asp:linkbutton></TD>
												</TR>
											</TABLE>
										</ItemTemplate>
									</asp:datalist></TD>
							</TR>
						</TABLE>
					</FIELDSET>
				</TD>
			</TR>
			<TR>
				<TD colSpan="2">
					<P align="center"><asp:button id="btnFinalizar" runat="server" CssClass="button" Text="Finalizar"></asp:button></P>
				</TD>
			</TR>
			<TR>
				<TD colSpan="2">
					<P align="right">&nbsp;</P>
				</TD>
			</TR>
			<TR>
				<TD align="center" colSpan="2"></TD>
			</TR>
			</TBODY></TABLE></form>
	</body>
</HTML>
