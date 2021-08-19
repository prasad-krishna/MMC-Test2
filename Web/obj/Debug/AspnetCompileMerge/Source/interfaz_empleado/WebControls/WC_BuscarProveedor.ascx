
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="WC_BuscarProveedor.ascx.cs" Inherits="TPA.interfaz_empleado.WebControls.WC_BuscarProveedor" %>
<script language="javascript">
			
			function ShowProveedor(sender, nametxtIdProveedor, idTipoServicio, nametxtNombreProveedor)
			{			
						
				var valuetxtIdProveedor = document.getElementById('valuetxtIdProveedor');				
				var valuetxtNombreProveedor = document.getElementById('valuetxtNombreProveedor');
												
				var txtTemp = document.getElementById('<%= this.txtTemp.ClientID %>');
				var valuetxtIdTipoServicio = document.getElementById('<%= this.txtIdTipoServicio.ClientID %>');
									
				valuetxtIdProveedor.value = nametxtIdProveedor;
				valuetxtIdTipoServicio.value = idTipoServicio;
				valuetxtNombreProveedor.value = nametxtNombreProveedor;				
				txtTemp.value = nametxtNombreProveedor;				
								
				var left = Narg_GetPosX(sender) - 150;
				var top = Narg_GetPosY(sender) + 15;            
				setTimeout('showLayer("BuscarProveedor",' + left + ',' +  top + ');', 50);
				setTimeout('document.Form1.WC_BuscarProveedor1_txtNombre.focus();', 51);
				
			} 	
			
			function ShowProveedorUser(sender, nametxtIdProveedor, idTipoServicio, nametxtNombreProveedor)
			{			
						
				var valuetxtIdProveedor = document.getElementById('valuetxtIdProveedor');				
				var valuetxtNombreProveedor = document.getElementById('valuetxtNombreProveedor');
												
				var txtTemp = document.getElementById('<%= this.txtTemp.ClientID %>');
				var txtCiudad = document.getElementById('<%= this.txtCiudad.ClientID %>');
				var valuetxtIdTipoServicio = document.getElementById('<%= this.txtIdTipoServicio.ClientID %>');
									
				valuetxtIdProveedor.value = nametxtIdProveedor;
				valuetxtIdTipoServicio.value = idTipoServicio;
				valuetxtNombreProveedor.value = nametxtNombreProveedor;				
				txtTemp.value = nametxtNombreProveedor;	
				txtCiudad.value = "1";			
								
				var left = Narg_GetPosX(sender) - 150;
				var top = Narg_GetPosY(sender) + 15;            
				setTimeout('showLayer("BuscarProveedor",' + left + ',' +  top + ');', 50);
				setTimeout('document.Form1.WC_BuscarProveedor1_txtNombre.focus();', 51);
				
			} 	
			
			function DevolverProveedor(valor, id, telefono, direccion, horario)
			{		
				var txtIdProveedor = document.getElementById(document.getElementById('valuetxtIdProveedor').value);
				var txtNombreProveedor = document.getElementById(document.getElementById('valuetxtNombreProveedor').value);
																
				if(txtNombreProveedor != null)
				{					
					txtNombreProveedor.value = valor;
					txtNombreProveedor.title = valor + " " + direccion + " Tel " + telefono+ "-" + horario;
					txtIdProveedor.value = id;
					document.getElementById("WC_BuscarProveedor1_txtNombre").value = "";
					document.getElementById("WC_BuscarProveedor1_ddlEspecialidad").selectedIndex = 0;
					
					closeLayer('BuscarProveedor');
				}				
			}	


</script>
<INPUT id="valuetxtNombreProveedor" type="hidden"><INPUT id="valuetxtIdProveedor" type="hidden">
<INPUT id="valuelblInformacion" type="hidden" size="50">
<iframe id="ifrBuscarProveedor" style="DISPLAY: none; Z-INDEX: 1000; WIDTH: 280px; POSITION: absolute; HEIGHT: 280px"
	frameBorder="0" src="pagina.htm"></iframe>
<div id="dvBuscarProveedor" style="BORDER-RIGHT: dimgray 1px outset; BORDER-TOP: dimgray 1px outset; OVERFLOW-Y: auto; DISPLAY: none; Z-INDEX: 1100; BORDER-LEFT: dimgray 1px outset; WIDTH: 280px; BORDER-BOTTOM: dimgray 1px outset; POSITION: absolute; HEIGHT: 270px; BACKGROUND-COLOR: white">
	<table style="WIDTH: 100%" cellSpacing="0" cellPadding="0" border="0">
		<tr>
			<td vAlign="top" align="right" height="14"><asp:image id="imgCerrar" style="CURSOR: pointer" onclick="closeLayer('BuscarProveedor');"
					runat="server" ImageUrl="../../images/imgClose.gif"></asp:image></td>
		</tr>
		<tr>
			<td vAlign="top" align="center">
				<fieldset style="WIDTH: 95%">
					<legend>Búsqueda Rápida</legend>
					<asp:updatePanel id="Ajaxpanel22" runat="server"><ContentTemplate>
      <TABLE style="WIDTH: 90%" cellSpacing=0 cellPadding=0 border=0>
        <TR>
          <TD align=left width="25%">Nombre </TD>
          <TD align=left>
<asp:TextBox id=txtNombre runat="server" CssClass="textBoxSmall" Width="120px"></asp:TextBox>
<asp:TextBox id=txtIdTipoServicio runat="server" Width="0px"></asp:TextBox>
<asp:TextBox id=txtTemp runat="server" Width="0px"></asp:TextBox>
<asp:TextBox id=txtCiudad runat="server" Width="0px"></asp:TextBox></TD></TR>
        <TR>
          <TD align=left width="25%">
            <P>Especialidad</P></TD>
          <TD align=left>
<asp:DropDownList id=ddlEspecialidad runat="server" CssClass="textBoxSmall" Width="190px" AutoPostBack="True"></asp:DropDownList>
<asp:LinkButton id=lnkBuscar runat="server" CausesValidation="False">Buscar</asp:LinkButton></TD></TR>
        <TR>
          <TD align=left width="20%" colSpan=2>
<asp:Label id=lblResultado runat="server" CssClass="textRed"></asp:Label>
<asp:DataGrid id=dtgProveedor runat="server" Width="100%" PageSize="6" ShowHeader="False" AutoGenerateColumns="False" EnableViewState="False">
<Columns>
<asp:TemplateColumn>
<ItemStyle Width="60%">
</ItemStyle>

<ItemTemplate>
													<asp:LinkButton CausesValidation="false" CssClass="textSmallBlue" id="lnkProveedor" Text='<%# DataBinder.Eval(Container, "DataItem.NombreProveedor") %>' runat="server" CommandName="seleccionar">
													</asp:LinkButton>
												
</ItemTemplate>
</asp:TemplateColumn>
<asp:BoundColumn Visible="False" DataField="IdProveedor"></asp:BoundColumn>
<asp:BoundColumn Visible="False" DataField="Telefonos"></asp:BoundColumn>
<asp:BoundColumn Visible="False" DataField="Direcciones"></asp:BoundColumn>
<asp:BoundColumn Visible="False" DataField="Horario"></asp:BoundColumn>
<asp:BoundColumn DataField="Informacion">
<ItemStyle Width="40%" CssClass="textSmallBlack">
</ItemStyle>
</asp:BoundColumn>
</Columns>

<PagerStyle Position="Top">
</PagerStyle>
</asp:DataGrid></TD></TR></TABLE>
					</ContentTemplate>
					<Triggers>
					<asp:AsyncPostBackTrigger ControlID="lnkBuscar" EventName="Click"/>
					</Triggers>
					</asp:updatePanel>
				</fieldset>
			</td>
		</tr>
	</table>
</div>
