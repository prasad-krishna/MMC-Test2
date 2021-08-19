<%@ Control Language="c#" AutoEventWireup="false" Codebehind="WC_AdicionarDiagnosticoConsultaCIE10.ascx.cs" Inherits="TPA.interfaz_empleado.WebControls.WC_AdicionarDiagnosticoConsultaCIE10" %>
        <style type="text/css">
            .textBoxOculto {
                display:none !important;
            }
        </style>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">

	<TR>
		<TD>
			<asp:TextBox id="txtDiagnostico1" runat="server" CssClass="textBoxSmall" Width="100px"></asp:TextBox>
			<asp:Button id="btnBuscarDiagnostico1" runat="server" CssClass="buttonSmall" Width="20px" CausesValidation="false"
				Text="..." ToolTip="Seleccione para buscar el diagnóstico"></asp:Button>
		</TD>
		<TD>
			<asp:TextBox id="txtIdDiagnostico1" runat="server" CssClass="textBox textBoxOculto" Width="0px" ></asp:TextBox>
		</TD>
	</TR>
	</TABLE>
