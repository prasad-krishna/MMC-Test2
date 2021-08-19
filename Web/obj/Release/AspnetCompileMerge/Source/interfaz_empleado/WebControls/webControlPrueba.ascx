<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="webControlPrueba.ascx.cs" Inherits="Web.interfaz_empleado.WebControls.webControlPrueba" %>
<script language="javascript">

    function ShowServicioProducto(sender, nametxtIdServicioProducto, idTipoServicio, nametxtNombreServicioProducto, nametxtValorServicioProducto, nametxtCantidad) {



        var valuetxtIdServicioProducto = document.getElementById('valuetxtIdServicioProducto');
        var valuetxtNombreServicioProducto = document.getElementById('valuetxtNombreServicioProducto');
        var valuetxtCantidad = document.getElementById('valuetxtCantidad');
        var txtTemp = document.getElementById('<%= this.txtTemp.ClientID %>');
        var valuetxtIdTipoServicio = document.getElementById('<%= this.txtIdTipoServicio.ClientID %>');
        var divLabel = document.getElementById('divLabel');
        var divLabelNombre = document.getElementById('divLabelNombre');
        var valuetxtValorServicioProducto = document.getElementById('valuetxtValorServicioProducto');

        valuetxtIdServicioProducto.value = nametxtIdServicioProducto;
        valuetxtIdTipoServicio.value = idTipoServicio;
        valuetxtNombreServicioProducto.value = nametxtNombreServicioProducto;
        valuetxtValorServicioProducto.value = nametxtValorServicioProducto;
        valuetxtCantidad.value = nametxtCantidad;

        txtTemp.value = nametxtNombreServicioProducto;

        if (idTipoServicio == "17" || idTipoServicio == "18") {
            divLabel.innerText = "Nombre";
            divLabelNombre.innerText = "Principio Activo";
        }
        else {
            divLabel.innerText = "Código as";
            divLabelNombre.innerText = "Nombre";
        }


        var left = Narg_GetPosX(sender) - 70;
        var top = Narg_GetPosY(sender) + 15;
        setTimeout('showLayer("BuscarServicioProducto",' + left + ',' + top + ');', 50);
        if (idTipoServicio == "17" || idTipoServicio == "18") {
            setTimeout('document.Form1.WC_BuscarServicioProducto1_txtCodigo.focus();', 51);
        }
        else {
            setTimeout('document.Form1.WC_BuscarServicioProducto1_txtNombre.focus();', 51);

        }

    }

    function DevolverServicioProducto(valor, id, valorSugerido) {
        var txtIdServicioProducto = document.getElementById(document.getElementById('valuetxtIdServicioProducto').value);
        var txtNombreServicioProducto = document.getElementById(document.getElementById('valuetxtNombreServicioProducto').value);
        var txtValorServicioProducto = document.getElementById(document.getElementById('valuetxtValorServicioProducto').value);
        var txtCantidad = document.getElementById(document.getElementById('valuetxtCantidad').value);

        if (txtNombreServicioProducto != null) {
            txtNombreServicioProducto.value = valor;
            txtNombreServicioProducto.title = valor;
            txtIdServicioProducto.value = id;
            document.getElementById("WC_BuscarServicioProducto1_txtNombre").value = "";
            document.getElementById("WC_BuscarServicioProducto1_txtCodigo").value = "";
            txtCantidad.focus();
            closeLayer('BuscarServicioProducto');
        }

    }	


</script>
<INPUT id="valuetxtNombreServicioProducto" type="hidden" size="50"><INPUT id="valuetxtIdServicioProducto" type="hidden">
<INPUT id="valuetxtValorServicioProducto" type="hidden" size="50"> <INPUT id="valuetxtCantidad" type="hidden" size="50">
<iframe id="ifrBuscarServicioProducto" style="DISPLAY: none; Z-INDEX: 1000; WIDTH: 320px; POSITION: absolute; HEIGHT: 300px"
	frameBorder="0" src="pagina.htm"></iframe>
<div id="dvBuscarServicioProducto" style="BORDER-RIGHT: dimgray 1px outset; BORDER-TOP: dimgray 1px outset; OVERFLOW-Y: auto; DISPLAY: none; Z-INDEX: 1100; BORDER-LEFT: dimgray 1px outset; WIDTH: 320px; BORDER-BOTTOM: dimgray 1px outset; POSITION: absolute; HEIGHT: 300px; BACKGROUND-COLOR: white">
	<table style="WIDTH: 100%" cellSpacing="0" cellPadding="0" border="0">
		<tr>
			<td vAlign="top" align="right" height="14"><asp:image id="imgCerrar" style="CURSOR: pointer" onclick="closeLayer('BuscarServicioProducto');"
					runat="server" ImageUrl="../../images/imgClose.gif"></asp:image></td>
		</tr>
		<tr>
			<td vAlign="top" align="center">
				<fieldset style="WIDTH: 95%">
					<legend>Búsqueda Rápida</legend>
					 
					<asp:UpdatePanel id="Ajaxpanel22" runat="server">
					<ContentTemplate>
						<TABLE style="WIDTH: 90%" cellSpacing="0" cellPadding="0" border="0">
							<TR>
								<TD align="left" width="30%">
									<DIV id="divLabel"></DIV>
								</TD>
								<TD align="left">
									<asp:TextBox id="txtCodigo" runat="server" Width="130px" CssClass="textBoxSmall"></asp:TextBox>&nbsp;
									<asp:TextBox id="txtTemp" runat="server" Width="0px"></asp:TextBox>
									<asp:TextBox id="txtIdTipoServicio" runat="server" Width="0px"></asp:TextBox></TD>
							</TR>
							<TR>
								<TD align="left">
									<DIV id="divLabelNombre"></DIV>
								</TD>
								<TD align="left">
									<asp:TextBox id="txtNombre" runat="server" Width="130px" CssClass="textBoxSmall"></asp:TextBox>
									<asp:LinkButton id="lnkBuscar" runat="server" CausesValidation="False">Buscar</asp:LinkButton></TD>
							</TR>
							<TR>
								<TD align="left" colSpan="2">
									<asp:Label id="lblResultado" runat="server" CssClass="textRed"></asp:Label>
									<asp:DataGrid id="dtgServicioProducto" runat="server" EnableViewState="False" AutoGenerateColumns="False"
										ShowHeader="False" PageSize="4">
										<Columns>
											<asp:TemplateColumn>
												<ItemTemplate>
													<asp:LinkButton CausesValidation="false" CssClass="textSmallBlue" id="lnkServicioProducto" Text='<%# DataBinder.Eval(Container, "DataItem.NombreCompleto") %>' runat="server" CommandName="seleccionar">
													</asp:LinkButton>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn Visible="False" DataField="IdServicioProducto"></asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="ValorConvenio"></asp:BoundColumn>
										</Columns>
										<PagerStyle Position="Top"></PagerStyle>
									</asp:DataGrid></TD>
							</TR>
							<TR>
								<TD>
									<asp:LinkButton id="lnkAdicionar" runat="server" CausesValidation="False" Visible="False">Adicionar</asp:LinkButton></TD>
							</TR>
						</TABLE>
						</ContentTemplate>
					</asp:UpdatePanel>
				</fieldset>
			</td>
		</tr>
	</table>
</div>
