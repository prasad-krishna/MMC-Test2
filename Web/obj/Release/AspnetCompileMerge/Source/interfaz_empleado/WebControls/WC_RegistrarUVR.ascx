
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="WC_RegistrarUVR.ascx.cs" Inherits="WebMedicamentos.interfaz_empleado.WebControls.WC_RegistrarUVR" %>
<script language="javascript">
			
			function ShowUVR(sender, nametxtUVR, nametxtValorCalculadoUVR)
			{		
				
				var valuetxtValorCalculadoUVR = document.getElementById('valuetxtValorCalculadoUVR');				
				var valuetxtUVRS = document.getElementById('valuetxtUVRS');
				var txtTemp = document.getElementById('<%= this.txtTemp.ClientID %>');
											
				valuetxtUVRS.value = nametxtUVR;
				valuetxtValorCalculadoUVR.value = nametxtValorCalculadoUVR;
				txtTemp.value = nametxtUVR;				
								
				var left = Narg_GetPosX(sender) - 70;
				var top = Narg_GetPosY(sender) + 15;            
				setTimeout('showLayer("RegistrarUVR",' + left + ',' +  top + ');', 50);
				setTimeout('document.Form1.WC_RegistrarUVR1_txtUVRS.focus();', 51);
			} 	
			
			function DevolverUVR(uvrs, valorCalculado)
			{		
				
				var txtUVRS = document.getElementById(document.getElementById('valuetxtUVRS').value);			
				var txtValorCalculadoUVRS = document.getElementById(document.getElementById('valuetxtValorCalculadoUVR').value);
				
				if(uvrs != null)
				{					
					txtUVRS.value = uvrs;						
					txtValorCalculadoUVRS.value = valorCalculado;				
					closeLayer('RegistrarUVR');
				}				
			}	


</script>
<INPUT id="valuetxtUVRS" type="hidden"><INPUT id="valuetxtValorCalculadoUVR" type="hidden">
<iframe id="ifrRegistrarUVR" frameborder="0" style="DISPLAY: none; Z-INDEX: 1000; WIDTH: 280px; POSITION: absolute; HEIGHT: 150px" src="pagina.htm"></iframe>
<div id="dvRegistrarUVR" style="BORDER-RIGHT: dimgray 1px outset; BORDER-TOP: dimgray 1px outset; DISPLAY: none; Z-INDEX: 1100; OVERFLOW: hidden; BORDER-LEFT: dimgray 1px outset; WIDTH: 280px; BORDER-BOTTOM: dimgray 1px outset; POSITION: absolute; HEIGHT: 150px; BACKGROUND-COLOR: white">
	<table border="0" cellpadding="0" cellspacing="0" style="WIDTH: 100%">
		<tr>
			<td align="right" valign="top">
				<asp:Image Style="CURSOR: pointer" ID="imgCerrar" onclick="closeLayer('RegistrarUVR');" ImageUrl="../../images/imgClose.gif"
					runat="server"></asp:Image>
			</td>
		</tr>
		<tr>
			<td align="center" valign="top">
				<fieldset style="WIDTH: 95%">
					<legend>
						Calcular UVR</legend>
					<asp:updatePanel id="Ajaxpanel4" runat="server" UpdateMode="Conditional"><ContentTemplate>
						<TABLE style="WIDTH: 90%" cellSpacing="0" cellPadding="0" border="0">
							<TR>
								<TD align="left" width="40%" colSpan="4">UVRS</TD>
								<TD align="left">
									<asp:TextBox onkeypress="return currencyFormat(this,event,true,false)" id="txtUVRS" runat="server"
										CssClass="textBox" Width="80px"></asp:TextBox>&nbsp;
								</TD>
							</TR>
							<TR>
								<TD align="left" width="40%" colSpan="4">Valor de la UVR</TD>
								<TD align="left">
									<asp:TextBox onkeypress="return currencyFormat(this,event,true,false)" id="txtValorUVR" runat="server"
										CssClass="textBox" Width="80px"></asp:TextBox>
									<asp:TextBox id="txtTemp" runat="server" Width="0px"></asp:TextBox>
									<asp:LinkButton id="lnkCalcular" runat="server" CausesValidation="False">Calcular</asp:LinkButton></TD>
							</TR>
							<TR>
								<TD align="left" width="20%" colSpan="5"></TD>
							</TR>
						</TABLE>
					</ContentTemplate>
					<Triggers>
					<asp:AsyncPostBackTrigger ControlID="lnkCalcular" EventName="Click"/>
					</Triggers>
					</asp:updatePanel>
				</fieldset>
			</td>
		</tr>
	</table>
</div>
