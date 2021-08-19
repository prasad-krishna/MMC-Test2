<%@ Control Language="c#" AutoEventWireup="false" Codebehind="WC_AdicionarDiagnosticoConsulta.ascx.cs" Inherits="TPA.interfaz_empleado.WebControls.WC_AdicionarDiagnosticoConsulta" %>
<script type="text/javascript">

			function BorrarItem(image, numberControl)
			{
				var tr = image.parentNode.parentNode;
				
				var inputs = tr.getElementsByTagName('input');                
                
				document.getElementById('<%= this.lblObligatorioDiagnostico.ClientID %>').style.display = "none";
											
				for (var j = 0; j < inputs.length; j++)
				{
					if(inputs[j].getAttribute('type') == 'text' && (inputs[j].getAttribute('id').indexOf('txtDiagnostico' + numberControl) != -1 || inputs[j].getAttribute('id').indexOf('txtIdDiagnostico' + numberControl) != -1 || inputs[j].getAttribute('id').indexOf('txtTiempoEvolucion' + numberControl) != -1)) 
					{
						inputs[j].value = "";						
					}	
				}
				
				inputs = tr.getElementsByTagName('select');

				for (var j = 0; j < inputs.length; j++) 
				{
				    if ((inputs[j].getAttribute('id').indexOf('ddlTiempoEvolucion' + numberControl) != -1 || inputs[j].getAttribute('id').indexOf('ddlTipoDiagnostico' + numberControl) != -1)) {
				        inputs[j].selectedIndex = 0;
				    }
				}
				
			}


		
</script>
        <style type="text/css">
            .textBoxOculto {
                display:none !important;
            }
        </style>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD width="15">
			</TD>
		<TD width="190">
			<asp:Label id="lblDiagnostico" runat="server" CssClass="textSmallBlack">Diagnóstico</asp:Label><SPAN class="textRed"><asp:Label ID="lblObligatorioDiagnostico" runat="server">*</asp:Label></SPAN></TD>
		<TD width="180">
			<asp:Label id="lblTiempoEvolución" CssClass="textSmallBlack" runat="server">Tiempo Evolución</asp:Label></TD>
		<TD width="200">&nbsp;&nbsp; <asp:Label id="Label4" CssClass="textSmallBlack" runat="server">Tipo Diagnóstico</asp:Label></TD>
		<TD width="40">
			</TD>
		<TD width="190">
			<asp:Label id="Label1" CssClass="textSmallBlack" runat="server">Diagnóstico</asp:Label></TD>
		<TD width="180">
			<asp:Label id="Label2" CssClass="textSmallBlack" runat="server">Tiempo Evolución</asp:Label></TD>
		<TD width="200">
			&nbsp;&nbsp;
			<asp:Label id="Label3" CssClass="textSmallBlack" runat="server">Tipo Diagnóstico</asp:Label></TD>
	</TR>
	<TR class="celdaFondo1">
	<TD colspan = 8 align="left">
	<asp:Label id="lblDiagnosticoPrincipal" CssClass="textSmallBlack" runat="server" 
            ForeColor="Maroon">Principal</asp:Label>
	</TD>
	</TR>
	<TR class="celdaFondo1">
		<TD>		
			</TD>
			
		<TD valign="top">
		
			<asp:TextBox id="txtDiagnostico1" runat="server" CssClass="textBoxSmallControl" Width="105px"></asp:TextBox>			
			<asp:Button id="btnBuscarDiagnostico1" runat="server" CssClass="buttonSmall" Width="20px" CausesValidation="false"			
				Text="..." ToolTip="Seleccione para buscar el diagnóstico"></asp:Button>				
		</TD>
		<TD>
			<asp:TextBox onkeypress="return currencyFormat(this,event,true,false)" id="txtTiempoEvolucion1"
				Width="40px" CssClass="textBoxSmallControl" runat="server"></asp:TextBox>
			<asp:dropdownlist id="ddlTiempoEvolucion1" Width="40px" 
                CssClass="textBoxSmallControl" runat="server">
				<asp:ListItem Value="0">--</asp:ListItem>
				<asp:ListItem Value="D&#237;as">Días</asp:ListItem>
				<asp:ListItem Value="Meses">Meses</asp:ListItem>
				<asp:ListItem Value="A&#241;os">Años</asp:ListItem>
			</asp:dropdownlist>
			<asp:TextBox id="txtIdDiagnostico1" runat="server" CssClass="textBox textBoxOculto" Width="0px"></asp:TextBox>
		</TD>
		<TD>
			<asp:dropdownlist id="ddlTipoDiagnostico1" CssClass="textBoxSmallControl" runat="server" Width="120px"></asp:dropdownlist><img class="ImageTransparentLink" runat="server" id="imgLimpiar1" src="../../iconos/ico_limpiar.gif" onclick="BorrarItem(this, 1);" border="0">
	    </TD>	    
		<TD valign="bottom">
			</TD>
		<TD valign="top">
			<asp:TextBox id="txtDiagnostico4" CssClass="textBoxSmallControl" runat="server"
				Width="105px"></asp:TextBox>
			<asp:Button id="btnBuscarDiagnostico4" CssClass="buttonSmall" runat="server" Width="20px" ToolTip="Seleccione para buscar el diagnóstico"
				Text="..." CausesValidation="false"></asp:Button>        
			</TD>
		<TD>
			<asp:TextBox onkeypress="return currencyFormat(this,event,true,false)" id="txtTiempoEvolucion4"
				CssClass="textBoxSmallControl" runat="server" Width="40px"></asp:TextBox>
			<asp:dropdownlist id="ddlTiempoEvolucion4" CssClass="textBoxSmallControl" 
                runat="server" Width="40px">
				<asp:ListItem Value="0">--</asp:ListItem>
				<asp:ListItem Value="D&#237;as">D&#237;as</asp:ListItem>
				<asp:ListItem Value="Meses">Meses</asp:ListItem>
				<asp:ListItem Value="A&#241;os">A&#241;os</asp:ListItem>
			</asp:dropdownlist>
			<asp:TextBox id="txtIdDiagnostico4" CssClass="textBox" runat="server" Width="0px"></asp:TextBox></TD>
		<TD>
			<asp:dropdownlist id="ddlTipoDiagnostico4" CssClass="textBoxSmallControl" runat="server" Width="120px"></asp:dropdownlist><img class="ImageTransparentLink" runat="server" id="imgLimpiar4" src="../../iconos/ico_limpiar.gif" onclick="BorrarItem(this, 4);" border="0"></TD>
	</TR>	
	<TR class="celdaFondo2">
		<TD>
			</TD>
		<TD colspan="3">
			<asp:dropdownlist id="ddlClasificacion1" CssClass="textBoxSmallControl" 
                runat="server" Width="335px"></asp:dropdownlist><asp:TextBox id="txtEnabledddlClasificacion1" runat="server" CssClass="textBox" Text="true" Width="0px"></asp:TextBox>
	        </TD>
		<TD>
			</TD>
		<TD colspan="3">
			<asp:dropdownlist id="ddlClasificacion4" CssClass="textBoxSmallControl" 
                runat="server" Width="335px"></asp:dropdownlist><asp:TextBox id="txtEnabledddlClasificacion4" runat="server" CssClass="textBox" Text="false" Width="0px"></asp:TextBox>
			</TD>
	</TR>
	<TR style="padding-top:6px">
		<TD>		
			</TD>
		<TD valign="top">		
			<asp:TextBox id="txtDiagnostico2" Width="105px" CssClass="textBoxSmallControl"
				runat="server"></asp:TextBox>
			<asp:Button id="btnBuscarDiagnostico2" Width="20px" CssClass="buttonSmall" runat="server" ToolTip="Seleccione para buscar el diagnóstico"
				Text="..." CausesValidation="false"></asp:Button>        
	        </TD>
		<TD>
			<asp:TextBox onkeypress="return currencyFormat(this,event,true,false)" id="txtTiempoEvolucion2"
				Width="40px" CssClass="textBoxSmallControl" runat="server"></asp:TextBox>
			<asp:dropdownlist id="ddlTiempoEvolucion2" Width="40px" 
                CssClass="textBoxSmallControl" runat="server">
				<asp:ListItem Value="0">--</asp:ListItem>
				<asp:ListItem Value="D&#237;as">D&#237;as</asp:ListItem>
				<asp:ListItem Value="Meses">Meses</asp:ListItem>
				<asp:ListItem Value="A&#241;os">A&#241;os</asp:ListItem>
			</asp:dropdownlist>
			<asp:TextBox id="txtIdDiagnostico2" runat="server" CssClass="textBox" Width="0px"></asp:TextBox>
		</TD>
		<TD>
			<asp:dropdownlist id="ddlTipoDiagnostico2" CssClass="textBoxSmallControl" runat="server" Width="120px"></asp:dropdownlist><img class="ImageTransparentLink" runat="server" id="imgLimpiar2" src="../../iconos/ico_limpiar.gif" onclick="BorrarItem(this, 2);" border="0"></TD>
		<TD valign="bottom">
			</TD>
		<TD valign="top">
			<asp:TextBox id="txtDiagnostico5" CssClass="textBoxSmallControl" runat="server"
				Width="105px"></asp:TextBox>
			<asp:Button id="btnBuscarDiagnostico5" CssClass="buttonSmall" runat="server" Width="20px" ToolTip="Seleccione para buscar el diagnóstico"
				Text="..." CausesValidation="false"></asp:Button>
			</TD>
		<TD>
			<asp:TextBox onkeypress="return currencyFormat(this,event,true,false)" id="txtTiempoEvolucion5"
				CssClass="textBoxSmallControl" runat="server" Width="40px"></asp:TextBox>
			<asp:dropdownlist id="ddlTiempoEvolucion5" CssClass="textBoxSmallControl" 
                runat="server" Width="40px">
				<asp:ListItem Value="0">--</asp:ListItem>
				<asp:ListItem Value="D&#237;as">D&#237;as</asp:ListItem>
				<asp:ListItem Value="Meses">Meses</asp:ListItem>
				<asp:ListItem Value="A&#241;os">A&#241;os</asp:ListItem>
			</asp:dropdownlist>
			<asp:TextBox id="txtIdDiagnostico5" CssClass="textBox" runat="server" Width="0px"></asp:TextBox></TD>
		<TD>
			<asp:dropdownlist id="ddlTipoDiagnostico5" CssClass="textBoxSmallControl" runat="server" Width="120px"></asp:dropdownlist><img class="ImageTransparentLink" runat="server" id="imgLimpiar5" src="../../iconos/ico_limpiar.gif" onclick="BorrarItem(this, 5);" border="0"></TD>
	</TR>
	<TR style="padding-bottom:6px">
		<TD>
			</TD>
		<TD colspan="3">
			<asp:dropdownlist id="ddlClasificacion2" CssClass="textBoxSmallControl" 
                runat="server" Width="335px"></asp:dropdownlist><asp:TextBox id="txtEnabledddlClasificacion2" runat="server" CssClass="textBox" Text="false" Width="0px"></asp:TextBox>
	        </TD>
		<TD>
			</TD>
		<TD colspan="3">
			<asp:dropdownlist id="ddlClasificacion5" CssClass="textBoxSmallControl" 
                runat="server" Width="335px"></asp:dropdownlist><asp:TextBox id="txtEnabledddlClasificacion5" runat="server" CssClass="textBox" Text="false" Width="0px"></asp:TextBox>
			</TD>
	</TR>
	<TR class="celdaFondo1">
		<TD>
			</TD>
		<TD valign="top">
			<asp:TextBox id="txtDiagnostico3" Width="105px" CssClass="textBoxSmallControl"
				runat="server"></asp:TextBox>
			<asp:Button id="btnBuscarDiagnostico3" Width="20px" CssClass="buttonSmall" 
                runat="server" ToolTip="Seleccione para buscar el diagnóstico"
				Text="..." CausesValidation="false"></asp:Button>
	        </TD>
		<TD>		
			<asp:TextBox onkeypress="return currencyFormat(this,event,true,false)" id="txtTiempoEvolucion3"
				Width="40px" CssClass="textBoxSmallControl" runat="server"></asp:TextBox>
			<asp:dropdownlist id="ddlTiempoEvolucion3" Width="40px" 
                CssClass="textBoxSmallControl" runat="server">
				<asp:ListItem Value="0">--</asp:ListItem>
				<asp:ListItem Value="D&#237;as">D&#237;as</asp:ListItem>
				<asp:ListItem Value="Meses">Meses</asp:ListItem>
				<asp:ListItem Value="A&#241;os">A&#241;os</asp:ListItem>
			</asp:dropdownlist>
			<asp:TextBox id="txtIdDiagnostico3" runat="server" CssClass="textBox" Width="0px"></asp:TextBox>
		</TD>
		<TD>
			<asp:dropdownlist id="ddlTipoDiagnostico3" CssClass="textBoxSmallControl" runat="server" Width="120px"></asp:dropdownlist><img class="ImageTransparentLink" runat="server" id="imgLimpiar3" src="../../iconos/ico_limpiar.gif" onclick="BorrarItem(this, 3);" border="0"></TD>
		<TD valign="bottom">
		</TD>
		<TD valign="top">
			<asp:TextBox id="txtDiagnostico6" CssClass="textBoxSmallControl" runat="server"
				Width="105px"></asp:TextBox>
			<asp:Button id="btnBuscarDiagnostico6" CssClass="buttonSmall" runat="server" Width="20px" ToolTip="Seleccione para buscar el diagnóstico"
				Text="..." CausesValidation="false"></asp:Button>
			</TD>
		<TD>
			<asp:TextBox onkeypress="return currencyFormat(this,event,true,false)" id="txtTiempoEvolucion6"
				CssClass="textBoxSmallControl" runat="server" Width="40px"></asp:TextBox>
			<asp:dropdownlist id="ddlTiempoEvolucion6" CssClass="textBoxSmallControl" 
                runat="server" Width="40px">
				<asp:ListItem Value="0">--</asp:ListItem>
				<asp:ListItem Value="D&#237;as">D&#237;as</asp:ListItem>
				<asp:ListItem Value="Meses">Meses</asp:ListItem>
				<asp:ListItem Value="A&#241;os">A&#241;os</asp:ListItem>
			</asp:dropdownlist>
			<asp:TextBox id="txtIdDiagnostico6" CssClass="textBox" runat="server" Width="0px"></asp:TextBox></TD>
		<TD>
			<asp:dropdownlist id="ddlTipoDiagnostico6" CssClass="textBoxSmallControl" runat="server" Width="120px"></asp:dropdownlist><img class="ImageTransparentLink" runat="server" id="imgLimpiar6" src="../../iconos/ico_limpiar.gif" onclick="BorrarItem(this, 6);" border="0"></TD>
	</TR>
	<TR class="celdaFondo2">
		<TD>
			</TD>
		<TD colspan="3">
			<asp:dropdownlist id="ddlClasificacion3" CssClass="textBoxSmallControl" 
                runat="server" Width="335px"></asp:dropdownlist><asp:TextBox id="txtEnabledddlClasificacion3" runat="server" CssClass="textBox" Text="false" Width="0px"></asp:TextBox>
	        </TD>
		<TD>
			</TD>
		<TD colspan="3">
			<asp:dropdownlist id="ddlClasificacion6" CssClass="textBoxSmallControl" 
                runat="server" Width="335px"></asp:dropdownlist><asp:TextBox id="txtEnabledddlClasificacion6" runat="server" CssClass="textBox" Width="0px"></asp:TextBox>
			</TD>
	</TR>
</TABLE>
