<%@ Control Language="c#" AutoEventWireup="false" Codebehind="WC_AdicionarPrestador.ascx.cs" Inherits="TPA.interfaz_empleado.WebControls.WC_AdicionarPrestador" %>
    <style type="text/css">
        .textBoxOculto {
            display: none !important;
        }
    </style>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD width="150">
			<asp:DropDownList id="ddlProveedor1" runat="server" Visible="false" CssClass="textBoxSmall" Width="140px"></asp:DropDownList>
			<asp:TextBox id="txtProveedor1" runat="server" Visible="false" CssClass="textBoxSmallControl"
				Width="110px"></asp:TextBox>
			<asp:Button id="btnBuscarProveedor1" runat="server" Visible="false" CssClass="buttonSmall" Width="20px"
				CausesValidation="false" Text="..." ToolTip="Seleccione para buscar el prestador"></asp:Button>
			<asp:TextBox id="txtConsecutivo1" Width="10px" Visible="False" runat="server"></asp:TextBox>
		</TD>
		<TD width="130">
			<asp:CheckBox id="chkDesplegarUVR1" runat="server" Visible="false" CssClass="textSmallBlack" Text="UVR en Formato"
				Height="5px"></asp:CheckBox>
			<asp:TextBox id="txtIdProveedor1" runat="server" CssClass="textBox textBoxOculto" Width="0px"></asp:TextBox>
		</TD>
	</TR>
	<TR id="trProveedor2" runat="server">
		<TD>
			<asp:DropDownList id="ddlProveedor2" runat="server" Visible="false" CssClass="textBoxSmall" Width="140px"></asp:DropDownList>
			<asp:TextBox id="txtProveedor2" CssClass="textBoxSmallControl" runat="server"
				Width="110px" Visible="false"></asp:TextBox>
			<asp:Button id="btnBuscarProveedor2" CssClass="buttonSmall" runat="server" Width="20px" ToolTip="Seleccione para buscar el prestador"
				Text="..." CausesValidation="false" Visible="false"></asp:Button>
			<asp:TextBox id="txtConsecutivo2" Width="10px" Visible="False" runat="server"></asp:TextBox>
		</TD>
		<TD>
			<asp:CheckBox id="chkDesplegarUVR2" CssClass="textSmallBlack" runat="server" Text="UVR en Formato"
				Visible="false"></asp:CheckBox>
			<asp:TextBox id="txtIdProveedor2" runat="server" CssClass="textBox" Width="0px"></asp:TextBox>
		</TD>
	</TR>
	<TR id="trProveedor3" runat="server">
		<TD>
			<asp:DropDownList id="ddlProveedor3" runat="server" Visible="false" CssClass="textBoxSmall" Width="140px"></asp:DropDownList>
			<asp:TextBox id="txtProveedor3" CssClass="textBoxSmallControl" runat="server"
				Width="110px" Visible="false"></asp:TextBox>
			<asp:Button id="btnBuscarProveedor3" CssClass="buttonSmall" runat="server" Width="20px" ToolTip="Seleccione para buscar el prestador"
				Text="..." CausesValidation="false" Visible="false"></asp:Button>
			<asp:TextBox id="txtConsecutivo3" Width="10px" Visible="False" runat="server"></asp:TextBox>
		</TD>
		<TD>
			<asp:CheckBox id="chkDesplegarUVR3" CssClass="textSmallBlack" runat="server" Text="UVR en Formato"
				Visible="false"></asp:CheckBox>
			<asp:TextBox id="txtIdProveedor3" runat="server" CssClass="textBox" Width="0px"></asp:TextBox>
		</TD>
	</TR>
</TABLE>
<asp:TextBox id="txtUVR" Width="0px" runat="server"></asp:TextBox>
