<%@ Control Language="c#" AutoEventWireup="false" Codebehind="WC_BuscarDiagnostico.ascx.cs" Inherits="TPA.interfaz_empleado.WebControls.WC_BuscarDiagnostico" %>
<script language="javascript">
			
			function ShowDiagnostico(sender, nametxtIdDiagnostico, nametxtNombreDiagnostico)
			{
						
				var valuetxtIdDiagnostico = document.getElementById('valuetxtIdDiagnostico');
				var valuetxtNombreDiagnostico = document.getElementById('valuetxtNombreDiagnostico');
				var valueddlIdClasificacion = document.getElementById('valueddlIdClasificacion');
				var txtTemp = document.getElementById('<%= this.txtTemp.ClientID %>');
				
				valuetxtIdDiagnostico.value = nametxtIdDiagnostico;
				valuetxtNombreDiagnostico.value = nametxtNombreDiagnostico;
				valueddlIdClasificacion.value = '';
				txtTemp.value = nametxtNombreDiagnostico;					
				
				var left = Narg_GetPosX(sender) - 50;
				var top = Narg_GetPosY(sender) + 15;  	
				setTimeout('showLayer("BuscarDiagnostico",' + left + ',' +  top + ');', 50);			
				setTimeout('document.Form1.WC_BuscarDiagnostico1_txtNombre.focus();', 51);						        
															
			}


			function ShowDiagnosticoClasificacion(sender, nametxtIdDiagnostico, nametxtNombreDiagnostico, nameddlClasificacion, namedtxtEnableClasificacion) {

			
			    var valuetxtIdDiagnostico = document.getElementById('valuetxtIdDiagnostico');
			    var valuetxtNombreDiagnostico = document.getElementById('valuetxtNombreDiagnostico');
			    var txtTemp = document.getElementById('<%= this.txtTemp.ClientID %>');

			    valuetxtIdDiagnostico.value = nametxtIdDiagnostico;
			    valuetxtNombreDiagnostico.value = nametxtNombreDiagnostico;
			    txtTemp.value = nametxtNombreDiagnostico;

			    var left = Narg_GetPosX(sender) - 50;
			    var top = Narg_GetPosY(sender) + 15;
			    setTimeout('showLayer("BuscarDiagnostico",' + left + ',' + top + ');', 50);
			    setTimeout('document.Form1.WC_BuscarDiagnostico1_txtNombre.focus();', 51);

			} 	
			
			function DevolverDiagnostico(valor, id, idClasificacion)
			{
				var txtIdDiagnostico = document.getElementById(document.getElementById('valuetxtIdDiagnostico').value);
				var txtNombreDiagnostico = document.getElementById(document.getElementById('valuetxtNombreDiagnostico').value);
			
				
				if(txtIdDiagnostico != null)
				{
					txtNombreDiagnostico.value = valor;
					txtNombreDiagnostico.title = valor;
					txtIdDiagnostico.value = id;
							
					document.getElementById('WC_BuscarDiagnostico1_txtNombre').value = "";
					document.getElementById('WC_BuscarDiagnostico1_txtCodigo').value = "";
					closeLayer('BuscarDiagnostico');
				}				
			}	


</script>
<INPUT id="valuetxtNombreDiagnostico" type="hidden"><INPUT id="valuetxtIdDiagnostico" type="hidden"><INPUT id="valueddlIdClasificacion" type="hidden"><INPUT id="valuetxtEnableClasificacion" type="text">
<iframe id="ifrBuscarDiagnostico" frameborder="0" style="DISPLAY: none; Z-INDEX: 1000; WIDTH: 280px; POSITION: absolute; HEIGHT: 300px" src="pagina.htm"></iframe>
<div id="dvBuscarDiagnostico" style="BORDER-RIGHT: dimgray 1px outset; BORDER-TOP: dimgray 1px outset; OVERFLOW-Y: auto; DISPLAY: none; Z-INDEX: 1100; BORDER-LEFT: dimgray 1px outset; WIDTH: 280px; BORDER-BOTTOM: dimgray 1px outset; POSITION: absolute; HEIGHT: 300px; BACKGROUND-COLOR: white">
	<table border="0" cellpadding="0" cellspacing="0" style="WIDTH: 100%">
		<tr>
			<td align="right" valign="top" height="14">
				<asp:Image Style="CURSOR: pointer" ID="imgCerrar" onclick="closeLayer('BuscarDiagnostico');"
					ImageUrl="../../images/imgClose.gif" runat="server"></asp:Image>
			</td>
		</tr>
		<tr>
			<td align="center" valign="top">
				<fieldset style="WIDTH: 95%">
					<legend>
						Búsqueda Rápida</legend>
					<asp:updatePanel id="Ajaxpanel100" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
      <TABLE style="WIDTH: 90%" cellSpacing=0 cellPadding=0 border=0>
        <TR>
          <TD align=left width="20%">Código </TD>
          <TD align=left>
<asp:TextBox id=txtCodigo runat="server" CssClass="textBoxSmall" Width="70px"></asp:TextBox>&nbsp; 
<asp:LinkButton id=lnkBuscar runat="server" CausesValidation="False">Buscar</asp:LinkButton></TD></TR>
        <TR>
          <TD align=left width="20%">Nombre</TD>
          <TD align=left>
<asp:TextBox id=txtNombre runat="server" CssClass="textBoxSmall"></asp:TextBox></TD>
<asp:TextBox id=txtTemp runat="server" Width="0px"></asp:TextBox></TR>
        <TR>
          <TD align=left colSpan=2>
<asp:Label id=lblResultado runat="server" CssClass="textRed"></asp:Label>
<asp:DataGrid id=dtgDiagnosticos runat="server" PageSize="4" ShowHeader="False" AutoGenerateColumns="False" EnableViewState="False">
<Columns>
<asp:TemplateColumn>
<ItemTemplate>
													<asp:LinkButton CausesValidation="false" CssClass="textSmallBlue" id="lnkDiagnostico" Text='<%# DataBinder.Eval(Container, "DataItem.NombreDiagnosticoCompleto") %>' runat="server">
													</asp:LinkButton>													
													
</ItemTemplate>
</asp:TemplateColumn>
<asp:BoundColumn Visible="False" DataField="IdDiagnostico"></asp:BoundColumn>
<asp:BoundColumn Visible="False" DataField="IdDiagnosticoClasificacion"></asp:BoundColumn>
</Columns>

<PagerStyle Position="Top">
</PagerStyle>
</asp:DataGrid></TD></TR></TABLE>
					</ContentTemplate>
					<Triggers>
					<asp:AsyncPostBackTrigger ControlID="lnkBuscar" EventName="Click" />					                    
					</Triggers>
					</asp:updatePanel>
				</fieldset>
			</td>
		</tr>
	</table>
</div>
