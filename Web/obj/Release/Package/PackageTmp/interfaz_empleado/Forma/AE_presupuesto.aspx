<%@ Register TagPrefix="uc1" TagName="WC_Report" Src="../WebControls/WC_Report.ascx" %>
<%@ Page language="c#" Codebehind="AE_presupuesto.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_admon.forma.AE_presupuesto" %>
<%@ Register TagPrefix="uc1" TagName="WC_Encabezado" Src="../WebControls/WC_Encabezado.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="../WebControls/WC_Menu.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_Pie" Src="../WebControls/WC_Pie.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>HC-Historias Clínicas</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../css/admon.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../../scripts/Base.js" type="text/javascript"></script>
	</HEAD>
	<body onload="CargarConfiguracion()">
		<form id="Form1" method="post" runat="server">
			<table class="GG" cellSpacing="0" cellPadding="10" align="center" border="0" id="Table1"
				width="100%">
				<TR>
					<TD>
						<uc1:WC_Report id="WC_Report1" runat="server"></uc1:WC_Report></TD>
				</TR>
				<tr>
					<td align="center">
						<asp:datagrid id="dtgPresupuesto" runat="server" CellPadding="6" AutoGenerateColumns="False" Width="100%"
							GridLines="Horizontal" CssClass="grid" AllowPaging="True" PageSize="20">
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
										<asp:Label runat="server" Text='<%# string.Format("{0:$ #,##0;($#,##0)}",Convert.ToDecimal(Convert.IsDBNull(DataBinder.Eval(Container, "DataItem.Presupuesto"))? 0 : DataBinder.Eval(Container, "DataItem.Presupuesto")) -  Convert.ToDecimal(Convert.IsDBNull(DataBinder.Eval(Container, "DataItem.Utilizado")) ? 0 : DataBinder.Eval(Container, "DataItem.Utilizado")))%>' ID="lblUtilizado">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Pemite Exceso">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox runat="server" ID="chkPermiteExceso" Checked='<%# DataBinder.Eval(Container, "DataItem.IngresoConExceso")%>'>
										</asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="ExcesoPresupuesto" HeaderText="Exceso" DataFormatString="{0:$ #,##0;($#,##0)}"></asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Center" BackColor="White" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
