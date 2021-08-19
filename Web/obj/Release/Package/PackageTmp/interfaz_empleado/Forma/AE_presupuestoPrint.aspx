<%@ Page language="c#" Codebehind="AE_presupuestoPrint.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_admon.forma.AE_presupuestoPrint" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>HC-Historias Clínicas</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../css/admon.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<asp:datagrid id="dtgPresupuesto" runat="server" Width="98%" AutoGenerateColumns="False" CellPadding="6"
				PageSize="12" CssClass="grid" GridLines="Horizontal">
				<AlternatingItemStyle CssClass="altItems"></AlternatingItemStyle>
				<ItemStyle CssClass="norItems"></ItemStyle>
				<HeaderStyle CssClass="headerGrid"></HeaderStyle>
				<Columns>
					<asp:BoundColumn DataField="NombreTipoProceso" HeaderText="Tipo de Proceso"></asp:BoundColumn>
					<asp:TemplateColumn HeaderText="Periodo">
						<ItemTemplate>
							<asp:Label ID="Label1" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.FechaInicio")).ToShortDateString()  + "-" +  Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.FechaFin")).ToShortDateString()%>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="Presupuesto" HeaderText="Presupuesto" DataFormatString="{0:$ #,##0;($#,##0)}"></asp:BoundColumn>
					<asp:BoundColumn DataField="Utilizado" HeaderText="Utilizado" DataFormatString="{0:$ #,##0;($#,##0)}"></asp:BoundColumn>
					<asp:TemplateColumn HeaderText="Disponible">
						<ItemTemplate>
							<asp:Label runat="server" Text='<%# string.Format("{0:$ #,##0;($#,##0)}",Convert.ToDecimal(DataBinder.Eval(Container, "DataItem.Presupuesto")) -  Convert.ToDecimal(DataBinder.Eval(Container, "DataItem.Utilizado")))%>' ID="lblUtilizado">
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Pemite Exceso">
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:Label runat="server" Text='<%# (Convert.ToBoolean(DataBinder.Eval(Container, "DataItem.IngresoConExceso")) == true ? "Si":"No") %>' ID="lblExceso">
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="ExcesoPresupuesto" HeaderText="Exceso" DataFormatString="{0:$ #,##0;($#,##0)}"></asp:BoundColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Center" BackColor="White" Mode="NumericPages"></PagerStyle>
			</asp:datagrid></form>
	</body>
</HTML>
