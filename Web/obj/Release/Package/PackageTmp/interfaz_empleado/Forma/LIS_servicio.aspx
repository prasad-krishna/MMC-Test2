<%@ Page language="c#" Codebehind="LIS_servicio.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_admon.forma.LIS_servicio" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>HC-Historias Clínicas</title>
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
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
					<TD>
						<table width="100%" cellpadding="0" cellspacing="0" border="0">
							<tr>
								<td>
									<TABLE class="tableBorder" id="Table2" cellSpacing="0" cellPadding="5" width="60%" align="center">
										<TR>
											<TD class="titleBackBlue" background="../../iconos/fondo_main.PNG" colSpan="4">
												Buscador Servicio</TD>
										</TR>
										<TR>
											<TD width="20%">Nombre</TD>
											<TD><asp:textbox id="txtNombre" runat="server" CssClass="textBox" Width="198px"></asp:textbox>
											</TD>
											<TD width="20%">Tipo</TD>
											<TD><asp:DropDownList id="ddlTipoServicio" runat="server" CssClass="textBox" Width="198px"></asp:DropDownList>
											</TD>
										</TR>
										<TR>
											<TD width="20%">Código servicio</TD>
											<TD><asp:textbox id="txtCodigoServicio" runat="server" CssClass="textBox" Width="198px"></asp:textbox>
											</TD>
											<TD width="20%"></TD>
											<TD>
											</TD>
										</TR>
										<TR>
											<TD colSpan="4">
												<P align="center"><asp:button id="btnBuscar" runat="server" CssClass="Button" 
                                                        Text="Buscar"></asp:button></P>
											</TD>
										</TR>
									</TABLE>
								</td>
							</tr>
						</table>
					</TD>
				</TR>
				<tr>
					<td>Solamente se listan los primeros 50 servicios que cumplen con los criterios de 
                        búsqueda.
					</td>
				</tr>
				<tr>
					<td><A>
							<asp:imagebutton id="imbAdicionar" runat="server" ImageUrl="../../iconos/ico_adicionar.gif"></asp:imagebutton></A>
						<asp:linkbutton id="lnkAdicionar" runat="server">Adicionar Servicio</asp:linkbutton>
						<label id="Fcount" runat="server"></label>
					</td>
				</tr>
				<TR>
					<TD align="center"><asp:datagrid id="dtgServicios" runat="server" CssClass="grid" Width="98%" AllowPaging="True"
							CellPadding="2" AutoGenerateColumns="False" PageSize="20" PagerStyle-Mode="NextPrev" PagerStyle-NextPageText="Next ->"
							PagerStyle-PrevPageText="<- Previous" PagerStyle-Font-Bold="True">
							<AlternatingItemStyle CssClass="altItems"></AlternatingItemStyle>
							<ItemStyle CssClass="norItems"></ItemStyle>
							<HeaderStyle CssClass="headerGrid"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="Editar">
									<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
									<ItemTemplate>
										<asp:imagebutton BackColor="Transparent" id="imgEditarMed" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.IdServicio") %>' runat="server" ImageUrl="../../iconos/ico_editar.gif">
										</asp:imagebutton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="CodigoServicio" HeaderText="C&#243;digo de servicio">
									<ItemStyle Width="10%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="NombreServicio" HeaderText="Nombre">
									<ItemStyle Width="40%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ValorConvenio" HeaderText="Valor del convenio">
									<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Activo">
									<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblActivo" runat="server" Text='<%# (Convert.ToBoolean(DataBinder.Eval(Container, "DataItem.Activo")) == true ? "Si" : "No") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle NextPageText="Next -&gt;" Font-Bold="True" PrevPageText="&lt;- Previous" HorizontalAlign="Center"
								CssClass="pagerGrid" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>&nbsp;</TD>
				</TR>
			</table>
			&nbsp;
		</form>
	</body>
</HTML>
