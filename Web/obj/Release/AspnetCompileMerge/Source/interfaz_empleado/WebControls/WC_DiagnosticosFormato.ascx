<%@ Control Language="c#" AutoEventWireup="false" Codebehind="WC_DiagnosticosFormato.ascx.cs" Inherits="TPA.interfaz_empleado.WebControls.WC_DiagnosticosFormato" %>
<asp:DataGrid id="dtgDiagnosticos" Width="100%" CellPadding="1" AllowPaging="False" AutoGenerateColumns="False"
	GridLines="Horizontal" CssClass="gridFormato" runat="server">
	<AlternatingItemStyle CssClass="altItemsFormato"></AlternatingItemStyle>
	<ItemStyle CssClass="norItemsFormato"></ItemStyle>
	<HeaderStyle CssClass="headerGridFormato"></HeaderStyle>
	<Columns>
		<asp:TemplateColumn HeaderText="C&#243;digo Diagn&#243;stico">
			<ItemStyle Width="20%"></ItemStyle>
			<ItemTemplate>
				<asp:Label runat="server" ID="lblDiagnostico" text='<%# DataBinder.Eval(Container, "DataItem.CodigoDiagnostico").ToString()%>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Diagn&#243;stico">
			<ItemStyle Width="60%"></ItemStyle>
			<ItemTemplate>
				<asp:Label runat="server" ID="Label1" text='<%# DataBinder.Eval(Container, "DataItem.NombreDiagnostico").ToString() %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Tiempo Evoluci&#243;n">
			<ItemStyle HorizontalAlign="Center" Width="20%"></ItemStyle>
			<ItemTemplate>
				<asp:Label runat="server" ID="lblEvolucion" text='<%# DataBinder.Eval(Container, "DataItem.TiempoEvolucion").ToString() + " " + DataBinder.Eval(Container, "DataItem.PeriodoEvolucion").ToString() %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
</asp:DataGrid>
