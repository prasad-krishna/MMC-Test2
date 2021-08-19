<%@ Page language="c#" Codebehind="AE_empresasusuarios.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_admon.forma.AE_empresasusuarios" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title></title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../css/admon.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../../scripts/Base.js" type="text/javascript"></script>
		<script language="javascript" src="../../scripts/Validaciones.js" type="text/javascript"></script>
		<script language="javascript" type="text/javascript">
		    function EliminarAsociación(EmpresaID) {
		        if (confirm("¿Esta seguro que quiere eliminar la asociación de la empresa?")) {
		            window.location = "AE_AsociarEmpresas.aspx?EmpresaID=" + EmpresaID;
		        }
		    }
		</script>
	    <style type="text/css">
            .style4
            {
            }
            .style5
            {
                width: 1%;
                height: 19px;
            }
            .style6
            {
                height: 19px;
            }
        </style>
	</HEAD>
	<body onload="CargarConfiguracion()">
		<form id="Form1" method="post" runat="server">
			<TABLE id="tblPrincipal" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td>
						<TABLE class="tableBorder" id="table6" width="100%" align="center">
							<tr>
								<td>
									<TABLE id="Table1" cellSpacing="0" cellPadding="3" width="100%" border="0">
										<TR>
											<TD class="titleBackBlue" colSpan="2">
                                                <asp:Label ID="lblTitulo" runat="server" 
                                                    Text="ASIGNAR Y/O QUITAR PERMISOS A USUARIOS"></asp:Label></TD>
										</TR>
										<TR>
											<TD colSpan="2">&nbsp;&nbsp;</TD>
										</TR>
										<TR>
											<TD class="style5">
                                                <asp:Label ID="lblUsuario" runat="server" Text="Usuario:"></asp:Label>
                                            </TD>
											<TD width="35%" class="style6">
                                                <asp:Label ID="lblNombreUsuario" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtIdPrestador" runat="server" Width="0px"></asp:TextBox>
                                            </TD>
										</TR>
										<TR>
											<TD class="style4">
                                                &nbsp;</TD>
											<TD width="35%">
                                                &nbsp;&nbsp;</TD>
										</TR>
										<TR>
											<TD colspan="2" class="textSmallBlack">
                                                <asp:Button ID="btnAceptar" CssClass="button" runat="server" Text="Aceptar" 
                                                    onclick="btnAceptar_Click" />
                                            &nbsp;
                                                <asp:Button ID="btnRegresar" CssClass="button" runat="server" Text="Regresar" 
                                                    onclick="btnRegresar_Click" />
                                            </TD>
										</TR>
										<TR>
											<TD colspan="2" class="textSmallBlack">
                                                &nbsp;&nbsp;</TD>
										</TR>
										<TR>
											<TD colspan="2" >
                                                <asp:Label ID="lblCamposObligatorios" runat="server" 
                                                    
                                                    
                                                    
                                                    
                                                    Text="Nota: Recuerde que al deseleccionar una empresa el usuario perderá sus permisos" 
                                                    Font-Bold="False" ForeColor="Red"></asp:Label>
                                            </TD>
										</TR>
										<TR>
											<TD class="style4" colspan="2">
                                                <asp:CheckBox ID="chkSeleccionarTodos" runat="server" 
                                                    Text="Seleccionar todos" AutoPostBack="True" 
                                                    oncheckedchanged="chkSeleccionarTodos_CheckedChanged" />
                                            </TD>
										</TR>
										<TR>
											<TD colSpan="2" height="20">
                                                <asp:GridView ID="gvEmpresas" runat="server" AllowSorting="True" 
                                                    AutoGenerateColumns="False" CssClass="grid" Width="70%" >
                                                    <AlternatingRowStyle CssClass="altItems"/>
                                                   <RowStyle CssClass="norItems" />							
							                        <HeaderStyle CssClass="headerGrid"></HeaderStyle>
                                                    <Columns>                                                                                                         
                                                    <asp:TemplateField HeaderText="Habilitar">
                                                    <ItemStyle Width=10% />
                                                    <ItemTemplate>                                                    
                                                        <asp:CheckBox runat="server" ID="chkHabilitar" />
                                                    </ItemTemplate>
                                                    </asp:TemplateField>    
                                                    <asp:TemplateField HeaderText="Nombre de la empresa">
                                                     <ItemStyle Width=90% />
                                                    <ItemTemplate>                                                    
                                                        <asp:Label runat="server" ID="lblNombreEmpresa" Text=<%# DataBinder.Eval(Container.DataItem, "nombre")%>></asp:Label>
                                                    </ItemTemplate>                                                    
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField Visible=false>                                                     
                                                    <ItemTemplate>                                                    
                                                        <asp:Label runat="server" ID="lblEmpresaId" Text=<%# DataBinder.Eval(Container.DataItem, "empresa_id")%>></asp:Label>
                                                    </ItemTemplate>
                                                    </asp:TemplateField>                                                                                                  
                                                    <asp:TemplateField Visible=false>                                                     
                                                    <ItemTemplate>                                                    
                                                        <asp:Label runat="server" ID="lblHabilitar" Text=<%# DataBinder.Eval(Container.DataItem, "Habilitar")%>></asp:Label>
                                                    </ItemTemplate>
                                                    </asp:TemplateField>                                                                                                  
                                                    </Columns>
                                                </asp:GridView>                                                
                                            </TD>
										</TR>
										<TR>
											<TD colSpan="2" height="20">
                                                &nbsp;&nbsp;</TD>
										</TR>
										<TR>
											<TD colSpan="2" height="20">
                                                &nbsp;&nbsp;</TD>
										</TR>
										</TABLE>
								</td>
							</tr>
						</TABLE>
					</td>
				</tr>
			</TABLE>
			&nbsp;
		</form>
	</body>
</HTML>
