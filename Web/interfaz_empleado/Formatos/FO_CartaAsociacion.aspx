<%@ Page language="c#" Codebehind="FO_CartaAsociacion.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_empleado.forma.FO_CartaAsociacion" %>
<%@ Register TagPrefix="uc1" TagName="WC_EncabezadoMercer" Src="../WebControls/WC_EncabezadoMercer.ascx" %>
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
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE class="tableFormato" id="Table1" cellSpacing="0" cellPadding="0" width="640" border="0">
				<TR>
					<TD>
						<uc1:WC_EncabezadoMercer id="WC_EncabezadoMercer1" runat="server"></uc1:WC_EncabezadoMercer></TD>
				</TR>
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="0" cellPadding="10" width="100%" border="0" class="tableCarta">
							<TR>
								<TD>
									<P>&nbsp;</P>
									<P>
										UE/
										<asp:Label id="lblAbreviacionEmpresa" runat="server" CssClass="tableCarta"></asp:Label>
										.
										<asp:Label id="lblConsecutivoNombre" runat="server" CssClass="tableCarta"></asp:Label></P>
									<P>Bogotá,
										<asp:Label id="lblFecha" runat="server"></asp:Label></P>
									<P>
										<asp:Label id="lblTexto" runat="server"></asp:Label></P>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<asp:DataGrid id="dtgDetalle" runat="server" CssClass="gridFormatoSmall" Width="100%" CellPadding="0"
							AllowPaging="False" AutoGenerateColumns="False" GridLines="Horizontal">
							<AlternatingItemStyle CssClass="norItemsFormatoSmall"></AlternatingItemStyle>
							<ItemStyle CssClass="norItemsFormatoSmall"></ItemStyle>
							<HeaderStyle CssClass="headerGridFormatoSmall"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="beneficiario_id"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="id_empleado" HeaderText="Empleado"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="IdSolicitud"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="C&#233;dula Titular">
									<ItemStyle Width="10%"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblCedulaEmpleado" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Nombre Titular">
									<ItemStyle Width="17%"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblEmpleado" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Paciente">
									<ItemStyle Width="20%"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblUsuario" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="ValorTotalConvenioSolicitado" HeaderText="Valor" DataFormatString="{0:$ #,##0;($#,##0)}">
									<ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Documentos" HeaderText="Documentos">
									<ItemStyle Width="15%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Observaciones" HeaderText="Observaciones">
									<ItemStyle Width="18%"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
						</asp:DataGrid></TD>
				</TR>
				<TR>
					<TD>
						<TABLE class="tableCarta" id="Table3" cellSpacing="0" cellPadding="8" width="100%" border="0">
							<TR>
								<TD>
									<P>
										<asp:Label id="lblFirma" runat="server"></asp:Label></P>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				</TR></TABLE>
		</form>
	</body>
</HTML>
