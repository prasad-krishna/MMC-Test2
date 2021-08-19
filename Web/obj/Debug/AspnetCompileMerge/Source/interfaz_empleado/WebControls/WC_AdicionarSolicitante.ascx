<%@ Control Language="c#" AutoEventWireup="false" Codebehind="WC_AdicionarSolicitante.ascx.cs" Inherits="TPA.interfaz_empleado.WebControls.WC_AdicionarSolicitante" %>
<script language="javascript">
			
			function ShowSolicitante(sender, idDiv)
			{
				var div = document.getElementById(idDiv);	
					
				if(sender.checked)
				{					
					div.style.display = "";
				}
				else
				{
					div.style.display = "none";				
				}					
				
			}

</script>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD><asp:checkbox id="chkOtroSolicitante" Text="Seleccionar otro Solicitante" runat="server" CssClass="textSmallBlack"></asp:checkbox>
			<div style="DISPLAY:none" id="dvSolicitante" runat="server">&nbsp;<asp:dropdownlist id="ddlPrestador" runat="server" Visible="False" CssClass="textBoxSmallControl"
					Width="200px"></asp:dropdownlist><asp:textbox id="txtIdPrestador" runat="server" Width="0px"></asp:textbox><asp:textbox id="txtPrestador" runat="server" Visible="False" CssClass="textBoxSmallControl"
					Width="110px"></asp:textbox>&nbsp;<INPUT class="buttonSmall" id="btnBuscarPrestador" style="DISPLAY: none" onclick="javascript:ShowPrestador(this);"
					type="button" value="..." name="btnBuscarPrestador" runat="server"></div>
		</TD>
	</TR>
</TABLE>
