<%@ Control Language="c#" AutoEventWireup="false" Codebehind="WC_AnotacionesFijasFormato.ascx.cs" Inherits="TPA.interfaz_empleado.WebControls.WC_AnotacionesFijasFormato" %>
<asp:DataGrid id="dtgAnotaciones" Width="100%" CellPadding="0" AutoGenerateColumns="False" GridLines="None"
	CssClass="gridFormatoVacio" runat="server" ShowHeader="False">
	<AlternatingItemStyle CssClass="norItemsVacio"></AlternatingItemStyle>
	<ItemStyle CssClass="norItemsVacio"></ItemStyle>
	<Columns>
		<asp:TemplateColumn>
			<ItemStyle Width="60%"></ItemStyle>
			<ItemTemplate>
				<asp:Label CssClass="lblFormatoVacio" runat="server" ID="lblAnotacion" text='<%# DataBinder.Eval(Container, "DataItem.Nombre").ToString() %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
</asp:DataGrid>
