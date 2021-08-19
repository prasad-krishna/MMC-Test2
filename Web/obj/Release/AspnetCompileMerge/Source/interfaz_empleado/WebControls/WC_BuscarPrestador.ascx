
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="WC_BuscarPrestador.ascx.cs" Inherits="TPA.interfaz_empleado.WebControls.WC_BuscarPrestador" %>
<script language="javascript">
			
			function ShowPrestador(sender)
			{					
				var left = Narg_GetPosX(sender) - 200;
				var top = Narg_GetPosY(sender) + 15; 
				setTimeout('showLayer("BuscarPrestador",' + left + ',' +  top +  ');', 50);
				setTimeout('document.Form1.WC_BuscarPrestador1_txtNombre.focus();', 51);
															
			} 	
			
			function DevolverPrestador(valor, id, idProveedor)
			{
			    var txtPrestador = document.getElementById('txtPrestador');
			    var txtIdPrestador = document.getElementById('txtIdPrestador');
			    var txtIdProveedor = document.getElementById('txtIdProveedor');
			    
				if(txtPrestador != null)
				{
					txtPrestador.value = valor;
					txtPrestador.title = valor;
					txtIdPrestador.value = id;
					txtIdProveedor.value = idProveedor;
					closeLayer('BuscarPrestador');
				}				
			}	


</script>
<iframe id="ifrBuscarPrestador" frameborder="0" style="DISPLAY: none; Z-INDEX: 1000; WIDTH: 280px; POSITION: absolute; HEIGHT: 280px" src="pagina.htm"></iframe>
<div id="dvBuscarPrestador" style="BORDER-RIGHT: dimgray 1px outset; BORDER-TOP: dimgray 1px outset; OVERFLOW-Y: auto; DISPLAY: none; Z-INDEX: 1100; BORDER-LEFT: dimgray 1px outset; WIDTH: 280px; BORDER-BOTTOM: dimgray 1px outset; POSITION: absolute; HEIGHT: 280px; BACKGROUND-COLOR: white">
	<table border="0" cellpadding="0" cellspacing="0" style="WIDTH: 100%">
		<tr>
			<td align="right" valign="top" height="14">
				<asp:Image Style="CURSOR: pointer" ID="imgCerrar" onclick="closeLayer('BuscarPrestador');"
					ImageUrl="../../images/imgClose.gif" runat="server"></asp:Image>
			</td>
		</tr>
		<tr>
			<td align="center" valign="top">
				<fieldset style="WIDTH: 95%">
					<legend>
						Búsqueda Rápida</legend>
					<asp:updatePanel id="Ajaxpanel4" runat="server"><ContentTemplate>
      <TABLE style="WIDTH: 90%" cellSpacing=0 cellPadding=0 border=0>
        <TR>
          <TD align=left width="20%">Nombre</TD>
          <TD align=left>
<asp:TextBox id=txtNombre runat="server" Width="130px" CssClass="textBoxSmall"></asp:TextBox></TD></TR>
        <TR>
          <TD align=left width="20%">Especialidad</TD>
          <TD align=left>
<asp:DropDownList id=ddlEspecialidad runat="server" Width="190px" CssClass="textBoxSmall" AutoPostBack="True"></asp:DropDownList>
<asp:LinkButton id=lnkBuscar runat="server" CausesValidation="False">Buscar</asp:LinkButton></TD></TR>
        <TR>
          <TD align=left width="20%" colSpan=2>
<asp:Label id=lblResultado runat="server" CssClass="textRed"></asp:Label>
<asp:DataGrid id=dtgPrestadores runat="server" Width="100%" AutoGenerateColumns="False" ShowHeader="False" PageSize="6">
										<Columns>
											<asp:BoundColumn Visible="False" DataField="IdPrestador"></asp:BoundColumn>
											<asp:TemplateColumn>
												<ItemTemplate>
													<asp:LinkButton CausesValidation="false" CssClass="textSmallBlue" id="lnkNombre" Text='<%# DataBinder.Eval(Container, "DataItem.NombrePrestador")%>' runat="server" CommandName="seleccionar">
													</asp:LinkButton>
												</ItemTemplate>
												<ItemStyle Width="60%"></ItemStyle>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="Informacion">
												<ItemStyle CssClass="textSmallBlack" Width="40%"></ItemStyle>
											</asp:BoundColumn>
										</Columns>
										<PagerStyle Position="Top"></PagerStyle>
									</asp:DataGrid></TD></TR></TABLE>
					</ContentTemplate>
					<Triggers><asp:AsyncPostBackTrigger ControlID="lnkBuscar" EventName="Click" /></Triggers>
					</asp:updatePanel>
				</fieldset>
			</td>
		</tr>
	</table>
</div>
