<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="../WebControls/WC_Menu.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_Encabezado" Src="../WebControls/WC_Encabezado.ascx" %>
<%@ Page language="c#" Codebehind="LIS_medicamento.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_admon.forma.LIS_medicamento" %>
<%@ Register TagPrefix="uc1" TagName="WC_Pie" Src="../WebControls/WC_Pie.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
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
	<body onload="CargarConfiguracion();">
		<form id="Form1" method="post" runat="server">
			<table class="GG" cellSpacing="0" cellPadding="10" width="100%" align="center">
				<TR>
					<TD>
						<TABLE class="tableBorder" id="Table2" cellSpacing="0" cellPadding="5" width="60%" align="center">
							<TR>
								<TD class="titleBackBlue" background="../../iconos/fondo_main.PNG" colSpan="2">Buscador 
									de Medicamentos</TD>
							</TR>
							<TR>
								<TD>Nombre Medicamento</TD>
								<TD>Laboratorio</TD>
							</TR>
							<TR>
								<TD><asp:textbox id="Fnombre" runat="server" CssClass="textBox" Width="198px"></asp:textbox></TD>
								<TD><asp:dropdownlist id="Flaboratory" runat="server" CssClass="textBox" 
                                        Width="100px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD colspan="2">Principio Activo</TD>
							</TR>
							<TR>
								<TD colspan="2"><asp:textbox id="Fprincipio_activo" runat="server" CssClass="textBox" Width="198px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD colSpan="2">
									<P align="center"><asp:button id="Bfinder" runat="server" CssClass="Button" Text="Buscar"></asp:button></P>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>Solamente se listan los primeros 60 medicamentos que cumplen con los criterios 
                        de búsqueda. </TD>
				</TR>
				<TR>
					<TD><asp:label id="lblMensaje" runat="server" CssClass="textoDestacado"></asp:label><BR>
						<a>
							<asp:imagebutton id="imbAdicionar" runat="server" ImageUrl="../../iconos/ico_adicionar.gif"></asp:imagebutton>&nbsp;</a>
						<asp:linkbutton id="lnkAdicionar" runat="server">Adicionar Medicamento</asp:linkbutton></TD>
				</TR>
				<TR>
					<TD align="center"><asp:datagrid id="Flist" runat="server" CssClass="grid" Width="98%" AllowPaging="True" CellPadding="3"
							AutoGenerateColumns="False" PageSize="20">
							<AlternatingItemStyle CssClass="altItems"></AlternatingItemStyle>
							<ItemStyle CssClass="norItems"></ItemStyle>
							<HeaderStyle CssClass="headerGrid"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="Editar">
									<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
									<ItemTemplate>
										<asp:imagebutton BackColor="Transparent" id="imgEditarMed" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.IdMedicamento") %>' runat="server" ImageUrl="../../iconos/ico_editar.gif">
										</asp:imagebutton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Activo">
									<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblActivo" runat="server" Text='<%# (Convert.ToBoolean(DataBinder.Eval(Container, "DataItem.Activo")) == true ? "Si" : "No") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="NombreComercial" HeaderText="Nombre Comercial">
									<ItemStyle HorizontalAlign="Center" Width="22%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="NombreLaboratorio" HeaderText="Laboratorio">
									<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PrincipioActivo" HeaderText="Principio Activo">
									<ItemStyle HorizontalAlign="Center" Width="22%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Presentacion" HeaderText="Presentaci&#243;n">
									<ItemStyle HorizontalAlign="Center" Width="12%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Concentracion" HeaderText="Concentraci&#243;n">
									<ItemStyle HorizontalAlign="Center" Width="12%"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Center" CssClass="pagerGrid" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>&nbsp;</TD>
				</TR>
				<TR>
					<TD align="right">Cantidad de&nbsp; Medicamentos
						<asp:label id="Fcount" runat="server" Font-Bold="True"></asp:label></TD>
				</TR>
			</table>
			&nbsp;
		</form>
	</body>
</HTML>
