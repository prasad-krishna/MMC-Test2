<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LIS_lineanegocio.aspx.cs" Inherits="TPA.interfaz_admon.forma.LIS_lineanegocio" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>HC-Historias Clínicas</title>
    <link href="../../css/admon.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../../scripts/Base.js" type="text/javascript"></script>
</head>
	<body onload="CargarConfiguracion();">
		<form id="Form1" method="post" runat="server">
			<table class="GG" cellSpacing="0" cellPadding="10" width="100%" align="center">
				<TR>
					<TD>
						<TABLE class="tableBorder" id="Table2" cellSpacing="0" cellPadding="5" width="270px" align="center">
							<TR>
								<TD class="titleBackBlue" background="../../iconos/fondo_main.PNG" >Buscador 
									de Líneas de Negocio</TD>
							</TR>
							<TR>
								<TD>Nombre Línea de Negocio</TD>
							</TR>
							<TR>
								<TD><asp:textbox id="txtNombreLineaNegocio" runat="server" CssClass="textBox" Width="260px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>
									<P align="center"><asp:button id="btnBuscar" runat="server" CssClass="Button" 
                                            Text="Buscar" onclick="btnBuscar_Click"></asp:button></P>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>Solamente se listan las líneas de negocio que cumplen con los criterios 
                        de búsqueda. </TD>
				</TR>
				<TR>
					<TD><asp:label id="lblMensaje" runat="server" CssClass="textoDestacado"></asp:label><BR>
						<a>
							<asp:imagebutton id="imbAdicionar" runat="server" 
                            ImageUrl="../../iconos/ico_adicionar.gif" onclick="imbAdicionar_Click1"></asp:imagebutton>&nbsp;</a>
						<asp:linkbutton id="lnkAdicionar" runat="server" onclick="lnkAdicionar_Click1">Adicionar Línea de Negocio</asp:linkbutton></TD>
				</TR>
				<TR>
					<TD align="center"><asp:datagrid id="grvList" runat="server" CssClass="grid" Width="500px" AllowPaging="True" CellPadding="3"
							AutoGenerateColumns="False" PageSize="20">
							<AlternatingItemStyle CssClass="altItems"></AlternatingItemStyle>
							<ItemStyle CssClass="norItems"></ItemStyle>
							<HeaderStyle CssClass="headerGrid"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="Editar">
									<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
									<ItemTemplate>
										<asp:imagebutton BackColor="Transparent" id="imgEditarLineaNegocio" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.IdLineaNegocio") %>' runat="server" ImageUrl="../../iconos/ico_editar.gif">
										</asp:imagebutton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Activa">
									<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblActiva" runat="server" Text='<%# (Convert.ToBoolean(DataBinder.Eval(Container, "DataItem.activa")) == true ? "Si" : "No") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="NombreLineaNegocio" HeaderText="Nombre Linea de Negocio">
									<ItemStyle HorizontalAlign="Center" Width="22%"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Center" CssClass="pagerGrid" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>&nbsp;</TD>
				</TR>
				<TR>
					<TD align="right">Cantidad de Líneas de Negocio
						<asp:label id="lblCount" runat="server" Font-Bold="True"></asp:label></TD>
				</TR>
			</table>
			&nbsp;
		</form>
	</body>
</html>
