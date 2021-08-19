<%@ Register TagPrefix="uc1" TagName="WC_Pie" Src="../WebControls/WC_Pie.ascx" %>
<%@ Page language="c#" Codebehind="LIS_beneficiarios.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_empleado.forma.LIS_beneficiarios" %>
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
			function openDependent(employee_id,dependent_id,acc) 
			{ 
				var params = "dependent=yes,directories=no,hotkeys=no,menubar=no,personalbar=no,scrollbars=yes,status=yes,titlebar=yes,toolbar=no,width=800,height=400,left=200,top=250";
				fullurl = "AE_beneficiario.aspx?employee_id="+employee_id+"&id="+dependent_id+"&acc="+acc;
				wcontainer2  = open ( fullurl, "beneficiario"+dependent_id, params);
			}
			
						
			function borrarEmployee(employee_id) 
			{ 
				var params = "dependent=yes,directories=no,hotkeys=no,menubar=no,personalbar=no,scrollbars=yes,status=yes,titlebar=yes,toolbar=no,width=320,height=140,left=200,top=250";
				fullurl = "AE_borrar.aspx?ACC=BEN&id="+employee_id;
				wcontainer2  = open ( fullurl, "delete"+employee_id, params);
			}



		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD height="10"></TD>
				</TR>
				<TR>
					<TD>
						<table class="tableBorder" cellSpacing="0" cellPadding="10" width="99%" align="center">
							<TR bgColor="#0c2234">
								<TD class="titleBackBlue" colSpan="4">Datos de los Beneficiarios</TD>
							</TR>
							<TR>
								<TD colSpan="4"></TD>
							</TR>
							<TR>
								<TD colSpan="4">
									<P align="center">
										<asp:datagrid id="dtgBeneficiarios" runat="server" PageSize="30" Width="100%" CellPadding="4"
											AutoGenerateColumns="False" CssClass="grid">
											<AlternatingItemStyle CssClass="altItems"></AlternatingItemStyle>
											<ItemStyle CssClass="norItems"></ItemStyle>
											<HeaderStyle CssClass="headerGrid"></HeaderStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="beneficiario_id"></asp:BoundColumn>
												<asp:BoundColumn DataField="identificacion" HeaderText="Identificaci&#243;n">
													<ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="nombre" HeaderText="Nombre">
													<ItemStyle Width="25%"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Sexo">
													<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblSexo" runat="server" Text='<%# (Convert.ToInt32(DataBinder.Eval(Container, "DataItem.Genero")) == 1 ? "M" : "F") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="edad" HeaderText="Edad">
													<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Parentesco" HeaderText="Parentesco">
													<ItemStyle Width="10%"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="FechaIngresoPlan" HeaderText="Ingreso Plan" DataFormatString="{0:dd/MM/yyyy} ">
													<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Preexistencias" HeaderText="Preexistencias">
													<ItemStyle Width="20%"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Detalle">
													<ItemStyle HorizontalAlign="Center" Width="20%"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton CausesValidation="false" CommandArgument="IdSolicitud" CommandName="Historico" id="lnkVer"
															runat="server">Histórico Solicitudes</asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></P>
								</TD>
							</TR>
						</table>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
