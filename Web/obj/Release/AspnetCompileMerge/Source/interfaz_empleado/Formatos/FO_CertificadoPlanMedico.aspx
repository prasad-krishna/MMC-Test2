<%@ Page language="c#" Codebehind="FO_CertificadoPlanMedico.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_empleado.forma.FO_CertificadoPlanMedico" %>
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
					<TD></TD>
				</TR>
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="0" cellPadding="10" width="100%" border="0" class="tableCarta">
							<TR>
								<TD>
									<P>&nbsp;</P>
            <P><BR><BR>&nbsp;</P>
									<P align="center"><span style="FONT-SIZE: 16pt">BP EXPLORATION COMPANY COLOMBIA LIMITED</span></P><br>
									<P align="center"><STRONG>CERTIFICA QUE</STRONG>
									</P>
            <P align=center>&nbsp;</P>
									<P align="justify"> El (La) Sr(a).
										<asp:Label id="lblNombre" runat="server"></asp:Label>, identificado(a) con 
										documento de identidad&nbsp;
										<asp:Label id="lblTipoDocumento" runat="server"></asp:Label>&nbsp;No.&nbsp;
										<asp:Label id="lblDocumento" runat="server"></asp:Label>
										,&nbsp;ingresó al Beneficio de Salud de BP Exploration a partir del
										<asp:Label id="lblFechaIngreso" runat="server"></asp:Label>, 
            con los siguientes beneficiarios:<BR><BR></P>
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
								<asp:BoundColumn DataField="nombre" HeaderText="Nombre" ItemStyle-Width="25%"></asp:BoundColumn>
								<asp:BoundColumn DataField="identificacion" HeaderText="Identificación" ItemStyle-Width="15%"></asp:BoundColumn>
								<asp:BoundColumn DataField="Parentesco" HeaderText="Parentesco" ItemStyle-Width="10%"></asp:BoundColumn>
								<asp:BoundColumn DataField="FechaIngresoPlan" HeaderText="Vinculado Desde" ItemStyle-Width="10%" DataFormatString="{0:dd/MM/yyyy} "></asp:BoundColumn>
								<asp:BoundColumn DataField="FechaRetiroPlan" HeaderText="Vinculado Hasta" ItemStyle-Width="10%" DataFormatString="{0:dd/MM/yyyy} "></asp:BoundColumn>
								<asp:BoundColumn DataField="Preexistencias" HeaderText="Preexistencias" ItemStyle-Width="30%"></asp:BoundColumn>
							</Columns>
						</asp:DataGrid></TD>
				</TR>
				<TR>
					<TD>
						<TABLE class="tableCarta" id="Table3" cellSpacing="0" cellPadding="8" width="100%" border="0">
							<TR>
								<TD>
									<P><BR>
										Forma parte de las coberturas de Beneficio de Salud la atención de emergencias en 
										ciudades donde no hay red nacional e internacional, bajo la modalidad de 
										reembolso.
									</P>
									<P>Se expide a solicitud del interesado para los trámites respectivos donde se 
										requiera demostración de cobertura de servicios de salud o pertenencia al Beneficio.
									</P>
									<P>Esta certificación tiene validez de treinta (30) días a partir de la fecha de 
										expedición.</P>
									<P><BR>Bogotá D.C.,
										<asp:Label id="lblFecha" runat="server"></asp:Label></P>
									<P><BR>Cordialmente,</P>
									<P>&nbsp;</P>
									<P>______________________________________<BR><STRONG>División Médica BP</STRONG></P>
									<P>&nbsp;</P>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR></TR></TABLE>
		</form>
	</body>
</HTML>
