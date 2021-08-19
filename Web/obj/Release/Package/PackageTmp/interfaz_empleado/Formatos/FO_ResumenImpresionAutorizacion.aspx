<%@ Register TagPrefix="uc1" TagName="WC_PieFormato" Src="../WebControls/WC_PieFormato.ascx" %>
<%@ Page language="c#" Codebehind="FO_ResumenImpresionAutorizacion.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_empleado.formatos.FO_ResumenImpresionAutorizacion" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>HC-Historias Clínicas</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../css/admon.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout" bottomMargin="4" topMargin="2">
		<form id="Form1" method="post" runat="server">
			<TABLE class="tableFormato" id="Table1" width="640" cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<TD class="titleFormato">
						<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="titleFormato" align="center">
									SOLICITUD</TD>
								<TD vAlign="middle" width="20">
								</TD>
								<TD width="20">
									<asp:Image id="imgLogo" runat="server"></asp:Image></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD height="5">
						<TABLE class="tableBorderFormato" id="tblOrden" height="40" cellSpacing="0" cellPadding="0"
							width="100%" border="0">
							<TR>
								<TD align="left" width="15%"><STRONG>FECHA CREACIÓN</STRONG></TD>
								<TD width="13%">
									<asp:label id="lblFecha" runat="server" CssClass="lblFormato"></asp:label></TD>
								<TD align="right" width="91" style="WIDTH: 91px">EXPEDIDA POR</TD>
								<TD width="14%">
									<asp:label id="lblExpedidaPor" runat="server" CssClass="lblFormato"></asp:label></TD>
								<TD align="right" width="15%">
									ESTADO</TD>
								<TD width="27%">
									<asp:label id="lblEstado" runat="server" CssClass="lblFormato"></asp:label></TD>
							</TR>
							<TR>
								<TD align="left">FORMA DE PAGO</TD>
								<TD>
									<asp:label id="lblFormaPago" runat="server" CssClass="lblFormato"></asp:label></TD>
								<TD align="right" style="WIDTH: 91px">MES LIQUIDACIÓN</TD>
								<TD>
									<asp:label id="lblMesLiquidacion" runat="server" CssClass="lblFormato"></asp:label></TD>
								<TD align="right"></TD>
								<TD></TD>
							</TR>
						</TABLE>
						<TABLE class="tableBorderFormato" id="Table4" cellSpacing="0" cellPadding="0" width="100%"
							border="0">
							<TR>
								<TD align="center" width="50%" class="headerGridFormatoGris" colSpan="4" style="BORDER-RIGHT: #000000 1px solid; BORDER-LEFT-STYLE: none"><STRONG>DATOS 
										DEL PACIENTE</STRONG></TD>
								<TD align="center" width="50%" class="headerGridFormatoGris" colSpan="4" style="BORDER-RIGHT: #000000 1px solid; BORDER-LEFT-STYLE: none"><STRONG>DATOS 
										DEL EMPLEADO</STRONG></TD>
							</TR>
							<TR>
								<TD align="left" width="18%">NOMBRE</TD>
								<TD width="25%" colSpan="3" style="BORDER-RIGHT: #000000 1px solid; BORDER-LEFT-STYLE: none">
									<asp:label id="lblNombrePaciente" runat="server" CssClass="lblFormato"></asp:label></TD>
								<TD align="left" width="18%">NOMBRE&nbsp;EMPLEADO</TD>
								<TD width="35%" colSpan="3">
									<asp:label id="lblNombreEmpleado" runat="server" CssClass="lblFormato"></asp:label></TD>
							</TR>
							<TR>
								<TD align="left" width="18%">IDENTIFICACIÓN</TD>
								<TD width="25%" colSpan="3" style="BORDER-RIGHT: #000000 1px solid; BORDER-LEFT-STYLE: none">
									<asp:label id="lblTipoIdentificacion" runat="server" CssClass="lblFormato"></asp:label>&nbsp;
									<asp:label id="lblNumero" runat="server" CssClass="lblFormato"></asp:label></TD>
								<TD align="left" width="18%">IDENTIFICACIÓN</TD>
								<TD width="35%" colSpan="3">
									<asp:label id="lblTipoIdentificacionEmpleado" runat="server" CssClass="lblFormato"></asp:label>&nbsp;
									<asp:label id="lblNumeroEmpleado" runat="server" CssClass="lblFormato"></asp:label></TD>
							</TR>
							<TR>
								<TD align="left" width="18%"></TD>
								<TD style="BORDER-RIGHT: #000000 1px solid; BORDER-LEFT-STYLE: none" width="25%" colSpan="3"></TD>
								<TD align="left" width="18%">CUPO</TD>
								<TD width="10%">
									<asp:label id="lblCupo" runat="server" CssClass="lblFormato"></asp:label></TD>
								<TD width="15%">&nbsp;DISPONIBLE</TD>
								<TD width="10%">
									<asp:label id="lblDisponible" runat="server" CssClass="lblFormato"></asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<asp:datalist id="dtlTipoServicio" runat="server" Width="100%" CellPadding="0">
							<ItemTemplate>
								<TABLE class="tableBorderFormato" id="Table4" cellSpacing="0" cellPadding="3" width="100%"
									border="0">
									<TR class="headerGridFormatoGris">
										<TD>No. Solicitud</TD>
										<TD>Tipo de Servicio</TD>
										<TD>Prestadores</TD>
										<TD>Diagnosticos</TD>
									</TR>
									<TR vAlign="top">
										<TD>
											<asp:Label id="lblNumTipoServicio" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ConsecutivoNombre") %>'>
											</asp:Label>
										</TD>
										<TD>
											<asp:Label id=lblTipoServicio runat="server" text='<%# DataBinder.Eval(Container, "DataItem.NombreTipoServicio") %>'>
											</asp:Label>
											<asp:Label id=lblIdTipoServicio runat="server" Width="0" text='<%# DataBinder.Eval(Container, "DataItem.IdTipoServicio") %>' Visible="false">
											</asp:Label>
										</TD>
										<TD>
											<asp:Label id="lblProveedor" runat="server" CssClass="textSmallBlack"></asp:Label></TD>
										<TD>
											<asp:Label id="lblDiagnosticos" runat="server" CssClass="textSmallBlack"></asp:Label></TD>
									</TR>
								</TABLE>
								<asp:DataGrid id="dtgProductoServicio" runat="server" CssClass="gridFormato" Width="100%" CellPadding="0"
									CellSpacing="0" AllowPaging="False" AutoGenerateColumns="False" GridLines="Horizontal" OnItemDataBound="dtgProductoServicio_ItemDataBound">
									<AlternatingItemStyle CssClass="altItemsFormato"></AlternatingItemStyle>
									<ItemStyle CssClass="norItemsFormato"></ItemStyle>
									<HeaderStyle CssClass="headerGridFormatoGris"></HeaderStyle>
									<Columns>
										<asp:TemplateColumn HeaderText="Servicio/Producto">
											<ItemTemplate>
												<asp:Label runat="server" ID="lblProductoServicio" text='<%# DataBinder.Eval(Container, "DataItem.NombreCompletoMedicamento").ToString() + DataBinder.Eval(Container, "DataItem.NombreCompletoServicio").ToString() %>'>
												</asp:Label>
											</ItemTemplate>
											<ItemStyle Width="250px"></ItemStyle>
										</asp:TemplateColumn>
										<asp:TemplateColumn HeaderText="Sesio./Cant.">
											<ItemTemplate>
												<asp:Label text='<%# DataBinder.Eval(Container, "DataItem.Cantidad") %>' ID="lblCantidad" Runat="server" >
												</asp:Label>
												<br />
												<asp:Label id="lblDosis" Visible="false" text='<%# "Dosis:" + DataBinder.Eval(Container, "DataItem.Dosis").ToString() %>' runat="server">
												</asp:Label>
											</ItemTemplate>
											<ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
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
										<asp:TemplateColumn HeaderText="Valor Aprobado">
											<ItemTemplate>
												<asp:Label runat="server" text='<%# string.Format("{0:0,0}",DataBinder.Eval(Container, "DataItem.ValorAprobado")) %>' ID="lblValorAprobado" >
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn HeaderText="Estado">
											<ItemTemplate>
												<asp:Label id="lblEstadoServicio" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.NombreSolicitudEstado") %>' >
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn HeaderText="Motivo">
											<ItemTemplate>
												<asp:Label id="lblMotivo" text='<%# DataBinder.Eval(Container, "DataItem.NombreSolicitudMotivo") %>' runat="server">
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
									</Columns>
								</asp:DataGrid>
								<TABLE class="tableNoBorderFormato" id="Table3" cellSpacing="0" cellPadding="6" width="100%"
									border="0">
									<TR>
										<TD align="right" width="59%">TOTALES&nbsp;&nbsp;&nbsp;
										</TD>
										<TD width="13%">&nbsp;
											<asp:Label id="lblTotalSolicitado" runat="server">0</asp:Label></TD>
										<TD>
											<asp:Label id="lblTotalAprobado" runat="server">0</asp:Label></TD>
									</TR>
								</TABLE>
							</ItemTemplate>
						</asp:datalist></TD>
				</TR>
				<TR>
					<TD height="8">
						<TABLE class="tableBorderFormato" id="Table14" height="40" cellSpacing="0" cellPadding="0"
							width="100%" border="0">
							<TR>
								<TD vAlign="top" align="center" width="10%" colSpan="6">
									<P align="left"><STRONG>OBSERVACIONES</STRONG>&nbsp;
										<asp:label id="lblObservaciones" runat="server" CssClass="lblFormato"></asp:label><BR>
									</P>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD height="8">
						<uc1:WC_PieFormato id="WC_PieFormato1" runat="server"></uc1:WC_PieFormato></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
