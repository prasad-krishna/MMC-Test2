<%@ Control Language="c#" AutoEventWireup="false" Codebehind="WC_AdicionarDiagnostico.ascx.cs" Inherits="TPA.interfaz_empleado.WebControls.WC_AdicionarDiagnostico" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD width="170">
			<asp:Label id="lblDiagnostico" runat="server" CssClass="textSmallBlack">Diagnóstico</asp:Label></TD>
		<TD width="210">
			<asp:Label id="lblTiempoEvolución" CssClass="textSmallBlack" runat="server">Tiempo Evolución</asp:Label></TD>
	</TR>
	<TR>
		<TD width="160">
			<asp:TextBox id="txtDiagnosticoTipoServicio1" runat="server" CssClass="textBoxSmallControl" Width="105px"></asp:TextBox>
			<asp:Button id="btnBuscarDiagnosticoTipoServicio1" runat="server" CssClass="buttonSmall" Width="20px"
				CausesValidation="false" Text="..." ToolTip="Seleccione para buscar el diagnóstico"></asp:Button>
		</TD>
		<TD>
			<asp:TextBox onkeypress="return currencyFormat(this,event,true,false)" id="txtTiempoEvolucion1"
				Width="40px" CssClass="textBoxSmallControl" runat="server"></asp:TextBox>
			<asp:dropdownlist id="ddlTiempoEvolucion1" Width="50px" CssClass="textBoxSmallControl" runat="server">
				<asp:ListItem Value="0">--</asp:ListItem>
				<asp:ListItem Value="D&#237;as">D&#237;as</asp:ListItem>
				<asp:ListItem Value="Meses">Meses</asp:ListItem>
				<asp:ListItem Value="A&#241;os">A&#241;os</asp:ListItem>
			</asp:dropdownlist>
			<asp:TextBox id="txtIdDiagnosticoTipoServicio1" runat="server" CssClass="textBox" Width="0px"></asp:TextBox>
		</TD>
	</TR>
	<TR>
		<TD>
			<asp:TextBox id="txtDiagnosticoTipoServicio2" Width="105px" CssClass="textBoxSmallControl"
				runat="server"></asp:TextBox>
			<asp:Button id="btnBuscarDiagnosticoTipoServicio2" Width="20px" CssClass="buttonSmall" runat="server"
				ToolTip="Seleccione para buscar el diagnóstico" Text="..." CausesValidation="false"></asp:Button></TD>
		<TD>
			<asp:TextBox onkeypress="return currencyFormat(this,event,true,false)" id="txtTiempoEvolucion2"
				Width="40px" CssClass="textBoxSmallControl" runat="server"></asp:TextBox>
			<asp:dropdownlist id="ddlTiempoEvolucion2" Width="50px" CssClass="textBoxSmallControl" runat="server">
				<asp:ListItem Value="">--</asp:ListItem>
				<asp:ListItem Value="D&#237;as">D&#237;as</asp:ListItem>
				<asp:ListItem Value="Meses">Meses</asp:ListItem>
				<asp:ListItem Value="A&#241;os">A&#241;os</asp:ListItem>
			</asp:dropdownlist>
			<asp:TextBox id="txtIdDiagnosticoTipoServicio2" runat="server" CssClass="textBox" Width="0px"></asp:TextBox>
		</TD>
	</TR>
	<TR>
		<TD>
			<asp:TextBox id="txtDiagnosticoTipoServicio3" Width="105px" CssClass="textBoxSmallControl"
				runat="server"></asp:TextBox>
			<asp:Button id="btnBuscarDiagnosticoTipoServicio3" Width="20px" CssClass="buttonSmall" runat="server"
				ToolTip="Seleccione para buscar el diagnóstico" Text="..." CausesValidation="false"></asp:Button></TD>
		<TD>
			<asp:TextBox onkeypress="return currencyFormat(this,event,true,false)" id="txtTiempoEvolucion3"
				Width="40px" CssClass="textBoxSmallControl" runat="server"></asp:TextBox>
			<asp:dropdownlist id="ddlTiempoEvolucion3" Width="50px" CssClass="textBoxSmallControl" runat="server">
				<asp:ListItem Value="0">--</asp:ListItem>
				<asp:ListItem Value="D&#237;as">D&#237;as</asp:ListItem>
				<asp:ListItem Value="Meses">Meses</asp:ListItem>
				<asp:ListItem Value="A&#241;os">A&#241;os</asp:ListItem>
			</asp:dropdownlist>
			<asp:TextBox id="txtIdDiagnosticoTipoServicio3" runat="server" CssClass="textBox" Width="0px"></asp:TextBox>
		</TD>
	</TR>
</TABLE>
