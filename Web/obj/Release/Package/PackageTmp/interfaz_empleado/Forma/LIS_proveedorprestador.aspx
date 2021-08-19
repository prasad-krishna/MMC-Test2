<%@ Page language="c#" Codebehind="LIS_proveedorprestador.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_admon.forma.LIS_proveedorprestador" %>
<HTML>
	<HEAD>
		<title>HC-Historias Clínicas</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../css/admon.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../../scripts/Base.js" type="text/javascript"></script>
	</HEAD>
	<body onload="CargarConfiguracion()">
		<form id="Form1" method="post" runat="server">
			<table class="GG" cellSpacing="0" cellPadding="10" width="100%" align="center">
				<TR>
					<TD colSpan="2">
						<TABLE class="tableBorder" id="Table2" cellSpacing="0" cellPadding="5" width="70%" align="center">
							<TR>
								<TD class="titleBackBlue" background="../../iconos/fondo_main.PNG" colSpan="4">Buscador 
									Prestadores/Solicitantes</TD>
							</TR>
							<TR>
								<TD width="20%">Nombre</TD>
								<TD>
									<asp:textbox id="Fnombre" runat="server" Width="198px" CssClass="textBox"></asp:textbox></TD>
								<TD width="20%">Especialidad</TD>
								<TD>
									<asp:DropDownList id="ddlEspecialidad" runat="server" Width="200px" CssClass="textBoxSmall"></asp:DropDownList></TD>
							</TR>
							<TR>
								<TD colSpan="4">
									<P align="center">
										<asp:button id="btnBuscar" runat="server" CssClass="Button" Text="Buscar"></asp:button></P>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<tr>
					<td><A>
							<asp:imagebutton id="imbAdicionar" runat="server" ImageUrl="../../iconos/ico_adicionar.gif" OnClick="imbAdicionar_Click"></asp:imagebutton></A>
						<asp:linkbutton id="lnkAdicionar" runat="server">Adicionar Prestador/Solicitante</asp:linkbutton>&nbsp;&nbsp;&nbsp; 
						&nbsp; <label id="Fcount" runat="server"></label>
					</td>
				</tr>
				<TR>
					<TD align="center" colSpan="2"><asp:datagrid id="dtgBusqueda" runat="server" CssClass="grid" Width="100%" CellPadding="3" GridLines="Horizontal"
							AutoGenerateColumns="False" AllowPaging="True" PageSize="20">
							<ALTERNATINGITEMSTYLE CssClass="altItems"></ALTERNATINGITEMSTYLE>
							<ITEMSTYLE CssClass="norItems"></ITEMSTYLE>
							<HEADERSTYLE CssClass="headerGrid"></HEADERSTYLE>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="IdPrestador"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="IdProveedor"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Editar">
									<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
									<ItemTemplate>
										<asp:imagebutton BackColor="Transparent" id="imgEditarMed" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.IdProveedor") %>' runat="server" ImageUrl="../../iconos/ico_editar.gif">
										</asp:imagebutton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="NombreProveedor" HeaderText="Nombre">
									<ItemStyle Width="20%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="NombreEspecialidad" HeaderText="Especialidad">
									<ItemStyle Width="15%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Telefonos" HeaderText="Telefonos">
									<ItemStyle Width="15%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Direcciones" HeaderText="Direcciones">
									<ItemStyle Width="15%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="NombreEstado" HeaderText="Estado">
									<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="NombreCiudad" HeaderText="Ciudad">
									<ItemStyle Width="8%"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<PAGERSTYLE Mode="NumericPages"></PAGERSTYLE>
						</asp:datagrid>&nbsp;</TD>
				</TR>
			</table>
			&nbsp;
		</form>
	</body>
</HTML>
