<%@ Page language="c#" Codebehind="AE_migrarEmpleado.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_admon.forma.AE_migrarEmpleado" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>::.Mercer.::</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../css/admon.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../../scripts/Base.js" type="text/javascript"></script>
</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table id="Table1" cellspacing="1" border="0" cellpadding="0" width="100%" class="tableBorder">
                <tr>
                    <td class="titleBackBlue" colspan="4">
                        <asp:Label ID="lblTitulo" runat="server" Text="MIGRACIÓN DE HISTORIAS CLÍNICAS - EMPLEADOS"></asp:Label>
                    </td>
                </tr>
                <tr id = tr1 runat="server" style="height:35px">
                    <td>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr id = tr2 runat="server">                   
                    <td align="center" >
                        <table class="tableBorder" id="Table2" width="380px" cellpadding="3" align="center">
							<TR>
								<TD class="titleBackTop" colSpan="4">Empleado de Origen</TD>
							</TR>
							<TR>
								<TD colSpan="4">Realice la búsqueda por algunos de los siguientes criterios.</TD>
							</TR>
							<tr>
							    <td>
							        <br />
							    </td>
							</tr>
							<TR>
							    <td colspan="1"><b>Empresa origen:</b></td>
							    <td colspan="3"><b>Empresa 1</b></td>
                            </tr>
							<tr>
								<TD><STRONG>Nombre</STRONG></TD>
								<TD><STRONG><asp:textbox id="txtNombre" runat="server" CssClass="textBox" Width="180px"></asp:textbox>
										<asp:TextBox id="hdnLiquidacionValue" runat="server" Width="0px"></asp:TextBox><asp:TextBox id="hdnPermisos" runat="server" Width="0px"></asp:TextBox>
										<asp:TextBox id="hdnRealizaAutorizaciones" runat="server" Width="0px"></asp:TextBox>
										<asp:TextBox id="hdnRealizaReembolsos" runat="server" Width="0px"></asp:TextBox>
										<asp:TextBox id="hdnRealizaConsultas" runat="server" Width="0px"></asp:TextBox>
										<asp:TextBox id="hdnRealizaCitas" runat="server" Width="0px"></asp:TextBox></STRONG></TD>
							</TR>
							<tr>
								<TD></TD>
								<td></td>
							</tr>
							<TR>
								<TD><STRONG>Identificación</STRONG></TD>
								<TD><STRONG><asp:textbox id="txtIdentificacion" runat="server" CssClass="textBox" Width="180px"></asp:textbox><INPUT id="hdnId" type="hidden" value="???" name="hdnId"><INPUT id="hdnIdBene" type="hidden" value="??" name="hdnId"><INPUT id="hdnLiquidacion" type="hidden" value="XXX" name="hdnLiquidacion"></STRONG></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD colSpan="4">
									<P align="center"><asp:button id="Bfinder" runat="server" CssClass="button" 
                                            Text="Buscar"></asp:button></P>
								</TD>
							</TR>
						</table>
                    </td>
                    <td align="center">
                        <table class="tableBorder" id="Table3" width="380px" cellpadding="3" align="center" style="padding-top:0">
							<TR>
								<TD class="titleBackTop" colSpan="4">Empleado Destino</TD>
							</TR>
							<TR>
								<td colspan="4">Realice la búsqueda por algunos de los siguientes criterios.</TD>
							</TR>
							<TR>
							    <td>
							        <br />
							    </td>
                            </tr>
                            <tr id = tr3 runat="server" >
                                <td colspan="1"><b>Empresa destino:</b></td>
                                <td colspan="2"><asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="true" Width="80%"
                                        CssClass="textBox">
                                        <asp:ListItem Selected="True" Value="0">--Seleccione--</asp:ListItem>
                                        <asp:ListItem Value="1">Empresa 1</asp:ListItem>
                                        <asp:ListItem Value="2">Empresa 2</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
								<TD><STRONG>Nombre</STRONG></TD>
								<TD><STRONG><asp:textbox id="Textbox1" runat="server" CssClass="textBox" Width="180px"></asp:textbox>
										<asp:TextBox id="TextBox2" runat="server" Width="0px"></asp:TextBox><asp:TextBox id="TextBox3" runat="server" Width="0px"></asp:TextBox>
										<asp:TextBox id="TextBox4" runat="server" Width="0px"></asp:TextBox>
										<asp:TextBox id="TextBox5" runat="server" Width="0px"></asp:TextBox>
										<asp:TextBox id="TextBox6" runat="server" Width="0px"></asp:TextBox>
										<asp:TextBox id="TextBox7" runat="server" Width="0px"></asp:TextBox></STRONG></TD>
							</TR>
							<tr>
								<TD></TD>
								<td></td>
							</tr>
							<TR>
								<TD><STRONG>Identificación</STRONG></TD>
								<TD><STRONG><asp:textbox id="Textbox8" runat="server" CssClass="textBox" Width="180px"></asp:textbox><INPUT id="Hidden1" type="hidden" value="???" name="hdnId"><INPUT id="Hidden2" type="hidden" value="??" name="hdnId"><INPUT id="Hidden3" type="hidden" value="XXX" name="hdnLiquidacion"></STRONG></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD colSpan="4">
									<P align="center"><asp:button id="Button1" runat="server" CssClass="button" 
                                            Text="Buscar"></asp:button></P>
								</TD>
							</TR>
						</table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:datagrid id="Datagrid1" runat="server" CssClass="grid" Width="99%" CellPadding="3" AllowPaging="True"
							AutoGenerateColumns="false" GridLines="Horizontal" PageSize="20">
							<AlternatingItemStyle CssClass="altItems"></AlternatingItemStyle>
							<ItemStyle CssClass="norItems"></ItemStyle>
							<HeaderStyle CssClass="headerGrid"></HeaderStyle>
							<Columns>
							    <asp:TemplateColumn HeaderText="">
									<ItemStyle HorizontalAlign="Center" Width="2%"></ItemStyle>
									<ItemTemplate>
										<asp:RadioButton ID="RowSelector" runat="server" GroupName="SelectGroup" />
									</ItemTemplate>
								</asp:TemplateColumn>
							
							    <asp:BoundColumn DataField="id_empleado" HeaderText="IdEmpleado" Visible="False" ></asp:BoundColumn>
								<asp:BoundColumn DataField="identificacion" HeaderText="Identificaci&#243;n">
									<ItemStyle HorizontalAlign="Center" Width="30%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="nombre_completo" HeaderText="Nombre"><ItemStyle HorizontalAlign="Center" Width="30%"></ItemStyle></asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Center" BackColor="White" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
                    </td>
                    <td>
                        <asp:datagrid id="Datagrid2" runat="server" CssClass="grid" Width="99%" CellPadding="3" AllowPaging="True"
							AutoGenerateColumns="false" GridLines="Horizontal" PageSize="20">
							<AlternatingItemStyle CssClass="altItems"></AlternatingItemStyle>
							<ItemStyle CssClass="norItems"></ItemStyle>
							<HeaderStyle CssClass="headerGrid"></HeaderStyle>
							<Columns>
							    <asp:TemplateColumn HeaderText="">
									<ItemStyle HorizontalAlign="Center" Width="2%"></ItemStyle>
									<ItemTemplate>
										<asp:RadioButton ID="RowSelector" runat="server" GroupName="SelectGroup" />
									</ItemTemplate>
								</asp:TemplateColumn>
							
							    <asp:BoundColumn DataField="id_empleado" HeaderText="IdEmpleado" Visible="False" ></asp:BoundColumn>
								<asp:BoundColumn DataField="identificacion" HeaderText="Identificaci&#243;n">
									<ItemStyle HorizontalAlign="Center" Width="30%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="nombre_completo" HeaderText="Nombre"><ItemStyle HorizontalAlign="Center" Width="30%"></ItemStyle></asp:BoundColumn>								
							</Columns>
							<PagerStyle HorizontalAlign="Center" BackColor="White" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
                    </td>
                </tr>
                <tr style="height:60px">
                    <td colspan="3">
                        <P align="center"><asp:button Enabled="false" id="Aceptar" runat="server" OnClientClick="javascript:confirm('¿Desea realizar la migración de empleados: \n\nOrigen: Nombre 3 Apellido 3 - 555-7890\n\nDestino: Nombre 2 Apellido 2 - 555-4567');" CssClass="button" Text="Aceptar"></asp:button></P>
                    </td>
                </tr>
                <tr>
                    <td class="textBlue" colspan="4" align="center">
                        <b><asp:Label ID="Label1" runat="server" Text="Proceso finalizado. Resultado de la migración:"></asp:Label></b>
                        <br />
                        <br />
                        
                        <table runat="server" class="textBlue">
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Empleado origen: <b>Nombre 3 Apellido 3 - 555-7890</b>"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Empleado destino: <b>Nombre 2 Apellido 2 - 555-4567</b>"></asp:Label>
                            </td>
                        </tr>
                       <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Consultas migradas: <b>16</b>"></asp:Label>
                            </td>
                        </tr>
                        </table>
                    </td>
                </tr>
			</table>
			&nbsp;
		</form>
	</body>
</HTML>
