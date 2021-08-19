<%@ Control Language="c#" AutoEventWireup="false" Codebehind="WC_Report.ascx.cs" Inherits="WebMedicamentos.interfaz_empleado.WebControls.WC_Report" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" border="0" width="100%">
	<TR>
		<TD width="2%"></TD>
		<TD>
			<asp:ImageButton id="imbImprimir" ImageUrl="../../iconos/icoPrint.gif" AlternateText="Imprimir" runat="server"></asp:ImageButton>
			<asp:LinkButton id="lnkImprimir" runat="server">Imprimir</asp:LinkButton>&nbsp;</TD>
		<TD>
			<asp:ImageButton id="imbExportar" ImageUrl="../../iconos/icoExportar.gif" AlternateText="Imprimir"
				runat="server"></asp:ImageButton>&nbsp;
			<asp:LinkButton id="lnkExportar" runat="server">Exportar</asp:LinkButton></TD>
		<TD width="75%"></TD>
	</TR>
</TABLE>
